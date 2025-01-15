using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200135C RID: 4956
	public interface IValueReference
	{
		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06008260 RID: 33376
		bool Evaluated { get; }

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x06008261 RID: 33377
		Value Value { get; }
	}
}
