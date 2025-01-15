using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000129 RID: 297
	public interface IToken
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000600 RID: 1536
		string Value { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000601 RID: 1537
		int StartIndex { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000602 RID: 1538
		int Length { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000603 RID: 1539
		TokenType Type { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000604 RID: 1540
		object ClrValue { get; }
	}
}
