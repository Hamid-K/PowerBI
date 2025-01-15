using System;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000236 RID: 566
	internal sealed class ReportImpl : Microsoft.ReportingServices.Interfaces.Report
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00050103 File Offset: 0x0004E303
		internal ReportImpl(NotificationImpl notification)
		{
			Global.m_Tracer.Assert(notification != null);
			this.m_notification = notification;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00050120 File Offset: 0x0004E320
		internal RSService Service
		{
			get
			{
				if (this.m_service == null)
				{
					this.m_service = new RSService(this.m_notification.Owner, this.m_notification.AuthType, this.m_notification.Path.Value);
				}
				return this.m_service;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0005016C File Offset: 0x0004E36C
		internal NotificationImpl Notification
		{
			get
			{
				return this.m_notification;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00050174 File Offset: 0x0004E374
		internal SnapshotNoOpClientRequest Session
		{
			get
			{
				return this.m_session;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0005017C File Offset: 0x0004E37C
		internal MemoryThenFileStreamFactory StreamFactory
		{
			get
			{
				return this.m_streamFactory;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00050184 File Offset: 0x0004E384
		internal string RenderFormat
		{
			get
			{
				return this.m_renderFormat;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0005018C File Offset: 0x0004E38C
		internal string DeviceInfo
		{
			get
			{
				return this.m_deviceInfo;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00050194 File Offset: 0x0004E394
		public override string Name
		{
			get
			{
				return this.m_notification.m_path.Value.Substring(this.m_notification.m_path.Value.LastIndexOf("/", Localization.CatalogStringComparison) + 1);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000501CC File Offset: 0x0004E3CC
		public override string URL
		{
			get
			{
				if (this.m_url == null)
				{
					CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service, this.m_notification.m_path, "Path");
					this.m_url = ReportImpl.BuildNotificationUrl(this.Service, catalogItemContext, this.m_notification.m_originalParameters, this.m_notification.m_snapShotDate, this.m_notification.m_subscriptionLocale);
				}
				return this.m_url;
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00050238 File Offset: 0x0004E438
		public static string BuildNotificationUrl(RSService Service, CatalogItemContext ctx, string parameters, DateTime snapShotDate, string locale)
		{
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(ctx);
			ParamValues paramValues = new ParamValues();
			paramValues.FromXml(parameters);
			bool flag;
			NameValueCollection nameValueCollection = new RSParameterTranslator(Service.GetScopedStorage()).StoreReportParameters(ctx.ItemPath, paramValues.AsNameValueCollection, out flag);
			catalogItemUrlBuilder.AppendReportParameters(nameValueCollection);
			catalogItemUrlBuilder.AppendCatalogParameter("ParameterLanguage", locale);
			if (snapShotDate != DateTime.MinValue)
			{
				catalogItemUrlBuilder.AppendCatalogParameter("Snapshot", Globals.ToSnapshotDateFormat(snapShotDate));
			}
			return catalogItemUrlBuilder.ToString();
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x000502B0 File Offset: 0x0004E4B0
		public override DateTime Date
		{
			get
			{
				return this.m_notification.m_subscriptionLastRunTime;
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000502C0 File Offset: 0x0004E4C0
		public override RenderedOutputFile[] Render(string renderFormat, string deviceInfo)
		{
			this.m_renderFormat = renderFormat;
			this.m_deviceInfo = deviceInfo;
			ArrayList arrayList = new ArrayList();
			ExternalItemPath path = this.m_notification.m_path;
			CachedSystemProperties.InvalidateCache();
			this.m_streamFactoryServiceMarker = this.Service.SetStreamFactory(this.m_streamFactory);
			this.m_session = new SnapshotNoOpClientRequest(new UserContext(this.m_notification.Owner, null, this.m_notification.AuthType), path);
			string text = ((this.m_notification.m_snapShotDate == DateTime.MinValue) ? null : Globals.ToSnapshotDateFormat(this.m_notification.m_snapShotDate));
			CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service);
			catalogItemContext.SetPath(path, ItemPathOptions.None);
			catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
			catalogItemContext.RSRequestParameters.FormatParamValue = renderFormat;
			catalogItemContext.RSRequestParameters.SnapshotParamValue = text;
			catalogItemContext.RSRequestParameters.SetRenderingParameters(deviceInfo);
			catalogItemContext.RSRequestParameters.SetReportParameters(this.m_notification.m_parameters);
			catalogItemContext.RSRequestParameters.SetBrowserCapabilities(null);
			catalogItemContext.RSRequestParameters.DatasourcesCred = new DatasourceCredentialsCollection();
			JobType jobType = SubscriptionJobType.CreateSubscriptionJobType(this.m_notification.IsDataDriven, JobTypeEnum.System);
			RenderReportAction renderReportAction = RenderReportAction.CreateWithCatalogItemContext(this.m_session, this.Service, catalogItemContext);
			if (string.Equals(this.m_renderFormat, "IMAGE", StringComparison.OrdinalIgnoreCase) && this.m_deviceInfo.ToUpperInvariant().Contains("<OUTPUTFORMAT>EMF</OUTPUTFORMAT>"))
			{
				renderReportAction.UsePersistedStreams = true;
				renderReportAction.WaitPersistStreamCompletion = true;
			}
			renderReportAction.JobType = jobType;
			renderReportAction.Render();
			RSStream resultStream = renderReportAction.ResultStream;
			ParameterInfoCollection effectiveParameters = renderReportAction.EffectiveParameters;
			string[] streamIds = renderReportAction.StreamIds;
			Warning[] warnings = renderReportAction.Warnings;
			if (resultStream != null)
			{
				resultStream.Flush();
				RenderedOutputFileImpl renderedOutputFileImpl = new RenderedOutputFileImpl(resultStream);
				arrayList.Add(renderedOutputFileImpl);
				if (streamIds != null && streamIds.Length != 0)
				{
					SessionReportItem.Load(DatabaseSessionStorage.Current, this.m_session.SessionID, this.m_notification.m_path, DateTime.MinValue, new UserContext(this.m_notification.Owner, null, this.m_notification.AuthType), this.m_notification.m_parameters, streamIds[0], null);
					CachedSystemProperties.InvalidateCache();
					foreach (string text2 in streamIds)
					{
						RenderedOutputFileImpl renderedOutputFileImpl2 = new RenderedOutputFileImpl(this, text2);
						arrayList.Add(renderedOutputFileImpl2);
					}
				}
				else if (renderReportAction.UsePersistedStreams)
				{
					int totalStreamCount = this.Service.StreamManager.PersistedStreamManager.GetTotalStreamCount();
					for (int j = 1; j <= totalStreamCount; j++)
					{
						RenderedOutputFileImpl renderedOutputFileImpl3 = new RenderedOutputFileImpl(this, this.Name + "_" + j, true);
						arrayList.Add(renderedOutputFileImpl3);
					}
				}
			}
			if (!renderReportAction.UsePersistedStreams)
			{
				this.Service.StreamManager.ClearSecondaryStreams();
			}
			return (RenderedOutputFile[])arrayList.ToArray(typeof(RenderedOutputFile));
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000505A7 File Offset: 0x0004E7A7
		internal void OpenStreamFactory()
		{
			if (this.m_streamFactory != null)
			{
				throw new InternalCatalogException("Stream factory already opened on OpenStreamFactory");
			}
			this.m_streamFactory = new MemoryThenFileStreamFactory();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000505C7 File Offset: 0x0004E7C7
		internal void CloseStreamFactory()
		{
			if (this.m_streamFactoryServiceMarker != null)
			{
				this.m_streamFactoryServiceMarker.Dispose();
				this.m_streamFactory = null;
			}
		}

		// Token: 0x0400074F RID: 1871
		private NotificationImpl m_notification;

		// Token: 0x04000750 RID: 1872
		private MemoryThenFileStreamFactory m_streamFactory;

		// Token: 0x04000751 RID: 1873
		private IDisposable m_streamFactoryServiceMarker;

		// Token: 0x04000752 RID: 1874
		private RSService m_service;

		// Token: 0x04000753 RID: 1875
		private SnapshotNoOpClientRequest m_session;

		// Token: 0x04000754 RID: 1876
		private string m_renderFormat;

		// Token: 0x04000755 RID: 1877
		private string m_deviceInfo;

		// Token: 0x04000756 RID: 1878
		private string m_url;
	}
}
