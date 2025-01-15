using System;
using System.Collections;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x02000113 RID: 275
	internal struct DictionaryEntryEnumerable : IEnumerable<DictionaryEntry>, IEnumerable
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x0002445D File Offset: 0x0002265D
		public DictionaryEntryEnumerable(IDictionary dictionary)
		{
			this._dictionary = dictionary;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00024466 File Offset: 0x00022666
		public DictionaryEntryEnumerable.DictionaryEntryEnumerator GetEnumerator()
		{
			IDictionary dictionary = this._dictionary;
			return new DictionaryEntryEnumerable.DictionaryEntryEnumerator((dictionary != null && dictionary.Count > 0) ? this._dictionary : null);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002448D File Offset: 0x0002268D
		IEnumerator<DictionaryEntry> IEnumerable<DictionaryEntry>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002449A File Offset: 0x0002269A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003EA RID: 1002
		private readonly IDictionary _dictionary;

		// Token: 0x02000267 RID: 615
		internal struct DictionaryEntryEnumerator : IEnumerator<DictionaryEntry>, IDisposable, IEnumerator
		{
			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x06001610 RID: 5648 RVA: 0x00039FD8 File Offset: 0x000381D8
			public DictionaryEntry Current
			{
				get
				{
					return this._entryEnumerator.Entry;
				}
			}

			// Token: 0x06001611 RID: 5649 RVA: 0x00039FE5 File Offset: 0x000381E5
			public DictionaryEntryEnumerator(IDictionary dictionary)
			{
				this._entryEnumerator = ((dictionary != null) ? dictionary.GetEnumerator() : null);
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06001612 RID: 5650 RVA: 0x00039FF9 File Offset: 0x000381F9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001613 RID: 5651 RVA: 0x0003A008 File Offset: 0x00038208
			public void Dispose()
			{
				IDisposable disposable;
				if ((disposable = this._entryEnumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x06001614 RID: 5652 RVA: 0x0003A02A File Offset: 0x0003822A
			public bool MoveNext()
			{
				IDictionaryEnumerator entryEnumerator = this._entryEnumerator;
				return entryEnumerator != null && entryEnumerator.MoveNext();
			}

			// Token: 0x06001615 RID: 5653 RVA: 0x0003A03D File Offset: 0x0003823D
			public void Reset()
			{
				IDictionaryEnumerator entryEnumerator = this._entryEnumerator;
				if (entryEnumerator == null)
				{
					return;
				}
				entryEnumerator.Reset();
			}

			// Token: 0x040006A1 RID: 1697
			private readonly IDictionaryEnumerator _entryEnumerator;
		}
	}
}
