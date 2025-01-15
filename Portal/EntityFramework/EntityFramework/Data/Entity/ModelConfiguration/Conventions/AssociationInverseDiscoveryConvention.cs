using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019E RID: 414
	public class AssociationInverseDiscoveryConvention : IConceptualModelConvention<EdmModel>, IConvention
	{
		// Token: 0x06001757 RID: 5975 RVA: 0x0003E1D8 File Offset: 0x0003C3D8
		public virtual void Apply(EdmModel item, DbModel model)
		{
			Check.NotNull<EdmModel>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			foreach (var <>f__AnonymousType in from g in (from a1 in item.AssociationTypes
					from a2 in item.AssociationTypes
					where a1 != a2
					where a1.SourceEnd.GetEntityType() == a2.TargetEnd.GetEntityType() && a1.TargetEnd.GetEntityType() == a2.SourceEnd.GetEntityType()
					let a1Configuration = a1.GetConfiguration() as NavigationPropertyConfiguration
					let a2Configuration = a2.GetConfiguration() as NavigationPropertyConfiguration
					where (a1Configuration == null || (a1Configuration.InverseEndKind == null && a1Configuration.InverseNavigationProperty == null)) && (a2Configuration == null || (a2Configuration.InverseEndKind == null && a2Configuration.InverseNavigationProperty == null))
					select new { a1, a2 }).Distinct((a, b) => a.a1 == b.a2 && a.a2 == b.a1).GroupBy((a, b) => a.a1.SourceEnd.GetEntityType() == b.a2.TargetEnd.GetEntityType() && a.a1.TargetEnd.GetEntityType() == b.a2.SourceEnd.GetEntityType())
				where g.Count() == 1
				select g.Single())
			{
				AssociationType associationType = ((<>f__AnonymousType.a2.GetConfiguration() != null) ? <>f__AnonymousType.a2 : <>f__AnonymousType.a1);
				AssociationType associationType2 = ((associationType == <>f__AnonymousType.a1) ? <>f__AnonymousType.a2 : <>f__AnonymousType.a1);
				associationType.SourceEnd.RelationshipMultiplicity = associationType2.TargetEnd.RelationshipMultiplicity;
				if (associationType2.Constraint != null)
				{
					associationType.Constraint = associationType2.Constraint;
					associationType.Constraint.FromRole = associationType.SourceEnd;
					associationType.Constraint.ToRole = associationType.TargetEnd;
				}
				PropertyInfo clrPropertyInfo = associationType2.SourceEnd.GetClrPropertyInfo();
				if (clrPropertyInfo != null)
				{
					associationType.TargetEnd.SetClrPropertyInfo(clrPropertyInfo);
				}
				AssociationInverseDiscoveryConvention.FixNavigationProperties(item, associationType, associationType2);
				item.RemoveAssociationType(associationType2);
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0003E4B0 File Offset: 0x0003C6B0
		private static void FixNavigationProperties(EdmModel model, AssociationType unifiedAssociation, AssociationType redundantAssociation)
		{
			IEnumerable<NavigationProperty> enumerable = model.EntityTypes.SelectMany((EntityType e) => e.NavigationProperties);
			Func<NavigationProperty, bool> <>9__1;
			Func<NavigationProperty, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (NavigationProperty np) => np.Association == redundantAssociation);
			}
			foreach (NavigationProperty navigationProperty in enumerable.Where(func))
			{
				navigationProperty.RelationshipType = unifiedAssociation;
				navigationProperty.FromEndMember = unifiedAssociation.TargetEnd;
				navigationProperty.ToEndMember = unifiedAssociation.SourceEnd;
			}
		}
	}
}
