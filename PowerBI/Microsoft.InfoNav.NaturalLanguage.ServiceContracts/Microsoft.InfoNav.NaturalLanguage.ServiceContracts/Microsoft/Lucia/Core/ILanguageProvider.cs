using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000098 RID: 152
	public interface ILanguageProvider
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002A4 RID: 676
		LanguageIdentifier Language { get; }

		// Token: 0x060002A5 RID: 677
		LanguageServiceSpecifications GetServiceSpecifications();
	}
}
