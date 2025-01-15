using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000165 RID: 357
	internal class CsdlSemanticsAssociationSetEnd : CsdlSemanticsElement, IEdmAssociationSetEnd, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x0001458F File Offset: 0x0001278F
		public CsdlSemanticsAssociationSetEnd(CsdlSemanticsAssociationSet context, CsdlAssociationSetEnd end, IEdmAssociationEnd role)
			: base(end)
		{
			this.context = context;
			this.end = end;
			this.role = role;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x000145C3 File Offset: 0x000127C3
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x000145D0 File Offset: 0x000127D0
		public override CsdlElement Element
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000145D8 File Offset: 0x000127D8
		public IEdmAssociationEnd Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000145E0 File Offset: 0x000127E0
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet.GetValue(this, CsdlSemanticsAssociationSetEnd.ComputeEntitySetFunc, null);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000145F4 File Offset: 0x000127F4
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsAssociationSetEnd.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00014620 File Offset: 0x00012820
		private IEdmEntitySet ComputeEntitySet()
		{
			if (this.end != null)
			{
				return this.context.Container.FindEntitySet(this.end.EntitySet) ?? new UnresolvedEntitySet(this.end.EntitySet, this.context.Container, base.Location);
			}
			IEnumerable<EdmError> enumerable = new EdmError[]
			{
				new EdmError(base.Location, EdmErrorCode.NoEntitySetsFoundForType, Strings.EdmModel_Validator_Semantic_NoEntitySetsFoundForType(this.context.Container.FullName() + this.context.Name, this.role.EntityType.FullName(), this.Role.Name))
			};
			return Enumerable.FirstOrDefault<IEdmEntitySet>(Enumerable.Where<IEdmEntitySet>(this.context.Container.EntitySets(), (IEdmEntitySet set) => set.ElementType == this.role.EntityType)) ?? new BadEntitySet("UnresolvedEntitySet", this.context.Container, enumerable);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00014730 File Offset: 0x00012930
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = new List<EdmError>();
			if (this.Role is UnresolvedAssociationEnd)
			{
				list.AddRange(this.Role.Errors());
			}
			if (this.EntitySet is UnresolvedEntitySet)
			{
				list.AddRange(this.EntitySet.Errors());
			}
			if (this.end == null)
			{
				if (Enumerable.Count<IEdmEntitySet>(Enumerable.Where<IEdmEntitySet>(this.context.Container.EntitySets(), (IEdmEntitySet set) => set.ElementType == this.role.EntityType)) > 1)
				{
					list.Add(new EdmError(base.Location, EdmErrorCode.CannotInferEntitySetWithMultipleSetsPerType, Strings.EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType(this.context.Container.FullName() + this.context.Name, this.role.EntityType.FullName(), this.Role.Name)));
				}
			}
			return list;
		}

		// Token: 0x040003BE RID: 958
		private readonly CsdlSemanticsAssociationSet context;

		// Token: 0x040003BF RID: 959
		private readonly CsdlAssociationSetEnd end;

		// Token: 0x040003C0 RID: 960
		private readonly IEdmAssociationEnd role;

		// Token: 0x040003C1 RID: 961
		private readonly Cache<CsdlSemanticsAssociationSetEnd, IEdmEntitySet> entitySet = new Cache<CsdlSemanticsAssociationSetEnd, IEdmEntitySet>();

		// Token: 0x040003C2 RID: 962
		private static readonly Func<CsdlSemanticsAssociationSetEnd, IEdmEntitySet> ComputeEntitySetFunc = (CsdlSemanticsAssociationSetEnd me) => me.ComputeEntitySet();

		// Token: 0x040003C3 RID: 963
		private readonly Cache<CsdlSemanticsAssociationSetEnd, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsAssociationSetEnd, IEnumerable<EdmError>>();

		// Token: 0x040003C4 RID: 964
		private static readonly Func<CsdlSemanticsAssociationSetEnd, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsAssociationSetEnd me) => me.ComputeErrors();
	}
}
