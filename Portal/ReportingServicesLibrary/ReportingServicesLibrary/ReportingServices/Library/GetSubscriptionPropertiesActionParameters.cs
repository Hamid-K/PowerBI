using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000211 RID: 529
	internal sealed class GetSubscriptionPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00042F57 File Offset: 0x00041157
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00042F5F File Offset: 0x0004115F
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

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00042F78 File Offset: 0x00041178
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00042F80 File Offset: 0x00041180
		public bool LookingForDataDriven
		{
			get
			{
				return this.m_lookingForDataDriven;
			}
			set
			{
				this.m_lookingForDataDriven = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00042F89 File Offset: 0x00041189
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00042F91 File Offset: 0x00041191
		public string Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00042F9A File Offset: 0x0004119A
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00042FA2 File Offset: 0x000411A2
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00042FAB File Offset: 0x000411AB
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x00042FB3 File Offset: 0x000411B3
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00042FBC File Offset: 0x000411BC
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00042FC4 File Offset: 0x000411C4
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00042FCD File Offset: 0x000411CD
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00042FD5 File Offset: 0x000411D5
		public ActiveState Active
		{
			get
			{
				return this.m_active;
			}
			set
			{
				this.m_active = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00042FDE File Offset: 0x000411DE
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00042FE6 File Offset: 0x000411E6
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00042FEF File Offset: 0x000411EF
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00042FF7 File Offset: 0x000411F7
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

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00043000 File Offset: 0x00041200
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00043008 File Offset: 0x00041208
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

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00043011 File Offset: 0x00041211
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00043019 File Offset: 0x00041219
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00043022 File Offset: 0x00041222
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.SubscriptionID);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00043039 File Offset: 0x00041239
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "ID {0}", this.SubscriptionID);
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00043050 File Offset: 0x00041250
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SubscriptionID))
			{
				throw new MissingParameterException("SubscriptionID");
			}
		}

		// Token: 0x040006B3 RID: 1715
		private string m_subscriptionID = Guid.Empty.ToString();

		// Token: 0x040006B4 RID: 1716
		private bool m_lookingForDataDriven;

		// Token: 0x040006B5 RID: 1717
		private string m_owner;

		// Token: 0x040006B6 RID: 1718
		private ExtensionSettings m_extensionSettings;

		// Token: 0x040006B7 RID: 1719
		private DataRetrievalPlan m_dataSettings;

		// Token: 0x040006B8 RID: 1720
		private string m_description;

		// Token: 0x040006B9 RID: 1721
		private ActiveState m_active;

		// Token: 0x040006BA RID: 1722
		private string m_status;

		// Token: 0x040006BB RID: 1723
		private string m_eventType;

		// Token: 0x040006BC RID: 1724
		private string m_matchData;

		// Token: 0x040006BD RID: 1725
		private ParameterValueOrFieldReference[] m_parameters;
	}
}
