using System;

namespace AngleSharp.Services
{
	// Token: 0x0200002F RID: 47
	public interface IIntegrityProvider
	{
		// Token: 0x06000132 RID: 306
		bool IsSatisfied(byte[] content, string integrity);
	}
}
