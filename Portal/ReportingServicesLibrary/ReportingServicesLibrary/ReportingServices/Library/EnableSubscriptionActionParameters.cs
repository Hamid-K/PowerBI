using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020B RID: 523
	internal sealed class EnableSubscriptionActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000425C3 File Offset: 0x000407C3
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x000425CB File Offset: 0x000407CB
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

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x000425E4 File Offset: 0x000407E4
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}", this.SubscriptionID);
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000425FB File Offset: 0x000407FB
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
		}

		// Token: 0x040006A8 RID: 1704
		private string m_subscriptionID = Guid.Empty.ToString();
	}
}
