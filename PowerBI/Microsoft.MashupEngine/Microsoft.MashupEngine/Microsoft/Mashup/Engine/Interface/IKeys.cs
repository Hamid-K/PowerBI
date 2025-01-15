using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000ED RID: 237
	public interface IKeys : IEnumerable<string>, IEnumerable
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000394 RID: 916
		int Length { get; }

		// Token: 0x1700016B RID: 363
		string this[int index] { get; }

		// Token: 0x06000396 RID: 918
		bool TryGetIndex(string key, out int index);
	}
}
