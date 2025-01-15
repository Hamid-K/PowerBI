using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AB RID: 427
	public class ForeignKeyNavigationPropertyAttributeConvention : IConceptualModelConvention<NavigationProperty>, IConvention
	{
		// Token: 0x06001779 RID: 6009 RVA: 0x0003F534 File Offset: 0x0003D734
		public virtual void Apply(NavigationProperty item, DbModel model)
		{
			Check.NotNull<NavigationProperty>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			AssociationType association = item.Association;
			if (association.Constraint != null)
			{
				return;
			}
			ForeignKeyAttribute foreignKeyAttribute = item.GetClrAttributes<ForeignKeyAttribute>().SingleOrDefault<ForeignKeyAttribute>();
			if (foreignKeyAttribute == null)
			{
				return;
			}
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			if (association.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2) || association.IsPrincipalConfigured())
			{
				associationEndMember2 = associationEndMember2 ?? association.TargetEnd;
				associationEndMember = associationEndMember ?? association.SourceEnd;
				IEnumerable<string> enumerable = from p in foreignKeyAttribute.Name.Split(new char[] { ',' })
					select p.Trim();
				EntityType entityType = model.ConceptualModel.EntityTypes.Single((EntityType e) => e.DeclaredNavigationProperties.Contains(item));
				List<EdmProperty> list = ForeignKeyNavigationPropertyAttributeConvention.GetDependentProperties(associationEndMember2.GetEntityType(), enumerable, entityType, item).ToList<EdmProperty>();
				ReferentialConstraint constraint = new ReferentialConstraint(associationEndMember, associationEndMember2, associationEndMember.GetEntityType().KeyProperties().ToList<EdmProperty>(), list);
				IEnumerable<EdmProperty> enumerable2 = associationEndMember2.GetEntityType().KeyProperties();
				if (enumerable2.Count<EdmProperty>() == constraint.ToProperties.Count<EdmProperty>() && enumerable2.All((EdmProperty kp) => constraint.ToProperties.Contains(kp)))
				{
					associationEndMember.RelationshipMultiplicity = RelationshipMultiplicity.One;
					if (associationEndMember2.RelationshipMultiplicity.IsMany())
					{
						associationEndMember2.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
					}
				}
				if (associationEndMember.IsRequired())
				{
					constraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
				}
				association.Constraint = constraint;
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0003F705 File Offset: 0x0003D905
		private static IEnumerable<EdmProperty> GetDependentProperties(EntityType dependentType, IEnumerable<string> dependentPropertyNames, EntityType declaringEntityType, NavigationProperty navigationProperty)
		{
			using (IEnumerator<string> enumerator = dependentPropertyNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string dependentPropertyName = enumerator.Current;
					if (string.IsNullOrWhiteSpace(dependentPropertyName))
					{
						throw Error.ForeignKeyAttributeConvention_EmptyKey(navigationProperty.Name, declaringEntityType.GetClrType());
					}
					EdmProperty edmProperty = dependentType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(dependentPropertyName, StringComparison.Ordinal));
					if (edmProperty == null)
					{
						throw Error.ForeignKeyAttributeConvention_InvalidKey(navigationProperty.Name, declaringEntityType.GetClrType(), dependentPropertyName, dependentType.GetClrType());
					}
					yield return edmProperty;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}
	}
}
