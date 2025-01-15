using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009DF RID: 2527
	[Guid("3050F508-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLTableCaptionClass))]
	[ComImport]
	public interface HTMLTableCaption : DispHTMLTableCaption, HTMLTextContainerEvents_Event
	{
	}
}
