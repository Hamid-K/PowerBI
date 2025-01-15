using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BF RID: 191
	public interface IEdmStructuredType : IEdmType, IEdmElement
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B9 RID: 1209
		bool IsAbstract { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004BA RID: 1210
		bool IsOpen { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004BB RID: 1211
		IEdmStructuredType BaseType { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004BC RID: 1212
		IEnumerable<IEdmProperty> DeclaredProperties { get; }

		// Token: 0x060004BD RID: 1213
		IEdmProperty FindProperty(string name);
	}
}
