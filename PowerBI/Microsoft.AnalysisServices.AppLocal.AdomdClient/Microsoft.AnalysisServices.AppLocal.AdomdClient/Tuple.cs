using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F1 RID: 241
	public sealed class Tuple : ISubordinateObject
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x000300BD File Offset: 0x0002E2BD
		internal Tuple(AdomdConnection connection, Set axis, int tupleOrdinal, string cubeName)
		{
			this.axis = axis;
			this.tupleOrdinal = tupleOrdinal;
			this.members = new MemberCollection(connection, this, cubeName);
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000300E2 File Offset: 0x0002E2E2
		internal Set Axis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000300EA File Offset: 0x0002E2EA
		public int TupleOrdinal
		{
			get
			{
				return this.tupleOrdinal;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000300F2 File Offset: 0x0002E2F2
		public MemberCollection Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000300FA File Offset: 0x0002E2FA
		object ISubordinateObject.Parent
		{
			get
			{
				return this.Axis.ParentAxis;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00030107 File Offset: 0x0002E307
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.TupleOrdinal;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0003010F File Offset: 0x0002E30F
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Tuple);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003011B File Offset: 0x0002E31B
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003013E File Offset: 0x0002E33E
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003014C File Offset: 0x0002E34C
		public static bool operator ==(Tuple o1, Tuple o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00030155 File Offset: 0x0002E355
		public static bool operator !=(Tuple o1, Tuple o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x0400084D RID: 2125
		private Set axis;

		// Token: 0x0400084E RID: 2126
		private int tupleOrdinal;

		// Token: 0x0400084F RID: 2127
		private MemberCollection members;

		// Token: 0x04000850 RID: 2128
		private int hashCode;

		// Token: 0x04000851 RID: 2129
		private bool hashCodeCalculated;
	}
}
