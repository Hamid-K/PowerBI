using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014B RID: 331
	public sealed class OperationSegment : ODataPathSegment
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002A784 File Offset: 0x00028984
		public OperationSegment(IEdmOperation operation, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmOperation>(operation, "operation");
			this.operations = new ReadOnlyCollection<IEdmOperation>(new IEdmOperation[] { operation });
			this.entitySet = entitySet;
			this.computedReturnEdmType = ((operation.ReturnType != null) ? operation.ReturnType.Definition : null);
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

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002A83A File Offset: 0x00028A3A
		public OperationSegment(IEdmOperation operation, IEnumerable<OperationSegmentParameter> parameters, IEdmEntitySetBase entitySet)
			: this(operation, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002A860 File Offset: 0x00028A60
		public OperationSegment(IEnumerable<IEdmOperation> operations, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<IEdmOperation>>(operations, "operations");
			this.operations = new ReadOnlyCollection<IEdmOperation>(Enumerable.ToList<IEdmOperation>(operations));
			ExceptionUtils.CheckArgumentCollectionNotNullOrEmpty<IEdmOperation>(this.operations, "operations");
			IEdmType typeSoFar = ((Enumerable.First<IEdmOperation>(this.operations).ReturnType != null) ? Enumerable.First<IEdmOperation>(this.operations).ReturnType.Definition : null);
			if (typeSoFar == null)
			{
				if (Enumerable.Any<IEdmOperation>(this.operations, (IEdmOperation operation) => operation.ReturnType != null))
				{
					typeSoFar = OperationSegment.UnknownSentinel;
				}
			}
			else if (Enumerable.Any<IEdmOperation>(this.operations, (IEdmOperation operationImport) => !typeSoFar.IsEquivalentTo(operationImport.ReturnType.Definition)))
			{
				typeSoFar = OperationSegment.UnknownSentinel;
			}
			this.computedReturnEdmType = typeSoFar;
			this.entitySet = entitySet;
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002A954 File Offset: 0x00028B54
		public OperationSegment(IEnumerable<IEdmOperation> operations, IEnumerable<OperationSegmentParameter> parameters, IEdmEntitySetBase entitySet)
			: this(operations, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002A979 File Offset: 0x00028B79
		private OperationSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0002A991 File Offset: 0x00028B91
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return Enumerable.AsEnumerable<IEdmOperation>(this.operations);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002A99E File Offset: 0x00028B9E
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0002A9A6 File Offset: 0x00028BA6
		public override IEdmType EdmType
		{
			get
			{
				if (this.computedReturnEdmType == OperationSegment.UnknownSentinel)
				{
					throw new ODataException(Strings.OperationSegment_ReturnTypeForMultipleOverloads);
				}
				return this.computedReturnEdmType;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002A9C6 File Offset: 0x00028BC6
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002A9CE File Offset: 0x00028BCE
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002A9E3 File Offset: 0x00028BE3
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002A9F8 File Offset: 0x00028BF8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationSegment operationSegment = other as OperationSegment;
			return operationSegment != null && Enumerable.SequenceEqual<IEdmOperation>(operationSegment.Operations, this.Operations) && operationSegment.EntitySet == this.entitySet;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002AA40 File Offset: 0x00028C40
		private void EnsureTypeAndSetAreCompatable()
		{
			if (this.entitySet == null || this.computedReturnEdmType == OperationSegment.UnknownSentinel)
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

		// Token: 0x0400077E RID: 1918
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x0400077F RID: 1919
		private readonly ReadOnlyCollection<IEdmOperation> operations;

		// Token: 0x04000780 RID: 1920
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x04000781 RID: 1921
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x04000782 RID: 1922
		private readonly IEdmType computedReturnEdmType;
	}
}
