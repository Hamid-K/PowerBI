using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000070 RID: 112
	internal class NotificationImpl : NotificationWithResult
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00012A90 File Offset: 0x00010C90
		internal NotificationImpl(IDataRecord record, RSService service)
		{
			this.m_NotificationID = record.GetGuid(0);
			this.m_subscriptionID = record.GetGuid(1);
			if (!record.IsDBNull(2))
			{
				this.m_activationID = record.GetGuid(2);
			}
			this.m_reportID = record.GetGuid(3);
			if (!record.IsDBNull(4))
			{
				this.m_snapShotDate = record.GetDateTime(4);
			}
			this.m_deliveryExtension = record.GetString(5);
			this.m_extensionSettings = ParameterValue.XmlToThisArray(record.GetString(6));
			this.m_subscriptionLocale = record.GetString(7);
			if (!record.IsDBNull(8))
			{
				this.m_parameters = record.GetString(8);
				this.m_originalParameters = this.m_parameters;
			}
			this.m_subscriptionLastRunTime = record.GetDateTime(9);
			this.m_processStart = record.GetDateTime(10);
			this.m_notificationEntered = record.GetDateTime(11);
			this.m_isDataDriven = record.GetBoolean(13);
			if (!record.IsDBNull(12))
			{
				this.m_attempt = record.GetInt32(12);
			}
			this.m_owner = UserUtil.GetUserNameBySid(record, 14, 15);
			CatalogItemPath catalogItemPath = new CatalogItemPath(record.GetString(16));
			this.m_pathZone = record.GetInt32(17);
			this.m_path = service.CatalogToExternal(catalogItemPath, this.m_pathZone, true);
			this.m_reportType = (ItemType)record.GetInt32(18);
			if (!record.IsDBNull(19))
			{
				this.m_secDesc = DataReaderHelper.ReadAllBytes(record, 19);
			}
			this.m_version = record.GetInt32(20);
			this.m_authType = (AuthenticationType)record.GetInt32(21);
			if (!record.IsDBNull(22))
			{
				this.m_subscriptionResult = record.GetString(22);
			}
			this.m_report = new ReportImpl(this);
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00012CBF File Offset: 0x00010EBF
		internal ExternalItemPath Path
		{
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012CC7 File Offset: 0x00010EC7
		public override Report Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00012CCF File Offset: 0x00010ECF
		public override string Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00012CD7 File Offset: 0x00010ED7
		public string DeliveryExtension
		{
			get
			{
				return this.m_deliveryExtension;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00012CE0 File Offset: 0x00010EE0
		public override Setting[] UserData
		{
			get
			{
				lock (this)
				{
					if (this.m_settings == null)
					{
						Settings settings = new Settings();
						Settings settings2 = settings;
						ParameterValueOrFieldReference[] extensionSettings = this.m_extensionSettings;
						settings2.FromSoapParameterValueArray(extensionSettings);
						this.m_settings = settings.SettingsArray;
					}
				}
				return this.m_settings;
			}
		}

		// Token: 0x17000140 RID: 320
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00012D44 File Offset: 0x00010F44
		public override string Status
		{
			set
			{
				this.m_subscriptionStatus = value;
				this.m_statusChanged = true;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00012D5D File Offset: 0x00010F5D
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00012D54 File Offset: 0x00010F54
		public override bool IsDataDriven
		{
			get
			{
				return this.m_isDataDriven;
			}
			set
			{
				this.m_isDataDriven = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00012D65 File Offset: 0x00010F65
		public override int MaxNumberOfRetries
		{
			get
			{
				return this.m_maxRetries;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00012D6D File Offset: 0x00010F6D
		public override int Attempt
		{
			get
			{
				return this.m_attempt;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00012D75 File Offset: 0x00010F75
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00012D7D File Offset: 0x00010F7D
		public override bool Retry
		{
			get
			{
				return this.m_retry;
			}
			set
			{
				this.m_retry = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00012D86 File Offset: 0x00010F86
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00012D8E File Offset: 0x00010F8E
		public override string SubscriptionResult
		{
			get
			{
				return this.m_subscriptionResult;
			}
			set
			{
				this.m_subscriptionResult = value;
				this.m_resultChanged = true;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012D9E File Offset: 0x00010F9E
		public override void Save()
		{
			if (this.m_statusChanged)
			{
				this.UpdateStatus();
			}
			if (this.m_resultChanged)
			{
				this.UpdateResult();
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00012DBC File Offset: 0x00010FBC
		public int Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00012DC4 File Offset: 0x00010FC4
		internal AuthenticationType AuthType
		{
			get
			{
				return this.m_authType;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012DCC File Offset: 0x00010FCC
		internal void UpdateStatus()
		{
			if (!this.m_isDataDriven)
			{
				SubscriptionDB subscriptionDB = new SubscriptionDB();
				subscriptionDB.ConnectionManager = new ConnectionManager();
				subscriptionDB.ConnectionManager.WillDisconnectStorage();
				try
				{
					subscriptionDB.UpdateSubscriptionStatus(this.m_subscriptionID, this.m_subscriptionStatus);
					subscriptionDB.Transaction.Commit();
					return;
				}
				catch
				{
					subscriptionDB.Transaction.Rollback();
					throw;
				}
				finally
				{
					subscriptionDB.DisconnectStorage();
				}
			}
			if (Global.m_Tracer.TraceVerbose)
			{
				Global.m_Tracer.Trace("Data Driven Notification for activation id {0} was saved.", new object[] { this.m_activationID });
				Global.m_Tracer.Trace("Status: {0}", new object[] { this.m_subscriptionStatus });
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012E9C File Offset: 0x0001109C
		internal void UpdateResult()
		{
			SubscriptionDB subscriptionDB = new SubscriptionDB();
			subscriptionDB.ConnectionManager = new ConnectionManager();
			subscriptionDB.ConnectionManager.WillDisconnectStorage();
			try
			{
				subscriptionDB.UpdateSubscriptionResult(this.m_subscriptionID, this.m_extensionSettings, this.m_subscriptionResult);
				subscriptionDB.Transaction.Commit();
			}
			catch
			{
				subscriptionDB.Transaction.Rollback();
				throw;
			}
			finally
			{
				subscriptionDB.DisconnectStorage();
			}
		}

		// Token: 0x04000212 RID: 530
		internal ReportImpl m_report;

		// Token: 0x04000213 RID: 531
		internal Guid m_NotificationID = Guid.Empty;

		// Token: 0x04000214 RID: 532
		internal Guid m_subscriptionID = Guid.Empty;

		// Token: 0x04000215 RID: 533
		internal Guid m_activationID = Guid.Empty;

		// Token: 0x04000216 RID: 534
		private Guid m_reportID = Guid.Empty;

		// Token: 0x04000217 RID: 535
		internal DateTime m_snapShotDate = DateTime.MinValue;

		// Token: 0x04000218 RID: 536
		internal string m_deliveryExtension;

		// Token: 0x04000219 RID: 537
		internal ParameterValue[] m_extensionSettings;

		// Token: 0x0400021A RID: 538
		internal string m_parameters;

		// Token: 0x0400021B RID: 539
		internal string m_originalParameters;

		// Token: 0x0400021C RID: 540
		internal string m_owner;

		// Token: 0x0400021D RID: 541
		internal DateTime m_subscriptionLastRunTime = DateTime.MinValue;

		// Token: 0x0400021E RID: 542
		internal DateTime m_processStart = DateTime.MinValue;

		// Token: 0x0400021F RID: 543
		internal DateTime m_notificationEntered = DateTime.MinValue;

		// Token: 0x04000220 RID: 544
		internal int m_maxRetries;

		// Token: 0x04000221 RID: 545
		internal int m_attempt;

		// Token: 0x04000222 RID: 546
		internal ExternalItemPath m_path;

		// Token: 0x04000223 RID: 547
		internal int m_pathZone;

		// Token: 0x04000224 RID: 548
		internal ItemType m_reportType;

		// Token: 0x04000225 RID: 549
		internal byte[] m_secDesc;

		// Token: 0x04000226 RID: 550
		private bool m_isDataDriven;

		// Token: 0x04000227 RID: 551
		private bool m_statusChanged;

		// Token: 0x04000228 RID: 552
		private bool m_resultChanged;

		// Token: 0x04000229 RID: 553
		private bool m_retry = true;

		// Token: 0x0400022A RID: 554
		private string m_subscriptionResult = "";

		// Token: 0x0400022B RID: 555
		internal string m_subscriptionLocale = Localization.ClientPrimaryCulture.ToString();

		// Token: 0x0400022C RID: 556
		internal string m_subscriptionStatus = "";

		// Token: 0x0400022D RID: 557
		private int m_version;

		// Token: 0x0400022E RID: 558
		private AuthenticationType m_authType;

		// Token: 0x0400022F RID: 559
		private Setting[] m_settings;

		// Token: 0x02000442 RID: 1090
		private enum NotificationProjection
		{
			// Token: 0x04000F44 RID: 3908
			NotificationID,
			// Token: 0x04000F45 RID: 3909
			SubscriptionID,
			// Token: 0x04000F46 RID: 3910
			ActivationID,
			// Token: 0x04000F47 RID: 3911
			ReportID,
			// Token: 0x04000F48 RID: 3912
			SnapshotDate,
			// Token: 0x04000F49 RID: 3913
			DeliveryExtension,
			// Token: 0x04000F4A RID: 3914
			ExtensionSettings,
			// Token: 0x04000F4B RID: 3915
			Locale,
			// Token: 0x04000F4C RID: 3916
			Parameters,
			// Token: 0x04000F4D RID: 3917
			SubscriptionLastRunTime,
			// Token: 0x04000F4E RID: 3918
			ProcessStart,
			// Token: 0x04000F4F RID: 3919
			NotificationEntered,
			// Token: 0x04000F50 RID: 3920
			Attempt,
			// Token: 0x04000F51 RID: 3921
			IsDataDriven,
			// Token: 0x04000F52 RID: 3922
			SubscriptionOwnerNameBySid,
			// Token: 0x04000F53 RID: 3923
			SubscriptionOwnerNameBackup,
			// Token: 0x04000F54 RID: 3924
			Path,
			// Token: 0x04000F55 RID: 3925
			PathZone,
			// Token: 0x04000F56 RID: 3926
			ItemType,
			// Token: 0x04000F57 RID: 3927
			SecurityDescriptor,
			// Token: 0x04000F58 RID: 3928
			Version,
			// Token: 0x04000F59 RID: 3929
			AuthType,
			// Token: 0x04000F5A RID: 3930
			SubscriptionResult
		}
	}
}
