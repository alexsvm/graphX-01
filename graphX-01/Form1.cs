﻿using System;
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

namespace WindowsFormsProject
{
    using Link = Dictionary<string, string>;

    public partial class Form1 : Form
    {
        List<fsm.State> states;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            states = new List<fsm.State>();
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

            
           // var myResourceDictionary = new ResourceDictionary {Source = new Uri("Templates\\template.xaml", UriKind.Relative)};
           // _zoomctrl.Resources.MergedDictionaries.Add(myResourceDictionary);

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

            hstates.Add("q0");
            hstates.Add("q1");
            hstates.Add("q2");
            hstates.Add("q3");
            hstates.Add("q3");
            links.Add("q0", new Link());
            links["q0"].Add("x1", "q1");
            links["q0"].Add("x2", "q3");
            links.Add("q1", new Link());
            links["q1"].Add("x1", "q3");
            links["q1"].Add("x3", "q2");
            links.Add("q2", new Link());
            links["q2"].Add("x1", "q3");

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
            //FOR DETAILED EXPLANATION please see SimpleGraph example project
            //var dataGraph = new GraphExample();
            //for (int i = 1; i < 10; i++)
            //{
            //    var dataVertex = new DataVertex("q " + i);
            //    dataGraph.AddVertex(dataVertex);
            //}
            //var vlist = dataGraph.Vertices.ToList();
            ////Then create two edges optionaly defining Text property to show who are connected
            //var dataEdge = new DataEdge(vlist[0], vlist[1]) { Text = string.Format("{0} -> {1}", vlist[0], vlist[1]) };
            //dataGraph.AddEdge(dataEdge);
            //dataEdge = new DataEdge(vlist[2], vlist[3]) { Text = string.Format("{0} -> {1}", vlist[2], vlist[3]) };
            //dataGraph.AddEdge(dataEdge);
            //dataEdge = new DataEdge(vlist[2], vlist[2]) { Text = string.Format("{0} -> {1}", vlist[2], vlist[2]) };
            //dataGraph.AddEdge(dataEdge);
            //return dataGraph;
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

        private void button1_Click(object sender, EventArgs e)
        {
            states.Add(new fsm.State(0, "q0"));
            states.Add(new fsm.State(1, "q1"));
            states.Add(new fsm.State(2, "q2"));
            states.Add(new fsm.State(3, "q3"));
            states.Add(new fsm.State(4, "q4"));
            states[0].Link["x1"] = states[1];
            //string s = states.Find(x => x.Name.Contains("q3")).Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            states.Clear();
            for (int i = 0; i < txtFSMTable.Lines.Count(); i ++)
            {
                string line = txtFSMTable.Lines[i];
                string[] StateAndLinks = line.Split(':');
                string StateName = StateAndLinks[0].Trim(' ');
                fsm.State state = new fsm.State(i, StateName);
                if (!states.Contains(state))
                    states.Add(state);
                if (StateAndLinks[1] != "") // Парсим переходы к другим состояниям
                {
                    foreach (string LinkPair in StateAndLinks[1].Split(','))
                    {
                        string[] SymAndDestState = LinkPair.Split('-');
                        string Symbol = SymAndDestState[0].Trim(' ');
                        string DestState = SymAndDestState[1].Trim(' ');
                        fsm.State destState = states.Find(x => x.Name.Contains(DestState));
                        if (destState != null)
                            state.Link[Symbol] = destState;
                        else if (DestState != "")
                        {
                            destState = new fsm.State(Convert.ToInt32(DestState[DestState.Count() - 1]), DestState);
                            if (!states.Contains(destState))
                                states.Add(destState);
                            state.Link[Symbol] = destState;
                        }
                            
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            var links = new Dictionary<string, Link>();
            HashSet<string> hstates = new HashSet<string>();

            hstates.Add("q0");
            hstates.Add("q1");
            hstates.Add("q2");
            hstates.Add("q3");
            hstates.Add("q3");
            links.Add("q0", new Link());
            links["q0"].Add("x1", "q1");
            links["q0"].Add("x2", "q3");
            links.Add("q1", new Link());
            links["q1"].Add("x1", "q3");
            links["q1"].Add("x3", "q2");
            links.Add("q2", new Link());
            links["q2"].Add("x1", "q3");

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
            _gArea.LogicCore.Graph = dataGraph;
            _gArea.RelayoutGraph();
        }
    }
}
