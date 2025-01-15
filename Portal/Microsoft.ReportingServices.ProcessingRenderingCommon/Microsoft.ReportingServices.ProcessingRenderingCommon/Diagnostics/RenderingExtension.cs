using System;
using System.Collections.Specialized;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal sealed class RenderingExtension : Extension
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00007788 File Offset: 0x00005988
		public RenderingExtension()
		{
			this.m_overrideNames = new NameValueCollection();
			this.m_deviceInfo = new NameValueCollection();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000077A8 File Offset: 0x000059A8
		public string GetOverrideName()
		{
			if (this.m_overrideNames.Count <= 0)
			{
				return null;
			}
			string text = this.m_overrideNames[0];
			string text2 = this.m_overrideNames.Get(Thread.CurrentThread.CurrentUICulture.Name);
			if (text2 != null)
			{
				text = text2;
			}
			return text;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000077F3 File Offset: 0x000059F3
		internal NameValueCollection OverrideNames
		{
			get
			{
				return this.m_overrideNames;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000077FB File Offset: 0x000059FB
		internal NameValueCollection DeviceInfo
		{
			get
			{
				return this.m_deviceInfo;
			}
		}

		// Token: 0x040000EC RID: 236
		private NameValueCollection m_overrideNames;

		// Token: 0x040000ED RID: 237
		private NameValueCollection m_deviceInfo;
	}
}
