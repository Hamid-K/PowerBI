using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001334 RID: 4916
	public interface IAccumulableFunction
	{
		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x060081CF RID: 33231
		string EnumerableParameter { get; }

		// Token: 0x060081D0 RID: 33232
		IAccumulable CreateAccumulable(RecordValue arguments);
	}
}
