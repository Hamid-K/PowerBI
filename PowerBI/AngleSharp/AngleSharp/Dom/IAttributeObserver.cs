using System;

namespace AngleSharp.Dom
{
	// Token: 0x02000156 RID: 342
	public interface IAttributeObserver
	{
		// Token: 0x06000BBD RID: 3005
		void NotifyChange(IElement host, string name, string value);
	}
}
