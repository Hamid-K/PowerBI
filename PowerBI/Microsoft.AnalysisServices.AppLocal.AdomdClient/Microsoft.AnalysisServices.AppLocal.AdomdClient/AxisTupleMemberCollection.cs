using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000071 RID: 113
	internal sealed class AxisTupleMemberCollection : IMemberCollectionInternal
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00024736 File Offset: 0x00022936
		internal AxisTupleMemberCollection(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.connection = connection;
			this.tuple = tuple;
			this.internalAxisMembers = tuple.Axis.AxisDataset;
			this.cubeName = cubeName;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00024764 File Offset: 0x00022964
		public int Count
		{
			get
			{
				return this.internalAxisMembers.Count;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00024771 File Offset: 0x00022971
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00024774 File Offset: 0x00022974
		public object SyncRoot
		{
			get
			{
				return this.internalAxisMembers.SyncRoot;
			}
		}

		// Token: 0x170001E4 RID: 484
		public Member this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataTable dataTable = this.internalAxisMembers[index];
				return new Member(this.connection, dataTable.Rows[this.tuple.TupleOrdinal], null, null, MemberOrigin.UserQuery, this.cubeName, this.tuple, index, null, null);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000247EC File Offset: 0x000229EC
		public Member Find(string index)
		{
			int num = 0;
			foreach (object obj in this.internalAxisMembers)
			{
				DataTable dataTable = (DataTable)obj;
				if (dataTable.Columns.Contains("UName") && dataTable.Rows[this.tuple.TupleOrdinal]["UName"].ToString() == index)
				{
					return new Member(this.connection, dataTable.Rows[this.tuple.TupleOrdinal], null, null, MemberOrigin.UserQuery, this.cubeName, this.tuple, num, null, null);
				}
				num++;
			}
			return null;
		}

		// Token: 0x04000519 RID: 1305
		private Tuple tuple;

		// Token: 0x0400051A RID: 1306
		private AdomdConnection connection;

		// Token: 0x0400051B RID: 1307
		private IDSFDataSet internalAxisMembers;

		// Token: 0x0400051C RID: 1308
		private string cubeName;
	}
}
