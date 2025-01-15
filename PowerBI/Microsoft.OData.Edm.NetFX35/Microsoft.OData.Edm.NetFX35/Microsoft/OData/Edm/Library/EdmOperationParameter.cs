using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000200 RID: 512
	public class EdmOperationParameter : EdmNamedElement, IEdmOperationParameter, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x00022104 File Offset: 0x00020304
		public EdmOperationParameter(IEdmOperation declaringOperation, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(declaringOperation, "declaringFunction");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.Type = type;
			this.DeclaringOperation = declaringOperation;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002213F File Offset: 0x0002033F
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00022147 File Offset: 0x00020347
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00022150 File Offset: 0x00020350
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00022158 File Offset: 0x00020358
		public IEdmOperation DeclaringOperation { get; private set; }
	}
}
