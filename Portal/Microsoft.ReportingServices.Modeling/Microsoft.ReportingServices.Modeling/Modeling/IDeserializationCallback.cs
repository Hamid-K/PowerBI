using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200005B RID: 91
	internal interface IDeserializationCallback
	{
		// Token: 0x060003A9 RID: 937
		bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx);
	}
}
