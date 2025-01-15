using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000184 RID: 388
	[DomName("DOMException")]
	public interface IDomException
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E0F RID: 3599
		[DomName("code")]
		int Code { get; }
	}
}
