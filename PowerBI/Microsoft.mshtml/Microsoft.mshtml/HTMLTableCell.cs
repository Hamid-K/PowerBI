using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A8F RID: 2703
	[Guid("3050F536-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTableCellClass))]
	[ComImport]
	public interface HTMLTableCell : DispHTMLTableCell, HTMLTextContainerEvents_Event
	{
	}
}
