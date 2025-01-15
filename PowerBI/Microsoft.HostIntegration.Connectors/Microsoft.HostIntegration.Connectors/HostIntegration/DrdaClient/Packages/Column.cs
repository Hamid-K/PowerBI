using System;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A5E RID: 2654
	public class Column
	{
		// Token: 0x060052C7 RID: 21191 RVA: 0x0015067C File Offset: 0x0014E87C
		internal Column(Column column)
		{
			this._column = column;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0015068B File Offset: 0x0014E88B
		public Column()
		{
			this._column = new Column();
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x0015069E File Offset: 0x0014E89E
		// (set) Token: 0x060052CA RID: 21194 RVA: 0x001506AB File Offset: 0x0014E8AB
		public string Name
		{
			get
			{
				return this._column.Name;
			}
			set
			{
				this._column.Name = value;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x001506BC File Offset: 0x0014E8BC
		public string Type
		{
			get
			{
				return this._column.Type.ToString();
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x001506E2 File Offset: 0x0014E8E2
		// (set) Token: 0x060052CD RID: 21197 RVA: 0x001506EF File Offset: 0x0014E8EF
		public short Ccsid
		{
			get
			{
				return this._column.Ccsid;
			}
			set
			{
				this._column.Ccsid = value;
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060052CE RID: 21198 RVA: 0x001506FD File Offset: 0x0014E8FD
		// (set) Token: 0x060052CF RID: 21199 RVA: 0x0015070A File Offset: 0x0014E90A
		public short Length
		{
			get
			{
				return this._column.Length;
			}
			set
			{
				this._column.Length = value;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060052D0 RID: 21200 RVA: 0x00150718 File Offset: 0x0014E918
		// (set) Token: 0x060052D1 RID: 21201 RVA: 0x00150725 File Offset: 0x0014E925
		public short Scale
		{
			get
			{
				return this._column.Scale;
			}
			set
			{
				this._column.Scale = value;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00150733 File Offset: 0x0014E933
		// (set) Token: 0x060052D3 RID: 21203 RVA: 0x00150740 File Offset: 0x0014E940
		public short Precision
		{
			get
			{
				return this._column.Precision;
			}
			set
			{
				this._column.Precision = value;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x0015074E File Offset: 0x0014E94E
		// (set) Token: 0x060052D5 RID: 21205 RVA: 0x0015075B File Offset: 0x0014E95B
		public bool Nullable
		{
			get
			{
				return this._column.Nullable;
			}
			set
			{
				this._column.Nullable = value;
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00150769 File Offset: 0x0014E969
		internal Column ToColumn()
		{
			return this._column;
		}

		// Token: 0x04004155 RID: 16725
		private Column _column;
	}
}
