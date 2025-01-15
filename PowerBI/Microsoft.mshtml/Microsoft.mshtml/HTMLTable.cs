using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A83 RID: 2691
	[CoClass(typeof(HTMLTableClass))]
	[Guid("3050F532-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLTable : DispHTMLTable, HTMLTableEvents_Event
	{
	}
}
