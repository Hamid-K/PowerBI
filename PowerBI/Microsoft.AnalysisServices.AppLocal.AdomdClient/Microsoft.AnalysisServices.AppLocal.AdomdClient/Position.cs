using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E8 RID: 232
	public sealed class Position : ISubordinateObject
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002F72C File Offset: 0x0002D92C
		internal Position(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.tuple = tuple;
			this.members = new MemberCollection(connection, tuple, cubeName);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002F749 File Offset: 0x0002D949
		public int Ordinal
		{
			get
			{
				return this.tuple.TupleOrdinal;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002F756 File Offset: 0x0002D956
		public MemberCollection Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002F75E File Offset: 0x0002D95E
		object ISubordinateObject.Parent
		{
			get
			{
				return this.tuple;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002F766 File Offset: 0x0002D966
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.Ordinal;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002F76E File Offset: 0x0002D96E
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Position);
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002F77A File Offset: 0x0002D97A
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002F79D File Offset: 0x0002D99D
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002F7AB File Offset: 0x0002D9AB
		public static bool operator ==(Position o1, Position o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		public static bool operator !=(Position o1, Position o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000816 RID: 2070
		private MemberCollection members;

		// Token: 0x04000817 RID: 2071
		private Tuple tuple;

		// Token: 0x04000818 RID: 2072
		private int hashCode;

		// Token: 0x04000819 RID: 2073
		private bool hashCodeCalculated;
	}
}
