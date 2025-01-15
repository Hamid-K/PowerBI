using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E72 RID: 7794
	internal class GuidColumn : Column<Guid>
	{
		// Token: 0x0600C001 RID: 49153 RVA: 0x0026B461 File Offset: 0x00269661
		public GuidColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F0D RID: 12045
		// (get) Token: 0x0600C002 RID: 49154 RVA: 0x000E78AA File Offset: 0x000E5AAA
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Guid;
			}
		}

		// Token: 0x0600C003 RID: 49155 RVA: 0x0026B46A File Offset: 0x0026966A
		public override void AddGuid(Guid value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600C004 RID: 49156 RVA: 0x0026B473 File Offset: 0x00269673
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(*(Guid*)value);
		}

		// Token: 0x0600C005 RID: 49157 RVA: 0x0026B481 File Offset: 0x00269681
		public override Guid GetGuid(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x0600C006 RID: 49158 RVA: 0x0026B48C File Offset: 0x0026968C
		public override string GetString(int row)
		{
			return base.GetValue(row).ToString();
		}

		// Token: 0x0600C007 RID: 49159 RVA: 0x0026B4B0 File Offset: 0x002696B0
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			Guid value = base.GetValue(row);
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600C008 RID: 49160 RVA: 0x0026B4D1 File Offset: 0x002696D1
		protected override void Serialize(PageWriter writer, Guid[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600C009 RID: 49161 RVA: 0x0026B4DD File Offset: 0x002696DD
		protected override void Deserialize(PageReader reader, Guid[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
