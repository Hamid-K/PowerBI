using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E8 RID: 232
	public sealed class Position : ISubordinateObject
	{
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002F3FC File Offset: 0x0002D5FC
		internal Position(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.tuple = tuple;
			this.members = new MemberCollection(connection, tuple, cubeName);
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002F419 File Offset: 0x0002D619
		public int Ordinal
		{
			get
			{
				return this.tuple.TupleOrdinal;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002F426 File Offset: 0x0002D626
		public MemberCollection Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002F42E File Offset: 0x0002D62E
		object ISubordinateObject.Parent
		{
			get
			{
				return this.tuple;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002F436 File Offset: 0x0002D636
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.Ordinal;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002F43E File Offset: 0x0002D63E
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Position);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002F44A File Offset: 0x0002D64A
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002F46D File Offset: 0x0002D66D
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002F47B File Offset: 0x0002D67B
		public static bool operator ==(Position o1, Position o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002F484 File Offset: 0x0002D684
		public static bool operator !=(Position o1, Position o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000809 RID: 2057
		private MemberCollection members;

		// Token: 0x0400080A RID: 2058
		private Tuple tuple;

		// Token: 0x0400080B RID: 2059
		private int hashCode;

		// Token: 0x0400080C RID: 2060
		private bool hashCodeCalculated;
	}
}
