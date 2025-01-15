using System;
using System.Collections;
using System.Diagnostics;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x0200078C RID: 1932
	public class PerformanceCounterInformationCollection : IEnumerable
	{
		// Token: 0x06003E67 RID: 15975 RVA: 0x000D17A5 File Offset: 0x000CF9A5
		public PerformanceCounterInformationCollection(int entries)
		{
			this.fields = new ArrayList(entries);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000D17B9 File Offset: 0x000CF9B9
		public IEnumerator GetEnumerator()
		{
			return new PerformanceCounterInformationCollection.PerformanceCounterInformationEnumerator(this);
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000D17C1 File Offset: 0x000CF9C1
		public int Length
		{
			get
			{
				return this.fields.Count;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		public PerformanceCounterInformation this[int index]
		{
			get
			{
				return (PerformanceCounterInformation)this.fields[index];
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x000D17E4 File Offset: 0x000CF9E4
		public CounterCreationDataCollection CreationDataCollection
		{
			get
			{
				CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
				foreach (object obj in this.fields)
				{
					((PerformanceCounterInformation)obj).AddTo(counterCreationDataCollection);
				}
				return counterCreationDataCollection;
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000D1844 File Offset: 0x000CFA44
		public void Add(PerformanceCounterInformation newField)
		{
			this.fields.Add(newField);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000D1853 File Offset: 0x000CFA53
		public void Remove(PerformanceCounterInformation oldField)
		{
			this.fields.Remove(oldField);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000D1861 File Offset: 0x000CFA61
		public void Clear()
		{
			this.fields.Clear();
		}

		// Token: 0x0400250A RID: 9482
		internal ArrayList fields;

		// Token: 0x0200078D RID: 1933
		private class PerformanceCounterInformationEnumerator : IEnumerator
		{
			// Token: 0x06003E6F RID: 15983 RVA: 0x000D186E File Offset: 0x000CFA6E
			public PerformanceCounterInformationEnumerator(PerformanceCounterInformationCollection theCollection)
			{
				this.theCollection = theCollection;
				this.location = -1;
			}

			// Token: 0x06003E70 RID: 15984 RVA: 0x000D1884 File Offset: 0x000CFA84
			public bool MoveNext()
			{
				this.location++;
				return this.location < this.theCollection.Length;
			}

			// Token: 0x17000EC3 RID: 3779
			// (get) Token: 0x06003E71 RID: 15985 RVA: 0x000D18AA File Offset: 0x000CFAAA
			public object Current
			{
				get
				{
					if (this.location < 0 || this.location >= this.theCollection.Length)
					{
						throw new InvalidOperationException("FATAL: Iterator off the end of PerformanceCounterInformationCollection");
					}
					return this.theCollection.fields[this.location];
				}
			}

			// Token: 0x06003E72 RID: 15986 RVA: 0x000D18E9 File Offset: 0x000CFAE9
			public void Reset()
			{
				this.location = -1;
			}

			// Token: 0x0400250B RID: 9483
			private PerformanceCounterInformationCollection theCollection;

			// Token: 0x0400250C RID: 9484
			private int location;
		}
	}
}
