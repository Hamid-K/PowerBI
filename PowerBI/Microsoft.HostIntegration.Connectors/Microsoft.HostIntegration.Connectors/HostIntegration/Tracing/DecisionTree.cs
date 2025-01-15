using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200064F RID: 1615
	public class DecisionTree
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000B69B4 File Offset: 0x000B4BB4
		public ITraceContainer Container
		{
			get
			{
				return this.traceContainer;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000B69BC File Offset: 0x000B4BBC
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000B69C4 File Offset: 0x000B4BC4
		public DecisionTreeNode RootNode
		{
			get
			{
				return this.decisionTreeRootNode;
			}
			set
			{
				this.decisionTreeRootNode = value;
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000B69CD File Offset: 0x000B4BCD
		public DecisionTree(TraceTreeLevelNode traceTreeLevelNode, XmlNode topLevelDecisionXmlNode)
		{
			this.parent = traceTreeLevelNode;
			this.traceContainer = this.parent.Container;
			this.decisionTreeRootNode = DecisionTreeNode.CreateNode(this, topLevelDecisionXmlNode);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000B69FA File Offset: 0x000B4BFA
		public DecisionTree(TraceTreeLevelNode traceTreeLevelNode)
		{
			this.parent = traceTreeLevelNode;
			this.traceContainer = this.parent.Container;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000B6A1A File Offset: 0x000B4C1A
		public DecisionTree(DecisionTreeNode rootNodeToClone, TraceTreeLevelNode parentTraceLevelNode, ITraceContainer newTraceContainer)
		{
			this.traceContainer = newTraceContainer;
			this.parent = parentTraceLevelNode;
			this.decisionTreeRootNode = rootNodeToClone.InternalClone(this);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000B6A3D File Offset: 0x000B4C3D
		public DecisionTree InternalClone(TraceTreeLevelNode parentTraceLevelNode)
		{
			return new DecisionTree(this.decisionTreeRootNode, parentTraceLevelNode, this.traceContainer);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000B6A51 File Offset: 0x000B4C51
		public EvaluationResult Evaluate()
		{
			return this.decisionTreeRootNode.Evaluate();
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000B6A5E File Offset: 0x000B4C5E
		public void RootRemoved()
		{
			this.decisionTreeRootNode = null;
			this.parent.RemoveThis(this);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000B6A73 File Offset: 0x000B4C73
		public void AddPropertyEqualNode(DecisionTreeEqualNode newNode)
		{
			if (this.propertyEqualNodes == null)
			{
				this.propertyEqualNodes = new List<DecisionTreeEqualNode>();
			}
			this.propertyEqualNodes.Add(newNode);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000B6A94 File Offset: 0x000B4C94
		public void FindEqualNodes(TraceTree tree)
		{
			if (this.propertyEqualNodes != null)
			{
				foreach (DecisionTreeEqualNode decisionTreeEqualNode in this.propertyEqualNodes)
				{
					tree.AddPropertyEqualNode(decisionTreeEqualNode);
				}
			}
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000B6AF0 File Offset: 0x000B4CF0
		public string GenerateXml()
		{
			return this.decisionTreeRootNode.GenerateXml();
		}

		// Token: 0x04001F2C RID: 7980
		private TraceTreeLevelNode parent;

		// Token: 0x04001F2D RID: 7981
		private DecisionTreeNode decisionTreeRootNode;

		// Token: 0x04001F2E RID: 7982
		private ITraceContainer traceContainer;

		// Token: 0x04001F2F RID: 7983
		private List<DecisionTreeEqualNode> propertyEqualNodes;
	}
}
