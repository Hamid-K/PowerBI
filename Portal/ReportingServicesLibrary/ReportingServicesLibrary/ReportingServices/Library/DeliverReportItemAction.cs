using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ReportingServices.Dashboarding;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Hybrid.OAuth;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000108 RID: 264
	internal sealed class DeliverReportItemAction
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x00028104 File Offset: 0x00026304
		public DeliverReportItemAction(SessionfulClientRequest session, RSService service)
		{
			this.m_service = service;
			this.m_session = session;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002811C File Offset: 0x0002631C
		public void DeliverReportItem(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData)
		{
			if (Format == null)
			{
				throw new MissingParameterException("Format");
			}
			if (ExtensionSettings == null)
			{
				throw new MissingParameterException("ExtensionSettings");
			}
			try
			{
				this.m_service.WillDisconnectStorage();
				if (!this.m_service.IsSchedulerRunning())
				{
					throw new SchedulerNotRespondingPreventsPinningException();
				}
				SessionStarterAction.CreateExisting(this.m_session, this.m_service);
				bool flag = false;
				CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_service, this.m_session.SessionReport.Report.ReportDefinitionPath, "report");
				CatalogItem catalogItem = this.m_service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
				ParameterValueOrFieldReference[] array = this.ConstructParameters();
				if (string.Compare(ExtensionClassFactory.GetNewInstanceExtensionClass(ExtensionSettings.Extension, "Delivery").LocalizedName, "Power BI Dashboard", StringComparison.OrdinalIgnoreCase) == 0)
				{
					string text = this.DeliverToPbi(Format, ExtensionSettings, catalogItemContext, array);
					if (!string.IsNullOrEmpty(text))
					{
						Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue = new Microsoft.ReportingServices.Library.Soap.ParameterValue();
						parameterValue.Name = "TileID";
						parameterValue.Value = text;
						int num = ExtensionSettings.ParameterValues.Length;
						Array.Resize<ParameterValueOrFieldReference>(ref ExtensionSettings.ParameterValues, num + 1);
						ExtensionSettings.ParameterValues[num] = parameterValue;
					}
					flag = true;
				}
				this.m_service.WillDisconnectStorage();
				string text2 = this.CreateRefreshSubscription(this.m_session.SessionReport.Report.ItemPath, catalogItem.ItemID, EventType, MatchData, ExtensionSettings, Description, array, ((BaseReportCatalogItem)catalogItem).SecurityDescriptor);
				if (!flag)
				{
					this.m_service.EventManager.FireEvent(EventType, text2);
				}
			}
			finally
			{
				this.m_service.DisconnectStorage();
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000282B4 File Offset: 0x000264B4
		private string DeliverToPbi(string Format, ExtensionSettings ExtensionSettings, CatalogItemContext itemContext, ParameterValueOrFieldReference[] parameters)
		{
			string text4;
			using (this.m_service.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				PBIDashboardAPI pbidashboardAPI = new PBIDashboardAPI(ServiceToken.FromJson(this.m_service.SecMgr.GetLatestAADToken()).AccessToken, this.m_service.UserName);
				string text = null;
				string text2 = null;
				string dashboardName = null;
				string text3 = string.Empty;
				ParameterValueOrFieldReference[] parameterValues = ExtensionSettings.ParameterValues;
				for (int i = 0; i < parameterValues.Length; i++)
				{
					Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue = parameterValues[i] as Microsoft.ReportingServices.Library.Soap.ParameterValue;
					if (parameterValue != null)
					{
						text4 = parameterValue.Name;
						if (!(text4 == "DashboardID"))
						{
							if (!(text4 == "DashboardName"))
							{
								if (!(text4 == "ReportVisualName"))
								{
									if (text4 == "GroupID")
									{
										text2 = parameterValue.Value;
									}
								}
								else
								{
									text3 = parameterValue.Value;
								}
							}
							else
							{
								dashboardName = parameterValue.Value;
							}
						}
						else
						{
							text = parameterValue.Value;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					Dashboard dashboard = (from d in pbidashboardAPI.GetDashboards(null)
						where d.DisplayName == dashboardName
						select d).FirstOrDefault<Dashboard>();
					text = ((dashboard != null) ? dashboard.Id : null);
				}
				RenderReportAction renderReportAction = RenderReportAction.CreateWithFormatDeviceInfo(this.m_session, this.m_service, Format, string.Format(CultureInfo.InvariantCulture, RSRequestParameters.PBIDeviceInfoStringFormat, text3), PageCountMode.Actual);
				renderReportAction.Render();
				string text5;
				string text6;
				CatalogItemNameUtility.SplitPath(this.m_session.SessionReport.Report.ItemPath.Value, out text5, out text6);
				string text7 = ReportImpl.BuildNotificationUrl(this.m_service, itemContext, ParameterValueOrFieldReference.ThisArrayToXml(parameters), this.m_session.SessionReport.Report.HistoryDate, Localization.ClientPrimaryCulture.ToString());
				text4 = pbidashboardAPI.AddTile(renderReportAction.ResultStream, text2, text, text7, text5, this.m_session.SessionReport.ExecutionDateTime.ToString());
			}
			return text4;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000284CC File Offset: 0x000266CC
		private ParameterValueOrFieldReference[] ConstructParameters()
		{
			List<ParameterValueOrFieldReference> list = new List<ParameterValueOrFieldReference>();
			ParameterInfoCollection effectiveParams = this.m_session.SessionReport.Report.EffectiveParams;
			if (effectiveParams != null)
			{
				for (int i = 0; i < effectiveParams.Count; i++)
				{
					ParameterInfo parameterInfo = effectiveParams[i];
					if (parameterInfo.PromptUser)
					{
						foreach (object obj in parameterInfo.Values)
						{
							list.Add(new Microsoft.ReportingServices.Library.Soap.ParameterValue
							{
								Name = parameterInfo.Name,
								Value = ((obj == null) ? null : obj.ToString())
							});
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00028574 File Offset: 0x00026774
		private string CreateRefreshSubscription(ExternalItemPath itemPath, Guid itemId, string eventType, string matchData, ExtensionSettings extensionSettings, string description, ParameterValueOrFieldReference[] parameters, byte[] securityDescriptor)
		{
			new CreateSubscriptionAction(this.m_service).ActionParameters.Description = description;
			return this.m_service.SubscriptionManager.CreateSubscription(default(Guid), itemPath, itemId, eventType, matchData, extensionSettings, description, parameters, null, securityDescriptor).ToString();
		}

		// Token: 0x04000492 RID: 1170
		private readonly SessionfulClientRequest m_session;

		// Token: 0x04000493 RID: 1171
		private readonly RSService m_service;
	}
}
