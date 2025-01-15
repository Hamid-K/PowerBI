using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DF RID: 479
	internal class WebApiRequestMessage : IWebApiRequestMessage
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003F8D0 File Offset: 0x0003DAD0
		public WebApiRequestMessage(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.innerRequest = request;
			this.Headers = new WebApiRequestHeaders(request.Headers);
			HttpRequestMessageProperties httpRequestMessageProperties = request.ODataProperties();
			if (httpRequestMessageProperties != null)
			{
				this.Context = new WebApiContext(httpRequestMessageProperties);
			}
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			if (configuration != null)
			{
				this.Options = new WebApiOptions(configuration);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003F935 File Offset: 0x0003DB35
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0003F93D File Offset: 0x0003DB3D
		public IWebApiContext Context { get; private set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003F946 File Offset: 0x0003DB46
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0003F94E File Offset: 0x0003DB4E
		public IWebApiHeaders Headers { get; private set; }

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003F957 File Offset: 0x0003DB57
		public bool IsCountRequest()
		{
			return ODataCountMediaTypeMapping.IsCountRequest(this.innerRequest.ODataProperties().Path);
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0003F970 File Offset: 0x0003DB70
		public ODataRequestMethod Method
		{
			get
			{
				bool flag = true;
				ODataRequestMethod odataRequestMethod = ODataRequestMethod.Unknown;
				if (Enum.TryParse<ODataRequestMethod>(this.innerRequest.Method.ToString(), flag, out odataRequestMethod))
				{
					return odataRequestMethod;
				}
				return ODataRequestMethod.Unknown;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0003F99E File Offset: 0x0003DB9E
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0003F9A6 File Offset: 0x0003DBA6
		public IWebApiOptions Options { get; private set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0003F9AF File Offset: 0x0003DBAF
		public IServiceProvider RequestContainer
		{
			get
			{
				return this.innerRequest.GetRequestContainer();
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0003F9BC File Offset: 0x0003DBBC
		public Uri RequestUri
		{
			get
			{
				return this.innerRequest.RequestUri;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0003F9C9 File Offset: 0x0003DBC9
		public ODataDeserializerProvider DeserializerProvider
		{
			get
			{
				return this.innerRequest.GetDeserializerProvider();
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003F9D6 File Offset: 0x0003DBD6
		public Uri GetNextPageLink(int pageSize, object instance = null, Func<object, string> objToSkipTokenValue = null)
		{
			return this.innerRequest.GetNextPageLink(pageSize, instance, objToSkipTokenValue);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public string CreateETag(IDictionary<string, object> properties)
		{
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(this.innerRequest);
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.RequestMustContainConfiguration, new object[0]);
			}
			EntityTagHeaderValue entityTagHeaderValue = configuration.GetETagHandler().CreateETag(properties);
			if (entityTagHeaderValue == null)
			{
				return null;
			}
			return entityTagHeaderValue.ToString();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003FA2D File Offset: 0x0003DC2D
		public ETag GetETag(EntityTagHeaderValue etagHeaderValue)
		{
			return this.innerRequest.GetETag(etagHeaderValue);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003FA3B File Offset: 0x0003DC3B
		public ETag GetETag<TEntity>(EntityTagHeaderValue etagHeaderValue)
		{
			return this.innerRequest.GetETag(etagHeaderValue);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0003FA49 File Offset: 0x0003DC49
		public IDictionary<string, string> ODataContentIdMapping
		{
			get
			{
				return this.innerRequest.GetODataContentIdMapping();
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003FA56 File Offset: 0x0003DC56
		public IODataPathHandler PathHandler
		{
			get
			{
				return this.innerRequest.GetPathHandler();
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003FA64 File Offset: 0x0003DC64
		public IDictionary<string, string> QueryParameters
		{
			get
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (KeyValuePair<string, string> keyValuePair in HttpRequestMessageExtensions.GetQueryNameValuePairs(this.innerRequest))
				{
					if (!dictionary.ContainsKey(keyValuePair.Key))
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				return dictionary;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003FADC File Offset: 0x0003DCDC
		public ODataMessageReaderSettings ReaderSettings
		{
			get
			{
				return this.innerRequest.GetReaderSettings();
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003FAE9 File Offset: 0x0003DCE9
		public ODataMessageWriterSettings WriterSettings
		{
			get
			{
				return this.innerRequest.GetWriterSettings();
			}
		}

		// Token: 0x04000453 RID: 1107
		private HttpRequestMessage innerRequest;
	}
}
