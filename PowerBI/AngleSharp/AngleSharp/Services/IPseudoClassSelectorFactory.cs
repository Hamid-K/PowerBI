using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services
{
	// Token: 0x02000032 RID: 50
	public interface IPseudoClassSelectorFactory
	{
		// Token: 0x06000135 RID: 309
		ISelector Create(string name);
	}
}
