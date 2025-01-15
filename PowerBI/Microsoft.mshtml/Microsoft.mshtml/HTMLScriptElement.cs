using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B17 RID: 2839
	[CoClass(typeof(HTMLScriptElementClass))]
	[Guid("3050F530-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLScriptElement : DispHTMLScriptElement, HTMLScriptEvents_Event
	{
	}
}
