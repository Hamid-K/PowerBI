using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000195 RID: 405
	internal abstract class CsdlSemanticsOperationImport : CsdlSemanticsElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0001E334 File Offset: 0x0001C534
		protected CsdlSemanticsOperationImport(CsdlSemanticsEntityContainer container, CsdlOperationImport operationImport, IEdmOperation operation)
			: base(operationImport)
		{
			this.container = container;
			this.operationImport = operationImport;
			this.Operation = operation;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001E35D File Offset: 0x0001C55D
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0001E365 File Offset: 0x0001C565
		public IEdmOperation Operation { get; private set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001E36E File Offset: 0x0001C56E
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Context.Model;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001E380 File Offset: 0x0001C580
		public override CsdlElement Element
		{
			get
			{
				return this.operationImport;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001E388 File Offset: 0x0001C588
		public string Name
		{
			get
			{
				return this.operationImport.Name;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001E395 File Offset: 0x0001C595
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001E39D File Offset: 0x0001C59D
		public IEdmExpression EntitySet
		{
			get
			{
				return this.entitySetCache.GetValue(this, CsdlSemanticsOperationImport.ComputeEntitySetFunc, null);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B22 RID: 2850
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001E3B1 File Offset: 0x0001C5B1
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001E3CA File Offset: 0x0001C5CA
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

		// Token: 0x0400069A RID: 1690
		private readonly CsdlOperationImport operationImport;

		// Token: 0x0400069B RID: 1691
		private readonly CsdlSemanticsEntityContainer container;

		// Token: 0x0400069C RID: 1692
		private readonly Cache<CsdlSemanticsOperationImport, IEdmExpression> entitySetCache = new Cache<CsdlSemanticsOperationImport, IEdmExpression>();

		// Token: 0x0400069D RID: 1693
		private static readonly Func<CsdlSemanticsOperationImport, IEdmExpression> ComputeEntitySetFunc = (CsdlSemanticsOperationImport me) => me.ComputeEntitySet();

		// Token: 0x020002DB RID: 731
		private sealed class OperationImportPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06001102 RID: 4354 RVA: 0x0001206F File Offset: 0x0001026F
			internal OperationImportPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06001103 RID: 4355 RVA: 0x0002DB69 File Offset: 0x0002BD69
			// (set) Token: 0x06001104 RID: 4356 RVA: 0x0002DB71 File Offset: 0x0002BD71
			public EdmLocation Location { get; set; }
		}
	}
}
