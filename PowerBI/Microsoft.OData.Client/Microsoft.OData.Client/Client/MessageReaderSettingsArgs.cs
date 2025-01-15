using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200002E RID: 46
	public class MessageReaderSettingsArgs
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000805A File Offset: 0x0000625A
		public MessageReaderSettingsArgs(ODataMessageReaderSettings settings)
		{
			WebUtil.CheckArgumentNull<ODataMessageReaderSettings>(settings, "settings");
			this.Settings = settings;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008075 File Offset: 0x00006275
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000807D File Offset: 0x0000627D
		public ODataMessageReaderSettings Settings { get; private set; }
	}
}
