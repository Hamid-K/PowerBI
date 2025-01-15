using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E0 RID: 224
	internal sealed class SetComponentDefinitionAction : UpdateItemAction<SetComponentDefinitionActionParameters, ComponentCatalogItem>
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00025DD7 File Offset: 0x00023FD7
		internal SetComponentDefinitionAction(RSService service)
			: base("SetComponentContentsAction", service)
		{
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025DE5 File Offset: 0x00023FE5
		internal override void Update(ComponentCatalogItem item)
		{
			item.ThrowIfNoAccess(ResourceOperation.UpdateContent);
			item.LoadFromDefinition(base.ActionParameters.ComponentDefinition);
			item.Save(false);
		}
	}
}
