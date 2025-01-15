using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000064 RID: 100
	internal abstract class CsdlSemanticsOperationImport : CsdlSemanticsElement, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00004133 File Offset: 0x00002333
		protected CsdlSemanticsOperationImport(CsdlSemanticsEntityContainer container, CsdlOperationImport operationImport, IEdmOperation operation)
			: base(operationImport)
		{
			this.container = container;
			this.operationImport = operationImport;
			this.Operation = operation;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000415C File Offset: 0x0000235C
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00004164 File Offset: 0x00002364
		public IEdmOperation Operation { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000416D File Offset: 0x0000236D
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Context.Model;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000417F File Offset: 0x0000237F
		public override CsdlElement Element
		{
			get
			{
				return this.operationImport;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004187 File Offset: 0x00002387
		public string Name
		{
			get
			{
				return this.operationImport.Name;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004194 File Offset: 0x00002394
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000419C File Offset: 0x0000239C
		public IEdmExpression EntitySet
		{
			get
			{
				return this.entitySetCache.GetValue(this, CsdlSemanticsOperationImport.ComputeEntitySetFunc, null);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000188 RID: 392
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x06000189 RID: 393 RVA: 0x000041B0 File Offset: 0x000023B0
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000041CC File Offset: 0x000023CC
		private IEdmExpression ComputeEntitySet()
		{
			if (this.operationImport.EntitySet == null)
			{
				return null;
			}
			if (this.operationImport.EntitySet.IndexOf("/", 4) > -1)
			{
				return new CsdlSemanticsOperationImport.OperationImportPathExpression(this.operationImport.EntitySet)
				{
					Location = base.Location
				};
			}
			IEdmEntitySet edmEntitySet = this.container.FindEntitySetExtended(this.operationImport.EntitySet) ?? new UnresolvedEntitySet(this.operationImport.EntitySet, this.Container, base.Location);
			return new CsdlSemanticsOperationImport.OperationImportEntitySetReferenceExpression(edmEntitySet)
			{
				Location = base.Location
			};
		}

		// Token: 0x04000080 RID: 128
		private readonly CsdlOperationImport operationImport;

		// Token: 0x04000081 RID: 129
		private readonly CsdlSemanticsEntityContainer container;

		// Token: 0x04000082 RID: 130
		private readonly Cache<CsdlSemanticsOperationImport, IEdmExpression> entitySetCache = new Cache<CsdlSemanticsOperationImport, IEdmExpression>();

		// Token: 0x04000083 RID: 131
		private static readonly Func<CsdlSemanticsOperationImport, IEdmExpression> ComputeEntitySetFunc = (CsdlSemanticsOperationImport me) => me.ComputeEntitySet();

		// Token: 0x02000067 RID: 103
		private sealed class OperationImportEntitySetReferenceExpression : EdmEntitySetReferenceExpression, IEdmLocatable
		{
			// Token: 0x06000191 RID: 401 RVA: 0x000042C1 File Offset: 0x000024C1
			internal OperationImportEntitySetReferenceExpression(IEdmEntitySet referencedEntitySet)
				: base(referencedEntitySet)
			{
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000192 RID: 402 RVA: 0x000042CA File Offset: 0x000024CA
			// (set) Token: 0x06000193 RID: 403 RVA: 0x000042D2 File Offset: 0x000024D2
			public EdmLocation Location { get; set; }
		}

		// Token: 0x02000068 RID: 104
		private sealed class OperationImportPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06000194 RID: 404 RVA: 0x000042DB File Offset: 0x000024DB
			internal OperationImportPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000195 RID: 405 RVA: 0x000042E4 File Offset: 0x000024E4
			// (set) Token: 0x06000196 RID: 406 RVA: 0x000042EC File Offset: 0x000024EC
			public EdmLocation Location { get; set; }
		}
	}
}
