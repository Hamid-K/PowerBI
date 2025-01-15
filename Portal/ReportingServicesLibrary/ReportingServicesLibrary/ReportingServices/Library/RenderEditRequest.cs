using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000190 RID: 400
	internal class RenderEditRequest : IRenderEditSession
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x00035E00 File Offset: 0x00034000
		public RenderEditRequest(string userName, string sessionId)
			: this(userName, sessionId, PowerViewSessionType.RdlxRenderEdit)
		{
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00035E0B File Offset: 0x0003400B
		public string SessionId
		{
			get
			{
				return this.m_sessionId;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00035E13 File Offset: 0x00034013
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00035E1B File Offset: 0x0003401B
		public bool IsPowerView
		{
			get
			{
				return ProgressiveCacheEntry.IsVrmOrDataShape(this.m_sessionType);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00035E28 File Offset: 0x00034028
		public bool IsPowerViewVrm
		{
			get
			{
				return this.m_sessionType == PowerViewSessionType.Vrm;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00035E33 File Offset: 0x00034033
		public PowerViewSessionType SessionType
		{
			get
			{
				return this.m_sessionType;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00035E3B File Offset: 0x0003403B
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00035E43 File Offset: 0x00034043
		public ExternalItemPath ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00035E4C File Offset: 0x0003404C
		public bool IsNewSession
		{
			get
			{
				return string.IsNullOrEmpty(this.m_sessionId);
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00035E5C File Offset: 0x0003405C
		public void EnsureValidSessionExists(string userName, string operationName, out ProgressiveCacheEntry entry)
		{
			entry = null;
			if (this.IsNewSession)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: no session provided", new object[] { operationName });
				throw new SessionNotFoundException(this.SessionId, userName);
			}
			this.ValidateSession();
			if (!ProgressiveExecutionCacheManager.TryGetCacheEntry(this, userName, operationName, out entry))
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Unable to find session", new object[] { operationName });
				throw new SessionNotFoundException(this.SessionId, userName);
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00035ED3 File Offset: 0x000340D3
		public void ValidateSession()
		{
			if (!this.IsSessionIdValid)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Invalid session ID: {0}", new object[] { this.SessionId });
				throw new InvalidSessionIdException(this.SessionId);
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00035F08 File Offset: 0x00034108
		public static RenderEditRequest CreateAndGenerate(string userName)
		{
			return RenderEditRequest.CreateAndGenerate(userName, PowerViewSessionType.RdlxRenderEdit);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00035F11 File Offset: 0x00034111
		public static RenderEditRequest CreateAndGenerate(string userName, PowerViewSessionType sessionType)
		{
			RenderEditRequest renderEditRequest = new RenderEditRequest(userName, null, sessionType);
			renderEditRequest.GenerateSession();
			return renderEditRequest;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00035F22 File Offset: 0x00034122
		public static string EncodeCacheKeyValue(string value)
		{
			RSTrace.CacheTracer.Assert(value != null, "EncodeCacheEntry: value != null");
			return value.Replace("|", "||");
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00035F47 File Offset: 0x00034147
		public RenderEditRequest(string userName, string sessionId, PowerViewSessionType sessionType)
		{
			if (RenderEditRequest.m_affinityPrefix == null)
			{
				RenderEditRequest.m_affinityPrefix = string.Empty;
			}
			this.m_sessionId = sessionId;
			this.m_userName = userName;
			this.m_sessionType = sessionType;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00035F75 File Offset: 0x00034175
		public string GenerateSession()
		{
			this.m_sessionId = RenderEditRequest.m_affinityPrefix + UrlFriendlyUIDGenerator.Create();
			return this.m_sessionId;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00035F92 File Offset: 0x00034192
		public bool IsSessionIdValid
		{
			get
			{
				return this.IsNewSession || (this.m_sessionId.Length > RenderEditRequest.m_affinityPrefix.Length && UrlFriendlyUIDGenerator.IsLegit(this.m_sessionId.Substring(RenderEditRequest.m_affinityPrefix.Length)));
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00035FD1 File Offset: 0x000341D1
		public string MakeCacheKey()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}", this.SessionId, RenderEditRequest.EncodeCacheKeyValue(this.UserName));
		}

		// Token: 0x04000609 RID: 1545
		private string m_sessionId;

		// Token: 0x0400060A RID: 1546
		private readonly string m_userName;

		// Token: 0x0400060B RID: 1547
		private readonly PowerViewSessionType m_sessionType;

		// Token: 0x0400060C RID: 1548
		private ExternalItemPath m_itemPath;

		// Token: 0x0400060D RID: 1549
		private static string m_affinityPrefix;
	}
}
