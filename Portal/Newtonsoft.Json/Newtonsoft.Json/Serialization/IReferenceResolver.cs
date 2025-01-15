using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x06000685 RID: 1669
		object ResolveReference(object context, string reference);

		// Token: 0x06000686 RID: 1670
		string GetReference(object context, object value);

		// Token: 0x06000687 RID: 1671
		bool IsReferenced(object context, object value);

		// Token: 0x06000688 RID: 1672
		void AddReference(object context, string reference, object value);
	}
}
