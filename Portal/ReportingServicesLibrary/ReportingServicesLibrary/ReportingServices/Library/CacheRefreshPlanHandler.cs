using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025B RID: 603
	internal sealed class CacheRefreshPlanHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x060015EF RID: 5615 RVA: 0x000053DC File Offset: 0x000035DC
		public bool CanSubscribe(ICatalogQuery catalogQuery, string itemName)
		{
			return true;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Microsoft.ReportingServices.Extensions.Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00057888 File Offset: 0x00055A88
		public void CleanUp(Microsoft.ReportingServices.Extensions.Subscription subscription)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((SubscriptionImpl)subscription).m_connectionManager;
			Microsoft.ReportingServices.Diagnostics.Task timeBasedSubscriptionSchedule = schedulingDBInterface.GetTimeBasedSubscriptionSchedule(subscription.ID);
			if (timeBasedSubscriptionSchedule.Type == ScheduleType.Shared)
			{
				schedulingDBInterface.RemoveReportFromSchedule(timeBasedSubscriptionSchedule.ID, subscription.ItemID, subscription.ID, ReportScheduleActions.RefreshCache);
				return;
			}
			schedulingDBInterface.DeleteTimeBasedSubscriptionSchedule(subscription.ID);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000578E7 File Offset: 0x00055AE7
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions(this.PerformActionHandler));
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000578FD File Offset: 0x00055AFD
		public string LocalizedName
		{
			get
			{
				return RepLibRes.CacheRefreshPlans;
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00057904 File Offset: 0x00055B04
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions)
		{
			RSService rsservice = new RSService(false);
			rsservice.WillDisconnectStorage();
			SubscriptionImpl subscriptionImpl = null;
			InActiveFlags inActiveFlags = InActiveFlags.Active;
			string text = RepLibRes.RefreshCachePlanSuccess;
			DateTime now = DateTime.Now;
			try
			{
				CachedSystemProperties.InvalidateCache();
				foreach (object obj in reportActions)
				{
					ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
					RSTrace.SubscriptionTracer.Assert(itemScheduleAction.Action == ReportScheduleActions.RefreshCache);
					RSService.EnsureItemTypeIsReportOrDataSet(itemScheduleAction.ItemType, itemScheduleAction.ItemPath.Value);
					SubscriptionDB subscriptionDB = new SubscriptionDB();
					subscriptionDB.ConnectionManager = new ConnectionManager();
					subscriptionDB.ConnectionManager.WillDisconnectStorage();
					subscriptionImpl = subscriptionDB.GetSubscription(itemScheduleAction.SubscriptionID, rsservice, true);
					rsservice.ValidateSubscriptionParameters(rsservice.CatalogToExternal(itemScheduleAction.ItemPath), subscriptionImpl.m_parameters, false);
					NameValueCollection nameValueCollection = ParameterValue.ThisArrayToNameValueCollection((ParameterValue[])subscriptionImpl.m_parameters);
					RSService rsservice2 = new RSService(false);
					using (rsservice2.SetStreamFactory(new MemoryThenFileStreamFactory()))
					{
						if (ItemType.Report == itemScheduleAction.ItemType || ItemType.LinkedReport == itemScheduleAction.ItemType)
						{
							if (RSTrace.ScheduleTracer.TraceInfo)
							{
								RSTrace.ScheduleTracer.Trace("Updating cache for report {0}", new object[] { itemScheduleAction.ItemPath });
							}
							new UpdateReportCacheAction(rsservice2)
							{
								ActionParameters = 
								{
									ItemPath = rsservice.CatalogToExternal(itemScheduleAction.ItemPath).Value
								},
								ActionParameters = 
								{
									Parameters = nameValueCollection
								},
								ActionParameters = 
								{
									JobType = JobTypeEnum.System
								}
							}.Execute();
						}
						else if (ItemType.DataSet == itemScheduleAction.ItemType)
						{
							if (RSTrace.ScheduleTracer.TraceInfo)
							{
								RSTrace.ScheduleTracer.Trace("Updating cache for data  set {0}", new object[] { itemScheduleAction.ItemPath });
							}
							UpdateDataSetCacheAction updateDataSetCacheAction = new UpdateDataSetCacheAction(rsservice2);
							updateDataSetCacheAction.ActionParameters.ItemPath = rsservice.CatalogToExternal(itemScheduleAction.ItemPath).Value;
							updateDataSetCacheAction.ActionParameters.Parameters = nameValueCollection;
							updateDataSetCacheAction.ActionParameters.JobType = JobTypeEnum.System;
							updateDataSetCacheAction.Execute();
							new UpdateDataSetJsonCacheAction(rsservice2)
							{
								ActionParameters = 
								{
									ItemPath = updateDataSetCacheAction.ActionParameters.ItemPath
								},
								ActionParameters = 
								{
									Parameters = nameValueCollection
								},
								ActionParameters = 
								{
									JobType = JobTypeEnum.System
								}
							}.Execute();
							if (Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.KpiItems))
							{
								try
								{
									rsservice2.WillDisconnectStorage();
									rsservice2.EventManager.FireSharedDatasetCacheUpdate(itemScheduleAction.ItemID);
								}
								catch (Exception ex)
								{
									RSTrace.ScheduleTracer.TraceException(TraceLevel.Error, ex.Message);
								}
								finally
								{
									rsservice2.DisconnectStorage();
								}
							}
						}
					}
					rsservice.UpdateSubscriptionLastRunInfo(subscriptionImpl.ID, inActiveFlags, now, text);
				}
			}
			catch (RSException ex2)
			{
				if (subscriptionImpl == null)
				{
					throw;
				}
				Exception ex3 = new Exception();
				if (ex2.InnerException != null)
				{
					ex3 = ex2.InnerException;
				}
				if (ex2 is ReportParameterValueNotSetException || ex3 is ReportParameterValueNotSetException)
				{
					inActiveFlags = InActiveFlags.MissingParameterValue;
					text = RepLibRes.RefreshCachePlanParametersInvalid;
				}
				else if (ex2 is ReportParameterTypeMismatchException || ex3 is ReportParameterTypeMismatchException || ex2 is InvalidReportParameterException || ex3 is InvalidReportParameterException)
				{
					inActiveFlags = InActiveFlags.InvalidParameterValue;
					text = RepLibRes.RefreshCachePlanParametersInvalid;
				}
				else if (ex2 is UnknownReportParameterException || ex3 is UnknownReportParameterException)
				{
					inActiveFlags = InActiveFlags.UnknownItemParameter;
					text = RepLibRes.RefreshCachePlanParametersInvalid;
				}
				else if (ex2 is InvalidDataSourceReferenceException || ex3 is InvalidDataSourceReferenceException)
				{
					subscriptionImpl.m_inactiveFlags |= InActiveFlags.SharedDataSourceRemoved;
					text = RepLibRes.RefreshCachePlanSharedDataSourceRemoved;
				}
				else if (ex2 is CachingNotEnabledException || ex3 is CachingNotEnabledException)
				{
					inActiveFlags = InActiveFlags.CachingNotEnabledOnItem;
					text = RepLibRes.RefreshCachePlanCachingIsNotEnabled;
				}
				else
				{
					text = RepLibRes.RefreshCachePlanError(ex2.Message);
				}
				rsservice.InvalidateSubscription(subscriptionImpl.ID, inActiveFlags, text);
			}
			finally
			{
				rsservice.Storage.DisconnectStorage();
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000577F4 File Offset: 0x000559F4
		protected override ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData)
		{
			return new ScheduleFireEventHandlerBase.RetrievalCommand
			{
				SqlCommand = "GetTimeBasedSubscriptionReportAction",
				Parameters = { { "@SubscriptionID", eventData } }
			};
		}
	}
}
