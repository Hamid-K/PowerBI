using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000454 RID: 1108
	internal sealed class PocoPropertyAccessorStrategy : IPropertyAccessorStrategy
	{
		// Token: 0x06003602 RID: 13826 RVA: 0x000ADE66 File Offset: 0x000AC066
		public PocoPropertyAccessorStrategy(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000ADE78 File Offset: 0x000AC078
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			object obj = null;
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueGetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = declaringType.GetTopProperty(relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueGetter = entityProxyFactory.CreateBaseGetter(topProperty.DeclaringType, topProperty);
				}
				bool flag = relatedEnd.DisableLazyLoading();
				try
				{
					obj = relatedEnd.TargetAccessor.ValueGetter(this._entity);
				}
				catch (Exception ex)
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, this._entity.GetType().FullName), ex);
				}
				finally
				{
					relatedEnd.ResetLazyLoading(flag);
				}
			}
			return obj;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000ADF68 File Offset: 0x000AC168
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueSetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = declaringType.GetTopProperty(relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueSetter = entityProxyFactory.CreateBaseSetter(topProperty.DeclaringType, topProperty);
				}
				try
				{
					relatedEnd.TargetAccessor.ValueSetter(this._entity, value);
				}
				catch (Exception ex)
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, this._entity.GetType().FullName), ex);
				}
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000AE038 File Offset: 0x000AC238
		private static Type GetDeclaringType(RelatedEnd relatedEnd)
		{
			if (relatedEnd.NavigationProperty != null)
			{
				return Util.GetObjectMapping((EntityType)relatedEnd.NavigationProperty.DeclaringType, relatedEnd.WrappedOwner.Context.MetadataWorkspace).ClrType.ClrType;
			}
			return relatedEnd.WrappedOwner.IdentityType;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000AE088 File Offset: 0x000AC288
		private static Type GetNavigationPropertyType(Type entityType, string propertyName)
		{
			PropertyInfo topProperty = entityType.GetTopProperty(propertyName);
			Type type;
			if (topProperty != null)
			{
				type = topProperty.PropertyType;
			}
			else
			{
				FieldInfo field = entityType.GetField(propertyName);
				if (!(field != null))
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(propertyName, entityType.FullName));
				}
				type = field.FieldType;
			}
			return type;
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000AE0DC File Offset: 0x000AC2DC
		public void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
			object entity = this._entity;
			try
			{
				object obj = this.GetNavigationPropertyValue(relatedEnd);
				if (obj == null)
				{
					obj = this.CollectionCreate(relatedEnd);
					this.SetNavigationPropertyValue(relatedEnd, obj);
				}
				if (obj != relatedEnd)
				{
					if (relatedEnd.TargetAccessor.CollectionAdd == null)
					{
						relatedEnd.TargetAccessor.CollectionAdd = PocoPropertyAccessorStrategy.CreateCollectionAddFunction(PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd), relatedEnd.TargetAccessor.PropertyName);
					}
					relatedEnd.TargetAccessor.CollectionAdd(obj, value);
				}
			}
			catch (Exception ex)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, entity.GetType().FullName), ex);
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000AE188 File Offset: 0x000AC388
		private static Action<object, object> CreateCollectionAddFunction(Type type, string propertyName)
		{
			Type collectionElementType = EntityUtil.GetCollectionElementType(PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName));
			return (Action<object, object>)PocoPropertyAccessorStrategy.AddToCollectionGeneric.MakeGenericMethod(new Type[] { collectionElementType }).Invoke(null, null);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000AE1C2 File Offset: 0x000AC3C2
		private static Action<object, object> AddToCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_CannotAddToFixedSizeArray(array.GetType()));
				}
				collection.Add((T)((object)item));
			};
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000AE1E4 File Offset: 0x000AC3E4
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			object entity = this._entity;
			try
			{
				object navigationPropertyValue = this.GetNavigationPropertyValue(relatedEnd);
				if (navigationPropertyValue != null)
				{
					if (navigationPropertyValue == relatedEnd)
					{
						return true;
					}
					if (relatedEnd.TargetAccessor.CollectionRemove == null)
					{
						relatedEnd.TargetAccessor.CollectionRemove = PocoPropertyAccessorStrategy.CreateCollectionRemoveFunction(PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd), relatedEnd.TargetAccessor.PropertyName);
					}
					return relatedEnd.TargetAccessor.CollectionRemove(navigationPropertyValue, value);
				}
			}
			catch (Exception ex)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, entity.GetType().FullName), ex);
			}
			return false;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000AE288 File Offset: 0x000AC488
		private static Func<object, object, bool> CreateCollectionRemoveFunction(Type type, string propertyName)
		{
			Type collectionElementType = EntityUtil.GetCollectionElementType(PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName));
			return (Func<object, object, bool>)PocoPropertyAccessorStrategy.RemoveFromCollectionGeneric.MakeGenericMethod(new Type[] { collectionElementType }).Invoke(null, null);
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000AE2C2 File Offset: 0x000AC4C2
		private static Func<object, object, bool> RemoveFromCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_CannotRemoveFromFixedSizeArray(array.GetType()));
				}
				return collection.Remove((T)((object)item));
			};
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000AE2E4 File Offset: 0x000AC4E4
		public object CollectionCreate(RelatedEnd relatedEnd)
		{
			if (this._entity is IEntityWithRelationships)
			{
				return relatedEnd;
			}
			if (relatedEnd.TargetAccessor.CollectionCreate == null)
			{
				Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
				string propertyName = relatedEnd.TargetAccessor.PropertyName;
				Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(declaringType, propertyName);
				relatedEnd.TargetAccessor.CollectionCreate = PocoPropertyAccessorStrategy.CreateCollectionCreateDelegate(navigationPropertyType, propertyName);
			}
			return relatedEnd.TargetAccessor.CollectionCreate();
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000AE348 File Offset: 0x000AC548
		private static Func<object> CreateCollectionCreateDelegate(Type navigationPropertyType, string propName)
		{
			Type type = EntityUtil.DetermineCollectionType(navigationPropertyType);
			if (type == null)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(propName, navigationPropertyType));
			}
			return Expression.Lambda<Func<object>>(DelegateFactory.GetNewExpressionForCollectionType(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x04001174 RID: 4468
		internal static readonly MethodInfo AddToCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetOnlyDeclaredMethod("AddToCollection");

		// Token: 0x04001175 RID: 4469
		internal static readonly MethodInfo RemoveFromCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetOnlyDeclaredMethod("RemoveFromCollection");

		// Token: 0x04001176 RID: 4470
		private readonly object _entity;
	}
}
