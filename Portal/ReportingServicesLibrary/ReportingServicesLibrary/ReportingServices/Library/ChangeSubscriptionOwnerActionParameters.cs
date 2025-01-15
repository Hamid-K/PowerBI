using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000213 RID: 531
	internal sealed class ChangeSubscriptionOwnerActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004316E File Offset: 0x0004136E
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00043176 File Offset: 0x00041376
		public string SubscriptionID
		{
			[DebuggerStepThrough]
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0004318F File Offset: 0x0004138F
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x00043197 File Offset: 0x00041397
		public string NewOwner
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_newOwner;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_newOwner = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000431B0 File Offset: 0x000413B0
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "SubscriptionID={0}, NewOwner={1}", (this.SubscriptionID != null) ? this.SubscriptionID : "null", (this.NewOwner != null) ? this.NewOwner : "null");
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000431EB File Offset: 0x000413EB
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
			if (string.IsNullOrEmpty(this.NewOwner))
			{
				throw new MissingParameterException("NewOwner");
			}
		}

		// Token: 0x040006BE RID: 1726
		private string m_subscriptionID = Guid.Empty.ToString();

		// Token: 0x040006BF RID: 1727
		private string m_newOwner;
	}
}
