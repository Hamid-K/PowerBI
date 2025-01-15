using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A86 RID: 2694
	[Guid("3050F533-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTableColClass))]
	[ComImport]
	public interface HTMLTableCol : DispHTMLTableCol, HTMLElementEvents_Event
	{
	}
}
