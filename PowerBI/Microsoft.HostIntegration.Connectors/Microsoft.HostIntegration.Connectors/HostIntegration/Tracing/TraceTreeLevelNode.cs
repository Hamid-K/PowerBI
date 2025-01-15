using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000666 RID: 1638
	public class TraceTreeLevelNode : BaseTraceTreeElement
	{
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000B8150 File Offset: 0x000B6350
		public ITraceContainer Container
		{
			get
			{
				return this.traceContainer;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000B8158 File Offset: 0x000B6358
		public int LevelOrFlags
		{
			get
			{
				return this.levelOrFlags;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000B8160 File Offset: 0x000B6360
		public string Name
		{
			get
			{
				return TraceTree.LevelsOrFlagsToString(this.traceContainer.UsesLevels, this.levelOrFlags);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000B8178 File Offset: 0x000B6378
		public List<DecisionTree> DecisionTrees
		{
			get
			{
				return this.decisionTrees;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000B8180 File Offset: 0x000B6380
		public TraceTreeLevelNode(TraceTreeNode parentNode, int levelRequested)
		{
			this.levelOrFlags = levelRequested;
			this.parent = parentNode;
			this.traceContainer = this.parent.Container;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000B81A8 File Offset: 0x000B63A8
		public void AddDecisionTree(XmlNode levelNode)
		{
			if (!levelNode.HasChildNodes)
			{
				return;
			}
			if (levelNode.ChildNodes.Count != 1)
			{
				throw new TraceException(SR.LevelNodeOneChild(this.Name));
			}
			DecisionTree decisionTree = new DecisionTree(this, levelNode.FirstChild);
			if (this.decisionTrees == null)
			{
				this.decisionTrees = new List<DecisionTree>();
			}
			this.decisionTrees.Add(decisionTree);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000B8209 File Offset: 0x000B6409
		public TraceTreeLevelNode(int newLevelOrFlags, TraceTreeNode parentTraceNode, ITraceContainer newTraceContainer)
		{
			this.traceContainer = newTraceContainer;
			this.levelOrFlags = newLevelOrFlags;
			this.parent = parentTraceNode;
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000B8226 File Offset: 0x000B6426
		private void SetDecisionTrees(List<DecisionTree> newDecisionTrees)
		{
			this.decisionTrees = newDecisionTrees;
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000B8230 File Offset: 0x000B6430
		public TraceTreeLevelNode InternalClone(TraceTreeNode parentTraceNode)
		{
			TraceTreeLevelNode traceTreeLevelNode = new TraceTreeLevelNode(this.levelOrFlags, parentTraceNode, this.traceContainer);
			List<DecisionTree> list = new List<DecisionTree>();
			if (this.decisionTrees != null)
			{
				foreach (DecisionTree decisionTree in this.decisionTrees)
				{
					list.Add(decisionTree.InternalClone(traceTreeLevelNode));
				}
			}
			traceTreeLevelNode.SetDecisionTrees(list);
			return traceTreeLevelNode;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000B82B4 File Offset: 0x000B64B4
		public DecisionTreeNode AddPastedInfo(XmlNode decisionTreeNode)
		{
			DecisionTree decisionTree = new DecisionTree(this, decisionTreeNode);
			if (this.decisionTrees == null)
			{
				this.decisionTrees = new List<DecisionTree>();
			}
			this.decisionTrees.Add(decisionTree);
			return decisionTree.RootNode;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B82F0 File Offset: 0x000B64F0
		public DecisionTreeNode AddEqualNode(int identifier, int integerValue, string stringValue, bool isInteger)
		{
			if (this.decisionTrees != null && this.decisionTrees.Count != 0)
			{
				throw new TraceException(SR.LevelNoneEmptyDecisionTree(this.Name));
			}
			this.decisionTrees = new List<DecisionTree>();
			DecisionTree decisionTree = new DecisionTree(this);
			DecisionTreeEqualNode decisionTreeEqualNode = new DecisionTreeEqualNode(decisionTree, identifier, false, integerValue, stringValue, isInteger);
			decisionTree.RootNode = decisionTreeEqualNode;
			this.decisionTrees.Add(decisionTree);
			return decisionTreeEqualNode;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000B8356 File Offset: 0x000B6556
		public void RemoveAll()
		{
			this.parent.Levels.Remove(this);
			this.decisionTrees = null;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000B8371 File Offset: 0x000B6571
		public void RemoveThis(DecisionTree decisionTree)
		{
			this.decisionTrees.Remove(decisionTree);
			if (this.decisionTrees.Count == 0)
			{
				this.decisionTrees = null;
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B8394 File Offset: 0x000B6594
		public DecisionTreeNode AddAndNode()
		{
			if (this.decisionTrees != null && this.decisionTrees.Count != 0)
			{
				throw new TraceException(SR.LevelNoneEmptyDecisionTree(this.Name));
			}
			this.decisionTrees = new List<DecisionTree>();
			DecisionTree decisionTree = new DecisionTree(this);
			DecisionTreeAndNode decisionTreeAndNode = new DecisionTreeAndNode(decisionTree, false, null);
			decisionTree.RootNode = decisionTreeAndNode;
			this.decisionTrees.Add(decisionTree);
			return decisionTreeAndNode;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B83F8 File Offset: 0x000B65F8
		public DecisionTreeNode AddOrNode()
		{
			if (this.decisionTrees != null && this.decisionTrees.Count != 0)
			{
				throw new TraceException(SR.LevelNoneEmptyDecisionTree(this.Name));
			}
			this.decisionTrees = new List<DecisionTree>();
			DecisionTree decisionTree = new DecisionTree(this);
			DecisionTreeOrNode decisionTreeOrNode = new DecisionTreeOrNode(decisionTree, false, null);
			decisionTree.RootNode = decisionTreeOrNode;
			this.decisionTrees.Add(decisionTree);
			return decisionTreeOrNode;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B845C File Offset: 0x000B665C
		public EvaluationResult Evaluate()
		{
			if (this.decisionTrees != null && this.decisionTrees.Count != 0)
			{
				bool flag = false;
				foreach (DecisionTree decisionTree in this.decisionTrees)
				{
					EvaluationResult evaluationResult = decisionTree.Evaluate();
					if (evaluationResult == EvaluationResult.True)
					{
						if (this.levelOrFlags == 0)
						{
							return EvaluationResult.TrueNone;
						}
						return evaluationResult;
					}
					else if (evaluationResult == EvaluationResult.False)
					{
						flag = true;
					}
				}
				if (flag)
				{
					return EvaluationResult.False;
				}
				return EvaluationResult.Indeterminate;
			}
			if (this.levelOrFlags == 0)
			{
				return EvaluationResult.TrueNone;
			}
			return EvaluationResult.True;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000B84F4 File Offset: 0x000B66F4
		public void FindEqualNodes(TraceTree tree)
		{
			if (this.decisionTrees != null)
			{
				foreach (DecisionTree decisionTree in this.decisionTrees)
				{
					decisionTree.FindEqualNodes(tree);
				}
			}
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000B8550 File Offset: 0x000B6750
		public override string GenerateXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<traceLevel level=\"{0}\">", this.Name);
			if (this.decisionTrees != null)
			{
				foreach (DecisionTree decisionTree in this.decisionTrees)
				{
					stringBuilder.Append(decisionTree.GenerateXml());
				}
			}
			stringBuilder.AppendFormat("</traceLevel>", Array.Empty<object>());
			return stringBuilder.ToString();
		}

		// Token: 0x04001F72 RID: 8050
		private int levelOrFlags;

		// Token: 0x04001F73 RID: 8051
		private List<DecisionTree> decisionTrees;

		// Token: 0x04001F74 RID: 8052
		private ITraceContainer traceContainer;

		// Token: 0x04001F75 RID: 8053
		private TraceTreeNode parent;
	}
}
