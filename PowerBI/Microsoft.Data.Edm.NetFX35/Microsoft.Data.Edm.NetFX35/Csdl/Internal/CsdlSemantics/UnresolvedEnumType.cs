using System;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000032 RID: 50
	internal class UnresolvedEnumType : BadEnumType, IUnresolvedElement
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00002F0C File Offset: 0x0000110C
		public UnresolvedEnumType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumType, Strings.Bad_UnresolvedEnumType(qualifiedName))
			})
		{
		}
	}
}
