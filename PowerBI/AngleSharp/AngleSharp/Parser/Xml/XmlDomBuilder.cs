using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Xml;
using AngleSharp.Extensions;
using AngleSharp.Services;
using AngleSharp.Xml;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000057 RID: 87
	internal sealed class XmlDomBuilder
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000D198 File Offset: 0x0000B398
		internal XmlDomBuilder(Document document, Func<Document, string, string, Element> creator = null)
		{
			IEntityProvider entityProvider = document.Options.GetProvider<IEntityProvider>() ?? XmlEntityService.Resolver;
			this._tokenizer = new XmlTokenizer(document.Source, entityProvider);
			this._document = document;
			this._standalone = false;
			this._openElements = new List<Element>();
			this._currentMode = XmlTreeMode.Initial;
			this._creator = creator ?? new Func<Document, string, string, Element>(XmlDomBuilder.CreateElement);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000D209 File Offset: 0x0000B409
		public Node CurrentNode
		{
			get
			{
				if (this._openElements.Count > 0)
				{
					return this._openElements[this._openElements.Count - 1];
				}
				return this._document;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000D238 File Offset: 0x0000B438
		public async Task<Document> ParseAsync(XmlParserOptions options, CancellationToken cancelToken)
		{
			TextSource source = this._document.Source;
			this._options = options;
			this._tokenizer.IsSuppressingErrors = options.IsSuppressingErrors;
			XmlToken xmlToken;
			do
			{
				if (source.Length - source.Index < 1024)
				{
					await source.PrefetchAsync(8192, cancelToken).ConfigureAwait(false);
				}
				xmlToken = this._tokenizer.Get();
				this.Consume(xmlToken);
			}
			while (xmlToken.Type != XmlTokenType.EndOfFile);
			return this._document;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D290 File Offset: 0x0000B490
		public Document Parse(XmlParserOptions options)
		{
			this._options = options;
			this._tokenizer.IsSuppressingErrors = options.IsSuppressingErrors;
			XmlToken xmlToken;
			do
			{
				xmlToken = this._tokenizer.Get();
				this.Consume(xmlToken);
			}
			while (xmlToken.Type != XmlTokenType.EndOfFile);
			return this._document;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		private void Consume(XmlToken token)
		{
			switch (this._currentMode)
			{
			case XmlTreeMode.Initial:
				this.Initial(token);
				return;
			case XmlTreeMode.Prolog:
				this.BeforeDoctype(token);
				return;
			case XmlTreeMode.Misc:
				this.InMisc(token);
				return;
			case XmlTreeMode.Body:
				this.InBody(token);
				return;
			case XmlTreeMode.After:
				this.AfterBody(token);
				return;
			default:
				return;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D334 File Offset: 0x0000B534
		private void Initial(XmlToken token)
		{
			if (token.Type == XmlTokenType.Declaration)
			{
				XmlDeclarationToken xmlDeclarationToken = (XmlDeclarationToken)token;
				this._standalone = xmlDeclarationToken.Standalone;
				if (!xmlDeclarationToken.IsEncodingMissing)
				{
					this.SetEncoding(xmlDeclarationToken.Encoding);
				}
				if (!this.CheckVersion(xmlDeclarationToken.Version) && !this._options.IsSuppressingErrors)
				{
					throw XmlParseError.XmlDeclarationVersionUnsupported.At(token.Position);
				}
			}
			else
			{
				this._currentMode = XmlTreeMode.Prolog;
				this.BeforeDoctype(token);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		private void BeforeDoctype(XmlToken token)
		{
			if (token.Type == XmlTokenType.Doctype)
			{
				XmlDoctypeToken xmlDoctypeToken = (XmlDoctypeToken)token;
				DocumentType documentType = new DocumentType(this._document, xmlDoctypeToken.Name)
				{
					SystemIdentifier = xmlDoctypeToken.SystemIdentifier,
					PublicIdentifier = xmlDoctypeToken.PublicIdentifier
				};
				this._document.AppendChild(documentType);
				this._currentMode = XmlTreeMode.Misc;
				return;
			}
			this.InMisc(token);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000D410 File Offset: 0x0000B610
		private void InMisc(XmlToken token)
		{
			XmlTokenType type = token.Type;
			if (type == XmlTokenType.StartTag)
			{
				this._currentMode = XmlTreeMode.Body;
				this.InBody(token);
				return;
			}
			if (type == XmlTokenType.Comment)
			{
				XmlCommentToken xmlCommentToken = (XmlCommentToken)token;
				IComment comment = this._document.CreateComment(xmlCommentToken.Data);
				this.CurrentNode.AppendChild(comment);
				return;
			}
			if (type == XmlTokenType.ProcessingInstruction)
			{
				XmlPIToken xmlPIToken = (XmlPIToken)token;
				IProcessingInstruction processingInstruction = this._document.CreateProcessingInstruction(xmlPIToken.Target, xmlPIToken.Content);
				this.CurrentNode.AppendChild(processingInstruction);
				return;
			}
			if (!token.IsIgnorable && !this._options.IsSuppressingErrors)
			{
				throw XmlParseError.XmlMissingRoot.At(token.Position);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		private void InBody(XmlToken token)
		{
			switch (token.Type)
			{
			case XmlTokenType.Doctype:
				if (!this._options.IsSuppressingErrors)
				{
					throw XmlParseError.XmlDoctypeAfterContent.At(token.Position);
				}
				break;
			case XmlTokenType.Declaration:
				if (!this._options.IsSuppressingErrors)
				{
					throw XmlParseError.XmlDeclarationMisplaced.At(token.Position);
				}
				break;
			case XmlTokenType.StartTag:
			{
				XmlTagToken xmlTagToken = (XmlTagToken)token;
				Element element = this._creator(this._document, xmlTagToken.Name, null);
				this.CurrentNode.AppendChild(element);
				if (!xmlTagToken.IsSelfClosing)
				{
					this._openElements.Add(element);
				}
				else if (this._openElements.Count == 0)
				{
					this._currentMode = XmlTreeMode.After;
				}
				for (int i = 0; i < xmlTagToken.Attributes.Count; i++)
				{
					string key = xmlTagToken.Attributes[i].Key;
					string text = xmlTagToken.Attributes[i].Value.Trim();
					element.SetAttribute(key, text);
				}
				if (this._options.OnCreated != null)
				{
					this._options.OnCreated(element, xmlTagToken.Position);
					return;
				}
				break;
			}
			case XmlTokenType.EndTag:
			{
				XmlTagToken xmlTagToken2 = (XmlTagToken)token;
				if (!this.CurrentNode.NodeName.Is(xmlTagToken2.Name))
				{
					if (!this._options.IsSuppressingErrors)
					{
						throw XmlParseError.TagClosingMismatch.At(token.Position);
					}
				}
				else
				{
					this._openElements.RemoveAt(this._openElements.Count - 1);
					if (this._openElements.Count == 0)
					{
						this._currentMode = XmlTreeMode.After;
						return;
					}
				}
				break;
			}
			case XmlTokenType.Comment:
			case XmlTokenType.ProcessingInstruction:
				this.InMisc(token);
				return;
			case XmlTokenType.CData:
			{
				XmlCDataToken xmlCDataToken = (XmlCDataToken)token;
				this.CurrentNode.AppendText(xmlCDataToken.Data);
				return;
			}
			case XmlTokenType.Character:
			{
				XmlCharacterToken xmlCharacterToken = (XmlCharacterToken)token;
				this.CurrentNode.AppendText(xmlCharacterToken.Data);
				return;
			}
			case XmlTokenType.CharacterReference:
				break;
			case XmlTokenType.EndOfFile:
				if (!this._options.IsSuppressingErrors)
				{
					throw XmlParseError.EOF.At(token.Position);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		private void AfterBody(XmlToken token)
		{
			XmlTokenType type = token.Type;
			if (type == XmlTokenType.Comment || type == XmlTokenType.ProcessingInstruction)
			{
				this.InMisc(token);
				return;
			}
			if (type != XmlTokenType.EndOfFile && !token.IsIgnorable && !this._options.IsSuppressingErrors)
			{
				throw XmlParseError.XmlMissingRoot.At(token.Position);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D729 File Offset: 0x0000B929
		private static Element CreateElement(Document document, string name, string prefix)
		{
			return new XmlElement(document, name, prefix);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D734 File Offset: 0x0000B934
		private bool CheckVersion(string ver)
		{
			double num = ver.ToDouble(0.0);
			return num >= 1.0 && num < 2.0;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D76C File Offset: 0x0000B96C
		private void SetEncoding(string charSet)
		{
			if (TextEncoding.IsSupported(charSet))
			{
				Encoding encoding = TextEncoding.Resolve(charSet);
				if (encoding != null)
				{
					try
					{
						this._document.Source.CurrentEncoding = encoding;
					}
					catch (NotSupportedException)
					{
						this._currentMode = XmlTreeMode.Initial;
						this._document.ReplaceAll(null, true);
						this._openElements.Clear();
					}
				}
			}
		}

		// Token: 0x040001D9 RID: 473
		private readonly XmlTokenizer _tokenizer;

		// Token: 0x040001DA RID: 474
		private readonly Document _document;

		// Token: 0x040001DB RID: 475
		private readonly List<Element> _openElements;

		// Token: 0x040001DC RID: 476
		private readonly Func<Document, string, string, Element> _creator;

		// Token: 0x040001DD RID: 477
		private XmlParserOptions _options;

		// Token: 0x040001DE RID: 478
		private XmlTreeMode _currentMode;

		// Token: 0x040001DF RID: 479
		private bool _standalone;
	}
}
