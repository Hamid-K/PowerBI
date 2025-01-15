using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000023 RID: 35
	internal class AmbiguousOperationImportBinding : AmbiguousBinding<IEdmOperationImport>, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00008E6F File Offset: 0x0000706F
		public AmbiguousOperationImportBinding(IEdmOperationImport first, IEdmOperationImport second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008E80 File Offset: 0x00007080
		public IEdmOperation Operation
		{
			get
			{
				return this.first.Operation;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008E8D File Offset: 0x0000708D
		public IEdmEntityContainer Container
		{
			get
			{
				return this.first.Container;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008E9A File Offset: 0x0000709A
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return this.first.ContainerElementKind;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmExpression EntitySet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400003C RID: 60
		private readonly IEdmOperationImport first;
	}
}
