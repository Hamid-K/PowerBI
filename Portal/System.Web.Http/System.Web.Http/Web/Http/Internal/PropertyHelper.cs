using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x0200017F RID: 383
	internal class PropertyHelper
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0001991A File Offset: 0x00017B1A
		public PropertyHelper(PropertyInfo property)
		{
			this.Name = property.Name;
			this._valueGetter = PropertyHelper.MakeFastPropertyGetter(property);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001993C File Offset: 0x00017B3C
		public static Action<TDeclaringType, object> MakeFastPropertySetter<TDeclaringType>(PropertyInfo propertyInfo) where TDeclaringType : class
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			Type reflectedType = propertyInfo.ReflectedType;
			Type parameterType = setMethod.GetParameters()[0].ParameterType;
			Delegate @delegate = setMethod.CreateDelegate(typeof(Action<, >).MakeGenericType(new Type[] { reflectedType, parameterType }));
			MethodInfo methodInfo = PropertyHelper._callPropertySetterOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, parameterType });
			return (Action<TDeclaringType, object>)Delegate.CreateDelegate(typeof(Action<TDeclaringType, object>), @delegate, methodInfo);
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000199B6 File Offset: 0x00017BB6
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000199BE File Offset: 0x00017BBE
		public virtual string Name { get; protected set; }

		// Token: 0x060009F2 RID: 2546 RVA: 0x000199C7 File Offset: 0x00017BC7
		public object GetValue(object instance)
		{
			return this._valueGetter(instance);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000199D5 File Offset: 0x00017BD5
		public static PropertyHelper[] GetProperties(object instance)
		{
			return PropertyHelper.GetProperties(instance, new Func<PropertyInfo, PropertyHelper>(PropertyHelper.CreateInstance), PropertyHelper._reflectionCache);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000199F0 File Offset: 0x00017BF0
		public static Func<object, object> MakeFastPropertyGetter(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			Type reflectedType = getMethod.ReflectedType;
			Type returnType = getMethod.ReturnType;
			Delegate delegate2;
			if (reflectedType.IsValueType)
			{
				Delegate @delegate = getMethod.CreateDelegate(typeof(PropertyHelper.ByRefFunc<, >).MakeGenericType(new Type[] { reflectedType, returnType }));
				MethodInfo methodInfo = PropertyHelper._callPropertyGetterByReferenceOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, returnType });
				delegate2 = Delegate.CreateDelegate(typeof(Func<object, object>), @delegate, methodInfo);
			}
			else
			{
				Delegate delegate3 = getMethod.CreateDelegate(typeof(Func<, >).MakeGenericType(new Type[] { reflectedType, returnType }));
				MethodInfo methodInfo2 = PropertyHelper._callPropertyGetterOpenGenericMethod.MakeGenericMethod(new Type[] { reflectedType, returnType });
				delegate2 = Delegate.CreateDelegate(typeof(Func<object, object>), delegate3, methodInfo2);
			}
			return (Func<object, object>)delegate2;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00019AC8 File Offset: 0x00017CC8
		private static PropertyHelper CreateInstance(PropertyInfo property)
		{
			return new PropertyHelper(property);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00019AD0 File Offset: 0x00017CD0
		private static object CallPropertyGetter<TDeclaringType, TValue>(Func<TDeclaringType, TValue> getter, object @this)
		{
			return getter((TDeclaringType)((object)@this));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00019AE4 File Offset: 0x00017CE4
		private static object CallPropertyGetterByReference<TDeclaringType, TValue>(PropertyHelper.ByRefFunc<TDeclaringType, TValue> getter, object @this)
		{
			TDeclaringType tdeclaringType = (TDeclaringType)((object)@this);
			return getter(ref tdeclaringType);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00019B05 File Offset: 0x00017D05
		private static void CallPropertySetter<TDeclaringType, TValue>(Action<TDeclaringType, TValue> setter, object @this, object value)
		{
			setter((TDeclaringType)((object)@this), (TValue)((object)value));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00019B1C File Offset: 0x00017D1C
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

		// Token: 0x040002AA RID: 682
		private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache = new ConcurrentDictionary<Type, PropertyHelper[]>();

		// Token: 0x040002AB RID: 683
		private Func<object, object> _valueGetter;

		// Token: 0x040002AD RID: 685
		private static readonly MethodInfo _callPropertyGetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040002AE RID: 686
		private static readonly MethodInfo _callPropertyGetterByReferenceOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetterByReference", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040002AF RID: 687
		private static readonly MethodInfo _callPropertySetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertySetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0200024C RID: 588
		// (Invoke) Token: 0x06000CBF RID: 3263
		private delegate TValue ByRefFunc<TDeclaringType, TValue>(ref TDeclaringType arg);
	}
}
