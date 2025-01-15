using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200006E RID: 110
	public interface IFoldingFailureService
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B3 RID: 435
		bool ThrowOnFoldingFailure { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B4 RID: 436
		bool ThrowOnVolatileFunctions { get; }
	}
}
