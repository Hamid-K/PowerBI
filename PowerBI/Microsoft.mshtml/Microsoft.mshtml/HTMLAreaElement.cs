using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009D9 RID: 2521
	[Guid("3050F503-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLAreaElementClass))]
	[ComImport]
	public interface HTMLAreaElement : DispHTMLAreaElement, HTMLAreaEvents_Event
	{
	}
}
