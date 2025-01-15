using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000195 RID: 405
	internal sealed class CreateReportAction : CreateReportActionBase<CreateReportActionParameters>
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00036395 File Offset: 0x00034595
		public CreateReportAction(RSService service)
			: base("CreateReportAction", service)
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000363A4 File Offset: 0x000345A4
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateReport, base.ActionParameters.ItemName, "Report", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, base.ActionParameters.ReportDefinition, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003641C File Offset: 0x0003461C
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.ReportDefinition = parameters.Content;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool UsePermanentCompiledDefinition
		{
			get
			{
				return true;
			}
		}
	}
}
