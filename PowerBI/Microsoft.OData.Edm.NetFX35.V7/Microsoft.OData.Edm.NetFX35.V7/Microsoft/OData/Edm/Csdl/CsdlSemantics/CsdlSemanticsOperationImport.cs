using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000185 RID: 389
	internal abstract class CsdlSemanticsOperationImport : CsdlSemanticsElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x0001C018 File Offset: 0x0001A218
		protected CsdlSemanticsOperationImport(CsdlSemanticsEntityContainer container, CsdlOperationImport operationImport, IEdmOperation operation)
			: base(operationImport)
		{
			this.container = container;
			this.operationImport = operationImport;
			this.Operation = operation;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001C041 File Offset: 0x0001A241
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0001C049 File Offset: 0x0001A249
		public IEdmOperation Operation { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001C052 File Offset: 0x0001A252
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Context.Model;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001C064 File Offset: 0x0001A264
		public override CsdlElement Element
		{
			get
			{
				return this.operationImport;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001C06C File Offset: 0x0001A26C
		public string Name
		{
			get
			{
				return this.operationImport.Name;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001C079 File Offset: 0x0001A279
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001C081 File Offset: 0x0001A281
		public IEdmExpression EntitySet
		{
			get
			{
				return this.entitySetCache.GetValue(this, CsdlSemanticsOperationImport.ComputeEntitySetFunc, null);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A59 RID: 2649
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001C095 File Offset: 0x0001A295
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001C0AE File Offset: 0x0001A2AE
		private IEdmExpression ComputeEntitySet()
		{
			if (this.operationImport.EntitySet != null)
			{
				return new CsdlSemanticsOperationImport.OperationImportPathExpression(this.operationImport.EntitySet)
				{
					Location = base.Location
				};
			}
			return null;
		}

		// Token: 0x04000619 RID: 1561
		private readonly CsdlOperationImport operationImport;

		// Token: 0x0400061A RID: 1562
		private readonly CsdlSemanticsEntityContainer container;

		// Token: 0x0400061B RID: 1563
		private readonly Cache<CsdlSemanticsOperationImport, IEdmExpression> entitySetCache = new Cache<CsdlSemanticsOperationImport, IEdmExpression>();

		// Token: 0x0400061C RID: 1564
		private static readonly Func<CsdlSemanticsOperationImport, IEdmExpression> ComputeEntitySetFunc = (CsdlSemanticsOperationImport me) => me.ComputeEntitySet();

		// Token: 0x020002C0 RID: 704
		private sealed class OperationImportPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06001016 RID: 4118 RVA: 0x00013B8B File Offset: 0x00011D8B
			internal OperationImportPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x06001017 RID: 4119 RVA: 0x0002B195 File Offset: 0x00029395
			// (set) Token: 0x06001018 RID: 4120 RVA: 0x0002B19D File Offset: 0x0002939D
			public EdmLocation Location { get; set; }
		}
	}
}
