using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B7 RID: 183
	public sealed class MiningDistribution : ISubordinateObject
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x0002B217 File Offset: 0x00029417
		internal MiningDistribution(AdomdConnection connection, DataRow miningDistributionRow, MiningContentNode parentNode)
		{
			this.connection = connection;
			this.miningDistributionRow = miningDistributionRow;
			this.parentNode = parentNode;
			this.propertiesCollection = null;
			this.distValue = null;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002B242 File Offset: 0x00029442
		public MiningModel ParentMiningModel
		{
			get
			{
				return this.parentNode.ParentMiningModel;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002B24F File Offset: 0x0002944F
		public MiningContentNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002B257 File Offset: 0x00029457
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002B25F File Offset: 0x0002945F
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionAttrNameColumn).ToString();
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0002B278 File Offset: 0x00029478
		public MiningAttribute Attribute
		{
			get
			{
				if (this.attribute == null)
				{
					string text = AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionAttrNameColumn).ToString();
					this.attribute = new MiningAttribute(this.ParentMiningModel, text);
				}
				return this.attribute;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002B2BC File Offset: 0x000294BC
		public MiningValue Value
		{
			get
			{
				if (this.distValue == null)
				{
					object property = AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionAttrValueColumn);
					int num = -1;
					if (this.ValueType == MiningValueType.Missing)
					{
						num = 0;
					}
					else if (this.ValueType == MiningValueType.Continuous)
					{
						num = 1;
					}
					this.distValue = new MiningValue(this.ValueType, num, property);
				}
				return this.distValue;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0002B315 File Offset: 0x00029515
		public double Probability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002B331 File Offset: 0x00029531
		public double Variance
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionVarianceColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002B34D File Offset: 0x0002954D
		public double Support
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionSupportColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002B369 File Offset: 0x00029569
		public MiningValueType ValueType
		{
			get
			{
				return (MiningValueType)Convert.ToInt32(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionValueTypeColumn).ToString(), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002B38A File Offset: 0x0002958A
		public PropertyCollection Properties
		{
			get
			{
				if (this.propertiesCollection == null)
				{
					this.propertiesCollection = new PropertyCollection(this.miningDistributionRow, this);
				}
				return this.propertiesCollection;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002B3AC File Offset: 0x000295AC
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0002B3B4 File Offset: 0x000295B4
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002B3BC File Offset: 0x000295BC
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(MiningDistribution);
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B3C8 File Offset: 0x000295C8
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002B3EB File Offset: 0x000295EB
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002B3F9 File Offset: 0x000295F9
		public static bool operator ==(MiningDistribution o1, MiningDistribution o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002B402 File Offset: 0x00029602
		public static bool operator !=(MiningDistribution o1, MiningDistribution o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040006E0 RID: 1760
		private DataRow miningDistributionRow;

		// Token: 0x040006E1 RID: 1761
		private MiningContentNode parentNode;

		// Token: 0x040006E2 RID: 1762
		private AdomdConnection connection;

		// Token: 0x040006E3 RID: 1763
		private PropertyCollection propertiesCollection;

		// Token: 0x040006E4 RID: 1764
		private MiningAttribute attribute;

		// Token: 0x040006E5 RID: 1765
		internal int ordinal;

		// Token: 0x040006E6 RID: 1766
		private MiningValue distValue;

		// Token: 0x040006E7 RID: 1767
		private int hashCode;

		// Token: 0x040006E8 RID: 1768
		private bool hashCodeCalculated;

		// Token: 0x040006E9 RID: 1769
		internal static string miningDistributionAttrNameColumn = "ATTRIBUTE_NAME";

		// Token: 0x040006EA RID: 1770
		internal static string miningDistributionAttrValueColumn = "ATTRIBUTE_VALUE";

		// Token: 0x040006EB RID: 1771
		internal static string miningDistributionSupportColumn = "SUPPORT";

		// Token: 0x040006EC RID: 1772
		internal static string miningDistributionProbabilityColumn = "PROBABILITY";

		// Token: 0x040006ED RID: 1773
		internal static string miningDistributionVarianceColumn = "VARIANCE";

		// Token: 0x040006EE RID: 1774
		internal static string miningDistributionValueTypeColumn = "VALUETYPE";
	}
}
