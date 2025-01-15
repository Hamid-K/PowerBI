using System;

namespace AngleSharp.Dom
{
	// Token: 0x0200017C RID: 380
	public interface IBindable
	{
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06000DAD RID: 3501
		// (remove) Token: 0x06000DAE RID: 3502
		event Action<string> Changed;

		// Token: 0x06000DAF RID: 3503
		void Update(string value);
	}
}
