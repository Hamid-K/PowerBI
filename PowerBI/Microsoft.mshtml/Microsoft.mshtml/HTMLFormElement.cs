using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200026B RID: 619
	[CoClass(typeof(HTMLFormElementClass))]
	[Guid("3050F510-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLFormElement : DispHTMLFormElement, HTMLFormElementEvents_Event
	{
	}
}
