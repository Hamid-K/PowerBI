using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000196 RID: 406
	public sealed class OperationImportSegment : ODataPathSegment
	{
		// Token: 0x060013A9 RID: 5033 RVA: 0x0003A20C File Offset: 0x0003840C
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

		// Token: 0x060013AA RID: 5034 RVA: 0x0003A2D8 File Offset: 0x000384D8
		public OperationImportSegment(IEdmOperationImport operationImport, IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
			: this(operationImport, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : parameters.ToList<OperationSegmentParameter>());
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0003A300 File Offset: 0x00038500
		public OperationImportSegment(IEnumerable<IEdmOperationImport> operationImports, IEdmEntitySetBase entitySet)
			: this()
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<IEdmOperationImport>>(operationImports, "operationImports");
			this.operationImports = new ReadOnlyCollection<IEdmOperationImport>(operationImports.ToList<IEdmOperationImport>());
			ExceptionUtils.CheckArgumentCollectionNotNullOrEmpty<IEdmOperationImport>(this.operationImports, "operations");
			base.Identifier = this.operationImports.First<IEdmOperationImport>().Name;
			IEdmType typeSoFar = ((this.operationImports.First<IEdmOperationImport>().Operation.ReturnType != null) ? this.operationImports.First<IEdmOperationImport>().Operation.ReturnType.Definition : null);
			if (typeSoFar == null)
			{
				if (this.operationImports.Any((IEdmOperationImport operation) => operation.Operation.ReturnType != null))
				{
					typeSoFar = OperationImportSegment.UnknownSentinel;
				}
			}
			else if (this.operationImports.Any((IEdmOperationImport operationImport) => !typeSoFar.IsEquivalentTo(operationImport.Operation.ReturnType.Definition)))
			{
				typeSoFar = OperationImportSegment.UnknownSentinel;
			}
			this.computedReturnEdmType = typeSoFar;
			this.entitySet = entitySet;
			this.EnsureTypeAndSetAreCompatable();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0003A414 File Offset: 0x00038614
		public OperationImportSegment(IEnumerable<IEdmOperationImport> operationImports, IEdmEntitySetBase entitySet, IEnumerable<OperationSegmentParameter> parameters)
			: this(operationImports, entitySet)
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>((parameters == null) ? new List<OperationSegmentParameter>() : parameters.ToList<OperationSegmentParameter>());
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003A439 File Offset: 0x00038639
		private OperationImportSegment()
		{
			this.parameters = new ReadOnlyCollection<OperationSegmentParameter>(new List<OperationSegmentParameter>());
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0003A451 File Offset: 0x00038651
		public IEnumerable<IEdmOperationImport> OperationImports
		{
			get
			{
				return this.operationImports.AsEnumerable<IEdmOperationImport>();
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003A45E File Offset: 0x0003865E
		public IEnumerable<OperationSegmentParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0003A466 File Offset: 0x00038666
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0003A486 File Offset: 0x00038686
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0003A48E File Offset: 0x0003868E
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0003A4A3 File Offset: 0x000386A3
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0003A4B8 File Offset: 0x000386B8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OperationImportSegment operationImportSegment = other as OperationImportSegment;
			return operationImportSegment != null && operationImportSegment.OperationImports.SequenceEqual(this.OperationImports) && operationImportSegment.EntitySet == this.entitySet;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0003A500 File Offset: 0x00038700
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

		// Token: 0x040008B4 RID: 2228
		private static readonly IEdmType UnknownSentinel = new EdmEnumType("Sentinel", "UndeterminableTypeMarker");

		// Token: 0x040008B5 RID: 2229
		private readonly ReadOnlyCollection<IEdmOperationImport> operationImports;

		// Token: 0x040008B6 RID: 2230
		private readonly ReadOnlyCollection<OperationSegmentParameter> parameters;

		// Token: 0x040008B7 RID: 2231
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x040008B8 RID: 2232
		private readonly IEdmType computedReturnEdmType;
	}
}
