using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B3 RID: 179
	internal sealed class PolymorphicTypeResolver
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
		public PolymorphicTypeResolver(JsonSerializerOptions options, JsonPolymorphismOptions polymorphismOptions, Type baseType, bool converterCanHaveMetadata)
		{
			this.UnknownDerivedTypeHandling = polymorphismOptions.UnknownDerivedTypeHandling;
			this.IgnoreUnrecognizedTypeDiscriminators = polymorphismOptions.IgnoreUnrecognizedTypeDiscriminators;
			this.BaseType = baseType;
			this._options = options;
			if (!PolymorphicTypeResolver.IsSupportedPolymorphicBaseType(this.BaseType))
			{
				ThrowHelper.ThrowInvalidOperationException_TypeDoesNotSupportPolymorphism(this.BaseType);
			}
			bool flag = false;
			foreach (JsonDerivedType jsonDerivedType in polymorphismOptions.DerivedTypes)
			{
				Type type;
				object obj;
				jsonDerivedType.Deconstruct(out type, out obj);
				Type type2 = type;
				object obj2 = obj;
				if (!PolymorphicTypeResolver.IsSupportedDerivedType(this.BaseType, type2) || (type2.IsAbstract && this.UnknownDerivedTypeHandling != JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor))
				{
					ThrowHelper.ThrowInvalidOperationException_DerivedTypeNotSupported(this.BaseType, type2);
				}
				PolymorphicTypeResolver.DerivedJsonTypeInfo derivedJsonTypeInfo = new PolymorphicTypeResolver.DerivedJsonTypeInfo(type2, obj2);
				if (!this._typeToDiscriminatorId.TryAdd(type2, derivedJsonTypeInfo))
				{
					ThrowHelper.ThrowInvalidOperationException_DerivedTypeIsAlreadySpecified(this.BaseType, type2);
				}
				if (obj2 != null)
				{
					Dictionary<object, PolymorphicTypeResolver.DerivedJsonTypeInfo> dictionary;
					if ((dictionary = this._discriminatorIdtoType) == null)
					{
						dictionary = (this._discriminatorIdtoType = new Dictionary<object, PolymorphicTypeResolver.DerivedJsonTypeInfo>());
					}
					if (!dictionary.TryAdd(obj2, derivedJsonTypeInfo))
					{
						ThrowHelper.ThrowInvalidOperationException_TypeDicriminatorIdIsAlreadySpecified(this.BaseType, obj2);
					}
					this.UsesTypeDiscriminators = true;
				}
				flag = true;
			}
			if (!flag)
			{
				ThrowHelper.ThrowInvalidOperationException_PolymorphicTypeConfigurationDoesNotSpecifyDerivedTypes(this.BaseType);
			}
			if (this.UsesTypeDiscriminators)
			{
				if (!converterCanHaveMetadata)
				{
					ThrowHelper.ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata(this.BaseType);
				}
				string typeDiscriminatorPropertyName = polymorphismOptions.TypeDiscriminatorPropertyName;
				JsonEncodedText jsonEncodedText = ((typeDiscriminatorPropertyName == "$type") ? JsonSerializer.s_metadataType : JsonEncodedText.Encode(typeDiscriminatorPropertyName, options.Encoder));
				if ((JsonSerializer.GetMetadataPropertyName(jsonEncodedText.EncodedUtf8Bytes, null) & ~MetadataPropertyName.Type) != MetadataPropertyName.None)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidCustomTypeDiscriminatorPropertyName();
				}
				this.TypeDiscriminatorPropertyName = typeDiscriminatorPropertyName;
				this.TypeDiscriminatorPropertyNameUtf8 = jsonEncodedText.EncodedUtf8Bytes.ToArray();
				this.CustomTypeDiscriminatorPropertyNameJsonEncoded = new JsonEncodedText?(jsonEncodedText);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002C988 File Offset: 0x0002AB88
		public Type BaseType { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002C990 File Offset: 0x0002AB90
		public JsonUnknownDerivedTypeHandling UnknownDerivedTypeHandling { get; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002C998 File Offset: 0x0002AB98
		public bool UsesTypeDiscriminators { get; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
		public bool IgnoreUnrecognizedTypeDiscriminators { get; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		public string TypeDiscriminatorPropertyName { get; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		public byte[] TypeDiscriminatorPropertyNameUtf8 { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002C9B8 File Offset: 0x0002ABB8
		public JsonEncodedText? CustomTypeDiscriminatorPropertyNameJsonEncoded { get; }

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
		public bool TryGetDerivedJsonTypeInfo(Type runtimeType, [NotNullWhen(true)] out JsonTypeInfo jsonTypeInfo, out object typeDiscriminator)
		{
			PolymorphicTypeResolver.DerivedJsonTypeInfo derivedJsonTypeInfo;
			if (!this._typeToDiscriminatorId.TryGetValue(runtimeType, out derivedJsonTypeInfo))
			{
				switch (this.UnknownDerivedTypeHandling)
				{
				case JsonUnknownDerivedTypeHandling.FallBackToBaseType:
					this._typeToDiscriminatorId.TryGetValue(this.BaseType, out derivedJsonTypeInfo);
					this._typeToDiscriminatorId[runtimeType] = derivedJsonTypeInfo;
					goto IL_007F;
				case JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor:
					derivedJsonTypeInfo = this.CalculateNearestAncestor(runtimeType);
					this._typeToDiscriminatorId[runtimeType] = derivedJsonTypeInfo;
					goto IL_007F;
				}
				if (runtimeType != this.BaseType)
				{
					ThrowHelper.ThrowNotSupportedException_RuntimeTypeNotSupported(this.BaseType, runtimeType);
				}
			}
			IL_007F:
			if (derivedJsonTypeInfo == null)
			{
				jsonTypeInfo = null;
				typeDiscriminator = null;
				return false;
			}
			jsonTypeInfo = derivedJsonTypeInfo.GetJsonTypeInfo(this._options);
			typeDiscriminator = derivedJsonTypeInfo.TypeDiscriminator;
			return true;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public bool TryGetDerivedJsonTypeInfo(object typeDiscriminator, [NotNullWhen(true)] out JsonTypeInfo jsonTypeInfo)
		{
			PolymorphicTypeResolver.DerivedJsonTypeInfo derivedJsonTypeInfo;
			if (this._discriminatorIdtoType.TryGetValue(typeDiscriminator, out derivedJsonTypeInfo))
			{
				jsonTypeInfo = derivedJsonTypeInfo.GetJsonTypeInfo(this._options);
				return true;
			}
			if (!this.IgnoreUnrecognizedTypeDiscriminators)
			{
				ThrowHelper.ThrowJsonException_UnrecognizedTypeDiscriminator(typeDiscriminator);
			}
			jsonTypeInfo = null;
			return false;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		public static bool IsSupportedPolymorphicBaseType(Type type)
		{
			return type != null && (type.IsClass || type.IsInterface) && !type.IsSealed && !type.IsGenericTypeDefinition && !type.IsPointer && type != JsonTypeInfo.ObjectType;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002CAEF File Offset: 0x0002ACEF
		public static bool IsSupportedDerivedType(Type baseType, Type derivedType)
		{
			return baseType.IsAssignableFrom(derivedType) && !derivedType.IsGenericTypeDefinition;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002CB08 File Offset: 0x0002AD08
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "The call to GetInterfaces will cross-reference results with interface types already declared as derived types of the polymorphic base type.")]
		private PolymorphicTypeResolver.DerivedJsonTypeInfo CalculateNearestAncestor(Type type)
		{
			if (type == this.BaseType)
			{
				return null;
			}
			PolymorphicTypeResolver.DerivedJsonTypeInfo derivedJsonTypeInfo = null;
			Type type2 = type.BaseType;
			while (this.BaseType.IsAssignableFrom(type2) && !this._typeToDiscriminatorId.TryGetValue(type2, out derivedJsonTypeInfo))
			{
				type2 = type2.BaseType;
			}
			if (this.BaseType.IsInterface)
			{
				foreach (Type type3 in type.GetInterfaces())
				{
					PolymorphicTypeResolver.DerivedJsonTypeInfo derivedJsonTypeInfo2;
					if (type3 != this.BaseType && this.BaseType.IsAssignableFrom(type3) && this._typeToDiscriminatorId.TryGetValue(type3, out derivedJsonTypeInfo2) && derivedJsonTypeInfo2 != null)
					{
						if (derivedJsonTypeInfo == null)
						{
							derivedJsonTypeInfo = derivedJsonTypeInfo2;
						}
						else
						{
							ThrowHelper.ThrowNotSupportedException_RuntimeTypeDiamondAmbiguity(this.BaseType, type, derivedJsonTypeInfo.DerivedType, derivedJsonTypeInfo2.DerivedType);
						}
					}
				}
			}
			return derivedJsonTypeInfo;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075:UnrecognizedReflectionPattern", Justification = "The call to GetInterfaces will cross-reference results with interface types already declared as derived types of the polymorphic base type.")]
		internal static JsonTypeInfo FindNearestPolymorphicBaseType(JsonTypeInfo typeInfo)
		{
			if (typeInfo.PolymorphismOptions != null)
			{
				return null;
			}
			JsonTypeInfo jsonTypeInfo = null;
			Type type = typeInfo.Type.BaseType;
			while (type != null)
			{
				JsonTypeInfo jsonTypeInfo2 = PolymorphicTypeResolver.<FindNearestPolymorphicBaseType>g__ResolveAncestorTypeInfo|30_0(type, typeInfo.Options);
				if (((jsonTypeInfo2 != null) ? jsonTypeInfo2.PolymorphismOptions : null) != null)
				{
					jsonTypeInfo = jsonTypeInfo2;
					break;
				}
				type = type.BaseType;
			}
			foreach (Type type2 in typeInfo.Type.GetInterfaces())
			{
				JsonTypeInfo jsonTypeInfo3 = PolymorphicTypeResolver.<FindNearestPolymorphicBaseType>g__ResolveAncestorTypeInfo|30_0(type2, typeInfo.Options);
				if (((jsonTypeInfo3 != null) ? jsonTypeInfo3.PolymorphismOptions : null) != null)
				{
					if (jsonTypeInfo != null)
					{
						if (jsonTypeInfo.Type.IsAssignableFrom(type2))
						{
							jsonTypeInfo = jsonTypeInfo3;
						}
						else if (!type2.IsAssignableFrom(jsonTypeInfo.Type))
						{
							return null;
						}
					}
					else
					{
						jsonTypeInfo = jsonTypeInfo3;
					}
				}
			}
			return jsonTypeInfo;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002CC9C File Offset: 0x0002AE9C
		[CompilerGenerated]
		internal static JsonTypeInfo <FindNearestPolymorphicBaseType>g__ResolveAncestorTypeInfo|30_0(Type type, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo;
			try
			{
				jsonTypeInfo = options.GetTypeInfoInternal(type, true, null, false, false);
			}
			catch
			{
				jsonTypeInfo = null;
			}
			return jsonTypeInfo;
		}

		// Token: 0x040003E1 RID: 993
		private readonly ConcurrentDictionary<Type, PolymorphicTypeResolver.DerivedJsonTypeInfo> _typeToDiscriminatorId = new ConcurrentDictionary<Type, PolymorphicTypeResolver.DerivedJsonTypeInfo>();

		// Token: 0x040003E2 RID: 994
		private readonly Dictionary<object, PolymorphicTypeResolver.DerivedJsonTypeInfo> _discriminatorIdtoType;

		// Token: 0x040003E3 RID: 995
		private readonly JsonSerializerOptions _options;

		// Token: 0x0200014D RID: 333
		private sealed class DerivedJsonTypeInfo
		{
			// Token: 0x06000E16 RID: 3606 RVA: 0x00036D02 File Offset: 0x00034F02
			public DerivedJsonTypeInfo(Type type, object typeDiscriminator)
			{
				this.DerivedType = type;
				this.TypeDiscriminator = typeDiscriminator;
			}

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00036D18 File Offset: 0x00034F18
			public Type DerivedType { get; }

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00036D20 File Offset: 0x00034F20
			public object TypeDiscriminator { get; }

			// Token: 0x06000E19 RID: 3609 RVA: 0x00036D28 File Offset: 0x00034F28
			public JsonTypeInfo GetJsonTypeInfo(JsonSerializerOptions options)
			{
				JsonTypeInfo jsonTypeInfo;
				if ((jsonTypeInfo = this._jsonTypeInfo) == null)
				{
					jsonTypeInfo = (this._jsonTypeInfo = options.GetTypeInfoInternal(this.DerivedType, true, new bool?(true), false, false));
				}
				return jsonTypeInfo;
			}

			// Token: 0x04000522 RID: 1314
			private volatile JsonTypeInfo _jsonTypeInfo;
		}
	}
}
