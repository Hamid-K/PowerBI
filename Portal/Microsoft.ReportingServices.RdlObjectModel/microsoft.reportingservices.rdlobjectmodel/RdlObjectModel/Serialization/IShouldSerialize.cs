using System;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D6 RID: 726
	public interface IShouldSerialize
	{
		// Token: 0x0600164F RID: 5711
		bool ShouldSerializeThis();

		// Token: 0x06001650 RID: 5712
		SerializationMethod ShouldSerializeProperty(string name);
	}
}
