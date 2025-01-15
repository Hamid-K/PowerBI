using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B4 RID: 180
	public sealed class MiningContentNode : IMetadataObject
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002A670 File Offset: 0x00028870
		internal MiningContentNode(AdomdConnection connection, DataRow miningContentNodeRow, MiningModel parentMiningModel, string catalog, string sessionId)
		{
			this.connection = connection;
			this.miningContentNodeRow = miningContentNodeRow;
			this.parentMiningModel = parentMiningModel;
			this.propertiesCollection = null;
			this.catalog = catalog;
			this.sessionId = sessionId;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002A6A4 File Offset: 0x000288A4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002A6AC File Offset: 0x000288AC
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeNameColumn).ToString();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002A6C3 File Offset: 0x000288C3
		public string UniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeUniqueNameColumn).ToString();
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002A6DA File Offset: 0x000288DA
		public MiningModel ParentMiningModel
		{
			get
			{
				return this.parentMiningModel;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002A6E4 File Offset: 0x000288E4
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002A727 File Offset: 0x00028927
		public MiningNodeType Type
		{
			get
			{
				return (MiningNodeType)Convert.ToInt32(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeTypeColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002A743 File Offset: 0x00028943
		public double Probability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002A75F File Offset: 0x0002895F
		public double MarginalProbability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeMargProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002A77B File Offset: 0x0002897B
		public double Support
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeSupportColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002A797 File Offset: 0x00028997
		public double Score
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeScoreColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002A7B3 File Offset: 0x000289B3
		public string Description
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeDescriptionColumn).ToString();
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002A7CA File Offset: 0x000289CA
		public string NodeRule
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeRuleColumn).ToString();
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002A7E1 File Offset: 0x000289E1
		public string MarginalRule
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeMargRuleColumn).ToString();
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002A7F8 File Offset: 0x000289F8
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002A83D File Offset: 0x00028A3D
		public string ParentUniqueName
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeParentUniqueNameColumn).ToString();
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002A854 File Offset: 0x00028A54
		public string Caption
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeCaptionColumn).ToString();
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002A86B File Offset: 0x00028A6B
		public string ShortCaption
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningContentNodeRow, MiningContentNode.miningContentNodeShortCaptionColumn).ToString();
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002A882 File Offset: 0x00028A82
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002A8B8 File Offset: 0x00028AB8
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002A8ED File Offset: 0x00028AED
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002A922 File Offset: 0x00028B22
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002A958 File Offset: 0x00028B58
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002A99C File Offset: 0x00028B9C
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

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A9BE File Offset: 0x00028BBE
		internal void SetParentNode(MiningContentNode node)
		{
			this.parent = node;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002A9C7 File Offset: 0x00028BC7
		AdomdConnection IMetadataObject.Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002A9CF File Offset: 0x00028BCF
		string IMetadataObject.Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002A9D7 File Offset: 0x00028BD7
		string IMetadataObject.SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002A9DF File Offset: 0x00028BDF
		string IMetadataObject.CubeName
		{
			get
			{
				return this.ParentMiningModel.Name;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002A9EC File Offset: 0x00028BEC
		string IMetadataObject.UniqueName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		Type IMetadataObject.Type
		{
			get
			{
				return typeof(MiningContentNode);
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002AA00 File Offset: 0x00028C00
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002AA23 File Offset: 0x00028C23
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as IMetadataObject);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002AA31 File Offset: 0x00028C31
		public static bool operator ==(MiningContentNode o1, MiningContentNode o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002AA3A File Offset: 0x00028C3A
		public static bool operator !=(MiningContentNode o1, MiningContentNode o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040006AC RID: 1708
		private DataRow miningContentNodeRow;

		// Token: 0x040006AD RID: 1709
		private MiningModel parentMiningModel;

		// Token: 0x040006AE RID: 1710
		private AdomdConnection connection;

		// Token: 0x040006AF RID: 1711
		private PropertyCollection propertiesCollection;

		// Token: 0x040006B0 RID: 1712
		private MiningContentNodeCollection ancestors;

		// Token: 0x040006B1 RID: 1713
		private MiningContentNodeCollection children;

		// Token: 0x040006B2 RID: 1714
		private MiningContentNodeCollection siblings;

		// Token: 0x040006B3 RID: 1715
		private MiningContentNodeCollection descendants;

		// Token: 0x040006B4 RID: 1716
		private MiningContentNode parent;

		// Token: 0x040006B5 RID: 1717
		private MiningDistributionCollection distributions;

		// Token: 0x040006B6 RID: 1718
		private MiningAttribute attribute;

		// Token: 0x040006B7 RID: 1719
		private string catalog;

		// Token: 0x040006B8 RID: 1720
		private string sessionId;

		// Token: 0x040006B9 RID: 1721
		private int hashCode;

		// Token: 0x040006BA RID: 1722
		private bool hashCodeCalculated;

		// Token: 0x040006BB RID: 1723
		internal static string miningContentNodeNameColumn = "NODE_NAME";

		// Token: 0x040006BC RID: 1724
		internal static string miningContentNodeUniqueNameColumn = "NODE_UNIQUE_NAME";

		// Token: 0x040006BD RID: 1725
		internal static string miningContentNodeTypeColumn = "NODE_TYPE";

		// Token: 0x040006BE RID: 1726
		internal static string miningContentNodeProbabilityColumn = "NODE_PROBABILITY";

		// Token: 0x040006BF RID: 1727
		internal static string miningContentNodeMargProbabilityColumn = "MARGINAL_PROBABILITY";

		// Token: 0x040006C0 RID: 1728
		internal static string miningContentNodeScoreColumn = "MSOLAP_NODE_SCORE";

		// Token: 0x040006C1 RID: 1729
		internal static string miningContentNodeSupportColumn = "NODE_SUPPORT";

		// Token: 0x040006C2 RID: 1730
		internal static string miningContentNodeDescriptionColumn = "NODE_DESCRIPTION";

		// Token: 0x040006C3 RID: 1731
		internal static string miningContentNodeRuleColumn = "NODE_RULE";

		// Token: 0x040006C4 RID: 1732
		internal static string miningContentNodeMargRuleColumn = "MARGINAL_RULE";

		// Token: 0x040006C5 RID: 1733
		internal static string miningContentNodeParentUniqueNameColumn = "PARENT_UNIQUE_NAME";

		// Token: 0x040006C6 RID: 1734
		internal static string miningContentNodeCaptionColumn = "NODE_CAPTION";

		// Token: 0x040006C7 RID: 1735
		internal static string miningContentNodeShortCaptionColumn = "MSOLAP_NODE_SHORT_CAPTION";

		// Token: 0x040006C8 RID: 1736
		internal static string miningContentNodeDistributionColumn = "NODE_DISTRIBUTION";

		// Token: 0x040006C9 RID: 1737
		internal static string miningContentNodeAttributeColumn = "ATTRIBUTE_NAME";
	}
}
