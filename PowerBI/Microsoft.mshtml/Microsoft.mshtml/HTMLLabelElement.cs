using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004A0 RID: 1184
	[CoClass(typeof(HTMLLabelElementClass))]
	[Guid("3050F522-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLLabelElement : DispHTMLLabelElement, HTMLLabelEvents_Event
	{
	}
}
