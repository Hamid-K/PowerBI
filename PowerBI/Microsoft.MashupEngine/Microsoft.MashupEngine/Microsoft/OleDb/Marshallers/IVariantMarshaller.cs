using System;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD2 RID: 8146
	public interface IVariantMarshaller : IMarshaller<object>, IMarshaller
	{
		// Token: 0x0600C708 RID: 50952
		bool CanHandle(VARTYPE variantType);
	}
}
