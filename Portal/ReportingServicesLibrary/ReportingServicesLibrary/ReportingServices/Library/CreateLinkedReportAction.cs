using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000193 RID: 403
	internal sealed class CreateLinkedReportAction : CreateItemAction<CreateLinkedReportActionParameters, LinkedReportCatalogItem>
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003609B File Offset: 0x0003429B
		public CreateLinkedReportAction(RSService service)
			: base("CreateLinkedReportAction", service)
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000360AC File Offset: 0x000342AC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateLinkedReport, base.ActionParameters.ItemName, "Report", base.ActionParameters.ParentPath, "Parent", base.ActionParameters.LinkPath, "Link", false, null, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00036120 File Offset: 0x00034320
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.LinkPath = parameters.Param;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003617C File Offset: 0x0003437C
		private ReportExecutionCacheDb ExecCacheDb
		{
			get
			{
				return base.Service.ExecCacheDb;
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003618C File Offset: 0x0003438C
		protected override void PrepareForNewItem(LinkedReportCatalogItem item)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service);
			if (!catalogItemContext.SetPath(base.ActionParameters.LinkPath))
			{
				throw new InvalidItemPathException(base.ActionParameters.LinkPath, "link");
			}
			ItemType itemType;
			string text;
			Guid guid;
			byte[] array;
			if (!base.Service.Storage.GetParameters(catalogItemContext.CatalogItemPath, out itemType, out text, out guid, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value, "link");
			}
			if (itemType != ItemType.Report)
			{
				throw new WrongItemTypeException(catalogItemContext.OriginalItemPath.Value);
			}
			ProfessionalReportCatalogItem professionalReportCatalogItem = (ProfessionalReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, guid, ItemType.Report, array);
			professionalReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
			professionalReportCatalogItem.ThrowIfNoAccess(ReportOperation.CreateLink);
			item.SourceReport = professionalReportCatalogItem;
			item.ParametersXml = text;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00036258 File Offset: 0x00034458
		protected override void FinalizeNewItem(LinkedReportCatalogItem item)
		{
			ExecutionSettingEnum executionSettingEnum;
			ScheduleDefinitionOrReference scheduleDefinitionOrReference;
			this.ExecCacheDb.GetExecutionOptions(item.LinkPath, item.LinkID, out executionSettingEnum, out scheduleDefinitionOrReference);
			if (executionSettingEnum == ExecutionSettingEnum.Snapshot)
			{
				this.ExecCacheDb.SetExecutionOptionsIfChanged(item.ItemContext.CatalogItemPath, ExecutionOptions.ExecutionSettingEnumToInt(executionSettingEnum), ExecutionOptions.Live);
				this.ExecCacheDb.SetReportSchedule(item.ItemID, scheduleDefinitionOrReference, ReportScheduleActions.UpdateReportExecutionSnapshot);
				return;
			}
			bool flag;
			ExpirationDefinition expirationDefinition;
			this.ExecCacheDb.GetCacheOptions(item.LinkPath, out flag, out expirationDefinition);
			if (flag)
			{
				this.ExecCacheDb.SetCacheOptions(item.ItemContext.CatalogItemPath, item.ItemID, flag, expirationDefinition);
			}
		}
	}
}
