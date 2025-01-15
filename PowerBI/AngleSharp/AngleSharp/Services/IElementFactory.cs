using System;
using AngleSharp.Dom;

namespace AngleSharp.Services
{
	// Token: 0x0200002A RID: 42
	internal interface IElementFactory<TElement> where TElement : Element
	{
		// Token: 0x0600012D RID: 301
		TElement Create(Document document, string localName, string prefix = null);
	}
}
