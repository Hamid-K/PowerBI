using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000786 RID: 1926
	[Guid("3050F518-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLBaseElementClass))]
	[ComImport]
	public interface HTMLBaseElement : DispHTMLBaseElement, HTMLElementEvents_Event
	{
	}
}
