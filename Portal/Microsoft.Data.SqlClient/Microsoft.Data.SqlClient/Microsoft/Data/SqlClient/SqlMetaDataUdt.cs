using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000110 RID: 272
	internal sealed class SqlMetaDataUdt
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x0005EBA4 File Offset: 0x0005CDA4
		public void CopyFrom(SqlMetaDataUdt original)
		{
			if (original != null)
			{
				this.Type = original.Type;
				this.DatabaseName = original.DatabaseName;
				this.SchemaName = original.SchemaName;
				this.TypeName = original.TypeName;
				this.AssemblyQualifiedName = original.AssemblyQualifiedName;
			}
		}

		// Token: 0x040008A9 RID: 2217
		internal Type Type;

		// Token: 0x040008AA RID: 2218
		internal string DatabaseName;

		// Token: 0x040008AB RID: 2219
		internal string SchemaName;

		// Token: 0x040008AC RID: 2220
		internal string TypeName;

		// Token: 0x040008AD RID: 2221
		internal string AssemblyQualifiedName;
	}
}
