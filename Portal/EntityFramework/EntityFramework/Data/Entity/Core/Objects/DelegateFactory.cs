using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000405 RID: 1029
	internal static class DelegateFactory
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x000975AC File Offset: 0x000957AC
		internal static Func<object> GetConstructorDelegateForType(ClrComplexType clrType)
		{
			Func<object> func;
			if ((func = clrType.Constructor) == null)
			{
				func = (clrType.Constructor = DelegateFactory.CreateConstructor(clrType.ClrType));
			}
			return func;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000975D8 File Offset: 0x000957D8
		internal static Func<object> GetConstructorDelegateForType(ClrEntityType clrType)
		{
			Func<object> func;
			if ((func = clrType.Constructor) == null)
			{
				func = (clrType.Constructor = DelegateFactory.CreateConstructor(clrType.ClrType));
			}
			return func;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00097603 File Offset: 0x00095803
		internal static object GetValue(EdmProperty property, object target)
		{
			return DelegateFactory.GetGetterDelegateForProperty(property)(target);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00097614 File Offset: 0x00095814
		internal static Func<object, object> GetGetterDelegateForProperty(EdmProperty property)
		{
			Func<object, object> func;
			if ((func = property.ValueGetter) == null)
			{
				func = (property.ValueGetter = DelegateFactory.CreatePropertyGetter(property.EntityDeclaringType, property.PropertyInfo));
			}
			return func;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00097645 File Offset: 0x00095845
		internal static void SetValue(EdmProperty property, object target, object value)
		{
			DelegateFactory.GetSetterDelegateForProperty(property)(target, value);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00097654 File Offset: 0x00095854
		internal static Action<object, object> GetSetterDelegateForProperty(EdmProperty property)
		{
			Action<object, object> action = property.ValueSetter;
			if (action == null)
			{
				action = DelegateFactory.CreatePropertySetter(property.EntityDeclaringType, property.PropertyInfo, property.Nullable);
				property.ValueSetter = action;
			}
			return action;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0009768C File Offset: 0x0009588C
		internal static RelatedEnd GetRelatedEnd(RelationshipManager sourceRelationshipManager, AssociationEndMember sourceMember, AssociationEndMember targetMember, RelatedEnd existingRelatedEnd)
		{
			Func<RelationshipManager, RelatedEnd, RelatedEnd> func = sourceMember.GetRelatedEnd;
			if (func == null)
			{
				func = DelegateFactory.CreateGetRelatedEndMethod(sourceMember, targetMember);
				sourceMember.GetRelatedEnd = func;
			}
			return func(sourceRelationshipManager, existingRelatedEnd);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000976BC File Offset: 0x000958BC
		internal static Action<object, object> CreateNavigationPropertySetter(Type declaringType, PropertyInfo navigationProperty)
		{
			PropertyInfo propertyInfoForSet = navigationProperty.GetPropertyInfoForSet();
			MethodInfo methodInfo = propertyInfoForSet.Setter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoSetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (methodInfo.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			return Expression.Lambda<Action<object, object>>(Expression.Assign(Expression.Property(Expression.Convert(parameterExpression, declaringType), propertyInfoForSet), Expression.Convert(parameterExpression2, navigationProperty.PropertyType)), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x00097774 File Offset: 0x00095974
		internal static ConstructorInfo GetConstructorForType(Type type)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[0]);
			if (null == declaredConstructor)
			{
				throw new InvalidOperationException(Strings.CodeGen_ConstructorNoParameterless(type.FullName));
			}
			return declaredConstructor;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000977AC File Offset: 0x000959AC
		internal static NewExpression GetNewExpressionForCollectionType(Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(HashSet<>))
			{
				return Expression.New(type.GetDeclaredConstructor(new Type[] { typeof(IEqualityComparer<>).MakeGenericType(type.GetGenericArguments()) }), new Expression[] { Expression.New(typeof(ObjectReferenceEqualityComparer)) });
			}
			return Expression.New(DelegateFactory.GetConstructorForType(type));
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x00097825 File Offset: 0x00095A25
		internal static Func<object> CreateConstructor(Type type)
		{
			DelegateFactory.GetConstructorForType(type);
			return Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00097844 File Offset: 0x00095A44
		internal static Func<object, object> CreatePropertyGetter(Type entityDeclaringType, PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = propertyInfo.Getter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoGetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (propertyInfo.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			if (propertyInfo.GetIndexParameters().Any<ParameterInfo>())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsIndexed);
			}
			Type propertyType = propertyInfo.PropertyType;
			if (propertyType.IsPointer)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyUnsupportedType);
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "entity");
			Expression expression = Expression.Property(Expression.Convert(parameterExpression, entityDeclaringType), propertyInfo);
			if (propertyType.IsValueType())
			{
				expression = Expression.Convert(expression, typeof(object));
			}
			return Expression.Lambda<Func<object, object>>(expression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x00097918 File Offset: 0x00095B18
		internal static Action<object, object> CreatePropertySetter(Type entityDeclaringType, PropertyInfo propertyInfo, bool allowNull)
		{
			PropertyInfo propertyInfo2 = DelegateFactory.ValidateSetterProperty(propertyInfo);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "entity");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "target");
			Type propertyType = propertyInfo.PropertyType;
			if (propertyType.IsValueType() && Nullable.GetUnderlyingType(propertyType) == null)
			{
				allowNull = false;
			}
			Expression expression = Expression.TypeIs(parameterExpression2, propertyType);
			if (allowNull)
			{
				expression = Expression.Or(Expression.ReferenceEqual(parameterExpression2, Expression.Constant(null)), expression);
			}
			return Expression.Lambda<Action<object, object>>(Expression.IfThenElse(expression, Expression.Assign(Expression.Property(Expression.Convert(parameterExpression, entityDeclaringType), propertyInfo2), Expression.Convert(parameterExpression2, propertyInfo.PropertyType)), Expression.Call(DelegateFactory._throwSetInvalidValue, parameterExpression2, Expression.Constant(propertyType), Expression.Constant(entityDeclaringType.Name), Expression.Constant(propertyInfo.Name))), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000979FC File Offset: 0x00095BFC
		internal static PropertyInfo ValidateSetterProperty(PropertyInfo propertyInfo)
		{
			PropertyInfo propertyInfoForSet = propertyInfo.GetPropertyInfoForSet();
			MethodInfo methodInfo = propertyInfoForSet.Setter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoSetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (propertyInfoForSet.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			if (propertyInfoForSet.GetIndexParameters().Any<ParameterInfo>())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsIndexed);
			}
			if (propertyInfoForSet.PropertyType.IsPointer)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyUnsupportedType);
			}
			return propertyInfoForSet;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00097A84 File Offset: 0x00095C84
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod(AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(sourceMember);
			EntityType entityTypeForEnd2 = MetadataHelper.GetEntityTypeForEnd(targetMember);
			NavigationPropertyAccessor navigationPropertyAccessor = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd2, targetMember, sourceMember);
			NavigationPropertyAccessor navigationPropertyAccessor2 = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd, sourceMember, targetMember);
			return (Func<RelationshipManager, RelatedEnd, RelatedEnd>)typeof(DelegateFactory).GetDeclaredMethod("CreateGetRelatedEndMethod", new Type[]
			{
				typeof(AssociationEndMember),
				typeof(AssociationEndMember),
				typeof(NavigationPropertyAccessor),
				typeof(NavigationPropertyAccessor)
			}).MakeGenericMethod(new Type[] { entityTypeForEnd.ClrType, entityTypeForEnd2.ClrType }).Invoke(null, new object[] { sourceMember, targetMember, navigationPropertyAccessor, navigationPropertyAccessor2 });
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00097B40 File Offset: 0x00095D40
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod<TSource, TTarget>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor) where TSource : class where TTarget : class
		{
			RelationshipMultiplicity relationshipMultiplicity = targetMember.RelationshipMultiplicity;
			Func<RelationshipManager, RelatedEnd, RelatedEnd> func;
			if (relationshipMultiplicity > RelationshipMultiplicity.One)
			{
				if (relationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					Type typeFromHandle = typeof(RelationshipMultiplicity);
					throw new ArgumentOutOfRangeException(typeFromHandle.Name, Strings.ADP_InvalidEnumerationValue(typeFromHandle.Name, ((int)targetMember.RelationshipMultiplicity).ToString(CultureInfo.InvariantCulture)));
				}
				func = (RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedCollection<TSource, TTarget>(sourceMember, targetMember, sourceAccessor, targetAccessor, relatedEnd);
			}
			else
			{
				func = (RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedReference<TSource, TTarget>(sourceMember, targetMember, sourceAccessor, targetAccessor, relatedEnd);
			}
			return func;
		}

		// Token: 0x04001018 RID: 4120
		private static readonly MethodInfo _throwSetInvalidValue = typeof(EntityUtil).GetDeclaredMethod("ThrowSetInvalidValue", new Type[]
		{
			typeof(object),
			typeof(Type),
			typeof(string),
			typeof(string)
		});
	}
}
