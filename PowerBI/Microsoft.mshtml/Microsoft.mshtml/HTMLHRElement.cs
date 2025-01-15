using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004CE RID: 1230
	[Guid("3050F53D-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLHRElementClass))]
	[ComImport]
	public interface HTMLHRElement : DispHTMLHRElement, HTMLElementEvents_Event
	{
	}
}
