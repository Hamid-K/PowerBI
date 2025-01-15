using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
	// Token: 0x02000055 RID: 85
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[StructLayout(LayoutKind.Auto)]
	internal struct WriteStack
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001523F File Offset: 0x0001343F
		public readonly int CurrentDepth
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00015247 File Offset: 0x00013447
		public readonly ref WriteStackFrame Parent
		{
			get
			{
				return ref this._stack[this._count - (int)this._indexOffset - 1];
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00015263 File Offset: 0x00013463
		public readonly bool IsContinuation
		{
			get
			{
				return this._continuationCount != 0;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001526E File Offset: 0x0001346E
		public readonly bool CurrentContainsMetadata
		{
			get
			{
				return this.NewReferenceId != null || this.PolymorphicTypeDiscriminator != null;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00015284 File Offset: 0x00013484
		private void EnsurePushCapacity()
		{
			if (this._stack == null)
			{
				this._stack = new WriteStackFrame[4];
				return;
			}
			if (this._count - (int)this._indexOffset == this._stack.Length)
			{
				Array.Resize<WriteStackFrame>(ref this._stack, 2 * this._stack.Length);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000152D4 File Offset: 0x000134D4
		internal void Initialize(JsonTypeInfo jsonTypeInfo, object rootValueBoxed = null, bool supportContinuation = false, bool supportAsync = false)
		{
			this.Current.JsonTypeInfo = jsonTypeInfo;
			this.Current.JsonPropertyInfo = jsonTypeInfo.PropertyInfoForTypeInfo;
			this.Current.NumberHandling = this.Current.JsonPropertyInfo.EffectiveNumberHandling;
			this.SupportContinuation = supportContinuation;
			this.SupportAsync = supportAsync;
			JsonSerializerOptions options = jsonTypeInfo.Options;
			if (options.ReferenceHandlingStrategy != ReferenceHandlingStrategy.None)
			{
				this.ReferenceResolver = options.ReferenceHandler.CreateResolver(true);
				if (options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.IgnoreCycles && rootValueBoxed != null && jsonTypeInfo.Type.IsValueType)
				{
					this.ReferenceResolver.PushReferenceForCycleDetection(rootValueBoxed);
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001536E File Offset: 0x0001356E
		public readonly JsonTypeInfo PeekNestedJsonTypeInfo()
		{
			if (this._count != 0)
			{
				return this.Current.JsonPropertyInfo.JsonTypeInfo;
			}
			return this.Current.JsonTypeInfo;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015394 File Offset: 0x00013594
		public void Push()
		{
			if (this._continuationCount != 0)
			{
				int count = this._count;
				this._count = count + 1;
				if (count > 0 || this._indexOffset == 0)
				{
					this.Current = this._stack[this._count - (int)this._indexOffset];
				}
				if (this._continuationCount == this._count)
				{
					this._continuationCount = 0;
				}
				return;
			}
			if (this._count == 0 && this.Current.PolymorphicSerializationState == PolymorphicSerializationState.None)
			{
				this._count = 1;
				this._indexOffset = 1;
				return;
			}
			JsonTypeInfo nestedJsonTypeInfo = this.Current.GetNestedJsonTypeInfo();
			JsonNumberHandling? numberHandling = this.Current.NumberHandling;
			this.EnsurePushCapacity();
			this._stack[this._count - (int)this._indexOffset] = this.Current;
			this.Current = default(WriteStackFrame);
			this._count++;
			this.Current.JsonTypeInfo = nestedJsonTypeInfo;
			this.Current.JsonPropertyInfo = nestedJsonTypeInfo.PropertyInfoForTypeInfo;
			JsonNumberHandling? jsonNumberHandling = numberHandling;
			this.Current.NumberHandling = ((jsonNumberHandling != null) ? jsonNumberHandling : this.Current.JsonPropertyInfo.EffectiveNumberHandling);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000154BC File Offset: 0x000136BC
		public void Pop(bool success)
		{
			int num;
			if (!success)
			{
				if (this._continuationCount == 0)
				{
					if (this._count == 1 && this._indexOffset > 0)
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
					if (num == 0 && this._indexOffset > 0)
					{
						return;
					}
				}
				int num2 = this._count - (int)this._indexOffset;
				this._stack[num2 + 1] = this.Current;
				this.Current = this._stack[num2];
				return;
			}
			num = this._count - 1;
			this._count = num;
			if (num > 0 || this._indexOffset == 0)
			{
				this.Current = this._stack[this._count - (int)this._indexOffset];
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000155A4 File Offset: 0x000137A4
		public void AddCompletedAsyncDisposable(IAsyncDisposable asyncDisposable)
		{
			List<IAsyncDisposable> list;
			if ((list = this.CompletedAsyncDisposables) == null)
			{
				list = (this.CompletedAsyncDisposables = new List<IAsyncDisposable>());
			}
			list.Add(asyncDisposable);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000155D0 File Offset: 0x000137D0
		public async ValueTask DisposeCompletedAsyncDisposables()
		{
			Exception exception = null;
			foreach (IAsyncDisposable asyncDisposable in this.CompletedAsyncDisposables)
			{
				try
				{
					await asyncDisposable.DisposeAsync().ConfigureAwait(false);
				}
				catch (Exception exception)
				{
				}
			}
			List<IAsyncDisposable>.Enumerator enumerator = default(List<IAsyncDisposable>.Enumerator);
			if (exception != null)
			{
				ExceptionDispatchInfo.Capture(exception).Throw();
			}
			this.CompletedAsyncDisposables.Clear();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015618 File Offset: 0x00013818
		public void DisposePendingDisposablesOnException()
		{
			Exception ex = null;
			WriteStack.<DisposePendingDisposablesOnException>g__DisposeFrame|31_0(this.Current.CollectionEnumerator, ref ex);
			int num = Math.Max(this._count, this._continuationCount);
			for (int i = 0; i < num - 1; i++)
			{
				WriteStack.<DisposePendingDisposablesOnException>g__DisposeFrame|31_0(this._stack[i].CollectionEnumerator, ref ex);
			}
			if (ex != null)
			{
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015680 File Offset: 0x00013880
		public async ValueTask DisposePendingDisposablesOnExceptionAsync()
		{
			Exception ex = null;
			Exception ex2 = await WriteStack.<DisposePendingDisposablesOnExceptionAsync>g__DisposeFrame|32_0(this.Current.CollectionEnumerator, this.Current.AsyncDisposable, ex).ConfigureAwait(false);
			ex = ex2;
			int stackSize = Math.Max(this._count, this._continuationCount);
			for (int i = 0; i < stackSize - 1; i++)
			{
				ex = await WriteStack.<DisposePendingDisposablesOnExceptionAsync>g__DisposeFrame|32_0(this._stack[i].CollectionEnumerator, this._stack[i].AsyncDisposable, ex).ConfigureAwait(false);
			}
			if (ex != null)
			{
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000156C8 File Offset: 0x000138C8
		public string PropertyPath()
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
			for (int i = 1; i <= item; i++)
			{
				WriteStack.<PropertyPath>g__AppendStackFrame|33_0(stringBuilder, ref this._stack[i - (int)this._indexOffset]);
			}
			if (item2)
			{
				WriteStack.<PropertyPath>g__AppendStackFrame|33_0(stringBuilder, ref this.Current);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001576C File Offset: 0x0001396C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				string text = "Path = {0} Current = ConverterStrategy.{1}, {2}";
				object obj = this.PropertyPath();
				JsonPropertyInfo jsonPropertyInfo = this.Current.JsonPropertyInfo;
				object obj2 = ((jsonPropertyInfo != null) ? new ConverterStrategy?(jsonPropertyInfo.EffectiveConverter.ConverterStrategy) : null);
				JsonTypeInfo jsonTypeInfo = this.Current.JsonTypeInfo;
				return string.Format(text, obj, obj2, (jsonTypeInfo != null) ? jsonTypeInfo.Type.Name : null);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000157D4 File Offset: 0x000139D4
		[CompilerGenerated]
		internal static void <DisposePendingDisposablesOnException>g__DisposeFrame|31_0(IEnumerator collectionEnumerator, ref Exception exception)
		{
			try
			{
				IDisposable disposable = collectionEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			catch (Exception ex)
			{
				exception = ex;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001580C File Offset: 0x00013A0C
		[CompilerGenerated]
		internal static async ValueTask<Exception> <DisposePendingDisposablesOnExceptionAsync>g__DisposeFrame|32_0(IEnumerator collectionEnumerator, IAsyncDisposable asyncDisposable, Exception ex)
		{
			try
			{
				IDisposable disposable = collectionEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				else if (asyncDisposable != null)
				{
					await asyncDisposable.DisposeAsync().ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
			}
			return ex;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015860 File Offset: 0x00013A60
		[CompilerGenerated]
		internal static void <PropertyPath>g__AppendStackFrame|33_0(StringBuilder sb, ref WriteStackFrame frame)
		{
			JsonPropertyInfo jsonPropertyInfo = frame.JsonPropertyInfo;
			string text = ((jsonPropertyInfo != null) ? jsonPropertyInfo.MemberName : null) ?? frame.JsonPropertyNameAsString;
			WriteStack.<PropertyPath>g__AppendPropertyName|33_1(sb, text);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015894 File Offset: 0x00013A94
		[CompilerGenerated]
		internal static void <PropertyPath>g__AppendPropertyName|33_1(StringBuilder sb, string propertyName)
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

		// Token: 0x04000202 RID: 514
		public WriteStackFrame Current;

		// Token: 0x04000203 RID: 515
		private WriteStackFrame[] _stack;

		// Token: 0x04000204 RID: 516
		private int _count;

		// Token: 0x04000205 RID: 517
		private int _continuationCount;

		// Token: 0x04000206 RID: 518
		private byte _indexOffset;

		// Token: 0x04000207 RID: 519
		public CancellationToken CancellationToken;

		// Token: 0x04000208 RID: 520
		public bool SuppressFlush;

		// Token: 0x04000209 RID: 521
		public Task PendingTask;

		// Token: 0x0400020A RID: 522
		public List<IAsyncDisposable> CompletedAsyncDisposables;

		// Token: 0x0400020B RID: 523
		public int FlushThreshold;

		// Token: 0x0400020C RID: 524
		public ReferenceResolver ReferenceResolver;

		// Token: 0x0400020D RID: 525
		public bool SupportContinuation;

		// Token: 0x0400020E RID: 526
		public bool SupportAsync;

		// Token: 0x0400020F RID: 527
		public string NewReferenceId;

		// Token: 0x04000210 RID: 528
		public object PolymorphicTypeDiscriminator;

		// Token: 0x04000211 RID: 529
		public PolymorphicTypeResolver PolymorphicTypeResolver;
	}
}
