using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B6 RID: 1206
	[Guid("3050F506-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLBlockElementClass))]
	[ComImport]
	public interface HTMLBlockElement : DispHTMLBlockElement, HTMLElementEvents_Event
	{
	}
}
