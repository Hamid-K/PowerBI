using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001336 RID: 4918
	internal static class IAccumulatorExtensions
	{
		// Token: 0x060081D3 RID: 33235 RVA: 0x001B910C File Offset: 0x001B730C
		public static void AccumulateRange(this IAccumulator accumulator, IEnumerable<IValueReference> enumerable)
		{
			foreach (IValueReference valueReference in enumerable)
			{
				accumulator.AccumulateNext(valueReference);
			}
		}
	}
}
