using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013C RID: 316
	internal sealed class BatchQueryExtensionSchemaTranslator : QueryExtensionSchemaTranslatorBase
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002F074 File Offset: 0x0002D274
		private BatchQueryExtensionSchemaTranslator(BatchQueryBuilder queryBuilder, FederatedEntityDataModel model, IFederatedConceptualSchema schema, IBatchQueryExtensionSchemaExpressionGenerator exprTranslator, bool useConceptualSchema)
			: base(model, schema, useConceptualSchema)
		{
			this.m_queryBuilder = queryBuilder;
			this.m_expressionGenerator = exprTranslator;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002F08F File Offset: 0x0002D28F
		internal static void Translate(ExtensionSchema extensionSchema, BatchQueryBuilder queryBuilder, FederatedEntityDataModel model, IFederatedConceptualSchema schema, IBatchQueryExtensionSchemaExpressionGenerator exprGenerator, bool useConceptualSchema)
		{
			if (extensionSchema == null)
			{
				return;
			}
			new BatchQueryExtensionSchemaTranslator(queryBuilder, model, schema, exprGenerator, useConceptualSchema).Translate(extensionSchema);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002F0A7 File Offset: 0x0002D2A7
		protected override void DeclareMeasure(QueryExpression measureContentExpr, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure)
		{
			this.m_queryBuilder.DeclareMeasure(measureContentExpr, targetEntitySet, edmMeasure, targetEntity, measure);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002F0BC File Offset: 0x0002D2BC
		protected override QueryExpression TranslatePropertyWithExpression(ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			return this.m_expressionGenerator.Generate(property, targetEntitySet, targetEntity).QueryExpression;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002F0D1 File Offset: 0x0002D2D1
		protected override void DeclareField(QueryExpression fieldContentExpr, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity, IConceptualColumn column)
		{
			this.m_queryBuilder.DeclareField(fieldContentExpr, targetEntitySet, field, targetEntity, column);
		}

		// Token: 0x040005E9 RID: 1513
		private readonly BatchQueryBuilder m_queryBuilder;

		// Token: 0x040005EA RID: 1514
		private readonly IBatchQueryExtensionSchemaExpressionGenerator m_expressionGenerator;
	}
}
