using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000184 RID: 388
	public class ODataMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x000326D1 File Offset: 0x000308D1
		public ODataMediaTypeFormatter(IEnumerable<ODataPayloadKind> payloadKinds)
		{
			if (payloadKinds == null)
			{
				throw Error.ArgumentNull("payloadKinds");
			}
			this._payloadKinds = payloadKinds;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000326EE File Offset: 0x000308EE
		internal ODataMediaTypeFormatter(ODataMediaTypeFormatter formatter, HttpRequestMessage request)
			: base(formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this._payloadKinds = formatter._payloadKinds;
			this.Request = request;
			this.BaseAddressFactory = formatter.BaseAddressFactory;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00032724 File Offset: 0x00030924
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0003272C File Offset: 0x0003092C
		public Func<HttpRequestMessage, Uri> BaseAddressFactory { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00032735 File Offset: 0x00030935
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0003273D File Offset: 0x0003093D
		public HttpRequestMessage Request { get; set; }

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00032746 File Offset: 0x00030946
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			base.GetPerRequestFormatterInstance(type, request, mediaType);
			if (this.Request != null && this.Request == request)
			{
				return this;
			}
			return new ODataMediaTypeFormatter(this, request);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003276C File Offset: 0x0003096C
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = null;
			if (ODataOutputFormatterHelper.TryGetContentHeader(type, mediaType, out mediaTypeHeaderValue))
			{
				headers.ContentType = mediaTypeHeaderValue;
			}
			else
			{
				base.SetDefaultContentHeaders(type, headers, mediaType);
			}
			IEnumerable<string> enumerable = this.Request.Headers.AcceptCharset.Select((StringWithQualityHeaderValue cs) => cs.Value);
			string empty = string.Empty;
			if (ODataOutputFormatterHelper.TryGetCharSet(headers.ContentType, enumerable, out empty))
			{
				headers.ContentType.CharSet = empty;
			}
			headers.TryAddWithoutValidation("OData-Version", ODataUtils.ODataVersionToString(ResultHelpers.GetODataResponseVersion(this.Request)));
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003280C File Offset: 0x00030A0C
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.Request != null)
			{
				ODataDeserializerProvider deserializerProvider = ServiceProviderServiceExtensions.GetRequiredService<ODataDeserializerProvider>(this.Request.GetRequestContainer());
				return ODataInputFormatterHelper.CanReadType(type, this.Request.GetModel(), this.Request.ODataProperties().Path, this._payloadKinds, (IEdmTypeReference objectType) => deserializerProvider.GetEdmTypeDeserializer(objectType), (Type objectType) => deserializerProvider.GetODataDeserializer(objectType, this.Request));
			}
			return false;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003289C File Offset: 0x00030A9C
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.Request != null)
			{
				ODataSerializerProvider serializerProvider = ServiceProviderServiceExtensions.GetRequiredService<ODataSerializerProvider>(this.Request.GetRequestContainer());
				bool flag = false;
				if (type.IsGenericType)
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					Type baseType = TypeHelper.GetBaseType(type);
					flag = genericTypeDefinition == typeof(SingleResult) || baseType == typeof(SingleResult);
				}
				return ODataOutputFormatterHelper.CanWriteType(type, this._payloadKinds, flag, new WebApiRequestMessage(this.Request), (Type objectType) => serializerProvider.GetODataPayloadSerializer(objectType, this.Request));
			}
			return false;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003294C File Offset: 0x00030B4C
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			if (this.Request == null)
			{
				throw Error.InvalidOperation(SRResources.ReadFromStreamAsyncMustHaveRequest, new object[0]);
			}
			object defaultValueForType = MediaTypeFormatter.GetDefaultValueForType(type);
			HttpContentHeaders contentHeaders = ((content == null) ? null : content.Headers);
			if (contentHeaders != null)
			{
				long? contentLength = contentHeaders.ContentLength;
				long num = 0L;
				if (!((contentLength.GetValueOrDefault() == num) & (contentLength != null)))
				{
					Task<object> task;
					try
					{
						Func<ODataDeserializerContext> func = () => new ODataDeserializerContext
						{
							Request = this.Request
						};
						Action<Exception> action = delegate(Exception ex)
						{
							if (formatterLogger == null)
							{
								throw ex;
							}
							formatterLogger.LogError(string.Empty, ex);
						};
						ODataDeserializerProvider deserializerProvider = ServiceProviderServiceExtensions.GetRequiredService<ODataDeserializerProvider>(this.Request.GetRequestContainer());
						task = Task.FromResult<object>(ODataInputFormatterHelper.ReadFromStream(type, defaultValueForType, this.Request.GetModel(), this.GetBaseAddressInternal(this.Request), new WebApiRequestMessage(this.Request), () => ODataMessageWrapperHelper.Create(readStream, contentHeaders, this.Request.GetODataContentIdMapping(), this.Request.GetRequestContainer()), (IEdmTypeReference objectType) => deserializerProvider.GetEdmTypeDeserializer(objectType), (Type objectType) => deserializerProvider.GetODataDeserializer(objectType, this.Request), func, delegate(IDisposable disposable)
						{
							HttpRequestMessageExtensions.RegisterForDispose(this.Request, disposable);
						}, action));
					}
					catch (Exception ex)
					{
						Exception ex2;
						task = TaskHelpers.FromError<object>(ex2);
					}
					return task;
				}
			}
			return Task.FromResult<object>(defaultValueForType);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00032AE0 File Offset: 0x00030CE0
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (this.Request == null)
			{
				throw Error.InvalidOperation(SRResources.WriteToStreamAsyncMustHaveRequest, new object[0]);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled();
			}
			Task task;
			try
			{
				if (HttpRequestMessageExtensions.GetConfiguration(this.Request) == null)
				{
					throw Error.InvalidOperation(SRResources.RequestMustContainConfiguration, new object[0]);
				}
				HttpContentHeaders contentHeaders = ((content == null) ? null : content.Headers);
				UrlHelper urlHelper = HttpRequestMessageExtensions.GetUrlHelper(this.Request) ?? new UrlHelper(this.Request);
				Func<ODataSerializerContext> func = () => new ODataSerializerContext
				{
					Request = this.Request,
					Url = urlHelper
				};
				ODataSerializerProvider serializerProvider = ServiceProviderServiceExtensions.GetRequiredService<ODataSerializerProvider>(this.Request.GetRequestContainer());
				ODataOutputFormatterHelper.WriteToStream(type, value, this.Request.GetModel(), ResultHelpers.GetODataResponseVersion(this.Request), this.GetBaseAddressInternal(this.Request), (contentHeaders == null) ? null : contentHeaders.ContentType, new WebApiUrlHelper(urlHelper), new WebApiRequestMessage(this.Request), new WebApiRequestHeaders(this.Request.Headers), (IServiceProvider services) => ODataMessageWrapperHelper.Create(writeStream, contentHeaders, services), (IEdmTypeReference edmType) => serializerProvider.GetEdmTypeSerializer(edmType), (Type objectType) => serializerProvider.GetODataPayloadSerializer(objectType, this.Request), func);
				task = TaskHelpers.Completed();
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError(ex);
			}
			return task;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00032C80 File Offset: 0x00030E80
		private Uri GetBaseAddressInternal(HttpRequestMessage request)
		{
			if (this.BaseAddressFactory != null)
			{
				return this.BaseAddressFactory(request);
			}
			return ODataMediaTypeFormatter.GetDefaultBaseAddress(request);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00032CA0 File Offset: 0x00030EA0
		public static Uri GetDefaultBaseAddress(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			string text = (HttpRequestMessageExtensions.GetUrlHelper(request) ?? new UrlHelper(request)).CreateODataLink(new ODataPathSegment[0]);
			if (text == null)
			{
				throw new SerializationException(SRResources.UnableToDetermineBaseUrl);
			}
			if (text[text.Length - 1] == '/')
			{
				return new Uri(text);
			}
			return new Uri(text + "/");
		}

		// Token: 0x040003AE RID: 942
		private readonly IEnumerable<ODataPayloadKind> _payloadKinds;
	}
}
