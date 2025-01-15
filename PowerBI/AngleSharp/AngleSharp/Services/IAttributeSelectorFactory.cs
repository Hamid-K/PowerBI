using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services
{
	// Token: 0x02000024 RID: 36
	public interface IAttributeSelectorFactory
	{
		// Token: 0x0600011A RID: 282
		ISelector Create(string combinator, string name, string value, string prefix, bool insensitive);
	}
}
