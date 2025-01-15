using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000AA RID: 170
	internal static class FederatedEntityDataModelFactory
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001D896 File Offset: 0x0001BA96
		public static FederatedEntityDataModel CreateModel(EntityDataModel baseModel)
		{
			return FederatedEntityDataModelFactory.CreateModel(baseModel, null);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		public static FederatedEntityDataModel CreateModel(EntityDataModel baseModel, ExtensionSchema dsqExtensionSchema)
		{
			ExtensionEntityDataModel extensionEntityDataModel = null;
			if (dsqExtensionSchema != null)
			{
				extensionEntityDataModel = ExtensionEntityDataModelFactory.CreateModel(dsqExtensionSchema, baseModel);
			}
			return new FederatedEntityDataModel(baseModel, extensionEntityDataModel);
		}
	}
}
