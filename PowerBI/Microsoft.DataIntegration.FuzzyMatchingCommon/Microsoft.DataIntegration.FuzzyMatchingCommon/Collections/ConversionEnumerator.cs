using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	internal class ConversionEnumerator<T, TOutput> : IEnumerator<TOutput>, IDisposable, IEnumerator
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001BF41 File Offset: 0x0001A141
		public ConversionEnumerator(IEnumerator<T> enumerator, Converter<T, TOutput> converter)
		{
			this.m_enumeratorT = enumerator;
			this.m_converter = converter;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001BF57 File Offset: 0x0001A157
		public void Dispose()
		{
			this.m_enumeratorT.Dispose();
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001BF64 File Offset: 0x0001A164
		public TOutput Current
		{
			get
			{
				return this.m_current;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001BF6C File Offset: 0x0001A16C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001BF79 File Offset: 0x0001A179
		public bool MoveNext()
		{
			if (this.m_enumeratorT.MoveNext())
			{
				this.m_current = this.m_converter.Invoke(this.m_enumeratorT.Current);
				return true;
			}
			return false;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001BFA7 File Offset: 0x0001A1A7
		public void Reset()
		{
			this.m_enumeratorT.Reset();
			this.m_current = default(TOutput);
		}

		// Token: 0x040000C3 RID: 195
		private IEnumerator<T> m_enumeratorT;

		// Token: 0x040000C4 RID: 196
		private Converter<T, TOutput> m_converter;

		// Token: 0x040000C5 RID: 197
		private TOutput m_current;
	}
}
