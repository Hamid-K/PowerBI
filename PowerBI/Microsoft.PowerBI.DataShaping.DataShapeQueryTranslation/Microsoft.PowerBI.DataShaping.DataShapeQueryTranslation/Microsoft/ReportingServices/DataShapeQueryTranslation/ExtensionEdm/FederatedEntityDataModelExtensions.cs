using System;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000A9 RID: 169
	internal static class FederatedEntityDataModelExtensions
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0001D75C File Offset: 0x0001B95C
		public static EdmFieldInstance GetCorrespondingEdmFieldInstance(this FederatedEntityDataModel model, IConceptualProperty conceptualProperty)
		{
			EntitySet entitySet;
			EdmField correspondingEdmField = model.GetCorrespondingEdmField(conceptualProperty, out entitySet);
			return entitySet.FieldInstance(correspondingEdmField);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001D77C File Offset: 0x0001B97C
		public static EdmField GetCorrespondingEdmField(this FederatedEntityDataModel model, IConceptualProperty conceptualProperty, out EntitySet entitySet)
		{
			ExtensionEntityDataModel extensionModel = model.ExtensionModel;
			entitySet = model.GetCorrespondingEntitySet(conceptualProperty.Entity);
			if (extensionModel != null && conceptualProperty.Entity.EntityContainerName == extensionModel.Name)
			{
				EdmField edmField;
				if (!extensionModel.TryGetField(conceptualProperty.Entity.Name, conceptualProperty.Name, out edmField))
				{
					Microsoft.DataShaping.Contract.RetailFail("Fail to get EdmField on the model");
				}
				return edmField;
			}
			return entitySet.ElementType.Fields[conceptualProperty.EdmName];
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
		public static EdmMeasure GetCorrespondingEdmMeasure(this FederatedEntityDataModel model, IConceptualProperty conceptualProperty, out EntitySet entitySet)
		{
			ExtensionEntityDataModel extensionModel = model.ExtensionModel;
			entitySet = model.GetCorrespondingEntitySet(conceptualProperty.Entity);
			if (extensionModel != null && conceptualProperty.Entity.EntityContainerName == extensionModel.Name)
			{
				EdmMeasure edmMeasure;
				if (!extensionModel.TryGetMeasure(conceptualProperty.Entity.Name, conceptualProperty.Name, out edmMeasure))
				{
					Microsoft.DataShaping.Contract.RetailFail("Fail to get EdmMeasure on the model");
				}
				return edmMeasure;
			}
			return entitySet.ElementType.Members[conceptualProperty.EdmName] as EdmMeasure;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001D878 File Offset: 0x0001BA78
		public static EntitySet GetCorrespondingEntitySet(this FederatedEntityDataModel model, IConceptualEntity conceptualEntity)
		{
			EntityDataModel baseModel = model.BaseModel;
			if (baseModel == null)
			{
				return null;
			}
			return baseModel.EntitySets.FindByEdmReferenceName(conceptualEntity.Name);
		}
	}
}
