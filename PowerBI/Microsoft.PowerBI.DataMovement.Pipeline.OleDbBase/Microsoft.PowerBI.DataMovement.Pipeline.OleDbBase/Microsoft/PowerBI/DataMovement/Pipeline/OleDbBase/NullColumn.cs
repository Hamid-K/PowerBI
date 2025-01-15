using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002E RID: 46
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NullColumn : Column
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004C08 File Offset: 0x00002E08
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Null;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004C0C File Offset: 0x00002E0C
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004C14 File Offset: 0x00002E14
		public override void AddValue(object value)
		{
			this.AddNull();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004C1C File Offset: 0x00002E1C
		public override bool TryAddValue(object value)
		{
			if (value == null)
			{
				this.AddNull();
				return true;
			}
			return false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004C2A File Offset: 0x00002E2A
		public override void AddNull()
		{
			this.rowCount++;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004C3A File Offset: 0x00002E3A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.AddNull();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004C42 File Offset: 0x00002E42
		public override bool IsNull(int row)
		{
			return true;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004C45 File Offset: 0x00002E45
		public override void Clear()
		{
			this.rowCount = 0;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004C4E File Offset: 0x00002E4E
		public override object GetObject(int row)
		{
			return DBNull.Value;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004C55 File Offset: 0x00002E55
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Zero;
			return DBSTATUS.S_ISNULL;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004C64 File Offset: 0x00002E64
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004C72 File Offset: 0x00002E72
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
		}

		// Token: 0x04000043 RID: 67
		private int rowCount;
	}
}
