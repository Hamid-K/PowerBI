using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D0D RID: 3341
	[CoClass(typeof(OldHTMLFormElementClass))]
	[Guid("3050F510-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface OldHTMLFormElement : DispHTMLFormElement, HTMLFormElementEvents_Event
	{
	}
}
