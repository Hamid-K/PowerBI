using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000667 RID: 1639
	public class TraceTreeNode : BaseTraceTreeElement
	{
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000B85E0 File Offset: 0x000B67E0
		public ITraceContainer Container
		{
			get
			{
				return this.traceContainer;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000B85E8 File Offset: 0x000B67E8
		public int TracePointIdentifier
		{
			get
			{
				return this.tracePointInformation.Identifier;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000B85F5 File Offset: 0x000B67F5
		public int LevelOrFlags
		{
			get
			{
				return this.levelOrFlags;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000B85FD File Offset: 0x000B67FD
		public TraceTreeNode Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x000B8605 File Offset: 0x000B6805
		public List<TraceTreeLevelNode> Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x000B860D File Offset: 0x000B680D
		public List<TraceTreeNode> TraceTreeNodes
		{
			get
			{
				return this.traceTreeNodes;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000B8618 File Offset: 0x000B6818
		public bool HasSomeTracingSet
		{
			get
			{
				if (this.levels != null)
				{
					using (List<TraceTreeLevelNode>.Enumerator enumerator = this.levels.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.LevelOrFlags != 0)
							{
								return true;
							}
						}
					}
				}
				if (this.traceTreeNodes != null)
				{
					using (List<TraceTreeNode>.Enumerator enumerator2 = this.traceTreeNodes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.HasSomeTracingSet)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000B86C8 File Offset: 0x000B68C8
		public bool UsesLevels
		{
			get
			{
				return this.traceContainer.UsesLevels;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060036B0 RID: 14000 RVA: 0x000B86D5 File Offset: 0x000B68D5
		public bool AllowPropertyUpdates
		{
			get
			{
				return this.tracePointInformation == null || this.tracePointInformation.AllowPropertyUpdates;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000B86EC File Offset: 0x000B68EC
		public string Name
		{
			get
			{
				if (this.tracePointInformation != null)
				{
					return this.tracePointInformation.Name;
				}
				return null;
			}
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B8704 File Offset: 0x000B6904
		public TraceTreeNode(ITraceContainer container)
		{
			this.parent = null;
			this.tracePointInformation = null;
			this.levels = null;
			this.traceContainer = container;
			if (container.TracePoints != null)
			{
				this.traceTreeNodes = new List<TraceTreeNode>();
				foreach (ITracePointInformation tracePointInformation in container.TracePoints)
				{
					TraceTreeNode traceTreeNode = new TraceTreeNode(this, tracePointInformation);
					this.traceTreeNodes.Add(traceTreeNode);
				}
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000B879C File Offset: 0x000B699C
		private TraceTreeNode(TraceTreeNode parentNode, ITracePointInformation containerTracePointInformation)
		{
			this.tracePointInformation = containerTracePointInformation;
			this.parent = parentNode;
			this.traceContainer = this.parent.Container;
			if (this.tracePointInformation.TracePoints != null)
			{
				this.traceTreeNodes = new List<TraceTreeNode>();
				foreach (ITracePointInformation tracePointInformation in this.tracePointInformation.TracePoints)
				{
					TraceTreeNode traceTreeNode = new TraceTreeNode(this, tracePointInformation);
					this.traceTreeNodes.Add(traceTreeNode);
				}
			}
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000B8840 File Offset: 0x000B6A40
		public TraceTreeNode GetTracePointNode(string tracePointName)
		{
			return this.GetTracePointNodeAnywhere(tracePointName);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000B884C File Offset: 0x000B6A4C
		public TraceTreeLevelNode GetLevelNode(int levelRequested)
		{
			if (this.levels != null)
			{
				foreach (TraceTreeLevelNode traceTreeLevelNode in this.levels)
				{
					if (traceTreeLevelNode.LevelOrFlags == levelRequested)
					{
						throw new TraceException(SR.LevelNoneEmptyDecisionTreeInsertOr(traceTreeLevelNode.Name));
					}
				}
			}
			if (this.levels == null)
			{
				this.levels = new List<TraceTreeLevelNode>();
			}
			TraceTreeLevelNode traceTreeLevelNode2 = new TraceTreeLevelNode(this, levelRequested);
			this.levels.Add(traceTreeLevelNode2);
			return traceTreeLevelNode2;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000B88E4 File Offset: 0x000B6AE4
		public void FindTracePointIdentifiers(TraceTree usedTraceTree)
		{
			if (this.parent != null)
			{
				usedTraceTree.AddTracePointNode(this);
			}
			this.traceTree = usedTraceTree;
			if (this.traceTreeNodes != null)
			{
				foreach (TraceTreeNode traceTreeNode in this.traceTreeNodes)
				{
					traceTreeNode.FindTracePointIdentifiers(usedTraceTree);
				}
			}
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000B8954 File Offset: 0x000B6B54
		private TraceTreeNode GetTracePointNodeAnywhere(string tracePointName)
		{
			if (this.tracePointInformation != null && string.Compare(this.tracePointInformation.Name, tracePointName, StringComparison.InvariantCulture) == 0)
			{
				return this;
			}
			if (this.traceTreeNodes != null)
			{
				foreach (TraceTreeNode traceTreeNode in this.traceTreeNodes)
				{
					TraceTreeNode tracePointNodeAnywhere = traceTreeNode.GetTracePointNodeAnywhere(tracePointName);
					if (tracePointNodeAnywhere != null)
					{
						return tracePointNodeAnywhere;
					}
				}
			}
			return null;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000B89D8 File Offset: 0x000B6BD8
		public TraceTreeNode(TraceTree containingTree, TraceTreeNode parentNode, ITracePointInformation newTracePointInformation, ITraceContainer newTraceContainer)
		{
			this.tracePointInformation = newTracePointInformation;
			this.traceContainer = newTraceContainer;
			this.parent = parentNode;
			this.traceTree = containingTree;
			if (this.parent != null)
			{
				this.traceTree.AddTracePointNode(this);
			}
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000B8A14 File Offset: 0x000B6C14
		public TraceTreeLevelNode AddPastedInfo(XmlNode levelNode)
		{
			int num = TraceTree.ParseLevel(this.UsesLevels, levelNode);
			TraceTreeLevelNode levelNode2 = this.GetLevelNode(num);
			levelNode2.AddDecisionTree(levelNode);
			return levelNode2;
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000B8A3C File Offset: 0x000B6C3C
		private void AddChildren(List<TraceTreeNode> newTraceTreeNodes, List<TraceTreeLevelNode> newLevels)
		{
			this.traceTreeNodes = newTraceTreeNodes;
			this.levels = newLevels;
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000B8A4C File Offset: 0x000B6C4C
		public void AddLevel(TraceTreeLevelNode newLevel)
		{
			if (this.levels == null)
			{
				this.levels = new List<TraceTreeLevelNode>();
			}
			this.levels.Add(newLevel);
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000B8A70 File Offset: 0x000B6C70
		public TraceTreeNode InternalClone(TraceTree containingTree, TraceTreeNode parentNode)
		{
			TraceTreeNode traceTreeNode = new TraceTreeNode(containingTree, parentNode, this.tracePointInformation, this.traceContainer);
			List<TraceTreeLevelNode> list = null;
			if (this.levels != null)
			{
				list = new List<TraceTreeLevelNode>();
				foreach (TraceTreeLevelNode traceTreeLevelNode in this.levels)
				{
					list.Add(traceTreeLevelNode.InternalClone(traceTreeNode));
				}
			}
			List<TraceTreeNode> list2 = null;
			if (this.traceTreeNodes != null)
			{
				list2 = new List<TraceTreeNode>();
				foreach (TraceTreeNode traceTreeNode2 in this.traceTreeNodes)
				{
					list2.Add(traceTreeNode2.InternalClone(containingTree, traceTreeNode));
				}
			}
			traceTreeNode.AddChildren(list2, list);
			return traceTreeNode;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000B8B54 File Offset: 0x000B6D54
		public void UpdateDefinitions(TraceTreeNode newTreeNode)
		{
			List<TraceTreeLevelNode> list = null;
			if (newTreeNode.Levels != null)
			{
				list = new List<TraceTreeLevelNode>();
				foreach (TraceTreeLevelNode traceTreeLevelNode in newTreeNode.Levels)
				{
					list.Add(traceTreeLevelNode.InternalClone(this));
				}
			}
			this.levels = list;
			if (this.traceTreeNodes != null)
			{
				for (int i = 0; i < this.traceTreeNodes.Count; i++)
				{
					this.traceTreeNodes[i].UpdateDefinitions(newTreeNode.TraceTreeNodes[i]);
				}
			}
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000B8C00 File Offset: 0x000B6E00
		public void Evaluate(int parentLevel)
		{
			int num = 0;
			bool usesLevels = this.UsesLevels;
			bool flag = false;
			if (this.levels != null)
			{
				foreach (TraceTreeLevelNode traceTreeLevelNode in this.levels)
				{
					EvaluationResult evaluationResult = traceTreeLevelNode.Evaluate();
					if (evaluationResult == EvaluationResult.True)
					{
						if (usesLevels)
						{
							if (traceTreeLevelNode.LevelOrFlags > num)
							{
								num = traceTreeLevelNode.LevelOrFlags;
							}
						}
						else
						{
							num |= traceTreeLevelNode.LevelOrFlags;
						}
					}
					if (evaluationResult == EvaluationResult.TrueNone)
					{
						flag = true;
					}
				}
			}
			if (num == 0 && !flag)
			{
				num = parentLevel;
			}
			this.levelOrFlags = num;
			if (this.traceTreeNodes != null)
			{
				foreach (TraceTreeNode traceTreeNode in this.traceTreeNodes)
				{
					traceTreeNode.Evaluate(this.levelOrFlags);
				}
			}
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000B8CF0 File Offset: 0x000B6EF0
		public void FindEqualNodes()
		{
			if (this.levels != null)
			{
				foreach (TraceTreeLevelNode traceTreeLevelNode in this.levels)
				{
					traceTreeLevelNode.FindEqualNodes(this.traceTree);
				}
			}
			if (this.traceTreeNodes != null)
			{
				foreach (TraceTreeNode traceTreeNode in this.traceTreeNodes)
				{
					traceTreeNode.FindEqualNodes();
				}
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000B8D98 File Offset: 0x000B6F98
		public void SetPropertyValue(int propertyIdentifier, object value)
		{
			ITracePointPropertyInformation tracePointPropertyInformation = null;
			foreach (ITracePointPropertyInformation tracePointPropertyInformation2 in this.tracePointInformation.Properties)
			{
				if (tracePointPropertyInformation2.Identifier == propertyIdentifier)
				{
					tracePointPropertyInformation = tracePointPropertyInformation2;
					break;
				}
			}
			if (tracePointPropertyInformation.ValueType == PropertyType.Enumeration)
			{
				value = (int)value;
			}
			this.traceTree.SetPropertyValue(this.AllowPropertyUpdates, propertyIdentifier, value);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000B8E24 File Offset: 0x000B7024
		public override string GenerateXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.parent != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				if (this.levels != null)
				{
					foreach (TraceTreeLevelNode traceTreeLevelNode in this.levels)
					{
						stringBuilder2.Append(traceTreeLevelNode.GenerateXml());
					}
				}
				if (stringBuilder2.Length != 0)
				{
					stringBuilder.AppendFormat("<tracePoint name=\"{0}\">{1}</tracePoint>", this.Name, stringBuilder2);
				}
			}
			if (this.traceTreeNodes != null)
			{
				foreach (TraceTreeNode traceTreeNode in this.traceTreeNodes)
				{
					stringBuilder.Append(traceTreeNode.GenerateXml());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F76 RID: 8054
		private ITracePointInformation tracePointInformation;

		// Token: 0x04001F77 RID: 8055
		private TraceTreeNode parent;

		// Token: 0x04001F78 RID: 8056
		private List<TraceTreeNode> traceTreeNodes;

		// Token: 0x04001F79 RID: 8057
		private List<TraceTreeLevelNode> levels;

		// Token: 0x04001F7A RID: 8058
		private ITraceContainer traceContainer;

		// Token: 0x04001F7B RID: 8059
		private TraceTree traceTree;

		// Token: 0x04001F7C RID: 8060
		private int levelOrFlags;
	}
}
