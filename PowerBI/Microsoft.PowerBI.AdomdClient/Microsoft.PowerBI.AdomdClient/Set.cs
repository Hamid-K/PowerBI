using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EF RID: 239
	public sealed class Set : ISubordinateObject
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x0002F987 File Offset: 0x0002DB87
		internal Set(AdomdConnection connection, IDSFDataSet dataset, string cubeName, Axis axis)
		{
			this.axisDataset = dataset;
			this.cubeName = cubeName;
			this.hierarchies = new HierarchyCollection(connection, this, cubeName);
			this.tuples = new TupleCollection(connection, this, cubeName);
			this.axis = axis;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002F9C1 File Offset: 0x0002DBC1
		internal IDSFDataSet AxisDataset
		{
			get
			{
				return this.axisDataset;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002F9C9 File Offset: 0x0002DBC9
		public HierarchyCollection Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002F9D1 File Offset: 0x0002DBD1
		public TupleCollection Tuples
		{
			get
			{
				return this.tuples;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002F9D9 File Offset: 0x0002DBD9
		object ISubordinateObject.Parent
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002F9E1 File Offset: 0x0002DBE1
		int ISubordinateObject.Ordinal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002F9E4 File Offset: 0x0002DBE4
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Set);
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002FA13 File Offset: 0x0002DC13
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002FA21 File Offset: 0x0002DC21
		public static bool operator ==(Set o1, Set o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002FA2A File Offset: 0x0002DC2A
		public static bool operator !=(Set o1, Set o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002FA36 File Offset: 0x0002DC36
		internal Axis ParentAxis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x04000837 RID: 2103
		private HierarchyCollection hierarchies;

		// Token: 0x04000838 RID: 2104
		private TupleCollection tuples;

		// Token: 0x04000839 RID: 2105
		private IDSFDataSet axisDataset;

		// Token: 0x0400083A RID: 2106
		private string cubeName;

		// Token: 0x0400083B RID: 2107
		private Axis axis;

		// Token: 0x0400083C RID: 2108
		private int hashCode;

		// Token: 0x0400083D RID: 2109
		private bool hashCodeCalculated;
	}
}
