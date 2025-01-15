using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B9 RID: 185
	public class EdmOperationParameter : EdmNamedElement, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0000B415 File Offset: 0x00009615
		public EdmOperationParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(declaringOperation, "declaringFunction");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.Type = type;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000B450 File Offset: 0x00009650
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0000B458 File Offset: 0x00009658
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B461 File Offset: 0x00009661
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000B469 File Offset: 0x00009669
		public IEdmOperation DeclaringOperation { get; private set; }
	}
}
