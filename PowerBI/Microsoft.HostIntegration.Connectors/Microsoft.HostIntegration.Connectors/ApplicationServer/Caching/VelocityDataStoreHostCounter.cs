using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039E RID: 926
	internal sealed class VelocityDataStoreHostCounter
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x00064828 File Offset: 0x00062A28
		internal VelocityDataStoreHostCounter()
		{
			this._evictedObjectCount = new SafeValueStore();
			this._exceptionCount = new SafeValueStore();
			this._expiredObjectCount = new SafeValueStore();
			this._getAndLockOperationCount = new SafeValueStore();
			this._getMissCount = new SafeValueStore();
			this._readRequestCount = new SafeValueStore();
			this._requestCount = new SafeValueStore();
			this._responseCount = new SafeValueStore();
			this._retryExceptionCount = new SafeValueStore();
			this._successfulGetAndLockOperationCount = new SafeValueStore();
			this._totalEvictionRun = new SafeValueStore();
			this._totalMemoryEvicted = new SafeValueStore();
			this._totalObjectReturned = new SafeValueStore();
			this._totalObjectReturned = new SafeValueStore();
			this._writeRequestCount = new SafeValueStore();
			this._totalRequestCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_REQUEST, new PerfCounterValue(this.GetRequestCount));
			this._totalRequestPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_REQUEST_PER_SEC, new PerfCounterValue(this.GetRequestCount));
			this._totalResponseCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_RESPONSE, new PerfCounterValue(this.GetResponseCount));
			this._totalResponsePerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_RESPONSE_PER_SEC, new PerfCounterValue(this.GetResponseCount));
			this._readRequestCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_READ_REQUEST, new PerfCounterValue(this.GetReadRequestCount));
			this._readRequestPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_READ_REQUEST_PER_SEC, new PerfCounterValue(this.GetReadRequestCount));
			this._writeRequestCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_WRITE_REQUEST, new PerfCounterValue(this.GetWriteRequestCount));
			this._writeRequestPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_WRITE_REQUEST_PER_SEC, new PerfCounterValue(this.GetWriteRequestCount));
			this._exceptionCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_EXCEPTION, new PerfCounterValue(this.GetExceptionCount));
			this._exceptionPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_EXCEPTION_PER_SEC, new PerfCounterValue(this.GetExceptionCount));
			this._retryExceptionCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_RETRY_EXCEPTION, new PerfCounterValue(this.GetRetryExceptionCount));
			this._retryExceptionPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_RETRY_EXCEPTION_PER_SEC, new PerfCounterValue(this.GetRetryExceptionCount));
			this._getAndLockOperationCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GETANDLOCK_REQUEST, new PerfCounterValue(this.GetGetAndLockOperationCount));
			this._getAndLockOperationPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GETANDLOCK_REQUEST_PER_SEC, new PerfCounterValue(this.GetGetAndLockOperationCount));
			this._successfulGetAndLockOperationCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_SUCCESSFULGETANDLOCK_REQUEST, new PerfCounterValue(this.GetSuccessfulGetAndLockOperationCount));
			this._successfulGetAndLockOperationPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_SUCCESSFULGETANDLOCK_REQUEST_PER_SEC, new PerfCounterValue(this.GetSuccessfulGetAndLockOperationCount));
			this._expiredObjectCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_EXPIRED_OBJECTS, new PerfCounterValue(this.GetExpiredObjectCount));
			this._evictedObjectCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_EVICTED_OBJECTS, new PerfCounterValue(this.GetEvictedObjectCount));
			this._totalEvictionRunCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_EVICTION_RUN, new PerfCounterValue(this.GetEvictionRunCount));
			this._totalMemoryEvictedCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_MEMORY_EVICTED, new PerfCounterValue(this.GetTotalEvictedMemory));
			this._totalObjectReturnedCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_OBJECTS_RETURNED, new PerfCounterValue(this.GetTotalObjectReturned));
			this._totalObjectReturnedCounterPerSec = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_OBJECTS_RETURNED_PER_SEC, new PerfCounterValue(this.GetTotalObjectReturned));
			this._getMissCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GET_REQUEST_MISS, new PerfCounterValue(this.GetGetMissCount));
			this._getMissPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GET_REQUEST_MISS_PER_SEC, new PerfCounterValue(this.GetGetMissCount));
			this._percentageRetryError = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.PERCENTAGE_RETRY_ERRORS, new PerfCounterValue(this.GetRetryExceptionCountPercentage));
			this._percentageRetryErrorBase = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.PERCENTAGE_RETRY_ERRORS_BASE, new PerfCounterValue(this.GetResponseCount));
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00064B6A File Offset: 0x00062D6A
		internal long GetGetMissCount()
		{
			return this._getMissCount.GetValue();
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00064B77 File Offset: 0x00062D77
		internal void IncrGetMissCount()
		{
			this._getMissCount.Increment();
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00064B84 File Offset: 0x00062D84
		internal long GetTotalObjectReturned()
		{
			return this._totalObjectReturned.GetValue();
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00064B91 File Offset: 0x00062D91
		internal void IncrTotalObjectReturnedBy(long count)
		{
			this._totalObjectReturned.Add(count);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00064B9F File Offset: 0x00062D9F
		internal long GetTotalEvictedMemory()
		{
			return this._totalMemoryEvicted.GetValue();
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00064BAC File Offset: 0x00062DAC
		internal void IncrTotalEvictedMemoryBy(long size)
		{
			this._totalMemoryEvicted.Add(size);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00064BBA File Offset: 0x00062DBA
		internal long GetEvictionRunCount()
		{
			return this._totalEvictionRun.GetValue();
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00064BC7 File Offset: 0x00062DC7
		internal void IncrEvictionRunCount()
		{
			this._totalEvictionRun.Increment();
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00064BD4 File Offset: 0x00062DD4
		internal long GetEvictedObjectCount()
		{
			return this._evictedObjectCount.GetValue();
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00064BE1 File Offset: 0x00062DE1
		internal void IncrEvictedObjectCountBy(long count)
		{
			this._evictedObjectCount.Add(count);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00064BEF File Offset: 0x00062DEF
		internal long GetExpiredObjectCount()
		{
			return this._expiredObjectCount.GetValue();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00064BFC File Offset: 0x00062DFC
		internal void IncrExpiredObjectCountBy(long count)
		{
			this._expiredObjectCount.Add(count);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00064C0A File Offset: 0x00062E0A
		internal long GetSuccessfulGetAndLockOperationCount()
		{
			return this._successfulGetAndLockOperationCount.GetValue();
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00064C17 File Offset: 0x00062E17
		internal void IncrSuccessfulGetAndLockOperationCount()
		{
			this._successfulGetAndLockOperationCount.Increment();
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00064C24 File Offset: 0x00062E24
		internal long GetGetAndLockOperationCount()
		{
			return this._getAndLockOperationCount.GetValue();
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00064C31 File Offset: 0x00062E31
		internal void IncrGetAndLockOperationCount()
		{
			this._getAndLockOperationCount.Increment();
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00064C3E File Offset: 0x00062E3E
		internal long GetExceptionCount()
		{
			return this._exceptionCount.GetValue();
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00064C4B File Offset: 0x00062E4B
		internal long IncrExceptionCount()
		{
			this._exceptionCount.Increment();
			return this._exceptionCount.GetValue();
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00064C63 File Offset: 0x00062E63
		internal long GetRetryExceptionCount()
		{
			return this._retryExceptionCount.GetValue();
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00064C70 File Offset: 0x00062E70
		internal long GetRetryExceptionCountPercentage()
		{
			return this._retryExceptionCount.GetValue() * 100L;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00064C81 File Offset: 0x00062E81
		internal long IncrRetryExceptionCount()
		{
			this._retryExceptionCount.Increment();
			return this._retryExceptionCount.GetValue();
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00064C99 File Offset: 0x00062E99
		internal long GetReadRequestCount()
		{
			return this._readRequestCount.GetValue();
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00064CA6 File Offset: 0x00062EA6
		internal long GetWriteRequestCount()
		{
			return this._writeRequestCount.GetValue();
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00064CB3 File Offset: 0x00062EB3
		internal long GetRequestCount()
		{
			return this._requestCount.GetValue();
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00064CC0 File Offset: 0x00062EC0
		internal long GetResponseCount()
		{
			return this._responseCount.GetValue();
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00064CCD File Offset: 0x00062ECD
		internal void IncrResponseCount()
		{
			this._responseCount.Increment();
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00064CDA File Offset: 0x00062EDA
		internal void IncrRequestCount()
		{
			this._requestCount.Increment();
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00064CE7 File Offset: 0x00062EE7
		internal void IncrWriteRequestCount()
		{
			this._writeRequestCount.Increment();
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00064CF4 File Offset: 0x00062EF4
		internal void IncrWriteRequestCountBy(long count)
		{
			this._writeRequestCount.Add(count);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00064D02 File Offset: 0x00062F02
		internal void IncrReadRequestCount()
		{
			this._readRequestCount.Increment();
		}

		// Token: 0x04001331 RID: 4913
		private IValueStore _requestCount;

		// Token: 0x04001332 RID: 4914
		private DelegateBasedHostPerfCounter _totalRequestCounter;

		// Token: 0x04001333 RID: 4915
		private DelegateBasedHostPerfCounter _totalRequestPerSecCounter;

		// Token: 0x04001334 RID: 4916
		private IValueStore _responseCount;

		// Token: 0x04001335 RID: 4917
		private DelegateBasedHostPerfCounter _totalResponseCounter;

		// Token: 0x04001336 RID: 4918
		private DelegateBasedHostPerfCounter _totalResponsePerSecCounter;

		// Token: 0x04001337 RID: 4919
		private IValueStore _readRequestCount;

		// Token: 0x04001338 RID: 4920
		private DelegateBasedHostPerfCounter _readRequestCounter;

		// Token: 0x04001339 RID: 4921
		private DelegateBasedHostPerfCounter _readRequestPerSecCounter;

		// Token: 0x0400133A RID: 4922
		private IValueStore _writeRequestCount;

		// Token: 0x0400133B RID: 4923
		private DelegateBasedHostPerfCounter _writeRequestCounter;

		// Token: 0x0400133C RID: 4924
		private DelegateBasedHostPerfCounter _writeRequestPerSecCounter;

		// Token: 0x0400133D RID: 4925
		private IValueStore _exceptionCount;

		// Token: 0x0400133E RID: 4926
		private DelegateBasedHostPerfCounter _exceptionCounter;

		// Token: 0x0400133F RID: 4927
		private DelegateBasedHostPerfCounter _exceptionPerSecCounter;

		// Token: 0x04001340 RID: 4928
		private IValueStore _retryExceptionCount;

		// Token: 0x04001341 RID: 4929
		private DelegateBasedHostPerfCounter _retryExceptionCounter;

		// Token: 0x04001342 RID: 4930
		private DelegateBasedHostPerfCounter _retryExceptionPerSecCounter;

		// Token: 0x04001343 RID: 4931
		private IValueStore _getAndLockOperationCount;

		// Token: 0x04001344 RID: 4932
		private DelegateBasedHostPerfCounter _getAndLockOperationCounter;

		// Token: 0x04001345 RID: 4933
		private DelegateBasedHostPerfCounter _getAndLockOperationPerSecCounter;

		// Token: 0x04001346 RID: 4934
		private IValueStore _successfulGetAndLockOperationCount;

		// Token: 0x04001347 RID: 4935
		private DelegateBasedHostPerfCounter _successfulGetAndLockOperationCounter;

		// Token: 0x04001348 RID: 4936
		private DelegateBasedHostPerfCounter _successfulGetAndLockOperationPerSecCounter;

		// Token: 0x04001349 RID: 4937
		private IValueStore _expiredObjectCount;

		// Token: 0x0400134A RID: 4938
		private DelegateBasedHostPerfCounter _expiredObjectCounter;

		// Token: 0x0400134B RID: 4939
		private IValueStore _evictedObjectCount;

		// Token: 0x0400134C RID: 4940
		private DelegateBasedHostPerfCounter _evictedObjectCounter;

		// Token: 0x0400134D RID: 4941
		private IValueStore _totalEvictionRun;

		// Token: 0x0400134E RID: 4942
		private DelegateBasedHostPerfCounter _totalEvictionRunCounter;

		// Token: 0x0400134F RID: 4943
		private IValueStore _totalMemoryEvicted;

		// Token: 0x04001350 RID: 4944
		private DelegateBasedHostPerfCounter _totalMemoryEvictedCounter;

		// Token: 0x04001351 RID: 4945
		private IValueStore _totalObjectReturned;

		// Token: 0x04001352 RID: 4946
		private DelegateBasedHostPerfCounter _totalObjectReturnedCounter;

		// Token: 0x04001353 RID: 4947
		private DelegateBasedHostPerfCounter _totalObjectReturnedCounterPerSec;

		// Token: 0x04001354 RID: 4948
		private IValueStore _getMissCount;

		// Token: 0x04001355 RID: 4949
		private DelegateBasedHostPerfCounter _getMissCounter;

		// Token: 0x04001356 RID: 4950
		private DelegateBasedHostPerfCounter _getMissPerSecCounter;

		// Token: 0x04001357 RID: 4951
		private DelegateBasedHostPerfCounter _percentageRetryError;

		// Token: 0x04001358 RID: 4952
		private DelegateBasedHostPerfCounter _percentageRetryErrorBase;
	}
}
