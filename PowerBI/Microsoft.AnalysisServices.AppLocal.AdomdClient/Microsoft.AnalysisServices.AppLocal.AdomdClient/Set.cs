using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EF RID: 239
	public sealed class Set : ISubordinateObject
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002FCB7 File Offset: 0x0002DEB7
		internal Set(AdomdConnection connection, IDSFDataSet dataset, string cubeName, Axis axis)
		{
			this.axisDataset = dataset;
			this.cubeName = cubeName;
			this.hierarchies = new HierarchyCollection(connection, this, cubeName);
			this.tuples = new TupleCollection(connection, this, cubeName);
			this.axis = axis;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002FCF1 File Offset: 0x0002DEF1
		internal IDSFDataSet AxisDataset
		{
			get
			{
				return this.axisDataset;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002FCF9 File Offset: 0x0002DEF9
		public HierarchyCollection Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002FD01 File Offset: 0x0002DF01
		public TupleCollection Tuples
		{
			get
			{
				return this.tuples;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0002FD09 File Offset: 0x0002DF09
		object ISubordinateObject.Parent
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002FD11 File Offset: 0x0002DF11
		int ISubordinateObject.Ordinal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002FD14 File Offset: 0x0002DF14
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Set);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002FD20 File Offset: 0x0002DF20
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002FD43 File Offset: 0x0002DF43
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002FD51 File Offset: 0x0002DF51
		public static bool operator ==(Set o1, Set o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002FD5A File Offset: 0x0002DF5A
		public static bool operator !=(Set o1, Set o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002FD66 File Offset: 0x0002DF66
		internal Axis ParentAxis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x04000844 RID: 2116
		private HierarchyCollection hierarchies;

		// Token: 0x04000845 RID: 2117
		private TupleCollection tuples;

		// Token: 0x04000846 RID: 2118
		private IDSFDataSet axisDataset;

		// Token: 0x04000847 RID: 2119
		private string cubeName;

		// Token: 0x04000848 RID: 2120
		private Axis axis;

		// Token: 0x04000849 RID: 2121
		private int hashCode;

		// Token: 0x0400084A RID: 2122
		private bool hashCodeCalculated;
	}
}
