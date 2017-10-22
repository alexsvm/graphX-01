using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using GraphX.Controls;
using GraphX.Controls.Models;
using QuickGraph;

/*
r0: x2-r5, x3-r1, x4-r3
r1: x1-r2
r2: x3-r3, x7-r3
r3: x1-r10, x5-r4
r4: x6-r10
r5: x2-r9, x5-r6, x7-r6
r6: x4-r7
r7: x6-r8
r8: x0-r10
r9: x0-r8
 */

namespace WindowsFormsProject
{
    using Link = Dictionary<string, string>;

    public partial class Form1 : Form
    {
        //List<fsm.State> states;
        fsm.Parser parser;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            //states = new List<fsm.State>();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            wpfHost.Child = GenerateWpfVisuals();
            _zoomctrl.ZoomToFill();
        }

        private ZoomControl _zoomctrl;
        private GraphAreaExample _gArea;

        private UIElement GenerateWpfVisuals()
        {
            _zoomctrl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(_zoomctrl, Visibility.Visible);
            var logic = new GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>();
            _gArea = new GraphAreaExample
            {
                // EnableWinFormsHostingMode = false,
                LogicCore = logic,
                EdgeLabelFactory = new DefaultEdgelabelFactory()
            };
            _gArea.ShowAllEdgesLabels(true);
            _gArea.ShowAllEdgesArrows(true);
            logic.Graph = GenerateGraph();
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            //((LinLogLayoutParameters)logic.DefaultLayoutAlgorithmParams). = 100;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams = logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            logic.AsyncAlgorithmCompute = false;
            _zoomctrl.Content = _gArea;

            _gArea.SetVerticesMathShape(VertexShape.Triangle);
            _gArea.RelayoutFinished += gArea_RelayoutFinished;


            var myResourceDictionary = new ResourceDictionary {Source = new Uri("Templates\\template.xaml", UriKind.Relative)};
            //var myResourceDictionary = new ResourceDictionary { Source = new Uri("Templates\\Mini\\CommonMiniTemplate.xaml", UriKind.Relative) };
            _zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);
            _gArea.ShowAllEdgesArrows(true);

            return _zoomctrl;
        }

        void gArea_RelayoutFinished(object sender, EventArgs e)
        {
            _zoomctrl.ZoomToFill();
        }

        private GraphExample GenerateGraph()
        {
            var links = new Dictionary<string, Link>();
            HashSet<string> hstates = new HashSet<string>();

            for (int i = 0; i <= 10; i++)
            {
                hstates.Add(string.Format("r{0}", i));
                links.Add(string.Format("r{0}", i), new Link());
            }
            links["r0"].Add("x2", "r5");
            links["r0"].Add("x3", "r1");
            links["r0"].Add("x4", "r3");
            links["r1"].Add("x1", "r2");
            links["r2"].Add("x3", "r3");
            links["r2"].Add("x7", "r3");
            links["r3"].Add("x1", "r10");
            links["r3"].Add("x5", "r4");
            links["r4"].Add("x6", "r10");
            links["r4"].Add("x0", "r0");
            links["r5"].Add("x2", "r9");
            links["r5"].Add("x5", "r6");
            links["r5"].Add("x7", "r6");
            links["r6"].Add("x4", "r7");
            links["r7"].Add("x6", "r8");
            links["r8"].Add("x0", "r10");
            links["r9"].Add("x0", "r8");

            var dataGraph = new GraphExample();
            foreach (string vs in hstates)
            {
                var dataVertex = new DataVertex(vs);
                dataGraph.AddVertex(dataVertex);
            }
            var vlist = dataGraph.Vertices.ToList();
            DataEdge edge;
            //Then create two edges optionaly defining Text property to show who are connected
            foreach (string state in hstates)
            {
                if (links.ContainsKey(state))
                    foreach (KeyValuePair<string, string> link in links[state])
                    {
                        var vA = vlist.Find(x => x.Text.Equals(state));
                        var vB = vlist.Find(x => x.Text.Equals(link.Value));
                        edge = new DataEdge(vA, vB) { Text = link.Key };
                        
                        dataGraph.AddEdge(edge);
                    }
            }
            return dataGraph;
        }

        private void but_generate_Click(object sender, EventArgs e)
        {
            _gArea.GenerateGraph(true);
            _gArea.SetVerticesDrag(true, true);
            _zoomctrl.ZoomToFill();
        }

        private void but_reload_Click(object sender, EventArgs e)
        {
            _gArea.RelayoutGraph();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var links = new Dictionary<string, Link>();
            var states = new HashSet<string>();
            string initState = "";
            string finalState = "";
            for (int i = 0; i < txtFSMTable.Lines.Count(); i++)
            {
                string line = txtFSMTable.Lines[i];
                string[] StateAndLinks = line.Split(':');
                string StateName = StateAndLinks[0].Trim(' ');
                states.Add(StateName);
                if (i == 0)
                    initState = StateName;
                if (i == txtFSMTable.Lines.Count() - 1)
                    finalState = StateName;
                links.Add(StateName, new Link());

                if (StateAndLinks[1] != "") // Парсим переходы к другим состояниям
                {
                    foreach (string LinkPair in StateAndLinks[1].Split(','))
                    {
                        string[] SymAndDestState = LinkPair.Split('-');
                        string Symbol = SymAndDestState[0].Trim(' ').Substring(1); //.Substring(1);
                        string DestState = SymAndDestState[1].Trim(' ');
                        states.Add(DestState);
                        links[StateName].Add(Symbol, DestState);
                    }
                }

            }
            parser = new fsm.Parser(states, links, initState, finalState);
            var dataGraph = new GraphExample();
            foreach (string vs in states)
            {
                var dataVertex = new DataVertex(vs);
                dataGraph.AddVertex(dataVertex);
            }
            var vlist = dataGraph.Vertices.ToList();
            DataEdge edge;
            //Then create two edges optionaly defining Text property to show who are connected
            foreach (string state in states)
            {
                if (links.ContainsKey(state))
                    foreach (KeyValuePair<string, string> link in links[state])
                    {
                        var vA = vlist.Find(x => x.Text.Equals(state));
                        var vB = vlist.Find(x => x.Text.Equals(link.Value));
                        edge = new DataEdge(vA, vB) { Text = link.Key };
                        dataGraph.AddEdge(edge);
                    }
            }
            _gArea.LogicCore.Graph = dataGraph;
            _gArea.GenerateGraph(true);
            _gArea.RelayoutGraph();
            _gArea.SetVerticesDrag(true, true);
            _zoomctrl.ZoomToFill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (parser == null)
                return;
            if (parser.ParseString(txtString.Text))
                txtLog.Text += string.Format("Строка '{0}' соответствует правилам.", txtString.Text) + Environment.NewLine;
            else
                txtLog.Text += string.Format("Строка '{0}' не соответствует правилам!", txtString.Text) + Environment.NewLine;
        }
    }
}
