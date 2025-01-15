using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F1 RID: 241
	public sealed class Tuple : ISubordinateObject
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x0002FD8D File Offset: 0x0002DF8D
		internal Tuple(AdomdConnection connection, Set axis, int tupleOrdinal, string cubeName)
		{
			this.axis = axis;
			this.tupleOrdinal = tupleOrdinal;
			this.members = new MemberCollection(connection, this, cubeName);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002FDB2 File Offset: 0x0002DFB2
		internal Set Axis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002FDBA File Offset: 0x0002DFBA
		public int TupleOrdinal
		{
			get
			{
				return this.tupleOrdinal;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002FDC2 File Offset: 0x0002DFC2
		public MemberCollection Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002FDCA File Offset: 0x0002DFCA
		object ISubordinateObject.Parent
		{
			get
			{
				return this.Axis.ParentAxis;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002FDD7 File Offset: 0x0002DFD7
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.TupleOrdinal;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002FDDF File Offset: 0x0002DFDF
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Tuple);
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002FDEB File Offset: 0x0002DFEB
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002FE0E File Offset: 0x0002E00E
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002FE1C File Offset: 0x0002E01C
		public static bool operator ==(Tuple o1, Tuple o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002FE25 File Offset: 0x0002E025
		public static bool operator !=(Tuple o1, Tuple o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000840 RID: 2112
		private Set axis;

		// Token: 0x04000841 RID: 2113
		private int tupleOrdinal;

		// Token: 0x04000842 RID: 2114
		private MemberCollection members;

		// Token: 0x04000843 RID: 2115
		private int hashCode;

		// Token: 0x04000844 RID: 2116
		private bool hashCodeCalculated;
	}
}
