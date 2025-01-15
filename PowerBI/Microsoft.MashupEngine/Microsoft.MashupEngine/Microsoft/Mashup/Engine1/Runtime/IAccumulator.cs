using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001335 RID: 4917
	public interface IAccumulator
	{
		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x060081D1 RID: 33233
		IValueReference Current { get; }

		// Token: 0x060081D2 RID: 33234
		void AccumulateNext(IValueReference next);
	}
}
