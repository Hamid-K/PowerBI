using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053F RID: 1343
	public interface IReferenceable
	{
		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06004974 RID: 18804
		int ID { get; }

		// Token: 0x06004975 RID: 18805
		ObjectType GetObjectType();
	}
}
