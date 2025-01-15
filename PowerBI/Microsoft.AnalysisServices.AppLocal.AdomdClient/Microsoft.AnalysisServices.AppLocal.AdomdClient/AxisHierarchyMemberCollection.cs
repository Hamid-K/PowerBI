using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000070 RID: 112
	internal sealed class AxisHierarchyMemberCollection : IMemberCollectionInternal
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x000245C4 File Offset: 0x000227C4
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0002461E File Offset: 0x0002281E
		public int Count
		{
			get
			{
				return this.memberHierarchyDataTable.Rows.Count;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00024630 File Offset: 0x00022830
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00024633 File Offset: 0x00022833
		public object SyncRoot
		{
			get
			{
				return this.memberHierarchyDataTable.Rows.SyncRoot;
			}
		}

		// Token: 0x170001E0 RID: 480
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

		// Token: 0x06000742 RID: 1858 RVA: 0x000246AC File Offset: 0x000228AC
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

		// Token: 0x04000512 RID: 1298
		private AdomdConnection connection;

		// Token: 0x04000513 RID: 1299
		private DataTable memberHierarchyDataTable;

		// Token: 0x04000514 RID: 1300
		private string cubeName;

		// Token: 0x04000515 RID: 1301
		private Level parentLevel;

		// Token: 0x04000516 RID: 1302
		private Member parentMember;

		// Token: 0x04000517 RID: 1303
		private string catalog;

		// Token: 0x04000518 RID: 1304
		private string sessionId;
	}
}
