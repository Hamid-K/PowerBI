using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000196 RID: 406
	internal abstract class CreateReportActionBase<P> : CreateItemAction<P, ProfessionalReportCatalogItem> where P : CreateReportActionParameters, new()
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00036489 File Offset: 0x00034689
		protected CreateReportActionBase(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000EEA RID: 3818
		protected abstract bool UsePermanentCompiledDefinition { get; }

		// Token: 0x06000EEB RID: 3819 RVA: 0x00036494 File Offset: 0x00034694
		protected override void UpdateExistingItem(ProfessionalReportCatalogItem item)
		{
			SetReportDefinitionAction setReportDefinitionAction = base.Service.SetReportDefinitionAction;
			setReportDefinitionAction.ActionParameters.Definition = base.ActionParameters.ReportDefinition;
			setReportDefinitionAction.Update(item);
			base.ActionParameters.Warnings = setReportDefinitionAction.ActionParameters.Warnings;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000364EC File Offset: 0x000346EC
		protected override void PrepareForNewItem(ProfessionalReportCatalogItem theItem)
		{
			byte[] array = ((base.ActionParameters.ReportDefinition == null) ? theItem.Content : base.ActionParameters.ReportDefinition);
			base.ActionParameters.Warnings = theItem.PrepareNewReport(array, this.UsePermanentCompiledDefinition);
		}
	}
}
