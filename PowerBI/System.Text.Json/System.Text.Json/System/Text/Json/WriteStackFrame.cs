using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
	// Token: 0x02000056 RID: 86
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[StructLayout(LayoutKind.Auto)]
	internal struct WriteStackFrame
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x000158E3 File Offset: 0x00013AE3
		public void EndCollectionElement()
		{
			this.PolymorphicSerializationState = PolymorphicSerializationState.None;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000158EC File Offset: 0x00013AEC
		public void EndDictionaryEntry()
		{
			this.PropertyState = StackFramePropertyState.None;
			this.PolymorphicSerializationState = PolymorphicSerializationState.None;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000158FC File Offset: 0x00013AFC
		public void EndProperty()
		{
			this.JsonPropertyInfo = null;
			this.JsonPropertyNameAsString = null;
			this.PropertyState = StackFramePropertyState.None;
			this.PolymorphicSerializationState = PolymorphicSerializationState.None;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001591A File Offset: 0x00013B1A
		public readonly JsonTypeInfo GetNestedJsonTypeInfo()
		{
			if (this.PolymorphicSerializationState != PolymorphicSerializationState.PolymorphicReEntryStarted)
			{
				return this.JsonPropertyInfo.JsonTypeInfo;
			}
			return this.PolymorphicTypeInfo;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00015938 File Offset: 0x00013B38
		public JsonTypeInfo InitializePolymorphicReEntry(Type runtimeType, JsonSerializerOptions options)
		{
			JsonTypeInfo polymorphicTypeInfo = this.PolymorphicTypeInfo;
			if (((polymorphicTypeInfo != null) ? polymorphicTypeInfo.Type : null) != runtimeType)
			{
				JsonTypeInfo typeInfoInternal = options.GetTypeInfoInternal(runtimeType, true, new bool?(true), false, true);
				this.PolymorphicTypeInfo = typeInfoInternal.AncestorPolymorphicType ?? typeInfoInternal;
			}
			this.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryStarted;
			return this.PolymorphicTypeInfo;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001598E File Offset: 0x00013B8E
		public JsonConverter InitializePolymorphicReEntry(JsonTypeInfo derivedJsonTypeInfo)
		{
			this.PolymorphicTypeInfo = derivedJsonTypeInfo;
			this.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryStarted;
			return derivedJsonTypeInfo.Converter;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000159A4 File Offset: 0x00013BA4
		public JsonConverter ResumePolymorphicReEntry()
		{
			this.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryStarted;
			return this.PolymorphicTypeInfo.Converter;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000159B8 File Offset: 0x00013BB8
		public void ExitPolymorphicConverter(bool success)
		{
			this.PolymorphicSerializationState = (success ? PolymorphicSerializationState.None : PolymorphicSerializationState.PolymorphicReEntrySuspended);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000159C8 File Offset: 0x00013BC8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string DebuggerDisplay
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

		// Token: 0x04000212 RID: 530
		public IEnumerator CollectionEnumerator;

		// Token: 0x04000213 RID: 531
		public IAsyncDisposable AsyncDisposable;

		// Token: 0x04000214 RID: 532
		public bool AsyncEnumeratorIsPendingCompletion;

		// Token: 0x04000215 RID: 533
		public JsonPropertyInfo JsonPropertyInfo;

		// Token: 0x04000216 RID: 534
		public bool IsWritingExtensionDataProperty;

		// Token: 0x04000217 RID: 535
		public JsonTypeInfo JsonTypeInfo;

		// Token: 0x04000218 RID: 536
		public int OriginalDepth;

		// Token: 0x04000219 RID: 537
		public bool ProcessedStartToken;

		// Token: 0x0400021A RID: 538
		public bool ProcessedEndToken;

		// Token: 0x0400021B RID: 539
		public StackFramePropertyState PropertyState;

		// Token: 0x0400021C RID: 540
		public int EnumeratorIndex;

		// Token: 0x0400021D RID: 541
		public string JsonPropertyNameAsString;

		// Token: 0x0400021E RID: 542
		public MetadataPropertyName MetadataPropertyName;

		// Token: 0x0400021F RID: 543
		public PolymorphicSerializationState PolymorphicSerializationState;

		// Token: 0x04000220 RID: 544
		public JsonTypeInfo PolymorphicTypeInfo;

		// Token: 0x04000221 RID: 545
		public JsonNumberHandling? NumberHandling;

		// Token: 0x04000222 RID: 546
		public bool IsPushedReferenceForCycleDetection;
	}
}
