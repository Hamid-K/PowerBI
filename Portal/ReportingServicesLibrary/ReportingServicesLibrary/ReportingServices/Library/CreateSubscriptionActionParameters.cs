using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000207 RID: 519
	internal sealed class CreateSubscriptionActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00041C9C File Offset: 0x0003FE9C
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x00041CA4 File Offset: 0x0003FEA4
		public string Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_report = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00041CBD File Offset: 0x0003FEBD
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00041CC5 File Offset: 0x0003FEC5
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00041CCE File Offset: 0x0003FECE
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00041CD6 File Offset: 0x0003FED6
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00041CDF File Offset: 0x0003FEDF
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00041CE7 File Offset: 0x0003FEE7
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00041CF0 File Offset: 0x0003FEF0
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00041CF8 File Offset: 0x0003FEF8
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00041D01 File Offset: 0x0003FF01
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00041D09 File Offset: 0x0003FF09
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

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00041D22 File Offset: 0x0003FF22
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00041D2A File Offset: 0x0003FF2A
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00041D43 File Offset: 0x0003FF43
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00041D4B File Offset: 0x0003FF4B
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00041D54 File Offset: 0x0003FF54
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00041D5C File Offset: 0x0003FF5C
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00041D75 File Offset: 0x0003FF75
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.SubscriptionID);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00041D8C File Offset: 0x0003FF8C
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					if (this.IsDataDriven)
					{
						text = string.Format(CultureInfo.InvariantCulture, "Report={0}, ExtensionSettings={1}, DataRetrievalPlan={2}, Description={3}, EventType={4}, MatchData={5}, Parameters={6}", new object[]
						{
							(this.Report != null) ? this.Report : "null",
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
						text = string.Format(CultureInfo.InvariantCulture, "Report={0}, ExtensionSettings={1}, Description={2}, EventType={3}, MatchData={4}, Parameters={5}", new object[]
						{
							(this.Report != null) ? this.Report : "null",
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

		// Token: 0x06001280 RID: 4736 RVA: 0x00041F3C File Offset: 0x0004013C
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.Report))
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
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
			if (this.IsDataDriven)
			{
				if (this.DataSettings == null)
				{
					throw new MissingElementException("DataRetrievalPlan");
				}
				if (this.DataSettings.Item == null)
				{
					throw new MissingElementException("DataSourceDefinitionOrReference");
				}
				if (this.DataSettings.DataSet == null)
				{
					throw new MissingElementException("DataSetDefinition");
				}
			}
			Subscription.CheckParameterArray(this.ExtensionSettings.ParameterValues, "ExtensionSettings");
			Subscription.CheckParameterArray(this.Parameters, "Parameters");
		}

		// Token: 0x0400069E RID: 1694
		private string m_report;

		// Token: 0x0400069F RID: 1695
		private ExtensionSettings m_extensionSettings;

		// Token: 0x040006A0 RID: 1696
		private bool m_isDataDriven;

		// Token: 0x040006A1 RID: 1697
		private DataRetrievalPlan m_dataSettings;

		// Token: 0x040006A2 RID: 1698
		private string m_description;

		// Token: 0x040006A3 RID: 1699
		private string m_eventType;

		// Token: 0x040006A4 RID: 1700
		private string m_matchData;

		// Token: 0x040006A5 RID: 1701
		private ParameterValueOrFieldReference[] m_parameters;

		// Token: 0x040006A6 RID: 1702
		private string m_subscriptionID = Guid.Empty.ToString();
	}
}
