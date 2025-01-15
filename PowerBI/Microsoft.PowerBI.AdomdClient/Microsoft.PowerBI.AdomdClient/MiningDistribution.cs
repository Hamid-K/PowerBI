using System;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B7 RID: 183
	public sealed class MiningDistribution : ISubordinateObject
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002AEE7 File Offset: 0x000290E7
		internal MiningDistribution(AdomdConnection connection, DataRow miningDistributionRow, MiningContentNode parentNode)
		{
			this.connection = connection;
			this.miningDistributionRow = miningDistributionRow;
			this.parentNode = parentNode;
			this.propertiesCollection = null;
			this.distValue = null;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002AF12 File Offset: 0x00029112
		public MiningModel ParentMiningModel
		{
			get
			{
				return this.parentNode.ParentMiningModel;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002AF1F File Offset: 0x0002911F
		public MiningContentNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002AF27 File Offset: 0x00029127
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002AF2F File Offset: 0x0002912F
		public string Name
		{
			get
			{
				return AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionAttrNameColumn).ToString();
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002AF48 File Offset: 0x00029148
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002AF8C File Offset: 0x0002918C
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

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002AFE5 File Offset: 0x000291E5
		public double Probability
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionProbabilityColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002B001 File Offset: 0x00029201
		public double Variance
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionVarianceColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002B01D File Offset: 0x0002921D
		public double Support
		{
			get
			{
				return Convert.ToDouble(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionSupportColumn), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002B039 File Offset: 0x00029239
		public MiningValueType ValueType
		{
			get
			{
				return (MiningValueType)Convert.ToInt32(AdomdUtils.GetProperty(this.miningDistributionRow, MiningDistribution.miningDistributionValueTypeColumn).ToString(), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002B05A File Offset: 0x0002925A
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002B07C File Offset: 0x0002927C
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentNode;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002B084 File Offset: 0x00029284
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002B08C File Offset: 0x0002928C
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(MiningDistribution);
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002B098 File Offset: 0x00029298
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002B0BB File Offset: 0x000292BB
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002B0C9 File Offset: 0x000292C9
		public static bool operator ==(MiningDistribution o1, MiningDistribution o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002B0D2 File Offset: 0x000292D2
		public static bool operator !=(MiningDistribution o1, MiningDistribution o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040006D3 RID: 1747
		private DataRow miningDistributionRow;

		// Token: 0x040006D4 RID: 1748
		private MiningContentNode parentNode;

		// Token: 0x040006D5 RID: 1749
		private AdomdConnection connection;

		// Token: 0x040006D6 RID: 1750
		private PropertyCollection propertiesCollection;

		// Token: 0x040006D7 RID: 1751
		private MiningAttribute attribute;

		// Token: 0x040006D8 RID: 1752
		internal int ordinal;

		// Token: 0x040006D9 RID: 1753
		private MiningValue distValue;

		// Token: 0x040006DA RID: 1754
		private int hashCode;

		// Token: 0x040006DB RID: 1755
		private bool hashCodeCalculated;

		// Token: 0x040006DC RID: 1756
		internal static string miningDistributionAttrNameColumn = "ATTRIBUTE_NAME";

		// Token: 0x040006DD RID: 1757
		internal static string miningDistributionAttrValueColumn = "ATTRIBUTE_VALUE";

		// Token: 0x040006DE RID: 1758
		internal static string miningDistributionSupportColumn = "SUPPORT";

		// Token: 0x040006DF RID: 1759
		internal static string miningDistributionProbabilityColumn = "PROBABILITY";

		// Token: 0x040006E0 RID: 1760
		internal static string miningDistributionVarianceColumn = "VARIANCE";

		// Token: 0x040006E1 RID: 1761
		internal static string miningDistributionValueTypeColumn = "VALUETYPE";
	}
}
