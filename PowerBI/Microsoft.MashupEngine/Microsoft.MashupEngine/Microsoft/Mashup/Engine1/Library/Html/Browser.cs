using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AAF RID: 2735
	internal sealed class Browser
	{
		// Token: 0x06004CA7 RID: 19623 RVA: 0x000FC854 File Offset: 0x000FAA54
		private Browser(IEngineHost host, string text, Request request)
		{
			this.host = host;
			this.documentText = text;
			this.request = request;
			if (request != null && request.Uri != null && request.Uri.AbsoluteUri.Contains("%00"))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.NullCharNotallowedInUrl, TextValue.New(this.request.Uri.AbsoluteUri), null);
			}
			Thread thread = new Thread(CloneCurrentCultures.CreateWrapper(new ThreadStart(this.ThreadCreate)));
			thread.IsBackground = true;
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			if (this.exception == null)
			{
				return;
			}
			if (this.exception is RuntimeException || !SafeExceptions.IsSafeException(this.exception))
			{
				throw this.exception;
			}
			throw new Exception(this.exception.Message, this.exception);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x000FC934 File Offset: 0x000FAB34
		private void ThreadCreate()
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Browser/ThreadCreate", TraceEventType.Information, null))
			{
				hostTrace.Add("RequestUrl", (this.request != null) ? this.request.Uri : null, true);
				hostTrace.Add("Text", this.documentText, true);
				try
				{
					using (NoUIWebBrowser noUIWebBrowser = new NoUIWebBrowser())
					{
						HtmlDocument htmlDocument = this.GetDocument(noUIWebBrowser);
						if (htmlDocument != null)
						{
							HtmlDocumentReader htmlDocumentReader = new HtmlDocumentReader();
							this.document = htmlDocumentReader.Read(htmlDocument);
						}
					}
				}
				catch (Exception ex)
				{
					this.exception = ex;
					SafeExceptions.TraceIsSafeException(hostTrace, ex);
				}
			}
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x000FCA08 File Offset: 0x000FAC08
		public static Stream GetDomFromText(IEngineHost host, string text)
		{
			Stream stream;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/Browser/GetDomFromText", TraceEventType.Information, null))
			{
				try
				{
					stream = new Browser(host, text, null).document;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return stream;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000FCA68 File Offset: 0x000FAC68
		public static Stream GetDomFromRequest(IEngineHost host, Request request)
		{
			Stream stream;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/Browser/GetDomFromUri", TraceEventType.Information, null))
			{
				try
				{
					hostTrace.Add("RequestUri", request.Uri, true);
					stream = new Browser(host, null, request).document;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return stream;
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x000FCAD8 File Offset: 0x000FACD8
		private HtmlDocument GetDocument(NoUIWebBrowser browser)
		{
			HtmlDocument htmlDocument = this.LoadDocument(browser);
			if (this.documentText != null && htmlDocument != null && this.documentText.Length > 0)
			{
				htmlDocument = htmlDocument.OpenNew(true);
				htmlDocument.Write(this.documentText);
			}
			return htmlDocument;
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x000FCB24 File Offset: 0x000FAD24
		private HtmlDocument LoadDocument(NoUIWebBrowser browser)
		{
			browser.ScriptErrorsSuppressed = true;
			browser.CreateControl();
			ApplicationContext context = new ApplicationContext();
			browser.DocumentCompleted += delegate(object o, WebBrowserDocumentCompletedEventArgs e)
			{
				context.ExitThread();
			};
			browser.BeforeNavigate += this.OnBeforeNavigate;
			browser.NavigateError += this.OnNavigateError;
			if (this.request == null)
			{
				browser.Navigate(Browser.AboutBlankUrl);
			}
			else
			{
				ResourceCredentialCollection resourceCredentialCollection;
				this.request.VerifyPermissionAndGetCredentials(out resourceCredentialCollection);
				this.CheckSupported(resourceCredentialCollection);
				byte[] array = (this.request.Content.IsNull ? null : this.request.Content.AsBinary.AsBytes);
				browser.Navigate(this.request.ApplyCredentialsToUri(this.request.Uri, resourceCredentialCollection), null, array, this.request.GetHeaderString());
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TimeSpan timeSpan = TimeSpan.MaxValue;
			if (this.request != null && !this.request.Timeout.IsNull)
			{
				timeSpan = this.request.Timeout.AsDuration.AsClrTimeSpan;
			}
			while (browser.ReadyState != WebBrowserReadyState.Complete && this.exception == null)
			{
				if (stopwatch.Elapsed > timeSpan)
				{
					if (browser.ReadyState < WebBrowserReadyState.Interactive)
					{
						this.exception = ValueException.NewDataSourceError<Message1>(Strings.WebPageTimedOut(timeSpan.TotalSeconds), Value.Null, null);
						break;
					}
					break;
				}
				else
				{
					Application.DoEvents();
				}
			}
			if (this.exception == null)
			{
				this.exception = this.CheckPermission(browser);
			}
			if (this.exception != null)
			{
				return null;
			}
			return browser.Document;
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x000FCCC0 File Offset: 0x000FAEC0
		private void CheckSupported(ResourceCredentialCollection credentials)
		{
			IResourceCredential resourceCredential = ((credentials.Count > 0) ? credentials[0] : null);
			WindowsCredential windowsCredential = resourceCredential as WindowsCredential;
			if (resourceCredential is UsernamePasswordCredential || resourceCredential is OAuthCredential || (windowsCredential != null && windowsCredential.OverrideCurrentUser))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, credentials.Resource, Strings.WebPageUnsupportedAuth, null, null);
			}
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x000FCD24 File Offset: 0x000FAF24
		private Exception CheckPermission(WebBrowser browser)
		{
			Exception ex2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Browser/CheckPermission", TraceEventType.Information, null))
			{
				try
				{
					string text = browser.Url.AbsoluteUri;
					if (this.request != null && browser.Url.AbsoluteUri.StartsWith(this.request.Uri.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
					{
						text = this.request.Uri.AbsoluteUri;
					}
					hostTrace.Add("Url", browser.Url, true);
					if (text != Browser.AboutBlankUrl.AbsoluteUri && !HostResourcePermissionService.InsecureRedirects(this.host))
					{
						IResource resource = Resource.New("Web", text);
						ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, this.request.RequestResource, resource, null);
						this.CheckSupported(resourceCredentialCollection);
					}
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					return ex;
				}
				ex2 = null;
			}
			return ex2;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x000FCE2C File Offset: 0x000FB02C
		private void OnBeforeNavigate(WebBrowser browser, object pDisp, string url, string targetFrameName, ref string headers, ref bool cancel)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Browser/BeforeNavigate", TraceEventType.Information, null))
			{
				try
				{
					if (this.request != null && this.request.Uri != null && url.StartsWith(this.request.Uri.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
					{
						url = this.request.Uri.AbsoluteUri;
					}
					if (url != Browser.AboutBlankUrl.AbsoluteUri && !url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) && !url.StartsWith(Browser.IEFrameDllUrlPrefix.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
					{
						Resource.New("Web", url);
					}
				}
				catch (Exception ex)
				{
					this.exception = ex;
				}
				if (this.exception != null)
				{
					SafeExceptions.TraceIsSafeException(hostTrace, this.exception);
					cancel = true;
					browser.Stop();
				}
			}
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000FCF24 File Offset: 0x000FB124
		private void OnNavigateError(WebBrowser browser, object pDisp, string frameName, string url, int statusCode, ref bool cancel)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Browser/NavigationError", TraceEventType.Information, null))
			{
				try
				{
					if (url.StartsWith(this.request.Uri.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
					{
						url = this.request.Uri.AbsoluteUri;
					}
					string text = ((browser.Document == null) ? url : browser.Document.Url.AbsoluteUri.Split(new char[] { '?' })[0]);
					hostTrace.Add("RequestUri", url, true);
					hostTrace.Add("UrlWithError", text, true);
					hostTrace.Add("FrameName", frameName, true);
					hostTrace.Add("StatusCode", statusCode, false);
					if (this.exception == null && browser.ActiveXInstance == pDisp)
					{
						IResource resource = Resource.New("Web", url);
						ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
						this.CheckSupported(resourceCredentialCollection);
						ResourceSecurityException ex;
						string text2;
						if (HttpServices.TryGetResourceSecurityException(this.host, statusCode, resource, out ex))
						{
							this.exception = ex;
						}
						else if (Enum.IsDefined(typeof(HttpStatusCode), statusCode))
						{
							if (statusCode - 200 <= 2)
							{
								this.exception = ValueException.NewDataFormatError<Message1>(Strings.WebPageParseError(text), TextValue.New(text), null);
							}
							else
							{
								this.exception = DataSourceException.NewDataSourceError<Message2>(this.host, Strings.WebPageHttpError(statusCode, text), resource, "Url", TextValue.New(text), TypeValue.Text, null);
							}
						}
						else if (Browser.TryGetInetErrorMessage(statusCode, out text2))
						{
							this.exception = ValueException.NewDataFormatError(text2, TextValue.NewOrNull(text), null);
						}
						else
						{
							this.exception = DataSourceException.NewDataSourceError<Message0>(this.host, Strings.WebPageUnknownError, resource, "StatusCode", NumberValue.New(statusCode), TypeValue.Number, null);
						}
					}
				}
				catch (Exception ex2)
				{
					this.exception = ex2;
				}
				if (this.exception != null)
				{
					SafeExceptions.TraceIsSafeException(hostTrace, this.exception);
					cancel = true;
					browser.Stop();
				}
			}
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x000FD160 File Offset: 0x000FB360
		private static bool TryGetInetErrorMessage(int errorCode, out string errorMessage)
		{
			errorMessage = null;
			if (errorCode != -2146697211)
			{
				if (errorCode == -2146697191)
				{
					errorMessage = Strings.WebPageInvalidCertificate;
				}
			}
			else
			{
				errorMessage = Strings.WebPageResourceNotFound;
			}
			return errorMessage != null;
		}

		// Token: 0x040028A1 RID: 10401
		private const string JavascriptUriPrefix = "javascript:";

		// Token: 0x040028A2 RID: 10402
		private static readonly Uri AboutBlankUrl = new Uri("about:blank");

		// Token: 0x040028A3 RID: 10403
		private static readonly Uri IEFrameDllUrlPrefix = new Uri("res://ieframe.dll/");

		// Token: 0x040028A4 RID: 10404
		private readonly IEngineHost host;

		// Token: 0x040028A5 RID: 10405
		private readonly string documentText;

		// Token: 0x040028A6 RID: 10406
		private readonly Request request;

		// Token: 0x040028A7 RID: 10407
		private Exception exception;

		// Token: 0x040028A8 RID: 10408
		private Stream document;
	}
}
