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
	// Token: 0x02000251 RID: 593
	public sealed class OperationImportSegment : ODataPathSegment
	{
		// Token: 0x06001506 RID: 5382 RVA: 0x0004A750 File Offset: 0x00048950
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		public OperationImportSegment(IEdmOperationImport operationImport, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmOperationImport>(operationImport, "operationImport");
			this.operationImports = new ReadOnlyCollection<IEdmOperationImport>(new IEdmOperationImport[] { operationImport });
			base.Identifier = operationImport.Name;
			this.entitySet = entitySet;
			this.computedReturnEdmType = ((operationImport.Operation.ReturnType != null) ? operationImport.Operation.ReturnType.Definition : null);
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004A800 File Offset: 0x00048A00
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x06001508 RID: 5384 RVA: 0x0004A911 File Offset: 0x00048B11
		public OperationImportSegment(IEnumerable<IEdmOperationImport> operationImports, IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
			: this(operationImports, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : Enumerable.ToList<OperationSegmentParameter>(parameters));
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004A936 File Offset: 0x00048B36
		private OperationImportSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0004A94E File Offset: 0x00048B4E
		public IEnumerable<IEdmOperationImport> OperationImports
		{
			get
			{
				return Enumerable.AsEnumerable<IEdmOperationImport>(this.operationImports);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0004A95B File Offset: 0x00048B5B
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004A963 File Offset: 0x00048B63
		public override IEdmType EdmType
		{
			get
			{
				if (object.ReferenceEquals(this.computedReturnEdmType, OperationImportSegment.UnknownSentinel))
				{
					throw new ODataException(Strings.OperationSegment_ReturnTypeForMultipleOverloads);
				}
				return this.computedReturnEdmType;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0004A988 File Offset: 0x00048B88
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004A990 File Offset: 0x00048B90
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004A9A4 File Offset: 0x00048BA4
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004A9B8 File Offset: 0x00048BB8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationImportSegment operationImportSegment = other as OperationImportSegment;
			return operationImportSegment != null && Enumerable.SequenceEqual<IEdmOperationImport>(operationImportSegment.OperationImports, this.OperationImports) && operationImportSegment.EntitySet == this.entitySet;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0004AA00 File Offset: 0x00048C00
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x040008C3 RID: 2243
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x040008C4 RID: 2244
		private readonly ReadOnlyCollection<IEdmOperationImport> operationImports;

		// Token: 0x040008C5 RID: 2245
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x040008C6 RID: 2246
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x040008C7 RID: 2247
		private readonly IEdmType computedReturnEdmType;
	}
}
