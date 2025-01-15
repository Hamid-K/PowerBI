using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B1 RID: 177
	internal abstract class MemberAccessor
	{
		// Token: 0x06000B18 RID: 2840
		public abstract Func<object> CreateParameterlessConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, ConstructorInfo constructorInfo);

		// Token: 0x06000B19 RID: 2841
		public abstract Func<object[], T> CreateParameterizedConstructor<T>(ConstructorInfo constructor);

		// Token: 0x06000B1A RID: 2842
		public abstract JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3> CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>(ConstructorInfo constructor);

		// Token: 0x06000B1B RID: 2843
		public abstract Action<TCollection, object> CreateAddMethodDelegate<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TCollection>();

		// Token: 0x06000B1C RID: 2844
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public abstract Func<IEnumerable<TElement>, TCollection> CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>();

		// Token: 0x06000B1D RID: 2845
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public abstract Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection> CreateImmutableDictionaryCreateRangeDelegate<TCollection, TKey, TValue>();

		// Token: 0x06000B1E RID: 2846
		public abstract Func<object, TProperty> CreatePropertyGetter<TProperty>(PropertyInfo propertyInfo);

		// Token: 0x06000B1F RID: 2847
		public abstract Action<object, TProperty> CreatePropertySetter<TProperty>(PropertyInfo propertyInfo);

		// Token: 0x06000B20 RID: 2848
		public abstract Func<object, TProperty> CreateFieldGetter<TProperty>(FieldInfo fieldInfo);

		// Token: 0x06000B21 RID: 2849
		public abstract Action<object, TProperty> CreateFieldSetter<TProperty>(FieldInfo fieldInfo);
	}
}
