using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text.Json.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B0 RID: 176
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public abstract class JsonTypeInfo
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002B1D0 File Offset: 0x000293D0
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0002B1D8 File Offset: 0x000293D8
		internal int ParameterCount { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002B1E1 File Offset: 0x000293E1
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0002B1E9 File Offset: 0x000293E9
		[Nullable(new byte[] { 2, 1 })]
		internal JsonPropertyDictionary<JsonParameterInfo> ParameterCache { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0002B1F2 File Offset: 0x000293F2
		internal bool UsesParameterizedConstructor
		{
			get
			{
				return this.ParameterCache != null;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002B1FD File Offset: 0x000293FD
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002B205 File Offset: 0x00029405
		[Nullable(new byte[] { 2, 1 })]
		internal JsonPropertyDictionary<JsonPropertyInfo> PropertyCache { get; private set; }

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002B210 File Offset: 0x00029410
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		internal JsonPropertyInfo CreatePropertyUsingReflection(Type propertyType, Type declaringType)
		{
			JsonTypeInfo jsonTypeInfo;
			JsonPropertyInfo jsonPropertyInfo;
			if (this.Options.TryGetTypeInfoCached(propertyType, out jsonTypeInfo))
			{
				jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(this, declaringType, this.Options);
			}
			else
			{
				Type type = typeof(JsonPropertyInfo<>).MakeGenericType(new Type[] { propertyType });
				jsonPropertyInfo = (JsonPropertyInfo)type.CreateInstanceNoWrapExceptions(new Type[]
				{
					typeof(Type),
					typeof(JsonTypeInfo),
					typeof(JsonSerializerOptions)
				}, new object[]
				{
					declaringType ?? this.Type,
					this,
					this.Options
				});
			}
			return jsonPropertyInfo;
		}

		// Token: 0x06000AA8 RID: 2728
		private protected abstract JsonPropertyInfo CreateJsonPropertyInfo(JsonTypeInfo declaringTypeInfo, Type declaringType, JsonSerializerOptions options);

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002B2B4 File Offset: 0x000294B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal JsonPropertyInfo GetProperty(ReadOnlySpan<byte> propertyName, ref ReadStackFrame frame, out byte[] utf8PropertyName)
		{
			this.ValidateCanBeUsedForPropertyMetadataSerialization();
			ulong key = JsonTypeInfo.GetKey(propertyName);
			PropertyRef[] propertyRefsSorted = this._propertyRefsSorted;
			if (propertyRefsSorted != null)
			{
				int propertyIndex = frame.PropertyIndex;
				int num = propertyRefsSorted.Length;
				int num2 = Math.Min(propertyIndex, num);
				int num3 = num2 - 1;
				PropertyRef propertyRef;
				for (;;)
				{
					if (num2 < num)
					{
						propertyRef = propertyRefsSorted[num2];
						if (JsonTypeInfo.IsPropertyRefEqual(in propertyRef, propertyName, key))
						{
							break;
						}
						num2++;
						if (num3 >= 0)
						{
							propertyRef = propertyRefsSorted[num3];
							if (JsonTypeInfo.IsPropertyRefEqual(in propertyRef, propertyName, key))
							{
								goto Block_5;
							}
							num3--;
						}
					}
					else
					{
						if (num3 < 0)
						{
							goto IL_00CC;
						}
						propertyRef = propertyRefsSorted[num3];
						if (JsonTypeInfo.IsPropertyRefEqual(in propertyRef, propertyName, key))
						{
							goto Block_7;
						}
						num3--;
					}
				}
				utf8PropertyName = propertyRef.NameFromJson;
				return propertyRef.Info;
				Block_5:
				utf8PropertyName = propertyRef.NameFromJson;
				return propertyRef.Info;
				Block_7:
				utf8PropertyName = propertyRef.NameFromJson;
				return propertyRef.Info;
			}
			IL_00CC:
			JsonPropertyInfo s_missingProperty;
			if (this.PropertyCache.TryGetValue(JsonHelpers.Utf8GetString(propertyName), out s_missingProperty))
			{
				if (this.Options.PropertyNameCaseInsensitive)
				{
					if (propertyName.SequenceEqual(s_missingProperty.NameAsUtf8Bytes))
					{
						utf8PropertyName = s_missingProperty.NameAsUtf8Bytes;
					}
					else
					{
						utf8PropertyName = propertyName.ToArray();
					}
				}
				else
				{
					utf8PropertyName = s_missingProperty.NameAsUtf8Bytes;
				}
			}
			else
			{
				s_missingProperty = JsonPropertyInfo.s_missingProperty;
				utf8PropertyName = propertyName.ToArray();
			}
			int num4 = 0;
			if (propertyRefsSorted != null)
			{
				num4 = propertyRefsSorted.Length;
			}
			if (num4 < 64)
			{
				if (frame.PropertyRefCache != null)
				{
					num4 += frame.PropertyRefCache.Count;
				}
				if (num4 < 64)
				{
					ref List<PropertyRef> ptr = ref frame.PropertyRefCache;
					if (ptr == null)
					{
						ptr = new List<PropertyRef>();
					}
					PropertyRef propertyRef = new PropertyRef(key, s_missingProperty, utf8PropertyName);
					frame.PropertyRefCache.Add(propertyRef);
				}
			}
			return s_missingProperty;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002B44C File Offset: 0x0002964C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal JsonParameterInfo GetParameter(ReadOnlySpan<byte> propertyName, ref ReadStackFrame frame, out byte[] utf8PropertyName)
		{
			ulong key = JsonTypeInfo.GetKey(propertyName);
			ParameterRef[] parameterRefsSorted = this._parameterRefsSorted;
			if (parameterRefsSorted != null)
			{
				int parameterIndex = frame.CtorArgumentState.ParameterIndex;
				int num = parameterRefsSorted.Length;
				int num2 = Math.Min(parameterIndex, num);
				int num3 = num2 - 1;
				ParameterRef parameterRef;
				for (;;)
				{
					if (num2 < num)
					{
						parameterRef = parameterRefsSorted[num2];
						if (JsonTypeInfo.IsParameterRefEqual(in parameterRef, propertyName, key))
						{
							break;
						}
						num2++;
						if (num3 >= 0)
						{
							parameterRef = parameterRefsSorted[num3];
							if (JsonTypeInfo.IsParameterRefEqual(in parameterRef, propertyName, key))
							{
								goto Block_5;
							}
							num3--;
						}
					}
					else
					{
						if (num3 < 0)
						{
							goto IL_00CB;
						}
						parameterRef = parameterRefsSorted[num3];
						if (JsonTypeInfo.IsParameterRefEqual(in parameterRef, propertyName, key))
						{
							goto Block_7;
						}
						num3--;
					}
				}
				utf8PropertyName = parameterRef.NameFromJson;
				return parameterRef.Info;
				Block_5:
				utf8PropertyName = parameterRef.NameFromJson;
				return parameterRef.Info;
				Block_7:
				utf8PropertyName = parameterRef.NameFromJson;
				return parameterRef.Info;
			}
			IL_00CB:
			JsonParameterInfo jsonParameterInfo;
			if (this.ParameterCache.TryGetValue(JsonHelpers.Utf8GetString(propertyName), out jsonParameterInfo))
			{
				if (this.Options.PropertyNameCaseInsensitive)
				{
					if (propertyName.SequenceEqual(jsonParameterInfo.NameAsUtf8Bytes))
					{
						utf8PropertyName = jsonParameterInfo.NameAsUtf8Bytes;
					}
					else
					{
						utf8PropertyName = propertyName.ToArray();
					}
				}
				else
				{
					utf8PropertyName = jsonParameterInfo.NameAsUtf8Bytes;
				}
			}
			else
			{
				utf8PropertyName = propertyName.ToArray();
			}
			int num4 = 0;
			if (parameterRefsSorted != null)
			{
				num4 = parameterRefsSorted.Length;
			}
			if (num4 < 32)
			{
				if (frame.CtorArgumentState.ParameterRefCache != null)
				{
					num4 += frame.CtorArgumentState.ParameterRefCache.Count;
				}
				if (num4 < 32)
				{
					ArgumentState ctorArgumentState = frame.CtorArgumentState;
					if (ctorArgumentState.ParameterRefCache == null)
					{
						ctorArgumentState.ParameterRefCache = new List<ParameterRef>();
					}
					ParameterRef parameterRef = new ParameterRef(key, jsonParameterInfo, utf8PropertyName);
					frame.CtorArgumentState.ParameterRefCache.Add(parameterRef);
				}
			}
			return jsonParameterInfo;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002B5F4 File Offset: 0x000297F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsPropertyRefEqual(in PropertyRef propertyRef, ReadOnlySpan<byte> propertyName, ulong key)
		{
			return key == propertyRef.Key && (propertyName.Length <= 7 || propertyName.SequenceEqual(propertyRef.NameFromJson));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002B61F File Offset: 0x0002981F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsParameterRefEqual(in ParameterRef parameterRef, ReadOnlySpan<byte> parameterName, ulong key)
		{
			return key == parameterRef.Key && (parameterName.Length <= 7 || parameterName.SequenceEqual(parameterRef.NameFromJson));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002B64C File Offset: 0x0002984C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static ulong GetKey(ReadOnlySpan<byte> name)
		{
			ref byte reference = ref MemoryMarshal.GetReference<byte>(name);
			int length = name.Length;
			ulong num;
			if (length > 7)
			{
				num = Unsafe.ReadUnaligned<ulong>(ref reference) & 72057594037927935UL;
				num |= (ulong)((ulong)((long)Math.Min(length, 255)) << 56);
			}
			else
			{
				num = ((length > 5) ? ((ulong)Unsafe.ReadUnaligned<uint>(ref reference) | ((ulong)Unsafe.ReadUnaligned<ushort>(Unsafe.Add<byte>(ref reference, 4)) << 32)) : ((length > 3) ? ((ulong)Unsafe.ReadUnaligned<uint>(ref reference)) : ((length > 1) ? ((ulong)Unsafe.ReadUnaligned<ushort>(ref reference)) : 0UL)));
				num |= (ulong)((ulong)((long)length) << 56);
				if ((length & 1) != 0)
				{
					int num2 = length - 1;
					num |= (ulong)(*Unsafe.Add<byte>(ref reference, num2)) << num2 * 8;
				}
			}
			return num;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002B6F4 File Offset: 0x000298F4
		internal void UpdateSortedPropertyCache(ref ReadStackFrame frame)
		{
			List<PropertyRef> propertyRefCache = frame.PropertyRefCache;
			if (this._propertyRefsSorted != null)
			{
				List<PropertyRef> list = new List<PropertyRef>(this._propertyRefsSorted);
				while (list.Count + propertyRefCache.Count > 64)
				{
					propertyRefCache.RemoveAt(propertyRefCache.Count - 1);
				}
				list.AddRange(propertyRefCache);
				this._propertyRefsSorted = list.ToArray();
			}
			else
			{
				this._propertyRefsSorted = propertyRefCache.ToArray();
			}
			frame.PropertyRefCache = null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002B770 File Offset: 0x00029970
		internal void UpdateSortedParameterCache(ref ReadStackFrame frame)
		{
			List<ParameterRef> parameterRefCache = frame.CtorArgumentState.ParameterRefCache;
			if (this._parameterRefsSorted != null)
			{
				List<ParameterRef> list = new List<ParameterRef>(this._parameterRefsSorted);
				while (list.Count + parameterRefCache.Count > 32)
				{
					parameterRefCache.RemoveAt(parameterRefCache.Count - 1);
				}
				list.AddRange(parameterRefCache);
				this._parameterRefsSorted = list.ToArray();
			}
			else
			{
				this._parameterRefsSorted = parameterRefCache.ToArray();
			}
			frame.CtorArgumentState.ParameterRefCache = null;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002B7F3 File Offset: 0x000299F3
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0002B7FB File Offset: 0x000299FB
		internal int NumberOfRequiredProperties { get; private set; }

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002B804 File Offset: 0x00029A04
		internal JsonTypeInfo(Type type, JsonConverter converter, JsonSerializerOptions options)
		{
			this.Type = type;
			this.Options = options;
			this.Converter = converter;
			this.Kind = JsonTypeInfo.GetTypeInfoKind(type, converter);
			this.PropertyInfoForTypeInfo = this.CreatePropertyInfoForTypeInfo();
			this.ElementType = converter.ElementType;
			this.KeyType = converter.KeyType;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002B86B File Offset: 0x00029A6B
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002B873 File Offset: 0x00029A73
		[Nullable(new byte[] { 2, 1 })]
		public Func<object> CreateObject
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._createObject;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.SetCreateObject(value);
			}
		}

		// Token: 0x06000AB5 RID: 2741
		private protected abstract void SetCreateObject(Delegate createObject);

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002B87C File Offset: 0x00029A7C
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0002B884 File Offset: 0x00029A84
		[Nullable(new byte[] { 2, 1 })]
		internal Func<object> CreateObjectForExtensionDataProperty { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002B88D File Offset: 0x00029A8D
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002B895 File Offset: 0x00029A95
		[Nullable(new byte[] { 2, 1 })]
		public Action<object> OnSerializing
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._onSerializing;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				this._onSerializing = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		[Nullable(new byte[] { 2, 1 })]
		public Action<object> OnSerialized
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._onSerialized;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				this._onSerialized = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002B8E3 File Offset: 0x00029AE3
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002B8EB File Offset: 0x00029AEB
		[Nullable(new byte[] { 2, 1 })]
		public Action<object> OnDeserializing
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._onDeserializing;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				this._onDeserializing = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002B90E File Offset: 0x00029B0E
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002B916 File Offset: 0x00029B16
		[Nullable(new byte[] { 2, 1 })]
		public Action<object> OnDeserialized
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._onDeserialized;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				this._onDeserialized = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002B939 File Offset: 0x00029B39
		[Nullable(1)]
		public IList<JsonPropertyInfo> Properties
		{
			[NullableContext(1)]
			get
			{
				return this.PropertyList;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002B941 File Offset: 0x00029B41
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonTypeInfo.JsonPropertyInfoList PropertyList
		{
			get
			{
				return this._properties ?? this.<get_PropertyList>g__CreatePropertyList|65_0();
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002B953 File Offset: 0x00029B53
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0002B95B File Offset: 0x00029B5B
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		internal Func<JsonSerializerContext, JsonPropertyInfo[]> SourceGenDelayedPropertyInitializer
		{
			get
			{
				return this._sourceGenDelayedPropertyInitializer;
			}
			set
			{
				this._sourceGenDelayedPropertyInitializer = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002B964 File Offset: 0x00029B64
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002B96C File Offset: 0x00029B6C
		public JsonPolymorphismOptions PolymorphismOptions
		{
			get
			{
				return this._polymorphismOptions;
			}
			set
			{
				this.VerifyMutable();
				if (value != null)
				{
					if (this.Kind == JsonTypeInfoKind.None)
					{
						ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
					}
					if (value.DeclaringTypeInfo != null && value.DeclaringTypeInfo != this)
					{
						ThrowHelper.ThrowArgumentException_JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo("value");
					}
					value.DeclaringTypeInfo = this;
				}
				this._polymorphismOptions = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B9BE File Offset: 0x00029BBE
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B9C6 File Offset: 0x00029BC6
		public bool IsReadOnly { get; private set; }

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002B9CF File Offset: 0x00029BCF
		public void MakeReadOnly()
		{
			this.IsReadOnly = true;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B9D8 File Offset: 0x00029BD8
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		internal object CreateObjectWithArgs { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002B9E9 File Offset: 0x00029BE9
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002B9F1 File Offset: 0x00029BF1
		internal object AddMethodDelegate { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002B9FA File Offset: 0x00029BFA
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0002BA02 File Offset: 0x00029C02
		internal JsonPropertyInfo ExtensionDataProperty { get; private set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002BA0B File Offset: 0x00029C0B
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0002BA13 File Offset: 0x00029C13
		internal PolymorphicTypeResolver PolymorphicTypeResolver { get; private set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002BA1C File Offset: 0x00029C1C
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002BA24 File Offset: 0x00029C24
		protected internal bool HasSerializeHandler { internal get; private protected set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002BA2D File Offset: 0x00029C2D
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0002BA35 File Offset: 0x00029C35
		internal bool CanUseSerializeHandler { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002BA3E File Offset: 0x00029C3E
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0002BA46 File Offset: 0x00029C46
		internal bool PropertyMetadataSerializationNotSupported { get; set; }

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002BA4F File Offset: 0x00029C4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ValidateCanBeUsedForPropertyMetadataSerialization()
		{
			if (this.PropertyMetadataSerializationNotSupported)
			{
				ThrowHelper.ThrowInvalidOperationException_NoMetadataForTypeProperties(this.Options.TypeInfoResolver, this.Type);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002BA6F File Offset: 0x00029C6F
		internal Type ElementType { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002BA77 File Offset: 0x00029C77
		internal Type KeyType { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002BA80 File Offset: 0x00029C80
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0002BA9E File Offset: 0x00029C9E
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonTypeInfo ElementTypeInfo
		{
			get
			{
				JsonTypeInfo elementTypeInfo = this._elementTypeInfo;
				if (elementTypeInfo != null)
				{
					elementTypeInfo.EnsureConfigured();
				}
				return elementTypeInfo;
			}
			set
			{
				this._elementTypeInfo = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002BAA8 File Offset: 0x00029CA8
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002BAC6 File Offset: 0x00029CC6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonTypeInfo KeyTypeInfo
		{
			get
			{
				JsonTypeInfo keyTypeInfo = this._keyTypeInfo;
				if (keyTypeInfo != null)
				{
					keyTypeInfo.EnsureConfigured();
				}
				return keyTypeInfo;
			}
			set
			{
				this._keyTypeInfo = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002BACF File Offset: 0x00029CCF
		[Nullable(1)]
		public JsonSerializerOptions Options
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002BAD7 File Offset: 0x00029CD7
		[Nullable(1)]
		public Type Type
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002BADF File Offset: 0x00029CDF
		[Nullable(1)]
		public JsonConverter Converter
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002BAE7 File Offset: 0x00029CE7
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002BAEF File Offset: 0x00029CEF
		public JsonTypeInfoKind Kind { get; private set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002BAF8 File Offset: 0x00029CF8
		[Nullable(1)]
		internal JsonPropertyInfo PropertyInfoForTypeInfo { get; }

		// Token: 0x06000AE4 RID: 2788
		private protected abstract JsonPropertyInfo CreatePropertyInfoForTypeInfo();

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002BB00 File Offset: 0x00029D00
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0002BB08 File Offset: 0x00029D08
		public JsonNumberHandling? NumberHandling
		{
			get
			{
				return this._numberHandling;
			}
			set
			{
				this.VerifyMutable();
				if (value != null && !JsonSerializer.IsValidNumberHandlingValue(value.Value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._numberHandling = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0002BB39 File Offset: 0x00029D39
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0002BB44 File Offset: 0x00029D44
		public JsonUnmappedMemberHandling? UnmappedMemberHandling
		{
			get
			{
				return this._unmappedMemberHandling;
			}
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				if (value != null && !JsonSerializer.IsValidUnmappedMemberHandlingValue(value.Value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._unmappedMemberHandling = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002BB94 File Offset: 0x00029D94
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0002BB9C File Offset: 0x00029D9C
		internal JsonUnmappedMemberHandling EffectiveUnmappedMemberHandling { get; private set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002BBA5 File Offset: 0x00029DA5
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0002BBB0 File Offset: 0x00029DB0
		public JsonObjectCreationHandling? PreferredPropertyObjectCreationHandling
		{
			get
			{
				return this._preferredPropertyObjectCreationHandling;
			}
			set
			{
				this.VerifyMutable();
				if (this.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this.Kind);
				}
				if (value != null && !JsonSerializer.IsValidCreationHandlingValue(value.Value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preferredPropertyObjectCreationHandling = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002BC00 File Offset: 0x00029E00
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x0002BC08 File Offset: 0x00029E08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IJsonTypeInfoResolver OriginatingResolver
		{
			get
			{
				return this._originatingResolver;
			}
			set
			{
				this.VerifyMutable();
				if (value is JsonSerializerContext)
				{
					this.IsCustomized = false;
				}
				this._originatingResolver = value;
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002BC26 File Offset: 0x00029E26
		internal void VerifyMutable()
		{
			if (this.IsReadOnly)
			{
				ThrowHelper.ThrowInvalidOperationException_TypeInfoImmutable();
			}
			this.IsCustomized = true;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002BC3C File Offset: 0x00029E3C
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0002BC44 File Offset: 0x00029E44
		internal bool IsCustomized { get; set; } = true;

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002BC4D File Offset: 0x00029E4D
		internal bool IsConfigured
		{
			get
			{
				return this._configurationState == JsonTypeInfo.ConfigurationState.Configured;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002BC5A File Offset: 0x00029E5A
		internal bool IsConfigurationStarted
		{
			get
			{
				return this._configurationState != JsonTypeInfo.ConfigurationState.NotConfigured;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002BC6A File Offset: 0x00029E6A
		internal void EnsureConfigured()
		{
			if (!this.IsConfigured)
			{
				this.<EnsureConfigured>g__ConfigureSynchronized|172_0();
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002BC7C File Offset: 0x00029E7C
		private void Configure()
		{
			this.PropertyInfoForTypeInfo.Configure();
			if (this.PolymorphismOptions != null)
			{
				this.PolymorphicTypeResolver = new PolymorphicTypeResolver(this.Options, this.PolymorphismOptions, this.Type, this.Converter.CanHaveMetadata);
			}
			if (this.Kind == JsonTypeInfoKind.Object)
			{
				this.ConfigureProperties();
				if (this.DetermineUsesParameterizedConstructor())
				{
					this.ConfigureConstructorParameters();
				}
			}
			if (this.ElementType != null)
			{
				if (this._elementTypeInfo == null)
				{
					this._elementTypeInfo = this.Options.GetTypeInfoInternal(this.ElementType, true, new bool?(true), false, false);
				}
				this._elementTypeInfo.EnsureConfigured();
			}
			if (this.KeyType != null)
			{
				if (this._keyTypeInfo == null)
				{
					this._keyTypeInfo = this.Options.GetTypeInfoInternal(this.KeyType, true, new bool?(true), false, false);
				}
				this._keyTypeInfo.EnsureConfigured();
			}
			this.DetermineIsCompatibleWithCurrentOptions();
			this.CanUseSerializeHandler = this.HasSerializeHandler && this.IsCompatibleWithCurrentOptions;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0002BD80 File Offset: 0x00029F80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonTypeInfo AncestorPolymorphicType
		{
			get
			{
				if (!this._isAncestorPolymorphicTypeResolved)
				{
					this._ancestorPolymorhicType = PolymorphicTypeResolver.FindNearestPolymorphicBaseType(this);
					this._isAncestorPolymorphicTypeResolved = true;
				}
				return this._ancestorPolymorhicType;
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		private void DetermineIsCompatibleWithCurrentOptions()
		{
			if (!this.<DetermineIsCompatibleWithCurrentOptions>g__IsCurrentNodeCompatible|178_0())
			{
				this.IsCompatibleWithCurrentOptions = false;
				return;
			}
			if (this._properties != null)
			{
				foreach (JsonPropertyInfo jsonPropertyInfo in this._properties)
				{
					if (jsonPropertyInfo.IsPropertyTypeInfoConfigured && !jsonPropertyInfo.JsonTypeInfo.IsCompatibleWithCurrentOptions)
					{
						this.IsCompatibleWithCurrentOptions = false;
						return;
					}
				}
			}
			JsonTypeInfo elementTypeInfo = this._elementTypeInfo;
			if (elementTypeInfo == null || elementTypeInfo.IsCompatibleWithCurrentOptions)
			{
				JsonTypeInfo keyTypeInfo = this._keyTypeInfo;
				if (keyTypeInfo == null || keyTypeInfo.IsCompatibleWithCurrentOptions)
				{
					return;
				}
			}
			this.IsCompatibleWithCurrentOptions = false;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002BE60 File Offset: 0x0002A060
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x0002BE68 File Offset: 0x0002A068
		private bool IsCompatibleWithCurrentOptions { get; set; } = true;

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002BE71 File Offset: 0x0002A071
		internal bool DetermineUsesParameterizedConstructor()
		{
			return this.Converter.ConstructorIsParameterized && this.CreateObject == null;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002BE8C File Offset: 0x0002A08C
		[NullableContext(1)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonTypeInfo<T> CreateJsonTypeInfo<[Nullable(2)] T>(JsonSerializerOptions options)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			JsonConverter converterForType = DefaultJsonTypeInfoResolver.GetConverterForType(typeof(T), options, false);
			return new JsonTypeInfo<T>(converterForType, options);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		[NullableContext(1)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonTypeInfo CreateJsonTypeInfo(Type type, JsonSerializerOptions options)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException("type");
			}
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			if (JsonTypeInfo.IsInvalidForSerialization(type))
			{
				ThrowHelper.ThrowArgumentException_CannotSerializeInvalidType("type", type, null, null);
			}
			JsonConverter converterForType = DefaultJsonTypeInfoResolver.GetConverterForType(type, options, false);
			return JsonTypeInfo.CreateJsonTypeInfo(type, converterForType, options);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002BF14 File Offset: 0x0002A114
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal static JsonTypeInfo CreateJsonTypeInfo(Type type, JsonConverter converter, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo;
			if (converter.Type == type)
			{
				jsonTypeInfo = converter.CreateJsonTypeInfo(options);
			}
			else
			{
				Type type2 = typeof(JsonTypeInfo<>).MakeGenericType(new Type[] { type });
				jsonTypeInfo = (JsonTypeInfo)type2.CreateInstanceNoWrapExceptions(new Type[]
				{
					typeof(JsonConverter),
					typeof(JsonSerializerOptions)
				}, new object[] { converter, options });
			}
			return jsonTypeInfo;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002BF90 File Offset: 0x0002A190
		[NullableContext(1)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public JsonPropertyInfo CreateJsonPropertyInfo(Type propertyType, string name)
		{
			if (propertyType == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyType");
			}
			if (name == null)
			{
				ThrowHelper.ThrowArgumentNullException("name");
			}
			if (JsonTypeInfo.IsInvalidForSerialization(propertyType))
			{
				ThrowHelper.ThrowArgumentException_CannotSerializeInvalidType("propertyType", propertyType, this.Type, name);
			}
			this.VerifyMutable();
			JsonPropertyInfo jsonPropertyInfo = this.CreatePropertyUsingReflection(propertyType, null);
			jsonPropertyInfo.Name = name;
			return jsonPropertyInfo;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002BFEE File Offset: 0x0002A1EE
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0002BFF6 File Offset: 0x0002A1F6
		[Nullable(new byte[] { 2, 1 })]
		internal JsonParameterInfoValues[] ParameterInfoValues { get; set; }

		// Token: 0x06000B01 RID: 2817
		internal abstract void SerializeAsObject(Utf8JsonWriter writer, object rootValue);

		// Token: 0x06000B02 RID: 2818
		internal abstract Task SerializeAsObjectAsync(Stream utf8Json, object rootValue, CancellationToken cancellationToken);

		// Token: 0x06000B03 RID: 2819
		internal abstract void SerializeAsObject(Stream utf8Json, object rootValue);

		// Token: 0x06000B04 RID: 2820
		internal abstract object DeserializeAsObject(ref Utf8JsonReader reader, ref ReadStack state);

		// Token: 0x06000B05 RID: 2821
		internal abstract ValueTask<object> DeserializeAsObjectAsync(Stream utf8Json, CancellationToken cancellationToken);

		// Token: 0x06000B06 RID: 2822
		internal abstract object DeserializeAsObject(Stream utf8Json);

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002C000 File Offset: 0x0002A200
		internal void ConfigureProperties()
		{
			JsonTypeInfo.JsonPropertyInfoList propertyList = this.PropertyList;
			JsonPropertyDictionary<JsonPropertyInfo> jsonPropertyDictionary = this.CreatePropertyCache(propertyList.Count);
			int num = 0;
			bool flag = true;
			int num2 = int.MinValue;
			foreach (JsonPropertyInfo jsonPropertyInfo in propertyList)
			{
				if (jsonPropertyInfo.IsExtensionData)
				{
					if (!(this.UnmappedMemberHandling != JsonUnmappedMemberHandling.Disallow))
					{
						ThrowHelper.ThrowInvalidOperationException_ExtensionDataConflictsWithUnmappedMemberHandling(this.Type, jsonPropertyInfo);
					}
					if (this.ExtensionDataProperty != null)
					{
						ThrowHelper.ThrowInvalidOperationException_SerializationDuplicateTypeAttribute(this.Type, typeof(JsonExtensionDataAttribute));
					}
					this.ExtensionDataProperty = jsonPropertyInfo;
				}
				else
				{
					if (jsonPropertyInfo.IsRequired)
					{
						jsonPropertyInfo.RequiredPropertyIndex = num++;
					}
					if (flag)
					{
						flag = num2 <= jsonPropertyInfo.Order;
						num2 = jsonPropertyInfo.Order;
					}
					if (!jsonPropertyDictionary.TryAddValue(jsonPropertyInfo.Name, jsonPropertyInfo))
					{
						ThrowHelper.ThrowInvalidOperationException_SerializerPropertyNameConflict(this.Type, jsonPropertyInfo.Name);
					}
				}
				jsonPropertyInfo.Configure();
			}
			if (!flag)
			{
				propertyList.SortProperties();
				jsonPropertyDictionary.List.StableSortByKey((KeyValuePair<string, JsonPropertyInfo> propInfo) => propInfo.Value.Order);
			}
			this.NumberOfRequiredProperties = num;
			this.PropertyCache = jsonPropertyDictionary;
			this.EffectiveUnmappedMemberHandling = this.UnmappedMemberHandling ?? ((this.ExtensionDataProperty == null) ? this.Options.UnmappedMemberHandling : JsonUnmappedMemberHandling.Skip);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002C198 File Offset: 0x0002A398
		internal void ConfigureConstructorParameters()
		{
			JsonParameterInfoValues[] array = this.ParameterInfoValues ?? Array.Empty<JsonParameterInfoValues>();
			JsonPropertyDictionary<JsonParameterInfo> jsonPropertyDictionary = new JsonPropertyDictionary<JsonParameterInfo>(this.Options.PropertyNameCaseInsensitive, array.Length);
			Dictionary<JsonTypeInfo.ParameterLookupKey, JsonTypeInfo.ParameterLookupValue> dictionary = new Dictionary<JsonTypeInfo.ParameterLookupKey, JsonTypeInfo.ParameterLookupValue>(this.PropertyCache.Count);
			foreach (KeyValuePair<string, JsonPropertyInfo> keyValuePair in this.PropertyCache.List)
			{
				JsonPropertyInfo value = keyValuePair.Value;
				string text = value.MemberName ?? value.Name;
				JsonTypeInfo.ParameterLookupKey parameterLookupKey = new JsonTypeInfo.ParameterLookupKey(text, value.PropertyType);
				JsonTypeInfo.ParameterLookupValue parameterLookupValue = new JsonTypeInfo.ParameterLookupValue(value);
				if (!dictionary.TryAdd(parameterLookupKey, parameterLookupValue))
				{
					JsonTypeInfo.ParameterLookupValue parameterLookupValue2 = dictionary[parameterLookupKey];
					parameterLookupValue2.DuplicateName = text;
				}
			}
			foreach (JsonParameterInfoValues jsonParameterInfoValues in array)
			{
				JsonTypeInfo.ParameterLookupKey parameterLookupKey2 = new JsonTypeInfo.ParameterLookupKey(jsonParameterInfoValues.Name, jsonParameterInfoValues.ParameterType);
				JsonTypeInfo.ParameterLookupValue parameterLookupValue3;
				if (dictionary.TryGetValue(parameterLookupKey2, out parameterLookupValue3))
				{
					if (parameterLookupValue3.DuplicateName != null)
					{
						ThrowHelper.ThrowInvalidOperationException_MultiplePropertiesBindToConstructorParameters(this.Type, jsonParameterInfoValues.Name, parameterLookupValue3.JsonPropertyInfo.Name, parameterLookupValue3.DuplicateName);
					}
					JsonPropertyInfo jsonPropertyInfo = parameterLookupValue3.JsonPropertyInfo;
					JsonParameterInfo jsonParameterInfo = jsonPropertyInfo.CreateJsonParameterInfo(jsonParameterInfoValues);
					jsonPropertyDictionary.Add(jsonPropertyInfo.Name, jsonParameterInfo);
				}
				else if (this.ExtensionDataProperty != null && StringComparer.OrdinalIgnoreCase.Equals(parameterLookupKey2.Name, this.ExtensionDataProperty.Name))
				{
					ThrowHelper.ThrowInvalidOperationException_ExtensionDataCannotBindToCtorParam(this.ExtensionDataProperty.MemberName, this.ExtensionDataProperty);
				}
			}
			this.ParameterCount = array.Length;
			this.ParameterCache = jsonPropertyDictionary;
			this.ParameterInfoValues = null;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002C360 File Offset: 0x0002A560
		internal static void ValidateType(Type type)
		{
			if (JsonTypeInfo.IsInvalidForSerialization(type))
			{
				ThrowHelper.ThrowInvalidOperationException_CannotSerializeInvalidType(type, null, null);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002C372 File Offset: 0x0002A572
		internal static bool IsInvalidForSerialization(Type type)
		{
			return type == typeof(void) || type.IsPointer || type.IsByRef || JsonTypeInfo.IsByRefLike(type) || type.ContainsGenericParameters;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002C3A8 File Offset: 0x0002A5A8
		internal void PopulatePolymorphismMetadata()
		{
			JsonPolymorphismOptions jsonPolymorphismOptions = JsonPolymorphismOptions.CreateFromAttributeDeclarations(this.Type);
			if (jsonPolymorphismOptions != null)
			{
				jsonPolymorphismOptions.DeclaringTypeInfo = this;
				this._polymorphismOptions = jsonPolymorphismOptions;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		internal void MapInterfaceTypesToCallbacks()
		{
			if (this.Kind == JsonTypeInfoKind.Object)
			{
				if (typeof(IJsonOnSerializing).IsAssignableFrom(this.Type))
				{
					this.OnSerializing = delegate(object obj)
					{
						((IJsonOnSerializing)obj).OnSerializing();
					};
				}
				if (typeof(IJsonOnSerialized).IsAssignableFrom(this.Type))
				{
					this.OnSerialized = delegate(object obj)
					{
						((IJsonOnSerialized)obj).OnSerialized();
					};
				}
				if (typeof(IJsonOnDeserializing).IsAssignableFrom(this.Type))
				{
					this.OnDeserializing = delegate(object obj)
					{
						((IJsonOnDeserializing)obj).OnDeserializing();
					};
				}
				if (typeof(IJsonOnDeserialized).IsAssignableFrom(this.Type))
				{
					this.OnDeserialized = delegate(object obj)
					{
						((IJsonOnDeserialized)obj).OnDeserialized();
					};
				}
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002C4DD File Offset: 0x0002A6DD
		internal void SetCreateObjectIfCompatible(Delegate createObject)
		{
			if (this.Converter.SupportsCreateObjectDelegate && !this.Converter.ConstructorIsParameterized)
			{
				this.SetCreateObject(createObject);
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002C500 File Offset: 0x0002A700
		private static bool IsByRefLike(Type type)
		{
			if (!type.IsValueType)
			{
				return false;
			}
			object[] customAttributes = type.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i].GetType().FullName == "System.Runtime.CompilerServices.IsByRefLikeAttribute")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002C549 File Offset: 0x0002A749
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool SupportsPolymorphicDeserialization
		{
			get
			{
				PolymorphicTypeResolver polymorphicTypeResolver = this.PolymorphicTypeResolver;
				return polymorphicTypeResolver != null && polymorphicTypeResolver.UsesTypeDiscriminators;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002C55C File Offset: 0x0002A75C
		internal static bool IsValidExtensionDataProperty(Type propertyType)
		{
			return typeof(IDictionary<string, object>).IsAssignableFrom(propertyType) || typeof(IDictionary<string, JsonElement>).IsAssignableFrom(propertyType) || (propertyType.FullName == "System.Text.Json.Nodes.JsonObject" && propertyType.Assembly == typeof(JsonTypeInfo).Assembly);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		internal JsonPropertyDictionary<JsonPropertyInfo> CreatePropertyCache(int capacity)
		{
			return new JsonPropertyDictionary<JsonPropertyInfo>(this.Options.PropertyNameCaseInsensitive, capacity);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		private static JsonTypeInfoKind GetTypeInfoKind(Type type, JsonConverter converter)
		{
			if (type == typeof(object) && converter.CanBePolymorphic)
			{
				return JsonTypeInfoKind.None;
			}
			ConverterStrategy converterStrategy = converter.ConverterStrategy;
			switch (converterStrategy)
			{
			case ConverterStrategy.None:
				ThrowHelper.ThrowNotSupportedException_SerializationNotSupported(type);
				return JsonTypeInfoKind.None;
			case ConverterStrategy.Object:
				return JsonTypeInfoKind.Object;
			case ConverterStrategy.Value:
				return JsonTypeInfoKind.None;
			default:
				if (converterStrategy == ConverterStrategy.Enumerable)
				{
					return JsonTypeInfoKind.Enumerable;
				}
				if (converterStrategy != ConverterStrategy.Dictionary)
				{
					throw new InvalidOperationException();
				}
				return JsonTypeInfoKind.Dictionary;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002C632 File Offset: 0x0002A832
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("Type = {0}, Kind = {1}", this.Type.Name, this.Kind);
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002C668 File Offset: 0x0002A868
		[CompilerGenerated]
		private JsonTypeInfo.JsonPropertyInfoList <get_PropertyList>g__CreatePropertyList|65_0()
		{
			JsonTypeInfo.JsonPropertyInfoList jsonPropertyInfoList = new JsonTypeInfo.JsonPropertyInfoList(this);
			Func<JsonSerializerContext, JsonPropertyInfo[]> sourceGenDelayedPropertyInitializer = this._sourceGenDelayedPropertyInitializer;
			if (sourceGenDelayedPropertyInitializer != null)
			{
				JsonMetadataServices.PopulateProperties(this, jsonPropertyInfoList, sourceGenDelayedPropertyInitializer);
			}
			JsonTypeInfo.JsonPropertyInfoList jsonPropertyInfoList2 = Interlocked.CompareExchange<JsonTypeInfo.JsonPropertyInfoList>(ref this._properties, jsonPropertyInfoList, null);
			this._sourceGenDelayedPropertyInitializer = null;
			return jsonPropertyInfoList2 ?? jsonPropertyInfoList;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002C6AC File Offset: 0x0002A8AC
		[CompilerGenerated]
		private void <EnsureConfigured>g__ConfigureSynchronized|172_0()
		{
			this.Options.MakeReadOnly();
			this.MakeReadOnly();
			ExceptionDispatchInfo cachedConfigureError = this._cachedConfigureError;
			if (cachedConfigureError != null)
			{
				cachedConfigureError.Throw();
			}
			JsonSerializerOptions.CachingContext cacheContext = this.Options.CacheContext;
			lock (cacheContext)
			{
				if (this._configurationState == JsonTypeInfo.ConfigurationState.NotConfigured)
				{
					ExceptionDispatchInfo cachedConfigureError2 = this._cachedConfigureError;
					if (cachedConfigureError2 != null)
					{
						cachedConfigureError2.Throw();
					}
					try
					{
						this._configurationState = JsonTypeInfo.ConfigurationState.Configuring;
						this.Configure();
						this._configurationState = JsonTypeInfo.ConfigurationState.Configured;
					}
					catch (Exception ex)
					{
						this._cachedConfigureError = ExceptionDispatchInfo.Capture(ex);
						this._configurationState = JsonTypeInfo.ConfigurationState.NotConfigured;
						throw;
					}
				}
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002C768 File Offset: 0x0002A968
		[CompilerGenerated]
		private bool <DetermineIsCompatibleWithCurrentOptions>g__IsCurrentNodeCompatible|178_0()
		{
			return this.Options.CanUseFastPathSerializationLogic || (!this.IsCustomized && this.OriginatingResolver.IsCompatibleWithOptions(this.Options));
		}

		// Token: 0x040003AC RID: 940
		internal static readonly Type ObjectType = typeof(object);

		// Token: 0x040003AD RID: 941
		private const int PropertyNameKeyLength = 7;

		// Token: 0x040003AE RID: 942
		private const int ParameterNameCountCacheThreshold = 32;

		// Token: 0x040003AF RID: 943
		private const int PropertyNameCountCacheThreshold = 64;

		// Token: 0x040003B3 RID: 947
		private volatile ParameterRef[] _parameterRefsSorted;

		// Token: 0x040003B4 RID: 948
		private volatile PropertyRef[] _propertyRefsSorted;

		// Token: 0x040003B5 RID: 949
		internal const string MetadataFactoryRequiresUnreferencedCode = "JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.";

		// Token: 0x040003B6 RID: 950
		internal const string JsonObjectTypeName = "System.Text.Json.Nodes.JsonObject";

		// Token: 0x040003B8 RID: 952
		private Action<object> _onSerializing;

		// Token: 0x040003B9 RID: 953
		private Action<object> _onSerialized;

		// Token: 0x040003BA RID: 954
		private Action<object> _onDeserializing;

		// Token: 0x040003BB RID: 955
		private Action<object> _onDeserialized;

		// Token: 0x040003BC RID: 956
		private protected Func<object> _createObject;

		// Token: 0x040003BE RID: 958
		private Func<JsonSerializerContext, JsonPropertyInfo[]> _sourceGenDelayedPropertyInitializer;

		// Token: 0x040003BF RID: 959
		private JsonTypeInfo.JsonPropertyInfoList _properties;

		// Token: 0x040003C1 RID: 961
		private protected JsonPolymorphismOptions _polymorphismOptions;

		// Token: 0x040003CB RID: 971
		private JsonTypeInfo _elementTypeInfo;

		// Token: 0x040003CC RID: 972
		private JsonTypeInfo _keyTypeInfo;

		// Token: 0x040003D2 RID: 978
		private JsonNumberHandling? _numberHandling;

		// Token: 0x040003D3 RID: 979
		private JsonUnmappedMemberHandling? _unmappedMemberHandling;

		// Token: 0x040003D5 RID: 981
		private JsonObjectCreationHandling? _preferredPropertyObjectCreationHandling;

		// Token: 0x040003D6 RID: 982
		private IJsonTypeInfoResolver _originatingResolver;

		// Token: 0x040003D8 RID: 984
		private volatile JsonTypeInfo.ConfigurationState _configurationState;

		// Token: 0x040003D9 RID: 985
		private ExceptionDispatchInfo _cachedConfigureError;

		// Token: 0x040003DA RID: 986
		private JsonTypeInfo _ancestorPolymorhicType;

		// Token: 0x040003DB RID: 987
		private volatile bool _isAncestorPolymorphicTypeResolved;

		// Token: 0x02000146 RID: 326
		// (Invoke) Token: 0x06000DFC RID: 3580
		internal delegate T ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);

		// Token: 0x02000147 RID: 327
		private enum ConfigurationState : byte
		{
			// Token: 0x04000511 RID: 1297
			NotConfigured,
			// Token: 0x04000512 RID: 1298
			Configuring,
			// Token: 0x04000513 RID: 1299
			Configured
		}

		// Token: 0x02000148 RID: 328
		internal ref struct PropertyHierarchyResolutionState
		{
			// Token: 0x06000DFF RID: 3583 RVA: 0x00036A04 File Offset: 0x00034C04
			public PropertyHierarchyResolutionState(JsonSerializerOptions options)
			{
				this.IgnoredProperties = null;
				this.IsPropertyOrderSpecified = false;
				this.AddedProperties = new Dictionary<string, global::System.ValueTuple<JsonPropertyInfo, int>>(options.PropertyNameCaseInsensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
			}

			// Token: 0x04000514 RID: 1300
			[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { null, "index" })]
			public Dictionary<string, global::System.ValueTuple<JsonPropertyInfo, int>> AddedProperties;

			// Token: 0x04000515 RID: 1301
			public Dictionary<string, JsonPropertyInfo> IgnoredProperties;

			// Token: 0x04000516 RID: 1302
			public bool IsPropertyOrderSpecified;
		}

		// Token: 0x02000149 RID: 329
		private sealed class ParameterLookupKey
		{
			// Token: 0x06000E00 RID: 3584 RVA: 0x00036A33 File Offset: 0x00034C33
			public ParameterLookupKey(string name, Type type)
			{
				this.Name = name;
				this.Type = type;
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00036A49 File Offset: 0x00034C49
			public string Name { get; }

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00036A51 File Offset: 0x00034C51
			public Type Type { get; }

			// Token: 0x06000E03 RID: 3587 RVA: 0x00036A59 File Offset: 0x00034C59
			public override int GetHashCode()
			{
				return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Name);
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x00036A6C File Offset: 0x00034C6C
			public override bool Equals([NotNullWhen(true)] object obj)
			{
				JsonTypeInfo.ParameterLookupKey parameterLookupKey = (JsonTypeInfo.ParameterLookupKey)obj;
				return this.Type == parameterLookupKey.Type && string.Equals(this.Name, parameterLookupKey.Name, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0200014A RID: 330
		private sealed class ParameterLookupValue
		{
			// Token: 0x06000E05 RID: 3589 RVA: 0x00036AA7 File Offset: 0x00034CA7
			public ParameterLookupValue(JsonPropertyInfo jsonPropertyInfo)
			{
				this.JsonPropertyInfo = jsonPropertyInfo;
			}

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00036AB6 File Offset: 0x00034CB6
			// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00036ABE File Offset: 0x00034CBE
			public string DuplicateName { get; set; }

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00036AC7 File Offset: 0x00034CC7
			public JsonPropertyInfo JsonPropertyInfo { get; }
		}

		// Token: 0x0200014B RID: 331
		internal sealed class JsonPropertyInfoList : ConfigurationList<JsonPropertyInfo>
		{
			// Token: 0x06000E09 RID: 3593 RVA: 0x00036ACF File Offset: 0x00034CCF
			public JsonPropertyInfoList(JsonTypeInfo jsonTypeInfo)
				: base(null)
			{
				this._jsonTypeInfo = jsonTypeInfo;
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00036ADF File Offset: 0x00034CDF
			public override bool IsReadOnly
			{
				get
				{
					return (this._jsonTypeInfo._properties == this && this._jsonTypeInfo.IsReadOnly) || this._jsonTypeInfo.Kind != JsonTypeInfoKind.Object;
				}
			}

			// Token: 0x06000E0B RID: 3595 RVA: 0x00036B0F File Offset: 0x00034D0F
			protected override void OnCollectionModifying()
			{
				if (this._jsonTypeInfo._properties == this)
				{
					this._jsonTypeInfo.VerifyMutable();
				}
				if (this._jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(this._jsonTypeInfo.Kind);
				}
			}

			// Token: 0x06000E0C RID: 3596 RVA: 0x00036B48 File Offset: 0x00034D48
			protected override void ValidateAddedValue(JsonPropertyInfo item)
			{
				item.EnsureChildOf(this._jsonTypeInfo);
			}

			// Token: 0x06000E0D RID: 3597 RVA: 0x00036B56 File Offset: 0x00034D56
			public void SortProperties()
			{
				this._list.StableSortByKey((JsonPropertyInfo propInfo) => propInfo.Order);
			}

			// Token: 0x06000E0E RID: 3598 RVA: 0x00036B84 File Offset: 0x00034D84
			public void AddPropertyWithConflictResolution(JsonPropertyInfo jsonPropertyInfo, ref JsonTypeInfo.PropertyHierarchyResolutionState state)
			{
				string memberName = jsonPropertyInfo.MemberName;
				if (state.AddedProperties.TryAdd(jsonPropertyInfo.Name, new global::System.ValueTuple<JsonPropertyInfo, int>(jsonPropertyInfo, base.Count)))
				{
					base.Add(jsonPropertyInfo);
					state.IsPropertyOrderSpecified |= jsonPropertyInfo.Order != 0;
				}
				else
				{
					global::System.ValueTuple<JsonPropertyInfo, int> valueTuple = state.AddedProperties[jsonPropertyInfo.Name];
					JsonPropertyInfo item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					if (item.IsIgnored)
					{
						state.AddedProperties[jsonPropertyInfo.Name] = new global::System.ValueTuple<JsonPropertyInfo, int>(jsonPropertyInfo, item2);
						base[item2] = jsonPropertyInfo;
						state.IsPropertyOrderSpecified |= jsonPropertyInfo.Order != 0;
					}
					else
					{
						bool flag;
						if (!jsonPropertyInfo.IsIgnored && !jsonPropertyInfo.IsOverriddenOrShadowedBy(item))
						{
							Dictionary<string, JsonPropertyInfo> ignoredProperties = state.IgnoredProperties;
							JsonPropertyInfo jsonPropertyInfo2;
							flag = ignoredProperties != null && ignoredProperties.TryGetValue(memberName, out jsonPropertyInfo2) && jsonPropertyInfo.IsOverriddenOrShadowedBy(jsonPropertyInfo2);
						}
						else
						{
							flag = true;
						}
						if (!flag)
						{
							ThrowHelper.ThrowInvalidOperationException_SerializerPropertyNameConflict(this._jsonTypeInfo.Type, jsonPropertyInfo.Name);
						}
					}
				}
				if (jsonPropertyInfo.IsIgnored)
				{
					ref Dictionary<string, JsonPropertyInfo> ptr = ref state.IgnoredProperties;
					Dictionary<string, JsonPropertyInfo> dictionary;
					if ((dictionary = ptr) == null)
					{
						Dictionary<string, JsonPropertyInfo> dictionary2;
						ptr = (dictionary2 = new Dictionary<string, JsonPropertyInfo>());
						dictionary = dictionary2;
					}
					dictionary[memberName] = jsonPropertyInfo;
				}
			}

			// Token: 0x0400051B RID: 1307
			private readonly JsonTypeInfo _jsonTypeInfo;
		}
	}
}
