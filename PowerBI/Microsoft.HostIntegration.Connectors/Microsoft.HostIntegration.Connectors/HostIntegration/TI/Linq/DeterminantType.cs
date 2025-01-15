using System;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000758 RID: 1880
	public enum DeterminantType
	{
		// Token: 0x040023B0 RID: 9136
		Endpoint = 2,
		// Token: 0x040023B1 RID: 9137
		Trm = 4,
		// Token: 0x040023B2 RID: 9138
		Data = 8,
		// Token: 0x040023B3 RID: 9139
		EnvelopeSna = 16,
		// Token: 0x040023B4 RID: 9140
		Elm = 32,
		// Token: 0x040023B5 RID: 9141
		Invalid = 64,
		// Token: 0x040023B6 RID: 9142
		NewElmLink = 128,
		// Token: 0x040023B7 RID: 9143
		NewElmUserDataMS = 256,
		// Token: 0x040023B8 RID: 9144
		NewElmUserDataIBM,
		// Token: 0x040023B9 RID: 9145
		NewHttp = 512,
		// Token: 0x040023BA RID: 9146
		NewSnaEndpoint = 1024,
		// Token: 0x040023BB RID: 9147
		NewSnaLink = 2048,
		// Token: 0x040023BC RID: 9148
		NewSnaUserData = 4096,
		// Token: 0x040023BD RID: 9149
		NewTrmLink = 8192
	}
}
