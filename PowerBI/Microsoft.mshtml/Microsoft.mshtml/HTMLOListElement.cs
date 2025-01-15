using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004AD RID: 1197
	[Guid("3050F52A-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLOListElementClass))]
	[ComImport]
	public interface HTMLOListElement : DispHTMLOListElement, HTMLElementEvents_Event
	{
	}
}
