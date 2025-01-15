using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7A RID: 7802
	internal class UnavailableColumn : Column
	{
		// Token: 0x17002F19 RID: 12057
		// (get) Token: 0x0600C051 RID: 49233 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override ColumnType Type
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002F1A RID: 12058
		// (get) Token: 0x0600C052 RID: 49234 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override int RowCount
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600C053 RID: 49235 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void AddNull()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C054 RID: 49236 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void AddValue(object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C055 RID: 49237 RVA: 0x0000EE09 File Offset: 0x0000D009
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C056 RID: 49238 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Clear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C057 RID: 49239 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Deserialize(PageReader reader)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C058 RID: 49240 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override object GetObject(int row)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C059 RID: 49241 RVA: 0x0026BB36 File Offset: 0x00269D36
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Zero;
			return DBSTATUS.E_UNAVAILABLE;
		}

		// Token: 0x0600C05A RID: 49242 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override bool IsNull(int row)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C05B RID: 49243 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override void Serialize(PageWriter writer)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C05C RID: 49244 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override bool TryAddValue(object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04006144 RID: 24900
		public static readonly UnavailableColumn Instance = new UnavailableColumn();
	}
}
