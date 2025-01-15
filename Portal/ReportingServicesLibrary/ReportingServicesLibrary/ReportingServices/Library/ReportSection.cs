using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200016F RID: 367
	internal sealed class ReportSection
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003159F File Offset: 0x0002F79F
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00031596 File Offset: 0x0002F796
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000315B0 File Offset: 0x0002F7B0
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x000315A7 File Offset: 0x0002F7A7
		internal string PreviewId
		{
			get
			{
				return this.m_previewId;
			}
			set
			{
				this.m_previewId = value;
			}
		}

		// Token: 0x0400059A RID: 1434
		private string m_name;

		// Token: 0x0400059B RID: 1435
		private string m_previewId;
	}
}
