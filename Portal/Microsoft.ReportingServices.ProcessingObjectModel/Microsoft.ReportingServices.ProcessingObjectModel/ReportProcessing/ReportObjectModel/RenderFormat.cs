using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200001C RID: 28
	public sealed class RenderFormat : MarshalByRefObject
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002120 File Offset: 0x00000320
		internal RenderFormat(RenderFormatImplBase renderFormatImpl)
		{
			this.m_renderFormatImpl = renderFormatImpl;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000212F File Offset: 0x0000032F
		public string Name
		{
			get
			{
				return this.m_renderFormatImpl.Name;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000213C File Offset: 0x0000033C
		public bool IsInteractive
		{
			get
			{
				return this.m_renderFormatImpl.IsInteractive;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002149 File Offset: 0x00000349
		public ReadOnlyNameValueCollection DeviceInfo
		{
			get
			{
				return this.m_renderFormatImpl.DeviceInfo;
			}
		}

		// Token: 0x04000001 RID: 1
		private RenderFormatImplBase m_renderFormatImpl;
	}
}
