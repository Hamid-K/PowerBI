using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BE8 RID: 3048
	[CoClass(typeof(HTMLSpanFlowClass))]
	[Guid("3050F544-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLSpanFlow : DispHTMLSpanFlow, HTMLTextContainerEvents_Event
	{
	}
}
