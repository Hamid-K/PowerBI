using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DC RID: 220
	internal sealed class CreateComponentAction : CreateItemAction<CreateComponentActionParameters, ComponentCatalogItem>
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00025C83 File Offset: 0x00023E83
		internal CreateComponentAction(RSService service)
			: base("CreateComponentAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ComponentLibrary);
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00025CA2 File Offset: 0x00023EA2
		protected override void PrepareForNewItem(ComponentCatalogItem item)
		{
			if (base.ActionParameters.ComponentDefinition != null)
			{
				item.Content = base.ActionParameters.ComponentDefinition;
			}
			item.LoadFromDefinition(item.Content);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00025CCE File Offset: 0x00023ECE
		protected override void UpdateExistingItem(ComponentCatalogItem item)
		{
			SetComponentDefinitionAction setComponentDefinitionAction = base.Service.SetComponentDefinitionAction;
			setComponentDefinitionAction.ActionParameters.ComponentDefinition = base.ActionParameters.ComponentDefinition;
			setComponentDefinitionAction.Update(item);
		}
	}
}
