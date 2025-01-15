using System;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000263 RID: 611
	internal sealed class UpgradeConnectionEncryption : UpgradeMultipleItemsTask
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x00058504 File Offset: 0x00056704
		public UpgradeConnectionEncryption(UpgradePollWorker pollWorker)
			: base(pollWorker)
		{
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000585A8 File Offset: 0x000567A8
		protected override UpgradeMultipleItemsTask.ItemCollection GetItemsToUpdate()
		{
			UpgradeMultipleItemsTask.ItemCollection itemCollection = new UpgradeMultipleItemsTask.ItemCollection();
			this.GetDataSourcesToUpdate(itemCollection);
			this.GetSubscriptionsToUpdate(itemCollection);
			return itemCollection;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000585CC File Offset: 0x000567CC
		private void GetDataSourcesToUpdate(UpgradeMultipleItemsTask.ItemCollection items)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetDataSourceForUpgrade", null))
			{
				instrumentedSqlCommand.AddParameter("@CurrentVersion", SqlDbType.Int, Encryption.CurrentVersion);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						items.Add(new UpgradeConnectionEncryption.ConnectionEncryptionItemInfo(guid));
					}
				}
			}
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00058654 File Offset: 0x00056854
		private void GetSubscriptionsToUpdate(UpgradeMultipleItemsTask.ItemCollection items)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSubscriptionsForUpgrade", null))
			{
				instrumentedSqlCommand.AddParameter("@CurrentVersion", SqlDbType.Int, Encryption.CurrentVersion);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						items.Add(new UpgradeConnectionEncryption.SubscriptionEncryptionItemInfo(guid));
					}
				}
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x000586DC File Offset: 0x000568DC
		public override string Name
		{
			get
			{
				return "UpgradeConnectionEncryption";
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000586E3 File Offset: 0x000568E3
		public static byte[] ReencryptData(int originalDataVersion, byte[] originalData, string tag)
		{
			return UpgradeConnectionEncryption.ConnectionEncryptionItemInfo.ReencryptData(originalDataVersion, originalData, tag);
		}

		// Token: 0x020004C1 RID: 1217
		private sealed class ConnectionEncryptionItemInfo : UpgradeMultipleItemsTask.ItemInfo
		{
			// Token: 0x06002430 RID: 9264 RVA: 0x00085BDB File Offset: 0x00083DDB
			public ConnectionEncryptionItemInfo(Guid dsID)
			{
				this.m_dsID = dsID;
			}

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x06002431 RID: 9265 RVA: 0x00085BEA File Offset: 0x00083DEA
			public override string TraceIdentifier
			{
				get
				{
					return this.m_dsID.ToString("D", CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06002432 RID: 9266 RVA: 0x00085C04 File Offset: 0x00083E04
			public override void Upgrade(Storage storage)
			{
				ActivationDB activationDB = new ActivationDB(storage.ConnectionManager);
				int num;
				byte[] array;
				byte[] array2;
				byte[] array3;
				byte[] array4;
				int num2;
				activationDB.GetDatasourceInfoForReencryption(this.m_dsID, out num, out array, out array2, out array3, out array4, out num2);
				if (!Encryption.IsCurrentVersion(num2))
				{
					byte[] array5 = UpgradeConnectionEncryption.ConnectionEncryptionItemInfo.ReencryptData(num2, array, "ConnectionString");
					byte[] array6 = UpgradeConnectionEncryption.ConnectionEncryptionItemInfo.ReencryptData(num2, array2, "OriginalConnectionString");
					byte[] array7 = UpgradeConnectionEncryption.ConnectionEncryptionItemInfo.ReencryptData(num2, array3, "UserName");
					byte[] array8 = UpgradeConnectionEncryption.ConnectionEncryptionItemInfo.ReencryptData(num2, array4, "Password");
					activationDB.SetReencryptedDatasourceInfo(this.m_dsID, num, array5, array6, array7, array8, Encryption.CurrentVersion);
				}
			}

			// Token: 0x06002433 RID: 9267 RVA: 0x00085C98 File Offset: 0x00083E98
			public static byte[] ReencryptData(int originalDataVersion, byte[] originalData, string tag)
			{
				byte[] array = null;
				byte[] array2;
				try
				{
					array = CatalogEncryption.Instance.Decrypt(originalDataVersion, originalData, tag);
					array2 = CatalogEncryption.Instance.Encrypt(array, tag);
				}
				finally
				{
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = 0;
						}
						array = null;
					}
				}
				return array2;
			}

			// Token: 0x04001101 RID: 4353
			private Guid m_dsID;
		}

		// Token: 0x020004C2 RID: 1218
		private sealed class SubscriptionEncryptionItemInfo : UpgradeMultipleItemsTask.ItemInfo
		{
			// Token: 0x06002434 RID: 9268 RVA: 0x00085CF0 File Offset: 0x00083EF0
			public SubscriptionEncryptionItemInfo(Guid subscriptionID)
			{
				this.m_subscriptionID = subscriptionID;
			}

			// Token: 0x17000A94 RID: 2708
			// (get) Token: 0x06002435 RID: 9269 RVA: 0x00085CFF File Offset: 0x00083EFF
			public override string TraceIdentifier
			{
				get
				{
					return this.m_subscriptionID.ToString("D", CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06002436 RID: 9270 RVA: 0x00085D18 File Offset: 0x00083F18
			public override void Upgrade(Storage storage)
			{
				using (ActivationDB activationDB = new ActivationDB(storage.ConnectionManager))
				{
					string text;
					Settings settings;
					SettingImpl[] array;
					int num;
					activationDB.GetSettingsForReencryption(this.m_subscriptionID, out text, out settings, out array, out num);
					if (!Encryption.IsCurrentVersion(num))
					{
						foreach (SettingImpl settingImpl in array)
						{
							string text2 = CatalogEncryption.Instance.DecryptToString(num, settingImpl.Value, settingImpl.Name);
							settingImpl.Value = CatalogEncryption.Instance.EncryptToString(text2, settingImpl.Name);
						}
						string text3 = ParameterValueOrFieldReference.ThisArrayToXml(settings.ToSoapParameterValueOrFieldReferenceArray());
						activationDB.SetReencryptedSubscriptionInfo(this.m_subscriptionID, text3, Encryption.CurrentVersion);
					}
				}
			}

			// Token: 0x04001102 RID: 4354
			private Guid m_subscriptionID;
		}
	}
}
