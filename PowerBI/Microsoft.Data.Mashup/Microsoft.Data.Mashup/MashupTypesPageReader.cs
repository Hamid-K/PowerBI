using System;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.OleDb;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000042 RID: 66
	internal class MashupTypesPageReader : DelegatingPageReader
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000D064 File Offset: 0x0000B264
		public static IPageReader New(IPageReader pageReader)
		{
			TableSchema tableSchema = pageReader.Schema;
			if (tableSchema.Any((SchemaColumn c) => c.ProviderSpecificDataType != null))
			{
				TableSchema tableSchema2 = tableSchema.Copy();
				foreach (SchemaColumn schemaColumn in tableSchema2)
				{
					if (schemaColumn.ProviderSpecificDataType == typeof(Date))
					{
						schemaColumn.DataType = typeof(Date);
					}
					else if (schemaColumn.ProviderSpecificDataType == typeof(Time))
					{
						schemaColumn.DataType = typeof(Time);
					}
				}
				return new MashupTypesPageReader(tableSchema2, pageReader);
			}
			return pageReader;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D138 File Offset: 0x0000B338
		private MashupTypesPageReader(TableSchema schema, IPageReader pageReader)
			: base(pageReader)
		{
			this.schema = schema;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D148 File Offset: 0x0000B348
		public override TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x040001AA RID: 426
		private readonly TableSchema schema;
	}
}
