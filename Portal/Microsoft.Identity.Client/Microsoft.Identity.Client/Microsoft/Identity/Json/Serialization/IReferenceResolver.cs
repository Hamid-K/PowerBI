using System;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	internal interface IReferenceResolver
	{
		// Token: 0x0600067C RID: 1660
		object ResolveReference(object context, string reference);

		// Token: 0x0600067D RID: 1661
		string GetReference(object context, object value);

		// Token: 0x0600067E RID: 1662
		bool IsReferenced(object context, object value);

		// Token: 0x0600067F RID: 1663
		void AddReference(object context, string reference, object value);
	}
}
