using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000954 RID: 2388
	[CoClass(typeof(HTMLMapElementClass))]
	[Guid("3050F526-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLMapElement : DispHTMLMapElement, HTMLMapEvents_Event
	{
	}
}
