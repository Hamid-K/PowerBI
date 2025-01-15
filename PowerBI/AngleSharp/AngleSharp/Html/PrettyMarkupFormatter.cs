using System;
using System.Linq;
using AngleSharp.Dom;

namespace AngleSharp.Html
{
	// Token: 0x020000BF RID: 191
	public class PrettyMarkupFormatter : IMarkupFormatter
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0002CB87 File Offset: 0x0002AD87
		public PrettyMarkupFormatter()
		{
			this._intendCount = 0;
			this._intendString = "\t";
			this._newLineString = "\n";
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0002CBAC File Offset: 0x0002ADAC
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
		public string Indentation
		{
			get
			{
				return this._intendString;
			}
			set
			{
				this._intendString = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0002CBBD File Offset: 0x0002ADBD
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0002CBC5 File Offset: 0x0002ADC5
		public string NewLine
		{
			get
			{
				return this._newLineString;
			}
			set
			{
				this._newLineString = value;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002CBCE File Offset: 0x0002ADCE
		string IMarkupFormatter.Comment(IComment comment)
		{
			return this.IntendBefore(comment.PreviousSibling) + HtmlMarkupFormatter.Instance.Comment(comment) + this.NewLineAfter(comment.NextSibling);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		string IMarkupFormatter.Doctype(IDocumentType doctype)
		{
			return this.IntendBefore(doctype.PreviousSibling) + HtmlMarkupFormatter.Instance.Doctype(doctype) + this.NewLineAfter(doctype.NextSibling);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002CC22 File Offset: 0x0002AE22
		string IMarkupFormatter.Processing(IProcessingInstruction processing)
		{
			return this.IntendBefore(processing.PreviousSibling) + HtmlMarkupFormatter.Instance.Processing(processing) + this.NewLineAfter(processing.NextSibling);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002CC4C File Offset: 0x0002AE4C
		string IMarkupFormatter.Text(string text)
		{
			string text2 = text.Replace('\n', ' ');
			return HtmlMarkupFormatter.Instance.Text(text2);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002CC70 File Offset: 0x0002AE70
		string IMarkupFormatter.OpenTag(IElement element, bool selfClosing)
		{
			string text = this.IntendBefore(element.PreviousSibling ?? element.Parent);
			this._intendCount++;
			return text + HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing) + this.NewLineAfter(element.FirstChild ?? element.NextSibling);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
		string IMarkupFormatter.CloseTag(IElement element, bool selfClosing)
		{
			this._intendCount--;
			return this.IntendBefore(element.LastChild ?? element.Parent) + HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing) + this.NewLineAfter(element.NextSibling ?? element.Parent);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002CD20 File Offset: 0x0002AF20
		string IMarkupFormatter.Attribute(IAttr attribute)
		{
			return HtmlMarkupFormatter.Instance.Attribute(attribute);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002CD2D File Offset: 0x0002AF2D
		private string NewLineAfter(INode node)
		{
			if (node != null && node.NodeType != NodeType.Text)
			{
				return this._newLineString;
			}
			return string.Empty;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002CD47 File Offset: 0x0002AF47
		private string IntendBefore(INode node)
		{
			if (node != null && node.NodeType != NodeType.Text)
			{
				return string.Join(string.Empty, Enumerable.Repeat<string>(this._intendString, this._intendCount));
			}
			return string.Empty;
		}

		// Token: 0x04000529 RID: 1321
		private string _intendString;

		// Token: 0x0400052A RID: 1322
		private string _newLineString;

		// Token: 0x0400052B RID: 1323
		private int _intendCount;
	}
}
