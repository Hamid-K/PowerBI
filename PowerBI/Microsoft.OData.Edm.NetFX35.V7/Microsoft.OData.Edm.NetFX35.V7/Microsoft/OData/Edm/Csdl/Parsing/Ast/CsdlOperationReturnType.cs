using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D9 RID: 473
	internal class CsdlOperationReturnType : CsdlElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x00023C46 File Offset: 0x00021E46
		public CsdlOperationReturnType(CsdlTypeReference returnType, CsdlLocation location)
			: base(location)
		{
			this.returnType = returnType;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00023C56 File Offset: 0x00021E56
		public CsdlTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x040006F1 RID: 1777
		private readonly CsdlTypeReference returnType;
	}
}
