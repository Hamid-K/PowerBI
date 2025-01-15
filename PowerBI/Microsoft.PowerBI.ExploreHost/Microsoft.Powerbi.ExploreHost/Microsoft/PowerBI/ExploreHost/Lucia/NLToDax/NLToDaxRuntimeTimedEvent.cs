using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000076 RID: 118
	internal class NLToDaxRuntimeTimedEvent : BaseTelemetryEvent
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000A6A1 File Offset: 0x000088A1
		public NLToDaxRuntimeTimedEvent(string name, bool isError)
			: base("PBI.NLToDax." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>();
			this.IsError = isError;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000A6C7 File Offset: 0x000088C7
		public NLToDaxRuntimeTimedEvent(string name, string message)
			: base("PBI.NLToDax." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>();
			this.Message = message;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A6ED File Offset: 0x000088ED
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000A6F5 File Offset: 0x000088F5
		public bool IsError
		{
			get
			{
				return this.m_isError;
			}
			set
			{
				this.m_isError = value;
				this.m_properties["isError"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A71A File Offset: 0x0000891A
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000A722 File Offset: 0x00008922
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
				this.m_properties["message"] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A73C File Offset: 0x0000893C
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000A744 File Offset: 0x00008944
		public long Duration
		{
			get
			{
				return this.m_duration;
			}
			set
			{
				this.m_duration = value;
				this.m_properties["duration"] = value.ToStringInvariant();
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A763 File Offset: 0x00008963
		public override Dictionary<string, string> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x04000173 RID: 371
		private const string NamePrefix = "PBI.NLToDax.";

		// Token: 0x04000174 RID: 372
		private readonly Dictionary<string, string> m_properties;

		// Token: 0x04000175 RID: 373
		private bool m_isError;

		// Token: 0x04000176 RID: 374
		private string m_message;

		// Token: 0x04000177 RID: 375
		private long m_duration;
	}
}
