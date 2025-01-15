using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000050 RID: 80
	internal static class SubscriptionExtensions
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00011FD2 File Offset: 0x000101D2
		public static KnownDeliveryExtensions KnownAs(this global::Model.Subscription librarySubscription)
		{
			return ExtensionExtension.GetKnownExtensionType(librarySubscription.DeliveryExtension);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011FE0 File Offset: 0x000101E0
		public static global::Model.Subscription ToWebApiModel(this SubscriptionImpl librarySubscription, Dictionary<string, ReportParameterType> parameterTypes, SubscriptionProperties properties = null)
		{
			if (librarySubscription == null)
			{
				throw new ArgumentNullException("librarySubscription");
			}
			global::Model.Subscription subscription = new global::Model.Subscription();
			subscription.Id = librarySubscription.ID;
			subscription.Description = librarySubscription.Description;
			subscription.IsActive = librarySubscription.IsActive();
			subscription.EventType = librarySubscription.EventType;
			subscription.Schedule = new global::Model.ScheduleReference
			{
				ScheduleID = SubscriptionExtensions.ScheduleDefinitionFromScheduleRef(librarySubscription.SubscriptionData),
				Definition = SubscriptionExtensions.ScheduleDefinitionFromMatchData(librarySubscription.SubscriptionData)
			};
			subscription.LastRunTime = ((librarySubscription.LastRunTime.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.LastRunTime));
			global::Model.ExtensionSettings extensionSettings;
			if (properties == null)
			{
				extensionSettings = null;
			}
			else
			{
				Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings2 = properties.ExtensionSettings;
				extensionSettings = ((extensionSettings2 != null) ? extensionSettings2.ToWebApiModel() : null);
			}
			subscription.ExtensionSettings = extensionSettings;
			string text;
			if (properties == null)
			{
				text = null;
			}
			else
			{
				Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings3 = properties.ExtensionSettings;
				text = ((extensionSettings3 != null) ? extensionSettings3.Extension : null);
			}
			subscription.DeliveryExtension = text ?? librarySubscription.DeliveryExtension;
			subscription.Report = librarySubscription.ReportName;
			UserContext owner = librarySubscription.Owner;
			subscription.Owner = ((owner != null) ? owner.UserName : null);
			UserContext modifiedBy = librarySubscription.ModifiedBy;
			subscription.ModifiedBy = ((modifiedBy != null) ? modifiedBy.UserName : null);
			subscription.ModifiedDate = ((librarySubscription.ModifiedDate.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.ModifiedDate));
			subscription.LastStatus = librarySubscription.LastStatus;
			subscription.IsDataDriven = librarySubscription.IsDataDriven();
			subscription.DataSource = null;
			subscription.DataQuery = null;
			subscription.ParameterValues = ((librarySubscription.m_parameters != null) ? SubscriptionExtensions.ToReportPameterList(librarySubscription, parameterTypes, librarySubscription.Culture) : new List<global::Model.ParameterValue>());
			return subscription;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0001218C File Offset: 0x0001038C
		public static global::Model.Subscription ToWebApiModelDataDriven(this SubscriptionImpl librarySubscription, Dictionary<string, ReportParameterType> parameterTypes, DataDrivenSubscriptionProperties properties)
		{
			if (librarySubscription == null)
			{
				throw new ArgumentNullException("librarySubscription");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			return new global::Model.Subscription
			{
				Id = librarySubscription.ID,
				Description = librarySubscription.Description,
				IsActive = librarySubscription.IsActive(),
				EventType = librarySubscription.EventType,
				Schedule = new global::Model.ScheduleReference
				{
					Definition = SubscriptionExtensions.ScheduleDefinitionFromMatchData(librarySubscription.SubscriptionData),
					ScheduleID = SubscriptionExtensions.ScheduleDefinitionFromScheduleRef(librarySubscription.SubscriptionData)
				},
				LastRunTime = ((librarySubscription.LastRunTime.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.LastRunTime)),
				ExtensionSettings = ((properties.ExtensionSettings == null) ? null : properties.ExtensionSettings.ToWebApiModel()),
				DeliveryExtension = ((properties.ExtensionSettings == null) ? librarySubscription.DeliveryExtension : properties.ExtensionSettings.Extension),
				Report = librarySubscription.ReportName,
				Owner = librarySubscription.Owner.UserName,
				ModifiedBy = librarySubscription.ModifiedBy.UserName,
				ModifiedDate = ((librarySubscription.ModifiedDate.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.ModifiedDate)),
				LastStatus = librarySubscription.LastStatus,
				IsDataDriven = librarySubscription.IsDataDriven(),
				DataSource = ((properties.DataSettings != null && properties.DataSettings.Item != null) ? properties.DataSettings.Item.ToDataSource() : null),
				DataQuery = ((properties.DataSettings != null && properties.DataSettings.DataSet != null) ? properties.DataSettings.DataSet.ToQueryDefinition() : null),
				ParameterValues = ((librarySubscription.m_parameters != null) ? SubscriptionExtensions.ToReportPameterList(librarySubscription, parameterTypes, librarySubscription.Culture) : new List<global::Model.ParameterValue>())
			};
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0001237C File Offset: 0x0001057C
		public static global::Model.Subscription ToWebAPIModel(this Microsoft.SqlServer.ReportingServices2010.Subscription proxySubscription)
		{
			if (proxySubscription == null)
			{
				throw new ArgumentNullException("proxySubscription");
			}
			return new global::Model.Subscription
			{
				Id = new Guid(proxySubscription.SubscriptionID),
				Description = proxySubscription.Description,
				IsActive = SubscriptionExtensions.IsActive(proxySubscription.Active),
				EventType = proxySubscription.EventType,
				Schedule = null,
				LastRunTime = ((proxySubscription.LastExecuted.Ticks == 0L) ? null : new DateTimeOffset?(proxySubscription.LastExecuted)),
				ExtensionSettings = proxySubscription.DeliverySettings.ToWebApiModel(),
				DeliveryExtension = proxySubscription.DeliverySettings.Extension,
				Report = proxySubscription.Path,
				Owner = proxySubscription.Owner,
				ModifiedBy = proxySubscription.ModifiedBy,
				ModifiedDate = ((proxySubscription.ModifiedDate.Ticks == 0L) ? null : new DateTimeOffset?(proxySubscription.ModifiedDate)),
				LastStatus = proxySubscription.Status,
				IsDataDriven = proxySubscription.IsDataDriven,
				DataSource = null,
				DataQuery = null,
				ParameterValues = new List<global::Model.ParameterValue>()
			};
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000124B8 File Offset: 0x000106B8
		private static bool IsActive(Microsoft.SqlServer.ReportingServices2010.ActiveState activeState)
		{
			return (!activeState.DeliveryExtensionRemoved || !activeState.DeliveryExtensionRemovedSpecified) && (!activeState.InvalidParameterValue || !activeState.InvalidParameterValueSpecified) && (!activeState.MissingParameterValue || !activeState.MissingParameterValueSpecified) && (!activeState.SharedDataSourceRemoved || !activeState.SharedDataSourceRemovedSpecified) && (!activeState.UnknownReportParameter || !activeState.UnknownReportParameterSpecified) && (!activeState.DisabledByUser || !activeState.DisabledByUserSpecified);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001252C File Offset: 0x0001072C
		private static List<global::Model.ParameterValue> ToReportPameterList(SubscriptionImpl librarySubscription, Dictionary<string, ReportParameterType> parameterTypes, string culture)
		{
			List<global::Model.ParameterValue> list = new List<global::Model.ParameterValue>();
			foreach (Microsoft.ReportingServices.Library.Soap.ParameterValueOrFieldReference parameterValueOrFieldReference in librarySubscription.m_parameters)
			{
				Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue = parameterValueOrFieldReference as Microsoft.ReportingServices.Library.Soap.ParameterValue;
				if (parameterValue != null)
				{
					if (parameterTypes.ContainsKey(parameterValue.Name))
					{
						list.Add(parameterValue.ToWebApiReportParameterValue(parameterTypes[parameterValue.Name], culture));
					}
				}
				else
				{
					list.Add(((Microsoft.ReportingServices.Library.Soap.ParameterFieldReference)parameterValueOrFieldReference).ToWebApiReportParameterValue());
				}
			}
			return list;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000125A4 File Offset: 0x000107A4
		public static global::Model.CacheRefreshPlan ToCacheRefreshPlanModel(this SubscriptionImpl librarySubscription, Dictionary<string, ReportParameterType> parameterTypes)
		{
			if (librarySubscription == null)
			{
				throw new ArgumentNullException("librarySubscription");
			}
			return new global::Model.CacheRefreshPlan
			{
				Id = librarySubscription.ID,
				Description = librarySubscription.Description,
				EventType = librarySubscription.EventType,
				CatalogItemPath = librarySubscription.ReportName,
				Schedule = new global::Model.ScheduleReference
				{
					Definition = SubscriptionExtensions.ScheduleDefinitionFromMatchData(librarySubscription.SubscriptionData),
					ScheduleID = SubscriptionExtensions.ScheduleDefinitionFromScheduleRef(librarySubscription.SubscriptionData)
				},
				LastRunTime = ((librarySubscription.LastRunTime.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.LastRunTime)),
				Owner = librarySubscription.Owner.UserName,
				ModifiedBy = librarySubscription.ModifiedBy.UserName,
				ModifiedDate = ((librarySubscription.ModifiedDate.Ticks == 0L) ? null : new DateTimeOffset?(librarySubscription.ModifiedDate)),
				LastStatus = librarySubscription.LastStatus,
				ParameterValues = ((librarySubscription.m_parameters != null) ? SubscriptionExtensions.ToReportPameterList(librarySubscription, parameterTypes, librarySubscription.Culture) : new List<global::Model.ParameterValue>())
			};
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000126D4 File Offset: 0x000108D4
		public static string ScheduleDefinitionFromScheduleRef(string matchData)
		{
			Guid guid;
			if (!Guid.TryParse(matchData, out guid))
			{
				return null;
			}
			return guid.ToString();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000126FC File Offset: 0x000108FC
		public static global::Model.ScheduleDefinition ScheduleDefinitionFromMatchData(string matchData)
		{
			if (string.IsNullOrEmpty(matchData))
			{
				return null;
			}
			if (SubscriptionExtensions.IsReference(matchData))
			{
				return null;
			}
			global::Model.ScheduleDefinition scheduleDefinition;
			try
			{
				scheduleDefinition = Microsoft.ReportingServices.Library.Soap.ScheduleDefinition.XmlToDefinition(matchData).ToWebAPI();
			}
			catch (InvalidElementException)
			{
				scheduleDefinition = null;
			}
			return scheduleDefinition;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00012744 File Offset: 0x00010944
		private static bool IsReference(string matchData)
		{
			Guid guid;
			return Guid.TryParse(matchData, out guid);
		}
	}
}
