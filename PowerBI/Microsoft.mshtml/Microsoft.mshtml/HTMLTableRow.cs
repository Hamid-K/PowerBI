using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A8C RID: 2700
	[Guid("3050F535-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTableRowClass))]
	[ComImport]
	public interface HTMLTableRow : DispHTMLTableRow, HTMLControlElementEvents_Event
	{
	}
}
