using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BB RID: 187
	public class EdmOptionalParameter : EdmOperationParameter, IEdmOptionalParameter, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0000B4C2 File Offset: 0x000096C2
		public EdmOptionalParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type)
			: this(declaringOperation, name, type, null)
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B4CE File Offset: 0x000096CE
		public EdmOptionalParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type, string defaultValue)
			: base(declaringOperation, name, type)
		{
			this.DefaultValueString = defaultValue;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000B4E1 File Offset: 0x000096E1
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000B4E9 File Offset: 0x000096E9
		public string DefaultValueString { get; private set; }
	}
}
