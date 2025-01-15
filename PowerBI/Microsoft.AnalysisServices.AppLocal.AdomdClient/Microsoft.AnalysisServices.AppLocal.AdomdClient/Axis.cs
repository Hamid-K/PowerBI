using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006E RID: 110
	public sealed class Axis : ISubordinateObject
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00024330 File Offset: 0x00022530
		internal Axis(AdomdConnection connection, IDSFDataSet dataset, string cubeName, CellSet cellSet, int axisOrdinal)
		{
			this.cellset = cellSet;
			this.axisOrdinal = axisOrdinal;
			this.set = new Set(connection, dataset, cubeName, this);
			this.positions = new PositionCollection(connection, this.set, cubeName);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002437C File Offset: 0x0002257C
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00024384 File Offset: 0x00022584
		public string Name
		{
			get
			{
				return this.Set.AxisDataset.DataSetName;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00024396 File Offset: 0x00022596
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0002439E File Offset: 0x0002259E
		public PositionCollection Positions
		{
			get
			{
				return this.positions;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000243A6 File Offset: 0x000225A6
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cellset;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000243AE File Offset: 0x000225AE
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.axisOrdinal;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000243B6 File Offset: 0x000225B6
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Axis);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000243C2 File Offset: 0x000225C2
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000243E5 File Offset: 0x000225E5
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000243F3 File Offset: 0x000225F3
		public static bool operator ==(Axis o1, Axis o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000243FC File Offset: 0x000225FC
		public static bool operator !=(Axis o1, Axis o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000508 RID: 1288
		private CellSet cellset;

		// Token: 0x04000509 RID: 1289
		private int axisOrdinal = -1;

		// Token: 0x0400050A RID: 1290
		private Set set;

		// Token: 0x0400050B RID: 1291
		private PositionCollection positions;

		// Token: 0x0400050C RID: 1292
		private int hashCode;

		// Token: 0x0400050D RID: 1293
		private bool hashCodeCalculated;
	}
}
