using System;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AB RID: 427
	internal sealed class SetCacheOptionsAction : RSSoapAction<SetCacheOptionsActionParameters>
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x000373E0 File Offset: 0x000355E0
		public SetCacheOptionsAction(RSService service)
			: base("SetCacheOptionsAction", service)
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000373F0 File Offset: 0x000355F0
		protected override void AddActionToBatch()
		{
			ExpirationDefinition expiration = base.ActionParameters.Expiration;
			string text = null;
			string text2 = null;
			if (expiration is TimeExpiration)
			{
				text = typeof(TimeExpiration).ToString();
				text2 = ((TimeExpiration)expiration).Minutes.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				base.Service.GetBatchSettingsForScheduleDefinition(((ScheduleExpiration)expiration).Schedule, out text, out text2);
			}
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetCacheOptions, base.ActionParameters.ReportPath, "report", text, null, null, null, base.ActionParameters.CacheReport, null, text2);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003749C File Offset: 0x0003569C
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.Expiration = null;
			if (parameters.Parent == typeof(TimeExpiration).ToString())
			{
				TimeExpiration timeExpiration = new TimeExpiration();
				timeExpiration.Minutes = int.Parse(parameters.Properties, CultureInfo.InvariantCulture);
				base.ActionParameters.Expiration = timeExpiration;
			}
			else
			{
				ScheduleExpiration scheduleExpiration = new ScheduleExpiration();
				scheduleExpiration.Schedule = base.Service.RetriveScheduleFromBatchStrings(parameters.Parent, parameters.Properties);
				base.ActionParameters.Expiration = scheduleExpiration;
			}
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.CacheReport = parameters.BoolParam;
			this.PerformActionNow();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00037554 File Offset: 0x00035754
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.DataSet
			});
			catalogItem.LoadProperties();
			BaseExecutableCatalogItem baseExecutableCatalogItem = catalogItem as BaseExecutableCatalogItem;
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseExecutableCatalogItem.ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			if (baseReportCatalogItem != null)
			{
				if (ExecutionOptions.IsSnapshotExecution(baseReportCatalogItem.ExecutionOptions))
				{
					throw new ReportSnapshotEnabledException();
				}
				if (base.ActionParameters.CacheReport)
				{
					baseReportCatalogItem.ThrowIfNotCacheableByProperties();
				}
			}
			if (base.ActionParameters.CacheReport)
			{
				baseExecutableCatalogItem.ThrowIfNotGoodForUnattended(false);
			}
			base.Service.ExecCacheDb.SetCacheOptions(catalogItemContext.CatalogItemPath, baseExecutableCatalogItem.ItemID, base.ActionParameters.CacheReport, base.ActionParameters.Expiration);
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}
	}
}
