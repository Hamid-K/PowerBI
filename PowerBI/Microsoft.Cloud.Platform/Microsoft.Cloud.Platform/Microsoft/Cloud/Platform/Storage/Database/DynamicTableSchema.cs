using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000053 RID: 83
	public sealed class DynamicTableSchema : TableSchema
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00006896 File Offset: 0x00004A96
		public DynamicTableSchema(string name, IEnumerable<ColumnDefinition> columns)
			: base(name, columns)
		{
			this.m_columns = columns;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000068A7 File Offset: 0x00004AA7
		protected override IEnumerable<ColumnDefinition> GetColumnDefinitions()
		{
			return this.m_columns;
		}

		// Token: 0x040000DF RID: 223
		private IEnumerable<ColumnDefinition> m_columns;
	}
}
