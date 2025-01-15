using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007C1 RID: 1985
	public class DateTimeMask
	{
		// Token: 0x06003F14 RID: 16148 RVA: 0x000D3B5B File Offset: 0x000D1D5B
		public DateTimeMask(string mask, DateTimeMaskType type, bool db2ToSql)
		{
			this._mask = mask;
			this._type = type;
			this._db2ToSql = db2ToSql;
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000D3B7F File Offset: 0x000D1D7F
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x000D3B87 File Offset: 0x000D1D87
		public string Mask
		{
			get
			{
				return this._mask;
			}
			set
			{
				this._mask = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000D3B90 File Offset: 0x000D1D90
		// (set) Token: 0x06003F18 RID: 16152 RVA: 0x000D3B98 File Offset: 0x000D1D98
		public DateTimeMaskType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000D3BA1 File Offset: 0x000D1DA1
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x000D3BA9 File Offset: 0x000D1DA9
		public bool Db2ToSql
		{
			get
			{
				return this._db2ToSql;
			}
			set
			{
				this._db2ToSql = value;
			}
		}

		// Token: 0x04002BB5 RID: 11189
		private string _mask;

		// Token: 0x04002BB6 RID: 11190
		private DateTimeMaskType _type;

		// Token: 0x04002BB7 RID: 11191
		private bool _db2ToSql = true;
	}
}
