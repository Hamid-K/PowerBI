using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000072 RID: 114
	public sealed class LocationContext
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000FE7D File Offset: 0x0000E07D
		internal LocationContext()
		{
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000FE85 File Offset: 0x0000E085
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000FE9C File Offset: 0x0000E09C
		public string Ip
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ip))
				{
					return this.ip;
				}
				return null;
			}
			set
			{
				this.ip = value;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FEA5 File Offset: 0x0000E0A5
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.LocationIp, this.Ip);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000FEBD File Offset: 0x0000E0BD
		internal void CopyTo(LocationContext target)
		{
			Tags.CopyTagValue(this.Ip, ref target.ip);
		}

		// Token: 0x0400016E RID: 366
		private string ip;
	}
}
