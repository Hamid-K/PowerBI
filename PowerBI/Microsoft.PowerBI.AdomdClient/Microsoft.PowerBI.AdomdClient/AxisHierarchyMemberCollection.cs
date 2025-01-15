using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000070 RID: 112
	internal sealed class AxisHierarchyMemberCollection : IMemberCollectionInternal
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x00024294 File Offset: 0x00022494
		internal AxisHierarchyMemberCollection(AdomdConnection connection, DataTable memberHierarchyDataTable, string cubeName, Level parentLevel, Member parentMember)
		{
			this.connection = connection;
			this.memberHierarchyDataTable = memberHierarchyDataTable;
			this.cubeName = cubeName;
			this.parentLevel = parentLevel;
			this.parentMember = parentMember;
			this.catalog = this.connection.CatalogConnectionStringProperty;
			this.sessionId = this.connection.SessionID;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000242EE File Offset: 0x000224EE
		public int Count
		{
			get
			{
				return this.memberHierarchyDataTable.Rows.Count;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00024300 File Offset: 0x00022500
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00024303 File Offset: 0x00022503
		public object SyncRoot
		{
			get
			{
				return this.memberHierarchyDataTable.Rows.SyncRoot;
			}
		}

		// Token: 0x170001DA RID: 474
		public Member this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return new Member(this.connection, this.memberHierarchyDataTable.Rows[index], this.parentLevel, this.parentMember, MemberOrigin.InternalMemberQuery, this.cubeName, null, index, this.catalog, this.sessionId);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002437C File Offset: 0x0002257C
		public Member Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			if (!this.memberHierarchyDataTable.Columns.Contains("MEMBER_NAME"))
			{
				throw new NotSupportedException();
			}
			string dataTableFilter = AdomdUtils.GetDataTableFilter("MEMBER_NAME", index);
			DataRow[] array = this.memberHierarchyDataTable.Select(dataTableFilter);
			Member member;
			if (array.Length != 0)
			{
				member = new Member(this.connection, array[0], this.parentLevel, this.parentMember, MemberOrigin.InternalMemberQuery, this.cubeName, null, -1, this.catalog, this.sessionId);
			}
			else
			{
				member = null;
			}
			return member;
		}

		// Token: 0x04000505 RID: 1285
		private AdomdConnection connection;

		// Token: 0x04000506 RID: 1286
		private DataTable memberHierarchyDataTable;

		// Token: 0x04000507 RID: 1287
		private string cubeName;

		// Token: 0x04000508 RID: 1288
		private Level parentLevel;

		// Token: 0x04000509 RID: 1289
		private Member parentMember;

		// Token: 0x0400050A RID: 1290
		private string catalog;

		// Token: 0x0400050B RID: 1291
		private string sessionId;
	}
}
