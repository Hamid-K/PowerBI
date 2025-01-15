using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AB RID: 171
	public sealed class MemberProperty : ISubordinateObject
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00029AC4 File Offset: 0x00027CC4
		internal MemberProperty(DataRow memberAxisRow, int index, Member parentMember)
		{
			if (memberAxisRow == null)
			{
				throw new ArgumentNullException("memberAxisRow");
			}
			if (index >= memberAxisRow.Table.Columns.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.memberAxisRow = memberAxisRow;
			this.index = index;
			this.parentMember = parentMember;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00029B23 File Offset: 0x00027D23
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00029B2C File Offset: 0x00027D2C
		public string Name
		{
			get
			{
				if (this.index >= this.memberAxisRow.Table.Columns.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.memberAxisRow.Table.Columns[this.index].ExtendedProperties["MemberPropertyUnqualifiedName"] as string;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00029B90 File Offset: 0x00027D90
		public string UniqueName
		{
			get
			{
				if (this.index >= this.memberAxisRow.Table.Columns.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.memberAxisRow.Table.Columns[this.index].Caption;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00029BE8 File Offset: 0x00027DE8
		public object Value
		{
			get
			{
				if (this.index >= this.memberAxisRow.Table.Columns.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object property = AdomdUtils.GetProperty(this.memberAxisRow, this.index);
				if (property is DBNull)
				{
					return null;
				}
				return property;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00029C3A File Offset: 0x00027E3A
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentMember;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00029C42 File Offset: 0x00027E42
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00029C4A File Offset: 0x00027E4A
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(MemberProperty);
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00029C56 File Offset: 0x00027E56
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00029C79 File Offset: 0x00027E79
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00029C87 File Offset: 0x00027E87
		public static bool operator ==(MemberProperty o1, MemberProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00029C90 File Offset: 0x00027E90
		public static bool operator !=(MemberProperty o1, MemberProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000676 RID: 1654
		private DataRow memberAxisRow;

		// Token: 0x04000677 RID: 1655
		private int index = -1;

		// Token: 0x04000678 RID: 1656
		private Member parentMember;

		// Token: 0x04000679 RID: 1657
		private int hashCode;

		// Token: 0x0400067A RID: 1658
		private bool hashCodeCalculated;
	}
}
