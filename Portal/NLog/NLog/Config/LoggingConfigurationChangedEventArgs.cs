using System;

namespace NLog.Config
{
	// Token: 0x0200018E RID: 398
	public class LoggingConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x0002F80B File Offset: 0x0002DA0B
		public LoggingConfigurationChangedEventArgs(LoggingConfiguration activatedConfiguration, LoggingConfiguration deactivatedConfiguration)
		{
			this.ActivatedConfiguration = activatedConfiguration;
			this.DeactivatedConfiguration = deactivatedConfiguration;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0002F821 File Offset: 0x0002DA21
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0002F829 File Offset: 0x0002DA29
		public LoggingConfiguration DeactivatedConfiguration { get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0002F832 File Offset: 0x0002DA32
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0002F83A File Offset: 0x0002DA3A
		public LoggingConfiguration ActivatedConfiguration { get; private set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0002F843 File Offset: 0x0002DA43
		[Obsolete("This option will be removed in NLog 5. Marked obsolete on NLog 4.5")]
		public LoggingConfiguration OldConfiguration
		{
			get
			{
				return this.ActivatedConfiguration;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0002F84B File Offset: 0x0002DA4B
		[Obsolete("This option will be removed in NLog 5. Marked obsolete on NLog 4.5")]
		public LoggingConfiguration NewConfiguration
		{
			get
			{
				return this.DeactivatedConfiguration;
			}
		}
	}
}
