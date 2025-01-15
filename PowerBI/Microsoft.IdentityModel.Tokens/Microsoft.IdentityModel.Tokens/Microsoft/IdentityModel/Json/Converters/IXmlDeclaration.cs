using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F6 RID: 246
	[NullableContext(2)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CD0 RID: 3280
		string Version { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CD1 RID: 3281
		// (set) Token: 0x06000CD2 RID: 3282
		string Encoding { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CD3 RID: 3283
		// (set) Token: 0x06000CD4 RID: 3284
		string Standalone { get; set; }
	}
}
