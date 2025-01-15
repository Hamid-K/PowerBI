using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AC2 RID: 2754
	internal sealed class HtmlModule : Module
	{
		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x000FDD3E File Offset: 0x000FBF3E
		public override string Name
		{
			get
			{
				return "Html";
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x000FDD45 File Offset: 0x000FBF45
		public override Keys ExportKeys
		{
			get
			{
				if (HtmlModule.exportKeys == null)
				{
					HtmlModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Web.Page";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return HtmlModule.exportKeys;
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x000FDD80 File Offset: 0x000FBF80
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new HtmlModule.PageFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040028F6 RID: 10486
		private static readonly Keys webMetadataKeys = Keys.New("Web.Url");

		// Token: 0x040028F7 RID: 10487
		private static Keys exportKeys;

		// Token: 0x02000AC3 RID: 2755
		private enum Exports
		{
			// Token: 0x040028F9 RID: 10489
			Page,
			// Token: 0x040028FA RID: 10490
			Count
		}

		// Token: 0x02000AC4 RID: 2756
		private sealed class PageFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x06004CFE RID: 19710 RVA: 0x000FDDC2 File Offset: 0x000FBFC2
			public PageFunctionValue(IEngineHost host)
				: base(TypeValue.Table, "html", TypeValue.Any)
			{
				this.host = host;
			}

			// Token: 0x06004CFF RID: 19711 RVA: 0x000FDDE0 File Offset: 0x000FBFE0
			public override TableValue TypedInvoke(Value html)
			{
				ContentHelper.VerifyIsContentType(html);
				Uri uri;
				string text;
				if (DataSource.TryGetContentUri(html, out uri))
				{
					text = uri.AbsoluteUri;
				}
				else
				{
					text = null;
					uri = null;
				}
				IPersistentCache persistentCache = this.host.GetPersistentCache();
				if (text == null)
				{
					Strings.HtmlPageProgress;
				}
				else
				{
					string text2 = new Uri(text).Host;
				}
				Stream stream;
				if (text != null && uri.Scheme != "pack" && uri.Scheme != Uri.UriSchemeFile)
				{
					string text3;
					if (!DataSource.TryGetInitialUri(html, out text3))
					{
						text3 = text;
					}
					Request requestFromMetadataOptions = WebOptionsHelper.GetRequestFromMetadataOptions(this.host, "Web", TextValue.New(text3), html);
					if (!requestFromMetadataOptions.Content.IsNull)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.WebPageContentsNotSupported, Value.Null, null);
					}
					string text4 = (requestFromMetadataOptions.Content.IsNull ? string.Empty : Convert.ToBase64String(requestFromMetadataOptions.Content.AsBinary.AsBytes));
					string text5 = requestFromMetadataOptions.GetHeaderString() ?? string.Empty;
					string text6 = PersistentCacheKey.WebPageUri.Qualify(requestFromMetadataOptions.Uri.AbsoluteUri, text4, text5);
					if (!persistentCache.TryGetValue(text6, out stream))
					{
						stream = persistentCache.Add(text6, Browser.GetDomFromRequest(this.host, requestFromMetadataOptions));
					}
				}
				else
				{
					string sourceText = HtmlModule.PageFunctionValue.GetSourceText(html);
					string text7 = PersistentCacheKey.WebPageText.Qualify(HtmlModule.PageFunctionValue.Digest(sourceText));
					if (!persistentCache.TryGetValue(text7, out stream))
					{
						stream = persistentCache.Add(text7, Browser.GetDomFromText(this.host, sourceText));
					}
				}
				DomNode domNode = DomNode.Deserialize(stream);
				stream.Close();
				TableValue tableValue = HtmlModuleHelper.Contents(domNode);
				if (text != null)
				{
					RecordValue recordValue = RecordValue.New(HtmlModule.webMetadataKeys, new Value[] { TextValue.New(text) });
					tableValue = BinaryOperator.AddMeta.Invoke(tableValue, recordValue).AsTable;
				}
				return tableValue;
			}

			// Token: 0x06004D00 RID: 19712 RVA: 0x000FDFC8 File Offset: 0x000FC1C8
			private static string Digest(string contents)
			{
				string @string;
				using (HashAlgorithm hashAlgorithm = CryptoAlgorithmFactory.CreateSHA256Algorithm())
				{
					hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(contents));
					@string = Base64Encoding.GetString(hashAlgorithm.Hash);
				}
				return @string;
			}

			// Token: 0x06004D01 RID: 19713 RVA: 0x000FE018 File Offset: 0x000FC218
			private static string GetSourceText(Value html)
			{
				if (html.IsText)
				{
					return html.AsString;
				}
				byte[] asBytes = html.AsBinary.AsBytes;
				return TextEncoding.GetTextDecoder(Value.Null).Decode(asBytes, 0, asBytes.Length);
			}

			// Token: 0x040028FB RID: 10491
			private IEngineHost host;
		}
	}
}
