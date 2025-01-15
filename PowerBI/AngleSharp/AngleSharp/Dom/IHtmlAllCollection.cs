using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000188 RID: 392
	[DomName("HTMLAllCollection")]
	public interface IHtmlAllCollection : IHtmlCollection<IElement>, IEnumerable<IElement>, IEnumerable
	{
	}
}
