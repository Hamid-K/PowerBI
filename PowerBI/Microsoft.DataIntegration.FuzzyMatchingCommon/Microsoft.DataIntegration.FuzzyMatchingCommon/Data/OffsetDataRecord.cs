using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000059 RID: 89
	public class OffsetDataRecord : DataRecordImplBase
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0001556D File Offset: 0x0001376D
		public OffsetDataRecord(DataTable schema, int offset)
			: this(schema, offset, schema.Rows.Count - offset)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00015584 File Offset: 0x00013784
		public OffsetDataRecord(DataTable schema, int offset, int length)
		{
			base.Schema = schema;
			this.m_offset = offset;
			for (int i = 0; i < this.m_offset; i++)
			{
				base.Schema.Rows.RemoveAt(0);
			}
			while (base.Schema.Rows.Count > length)
			{
				base.Schema.Rows.RemoveAt(base.Schema.Rows.Count - 1);
			}
			int ordinal = base.Schema.Columns[SchemaTableColumn.ColumnOrdinal].Ordinal;
			base.Schema.Columns[ordinal].ReadOnly = false;
			for (int j = 0; j < base.Schema.Rows.Count; j++)
			{
				base.Schema.Rows[j].BeginEdit();
				base.Schema.Rows[j][ordinal] = j;
				base.Schema.Rows[j].EndEdit();
				base.Schema.Rows[j].AcceptChanges();
			}
			SchemaUtils.CreateIndexOnColumnOrdinal(base.Schema);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000156B5 File Offset: 0x000138B5
		public void SetRecord(IDataRecord record)
		{
			this.m_record = record;
		}

		// Token: 0x17000076 RID: 118
		public override object this[int i]
		{
			get
			{
				return this.m_record[i + this.m_offset];
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000156DA File Offset: 0x000138DA
		public override DataTable GetSchemaTable()
		{
			return base.Schema;
		}

		// Token: 0x04000079 RID: 121
		public IDataRecord m_record;

		// Token: 0x0400007A RID: 122
		private int m_offset;
	}
}
