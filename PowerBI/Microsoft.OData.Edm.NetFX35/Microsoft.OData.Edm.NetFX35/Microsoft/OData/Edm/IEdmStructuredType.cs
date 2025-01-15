using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000055 RID: 85
	public interface IEdmStructuredType : IEdmType, IEdmElement
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000130 RID: 304
		bool IsAbstract { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000131 RID: 305
		bool IsOpen { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000132 RID: 306
		IEdmStructuredType BaseType { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000133 RID: 307
		IEnumerable<IEdmProperty> DeclaredProperties { get; }

		// Token: 0x06000134 RID: 308
		IEdmProperty FindProperty(string name);
	}
}
