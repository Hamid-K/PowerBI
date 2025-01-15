using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002B RID: 43
	internal sealed class DataDrivenSubscriptionHandler : IEventHandler, IExtension
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string reportName)
		{
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Microsoft.ReportingServices.Extensions.Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Microsoft.ReportingServices.Extensions.Subscription subscription)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			Guid guid = Guid.Empty;
			try
			{
				if (!Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions))
				{
					if (Global.m_Tracer.TraceWarning)
					{
						Global.m_Tracer.Trace(TraceLevel.Warning, "Data driven subscription event found on SKU that does not allow them.  Event will be skipped.");
					}
					return;
				}
				guid = new Guid(eventData);
			}
			catch (FormatException)
			{
				if (Global.m_Tracer.TraceError)
				{
					Global.m_Tracer.Trace(TraceLevel.Error, "Event data for a Data Driven Subscription event was not a Guid as expected");
				}
				return;
			}
			ReportServerThreadPool.QueueUserWorkItem(new WaitCallback(this.DataDrivenSubscriptionWorker), guid);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005C88 File Offset: 0x00003E88
		public string LocalizedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005C8C File Offset: 0x00003E8C
		private void DataDrivenSubscriptionWorker(object o)
		{
			Guid guid = Guid.Empty;
			try
			{
				guid = (Guid)o;
				SubscriptionDB subscriptionDB = new SubscriptionDB();
				subscriptionDB.ConnectionManager = new ConnectionManager();
				subscriptionDB.ConnectionManager.WillDisconnectStorage();
				ActiveSubscription activeSubscription = new ActiveSubscription(subscriptionDB.ConnectionManager);
				RSService rsservice = null;
				SubscriptionImpl subscriptionImpl = null;
				Guid guid2 = Guid.Empty;
				try
				{
					try
					{
						subscriptionDB.UpdateSubscriptionStatus(guid, RepLibRes.SubscriptionPending);
						subscriptionDB.Commit();
					}
					catch
					{
						subscriptionDB.AbortTransaction();
						throw;
					}
					Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection = null;
					Microsoft.ReportingServices.Library.DataSet dataSet = null;
					Microsoft.ReportingServices.DataProcessing.IDataReader dataReader = null;
					SubscriptionDB subscriptionDB2 = null;
					IDeliveryExtension deliveryExtension = null;
					try
					{
						subscriptionDB2 = new SubscriptionDB();
						subscriptionDB2.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
						subscriptionDB2.ConnectionManager.WillDisconnectStorage();
						ActiveSubscription activeSubscription2 = new ActiveSubscription(subscriptionDB2.ConnectionManager);
						rsservice = new RSService(false);
						subscriptionImpl = subscriptionDB2.GetSubscription(guid, rsservice, false);
						if (Global.m_Tracer.TraceInfo)
						{
							Global.m_Tracer.Trace(TraceLevel.Info, "Handling data-driven subscription {0} to report {1}, owner: {2}, delivery extension: {3}.", new object[]
							{
								subscriptionImpl.ID,
								subscriptionImpl.ItemPath,
								subscriptionImpl.Owner.UserName,
								subscriptionImpl.DeliveryExtension
							});
						}
						Localization.SetCultureFromPriorityList(new string[] { subscriptionImpl.Culture }, false);
						if (!subscriptionImpl.IsDataDriven())
						{
							if (Global.m_Tracer.TraceError)
							{
								Global.m_Tracer.Trace(TraceLevel.Error, "Non data driven subscription {0} passed as event data for data driven subscription event.", new object[] { subscriptionImpl.ID });
							}
							return;
						}
						if (!subscriptionImpl.IsActive())
						{
							Global.m_Tracer.Trace(TraceLevel.Warning, "Skipping processing data driven subscription {0} as it has an inactive state.", new object[] { subscriptionImpl.ID });
						}
						if (!string.IsNullOrEmpty(subscriptionImpl.DeliveryExtension))
						{
							deliveryExtension = (IDeliveryExtension)ExtensionClassFactory.GetNewInstanceExtensionClass(subscriptionImpl.DeliveryExtension, "Delivery");
						}
						if (deliveryExtension == null)
						{
							throw new DeliveryExtensionNotFoundException();
						}
						rsservice = new RSService(subscriptionImpl.Owner.UserName, subscriptionImpl.Owner.AuthenticationType, subscriptionImpl.ReportName);
						rsservice.WillDisconnectStorage(subscriptionDB2.ConnectionManager);
						CachedSystemProperties.InvalidateCache();
						new SubscriptionManager(rsservice).ValidateSubscriptionParameters(subscriptionImpl.m_itemName, subscriptionImpl.m_parameters, JobType.SystemJobType);
						dbConnection = rsservice.OpenDataSourceConnection(subscriptionImpl.m_dataSource, rsservice.HowToCreateDataExtensionInstance);
						dataSet = new Microsoft.ReportingServices.Library.DataSet();
						dataSet.FromSoapDataSet(subscriptionImpl.m_dataSet);
						using (Microsoft.ReportingServices.DataProcessing.IDbCommand dbCommand = dbConnection.CreateCommand())
						{
							dbCommand.CommandText = dataSet.Query.CommandText;
							dbCommand.CommandType = Microsoft.ReportingServices.DataProcessing.CommandType.Text;
							dbCommand.CommandTimeout = dataSet.Query.Timeout;
							dataReader = dbCommand.ExecuteReader(Microsoft.ReportingServices.DataProcessing.CommandBehavior.SingleResult);
							if (dataReader.FieldCount == 0)
							{
								throw new CannotPrepareQueryException(string.Format(CultureInfo.InvariantCulture, "Data-driven subscription {0} query did not return any field. \r\n SQL: {1}", guid, subscriptionImpl.m_dataSet.Query.CommandText));
							}
						}
						subscriptionImpl.m_lastRunTime = DateTime.Now;
						guid2 = activeSubscription2.CreateNewActiveSubscription(guid);
					}
					catch (SubscriptionNotFoundException)
					{
						subscriptionDB.AbortTransaction();
						if (Global.m_Tracer.TraceInfo)
						{
							Global.m_Tracer.Trace(TraceLevel.Info, "Data Driven subscription not found.  It may have been deleted before the event was processed");
						}
						if (dbConnection != null)
						{
							dbConnection.Close();
						}
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
						return;
					}
					catch (Exception)
					{
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
						throw;
					}
					finally
					{
						Localization.Reset();
						if (subscriptionDB2 != null && subscriptionDB2.ConnectionManager != null)
						{
							subscriptionDB2.ConnectionManager.DisconnectStorage();
						}
					}
					Settings settings = new Settings();
					settings.FromSoapParameterValueArray(subscriptionImpl.m_extensionSettings.ParameterValues);
					foreach (Setting setting in ProviderManager.InitDeliveryExtension(deliveryExtension, true, null, null))
					{
						if (setting.Encrypted)
						{
							SettingImpl settingImpl = settings[setting.Name];
							if (settingImpl != null)
							{
								settingImpl.EncryptFieldValue = true;
							}
						}
					}
					ParamValues paramValues = new ParamValues();
					paramValues.FromXml(ParameterValueOrFieldReference.ThisArrayToXml(subscriptionImpl.m_parameters));
					int num = 0;
					try
					{
						Fields fields = dataSet.Fields;
						using (MappingDataReader mappingDataReader = new MappingDataReader("", dataReader, fields.FieldAliases, fields.FieldNames, null))
						{
							Hashtable hashtable = CollectionsUtil.CreateCaseInsensitiveHashtable(fields.FieldAliases.Length);
							for (int j = 0; j < fields.FieldAliases.Length; j++)
							{
								hashtable[fields.FieldAliases[j]] = j;
							}
							while (mappingDataReader.GetNextRow())
							{
								foreach (string text in settings.FieldKeys)
								{
									object obj = hashtable[text];
									int num2 = ((obj != null) ? ((int)obj) : (-1));
									object fieldValue = mappingDataReader.GetFieldValue(num2);
									settings.m_fields[text] = ((fieldValue == null || fieldValue is DBNull) ? null : fieldValue.ToString());
								}
								foreach (string text2 in paramValues.FieldKeys)
								{
									object obj2 = hashtable[text2];
									int num2 = ((obj2 != null) ? ((int)obj2) : (-1));
									object fieldValue2 = mappingDataReader.GetFieldValue(num2);
									paramValues.Fields[text2] = ((fieldValue2 == null || fieldValue2 is DBNull) ? null : fieldValue2.ToString());
								}
								try
								{
									activeSubscription.CreateDataDrivenNotification(subscriptionImpl, guid2, settings, paramValues);
								}
								catch (ReportServerStorageException ex)
								{
									if (!ex.IsSqlException)
									{
										throw;
									}
									activeSubscription.AbortTransaction();
									if (547 != ex.SqlErrorNumber && 50000 != ex.SqlErrorNumber)
									{
										throw;
									}
									if (Global.m_Tracer.TraceVerbose)
									{
										Global.m_Tracer.Trace(TraceLevel.Error, ex.ToString());
									}
									Global.m_Tracer.Trace(TraceLevel.Error, "Data Driven Subscription {0} has been deleted. Stopping further subscription processing and generating notifications.", new object[] { guid });
									return;
								}
								num++;
								activeSubscription.Commit();
							}
						}
					}
					finally
					{
						if (dbConnection != null)
						{
							dbConnection.Close();
						}
					}
					if (num == 0)
					{
						subscriptionImpl.LastStatus = RepLibRes.SubscriptionDone(0, 0, 0);
					}
					subscriptionDB.UpdateSubscription(subscriptionImpl);
					if (num != 0)
					{
						activeSubscription.SetActiveSubscriptionProperty(guid2, subscriptionImpl.ID, num, ActiveSubscriptionProperties.TotalNotifications);
					}
					else
					{
						activeSubscription.DeleteActiveSubscription(guid2);
					}
					subscriptionDB.Commit();
					ServiceController.ItemPlacedInNotificationQueue();
				}
				catch (Exception ex2)
				{
					subscriptionDB.AbortTransaction();
					if (subscriptionImpl != null)
					{
						Exception ex3 = new Exception();
						if (ex2.InnerException != null)
						{
							ex3 = ex2.InnerException;
						}
						subscriptionImpl.LastStatus = RepLibRes.SubscriptionError(ex2.Message);
						if (ex2 is InvalidDataSourceReferenceException || ex3 is InvalidDataSourceReferenceException)
						{
							subscriptionImpl.m_inactiveFlags |= InActiveFlags.SharedDataSourceRemoved;
						}
						else if (ex2 is ReportParameterValueNotSetException || ex3 is ReportParameterValueNotSetException)
						{
							subscriptionImpl.m_inactiveFlags |= InActiveFlags.MissingParameterValue;
						}
						else if (ex2 is ReportParameterTypeMismatchException || ex3 is ReportParameterTypeMismatchException || ex2 is InvalidReportParameterException || ex3 is InvalidReportParameterException)
						{
							subscriptionImpl.m_inactiveFlags |= InActiveFlags.InvalidParameterValue;
						}
						else if (ex2 is UnknownReportParameterException || ex3 is UnknownReportParameterException)
						{
							subscriptionImpl.m_inactiveFlags |= InActiveFlags.UnknownItemParameter;
						}
						try
						{
							subscriptionDB.UpdateSubscription(subscriptionImpl);
							activeSubscription.DeleteActiveSubscription(guid2);
							subscriptionDB.Commit();
						}
						catch
						{
							subscriptionDB.AbortTransaction();
						}
					}
					throw;
				}
				finally
				{
					if (rsservice != null)
					{
						rsservice.DisconnectStorage();
					}
				}
			}
			catch (Exception ex4)
			{
				if (Global.m_Tracer.TraceError)
				{
					Global.m_Tracer.Trace(TraceLevel.Error, "Error processing data driven subscription {0}: {1}", new object[]
					{
						guid.ToString(),
						ex4.ToString()
					});
				}
			}
		}
	}
}
