using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AC RID: 684
	internal sealed class ExtendedProperty
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x0006A030 File Offset: 0x00068230
		internal ExtendedProperty(string name, object value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0006A046 File Offset: 0x00068246
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0006A04E File Offset: 0x0006824E
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0006A056 File Offset: 0x00068256
		internal void UpdateValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x04000D12 RID: 3346
		private string m_name;

		// Token: 0x04000D13 RID: 3347
		private object m_value;
	}
}
