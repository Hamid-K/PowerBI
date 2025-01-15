using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000217 RID: 535
	internal sealed class SetCacheRefreshPlanPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004362F File Offset: 0x0004182F
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00043637 File Offset: 0x00041837
		public string CacheRefreshPlanID
		{
			get
			{
				return this.m_cacheRefreshPlanID;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_cacheRefreshPlanID = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00043650 File Offset: 0x00041850
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00043658 File Offset: 0x00041858
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

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00043661 File Offset: 0x00041861
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00043677 File Offset: 0x00041877
		public string EventType
		{
			get
			{
				if (this.m_eventType != null)
				{
					return this.m_eventType;
				}
				return "RefreshCache";
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

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00043690 File Offset: 0x00041890
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00043698 File Offset: 0x00041898
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x000436B1 File Offset: 0x000418B1
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x000436B9 File Offset: 0x000418B9
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

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x000436C4 File Offset: 0x000418C4
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					text = string.Format(CultureInfo.InvariantCulture, "CacheRefreshPlanID={0}, Description={1}, EventType={2}, MatchData={3}, Parameters={4}", new object[]
					{
						(this.CacheRefreshPlanID != null) ? this.CacheRefreshPlanID : "null",
						(this.Description != null) ? this.Description : "null",
						(this.EventType != null) ? this.EventType : "null",
						(this.MatchData != null) ? this.MatchData : "null",
						(this.Parameters != null) ? ParameterValueOrFieldReference.ThisArrayToXml(this.Parameters) : "null"
					});
				}
				return text;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004377C File Offset: 0x0004197C
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.CacheRefreshPlanID))
			{
				throw new MissingParameterException("CacheRefreshPlanID");
			}
			if (string.IsNullOrEmpty(this.EventType))
			{
				throw new MissingParameterException("EventType");
			}
			if ("RefreshCache" != this.EventType && !this.EventType.Equals("DataModelRefresh"))
			{
				throw new InvalidParameterException("EventType");
			}
			Subscription.CheckParameterArray(this.Parameters, "Parameters");
		}

		// Token: 0x040006C9 RID: 1737
		private string m_cacheRefreshPlanID = Guid.Empty.ToString();

		// Token: 0x040006CA RID: 1738
		private string m_description;

		// Token: 0x040006CB RID: 1739
		private const string _RefreshCacheEventType = "RefreshCache";

		// Token: 0x040006CC RID: 1740
		private string m_eventType;

		// Token: 0x040006CD RID: 1741
		private string m_matchData;

		// Token: 0x040006CE RID: 1742
		private ParameterValueOrFieldReference[] m_parameters;
	}
}
