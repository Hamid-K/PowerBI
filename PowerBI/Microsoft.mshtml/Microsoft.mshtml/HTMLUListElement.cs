using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004A9 RID: 1193
	[CoClass(typeof(HTMLUListElementClass))]
	[Guid("3050F538-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLUListElement : DispHTMLUListElement, HTMLElementEvents_Event
	{
	}
}
