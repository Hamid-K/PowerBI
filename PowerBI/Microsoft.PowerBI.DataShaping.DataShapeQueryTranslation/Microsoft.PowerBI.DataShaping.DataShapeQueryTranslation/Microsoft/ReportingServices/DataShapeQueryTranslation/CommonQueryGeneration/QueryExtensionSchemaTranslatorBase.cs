using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x02000118 RID: 280
	internal abstract class QueryExtensionSchemaTranslatorBase
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00028D62 File Offset: 0x00026F62
		protected QueryExtensionSchemaTranslatorBase(FederatedEntityDataModel model, IFederatedConceptualSchema schema, bool useConceptualSchema)
		{
			this.m_model = model;
			this.m_schema = schema;
			this.m_useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00028D80 File Offset: 0x00026F80
		protected void Translate(ExtensionSchema extensionSchema)
		{
			List<ExtensionEntity> entities = extensionSchema.Entities;
			for (int i = 0; i < entities.Count; i++)
			{
				this.TranslateEntity(entities[i], extensionSchema.Name);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00028DB8 File Offset: 0x00026FB8
		private void TranslateEntity(ExtensionEntity extensionEntity, string extensionSchemaName)
		{
			EntitySet entitySet = null;
			IConceptualEntity conceptualEntity = null;
			if (this.m_useConceptualSchema)
			{
				if (!this.m_schema.TryGetEntity(extensionEntity.Name, extensionSchemaName, out conceptualEntity))
				{
					Microsoft.DataShaping.Contract.RetailFail("Cannot find the extensionEntity on the schema.");
				}
			}
			else
			{
				EntityDataModel baseModel = this.m_model.BaseModel;
				entitySet = ((baseModel != null) ? baseModel.EntitySets.FindByEdmReferenceName(extensionEntity.Name) : null);
			}
			List<ExtensionMeasure> measures = extensionEntity.Measures;
			for (int i = 0; i < measures.Count; i++)
			{
				this.TranslateMeasure(measures[i], entitySet, conceptualEntity);
			}
			List<ExtensionColumn> columns = extensionEntity.Columns;
			for (int j = 0; j < columns.Count; j++)
			{
				this.TranslateField(columns[j], entitySet, conceptualEntity);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00028E70 File Offset: 0x00027070
		private void TranslateMeasure(ExtensionMeasure extensionMeasure, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			EdmMeasure edmMeasure = null;
			IConceptualMeasure conceptualMeasure = null;
			if (this.m_useConceptualSchema)
			{
				IConceptualProperty conceptualProperty;
				bool flag = targetEntity.TryGetProperty(extensionMeasure.Name, out conceptualProperty);
				conceptualMeasure = conceptualProperty as IConceptualMeasure;
				Microsoft.DataShaping.Contract.RetailAssert(flag && conceptualMeasure != null, "Measure should exist in the extension schema.");
			}
			else
			{
				Microsoft.DataShaping.Contract.RetailAssert(this.m_model.ExtensionModel.TryGetMeasure(targetEntitySet.ReferenceName, extensionMeasure.Name, out edmMeasure) && edmMeasure != null, "EdmMeasure should exist in extension model.");
			}
			QueryExpression queryExpression = this.TranslatePropertyWithExpression(extensionMeasure, targetEntitySet, targetEntity);
			this.DeclareMeasure(queryExpression, targetEntitySet, edmMeasure, targetEntity, conceptualMeasure);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00028EFC File Offset: 0x000270FC
		private void TranslateField(ExtensionColumn extensionColumn, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			EdmField edmField = null;
			IConceptualColumn conceptualColumn = null;
			if (this.m_useConceptualSchema)
			{
				IConceptualProperty conceptualProperty;
				bool flag = targetEntity.TryGetProperty(extensionColumn.Name, out conceptualProperty);
				conceptualColumn = conceptualProperty as IConceptualColumn;
				Microsoft.DataShaping.Contract.RetailAssert(flag && conceptualColumn != null, "ConceptualColumn should exist in the extension schema.");
			}
			else
			{
				Microsoft.DataShaping.Contract.RetailAssert(this.m_model.ExtensionModel.TryGetField(targetEntitySet.ReferenceName, extensionColumn.Name, out edmField) && edmField != null, "EdmField should exist in the extension model.");
			}
			QueryExpression queryExpression = this.TranslatePropertyWithExpression(extensionColumn, targetEntitySet, targetEntity);
			this.DeclareField(queryExpression, targetEntitySet, edmField, targetEntity, conceptualColumn);
		}

		// Token: 0x06000A93 RID: 2707
		protected abstract void DeclareMeasure(QueryExpression measureContentExpr, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure);

		// Token: 0x06000A94 RID: 2708
		protected abstract QueryExpression TranslatePropertyWithExpression(ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity);

		// Token: 0x06000A95 RID: 2709
		protected abstract void DeclareField(QueryExpression fieldContentExpr, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity, IConceptualColumn column);

		// Token: 0x04000542 RID: 1346
		protected readonly FederatedEntityDataModel m_model;

		// Token: 0x04000543 RID: 1347
		protected readonly IFederatedConceptualSchema m_schema;

		// Token: 0x04000544 RID: 1348
		protected readonly bool m_useConceptualSchema;
	}
}
