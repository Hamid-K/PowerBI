using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AA RID: 6570
	public class CurrentTimeService : ICurrentTimeService
	{
		// Token: 0x0600A688 RID: 42632 RVA: 0x0022727C File Offset: 0x0022547C
		public CurrentTimeService(DateTime? baseUtcNow = null)
		{
			this.baseUtcNow = baseUtcNow;
			if (baseUtcNow == null)
			{
				this.fixedUtcNow = DateTime.UtcNow;
				this.nowOffset = TimeSpan.Zero;
				return;
			}
			this.fixedUtcNow = baseUtcNow.Value;
			this.nowOffset = baseUtcNow.Value - DateTime.UtcNow;
		}

		// Token: 0x17002A7B RID: 10875
		// (get) Token: 0x0600A689 RID: 42633 RVA: 0x002272DA File Offset: 0x002254DA
		public DateTime FixedUtcNow
		{
			get
			{
				return this.fixedUtcNow;
			}
		}

		// Token: 0x17002A7C RID: 10876
		// (get) Token: 0x0600A68A RID: 42634 RVA: 0x002272E2 File Offset: 0x002254E2
		public DateTime UtcNow
		{
			get
			{
				return DateTime.UtcNow + this.nowOffset;
			}
		}

		// Token: 0x17002A7D RID: 10877
		// (get) Token: 0x0600A68B RID: 42635 RVA: 0x002272F4 File Offset: 0x002254F4
		public DateTime? BaseUtcNow
		{
			get
			{
				return this.baseUtcNow;
			}
		}

		// Token: 0x040056A8 RID: 22184
		private readonly DateTime fixedUtcNow;

		// Token: 0x040056A9 RID: 22185
		private readonly DateTime? baseUtcNow;

		// Token: 0x040056AA RID: 22186
		private readonly TimeSpan nowOffset;
	}
}
