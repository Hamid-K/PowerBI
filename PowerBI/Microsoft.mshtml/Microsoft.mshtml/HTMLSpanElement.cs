using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009ED RID: 2541
	[Guid("3050F548-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLSpanElementClass))]
	[ComImport]
	public interface HTMLSpanElement : DispHTMLSpanElement, HTMLElementEvents_Event
	{
	}
}
