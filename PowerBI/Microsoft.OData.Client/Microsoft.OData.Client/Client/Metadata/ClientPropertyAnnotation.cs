using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Client.Metadata
{
	// Token: 0x020000F3 RID: 243
	[DebuggerDisplay("{PropertyName}")]
	internal sealed class ClientPropertyAnnotation
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x00025DD8 File Offset: 0x00023FD8
		internal ClientPropertyAnnotation(IEdmProperty edmProperty, PropertyInfo propertyInfo, ClientEdmModel model)
		{
			this.EdmProperty = edmProperty;
			this.PropertyName = ClientTypeUtil.GetServerDefinedName(propertyInfo);
			this.PropertyInfo = propertyInfo;
			this.NullablePropertyType = propertyInfo.PropertyType;
			this.PropertyType = Nullable.GetUnderlyingType(this.NullablePropertyType) ?? this.NullablePropertyType;
			this.DeclaringClrType = propertyInfo.DeclaringType;
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			this.propertyGetter = ((getMethod == null) ? null : ((Func<object, object>)Expression.Lambda(Expression.Convert(Expression.Call(Expression.Convert(parameterExpression, this.DeclaringClrType), getMethod), typeof(object)), new ParameterExpression[] { parameterExpression }).Compile()));
			this.propertySetter = ((setMethod == null) ? null : ((Action<object, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.DeclaringClrType), setMethod, new Expression[] { Expression.Convert(parameterExpression2, this.NullablePropertyType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
			this.Model = model;
			this.IsKnownType = PrimitiveType.IsKnownType(this.PropertyType);
			if (!this.IsKnownType)
			{
				MethodInfo methodForGenericType = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(IDictionary<, >), "set_Item", out this.DictionaryValueType);
				if (methodForGenericType != null)
				{
					ParameterExpression parameterExpression3 = Expression.Parameter(typeof(string), "propertyName");
					this.dictionarySetter = (Action<object, string, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, typeof(IDictionary<, >).MakeGenericType(new Type[]
					{
						typeof(string),
						this.DictionaryValueType
					})), methodForGenericType, parameterExpression3, Expression.Convert(parameterExpression2, this.DictionaryValueType)), new ParameterExpression[] { parameterExpression, parameterExpression3, parameterExpression2 }).Compile();
					return;
				}
				MethodInfo methodForGenericType2 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Contains", out this.collectionGenericType);
				MethodInfo addToCollectionMethod = ClientTypeUtil.GetAddToCollectionMethod(this.PropertyType, out this.collectionGenericType);
				MethodInfo methodForGenericType3 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Remove", out this.collectionGenericType);
				MethodInfo methodForGenericType4 = ClientTypeUtil.GetMethodForGenericType(this.PropertyType, typeof(ICollection<>), "Clear", out this.collectionGenericType);
				this.collectionContains = ((methodForGenericType2 == null) ? null : ((Func<object, object, bool>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType2, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionAdd = ((addToCollectionMethod == null) ? null : ((Action<object, object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), addToCollectionMethod, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionRemove = ((methodForGenericType3 == null) ? null : ((Func<object, object, bool>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType3, new Expression[] { Expression.Convert(parameterExpression2, this.collectionGenericType) }), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile()));
				this.collectionClear = ((methodForGenericType4 == null) ? null : ((Action<object>)Expression.Lambda(Expression.Call(Expression.Convert(parameterExpression, this.PropertyType), methodForGenericType4), new ParameterExpression[] { parameterExpression }).Compile()));
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000261AA File Offset: 0x000243AA
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x000261B2 File Offset: 0x000243B2
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000261BB File Offset: 0x000243BB
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x000261C3 File Offset: 0x000243C3
		internal ClientPropertyAnnotation MimeTypeProperty
		{
			get
			{
				return this.mimeTypeProperty;
			}
			set
			{
				this.mimeTypeProperty = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x000261CC File Offset: 0x000243CC
		internal Type EntityCollectionItemType
		{
			get
			{
				if (!this.IsEntityCollection)
				{
					return null;
				}
				return this.collectionGenericType;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000261DE File Offset: 0x000243DE
		internal bool IsEntityCollection
		{
			get
			{
				return this.collectionGenericType != null && !this.IsPrimitiveOrEnumOrComplexCollection;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x000261F9 File Offset: 0x000243F9
		internal Type ResourceSetItemType
		{
			get
			{
				if (!this.IsResourceSet)
				{
					return null;
				}
				return this.collectionGenericType;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002620C File Offset: 0x0002440C
		internal bool IsResourceSet
		{
			get
			{
				if (this.isResourceSet == null)
				{
					if (this.collectionGenericType == null)
					{
						this.isResourceSet = new bool?(false);
					}
					else
					{
						this.isResourceSet = new bool?((this.EdmProperty.PropertyKind == EdmPropertyKind.Structural || this.EdmProperty.PropertyKind == EdmPropertyKind.Navigation) && this.EdmProperty.Type.AsCollection().ElementType().IsStructured());
					}
				}
				return this.isResourceSet.Value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00026291 File Offset: 0x00024491
		internal Type PrimitiveOrComplexCollectionItemType
		{
			get
			{
				if (this.IsPrimitiveOrEnumOrComplexCollection)
				{
					return this.collectionGenericType;
				}
				return null;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000262A3 File Offset: 0x000244A3
		internal bool IsEnumType
		{
			get
			{
				return this.PropertyType.IsEnum();
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000262B0 File Offset: 0x000244B0
		internal bool IsComplex
		{
			get
			{
				if (this.isComplex == null)
				{
					this.isComplex = new bool?(this.EdmProperty.Type.TypeKind() == EdmTypeKind.Complex);
				}
				return this.isComplex.Value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x000262E8 File Offset: 0x000244E8
		internal bool IsComplexCollection
		{
			get
			{
				if (this.isComplexCollection == null)
				{
					if (this.collectionGenericType == null)
					{
						this.isComplexCollection = new bool?(false);
					}
					else
					{
						IEdmTypeReference type = this.EdmProperty.Type;
						this.isComplexCollection = new bool?(this.EdmProperty.PropertyKind == EdmPropertyKind.Structural && type.IsCollection() && (type as IEdmCollectionTypeReference).ElementType().IsComplex());
					}
				}
				return this.isComplexCollection.Value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002636C File Offset: 0x0002456C
		internal bool IsPrimitiveOrEnumOrComplexCollection
		{
			get
			{
				if (this.isPrimitiveOrEnumOrComplexCollection == null)
				{
					if (this.collectionGenericType == null)
					{
						this.isPrimitiveOrEnumOrComplexCollection = new bool?(false);
					}
					else
					{
						bool flag = this.EdmProperty.PropertyKind == EdmPropertyKind.Structural && this.EdmProperty.Type.TypeKind() == EdmTypeKind.Collection;
						this.isPrimitiveOrEnumOrComplexCollection = new bool?(flag);
					}
				}
				return this.isPrimitiveOrEnumOrComplexCollection.Value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000263E0 File Offset: 0x000245E0
		internal bool IsSpatialType
		{
			get
			{
				if (this.isSpatialType == null)
				{
					if (typeof(ISpatial).IsAssignableFrom(this.PropertyType))
					{
						this.isSpatialType = new bool?(true);
					}
					else
					{
						this.isSpatialType = new bool?(false);
					}
				}
				return this.isSpatialType.Value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00026436 File Offset: 0x00024636
		internal bool IsDictionary
		{
			get
			{
				return this.DictionaryValueType != null;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00026444 File Offset: 0x00024644
		internal bool IsStreamLinkProperty
		{
			get
			{
				return this.PropertyType == typeof(DataServiceStreamLink);
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002645B File Offset: 0x0002465B
		internal object GetValue(object instance)
		{
			return this.propertyGetter(instance);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00026469 File Offset: 0x00024669
		internal void RemoveValue(object instance, object value)
		{
			this.collectionRemove(instance, value);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002647C File Offset: 0x0002467C
		internal void SetValue(object instance, object value, string propertyName, bool allowAdd)
		{
			if (this.dictionarySetter != null)
			{
				this.dictionarySetter(instance, propertyName, value);
				return;
			}
			if (allowAdd && this.collectionAdd != null)
			{
				if (!this.collectionContains(instance, value))
				{
					this.AddValueToBackingICollectionInstance(instance, value);
					return;
				}
				return;
			}
			else
			{
				if (this.propertySetter != null)
				{
					this.propertySetter(instance, value);
					return;
				}
				throw Error.InvalidOperation(Strings.ClientType_MissingProperty(value.GetType().ToString(), propertyName));
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000264F1 File Offset: 0x000246F1
		internal void ClearBackingICollectionInstance(object collectionInstance)
		{
			this.collectionClear(collectionInstance);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000264FF File Offset: 0x000246FF
		internal void AddValueToBackingICollectionInstance(object collectionInstance, object value)
		{
			this.collectionAdd(collectionInstance, value);
		}

		// Token: 0x040005EC RID: 1516
		internal readonly IEdmProperty EdmProperty;

		// Token: 0x040005ED RID: 1517
		internal readonly string PropertyName;

		// Token: 0x040005EE RID: 1518
		internal readonly PropertyInfo PropertyInfo;

		// Token: 0x040005EF RID: 1519
		internal readonly Type NullablePropertyType;

		// Token: 0x040005F0 RID: 1520
		internal readonly Type PropertyType;

		// Token: 0x040005F1 RID: 1521
		internal readonly Type DictionaryValueType;

		// Token: 0x040005F2 RID: 1522
		internal readonly Type DeclaringClrType;

		// Token: 0x040005F3 RID: 1523
		internal readonly bool IsKnownType;

		// Token: 0x040005F4 RID: 1524
		private readonly Func<object, object> propertyGetter;

		// Token: 0x040005F5 RID: 1525
		private readonly Action<object, object> propertySetter;

		// Token: 0x040005F6 RID: 1526
		private readonly Action<object, string, object> dictionarySetter;

		// Token: 0x040005F7 RID: 1527
		private readonly Action<object, object> collectionAdd;

		// Token: 0x040005F8 RID: 1528
		private readonly Func<object, object, bool> collectionRemove;

		// Token: 0x040005F9 RID: 1529
		private readonly Func<object, object, bool> collectionContains;

		// Token: 0x040005FA RID: 1530
		private readonly Action<object> collectionClear;

		// Token: 0x040005FB RID: 1531
		private readonly Type collectionGenericType;

		// Token: 0x040005FC RID: 1532
		private bool? isPrimitiveOrEnumOrComplexCollection;

		// Token: 0x040005FD RID: 1533
		private bool? isComplex;

		// Token: 0x040005FE RID: 1534
		private bool? isComplexCollection;

		// Token: 0x040005FF RID: 1535
		private bool? isResourceSet;

		// Token: 0x04000600 RID: 1536
		private bool? isSpatialType;

		// Token: 0x04000601 RID: 1537
		private ClientPropertyAnnotation mimeTypeProperty;
	}
}
