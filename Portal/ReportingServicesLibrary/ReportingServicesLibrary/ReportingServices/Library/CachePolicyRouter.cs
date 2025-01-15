using System;
using System.Text;
using System.Threading;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DB RID: 731
	internal class CachePolicyRouter<TKey, TValue> : ICacheRetentionPolicy<TKey, TValue>
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00069008 File Offset: 0x00067208
		public CachePolicyRouter(int numPolicies, CachePolicyRouter<TKey, TValue>.CacheRetentionConstructionDelegate constructor)
		{
			this.m_numPolicies = Math.Max(1, numPolicies);
			this.m_cachePolicies = new ICacheRetentionPolicy<TKey, TValue>[numPolicies];
			for (int i = 0; i < numPolicies; i++)
			{
				this.m_cachePolicies[i] = constructor();
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006905C File Offset: 0x0006725C
		public bool Add(TKey key, TValue value)
		{
			bool flag;
			try
			{
				this.m_rwGlobalLock.AcquireReaderLock(-1);
				flag = this.GetPolicy(key).Add(key, value);
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseReaderLock();
			}
			return flag;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000690A4 File Offset: 0x000672A4
		public bool Remove(TKey key)
		{
			bool flag;
			try
			{
				this.m_rwGlobalLock.AcquireReaderLock(-1);
				flag = this.GetPolicy(key).Remove(key);
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseReaderLock();
			}
			return flag;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000690EC File Offset: 0x000672EC
		public void MarkAsUsed(TKey key)
		{
			try
			{
				this.m_rwGlobalLock.AcquireReaderLock(-1);
				this.GetPolicy(key).MarkAsUsed(key);
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00069130 File Offset: 0x00067330
		public void RetrieveTracingInfo(TKey key, StringBuilder targetBuilder)
		{
			try
			{
				this.m_rwGlobalLock.AcquireReaderLock(-1);
				int num = this.DeterminePolicyIndex(key);
				targetBuilder.AppendFormat(" Policy Index: {0}", num);
				this.GetPolicy(key).RetrieveTracingInfo(key, targetBuilder);
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00069190 File Offset: 0x00067390
		public void PerformEviction(CachePolicyDelegates<TValue>.EvictionCallback callback, bool aggressivePurge)
		{
			try
			{
				this.m_rwGlobalLock.AcquireWriterLock(-1);
				for (int i = 0; i < this.m_numPolicies; i++)
				{
					this.m_cachePolicies[i].PerformEviction(callback, false);
				}
				if (aggressivePurge)
				{
					for (int j = 0; j < this.m_numPolicies; j++)
					{
						this.m_cachePolicies[j].PerformEviction(callback, true);
					}
				}
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0006920C File Offset: 0x0006740C
		public void Reset()
		{
			try
			{
				this.m_rwGlobalLock.AcquireWriterLock(-1);
				for (int i = 0; i < this.m_numPolicies; i++)
				{
					this.m_cachePolicies[i].Reset();
				}
			}
			finally
			{
				this.m_rwGlobalLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00069264 File Offset: 0x00067464
		private ICacheRetentionPolicy<TKey, TValue> GetPolicy(TKey key)
		{
			int num = this.DeterminePolicyIndex(key);
			return this.m_cachePolicies[num];
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00069281 File Offset: 0x00067481
		private int DeterminePolicyIndex(TKey key)
		{
			if (key == null)
			{
				return 0;
			}
			return Math.Abs((key.GetHashCode() >> 16) % this.m_numPolicies);
		}

		// Token: 0x04000976 RID: 2422
		private readonly ICacheRetentionPolicy<TKey, TValue>[] m_cachePolicies;

		// Token: 0x04000977 RID: 2423
		private ReaderWriterLock m_rwGlobalLock = new ReaderWriterLock();

		// Token: 0x04000978 RID: 2424
		private readonly int m_numPolicies;

		// Token: 0x04000979 RID: 2425
		private const int LockTimeout = -1;

		// Token: 0x020004EA RID: 1258
		// (Invoke) Token: 0x060024B3 RID: 9395
		public delegate ICacheRetentionPolicy<TKey, TValue> CacheRetentionConstructionDelegate();
	}
}
