using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000071 RID: 113
	internal sealed class NotificationQueueWorker : QueuePollWorker
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00012F3C File Offset: 0x0001113C
		protected override void CleanInactiveRows(int minutes)
		{
			base.ExecuteStoredProcedure("CleanNotificationRecords", new Dictionary<string, object> { { "@MaxAgeMinutes", minutes } });
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00012F6C File Offset: 0x0001116C
		protected override string QueueIDColumnName
		{
			get
			{
				return "NotificationID";
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00012F73 File Offset: 0x00011173
		protected override string QueueTableName
		{
			get
			{
				return "Notifications";
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012F7A File Offset: 0x0001117A
		public override QueueItem GetNextQueueItem(IDataRecord record)
		{
			NotificationQueueItem notificationQueueItem = new NotificationQueueItem();
			if (this.m_service == null)
			{
				this.m_service = new RSService(false);
			}
			notificationQueueItem.Notification = new NotificationImpl(record, this.m_service);
			return notificationQueueItem;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00012FA7 File Offset: 0x000111A7
		protected override string PollingTraceName
		{
			get
			{
				return "NotificationPolling";
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012FAE File Offset: 0x000111AE
		public override bool QueueWorker(QueueItem item)
		{
			return this.HandleNotification(item);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012FB8 File Offset: 0x000111B8
		public bool HandleNotification(QueueItem item)
		{
			NotificationQueueItem notificationQueueItem = (NotificationQueueItem)item;
			NotificationImpl notification = notificationQueueItem.Notification;
			if (notification.Path == null)
			{
				return true;
			}
			RSService rsservice = new RSService(notification.Owner, notification.AuthType, notification.Path.Value);
			rsservice.WillDisconnectStorage();
			bool flag;
			try
			{
				if (notificationQueueItem.Notification.IsDataDriven && !Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions))
				{
					if (this.m_tracer.TraceError)
					{
						this.m_tracer.Trace(TraceLevel.Error, "Data Driven notification found for invalid SKU.  Notification will not be processed.");
					}
					flag = true;
				}
				else
				{
					Localization.SetCultureFromPriorityList(new string[] { notification.m_subscriptionLocale }, false);
					CachedSystemProperties.InvalidateCache();
					try
					{
						RSService rsservice2 = rsservice;
						ExternalItemPath path = notification.m_path;
						ParameterValueOrFieldReference[] array = Microsoft.ReportingServices.Library.Soap.ParameterValue.XmlToThisArray(notification.m_parameters);
						rsservice2.ValidateSubscriptionParameters(path, array, notification.IsDataDriven);
					}
					catch (RSException ex)
					{
						Exception ex2 = new Exception();
						if (ex.InnerException != null)
						{
							ex2 = ex.InnerException;
						}
						notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
						InActiveFlags inActiveFlags;
						if (ex is ReportParameterValueNotSetException || ex2 is ReportParameterValueNotSetException)
						{
							inActiveFlags = InActiveFlags.MissingParameterValue;
						}
						else if (ex is ReportParameterTypeMismatchException || ex2 is ReportParameterTypeMismatchException || ex is InvalidReportParameterException || ex2 is InvalidReportParameterException)
						{
							inActiveFlags = InActiveFlags.InvalidParameterValue;
						}
						else
						{
							if (!(ex is UnknownReportParameterException) && !(ex2 is UnknownReportParameterException))
							{
								throw;
							}
							inActiveFlags = InActiveFlags.UnknownItemParameter;
						}
						if (!notification.IsDataDriven)
						{
							rsservice.InvalidateSubscription(notification.m_subscriptionID, inActiveFlags, RepLibRes.SubscriptionParametersInvalid);
						}
						return true;
					}
					ParamValues paramValues = new ParamValues();
					paramValues.FromXml(notification.m_parameters);
					notification.m_parameters = paramValues.ToOldParameterXml();
					DeliveryExtension deliveryExtension = (DeliveryExtension)Globals.Configuration.Extensions.Delivery[notification.m_deliveryExtension];
					if (deliveryExtension == null)
					{
						notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
						flag = true;
					}
					else
					{
						notificationQueueItem.SecondsBeforeRetry = deliveryExtension.SecondsBeforeRetry;
						IExtension extension = null;
						Exception ex3 = null;
						try
						{
							extension = ExtensionClassFactory.GetNewInstanceExtensionClass(notification.m_deliveryExtension, "Delivery");
						}
						catch (Exception ex3)
						{
							extension = null;
						}
						if (extension == null)
						{
							notification.Status = RepLibRes.DeliveryExtensionFailedToLoad;
							notification.Save();
							notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
							if (this.m_tracer.TraceError)
							{
								if (ex3 == null)
								{
									this.m_tracer.Trace(TraceLevel.Error, "Extension {0} did not load, extension factory returned null", new object[] { notification.m_deliveryExtension });
								}
								else
								{
									this.m_tracer.Trace(TraceLevel.Error, "Extension {0} did not load, extension factory threw exception: {1}", new object[]
									{
										notification.m_deliveryExtension,
										ex3.ToString()
									});
								}
							}
							flag = true;
						}
						else
						{
							bool flag2 = false;
							IDeliveryExtension deliveryExtension2 = (IDeliveryExtension)extension;
							SubscriptionManager.PrepareSubscriptionSettingsForUse(ProviderManager.InitDeliveryExtension(deliveryExtension2, true, rsservice, notification.Path), ref notification.m_extensionSettings, notification.Version);
							if (notification.Attempt > deliveryExtension.MaxRetries + 1)
							{
								notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
								flag = true;
							}
							else
							{
								notification.m_maxRetries = deliveryExtension.MaxRetries;
								if (!base.ContinueWorking)
								{
									flag = false;
								}
								else
								{
									try
									{
										if (this.m_tracer.TraceInfo)
										{
											this.m_tracer.Trace(TraceLevel.Info, "Handling subscription {0} to report {1}, owner: {2}, delivery extension: {3}.", new object[]
											{
												notification.m_subscriptionID,
												notification.Report.Name,
												notification.Owner,
												notification.m_deliveryExtension
											});
										}
										notification.m_report.OpenStreamFactory();
										flag2 = deliveryExtension2.Deliver(notification);
										if (!flag2 && this.m_tracer.TraceError)
										{
											this.m_tracer.Trace(TraceLevel.Error, "Error occurred processing subscription {0}: {1}", new object[] { notification.m_subscriptionID, notification.m_subscriptionStatus });
										}
									}
									catch (Exception ex4)
									{
										if (ex4 is OutOfMemoryException)
										{
											GC.Collect();
										}
										notification.Status = RepLibRes.SubscriptionError(ex4.Message);
										notification.Save();
										if (Global.m_Tracer.TraceError)
										{
											this.m_tracer.Trace(TraceLevel.Error, "Error thrown by delivery provider: {0}", new object[] { ex4.ToString() });
										}
										notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
										return true;
									}
									finally
									{
										notification.m_report.CloseStreamFactory();
									}
									if (!flag2 && notification.Retry)
									{
										notification.m_attempt++;
										if (notification.m_attempt < deliveryExtension.MaxRetries + 1)
										{
											notificationQueueItem.DeleteItem = false;
											flag = true;
										}
										else
										{
											notificationQueueItem.DeleteAsErrorForDataDrivenNotification = true;
											flag = true;
										}
									}
									else
									{
										notificationQueueItem.DeleteAsErrorForDataDrivenNotification = !flag2;
										flag = true;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex5)
			{
				if (this.m_tracer.TraceError)
				{
					this.m_tracer.Trace(TraceLevel.Error, "Error occurred processing subscription {0}: {1}", new object[] { notification.m_subscriptionID, ex5.Message });
				}
				notification.Status = ex5.Message;
				notification.Save();
				flag = true;
			}
			finally
			{
				Localization.Reset();
				rsservice.Storage.DisconnectStorage();
			}
			return flag;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013500 File Offset: 0x00011700
		public override void DeleteQueueItem(QueueItem item)
		{
			NotificationQueueItem notificationQueueItem = (NotificationQueueItem)item;
			if (!notificationQueueItem.DeleteItem)
			{
				base.ExecuteStoredProcedure("SetNotificationAttempt", new Dictionary<string, object>
				{
					{
						"@Attempt",
						notificationQueueItem.Notification.m_attempt
					},
					{ "@SecondsToAdd", notificationQueueItem.SecondsBeforeRetry },
					{
						"@NotificationID",
						notificationQueueItem.Notification.m_NotificationID
					}
				});
				return;
			}
			this.DeleteNotification(notificationQueueItem);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00013584 File Offset: 0x00011784
		private void DeleteNotification(NotificationQueueItem item)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			NotificationImpl notification = item.Notification;
			try
			{
				connectionManager.BeginTransaction();
				using (InstrumentedSqlCommand instrumentedSqlCommand = PollWorker.NewStandardSqlCommand("DeleteNotification", connectionManager))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ID", notification.m_NotificationID);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
				using (InstrumentedSqlCommand instrumentedSqlCommand2 = PollWorker.NewStandardSqlCommand("DeleteNotification", connectionManager))
				{
					instrumentedSqlCommand2.Parameters.AddWithValue("@ID", notification.m_NotificationID);
					instrumentedSqlCommand2.ExecuteNonQuery();
				}
				if (notification.IsDataDriven)
				{
					ActiveSubscription activeSubscription = new ActiveSubscription(connectionManager);
					ActiveSubscriptionProperties activeSubscriptionProperties = ActiveSubscriptionProperties.TotalSuccesses;
					if (item.DeleteAsErrorForDataDrivenNotification)
					{
						activeSubscriptionProperties = ActiveSubscriptionProperties.TotalFailures;
					}
					activeSubscription.SetActiveSubscriptionProperty(notification.m_activationID, notification.m_subscriptionID, 1, activeSubscriptionProperties);
				}
				if (Global.m_Tracer.TraceInfo)
				{
					this.m_tracer.Trace("Notification {0} completed.  Success: {1}, Status: {2}, DeliveryExtension: {3}, Report: {4}, Attempt {5}", new object[]
					{
						notification.m_NotificationID,
						!item.DeleteAsErrorForDataDrivenNotification,
						notification.m_subscriptionStatus,
						notification.m_deliveryExtension,
						notification.Report.Name,
						notification.Attempt
					});
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001373C File Offset: 0x0001193C
		public override InstrumentedSqlCommand PollCommand
		{
			get
			{
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(string.Format(CultureInfo.InvariantCulture, this.m_commandFormat, base.NumberOfItemsToRetrieve)));
				instrumentedSqlCommand.CommandType = CommandType.Text;
				return instrumentedSqlCommand;
			}
		}

		// Token: 0x04000230 RID: 560
		private RSTrace m_tracer = RSTrace.NotificationTracer;

		// Token: 0x04000231 RID: 561
		private RSService m_service;

		// Token: 0x04000232 RID: 562
		private string m_commandFormat = "\r\n                                    declare @BatchID uniqueidentifier\r\n\r\n                                    set @BatchID = newid()\r\n\r\n                                    UPDATE [Notifications] WITH (TABLOCKX)\r\n                                        SET [BatchID] = @BatchID,\r\n                                        [ProcessStart] = GETUTCDATE(),\r\n                                        [ProcessHeartbeat] = GETUTCDATE()\r\n                                    FROM (\r\n                                        SELECT TOP {0}  [NotificationID] FROM [Notifications] WITH (TABLOCKX) WHERE ProcessStart is NULL and\r\n\t                                    (ProcessAfter is NULL or ProcessAfter < GETUTCDATE()) ORDER BY [NotificationEntered]\r\n                                    ) AS t1\r\n                                    WHERE [Notifications].[NotificationID] = t1.[NotificationID]\r\n\r\n                                    select top {0}\r\n\t\t                                    -- Notification data\r\n\t\t                                    N.[NotificationID],\r\n\t\t                                    N.[SubscriptionID],\r\n\t\t                                    N.[ActivationID],\r\n\t\t                                    N.[ReportID],\r\n\t\t                                    N.[SnapShotDate],\r\n\t\t                                    N.[DeliveryExtension],\r\n\t\t                                    N.[ExtensionSettings],\r\n                                            N.[Locale],\r\n\t\t                                    N.[Parameters],\r\n\t\t                                    N.[SubscriptionLastRunTime],\r\n\t\t                                    N.[ProcessStart],\r\n\t\t                                    N.[NotificationEntered],\r\n\t\t                                    N.[Attempt],\r\n\t\t                                    N.[IsDataDriven],\r\n\t\t                                    SUSER_SNAME(Owner.[Sid]),\r\n\t\t                                    Owner.[UserName],\r\n\t\t                                    -- Report Data\r\n\t\t                                    O.[Path],\r\n\t\t                                    N.[ReportZone],\r\n\t\t                                    O.[Type],\r\n\t\t                                    SD.NtSecDescPrimary,\r\n                                            N.[Version],\r\n                                            Owner.[AuthType],\r\n                                            SR.[SubscriptionResult]\r\n\t                                    from \r\n\t\t                                    [Notifications] N with (TABLOCKX) inner join [Catalog] O on O.[ItemID] = N.[ReportID]\r\n\t\t                                    inner join [Users] Owner on N.SubscriptionOwnerID = Owner.UserID\r\n\t\t                                    left outer join [SecData] SD on O.[PolicyID] = SD.[PolicyID] AND SD.AuthType = Owner.AuthType\r\n\t\t                                    left outer join [SubscriptionResults] SR on N.[SubscriptionID] = SR.[SubscriptionID] AND CHECKSUM(convert(nvarchar(max),N.[ExtensionSettings])) = SR.[ExtensionSettingsHash]\r\n\t                                    where \r\n\t\t                                    N.[BatchID] = @BatchID\r\n                                    ORDER BY [NotificationEntered]\r\n                                    ";
	}
}
