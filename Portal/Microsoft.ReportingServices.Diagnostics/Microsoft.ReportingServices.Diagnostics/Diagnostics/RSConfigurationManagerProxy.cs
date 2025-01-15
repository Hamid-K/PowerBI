using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003B RID: 59
	internal class RSConfigurationManagerProxy : RSConfigurationManager
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00007FC8 File Offset: 0x000061C8
		internal static void ThrowInvalidFormat(string element)
		{
			throw new Exception(ErrorStringsWrapper.InvalidConfigElement(element));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007FD5 File Offset: 0x000061D5
		internal static void ThrowElementMissing(string element)
		{
			throw new Exception(ErrorStringsWrapper.CouldNotFindElement(element));
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001DA RID: 474 RVA: 0x00007FE4 File Offset: 0x000061E4
		// (remove) Token: 0x060001DB RID: 475 RVA: 0x0000801C File Offset: 0x0000621C
		public override event ConfigurationChangeEventHandler Changed;

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00008051 File Offset: 0x00006251
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00008059 File Offset: 0x00006259
		public override bool EnableRaisingEvents
		{
			get
			{
				return this.m_raiseEvents;
			}
			set
			{
				this.m_raiseEvents = value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008064 File Offset: 0x00006264
		public override void ChangeConfiguration(ConfigurationPropertyBag properties, bool resetProperties, RSConfigurationManager source)
		{
			this.m_config.Load(properties, resetProperties);
			try
			{
				this.m_parent.ChangeConfiguration(properties, resetProperties, source);
			}
			catch (AppDomainUnloadedException)
			{
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000080A4 File Offset: 0x000062A4
		public RSConfigurationManagerProxy(RSConfigurationManager parent)
		{
			this.m_parent = parent;
			try
			{
				this.m_config = parent.GetConfiguration();
			}
			catch (AppDomainUnloadedException)
			{
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000080E8 File Offset: 0x000062E8
		private void OnChanged(object sender, ConfigurationChangeEventArgs e)
		{
			if (sender != this)
			{
				this.m_config.Load(e.Properties, e.ResetProperties);
			}
			if (this.m_raiseEvents && this.Changed != null)
			{
				this.Changed(this, e);
			}
		}

		// Token: 0x040001E6 RID: 486
		private RSConfigurationManager m_parent;

		// Token: 0x040001E7 RID: 487
		private bool m_raiseEvents = true;
	}
}
