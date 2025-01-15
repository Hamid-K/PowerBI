using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Reflection;
using System.Text.Json.Serialization.Converters;
using System.Threading;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	[Nullable(0)]
	public class DefaultJsonTypeInfoResolver : IJsonTypeInfoResolver, IBuiltInJsonTypeInfoResolver
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x000273D8 File Offset: 0x000255D8
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonConverterFactory[] GetDefaultFactoryConverters()
		{
			return new JsonConverterFactory[]
			{
				new UnsupportedTypeConverterFactory(),
				new NullableConverterFactory(),
				new EnumConverterFactory(),
				new JsonNodeConverterFactory(),
				new FSharpTypeConverterFactory(),
				new MemoryConverterFactory(),
				new IAsyncEnumerableConverterFactory(),
				new IEnumerableConverterFactory(),
				new ObjectConverterFactory(true)
			};
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00027438 File Offset: 0x00025638
		private static Dictionary<Type, JsonConverter> GetDefaultSimpleConverters()
		{
			DefaultJsonTypeInfoResolver.<>c__DisplayClass3_0 CS$<>8__locals1;
			CS$<>8__locals1.converters = new Dictionary<Type, JsonConverter>(31);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.BooleanConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.ByteConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.ByteArrayConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.CharConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.DateTimeConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.DateTimeOffsetConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.DoubleConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.DecimalConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.GuidConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.Int16Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.Int32Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.Int64Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.JsonElementConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.JsonDocumentConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.MemoryByteConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.ReadOnlyMemoryByteConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.ObjectConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.SByteConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.SingleConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.StringConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.TimeSpanConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.UInt16Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.UInt32Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.UInt64Converter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.UriConverter, ref CS$<>8__locals1);
			DefaultJsonTypeInfoResolver.<GetDefaultSimpleConverters>g__Add|3_0(JsonMetadataServices.VersionConverter, ref CS$<>8__locals1);
			return CS$<>8__locals1.converters;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027594 File Offset: 0x00025794
		private static JsonConverter GetBuiltInConverter(Type typeToConvert)
		{
			JsonConverter jsonConverter;
			if (DefaultJsonTypeInfoResolver.s_defaultSimpleConverters.TryGetValue(typeToConvert, out jsonConverter))
			{
				return jsonConverter;
			}
			foreach (JsonConverterFactory jsonConverterFactory in DefaultJsonTypeInfoResolver.s_defaultFactoryConverters)
			{
				if (jsonConverterFactory.CanConvert(typeToConvert))
				{
					jsonConverter = jsonConverterFactory;
					break;
				}
			}
			return jsonConverter;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000275D8 File Offset: 0x000257D8
		internal static bool TryGetDefaultSimpleConverter(Type typeToConvert, [NotNullWhen(true)] out JsonConverter converter)
		{
			if (DefaultJsonTypeInfoResolver.s_defaultSimpleConverters == null)
			{
				converter = null;
				return false;
			}
			return DefaultJsonTypeInfoResolver.s_defaultSimpleConverters.TryGetValue(typeToConvert, out converter);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000275F4 File Offset: 0x000257F4
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonConverter GetCustomConverterForMember(Type typeToConvert, MemberInfo memberInfo, JsonSerializerOptions options)
		{
			JsonConverterAttribute uniqueCustomAttribute = memberInfo.GetUniqueCustomAttribute(false);
			if (uniqueCustomAttribute != null)
			{
				return DefaultJsonTypeInfoResolver.GetConverterFromAttribute(uniqueCustomAttribute, typeToConvert, memberInfo, options);
			}
			return null;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00027618 File Offset: 0x00025818
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal static JsonConverter GetConverterForType(Type typeToConvert, JsonSerializerOptions options, bool resolveJsonConverterAttribute = true)
		{
			DefaultJsonTypeInfoResolver.RootDefaultInstance();
			JsonConverter jsonConverter = options.GetConverterFromList(typeToConvert);
			if (resolveJsonConverterAttribute && jsonConverter == null)
			{
				JsonConverterAttribute uniqueCustomAttribute = typeToConvert.GetUniqueCustomAttribute(false);
				if (uniqueCustomAttribute != null)
				{
					jsonConverter = DefaultJsonTypeInfoResolver.GetConverterFromAttribute(uniqueCustomAttribute, typeToConvert, null, options);
				}
			}
			if (jsonConverter == null)
			{
				jsonConverter = DefaultJsonTypeInfoResolver.GetBuiltInConverter(typeToConvert);
			}
			jsonConverter = options.ExpandConverterFactory(jsonConverter, typeToConvert);
			if (!jsonConverter.Type.IsInSubtypeRelationshipWith(typeToConvert))
			{
				ThrowHelper.ThrowInvalidOperationException_SerializationConverterNotCompatible(jsonConverter.GetType(), typeToConvert);
			}
			JsonSerializerOptions.CheckConverterNullabilityIsSameAsPropertyType(jsonConverter, typeToConvert);
			return jsonConverter;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00027684 File Offset: 0x00025884
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonConverter GetConverterFromAttribute(JsonConverterAttribute converterAttribute, Type typeToConvert, MemberInfo memberInfo, JsonSerializerOptions options)
		{
			Type type = ((memberInfo != null) ? memberInfo.DeclaringType : null) ?? typeToConvert;
			Type converterType = converterAttribute.ConverterType;
			JsonConverter jsonConverter;
			if (converterType == null)
			{
				jsonConverter = converterAttribute.CreateConverter(typeToConvert);
				if (jsonConverter == null)
				{
					ThrowHelper.ThrowInvalidOperationException_SerializationConverterOnAttributeNotCompatible(type, memberInfo, typeToConvert);
				}
			}
			else
			{
				ConstructorInfo constructor = converterType.GetConstructor(Type.EmptyTypes);
				if (!typeof(JsonConverter).IsAssignableFrom(converterType) || constructor == null || !constructor.IsPublic)
				{
					ThrowHelper.ThrowInvalidOperationException_SerializationConverterOnAttributeInvalid(type, memberInfo);
				}
				jsonConverter = (JsonConverter)Activator.CreateInstance(converterType);
			}
			if (!jsonConverter.CanConvert(typeToConvert))
			{
				Type underlyingType = Nullable.GetUnderlyingType(typeToConvert);
				if (underlyingType != null && jsonConverter.CanConvert(underlyingType))
				{
					JsonConverterFactory jsonConverterFactory = jsonConverter as JsonConverterFactory;
					if (jsonConverterFactory != null)
					{
						jsonConverter = jsonConverterFactory.GetConverterInternal(underlyingType, options);
					}
					return NullableConverterFactory.CreateValueConverter(underlyingType, jsonConverter);
				}
				ThrowHelper.ThrowInvalidOperationException_SerializationConverterOnAttributeNotCompatible(type, memberInfo, typeToConvert);
			}
			return jsonConverter;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00027758 File Offset: 0x00025958
		internal static MemberAccessor MemberAccessor
		{
			[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
			get
			{
				MemberAccessor memberAccessor;
				if ((memberAccessor = DefaultJsonTypeInfoResolver.s_memberAccessor) == null)
				{
					memberAccessor = (DefaultJsonTypeInfoResolver.s_memberAccessor = new ReflectionEmitCachingMemberAccessor());
				}
				return memberAccessor;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00027770 File Offset: 0x00025970
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonTypeInfo CreateTypeInfoCore(Type type, JsonConverter converter, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo = JsonTypeInfo.CreateJsonTypeInfo(type, converter, options);
			JsonNumberHandling? numberHandlingForType = DefaultJsonTypeInfoResolver.GetNumberHandlingForType(jsonTypeInfo.Type);
			if (numberHandlingForType != null)
			{
				JsonNumberHandling valueOrDefault = numberHandlingForType.GetValueOrDefault();
				jsonTypeInfo.NumberHandling = new JsonNumberHandling?(valueOrDefault);
			}
			JsonObjectCreationHandling? objectCreationHandlingForType = DefaultJsonTypeInfoResolver.GetObjectCreationHandlingForType(jsonTypeInfo.Type);
			if (objectCreationHandlingForType != null)
			{
				JsonObjectCreationHandling valueOrDefault2 = objectCreationHandlingForType.GetValueOrDefault();
				jsonTypeInfo.PreferredPropertyObjectCreationHandling = new JsonObjectCreationHandling?(valueOrDefault2);
			}
			JsonUnmappedMemberHandling? unmappedMemberHandling = DefaultJsonTypeInfoResolver.GetUnmappedMemberHandling(jsonTypeInfo.Type);
			if (unmappedMemberHandling != null)
			{
				JsonUnmappedMemberHandling valueOrDefault3 = unmappedMemberHandling.GetValueOrDefault();
				jsonTypeInfo.UnmappedMemberHandling = new JsonUnmappedMemberHandling?(valueOrDefault3);
			}
			jsonTypeInfo.PopulatePolymorphismMetadata();
			jsonTypeInfo.MapInterfaceTypesToCallbacks();
			Func<object> func = DefaultJsonTypeInfoResolver.DetermineCreateObjectDelegate(type, converter);
			jsonTypeInfo.SetCreateObjectIfCompatible(func);
			jsonTypeInfo.CreateObjectForExtensionDataProperty = func;
			if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object)
			{
				DefaultJsonTypeInfoResolver.PopulateProperties(jsonTypeInfo);
				if (converter.ConstructorIsParameterized)
				{
					DefaultJsonTypeInfoResolver.PopulateParameterInfoValues(jsonTypeInfo);
				}
			}
			converter.ConfigureJsonTypeInfo(jsonTypeInfo, options);
			converter.ConfigureJsonTypeInfoUsingReflection(jsonTypeInfo, options);
			return jsonTypeInfo;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00027858 File Offset: 0x00025A58
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static void PopulateProperties(JsonTypeInfo typeInfo)
		{
			ConstructorInfo constructorInfo = typeInfo.Converter.ConstructorInfo;
			bool flag = constructorInfo != null && constructorInfo.HasSetsRequiredMembersAttribute();
			JsonTypeInfo.PropertyHierarchyResolutionState propertyHierarchyResolutionState = new JsonTypeInfo.PropertyHierarchyResolutionState(typeInfo.Options);
			foreach (Type type in typeInfo.Type.GetSortedTypeHierarchy())
			{
				if (type == JsonTypeInfo.ObjectType || type == typeof(ValueType))
				{
					break;
				}
				DefaultJsonTypeInfoResolver.AddMembersDeclaredBySuperType(typeInfo, type, flag, ref propertyHierarchyResolutionState);
			}
			if (propertyHierarchyResolutionState.IsPropertyOrderSpecified)
			{
				typeInfo.PropertyList.SortProperties();
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000278E8 File Offset: 0x00025AE8
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static void AddMembersDeclaredBySuperType(JsonTypeInfo typeInfo, Type currentType, bool constructorHasSetsRequiredMembersAttribute, ref JsonTypeInfo.PropertyHierarchyResolutionState state)
		{
			bool flag = !constructorHasSetsRequiredMembersAttribute && currentType.HasRequiredMemberAttribute();
			foreach (PropertyInfo propertyInfo in currentType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (propertyInfo.GetIndexParameters().Length == 0 && !DefaultJsonTypeInfoResolver.PropertyIsOverriddenAndIgnored(propertyInfo, state.IgnoredProperties))
				{
					bool flag2 = propertyInfo.GetCustomAttribute(false) != null;
					MethodInfo getMethod = propertyInfo.GetMethod;
					bool flag3;
					if (getMethod == null || !getMethod.IsPublic)
					{
						MethodInfo setMethod = propertyInfo.SetMethod;
						flag3 = setMethod != null && setMethod.IsPublic;
					}
					else
					{
						flag3 = true;
					}
					if (flag3 || flag2)
					{
						DefaultJsonTypeInfoResolver.AddMember(typeInfo, propertyInfo.PropertyType, propertyInfo, flag, flag2, ref state);
					}
				}
			}
			foreach (FieldInfo fieldInfo in currentType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				bool flag4 = fieldInfo.GetCustomAttribute(false) != null;
				if (flag4 || (fieldInfo.IsPublic && typeInfo.Options.IncludeFields))
				{
					DefaultJsonTypeInfoResolver.AddMember(typeInfo, fieldInfo.FieldType, fieldInfo, flag, flag4, ref state);
				}
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000279E0 File Offset: 0x00025BE0
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static void AddMember(JsonTypeInfo typeInfo, Type typeToConvert, MemberInfo memberInfo, bool shouldCheckForRequiredKeyword, bool hasJsonIncludeAttribute, ref JsonTypeInfo.PropertyHierarchyResolutionState state)
		{
			JsonPropertyInfo jsonPropertyInfo = DefaultJsonTypeInfoResolver.CreatePropertyInfo(typeInfo, typeToConvert, memberInfo, typeInfo.Options, shouldCheckForRequiredKeyword, hasJsonIncludeAttribute);
			if (jsonPropertyInfo == null)
			{
				return;
			}
			typeInfo.PropertyList.AddPropertyWithConflictResolution(jsonPropertyInfo, ref state);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00027A14 File Offset: 0x00025C14
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonPropertyInfo CreatePropertyInfo(JsonTypeInfo typeInfo, Type typeToConvert, MemberInfo memberInfo, JsonSerializerOptions options, bool shouldCheckForRequiredKeyword, bool hasJsonIncludeAttribute)
		{
			JsonIgnoreAttribute customAttribute = memberInfo.GetCustomAttribute(false);
			JsonIgnoreCondition? jsonIgnoreCondition = ((customAttribute != null) ? new JsonIgnoreCondition?(customAttribute.Condition) : null);
			JsonIgnoreCondition? jsonIgnoreCondition2;
			JsonIgnoreCondition jsonIgnoreCondition3;
			if (JsonTypeInfo.IsInvalidForSerialization(typeToConvert))
			{
				jsonIgnoreCondition2 = jsonIgnoreCondition;
				jsonIgnoreCondition3 = JsonIgnoreCondition.Always;
				if ((jsonIgnoreCondition2.GetValueOrDefault() == jsonIgnoreCondition3) & (jsonIgnoreCondition2 != null))
				{
					return null;
				}
				ThrowHelper.ThrowInvalidOperationException_CannotSerializeInvalidType(typeToConvert, memberInfo.DeclaringType, memberInfo);
			}
			JsonConverter customConverterForMember;
			object obj;
			bool flag;
			try
			{
				customConverterForMember = DefaultJsonTypeInfoResolver.GetCustomConverterForMember(typeToConvert, memberInfo, options);
			}
			catch when (delegate
			{
				// Failed to create a 'catch-when' expression
				if (!(obj is InvalidOperationException))
				{
					flag = false;
				}
				else
				{
					jsonIgnoreCondition2 = jsonIgnoreCondition;
					jsonIgnoreCondition3 = JsonIgnoreCondition.Always;
					flag = ((jsonIgnoreCondition2.GetValueOrDefault() == jsonIgnoreCondition3) & (jsonIgnoreCondition2 != null)) > false;
				}
				endfilter(flag);
			})
			{
				return null;
			}
			JsonPropertyInfo jsonPropertyInfo = typeInfo.CreatePropertyUsingReflection(typeToConvert, memberInfo.DeclaringType);
			DefaultJsonTypeInfoResolver.PopulatePropertyInfo(jsonPropertyInfo, memberInfo, customConverterForMember, jsonIgnoreCondition, shouldCheckForRequiredKeyword, hasJsonIncludeAttribute);
			return jsonPropertyInfo;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00027AE0 File Offset: 0x00025CE0
		private static JsonNumberHandling? GetNumberHandlingForType(Type type)
		{
			JsonNumberHandlingAttribute uniqueCustomAttribute = type.GetUniqueCustomAttribute(false);
			if (uniqueCustomAttribute == null)
			{
				return null;
			}
			return new JsonNumberHandling?(uniqueCustomAttribute.Handling);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027B10 File Offset: 0x00025D10
		private static JsonObjectCreationHandling? GetObjectCreationHandlingForType(Type type)
		{
			JsonObjectCreationHandlingAttribute uniqueCustomAttribute = type.GetUniqueCustomAttribute(false);
			if (uniqueCustomAttribute == null)
			{
				return null;
			}
			return new JsonObjectCreationHandling?(uniqueCustomAttribute.Handling);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00027B40 File Offset: 0x00025D40
		private static JsonUnmappedMemberHandling? GetUnmappedMemberHandling(Type type)
		{
			JsonUnmappedMemberHandlingAttribute uniqueCustomAttribute = type.GetUniqueCustomAttribute(false);
			if (uniqueCustomAttribute == null)
			{
				return null;
			}
			return new JsonUnmappedMemberHandling?(uniqueCustomAttribute.UnmappedMemberHandling);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00027B70 File Offset: 0x00025D70
		private static bool PropertyIsOverriddenAndIgnored(PropertyInfo propertyInfo, Dictionary<string, JsonPropertyInfo> ignoredMembers)
		{
			JsonPropertyInfo jsonPropertyInfo;
			return propertyInfo.IsVirtual() && ignoredMembers != null && ignoredMembers.TryGetValue(propertyInfo.Name, out jsonPropertyInfo) && jsonPropertyInfo.IsVirtual && propertyInfo.PropertyType == jsonPropertyInfo.PropertyType;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00027BB4 File Offset: 0x00025DB4
		private static void PopulateParameterInfoValues(JsonTypeInfo typeInfo)
		{
			ParameterInfo[] parameters = typeInfo.Converter.ConstructorInfo.GetParameters();
			int num = parameters.Length;
			JsonParameterInfoValues[] array = new JsonParameterInfoValues[num];
			for (int i = 0; i < num; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (string.IsNullOrEmpty(parameterInfo.Name))
				{
					ThrowHelper.ThrowNotSupportedException_ConstructorContainsNullParameterNames(typeInfo.Converter.ConstructorInfo.DeclaringType);
				}
				JsonParameterInfoValues jsonParameterInfoValues = new JsonParameterInfoValues
				{
					Name = parameterInfo.Name,
					ParameterType = parameterInfo.ParameterType,
					Position = parameterInfo.Position,
					HasDefaultValue = parameterInfo.HasDefaultValue,
					DefaultValue = parameterInfo.GetDefaultValue()
				};
				array[i] = jsonParameterInfoValues;
			}
			typeInfo.ParameterInfoValues = array;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00027C68 File Offset: 0x00025E68
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static void PopulatePropertyInfo(JsonPropertyInfo jsonPropertyInfo, MemberInfo memberInfo, JsonConverter customConverter, JsonIgnoreCondition? ignoreCondition, bool shouldCheckForRequiredKeyword, bool hasJsonIncludeAttribute)
		{
			jsonPropertyInfo.AttributeProvider = memberInfo;
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo == null)
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					jsonPropertyInfo.MemberName = fieldInfo.Name;
					jsonPropertyInfo.MemberType = MemberTypes.Field;
				}
			}
			else
			{
				jsonPropertyInfo.MemberName = propertyInfo.Name;
				jsonPropertyInfo.IsVirtual = propertyInfo.IsVirtual();
				jsonPropertyInfo.MemberType = MemberTypes.Property;
			}
			jsonPropertyInfo.CustomConverter = customConverter;
			DefaultJsonTypeInfoResolver.DeterminePropertyPolicies(jsonPropertyInfo, memberInfo);
			DefaultJsonTypeInfoResolver.DeterminePropertyName(jsonPropertyInfo, memberInfo);
			DefaultJsonTypeInfoResolver.DeterminePropertyIsRequired(jsonPropertyInfo, memberInfo, shouldCheckForRequiredKeyword);
			JsonIgnoreCondition? jsonIgnoreCondition = ignoreCondition;
			JsonIgnoreCondition jsonIgnoreCondition2 = JsonIgnoreCondition.Always;
			if (!((jsonIgnoreCondition.GetValueOrDefault() == jsonIgnoreCondition2) & (jsonIgnoreCondition != null)))
			{
				jsonPropertyInfo.DetermineReflectionPropertyAccessors(memberInfo, hasJsonIncludeAttribute);
			}
			jsonPropertyInfo.IgnoreCondition = ignoreCondition;
			jsonPropertyInfo.IsExtensionData = memberInfo.GetCustomAttribute(false) != null;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00027D24 File Offset: 0x00025F24
		private static void DeterminePropertyPolicies(JsonPropertyInfo propertyInfo, MemberInfo memberInfo)
		{
			JsonPropertyOrderAttribute customAttribute = memberInfo.GetCustomAttribute(false);
			propertyInfo.Order = ((customAttribute != null) ? customAttribute.Order : 0);
			JsonNumberHandlingAttribute customAttribute2 = memberInfo.GetCustomAttribute(false);
			propertyInfo.NumberHandling = ((customAttribute2 != null) ? new JsonNumberHandling?(customAttribute2.Handling) : null);
			JsonObjectCreationHandlingAttribute customAttribute3 = memberInfo.GetCustomAttribute(false);
			propertyInfo.ObjectCreationHandling = ((customAttribute3 != null) ? new JsonObjectCreationHandling?(customAttribute3.Handling) : null);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00027D9C File Offset: 0x00025F9C
		private static void DeterminePropertyName(JsonPropertyInfo propertyInfo, MemberInfo memberInfo)
		{
			JsonPropertyNameAttribute customAttribute = memberInfo.GetCustomAttribute(false);
			string text;
			if (customAttribute != null)
			{
				text = customAttribute.Name;
			}
			else if (propertyInfo.Options.PropertyNamingPolicy != null)
			{
				text = propertyInfo.Options.PropertyNamingPolicy.ConvertName(memberInfo.Name);
			}
			else
			{
				text = memberInfo.Name;
			}
			if (text == null)
			{
				ThrowHelper.ThrowInvalidOperationException_SerializerPropertyNameNull(propertyInfo);
			}
			propertyInfo.Name = text;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00027DFA File Offset: 0x00025FFA
		private static void DeterminePropertyIsRequired(JsonPropertyInfo propertyInfo, MemberInfo memberInfo, bool shouldCheckForRequiredKeyword)
		{
			propertyInfo.IsRequired = memberInfo.GetCustomAttribute(false) != null || (shouldCheckForRequiredKeyword && memberInfo.HasRequiredMemberAttribute());
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00027E1C File Offset: 0x0002601C
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal static void DeterminePropertyAccessors<T>(JsonPropertyInfo<T> jsonPropertyInfo, MemberInfo memberInfo, bool useNonPublicAccessors)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo == null)
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo == null)
				{
					return;
				}
				jsonPropertyInfo.Get = DefaultJsonTypeInfoResolver.MemberAccessor.CreateFieldGetter<T>(fieldInfo);
				if (!fieldInfo.IsInitOnly)
				{
					jsonPropertyInfo.Set = DefaultJsonTypeInfoResolver.MemberAccessor.CreateFieldSetter<T>(fieldInfo);
				}
			}
			else
			{
				MethodInfo getMethod = propertyInfo.GetMethod;
				if (getMethod != null && (getMethod.IsPublic || useNonPublicAccessors))
				{
					jsonPropertyInfo.Get = DefaultJsonTypeInfoResolver.MemberAccessor.CreatePropertyGetter<T>(propertyInfo);
				}
				MethodInfo setMethod = propertyInfo.SetMethod;
				if (setMethod != null && (setMethod.IsPublic || useNonPublicAccessors))
				{
					jsonPropertyInfo.Set = DefaultJsonTypeInfoResolver.MemberAccessor.CreatePropertySetter<T>(propertyInfo);
					return;
				}
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00027EC0 File Offset: 0x000260C0
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static Func<object> DetermineCreateObjectDelegate(Type type, JsonConverter converter)
		{
			ConstructorInfo constructorInfo = null;
			if (converter.ConstructorInfo != null && !converter.ConstructorIsParameterized)
			{
				constructorInfo = converter.ConstructorInfo;
			}
			if (constructorInfo == null)
			{
				constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
			}
			return DefaultJsonTypeInfoResolver.MemberAccessor.CreateParameterlessConstructor(type, constructorInfo);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00027F0B File Offset: 0x0002610B
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public DefaultJsonTypeInfoResolver()
			: this(true)
		{
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00027F14 File Offset: 0x00026114
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private DefaultJsonTypeInfoResolver(bool mutable)
		{
			this._mutable = mutable;
			if (DefaultJsonTypeInfoResolver.s_defaultFactoryConverters == null)
			{
				DefaultJsonTypeInfoResolver.s_defaultFactoryConverters = DefaultJsonTypeInfoResolver.GetDefaultFactoryConverters();
			}
			if (DefaultJsonTypeInfoResolver.s_defaultSimpleConverters == null)
			{
				DefaultJsonTypeInfoResolver.s_defaultSimpleConverters = DefaultJsonTypeInfoResolver.GetDefaultSimpleConverters();
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00027F48 File Offset: 0x00026148
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		[UnconditionalSuppressMessage("AotAnalysis", "IL3050:RequiresDynamicCode", Justification = "The ctor is marked RequiresDynamicCode.")]
		public virtual JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException("type");
			}
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			this._mutable = false;
			JsonTypeInfo.ValidateType(type);
			JsonTypeInfo jsonTypeInfo = DefaultJsonTypeInfoResolver.CreateJsonTypeInfo(type, options);
			jsonTypeInfo.OriginatingResolver = this;
			jsonTypeInfo.IsCustomized = false;
			if (this._modifiers != null)
			{
				foreach (Action<JsonTypeInfo> action in this._modifiers)
				{
					action(jsonTypeInfo);
				}
			}
			return jsonTypeInfo;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00027FE8 File Offset: 0x000261E8
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonTypeInfo CreateJsonTypeInfo(Type type, JsonSerializerOptions options)
		{
			JsonConverter converterForType = DefaultJsonTypeInfoResolver.GetConverterForType(type, options, true);
			return DefaultJsonTypeInfoResolver.CreateTypeInfoCore(type, converterForType, options);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00028008 File Offset: 0x00026208
		public IList<Action<JsonTypeInfo>> Modifiers
		{
			get
			{
				DefaultJsonTypeInfoResolver.ModifierCollection modifierCollection;
				if ((modifierCollection = this._modifiers) == null)
				{
					modifierCollection = (this._modifiers = new DefaultJsonTypeInfoResolver.ModifierCollection(this));
				}
				return modifierCollection;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00028030 File Offset: 0x00026230
		bool IBuiltInJsonTypeInfoResolver.IsCompatibleWithOptions(JsonSerializerOptions _)
		{
			DefaultJsonTypeInfoResolver.ModifierCollection modifiers = this._modifiers;
			bool flag = modifiers == null || modifiers.Count == 0;
			return flag && base.GetType() == typeof(DefaultJsonTypeInfoResolver);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00028071 File Offset: 0x00026271
		internal static bool IsDefaultInstanceRooted
		{
			get
			{
				return DefaultJsonTypeInfoResolver.s_defaultInstance != null;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00028080 File Offset: 0x00026280
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal static DefaultJsonTypeInfoResolver RootDefaultInstance()
		{
			DefaultJsonTypeInfoResolver defaultJsonTypeInfoResolver = DefaultJsonTypeInfoResolver.s_defaultInstance;
			if (defaultJsonTypeInfoResolver != null)
			{
				return defaultJsonTypeInfoResolver;
			}
			DefaultJsonTypeInfoResolver defaultJsonTypeInfoResolver2 = new DefaultJsonTypeInfoResolver(false);
			DefaultJsonTypeInfoResolver defaultJsonTypeInfoResolver3 = Interlocked.CompareExchange<DefaultJsonTypeInfoResolver>(ref DefaultJsonTypeInfoResolver.s_defaultInstance, defaultJsonTypeInfoResolver2, null);
			return defaultJsonTypeInfoResolver3 ?? defaultJsonTypeInfoResolver2;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000280B2 File Offset: 0x000262B2
		[CompilerGenerated]
		internal static void <GetDefaultSimpleConverters>g__Add|3_0(JsonConverter converter, ref DefaultJsonTypeInfoResolver.<>c__DisplayClass3_0 A_1)
		{
			A_1.converters.Add(converter.Type, converter);
		}

		// Token: 0x04000312 RID: 786
		private static Dictionary<Type, JsonConverter> s_defaultSimpleConverters;

		// Token: 0x04000313 RID: 787
		private static JsonConverterFactory[] s_defaultFactoryConverters;

		// Token: 0x04000314 RID: 788
		private static MemberAccessor s_memberAccessor;

		// Token: 0x04000315 RID: 789
		private bool _mutable;

		// Token: 0x04000316 RID: 790
		private DefaultJsonTypeInfoResolver.ModifierCollection _modifiers;

		// Token: 0x04000317 RID: 791
		private static DefaultJsonTypeInfoResolver s_defaultInstance;

		// Token: 0x02000134 RID: 308
		private sealed class ModifierCollection : ConfigurationList<Action<JsonTypeInfo>>
		{
			// Token: 0x06000DDB RID: 3547 RVA: 0x00035EB6 File Offset: 0x000340B6
			public ModifierCollection(DefaultJsonTypeInfoResolver resolver)
				: base(null)
			{
				this._resolver = resolver;
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00035EC6 File Offset: 0x000340C6
			public override bool IsReadOnly
			{
				get
				{
					return !this._resolver._mutable;
				}
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x00035ED6 File Offset: 0x000340D6
			protected override void OnCollectionModifying()
			{
				if (!this._resolver._mutable)
				{
					ThrowHelper.ThrowInvalidOperationException_DefaultTypeInfoResolverImmutable();
				}
			}

			// Token: 0x040004CC RID: 1228
			private readonly DefaultJsonTypeInfoResolver _resolver;
		}
	}
}
