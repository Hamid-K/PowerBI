using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014A RID: 330
	public sealed class OperationImportSegment : ODataPathSegment
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x0002A3F0 File Offset: 0x000285F0
		public OperationImportSegment(IEdmOperationImport operationImport, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmOperationImport>(operationImport, "operationImport");
			this.operationImports = new ReadOnlyCollection<IEdmOperationImport>(new IEdmOperationImport[] { operationImport });
			base.Identifier = operationImport.Name;
			this.entitySet = entitySet;
			this.computedReturnEdmType = ((operationImport.Operation.ReturnType != null) ? operationImport.Operation.ReturnType.Definition : null);
			this.EnsureTypeAndSetAreCompatable();
			if (this.computedReturnEdmType != null)
			{
				base.TargetEdmNavigationSource = entitySet;
				base.TargetEdmType = this.computedReturnEdmType;
				base.TargetKind = base.TargetEdmType.GetTargetKindFromType();
				base.SingleResult = this.computedReturnEdmType.TypeKind != EdmTypeKind.Collection;
				return;
			}
			base.TargetEdmNavigationSource = null;
			base.TargetEdmType = null;
			base.TargetKind = RequestTargetKind.VoidOperation;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002A4BC File Offset: 0x000286BC
		public OperationImportSegment(IEdmOperationImport operationImport, IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
			: this(operationImport, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002A4E4 File Offset: 0x000286E4
		public OperationImportSegment(IEnumerable<IEdmOperationImport> operationImports, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<IEdmOperationImport>>(operationImports, "operations");
			this.operationImports = new ReadOnlyCollection<IEdmOperationImport>(Enumerable.ToList<IEdmOperationImport>(operationImports));
			ExceptionUtils.CheckArgumentCollectionNotNullOrEmpty<IEdmOperationImport>(this.operationImports, "operations");
			base.Identifier = Enumerable.First<IEdmOperationImport>(this.operationImports).Name;
			IEdmType typeSoFar = ((Enumerable.First<IEdmOperationImport>(this.operationImports).Operation.ReturnType != null) ? Enumerable.First<IEdmOperationImport>(this.operationImports).Operation.ReturnType.Definition : null);
			if (typeSoFar == null)
			{
				if (Enumerable.Any<IEdmOperationImport>(this.operationImports, (IEdmOperationImport operation) => operation.Operation.ReturnType != null))
				{
					typeSoFar = OperationImportSegment.UnknownSentinel;
				}
			}
			else if (Enumerable.Any<IEdmOperationImport>(this.operationImports, (IEdmOperationImport operationImport) => !typeSoFar.IsEquivalentTo(operationImport.Operation.ReturnType.Definition)))
			{
				typeSoFar = OperationImportSegment.UnknownSentinel;
			}
			this.computedReturnEdmType = typeSoFar;
			this.entitySet = entitySet;
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002A5F8 File Offset: 0x000287F8
		public OperationImportSegment(IEnumerable<IEdmOperationImport> operationImports, IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
			: this(operationImports, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002A61D File Offset: 0x0002881D
		private OperationImportSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0002A635 File Offset: 0x00028835
		public IEnumerable<IEdmOperationImport> OperationImports
		{
			get
			{
				return Enumerable.AsEnumerable<IEdmOperationImport>(this.operationImports);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0002A642 File Offset: 0x00028842
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0002A64A File Offset: 0x0002884A
		public override IEdmType EdmType
		{
			get
			{
				if (this.computedReturnEdmType == OperationImportSegment.UnknownSentinel)
				{
					throw new ODataException(Strings.OperationSegment_ReturnTypeForMultipleOverloads);
				}
				return this.computedReturnEdmType;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0002A66A File Offset: 0x0002886A
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002A672 File Offset: 0x00028872
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002A687 File Offset: 0x00028887
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002A69C File Offset: 0x0002889C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationImportSegment operationImportSegment = other as OperationImportSegment;
			return operationImportSegment != null && Enumerable.SequenceEqual<IEdmOperationImport>(operationImportSegment.OperationImports, this.OperationImports) && operationImportSegment.EntitySet == this.entitySet;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002A6E4 File Offset: 0x000288E4
		private void EnsureTypeAndSetAreCompatable()
		{
			if (this.entitySet == null || this.computedReturnEdmType == OperationImportSegment.UnknownSentinel)
			{
				return;
			}
			if (this.computedReturnEdmType == null)
			{
				throw new ODataException(Strings.OperationSegment_CannotReturnNull);
			}
			IEdmType definition = this.computedReturnEdmType;
			IEdmCollectionType edmCollectionType = this.computedReturnEdmType as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				definition = edmCollectionType.ElementType.Definition;
			}
			if (!this.entitySet.EntityType().IsOrInheritsFrom(definition) && !definition.IsOrInheritsFrom(this.entitySet.EntityType()))
			{
				throw new ODataException(Strings.OperationSegment_CannotReturnNull);
			}
		}

		// Token: 0x04000779 RID: 1913
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x0400077A RID: 1914
		private readonly ReadOnlyCollection<IEdmOperationImport> operationImports;

		// Token: 0x0400077B RID: 1915
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x0400077C RID: 1916
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x0400077D RID: 1917
		private readonly IEdmType computedReturnEdmType;
	}
}
