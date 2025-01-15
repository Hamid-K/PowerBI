using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CA0 RID: 7328
	internal sealed class EvaluatorSession
	{
		// Token: 0x0600B638 RID: 46648 RVA: 0x0024FDF5 File Offset: 0x0024DFF5
		public EvaluatorSession(string id)
		{
			this.syncRoot = new object();
			this.id = id;
			this.containers = new ConcurrentSet<int>();
		}

		// Token: 0x17002D7A RID: 11642
		// (get) Token: 0x0600B639 RID: 46649 RVA: 0x0024FE1A File Offset: 0x0024E01A
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17002D7B RID: 11643
		// (get) Token: 0x0600B63A RID: 46650 RVA: 0x0024FE24 File Offset: 0x0024E024
		public DateTime LastRecentlyUsed
		{
			get
			{
				object obj = this.syncRoot;
				DateTime dateTime;
				lock (obj)
				{
					dateTime = this.lastRecentlyUsed;
				}
				return dateTime;
			}
		}

		// Token: 0x0600B63B RID: 46651 RVA: 0x0024FE68 File Offset: 0x0024E068
		public T UsingFamiliarContainers<T>(Func<ConcurrentSet<int>, T> func)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.useCount++;
			}
			T t;
			try
			{
				t = func(this.containers);
			}
			finally
			{
				obj = this.syncRoot;
				lock (obj)
				{
					this.useCount--;
				}
			}
			return t;
		}

		// Token: 0x0600B63C RID: 46652 RVA: 0x0024FF08 File Offset: 0x0024E108
		public int AddRef()
		{
			object obj = this.syncRoot;
			int num;
			lock (obj)
			{
				num = this.refCount + 1;
				this.refCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600B63D RID: 46653 RVA: 0x0024FF58 File Offset: 0x0024E158
		public int DecreaseRef()
		{
			object obj = this.syncRoot;
			int num;
			lock (obj)
			{
				this.lastRecentlyUsed = DateTime.UtcNow;
				num = this.refCount - 1;
				this.refCount = num;
				int num2 = num;
				if (num2 == 0 && this.useCount > 0)
				{
					throw new InvalidOperationException("EvaluatorSession is still in use.");
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x04005D14 RID: 23828
		private readonly object syncRoot;

		// Token: 0x04005D15 RID: 23829
		private readonly string id;

		// Token: 0x04005D16 RID: 23830
		private readonly ConcurrentSet<int> containers;

		// Token: 0x04005D17 RID: 23831
		private DateTime lastRecentlyUsed;

		// Token: 0x04005D18 RID: 23832
		private int refCount;

		// Token: 0x04005D19 RID: 23833
		private int useCount;
	}
}
