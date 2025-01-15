using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200023D RID: 573
	internal class SubscriptionDB : Storage
	{
		// Token: 0x060014D5 RID: 5333 RVA: 0x00050F0C File Offset: 0x0004F10C
		public void DeleteSubscription(Guid id)
		{
			try
			{
				InstrumentedSqlCommand instrumentedSqlCommand2;
				InstrumentedSqlCommand instrumentedSqlCommand = (instrumentedSqlCommand2 = this.NewStandardSqlCommand("AddSubscriptionToBeingDeleted", null));
				try
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", id);
					instrumentedSqlCommand.ExecuteNonQuery();
					this.ConnectionManager.CommitTransaction();
				}
				finally
				{
					if (instrumentedSqlCommand2 != null)
					{
						((IDisposable)instrumentedSqlCommand2).Dispose();
					}
				}
				instrumentedSqlCommand = (instrumentedSqlCommand2 = this.NewStandardSqlCommand("DeleteSubscription", null));
				try
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", id);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
				finally
				{
					if (instrumentedSqlCommand2 != null)
					{
						((IDisposable)instrumentedSqlCommand2).Dispose();
					}
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.IsSqlException)
				{
					this.ConnectionManager.AbortTransaction();
					using (InstrumentedSqlCommand instrumentedSqlCommand3 = this.NewStandardSqlCommand("RemoveSubscriptionFromBeingDeleted", null))
					{
						instrumentedSqlCommand3.Parameters.AddWithValue("@SubscriptionID", id);
						instrumentedSqlCommand3.ExecuteNonQuery();
						this.ConnectionManager.CommitTransaction();
						if (ex.SqlErrorNumber != 14262)
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00051034 File Offset: 0x0004F234
		public void CreateSubscription(SubscriptionImpl subscription)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateSubscription", null))
				{
					RSService rsservice = new RSService(false);
					instrumentedSqlCommand.AddParameter("@Report_Name", SqlDbType.NVarChar, rsservice.ExternalToCatalog(subscription.m_itemName.Value));
					instrumentedSqlCommand.AddParameter("@ReportZone", SqlDbType.Int, subscription.m_itemZone);
					this.SetProcedureParameters(instrumentedSqlCommand, subscription);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.IsSqlException)
				{
					string message = ex.Message;
					if (ex.Message == "Report Not Found")
					{
						throw new ItemNotFoundException(subscription.m_itemName.Value);
					}
				}
				throw;
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000510FC File Offset: 0x0004F2FC
		public void UpdateSubscription(SubscriptionImpl subscription)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSubscription", null))
			{
				this.SetProcedureParameters(instrumentedSqlCommand, subscription);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00051144 File Offset: 0x0004F344
		public void UpdateSubscriptionStatus(Guid id, string status)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSubscriptionStatus", null))
			{
				SqlParameter sqlParameter = new SqlParameter("@SubscriptionID", id);
				sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@Status", status);
				sqlParameter.SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x000511C8 File Offset: 0x0004F3C8
		public void UpdateSubscriptionResult(Guid subscriptionID, ParameterValue[] extensionSettings, string subscriptionResult)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSubscriptionResult", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				instrumentedSqlCommand.AddParameter("@ExtensionSettings", SqlDbType.NText, ParameterValueOrFieldReference.ThisArrayToXml(extensionSettings));
				instrumentedSqlCommand.AddParameter("@SubscriptionResult", SqlDbType.NVarChar, subscriptionResult);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00051244 File Offset: 0x0004F444
		public void InvalidateSubscription(Guid subscriptionID, InActiveFlags invalidFlags, string status)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("InvalidateSubscription", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Flags", invalidFlags);
				instrumentedSqlCommand.AddParameter("@LastStatus", SqlDbType.NVarChar, status);
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000512C0 File Offset: 0x0004F4C0
		public void UpdateSubscriptionLastRunInfo(Guid subscriptionID, InActiveFlags stateFlag, DateTime lastRunTime, string status)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSubscriptionLastRunInfo", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				instrumentedSqlCommand.Parameters.AddWithValue("@Flags", stateFlag);
				instrumentedSqlCommand.Parameters.AddWithValue("@LastRunTime", lastRunTime);
				instrumentedSqlCommand.AddParameter("@LastStatus", SqlDbType.NVarChar, status);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00051354 File Offset: 0x0004F554
		public SubscriptionImpl GetSubscription(Guid id, IPathTranslator pathTranslator, bool isCacheRefreshPlanExpected)
		{
			SubscriptionImpl subscriptionImpl = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSubscription", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", id);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						subscriptionImpl = new SubscriptionImpl(dataReader, 0, pathTranslator);
					}
					else
					{
						if (isCacheRefreshPlanExpected)
						{
							throw new CacheRefreshPlanNotFoundException(id.ToString());
						}
						throw new SubscriptionNotFoundException(id.ToString());
					}
				}
				if (subscriptionImpl != null && subscriptionImpl.IsDataDriven())
				{
					DataSourceInfoCollection dataSourcesAndResolveModelLink = new DBInterface(subscriptionImpl.Owner)
					{
						ConnectionManager = this.ConnectionManager
					}.GetDataSourcesAndResolveModelLink(subscriptionImpl.ID);
					if (dataSourcesAndResolveModelLink.Count == 0)
					{
						throw new InvalidDataSourceReferenceException(subscriptionImpl.ID.ToString());
					}
					DataSourceInfo theOnlyDataSource = dataSourcesAndResolveModelLink.GetTheOnlyDataSource();
					if (theOnlyDataSource.DataSourceReference != null)
					{
						theOnlyDataSource.DataSourceReference = pathTranslator.CatalogToExternal(new CatalogItemPath(theOnlyDataSource.DataSourceReference), subscriptionImpl.m_itemZone).Value;
					}
					subscriptionImpl.m_dataSource = theOnlyDataSource;
				}
			}
			return subscriptionImpl;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00051488 File Offset: 0x0004F688
		public List<SubscriptionImpl> ListSubscriptions(string user, ExternalItemPath path, bool pathIsSiteOrFolder, AuthenticationType authType, IPathTranslator pathTranslator, SubscriptionType subscriptionType)
		{
			List<SubscriptionImpl> list = new List<SubscriptionImpl>();
			StringBuilder stringBuilder = new StringBuilder("select \r\n                                                    S.[SubscriptionID],\r\n                                                    S.[Report_OID],\r\n                                                    S.[ReportZone],\r\n                                                    S.[Locale],\r\n                                                    S.[InactiveFlags],\r\n                                                    S.[DeliveryExtension], \r\n                                                    S.[ExtensionSettings],\r\n                                                    SUSER_SNAME(Modified.[Sid]), \r\n                                                    Modified.[UserName],\r\n                                                    S.[ModifiedDate], \r\n                                                    S.[Description],\r\n                                                    S.[LastStatus],\r\n                                                    S.[EventType],\r\n                                                    S.[MatchData],\r\n                                                    S.[Parameters],\r\n                                                    S.[DataSettings],\r\n                                                    A.[TotalNotifications],\r\n                                                    A.[TotalSuccesses],\r\n                                                    A.[TotalFailures],\r\n                                                    SUSER_SNAME(Owner.[Sid]),\r\n                                                    Owner.[UserName],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Path],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tS.[LastRunTime],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Type],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSD.NtSecDescPrimary,\r\n                                                    S.[Version],\r\n                                                    Owner.[AuthType]\r\n\t\t\t\t\t\t\t\t\t\t\t\tfrom\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t[Subscriptions] S inner join [Catalog] CAT on S.[Report_OID] = CAT.[ItemID]\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Owner on S.OwnerID = Owner.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Modified on S.ModifiedByID = Modified.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tleft outer join [SecData] SD on CAT.[PolicyID] = SD.[PolicyID] AND SD.AuthType = Owner.AuthType\r\n                                                    left outer join [ActiveSubscriptions] A with (NOLOCK) on S.[SubscriptionID] = A.[SubscriptionID]");
			bool flag = true;
			using (this.ConnectionManager.EnterThreadSafeContext())
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = Storage.NewSqlCommand("select \r\n                                                    S.[SubscriptionID],\r\n                                                    S.[Report_OID],\r\n                                                    S.[ReportZone],\r\n                                                    S.[Locale],\r\n                                                    S.[InactiveFlags],\r\n                                                    S.[DeliveryExtension], \r\n                                                    S.[ExtensionSettings],\r\n                                                    SUSER_SNAME(Modified.[Sid]), \r\n                                                    Modified.[UserName],\r\n                                                    S.[ModifiedDate], \r\n                                                    S.[Description],\r\n                                                    S.[LastStatus],\r\n                                                    S.[EventType],\r\n                                                    S.[MatchData],\r\n                                                    S.[Parameters],\r\n                                                    S.[DataSettings],\r\n                                                    A.[TotalNotifications],\r\n                                                    A.[TotalSuccesses],\r\n                                                    A.[TotalFailures],\r\n                                                    SUSER_SNAME(Owner.[Sid]),\r\n                                                    Owner.[UserName],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Path],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tS.[LastRunTime],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Type],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSD.NtSecDescPrimary,\r\n                                                    S.[Version],\r\n                                                    Owner.[AuthType]\r\n\t\t\t\t\t\t\t\t\t\t\t\tfrom\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t[Subscriptions] S inner join [Catalog] CAT on S.[Report_OID] = CAT.[ItemID]\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Owner on S.OwnerID = Owner.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Modified on S.ModifiedByID = Modified.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tleft outer join [SecData] SD on CAT.[PolicyID] = SD.[PolicyID] AND SD.AuthType = Owner.AuthType\r\n                                                    left outer join [ActiveSubscriptions] A with (NOLOCK) on S.[SubscriptionID] = A.[SubscriptionID]", CommandType.Text, base.Connection, base.Transaction, base.SqlCommandTimeout))
				{
					if (user != null && user != "")
					{
						if (authType == AuthenticationType.Windows)
						{
							flag = this.AddClause(stringBuilder, flag, "Owner.[Sid] = @UserSid");
							instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Native.NameToSid(user));
						}
						else
						{
							flag = this.AddClause(stringBuilder, flag, "Owner.[UserName] = @UserName");
							instrumentedSqlCommand.Parameters.AddWithValue("@UserName", user);
						}
					}
					if (!ItemPathBase.IsNullOrEmpty(path))
					{
						string text = pathTranslator.ExternalToCatalog(path.Value);
						flag = this.AddClause(stringBuilder, flag, "S.[ReportZone] = @ReportZone");
						instrumentedSqlCommand.Parameters.AddWithValue("@ReportZone", pathTranslator.GetExternalRootZone(path));
						if (pathIsSiteOrFolder)
						{
							string text2 = string.Format(CultureInfo.InvariantCulture, "SUBSTRING(CAT.[Path],1,{0}) = @Path", text.Length);
							flag = this.AddClause(stringBuilder, flag, text2);
							instrumentedSqlCommand.Parameters.AddWithValue("@Path", text);
						}
						else
						{
							flag = this.AddClause(stringBuilder, flag, "CAT.[Path] = @Report");
							instrumentedSqlCommand.Parameters.AddWithValue("@Report", text);
						}
					}
					if (subscriptionType != SubscriptionType.ReportSubscription)
					{
						if (subscriptionType == SubscriptionType.CacheRefreshPlan)
						{
							flag = this.AddClause(stringBuilder, flag, "S.[EventType] = 'RefreshCache' OR S.[EventType] = 'DataModelRefresh' ");
						}
					}
					else
					{
						flag = this.AddClause(stringBuilder, flag, "S.[EventType] = 'TimedSubscription' OR S.[EventType] = 'SnapshotUpdated'");
					}
					instrumentedSqlCommand.CommandText = stringBuilder.ToString();
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						Hashtable hashtable = new Hashtable();
						while (dataReader.Read())
						{
							SubscriptionImpl subscriptionImpl = null;
							try
							{
								subscriptionImpl = new SubscriptionImpl(dataReader, 0, pathTranslator);
							}
							catch (InvalidSubscriptionException)
							{
								continue;
							}
							if (!this.IsDuplicateSubscription(hashtable, subscriptionImpl.ID))
							{
								list.Add(subscriptionImpl);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x000516E4 File Offset: 0x0004F8E4
		public List<SubscriptionImpl> ListSubscriptionsUsingDataSource(ExternalItemPath dataSourceName, IPathTranslator pathTranslator)
		{
			List<SubscriptionImpl> list = new List<SubscriptionImpl>();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListSubscriptionsUsingDataSource", null))
			{
				instrumentedSqlCommand.AddParameter("@DataSourceName", SqlDbType.NVarChar, pathTranslator.ExternalToCatalog(dataSourceName.Value));
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					Hashtable hashtable = new Hashtable();
					while (dataReader.Read())
					{
						SubscriptionImpl subscriptionImpl = null;
						try
						{
							subscriptionImpl = new SubscriptionImpl(dataReader, 0, pathTranslator);
						}
						catch (InvalidSubscriptionException)
						{
							continue;
						}
						if (!this.IsDuplicateSubscription(hashtable, subscriptionImpl.ID))
						{
							list.Add(subscriptionImpl);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000517A0 File Offset: 0x0004F9A0
		private bool IsDuplicateSubscription(Hashtable list, Guid id)
		{
			bool flag = false;
			if (list[id] == null)
			{
				list.Add(id, id);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000517D4 File Offset: 0x0004F9D4
		private bool AddClause(StringBuilder query, bool isFirst, string clause)
		{
			query.Append(isFirst ? " where " : " and ");
			query.Append('(');
			query.Append(clause);
			query.Append(')');
			return false;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00051808 File Offset: 0x0004FA08
		private void SetProcedureParameters(InstrumentedSqlCommand command, SubscriptionImpl subscription)
		{
			command.Parameters.AddWithValue("@id", subscription.m_id);
			command.Parameters.AddWithValue("@OwnerSid", Global.NameToSid(subscription.Owner));
			command.AddParameter("@OwnerName", SqlDbType.NVarChar, subscription.Owner.UserName);
			command.Parameters.AddWithValue("@OwnerAuthType", (int)subscription.Owner.AuthenticationType);
			command.AddParameter("@Locale", SqlDbType.NVarChar, subscription.m_subscriptionCulture);
			command.AddParameter("@DeliveryExtension", SqlDbType.NVarChar, subscription.DeliveryExtension);
			command.Parameters.AddWithValue("@InactiveFlags", (int)subscription.m_inactiveFlags);
			command.Parameters.AddWithValue("@ModifiedBySid", Global.NameToSid(subscription.ModifiedBy));
			command.AddParameter("@ModifiedByName", SqlDbType.NVarChar, subscription.ModifiedBy.UserName);
			command.Parameters.AddWithValue("@ModifiedByAuthType", (int)subscription.ModifiedBy.AuthenticationType);
			command.Parameters.AddWithValue("@ModifiedDate", subscription.m_modifiedDate);
			command.AddParameter("@Description", SqlDbType.NVarChar, subscription.m_description);
			command.AddParameter("@LastStatus", SqlDbType.NVarChar, subscription.LastStatus);
			command.AddParameter("@EventType", SqlDbType.NVarChar, subscription.m_eventType);
			if (subscription.m_extensionSettings != null)
			{
				command.AddParameter("@ExtensionSettings", SqlDbType.NText, ParameterValueOrFieldReference.ThisArrayToXml(subscription.m_extensionSettings.ParameterValues));
			}
			if (subscription.m_matchData != null)
			{
				command.AddParameter("@MatchData", SqlDbType.NText, subscription.m_matchData);
			}
			if (subscription.m_parameters != null)
			{
				command.AddParameter("@Parameters", SqlDbType.NText, ParameterValueOrFieldReference.ThisArrayToXml(subscription.m_parameters));
			}
			if (subscription.m_dataSet != null)
			{
				command.AddParameter("@DataSettings", SqlDbType.NText, Microsoft.ReportingServices.Library.Soap.DataSetDefinition.ThisToXml(subscription.m_dataSet));
			}
			command.Parameters.AddWithValue("@Version", subscription.m_version);
		}

		// Token: 0x0400078C RID: 1932
		private const string _listProjection = "select \r\n                                                    S.[SubscriptionID],\r\n                                                    S.[Report_OID],\r\n                                                    S.[ReportZone],\r\n                                                    S.[Locale],\r\n                                                    S.[InactiveFlags],\r\n                                                    S.[DeliveryExtension], \r\n                                                    S.[ExtensionSettings],\r\n                                                    SUSER_SNAME(Modified.[Sid]), \r\n                                                    Modified.[UserName],\r\n                                                    S.[ModifiedDate], \r\n                                                    S.[Description],\r\n                                                    S.[LastStatus],\r\n                                                    S.[EventType],\r\n                                                    S.[MatchData],\r\n                                                    S.[Parameters],\r\n                                                    S.[DataSettings],\r\n                                                    A.[TotalNotifications],\r\n                                                    A.[TotalSuccesses],\r\n                                                    A.[TotalFailures],\r\n                                                    SUSER_SNAME(Owner.[Sid]),\r\n                                                    Owner.[UserName],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Path],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tS.[LastRunTime],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tCAT.[Type],\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tSD.NtSecDescPrimary,\r\n                                                    S.[Version],\r\n                                                    Owner.[AuthType]\r\n\t\t\t\t\t\t\t\t\t\t\t\tfrom\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t[Subscriptions] S inner join [Catalog] CAT on S.[Report_OID] = CAT.[ItemID]\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Owner on S.OwnerID = Owner.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tinner join [Users] Modified on S.ModifiedByID = Modified.UserID\r\n\t\t\t\t\t\t\t\t\t\t\t\t\tleft outer join [SecData] SD on CAT.[PolicyID] = SD.[PolicyID] AND SD.AuthType = Owner.AuthType\r\n                                                    left outer join [ActiveSubscriptions] A with (NOLOCK) on S.[SubscriptionID] = A.[SubscriptionID]";
	}
}
