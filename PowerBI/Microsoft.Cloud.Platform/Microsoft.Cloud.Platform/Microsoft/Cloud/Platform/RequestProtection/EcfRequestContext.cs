using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Claims;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.CommunicationFramework;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200045A RID: 1114
	public class EcfRequestContext : IRequestProtectionContext, IHttpRequestContext, IHttpRequestMessage, IHttpResponseMessage
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x0007E47B File Offset: 0x0007C67B
		public EcfRequestContext(IEventsKitFactory eventsKitFactory)
			: this(eventsKitFactory, RequestProtectionOptions.None)
		{
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0007E488 File Offset: 0x0007C688
		public EcfRequestContext([NotNull] IEventsKitFactory eventsKitFactory, RequestProtectionOptions options)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.m_bottomLevelHandler = new BottomLevelHandler<IEcfListenerEventsKit, string>(EcfRequestContext.s_regularExceptionToMonitoredException, eventsKitFactory.CreateEventsKit<IEcfListenerEventsKit>());
			this.m_webOperationContext = WebOperationContext.Current;
			this.m_operationContext = OperationContext.Current;
			this.Options = options;
			ExtendedDiagnostics.EnsureNotNull<WebOperationContext>(this.m_webOperationContext, "m_webOperationContext");
			ExtendedDiagnostics.EnsureNotNull<OperationContext>(this.m_operationContext, "m_operationContext");
			this.m_operationContext.TryTraceTlsInformation("EcfRequest");
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x0007E509 File Offset: 0x0007C709
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x0007E511 File Offset: 0x0007C711
		public string EndpointIdentifier { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0007E51A File Offset: 0x0007C71A
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x0007E522 File Offset: 0x0007C722
		public AuthenticationResult AuthenticationResult { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x0007E52B File Offset: 0x0007C72B
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x0007E533 File Offset: 0x0007C733
		public AuthorizationResult AuthorizationResult { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0007E53C File Offset: 0x0007C73C
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x0007E544 File Offset: 0x0007C744
		public RequestProtectionOptions Options { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0007E54D File Offset: 0x0007C74D
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x0007E555 File Offset: 0x0007C755
		public IDenialOfServiceProtection<IPAddress> DenialOfServiceProtectionHandle { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0007E55E File Offset: 0x0007C75E
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x0007E566 File Offset: 0x0007C766
		public IRequestBlocker<string> KeyBasedProtectionHandle { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x0007E56F File Offset: 0x0007C76F
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x0007E577 File Offset: 0x0007C777
		public string RequestBlockerKey { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0007E580 File Offset: 0x0007C780
		public IPEndPoint ClientEndpoint
		{
			get
			{
				return this.m_bottomLevelHandler.Run<IPEndPoint>("ClientEndpoint_get", delegate
				{
					RemoteEndpointMessageProperty remoteEndpointMessageProperty = this.m_operationContext.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
					IPAddress ipaddress;
					if (remoteEndpointMessageProperty != null && !string.IsNullOrWhiteSpace(remoteEndpointMessageProperty.Address) && IPAddress.TryParse(remoteEndpointMessageProperty.Address, out ipaddress))
					{
						return new IPEndPoint(ipaddress, remoteEndpointMessageProperty.Port);
					}
					return new IPEndPoint(IPAddress.None, 0);
				});
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0007E59E File Offset: 0x0007C79E
		public string RequestContentType
		{
			get
			{
				return this.m_bottomLevelHandler.Run<string>("RequestContentType_get", delegate
				{
					ExtendedDiagnostics.EnsureNotNull<IncomingWebRequestContext>(this.m_webOperationContext.IncomingRequest, "m_webOperationContext.IncomingRequest");
					return this.m_webOperationContext.IncomingRequest.ContentType;
				});
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0007E5BC File Offset: 0x0007C7BC
		public Uri RequestUri
		{
			get
			{
				return this.m_bottomLevelHandler.Run<Uri>("RequestUri_get", delegate
				{
					ExtendedDiagnostics.EnsureNotNull<MessageHeaders>(this.m_operationContext.IncomingMessageHeaders, "m_operationContext.IncomingMessageHeaders");
					Uri to = this.m_operationContext.IncomingMessageHeaders.To;
					if (to == null)
					{
						throw new HttpListenerContextException("Request uri cannot be null");
					}
					return to;
				});
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0007E5DA File Offset: 0x0007C7DA
		public long RequestLength
		{
			get
			{
				return this.m_bottomLevelHandler.Run<long>("RequestLength_get", delegate
				{
					IEnumerable<string> requestHeader = this.GetRequestHeader("Content-Length");
					if (!requestHeader.Any<string>())
					{
						return -1L;
					}
					return long.Parse(requestHeader.First<string>(), CultureInfo.InvariantCulture);
				});
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0007E5F8 File Offset: 0x0007C7F8
		public IDictionary<string, IEnumerable<string>> RequestHeaders
		{
			get
			{
				if (this.m_requestHeadersCache == null)
				{
					Dictionary<string, IEnumerable<string>> dictionary = this.m_bottomLevelHandler.Run<Dictionary<string, IEnumerable<string>>>("RequestHeaders_get", delegate
					{
						HttpRequestMessageProperty httpRequestMessageProperty = this.m_operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
						ExtendedDiagnostics.EnsureNotNull<HttpRequestMessageProperty>(httpRequestMessageProperty, "requestMessageProperty");
						return httpRequestMessageProperty.Headers.ToDictionaryRepresentation();
					});
					this.m_requestHeadersCache = dictionary;
				}
				return this.m_requestHeadersCache;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0007E637 File Offset: 0x0007C837
		public HttpMethod Method
		{
			get
			{
				return this.m_bottomLevelHandler.Run<HttpMethod>("Method_get", delegate
				{
					HttpRequestMessageProperty httpRequestMessageProperty = this.m_operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
					ExtendedDiagnostics.EnsureNotNull<HttpRequestMessageProperty>(httpRequestMessageProperty, "requestMessageProperty");
					HttpMethod httpMethod;
					if (Enum.TryParse<HttpMethod>(httpRequestMessageProperty.Method, true, out httpMethod))
					{
						return httpMethod;
					}
					throw new CommunicationFrameworkInvalidOperationContextException("Could not parse http request method to HttpMethod enum. The method on the request is {0}".FormatWithInvariantCulture(new object[] { httpRequestMessageProperty.Method ?? "null" }));
				});
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0007E658 File Offset: 0x0007C858
		public IDictionary<string, IEnumerable<string>> QueryString
		{
			get
			{
				if (this.m_queryStringCache == null)
				{
					Dictionary<string, IEnumerable<string>> dictionary = this.m_bottomLevelHandler.Run<Dictionary<string, IEnumerable<string>>>("QueryString_get", () => this.m_webOperationContext.IncomingRequest.UriTemplateMatch.QueryParameters.ToDictionaryRepresentation());
					this.m_queryStringCache = dictionary;
				}
				return this.m_queryStringCache;
			}
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007E698 File Offset: 0x0007C898
		public IDictionary<string, IEnumerable<string>> GetFormData()
		{
			if (this.m_formDataCache == null)
			{
				if (this.RequestContentType.Equals("multipart/form-data", StringComparison.OrdinalIgnoreCase) || this.RequestContentType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
				{
					if (this.RequestLength > EcfRequestContext.s_maxFormContentLengthInBytes || this.RequestLength == -1L)
					{
						throw new InvalidOperationException("Form data is limited to {0}KB, current length: {1}KB".FormatWithInvariantCulture(new object[]
						{
							ExtendedMath.BytesToKB(EcfRequestContext.s_maxFormContentLengthInBytes),
							ExtendedMath.BytesToKB(this.RequestLength)
						}));
					}
					byte[] array = Convert.FromBase64String(this.m_operationContext.RequestContext.RequestMessage.GetReaderAtBodyContents().ReadInnerXml());
					this.m_formDataCache = HttpUtility.ParseQueryString(Encoding.UTF8.GetString(array), Encoding.UTF8).ToDictionaryRepresentation();
				}
				else
				{
					this.m_formDataCache = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);
				}
			}
			return this.m_formDataCache;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007E784 File Offset: 0x0007C984
		public IAsyncResult BeginGetClientCertificate(AsyncCallback asyncCallback, object state)
		{
			AsyncResult<X509Certificate2> asyncResult = new AsyncResult<X509Certificate2>(asyncCallback, state);
			return this.m_bottomLevelHandler.Run<AsyncResult<X509Certificate2>>("BeginGetClientCertificate", delegate
			{
				X509CertificateClaimSet x509CertificateClaimSet = this.m_operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets.OfType<X509CertificateClaimSet>().FirstOrDefault<X509CertificateClaimSet>();
				X509Certificate2 x509Certificate = ((x509CertificateClaimSet != null && x509CertificateClaimSet.X509Certificate != null) ? x509CertificateClaimSet.X509Certificate : null);
				asyncResult.SignalCompletion(true, x509Certificate);
				return asyncResult;
			});
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0007E7C8 File Offset: 0x0007C9C8
		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			return this.m_bottomLevelHandler.Run<X509Certificate2>("EndGetClientCertificate", () => (asyncResult as AsyncResult<X509Certificate2>).End());
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0007E800 File Offset: 0x0007CA00
		public IEnumerable<string> GetRequestHeader(string headerKey)
		{
			return this.m_bottomLevelHandler.Run<IEnumerable<string>>("GetRequestHeader for key {0}".FormatWithInvariantCulture(new object[] { headerKey }), delegate
			{
				IEnumerable<string> enumerable;
				this.RequestHeaders.TryGetValue(headerKey, out enumerable);
				return enumerable ?? new string[0];
			});
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0007E851 File Offset: 0x0007CA51
		// (set) Token: 0x060022D1 RID: 8913 RVA: 0x0007E874 File Offset: 0x0007CA74
		public long ResponseContentLength
		{
			get
			{
				long num = 0L;
				this.m_bottomLevelHandler.Run<long>("ResponseContentLength_get", () => this.m_webOperationContext.OutgoingResponse.ContentLength);
				return num;
			}
			set
			{
				this.m_bottomLevelHandler.Run<long>("ResponseContentLength_set", () => this.m_webOperationContext.OutgoingResponse.ContentLength = value);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0007E8B4 File Offset: 0x0007CAB4
		// (set) Token: 0x060022D3 RID: 8915 RVA: 0x0007E8F8 File Offset: 0x0007CAF8
		public string ResponseContentType
		{
			get
			{
				string contentType = null;
				this.m_bottomLevelHandler.Run<string>("ResponseContentType_get", () => contentType = this.m_webOperationContext.OutgoingResponse.ContentType);
				return contentType;
			}
			set
			{
				this.m_bottomLevelHandler.Run<string>("ResponseContentType_set", () => this.m_webOperationContext.OutgoingResponse.ContentType = value);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0007E936 File Offset: 0x0007CB36
		// (set) Token: 0x060022D5 RID: 8917 RVA: 0x0007E954 File Offset: 0x0007CB54
		public HttpStatusCode ResponseStatusCode
		{
			get
			{
				return this.m_bottomLevelHandler.Run<HttpStatusCode>("ResponseStatusCode_get", () => this.m_webOperationContext.OutgoingResponse.StatusCode);
			}
			set
			{
				this.m_bottomLevelHandler.Run<HttpStatusCode>("ResponseStatusCode_set", () => this.m_webOperationContext.OutgoingResponse.StatusCode = value);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0007E992 File Offset: 0x0007CB92
		// (set) Token: 0x060022D7 RID: 8919 RVA: 0x0007E9B0 File Offset: 0x0007CBB0
		public string ResponseLocation
		{
			get
			{
				return this.m_bottomLevelHandler.Run<string>("ResponseLocation_get", () => this.m_webOperationContext.OutgoingResponse.Location);
			}
			set
			{
				this.m_bottomLevelHandler.Run<string>("ResponseLocation_set", () => this.m_webOperationContext.OutgoingResponse.Location = value);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0007E9EE File Offset: 0x0007CBEE
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x0007EA0C File Offset: 0x0007CC0C
		public string ResponseStatusDescription
		{
			get
			{
				return this.m_bottomLevelHandler.Run<string>("ResponseStatusDescription_get", () => this.m_webOperationContext.OutgoingResponse.StatusDescription);
			}
			set
			{
				this.m_bottomLevelHandler.Run<string>("ResponseStatusDescription_set", () => this.m_webOperationContext.OutgoingResponse.StatusDescription = value);
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007EA4C File Offset: 0x0007CC4C
		public void SetResponseHeader(string headerKey, string headerValue)
		{
			this.m_bottomLevelHandler.Run<string>("SetResponseHeader", () => this.m_webOperationContext.OutgoingResponse.Headers[headerKey] = headerValue);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007EA94 File Offset: 0x0007CC94
		public void AddResponseHeader(string headerKey, string headerValue)
		{
			this.m_bottomLevelHandler.Run("AddResponseHeader", delegate
			{
				this.m_webOperationContext.OutgoingResponse.Headers.Add(headerKey, headerValue);
			});
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x0007EAD8 File Offset: 0x0007CCD8
		public Uri RequestBaseUri
		{
			get
			{
				return this.m_bottomLevelHandler.Run<Uri>("RequestBaseUri_get", () => this.m_webOperationContext.IncomingRequest.UriTemplateMatch.BaseUri);
			}
		}

		// Token: 0x04000C17 RID: 3095
		private static long s_maxFormContentLengthInBytes = ExtendedMath.KBtoBytes(16L);

		// Token: 0x04000C18 RID: 3096
		private BottomLevelHandler<IEcfListenerEventsKit, string> m_bottomLevelHandler;

		// Token: 0x04000C19 RID: 3097
		private WebOperationContext m_webOperationContext;

		// Token: 0x04000C1A RID: 3098
		private OperationContext m_operationContext;

		// Token: 0x04000C1B RID: 3099
		private Dictionary<string, IEnumerable<string>> m_requestHeadersCache;

		// Token: 0x04000C1C RID: 3100
		private Dictionary<string, IEnumerable<string>> m_queryStringCache;

		// Token: 0x04000C1D RID: 3101
		private Dictionary<string, IEnumerable<string>> m_formDataCache;

		// Token: 0x04000C1E RID: 3102
		private static readonly Dictionary<Type, Action<Exception, IEcfListenerEventsKit, string>> s_regularExceptionToMonitoredException = new Dictionary<Type, Action<Exception, IEcfListenerEventsKit, string>>
		{
			{
				typeof(ObjectDisposedException),
				delegate(Exception ex, IEcfListenerEventsKit eventsKit, string operationName)
				{
					CommunicationFrameworkInvalidOperationContextException ex2 = new CommunicationFrameworkInvalidOperationContextException(operationName, string.Empty, ex);
					eventsKit.NotifyInvalidOperationContext(ex2);
					throw ex2;
				}
			},
			{
				typeof(InvalidOperationException),
				delegate(Exception ex, IEcfListenerEventsKit eventsKit, string operationName)
				{
					CommunicationFrameworkInvalidOperationContextException ex3 = new CommunicationFrameworkInvalidOperationContextException(operationName, string.Empty, ex);
					eventsKit.NotifyInvalidOperationContext(ex3);
					throw ex3;
				}
			}
		};
	}
}
