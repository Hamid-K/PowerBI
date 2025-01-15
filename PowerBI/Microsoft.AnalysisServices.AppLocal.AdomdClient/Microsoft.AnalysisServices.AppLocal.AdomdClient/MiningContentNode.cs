using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B4 RID: 180
	public sealed class MiningContentNode : IMetadataObject
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A9A0 File Offset: 0x00028BA0
		internal MiningContentNode(AdomdConnection connection, DataRow miningContentNodeRow, MiningModel parentMiningModel, string catalog, string sessionId)
		{
			this.connection = connection;
			this.miningContentNodeRow = miningContentNodeRow;
			this.parentMiningModel = parentMiningModel;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002A9DC File Offset: 0x00028BDC
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeNameColumn).ToString();
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002A9F3 File Offset: 0x00028BF3
		public string UniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeUniqueNameColumn).ToString();
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002AA0A File Offset: 0x00028C0A
		public MiningModel ParentMiningModel
		{
			get
			{
				return this.parentMiningModel;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002AA14 File Offset: 0x00028C14
		public MiningAttribute Attribute
		{
			get
			{
				if (this.attribute == null)
				{
					string text = AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeAttributeColumn).ToString();
					this.attribute = new MiningAttribute(this.parentMiningModel, text);
				}
				return this.attribute;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002AA57 File Offset: 0x00028C57
		public MiningNodeType Type
		{
			get
			{
				return (MiningNodeType)Convert.ToInt32(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeTypeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002AA73 File Offset: 0x00028C73
		public double Probability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002AA8F File Offset: 0x00028C8F
		public double MarginalProbability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeMargProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002AAAB File Offset: 0x00028CAB
		public double Support
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeSupportColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002AAC7 File Offset: 0x00028CC7
		public double Score
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeScoreColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002AAE3 File Offset: 0x00028CE3
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeDescriptionColumn).ToString();
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002AAFA File Offset: 0x00028CFA
		public string NodeRule
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeRuleColumn).ToString();
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002AB11 File Offset: 0x00028D11
		public string MarginalRule
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeMargRuleColumn).ToString();
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002AB28 File Offset: 0x00028D28
		public MiningContentNode ParentNode
		{
			get
			{
				if (null == this.parent)
				{
					MiningContentNodeCollection miningContentNodeCollection = new MiningContentNodeCollection(this.connection, this, MiningNodeTreeOpType.TreeopParent);
					if (miningContentNodeCollection.Count > 0)
					{
						this.parent = miningContentNodeCollection[0];
					}
				}
				return this.parent;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002AB6D File Offset: 0x00028D6D
		public string ParentUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeParentUniqueNameColumn).ToString();
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002AB84 File Offset: 0x00028D84
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeCaptionColumn).ToString();
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002AB9B File Offset: 0x00028D9B
		public string ShortCaption
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeShortCaptionColumn).ToString();
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002ABB2 File Offset: 0x00028DB2
		public MiningContentNodeCollection Ancestors
		{
			get
			{
				if (this.ancestors == null)
				{
					this.ancestors = new MiningContentNodeCollection(this.connection, this, MiningNodeTreeOpType.TreeopAncestors);
				}
				else
				{
					this.ancestors.CollectionInternal.CheckCache();
				}
				return this.ancestors;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		public MiningContentNodeCollection Children
		{
			get
			{
				if (this.children == null)
				{
					this.children = new MiningContentNodeCollection(this.connection, this, MiningNodeTreeOpType.TreeopChildren);
				}
				else
				{
					this.children.CollectionInternal.CheckCache();
				}
				return this.children;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002AC1D File Offset: 0x00028E1D
		public MiningContentNodeCollection Siblings
		{
			get
			{
				if (this.siblings == null)
				{
					this.siblings = new MiningContentNodeCollection(this.connection, this, MiningNodeTreeOpType.TreeopSiblings);
				}
				else
				{
					this.siblings.CollectionInternal.CheckCache();
				}
				return this.siblings;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002AC52 File Offset: 0x00028E52
		public MiningContentNodeCollection Descendants
		{
			get
			{
				if (this.descendants == null)
				{
					this.descendants = new MiningContentNodeCollection(this.connection, this, MiningNodeTreeOpType.TreeopDescendants);
				}
				else
				{
					this.descendants.CollectionInternal.CheckCache();
				}
				return this.siblings;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002AC88 File Offset: 0x00028E88
		public MiningDistributionCollection Distribution
		{
			get
			{
				if (this.distributions == null)
				{
					DataRow[] array = (DataRow[])AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeDistributionColumn);
					this.distributions = new MiningDistributionCollection(this.connection, this, array);
				}
				return this.distributions;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002ACCC File Offset: 0x00028ECC
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.miningContentNodeRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002ACEE File Offset: 0x00028EEE
		internal void SetParentNode(MiningContentNode node)
		{
			this.parent = node;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002ACF7 File Offset: 0x00028EF7
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0002ACFF File Offset: 0x00028EFF
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002AD07 File Offset: 0x00028F07
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002AD0F File Offset: 0x00028F0F
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentMiningModel.Name;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002AD1C File Offset: 0x00028F1C
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002AD24 File Offset: 0x00028F24
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningContentNode);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002AD30 File Offset: 0x00028F30
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002AD53 File Offset: 0x00028F53
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002AD61 File Offset: 0x00028F61
		public static bool operator ==(MiningContentNode o1, MiningContentNode o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002AD6A File Offset: 0x00028F6A
		public static bool operator !=(MiningContentNode o1, MiningContentNode o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040006B9 RID: 1721
		private DataRow miningContentNodeRow;

		// Token: 0x040006BA RID: 1722
		private MiningModel parentMiningModel;

		// Token: 0x040006BB RID: 1723
		private AdomdConnection connection;

		// Token: 0x040006BC RID: 1724
		private PropertyCollection propertiesCollection;

		// Token: 0x040006BD RID: 1725
		private MiningContentNodeCollection ancestors;

		// Token: 0x040006BE RID: 1726
		private MiningContentNodeCollection children;

		// Token: 0x040006BF RID: 1727
		private MiningContentNodeCollection siblings;

		// Token: 0x040006C0 RID: 1728
		private MiningContentNodeCollection descendants;

		// Token: 0x040006C1 RID: 1729
		private MiningContentNode parent;

		// Token: 0x040006C2 RID: 1730
		private MiningDistributionCollection distributions;

		// Token: 0x040006C3 RID: 1731
		private MiningAttribute attribute;

		// Token: 0x040006C4 RID: 1732
		private string catalog;

		// Token: 0x040006C5 RID: 1733
		private string sessionId;

		// Token: 0x040006C6 RID: 1734
		private int hashCode;

		// Token: 0x040006C7 RID: 1735
		private bool hashCodeCalculated;

		// Token: 0x040006C8 RID: 1736
		internal static string miningContentNodeNameColumn = "NODE_NAME";

		// Token: 0x040006C9 RID: 1737
		internal static string miningContentNodeUniqueNameColumn = "NODE_UNIQUE_NAME";

		// Token: 0x040006CA RID: 1738
		internal static string miningContentNodeTypeColumn = "NODE_TYPE";

		// Token: 0x040006CB RID: 1739
		internal static string miningContentNodeProbabilityColumn = "NODE_PROBABILITY";

		// Token: 0x040006CC RID: 1740
		internal static string miningContentNodeMargProbabilityColumn = "MARGINAL_PROBABILITY";

		// Token: 0x040006CD RID: 1741
		internal static string miningContentNodeScoreColumn = "MSOLAP_NODE_SCORE";

		// Token: 0x040006CE RID: 1742
		internal static string miningContentNodeSupportColumn = "NODE_SUPPORT";

		// Token: 0x040006CF RID: 1743
		internal static string miningContentNodeDescriptionColumn = "NODE_DESCRIPTION";

		// Token: 0x040006D0 RID: 1744
		internal static string miningContentNodeRuleColumn = "NODE_RULE";

		// Token: 0x040006D1 RID: 1745
		internal static string miningContentNodeMargRuleColumn = "MARGINAL_RULE";

		// Token: 0x040006D2 RID: 1746
		internal static string miningContentNodeParentUniqueNameColumn = "PARENT_UNIQUE_NAME";

		// Token: 0x040006D3 RID: 1747
		internal static string miningContentNodeCaptionColumn = "NODE_CAPTION";

		// Token: 0x040006D4 RID: 1748
		internal static string miningContentNodeShortCaptionColumn = "MSOLAP_NODE_SHORT_CAPTION";

		// Token: 0x040006D5 RID: 1749
		internal static string miningContentNodeDistributionColumn = "NODE_DISTRIBUTION";

		// Token: 0x040006D6 RID: 1750
		internal static string miningContentNodeAttributeColumn = "ATTRIBUTE_NAME";
	}
}
