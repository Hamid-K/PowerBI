using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Utils
{
	// Token: 0x02000017 RID: 23
	internal static class QdmHelpers
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004B9C File Offset: 0x00002D9C
		internal static QueryExpression BuildFieldExpression(string tableName, string columnName, Version modelVersion, IConceptualSchema schema)
		{
			ConceptualTypeColumn conceptualTypeColumn = new ConceptualTypeColumn(ConceptualPrimitiveResultType.Text, columnName);
			IConceptualEntity conceptualEntity = null;
			EntitySet entitySet = null;
			if (schema != null)
			{
				conceptualEntity = TransientEdmItemFactory.BuildEntity(tableName, schema, new ConceptualTypeColumn[] { conceptualTypeColumn });
			}
			else
			{
				entitySet = TransientEdmItemFactory.BuildEntitySet(tableName, modelVersion, new ConceptualTypeColumn[] { conceptualTypeColumn });
			}
			return QdmHelpers.BuildFieldExpression(entitySet, columnName, conceptualEntity);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004BEC File Offset: 0x00002DEC
		internal static QueryExpression BuildFieldExpression(EntitySet entitySet, string columnName, IConceptualEntity entity)
		{
			QueryFieldExpression queryFieldExpression;
			if (entity != null)
			{
				IConceptualColumn conceptualColumn = entity.GetPropertyByEdmName(columnName).AsColumn();
				queryFieldExpression = entity.ScalarEntity().Field(conceptualColumn);
			}
			else
			{
				ConceptualPrimitiveResultType text = ConceptualPrimitiveResultType.Text;
				EdmField edmField = TransientEdmItemFactory.CreateField(entitySet, columnName, text);
				queryFieldExpression = entitySet.ScalarEntity(null).Field(edmField, null);
			}
			return queryFieldExpression;
		}
	}
}
