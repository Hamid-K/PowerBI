using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1B RID: 2843
	[Guid("3050F528-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLNoShowElementClass))]
	[ComImport]
	public interface HTMLNoShowElement : DispHTMLNoShowElement, HTMLElementEvents_Event
	{
	}
}
