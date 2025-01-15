using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A60 RID: 2656
	public class ResultSet
	{
		// Token: 0x060052E8 RID: 21224 RVA: 0x00150858 File Offset: 0x0014EA58
		internal ResultSet(ResultSet resultSet)
		{
			this.Columns = new List<Column>();
			this._resultSet = resultSet;
			this.Sync(true);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00150879 File Offset: 0x0014EA79
		public ResultSet()
		{
			this.Columns = new List<Column>();
			this._resultSet = new ResultSet();
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060052EA RID: 21226 RVA: 0x00150897 File Offset: 0x0014EA97
		// (set) Token: 0x060052EB RID: 21227 RVA: 0x0015089F File Offset: 0x0014EA9F
		public List<Column> Columns { get; private set; }

		// Token: 0x060052EC RID: 21228 RVA: 0x001508A8 File Offset: 0x0014EAA8
		internal ResultSet ToResultSet()
		{
			this.Sync(false);
			return this._resultSet;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x001508B8 File Offset: 0x0014EAB8
		private void Sync(bool isRead)
		{
			if (isRead)
			{
				this.Columns.Clear();
				using (List<Column>.Enumerator enumerator = this._resultSet.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Column column = enumerator.Current;
						this.Columns.Add(new Column(column));
					}
					return;
				}
			}
			this._resultSet.Columns.Clear();
			foreach (Column column2 in this.Columns)
			{
				this._resultSet.Columns.Add(column2.ToColumn());
			}
		}

		// Token: 0x04004157 RID: 16727
		private ResultSet _resultSet;
	}
}
