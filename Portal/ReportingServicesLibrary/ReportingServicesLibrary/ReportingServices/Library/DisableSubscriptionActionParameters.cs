using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000209 RID: 521
	internal sealed class DisableSubscriptionActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x000424A7 File Offset: 0x000406A7
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x000424AF File Offset: 0x000406AF
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

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x000424C8 File Offset: 0x000406C8
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}", this.SubscriptionID);
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000424DF File Offset: 0x000406DF
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
		}

		// Token: 0x040006A7 RID: 1703
		private string m_subscriptionID = Guid.Empty.ToString();
	}
}
