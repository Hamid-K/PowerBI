using System;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x020001E5 RID: 485
	public enum AutodiscoverErrorCode
	{
		// Token: 0x04000D34 RID: 3380
		NoError,
		// Token: 0x04000D35 RID: 3381
		RedirectAddress,
		// Token: 0x04000D36 RID: 3382
		RedirectUrl,
		// Token: 0x04000D37 RID: 3383
		InvalidUser,
		// Token: 0x04000D38 RID: 3384
		InvalidRequest,
		// Token: 0x04000D39 RID: 3385
		InvalidSetting,
		// Token: 0x04000D3A RID: 3386
		SettingIsNotAvailable,
		// Token: 0x04000D3B RID: 3387
		ServerBusy,
		// Token: 0x04000D3C RID: 3388
		InvalidDomain,
		// Token: 0x04000D3D RID: 3389
		NotFederated,
		// Token: 0x04000D3E RID: 3390
		InternalServerError
	}
}
