using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000019 RID: 25
	[NonValidatingParameterBinding]
	public class EdmDeltaComplexObject : EdmComplexObject
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004186 File Offset: 0x00002386
		public EdmDeltaComplexObject(IEdmComplexType edmType)
			: this(edmType, false)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004190 File Offset: 0x00002390
		public EdmDeltaComplexObject(IEdmComplexTypeReference edmType)
			: this(edmType.ComplexDefinition(), edmType.IsNullable)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041A4 File Offset: 0x000023A4
		public EdmDeltaComplexObject(IEdmComplexType edmType, bool isNullable)
			: base(edmType, isNullable)
		{
		}
	}
}
