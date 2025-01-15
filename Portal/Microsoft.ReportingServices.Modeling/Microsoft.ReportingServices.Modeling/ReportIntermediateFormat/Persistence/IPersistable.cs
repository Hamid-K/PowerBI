using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000020 RID: 32
	public interface IPersistable
	{
		// Token: 0x0600018A RID: 394
		void Serialize(IntermediateFormatWriter writer);

		// Token: 0x0600018B RID: 395
		void Deserialize(IntermediateFormatReader reader);

		// Token: 0x0600018C RID: 396
		void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x0600018D RID: 397
		ObjectType GetObjectType();
	}
}
