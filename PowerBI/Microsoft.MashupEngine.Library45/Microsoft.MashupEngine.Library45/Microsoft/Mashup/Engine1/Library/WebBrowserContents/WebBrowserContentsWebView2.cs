using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.HtmlUtils;
using Microsoft.Mashup.WebBrowserContents;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Microsoft.Mashup.Engine1.Library.WebBrowserContents
{
	// Token: 0x0200204C RID: 8268
	internal class WebBrowserContentsWebView2
	{
		// Token: 0x06011374 RID: 70516 RVA: 0x003B42DC File Offset: 0x003B24DC
		public static bool IsWebView2RuntimeAvailable()
		{
			try
			{
				CoreWebView2Environment.GetAvailableBrowserVersionString(null);
			}
			catch (WebView2RuntimeNotFoundException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06011375 RID: 70517 RVA: 0x003B430C File Offset: 0x003B250C
		public static string GetPageSource(IEngineHost host, string url, WebBrowserContentsOptions options, IResource resource, ResourceCredentialCollection credentials)
		{
			if (!WebBrowserContentsWebView2.IsWebView2RuntimeAvailable())
			{
				throw ValueException.NewDataSourceError<Message0>(Resources.RuntimeMissing, Value.Null, null);
			}
			WebBrowserContentsWebView2.ValidateUrl(url);
			WebBrowserContentsWebView2.WebCredential webCredential;
			WebBrowserContentsWebView2.ValidateCredentials(host, options, resource, credentials, out webCredential);
			string pageSourceInternal;
			using (webCredential.WindowsCredentialOrNull.GetImpersonationWrapper(host, resource)())
			{
				pageSourceInternal = WebBrowserContentsWebView2.GetPageSourceInternal(host, url, options, webCredential);
			}
			return pageSourceInternal;
		}

		// Token: 0x06011376 RID: 70518 RVA: 0x003B4380 File Offset: 0x003B2580
		private static string GetPageSourceInternal(IEngineHost host, string url, WebBrowserContentsOptions options, WebBrowserContentsWebView2.WebCredential webCredential)
		{
			ITempDirectoryConfig tempDirectoryConfig = host.QueryService<ITempDirectoryConfig>();
			string userDataFolder = Path.Combine(tempDirectoryConfig.TempDirectoryPath, "WebView2-" + Path.GetRandomFileName());
			string documentHtml;
			try
			{
				using (WebView2 webView = new WebView2())
				{
					bool domLoaded = false;
					bool navComplete = false;
					Exception exception = null;
					int basicAuthRequests = 0;
					int basicAuthNavigationFailures = 0;
					webView.Visible = false;
					Task<CoreWebView2Environment> envTask = CoreWebView2Environment.CreateAsync(null, userDataFolder, new CoreWebView2EnvironmentOptions(null, null, null, webCredential.Kind == AuthenticationKind.Windows, null));
					WebBrowserContentsWebView2.WaitForCondition(() => envTask.IsCompleted, WebBrowserContentsWebView2.initializationTimeout, Resources.TimedOutWaitingForInitialization, url);
					envTask.Result.BrowserProcessExited += delegate(object browserExitedSender, CoreWebView2BrowserProcessExitedEventArgs browserExitedArgs)
					{
						if (Directory.Exists(userDataFolder))
						{
							try
							{
								Directory.Delete(userDataFolder, true);
							}
							catch (Exception ex4) when (SafeExceptions.IsSafeException(ex4))
							{
							}
						}
					};
					EventHandler<CoreWebView2DOMContentLoadedEventArgs> <>9__7;
					EventHandler<CoreWebView2BasicAuthenticationRequestedEventArgs> <>9__8;
					EventHandler<CoreWebView2ProcessFailedEventArgs> <>9__10;
					webView.CoreWebView2InitializationCompleted += delegate(object sender, CoreWebView2InitializationCompletedEventArgs args)
					{
						if (!args.IsSuccess)
						{
							exception = ValueException.NewDataSourceError<Message0>(Resources.InitializationError, TextValue.New(args.InitializationException.Message), null);
							return;
						}
						webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
						webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
						webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
						webView.CoreWebView2.Settings.AreHostObjectsAllowed = false;
						webView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
						webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
						webView.CoreWebView2.Settings.IsWebMessageEnabled = false;
						webView.CoreWebView2.Settings.IsZoomControlEnabled = false;
						webView.CoreWebView2.Settings.IsScriptEnabled = true;
						CoreWebView2 coreWebView = webView.CoreWebView2;
						EventHandler<CoreWebView2DOMContentLoadedEventArgs> eventHandler;
						if ((eventHandler = <>9__7) == null)
						{
							eventHandler = (<>9__7 = delegate(object domLoadedSender, CoreWebView2DOMContentLoadedEventArgs domLoadedArgs)
							{
								domLoaded = true;
							});
						}
						coreWebView.DOMContentLoaded += eventHandler;
						if (webCredential.Kind == AuthenticationKind.UsernamePassword)
						{
							CoreWebView2 coreWebView2 = webView.CoreWebView2;
							EventHandler<CoreWebView2BasicAuthenticationRequestedEventArgs> eventHandler2;
							if ((eventHandler2 = <>9__8) == null)
							{
								eventHandler2 = (<>9__8 = delegate(object authSender, CoreWebView2BasicAuthenticationRequestedEventArgs authArgs)
								{
									authArgs.Response.UserName = webCredential.BasicAuthCredential.Username;
									authArgs.Response.Password = webCredential.BasicAuthCredential.Password;
									int basicAuthRequests2 = basicAuthRequests;
									basicAuthRequests = basicAuthRequests2 + 1;
								});
							}
							coreWebView2.BasicAuthenticationRequested += eventHandler2;
						}
						webView.CoreWebView2.FrameNavigationStarting += delegate(object frameNavStartingSender, CoreWebView2NavigationStartingEventArgs frameNavStartingArgs)
						{
							try
							{
								WebBrowserContentsWebView2.ValidateUrl(frameNavStartingArgs.Uri);
							}
							catch (ValueException)
							{
								frameNavStartingArgs.Cancel = true;
							}
						};
						CoreWebView2 coreWebView3 = webView.CoreWebView2;
						EventHandler<CoreWebView2ProcessFailedEventArgs> eventHandler3;
						if ((eventHandler3 = <>9__10) == null)
						{
							eventHandler3 = (<>9__10 = delegate(object procSender, CoreWebView2ProcessFailedEventArgs procArgs)
							{
								Exception ex5;
								if ((ex5 = exception) == null)
								{
									ex5 = ValueException.NewDataSourceError<Message0>(Resources.ProcessFailed, RecordValue.New(new NamedValue[]
									{
										new NamedValue("Url", TextValue.New(url)),
										new NamedValue("ExitCode", NumberValue.New(procArgs.ExitCode)),
										new NamedValue("ProcessDescription", TextValue.NewOrNull(procArgs.ProcessDescription)),
										new NamedValue("ProcessFailedKind", TextValue.New(procArgs.ProcessFailedKind.ToString())),
										new NamedValue("Reason", TextValue.New(procArgs.Reason.ToString()))
									}), null);
								}
								exception = ex5;
							});
						}
						coreWebView3.ProcessFailed += eventHandler3;
					};
					Task initTask = webView.EnsureCoreWebView2Async(envTask.Result);
					WebBrowserContentsWebView2.WaitForCondition(() => initTask.IsCompleted || exception != null, WebBrowserContentsWebView2.initializationTimeout, Resources.TimedOutWaitingForInitialization, url);
					if (exception != null)
					{
						throw exception;
					}
					webView.NavigationStarting += delegate(object sender, CoreWebView2NavigationStartingEventArgs args)
					{
						try
						{
							if (args.IsRedirected)
							{
								throw new RedirectException(url, args.Uri);
							}
							WebBrowserContentsWebView2.ValidateUrl(args.Uri);
						}
						catch (Exception ex6) when (SafeExceptions.IsSafeException(ex6))
						{
							args.Cancel = true;
							exception = exception ?? ex6;
						}
					};
					webView.NavigationCompleted += delegate(object sender, CoreWebView2NavigationCompletedEventArgs args)
					{
						if (args.IsSuccess)
						{
							navComplete = domLoaded;
							return;
						}
						if (args.HttpStatusCode == 401 && args.WebErrorStatus == 17 && basicAuthRequests == 1 && basicAuthNavigationFailures == 0)
						{
							int basicAuthNavigationFailures2 = basicAuthNavigationFailures;
							basicAuthNavigationFailures = basicAuthNavigationFailures2 + 1;
							return;
						}
						Exception ex7;
						if ((ex7 = exception) == null)
						{
							ex7 = ValueException.NewDataSourceError<Message0>(Resources.UnableToRetrieveTheContentsOfTheWebPage, RecordValue.New(new NamedValue[]
							{
								new NamedValue("Url", TextValue.New(url)),
								new NamedValue("HttpStatusCode", NumberValue.New(args.HttpStatusCode)),
								new NamedValue("ErrorCode", TextValue.New(args.WebErrorStatus.ToString()))
							}), null);
						}
						exception = ex7;
					};
					UriBuilder uriBuilder = new UriBuilder(url);
					if (webCredential.Kind == AuthenticationKind.WebApi)
					{
						uriBuilder.Query = UriHelper.AddQueryPart(UriHelper.NormalizeUriComponent(uriBuilder.Query), options.ApiKeyName, webCredential.WebApiKeyCredential.ApiKeyValue);
					}
					webView.Source = uriBuilder.Uri;
					WebBrowserContentsWebView2.WaitForCondition(() => navComplete || exception != null, WebBrowserContentsWebView2.pageLoadTimeout, Resources.TimedOutWaitingForPageLoad, url);
					if (exception != null)
					{
						throw exception;
					}
					documentHtml = WebBrowserContentsWebView2.GetDocumentHtml(webView, options, url);
				}
			}
			catch (AggregateException ex)
			{
				Exception ex2 = WebBrowserContentsWebView2.FlattenException(ex);
				if (ex2 is ValueException || !SafeExceptions.IsSafeException(ex2))
				{
					throw ex2;
				}
				throw ValueException.NewDataSourceError<Message0>(Resources.UnableToRetrieveTheContentsOfTheWebPage, RecordValue.New(new NamedValue[]
				{
					new NamedValue("Url", TextValue.New(url)),
					new NamedValue("ErrorMessage", TextValue.New(ex2.Message))
				}), null);
			}
			catch (Exception ex3) when (SafeExceptions.IsSafeException(ex3) && !(ex3 is ValueException) && !(ex3 is RuntimeException) && !(ex3 is RedirectException))
			{
				throw ValueException.NewDataSourceError<Message0>(Resources.UnableToRetrieveTheContentsOfTheWebPage, RecordValue.New(new NamedValue[]
				{
					new NamedValue("Url", TextValue.New(url)),
					new NamedValue("ErrorMessage", TextValue.New(ex3.Message))
				}), null);
			}
			return documentHtml;
		}

		// Token: 0x06011377 RID: 70519 RVA: 0x003B4744 File Offset: 0x003B2944
		private static void ValidateCredentials(IEngineHost host, WebBrowserContentsOptions options, IResource resource, ResourceCredentialCollection credentials, out WebBrowserContentsWebView2.WebCredential webCredential)
		{
			webCredential = new WebBrowserContentsWebView2.WebCredential(host, resource, credentials);
			if (webCredential.Kind != AuthenticationKind.WebApi && options.ApiKeyName != null)
			{
				string text = Strings.HttpCredentialsWebApiKeyRequiresApiKeyName;
				throw DataSourceException.NewInvalidCredentialsError(host, resource, text, text, null);
			}
			if (webCredential.Kind == AuthenticationKind.WebApi && options.ApiKeyName == null)
			{
				string text2 = Strings.HttpCredentialsWebApiKeyOnlyUsedWithApiKeyName;
				throw DataSourceException.NewInvalidCredentialsError(host, resource, text2, text2, null);
			}
		}

		// Token: 0x06011378 RID: 70520 RVA: 0x003B47B0 File Offset: 0x003B29B0
		private static Exception FlattenException(AggregateException e)
		{
			AggregateException ex = e.Flatten();
			if (ex.InnerExceptions.Count == 1)
			{
				return ex.InnerExceptions[0];
			}
			return ex;
		}

		// Token: 0x06011379 RID: 70521 RVA: 0x003B47E0 File Offset: 0x003B29E0
		private static string GetDocumentHtml(WebView2 webView, WebBrowserContentsOptions options, string currentUrl)
		{
			if (options.WaitForSelector != null)
			{
				TimeSpan timeSpan = options.WaitForTimeout ?? WebBrowserContentsWebView2.defaultWaitForTimeoutIfSelectorIsSpecified;
				DateTime dateTime = DateTime.Now.AddMilliseconds(timeSpan.TotalMilliseconds);
				bool flag = false;
				while (!flag && DateTime.Now <= dateTime)
				{
					string documentHtml = WebBrowserContentsWebView2.GetDocumentHtml(webView, currentUrl);
					try
					{
						using (IHtmlDocument htmlDocument = AngleSharpUtils.ParseHtmlAndNormalizeStyles(documentHtml))
						{
							flag = htmlDocument.QuerySelectorAll(options.WaitForSelector).Any<IElement>();
						}
					}
					catch (DomException ex)
					{
						throw ValueException.NewDataSourceError<Message0>(Resources.UnableToUseSelectorOnPage, RecordValue.New(new NamedValue[]
						{
							new NamedValue("Url", TextValue.New(currentUrl)),
							new NamedValue("Selector", TextValue.New(options.WaitForSelector)),
							new NamedValue("ErrorMessage", TextValue.New(ex.Message))
						}), null);
					}
				}
				if (!flag)
				{
					throw ValueException.NewDataSourceError<Message0>(Resources.TimedOutWaitingForSelector, RecordValue.New(new NamedValue[]
					{
						new NamedValue("Url", TextValue.New(currentUrl)),
						new NamedValue("Selector", TextValue.New(options.WaitForSelector))
					}), null);
				}
			}
			else if (options.WaitForTimeout != null)
			{
				Task waitTask = Task.Delay((int)options.WaitForTimeout.Value.TotalMilliseconds);
				WebBrowserContentsWebView2.WaitForCondition(() => waitTask.IsCompleted, TimeSpan.MaxValue, null, null);
			}
			return WebBrowserContentsWebView2.GetDocumentHtml(webView, currentUrl);
		}

		// Token: 0x0601137A RID: 70522 RVA: 0x003B49A8 File Offset: 0x003B2BA8
		private static string GetDocumentHtml(WebView2 webView, string currentUrl)
		{
			Task<string> htmlTask = webView.ExecuteScriptAsync("document.documentElement.outerHTML");
			WebBrowserContentsWebView2.WaitForCondition(() => htmlTask.IsCompleted, WebBrowserContentsWebView2.javascriptTimeout, Resources.TimedOutWaitingForJavascript, currentUrl);
			return new JavaScriptSerializer
			{
				MaxJsonLength = int.MaxValue
			}.Deserialize<string>(htmlTask.Result);
		}

		// Token: 0x0601137B RID: 70523 RVA: 0x003B4A10 File Offset: 0x003B2C10
		private static void ValidateUrl(string url)
		{
			try
			{
				Uri uri = new Uri(url);
				if (uri.Scheme == "http" || uri.Scheme == "https")
				{
					return;
				}
			}
			catch (UriFormatException)
			{
			}
			throw ValueException.NewDataSourceError<Message0>(Resources.InvalidURL, TextValue.NewOrNull(url), null);
		}

		// Token: 0x0601137C RID: 70524 RVA: 0x003B4A74 File Offset: 0x003B2C74
		private static void WaitForCondition(Func<bool> condition, TimeSpan timeout, string timeoutMessage, string timeoutDetail)
		{
			WebBrowserContentsWebView2.WaitForCondition(condition, timeout, timeoutMessage, TextValue.NewOrNull(timeoutDetail));
		}

		// Token: 0x0601137D RID: 70525 RVA: 0x003B4A84 File Offset: 0x003B2C84
		private static void WaitForCondition(Func<bool> condition, TimeSpan timeout, string timeoutMessage, Value timeoutDetail = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (!condition())
			{
				if (stopwatch.Elapsed > timeout)
				{
					throw ValueException.NewDataSourceError(timeoutMessage, timeoutDetail ?? Value.Null, null);
				}
				Application.DoEvents();
			}
		}

		// Token: 0x04006870 RID: 26736
		private static readonly TimeSpan initializationTimeout = TimeSpan.FromMilliseconds(30000.0);

		// Token: 0x04006871 RID: 26737
		private static readonly TimeSpan pageLoadTimeout = TimeSpan.FromMilliseconds(120000.0);

		// Token: 0x04006872 RID: 26738
		private static readonly TimeSpan javascriptTimeout = TimeSpan.FromMilliseconds(10000.0);

		// Token: 0x04006873 RID: 26739
		private static readonly TimeSpan defaultWaitForTimeoutIfSelectorIsSpecified = TimeSpan.FromSeconds(30.0);

		// Token: 0x0200204D RID: 8269
		private struct WebCredential
		{
			// Token: 0x06011380 RID: 70528 RVA: 0x003B4B28 File Offset: 0x003B2D28
			public WebCredential(IEngineHost host, IResource resource, ResourceCredentialCollection credentials)
			{
				this.credential = credentials.RemoveAdornments();
				if (this.credential == null)
				{
					this.Kind = AuthenticationKind.Implicit;
					return;
				}
				if (this.credential is BasicAuthCredential)
				{
					this.Kind = AuthenticationKind.UsernamePassword;
					return;
				}
				if (this.credential is WebApiKeyCredential)
				{
					this.Kind = AuthenticationKind.WebApi;
					return;
				}
				if (this.credential is WindowsCredential)
				{
					this.Kind = AuthenticationKind.Windows;
					return;
				}
				string text = Resources.OnlyAnonymousWindowsWebApiBasicAuthenticationSupported("Web.BrowserContents");
				throw DataSourceException.NewInvalidCredentialsError(host, resource, text, text, null);
			}

			// Token: 0x17002DFA RID: 11770
			// (get) Token: 0x06011381 RID: 70529 RVA: 0x003B4BAA File Offset: 0x003B2DAA
			public BasicAuthCredential BasicAuthCredential
			{
				get
				{
					return (BasicAuthCredential)this.credential;
				}
			}

			// Token: 0x17002DFB RID: 11771
			// (get) Token: 0x06011382 RID: 70530 RVA: 0x003B4BB7 File Offset: 0x003B2DB7
			public WebApiKeyCredential WebApiKeyCredential
			{
				get
				{
					return (WebApiKeyCredential)this.credential;
				}
			}

			// Token: 0x17002DFC RID: 11772
			// (get) Token: 0x06011383 RID: 70531 RVA: 0x003B4BC4 File Offset: 0x003B2DC4
			public WindowsCredential WindowsCredential
			{
				get
				{
					return (WindowsCredential)this.credential;
				}
			}

			// Token: 0x17002DFD RID: 11773
			// (get) Token: 0x06011384 RID: 70532 RVA: 0x003B4BD1 File Offset: 0x003B2DD1
			public WindowsCredential WindowsCredentialOrNull
			{
				get
				{
					return this.credential as WindowsCredential;
				}
			}

			// Token: 0x04006874 RID: 26740
			public readonly AuthenticationKind Kind;

			// Token: 0x04006875 RID: 26741
			private readonly IResourceCredential credential;
		}
	}
}
