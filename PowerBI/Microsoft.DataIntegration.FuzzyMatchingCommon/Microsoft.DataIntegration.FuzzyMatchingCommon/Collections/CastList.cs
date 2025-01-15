using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	internal sealed class CastList<TInput, TOutput> : ConversionList<TInput, TOutput> where TInput : TOutput
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0001C028 File Offset: 0x0001A228
		private static TOutput Cast(TInput t)
		{
			return (TOutput)((object)t);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001C035 File Offset: 0x0001A235
		public CastList(IList<TInput> list)
			: base(list, new Converter<TInput, TOutput>(CastList<TInput, TOutput>.Cast))
		{
		}
	}
}
