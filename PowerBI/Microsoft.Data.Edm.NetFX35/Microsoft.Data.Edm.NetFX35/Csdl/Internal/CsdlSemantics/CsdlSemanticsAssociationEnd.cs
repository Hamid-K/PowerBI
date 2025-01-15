using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000163 RID: 355
	internal class CsdlSemanticsAssociationEnd : CsdlSemanticsElement, IEdmAssociationEnd, IEdmNamedElement, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00013FEF File Offset: 0x000121EF
		public CsdlSemanticsAssociationEnd(CsdlSemanticsSchema context, CsdlSemanticsAssociation association, CsdlAssociationEnd end)
			: base(end)
		{
			this.end = end;
			this.definingAssociation = association;
			this.context = context;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00014023 File Offset: 0x00012223
		public EdmMultiplicity Multiplicity
		{
			get
			{
				return this.end.Multiplicity;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00014030 File Offset: 0x00012230
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				if (this.end.OnDelete == null)
				{
					return EdmOnDeleteAction.None;
				}
				return this.end.OnDelete.Action;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00014051 File Offset: 0x00012251
		public IEdmAssociation DeclaringAssociation
		{
			get
			{
				return this.definingAssociation;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00014059 File Offset: 0x00012259
		public IEdmEntityType EntityType
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsAssociationEnd.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001406D File Offset: 0x0001226D
		public string Name
		{
			get
			{
				return this.end.Name ?? string.Empty;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00014083 File Offset: 0x00012283
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00014090 File Offset: 0x00012290
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsAssociationEnd.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000140A4 File Offset: 0x000122A4
		public override CsdlElement Element
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000140AC File Offset: 0x000122AC
		private IEdmEntityType ComputeType()
		{
			IEdmTypeReference edmTypeReference = CsdlSemanticsModel.WrapTypeReference(this.context, this.end.Type);
			if (edmTypeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return new UnresolvedEntityType(edmTypeReference.FullName(), base.Location);
			}
			return edmTypeReference.AsEntity().EntityDefinition();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000140F8 File Offset: 0x000122F8
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = null;
			if (this.EntityType is UnresolvedEntityType)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, this.EntityType.Errors());
			}
			return list ?? Enumerable.Empty<EdmError>();
		}

		// Token: 0x040003AA RID: 938
		private readonly CsdlAssociationEnd end;

		// Token: 0x040003AB RID: 939
		private readonly CsdlSemanticsAssociation definingAssociation;

		// Token: 0x040003AC RID: 940
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040003AD RID: 941
		private readonly Cache<CsdlSemanticsAssociationEnd, IEdmEntityType> typeCache = new Cache<CsdlSemanticsAssociationEnd, IEdmEntityType>();

		// Token: 0x040003AE RID: 942
		private static readonly Func<CsdlSemanticsAssociationEnd, IEdmEntityType> ComputeTypeFunc = (CsdlSemanticsAssociationEnd me) => me.ComputeType();

		// Token: 0x040003AF RID: 943
		private readonly Cache<CsdlSemanticsAssociationEnd, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsAssociationEnd, IEnumerable<EdmError>>();

		// Token: 0x040003B0 RID: 944
		private static readonly Func<CsdlSemanticsAssociationEnd, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsAssociationEnd me) => me.ComputeErrors();
	}
}
