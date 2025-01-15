using System;
using Microsoft.InfoNav.Data.Library;

namespace Microsoft.InfoNav
{
	// Token: 0x02000042 RID: 66
	internal static class ConceptualSchemaExtensions
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00002B6D File Offset: 0x00000D6D
		internal static IFederatedConceptualSchema ToFederatedSchema(this IConceptualSchema schema)
		{
			if (schema == null)
			{
				return null;
			}
			return new FederatedConceptualSchema(new IConceptualSchema[] { schema });
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002B84 File Offset: 0x00000D84
		internal static IConceptualColumn GetColumn(this IConceptualSchema schema, string entityName, string columnName)
		{
			IConceptualProperty conceptualProperty = null;
			IConceptualEntity conceptualEntity;
			if (!schema.TryGetEntity(entityName, out conceptualEntity) || !conceptualEntity.TryGetProperty(columnName, out conceptualProperty))
			{
				Contract.Check(false, "Unable to find the entity or the property in the schema.");
			}
			return conceptualProperty.AsColumn();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00002BBC File Offset: 0x00000DBC
		internal static IConceptualEntity GetEntity(this IConceptualSchema schema, string referenceName)
		{
			IConceptualEntity conceptualEntity;
			Contract.Check(schema.TryGetEntity(referenceName, out conceptualEntity), "Unable to find the entity in the schema.");
			return conceptualEntity;
		}
	}
}
