using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B8 RID: 440
	internal sealed class GetSnapshotLimitActionParameters : SnapshotLimitActionParameters
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000381B4 File Offset: 0x000363B4
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000381BC File Offset: 0x000363BC
		public int SystemLimit
		{
			get
			{
				return this.m_systemLimit;
			}
			set
			{
				this.m_systemLimit = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000381C5 File Offset: 0x000363C5
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ScopedLimit, base.UseSystem, this.SystemLimit);
			}
		}

		// Token: 0x0400063A RID: 1594
		private int m_systemLimit;
	}
}
