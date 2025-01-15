using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BE4 RID: 3044
	[CoClass(typeof(HTMLLegendElementClass))]
	[Guid("3050F546-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLLegendElement : DispHTMLLegendElement, HTMLTextContainerEvents_Event
	{
	}
}
