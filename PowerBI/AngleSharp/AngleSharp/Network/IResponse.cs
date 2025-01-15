using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AngleSharp.Network
{
	// Token: 0x02000096 RID: 150
	public interface IResponse : IDisposable
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000476 RID: 1142
		HttpStatusCode StatusCode { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000477 RID: 1143
		Url Address { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000478 RID: 1144
		IDictionary<string, string> Headers { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000479 RID: 1145
		Stream Content { get; }
	}
}
