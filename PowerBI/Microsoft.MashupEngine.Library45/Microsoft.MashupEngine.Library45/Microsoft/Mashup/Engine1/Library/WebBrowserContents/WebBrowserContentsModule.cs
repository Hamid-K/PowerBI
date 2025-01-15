using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.WebBrowserContents;

namespace Microsoft.Mashup.Engine1.Library.WebBrowserContents
{
	// Token: 0x02002045 RID: 8261
	public sealed class WebBrowserContentsModule : Module45
	{
		// Token: 0x17002DF3 RID: 11763
		// (get) Token: 0x06011358 RID: 70488 RVA: 0x003B3C07 File Offset: 0x003B1E07
		public override ResourceManager DocumentationResources
		{
			get
			{
				return Resources.ResourceManager;
			}
		}

		// Token: 0x06011359 RID: 70489 RVA: 0x003B3C10 File Offset: 0x003B1E10
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new WebBrowserContentsModule.BrowserContentsFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x17002DF4 RID: 11764
		// (get) Token: 0x0601135A RID: 70490 RVA: 0x003B3C41 File Offset: 0x003B1E41
		public override string Name
		{
			get
			{
				return "WebBrowserContents";
			}
		}

		// Token: 0x17002DF5 RID: 11765
		// (get) Token: 0x0601135B RID: 70491 RVA: 0x003B3C48 File Offset: 0x003B1E48
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Web.BrowserContents";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x0400685B RID: 26715
		public const string DataSourceNameString = "WebBrowserContents";

		// Token: 0x0400685C RID: 26716
		public const string WebBrowserContentsName = "Web.BrowserContents";

		// Token: 0x0400685D RID: 26717
		private Keys exportKeys;

		// Token: 0x02002046 RID: 8262
		private class BrowserContentsFunctionValue : NativeFunctionValue2<TextValue, TextValue, Value>
		{
			// Token: 0x0601135D RID: 70493 RVA: 0x003B3C83 File Offset: 0x003B1E83
			public BrowserContentsFunctionValue(IEngineHost host)
				: base(TypeValue.Text, 1, "url", TypeValue.Text, "options", WebBrowserContentsModule.BrowserContentsFunctionValue.optionType)
			{
				this.host = host;
			}

			// Token: 0x17002DF6 RID: 11766
			// (get) Token: 0x0601135E RID: 70494 RVA: 0x003B3CAC File Offset: 0x003B1EAC
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x0601135F RID: 70495 RVA: 0x003B3CB4 File Offset: 0x003B1EB4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				string text;
				string text2;
				if (WebBrowserContentsModule.BrowserContentsFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("url", out value) && value.IsText && WebBrowserContentsModule.BrowserContentsFunctionValue.TryGetAbsoluteUrl(value.AsString, out text) && Resource.TryNormalizeWebUri(text, out text2, out text))
				{
					HttpDataSourceLocation httpDataSourceLocation = new HttpDataSourceLocation
					{
						Address = new Dictionary<string, object> { { "url", text } }
					};
					Value value2 = Value.Null;
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2))
					{
						value2 = ExpressionAnalysis.GetValue(expression2);
					}
					if (value2.IsRecord)
					{
						foundOptions = ExpressionAnalysis.RemovePlaceholders(value2.AsRecord, out unknownOptions);
					}
					else if (value2.IsNull)
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
					}
					else
					{
						foundOptions = null;
						unknownOptions = null;
					}
					if (foundOptions != null)
					{
						foundOptions = foundOptions.Concatenate(RecordValue.New(new NamedValue[]
						{
							new NamedValue("IsWebBrowserContents", LogicalValue.True)
						})).AsRecord;
						location = httpDataSourceLocation;
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06011360 RID: 70496 RVA: 0x003B3DD0 File Offset: 0x003B1FD0
			public override TextValue TypedInvoke(TextValue url, Value options)
			{
				string absoluteUrl = WebBrowserContentsModule.BrowserContentsFunctionValue.GetAbsoluteUrl(url.AsString);
				IResource resource = Resource.New("Web", absoluteUrl);
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
				WebBrowserContentsOptions webBrowserContentsOptions = new WebBrowserContentsOptions("Web.BrowserContents", WebBrowserContentsModule.BrowserContentsFunctionValue.optionRecord.CreateOptions("Web.BrowserContents", options));
				IPersistentCache persistentCache = this.host.GetPersistentCache();
				string text = PersistentCacheKey.WebBrowserContents.Qualify(resourceCredentialCollection.GetHash(), absoluteUrl, options.CreateCacheKey(), true.ToString());
				Stream stream;
				if (!persistentCache.TryGetValue(text, out stream))
				{
					string pageSourceWithRedirects = this.GetPageSourceWithRedirects(absoluteUrl, webBrowserContentsOptions, resource, resourceCredentialCollection);
					stream = persistentCache.Add(text, new MemoryStream(Encoding.UTF8.GetBytes(pageSourceWithRedirects)));
				}
				string text2 = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
				stream.Close();
				RecordValue recordValue = RecordValue.New(Keys.New("Web.Url"), new Value[] { TextValue.New(absoluteUrl) });
				return TextValue.New(text2).NewMeta(recordValue).AsText;
			}

			// Token: 0x06011361 RID: 70497 RVA: 0x003B3EDE File Offset: 0x003B20DE
			private string GetPageSource(string url, WebBrowserContentsOptions options, IResource resource, ResourceCredentialCollection credentials)
			{
				return WebBrowserContentsWebView2.GetPageSource(this.host, url, options, resource, credentials);
			}

			// Token: 0x06011362 RID: 70498 RVA: 0x003B3EF0 File Offset: 0x003B20F0
			private string GetPageSourceWithRedirects(string originalUrl, WebBrowserContentsOptions options, IResource originalResource, ResourceCredentialCollection originalCredentials)
			{
				string text = originalUrl;
				IResource resource = originalResource;
				ResourceCredentialCollection resourceCredentialCollection = originalCredentials;
				int i = 0;
				while (i <= 20)
				{
					try
					{
						return this.GetPageSource(text, options, resource, resourceCredentialCollection);
					}
					catch (RedirectException ex)
					{
						i++;
						text = WebBrowserContentsModule.BrowserContentsFunctionValue.GetAbsoluteUrl(ex.NewUrl);
						resource = Resource.New("Web", text);
						resourceCredentialCollection = this.GetRedirectCredentials(text, resource, originalUrl, originalCredentials);
					}
				}
				throw DataSourceException.NewDataSourceError<Message1>(this.host, Resources.TooManyRedirects(20), Resource.New("Web", originalUrl), null, null);
			}

			// Token: 0x06011363 RID: 70499 RVA: 0x003B3F7C File Offset: 0x003B217C
			private ResourceCredentialCollection GetRedirectCredentials(string currentUrl, IResource currentResource, string originalUrl, ResourceCredentialCollection originalCredentials)
			{
				if (!HostResourcePermissionService.InsecureRedirects(this.host))
				{
					return HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, currentResource, null);
				}
				Uri uri = new Uri(currentUrl);
				Uri uri2 = new Uri(originalUrl);
				if (string.Equals(uri.Authority, uri2.Authority, StringComparison.OrdinalIgnoreCase))
				{
					return originalCredentials;
				}
				return new ResourceCredentialCollection(currentResource, Array.Empty<IResourceCredential>());
			}

