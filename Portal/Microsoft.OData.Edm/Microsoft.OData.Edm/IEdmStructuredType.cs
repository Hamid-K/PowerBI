using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A6 RID: 166
	public interface IEdmStructuredType : IEdmType, IEdmElement
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003E0 RID: 992
		bool IsAbstract { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003E1 RID: 993
		bool IsOpen { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003E2 RID: 994
		IEdmStructuredType BaseType { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003E3 RID: 995
		IEnumerable<IEdmProperty> DeclaredProperties { get; }

		// Token: 0x060003E4 RID: 996
		IEdmProperty FindProperty(string name);
	}
}
