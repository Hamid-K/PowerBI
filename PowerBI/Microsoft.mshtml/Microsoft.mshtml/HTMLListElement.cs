using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004A5 RID: 1189
	[CoClass(typeof(HTMLListElementClass))]
	[Guid("3050F525-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLListElement : DispHTMLListElement, HTMLElementEvents_Event
	{
	}
}
