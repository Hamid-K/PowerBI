using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000022 RID: 34
	public interface IReferenceable
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600018F RID: 399
		int ID { get; }

		// Token: 0x06000190 RID: 400
		ObjectType GetObjectType();
	}
}
