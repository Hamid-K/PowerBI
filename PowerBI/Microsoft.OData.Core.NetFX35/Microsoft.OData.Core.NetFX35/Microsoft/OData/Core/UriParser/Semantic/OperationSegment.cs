using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000252 RID: 594
	public sealed class OperationSegment : ODataPathSegment
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x0004AAA0 File Offset: 0x00048CA0
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		public OperationSegment(IEdmOperation operation, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmOperation>(operation, "operation");
			this.operations = new ReadOnlyCollection<IEdmOperation>(new IEdmOperation[] { operation });
			this.entitySet = entitySet;
			this.computedReturnEdmType = ((operation.ReturnType != null) ? operation.ReturnType.Definition : null);
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004AB30 File Offset: 0x00048D30
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x06001516 RID: 5398 RVA: 0x0004AC21 File Offset: 0x00048E21
		public OperationSegment(IEnumerable<IEdmOperation> operations, IEnumerable<OperationSegmentParameter> parameters, IEdmEntitySetBase entitySet)
			: this(operations, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004AC46 File Offset: 0x00048E46
		private OperationSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004AC5E File Offset: 0x00048E5E
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return Enumerable.AsEnumerable<IEdmOperation>(this.operations);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0004AC6B File Offset: 0x00048E6B
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004AC73 File Offset: 0x00048E73
		public override IEdmType EdmType
		{
			get
			{
				if (object.ReferenceEquals(this.computedReturnEdmType, OperationSegment.UnknownSentinel))
				{
					throw new ODataException(Strings.OperationSegment_ReturnTypeForMultipleOverloads);
				}
				return this.computedReturnEdmType;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0004AC98 File Offset: 0x00048E98
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004ACA0 File Offset: 0x00048EA0
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004ACB4 File Offset: 0x00048EB4
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0004ACC8 File Offset: 0x00048EC8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationSegment operationSegment = other as OperationSegment;
			return operationSegment != null && Enumerable.SequenceEqual<IEdmOperation>(operationSegment.Operations, this.Operations) && operationSegment.EntitySet == this.entitySet;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004AD10 File Offset: 0x00048F10
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x040008C9 RID: 2249
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x040008CA RID: 2250
		private readonly ReadOnlyCollection<IEdmOperation> operations;

		// Token: 0x040008CB RID: 2251
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x040008CC RID: 2252
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x040008CD RID: 2253
		private readonly IEdmType computedReturnEdmType;
	}
}
