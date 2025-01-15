using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000071 RID: 113
	internal sealed class AxisTupleMemberCollection : IMemberCollectionInternal
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00024406 File Offset: 0x00022606
		internal AxisTupleMemberCollection(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.connection = connection;
			this.tuple = tuple;
			this.internalAxisMembers = tuple.Axis.AxisDataset;
			this.cubeName = cubeName;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00024434 File Offset: 0x00022634
		public int Count
		{
			get
			{
				return this.internalAxisMembers.Count;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00024441 File Offset: 0x00022641
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00024444 File Offset: 0x00022644
		public object SyncRoot
		{
			get
			{
				return this.internalAxisMembers.SyncRoot;
			}
		}

		// Token: 0x170001DE RID: 478
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

		// Token: 0x0600073B RID: 1851 RVA: 0x000244BC File Offset: 0x000226BC
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

		// Token: 0x0400050C RID: 1292
		private Tuple tuple;

		// Token: 0x0400050D RID: 1293
		private AdomdConnection connection;

		// Token: 0x0400050E RID: 1294
		private IDSFDataSet internalAxisMembers;

		// Token: 0x0400050F RID: 1295
		private string cubeName;
	}
}
