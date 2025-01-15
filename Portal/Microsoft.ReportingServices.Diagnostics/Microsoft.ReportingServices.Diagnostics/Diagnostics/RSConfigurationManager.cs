using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003A RID: 58
	internal abstract class RSConfigurationManager : MarshalByRefObject
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00004F46 File Offset: 0x00003146
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001D0 RID: 464
		// (remove) Token: 0x060001D1 RID: 465
		public abstract event ConfigurationChangeEventHandler Changed;

		// Token: 0x060001D2 RID: 466 RVA: 0x00007FAD File Offset: 0x000061AD
		public RSConfiguration GetConfiguration()
		{
			return this.m_config;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060001D3 RID: 467
		// (set) Token: 0x060001D4 RID: 468
		public abstract bool EnableRaisingEvents { get; set; }

		// Token: 0x060001D5 RID: 469 RVA: 0x00007FB5 File Offset: 0x000061B5
		public void ChangeConfiguration(ConfigurationPropertyBag properties, bool resetProperties)
		{
			this.ChangeConfiguration(properties, resetProperties, this);
		}

		// Token: 0x060001D6 RID: 470
		public abstract void ChangeConfiguration(ConfigurationPropertyBag properties, bool resetProperties, RSConfigurationManager source);

		// Token: 0x040001E5 RID: 485
		protected RSConfiguration m_config;
	}
}
