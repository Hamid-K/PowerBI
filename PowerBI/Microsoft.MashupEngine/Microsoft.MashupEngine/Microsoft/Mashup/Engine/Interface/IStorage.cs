using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000050 RID: 80
	public interface IStorage : IDisposable
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014F RID: 335
		IEnumerable<int> StreamIds { get; }

		// Token: 0x06000150 RID: 336
		Stream OpenStream(int id);

		// Token: 0x06000151 RID: 337
		Stream CreateStream();

		// Token: 0x06000152 RID: 338
		Stream CommitStream(int id, Stream stream);

		// Token: 0x06000153 RID: 339
		void Close();
	}
}
