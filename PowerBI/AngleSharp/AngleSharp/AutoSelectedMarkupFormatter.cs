using System;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.XHtml;
using AngleSharp.Xml;

namespace AngleSharp
{
	// Token: 0x02000006 RID: 6
	public sealed class AutoSelectedMarkupFormatter : IMarkupFormatter
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023AA File Offset: 0x000005AA
		public AutoSelectedMarkupFormatter(IDocumentType docType = null)
		{
			this._docType = docType;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023BC File Offset: 0x000005BC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000242E File Offset: 0x0000062E
		private IMarkupFormatter ChildFormatter
		{
			get
			{
				if (this.childFormatter == null && this._docType != null)
				{
					if (this._docType.PublicIdentifier.Contains("XML"))
					{
						this.childFormatter = XmlMarkupFormatter.Instance;
					}
					else if (this._docType.PublicIdentifier.Contains("XHTML"))
					{
						this.childFormatter = XhtmlMarkupFormatter.Instance;
					}
				}
				return this.childFormatter ?? HtmlMarkupFormatter.Instance;
			}
			set
			{
				this.childFormatter = value;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002437 File Offset: 0x00000637
		public string Attribute(IAttr attribute)
		{
			return this.ChildFormatter.Attribute(attribute);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002445 File Offset: 0x00000645
		public string OpenTag(IElement element, bool selfClosing)
		{
			this.Confirm(element.Owner.Doctype);
			return this.ChildFormatter.OpenTag(element, selfClosing);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002465 File Offset: 0x00000665
		public string CloseTag(IElement element, bool selfClosing)
		{
			this.Confirm(element.Owner.Doctype);
			return this.ChildFormatter.CloseTag(element, selfClosing);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002485 File Offset: 0x00000685
		public string Comment(IComment comment)
		{
			this.Confirm(comment.Owner.Doctype);
			return this.ChildFormatter.Comment(comment);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A4 File Offset: 0x000006A4
		public string Doctype(IDocumentType doctype)
		{
			this.Confirm(doctype);
			return this.ChildFormatter.Doctype(doctype);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024B9 File Offset: 0x000006B9
		public string Processing(IProcessingInstruction processing)
		{
			this.Confirm(processing.Owner.Doctype);
			return this.ChildFormatter.Processing(processing);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024D8 File Offset: 0x000006D8
		public string Text(string text)
		{
			return this.ChildFormatter.Text(text);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024E6 File Offset: 0x000006E6
		private void Confirm(IDocumentType docType)
		{
			if (this._docType == null)
			{
				this._docType = docType;
			}
		}

		// Token: 0x04000006 RID: 6
		private IMarkupFormatter childFormatter;

		// Token: 0x04000007 RID: 7
		private IDocumentType _docType;
	}
}
