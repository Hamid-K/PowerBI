using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A89 RID: 2697
	[Guid("3050F534-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTableSectionClass))]
	[ComImport]
	public interface HTMLTableSection : DispHTMLTableSection, HTMLElementEvents_Event
	{
	}
}
