using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BE1 RID: 3041
	[Guid("3050F545-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLFieldSetElementClass))]
	[ComImport]
	public interface HTMLFieldSetElement : DispHTMLFieldSetElement, HTMLTextContainerEvents_Event
	{
	}
}
