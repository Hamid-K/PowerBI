using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006E RID: 110
	public sealed class Axis : ISubordinateObject
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x00024000 File Offset: 0x00022200
		internal Axis(AdomdConnection connection, IDSFDataSet dataset, string cubeName, CellSet cellSet, int axisOrdinal)
		{
			this.cellset = cellSet;
			this.axisOrdinal = axisOrdinal;
			this.set = new Set(connection, dataset, cubeName, this);
			this.positions = new PositionCollection(connection, this.set, cubeName);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0002404C File Offset: 0x0002224C
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00024054 File Offset: 0x00022254
		public string Name
		{
			get
			{
				return this.Set.AxisDataset.DataSetName;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00024066 File Offset: 0x00022266
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0002406E File Offset: 0x0002226E
		public PositionCollection Positions
		{
			get
			{
				return this.positions;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00024076 File Offset: 0x00022276
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cellset;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0002407E File Offset: 0x0002227E
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.axisOrdinal;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00024086 File Offset: 0x00022286
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Axis);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00024092 File Offset: 0x00022292
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000240B5 File Offset: 0x000222B5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000240C3 File Offset: 0x000222C3
		public static bool operator ==(Axis o1, Axis o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000240CC File Offset: 0x000222CC
		public static bool operator !=(Axis o1, Axis o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x040004FB RID: 1275
		private CellSet cellset;

		// Token: 0x040004FC RID: 1276
		private int axisOrdinal = -1;

		// Token: 0x040004FD RID: 1277
		private Set set;

		// Token: 0x040004FE RID: 1278
		private PositionCollection positions;

		// Token: 0x040004FF RID: 1279
		private int hashCode;

		// Token: 0x04000500 RID: 1280
		private bool hashCodeCalculated;
	}
}
