using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000305 RID: 773
	[CoClass(typeof(HTMLImgClass))]
	[Guid("3050F51C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface HTMLImg : DispHTMLImg, HTMLImgEvents_Event
	{
	}
}
