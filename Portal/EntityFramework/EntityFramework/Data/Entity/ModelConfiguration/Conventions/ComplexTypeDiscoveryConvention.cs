using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019F RID: 415
	public class ComplexTypeDiscoveryConvention : IConceptualModelConvention<EdmModel>, IConvention
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x0003E570 File Offset: 0x0003C770
		public virtual void Apply(EdmModel item, DbModel model)
		{
			Check.NotNull<EdmModel>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			foreach (var <>f__AnonymousType in (from entityType in item.EntityTypes
				where entityType.KeyProperties.Count == 0 && entityType.BaseType == null
				let entityTypeConfiguration = entityType.GetConfiguration() as EntityTypeConfiguration
				where (entityTypeConfiguration == null || (!entityTypeConfiguration.IsExplicitEntity && entityTypeConfiguration.IsStructuralConfigurationOnly)) && !entityType.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty)).Any<EdmMember>()
				let matchingAssociations = from associationType in item.AssociationTypes
					where associationType.SourceEnd.GetEntityType() == entityType || associationType.TargetEnd.GetEntityType() == entityType
					let declaringEnd = (associationType.SourceEnd.GetEntityType() == entityType) ? associationType.SourceEnd : associationType.TargetEnd
					let declaringEntity = associationType.GetOtherEnd(declaringEnd).GetEntityType()
					let navigationProperties = from NavigationProperty n in declaringEntity.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty))
						where n.ResultEnd.GetEntityType() == entityType
						select n
					select new
					{
						DeclaringEnd = declaringEnd,
						AssociationType = associationType,
						DeclaringEntityType = declaringEntity,
						NavigationProperties = navigationProperties.ToList<NavigationProperty>()
					}
				where matchingAssociations.All(delegate(a)
				{
					if (a.AssociationType.Constraint == null && a.AssociationType.GetConfiguration() == null && !a.AssociationType.IsSelfReferencing() && a.DeclaringEnd.IsOptional())
					{
						return a.NavigationProperties.All((NavigationProperty n) => n.GetConfiguration() == null);
					}
					return false;
				})
				select new
				{
					EntityType = entityType,
					MatchingAssociations = matchingAssociations.ToList()
				}).ToList())
			{
				ComplexType complexType = item.AddComplexType(<>f__AnonymousType.EntityType.Name, <>f__AnonymousType.EntityType.NamespaceName);
				foreach (EdmProperty edmProperty in <>f__AnonymousType.EntityType.DeclaredProperties)
				{
					complexType.AddMember(edmProperty);
				}
				foreach (MetadataProperty metadataProperty in <>f__AnonymousType.EntityType.Annotations)
				{
					complexType.GetMetadataProperties().Add(metadataProperty);
				}
				foreach (var <>f__AnonymousType2 in <>f__AnonymousType.MatchingAssociations)
				{
					foreach (NavigationProperty navigationProperty in <>f__AnonymousType2.NavigationProperties)
					{
						if (<>f__AnonymousType2.DeclaringEntityType.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty)).Contains(navigationProperty))
						{
							<>f__AnonymousType2.DeclaringEntityType.RemoveMember(navigationProperty);
							EdmProperty edmProperty2 = <>f__AnonymousType2.DeclaringEntityType.AddComplexProperty(navigationProperty.Name, complexType);
							foreach (MetadataProperty metadataProperty2 in navigationProperty.Annotations)
							{
								edmProperty2.GetMetadataProperties().Add(metadataProperty2);
							}
						}
					}
					item.RemoveAssociationType(<>f__AnonymousType2.AssociationType);
				}
				item.RemoveEntityType(<>f__AnonymousType.EntityType);
			}
		}
	}
}
