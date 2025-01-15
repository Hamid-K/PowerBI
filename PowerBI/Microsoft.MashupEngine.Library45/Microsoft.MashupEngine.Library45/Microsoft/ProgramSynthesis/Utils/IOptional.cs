using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004D7 RID: 1239
	public interface IOptional
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001B8C RID: 7052
		object Value { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001B8D RID: 7053
		bool HasValue { get; }
	}
}
