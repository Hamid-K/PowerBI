using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient.Authentication;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.MsoId;
using Microsoft.AnalysisServices.AdomdClient.Runtime;
using Microsoft.AnalysisServices.AdomdClient.Security;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000028 RID: 40
	internal class HttpStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		private HttpStream(HttpStream.HttpChannelController controller, ConnectionInfo info, Uri dataSource, UserContext context, string coreServer, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType, int readTimeout)
			: base(false, desiredRequestType, desiredResponseType)
		{
			this.controller = controller;
			this.info = info;
			this.dataSource = dataSource;
			this.userContext = context;
			this.defaultHeaders = this.controller.GetDefaultStreamHeaders(this.info, this.dataSource, coreServer);
			this.ReadTimeout = readTimeout;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000C048 File Offset: 0x0000A248
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000C04B File Offset: 0x0000A24B
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000C053 File Offset: 0x0000A253
		public override int ReadTimeout { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000C05C File Offset: 0x0000A25C
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000C064 File Offset: 0x0000A264
		internal XmlaStreamException StreamException { get; private set; }

		// Token: 0x0600027F RID: 639 RVA: 0x0000C070 File Offset: 0x0000A270
		public static HttpStream Create(IConnectivityOwner owner, ConnectionInfo info, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType, int readTimeout, out bool isSessionTokenNeeded)
		{
			CookieContainer cookieContainer = new CookieContainer();
			Uri uri;
			if (info.IsForRedirector)
			{
				bool flag = !string.IsNullOrEmpty(info.DataSourceVersion);
				string text;
				string text2;
				string text3;
				HttpStream.LegacyController.GetSandboxDatabaseInformation(info.GetRedirectorUrlForDatabase(), info.UserID, info.Password, flag, cookieContainer, out text, out text2, out text3);
				if (!flag)
				{
					info.DataSourceVersion = text3;
				}
				info.SetCatalog(text2);
				uri = new Uri(info.GetRedirectorUrlForRedirect(text, flag));
			}
			else
			{
				uri = info.DatasourceUriInitializeHelper();
			}
			string text4 = null;
			if (info.IsInternalPaaSInfrastructure && info.CanBuildExternalAuthenticationHandle(owner))
			{
				info.BuildExternalAuthenticationHandle(owner);
			}
			if (info.IsPaaSInfrastructure)
			{
				if (info.IsWorkloadDirectConnection)
				{
					text4 = info.PBIPVirtualServiceName;
					info.BuildExternalAuthenticationHandle(owner);
				}
				else
				{
					text4 = info.AcquireAADTokenAndResolveHTTPConnectionPropertiesForPaaSInfrastructure(owner, ref uri);
				}
				if (info.IsPaaSInfrastructure && string.IsNullOrEmpty(text4))
				{
					throw new InvalidDataException("Failed to resolve the core XMLA server");
				}
			}
			else if (info.IsPbiDataset && info.AuthHandle == null)
			{
				info.BuildExternalAuthenticationHandle(owner);
			}
			HttpStream.HttpChannelController httpChannelController;
			UserContext userContext;
			HttpStream.HttpChannelManager.GetControllerAndUserContext(info, cookieContainer, out httpChannelController, out userContext);
			isSessionTokenNeeded = info.ClientCertificateThumbprint != null || info.IntegratedSecurity == IntegratedSecurity.Federated;
			return new HttpStream(httpChannelController, info, uri, userContext, text4, desiredRequestType, desiredResponseType, readTimeout);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C190 File Offset: 0x0000A390
		public override void WriteSoapActionHeader(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				this.soapActionHeader = null;
				this.soapActionValue = null;
				return;
			}
			int num = action.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException("action");
			}
			this.soapActionHeader = action.Substring(0, num);
			this.soapActionValue = action.Substring(num + 1);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.EnsureOperationIsReadyForWrite();
				this.operation.WriteRequestPayload(buffer, offset, size);
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (SecurityException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			catch (ProtocolViolationException ex6)
			{
				throw new XmlaStreamException(ex6);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.EnsureOperationIsReadyForWrite();
				this.operation.CompleteRequest();
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C33C File Offset: 0x0000A53C
		public override XmlaDataType GetResponseDataType()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			XmlaDataType responseDataType;
			try
			{
				bool flag;
				this.EnsureOperationIsReadyForRead(out flag);
				responseDataType = this.operation.GetResponseDataType();
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (ProtocolViolationException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			return responseDataType;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			int num;
			try
			{
				bool flag;
				this.EnsureOperationIsReadyForRead(out flag);
				if (flag)
				{
					num = 0;
				}
				else
				{
					num = this.operation.ReadResponsePayload(buffer, offset, size);
				}
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
			catch (WebException ex2)
			{
				throw new XmlaStreamException(ex2);
			}
			catch (HttpRequestException ex3)
			{
				throw new XmlaStreamException(ex3);
			}
			catch (TaskCanceledException ex4)
			{
				throw new XmlaStreamException(ex4);
			}
			catch (ProtocolViolationException ex5)
			{
				throw new XmlaStreamException(ex5);
			}
			return num;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public override string GetExtendedErrorInfo()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.operation == null)
			{
				return string.Empty;
			}
			return this.operation.GetExtendedErrorInfo();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				if (this.operation != null)
				{
					this.operation.Dispose();
					this.operation = null;
				}
				this.StreamException = null;
			}
			catch (XmlaStreamException)
			{
				throw;
			}
			catch (IOException ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C564 File Offset: 0x0000A764
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C575 File Offset: 0x0000A775
		public override void Close()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C578 File Offset: 0x0000A778
		public override void Dispose()
		{
			try
			{
				if (this.operation != null)
				{
					this.operation.Dispose();
					this.operation = null;
				}
				HttpStream.HttpChannelManager.ReleaseController(this.controller);
				this.userContext.Dispose();
			}
			catch (IOException)
			{
			}
			catch (WebException)
			{
			}
			catch (HttpRequestException)
			{
			}
			catch (TaskCanceledException)
			{
			}
			catch (ProtocolViolationException)
			{
			}
			catch (SecurityException)
			{
			}
			finally
			{
				base.Dispose(true);
				this.disposed = true;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C630 File Offset: 0x0000A830
		private void EnsureOperationIsReadyForWrite()
		{
			if (this.operation != null)
			{
				switch (this.operation.Status)
				{
				case HttpStream.HttpXmlaOperationStatus.Request:
					return;
				case HttpStream.HttpXmlaOperationStatus.Response:
				case HttpStream.HttpXmlaOperationStatus.Completed:
				case HttpStream.HttpXmlaOperationStatus.Error:
					this.operation.Dispose();
					break;
				}
			}
			this.operation = this.controller.StartNewOperation(this);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C68C File Offset: 0x0000A88C
		private void EnsureOperationIsReadyForRead(out bool isCompleted)
		{
			if (this.operation == null)
			{
				throw new InvalidOperationException("The stream does not have an active operation!");
			}
			HttpStream.HttpXmlaOperationStatus status = this.operation.Status;
			isCompleted = status == HttpStream.HttpXmlaOperationStatus.Completed;
			bool flag = false;
			switch (status)
			{
			case HttpStream.HttpXmlaOperationStatus.Request:
			case HttpStream.HttpXmlaOperationStatus.Error:
				this.operation.Dispose();
				this.operation = null;
				break;
			case HttpStream.HttpXmlaOperationStatus.Response:
			case HttpStream.HttpXmlaOperationStatus.Completed:
				flag = true;
				break;
			}
			if (!flag)
			{
				throw new XmlaStreamException(XmlaSR.HttpStream_InvalidReadRequest(status.ToString()));
			}
		}

		// Token: 0x040001F3 RID: 499
		private const string ContentTypeParameter = "content_type";

		// Token: 0x040001F4 RID: 500
		protected static readonly Regex contentTypeRegex = new Regex(string.Format("^(\\s*)(?<{0}>(({1})|({2})|({3})|({4})))(\\s*)((;(.*))|)(\\s*)\\z", new object[]
		{
			"content_type",
			"text/xml".Replace("+", "\\+"),
			"application/xml+xpress".Replace("+", "\\+"),
			"application/sx".Replace("+", "\\+"),
			"application/sx+xpress".Replace("+", "\\+")
		}), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001F5 RID: 501
		private readonly HttpStream.HttpChannelController controller;

		// Token: 0x040001F6 RID: 502
		private readonly ConnectionInfo info;

		// Token: 0x040001F7 RID: 503
		private readonly Uri dataSource;

		// Token: 0x040001F8 RID: 504
		private readonly UserContext userContext;

		// Token: 0x040001F9 RID: 505
		private readonly IDictionary<string, string> defaultHeaders;

		// Token: 0x040001FA RID: 506
		private string soapActionHeader;

		// Token: 0x040001FB RID: 507
		private string soapActionValue;

		// Token: 0x040001FC RID: 508
		private bool outdatedVersion;

		// Token: 0x040001FD RID: 509
		private HttpStream.HttpXmlaOperation operation;

		// Token: 0x02000181 RID: 385
		private abstract class HttpChannelController : Disposable
		{
			// Token: 0x060011A7 RID: 4519 RVA: 0x0003CECC File Offset: 0x0003B0CC
			protected HttpChannelController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
			{
				this.options = options;
				this.cookieContainer = cookieContainer;
				if (!string.IsNullOrEmpty(options.CertificateThumbprint))
				{
					this.clientCertificate = CertUtils.LoadCertificateByThumbprint(options.CertificateThumbprint, true);
				}
				bool flag;
				this.InitializeHttpClient(out flag);
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x0003CF16 File Offset: 0x0003B116
			private protected HttpChannelController(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.options = HttpStream.ChannelOptions.DeserializeFromGlobalObject(elements, 0);
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0003CF34 File Offset: 0x0003B134
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.options.Mode;
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x060011AA RID: 4522 RVA: 0x0003CF41 File Offset: 0x0003B141
			internal string CacheKey
			{
				get
				{
					return this.options.CacheKey;
				}
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x0003CF50 File Offset: 0x0003B150
			public IDictionary<string, string> GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer)
			{
				base.ThrowIfAlreadyDisposed();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				this.GetDefaultStreamHeaders(info, dataSource, coreServer, dictionary);
				return dictionary;
			}

			// Token: 0x060011AC RID: 4524 RVA: 0x0003CF74 File Offset: 0x0003B174
			public HttpStream.HttpXmlaOperation StartNewOperation(HttpStream stream)
			{
				base.ThrowIfAlreadyDisposed();
				bool flag;
				switch (stream.info.HttpHandling)
				{
				case HttpChannelHandling.Default:
					flag = ClientFeaturesManager.CheckIfHttpClientIsSupportedByDefault() && this.httpClient != null && stream.ReadTimeout == this.options.DefaultTimeoutInMiliseconds;
					break;
				case HttpChannelHandling.WebRequestBased:
					flag = false;
					break;
				case HttpChannelHandling.PreferHttpClient:
					flag = this.httpClient != null && stream.ReadTimeout == this.options.DefaultTimeoutInMiliseconds;
					break;
				default:
					flag = false;
					break;
				}
				if (flag && this.options.Credentials != null)
				{
					flag = false;
				}
				HttpStream.HttpXmlaOperation httpXmlaOperation = this.CreateNewOperation(stream, flag);
				httpXmlaOperation.StartRequest();
				return httpXmlaOperation;
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x0003D016 File Offset: 0x0003B216
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.httpClient != null)
				{
					this.httpClient.Dispose();
					this.httpClient = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x0003D03C File Offset: 0x0003B23C
			protected virtual void GetDefaultStreamHeaders(ConnectionInfo info, Uri webSite, string coreServer, IDictionary<string, string> headers)
			{
				headers.Add("User-Agent", "ADOMD.NET");
				if (!info.IsInternalPaaSInfrastructure)
				{
					headers.Add("X-AS-AcquireTokenStats", "AppName=");
				}
				if (info.IsWorkloadDirectConnection)
				{
					headers.Add("x-ms-workload-directconnection", "1");
				}
			}

			// Token: 0x060011AF RID: 4527 RVA: 0x0003D08C File Offset: 0x0003B28C
			protected virtual void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				headers.Add(new KeyValuePair<string, string>("Content-Type", DataTypes.GetDataTypeFromEnum(owner.GetRequestDataType())));
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x0003D0A9 File Offset: 0x0003B2A9
			protected virtual void HandleResponseStatusCode(int statusCode, Exception ex)
			{
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x0003D0AB File Offset: 0x0003B2AB
			protected virtual void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
			}

			// Token: 0x060011B2 RID: 4530
			protected abstract HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient);

			// Token: 0x060011B3 RID: 4531
			protected abstract HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception);

			// Token: 0x060011B4 RID: 4532 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
			internal HttpWebRequest StartWebRequestBasedOperation(HttpStream owner, object context, out Stream requestStream)
			{
				HttpWebRequest request = this.PrepareWebRequestImpl(owner, context);
				requestStream = owner.userContext.ExecuteInUserContext<Stream>(() => new BufferedStream(request.GetRequestStream(), ClientFeaturesManager.GetHttpStreamBufferSize()));
				return request;
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x0003D0F0 File Offset: 0x0003B2F0
			internal bool CompleteWebRequestBasedOperation(HttpStream owner, object context, HttpWebRequest request, out HttpWebResponse response, out Stream responseStream)
			{
				response = this.GetWebResponseImpl(owner, context, request);
				if (response == null)
				{
					responseStream = null;
					return false;
				}
				this.ProcessWebResponse(owner, context, new HttpStream.HttpChannelController.WebResponseInfo(response));
				responseStream = new BufferedStream(response.GetResponseStream(), ClientFeaturesManager.GetHttpStreamBufferSize());
				return true;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x0003D12F File Offset: 0x0003B32F
			internal HttpRequestMessage StartHttpClientBasedOperation(HttpStream owner, object context, CancellationTokenSource cts, out Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				requestStream = new HttpStream.HttpChannelController.RequestPayloadStream(ClientFeaturesManager.GetHttpClientPayloadQueueLimit(), ClientFeaturesManager.GetHttpStreamBufferSize());
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, requestStream, out pendingResponse);
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x0003D151 File Offset: 0x0003B351
			internal HttpRequestMessage StartHttpClientBasedOperationWithoutPayload(HttpStream owner, object context, CancellationTokenSource cts, out Task<HttpResponseMessage> pendingResponse)
			{
				return this.StartHttpClientBasedOperationImpl(owner, context, cts, null, out pendingResponse);
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x0003D160 File Offset: 0x0003B360
			internal Stream CompleteHttpClientBasedOperation(HttpStream owner, object context, Task<HttpResponseMessage> pendingResponse)
			{
				HttpResponseMessage responseMessageImpl = this.GetResponseMessageImpl(owner, context, pendingResponse);
				if (responseMessageImpl == null)
				{
					return null;
				}
				this.ProcessWebResponse(owner, context, new HttpStream.HttpChannelController.WebResponseInfo(responseMessageImpl));
				return new BufferedStream(responseMessageImpl.GetResponseContent(), ClientFeaturesManager.GetHttpStreamBufferSize());
			}

			// Token: 0x060011B9 RID: 4537 RVA: 0x0003D19A File Offset: 0x0003B39A
			private protected virtual void SerializeToGlobalObject(IList<object> elements)
			{
				this.options.SerializeToGlobalObject(elements);
				elements.Add(this.cookieContainer);
				elements.Add(this.clientCertificate);
				elements.Add(this.httpClient);
			}

			// Token: 0x060011BA RID: 4538 RVA: 0x0003D1CC File Offset: 0x0003B3CC
			private protected virtual void RefreshFromGlobalObject(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				this.RefreshFromGlobalObjectImpl(elements, out offest, out hasElementsUpdate);
			}

			// Token: 0x060011BB RID: 4539 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
			internal IList<object> ToGlobalObject()
			{
				IList<object> list = new List<object>(16);
				this.SerializeToGlobalObject(list);
				return list;
			}

			// Token: 0x060011BC RID: 4540 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
			internal void RefreshFromGlobalObject(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				this.RefreshFromGlobalObject(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011BD RID: 4541 RVA: 0x0003D210 File Offset: 0x0003B410
			private static bool AreNetCoreClientsLoaded()
			{
				if (HttpStream.HttpChannelController.netCoreClientsLoaded != null)
				{
					return HttpStream.HttpChannelController.netCoreClientsLoaded.Value;
				}
				if (!FrameworkRuntimeHelper.IsNetCoreDomain)
				{
					HttpStream.HttpChannelController.netCoreClientsLoaded = new bool?(false);
					return false;
				}
				if (FrameworkRuntimeHelper.IsAssemblyLoaded("Microsoft.AnalysisServices.Runtime.Core", null, null, "89845dcd8080cc91"))
				{
					HttpStream.HttpChannelController.netCoreClientsLoaded = new bool?(true);
					return true;
				}
				return false;
			}

			// Token: 0x060011BE RID: 4542 RVA: 0x0003D26C File Offset: 0x0003B46C
			private void RefreshFromGlobalObjectImpl(IList<object> elements, out int offest, out bool hasElementsUpdate)
			{
				offest = 6;
				int num = offest;
				offest = num + 1;
				this.cookieContainer = (CookieContainer)elements[num];
				num = offest;
				offest = num + 1;
				this.clientCertificate = (X509Certificate2)elements[num];
				num = offest;
				offest = num + 1;
				this.httpClient = (HttpClient)elements[num];
				this.InitializeHttpClient(out hasElementsUpdate);
			}

			// Token: 0x060011BF RID: 4543 RVA: 0x0003D2D4 File Offset: 0x0003B4D4
			private void InitializeHttpClient(out bool wasNewClientCreated)
			{
				if (this.httpClient == null)
				{
					bool flag = true;
					if (flag && HttpStream.HttpChannelController.AreNetCoreClientsLoaded())
					{
						flag = false;
					}
					if (flag)
					{
						WebRequestHandler webRequestHandler = new WebRequestHandler
						{
							AllowAutoRedirect = this.options.AllowAutoRedirect,
							AutomaticDecompression = (this.options.AllowGzipCompression ? DecompressionMethods.GZip : DecompressionMethods.None),
							CookieContainer = this.cookieContainer,
							UnsafeAuthenticatedConnectionSharing = true
						};
						if (this.clientCertificate != null)
						{
							webRequestHandler.ContinueTimeout = TimeSpan.FromMilliseconds(30000.0);
							webRequestHandler.ClientCertificates.Add(this.clientCertificate);
						}
						if (this.options.Credentials != null)
						{
							webRequestHandler.Credentials = this.options.Credentials;
						}
						this.httpClient = new HttpClient(webRequestHandler);
						this.httpClient.Timeout = TimeSpan.FromMilliseconds((double)this.options.DefaultTimeoutInMiliseconds);
						this.httpClient.DefaultRequestHeaders.TransferEncodingChunked = new bool?(true);
						this.httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
					}
					wasNewClientCreated = this.httpClient != null;
					return;
				}
				wasNewClientCreated = false;
			}

			// Token: 0x060011C0 RID: 4544 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
			private HttpRequestMessage StartHttpClientBasedOperationImpl(HttpStream owner, object context, CancellationTokenSource cts, Stream requestStream, out Task<HttpResponseMessage> pendingResponse)
			{
				HttpRequestMessage request = this.PrepareRequestMessageImpl(owner, context, requestStream);
				pendingResponse = owner.userContext.ExecuteInUserContext<Task<HttpResponseMessage>>(() => this.httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, (cts != null) ? cts.Token : CancellationToken.None));
				return request;
			}

			// Token: 0x060011C1 RID: 4545 RVA: 0x0003D448 File Offset: 0x0003B648
			private HttpWebRequest PrepareWebRequestImpl(HttpStream owner, object context)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(owner.dataSource);
				httpWebRequest.Method = "POST";
				httpWebRequest.ServicePoint.SetTcpKeepAlive(true, 30000, 30000);
				httpWebRequest.AllowAutoRedirect = this.options.AllowAutoRedirect;
				httpWebRequest.SendChunked = true;
				httpWebRequest.KeepAlive = true;
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
				httpWebRequest.AutomaticDecompression = (this.options.AllowGzipCompression ? DecompressionMethods.GZip : DecompressionMethods.None);
				httpWebRequest.Timeout = owner.ReadTimeout;
				httpWebRequest.CookieContainer = this.cookieContainer;
				if (this.clientCertificate != null)
				{
					httpWebRequest.ContinueTimeout = 30000;
					httpWebRequest.ClientCertificates.Add(this.clientCertificate);
				}
				if (this.options.Mode == HttpStream.HttpChannelMode.PaasInfra || !owner.IsSessionTokenNeeded)
				{
					httpWebRequest.Headers.Remove("X-AS-GetSessionToken");
				}
				HttpHelper.ApplyHeadersToRequest(owner.defaultHeaders, httpWebRequest);
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				this.GetPerRequestHeaders(owner, context, list);
				HttpHelper.ApplyHeadersToRequest(list, httpWebRequest);
				owner.userContext.UpdateHttpRequest(httpWebRequest);
				return httpWebRequest;
			}

			// Token: 0x060011C2 RID: 4546 RVA: 0x0003D558 File Offset: 0x0003B758
			private HttpWebResponse GetWebResponseImpl(HttpStream owner, object context, HttpWebRequest request)
			{
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = owner.userContext.ExecuteInUserContext<HttpWebResponse>(() => (HttpWebResponse)request.GetResponse());
					this.HandleResponseStatusCode((int)httpWebResponse.StatusCode, null);
				}
				catch (WebException ex)
				{
					if (!(ex.Response is HttpWebResponse))
					{
						throw;
					}
					httpWebResponse = (HttpWebResponse)ex.Response;
					this.HandleResponseStatusCode((int)httpWebResponse.StatusCode, ex);
					Exception ex2;
					switch (this.ClassifyWebError(owner, context, new HttpStream.HttpChannelController.WebErrorInfo(ex), out ex2))
					{
					case HttpStream.HttpChannelController.WebErrorClass.Raise:
						if (ex2 != null)
						{
							throw ex2;
						}
						throw;
					case HttpStream.HttpChannelController.WebErrorClass.Ignore:
						break;
					case HttpStream.HttpChannelController.WebErrorClass.Retry:
						httpWebResponse = null;
						break;
					default:
						throw;
					}
				}
				return httpWebResponse;
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x0003D60C File Offset: 0x0003B80C
			private HttpRequestMessage PrepareRequestMessageImpl(HttpStream owner, object context, Stream requestStream)
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, owner.dataSource);
				if (requestStream != null)
				{
					httpRequestMessage.Content = new StreamContent(requestStream);
				}
				else
				{
					httpRequestMessage.Content = new HttpStream.HttpChannelController.EmptyHttpContent();
				}
				if (!HttpStream.HttpChannelController.AreNetCoreClientsLoaded())
				{
					ServicePoint servicePoint = ServicePointManager.FindServicePoint(httpRequestMessage.RequestUri);
					if (servicePoint != null)
					{
						servicePoint.SetTcpKeepAlive(true, 30000, 30000);
					}
				}
				if (this.options.Mode == HttpStream.HttpChannelMode.PaasInfra || !owner.IsSessionTokenNeeded)
				{
					httpRequestMessage.Headers.Remove("X-AS-GetSessionToken");
				}
				HttpHelper.ApplyHeadersToRequest(owner.defaultHeaders, httpRequestMessage);
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				this.GetPerRequestHeaders(owner, context, list);
				HttpHelper.ApplyHeadersToRequest(list, httpRequestMessage);
				owner.userContext.UpdateHttpRequest(httpRequestMessage);
				return httpRequestMessage;
			}

			// Token: 0x060011C4 RID: 4548 RVA: 0x0003D6C4 File Offset: 0x0003B8C4
			private HttpResponseMessage GetResponseMessageImpl(HttpStream owner, object context, Task<HttpResponseMessage> pendingResponse)
			{
				HttpResponseMessage httpResponseMessage = pendingResponse.WaitForTaskCompletionAndGetResult<HttpResponseMessage>();
				this.HandleResponseStatusCode((int)httpResponseMessage.StatusCode, null);
				if (!httpResponseMessage.IsSuccessStatusCode)
				{
					Exception ex;
					switch (this.ClassifyWebError(owner, context, new HttpStream.HttpChannelController.WebErrorInfo(httpResponseMessage), out ex))
					{
					case HttpStream.HttpChannelController.WebErrorClass.Raise:
						if (ex != null)
						{
							throw ex;
						}
						httpResponseMessage.EnsureSuccessStatusCode();
						throw new HttpRequestException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)httpResponseMessage.StatusCode).ToString(), httpResponseMessage.ReasonPhrase));
					case HttpStream.HttpChannelController.WebErrorClass.Ignore:
						break;
					case HttpStream.HttpChannelController.WebErrorClass.Retry:
						httpResponseMessage = null;
						break;
					default:
						throw new HttpRequestException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)httpResponseMessage.StatusCode).ToString(), httpResponseMessage.ReasonPhrase));
					}
				}
				return httpResponseMessage;
			}

			// Token: 0x04000C11 RID: 3089
			internal const int GlobalObjectOptionsOffset = 0;

			// Token: 0x04000C12 RID: 3090
			private const int TCP_KEEP_ALIVE_TIME_IN_MS = 30000;

			// Token: 0x04000C13 RID: 3091
			private const int TCP_KEEP_ALIVE_INTERVAL_IN_MS = 30000;

			// Token: 0x04000C14 RID: 3092
			private const int CONTINUE_TIMEOUT_IN_MS = 30000;

			// Token: 0x04000C15 RID: 3093
			private const string ASRuntimeCoreAssemblyName = "Microsoft.AnalysisServices.Runtime.Core";

			// Token: 0x04000C16 RID: 3094
			private const string ASRuntimeCorePublicKeyToken = "89845dcd8080cc91";

			// Token: 0x04000C17 RID: 3095
			private static bool? netCoreClientsLoaded;

			// Token: 0x04000C18 RID: 3096
			protected HttpStream.ChannelOptions options;

			// Token: 0x04000C19 RID: 3097
			private CookieContainer cookieContainer;

			// Token: 0x04000C1A RID: 3098
			private X509Certificate2 clientCertificate;

			// Token: 0x04000C1B RID: 3099
			private HttpClient httpClient;

			// Token: 0x0200021F RID: 543
			protected enum WebErrorClass
			{
				// Token: 0x04000F26 RID: 3878
				Raise,
				// Token: 0x04000F27 RID: 3879
				Ignore,
				// Token: 0x04000F28 RID: 3880
				Retry
			}

			// Token: 0x02000220 RID: 544
			protected struct WebResponseInfo
			{
				// Token: 0x06001502 RID: 5378 RVA: 0x000471D2 File Offset: 0x000453D2
				public WebResponseInfo(HttpWebResponse response)
				{
					this.webResponse = response;
					this.responseMessage = null;
				}

				// Token: 0x06001503 RID: 5379 RVA: 0x000471E2 File Offset: 0x000453E2
				public WebResponseInfo(HttpResponseMessage response)
				{
					this.webResponse = null;
					this.responseMessage = response;
				}

				// Token: 0x1700073E RID: 1854
				// (get) Token: 0x06001504 RID: 5380 RVA: 0x000471F2 File Offset: 0x000453F2
				public bool IsValid
				{
					get
					{
						return (this.webResponse != null) ^ (this.responseMessage != null);
					}
				}

				// Token: 0x1700073F RID: 1855
				// (get) Token: 0x06001505 RID: 5381 RVA: 0x00047207 File Offset: 0x00045407
				public HttpStatusCode StatusCode
				{
					get
					{
						if (this.responseMessage != null)
						{
							return this.responseMessage.StatusCode;
						}
						return this.webResponse.StatusCode;
					}
				}

				// Token: 0x06001506 RID: 5382 RVA: 0x00047228 File Offset: 0x00045428
				public string GetResponseHeaderValue(string header)
				{
					string text;
					if (!this.TryGetResponseHeaderValue(header, out text))
					{
						text = null;
					}
					return text;
				}

				// Token: 0x06001507 RID: 5383 RVA: 0x00047243 File Offset: 0x00045443
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.responseMessage != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.responseMessage, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse(this.webResponse, header, out value);
				}

				// Token: 0x04000F29 RID: 3881
				private HttpWebResponse webResponse;

				// Token: 0x04000F2A RID: 3882
				private HttpResponseMessage responseMessage;
			}

			// Token: 0x02000221 RID: 545
			protected struct WebErrorInfo
			{
				// Token: 0x06001508 RID: 5384 RVA: 0x00047268 File Offset: 0x00045468
				public WebErrorInfo(WebException error)
				{
					this.error = error;
					this.response = null;
				}

				// Token: 0x06001509 RID: 5385 RVA: 0x00047278 File Offset: 0x00045478
				public WebErrorInfo(HttpResponseMessage response)
				{
					this.error = null;
					this.response = response;
				}

				// Token: 0x17000740 RID: 1856
				// (get) Token: 0x0600150A RID: 5386 RVA: 0x00047288 File Offset: 0x00045488
				public bool IsValid
				{
					get
					{
						return (this.error != null) ^ (this.response != null);
					}
				}

				// Token: 0x17000741 RID: 1857
				// (get) Token: 0x0600150B RID: 5387 RVA: 0x0004729D File Offset: 0x0004549D
				public HttpStatusCode StatusCode
				{
					get
					{
						if (this.response != null)
						{
							return this.response.StatusCode;
						}
						return ((HttpWebResponse)this.error.Response).StatusCode;
					}
				}

				// Token: 0x17000742 RID: 1858
				// (get) Token: 0x0600150C RID: 5388 RVA: 0x000472C8 File Offset: 0x000454C8
				public WebExceptionStatus? ErrorStatus
				{
					get
					{
						if (this.response != null)
						{
							return null;
						}
						return new WebExceptionStatus?(this.error.Status);
					}
				}

				// Token: 0x0600150D RID: 5389 RVA: 0x000472F7 File Offset: 0x000454F7
				public bool TryGetResponseHeaderValue(string header, out string value)
				{
					if (this.response != null)
					{
						return HttpHelper.TryGetHeaderValueFromResponse(this.response, header, out value);
					}
					return HttpHelper.TryGetHeaderValueFromResponse((HttpWebResponse)this.error.Response, header, out value);
				}

				// Token: 0x0600150E RID: 5390 RVA: 0x00047328 File Offset: 0x00045528
				public XmlaStreamException CreateStreamException(ConnectionExceptionCause? exceptionCause = null)
				{
					if (this.response != null)
					{
						if (exceptionCause != null)
						{
							return new XmlaStreamException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)this.response.StatusCode).ToString(), this.response.ReasonPhrase), exceptionCause.Value);
						}
						return new XmlaStreamException(XmlaSR.HttpStream_ResponseWithFailedStatus(((int)this.response.StatusCode).ToString(), this.response.ReasonPhrase));
					}
					else
					{
						if (exceptionCause != null)
						{
							return new XmlaStreamException(this.error, exceptionCause.Value);
						}
						return new XmlaStreamException(this.error);
					}
				}

				// Token: 0x0600150F RID: 5391 RVA: 0x000473C6 File Offset: 0x000455C6
				public Exception CreatePaasInfraConnectionException(AsInstanceType asInstanceType)
				{
					if (this.response != null)
					{
						return ASAzureUtility.GetConnectionException(this.response, asInstanceType);
					}
					return ASAzureUtility.GetConnectionException(this.error, asInstanceType);
				}

				// Token: 0x04000F2B RID: 3883
				private WebException error;

				// Token: 0x04000F2C RID: 3884
				private HttpResponseMessage response;
			}

			// Token: 0x02000222 RID: 546
			protected sealed class RequestPayloadStream : Stream
			{
				// Token: 0x06001510 RID: 5392 RVA: 0x000473E9 File Offset: 0x000455E9
				public RequestPayloadStream(long maxQueueSize, int defaultBufferSize)
				{
					this.maxQueueSize = maxQueueSize;
					this.defaultBufferSize = defaultBufferSize;
					this.pendingChunks = new Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk>();
				}

				// Token: 0x17000743 RID: 1859
				// (get) Token: 0x06001511 RID: 5393 RVA: 0x0004740A File Offset: 0x0004560A
				public override bool CanRead
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000744 RID: 1860
				// (get) Token: 0x06001512 RID: 5394 RVA: 0x0004740D File Offset: 0x0004560D
				public override bool CanWrite
				{
					get
					{
						return !this.IsCompleted();
					}
				}

				// Token: 0x17000745 RID: 1861
				// (get) Token: 0x06001513 RID: 5395 RVA: 0x00047418 File Offset: 0x00045618
				public override bool CanSeek
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17000746 RID: 1862
				// (get) Token: 0x06001514 RID: 5396 RVA: 0x0004741B File Offset: 0x0004561B
				// (set) Token: 0x06001515 RID: 5397 RVA: 0x00047423 File Offset: 0x00045623
				public override long Position
				{
					get
					{
						return this.position;
					}
					set
					{
						throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
					}
				}

				// Token: 0x17000747 RID: 1863
				// (get) Token: 0x06001516 RID: 5398 RVA: 0x0004742F File Offset: 0x0004562F
				public override long Length
				{
					get
					{
						return this.length;
					}
				}

				// Token: 0x06001517 RID: 5399 RVA: 0x00047438 File Offset: 0x00045638
				public void MarkAsCompleted()
				{
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					lock (queue)
					{
						if (this.HasPendingReadRequest() && !this.pendingReadRequest.IsReady(this.defaultBufferSize))
						{
							this.pendingReadRequest.Complete(this, null);
						}
						if (this.HasActiveWriteChunk())
						{
							this.PushWriteChunkToQueue();
						}
						this.isCompleted = true;
					}
				}

				// Token: 0x06001518 RID: 5400 RVA: 0x000474B0 File Offset: 0x000456B0
				public override int Read(byte[] buffer, int offset, int count)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (this.IsCompleted())
					{
						return 0;
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					int num2;
					lock (queue)
					{
						int num;
						if (this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							num2 = num;
						}
						else
						{
							num2 = this.WaitForReadRequestCompletion(buffer, offset, offset + num, count - num);
						}
					}
					return num2;
				}

				// Token: 0x06001519 RID: 5401 RVA: 0x00047554 File Offset: 0x00045754
				public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (this.IsCompleted())
					{
						return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.CreateCompleted(callback, state, 0);
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					int num;
					lock (queue)
					{
						if (!this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							return this.QueueIncompleteAsyncReadRequest(buffer, offset, offset + num, count - num, callback, state);
						}
					}
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.CreateCompleted(callback, state, num);
				}

				// Token: 0x0600151A RID: 5402 RVA: 0x0004760C File Offset: 0x0004580C
				public override int EndRead(IAsyncResult asyncResult)
				{
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.End(asyncResult);
				}

				// Token: 0x0600151B RID: 5403 RVA: 0x00047614 File Offset: 0x00045814
				public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0 || offset > buffer.Length - 1)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count <= 0 || offset + count > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (cancellationToken.IsCancellationRequested)
					{
						return Task.FromCanceled<int>(cancellationToken);
					}
					if (this.IsCompleted())
					{
						return AsyncHelper.GetCompletedTaskWithDefaultValue<int>();
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					IAsyncResult asyncResult;
					lock (queue)
					{
						int num;
						if (this.TryCompleteReadRequest(buffer, offset, count, out num))
						{
							return Task.FromResult<int>(num);
						}
						asyncResult = this.QueueIncompleteAsyncReadRequest(buffer, offset, offset + num, count - num, null, null);
					}
					return new TaskFactory<int>(cancellationToken).FromAsync(asyncResult, new Func<IAsyncResult, int>(HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.End));
				}

				// Token: 0x0600151C RID: 5404 RVA: 0x000476F0 File Offset: 0x000458F0
				public override void Write(byte[] buffer, int offset, int count)
				{
					if (this.isCompleted)
					{
						throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_WriteAfterComplete);
					}
					Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> queue = this.pendingChunks;
					lock (queue)
					{
						do
						{
							if (this.IsOutgoingQueueFull())
							{
								this.WaitForAdditionalSpaceInOutgoingQueue();
							}
							this.WriteNextChunk(buffer, ref offset, ref count);
						}
						while (count > 0);
					}
				}

				// Token: 0x0600151D RID: 5405 RVA: 0x0004775C File Offset: 0x0004595C
				public override void Flush()
				{
				}

				// Token: 0x0600151E RID: 5406 RVA: 0x0004775E File Offset: 0x0004595E
				public override long Seek(long offset, SeekOrigin origin)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x0600151F RID: 5407 RVA: 0x0004776A File Offset: 0x0004596A
				public override void SetLength(long value)
				{
					throw new NotSupportedException(XmlaSR.HttpStream_RequestPayloadStream_InvalidStreamOperation);
				}

				// Token: 0x06001520 RID: 5408 RVA: 0x00047778 File Offset: 0x00045978
				private bool TryCompleteReadRequest(byte[] buffer, int offset, int count, out int bytesRead)
				{
					int num = offset;
					int num2;
					do
					{
						num2 = this.ReadNextChunk(buffer, offset, count);
						if (num2 > 0)
						{
							offset += num2;
							count -= num2;
						}
					}
					while (num2 > 0 && !HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(this.defaultBufferSize, num, offset, count));
					bytesRead = offset - num;
					return HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(this.defaultBufferSize, num, offset, count) || this.IsCompleted();
				}

				// Token: 0x06001521 RID: 5409 RVA: 0x000477D4 File Offset: 0x000459D4
				private int ReadNextChunk(byte[] buffer, int offset, int count)
				{
					int num;
					if (this.HasActiveReadChunk())
					{
						num = this.currentReadChunk.Read(buffer, offset, count);
						this.position += (long)num;
						if (this.currentReadChunk.Offset >= this.currentReadChunk.Count)
						{
							if (this.pendingChunks.Count > 0)
							{
								this.currentReadChunk = this.pendingChunks.Dequeue();
							}
							else
							{
								this.currentReadChunk = default(HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk);
							}
						}
					}
					else if (this.HasActiveWriteChunk() && this.currentWriteChunk.Offset < this.currentWriteChunk.Count)
					{
						num = this.currentWriteChunk.Read(buffer, offset, count);
						this.position += (long)num;
					}
					else
					{
						num = 0;
					}
					if (this.hasPendingWriteRequest && num > 0)
					{
						Monitor.Pulse(this.pendingChunks);
					}
					return num;
				}

				// Token: 0x06001522 RID: 5410 RVA: 0x000478A8 File Offset: 0x00045AA8
				private int WaitForReadRequestCompletion(byte[] buffer, int initialOffset, int offset, int count)
				{
					int num;
					try
					{
						this.pendingReadRequest = new HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest(buffer, initialOffset, offset, count, null);
						Monitor.Wait(this.pendingChunks);
						if (this.pendingReadRequest.Error != null)
						{
							throw this.pendingReadRequest.Error;
						}
						num = this.pendingReadRequest.Offset - this.pendingReadRequest.InitialOffset;
					}
					finally
					{
						this.pendingReadRequest = default(HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest);
					}
					return num;
				}

				// Token: 0x06001523 RID: 5411 RVA: 0x00047924 File Offset: 0x00045B24
				private IAsyncResult QueueIncompleteAsyncReadRequest(byte[] buffer, int initialOffset, int offset, int count, AsyncCallback callback, object state)
				{
					HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
					this.pendingReadRequest = new HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest(buffer, initialOffset, offset, count, readAsyncResult);
					return readAsyncResult;
				}

				// Token: 0x06001524 RID: 5412 RVA: 0x00047950 File Offset: 0x00045B50
				private void WaitForAdditionalSpaceInOutgoingQueue()
				{
					try
					{
						this.hasPendingWriteRequest = true;
						Monitor.Wait(this.pendingChunks);
					}
					finally
					{
						this.hasPendingWriteRequest = false;
					}
				}

				// Token: 0x06001525 RID: 5413 RVA: 0x0004798C File Offset: 0x00045B8C
				private void WriteNextChunk(byte[] buffer, ref int offset, ref int count)
				{
					if (this.HasPendingReadRequest() && !this.pendingReadRequest.IsReady(this.defaultBufferSize))
					{
						try
						{
							int num = this.pendingReadRequest.Write(buffer, ref offset, ref count);
							this.length += (long)num;
							this.position += (long)num;
							if (this.pendingReadRequest.IsReady(this.defaultBufferSize))
							{
								this.pendingReadRequest.Complete(this, null);
							}
							return;
						}
						catch (Exception ex)
						{
							if (FrameworkRuntimeHelper.IsFatalException(ex))
							{
								throw;
							}
							this.pendingReadRequest.Complete(this, ex);
							throw new XmlaStreamException(XmlaSR.HttpStream_RequestPayloadStream_ErrorInWrite(ex.Message, Environment.NewLine));
						}
					}
					if (this.HasActiveWriteChunk())
					{
						int num = this.currentWriteChunk.Write(buffer, ref offset, ref count);
						this.length += (long)num;
					}
					else
					{
						long num2 = this.maxQueueSize - (this.length - this.position);
						if (num2 >= (long)count && num2 >= (long)this.defaultBufferSize)
						{
							this.currentWriteChunk = new HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk(this.defaultBufferSize, buffer, offset, count);
							int num = count;
							offset += count;
							count = 0;
							this.length += (long)num;
						}
						else
						{
							this.currentWriteChunk = new HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk((int)num2);
							int num = this.currentWriteChunk.Write(buffer, ref offset, ref count);
							this.length += (long)num;
						}
					}
					if (this.currentWriteChunk.IsFull)
					{
						this.PushWriteChunkToQueue();
					}
				}

				// Token: 0x06001526 RID: 5414 RVA: 0x00047B0C File Offset: 0x00045D0C
				private void PushWriteChunkToQueue()
				{
					if (this.currentWriteChunk.Offset < this.currentWriteChunk.Count)
					{
						if (this.HasActiveReadChunk())
						{
							this.pendingChunks.Enqueue(this.currentWriteChunk);
						}
						else
						{
							this.currentReadChunk = this.currentWriteChunk;
						}
					}
					this.currentWriteChunk = default(HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk);
				}

				// Token: 0x06001527 RID: 5415 RVA: 0x00047B64 File Offset: 0x00045D64
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsOutgoingQueueFull()
				{
					return this.length - this.position >= this.maxQueueSize;
				}

				// Token: 0x06001528 RID: 5416 RVA: 0x00047B7E File Offset: 0x00045D7E
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasPendingReadRequest()
				{
					return this.pendingReadRequest.Buffer != null;
				}

				// Token: 0x06001529 RID: 5417 RVA: 0x00047B8E File Offset: 0x00045D8E
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveReadChunk()
				{
					return this.currentReadChunk.Buffer != null;
				}

				// Token: 0x0600152A RID: 5418 RVA: 0x00047B9E File Offset: 0x00045D9E
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool HasActiveWriteChunk()
				{
					return this.currentWriteChunk.Buffer != null;
				}

				// Token: 0x0600152B RID: 5419 RVA: 0x00047BAE File Offset: 0x00045DAE
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				private bool IsCompleted()
				{
					return this.isCompleted && this.position == this.length;
				}

				// Token: 0x04000F2D RID: 3885
				private readonly long maxQueueSize;

				// Token: 0x04000F2E RID: 3886
				private readonly int defaultBufferSize;

				// Token: 0x04000F2F RID: 3887
				private readonly Queue<HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk> pendingChunks;

				// Token: 0x04000F30 RID: 3888
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentReadChunk;

				// Token: 0x04000F31 RID: 3889
				private HttpStream.HttpChannelController.RequestPayloadStream.PayloadChunk currentWriteChunk;

				// Token: 0x04000F32 RID: 3890
				private HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest pendingReadRequest;

				// Token: 0x04000F33 RID: 3891
				private bool hasPendingWriteRequest;

				// Token: 0x04000F34 RID: 3892
				private long position;

				// Token: 0x04000F35 RID: 3893
				private long length;

				// Token: 0x04000F36 RID: 3894
				private bool isCompleted;

				// Token: 0x02000244 RID: 580
				private struct ReadRequest
				{
					// Token: 0x060015B2 RID: 5554 RVA: 0x00048A37 File Offset: 0x00046C37
					public ReadRequest(byte[] buffer, int initialOffset, int offset, int count, HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult)
					{
						this.Buffer = buffer;
						this.InitialOffset = initialOffset;
						this.Offset = offset;
						this.Count = count;
						this.asyncResult = asyncResult;
						this.Error = null;
					}

					// Token: 0x17000758 RID: 1880
					// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00048A65 File Offset: 0x00046C65
					public byte[] Buffer { get; }

					// Token: 0x17000759 RID: 1881
					// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00048A6D File Offset: 0x00046C6D
					public int InitialOffset { get; }

					// Token: 0x1700075A RID: 1882
					// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00048A75 File Offset: 0x00046C75
					// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00048A7D File Offset: 0x00046C7D
					public int Offset { get; private set; }

					// Token: 0x1700075B RID: 1883
					// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00048A86 File Offset: 0x00046C86
					// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00048A8E File Offset: 0x00046C8E
					public int Count { get; private set; }

					// Token: 0x1700075C RID: 1884
					// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00048A97 File Offset: 0x00046C97
					// (set) Token: 0x060015BA RID: 5562 RVA: 0x00048A9F File Offset: 0x00046C9F
					public Exception Error { get; private set; }

					// Token: 0x060015BB RID: 5563 RVA: 0x00048AA8 File Offset: 0x00046CA8
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public static bool IsReady(int minSize, int initialOffset, int offset, int count)
					{
						return count == 0 || offset - initialOffset >= minSize;
					}

					// Token: 0x060015BC RID: 5564 RVA: 0x00048AB8 File Offset: 0x00046CB8
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					public bool IsReady(int minSize)
					{
						return HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest.IsReady(minSize, this.InitialOffset, this.Offset, this.Count);
					}

					// Token: 0x060015BD RID: 5565 RVA: 0x00048AD4 File Offset: 0x00046CD4
					public int Write(byte[] buffer, ref int offset, ref int count)
					{
						int num = ((this.Count < count) ? this.Count : count);
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, this.Offset, num);
						this.Offset += num;
						this.Count -= num;
						offset += num;
						count -= num;
						return num;
					}

					// Token: 0x060015BE RID: 5566 RVA: 0x00048B34 File Offset: 0x00046D34
					public void Complete(HttpStream.HttpChannelController.RequestPayloadStream owner, Exception error)
					{
						if (this.asyncResult != null)
						{
							try
							{
								this.asyncResult.Complete(this.Offset - this.InitialOffset, error);
								return;
							}
							finally
							{
								owner.pendingReadRequest = default(HttpStream.HttpChannelController.RequestPayloadStream.ReadRequest);
							}
						}
						this.Error = error;
						Monitor.Pulse(owner.pendingChunks);
					}

					// Token: 0x04000F7A RID: 3962
					private HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult asyncResult;
				}

				// Token: 0x02000245 RID: 581
				private struct PayloadChunk
				{
					// Token: 0x060015BF RID: 5567 RVA: 0x00048B98 File Offset: 0x00046D98
					public PayloadChunk(int bufferSize)
					{
						this.Buffer = new byte[bufferSize];
						this.Offset = 0;
						this.Count = 0;
					}

					// Token: 0x060015C0 RID: 5568 RVA: 0x00048BB4 File Offset: 0x00046DB4
					public PayloadChunk(int bufferSize, byte[] buffer, int offset, int count)
					{
						this.Buffer = ((count > bufferSize) ? new byte[count] : new byte[bufferSize]);
						this.Offset = 0;
						this.Count = count;
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, 0, count);
					}

					// Token: 0x1700075D RID: 1885
					// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00048BEF File Offset: 0x00046DEF
					public byte[] Buffer { get; }

					// Token: 0x1700075E RID: 1886
					// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00048BF7 File Offset: 0x00046DF7
					// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00048BFF File Offset: 0x00046DFF
					public int Offset { get; private set; }

					// Token: 0x1700075F RID: 1887
					// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00048C08 File Offset: 0x00046E08
					// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00048C10 File Offset: 0x00046E10
					public int Count { get; private set; }

					// Token: 0x17000760 RID: 1888
					// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00048C19 File Offset: 0x00046E19
					public bool IsFull
					{
						[MethodImpl(MethodImplOptions.AggressiveInlining)]
						get
						{
							return this.Count == this.Buffer.Length;
						}
					}

					// Token: 0x060015C7 RID: 5575 RVA: 0x00048C2C File Offset: 0x00046E2C
					public int Read(byte[] buffer, int offset, int count)
					{
						int num = this.Count - this.Offset;
						if (num == 0)
						{
							return 0;
						}
						int num2 = ((num < count) ? num : count);
						global::System.Buffer.BlockCopy(this.Buffer, this.Offset, buffer, offset, num2);
						this.Offset += num2;
						return num2;
					}

					// Token: 0x060015C8 RID: 5576 RVA: 0x00048C78 File Offset: 0x00046E78
					public int Write(byte[] buffer, ref int offset, ref int count)
					{
						int num = this.Buffer.Length - this.Count;
						int num2 = ((num < count) ? num : count);
						global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, this.Count, num2);
						this.Count += num2;
						offset += num2;
						count -= num2;
						return num2;
					}
				}

				// Token: 0x02000246 RID: 582
				private sealed class ReadAsyncResult : IAsyncResult
				{
					// Token: 0x060015C9 RID: 5577 RVA: 0x00048CD0 File Offset: 0x00046ED0
					public ReadAsyncResult(AsyncCallback callback, object state)
					{
						this.callback = callback;
						this.AsyncState = state;
						this.@lock = new object();
					}

					// Token: 0x17000761 RID: 1889
					// (get) Token: 0x060015CA RID: 5578 RVA: 0x00048CF1 File Offset: 0x00046EF1
					public object AsyncState { get; }

					// Token: 0x17000762 RID: 1890
					// (get) Token: 0x060015CB RID: 5579 RVA: 0x00048CFC File Offset: 0x00046EFC
					public WaitHandle AsyncWaitHandle
					{
						get
						{
							if (this.completionEvent != null)
							{
								return this.completionEvent;
							}
							object obj = this.@lock;
							WaitHandle waitHandle;
							lock (obj)
							{
								if (this.completionEvent == null)
								{
									this.completionEvent = new ManualResetEvent(this.IsCompleted);
								}
								waitHandle = this.completionEvent;
							}
							return waitHandle;
						}
					}

					// Token: 0x17000763 RID: 1891
					// (get) Token: 0x060015CC RID: 5580 RVA: 0x00048D68 File Offset: 0x00046F68
					// (set) Token: 0x060015CD RID: 5581 RVA: 0x00048D70 File Offset: 0x00046F70
					public bool CompletedSynchronously { get; private set; }

					// Token: 0x17000764 RID: 1892
					// (get) Token: 0x060015CE RID: 5582 RVA: 0x00048D79 File Offset: 0x00046F79
					// (set) Token: 0x060015CF RID: 5583 RVA: 0x00048D81 File Offset: 0x00046F81
					public bool IsCompleted { get; private set; }

					// Token: 0x060015D0 RID: 5584 RVA: 0x00048D8A File Offset: 0x00046F8A
					public static HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult CreateCompleted(AsyncCallback callback, object state, int bytesRead)
					{
						HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = new HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult(callback, state);
						readAsyncResult.bytesRead = bytesRead;
						readAsyncResult.CompleteImpl(true);
						return readAsyncResult;
					}

					// Token: 0x060015D1 RID: 5585 RVA: 0x00048DA4 File Offset: 0x00046FA4
					public static int End(IAsyncResult asyncResult)
					{
						if (asyncResult == null)
						{
							throw new ArgumentNullException("asyncResult");
						}
						HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = asyncResult as HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult;
						if (readAsyncResult == null)
						{
							throw new ArgumentException(XmlaSR.HttpStream_RequestPayloadStream_InvalidAsyncResultType, "asyncResult");
						}
						if (readAsyncResult.endWasCalled)
						{
							throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_EndReadAlreadyCalled);
						}
						readAsyncResult.endWasCalled = true;
						object obj = readAsyncResult.@lock;
						WaitHandle waitHandle;
						lock (obj)
						{
							if (readAsyncResult.IsCompleted)
							{
								waitHandle = null;
							}
							else
							{
								if (readAsyncResult.completionEvent == null)
								{
									readAsyncResult.completionEvent = new ManualResetEvent(false);
								}
								waitHandle = readAsyncResult.completionEvent;
							}
						}
						if (waitHandle != null)
						{
							waitHandle.WaitOne();
						}
						if (readAsyncResult.completionEvent != null)
						{
							readAsyncResult.completionEvent.Close();
						}
						if (readAsyncResult.ex != null)
						{
							throw readAsyncResult.ex;
						}
						return readAsyncResult.bytesRead;
					}

					// Token: 0x060015D2 RID: 5586 RVA: 0x00048E7C File Offset: 0x0004707C
					public void Complete(int bytesRead, Exception ex)
					{
						this.bytesRead = bytesRead;
						this.ex = ex;
						this.CompleteImpl(false);
					}

					// Token: 0x060015D3 RID: 5587 RVA: 0x00048E94 File Offset: 0x00047094
					private void CompleteImpl(bool completedSynchronously)
					{
						this.CompletedSynchronously = completedSynchronously;
						if (completedSynchronously)
						{
							this.IsCompleted = true;
						}
						else
						{
							object obj = this.@lock;
							lock (obj)
							{
								this.IsCompleted = true;
								if (this.completionEvent != null)
								{
									this.completionEvent.Set();
								}
							}
						}
						if (this.callback != null)
						{
							if (completedSynchronously)
							{
								try
								{
									this.callback(this);
									return;
								}
								catch (Exception ex)
								{
									if (FrameworkRuntimeHelper.IsFatalException(ex))
									{
										throw;
									}
									throw new InvalidOperationException(XmlaSR.HttpStream_RequestPayloadStream_ErrorInCallback, ex);
								}
							}
							ThreadPool.QueueUserWorkItem(HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult.onDeferredCallback, this);
						}
					}

					// Token: 0x04000F83 RID: 3971
					private static WaitCallback onDeferredCallback = delegate(object state)
					{
						try
						{
							HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult readAsyncResult = (HttpStream.HttpChannelController.RequestPayloadStream.ReadAsyncResult)state;
							readAsyncResult.callback(readAsyncResult);
						}
						catch (Exception ex)
						{
							if (FrameworkRuntimeHelper.IsFatalException(ex))
							{
								throw;
							}
						}
					};

					// Token: 0x04000F84 RID: 3972
					private readonly object @lock;

					// Token: 0x04000F85 RID: 3973
					private readonly AsyncCallback callback;

					// Token: 0x04000F86 RID: 3974
					private ManualResetEvent completionEvent;

					// Token: 0x04000F87 RID: 3975
					private bool endWasCalled;

					// Token: 0x04000F88 RID: 3976
					private int bytesRead;

					// Token: 0x04000F89 RID: 3977
					private Exception ex;
				}
			}

			// Token: 0x02000223 RID: 547
			private sealed class EmptyHttpContent : HttpContent
			{
				// Token: 0x0600152D RID: 5421 RVA: 0x00047BD0 File Offset: 0x00045DD0
				protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
				{
					if (stream == null)
					{
						throw new ArgumentNullException("stream");
					}
					return AsyncHelper.GetCompletedTaskWithDefaultValue<object>();
				}

				// Token: 0x0600152E RID: 5422 RVA: 0x00047BE5 File Offset: 0x00045DE5
				protected override bool TryComputeLength(out long length)
				{
					length = 0L;
					return true;
				}
			}
		}

		// Token: 0x02000182 RID: 386
		private enum HttpChannelMode
		{
			// Token: 0x04000C1D RID: 3101
			Unknonw,
			// Token: 0x04000C1E RID: 3102
			Legacy,
			// Token: 0x04000C1F RID: 3103
			PaasInfra
		}

		// Token: 0x02000183 RID: 387
		private struct ChannelOptions
		{
			// Token: 0x060011C5 RID: 4549 RVA: 0x0003D764 File Offset: 0x0003B964
			public ChannelOptions(ConnectionInfo info, out UserContext context)
			{
				this.defaultTimeoutInMiliseconds = ((info.Timeout > 0) ? (info.Timeout * 1000) : (-1));
				this.certificateThumbprint = info.ClientCertificateThumbprint ?? string.Empty;
				this.flags = HttpStream.ChannelOptions.BitmapOptions.None;
				if (info.IsPaaSInfrastructure || info.IsInternalPaaSInfrastructure)
				{
					this.flags |= (HttpStream.ChannelOptions.BitmapOptions)2;
				}
				else
				{
					this.flags |= (HttpStream.ChannelOptions.BitmapOptions)1;
				}
				if (info.AllowAutoRedirect)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.AutoRedirect;
				}
				if (info.IsPaaSInfrastructure)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.PaasInfrastructure;
				}
				if (info.IsPbiDataset)
				{
					this.flags |= HttpStream.ChannelOptions.BitmapOptions.PbiDataset;
				}
				if (info.IsPaaSInfrastructure || info.IsPbiDataset)
				{
					context = new AuthenticatedUserContext(info.AuthHandle);
				}
				else if (info.IntegratedSecurity == IntegratedSecurity.Federated)
				{
					if (string.IsNullOrEmpty(info.IdentityProvider) || string.Compare(info.IdentityProvider, "MsoID", CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase) != 0)
					{
						throw new NotSupportedException(XmlaSR.ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated);
					}
					UserContext userContext;
					if (!string.IsNullOrEmpty(info.UserID))
					{
						MsoIdUserContext msoIdUserContext = new MsoIdUserContext(info.UserID, info.Password);
						msoIdUserContext.CacheAccessToken = true;
						userContext = msoIdUserContext;
						msoIdUserContext.OnAuthenticationException = new MsoIdAuthenticationExceptionHandler(HttpStream.ChannelOptions.OnMsoIdAuthenticationException);
					}
					else
					{
						MsoIdUserContext msoIdUserContext2 = new MsoIdUserContext();
						msoIdUserContext2.CacheAccessToken = true;
						userContext = msoIdUserContext2;
						msoIdUserContext2.OnAuthenticationException = new MsoIdAuthenticationExceptionHandler(HttpStream.ChannelOptions.OnMsoIdAuthenticationException);
					}
					context = userContext;
				}
				else
				{
					if (info.UserID == null)
					{
						context = WindowsIdentityUserContext.GetCurrent(true, false);
					}
					else
					{
						if (info.Password == null)
						{
							throw new ArgumentException(XmlaSR.ConnectionString_UnsupportedPropertyValue("Password", "null"));
						}
						context = new HttpStream.ChannelOptions.NetworkCredentialUserContext(info.UserID, info.Password);
					}
					if (info.AuthHandle != null)
					{
						context = new HttpStream.ChannelOptions.AuthenticatedComposedUserContext(info.AuthHandle, context);
					}
				}
				context.TryGetCredentials(out this.credentials, out this.groupName);
				this.groupName = this.groupName ?? string.Empty;
				this.cacheKey = HttpStream.ChannelOptions.BuildCacheKey(this.flags, this.defaultTimeoutInMiliseconds, this.certificateThumbprint, this.groupName);
			}

			// Token: 0x060011C6 RID: 4550 RVA: 0x0003D98A File Offset: 0x0003BB8A
			private ChannelOptions(int defaultTimeoutInMiliseconds, string certificateThumbprint, ICredentials credentials, string groupName, HttpStream.ChannelOptions.BitmapOptions flags, string cacheKey)
			{
				this.defaultTimeoutInMiliseconds = defaultTimeoutInMiliseconds;
				this.certificateThumbprint = certificateThumbprint;
				this.credentials = credentials;
				this.groupName = groupName;
				this.flags = flags;
				this.cacheKey = cacheKey;
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003D9B9 File Offset: 0x0003BBB9
			public int DefaultTimeoutInMiliseconds
			{
				get
				{
					return this.defaultTimeoutInMiliseconds;
				}
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0003D9C1 File Offset: 0x0003BBC1
			public string CertificateThumbprint
			{
				get
				{
					return this.certificateThumbprint;
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0003D9C9 File Offset: 0x0003BBC9
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x060011CA RID: 4554 RVA: 0x0003D9D1 File Offset: 0x0003BBD1
			public HttpStream.HttpChannelMode Mode
			{
				get
				{
					return this.GetMode();
				}
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x060011CB RID: 4555 RVA: 0x0003D9D9 File Offset: 0x0003BBD9
			public bool AllowAutoRedirect
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.AutoRedirect);
				}
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x060011CC RID: 4556 RVA: 0x0003D9E6 File Offset: 0x0003BBE6
			public bool AllowGzipCompression
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.GzipCompression);
				}
			}

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003D9F3 File Offset: 0x0003BBF3
			public bool IsPaasInfrastructure
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PaasInfrastructure);
				}
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x060011CE RID: 4558 RVA: 0x0003DA00 File Offset: 0x0003BC00
			public bool IsPbiDataset
			{
				get
				{
					return this.IsEnabled(HttpStream.ChannelOptions.BitmapOptions.PbiDataset);
				}
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x0003DA0D File Offset: 0x0003BC0D
			internal string CacheKey
			{
				get
				{
					return this.cacheKey;
				}
			}

			// Token: 0x060011D0 RID: 4560 RVA: 0x0003DA15 File Offset: 0x0003BC15
			public static string GetCacheKeyFromGlobalObject(IList<object> elements, int index)
			{
				return (string)elements[index + 5];
			}

			// Token: 0x060011D1 RID: 4561 RVA: 0x0003DA28 File Offset: 0x0003BC28
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("HTTP Channel:\r\n\tOptions=");
				stringBuilder.Append(this.GetMode().ToString());
				for (int i = 8; i < 32; i++)
				{
					int num = 1 << i;
					if ((this.flags & (HttpStream.ChannelOptions.BitmapOptions)num) != HttpStream.ChannelOptions.BitmapOptions.None)
					{
						HttpStream.ChannelOptions.BitmapOptions bitmapOptions = (HttpStream.ChannelOptions.BitmapOptions)num;
						stringBuilder.AppendFormat(" | {0}", bitmapOptions.ToString());
					}
				}
				stringBuilder.AppendFormat(" [{0}]", (int)this.flags);
				if (this.defaultTimeoutInMiliseconds != -1)
				{
					stringBuilder.AppendFormat("\r\n\tTimeout={0}", this.defaultTimeoutInMiliseconds);
				}
				if (!string.IsNullOrEmpty(this.certificateThumbprint))
				{
					stringBuilder.AppendFormat("\r\n\tCertificate={0}", this.certificateThumbprint);
				}
				if (!string.IsNullOrEmpty(this.groupName))
				{
					stringBuilder.AppendFormat("\r\n\tGroup={0}", this.groupName);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060011D2 RID: 4562 RVA: 0x0003DB10 File Offset: 0x0003BD10
			internal static HttpStream.ChannelOptions DeserializeFromGlobalObject(IList<object> elements, int index)
			{
				return new HttpStream.ChannelOptions((int)elements[index], (string)elements[index + 1], (ICredentials)elements[index + 2], (string)elements[index + 3], (HttpStream.ChannelOptions.BitmapOptions)((int)elements[index + 4]), (string)elements[index + 5]);
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x0003DB74 File Offset: 0x0003BD74
			internal void SerializeToGlobalObject(IList<object> elements)
			{
				elements.Add(this.defaultTimeoutInMiliseconds);
				elements.Add(this.certificateThumbprint);
				elements.Add(this.credentials);
				elements.Add(this.groupName);
				elements.Add((int)this.flags);
				elements.Add(this.cacheKey);
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
			private static string BuildCacheKey(HttpStream.ChannelOptions.BitmapOptions flags, int defaultTimeoutInMiliseconds, string certificateThumbprint, string groupName)
			{
				StringBuilder stringBuilder = new StringBuilder("HTTP Channel: ");
				stringBuilder.AppendFormat("Options=0x{0:X8}", (int)flags);
				if (defaultTimeoutInMiliseconds != -1)
				{
					stringBuilder.AppendFormat(", Timeout={0}", defaultTimeoutInMiliseconds);
				}
				if (!string.IsNullOrEmpty(certificateThumbprint))
				{
					stringBuilder.AppendFormat(", Certificate={0}", certificateThumbprint);
				}
				if (!string.IsNullOrEmpty(groupName))
				{
					stringBuilder.AppendFormat(", Group={0}", groupName);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060011D5 RID: 4565 RVA: 0x0003DC44 File Offset: 0x0003BE44
			private static void OnMsoIdAuthenticationException(MsoIdAuthenticationException error, out bool ignoreError)
			{
				switch (error.ErrorType)
				{
				case MsoIdAuthenticationError.NotInstalled:
				case MsoIdAuthenticationError.LoadFailure:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_MissingSignInAssistant, error);
				case MsoIdAuthenticationError.InitFailure:
				case MsoIdAuthenticationError.OperationalError:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_InternalError, error);
				case MsoIdAuthenticationError.SsoWithNonDomainUser:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_SsoFailedNonDomainUser, error);
				case MsoIdAuthenticationError.MissingPassword:
					throw new AdomdConnectionException(XmlaSR.ConnectionString_MissingPassword, error);
				case MsoIdAuthenticationError.SsoAuthenticationFailed:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_SsoFailed, error);
				case MsoIdAuthenticationError.AuthenticationFailed:
					throw new AdomdConnectionException(XmlaSR.Authentication_MsoID_InvalidCredentials, error);
				}
				throw new AdomdConnectionException(XmlaSR.InternalError, error);
			}

			// Token: 0x060011D6 RID: 4566 RVA: 0x0003DCE1 File Offset: 0x0003BEE1
			private HttpStream.HttpChannelMode GetMode()
			{
				return (HttpStream.HttpChannelMode)(this.flags & HttpStream.ChannelOptions.BitmapOptions.ChannelModeMask);
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x0003DCEF File Offset: 0x0003BEEF
			private bool IsEnabled(HttpStream.ChannelOptions.BitmapOptions options)
			{
				return (this.flags & options) == options;
			}

			// Token: 0x04000C20 RID: 3104
			internal const int GlobalObjectElementCount = 6;

			// Token: 0x04000C21 RID: 3105
			private const int GlobalObjectElementOffset_DefaultTimeout = 0;

			// Token: 0x04000C22 RID: 3106
			private const int GlobalObjectElementOffset_CertificateThumbprint = 1;

			// Token: 0x04000C23 RID: 3107
			private const int GlobalObjectElementOffset_Credentials = 2;

			// Token: 0x04000C24 RID: 3108
			private const int GlobalObjectElementOffset_GroupName = 3;

			// Token: 0x04000C25 RID: 3109
			private const int GlobalObjectElementOffset_Options = 4;

			// Token: 0x04000C26 RID: 3110
			private const int GlobalObjectElementOffset_CacheKey = 5;

			// Token: 0x04000C27 RID: 3111
			private readonly int defaultTimeoutInMiliseconds;

			// Token: 0x04000C28 RID: 3112
			private readonly string certificateThumbprint;

			// Token: 0x04000C29 RID: 3113
			private readonly ICredentials credentials;

			// Token: 0x04000C2A RID: 3114
			private readonly string groupName;

			// Token: 0x04000C2B RID: 3115
			private readonly HttpStream.ChannelOptions.BitmapOptions flags;

			// Token: 0x04000C2C RID: 3116
			private readonly string cacheKey;

			// Token: 0x02000227 RID: 551
			[Flags]
			private enum BitmapOptions
			{
				// Token: 0x04000F3D RID: 3901
				None = 0,
				// Token: 0x04000F3E RID: 3902
				ChannelModeMask = 255,
				// Token: 0x04000F3F RID: 3903
				AutoRedirect = 256,
				// Token: 0x04000F40 RID: 3904
				GzipCompression = 512,
				// Token: 0x04000F41 RID: 3905
				PaasInfrastructure = 65536,
				// Token: 0x04000F42 RID: 3906
				PbiDataset = 131072
			}

			// Token: 0x02000228 RID: 552
			private sealed class NetworkCredentialUserContext : UserContext
			{
				// Token: 0x06001535 RID: 5429 RVA: 0x00047C60 File Offset: 0x00045E60
				public NetworkCredentialUserContext(string userId, string password)
				{
					this.credentials = new NetworkCredential(userId, password);
					using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
					{
						byte[] array = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
						byte[] bytes = Encoding.UTF8.GetBytes(userId);
						this.connectionGroupName = string.Format("{0}|{1}", Convert.ToBase64String(bytes), Convert.ToBase64String(array));
					}
				}

				// Token: 0x06001536 RID: 5430 RVA: 0x00047CDC File Offset: 0x00045EDC
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					credentials = this.credentials;
					groupName = this.connectionGroupName;
					return true;
				}

				// Token: 0x06001537 RID: 5431 RVA: 0x00047CEF File Offset: 0x00045EEF
				protected override void ExecuteInUserContextImpl(Action action)
				{
					action();
				}

				// Token: 0x06001538 RID: 5432 RVA: 0x00047CF7 File Offset: 0x00045EF7
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return action();
				}

				// Token: 0x06001539 RID: 5433 RVA: 0x00047CFF File Offset: 0x00045EFF
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					request.Credentials = this.credentials;
					request.ConnectionGroupName = this.connectionGroupName;
				}

				// Token: 0x0600153A RID: 5434 RVA: 0x00047D19 File Offset: 0x00045F19
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
				}

				// Token: 0x04000F43 RID: 3907
				private ICredentials credentials;

				// Token: 0x04000F44 RID: 3908
				private string connectionGroupName;
			}

			// Token: 0x02000229 RID: 553
			private sealed class AuthenticatedComposedUserContext : UserContext
			{
				// Token: 0x0600153B RID: 5435 RVA: 0x00047D1B File Offset: 0x00045F1B
				public AuthenticatedComposedUserContext(AuthenticationHandle handle, UserContext context)
				{
					this.handle = handle;
					this.context = context;
				}

				// Token: 0x0600153C RID: 5436 RVA: 0x00047D31 File Offset: 0x00045F31
				public override bool TryGetCredentials(out ICredentials credentials, out string groupName)
				{
					return this.context.TryGetCredentials(out credentials, out groupName);
				}

				// Token: 0x0600153D RID: 5437 RVA: 0x00047D40 File Offset: 0x00045F40
				protected override void ExecuteInUserContextImpl(Action action)
				{
					this.context.ExecuteInUserContext(action);
				}

				// Token: 0x0600153E RID: 5438 RVA: 0x00047D4E File Offset: 0x00045F4E
				protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
				{
					return this.context.ExecuteInUserContext<TResult>(action);
				}

				// Token: 0x0600153F RID: 5439 RVA: 0x00047D5C File Offset: 0x00045F5C
				protected override void UpdateHttpRequestImpl(HttpWebRequest request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Add(HttpRequestHeader.Authorization, string.Format("{0} {1}", this.handle.AuthenticationScheme, this.handle.GetAccessToken()));
				}

				// Token: 0x06001540 RID: 5440 RVA: 0x00047D97 File Offset: 0x00045F97
				protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
				{
					this.context.UpdateHttpRequest(request);
					request.Headers.Authorization = new AuthenticationHeaderValue(this.handle.AuthenticationScheme, this.handle.GetAccessToken());
				}

				// Token: 0x04000F45 RID: 3909
				private AuthenticationHandle handle;

				// Token: 0x04000F46 RID: 3910
				private UserContext context;
			}
		}

		// Token: 0x02000184 RID: 388
		private static class HttpChannelManager
		{
			// Token: 0x060011D8 RID: 4568 RVA: 0x0003DCFC File Offset: 0x0003BEFC
			public static void GetControllerAndUserContext(ConnectionInfo info, CookieContainer cookieContainer, out HttpStream.HttpChannelController controller, out UserContext context)
			{
				HttpStream.ChannelOptions channelOptions = new HttpStream.ChannelOptions(info, out context);
				if (!HttpStream.HttpChannelManager.controllers.TryGetResource<HttpStream.HttpChannelController>(channelOptions.CacheKey, out controller))
				{
					HttpStream.HttpChannelMode mode = channelOptions.Mode;
					if (mode != HttpStream.HttpChannelMode.Legacy)
					{
						if (mode != HttpStream.HttpChannelMode.PaasInfra)
						{
							throw new NotSupportedException(string.Format("Invalid channel mode - '{0}' is not supported!", channelOptions.Mode));
						}
						controller = new HttpStream.PaasInfraController(channelOptions, cookieContainer);
					}
					else
					{
						controller = new HttpStream.LegacyController(channelOptions, cookieContainer);
					}
					HttpStream.HttpChannelManager.controllers.Insert<HttpStream.HttpChannelController>(channelOptions.CacheKey, ref controller);
				}
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x0003DD7C File Offset: 0x0003BF7C
			public static void ReleaseController(HttpStream.HttpChannelController controller)
			{
				HttpStream.HttpChannelManager.controllers.Remove(controller.CacheKey);
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0003DD90 File Offset: 0x0003BF90
			private static void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
			{
				HttpStream.HttpChannelController httpChannelController = item as HttpStream.HttpChannelController;
				if (httpChannelController != null)
				{
					HttpStream.HttpChannelMode mode = httpChannelController.Mode;
					if (mode != HttpStream.HttpChannelMode.Legacy)
					{
						if (mode != HttpStream.HttpChannelMode.PaasInfra)
						{
							typeCode = null;
						}
						else
						{
							typeCode = "HttpStream.PaasInfraController";
						}
					}
					else
					{
						typeCode = "HttpStream.LegacyController";
					}
					if (!string.IsNullOrEmpty(typeCode))
					{
						item = httpChannelController.ToGlobalObject();
					}
					if (!cacheName.Equals("XmlaLibControllerCache", StringComparison.Ordinal))
					{
						return;
					}
					IDictionary<string, HttpStream.HttpChannelController> dictionary = HttpStream.HttpChannelManager.activeControllers;
					lock (dictionary)
					{
						HttpStream.HttpChannelManager.activeControllers.Remove(httpChannelController.CacheKey);
						return;
					}
				}
				typeCode = null;
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x0003DE30 File Offset: 0x0003C030
			private static object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
			{
				bool flag;
				if (!(typeCode == "HttpStream.LegacyController"))
				{
					if (!(typeCode == "HttpStream.PaasInfraController"))
					{
						return cachedItem;
					}
					flag = false;
				}
				else
				{
					flag = true;
				}
				IList<object> list = (IList<object>)cachedItem;
				HttpStream.HttpChannelController httpChannelController;
				if (cacheName.Equals("XmlaLibControllerManager", StringComparison.Ordinal))
				{
					string cacheKeyFromGlobalObject = HttpStream.ChannelOptions.GetCacheKeyFromGlobalObject(list, 0);
					IDictionary<string, HttpStream.HttpChannelController> dictionary = HttpStream.HttpChannelManager.activeControllers;
					bool flag3;
					lock (dictionary)
					{
						if (HttpStream.HttpChannelManager.activeControllers.TryGetValue(cacheKeyFromGlobalObject, out httpChannelController))
						{
							httpChannelController.RefreshFromGlobalObject(list, out flag3);
						}
						else
						{
							httpChannelController = HttpStream.HttpChannelManager.CreateControllerFromGlobalObject(flag, list, out flag3);
							HttpStream.HttpChannelManager.activeControllers.Add(cacheKeyFromGlobalObject, httpChannelController);
						}
					}
					if (flag3)
					{
						HttpStream.HttpChannelManager.controllers.ReplaceSharedResource(httpChannelController.CacheKey, httpChannelController.ToGlobalObject());
					}
				}
				else
				{
					bool flag2;
					httpChannelController = HttpStream.HttpChannelManager.CreateControllerFromGlobalObject(flag, list, out flag2);
				}
				return httpChannelController;
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x0003DF0C File Offset: 0x0003C10C
			private static HttpStream.HttpChannelController CreateControllerFromGlobalObject(bool isLegacyController, IList<object> elements, out bool hasElementsUpdate)
			{
				if (isLegacyController)
				{
					return new HttpStream.LegacyController(elements, out hasElementsUpdate);
				}
				return new HttpStream.PaasInfraController(elements, out hasElementsUpdate);
			}

			// Token: 0x04000C2D RID: 3117
			private const string ControllerCacheName = "XmlaLibControllerCache";

			// Token: 0x04000C2E RID: 3118
			private const string ControllerManagerName = "XmlaLibControllerManager";

			// Token: 0x04000C2F RID: 3119
			private const string ControllerTypeCode_Legacy = "HttpStream.LegacyController";

			// Token: 0x04000C30 RID: 3120
			private const string ControllerTypeCode_PaasInfra = "HttpStream.PaasInfraController";

			// Token: 0x04000C31 RID: 3121
			private const int ControllerCacheTimeoutInMinutes = 10;

			// Token: 0x04000C32 RID: 3122
			private static SharedMemoryCache controllerCache = SharedMemoryCache.Create("XmlaLibControllerCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), StringComparer.OrdinalIgnoreCase, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000C33 RID: 3123
			private static SharedResourceManager controllers = SharedResourceManager.Create("XmlaLibControllerManager", StringComparer.OrdinalIgnoreCase, HttpStream.HttpChannelManager.controllerCache, new PrepareItemForCaching(HttpStream.HttpChannelManager.PrepareItemForCaching), new ConvertCachedItem(HttpStream.HttpChannelManager.ConvertCachedItem));

			// Token: 0x04000C34 RID: 3124
			private static IDictionary<string, HttpStream.HttpChannelController> activeControllers = new Dictionary<string, HttpStream.HttpChannelController>();
		}

		// Token: 0x02000185 RID: 389
		private sealed class LegacyController : HttpStream.HttpChannelController
		{
			// Token: 0x060011DE RID: 4574 RVA: 0x0003DFA7 File Offset: 0x0003C1A7
			public LegacyController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x0003DFB4 File Offset: 0x0003C1B4
			internal LegacyController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x0003DFCC File Offset: 0x0003C1CC
			protected override void GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer, IDictionary<string, string> headers)
			{
				base.GetDefaultStreamHeaders(info, dataSource, coreServer, headers);
				if (!string.IsNullOrEmpty(info.ApplicationName))
				{
					headers.Add("SspropInitAppName", info.ApplicationName);
				}
				if (info.IsPbiDataset)
				{
					if (!object.Equals(info.ConnectionActivityId, Guid.Empty))
					{
						headers.Add("X-AS-ActivityID", info.ConnectionActivityId.ToString());
					}
					if (!string.IsNullOrEmpty(info.ServiceToServiceToken))
					{
						headers.Add("x-ms-xls2stoken", info.ServiceToServiceToken);
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x0003E088 File Offset: 0x0003C288
			protected override void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				base.GetPerRequestHeaders(owner, context, headers);
				if (!string.IsNullOrEmpty(owner.soapActionHeader))
				{
					headers.Add(new KeyValuePair<string, string>(owner.soapActionHeader, owner.soapActionValue));
				}
				headers.Add(new KeyValuePair<string, string>("X-Transport-Caps-Negotiation-Flags", owner.GetTransportCapabilitiesString()));
				if (!string.IsNullOrEmpty(owner.SessionID))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-SessionID", owner.SessionID));
				}
				if (owner.IsSessionTokenNeeded)
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-GetSessionToken", true.ToString()));
				}
				Guid guid = owner.ActivityID;
				if (object.Equals(guid, Guid.Empty))
				{
					guid = Guid.NewGuid();
				}
				headers.Add(new KeyValuePair<string, string>("X-AS-ActivityID", guid.ToString()));
				if (!object.Equals(owner.RequestID, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-RequestID", owner.RequestID.ToString()));
				}
				if (!object.Equals(owner.CurrentActivityID, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-CurrentActivityID", owner.CurrentActivityID.ToString()));
				}
				if (!string.IsNullOrEmpty(owner.info.RoutingToken))
				{
					headers.Add(new KeyValuePair<string, string>("X-AS-Routing", owner.info.RoutingToken));
				}
			}

			// Token: 0x060011E2 RID: 4578 RVA: 0x0003E20C File Offset: 0x0003C40C
			protected override void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
				base.ProcessWebResponse(owner, context, response);
				string text;
				if (response.TryGetResponseHeaderValue("OutdatedVersion", out text))
				{
					owner.outdatedVersion = true;
					throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
				}
				TransportCapabilities transportCapabilities = new TransportCapabilities();
				string text2;
				if (response.TryGetResponseHeaderValue("X-Transport-Caps-Negotiation-Flags", out text2))
				{
					transportCapabilities.FromString(text2);
				}
				transportCapabilities.ContentTypeNegotiated = true;
				owner.SetTransportCapabilities(transportCapabilities);
				owner.info.RoutingToken = response.GetResponseHeaderValue("X-AS-Routing");
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x0003E28F File Offset: 0x0003C48F
			protected override HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient)
			{
				if (useHttpClient)
				{
					return new HttpStream.LegacyController.HttpClientXmlaOperation(owner, this);
				}
				return new HttpStream.LegacyController.WebRequestXmlaOperation(owner, this);
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
			protected override HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception)
			{
				if (error.StatusCode == HttpStatusCode.InternalServerError)
				{
					string text;
					if (!error.TryGetResponseHeaderValue("Content-Type", out text))
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					if (!HttpStream.contentTypeRegex.Match(text).Success)
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					owner.StreamException = error.CreateStreamException(null);
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Ignore;
				}
				else
				{
					if ((error.ErrorStatus == null || error.ErrorStatus.Value == WebExceptionStatus.ProtocolError) && error.StatusCode == HttpStatusCode.Unauthorized)
					{
						exception = error.CreateStreamException(new ConnectionExceptionCause?(ConnectionExceptionCause.AuthenticationFailed));
						return HttpStream.HttpChannelController.WebErrorClass.Raise;
					}
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Raise;
				}
			}

			// Token: 0x060011E5 RID: 4581 RVA: 0x0003E354 File Offset: 0x0003C554
			internal static void GetSandboxDatabaseInformation(string url, string user, string password, bool specificVersion, CookieContainer cookieContainer, out string databaseId, out string databaseName, out string dataSourceVersion)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = "POST";
				httpWebRequest.Timeout = -1;
				ICredentials credentials2;
				if (!string.IsNullOrEmpty(user))
				{
					ICredentials credentials = new NetworkCredential(user, password);
					credentials2 = credentials;
				}
				else
				{
					credentials2 = CredentialCache.DefaultCredentials;
				}
				httpWebRequest.Credentials = credentials2;
				httpWebRequest.UserAgent = "ADOMD.NET";
				httpWebRequest.ContentLength = 0L;
				httpWebRequest.CookieContainer = cookieContainer;
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					if (httpWebResponse.ContentLength > 0L)
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(httpWebResponse.GetResponseStream())
						{
							DtdProcessing = DtdProcessing.Prohibit
						})
						{
							try
							{
								xmlTextReader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
								if (xmlTextReader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
								{
									xmlTextReader.Skip();
								}
								xmlTextReader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
								XmlaClient.CheckForSoapFault(xmlTextReader, new XmlaResult(), true);
								throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected soap:Fault, got {0}", xmlTextReader.Name));
							}
							catch (XmlException ex)
							{
								throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
							}
							catch (IOException ex2)
							{
								throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
							}
						}
					}
					databaseId = httpWebResponse.Headers["DatabaseId"];
					if (databaseId != null)
					{
						databaseId = databaseId.Trim();
					}
					if (string.IsNullOrEmpty(databaseId))
					{
						throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
					}
					databaseName = httpWebResponse.Headers["DatabaseName"];
					if (databaseName != null)
					{
						databaseName = databaseName.Trim();
					}
					if (string.IsNullOrEmpty(databaseName))
					{
						throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
					}
					if (!specificVersion)
					{
						dataSourceVersion = httpWebResponse.Headers["DataSourceVersion"];
						if (!string.IsNullOrEmpty(dataSourceVersion))
						{
							dataSourceVersion = dataSourceVersion.Trim();
						}
						if (string.IsNullOrEmpty(dataSourceVersion))
						{
							throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
						}
					}
					else
					{
						dataSourceVersion = null;
					}
				}
			}

			// Token: 0x0200022A RID: 554
			internal static class XmlaHttpHeaders
			{
				// Token: 0x04000F47 RID: 3911
				public const string AcquireTokenStats = "X-AS-AcquireTokenStats";

				// Token: 0x04000F48 RID: 3912
				public const string ActivityId = "X-AS-ActivityID";

				// Token: 0x04000F49 RID: 3913
				public const string TransportCapabilitiesNegotiation = "X-Transport-Caps-Negotiation-Flags";

				// Token: 0x04000F4A RID: 3914
				public const string ApplicationName = "SspropInitAppName";

				// Token: 0x04000F4B RID: 3915
				public const string SessionId = "X-AS-SessionID";

				// Token: 0x04000F4C RID: 3916
				public const string SessionTokenRequest = "X-AS-GetSessionToken";

				// Token: 0x04000F4D RID: 3917
				public const string RequestId = "X-AS-RequestID";

				// Token: 0x04000F4E RID: 3918
				public const string CurrentActivityId = "X-AS-CurrentActivityID";

				// Token: 0x04000F4F RID: 3919
				public const string XASRouting = "X-AS-Routing";

				// Token: 0x04000F50 RID: 3920
				public const string OutdatedVersion = "OutdatedVersion";
			}

			// Token: 0x0200022B RID: 555
			private abstract class LegacyXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x06001541 RID: 5441 RVA: 0x00047DCB File Offset: 0x00045FCB
				protected LegacyXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001542 RID: 5442 RVA: 0x00047DD5 File Offset: 0x00045FD5
				public sealed override string GetExtendedErrorInfo()
				{
					return string.Empty;
				}

				// Token: 0x06001543 RID: 5443 RVA: 0x00047DDC File Offset: 0x00045FDC
				protected sealed override void EnsureCanRead()
				{
					if (base.Owner.outdatedVersion)
					{
						throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
					}
					base.EnsureCanRead();
				}

				// Token: 0x06001544 RID: 5444 RVA: 0x00047E10 File Offset: 0x00046010
				protected sealed override XmlaDataType GetResponseDataTypeImpl()
				{
					string text;
					if (!this.TryGetResponseHeaderValueImpl("Content-Type", out text))
					{
						return XmlaDataType.Unknown;
					}
					return HttpStream.HttpXmlaOperation.GetResponseDataTypeFromContentType(text);
				}
			}

			// Token: 0x0200022C RID: 556
			private sealed class WebRequestXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x06001545 RID: 5445 RVA: 0x00047E34 File Offset: 0x00046034
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001546 RID: 5446 RVA: 0x00047E3E File Offset: 0x0004603E
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x06001547 RID: 5447 RVA: 0x00047E4D File Offset: 0x0004604D
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001548 RID: 5448 RVA: 0x00047E5B File Offset: 0x0004605B
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x06001549 RID: 5449 RVA: 0x00047E6F File Offset: 0x0004606F
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F51 RID: 3921
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x0200022D RID: 557
			private sealed class HttpClientXmlaOperation : HttpStream.LegacyController.LegacyXmlaOperation
			{
				// Token: 0x0600154A RID: 5450 RVA: 0x00047E88 File Offset: 0x00046088
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.LegacyController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x0600154B RID: 5451 RVA: 0x00047E92 File Offset: 0x00046092
				protected override Stream StartRequestImpl()
				{
					return this.manager.StartRequest(this, true);
				}

				// Token: 0x0600154C RID: 5452 RVA: 0x00047EA1 File Offset: 0x000460A1
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x0600154D RID: 5453 RVA: 0x00047EB3 File Offset: 0x000460B3
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x0600154E RID: 5454 RVA: 0x00047EC1 File Offset: 0x000460C1
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x0600154F RID: 5455 RVA: 0x00047ED5 File Offset: 0x000460D5
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F52 RID: 3922
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000186 RID: 390
		private sealed class PaasInfraController : HttpStream.HttpChannelController
		{
			// Token: 0x060011E6 RID: 4582 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
			public PaasInfraController(HttpStream.ChannelOptions options, CookieContainer cookieContainer)
				: base(options, cookieContainer)
			{
			}

			// Token: 0x060011E7 RID: 4583 RVA: 0x0003E5BC File Offset: 0x0003C7BC
			internal PaasInfraController(IList<object> elements, out bool hasElementsUpdate)
			{
				int num;
				base..ctor(elements, out num, out hasElementsUpdate);
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
			protected override void GetDefaultStreamHeaders(ConnectionInfo info, Uri dataSource, string coreServer, IDictionary<string, string> headers)
			{
				if ((info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection) && string.IsNullOrEmpty(info.Catalog))
				{
					throw new InvalidDataException("Invalid XMLA database");
				}
				if (info.IsPbiPremiumXmlaEp && info.IsWorkloadDirectConnection)
				{
					if (!string.IsNullOrEmpty(info.ContextualIdentityKey))
					{
						throw new InvalidDataException("Invalid ContextualIdentityKey");
					}
					if (!string.IsNullOrEmpty(info.ContextualIdentityType))
					{
						throw new InvalidDataException("Invalid ContextualIdentityType");
					}
				}
				HttpStream.PaasInfraController.TransientModelMode transientModelMode = HttpStream.PaasInfraController.TransientModelMode.Disabled;
				if (string.Equals(info.TransientModelMode, "Enabled", StringComparison.InvariantCultureIgnoreCase))
				{
					if (!info.IsPbiPremiumInternal && !info.IsWorkloadDirectConnection)
					{
						throw new InvalidDataException("Invalid TransientModelMode");
					}
					transientModelMode = HttpStream.PaasInfraController.TransientModelMode.Enabled;
				}
				if ((info.IsPbiPremiumXmlaEp && string.IsNullOrEmpty(info.PbiPremiumWorkspaceObjectId)) || (!info.IsPbiPremiumXmlaEp && !string.IsNullOrEmpty(info.PbiPremiumWorkspaceObjectId)))
				{
					throw new InvalidDataException("Invalid PbiPremiumWorkspaceObjectId");
				}
				base.GetDefaultStreamHeaders(info, dataSource, coreServer, headers);
				if (info.IsInternalPaaSInfrastructure || info.IsWorkloadDirectConnection)
				{
					Match match = HttpStream.PaasInfraController.xmlaPumpUriRegex.Match(dataSource.ToString());
					if (match.Success)
					{
						headers.Add("x-ms-virtualserviceobjectid", match.Groups[1].ToString());
					}
				}
				headers.Add("x-ms-xmlaserver", coreServer);
				if (info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection)
				{
					if (!string.IsNullOrEmpty(info.Catalog))
					{
						headers.Add("x-ms-xmladatabase", info.Catalog);
					}
					if (string.Equals(info.BypassAuthorization, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						headers.Add("x-ms-xmlabypassauthorization", "true");
						if (!string.IsNullOrEmpty(info.RestrictUser))
						{
							headers.Add("x-ms-xmlauser", info.RestrictUser);
						}
						if (!string.IsNullOrEmpty(info.RestrictRoles))
						{
							headers.Add("x-ms-xmlaroles", info.RestrictRoles);
						}
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
				if (((info.IsPbiPremiumXmlaEp && !info.IsPbiPremiumXmlaEpWithPowerBIEmbedToken) || info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection) && !string.IsNullOrEmpty(info.ContextualIdentity))
				{
					headers.Add("x-ms-xmlacontextualidentity", info.ContextualIdentity);
				}
				if (info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection)
				{
					if (!string.IsNullOrEmpty(info.ContextualIdentityKey))
					{
						headers.Add("x-ms-xmlacontextualidentitykey", info.ContextualIdentityKey);
					}
					if (!string.IsNullOrEmpty(info.ContextualIdentityType))
					{
						headers.Add("x-ms-xmlacontextualidentitytype", info.ContextualIdentityType);
					}
				}
				if (info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection)
				{
					headers.Add("x-ms-xmlatransientmodelmode", Convert.ToString((int)transientModelMode, CultureInfo.InvariantCulture));
				}
				if (info.IsPbiPremiumXmlaEp && info.UseAadTokenInPublicXmlaEP)
				{
					headers.Add("x-ms-xmlaworkspaceobjectid", info.PbiPremiumWorkspaceObjectId);
				}
				if ((info.IsPbiPremiumXmlaEp || info.IsPbiPremiumInternal || info.IsWorkloadDirectConnection) && (info.IntendedUsage == IntendedUsage.ScheduledProcessing || info.IntendedUsage == IntendedUsage.InteractiveProcessing))
				{
					headers.Add("x-ms-routing-scenario", "Processing");
				}
				if (info.IsPbiPremiumXmlaEp || info.IsPbiPremiumInternal)
				{
					switch (info.PbipAccessMode)
					{
					case ConnectionAccessMode.ReadWrite:
						headers.Add("x-ms-xmlareadonly", "0");
						break;
					case ConnectionAccessMode.ReadOnly:
						headers.Add("x-ms-xmlareadonly", "1");
						break;
					case ConnectionAccessMode.RestrictedReadOnly:
						headers.Add("x-ms-xmlareadonly", "2");
						break;
					}
					if (info.IsScaleOutAutoSyncEnabled)
					{
						headers.Add("x-ms-xmlascaleoutautosync", "1");
					}
				}
				AuthenticationEndpoint endpoint = info.AuthHandle.Endpoint;
				string text;
				if (endpoint != AuthenticationEndpoint.AadV1)
				{
					if (endpoint != AuthenticationEndpoint.AadV2)
					{
						text = "Unknown";
					}
					else
					{
						text = "MSAL";
					}
				}
				else
				{
					text = "ADAL";
				}
				headers.Add("x-ms-xmlaapp-general-info", AsPaasHelper.BuildGeneralInfoHeader(text, info.GetTokensCacheUsage(), !string.IsNullOrEmpty(info.UserID), info.IsLinkReference, info.ApplicationName));
				if (info.IsBinarySupported)
				{
					headers.Add("x-ms-xmlaclienttraits", "1");
				}
				if (info.IsPbiDataset)
				{
					if (!object.Equals(info.ConnectionActivityId, Guid.Empty))
					{
						headers.Add("X-AS-ActivityID", info.ConnectionActivityId.ToString());
					}
					if (!string.IsNullOrEmpty(info.ServiceToServiceToken))
					{
						headers.Add("x-ms-xls2stoken", info.ServiceToServiceToken);
					}
					headers.Add("x-ms-xmlaintendedusage", info.IntendedUsage.ToString("D"));
				}
				headers.Add("x-ms-xmladedicatedconnection", info.DedicatedAdminConnection ? "1" : "0");
			}

			// Token: 0x060011E9 RID: 4585 RVA: 0x0003EA74 File Offset: 0x0003CC74
			protected override void GetPerRequestHeaders(HttpStream owner, object context, IList<KeyValuePair<string, string>> headers)
			{
				HttpStream.PaasInfraController.XmlaOperationContext xmlaOperationContext = (HttpStream.PaasInfraController.XmlaOperationContext)context;
				base.GetPerRequestHeaders(owner, context, headers);
				headers.Add(new KeyValuePair<string, string>("x-ms-xmlacaps-negotiation-flags", xmlaOperationContext.RequestTransportCapabilities));
				if (!string.IsNullOrEmpty(owner.SessionID))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-xmlasession-id", owner.SessionID));
				}
				headers.Add(new KeyValuePair<string, string>("x-ms-request-registration-id", xmlaOperationContext.RegistrationId.ToString()));
				headers.Add(new KeyValuePair<string, string>("x-ms-accepts-continuations", "1"));
				headers.Add(new KeyValuePair<string, string>("x-ms-round-trip-id", xmlaOperationContext.RoundTripId.ToString()));
				if (!owner.info.IsInternalPaaSInfrastructure && !string.IsNullOrEmpty(xmlaOperationContext.RootActivityId))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-root-activity-id", xmlaOperationContext.RootActivityId));
				}
				if (!owner.info.IsInternalPaaSInfrastructure)
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-parent-activity-id", xmlaOperationContext.ParentActivityId.ToString()));
				}
				if (owner.info.IsPbiPremiumXmlaEp || owner.info.IsPbiPremiumInternal)
				{
					if (!string.IsNullOrEmpty(owner.info.PbipWorkloadResourceMoniker))
					{
						headers.Add(new KeyValuePair<string, string>("x-ms-workload-resource-moniker", owner.info.PbipWorkloadResourceMoniker));
					}
					if (!string.IsNullOrEmpty(owner.info.PbipCoreServiceRoutingHint))
					{
						headers.Add(new KeyValuePair<string, string>("x-ms-routing-hint", owner.info.PbipCoreServiceRoutingHint));
					}
				}
				if (owner.info.IsPbiPremiumInternal && !xmlaOperationContext.IsUnderLROProtocol && !object.Equals(owner.info.SourceCapacityObjectId, Guid.Empty))
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-src-capacity-id", owner.info.SourceCapacityObjectId.ToString()));
				}
				if (owner.info.IsInternalPaaSInfrastructure)
				{
					headers.Add(new KeyValuePair<string, string>("x-ms-root-activity-id", xmlaOperationContext.RootActivityId));
					headers.Add(new KeyValuePair<string, string>("x-ms-parent-activity-id", xmlaOperationContext.ParentActivityId.ToString()));
					headers.Add(new KeyValuePair<string, string>("x-ms-correlatedservicecalls", "1"));
				}
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x0003ECB1 File Offset: 0x0003CEB1
			protected override void HandleResponseStatusCode(int statusCode, Exception ex)
			{
				if (statusCode >= 300 && statusCode <= 399)
				{
					AsPaasEndpointInfo.InvalidateCache();
					throw new AdomdConnectionException(XmlaSR.Connection_AnalysisServicesInstanceWasMoved, ex, ConnectionExceptionCause.ServerHasMoved);
				}
				base.HandleResponseStatusCode(statusCode, ex);
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
			protected override void ProcessWebResponse(HttpStream owner, object context, HttpStream.HttpChannelController.WebResponseInfo response)
			{
				base.ProcessWebResponse(owner, context, response);
				if (owner.info.IsPbiPremiumInternal || owner.info.IsWorkloadDirectConnection)
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						owner.info.PbipCoreServiceRoutingHint = response.GetResponseHeaderValue("x-ms-routing-hint");
					}
					else
					{
						owner.info.PbipCoreServiceRoutingHint = string.Empty;
					}
				}
				else if (owner.info.IsPbiPremiumXmlaEp)
				{
					if (response.StatusCode == HttpStatusCode.OK)
					{
						owner.info.PbipWorkloadResourceMoniker = response.GetResponseHeaderValue("x-ms-workload-resource-moniker");
						owner.info.PbipCoreServiceRoutingHint = response.GetResponseHeaderValue("x-ms-routing-hint");
					}
					else
					{
						owner.info.PbipWorkloadResourceMoniker = string.Empty;
						owner.info.PbipCoreServiceRoutingHint = string.Empty;
					}
				}
				string text;
				if (!response.TryGetResponseHeaderValue("x-ms-xmlacaps-negotiation-flags", out text))
				{
					throw new InvalidDataException(string.Format("The '{0}' header is missing from the HTTP response!", "x-ms-xmlacaps-negotiation-flags"));
				}
				((HttpStream.PaasInfraController.XmlaOperationContext)context).ResponseTransportCapabilities = text;
			}

			// Token: 0x060011EC RID: 4588 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
			protected override HttpStream.HttpXmlaOperation CreateNewOperation(HttpStream owner, bool useHttpClient)
			{
				object obj = new HttpStream.PaasInfraController.XmlaOperationContext(owner, owner.info.IsInternalPaaSInfrastructure);
				if (useHttpClient)
				{
					return new HttpStream.PaasInfraController.HttpClientXmlaOperation(owner, this)
					{
						Context = obj
					};
				}
				return new HttpStream.PaasInfraController.WebRequestXmlaOperation(owner, this)
				{
					Context = obj
				};
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x0003EE28 File Offset: 0x0003D028
			protected override HttpStream.HttpChannelController.WebErrorClass ClassifyWebError(HttpStream owner, object context, HttpStream.HttpChannelController.WebErrorInfo error, out Exception exception)
			{
				string text;
				if ((owner.info.IsPbiPremiumInternal || owner.info.IsPbiPremiumXmlaEp || owner.info.IsWorkloadDirectConnection) && error.StatusCode == HttpStatusCode.BadRequest && error.TryGetResponseHeaderValue("x-ms-wrong-node", out text) && text == "1")
				{
					exception = null;
					return HttpStream.HttpChannelController.WebErrorClass.Ignore;
				}
				if (((HttpStream.PaasInfraController.XmlaOperationContext)context).IsUnderLROProtocol)
				{
					HttpStatusCode statusCode = error.StatusCode;
					if (statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.ServiceUnavailable)
					{
						exception = null;
						return HttpStream.HttpChannelController.WebErrorClass.Retry;
					}
				}
				exception = error.CreatePaasInfraConnectionException(owner.info.AsInstanceType);
				return HttpStream.HttpChannelController.WebErrorClass.Raise;
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x0003EECE File Offset: 0x0003D0CE
			private static int GetMinimumWaitTimeForRetryAttempt(int attempt)
			{
				if (HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length > attempt)
				{
					return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[attempt];
				}
				return HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt[HttpStream.PaasInfraController.MinimumWaitTimeInMsForRetryAttempt.Length - 1];
			}

			// Token: 0x04000C35 RID: 3125
			private const int MAX_IDLE_CONNECTION_TIMEOUT_IN_MS = 120000;

			// Token: 0x04000C36 RID: 3126
			private const int LRO_CLIENT_RECONNECT_REQUESTED = 1;

			// Token: 0x04000C37 RID: 3127
			private const int LRO_CLIENT_REQUEST_FINISHED = 0;

			// Token: 0x04000C38 RID: 3128
			private static TimeoutUtils.OnTimeoutAction onConnectTimeout = delegate(bool isOnDispose)
			{
				if (!isOnDispose)
				{
					throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
				}
				return true;
			};

			// Token: 0x04000C39 RID: 3129
			private static readonly int[] MinimumWaitTimeInMsForRetryAttempt = new int[] { 100, 200, 400, 800, 1600, 3200, 6400, 10000 };

			// Token: 0x04000C3A RID: 3130
			private static readonly Regex xmlaPumpUriRegex = new Regex("/([^/]*)/[^/]*/AS/ASWLService/xmla/pump", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x0200022E RID: 558
			private enum TransientModelMode
			{
				// Token: 0x04000F54 RID: 3924
				Disabled,
				// Token: 0x04000F55 RID: 3925
				Enabled
			}

			// Token: 0x0200022F RID: 559
			private sealed class XmlaOperationContext
			{
				// Token: 0x06001550 RID: 5456 RVA: 0x00047EF0 File Offset: 0x000460F0
				public XmlaOperationContext(HttpStream owner, bool isInternalPaaSInfrastructure)
				{
					this.RegistrationId = Guid.NewGuid();
					if (isInternalPaaSInfrastructure)
					{
						this.RootActivityId = HttpStream.PaasInfraController.XmlaOperationContext.GetActivityIdFromCallContext("x-ms-root-activity-id", true).ToString();
						this.ParentActivityId = HttpStream.PaasInfraController.XmlaOperationContext.GetActivityIdFromCallContext("x-ms-parent-activity-id", true);
					}
					else
					{
						this.RootActivityId = null;
						this.ParentActivityId = owner.RequestID;
						if (object.Equals(this.ParentActivityId, Guid.Empty))
						{
							this.ParentActivityId = Guid.NewGuid();
						}
					}
					this.RequestTransportCapabilities = owner.GetTransportCapabilitiesString();
					this.roundTripId = 0;
				}

				// Token: 0x17000748 RID: 1864
				// (get) Token: 0x06001551 RID: 5457 RVA: 0x00047F90 File Offset: 0x00046190
				// (set) Token: 0x06001552 RID: 5458 RVA: 0x00047F98 File Offset: 0x00046198
				public Guid RegistrationId { get; private set; }

				// Token: 0x17000749 RID: 1865
				// (get) Token: 0x06001553 RID: 5459 RVA: 0x00047FA1 File Offset: 0x000461A1
				// (set) Token: 0x06001554 RID: 5460 RVA: 0x00047FA9 File Offset: 0x000461A9
				public string RootActivityId { get; set; }

				// Token: 0x1700074A RID: 1866
				// (get) Token: 0x06001555 RID: 5461 RVA: 0x00047FB2 File Offset: 0x000461B2
				// (set) Token: 0x06001556 RID: 5462 RVA: 0x00047FBA File Offset: 0x000461BA
				public Guid ParentActivityId { get; private set; }

				// Token: 0x1700074B RID: 1867
				// (get) Token: 0x06001557 RID: 5463 RVA: 0x00047FC3 File Offset: 0x000461C3
				public string RequestTransportCapabilities { get; }

				// Token: 0x1700074C RID: 1868
				// (get) Token: 0x06001558 RID: 5464 RVA: 0x00047FCB File Offset: 0x000461CB
				// (set) Token: 0x06001559 RID: 5465 RVA: 0x00047FD3 File Offset: 0x000461D3
				public string ResponseTransportCapabilities { get; set; }

				// Token: 0x1700074D RID: 1869
				// (get) Token: 0x0600155A RID: 5466 RVA: 0x00047FDC File Offset: 0x000461DC
				public int RoundTripId
				{
					get
					{
						return this.roundTripId;
					}
				}

				// Token: 0x1700074E RID: 1870
				// (get) Token: 0x0600155B RID: 5467 RVA: 0x00047FE4 File Offset: 0x000461E4
				public bool IsUnderLROProtocol
				{
					get
					{
						return this.isUnderLROProtocol;
					}
				}

				// Token: 0x1700074F RID: 1871
				// (get) Token: 0x0600155C RID: 5468 RVA: 0x00047FEC File Offset: 0x000461EC
				// (set) Token: 0x0600155D RID: 5469 RVA: 0x00048008 File Offset: 0x00046208
				public XmlaDataType ResponseDataType
				{
					get
					{
						if (this.responseDataType != null)
						{
							return this.responseDataType.Value;
						}
						return XmlaDataType.Unknown;
					}
					set
					{
						this.responseDataType = new XmlaDataType?(value);
					}
				}

				// Token: 0x17000750 RID: 1872
				// (get) Token: 0x0600155E RID: 5470 RVA: 0x00048016 File Offset: 0x00046216
				public bool HasResponseDataType
				{
					get
					{
						return this.responseDataType != null;
					}
				}

				// Token: 0x0600155F RID: 5471 RVA: 0x00048023 File Offset: 0x00046223
				internal void MarkAsUnderLROProtocol()
				{
					this.isUnderLROProtocol = true;
					this.roundTripId++;
				}

				// Token: 0x06001560 RID: 5472 RVA: 0x0004803A File Offset: 0x0004623A
				internal void SetDataBytes(byte[] buffer)
				{
					if (buffer != null)
					{
						this.firstDataByte = new byte?(buffer[0]);
						this.lastDataByte = new byte?(buffer[1]);
						return;
					}
					this.firstDataByte = null;
					this.lastDataByte = null;
				}

				// Token: 0x06001561 RID: 5473 RVA: 0x00048074 File Offset: 0x00046274
				internal bool TryReadFirstDataByte(byte[] buffer, int offset)
				{
					if (this.firstDataByte == null)
					{
						return false;
					}
					buffer[offset] = this.firstDataByte.Value;
					this.firstDataByte = null;
					return true;
				}

				// Token: 0x06001562 RID: 5474 RVA: 0x000480A0 File Offset: 0x000462A0
				internal void SwitchLastDataByte(byte[] buffer, int offset, byte lastDataByte)
				{
					buffer[offset] = this.lastDataByte.Value;
					this.lastDataByte = new byte?(lastDataByte);
				}

				// Token: 0x06001563 RID: 5475 RVA: 0x000480BC File Offset: 0x000462BC
				internal bool TryGetLastDataByte(out byte lastByte)
				{
					if (this.lastDataByte == null)
					{
						lastByte = 0;
						return false;
					}
					lastByte = this.lastDataByte.Value;
					this.lastDataByte = null;
					return true;
				}

				// Token: 0x06001564 RID: 5476 RVA: 0x000480EC File Offset: 0x000462EC
				private static Guid GetActivityIdFromCallContext(string key, bool throwIfEmpty)
				{
					object obj;
					try
					{
						obj = CallContext.LogicalGetData(key);
					}
					catch
					{
						obj = null;
					}
					Guid guid = ((obj != null) ? ((Guid)obj) : Guid.Empty);
					if (throwIfEmpty && guid == Guid.Empty)
					{
						throw new InvalidOperationException(string.Format("{0} is not set on call context for internal AS Azure usage.", key));
					}
					return guid;
				}

				// Token: 0x04000F56 RID: 3926
				private const string RootActivityIdCallContextKey = "x-ms-root-activity-id";

				// Token: 0x04000F57 RID: 3927
				private const string ParentActivityIdCallContextKey = "x-ms-parent-activity-id";

				// Token: 0x04000F58 RID: 3928
				private int roundTripId;

				// Token: 0x04000F59 RID: 3929
				private XmlaDataType? responseDataType;

				// Token: 0x04000F5A RID: 3930
				private bool isUnderLROProtocol;

				// Token: 0x04000F5B RID: 3931
				private byte? firstDataByte;

				// Token: 0x04000F5C RID: 3932
				private byte? lastDataByte;
			}

			// Token: 0x02000230 RID: 560
			private abstract class PaasInfraXmlaOperation : HttpStream.HttpXmlaOperation
			{
				// Token: 0x06001565 RID: 5477 RVA: 0x0004814C File Offset: 0x0004634C
				protected PaasInfraXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001566 RID: 5478 RVA: 0x00048156 File Offset: 0x00046356
				public sealed override string GetExtendedErrorInfo()
				{
					if (!base.Owner.info.IsAsAzure && !base.Owner.info.IsPbiPremiumXmlaEp)
					{
						return string.Empty;
					}
					return this.GetExtendedErrorInfoFromResponse();
				}

				// Token: 0x06001567 RID: 5479
				protected abstract Stream StartRequestAndGetRequestPayload(bool isReconnect);

				// Token: 0x06001568 RID: 5480
				protected abstract string GetExtendedErrorInfoFromResponse();

				// Token: 0x06001569 RID: 5481 RVA: 0x00048188 File Offset: 0x00046388
				protected sealed override Stream StartRequestImpl()
				{
					return this.StartRequestAndGetRequestPayload(false);
				}

				// Token: 0x0600156A RID: 5482 RVA: 0x00048194 File Offset: 0x00046394
				protected sealed override void EnsureCanRead()
				{
					if (base.ResponsePayload == null)
					{
						((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SetDataBytes(null);
						byte[] array = new byte[2];
						TimeoutUtils.TimeLeft timeLeft = TimeoutUtils.TimeLeft.FromMs(120000);
						Stopwatch stopwatch = new Stopwatch();
						using (TimeoutUtils.TimeRestrictedMonitor timeRestrictedMonitor = new TimeoutUtils.TimeRestrictedMonitor(timeLeft, HttpStream.PaasInfraController.onConnectTimeout))
						{
							int num = 0;
							bool flag;
							do
							{
								flag = false;
								try
								{
									stopwatch.Reset();
									stopwatch.Start();
									base.EnsureCanRead();
									if (base.ResponsePayload != null)
									{
										timeRestrictedMonitor.Restart();
										num = 0;
										int num2 = base.ResponsePayload.Read(array, 0, 2);
										if (num2 == 0)
										{
											throw new AdomdConnectionException(XmlaSR.ConnectionBroken, null);
										}
										bool flag2;
										if (num2 != 1)
										{
											flag2 = true;
										}
										else if (base.ResponsePayload.Read(array, 1, 1) == 0)
										{
											byte b = array[0];
											if (b != 0)
											{
												if (b != 1)
												{
													throw new AdomdConnectionException(XmlaSR.UnknownServerResponseFormat, new ConnectionExceptionCause?(ConnectionExceptionCause.TransportProtocolError));
												}
												flag = true;
												((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).MarkAsUnderLROProtocol();
											}
											flag2 = false;
										}
										else
										{
											flag2 = true;
										}
										if (flag2)
										{
											((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SetDataBytes(array);
										}
									}
									else
									{
										flag = true;
										num++;
									}
									if (flag)
									{
										string text;
										if (string.IsNullOrEmpty(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId) && base.Owner.info.IsPaaSInfrastructure && this.TryGetResponseHeaderValueImpl("x-ms-root-activity-id", out text))
										{
											((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId = text;
										}
										timeRestrictedMonitor.CheckNow();
										int minimumWaitTimeForRetryAttempt = HttpStream.PaasInfraController.GetMinimumWaitTimeForRetryAttempt(num);
										long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
										if ((long)minimumWaitTimeForRetryAttempt > elapsedMilliseconds)
										{
											Thread.Sleep((int)((long)minimumWaitTimeForRetryAttempt - elapsedMilliseconds));
										}
										this.Reset(true, true, false);
										this.StartRequestAndGetRequestPayload(true);
									}
								}
								finally
								{
									if (stopwatch.IsRunning)
									{
										stopwatch.Stop();
									}
								}
							}
							while (flag);
							TransportCapabilities transportCapabilities = new TransportCapabilities();
							transportCapabilities.FromString(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseTransportCapabilities);
							transportCapabilities.ContentTypeNegotiated = true;
							base.Owner.SetTransportCapabilities(transportCapabilities);
						}
					}
				}

				// Token: 0x0600156B RID: 5483 RVA: 0x000483C0 File Offset: 0x000465C0
				protected sealed override XmlaDataType GetResponseDataTypeImpl()
				{
					if (((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).HasResponseDataType)
					{
						return ((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseDataType;
					}
					string text;
					XmlaDataType xmlaDataType;
					if (this.TryGetResponseHeaderValueImpl("Content-Type", out text))
					{
						xmlaDataType = HttpStream.HttpXmlaOperation.GetResponseDataTypeFromContentType(text);
						((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).ResponseDataType = xmlaDataType;
					}
					else
					{
						xmlaDataType = XmlaDataType.Unknown;
					}
					return xmlaDataType;
				}

				// Token: 0x0600156C RID: 5484 RVA: 0x0004841C File Offset: 0x0004661C
				protected sealed override int ReadResponsePayloadImpl(byte[] buffer, int offset, int size)
				{
					for (;;)
					{
						int num;
						bool flag;
						if (((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).TryReadFirstDataByte(buffer, offset))
						{
							num = 1;
							flag = false;
						}
						else
						{
							byte[] array = ((size == 1) ? new byte[1] : buffer);
							int num2 = ((size == 1) ? 0 : (offset + 1));
							int num3 = ((size == 1) ? 1 : (size - 1));
							num = base.ResponsePayload.Read(array, num2, num3);
							if (num > 0)
							{
								((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).SwitchLastDataByte(buffer, offset, (size == 1) ? array[0] : buffer[offset + num]);
								flag = false;
							}
							else
							{
								byte b;
								if (!((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).TryGetLastDataByte(out b))
								{
									goto IL_0127;
								}
								if (b != 0)
								{
									if (b != 1)
									{
										break;
									}
									flag = true;
									((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).MarkAsUnderLROProtocol();
									string text;
									if (string.IsNullOrEmpty(((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId) && base.Owner.info.IsPaaSInfrastructure && this.TryGetResponseHeaderValueImpl("x-ms-root-activity-id", out text))
									{
										((HttpStream.PaasInfraController.XmlaOperationContext)base.Context).RootActivityId = text;
									}
									this.Reset(true, true, false);
									this.StartRequestAndGetRequestPayload(true);
									this.EnsureCanRead();
								}
								else
								{
									flag = false;
								}
							}
						}
						if (!flag)
						{
							return num;
						}
					}
					throw new AdomdConnectionException(XmlaSR.UnknownServerResponseFormat, new ConnectionExceptionCause?(ConnectionExceptionCause.TransportProtocolError));
					IL_0127:
					throw new XmlaStreamException(XmlaSR.HttpStream_InvalidReadRequest(base.Status.ToString()));
				}
			}

			// Token: 0x02000231 RID: 561
			private sealed class WebRequestXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x0600156D RID: 5485 RVA: 0x00048577 File Offset: 0x00046777
				public WebRequestXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x0600156E RID: 5486 RVA: 0x00048581 File Offset: 0x00046781
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x0600156F RID: 5487 RVA: 0x00048593 File Offset: 0x00046793
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001570 RID: 5488 RVA: 0x000485A1 File Offset: 0x000467A1
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x06001571 RID: 5489 RVA: 0x000485B5 File Offset: 0x000467B5
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x06001572 RID: 5490 RVA: 0x000485C7 File Offset: 0x000467C7
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F62 RID: 3938
				private HttpStream.HttpXmlaOperation.WebRequestOperationManager manager;
			}

			// Token: 0x02000232 RID: 562
			private sealed class HttpClientXmlaOperation : HttpStream.PaasInfraController.PaasInfraXmlaOperation
			{
				// Token: 0x06001573 RID: 5491 RVA: 0x000485E0 File Offset: 0x000467E0
				public HttpClientXmlaOperation(HttpStream owner, HttpStream.PaasInfraController controller)
					: base(owner, controller)
				{
				}

				// Token: 0x06001574 RID: 5492 RVA: 0x000485EA File Offset: 0x000467EA
				protected override Stream StartRequestAndGetRequestPayload(bool isReconnect)
				{
					return this.manager.StartRequest(this, !isReconnect);
				}

				// Token: 0x06001575 RID: 5493 RVA: 0x000485FC File Offset: 0x000467FC
				protected override void CompleteRequestImpl()
				{
					((HttpStream.HttpChannelController.RequestPayloadStream)base.RequestPayload).MarkAsCompleted();
				}

				// Token: 0x06001576 RID: 5494 RVA: 0x0004860E File Offset: 0x0004680E
				protected override Stream GetResponseImpl()
				{
					return this.manager.GetResponse(this);
				}

				// Token: 0x06001577 RID: 5495 RVA: 0x0004861C File Offset: 0x0004681C
				protected override bool TryGetResponseHeaderValueImpl(string header, out string value)
				{
					return HttpHelper.TryGetHeaderValueFromResponse(this.manager.Response, header, out value);
				}

				// Token: 0x06001578 RID: 5496 RVA: 0x00048630 File Offset: 0x00046830
				protected override string GetExtendedErrorInfoFromResponse()
				{
					return AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(this.manager.Response);
				}

				// Token: 0x06001579 RID: 5497 RVA: 0x00048642 File Offset: 0x00046842
				protected override void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
				{
					this.manager.Reset(closeRequest, ref closeResponse);
					base.Reset(closeRequest, closeResponse, resetStatus);
				}

				// Token: 0x04000F63 RID: 3939
				private HttpStream.HttpXmlaOperation.HttpClientOperationManager manager;
			}
		}

		// Token: 0x02000187 RID: 391
		private enum HttpXmlaOperationStatus
		{
			// Token: 0x04000C3C RID: 3132
			Idle,
			// Token: 0x04000C3D RID: 3133
			Request,
			// Token: 0x04000C3E RID: 3134
			Response,
			// Token: 0x04000C3F RID: 3135
			Completed,
			// Token: 0x04000C40 RID: 3136
			Error
		}

		// Token: 0x02000188 RID: 392
		private abstract class HttpXmlaOperation : Disposable
		{
			// Token: 0x060011F0 RID: 4592 RVA: 0x0003EF40 File Offset: 0x0003D140
			protected HttpXmlaOperation(HttpStream owner, HttpStream.HttpChannelController controller)
			{
				this.Owner = owner;
				this.Controller = controller;
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0003EF56 File Offset: 0x0003D156
			public HttpStream Owner { get; }

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0003EF5E File Offset: 0x0003D15E
			public HttpStream.HttpChannelController Controller { get; }

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0003EF66 File Offset: 0x0003D166
			// (set) Token: 0x060011F4 RID: 4596 RVA: 0x0003EF6E File Offset: 0x0003D16E
			public object Context { get; set; }

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0003EF77 File Offset: 0x0003D177
			// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0003EF7F File Offset: 0x0003D17F
			public HttpStream.HttpXmlaOperationStatus Status { get; private set; }

			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0003EF88 File Offset: 0x0003D188
			// (set) Token: 0x060011F8 RID: 4600 RVA: 0x0003EF90 File Offset: 0x0003D190
			private protected Stream RequestPayload { protected get; private set; }

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0003EF99 File Offset: 0x0003D199
			// (set) Token: 0x060011FA RID: 4602 RVA: 0x0003EFA1 File Offset: 0x0003D1A1
			private protected Stream ResponsePayload { protected get; private set; }

			// Token: 0x060011FB RID: 4603 RVA: 0x0003EFAC File Offset: 0x0003D1AC
			public void StartRequest()
			{
				try
				{
					this.RequestPayload = this.StartRequestImpl();
					this.Status = HttpStream.HttpXmlaOperationStatus.Request;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x0003EFE8 File Offset: 0x0003D1E8
			public void WriteRequestPayload(byte[] buffer, int offset, int size)
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload == null)
				{
					throw new InvalidOperationException("Cannot write into the request-payload of a completed request!");
				}
				try
				{
					this.WriteRequestPayloadImpl(buffer, offset, size);
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x060011FD RID: 4605 RVA: 0x0003F034 File Offset: 0x0003D234
			public void CompleteRequest()
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload == null)
				{
					throw new InvalidOperationException("Cannot complete the request of a completed request!");
				}
				try
				{
					this.CompleteRequestImpl();
					this.RequestPayload = null;
					this.Status = HttpStream.HttpXmlaOperationStatus.Response;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
			}

			// Token: 0x060011FE RID: 4606 RVA: 0x0003F08C File Offset: 0x0003D28C
			public XmlaDataType GetResponseDataType()
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload != null)
				{
					throw new InvalidOperationException("Cannot get the response data-type when the request wasn't already completed!");
				}
				XmlaDataType responseDataTypeImpl;
				try
				{
					this.EnsureCanRead();
					responseDataTypeImpl = this.GetResponseDataTypeImpl();
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
				return responseDataTypeImpl;
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
			public int ReadResponsePayload(byte[] buffer, int offset, int size)
			{
				base.ThrowIfAlreadyDisposed();
				if (this.RequestPayload != null)
				{
					throw new InvalidOperationException("Cannot read the response payload when the request wasn't already completed!");
				}
				int num2;
				try
				{
					this.EnsureCanRead();
					int num = this.ReadResponsePayloadImpl(buffer, offset, size);
					if (num == 0)
					{
						this.Status = HttpStream.HttpXmlaOperationStatus.Completed;
					}
					num2 = num;
				}
				catch (Exception)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Error;
					throw;
				}
				return num2;
			}

			// Token: 0x06001200 RID: 4608
			public abstract string GetExtendedErrorInfo();

			// Token: 0x06001201 RID: 4609 RVA: 0x0003F140 File Offset: 0x0003D340
			protected static XmlaDataType GetResponseDataTypeFromContentType(string contentType)
			{
				Match match = HttpStream.contentTypeRegex.Match(contentType);
				if (!match.Success)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(match.Groups["content_type"].Value);
				if (dataTypeFromString == XmlaDataType.Undetermined || dataTypeFromString == XmlaDataType.Unknown)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(contentType), string.Empty);
				}
				return dataTypeFromString;
			}

			// Token: 0x06001202 RID: 4610
			protected abstract Stream StartRequestImpl();

			// Token: 0x06001203 RID: 4611
			protected abstract Stream GetResponseImpl();

			// Token: 0x06001204 RID: 4612
			protected abstract bool TryGetResponseHeaderValueImpl(string header, out string value);

			// Token: 0x06001205 RID: 4613
			protected abstract XmlaDataType GetResponseDataTypeImpl();

			// Token: 0x06001206 RID: 4614 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
			protected virtual void WriteRequestPayloadImpl(byte[] buffer, int offset, int size)
			{
				this.RequestPayload.Write(buffer, offset, size);
			}

			// Token: 0x06001207 RID: 4615 RVA: 0x0003F1B4 File Offset: 0x0003D3B4
			protected virtual void CompleteRequestImpl()
			{
				this.RequestPayload.Flush();
				this.RequestPayload.Close();
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x0003F1CC File Offset: 0x0003D3CC
			protected virtual void EnsureCanRead()
			{
				if (this.ResponsePayload == null)
				{
					this.ResponsePayload = this.GetResponseImpl();
				}
			}

			// Token: 0x06001209 RID: 4617 RVA: 0x0003F1E2 File Offset: 0x0003D3E2
			protected virtual int ReadResponsePayloadImpl(byte[] buffer, int offset, int size)
			{
				return this.ResponsePayload.Read(buffer, offset, size);
			}

			// Token: 0x0600120A RID: 4618 RVA: 0x0003F1F4 File Offset: 0x0003D3F4
			protected virtual void Reset(bool closeRequest, bool closeResponse, bool resetStatus)
			{
				if (this.RequestPayload != null)
				{
					if (closeRequest)
					{
						this.RequestPayload.Close();
					}
					this.RequestPayload = null;
				}
				if (this.ResponsePayload != null)
				{
					if (closeResponse)
					{
						this.ResponsePayload.Close();
					}
					this.ResponsePayload = null;
				}
				if (resetStatus)
				{
					this.Status = HttpStream.HttpXmlaOperationStatus.Idle;
				}
			}

			// Token: 0x0600120B RID: 4619 RVA: 0x0003F248 File Offset: 0x0003D448
			protected sealed override void Dispose(bool disposing)
			{
				if (disposing)
				{
					bool flag = this.Status == HttpStream.HttpXmlaOperationStatus.Error;
					try
					{
						this.Reset(true, true, true);
					}
					catch (Exception)
					{
						this.Status = HttpStream.HttpXmlaOperationStatus.Error;
						if (!flag)
						{
							throw;
						}
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x02000234 RID: 564
			protected struct WebRequestOperationManager
			{
				// Token: 0x17000751 RID: 1873
				// (get) Token: 0x0600157D RID: 5501 RVA: 0x00048695 File Offset: 0x00046895
				public HttpWebRequest Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x17000752 RID: 1874
				// (get) Token: 0x0600157E RID: 5502 RVA: 0x0004869D File Offset: 0x0004689D
				public HttpWebResponse Response
				{
					get
					{
						return this.response;
					}
				}

				// Token: 0x0600157F RID: 5503 RVA: 0x000486A8 File Offset: 0x000468A8
				public Stream StartRequest(HttpStream.HttpXmlaOperation operation, bool hasXmlaPayload)
				{
					Stream stream;
					this.request = operation.Controller.StartWebRequestBasedOperation(operation.Owner, operation.Context, out stream);
					if (!hasXmlaPayload)
					{
						stream.Close();
						stream = null;
					}
					return stream;
				}

				// Token: 0x06001580 RID: 5504 RVA: 0x000486E0 File Offset: 0x000468E0
				public Stream GetResponse(HttpStream.HttpXmlaOperation operation)
				{
					Stream stream;
					if (!operation.Controller.CompleteWebRequestBasedOperation(operation.Owner, operation.Context, this.request, out this.response, out stream))
					{
						return null;
					}
					return stream;
				}

				// Token: 0x06001581 RID: 5505 RVA: 0x00048718 File Offset: 0x00046918
				public void Reset(bool closeRequest, ref bool closeResponse)
				{
					if (closeRequest && this.request != null)
					{
						if (this.response == null)
						{
							this.request.Abort();
							closeResponse = false;
						}
						this.request = null;
					}
					if (closeResponse && this.response != null)
					{
						this.response.Close();
						this.response = null;
						closeResponse = false;
					}
				}

				// Token: 0x04000F65 RID: 3941
				private HttpWebRequest request;

				// Token: 0x04000F66 RID: 3942
				private HttpWebResponse response;
			}

			// Token: 0x02000235 RID: 565
			protected struct HttpClientOperationManager
			{
				// Token: 0x17000753 RID: 1875
				// (get) Token: 0x06001582 RID: 5506 RVA: 0x0004876E File Offset: 0x0004696E
				public HttpRequestMessage Request
				{
					get
					{
						return this.request;
					}
				}

				// Token: 0x17000754 RID: 1876
				// (get) Token: 0x06001583 RID: 5507 RVA: 0x00048776 File Offset: 0x00046976
				public HttpResponseMessage Response
				{
					get
					{
						if (this.response == null || !this.response.IsCompleted)
						{
							return null;
						}
						return this.response.Result;
					}
				}

				// Token: 0x06001584 RID: 5508 RVA: 0x0004879C File Offset: 0x0004699C
				public Stream StartRequest(HttpStream.HttpXmlaOperation operation, bool hasXmlaPayload)
				{
					this.cts = new CancellationTokenSource();
					Stream stream;
					if (hasXmlaPayload)
					{
						this.request = operation.Controller.StartHttpClientBasedOperation(operation.Owner, operation.Context, this.cts, out stream, out this.response);
					}
					else
					{
						this.request = operation.Controller.StartHttpClientBasedOperationWithoutPayload(operation.Owner, operation.Context, this.cts, out this.response);
						stream = null;
					}
					return stream;
				}

				// Token: 0x06001585 RID: 5509 RVA: 0x00048810 File Offset: 0x00046A10
				public Stream GetResponse(HttpStream.HttpXmlaOperation operation)
				{
					Stream stream;
					try
					{
						stream = operation.Controller.CompleteHttpClientBasedOperation(operation.Owner, operation.Context, this.response);
					}
					finally
					{
						this.request.Dispose();
						this.request = null;
						this.cts.Dispose();
						this.cts = null;
					}
					return stream;
				}

				// Token: 0x06001586 RID: 5510 RVA: 0x00048874 File Offset: 0x00046A74
				public void Reset(bool closeRequest, ref bool closeResponse)
				{
					if (closeRequest && this.request != null)
					{
						if (this.response != null && !this.response.IsTaskInTerminalState())
						{
							this.response.EnsureTaskInTerminalState(this.cts);
							if (this.response.IsCompleted)
							{
								this.response.Result.Dispose();
							}
							this.response = null;
							closeResponse = false;
						}
						this.request.Dispose();
						this.request = null;
						this.cts.Dispose();
						this.cts = null;
					}
					if (closeResponse && this.response != null)
					{
						if (this.response.IsCompleted)
						{
							this.response.Result.Dispose();
						}
						this.response = null;
					}
				}

				// Token: 0x04000F67 RID: 3943
				private CancellationTokenSource cts;

				// Token: 0x04000F68 RID: 3944
				private HttpRequestMessage request;

				// Token: 0x04000F69 RID: 3945
				private Task<HttpResponseMessage> response;
			}
		}
	}
}
