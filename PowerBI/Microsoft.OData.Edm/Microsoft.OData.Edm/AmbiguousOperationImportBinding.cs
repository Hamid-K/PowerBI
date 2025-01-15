using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004D RID: 77
	internal class AmbiguousOperationImportBinding : AmbiguousBinding<IEdmOperationImport>, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public AmbiguousOperationImportBinding(IEdmOperationImport first, IEdmOperationImport second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004BF1 File Offset: 0x00002DF1
		public IEdmOperation Operation
		{
			get
			{
				return this.first.Operation;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004BFE File Offset: 0x00002DFE
		public IEdmEntityContainer Container
		{
			get
			{
				return this.first.Container;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004C0B File Offset: 0x00002E0B
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return this.first.ContainerElementKind;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmExpression EntitySet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000092 RID: 146
		private readonly IEdmOperationImport first;
	}
}
