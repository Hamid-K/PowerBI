using System;
using System.Data;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000016 RID: 22
	internal class ActiveSubscription : Storage
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004714 File Offset: 0x00002914
		public ActiveSubscription(ConnectionManager connManager)
		{
			this.ConnectionManager = connManager;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004724 File Offset: 0x00002924
		public void CreateDataDrivenNotification(SubscriptionImpl subscription, Guid activationID, Settings settings, ParamValues paramValues)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateDataDrivenNotification", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscription.ID);
				instrumentedSqlCommand.Parameters.AddWithValue("@ActiveationID", activationID);
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", subscription.ItemID);
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportZone", subscription.m_itemZone);
				instrumentedSqlCommand.AddParameter("@Locale", SqlDbType.NVarChar, subscription.m_subscriptionCulture);
				InstrumentedSqlCommand instrumentedSqlCommand2 = instrumentedSqlCommand;
				string text = "@ExtensionSettings";
				SqlDbType sqlDbType = SqlDbType.NText;
				ParameterValueOrFieldReference[] array = settings.ToSoapParameterValueArray();
				instrumentedSqlCommand2.AddParameter(text, sqlDbType, ParameterValueOrFieldReference.ThisArrayToXml(array));
				instrumentedSqlCommand.AddParameter("@Parameters", SqlDbType.NText, paramValues.ToXml(false));
				instrumentedSqlCommand.Parameters.AddWithValue("@LastRunTime", subscription.m_lastRunTime);
				instrumentedSqlCommand.AddParameter("@DeliveryExtension", SqlDbType.NVarChar, subscription.DeliveryExtension);
				instrumentedSqlCommand.Parameters.AddWithValue("@OwnerSid", Global.NameToSid(subscription.Owner)).SqlDbType = SqlDbType.VarBinary;
				instrumentedSqlCommand.AddParameter("@OwnerName", SqlDbType.NVarChar, subscription.Owner.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@OwnerAuthType", (int)subscription.Owner.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@Version", subscription.m_version);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000048C8 File Offset: 0x00002AC8
		public Guid CreateNewActiveSubscription(Guid subscriptionID)
		{
			Guid guid2;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateNewActiveSubscription", null))
			{
				Guid guid = Guid.NewGuid();
				instrumentedSqlCommand.Parameters.AddWithValue("@ActiveID", guid);
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				instrumentedSqlCommand.ExecuteNonQuery();
				guid2 = guid;
			}
			return guid2;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000493C File Offset: 0x00002B3C
		private void FinishProcessingActiveSubscription(Guid activeID, Guid subscriptionID, int total, int failures)
		{
			this.DeleteActiveSubscription(activeID);
			new SubscriptionDB
			{
				ConnectionManager = this.ConnectionManager
			}.UpdateSubscriptionStatus(subscriptionID, RepLibRes.SubscriptionDone(total, total, failures));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004968 File Offset: 0x00002B68
		public void SetActiveSubscriptionProperty(Guid activeID, Guid subscriptionID, int val, ActiveSubscriptionProperties property)
		{
			int num;
			int num2;
			int num3;
			this.UpdateActiveSubscription(val, property, activeID, out num, out num2, out num3);
			if (property == ActiveSubscriptionProperties.TotalNotifications)
			{
				if (num2 + num3 >= val)
				{
					this.FinishProcessingActiveSubscription(activeID, subscriptionID, val, num3);
					return;
				}
			}
			else if (num != -1 && num2 + num3 >= num)
			{
				this.FinishProcessingActiveSubscription(activeID, subscriptionID, num, num3);
				return;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000049B0 File Offset: 0x00002BB0
		public void DeleteActiveSubscription(Guid activeID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteActiveSubscription", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ActiveID", activeID);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004A04 File Offset: 0x00002C04
		public void UpdateActiveSubscription(int val, ActiveSubscriptionProperties property, Guid activeID, out int total, out int success, out int failures)
		{
			success = (total = (failures = -1));
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateActiveSubscription", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ActiveID", activeID);
				switch (property)
				{
				case ActiveSubscriptionProperties.TotalNotifications:
					instrumentedSqlCommand.Parameters.AddWithValue("@TotalNotifications", val);
					break;
				case ActiveSubscriptionProperties.TotalSuccesses:
					instrumentedSqlCommand.Parameters.AddWithValue("@TotalSuccesses", val);
					break;
				case ActiveSubscriptionProperties.TotalFailures:
					instrumentedSqlCommand.Parameters.AddWithValue("@TotalFailures", val);
					break;
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						if (!dataReader.IsDBNull(0))
						{
							total = dataReader.GetInt32(0);
						}
						success = dataReader.GetInt32(1);
						failures = dataReader.GetInt32(2);
					}
				}
			}
		}
	}
}
