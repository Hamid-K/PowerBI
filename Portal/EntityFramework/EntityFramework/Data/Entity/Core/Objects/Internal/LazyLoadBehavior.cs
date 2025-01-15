using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200044A RID: 1098
	internal sealed class LazyLoadBehavior
	{
		// Token: 0x06003578 RID: 13688 RVA: 0x000AC58C File Offset: 0x000AA78C
		internal static Func<TProxy, TItem, bool> GetInterceptorDelegate<TProxy, TItem>(EdmMember member, Func<object, object> getEntityWrapperDelegate) where TProxy : class where TItem : class
		{
			Func<TProxy, TItem, bool> func = (TProxy proxy, TItem item) => true;
			if (member.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
			{
				NavigationProperty navProperty = (NavigationProperty)member;
				if (navProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					func = (TProxy proxy, TItem item) => LazyLoadBehavior.LoadProperty<TItem>(item, navProperty.RelationshipType.Identity, navProperty.ToEndMember.Identity, false, getEntityWrapperDelegate(proxy));
				}
				else
				{
					func = (TProxy proxy, TItem item) => LazyLoadBehavior.LoadProperty<TItem>(item, navProperty.RelationshipType.Identity, navProperty.ToEndMember.Identity, true, getEntityWrapperDelegate(proxy));
				}
			}
			return func;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000AC60C File Offset: 0x000AA80C
		internal static bool IsLazyLoadCandidate(EntityType ospaceEntityType, EdmMember member)
		{
			bool flag = false;
			if (member.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
			{
				RelationshipMultiplicity relationshipMultiplicity = ((NavigationProperty)member).ToEndMember.RelationshipMultiplicity;
				Type propertyType = ospaceEntityType.ClrType.GetTopProperty(member.Name).PropertyType;
				if (relationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					flag = propertyType.TryGetElementType(typeof(ICollection<>)) != null;
				}
				else if (relationshipMultiplicity == RelationshipMultiplicity.One || relationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000AC674 File Offset: 0x000AA874
		private static bool LoadProperty<TItem>(TItem propertyValue, string relationshipName, string targetRoleName, bool mustBeNull, object wrapperObject) where TItem : class
		{
			IEntityWrapper entityWrapper = (IEntityWrapper)wrapperObject;
			if (entityWrapper != null && entityWrapper.Context != null)
			{
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				if (relationshipManager != null && (!mustBeNull || propertyValue == null))
				{
					relationshipManager.GetRelatedEndInternal(relationshipName, targetRoleName).DeferredLoad();
				}
			}
			return propertyValue != null;
		}
	}
}
