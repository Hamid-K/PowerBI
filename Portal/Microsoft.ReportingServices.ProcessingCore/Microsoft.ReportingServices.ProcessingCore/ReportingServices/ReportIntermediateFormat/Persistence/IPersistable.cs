using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053D RID: 1341
	public interface IPersistable
	{
		// Token: 0x0600496F RID: 18799
		void Serialize(IntermediateFormatWriter writer);

		// Token: 0x06004970 RID: 18800
		void Deserialize(IntermediateFormatReader reader);

		// Token: 0x06004971 RID: 18801
		void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x06004972 RID: 18802
		ObjectType GetObjectType();
	}
}
