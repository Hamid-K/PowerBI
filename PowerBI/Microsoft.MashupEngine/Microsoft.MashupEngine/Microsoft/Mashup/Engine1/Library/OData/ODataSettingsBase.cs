using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000750 RID: 1872
	internal abstract class ODataSettingsBase
	{
		// Token: 0x0600375A RID: 14170 RVA: 0x000B0EC4 File Offset: 0x000AF0C4
		protected ODataSettingsBase(IEngineHost host, HttpResource resource, Uri uri)
		{
			this.fallbackHandler = new ODataSettingsBasedODataFallbackVersionHandler(host, resource, uri, this);
			this.Cache = new ODataCacheSettings();
			this.ApplyCredentials = true;
			this.ConcurrentRequestsLimit = 16;
			this.ForceConcurrentRequests = false;
			this.RetryPolicy = RetryPolicy.Default;
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000B0F12 File Offset: 0x000AF112
		public ODataFallbackVersionHandler FallbackHandler
		{
			get
			{
				return this.fallbackHandler;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x0600375C RID: 14172 RVA: 0x000B0F1A File Offset: 0x000AF11A
		// (set) Token: 0x0600375D RID: 14173 RVA: 0x000B0F22 File Offset: 0x000AF122
		public bool IsSharePoint
		{
			get
			{
				return this.isSharePoint;
			}
			set
			{
				this.isSharePoint = value;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000B0F2B File Offset: 0x000AF12B
		// (set) Token: 0x0600375F RID: 14175 RVA: 0x000B0F33 File Offset: 0x000AF133
		public ODataServerVersion ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
			set
			{
				this.serverVersion = value;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000B0F3C File Offset: 0x000AF13C
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x000B0F44 File Offset: 0x000AF144
		public ODataCacheSettings Cache { get; set; }

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000B0F4D File Offset: 0x000AF14D
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x000B0F55 File Offset: 0x000AF155
		public bool ApplyCredentials { get; set; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000B0F5E File Offset: 0x000AF15E
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x000B0F66 File Offset: 0x000AF166
		public int ConcurrentRequestsLimit { get; set; }

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000B0F6F File Offset: 0x000AF16F
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x000B0F77 File Offset: 0x000AF177
		public bool ForceConcurrentRequests { get; set; }

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000B0F80 File Offset: 0x000AF180
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000B0F88 File Offset: 0x000AF188
		public RetryPolicy RetryPolicy { get; set; }

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000B0F91 File Offset: 0x000AF191
		public string ProposedServiceDocumentContentTypes
		{
			get
			{
				return ODataSettingsBase.ProposedResultContentTypesLookup[this.ServerVersion];
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000B0FA3 File Offset: 0x000AF1A3
		public string ProposedResultContentTypes
		{
			get
			{
				if (!this.HasEdmModel && this.ServerVersion == ODataServerVersion.V3)
				{
					return "application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";
				}
				return ODataSettingsBase.ProposedResultContentTypesLookup[this.ServerVersion];
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x0600376C RID: 14188
		protected abstract bool HasEdmModel { get; }

		// Token: 0x04001C90 RID: 7312
		private static readonly Dictionary<ODataServerVersion, string> ProposedResultContentTypesLookup = new Dictionary<ODataServerVersion, string>
		{
			{
				ODataServerVersion.All,
				"application/json;odata.metadata=minimal;q=1.0,application/json;odata=minimalmetadata;q=0.9,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7"
			},
			{
				ODataServerVersion.V4,
				"application/json;odata.metadata=minimal"
			},
			{
				ODataServerVersion.V3,
				"application/json;odata=minimalmetadata;q=1.0,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7"
			},
			{
				ODataServerVersion.V2,
				"application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7"
			}
		};

		// Token: 0x04001C91 RID: 7313
		private readonly ODataFallbackVersionHandler fallbackHandler;

		// Token: 0x04001C92 RID: 7314
		private ODataServerVersion serverVersion;

		// Token: 0x04001C93 RID: 7315
		private bool isSharePoint;
	}
}
