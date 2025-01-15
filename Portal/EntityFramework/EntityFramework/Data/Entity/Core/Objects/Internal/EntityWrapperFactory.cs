using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000441 RID: 1089
	internal class EntityWrapperFactory
	{
		// Token: 0x0600351D RID: 13597 RVA: 0x000AADF0 File Offset: 0x000A8FF0
		internal static IEntityWrapper CreateNewWrapper(object entity, EntityKey key)
		{
			if (entity == null)
			{
				return NullEntityWrapper.NullWrapper;
			}
			IEntityWrapper entityWrapper = EntityWrapperFactory._delegateCache.Evaluate(entity.GetType())(entity);
			entityWrapper.RelationshipManager.SetWrappedOwner(entityWrapper, entity);
			if (key != null && entityWrapper.EntityKey == null)
			{
				entityWrapper.EntityKey = key;
			}
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(entity.GetType(), out entityProxyTypeInfo))
			{
				entityProxyTypeInfo.SetEntityWrapper(entityWrapper);
			}
			return entityWrapper;
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000AAE54 File Offset: 0x000A9054
		private static Func<object, IEntityWrapper> CreateWrapperDelegate(Type entityType)
		{
			bool flag = typeof(IEntityWithRelationships).IsAssignableFrom(entityType);
			bool flag2 = typeof(IEntityWithChangeTracker).IsAssignableFrom(entityType);
			bool flag3 = typeof(IEntityWithKey).IsAssignableFrom(entityType);
			bool flag4 = EntityProxyFactory.IsProxyType(entityType);
			MethodInfo methodInfo;
			if (flag && flag2 && flag3 && !flag4)
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedLightweightMethod;
			}
			else if (flag)
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedWithRelationshipsMethod;
			}
			else
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedWithoutRelationshipsMethod;
			}
			methodInfo = methodInfo.MakeGenericMethod(new Type[] { entityType });
			return (Func<object, IEntityWrapper>)methodInfo.Invoke(null, new object[0]);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000AAEE7 File Offset: 0x000A90E7
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedLightweight<TEntity>() where TEntity : class, IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			return (object entity) => new LightweightEntityWrapper<TEntity>((TEntity)((object)entity), overridesEquals);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000AAF10 File Offset: 0x000A9110
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithRelationships<TEntity>() where TEntity : class, IEntityWithRelationships
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy, overridesEquals);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000AAF5C File Offset: 0x000A915C
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithoutRelationships<TEntity>() where TEntity : class
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithoutRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy, overridesEquals);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000AAFA8 File Offset: 0x000A91A8
		private static void CreateStrategies<TEntity>(out Func<object, IPropertyAccessorStrategy> createPropertyAccessorStrategy, out Func<object, IChangeTrackingStrategy> createChangeTrackingStrategy, out Func<object, IEntityKeyStrategy> createKeyStrategy)
		{
			Type typeFromHandle = typeof(TEntity);
			int num = (typeof(IEntityWithRelationships).IsAssignableFrom(typeFromHandle) ? 1 : 0);
			bool flag = typeof(IEntityWithChangeTracker).IsAssignableFrom(typeFromHandle);
			bool flag2 = typeof(IEntityWithKey).IsAssignableFrom(typeFromHandle);
			bool flag3 = EntityProxyFactory.IsProxyType(typeFromHandle);
			if (num == 0 || flag3)
			{
				createPropertyAccessorStrategy = EntityWrapperFactory.GetPocoPropertyAccessorStrategyFunc();
			}
			else
			{
				createPropertyAccessorStrategy = EntityWrapperFactory.GetNullPropertyAccessorStrategyFunc();
			}
			if (flag)
			{
				createChangeTrackingStrategy = EntityWrapperFactory.GetEntityWithChangeTrackerStrategyFunc();
			}
			else
			{
				createChangeTrackingStrategy = EntityWrapperFactory.GetSnapshotChangeTrackingStrategyFunc();
			}
			if (flag2)
			{
				createKeyStrategy = EntityWrapperFactory.GetEntityWithKeyStrategyStrategyFunc();
				return;
			}
			createKeyStrategy = EntityWrapperFactory.GetPocoEntityKeyStrategyFunc();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000AB038 File Offset: 0x000A9238
		internal IEntityWrapper WrapEntityUsingContext(object entity, ObjectContext context)
		{
			EntityEntry entityEntry;
			return this.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out entityEntry);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000AB05A File Offset: 0x000A925A
		internal IEntityWrapper WrapEntityUsingContextGettingEntry(object entity, ObjectContext context, out EntityEntry existingEntry)
		{
			return this.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out existingEntry);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000AB070 File Offset: 0x000A9270
		internal IEntityWrapper WrapEntityUsingStateManager(object entity, ObjectStateManager stateManager)
		{
			EntityEntry entityEntry;
			return this.WrapEntityUsingStateManagerGettingEntry(entity, stateManager, out entityEntry);
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000AB088 File Offset: 0x000A9288
		internal virtual IEntityWrapper WrapEntityUsingStateManagerGettingEntry(object entity, ObjectStateManager stateManager, out EntityEntry existingEntry)
		{
			IEntityWrapper entityWrapper = null;
			existingEntry = null;
			if (entity == null)
			{
				return NullEntityWrapper.NullWrapper;
			}
			if (stateManager != null)
			{
				existingEntry = stateManager.FindEntityEntry(entity);
				if (existingEntry != null)
				{
					return existingEntry.WrappedEntity;
				}
				if (stateManager.TransactionManager.TrackProcessedEntities && stateManager.TransactionManager.WrappedEntities.TryGetValue(entity, out entityWrapper))
				{
					return entityWrapper;
				}
			}
			IEntityWithRelationships entityWithRelationships = entity as IEntityWithRelationships;
			if (entityWithRelationships == null)
			{
				EntityProxyFactory.TryGetProxyWrapper(entity, out entityWrapper);
				if (entityWrapper == null)
				{
					IEntityWithKey entityWithKey = entity as IEntityWithKey;
					entityWrapper = EntityWrapperFactory.CreateNewWrapper(entity, (entityWithKey == null) ? null : entityWithKey.EntityKey);
				}
				if (stateManager != null && stateManager.TransactionManager.TrackProcessedEntities)
				{
					stateManager.TransactionManager.WrappedEntities.Add(entity, entityWrapper);
				}
				return entityWrapper;
			}
			RelationshipManager relationshipManager = entityWithRelationships.RelationshipManager;
			if (relationshipManager == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			IEntityWrapper wrappedOwner = relationshipManager.WrappedOwner;
			if (wrappedOwner.Entity != entity)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
			}
			return wrappedOwner;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000AB160 File Offset: 0x000A9360
		internal virtual void UpdateNoTrackingWrapper(IEntityWrapper wrapper, ObjectContext context, EntitySet entitySet)
		{
			if (wrapper.EntityKey == null)
			{
				wrapper.EntityKey = context.ObjectStateManager.CreateEntityKey(entitySet, wrapper.Entity);
			}
			if (wrapper.Context == null)
			{
				wrapper.AttachContext(context, entitySet, MergeOption.NoTracking);
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000AB199 File Offset: 0x000A9399
		internal static Func<object, IPropertyAccessorStrategy> GetPocoPropertyAccessorStrategyFunc()
		{
			return (object entity) => new PocoPropertyAccessorStrategy(entity);
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000AB1BA File Offset: 0x000A93BA
		internal static Func<object, IPropertyAccessorStrategy> GetNullPropertyAccessorStrategyFunc()
		{
			return (object entity) => null;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000AB1DB File Offset: 0x000A93DB
		internal static Func<object, IChangeTrackingStrategy> GetEntityWithChangeTrackerStrategyFunc()
		{
			return (object entity) => new EntityWithChangeTrackerStrategy((IEntityWithChangeTracker)entity);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000AB1FC File Offset: 0x000A93FC
		internal static Func<object, IChangeTrackingStrategy> GetSnapshotChangeTrackingStrategyFunc()
		{
			return (object entity) => SnapshotChangeTrackingStrategy.Instance;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000AB21D File Offset: 0x000A941D
		internal static Func<object, IEntityKeyStrategy> GetEntityWithKeyStrategyStrategyFunc()
		{
			return (object entity) => new EntityWithKeyStrategy((IEntityWithKey)entity);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000AB23E File Offset: 0x000A943E
		internal static Func<object, IEntityKeyStrategy> GetPocoEntityKeyStrategyFunc()
		{
			return (object entity) => new PocoEntityKeyStrategy();
		}

		// Token: 0x04001134 RID: 4404
		private static readonly Memoizer<Type, Func<object, IEntityWrapper>> _delegateCache = new Memoizer<Type, Func<object, IEntityWrapper>>(new Func<Type, Func<object, IEntityWrapper>>(EntityWrapperFactory.CreateWrapperDelegate), null);

		// Token: 0x04001135 RID: 4405
		internal static readonly MethodInfo CreateWrapperDelegateTypedLightweightMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedLightweight");

		// Token: 0x04001136 RID: 4406
		internal static readonly MethodInfo CreateWrapperDelegateTypedWithRelationshipsMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedWithRelationships");

		// Token: 0x04001137 RID: 4407
		internal static readonly MethodInfo CreateWrapperDelegateTypedWithoutRelationshipsMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedWithoutRelationships");
	}
}
