using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000047 RID: 71
	public static class FederatedConceptualSchemaExtensions
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00002BDD File Offset: 0x00000DDD
		public static bool TryGetDefaultSchema(this IFederatedConceptualSchema federatedConceptualSchema, out IConceptualSchema schema)
		{
			return federatedConceptualSchema.TryGetSchema("", out schema);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static bool TryGetEntity(this IFederatedConceptualSchema federatedConceptualSchema, string entityName, string schemaId, out IConceptualEntity entity)
		{
			IConceptualSchema conceptualSchema;
			if (federatedConceptualSchema.TryGetSchema(schemaId, out conceptualSchema))
			{
				return conceptualSchema.TryGetEntity(entityName, out entity);
			}
			entity = null;
			return false;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002C14 File Offset: 0x00000E14
		internal static bool TryGetEntityByEdmName(this IFederatedConceptualSchema federatedConceptualSchema, string entityQualifiedName, string schemaId, out IConceptualEntity entity)
		{
			IConceptualSchema conceptualSchema;
			if (federatedConceptualSchema.TryGetSchema(schemaId, out conceptualSchema))
			{
				return conceptualSchema.TryGetEntityByEdmName(entityQualifiedName, out entity);
			}
			entity = null;
			return false;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00002C39 File Offset: 0x00000E39
		public static bool IsDefaultSchema(this IConceptualSchema schema)
		{
			return ConceptualNameComparer.Instance.Equals(schema.SchemaId, "");
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002C50 File Offset: 0x00000E50
		public static bool TryGetProperty(this IFederatedConceptualSchema federatedSchema, string schemaId, string entityName, string propertyName, out IConceptualProperty property)
		{
			property = null;
			IConceptualEntity conceptualEntity;
			return federatedSchema.TryGetEntity(entityName, schemaId, out conceptualEntity) && conceptualEntity.TryGetProperty(propertyName, out property);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00002C78 File Offset: 0x00000E78
		public static IConceptualSchema GetDefaultSchema(this IFederatedConceptualSchema federatedSchema)
		{
			IConceptualSchema conceptualSchema = null;
			if (federatedSchema != null && !federatedSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				Contract.Check(false, "Fail to get default schema from IFederatedConceptualSchema");
			}
			return conceptualSchema;
		}
	}
}
