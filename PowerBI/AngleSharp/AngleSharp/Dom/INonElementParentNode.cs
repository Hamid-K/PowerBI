using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000195 RID: 405
	[DomName("NonElementParentNode")]
	[DomNoInterfaceObject]
	public interface INonElementParentNode
	{
		// Token: 0x06000E86 RID: 3718
		[DomName("getElementById")]
		IElement GetElementById(string elementId);
	}
}
