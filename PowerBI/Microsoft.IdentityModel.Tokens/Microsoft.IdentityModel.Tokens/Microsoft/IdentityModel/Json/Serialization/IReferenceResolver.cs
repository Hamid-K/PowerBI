using System;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	internal interface IReferenceResolver
	{
		// Token: 0x06000686 RID: 1670
		object ResolveReference(object context, string reference);

		// Token: 0x06000687 RID: 1671
		string GetReference(object context, object value);

		// Token: 0x06000688 RID: 1672
		bool IsReferenced(object context, object value);

		// Token: 0x06000689 RID: 1673
		void AddReference(object context, string reference, object value);
	}
}
