using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	public enum BindingType
	{
		// Token: 0x04000C79 RID: 3193
		Tcp,
		// Token: 0x04000C7A RID: 3194
		BasicHttp,
		// Token: 0x04000C7B RID: 3195
		WsHttp,
		// Token: 0x04000C7C RID: 3196
		WebHttp,
		// Token: 0x04000C7D RID: 3197
		HttpsWithSoap12,
		// Token: 0x04000C7E RID: 3198
		NamedPipe
	}
}
