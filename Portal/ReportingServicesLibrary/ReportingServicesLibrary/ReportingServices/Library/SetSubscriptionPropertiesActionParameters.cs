using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020F RID: 527
	internal sealed class SetSubscriptionPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00042818 File Offset: 0x00040A18
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00042820 File Offset: 0x00040A20
		public string SubscriptionID
		{
			get
			{
				return this.m_subscriptionID;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_subscriptionID = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00042839 File Offset: 0x00040A39
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00042841 File Offset: 0x00040A41
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				return this.m_extensionSettings;
			}
			set
			{
				this.m_extensionSettings = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004284A File Offset: 0x00040A4A
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00042852 File Offset: 0x00040A52
		public bool IsDataDriven
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

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0004285B File Offset: 0x00040A5B
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00042863 File Offset: 0x00040A63
		public DataRetrievalPlan DataSettings
		{
			get
			{
				return this.m_dataSettings;
			}
			set
			{
				this.m_dataSettings = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004286C File Offset: 0x00040A6C
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00042874 File Offset: 0x00040A74
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004287D File Offset: 0x00040A7D
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00042885 File Offset: 0x00040A85
		public string EventType
		{
			get
			{
				return this.m_eventType;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_eventType = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0004289E File Offset: 0x00040A9E
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x000428A6 File Offset: 0x00040AA6
		public string MatchData
		{
			get
			{
				return this.m_matchData;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_matchData = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000428BF File Offset: 0x00040ABF
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x000428C7 File Offset: 0x00040AC7
		public ParameterValueOrFieldReference[] Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000428D0 File Offset: 0x00040AD0
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					if (this.IsDataDriven)
					{
						text = string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}, ExtensionSettings={1}, DataRetrievalPlan={2}, Description={3}, EventType={4}, MatchData={5}, Parameters={6}", new object[]
						{
							(this.SubscriptionID != null) ? this.SubscriptionID : "null",
							(this.ExtensionSettings != null) ? ExtensionSettings.ThisToXml(this.ExtensionSettings) : "null",
							(this.DataSettings != null) ? DataRetrievalPlan.ThisToXml(this.DataSettings) : "null",
							(this.Description != null) ? this.Description : "null",
							(this.EventType != null) ? this.EventType : "null",
							(this.MatchData != null) ? this.MatchData : "null",
							(this.Parameters != null) ? ParameterValueOrFieldReference.ThisArrayToXml(this.Parameters) : "null"
						});
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}, ExtensionSettings={1}, Description={2}, EventType={3}, MatchData={4}, Parameters={5}", new object[]
						{
							(this.SubscriptionID != null) ? this.SubscriptionID : "null",
							(this.ExtensionSettings != null) ? ExtensionSettings.ThisToXml(this.ExtensionSettings) : "null",
							(this.Description != null) ? this.Description : "null",
							(this.EventType != null) ? this.EventType : "null",
							(this.MatchData != null) ? this.MatchData : "null",
							(this.Parameters != null) ? ParameterValueOrFieldReference.ThisArrayToXml(this.Parameters) : "null"
						});
					}
				}
				return text;
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00042A80 File Offset: 0x00040C80
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
			if (this.IsDataDriven)
			{
				if (this.DataSettings != null)
				{
					if (this.DataSettings.Item == null)
					{
						throw new MissingElementException("DataSourceDefinitionOrReference");
					}
					if (this.DataSettings.DataSet == null)
					{
						throw new MissingElementException("DataSetDefinition");
					}
					if (this.ExtensionSettings == null)
					{
						throw new MissingParameterException("ExtensionSettings");
					}
					if (this.Parameters == null)
					{
						throw new MissingParameterException("Parameters");
					}
					if (string.IsNullOrEmpty(this.EventType))
					{
						throw new MissingParameterException("EventType");
					}
					if ("TimedSubscription" != this.EventType && "SnapshotUpdated" != this.EventType)
					{
						throw new InvalidParameterException("EventType");
					}
				}
				else if (!string.IsNullOrEmpty(this.MatchData) && string.IsNullOrEmpty(this.EventType))
				{
					throw new MissingParameterException("EventType");
				}
			}
			else
			{
				if (this.ExtensionSettings == null)
				{
					throw new MissingParameterException("ExtensionSettings");
				}
				if (string.IsNullOrEmpty(this.EventType))
				{
					throw new MissingParameterException("EventType");
				}
				if ("TimedSubscription" != this.EventType && "SnapshotUpdated" != this.EventType)
				{
					throw new InvalidParameterException("EventType");
				}
			}
			if (this.ExtensionSettings != null)
			{
				Subscription.CheckParameterArray(this.ExtensionSettings.ParameterValues, "ExtensionSettings");
			}
			Subscription.CheckParameterArray(this.Parameters, "Parameters");
		}

		// Token: 0x040006AB RID: 1707
		private string m_subscriptionID = Guid.Empty.ToString();

		// Token: 0x040006AC RID: 1708
		private ExtensionSettings m_extensionSettings;

		// Token: 0x040006AD RID: 1709
		private bool m_isDataDriven;

		// Token: 0x040006AE RID: 1710
		private DataRetrievalPlan m_dataSettings;

		// Token: 0x040006AF RID: 1711
		private string m_description;

		// Token: 0x040006B0 RID: 1712
		private string m_eventType;

		// Token: 0x040006B1 RID: 1713
		private string m_matchData;

		// Token: 0x040006B2 RID: 1714
		private ParameterValueOrFieldReference[] m_parameters;
	}
}
