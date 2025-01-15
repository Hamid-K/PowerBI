using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
	// Token: 0x02000052 RID: 82
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[StructLayout(LayoutKind.Auto)]
	internal struct ReadStackFrame
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00015107 File Offset: 0x00013307
		public JsonTypeInfo BaseJsonTypeInfo
		{
			get
			{
				if (this.PolymorphicSerializationState != PolymorphicSerializationState.PolymorphicReEntryStarted)
				{
					return this.JsonTypeInfo;
				}
				return this.PolymorphicJsonTypeInfo;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001511F File Offset: 0x0001331F
		public void EndConstructorParameter()
		{
			this.CtorArgumentState.JsonParameterInfo = null;
			this.JsonPropertyName = null;
			this.PropertyState = StackFramePropertyState.None;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001513B File Offset: 0x0001333B
		public void EndProperty()
		{
			this.JsonPropertyInfo = null;
			this.JsonPropertyName = null;
			this.JsonPropertyNameAsString = null;
			this.PropertyState = StackFramePropertyState.None;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00015159 File Offset: 0x00013359
		public void EndElement()
		{
			this.JsonPropertyNameAsString = null;
			this.PropertyState = StackFramePropertyState.None;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00015169 File Offset: 0x00013369
		public bool IsProcessingDictionary()
		{
			return this.JsonTypeInfo.Kind == JsonTypeInfoKind.Dictionary;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015179 File Offset: 0x00013379
		public bool IsProcessingEnumerable()
		{
			return this.JsonTypeInfo.Kind == JsonTypeInfoKind.Enumerable;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015189 File Offset: 0x00013389
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MarkRequiredPropertyAsRead(JsonPropertyInfo propertyInfo)
		{
			if (propertyInfo.IsRequired)
			{
				this.RequiredPropertiesSet[propertyInfo.RequiredPropertyIndex] = true;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000151A5 File Offset: 0x000133A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void InitializeRequiredPropertiesValidationState(JsonTypeInfo typeInfo)
		{
			if (typeInfo.NumberOfRequiredProperties > 0)
			{
				this.RequiredPropertiesSet = new BitArray(typeInfo.NumberOfRequiredProperties);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000151C1 File Offset: 0x000133C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ValidateAllRequiredPropertiesAreRead(JsonTypeInfo typeInfo)
		{
			if (typeInfo.NumberOfRequiredProperties > 0 && !this.RequiredPropertiesSet.HasAllSet())
			{
				ThrowHelper.ThrowJsonException_JsonRequiredPropertyMissing(typeInfo, this.RequiredPropertiesSet);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000151E8 File Offset: 0x000133E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				string text = "ConverterStrategy.{0}, {1}";
				JsonTypeInfo jsonTypeInfo = this.JsonTypeInfo;
				object obj = ((jsonTypeInfo != null) ? new ConverterStrategy?(jsonTypeInfo.Converter.ConverterStrategy) : null);
				JsonTypeInfo jsonTypeInfo2 = this.JsonTypeInfo;
				return string.Format(text, obj, (jsonTypeInfo2 != null) ? jsonTypeInfo2.Type.Name : null);
			}
		}

		// Token: 0x040001DD RID: 477
		public JsonPropertyInfo JsonPropertyInfo;

		// Token: 0x040001DE RID: 478
		public StackFramePropertyState PropertyState;

		// Token: 0x040001DF RID: 479
		public bool UseExtensionProperty;

		// Token: 0x040001E0 RID: 480
		public byte[] JsonPropertyName;

		// Token: 0x040001E1 RID: 481
		public string JsonPropertyNameAsString;

		// Token: 0x040001E2 RID: 482
		public object DictionaryKey;

		// Token: 0x040001E3 RID: 483
		public object ReturnValue;

		// Token: 0x040001E4 RID: 484
		public JsonTypeInfo JsonTypeInfo;

		// Token: 0x040001E5 RID: 485
		public StackFrameObjectState ObjectState;

		// Token: 0x040001E6 RID: 486
		public bool CanContainMetadata;

		// Token: 0x040001E7 RID: 487
		public MetadataPropertyName LatestMetadataPropertyName;

		// Token: 0x040001E8 RID: 488
		public MetadataPropertyName MetadataPropertyNames;

		// Token: 0x040001E9 RID: 489
		public PolymorphicSerializationState PolymorphicSerializationState;

		// Token: 0x040001EA RID: 490
		public JsonTypeInfo PolymorphicJsonTypeInfo;

		// Token: 0x040001EB RID: 491
		public int PropertyIndex;

		// Token: 0x040001EC RID: 492
		public List<PropertyRef> PropertyRefCache;

		// Token: 0x040001ED RID: 493
		public ArgumentState CtorArgumentState;

		// Token: 0x040001EE RID: 494
		public JsonNumberHandling? NumberHandling;

		// Token: 0x040001EF RID: 495
		public BitArray RequiredPropertiesSet;

		// Token: 0x040001F0 RID: 496
		public bool HasParentObject;

		// Token: 0x040001F1 RID: 497
		public bool IsPopulating;
	}
}
