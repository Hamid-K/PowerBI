using System;
using System.Collections;
using System.Text;
using System.Web;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DC RID: 732
	internal class ClearAspNetCachePolicy<TKey, TValue> : ICacheRetentionPolicy<TKey, TValue> where TValue : class
	{
		// Token: 0x06001A20 RID: 6688 RVA: 0x000053DC File Offset: 0x000035DC
		public bool Add(TKey key, TValue value)
		{
			return true;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000053DC File Offset: 0x000035DC
		public bool Remove(TKey key)
		{
			return true;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void MarkAsUsed(TKey key)
		{
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000692AC File Offset: 0x000674AC
		public void PerformEviction(CachePolicyDelegates<TValue>.EvictionCallback callback, bool aggressivePurge)
		{
			foreach (object obj in HttpRuntime.Cache)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				TValue tvalue = HttpRuntime.Cache[(string)dictionaryEntry.Key] as TValue;
				if (tvalue != null && !callback(tvalue))
				{
					break;
				}
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void Reset()
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void RetrieveTracingInfo(TKey key, StringBuilder targetBuilder)
		{
		}
	}
}