			// Token: 0x06011364 RID: 70500 RVA: 0x003B3FD4 File Offset: 0x003B21D4
			private static string GetAbsoluteUrl(string url)
			{
				string text;
				try
				{
					text = new Uri(url).AbsoluteUri;
				}
				catch (UriFormatException ex)
				{
					try
					{
						text = new Uri("https://" + url).AbsoluteUri;
					}
					catch (UriFormatException)
					{
						throw ValueException.NewDataSourceError(ex.Message, TextValue.New(url), ex);
					}
				}
				return text;
			}

			// Token: 0x06011365 RID: 70501 RVA: 0x003B403C File Offset: 0x003B223C
			private static bool TryGetAbsoluteUrl(string url, out string absoluteUrl)
			{
				bool flag;
				try
				{
					absoluteUrl = WebBrowserContentsModule.BrowserContentsFunctionValue.GetAbsoluteUrl(url);
					flag = true;
				}
				catch (ValueException)
				{
					absoluteUrl = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x0400685E RID: 26718
			private const int RedirectLimit = 20;

			// Token: 0x0400685F RID: 26719
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_options)" });

			// Token: 0x04006860 RID: 26720
			private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(new OptionItem[]
			{
				new OptionItem("ApiKeyName", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
				new OptionItem("WaitFor", WebBrowserContentsOptions.WaitForOptionRecord.CreateRecordType().Nullable, Value.Null, OptionItemOption.None, delegate(Value option, out object value)
				{
					if (option.IsRecord)
					{
						value = ValueMarshaller.MarshalToClrDictionary(option.AsRecord);
						return true;
					}
					value = null;
					return false;
				}, null)
			});

			// Token: 0x04006861 RID: 26721
			private static readonly TypeValue optionType = WebBrowserContentsModule.BrowserContentsFunctionValue.optionRecord.CreateRecordType().Nullable;

			// Token: 0x04006862 RID: 26722
			private static readonly TimeSpan defaultWaitForTimeoutIfSelectorIsSpecified = TimeSpan.FromSeconds(30.0);

			// Token: 0x04006863 RID: 26723
			private readonly IEngineHost host;
		}

		// Token: 0x02002048 RID: 8264
		private enum Exports
		{
			// Token: 0x04006866 RID: 26726
			Web_BrowserContents,
			// Token: 0x04006867 RID: 26727
			Count
		}
	}
}
