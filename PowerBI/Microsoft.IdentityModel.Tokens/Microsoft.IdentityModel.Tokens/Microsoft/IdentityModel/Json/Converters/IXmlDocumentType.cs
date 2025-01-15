using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F7 RID: 247
	[NullableContext(2)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CD5 RID: 3285
		[Nullable(1)]
		string Name
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CD6 RID: 3286
		string System { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CD7 RID: 3287
		string Public { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CD8 RID: 3288
		string InternalSubset { get; }
	}
}
