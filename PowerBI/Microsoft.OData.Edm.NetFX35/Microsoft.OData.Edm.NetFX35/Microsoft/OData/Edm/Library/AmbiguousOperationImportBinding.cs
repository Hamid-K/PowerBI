using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200011C RID: 284
	internal class AmbiguousOperationImportBinding : AmbiguousBinding<IEdmOperationImport>, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0000DE8D File Offset: 0x0000C08D
		public AmbiguousOperationImportBinding(IEdmOperationImport first, IEdmOperationImport second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000DE9E File Offset: 0x0000C09E
		public IEdmOperation Operation
		{
			get
			{
				return this.first.Operation;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000DEAB File Offset: 0x0000C0AB
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000DEAE File Offset: 0x0000C0AE
		public IEdmEntityContainer Container
		{
			get
			{
				return this.first.Container;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return this.first.ContainerElementKind;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		public IEdmExpression EntitySet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400021D RID: 541
		private readonly IEdmOperationImport first;
	}
}
