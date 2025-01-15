using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x0200005C RID: 92
	[Target("WebService")]
	public sealed class WebServiceTarget : MethodCallTargetBase
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00014C74 File Offset: 0x00012E74
		public WebServiceTarget()
		{
			this.Protocol = WebServiceProtocol.Soap11;
			this.Encoding = new UTF8Encoding(false);
			this.IncludeBOM = new bool?(false);
			base.OptimizeBufferReuse = true;
			this.Headers = new List<MethodCallParameter>();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00014CC3 File Offset: 0x00012EC3
		public WebServiceTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00014CD2 File Offset: 0x00012ED2
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00014CDA File Offset: 0x00012EDA
		public Uri Url { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00014CE3 File Offset: 0x00012EE3
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00014CEB File Offset: 0x00012EEB
		public string MethodName { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00014CF4 File Offset: 0x00012EF4
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00014CFC File Offset: 0x00012EFC
		public string Namespace { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00014D05 File Offset: 0x00012F05
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00014D12 File Offset: 0x00012F12
		[DefaultValue("Soap11")]
		public WebServiceProtocol Protocol
		{
			get
			{
				return this._activeProtocol.Key;
			}
			set
			{
				this._activeProtocol = new KeyValuePair<WebServiceProtocol, WebServiceTarget.HttpPostFormatterBase>(value, null);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00014D21 File Offset: 0x00012F21
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00014D2E File Offset: 0x00012F2E
		[DefaultValue("DefaultWebProxy")]
		public WebServiceProxyType ProxyType
		{
			get
			{
				return this._activeProxy.Key;
			}
			set
			{
				this._activeProxy = new KeyValuePair<WebServiceProxyType, IWebProxy>(value, null);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00014D3D File Offset: 0x00012F3D
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00014D45 File Offset: 0x00012F45
		public string ProxyAddress { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00014D4E File Offset: 0x00012F4E
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00014D56 File Offset: 0x00012F56
		public bool? IncludeBOM { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00014D5F File Offset: 0x00012F5F
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00014D67 File Offset: 0x00012F67
		public Encoding Encoding { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00014D70 File Offset: 0x00012F70
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00014D78 File Offset: 0x00012F78
		public bool EscapeDataRfc3986 { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00014D81 File Offset: 0x00012F81
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00014D89 File Offset: 0x00012F89
		public bool EscapeDataNLogLegacy { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00014D92 File Offset: 0x00012F92
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00014D9A File Offset: 0x00012F9A
		public string XmlRoot { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00014DA3 File Offset: 0x00012FA3
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00014DAB File Offset: 0x00012FAB
		public string XmlRootNamespace { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00014DB4 File Offset: 0x00012FB4
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00014DBC File Offset: 0x00012FBC
		[ArrayParameter(typeof(MethodCallParameter), "header")]
		public IList<MethodCallParameter> Headers { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00014DC5 File Offset: 0x00012FC5
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00014DCD File Offset: 0x00012FCD
		public bool PreAuthenticate { get; set; }

		// Token: 0x0600083F RID: 2111 RVA: 0x00014DD6 File Offset: 0x00012FD6
		protected override void DoInvoke(object[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00014DE0 File Offset: 0x00012FE0
		protected override void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.BuildWebServiceUrl(parameters));
			this.DoInvoke(parameters, httpWebRequest, continuation);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00014E08 File Offset: 0x00013008
		protected override void DoInvoke(object[] parameters, AsyncLogEventInfo logEvent)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.BuildWebServiceUrl(parameters));
			if (this.Headers != null && this.Headers.Count > 0)
			{
				for (int i = 0; i < this.Headers.Count; i++)
				{
					string text = base.RenderLogEvent(this.Headers[i].Layout, logEvent.LogEvent);
					if (text != null)
					{
						httpWebRequest.Headers[this.Headers[i].Name] = text;
					}
				}
			}
			this.DoInvoke(parameters, httpWebRequest, logEvent.Continuation);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00014EA4 File Offset: 0x000130A4
		private void DoInvoke(object[] parameters, HttpWebRequest request, AsyncContinuation continuation)
		{
			Func<AsyncCallback, IAsyncResult> func = (AsyncCallback r) => request.BeginGetRequestStream(r, null);
			Func<IAsyncResult, Stream> func2 = new Func<IAsyncResult, Stream>(request.EndGetRequestStream);
			switch (this.ProxyType)
			{
			case WebServiceProxyType.DefaultWebProxy:
				goto IL_00F9;
			case WebServiceProxyType.AutoProxy:
				if (this._activeProxy.Value == null)
				{
					IWebProxy systemWebProxy = WebRequest.GetSystemWebProxy();
					systemWebProxy.Credentials = CredentialCache.DefaultCredentials;
					this._activeProxy = new KeyValuePair<WebServiceProxyType, IWebProxy>(this.ProxyType, systemWebProxy);
				}
				request.Proxy = this._activeProxy.Value;
				goto IL_00F9;
			case WebServiceProxyType.ProxyAddress:
				if (!string.IsNullOrEmpty(this.ProxyAddress))
				{
					if (this._activeProxy.Value == null)
					{
						IWebProxy webProxy = new WebProxy(this.ProxyAddress, true);
						this._activeProxy = new KeyValuePair<WebServiceProxyType, IWebProxy>(this.ProxyType, webProxy);
					}
					request.Proxy = this._activeProxy.Value;
					goto IL_00F9;
				}
				goto IL_00F9;
			}
			request.Proxy = null;
			IL_00F9:
			if (this.PreAuthenticate || this.ProxyType == WebServiceProxyType.AutoProxy)
			{
				request.PreAuthenticate = true;
			}
			this.DoInvoke(parameters, continuation, request, func, func2);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00014FD8 File Offset: 0x000131D8
		internal void DoInvoke(object[] parameters, AsyncContinuation continuation, HttpWebRequest request, Func<AsyncCallback, IAsyncResult> beginFunc, Func<IAsyncResult, Stream> getStreamFunc)
		{
			Stream stream = null;
			if (this.Protocol == WebServiceProtocol.HttpGet)
			{
				this.PrepareGetRequest(request);
			}
			else
			{
				if (this._activeProtocol.Value == null)
				{
					this._activeProtocol = new KeyValuePair<WebServiceProtocol, WebServiceTarget.HttpPostFormatterBase>(this.Protocol, WebServiceTarget._postFormatterFactories[this.Protocol](this));
				}
				stream = this._activeProtocol.Value.PrepareRequest(request, parameters);
			}
			AsyncContinuation asyncContinuation = this.CreateSendContinuation(continuation, request);
			this.PostPayload(continuation, beginFunc, getStreamFunc, stream, asyncContinuation);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00015056 File Offset: 0x00013256
		private AsyncContinuation CreateSendContinuation(AsyncContinuation continuation, HttpWebRequest request)
		{
			AsyncCallback <>9__1;
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					this.DoInvokeCompleted(continuation, ex);
					return;
				}
				try
				{
					WebRequest request2 = request;
					AsyncCallback asyncCallback;
					if ((asyncCallback = <>9__1) == null)
					{
						asyncCallback = (<>9__1 = delegate(IAsyncResult r)
						{
							try
							{
								using (request.EndGetResponse(r))
								{
								}
								this.DoInvokeCompleted(continuation, null);
							}
							catch (Exception ex3)
							{
								InternalLogger.Error(ex3, "WebServiceTarget(Name={0}): Error sending request", new object[] { this.Name });
								if (ex3.MustBeRethrownImmediately())
								{
									throw;
								}
								this.DoInvokeCompleted(continuation, ex3);
							}
						});
					}
					request2.BeginGetResponse(asyncCallback, null);
				}
				catch (Exception ex2)
				{
					InternalLogger.Error(ex2, "WebServiceTarget(Name={0}): Error starting request", new object[] { this.Name });
					if (ex2.MustBeRethrown())
					{
						throw;
					}
					this.DoInvokeCompleted(continuation, ex2);
				}
			};
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00015080 File Offset: 0x00013280
		private void PostPayload(AsyncContinuation continuation, Func<AsyncCallback, IAsyncResult> beginFunc, Func<IAsyncResult, Stream> getStreamFunc, Stream postPayload, AsyncContinuation sendContinuation)
		{
			if (postPayload != null && postPayload.Length > 0L)
			{
				postPayload.Position = 0L;
				try
				{
					this._pendingManualFlushList.BeginOperation();
					beginFunc(delegate(IAsyncResult result)
					{
						try
						{
							using (Stream stream = getStreamFunc(result))
							{
								WebServiceTarget.WriteStreamAndFixPreamble(postPayload, stream, this.IncludeBOM, this.Encoding);
								postPayload.Dispose();
							}
							sendContinuation(null);
						}
						catch (Exception ex2)
						{
							InternalLogger.Error(ex2, "WebServiceTarget(Name={0}): Error sending post data", new object[] { this.Name });
							if (ex2.MustBeRethrownImmediately())
							{
								throw;
							}
							postPayload.Dispose();
							this.DoInvokeCompleted(continuation, ex2);
						}
					});
					return;
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "WebServiceTarget(Name={0}): Error starting post data", new object[] { base.Name });
					if (ex.MustBeRethrown())
					{
						throw;
					}
					this.DoInvokeCompleted(continuation, ex);
					return;
				}
			}
			this._pendingManualFlushList.BeginOperation();
			sendContinuation(null);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00015158 File Offset: 0x00013358
		private void DoInvokeCompleted(AsyncContinuation continuation, Exception ex)
		{
			this._pendingManualFlushList.CompleteOperation(ex);
			continuation(ex);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001516D File Offset: 0x0001336D
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this._pendingManualFlushList.RegisterCompletionNotification(asyncContinuation)(null);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00015181 File Offset: 0x00013381
		protected override void CloseTarget()
		{
			this._pendingManualFlushList.Clear();
			base.CloseTarget();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00015194 File Offset: 0x00013394
		private Uri BuildWebServiceUrl(object[] parameterValues)
		{
			if (this.Protocol != WebServiceProtocol.HttpGet)
			{
				return this.Url;
			}
			string text;
			using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = (base.OptimizeBufferReuse ? this.ReusableLayoutBuilder.Allocate() : this.ReusableLayoutBuilder.None))
			{
				StringBuilder stringBuilder = lockOject.Result ?? new StringBuilder();
				UrlHelper.EscapeEncodingOptions uriStringEncodingFlags = UrlHelper.GetUriStringEncodingFlags(this.EscapeDataNLogLegacy, false, this.EscapeDataRfc3986);
				this.BuildWebServiceQueryParameters(parameterValues, stringBuilder, uriStringEncodingFlags);
				text = stringBuilder.ToString();
			}
			UriBuilder uriBuilder = new UriBuilder(this.Url);
			if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
			{
				uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + text;
			}
			else
			{
				uriBuilder.Query = text;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00015278 File Offset: 0x00013478
		private void BuildWebServiceQueryParameters(object[] parameterValues, StringBuilder sb, UrlHelper.EscapeEncodingOptions encodingOptions)
		{
			string text = string.Empty;
			for (int i = 0; i < base.Parameters.Count; i++)
			{
				sb.Append(text);
				sb.Append(base.Parameters[i].Name);
				sb.Append("=");
				string text2 = XmlHelper.XmlConvertToString(parameterValues[i]);
				if (!string.IsNullOrEmpty(text2))
				{
					UrlHelper.EscapeDataEncode(text2, sb, encodingOptions);
				}
				text = "&";
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000152EC File Offset: 0x000134EC
		private void PrepareGetRequest(HttpWebRequest request)
		{
			request.Method = "GET";
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000152FC File Offset: 0x000134FC
		private static void WriteStreamAndFixPreamble(Stream input, Stream output, bool? writeUtf8BOM, Encoding encoding)
		{
			bool flag = writeUtf8BOM == null || !(encoding is UTF8Encoding);
			if (!flag)
			{
				bool flag2 = encoding.GetPreamble().Length == 3;
				flag = (writeUtf8BOM.Value && flag2) || (!writeUtf8BOM.Value && !flag2);
			}
			int num = (flag ? 0 : 3);
			input.CopyWithOffset(output, num);
		}

		// Token: 0x040001A7 RID: 423
		private const string SoapEnvelopeNamespaceUri = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040001A8 RID: 424
		private const string Soap12EnvelopeNamespaceUri = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040001A9 RID: 425
		private static Dictionary<WebServiceProtocol, Func<WebServiceTarget, WebServiceTarget.HttpPostFormatterBase>> _postFormatterFactories = new Dictionary<WebServiceProtocol, Func<WebServiceTarget, WebServiceTarget.HttpPostFormatterBase>>
		{
			{
				WebServiceProtocol.Soap11,
				(WebServiceTarget t) => new WebServiceTarget.HttpPostSoap11Formatter(t)
			},
			{
				WebServiceProtocol.Soap12,
				(WebServiceTarget t) => new WebServiceTarget.HttpPostSoap12Formatter(t)
			},
			{
				WebServiceProtocol.HttpPost,
				(WebServiceTarget t) => new WebServiceTarget.HttpPostFormEncodedFormatter(t)
			},
			{
				WebServiceProtocol.JsonPost,
				(WebServiceTarget t) => new WebServiceTarget.HttpPostJsonFormatter(t)
			},
			{
				WebServiceProtocol.XmlPost,
				(WebServiceTarget t) => new WebServiceTarget.HttpPostXmlDocumentFormatter(t)
			}
		};

		// Token: 0x040001AD RID: 429
		private KeyValuePair<WebServiceProtocol, WebServiceTarget.HttpPostFormatterBase> _activeProtocol;

		// Token: 0x040001AE RID: 430
		private KeyValuePair<WebServiceProxyType, IWebProxy> _activeProxy;

		// Token: 0x040001B8 RID: 440
		private readonly AsyncOperationCounter _pendingManualFlushList = new AsyncOperationCounter();

		// Token: 0x02000233 RID: 563
		private abstract class HttpPostFormatterBase
		{
			// Token: 0x0600154E RID: 5454 RVA: 0x00038624 File Offset: 0x00036824
			protected HttpPostFormatterBase(WebServiceTarget target)
			{
				this.Target = target;
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x0600154F RID: 5455 RVA: 0x00038634 File Offset: 0x00036834
			protected string ContentType
			{
				get
				{
					string text;
					if ((text = this._contentType) == null)
					{
						text = (this._contentType = this.GetContentType(this.Target));
					}
					return text;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06001550 RID: 5456 RVA: 0x00038660 File Offset: 0x00036860
			// (set) Token: 0x06001551 RID: 5457 RVA: 0x00038668 File Offset: 0x00036868
			private protected WebServiceTarget Target { protected get; private set; }

			// Token: 0x06001552 RID: 5458 RVA: 0x00038671 File Offset: 0x00036871
			protected virtual string GetContentType(WebServiceTarget target)
			{
				return "charset=" + target.Encoding.WebName;
			}

			// Token: 0x06001553 RID: 5459 RVA: 0x00038688 File Offset: 0x00036888
			public MemoryStream PrepareRequest(HttpWebRequest request, object[] parameterValues)
			{
				this.InitRequest(request);
				MemoryStream memoryStream = new MemoryStream();
				this.WriteContent(memoryStream, parameterValues);
				return memoryStream;
			}

			// Token: 0x06001554 RID: 5460 RVA: 0x000386AB File Offset: 0x000368AB
			protected virtual void InitRequest(HttpWebRequest request)
			{
				request.Method = "POST";
				request.ContentType = this.ContentType;
			}

			// Token: 0x06001555 RID: 5461
			protected abstract void WriteContent(MemoryStream ms, object[] parameterValues);

			// Token: 0x04000615 RID: 1557
			private string _contentType;
		}

		// Token: 0x02000234 RID: 564
		private class HttpPostFormEncodedFormatter : WebServiceTarget.HttpPostTextFormatterBase
		{
			// Token: 0x06001556 RID: 5462 RVA: 0x000386C4 File Offset: 0x000368C4
			public HttpPostFormEncodedFormatter(WebServiceTarget target)
				: base(target)
			{
				this._encodingOptions = UrlHelper.GetUriStringEncodingFlags(target.EscapeDataNLogLegacy, true, target.EscapeDataRfc3986);
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x000386E5 File Offset: 0x000368E5
			protected override string GetContentType(WebServiceTarget target)
			{
				return "application/x-www-form-urlencoded" + "; " + base.GetContentType(target);
			}

			// Token: 0x06001558 RID: 5464 RVA: 0x000386FD File Offset: 0x000368FD
			protected override void WriteStringContent(StringBuilder builder, object[] parameterValues)
			{
				base.Target.BuildWebServiceQueryParameters(parameterValues, builder, this._encodingOptions);
			}

			// Token: 0x04000617 RID: 1559
			private readonly UrlHelper.EscapeEncodingOptions _encodingOptions;
		}

		// Token: 0x02000235 RID: 565
		private class HttpPostJsonFormatter : WebServiceTarget.HttpPostTextFormatterBase
		{
			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x00038714 File Offset: 0x00036914
			private IJsonConverter JsonConverter
			{
				get
				{
					IJsonConverter jsonConverter;
					if ((jsonConverter = this._jsonConverter) == null)
					{
						jsonConverter = (this._jsonConverter = ConfigurationItemFactory.Default.JsonConverter);
					}
					return jsonConverter;
				}
			}

			// Token: 0x0600155A RID: 5466 RVA: 0x0003873E File Offset: 0x0003693E
			public HttpPostJsonFormatter(WebServiceTarget target)
				: base(target)
			{
			}

			// Token: 0x0600155B RID: 5467 RVA: 0x00038747 File Offset: 0x00036947
			protected override string GetContentType(WebServiceTarget target)
			{
				return "application/json" + "; " + base.GetContentType(target);
			}

			// Token: 0x0600155C RID: 5468 RVA: 0x00038760 File Offset: 0x00036960
			protected override void WriteStringContent(StringBuilder builder, object[] parameterValues)
			{
				string text;
				if (base.Target.Parameters.Count == 1 && string.IsNullOrEmpty(base.Target.Parameters[0].Name) && (text = parameterValues[0] as string) != null)
				{
					builder.Append(text);
					return;
				}
				builder.Append("{");
				string text2 = string.Empty;
				for (int i = 0; i < base.Target.Parameters.Count; i++)
				{
					MethodCallParameter methodCallParameter = base.Target.Parameters[i];
					builder.Append(text2);
					builder.Append('"');
					builder.Append(methodCallParameter.Name);
					builder.Append("\":");
					this.JsonConverter.SerializeObject(parameterValues[i], builder);
					text2 = ",";
				}
				builder.Append('}');
			}

			// Token: 0x04000618 RID: 1560
			private IJsonConverter _jsonConverter;
		}

		// Token: 0x02000236 RID: 566
		private class HttpPostSoap11Formatter : WebServiceTarget.HttpPostSoapFormatterBase
		{
			// Token: 0x0600155D RID: 5469 RVA: 0x0003883A File Offset: 0x00036A3A
			public HttpPostSoap11Formatter(WebServiceTarget target)
				: base(target)
			{
				this._defaultSoapAction = WebServiceTarget.HttpPostSoapFormatterBase.GetDefaultSoapAction(target);
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x0600155E RID: 5470 RVA: 0x0003884F File Offset: 0x00036A4F
			protected override string SoapEnvelopeNamespace
			{
				get
				{
					return "http://schemas.xmlsoap.org/soap/envelope/";
				}
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x0600155F RID: 5471 RVA: 0x00038856 File Offset: 0x00036A56
			protected override string SoapName
			{
				get
				{
					return "soap";
				}
			}

			// Token: 0x06001560 RID: 5472 RVA: 0x0003885D File Offset: 0x00036A5D
			protected override string GetContentType(WebServiceTarget target)
			{
				return "text/xml" + "; " + base.GetContentType(target);
			}

			// Token: 0x06001561 RID: 5473 RVA: 0x00038878 File Offset: 0x00036A78
			protected override void InitRequest(HttpWebRequest request)
			{
				base.InitRequest(request);
				IList<MethodCallParameter> headers = base.Target.Headers;
				if ((headers != null && headers.Count == 0) || string.IsNullOrEmpty(request.Headers["SOAPAction"]))
				{
					request.Headers["SOAPAction"] = this._defaultSoapAction;
				}
			}

			// Token: 0x04000619 RID: 1561
			private readonly string _defaultSoapAction;
		}

		// Token: 0x02000237 RID: 567
		private class HttpPostSoap12Formatter : WebServiceTarget.HttpPostSoapFormatterBase
		{
			// Token: 0x06001562 RID: 5474 RVA: 0x000388D5 File Offset: 0x00036AD5
			public HttpPostSoap12Formatter(WebServiceTarget target)
				: base(target)
			{
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x06001563 RID: 5475 RVA: 0x000388DE File Offset: 0x00036ADE
			protected override string SoapEnvelopeNamespace
			{
				get
				{
					return "http://www.w3.org/2003/05/soap-envelope";
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06001564 RID: 5476 RVA: 0x000388E5 File Offset: 0x00036AE5
			protected override string SoapName
			{
				get
				{
					return "soap12";
				}
			}

			// Token: 0x06001565 RID: 5477 RVA: 0x000388EC File Offset: 0x00036AEC
			protected override string GetContentType(WebServiceTarget target)
			{
				return this.GetContentTypeSoap12(target, WebServiceTarget.HttpPostSoapFormatterBase.GetDefaultSoapAction(target));
			}

			// Token: 0x06001566 RID: 5478 RVA: 0x000388FC File Offset: 0x00036AFC
			protected override void InitRequest(HttpWebRequest request)
			{
				base.InitRequest(request);
				IList<MethodCallParameter> headers = base.Target.Headers;
				string text = ((headers != null && headers.Count > 0) ? request.Headers["SOAPAction"] : string.Empty);
				if (!string.IsNullOrEmpty(text))
				{
					request.ContentType = this.GetContentTypeSoap12(base.Target, text);
				}
			}

			// Token: 0x06001567 RID: 5479 RVA: 0x0003895F File Offset: 0x00036B5F
			private string GetContentTypeSoap12(WebServiceTarget target, string soapAction)
			{
				return string.Concat(new string[]
				{
					"application/soap+xml",
					"; ",
					base.GetContentType(target),
					"; action=\"",
					soapAction,
					"\""
				});
			}
		}

		// Token: 0x02000238 RID: 568
		private abstract class HttpPostSoapFormatterBase : WebServiceTarget.HttpPostXmlFormatterBase
		{
			// Token: 0x06001568 RID: 5480 RVA: 0x0003899A File Offset: 0x00036B9A
			protected HttpPostSoapFormatterBase(WebServiceTarget target)
				: base(target)
			{
				this._xmlWriterSettings = new XmlWriterSettings
				{
					Encoding = target.Encoding
				};
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x06001569 RID: 5481
			protected abstract string SoapEnvelopeNamespace { get; }

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x0600156A RID: 5482
			protected abstract string SoapName { get; }

			// Token: 0x0600156B RID: 5483 RVA: 0x000389BC File Offset: 0x00036BBC
			protected override void WriteContent(MemoryStream ms, object[] parameterValues)
			{
				XmlWriter xmlWriter = XmlWriter.Create(ms, this._xmlWriterSettings);
				xmlWriter.WriteStartElement(this.SoapName, "Envelope", this.SoapEnvelopeNamespace);
				xmlWriter.WriteStartElement("Body", this.SoapEnvelopeNamespace);
				xmlWriter.WriteStartElement(base.Target.MethodName, base.Target.Namespace);
				base.WriteAllParametersToCurrenElement(xmlWriter, parameterValues);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
			}

			// Token: 0x0600156C RID: 5484 RVA: 0x00038A3A File Offset: 0x00036C3A
			protected static string GetDefaultSoapAction(WebServiceTarget target)
			{
				if (!target.Namespace.EndsWith("/", StringComparison.Ordinal))
				{
					return target.Namespace + "/" + target.MethodName;
				}
				return target.Namespace + target.MethodName;
			}

			// Token: 0x0400061A RID: 1562
			private readonly XmlWriterSettings _xmlWriterSettings;
		}

		// Token: 0x02000239 RID: 569
		private abstract class HttpPostTextFormatterBase : WebServiceTarget.HttpPostFormatterBase
		{
			// Token: 0x0600156D RID: 5485 RVA: 0x00038A77 File Offset: 0x00036C77
			protected HttpPostTextFormatterBase(WebServiceTarget target)
				: base(target)
			{
				this._encodingPreamble = target.Encoding.GetPreamble();
			}

			// Token: 0x0600156E RID: 5486 RVA: 0x00038AAC File Offset: 0x00036CAC
			protected override void WriteContent(MemoryStream ms, object[] parameterValues)
			{
				ReusableBuilderCreator reusableStringBuilder = this._reusableStringBuilder;
				lock (reusableStringBuilder)
				{
					using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this._reusableStringBuilder.Allocate())
					{
						this.WriteStringContent(lockOject.Result, parameterValues);
						using (ReusableObjectCreator<char[]>.LockOject lockOject2 = this._reusableEncodingBuffer.Allocate())
						{
							if (this._encodingPreamble.Length != 0)
							{
								ms.Write(this._encodingPreamble, 0, this._encodingPreamble.Length);
							}
							lockOject.Result.CopyToStream(ms, base.Target.Encoding, lockOject2.Result);
						}
					}
				}
			}

			// Token: 0x0600156F RID: 5487
			protected abstract void WriteStringContent(StringBuilder builder, object[] parameterValues);

			// Token: 0x0400061B RID: 1563
			private readonly ReusableBuilderCreator _reusableStringBuilder = new ReusableBuilderCreator();

			// Token: 0x0400061C RID: 1564
			private readonly ReusableBufferCreator _reusableEncodingBuffer = new ReusableBufferCreator(1024);

			// Token: 0x0400061D RID: 1565
			private readonly byte[] _encodingPreamble;
		}

		// Token: 0x0200023A RID: 570
		private class HttpPostXmlDocumentFormatter : WebServiceTarget.HttpPostXmlFormatterBase
		{
			// Token: 0x06001570 RID: 5488 RVA: 0x00038B7C File Offset: 0x00036D7C
			public HttpPostXmlDocumentFormatter(WebServiceTarget target)
				: base(target)
			{
				if (string.IsNullOrEmpty(target.XmlRoot))
				{
					throw new InvalidOperationException("WebServiceProtocol.Xml requires WebServiceTarget.XmlRoot to be set.");
				}
				this._xmlWriterSettings = new XmlWriterSettings
				{
					Encoding = target.Encoding,
					OmitXmlDeclaration = true,
					Indent = false
				};
			}

			// Token: 0x06001571 RID: 5489 RVA: 0x00038BCD File Offset: 0x00036DCD
			protected override string GetContentType(WebServiceTarget target)
			{
				return "application/xml" + "; " + base.GetContentType(target);
			}

			// Token: 0x06001572 RID: 5490 RVA: 0x00038BE8 File Offset: 0x00036DE8
			protected override void WriteContent(MemoryStream ms, object[] parameterValues)
			{
				XmlWriter xmlWriter = XmlWriter.Create(ms, this._xmlWriterSettings);
				xmlWriter.WriteStartElement(base.Target.XmlRoot, base.Target.XmlRootNamespace);
				base.WriteAllParametersToCurrenElement(xmlWriter, parameterValues);
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
			}

			// Token: 0x0400061E RID: 1566
			private readonly XmlWriterSettings _xmlWriterSettings;
		}

		// Token: 0x0200023B RID: 571
		private abstract class HttpPostXmlFormatterBase : WebServiceTarget.HttpPostFormatterBase
		{
			// Token: 0x06001573 RID: 5491 RVA: 0x00038C32 File Offset: 0x00036E32
			protected HttpPostXmlFormatterBase(WebServiceTarget target)
				: base(target)
			{
			}

			// Token: 0x06001574 RID: 5492 RVA: 0x00038C3C File Offset: 0x00036E3C
			protected void WriteAllParametersToCurrenElement(XmlWriter currentXmlWriter, object[] parameterValues)
			{
				for (int i = 0; i < base.Target.Parameters.Count; i++)
				{
					string text = XmlHelper.XmlConvertToStringSafe(parameterValues[i]);
					currentXmlWriter.WriteElementString(base.Target.Parameters[i].Name, text);
				}
			}
		}
	}
}
