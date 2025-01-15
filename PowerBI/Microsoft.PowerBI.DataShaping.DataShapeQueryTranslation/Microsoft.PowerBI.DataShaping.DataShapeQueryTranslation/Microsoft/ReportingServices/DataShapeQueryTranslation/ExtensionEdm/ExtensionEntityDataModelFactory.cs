using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000A7 RID: 167
	internal sealed class ExtensionEntityDataModelFactory
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0001D5A4 File Offset: 0x0001B7A4
		private ExtensionEntityDataModelFactory(EntityDataModel baseModel)
		{
			this.m_baseModel = baseModel;
			this.m_measures = new ExtensionEdmItemCollection<EdmMeasure>();
			this.m_fields = new ExtensionEdmItemCollection<EdmField>();
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001D5C9 File Offset: 0x0001B7C9
		public static ExtensionEntityDataModel CreateModel(ExtensionSchema extensionSchema, EntityDataModel baseModel)
		{
			return new ExtensionEntityDataModelFactory(baseModel).CreateModel(extensionSchema);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		private ExtensionEntityDataModel CreateModel(ExtensionSchema extensionSchema)
		{
			List<ExtensionEntity> entities = extensionSchema.Entities;
			for (int i = 0; i < entities.Count; i++)
			{
				this.PopulateEntity(entities[i]);
			}
			return new ExtensionEntityDataModel(extensionSchema.Name, this.m_measures, this.m_fields);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001D624 File Offset: 0x0001B824
		private void PopulateEntity(ExtensionEntity extensionEntity)
		{
			EntitySet entitySet = this.m_baseModel.EntitySets.FindByEdmReferenceName(extensionEntity.Name);
			Contract.RetailAssert(entitySet != null, "The ExtensionSchema referred to EntitySet " + extensionEntity.Name + ", but that EntitySet does not exist in the base model.");
			List<ExtensionMeasure> measures = extensionEntity.Measures;
			for (int i = 0; i < measures.Count; i++)
			{
				ExtensionMeasure extensionMeasure = measures[i];
				EdmMeasure edmMeasure = TransientEdmItemFactory.CreateMeasure(entitySet, extensionMeasure.Name, ConceptualPrimitiveResultType.FromPrimitive(extensionMeasure.DataType.Value));
				this.m_measures.AddItem(extensionEntity.Name, extensionMeasure.Name, edmMeasure);
			}
			List<ExtensionColumn> columns = extensionEntity.Columns;
			for (int j = 0; j < columns.Count; j++)
			{
				ExtensionColumn extensionColumn = columns[j];
				EdmField edmField = TransientEdmItemFactory.CreateField(entitySet, extensionColumn.Name, ConceptualPrimitiveResultType.FromPrimitive(extensionColumn.DataType.Value));
				edmField.CompleteInitialization();
				edmField.Grouping.CompleteInitialization();
				this.m_fields.AddItem(extensionEntity.Name, extensionColumn.Name, edmField);
			}
		}

		// Token: 0x040003CA RID: 970
		private readonly EntityDataModel m_baseModel;

		// Token: 0x040003CB RID: 971
		private readonly ExtensionEdmItemCollection<EdmMeasure> m_measures;

		// Token: 0x040003CC RID: 972
		private readonly ExtensionEdmItemCollection<EdmField> m_fields;
	}
}
