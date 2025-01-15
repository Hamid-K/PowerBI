using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000202 RID: 514
	public abstract class EdmProperty : EdmNamedElement, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00022445 File Offset: 0x00020645
		protected EdmProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.declaringType = declaringType;
			this.type = type;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00022474 File Offset: 0x00020674
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0002247C File Offset: 0x0002067C
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C1F RID: 3103
		public abstract EdmPropertyKind PropertyKind { get; }

		// Token: 0x04000588 RID: 1416
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000589 RID: 1417
		private readonly IEdmTypeReference type;
	}
}
