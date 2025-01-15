using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200004C RID: 76
	public interface IDatabaseSpecificationProxy
	{
		// Token: 0x060001D4 RID: 468
		IDatabaseSpecification GetSpecification();

		// Token: 0x060001D5 RID: 469
		IDatabaseSpecification GetEnabledSpecification();

		// Token: 0x060001D6 RID: 470
		IDatabaseSpecification GetSpecificationPreferSecondary();

		// Token: 0x060001D7 RID: 471
		IDatabaseSpecification GetEnabledSpecificationPreferSecondary();

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D8 RID: 472
		string Key { get; }
	}
}
