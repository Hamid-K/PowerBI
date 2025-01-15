using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B0 RID: 432
	public class OneToOneConstraintIntroductionConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x0003F9A0 File Offset: 0x0003DBA0
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.IsOneToOne() && !item.IsSelfReferencing() && !item.IsIndependent() && item.Constraint == null)
			{
				IEnumerable<EdmProperty> enumerable = item.SourceEnd.GetEntityType().KeyProperties();
				IEnumerable<EdmProperty> enumerable2 = item.TargetEnd.GetEntityType().KeyProperties();
				if (enumerable.Count<EdmProperty>() == enumerable2.Count<EdmProperty>())
				{
					AssociationEndMember associationEndMember;
					AssociationEndMember associationEndMember2;
					if (enumerable.Select((EdmProperty p) => p.UnderlyingPrimitiveType).SequenceEqual(enumerable2.Select((EdmProperty p) => p.UnderlyingPrimitiveType)) && (item.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2) || item.IsPrincipalConfigured()))
					{
						associationEndMember2 = associationEndMember2 ?? item.TargetEnd;
						AssociationEndMember otherEnd = item.GetOtherEnd(associationEndMember2);
						ReferentialConstraint referentialConstraint = new ReferentialConstraint(otherEnd, associationEndMember2, otherEnd.GetEntityType().KeyProperties().ToList<EdmProperty>(), associationEndMember2.GetEntityType().KeyProperties().ToList<EdmProperty>());
						item.Constraint = referentialConstraint;
					}
				}
			}
		}
	}
}
