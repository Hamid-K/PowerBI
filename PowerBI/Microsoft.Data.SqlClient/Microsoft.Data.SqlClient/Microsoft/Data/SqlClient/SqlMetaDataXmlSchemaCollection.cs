using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010F RID: 271
	internal sealed class SqlMetaDataXmlSchemaCollection
	{
		// Token: 0x060015B2 RID: 5554 RVA: 0x0005EB7B File Offset: 0x0005CD7B
		public void CopyFrom(SqlMetaDataXmlSchemaCollection original)
		{
			if (original != null)
			{
				this.Database = original.Database;
				this.OwningSchema = original.OwningSchema;
				this.Name = original.Name;
			}
		}

		// Token: 0x040008A6 RID: 2214
		internal string Database;

		// Token: 0x040008A7 RID: 2215
		internal string OwningSchema;

		// Token: 0x040008A8 RID: 2216
		internal string Name;
	}
}
