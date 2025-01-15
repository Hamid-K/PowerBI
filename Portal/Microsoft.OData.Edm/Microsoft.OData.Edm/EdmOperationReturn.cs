using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BA RID: 186
	internal class EdmOperationReturn : EdmElement, IEdmOperationReturn, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0000B472 File Offset: 0x00009672
		public EdmOperationReturn(IEdmOperation declaringOperation, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(declaringOperation, "declaringOperation");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.Type = type;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000B4A0 File Offset: 0x000096A0
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000B4A8 File Offset: 0x000096A8
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B4B1 File Offset: 0x000096B1
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000B4B9 File Offset: 0x000096B9
		public IEdmOperation DeclaringOperation { get; private set; }
	}
}
