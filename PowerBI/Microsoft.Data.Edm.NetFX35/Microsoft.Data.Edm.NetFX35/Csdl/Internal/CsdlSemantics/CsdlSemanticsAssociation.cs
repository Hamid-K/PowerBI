using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000162 RID: 354
	internal class CsdlSemanticsAssociation : CsdlSemanticsElement, IEdmAssociation, IEdmNamedElement, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00013D23 File Offset: 0x00011F23
		public CsdlSemanticsAssociation(CsdlSemanticsSchema context, CsdlAssociation association)
			: base(association)
		{
			this.association = association;
			this.context = context;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00013D5B File Offset: 0x00011F5B
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00013D68 File Offset: 0x00011F68
		public string Name
		{
			get
			{
				return this.association.Name;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00013D75 File Offset: 0x00011F75
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00013D82 File Offset: 0x00011F82
		public override CsdlElement Element
		{
			get
			{
				return this.association;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00013D8A File Offset: 0x00011F8A
		public IEdmAssociationEnd End1
		{
			get
			{
				return this.endsCache.GetValue(this, CsdlSemanticsAssociation.ComputeEndsFunc, null).Item1;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public IEdmAssociationEnd End2
		{
			get
			{
				return this.endsCache.GetValue(this, CsdlSemanticsAssociation.ComputeEndsFunc, null).Item2;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00013DBC File Offset: 0x00011FBC
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsAssociation.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public CsdlSemanticsReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraintCache.GetValue(this, CsdlSemanticsAssociation.ComputeReferentialConstraintFunc, null);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00013DE4 File Offset: 0x00011FE4
		private TupleInternal<IEdmAssociationEnd, IEdmAssociationEnd> ComputeEnds()
		{
			IEdmAssociationEnd edmAssociationEnd2;
			if (this.association.End1 == null)
			{
				IEdmAssociationEnd edmAssociationEnd = new BadAssociationEnd(this, "End1", new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidAssociation, Strings.CsdlParser_InvalidAssociationIncorrectNumberOfEnds(this.Namespace + "." + this.Name))
				});
				edmAssociationEnd2 = edmAssociationEnd;
			}
			else
			{
				edmAssociationEnd2 = new CsdlSemanticsAssociationEnd(this.context, this, this.association.End1);
			}
			IEdmAssociationEnd edmAssociationEnd4;
			if (this.association.End2 == null)
			{
				IEdmAssociationEnd edmAssociationEnd3 = new BadAssociationEnd(this, "End2", new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidAssociation, Strings.CsdlParser_InvalidAssociationIncorrectNumberOfEnds(this.Namespace + "." + this.Name))
				});
				edmAssociationEnd4 = edmAssociationEnd3;
			}
			else
			{
				edmAssociationEnd4 = new CsdlSemanticsAssociationEnd(this.context, this, this.association.End2);
			}
			return TupleInternal.Create<IEdmAssociationEnd, IEdmAssociationEnd>(edmAssociationEnd2, edmAssociationEnd4);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00013EC4 File Offset: 0x000120C4
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = null;
			if (this.association.End1.Name == this.association.End2.Name)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(this.association.End2.Location ?? base.Location, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(this.association.End1.Name)));
			}
			return list ?? Enumerable.Empty<EdmError>();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00013F41 File Offset: 0x00012141
		private CsdlSemanticsReferentialConstraint ComputeReferentialConstraint()
		{
			if (this.association.Constraint == null)
			{
				return null;
			}
			return new CsdlSemanticsReferentialConstraint(this, this.association.Constraint);
		}

		// Token: 0x0400039F RID: 927
		private readonly CsdlAssociation association;

		// Token: 0x040003A0 RID: 928
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040003A1 RID: 929
		private readonly Cache<CsdlSemanticsAssociation, CsdlSemanticsReferentialConstraint> referentialConstraintCache = new Cache<CsdlSemanticsAssociation, CsdlSemanticsReferentialConstraint>();

		// Token: 0x040003A2 RID: 930
		private static readonly Func<CsdlSemanticsAssociation, CsdlSemanticsReferentialConstraint> ComputeReferentialConstraintFunc = (CsdlSemanticsAssociation me) => me.ComputeReferentialConstraint();

		// Token: 0x040003A3 RID: 931
		private readonly Cache<CsdlSemanticsAssociation, TupleInternal<IEdmAssociationEnd, IEdmAssociationEnd>> endsCache = new Cache<CsdlSemanticsAssociation, TupleInternal<IEdmAssociationEnd, IEdmAssociationEnd>>();

		// Token: 0x040003A4 RID: 932
		private static readonly Func<CsdlSemanticsAssociation, TupleInternal<IEdmAssociationEnd, IEdmAssociationEnd>> ComputeEndsFunc = (CsdlSemanticsAssociation me) => me.ComputeEnds();

		// Token: 0x040003A5 RID: 933
		private readonly Cache<CsdlSemanticsAssociation, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsAssociation, IEnumerable<EdmError>>();

		// Token: 0x040003A6 RID: 934
		private static readonly Func<CsdlSemanticsAssociation, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsAssociation me) => me.ComputeErrors();
	}
}
