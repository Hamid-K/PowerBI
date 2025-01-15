using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CFB RID: 3323
	[Guid("3050F589-98B5-11CF-BB82-00AA00BDCE0B")]
	[CoClass(typeof(HTMLPopupClass))]
	[ComImport]
	public interface HTMLPopup : DispHTMLPopup
	{
	}
}
