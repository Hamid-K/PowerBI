using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200008C RID: 140
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonDictionaryContract : JsonContainerContract
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001C7F2 File Offset: 0x0001A9F2
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001C7FA File Offset: 0x0001A9FA
		[Nullable(new byte[] { 2, 0, 0 })]
		public Func<string, string> DictionaryKeyResolver
		{
			[return: Nullable(new byte[] { 2, 0, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0, 0 })]
			set;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001C803 File Offset: 0x0001AA03
		public Type DictionaryKeyType { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001C80B File Offset: 0x0001AA0B
		public Type DictionaryValueType { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001C813 File Offset: 0x0001AA13
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001C81B File Offset: 0x0001AA1B
		internal JsonContract KeyContract { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001C824 File Offset: 0x0001AA24
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001C82C File Offset: 0x0001AA2C
		[Nullable(new byte[] { 2, 0 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				if (this._parameterizedCreator == null && this._parameterizedConstructor != null)
				{
					this._parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(this._parameterizedConstructor);
				}
				return this._parameterizedCreator;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001C860 File Offset: 0x0001AA60
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001C868 File Offset: 0x0001AA68
		[Nullable(new byte[] { 2, 0 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 0 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001C871 File Offset: 0x0001AA71
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001C879 File Offset: 0x0001AA79
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001C882 File Offset: 0x0001AA82
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		[NullableContext(0)]
		public JsonDictionaryContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Dictionary;
			Type type;
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IDictionary<, >), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IDictionary<, >)))
				{
					base.CreatedType = typeof(Dictionary<, >).MakeGenericType(new Type[] { type, type2 });
				}
				else if (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.ConcurrentDictionary`2")
				{
					this.ShouldCreateWrapper = true;
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(this.NonNullableUnderlyingType, typeof(ReadOnlyDictionary<, >));
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >)))
				{
					base.CreatedType = typeof(ReadOnlyDictionary<, >).MakeGenericType(new Type[] { type, type2 });
				}
				this.IsReadOnlyOrFixedSize = true;
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(this.NonNullableUnderlyingType, out type, out type2);
				if (this.NonNullableUnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (type != null && type2 != null)
			{
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(KeyValuePair<, >).MakeGenericType(new Type[] { type, type2 }), typeof(IDictionary<, >).MakeGenericType(new Type[] { type, type2 }));
				if (!this.HasParameterizedCreatorInternal && this.NonNullableUnderlyingType.Name == "FSharpMap`2")
				{
					FSharpUtils.EnsureInitialized(this.NonNullableUnderlyingType.Assembly());
					this._parameterizedCreator = FSharpUtils.Instance.CreateMap(type, type2);
				}
			}
			if (!typeof(IDictionary).IsAssignableFrom(base.CreatedType))
			{
				this.ShouldCreateWrapper = true;
			}
			this.DictionaryKeyType = type;
			this.DictionaryValueType = type2;
			Type type3;
			ObjectConstructor<object> objectConstructor;
			if (this.DictionaryKeyType != null && this.DictionaryValueType != null && ImmutableCollectionsUtils.TryBuildImmutableForDictionaryContract(this.NonNullableUnderlyingType, this.DictionaryKeyType, this.DictionaryValueType, out type3, out objectConstructor))
			{
				base.CreatedType = type3;
				this._parameterizedCreator = objectConstructor;
				this.IsReadOnlyOrFixedSize = true;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001CB58 File Offset: 0x0001AD58
		[NullableContext(0)]
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(new Type[] { this.DictionaryKeyType, this.DictionaryValueType });
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[] { this._genericCollectionDefinitionType });
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedDictionary)this._genericWrapperCreator(new object[] { dictionary });
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
		[NullableContext(0)]
		internal IDictionary CreateTemporaryDictionary()
		{
			if (this._genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary<, >).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType ?? typeof(object),
					this.DictionaryValueType ?? typeof(object)
				});
				this._genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)this._genericTemporaryDictionaryCreator();
		}

		// Token: 0x04000268 RID: 616
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x04000269 RID: 617
		private Type _genericWrapperType;

		// Token: 0x0400026A RID: 618
		[Nullable(new byte[] { 2, 0 })]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x0400026B RID: 619
		[Nullable(new byte[] { 2, 0 })]
		private Func<object> _genericTemporaryDictionaryCreator;

		// Token: 0x0400026D RID: 621
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x0400026E RID: 622
		[Nullable(new byte[] { 2, 0 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x0400026F RID: 623
		[Nullable(new byte[] { 2, 0 })]
		private ObjectConstructor<object> _parameterizedCreator;
	}
}
