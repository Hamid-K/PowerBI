using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006B RID: 107
	public abstract class EdmOperationImport : EdmNamedElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0000BDF6 File Offset: 0x00009FF6
		protected EdmOperationImport(IEdmEntityContainer container, IEdmOperation operation, string name, IEdmExpression entitySet)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			EdmUtil.CheckArgumentNull<IEdmOperation>(operation, this.OperationArgumentNullParameterName());
			this.Container = container;
			this.Operation = operation;
			this.EntitySet = entitySet;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000BE2E File Offset: 0x0000A02E
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000BE36 File Offset: 0x0000A036
		public IEdmOperation Operation { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000BE3F File Offset: 0x0000A03F
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000BE47 File Offset: 0x0000A047
		public IEdmExpression EntitySet { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003E6 RID: 998
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000BE50 File Offset: 0x0000A050
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000BE58 File Offset: 0x0000A058
		public IEdmEntityContainer Container { get; private set; }

		// Token: 0x060003E9 RID: 1001
		protected abstract string OperationArgumentNullParameterName();
	}
}
