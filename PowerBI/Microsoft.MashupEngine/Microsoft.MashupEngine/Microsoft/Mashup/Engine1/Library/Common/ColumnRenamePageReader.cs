using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001037 RID: 4151
	internal sealed class ColumnRenamePageReader : DelegatingPageReader
	{
		// Token: 0x06006C4F RID: 27727 RVA: 0x001754D8 File Offset: 0x001736D8
		public ColumnRenamePageReader(IPageReader reader, string[] columnNames)
			: base(reader)
		{
			this.columnNames = columnNames;
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06006C50 RID: 27728 RVA: 0x001754E8 File Offset: 0x001736E8
		public override TableSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					TableSchema tableSchema = base.Schema;
					TableSchema tableSchema2 = new TableSchema(tableSchema.ColumnCount);
					for (int i = 0; i < tableSchema.ColumnCount; i++)
					{
						string text = ((i < this.columnNames.Length) ? this.columnNames[i] : null);
						tableSchema2.AddColumn(tableSchema.GetColumn(i).Clone(text));
					}
					this.schema = tableSchema2;
				}
				return this.schema;
			}
		}

		// Token: 0x04003C48 RID: 15432
		private readonly string[] columnNames;

		// Token: 0x04003C49 RID: 15433
		private TableSchema schema;
	}
}
