using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000035 RID: 53
	public interface ICultureService
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000128 RID: 296
		ICulture DefaultCulture { get; }

		// Token: 0x06000129 RID: 297
		ICulture GetCulture(string name);
	}
}
