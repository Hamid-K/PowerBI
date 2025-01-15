using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000EC RID: 236
	public interface IValueReference2
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000392 RID: 914
		bool Evaluated { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000393 RID: 915
		IValue Value { get; }
	}
}
