using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	internal sealed class CastEnumerator<TInput, TOutput> : ConversionEnumerator<TInput, TOutput> where TInput : TOutput
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x0001C006 File Offset: 0x0001A206
		private static TOutput Cast(TInput t)
		{
			return (TOutput)((object)t);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001C013 File Offset: 0x0001A213
		public CastEnumerator(IEnumerator<TInput> enumerator)
			: base(enumerator, new Converter<TInput, TOutput>(CastEnumerator<TInput, TOutput>.Cast))
		{
		}
	}
}
