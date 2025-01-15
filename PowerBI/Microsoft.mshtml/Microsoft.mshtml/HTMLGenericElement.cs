using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000139 RID: 313
	[Guid("3050F563-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLGenericElementClass))]
	[ComImport]
	public interface HTMLGenericElement : DispHTMLGenericElement, HTMLElementEvents_Event
	{
	}
}
