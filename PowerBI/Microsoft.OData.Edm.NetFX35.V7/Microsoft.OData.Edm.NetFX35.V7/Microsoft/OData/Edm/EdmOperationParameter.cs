using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006C RID: 108
	public class EdmOperationParameter : EdmNamedElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000BE61 File Offset: 0x0000A061
		public EdmOperationParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(declaringOperation, "declaringFunction");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.Type = type;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000BE9C File Offset: 0x0000A09C
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		public IEdmOperation DeclaringOperation { get; private set; }
	}
}
