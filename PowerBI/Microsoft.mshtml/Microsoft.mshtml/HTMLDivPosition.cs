using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BDE RID: 3038
	[Guid("3050F50F-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLDivPositionClass))]
	[ComImport]
	public interface HTMLDivPosition : DispHTMLDivPosition, HTMLTextContainerEvents_Event
	{
	}
}
