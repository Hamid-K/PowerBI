using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000DA RID: 218
	internal class TokenizeFieldIterator : LockFreeStack<TokenizeFieldIterator>.Node, IEnumerable, IEnumerator
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0002A320 File Offset: 0x00028520
		public void Reset()
		{
			this.TokenizerContext.Reset();
			this.TokenIdProvider.Reset();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002A338 File Offset: 0x00028538
		public IEnumerator GetEnumerator()
		{
			this.Reset();
			return this;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002A341 File Offset: 0x00028541
		public object Current
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002A344 File Offset: 0x00028544
		public bool MoveNext()
		{
			if (this.m_tokenEnumerator.MoveNext())
			{
				this.index++;
				return true;
			}
			this.ContextCache.Push(this);
			return false;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002A370 File Offset: 0x00028570
		void IEnumerator.Reset()
		{
			this.index = -1;
			this.m_tokenEnumerator = this.m_tokenEnumerable.GetEnumerator();
		}

		// Token: 0x04000372 RID: 882
		public TokenizerContext TokenizerContext;

		// Token: 0x04000373 RID: 883
		public IEnumerable<StringExtent> m_tokenEnumerable;

		// Token: 0x04000374 RID: 884
		public IEnumerator<StringExtent> m_tokenEnumerator;

		// Token: 0x04000375 RID: 885
		public int index;

		// Token: 0x04000376 RID: 886
		public IRecordTokenizer recordTokenizer;

		// Token: 0x04000377 RID: 887
		public DataTable TokenizerSchema;

		// Token: 0x04000378 RID: 888
		public SimpleDataRecord simpleDataRecord;

		// Token: 0x04000379 RID: 889
		public SessionTokenIdProvider TokenIdProvider;

		// Token: 0x0400037A RID: 890
		public SqlChars sqlChars = new SqlChars(new char[4000]);

		// Token: 0x0400037B RID: 891
		public LockFreeStack<TokenizeFieldIterator> ContextCache;
	}
}
