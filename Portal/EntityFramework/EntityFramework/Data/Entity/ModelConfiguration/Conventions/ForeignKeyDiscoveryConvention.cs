using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AA RID: 426
	public abstract class ForeignKeyDiscoveryConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0003F302 File Offset: 0x0003D502
		protected virtual bool SupportsMultipleAssociations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001776 RID: 6006
		protected abstract bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty);

		// Token: 0x06001777 RID: 6007 RVA: 0x0003F308 File Offset: 0x0003D508
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.Constraint != null || item.IsIndependent() || (item.IsOneToOne() && item.IsSelfReferencing()))
			{
				return;
			}
			AssociationEndMember dependentEnd;
			AssociationEndMember principalEnd;
			if (!item.TryGuessPrincipalAndDependentEnds(out principalEnd, out dependentEnd))
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable = principalEnd.GetEntityType().KeyProperties();
			if (!enumerable.Any<EdmProperty>())
			{
				return;
			}
			if (!this.SupportsMultipleAssociations && model.ConceptualModel.GetAssociationTypesBetween(principalEnd.GetEntityType(), dependentEnd.GetEntityType()).Count<AssociationType>() > 1)
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable2 = from p in enumerable
				from d in dependentEnd.GetEntityType().DeclaredProperties
				where this.MatchDependentKeyProperty(item, dependentEnd, d, principalEnd.GetEntityType(), p) && p.UnderlyingPrimitiveType == d.UnderlyingPrimitiveType
				select d;
			if (!enumerable2.Any<EdmProperty>() || enumerable2.Count<EdmProperty>() != enumerable.Count<EdmProperty>())
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable3 = dependentEnd.GetEntityType().KeyProperties();
			bool flag = enumerable3.Count<EdmProperty>() == enumerable2.Count<EdmProperty>() && enumerable3.All(new Func<EdmProperty, bool>(enumerable2.Contains<EdmProperty>));
			if ((dependentEnd.IsMany() || item.IsSelfReferencing()) && flag)
			{
				return;
			}
			if (!dependentEnd.IsMany() && !flag)
			{
				return;
			}
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(principalEnd, dependentEnd, enumerable.ToList<EdmProperty>(), enumerable2.ToList<EdmProperty>());
			item.Constraint = referentialConstraint;
			if (principalEnd.IsRequired())
			{
				referentialConstraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
			}
		}
	}
}
