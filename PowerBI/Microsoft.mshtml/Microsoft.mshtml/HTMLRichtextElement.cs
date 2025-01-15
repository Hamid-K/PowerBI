using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200069A RID: 1690
	[Guid("3050F54D-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLRichtextElementClass))]
	[ComImport]
	public interface HTMLRichtextElement : DispHTMLRichtextElement, HTMLInputTextElementEvents_Event
	{
	}
}
