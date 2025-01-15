using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000732 RID: 1842
	internal abstract class ODataFallbackVersionHandler
	{
		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x060036E0 RID: 14048
		// (set) Token: 0x060036E1 RID: 14049
		public abstract ODataServerVersion ServerVersion { get; set; }

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000AEFAA File Offset: 0x000AD1AA
		// (set) Token: 0x060036E3 RID: 14051 RVA: 0x000AEFB2 File Offset: 0x000AD1B2
		public Dictionary<ODataServerVersion, WebException> Errors { get; protected set; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000AEFBB File Offset: 0x000AD1BB
		// (set) Token: 0x060036E5 RID: 14053 RVA: 0x000AEFC3 File Offset: 0x000AD1C3
		public IEngineHost Host { get; private set; }

		// Token: 0x060036E6 RID: 14054 RVA: 0x000AEFCC File Offset: 0x000AD1CC
		protected ODataFallbackVersionHandler(IEngineHost host, HttpResource resource, Uri uri)
		{
			this.Host = host;
			this.resource = resource;
			this.uri = uri;
			this.Errors = new Dictionary<ODataServerVersion, WebException>();
			this.canTryOlderVersion = true;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000AEFFC File Offset: 0x000AD1FC
		public T HandleVersionFallback<T>(Func<T> function, bool throwOnBadRequest = false)
		{
			T t;
			for (;;)
			{
				try
				{
					t = function();
				}
				catch (ODataFallbackVersionHandler.TryAnotherVersionException ex)
				{
					if (this.TryLowerVersion(null))
					{
						continue;
					}
					throw ex.throwIfNotPossible;
				}
				catch (WebException ex2)
				{
					if (throwOnBadRequest && HttpResponseHandler.IsBadRequestError(ex2))
					{
						throw;
					}
					this.Errors[this.ServerVersion] = ex2;
					MashupHttpWebResponse mashupHttpWebResponse;
					if (this.IsFallbackException(ex2, out mashupHttpWebResponse) && this.TryLowerVersion(mashupHttpWebResponse))
					{
						continue;
					}
					throw this.GetErrorMessage();
				}
				break;
			}
			return t;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000AF080 File Offset: 0x000AD280
		public void FallbackToOlderVersionIfPossible(Exception throwIfNotPossible)
		{
			if (this.canTryOlderVersion && this.ServerVersion != ODataServerVersion.V2)
			{
				throw new ODataFallbackVersionHandler.TryAnotherVersionException();
			}
			throw throwIfNotPossible;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000AF099 File Offset: 0x000AD299
		public void FallbackToOlderVersion(ValueException throwIfNotPossible)
		{
			throw new ODataFallbackVersionHandler.TryAnotherVersionException(throwIfNotPossible);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000AF0A4 File Offset: 0x000AD2A4
		private bool TryLowerVersion(MashupHttpWebResponse exceptionResponse = null)
		{
			if (exceptionResponse != null)
			{
				if (exceptionResponse.Headers.AllKeys.FirstOrDefault((string key) => key.Equals("OData-Version")) != null)
				{
					if (this.ServerVersion != ODataServerVersion.V4)
					{
						this.ServerVersion = ODataServerVersion.V4;
						this.canTryOlderVersion = false;
						return true;
					}
					return false;
				}
			}
			switch (this.ServerVersion)
			{
			case ODataServerVersion.V3:
				this.ServerVersion = ODataServerVersion.V2;
				return true;
			case ODataServerVersion.V4:
				this.ServerVersion = ODataServerVersion.V3;
				return true;
			case ODataServerVersion.All:
				this.ServerVersion = ODataServerVersion.V4;
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000AF13C File Offset: 0x000AD33C
		private bool IsFallbackException(WebException e, out MashupHttpWebResponse exceptionResponse)
		{
			exceptionResponse = e.Response as MashupHttpWebResponse;
			if (exceptionResponse == null)
			{
				return false;
			}
			if (this.ServerVersion == ODataServerVersion.V3)
			{
				return exceptionResponse.StatusCode == HttpStatusCode.NotAcceptable || exceptionResponse.StatusCode == HttpStatusCode.UnsupportedMediaType;
			}
			return exceptionResponse.StatusCode == HttpStatusCode.NotAcceptable || exceptionResponse.StatusCode == HttpStatusCode.UnsupportedMediaType || exceptionResponse.StatusCode == HttpStatusCode.BadRequest || exceptionResponse.StatusCode == HttpStatusCode.NotFound || exceptionResponse.StatusCode == HttpStatusCode.InternalServerError;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000AF1CC File Offset: 0x000AD3CC
		private ValueException GetErrorMessage()
		{
			WebException value = this.Errors.ElementAt(0).Value;
			Uri uri = ((value.Response == null) ? this.uri : value.Response.ResponseUri);
			if (this.Errors.Count == 1)
			{
				throw ODataCommonErrors.RequestFailed(this.Host, value, uri, this.resource);
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			foreach (KeyValuePair<ODataServerVersion, WebException> keyValuePair in this.Errors)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(Strings.RequestFailedUsingDifferentVersions(this.GetVersionString(keyValuePair.Key), ODataCommonErrors.ExtractErrorMessage(this.Host, this.resource.Resource, keyValuePair.Value)));
				string text;
				if (ODataCommonErrors.TryExtractSharePointCorrelationID(keyValuePair.Value, out text))
				{
					if (stringBuilder2.Length > 0)
					{
						stringBuilder2.Append(", ");
					}
					stringBuilder2.Append(text);
				}
			}
			if (stringBuilder2.Length > 0)
			{
				list.Add(new RecordKeyDefinition("SPRequestGuid", TextValue.New(stringBuilder2.ToString()), TypeValue.Text));
			}
			return DataSourceException.NewDataSourceError<Message2>(this.Host, Strings.RequestFailedWithoutStatusCode(this.resource, stringBuilder.ToString()), this.resource.Resource, list, null);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000AF348 File Offset: 0x000AD548
		private string GetVersionString(ODataServerVersion version)
		{
			switch (version)
			{
			case ODataServerVersion.V2:
				return Strings.ODataVersion2;
			case ODataServerVersion.V3:
				return Strings.ODataVersion3;
			case ODataServerVersion.V4:
				return Strings.ODataVersion4;
			case ODataServerVersion.All:
				return Strings.ODataVersion3And4;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04001C34 RID: 7220
		private readonly HttpResource resource;

		// Token: 0x04001C35 RID: 7221
		private readonly Uri uri;

		// Token: 0x04001C36 RID: 7222
		private bool canTryOlderVersion;

		// Token: 0x02000733 RID: 1843
		[Serializable]
		protected class TryAnotherVersionException : Exception
		{
			// Token: 0x060036EE RID: 14062 RVA: 0x00005F33 File Offset: 0x00004133
			public TryAnotherVersionException()
			{
			}

			// Token: 0x060036EF RID: 14063 RVA: 0x000AF39E File Offset: 0x000AD59E
			public TryAnotherVersionException(ValueException throwIfNotPossible)
			{
				this.throwIfNotPossible = throwIfNotPossible;
			}

			// Token: 0x060036F0 RID: 14064 RVA: 0x00005F45 File Offset: 0x00004145
			protected TryAnotherVersionException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			// Token: 0x04001C37 RID: 7223
			public readonly ValueException throwIfNotPossible;
		}
	}
}
