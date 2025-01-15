using System;
using System.Collections.Specialized;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000211 RID: 529
	public class ExternalRequestActivityCreator
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x000311A0 File Offset: 0x0002F3A0
		public ExternalRequestActivityCreator(ActivityType activityType, [NotNull] string rootActivityHeaderName, [NotNull] string clientActivityHeaderName, ClientActivityContextSource source, IActivityFactory activityFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(rootActivityHeaderName, "rootActivityHeaderName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(clientActivityHeaderName, "clientActivityHeaderName");
			ExtendedDiagnostics.EnsureOperation(source.HasFlag(ClientActivityContextSource.CreateNew), "CreateNew option must be on to create a new activity");
			this.m_activityType = activityType;
			this.m_rootActivityParamName = rootActivityHeaderName;
			this.m_clientActivityParamName = clientActivityHeaderName;
			this.m_source = source;
			this.m_activityFactory = activityFactory;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003120C File Offset: 0x0002F40C
		public ExternalSyncActivity CreateActivity(NameValueCollection requestHeaders, NameValueCollection queryStringPairs)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (this.m_source.HasFlag(ClientActivityContextSource.Header) && requestHeaders != null)
			{
				text = requestHeaders[this.m_clientActivityParamName] ?? string.Empty;
				text2 = requestHeaders[this.m_rootActivityParamName] ?? string.Empty;
			}
			bool flag = !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2);
			if (!flag && this.m_source.HasFlag(ClientActivityContextSource.QueryString) && queryStringPairs != null)
			{
				text = queryStringPairs[this.m_clientActivityParamName] ?? string.Empty;
				text2 = queryStringPairs[this.m_rootActivityParamName] ?? string.Empty;
				flag = !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2);
			}
			if (!flag && this.m_source.HasFlag(ClientActivityContextSource.Cookie) && requestHeaders != null)
			{
				string text3 = requestHeaders["Cookie"] ?? string.Empty;
				if (!string.IsNullOrEmpty(text3))
				{
					CookieUtils.TryGetCookieValue(text3, this.m_clientActivityParamName, out text);
					CookieUtils.TryGetCookieValue(text3, this.m_rootActivityParamName, out text2);
				}
				bool flag2 = !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2);
			}
			if (!UtilsContext.Current.Activity.Equals(Activity.Empty))
			{
				UtilsContext.Current.ClearStack();
			}
			TraceSourceBase<CommonTrace>.Tracer.Trace(TraceVerbosity.Verbose, "RootActivityId is '{0}' and ClientActivityId is '{1}'", new object[] { text2, text });
			Guid empty = Guid.Empty;
			ExternalSyncActivity externalSyncActivity;
			if (!string.IsNullOrEmpty(text2) && Guid.TryParse(text2, out empty) && !empty.Equals(Guid.Empty))
			{
				Guid guid = Guid.NewGuid();
				externalSyncActivity = this.m_activityFactory.CreateExternalSyncActivity(guid, this.m_activityType, empty, text);
			}
			else
			{
				Guid guid2 = Guid.NewGuid();
				externalSyncActivity = this.m_activityFactory.CreateExternalSyncActivity(guid2, this.m_activityType, guid2, text);
			}
			return externalSyncActivity;
		}

		// Token: 0x0400057A RID: 1402
		private readonly ActivityType m_activityType;

		// Token: 0x0400057B RID: 1403
		private readonly string m_rootActivityParamName;

		// Token: 0x0400057C RID: 1404
		private readonly string m_clientActivityParamName;

		// Token: 0x0400057D RID: 1405
		private readonly ClientActivityContextSource m_source;

		// Token: 0x0400057E RID: 1406
		private readonly IActivityFactory m_activityFactory;
	}
}
