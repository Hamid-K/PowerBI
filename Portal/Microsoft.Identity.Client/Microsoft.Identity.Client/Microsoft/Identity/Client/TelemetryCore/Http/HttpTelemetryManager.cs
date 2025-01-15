using System;
using System.Collections.Concurrent;
using System.Text;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.TelemetryCore.Http
{
	// Token: 0x020001EA RID: 490
	internal class HttpTelemetryManager : IHttpTelemetryManager
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x00046178 File Offset: 0x00044378
		public void ResetPreviousUnsentData()
		{
			this._successfullSilentCallCount = 0;
			ApiEvent apiEvent;
			while (this._failedEvents.TryDequeue(out apiEvent))
			{
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004619B File Offset: 0x0004439B
		public void RecordStoppedEvent(ApiEvent stoppedEvent)
		{
			if (!string.IsNullOrEmpty(stoppedEvent.ApiErrorCode))
			{
				this._failedEvents.Enqueue(stoppedEvent);
			}
			if (stoppedEvent.IsAccessTokenCacheHit)
			{
				this._successfullSilentCallCount++;
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000461CC File Offset: 0x000443CC
		public string GetLastRequestHeader()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = true;
			foreach (ApiEvent apiEvent in this._failedEvents)
			{
				if (!flag)
				{
					stringBuilder2.Append(',');
				}
				stringBuilder2.Append(HttpHeaderSanitizer.SanitizeHeader(apiEvent.ApiErrorCode));
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(apiEvent.ApiIdString);
				stringBuilder.Append(',');
				stringBuilder.Append(apiEvent.CorrelationId.ToString());
				flag = false;
			}
			string text = string.Format("{0}|", '5') + string.Format("{0}|", this._successfullSilentCallCount) + string.Format("{0}|", stringBuilder) + string.Format("{0}|", stringBuilder2);
			if (text.Length > 3800)
			{
				this.ResetPreviousUnsentData();
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000462E4 File Offset: 0x000444E4
		public string GetCurrentRequestHeader(ApiEvent eventInProgress)
		{
			if (eventInProgress == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('5');
			stringBuilder.Append('|');
			stringBuilder.Append(eventInProgress.ApiIdString);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.CacheInfoString);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.RegionUsed);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.RegionAutodetectionSourceString);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.RegionOutcomeString);
			stringBuilder.Append('|');
			stringBuilder.Append(eventInProgress.IsTokenCacheSerializedString);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.IsLegacyCacheEnabledString);
			stringBuilder.Append(',');
			stringBuilder.Append(eventInProgress.TokenTypeString);
			return stringBuilder.ToString();
		}

		// Token: 0x040008AE RID: 2222
		private int _successfullSilentCallCount;

		// Token: 0x040008AF RID: 2223
		private ConcurrentQueue<ApiEvent> _failedEvents = new ConcurrentQueue<ApiEvent>();
	}
}
