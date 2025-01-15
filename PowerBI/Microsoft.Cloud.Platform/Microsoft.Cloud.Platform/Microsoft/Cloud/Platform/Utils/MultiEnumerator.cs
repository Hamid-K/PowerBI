using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200025E RID: 606
	internal sealed class MultiEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x000371C3 File Offset: 0x000353C3
		internal MultiEnumerator(IEnumerable<IEnumerable<T>> enumerables)
		{
			this.m_enumerableEnumerator = enumerables.GetEnumerator();
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x000371D7 File Offset: 0x000353D7
		T IEnumerator<T>.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000371DF File Offset: 0x000353DF
		public void Dispose()
		{
			this.m_enumerableEnumerator.Dispose();
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000371EC File Offset: 0x000353EC
		public bool MoveNext()
		{
			while (this.m_currentEnumerator == null || !this.m_currentEnumerator.MoveNext())
			{
				if (!this.m_enumerableEnumerator.MoveNext())
				{
					return false;
				}
				this.m_currentEnumerator = this.m_enumerableEnumerator.Current.GetEnumerator();
			}
			return true;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003722B File Offset: 0x0003542B
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00037232 File Offset: 0x00035432
		public object Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003723F File Offset: 0x0003543F
		private T GetCurrent()
		{
			if (this.m_currentEnumerator != null)
			{
				return this.m_currentEnumerator.Current;
			}
			throw new InvalidOperationException("Enumerator must MoveNext once before calling current item");
		}

		// Token: 0x040005FD RID: 1533
		private readonly IEnumerator<IEnumerable<T>> m_enumerableEnumerator;

		// Token: 0x040005FE RID: 1534
		private IEnumerator<T> m_currentEnumerator;
	}
}
