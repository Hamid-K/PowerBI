using System;
using System.Collections.Generic;
using System.IO;

namespace AngleSharp.Network
{
	// Token: 0x02000093 RID: 147
	public interface IRequest
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600046F RID: 1135
		HttpMethod Method { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000470 RID: 1136
		Url Address { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000471 RID: 1137
		IDictionary<string, string> Headers { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000472 RID: 1138
		Stream Content { get; }
	}
}
