using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services
{
	// Token: 0x02000033 RID: 51
	public interface IPseudoElementSelectorFactory
	{
		// Token: 0x06000136 RID: 310
		ISelector Create(string name);
	}
}
