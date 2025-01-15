using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200002F RID: 47
	public class MessageWriterSettingsArgs
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00008086 File Offset: 0x00006286
		public MessageWriterSettingsArgs(ODataMessageWriterSettings settings)
		{
			WebUtil.CheckArgumentNull<ODataMessageWriterSettings>(settings, "settings");
			this.Settings = settings;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000080A1 File Offset: 0x000062A1
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000080A9 File Offset: 0x000062A9
		public ODataMessageWriterSettings Settings { get; private set; }
	}
}
