using System;
using System.Data;
using System.Xml;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200023B RID: 571
	internal sealed class SubscriptionImpl : Microsoft.ReportingServices.Extensions.Subscription
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00050812 File Offset: 0x0004EA12
		public override Guid ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0005081A File Offset: 0x0004EA1A
		public override UserContext Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00050822 File Offset: 0x0004EA22
		public override Guid ItemID
		{
			get
			{
				return this.m_itemtID;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0005082A File Offset: 0x0004EA2A
		public override string SubscriptionData
		{
			get
			{
				return this.m_matchData;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00050832 File Offset: 0x0004EA32
		public override string EventType
		{
			get
			{
				return this.m_eventType;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0005083A File Offset: 0x0004EA3A
		public override string ReportName
		{
			get
			{
				return this.m_itemName.Value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0005083A File Offset: 0x0004EA3A
		public string ItemName
		{
			get
			{
				return this.m_itemName.Value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00050847 File Offset: 0x0004EA47
		internal string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0005084F File Offset: 0x0004EA4F
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00050857 File Offset: 0x0004EA57
		internal UserContext ModifiedBy
		{
			get
			{
				return this.m_modifiedBy;
			}
			set
			{
				this.m_modifiedBy = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00050860 File Offset: 0x0004EA60
		internal DateTime ModifiedDate
		{
			get
			{
				return this.m_modifiedDate;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00050868 File Offset: 0x0004EA68
		public string Culture
		{
			get
			{
				return this.m_subscriptionCulture;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00050870 File Offset: 0x0004EA70
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00050887 File Offset: 0x0004EA87
		internal string DeliveryExtension
		{
			get
			{
				if (this.m_extensionSettings != null)
				{
					return this.m_extensionSettings.Extension;
				}
				return null;
			}
			set
			{
				if (this.m_extensionSettings != null)
				{
					this.m_extensionSettings.Extension = value;
				}
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0005089D File Offset: 0x0004EA9D
		internal ExtensionSettings ExtensionSettings
		{
			get
			{
				return this.m_extensionSettings;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000508A5 File Offset: 0x0004EAA5
		internal DateTime LastRunTime
		{
			get
			{
				return this.m_lastRunTime;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000508B0 File Offset: 0x0004EAB0
		internal ActiveState ActiveState
		{
			get
			{
				ActiveState activeState = new ActiveState();
				if (!this.IsActive())
				{
					if ((this.m_inactiveFlags & InActiveFlags.DeliveryProviderRemoved) > InActiveFlags.Active)
					{
						activeState.DeliveryExtensionRemoved = true;
						activeState.DeliveryExtensionRemovedSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.InvalidParameterValue) > InActiveFlags.Active)
					{
						activeState.InvalidParameterValue = true;
						activeState.InvalidParameterValueSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.MissingParameterValue) > InActiveFlags.Active)
					{
						activeState.MissingParameterValue = true;
						activeState.MissingParameterValueSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.SharedDataSourceRemoved) > InActiveFlags.Active)
					{
						activeState.SharedDataSourceRemoved = true;
						activeState.SharedDataSourceRemovedSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.UnknownItemParameter) > InActiveFlags.Active)
					{
						activeState.UnknownItemParameter = true;
						activeState.UnknownItemParameterSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.CachingNotEnabledOnItem) > InActiveFlags.Active)
					{
						activeState.CachingNotEnabledOnItem = true;
						activeState.CachingNotEnabledOnItemSpecified = true;
					}
					if ((this.m_inactiveFlags & InActiveFlags.DisabledByUser) > InActiveFlags.Active)
					{
						activeState.DisabledByUser = true;
						activeState.DisabledByUserSpecified = true;
					}
				}
				return activeState;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00050984 File Offset: 0x0004EB84
		internal bool HasParameters
		{
			get
			{
				return this.m_parameters != null;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0005098F File Offset: 0x0004EB8F
		internal string ItemNameOnly
		{
			get
			{
				return this.m_itemName.Value.Substring(this.m_itemName.Value.LastIndexOf("/", Localization.CatalogStringComparison) + 1);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x000509BD File Offset: 0x0004EBBD
		internal ExternalItemPath ItemPath
		{
			get
			{
				return this.m_itemName;
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x000509C8 File Offset: 0x0004EBC8
		internal void SetDataSettings(DataRetrievalPlan dataSetting)
		{
			if (dataSetting == null)
			{
				this.m_dataSet = null;
				this.m_dataSource = null;
				return;
			}
			this.m_dataSet = dataSetting.DataSet;
			try
			{
				this.m_dataSource = DataSourceDefinitionOrReference.ThisToDataSourceInfo(dataSetting.Item, null);
			}
			catch (Exception ex)
			{
				throw new InvalidParameterException("dataSettings", ex);
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00050A24 File Offset: 0x0004EC24
		internal DataRetrievalPlan GetDataSettings()
		{
			DataRetrievalPlan dataRetrievalPlan = null;
			if (this.m_dataSet != null)
			{
				dataRetrievalPlan = new DataRetrievalPlan();
				dataRetrievalPlan.DataSet = this.m_dataSet;
				string text;
				dataRetrievalPlan.Item = DataSourceDefinitionOrReference.DataSourceInfoToThis(this.m_dataSource, out text, false, false);
			}
			return dataRetrievalPlan;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00050A64 File Offset: 0x0004EC64
		public SubscriptionImpl(ExternalItemPath reportName, string eventType, Guid id, IRSService service)
		{
			if (id == Guid.Empty)
			{
				this.m_id = Guid.NewGuid();
			}
			else
			{
				this.m_id = id;
			}
			this.m_modifiedDate = DateTime.Now;
			this.m_itemName = reportName;
			this.m_itemZone = service.GetExternalRootZone(reportName);
			this.m_eventType = eventType;
			this.m_owner = service.UserContext;
			this.m_modifiedBy = service.UserContext;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00050B4C File Offset: 0x0004ED4C
		public SubscriptionImpl(IDataRecord record, int indexStart, IPathTranslator pathTranslator)
		{
			try
			{
				string text = null;
				this.m_id = record.GetGuid(indexStart);
				this.m_subscriptionCulture = record.GetString(3 + indexStart);
				this.m_itemtID = record.GetGuid(1 + indexStart);
				this.m_inactiveFlags = (InActiveFlags)record.GetInt32(4 + indexStart);
				if (!record.IsDBNull(5 + indexStart))
				{
					text = record.GetString(5 + indexStart);
				}
				if (!record.IsDBNull(6 + indexStart))
				{
					this.m_extensionSettings = new ExtensionSettings();
					this.DeliveryExtension = text;
					this.m_extensionSettings.ParameterValues = ParameterValueOrFieldReference.XmlToThisArray(record.GetString(6 + indexStart), false);
				}
				string userNameBySid = UserUtil.GetUserNameBySid(record, 7 + indexStart, 8 + indexStart);
				this.m_modifiedDate = record.GetDateTime(9 + indexStart);
				if (!record.IsDBNull(10 + indexStart))
				{
					this.m_description = record.GetString(10 + indexStart);
				}
				if (!record.IsDBNull(11 + indexStart))
				{
					this.m_lastStatus = record.GetString(11 + indexStart);
				}
				this.m_eventType = record.GetString(12 + indexStart);
				if (!record.IsDBNull(13 + indexStart))
				{
					this.m_matchData = record.GetString(13 + indexStart);
				}
				if (!record.IsDBNull(15 + indexStart))
				{
					this.m_dataSet = Microsoft.ReportingServices.Library.Soap.DataSetDefinition.XmlToThis(record.GetString(15 + indexStart));
				}
				if (!record.IsDBNull(14 + indexStart))
				{
					this.m_parameters = ParameterValueOrFieldReference.XmlToThisArray(record.GetString(14 + indexStart), !this.IsDataDriven());
				}
				if (!record.IsDBNull(16 + indexStart))
				{
					this.m_hasActiveSubscriptions = true;
					this.m_totalNotifications = record.GetInt32(16 + indexStart);
					if (!record.IsDBNull(17 + indexStart))
					{
						this.m_totalSuccess = record.GetInt32(17 + indexStart);
					}
					if (!record.IsDBNull(18 + indexStart))
					{
						this.m_totalFailures = record.GetInt32(18 + indexStart);
					}
				}
				string userNameBySid2 = UserUtil.GetUserNameBySid(record, 19 + indexStart, 20 + indexStart);
				if (!record.IsDBNull(21 + indexStart))
				{
					CatalogItemPath catalogItemPath = new CatalogItemPath(record.GetString(21 + indexStart));
					int @int = record.GetInt32(2 + indexStart);
					this.m_itemName = pathTranslator.CatalogToExternal(catalogItemPath, @int);
				}
				if (!record.IsDBNull(22 + indexStart))
				{
					this.m_lastRunTime = record.GetDateTime(22 + indexStart);
				}
				if (!record.IsDBNull(23 + indexStart))
				{
					this.m_itemType = (ItemType)record.GetInt32(23 + indexStart);
				}
				if (!record.IsDBNull(24 + indexStart))
				{
					this.m_securityDesc = DataReaderHelper.ReadAllBytes(record, 24 + indexStart);
				}
				if (Localization.CatalogCultureCompare(this.m_eventType, "TimedSubscription") == 0 || Localization.CatalogCultureCompare(this.m_eventType, "RefreshCache") == 0)
				{
					this.m_matchData = Microsoft.ReportingServices.Diagnostics.Task.EnsureValidScheduleXml(this.m_matchData);
				}
				this.m_version = record.GetInt32(25);
				AuthenticationType int2 = (AuthenticationType)record.GetInt32(26);
				this.m_owner = new UserContext(userNameBySid2, null, int2);
				this.m_modifiedBy = new UserContext(userNameBySid, null, int2);
			}
			catch (XmlException ex)
			{
				throw new InvalidSubscriptionException(this.ID, ex);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00050EB4 File Offset: 0x0004F0B4
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x00050EEB File Offset: 0x0004F0EB
		internal string LastStatus
		{
			get
			{
				if (this.IsDataDriven() && this.m_hasActiveSubscriptions)
				{
					return RepLibRes.SubscriptionProcessing(this.m_totalFailures + this.m_totalSuccess, this.m_totalNotifications, this.m_totalFailures);
				}
				return this.m_lastStatus;
			}
			set
			{
				this.m_lastStatus = value;
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		public bool IsActive()
		{
			return this.m_inactiveFlags == InActiveFlags.Active;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00050F01 File Offset: 0x0004F101
		public bool IsDataDriven()
		{
			return this.m_dataSet != null;
		}

		// Token: 0x0400076F RID: 1903
		internal Guid m_id = Guid.Empty;

		// Token: 0x04000770 RID: 1904
		internal string m_description = "";

		// Token: 0x04000771 RID: 1905
		internal Guid m_itemtID = Guid.Empty;

		// Token: 0x04000772 RID: 1906
		internal ExtensionSettings m_extensionSettings;

		// Token: 0x04000773 RID: 1907
		internal InActiveFlags m_inactiveFlags;

		// Token: 0x04000774 RID: 1908
		internal DateTime m_modifiedDate = DateTime.MinValue;

		// Token: 0x04000775 RID: 1909
		internal string m_matchData;

		// Token: 0x04000776 RID: 1910
		internal string m_eventType = "";

		// Token: 0x04000777 RID: 1911
		internal ExternalItemPath m_itemName = ExternalItemPath.Empty;

		// Token: 0x04000778 RID: 1912
		internal DateTime m_lastRunTime = DateTime.MinValue;

		// Token: 0x04000779 RID: 1913
		internal ItemType m_itemType;

		// Token: 0x0400077A RID: 1914
		internal byte[] m_securityDesc;

		// Token: 0x0400077B RID: 1915
		internal ParameterValueOrFieldReference[] m_parameters;

		// Token: 0x0400077C RID: 1916
		internal Microsoft.ReportingServices.Library.Soap.DataSetDefinition m_dataSet;

		// Token: 0x0400077D RID: 1917
		internal int m_itemZone;

		// Token: 0x0400077E RID: 1918
		private string m_lastStatus = "";

		// Token: 0x0400077F RID: 1919
		private bool m_hasActiveSubscriptions;

		// Token: 0x04000780 RID: 1920
		private int m_totalNotifications;

		// Token: 0x04000781 RID: 1921
		private int m_totalSuccess;

		// Token: 0x04000782 RID: 1922
		private int m_totalFailures;

		// Token: 0x04000783 RID: 1923
		internal DataSourceInfo m_dataSource;

		// Token: 0x04000784 RID: 1924
		internal string m_subscriptionCulture = Localization.ClientPrimaryCulture.ToString();

		// Token: 0x04000785 RID: 1925
		internal int m_version = Encryption.CurrentVersion;

		// Token: 0x04000786 RID: 1926
		internal UserContext m_owner;

		// Token: 0x04000787 RID: 1927
		private UserContext m_modifiedBy;

		// Token: 0x04000788 RID: 1928
		internal ConnectionManager m_connectionManager;

		// Token: 0x020004B1 RID: 1201
		private enum SubscriptionColumns
		{
			// Token: 0x040010A7 RID: 4263
			id,
			// Token: 0x040010A8 RID: 4264
			ReportID,
			// Token: 0x040010A9 RID: 4265
			ReportZone,
			// Token: 0x040010AA RID: 4266
			Locale,
			// Token: 0x040010AB RID: 4267
			InactiveFlags,
			// Token: 0x040010AC RID: 4268
			DeliveryExtension,
			// Token: 0x040010AD RID: 4269
			ExtensionSettings,
			// Token: 0x040010AE RID: 4270
			ModifiedByUserNameSid,
			// Token: 0x040010AF RID: 4271
			ModifiedByUserNameBackup,
			// Token: 0x040010B0 RID: 4272
			ModifiedDate,
			// Token: 0x040010B1 RID: 4273
			Description,
			// Token: 0x040010B2 RID: 4274
			LastStatus,
			// Token: 0x040010B3 RID: 4275
			EventType,
			// Token: 0x040010B4 RID: 4276
			MatchData,
			// Token: 0x040010B5 RID: 4277
			Parameters,
			// Token: 0x040010B6 RID: 4278
			DataSettings,
			// Token: 0x040010B7 RID: 4279
			TotalNotifications,
			// Token: 0x040010B8 RID: 4280
			TotalSuccesses,
			// Token: 0x040010B9 RID: 4281
			TotalFailures,
			// Token: 0x040010BA RID: 4282
			OwnerBySid,
			// Token: 0x040010BB RID: 4283
			OwnerBackup,
			// Token: 0x040010BC RID: 4284
			ItemName,
			// Token: 0x040010BD RID: 4285
			LastRun,
			// Token: 0x040010BE RID: 4286
			ReportType,
			// Token: 0x040010BF RID: 4287
			SecurityDescriptor,
			// Token: 0x040010C0 RID: 4288
			Version,
			// Token: 0x040010C1 RID: 4289
			AuthType
		}
	}
}
