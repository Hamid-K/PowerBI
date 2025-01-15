using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AB RID: 171
	public sealed class MemberProperty : ISubordinateObject
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00029794 File Offset: 0x00027994
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

		// Token: 0x060009C2 RID: 2498 RVA: 0x000297F3 File Offset: 0x000279F3
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000297FC File Offset: 0x000279FC
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00029860 File Offset: 0x00027A60
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x000298B8 File Offset: 0x00027AB8
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002990A File Offset: 0x00027B0A
		object ISubordinateObject.Parent
		{
			get
			{
				return this.parentMember;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00029912 File Offset: 0x00027B12
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002991A File Offset: 0x00027B1A
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(MemberProperty);
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00029926 File Offset: 0x00027B26
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00029949 File Offset: 0x00027B49
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00029957 File Offset: 0x00027B57
		public static bool operator ==(MemberProperty o1, MemberProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00029960 File Offset: 0x00027B60
		public static bool operator !=(MemberProperty o1, MemberProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000669 RID: 1641
		private DataRow memberAxisRow;

		// Token: 0x0400066A RID: 1642
		private int index = -1;

		// Token: 0x0400066B RID: 1643
		private Member parentMember;

		// Token: 0x0400066C RID: 1644
		private int hashCode;

		// Token: 0x0400066D RID: 1645
		private bool hashCodeCalculated;
	}
}
