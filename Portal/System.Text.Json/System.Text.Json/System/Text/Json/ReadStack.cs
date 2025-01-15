using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
	// Token: 0x02000051 RID: 81
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[StructLayout(LayoutKind.Auto)]
	internal struct ReadStack
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00014976 File Offset: 0x00012B76
		public readonly ref ReadStackFrame Parent
		{
			get
			{
				return ref this._stack[this._count - 2];
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001498B File Offset: 0x00012B8B
		public readonly JsonPropertyInfo ParentProperty
		{
			get
			{
				if (!this.Current.HasParentObject)
				{
					return null;
				}
				return this.Parent.JsonPropertyInfo;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000149A7 File Offset: 0x00012BA7
		public bool IsContinuation
		{
			get
			{
				return this._continuationCount != 0;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000149B2 File Offset: 0x00012BB2
		private void EnsurePushCapacity()
		{
			if (this._stack == null)
			{
				this._stack = new ReadStackFrame[4];
				return;
			}
			if (this._count - 1 == this._stack.Length)
			{
				Array.Resize<ReadStackFrame>(ref this._stack, 2 * this._stack.Length);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000149F0 File Offset: 0x00012BF0
		internal void Initialize(JsonTypeInfo jsonTypeInfo, bool supportContinuation = false)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			if (options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.Preserve)
			{
				this.ReferenceResolver = options.ReferenceHandler.CreateResolver(false);
				this.PreserveReferences = true;
			}
			this.Current.JsonTypeInfo = jsonTypeInfo;
			this.Current.JsonPropertyInfo = jsonTypeInfo.PropertyInfoForTypeInfo;
			this.Current.NumberHandling = this.Current.JsonPropertyInfo.EffectiveNumberHandling;
			bool flag;
			if (!this.PreserveReferences)
			{
				PolymorphicTypeResolver polymorphicTypeResolver = jsonTypeInfo.PolymorphicTypeResolver;
				flag = polymorphicTypeResolver != null && polymorphicTypeResolver.UsesTypeDiscriminators;
			}
			else
			{
				flag = true;
			}
			this.Current.CanContainMetadata = flag;
			this.SupportContinuation = supportContinuation;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00014A90 File Offset: 0x00012C90
		public void Push()
		{
			if (this._continuationCount == 0)
			{
				if (this._count == 0)
				{
					this._count = 1;
				}
				else
				{
					JsonPropertyInfo jsonPropertyInfo = this.Current.JsonPropertyInfo;
					JsonTypeInfo jsonTypeInfo = ((jsonPropertyInfo != null) ? jsonPropertyInfo.JsonTypeInfo : null) ?? this.Current.CtorArgumentState.JsonParameterInfo.JsonTypeInfo;
					JsonNumberHandling? numberHandling = this.Current.NumberHandling;
					this.EnsurePushCapacity();
					this._stack[this._count - 1] = this.Current;
					this.Current = default(ReadStackFrame);
					this._count++;
					this.Current.JsonTypeInfo = jsonTypeInfo;
					this.Current.JsonPropertyInfo = jsonTypeInfo.PropertyInfoForTypeInfo;
					JsonNumberHandling? jsonNumberHandling = numberHandling;
					this.Current.NumberHandling = ((jsonNumberHandling != null) ? jsonNumberHandling : this.Current.JsonPropertyInfo.EffectiveNumberHandling);
					bool flag;
					if (!this.PreserveReferences)
					{
						PolymorphicTypeResolver polymorphicTypeResolver = jsonTypeInfo.PolymorphicTypeResolver;
						flag = polymorphicTypeResolver != null && polymorphicTypeResolver.UsesTypeDiscriminators;
					}
					else
					{
						flag = true;
					}
					this.Current.CanContainMetadata = flag;
				}
			}
			else
			{
				int count = this._count;
				this._count = count + 1;
				if (count > 0)
				{
					this._stack[this._count - 2] = this.Current;
					this.Current = this._stack[this._count - 1];
				}
				if (this._continuationCount == this._count)
				{
					this._continuationCount = 0;
				}
			}
			this.SetConstructorArgumentState();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00014C04 File Offset: 0x00012E04
		public void Pop(bool success)
		{
			int num;
			if (!success)
			{
				if (this._continuationCount == 0)
				{
					if (this._count == 1)
					{
						this._continuationCount = 1;
						this._count = 0;
						return;
					}
					this.EnsurePushCapacity();
					num = this._count;
					this._count = num - 1;
					this._continuationCount = num;
				}
				else
				{
					num = this._count - 1;
					this._count = num;
					if (num == 0)
					{
						return;
					}
				}
				this._stack[this._count] = this.Current;
				this.Current = this._stack[this._count - 1];
				return;
			}
			num = this._count - 1;
			this._count = num;
			if (num > 0)
			{
				this.Current = this._stack[this._count - 1];
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public JsonConverter InitializePolymorphicReEntry(JsonTypeInfo derivedJsonTypeInfo)
		{
			this.Current.PolymorphicJsonTypeInfo = this.Current.JsonTypeInfo;
			this.Current.JsonTypeInfo = derivedJsonTypeInfo;
			this.Current.JsonPropertyInfo = derivedJsonTypeInfo.PropertyInfoForTypeInfo;
			ref JsonNumberHandling? ptr = ref this.Current.NumberHandling;
			JsonNumberHandling? jsonNumberHandling = ptr;
			if (jsonNumberHandling == null)
			{
				ptr = this.Current.JsonPropertyInfo.NumberHandling;
			}
			this.Current.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryStarted;
			this.SetConstructorArgumentState();
			return derivedJsonTypeInfo.Converter;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00014D54 File Offset: 0x00012F54
		public JsonConverter ResumePolymorphicReEntry()
		{
			ref JsonTypeInfo ptr = ref this.Current.JsonTypeInfo;
			JsonTypeInfo polymorphicJsonTypeInfo = this.Current.PolymorphicJsonTypeInfo;
			JsonTypeInfo jsonTypeInfo = this.Current.JsonTypeInfo;
			ptr = polymorphicJsonTypeInfo;
			this.Current.PolymorphicJsonTypeInfo = jsonTypeInfo;
			this.Current.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryStarted;
			return this.Current.JsonTypeInfo.Converter;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public void ExitPolymorphicConverter(bool success)
		{
			ref JsonTypeInfo ptr = ref this.Current.JsonTypeInfo;
			JsonTypeInfo polymorphicJsonTypeInfo = this.Current.PolymorphicJsonTypeInfo;
			JsonTypeInfo jsonTypeInfo = this.Current.JsonTypeInfo;
			ptr = polymorphicJsonTypeInfo;
			this.Current.PolymorphicJsonTypeInfo = jsonTypeInfo;
			this.Current.PolymorphicSerializationState = (success ? PolymorphicSerializationState.None : PolymorphicSerializationState.PolymorphicReEntrySuspended);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00014E08 File Offset: 0x00013008
		public string JsonPath()
		{
			StringBuilder stringBuilder = new StringBuilder("$");
			int continuationCount = this._continuationCount;
			global::System.ValueTuple<int, bool> valueTuple;
			if (continuationCount != 0)
			{
				if (continuationCount != 1)
				{
					valueTuple = new global::System.ValueTuple<int, bool>(continuationCount, false);
				}
				else
				{
					valueTuple = new global::System.ValueTuple<int, bool>(0, true);
				}
			}
			else
			{
				valueTuple = new global::System.ValueTuple<int, bool>(this._count - 1, true);
			}
			global::System.ValueTuple<int, bool> valueTuple2 = valueTuple;
			int item = valueTuple2.Item1;
			bool item2 = valueTuple2.Item2;
			for (int i = 0; i < item; i++)
			{
				ReadStack.<JsonPath>g__AppendStackFrame|24_0(stringBuilder, ref this._stack[i]);
			}
			if (item2)
			{
				ReadStack.<JsonPath>g__AppendStackFrame|24_0(stringBuilder, ref this.Current);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00014EA4 File Offset: 0x000130A4
		public JsonTypeInfo GetTopJsonTypeInfoWithParameterizedConstructor()
		{
			for (int i = 0; i < this._count - 1; i++)
			{
				if (this._stack[i].JsonTypeInfo.UsesParameterizedConstructor)
				{
					return this._stack[i].JsonTypeInfo;
				}
			}
			return this.Current.JsonTypeInfo;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00014EFC File Offset: 0x000130FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetConstructorArgumentState()
		{
			if (this.Current.JsonTypeInfo.UsesParameterizedConstructor)
			{
				ref ArgumentState ptr = ref this.Current.CtorArgumentState;
				if (ptr == null)
				{
					ptr = new ArgumentState();
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00014F34 File Offset: 0x00013134
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				string text = "Path = {0}, Current = ConverterStrategy.{1}, {2}";
				object obj = this.JsonPath();
				JsonTypeInfo jsonTypeInfo = this.Current.JsonTypeInfo;
				object obj2 = ((jsonTypeInfo != null) ? new ConverterStrategy?(jsonTypeInfo.Converter.ConverterStrategy) : null);
				JsonTypeInfo jsonTypeInfo2 = this.Current.JsonTypeInfo;
				return string.Format(text, obj, obj2, (jsonTypeInfo2 != null) ? jsonTypeInfo2.Type.Name : null);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00014F9C File Offset: 0x0001319C
		[CompilerGenerated]
		internal static void <JsonPath>g__AppendStackFrame|24_0(StringBuilder sb, ref ReadStackFrame frame)
		{
			string text = ReadStack.<JsonPath>g__GetPropertyName|24_3(ref frame);
			ReadStack.<JsonPath>g__AppendPropertyName|24_2(sb, text);
			if (frame.JsonTypeInfo != null && frame.IsProcessingEnumerable())
			{
				IEnumerable enumerable = frame.ReturnValue as IEnumerable;
				if (enumerable == null)
				{
					return;
				}
				if (frame.ObjectState == StackFrameObjectState.None || frame.ObjectState == StackFrameObjectState.CreatedObject || frame.ObjectState == StackFrameObjectState.ReadElements)
				{
					sb.Append('[');
					sb.Append(ReadStack.<JsonPath>g__GetCount|24_1(enumerable));
					sb.Append(']');
				}
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00015010 File Offset: 0x00013210
		[CompilerGenerated]
		internal static int <JsonPath>g__GetCount|24_1(IEnumerable enumerable)
		{
			ICollection collection = enumerable as ICollection;
			if (collection != null)
			{
				return collection.Count;
			}
			int num = 0;
			IEnumerator enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015048 File Offset: 0x00013248
		[CompilerGenerated]
		internal static void <JsonPath>g__AppendPropertyName|24_2(StringBuilder sb, string propertyName)
		{
			if (propertyName != null)
			{
				if (propertyName.AsSpan().ContainsSpecialCharacters())
				{
					sb.Append("['");
					sb.Append(propertyName);
					sb.Append("']");
					return;
				}
				sb.Append('.');
				sb.Append(propertyName);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015098 File Offset: 0x00013298
		[CompilerGenerated]
		internal static string <JsonPath>g__GetPropertyName|24_3(ref ReadStackFrame frame)
		{
			string text = null;
			byte[] array = frame.JsonPropertyName;
			if (array == null)
			{
				if (frame.JsonPropertyNameAsString != null)
				{
					text = frame.JsonPropertyNameAsString;
				}
				else
				{
					JsonPropertyInfo jsonPropertyInfo = frame.JsonPropertyInfo;
					byte[] array2;
					if ((array2 = ((jsonPropertyInfo != null) ? jsonPropertyInfo.NameAsUtf8Bytes : null)) == null)
					{
						ArgumentState ctorArgumentState = frame.CtorArgumentState;
						if (ctorArgumentState == null)
						{
							array2 = null;
						}
						else
						{
							JsonParameterInfo jsonParameterInfo = ctorArgumentState.JsonParameterInfo;
							array2 = ((jsonParameterInfo != null) ? jsonParameterInfo.NameAsUtf8Bytes : null);
						}
					}
					array = array2;
				}
			}
			if (array != null)
			{
				text = JsonHelpers.Utf8GetString(array);
			}
			return text;
		}

		// Token: 0x040001D2 RID: 466
		public ReadStackFrame Current;

		// Token: 0x040001D3 RID: 467
		private ReadStackFrame[] _stack;

		// Token: 0x040001D4 RID: 468
		private int _count;

		// Token: 0x040001D5 RID: 469
		private int _continuationCount;

		// Token: 0x040001D6 RID: 470
		public long BytesConsumed;

		// Token: 0x040001D7 RID: 471
		public bool ReadAhead;

		// Token: 0x040001D8 RID: 472
		public ReferenceResolver ReferenceResolver;

		// Token: 0x040001D9 RID: 473
		public bool SupportContinuation;

		// Token: 0x040001DA RID: 474
		public string ReferenceId;

		// Token: 0x040001DB RID: 475
		public object PolymorphicTypeDiscriminator;

		// Token: 0x040001DC RID: 476
		public bool PreserveReferences;
	}
}
