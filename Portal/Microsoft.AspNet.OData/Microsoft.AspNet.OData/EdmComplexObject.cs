using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200003B RID: 59
	[NonValidatingParameterBinding]
	public class EdmComplexObject : EdmStructuredObject, IEdmComplexObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000074E2 File Offset: 0x000056E2
		public EdmComplexObject(IEdmComplexType edmType)
			: this(edmType, false)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000074EC File Offset: 0x000056EC
		public EdmComplexObject(IEdmComplexTypeReference edmType)
			: this(edmType.ComplexDefinition(), edmType.IsNullable)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007500 File Offset: 0x00005700
		public EdmComplexObject(IEdmComplexType edmType, bool isNullable)
			: base(edmType, isNullable)
		{
		}
	}
}
