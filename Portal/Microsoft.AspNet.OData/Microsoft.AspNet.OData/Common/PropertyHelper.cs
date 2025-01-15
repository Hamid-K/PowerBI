using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000066 RID: 102
	internal class PropertyHelper
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public PropertyHelper(PropertyInfo property)
		{
			this.Name = property.Name;
			this._valueGetter = PropertyHelper.MakeFastPropertyGetter(property);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public static Action<TDeclaringType, object> MakeFastPropertySetter<TDeclaringType>(PropertyInfo propertyInfo) where TDeclaringType : class
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			Type reflectedType = TypeHelper.GetReflectedType(propertyInfo);
			Type parameterType = setMethod.GetParameters()[0].ParameterType;
			Delegate @delegate = setMethod.CreateDelegate(typeof(Action<, >).MakeGenericType(new Type[] { reflectedType, parameterType }));
			return (Action<TDeclaringType, object>)PropertyHelper._callPropertySetterOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, parameterType }).CreateDelegate(typeof(Action<TDeclaringType, object>), @delegate);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		public virtual string Name { get; protected set; }

		// Token: 0x060003D7 RID: 983 RVA: 0x0000CAF1 File Offset: 0x0000ACF1
		public object GetValue(object instance)
		{
			return this._valueGetter(instance);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000CAFF File Offset: 0x0000ACFF
		public static PropertyHelper[] GetProperties(object instance)
		{
			return PropertyHelper.GetProperties(instance, new Func<PropertyInfo, PropertyHelper>(PropertyHelper.CreateInstance), PropertyHelper._reflectionCache);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public static Func<object, object> MakeFastPropertyGetter(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			Type reflectedType = TypeHelper.GetReflectedType(getMethod);
			Type returnType = getMethod.ReturnType;
			Delegate delegate2;
			if (TypeHelper.IsValueType(reflectedType))
			{
				Delegate @delegate = getMethod.CreateDelegate(typeof(PropertyHelper.ByRefFunc<, >).MakeGenericType(new Type[] { reflectedType, returnType }));
				delegate2 = PropertyHelper._callPropertyGetterByReferenceOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, returnType }).CreateDelegate(typeof(Func<object, object>), @delegate);
			}
			else
			{
				Delegate delegate3 = getMethod.CreateDelegate(typeof(Func<, >).MakeGenericType(new Type[] { reflectedType, returnType }));
				delegate2 = PropertyHelper._callPropertyGetterOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, returnType }).CreateDelegate(typeof(Func<object, object>), delegate3);
			}
			return (Func<object, object>)delegate2;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		private static PropertyHelper CreateInstance(PropertyInfo property)
		{
			return new PropertyHelper(property);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		private static object CallPropertyGetter<TDeclaringType, TValue>(Func<TDeclaringType, TValue> getter, object @this)
		{
			return getter((TDeclaringType)((object)@this));
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000CC04 File Offset: 0x0000AE04
		private static object CallPropertyGetterByReference<TDeclaringType, TValue>(PropertyHelper.ByRefFunc<TDeclaringType, TValue> getter, object @this)
		{
			TDeclaringType tdeclaringType = (TDeclaringType)((object)@this);
			return getter(ref tdeclaringType);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000CC25 File Offset: 0x0000AE25
		private static void CallPropertySetter<TDeclaringType, TValue>(Action<TDeclaringType, TValue> setter, object @this, object value)
		{
			setter((TDeclaringType)((object)@this), (TValue)((object)value));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		protected static PropertyHelper[] GetProperties(object instance, Func<PropertyInfo, PropertyHelper> createPropertyHelper, ConcurrentDictionary<Type, PropertyHelper[]> cache)
		{
			Type type = instance.GetType();
			PropertyHelper[] array;
			if (!cache.TryGetValue(type, out array))
			{
				IEnumerable<PropertyInfo> enumerable = from prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					where prop.GetIndexParameters().Length == 0 && prop.GetMethod != null
					select prop;
				List<PropertyHelper> list = new List<PropertyHelper>();
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					PropertyHelper propertyHelper = createPropertyHelper(propertyInfo);
					list.Add(propertyHelper);
				}
				array = list.ToArray();
				cache.TryAdd(type, array);
			}
			return array;
		}

		// Token: 0x040000C2 RID: 194
		private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache = new ConcurrentDictionary<Type, PropertyHelper[]>();

		// Token: 0x040000C3 RID: 195
		private Func<object, object> _valueGetter;

		// Token: 0x040000C5 RID: 197
		private static readonly MethodInfo _callPropertyGetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000C6 RID: 198
		private static readonly MethodInfo _callPropertyGetterByReferenceOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetterByReference", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000C7 RID: 199
		private static readonly MethodInfo _callPropertySetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertySetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x02000201 RID: 513
		// (Invoke) Token: 0x06001025 RID: 4133
		private delegate TValue ByRefFunc<TDeclaringType, TValue>(ref TDeclaringType arg);
	}
}
