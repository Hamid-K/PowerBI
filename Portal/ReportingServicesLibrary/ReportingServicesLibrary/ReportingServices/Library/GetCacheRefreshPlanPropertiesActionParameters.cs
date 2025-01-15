using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000219 RID: 537
	internal sealed class GetCacheRefreshPlanPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00043892 File Offset: 0x00041A92
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0004389A File Offset: 0x00041A9A
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x000438B3 File Offset: 0x00041AB3
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x000438BB File Offset: 0x00041ABB
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

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000438C4 File Offset: 0x00041AC4
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x000438CC File Offset: 0x00041ACC
		public ActiveState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000438D5 File Offset: 0x00041AD5
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000438DD File Offset: 0x00041ADD
		public string Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000438E6 File Offset: 0x00041AE6
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x000438EE File Offset: 0x00041AEE
		public string EventType
		{
			get
			{
				return this.m_eventType;
			}
			set
			{
				this.m_eventType = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x000438F7 File Offset: 0x00041AF7
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x000438FF File Offset: 0x00041AFF
		public string MatchData
		{
			get
			{
				return this.m_matchData;
			}
			set
			{
				this.m_matchData = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00043908 File Offset: 0x00041B08
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00043910 File Offset: 0x00041B10
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

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00043919 File Offset: 0x00041B19
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.CacheRefreshPlanID);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00043930 File Offset: 0x00041B30
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "ID {0}", this.CacheRefreshPlanID);
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00043947 File Offset: 0x00041B47
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.CacheRefreshPlanID))
			{
				throw new MissingParameterException("CacheRefreshPlanID");
			}
		}

		// Token: 0x040006CF RID: 1743
		private string m_cacheRefreshPlanID = Guid.Empty.ToString();

		// Token: 0x040006D0 RID: 1744
		private string m_description;

		// Token: 0x040006D1 RID: 1745
		private ActiveState m_state;

		// Token: 0x040006D2 RID: 1746
		private string m_status;

		// Token: 0x040006D3 RID: 1747
		private string m_eventType;

		// Token: 0x040006D4 RID: 1748
		private string m_matchData;

		// Token: 0x040006D5 RID: 1749
		private ParameterValueOrFieldReference[] m_parameters;
	}
}
