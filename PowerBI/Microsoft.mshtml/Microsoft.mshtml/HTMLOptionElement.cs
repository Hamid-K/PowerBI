using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200056E RID: 1390
	[Guid("3050F52B-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLOptionElementClass))]
	[ComImport]
	public interface HTMLOptionElement : DispHTMLOptionElement, HTMLElementEvents_Event
	{
	}
}
