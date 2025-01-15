using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006D RID: 109
	public class EdmOptionalParameter : EdmOperationParameter, IEdmOptionalParameter, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000BEBE File Offset: 0x0000A0BE
		public EdmOptionalParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type)
			: this(declaringOperation, name, type, null)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000BECA File Offset: 0x0000A0CA
		public EdmOptionalParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type, string defaultValue)
			: base(declaringOperation, name, type)
		{
			this.DefaultValueString = defaultValue;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		public string DefaultValueString { get; private set; }
	}
}
