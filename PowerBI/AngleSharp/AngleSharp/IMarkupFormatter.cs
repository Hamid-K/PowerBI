using System;
using AngleSharp.Dom;

namespace AngleSharp
{
	// Token: 0x02000011 RID: 17
	public interface IMarkupFormatter
	{
		// Token: 0x0600006C RID: 108
		string Text(string text);

		// Token: 0x0600006D RID: 109
		string Comment(IComment comment);

		// Token: 0x0600006E RID: 110
		string Processing(IProcessingInstruction processing);

		// Token: 0x0600006F RID: 111
		string Doctype(IDocumentType doctype);

		// Token: 0x06000070 RID: 112
		string OpenTag(IElement element, bool selfClosing);

		// Token: 0x06000071 RID: 113
		string CloseTag(IElement element, bool selfClosing);

		// Token: 0x06000072 RID: 114
		string Attribute(IAttr attribute);
	}
}
