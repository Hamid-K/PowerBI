using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F4 RID: 1524
	internal class DisposableCollectionWrapper<T> : IDisposable, IEnumerable<T>, IEnumerable where T : IDisposable
	{
		// Token: 0x06004A85 RID: 19077 RVA: 0x001083D4 File Offset: 0x001065D4
		internal DisposableCollectionWrapper(IEnumerable<T> enumerable)
		{
			this._enumerable = enumerable;
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x001083E4 File Offset: 0x001065E4
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (this._enumerable != null)
			{
				foreach (T t in this._enumerable)
				{
					if (t != null)
					{
						t.Dispose();
					}
				}
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00108450 File Offset: 0x00106650
		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0010845D File Offset: 0x0010665D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x04001A3A RID: 6714
		private readonly IEnumerable<T> _enumerable;
	}
}
