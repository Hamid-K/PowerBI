using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	internal sealed class ConvertEnumerable<T, TOutput> : IEnumerable<TOutput>, IEnumerable
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		public ConvertEnumerable(IEnumerable<T> enumerable, Converter<T, TOutput> converter)
		{
			this.m_enumerableT = enumerable;
			this.m_converter = converter;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001BFD6 File Offset: 0x0001A1D6
		public IEnumerator<TOutput> GetEnumerator()
		{
			return new ConversionEnumerator<T, TOutput>(this.m_enumerableT.GetEnumerator(), this.m_converter);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001BFEE File Offset: 0x0001A1EE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ConversionEnumerator<T, TOutput>(this.m_enumerableT.GetEnumerator(), this.m_converter);
		}

		// Token: 0x040000C6 RID: 198
		private IEnumerable<T> m_enumerableT;

		// Token: 0x040000C7 RID: 199
		private Converter<T, TOutput> m_converter;
	}
}
