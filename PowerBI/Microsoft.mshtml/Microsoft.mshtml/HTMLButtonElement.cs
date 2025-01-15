using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020006DD RID: 1757
	[CoClass(typeof(HTMLButtonElementClass))]
	[Guid("3050F51F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLButtonElement : DispHTMLButtonElement, HTMLButtonElementEvents_Event
	{
	}
}
