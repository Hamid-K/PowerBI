using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000565 RID: 1381
	[CoClass(typeof(HTMLSelectElementClass))]
	[Guid("3050F531-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLSelectElement : DispHTMLSelectElement, HTMLSelectElementEvents_Event
	{
	}
}
