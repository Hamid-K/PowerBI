using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AD RID: 173
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public abstract class JsonPropertyInfo
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00029BC9 File Offset: 0x00027DC9
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00029BD1 File Offset: 0x00027DD1
		[Nullable(2)]
		internal JsonTypeInfo ParentTypeInfo { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00029BDA File Offset: 0x00027DDA
		internal JsonConverter EffectiveConverter
		{
			get
			{
				return this._effectiveConverter;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00029BE2 File Offset: 0x00027DE2
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00029BEA File Offset: 0x00027DEA
		[Nullable(2)]
		public JsonConverter CustomConverter
		{
			[NullableContext(2)]
			get
			{
				return this._customConverter;
			}
			[NullableContext(2)]
			set
			{
				this.VerifyMutable();
				this._customConverter = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00029BF9 File Offset: 0x00027DF9
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x00029C01 File Offset: 0x00027E01
		[Nullable(new byte[] { 2, 1, 2 })]
		public Func<object, object> Get
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get
			{
				return this._untypedGet;
			}
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set
			{
				this.VerifyMutable();
				this.SetGetter(value);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00029C10 File Offset: 0x00027E10
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00029C18 File Offset: 0x00027E18
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> Set
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get
			{
				return this._untypedSet;
			}
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set
			{
				this.VerifyMutable();
				this.SetSetter(value);
				this._isUserSpecifiedSetter = true;
			}
		}

		// Token: 0x06000A0A RID: 2570
		private protected abstract void SetGetter(Delegate getter);

		// Token: 0x06000A0B RID: 2571
		private protected abstract void SetSetter(Delegate setter);

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00029C2E File Offset: 0x00027E2E
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00029C36 File Offset: 0x00027E36
		[Nullable(new byte[] { 2, 1, 2 })]
		public Func<object, object, bool> ShouldSerialize
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get
			{
				return this._shouldSerialize;
			}
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set
			{
				this.VerifyMutable();
				this.SetShouldSerialize(value);
				this._isUserSpecifiedShouldSerialize = true;
				this.IgnoreDefaultValuesOnWrite = false;
			}
		}

		// Token: 0x06000A0E RID: 2574
		private protected abstract void SetShouldSerialize(Delegate predicate);

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00029C53 File Offset: 0x00027E53
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00029C5B File Offset: 0x00027E5B
		internal JsonIgnoreCondition? IgnoreCondition
		{
			get
			{
				return this._ignoreCondition;
			}
			set
			{
				this.ConfigureIgnoreCondition(value);
				this._ignoreCondition = value;
			}
		}

		// Token: 0x06000A11 RID: 2577
		private protected abstract void ConfigureIgnoreCondition(JsonIgnoreCondition? ignoreCondition);

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00029C6B File Offset: 0x00027E6B
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00029C73 File Offset: 0x00027E73
		[Nullable(2)]
		public ICustomAttributeProvider AttributeProvider
		{
			[NullableContext(2)]
			get
			{
				return this._attributeProvider;
			}
			[NullableContext(2)]
			set
			{
				this.VerifyMutable();
				this._attributeProvider = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00029C82 File Offset: 0x00027E82
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00029C8A File Offset: 0x00027E8A
		internal JsonObjectCreationHandling EffectiveObjectCreationHandling { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00029C93 File Offset: 0x00027E93
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00029C9B File Offset: 0x00027E9B
		public JsonObjectCreationHandling? ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				this.VerifyMutable();
				if (value != null && !JsonSerializer.IsValidCreationHandlingValue(value.Value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00029CCC File Offset: 0x00027ECC
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00029CD4 File Offset: 0x00027ED4
		[Nullable(2)]
		internal string MemberName { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00029CDD File Offset: 0x00027EDD
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00029CE5 File Offset: 0x00027EE5
		internal MemberTypes MemberType { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00029CEE File Offset: 0x00027EEE
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00029CF6 File Offset: 0x00027EF6
		internal bool IsVirtual { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00029CFF File Offset: 0x00027EFF
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x00029D07 File Offset: 0x00027F07
		public bool IsExtensionData
		{
			get
			{
				return this._isExtensionDataProperty;
			}
			set
			{
				this.VerifyMutable();
				if (value && !JsonTypeInfo.IsValidExtensionDataProperty(this.PropertyType))
				{
					ThrowHelper.ThrowInvalidOperationException_SerializationDataExtensionPropertyInvalid(this);
				}
				this._isExtensionDataProperty = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00029D2C File Offset: 0x00027F2C
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00029D34 File Offset: 0x00027F34
		public bool IsRequired
		{
			get
			{
				return this._isRequired;
			}
			set
			{
				this.VerifyMutable();
				this._isRequired = value;
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00029D43 File Offset: 0x00027F43
		internal JsonPropertyInfo(Type declaringType, Type propertyType, JsonTypeInfo declaringTypeInfo, JsonSerializerOptions options)
		{
			this.DeclaringType = declaringType;
			this.PropertyType = propertyType;
			this.ParentTypeInfo = declaringTypeInfo;
			this.Options = options;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00029D68 File Offset: 0x00027F68
		internal static JsonPropertyInfo GetPropertyPlaceholder()
		{
			return new JsonPropertyInfo<object>(typeof(object), null, null)
			{
				Name = string.Empty
			};
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00029D93 File Offset: 0x00027F93
		public Type PropertyType { get; }

		// Token: 0x06000A25 RID: 2597 RVA: 0x00029D9B File Offset: 0x00027F9B
		private protected void VerifyMutable()
		{
			JsonTypeInfo parentTypeInfo = this.ParentTypeInfo;
			if (parentTypeInfo == null)
			{
				return;
			}
			parentTypeInfo.VerifyMutable();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00029DAD File Offset: 0x00027FAD
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00029DB5 File Offset: 0x00027FB5
		internal bool IsConfigured { get; private set; }

		// Token: 0x06000A28 RID: 2600 RVA: 0x00029DC0 File Offset: 0x00027FC0
		internal void Configure()
		{
			if (this.IsIgnored)
			{
				this.CanSerialize = false;
				this.CanDeserialize = false;
			}
			else
			{
				if (this._jsonTypeInfo == null)
				{
					this._jsonTypeInfo = this.Options.GetTypeInfoInternal(this.PropertyType, true, new bool?(true), false, false);
				}
				this._jsonTypeInfo.EnsureConfigured();
				this.DetermineEffectiveConverter(this._jsonTypeInfo);
				this.DetermineNumberHandlingForProperty();
				this.DetermineEffectiveObjectCreationHandlingForProperty();
				this.DetermineSerializationCapabilities();
				this.DetermineIgnoreCondition();
			}
			if (this.IsForTypeInfo)
			{
				this.DetermineNumberHandlingForTypeInfo();
			}
			else
			{
				this.CacheNameAsUtf8BytesAndEscapedNameSection();
			}
			if (this.IsRequired)
			{
				if (!this.CanDeserialize)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonPropertyRequiredAndNotDeserializable(this);
				}
				if (this.IsExtensionData)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonPropertyRequiredAndExtensionData(this);
				}
			}
			this.IsConfigured = true;
		}

		// Token: 0x06000A29 RID: 2601
		private protected abstract void DetermineEffectiveConverter(JsonTypeInfo jsonTypeInfo);

		// Token: 0x06000A2A RID: 2602
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal abstract void DetermineReflectionPropertyAccessors(MemberInfo memberInfo, bool useNonPublicAccessors);

		// Token: 0x06000A2B RID: 2603 RVA: 0x00029E7D File Offset: 0x0002807D
		private void CacheNameAsUtf8BytesAndEscapedNameSection()
		{
			this.NameAsUtf8Bytes = Encoding.UTF8.GetBytes(this.Name);
			this.EscapedNameSection = JsonHelpers.GetEscapedPropertyNameSection(this.NameAsUtf8Bytes, this.Options.Encoder);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00029EB8 File Offset: 0x000280B8
		private void DetermineIgnoreCondition()
		{
			if (this._ignoreCondition != null)
			{
				return;
			}
			if (this.Options.IgnoreNullValues)
			{
				if (this.PropertyTypeCanBeNull)
				{
					this.IgnoreNullTokensOnRead = !this._isUserSpecifiedSetter && !this.IsRequired;
					this.IgnoreDefaultValuesOnWrite = this.ShouldSerialize == null;
					return;
				}
			}
			else if (this.Options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull)
			{
				if (this.PropertyTypeCanBeNull)
				{
					this.IgnoreDefaultValuesOnWrite = this.ShouldSerialize == null;
					return;
				}
			}
			else if (this.Options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingDefault)
			{
				this.IgnoreDefaultValuesOnWrite = this.ShouldSerialize == null;
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00029F58 File Offset: 0x00028158
		private void DetermineSerializationCapabilities()
		{
			this.CanSerialize = this.HasGetter;
			this.CanDeserialize = this.HasSetter;
			if (this.MemberType == (MemberTypes)0 || this._ignoreCondition != null)
			{
				this.CanDeserializeOrPopulate = this.CanDeserialize || this.EffectiveObjectCreationHandling == JsonObjectCreationHandling.Populate;
				return;
			}
			if ((this.EffectiveConverter.ConverterStrategy & (ConverterStrategy)24) != ConverterStrategy.None)
			{
				if (this.Get == null && this.Set != null && !this._isUserSpecifiedSetter)
				{
					this.CanDeserialize = false;
				}
			}
			else if (this.Get != null && this.Set == null && this.IgnoreReadOnlyMember && !this._isUserSpecifiedShouldSerialize)
			{
				this.CanSerialize = false;
			}
			this.CanDeserializeOrPopulate = this.CanDeserialize || this.EffectiveObjectCreationHandling == JsonObjectCreationHandling.Populate;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002A020 File Offset: 0x00028220
		private void DetermineNumberHandlingForTypeInfo()
		{
			JsonNumberHandling? numberHandling = this.ParentTypeInfo.NumberHandling;
			if (numberHandling != null)
			{
				JsonNumberHandling? jsonNumberHandling = numberHandling;
				JsonNumberHandling jsonNumberHandling2 = JsonNumberHandling.Strict;
				if (!((jsonNumberHandling.GetValueOrDefault() == jsonNumberHandling2) & (jsonNumberHandling != null)) && !this.EffectiveConverter.IsInternalConverter)
				{
					ThrowHelper.ThrowInvalidOperationException_NumberHandlingOnPropertyInvalid(this);
				}
			}
			if (this.NumberHandingIsApplicable())
			{
				this.EffectiveNumberHandling = numberHandling;
				if (this.EffectiveNumberHandling == null && this.Options.NumberHandling != JsonNumberHandling.Strict)
				{
					this.EffectiveNumberHandling = new JsonNumberHandling?(this.Options.NumberHandling);
				}
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A0B0 File Offset: 0x000282B0
		private void DetermineNumberHandlingForProperty()
		{
			bool flag = this.NumberHandingIsApplicable();
			if (flag)
			{
				JsonNumberHandling? jsonNumberHandling = this.NumberHandling;
				JsonNumberHandling? jsonNumberHandling2;
				if (jsonNumberHandling == null)
				{
					JsonNumberHandling? numberHandling = this.ParentTypeInfo.NumberHandling;
					jsonNumberHandling2 = ((numberHandling != null) ? numberHandling : this._jsonTypeInfo.NumberHandling);
				}
				else
				{
					jsonNumberHandling2 = jsonNumberHandling;
				}
				JsonNumberHandling? jsonNumberHandling3 = jsonNumberHandling2;
				if (jsonNumberHandling3 == null && this.Options.NumberHandling != JsonNumberHandling.Strict)
				{
					jsonNumberHandling3 = new JsonNumberHandling?(this.Options.NumberHandling);
				}
				this.EffectiveNumberHandling = jsonNumberHandling3;
				return;
			}
			if (this.NumberHandling != null)
			{
				JsonNumberHandling? jsonNumberHandling = this.NumberHandling;
				JsonNumberHandling jsonNumberHandling4 = JsonNumberHandling.Strict;
				if (!((jsonNumberHandling.GetValueOrDefault() == jsonNumberHandling4) & (jsonNumberHandling != null)))
				{
					ThrowHelper.ThrowInvalidOperationException_NumberHandlingOnPropertyInvalid(this);
				}
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002A164 File Offset: 0x00028364
		private void DetermineEffectiveObjectCreationHandlingForProperty()
		{
			JsonObjectCreationHandling jsonObjectCreationHandling = JsonObjectCreationHandling.Replace;
			if (this.ObjectCreationHandling == null)
			{
				JsonObjectCreationHandling jsonObjectCreationHandling2 = this.ParentTypeInfo.PreferredPropertyObjectCreationHandling ?? (this.ParentTypeInfo.DetermineUsesParameterizedConstructor() ? JsonObjectCreationHandling.Replace : this.Options.PreferredObjectCreationHandling);
				jsonObjectCreationHandling = ((jsonObjectCreationHandling2 == JsonObjectCreationHandling.Populate && this.EffectiveConverter.CanPopulate && this.Get != null && (!this.PropertyType.IsValueType || this.Set != null) && !this.ParentTypeInfo.SupportsPolymorphicDeserialization && (this.Set != null || !this.IgnoreReadOnlyMember)) ? JsonObjectCreationHandling.Populate : JsonObjectCreationHandling.Replace);
			}
			else
			{
				JsonObjectCreationHandling? objectCreationHandling = this.ObjectCreationHandling;
				JsonObjectCreationHandling jsonObjectCreationHandling3 = JsonObjectCreationHandling.Populate;
				if ((objectCreationHandling.GetValueOrDefault() == jsonObjectCreationHandling3) & (objectCreationHandling != null))
				{
					if (!this.EffectiveConverter.CanPopulate)
					{
						ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPopulateNotSupportedByConverter(this);
					}
					if (this.Get == null)
					{
						ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPropertyMustHaveAGetter(this);
					}
					if (this.PropertyType.IsValueType && this.Set == null)
					{
						ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPropertyValueTypeMustHaveASetter(this);
					}
					if (this.JsonTypeInfo.SupportsPolymorphicDeserialization)
					{
						ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization(this);
					}
					if (this.Set == null && this.IgnoreReadOnlyMember)
					{
						ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReadOnlyMember(this);
					}
					jsonObjectCreationHandling = JsonObjectCreationHandling.Populate;
				}
			}
			if (jsonObjectCreationHandling == JsonObjectCreationHandling.Populate)
			{
				if (this.ParentTypeInfo.DetermineUsesParameterizedConstructor())
				{
					ThrowHelper.ThrowNotSupportedException_ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors();
				}
				if (this.Options.ReferenceHandlingStrategy != ReferenceHandlingStrategy.None)
				{
					ThrowHelper.ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReferenceHandling();
				}
			}
			this.EffectiveObjectCreationHandling = jsonObjectCreationHandling;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002A2D8 File Offset: 0x000284D8
		private bool NumberHandingIsApplicable()
		{
			if (this.EffectiveConverter.IsInternalConverterForNumberType)
			{
				return true;
			}
			Type type;
			if (!this.EffectiveConverter.IsInternalConverter || ((ConverterStrategy)24 & this.EffectiveConverter.ConverterStrategy) == ConverterStrategy.None)
			{
				type = this.PropertyType;
			}
			else
			{
				type = this.EffectiveConverter.ElementType;
			}
			type = Nullable.GetUnderlyingType(type) ?? type;
			return type == typeof(byte) || type == typeof(decimal) || type == typeof(double) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(sbyte) || type == typeof(float) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == JsonTypeInfo.ObjectType;
		}

		// Token: 0x06000A32 RID: 2610
		internal abstract JsonParameterInfo CreateJsonParameterInfo(JsonParameterInfoValues parameterInfoValues);

		// Token: 0x06000A33 RID: 2611
		internal abstract bool GetMemberAndWriteJson(object obj, ref WriteStack state, Utf8JsonWriter writer);

		// Token: 0x06000A34 RID: 2612
		internal abstract bool GetMemberAndWriteJsonExtensionData(object obj, ref WriteStack state, Utf8JsonWriter writer);

		// Token: 0x06000A35 RID: 2613
		internal abstract object GetValueAsObject(object obj);

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002A411 File Offset: 0x00028611
		internal bool HasGetter
		{
			get
			{
				return this._untypedGet != null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002A41F File Offset: 0x0002861F
		internal bool HasSetter
		{
			get
			{
				return this._untypedSet != null;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002A42D File Offset: 0x0002862D
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0002A435 File Offset: 0x00028635
		protected internal bool IgnoreNullTokensOnRead { internal get; private protected set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002A43E File Offset: 0x0002863E
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0002A446 File Offset: 0x00028646
		protected internal bool IgnoreDefaultValuesOnWrite { internal get; private protected set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002A450 File Offset: 0x00028650
		internal bool IgnoreReadOnlyMember
		{
			get
			{
				MemberTypes memberType = this.MemberType;
				bool flag;
				if (memberType != MemberTypes.Field)
				{
					flag = memberType == MemberTypes.Property && this.Options.IgnoreReadOnlyProperties;
				}
				else
				{
					flag = this.Options.IgnoreReadOnlyFields;
				}
				return flag;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002A48C File Offset: 0x0002868C
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0002A494 File Offset: 0x00028694
		internal bool IsForTypeInfo { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002A49D File Offset: 0x0002869D
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0002A4A5 File Offset: 0x000286A5
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.VerifyMutable();
				if (value == null)
				{
					ThrowHelper.ThrowArgumentNullException("value");
				}
				this._name = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002A4C1 File Offset: 0x000286C1
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0002A4C9 File Offset: 0x000286C9
		internal byte[] NameAsUtf8Bytes { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002A4D2 File Offset: 0x000286D2
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0002A4DA File Offset: 0x000286DA
		internal byte[] EscapedNameSection { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002A4E3 File Offset: 0x000286E3
		public JsonSerializerOptions Options { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002A4EB File Offset: 0x000286EB
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002A4F3 File Offset: 0x000286F3
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this.VerifyMutable();
				this._order = value;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002A504 File Offset: 0x00028704
		internal bool ReadJsonAndAddExtensionProperty(object obj, [ScopedRef] ref ReadStack state, ref Utf8JsonReader reader)
		{
			object valueAsObject = this.GetValueAsObject(obj);
			IDictionary<string, object> dictionary = valueAsObject as IDictionary<string, object>;
			if (dictionary != null)
			{
				if (reader.TokenType == JsonTokenType.Null)
				{
					dictionary[state.Current.JsonPropertyNameAsString] = null;
				}
				else
				{
					JsonConverter<object> jsonConverter = this.<ReadJsonAndAddExtensionProperty>g__GetDictionaryValueConverter|127_0<object>();
					object obj2 = jsonConverter.Read(ref reader, JsonTypeInfo.ObjectType, this.Options);
					dictionary[state.Current.JsonPropertyNameAsString] = obj2;
				}
			}
			else
			{
				IDictionary<string, JsonElement> dictionary2 = valueAsObject as IDictionary<string, JsonElement>;
				if (dictionary2 != null)
				{
					JsonConverter<JsonElement> jsonConverter2 = this.<ReadJsonAndAddExtensionProperty>g__GetDictionaryValueConverter|127_0<JsonElement>();
					JsonElement jsonElement = jsonConverter2.Read(ref reader, typeof(JsonElement), this.Options);
					dictionary2[state.Current.JsonPropertyNameAsString] = jsonElement;
				}
				else
				{
					this.EffectiveConverter.ReadElementAndSetProperty(valueAsObject, state.Current.JsonPropertyNameAsString, ref reader, this.Options, ref state);
				}
			}
			return true;
		}

		// Token: 0x06000A49 RID: 2633
		internal abstract bool ReadJsonAndSetMember(object obj, [ScopedRef] ref ReadStack state, ref Utf8JsonReader reader);

		// Token: 0x06000A4A RID: 2634
		internal abstract bool ReadJsonAsObject([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, out object value);

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002A5D8 File Offset: 0x000287D8
		internal bool ReadJsonExtensionDataValue([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, out object value)
		{
			if (this.JsonTypeInfo.ElementType == JsonTypeInfo.ObjectType && reader.TokenType == JsonTokenType.Null)
			{
				value = null;
				return true;
			}
			JsonConverter<JsonElement> jsonConverter = (JsonConverter<JsonElement>)this.Options.GetConverterInternal(typeof(JsonElement));
			JsonElement jsonElement;
			bool flag;
			if (!jsonConverter.TryRead(ref reader, typeof(JsonElement), this.Options, ref state, out jsonElement, out flag))
			{
				value = null;
				return false;
			}
			value = jsonElement;
			return true;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002A652 File Offset: 0x00028852
		internal void EnsureChildOf(JsonTypeInfo parent)
		{
			if (this.ParentTypeInfo == null)
			{
				this.ParentTypeInfo = parent;
				return;
			}
			if (this.ParentTypeInfo != parent)
			{
				ThrowHelper.ThrowInvalidOperationException_JsonPropertyInfoIsBoundToDifferentJsonTypeInfo(this);
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002A674 File Offset: 0x00028874
		internal bool TryGetPrePopulatedValue([ScopedRef] ref ReadStack state)
		{
			if (this.EffectiveObjectCreationHandling != JsonObjectCreationHandling.Populate)
			{
				return false;
			}
			object obj = this.Get(state.Parent.ReturnValue);
			state.Current.ReturnValue = obj;
			state.Current.IsPopulating = obj != null;
			return obj != null;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002A6C2 File Offset: 0x000288C2
		internal Type DeclaringType { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002A6CC File Offset: 0x000288CC
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0002A6E7 File Offset: 0x000288E7
		internal JsonTypeInfo JsonTypeInfo
		{
			get
			{
				JsonTypeInfo jsonTypeInfo = this._jsonTypeInfo;
				jsonTypeInfo.EnsureConfigured();
				return jsonTypeInfo;
			}
			set
			{
				this._jsonTypeInfo = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002A6F0 File Offset: 0x000288F0
		internal bool IsPropertyTypeInfoConfigured
		{
			get
			{
				JsonTypeInfo jsonTypeInfo = this._jsonTypeInfo;
				return jsonTypeInfo != null && jsonTypeInfo.IsConfigured;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002A704 File Offset: 0x00028904
		internal bool IsIgnored
		{
			get
			{
				return !(this._ignoreCondition != JsonIgnoreCondition.Always) && this.Get == null && this.Set == null;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002A73E File Offset: 0x0002893E
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0002A746 File Offset: 0x00028946
		internal bool CanSerialize { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002A74F File Offset: 0x0002894F
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0002A757 File Offset: 0x00028957
		internal bool CanDeserialize { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002A760 File Offset: 0x00028960
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0002A768 File Offset: 0x00028968
		internal bool CanDeserializeOrPopulate { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002A771 File Offset: 0x00028971
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0002A779 File Offset: 0x00028979
		internal bool SrcGen_HasJsonInclude { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002A782 File Offset: 0x00028982
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0002A78A File Offset: 0x0002898A
		internal bool SrcGen_IsPublic { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002A793 File Offset: 0x00028993
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0002A79B File Offset: 0x0002899B
		public JsonNumberHandling? NumberHandling
		{
			get
			{
				return this._numberHandling;
			}
			set
			{
				this.VerifyMutable();
				this._numberHandling = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002A7AA File Offset: 0x000289AA
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0002A7B2 File Offset: 0x000289B2
		internal JsonNumberHandling? EffectiveNumberHandling { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A61 RID: 2657
		internal abstract bool PropertyTypeCanBeNull { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A62 RID: 2658
		[Nullable(2)]
		internal abstract object DefaultValue { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002A7BB File Offset: 0x000289BB
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0002A7C3 File Offset: 0x000289C3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal int RequiredPropertyIndex
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002A7CC File Offset: 0x000289CC
		internal bool IsOverriddenOrShadowedBy(JsonPropertyInfo other)
		{
			return this.MemberName == other.MemberName && this.DeclaringType.IsAssignableFrom(other.DeclaringType);
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002A7F4 File Offset: 0x000289F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("Name = {0}, PropertyType = {1}", this.Name, this.PropertyType);
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002A818 File Offset: 0x00028A18
		[CompilerGenerated]
		private JsonConverter<TValue> <ReadJsonAndAddExtensionProperty>g__GetDictionaryValueConverter|127_0<TValue>()
		{
			JsonTypeInfo jsonTypeInfo = this.JsonTypeInfo.ElementTypeInfo ?? this.Options.GetTypeInfoInternal(typeof(TValue), true, new bool?(true), false, false);
			return ((JsonTypeInfo<TValue>)jsonTypeInfo).EffectiveConverter;
		}

		// Token: 0x04000374 RID: 884
		internal static readonly JsonPropertyInfo s_missingProperty = JsonPropertyInfo.GetPropertyPlaceholder();

		// Token: 0x04000376 RID: 886
		private protected JsonConverter _effectiveConverter;

		// Token: 0x04000377 RID: 887
		private JsonConverter _customConverter;

		// Token: 0x04000378 RID: 888
		private protected Func<object, object> _untypedGet;

		// Token: 0x04000379 RID: 889
		private protected Action<object, object> _untypedSet;

		// Token: 0x0400037A RID: 890
		private bool _isUserSpecifiedSetter;

		// Token: 0x0400037B RID: 891
		private protected Func<object, object, bool> _shouldSerialize;

		// Token: 0x0400037C RID: 892
		private bool _isUserSpecifiedShouldSerialize;

		// Token: 0x0400037D RID: 893
		private JsonIgnoreCondition? _ignoreCondition;

		// Token: 0x0400037E RID: 894
		private JsonObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000380 RID: 896
		private ICustomAttributeProvider _attributeProvider;

		// Token: 0x04000384 RID: 900
		private bool _isExtensionDataProperty;

		// Token: 0x04000385 RID: 901
		private bool _isRequired;

		// Token: 0x0400038B RID: 907
		private string _name;

		// Token: 0x0400038F RID: 911
		private int _order;

		// Token: 0x04000391 RID: 913
		private JsonTypeInfo _jsonTypeInfo;

		// Token: 0x04000397 RID: 919
		private JsonNumberHandling? _numberHandling;

		// Token: 0x04000399 RID: 921
		private int _index;
	}
}
