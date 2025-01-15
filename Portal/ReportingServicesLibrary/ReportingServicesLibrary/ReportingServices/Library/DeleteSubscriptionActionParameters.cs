using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020D RID: 525
	internal sealed class DeleteSubscriptionActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x000426DF File Offset: 0x000408DF
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x000426E7 File Offset: 0x000408E7
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

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00042700 File Offset: 0x00040900
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x00042708 File Offset: 0x00040908
		public bool IsCacheRefreshPlanExpected
		{
			get
			{
				return this.m_isCacheRefreshPlan;
			}
			set
			{
				this.m_isCacheRefreshPlan = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00042711 File Offset: 0x00040911
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}", this.SubscriptionID);
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00042728 File Offset: 0x00040928
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
		}

		// Token: 0x040006A9 RID: 1705
		private string m_subscriptionID = Guid.Empty.ToString();

		// Token: 0x040006AA RID: 1706
		private bool m_isCacheRefreshPlan;
	}
}
