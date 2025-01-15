using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002F RID: 47
	public static class ConceptualSchemaNames
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00002916 File Offset: 0x00000B16
		public static string NormalizeForSerialization(string schemaName)
		{
			if (ConceptualNameComparer.Instance.Equals(schemaName, ""))
			{
				return null;
			}
			return schemaName;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000292D File Offset: 0x00000B2D
		public static string NormalizeSchemaNameForSerialization(IConceptualEntity entity)
		{
			return ConceptualSchemaNames.NormalizeForSerialization((entity.Schema == null) ? "" : entity.Schema.SchemaId);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000294E File Offset: 0x00000B4E
		public static string NormalizeForInMemory(string schemaName)
		{
			return schemaName ?? "";
		}

		// Token: 0x040000DA RID: 218
		public const string Default = "";
	}
}
