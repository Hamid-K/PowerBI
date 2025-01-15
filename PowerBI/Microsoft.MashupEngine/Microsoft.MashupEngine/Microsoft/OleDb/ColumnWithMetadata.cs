using System;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7F RID: 7807
	internal class ColumnWithMetadata : DelegatingColumn
	{
		// Token: 0x0600C0DE RID: 49374 RVA: 0x0026CC6E File Offset: 0x0026AE6E
		public ColumnWithMetadata(Column data, int maxRowCount)
			: base(data)
		{
			this.metadata = new UntypedRecordColumn(false, maxRowCount);
		}

		// Token: 0x0600C0DF RID: 49375 RVA: 0x0026CC84 File Offset: 0x0026AE84
		public override void AddNull()
		{
			base.AddNull();
			this.metadata.AddValue(DataRecord.Empty);
		}

		// Token: 0x0600C0E0 RID: 49376 RVA: 0x0026CC9C File Offset: 0x0026AE9C
		protected override void AddNotNull()
		{
			this.metadata.AddValue(DataRecord.Empty);
		}

		// Token: 0x0600C0E1 RID: 49377 RVA: 0x0026CCB0 File Offset: 0x0026AEB0
		public override void AddValue(object value)
		{
			ValueWithMetadata valueWithMetadata = (ValueWithMetadata)value;
			Column.AddValueToColumn(base.Column, valueWithMetadata.Value);
			this.metadata.AddValue(valueWithMetadata.Metadata);
		}

		// Token: 0x0600C0E2 RID: 49378 RVA: 0x0026CCE8 File Offset: 0x0026AEE8
		public override bool TryAddValue(object value)
		{
			ValueWithMetadata valueWithMetadata = value as ValueWithMetadata;
			if (valueWithMetadata != null && base.Column.TryAddValue(valueWithMetadata.Value))
			{
				this.metadata.AddValue(valueWithMetadata.Metadata);
				return true;
			}
			return false;
		}

		// Token: 0x0600C0E3 RID: 49379 RVA: 0x0026CD26 File Offset: 0x0026AF26
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(type, value, length);
			this.metadata.AddValue(DataRecord.Empty);
		}

		// Token: 0x0600C0E4 RID: 49380 RVA: 0x0026CD44 File Offset: 0x0026AF44
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			Column displayTextColumn;
			int num;
			this.metadata.TryGetColumn(row, "Documentation.DisplayName", out displayTextColumn, out num);
			Binding binding2;
			Binding binding3;
			if (PageReaderStructuredEntityRowset.TrySplitBinding(binding, out binding2, out binding3))
			{
				DBSTATUS value = base.GetValue(row, dataConvert, binding3, destValue, out destLength);
				if (value == DBSTATUS.S_OK)
				{
					PageReaderStructuredEntityRowset.TryGetData(num, binding2, destValue, dataConvert, (int i) => displayTextColumn);
				}
				return value;
			}
			DBSTATUS value2 = base.GetValue(row, dataConvert, binding, destValue, out destLength);
			if (value2 == DBSTATUS.E_CANTCONVERTVALUE)
			{
				return displayTextColumn.GetValue(num, dataConvert, binding, destValue, out destLength);
			}
			return value2;
		}

		// Token: 0x0600C0E5 RID: 49381 RVA: 0x0026CDCE File Offset: 0x0026AFCE
		public override object GetObject(int row)
		{
			return new ValueWithMetadata
			{
				Value = base.GetObject(row),
				Metadata = this.metadata.GetRecord(row)
			};
		}

		// Token: 0x0600C0E6 RID: 49382 RVA: 0x0026CDF4 File Offset: 0x0026AFF4
		public override void Clear()
		{
			base.Clear();
			this.metadata.Clear();
		}

		// Token: 0x0600C0E7 RID: 49383 RVA: 0x0026CE07 File Offset: 0x0026B007
		public override void Serialize(PageWriter writer)
		{
			base.Serialize(writer);
			this.metadata.Serialize(writer);
		}

		// Token: 0x0600C0E8 RID: 49384 RVA: 0x0026CE1C File Offset: 0x0026B01C
		public override void Deserialize(PageReader reader)
		{
			base.Deserialize(reader);
			this.metadata.Deserialize(reader);
		}

		// Token: 0x04006159 RID: 24921
		private readonly UntypedRecordColumn metadata;
	}
}
