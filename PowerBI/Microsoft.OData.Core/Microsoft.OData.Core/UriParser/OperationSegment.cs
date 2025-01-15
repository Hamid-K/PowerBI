using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000197 RID: 407
	public sealed class OperationSegment : ODataPathSegment
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x0003A5A0 File Offset: 0x000387A0
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

		// Token: 0x060013B8 RID: 5048 RVA: 0x0003A656 File Offset: 0x00038856
		public OperationSegment(IEdmOperation operation, IEnumerable<OperationSegmentParameter> parameters, IEdmEntitySetBase entitySet)
			: this(operation, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : parameters.ToList<OperationSegmentParameter>());
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0003A67C File Offset: 0x0003887C
		public OperationSegment(IEnumerable<IEdmOperation> operations, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<IEdmOperation>>(operations, "operations");
			this.operations = new ReadOnlyCollection<IEdmOperation>(operations.ToList<IEdmOperation>());
			ExceptionUtils.CheckArgumentCollectionNotNullOrEmpty<IEdmOperation>(this.operations, "operations");
			IEdmType typeSoFar = ((this.operations.First<IEdmOperation>().ReturnType != null) ? this.operations.First<IEdmOperation>().ReturnType.Definition : null);
			if (typeSoFar == null)
			{
				if (this.operations.Any((IEdmOperation operation) => operation.ReturnType != null))
				{
					typeSoFar = OperationSegment.UnknownSentinel;
				}
			}
			else if (this.operations.Any((IEdmOperation operationImport) => !typeSoFar.IsEquivalentTo(operationImport.ReturnType.Definition)))
			{
				typeSoFar = OperationSegment.UnknownSentinel;
			}
			this.computedReturnEdmType = typeSoFar;
			this.entitySet = entitySet;
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0003A770 File Offset: 0x00038970
		public OperationSegment(IEnumerable<IEdmOperation> operations, IEnumerable<OperationSegmentParameter> parameters, IEdmEntitySetBase entitySet)
			: this(operations, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : parameters.ToList<OperationSegmentParameter>());
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0003A795 File Offset: 0x00038995
		private OperationSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003A7AD File Offset: 0x000389AD
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return this.operations.AsEnumerable<IEdmOperation>();
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003A7BA File Offset: 0x000389BA
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003A7C2 File Offset: 0x000389C2
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0003A7E2 File Offset: 0x000389E2
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0003A7EA File Offset: 0x000389EA
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0003A7FF File Offset: 0x000389FF
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0003A814 File Offset: 0x00038A14
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationSegment operationSegment = other as OperationSegment;
			return operationSegment != null && operationSegment.Operations.SequenceEqual(this.Operations) && operationSegment.EntitySet == this.entitySet;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0003A85C File Offset: 0x00038A5C
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

		// Token: 0x040008B9 RID: 2233
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x040008BA RID: 2234
		private readonly ReadOnlyCollection<IEdmOperation> operations;

		// Token: 0x040008BB RID: 2235
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x040008BC RID: 2236
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x040008BD RID: 2237
		private readonly IEdmType computedReturnEdmType;
	}
}
