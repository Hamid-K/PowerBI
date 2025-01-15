using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004BE RID: 1214
	[CoClass(typeof(HTMLDDElementClass))]
	[Guid("3050F50B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLDDElement : DispHTMLDDElement, HTMLElementEvents_Event
	{
	}
}
