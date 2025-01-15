using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000040 RID: 64
	internal enum WriteState
	{
		// Token: 0x04000145 RID: 325
		Error,
		// Token: 0x04000146 RID: 326
		Closed,
		// Token: 0x04000147 RID: 327
		Object,
		// Token: 0x04000148 RID: 328
		Array,
		// Token: 0x04000149 RID: 329
		Constructor,
		// Token: 0x0400014A RID: 330
		Property,
		// Token: 0x0400014B RID: 331
		Start
	}
}
