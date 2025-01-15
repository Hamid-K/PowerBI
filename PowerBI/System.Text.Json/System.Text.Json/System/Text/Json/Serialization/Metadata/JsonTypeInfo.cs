using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x0200009F RID: 159
	public sealed class JsonTypeInfo<[Nullable(2)] T> : JsonTypeInfo
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x00028278 File Offset: 0x00026478
		internal T Deserialize(ref Utf8JsonReader reader, ref ReadStack state)
		{
			return this.EffectiveConverter.ReadCore(ref reader, base.Options, ref state);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00028290 File Offset: 0x00026490
		internal ValueTask<T> DeserializeAsync(Stream utf8Json, CancellationToken cancellationToken)
		{
			JsonTypeInfo<T>.<DeserializeAsync>d__1 <DeserializeAsync>d__;
			<DeserializeAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<T>.Create();
			<DeserializeAsync>d__.<>4__this = this;
			<DeserializeAsync>d__.utf8Json = utf8Json;
			<DeserializeAsync>d__.cancellationToken = cancellationToken;
			<DeserializeAsync>d__.<>1__state = -1;
			<DeserializeAsync>d__.<>t__builder.Start<JsonTypeInfo<T>.<DeserializeAsync>d__1>(ref <DeserializeAsync>d__);
			return <DeserializeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000282E4 File Offset: 0x000264E4
		internal T Deserialize(Stream utf8Json)
		{
			JsonSerializerOptions options = base.Options;
			ReadBufferState readBufferState = new ReadBufferState(options.DefaultBufferSize);
			ReadStack readStack = default(ReadStack);
			readStack.Initialize(this, true);
			JsonReaderState jsonReaderState = new JsonReaderState(options.GetReaderOptions());
			T t2;
			try
			{
				T t;
				do
				{
					readBufferState.ReadFromStream(utf8Json);
					t = this.ContinueDeserialize(ref readBufferState, ref jsonReaderState, ref readStack);
				}
				while (!readBufferState.IsFinalBlock);
				t2 = t;
			}
			finally
			{
				readBufferState.Dispose();
			}
			return t2;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00028364 File Offset: 0x00026564
		internal sealed override object DeserializeAsObject(ref Utf8JsonReader reader, ref ReadStack state)
		{
			return this.Deserialize(ref reader, ref state);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00028374 File Offset: 0x00026574
		internal sealed override async ValueTask<object> DeserializeAsObjectAsync(Stream utf8Json, CancellationToken cancellationToken)
		{
			T t = await this.DeserializeAsync(utf8Json, cancellationToken).ConfigureAwait(false);
			T t2 = t;
			return t2;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000283C7 File Offset: 0x000265C7
		internal sealed override object DeserializeAsObject(Stream utf8Json)
		{
			return this.Deserialize(utf8Json);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000283D8 File Offset: 0x000265D8
		internal T ContinueDeserialize(ref ReadBufferState bufferState, ref JsonReaderState jsonReaderState, ref ReadStack readStack)
		{
			Utf8JsonReader utf8JsonReader = new Utf8JsonReader(bufferState.Bytes, bufferState.IsFinalBlock, jsonReaderState);
			readStack.ReadAhead = !bufferState.IsFinalBlock;
			readStack.BytesConsumed = 0L;
			T t = this.EffectiveConverter.ReadCore(ref utf8JsonReader, base.Options, ref readStack);
			bufferState.AdvanceBuffer((int)readStack.BytesConsumed);
			jsonReaderState = utf8JsonReader.CurrentState;
			return t;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00028448 File Offset: 0x00026648
		internal void Serialize(Utf8JsonWriter writer, in T rootValue, object rootValueBoxed = null)
		{
			if (base.CanUseSerializeHandler)
			{
				this.SerializeHandler(writer, rootValue);
				writer.Flush();
				return;
			}
			JsonTypeInfo jsonTypeInfo;
			if (base.Converter.CanBePolymorphic && rootValue != null && base.Options.TryGetPolymorphicTypeInfoForRootType(rootValue, out jsonTypeInfo))
			{
				jsonTypeInfo.SerializeAsObject(writer, rootValue);
				return;
			}
			WriteStack writeStack = default(WriteStack);
			writeStack.Initialize(this, rootValueBoxed, false, false);
			bool flag = this.EffectiveConverter.WriteCore(writer, in rootValue, base.Options, ref writeStack);
			writer.Flush();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000284EC File Offset: 0x000266EC
		internal async Task SerializeAsync(Stream utf8Json, T rootValue, CancellationToken cancellationToken, object rootValueBoxed = null)
		{
			JsonTypeInfo jsonTypeInfo;
			if (this.CanUseSerializeHandlerInStreaming)
			{
				using (PooledByteBufferWriter bufferWriter = new PooledByteBufferWriter(base.Options.DefaultBufferSize))
				{
					Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriter(base.Options, bufferWriter);
					try
					{
						this.SerializeHandler(utf8JsonWriter, rootValue);
						utf8JsonWriter.Flush();
					}
					finally
					{
						this.OnRootLevelAsyncSerializationCompleted(utf8JsonWriter.BytesCommitted + (long)utf8JsonWriter.BytesPending);
						Utf8JsonWriterCache.ReturnWriter(utf8JsonWriter);
					}
					await bufferWriter.WriteToStreamAsync(utf8Json, cancellationToken).ConfigureAwait(false);
				}
				PooledByteBufferWriter bufferWriter = null;
			}
			else if (base.Converter.CanBePolymorphic && rootValue != null && base.Options.TryGetPolymorphicTypeInfoForRootType(rootValue, out jsonTypeInfo))
			{
				await jsonTypeInfo.SerializeAsObjectAsync(utf8Json, rootValue, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				WriteStack state = default(WriteStack);
				state.Initialize(this, rootValueBoxed, true, true);
				state.CancellationToken = cancellationToken;
				using (PooledByteBufferWriter bufferWriter = new PooledByteBufferWriter(base.Options.DefaultBufferSize))
				{
					using (Utf8JsonWriter writer = new Utf8JsonWriter(bufferWriter, base.Options.GetWriterOptions()))
					{
						int num = 0;
						try
						{
							object obj2;
							for (;;)
							{
								state.FlushThreshold = (int)((float)bufferWriter.Capacity * 0.9f);
								object obj = null;
								bool isFinalBlock;
								try
								{
									isFinalBlock = this.EffectiveConverter.WriteCore(writer, in rootValue, base.Options, ref state);
									writer.Flush();
									if (state.SuppressFlush)
									{
										state.SuppressFlush = false;
									}
									else
									{
										await bufferWriter.WriteToStreamAsync(utf8Json, cancellationToken).ConfigureAwait(false);
										bufferWriter.Clear();
									}
								}
								catch (object obj)
								{
								}
								if (state.PendingTask != null)
								{
									try
									{
										await state.PendingTask.ConfigureAwait(false);
									}
									catch
									{
									}
								}
								List<IAsyncDisposable> completedAsyncDisposables = state.CompletedAsyncDisposables;
								if (completedAsyncDisposables != null && completedAsyncDisposables.Count > 0)
								{
									await state.DisposeCompletedAsyncDisposables().ConfigureAwait(false);
								}
								obj2 = obj;
								if (obj2 != null)
								{
									Exception ex = obj2 as Exception;
									if (ex == null)
									{
										break;
									}
									ExceptionDispatchInfo.Capture(ex).Throw();
								}
								obj = null;
								if (isFinalBlock)
								{
									goto Block_26;
								}
							}
							throw obj2;
							Block_26:;
						}
						catch (object obj3)
						{
							num = 1;
						}
						object obj3;
						if (num == 1)
						{
							await state.DisposePendingDisposablesOnExceptionAsync().ConfigureAwait(false);
							Exception ex = obj3 as Exception;
							if (ex == null)
							{
								throw obj3;
							}
							ExceptionDispatchInfo.Capture(ex).Throw();
						}
						obj3 = null;
						if (base.CanUseSerializeHandler)
						{
							this.OnRootLevelAsyncSerializationCompleted(writer.BytesCommitted);
						}
					}
				}
				state = default(WriteStack);
				PooledByteBufferWriter bufferWriter = null;
				Utf8JsonWriter writer = null;
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00028550 File Offset: 0x00026750
		internal void Serialize(Stream utf8Json, in T rootValue, object rootValueBoxed = null)
		{
			if (this.CanUseSerializeHandlerInStreaming)
			{
				PooledByteBufferWriter pooledByteBufferWriter;
				Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(base.Options, out pooledByteBufferWriter);
				try
				{
					this.SerializeHandler(utf8JsonWriter, rootValue);
					utf8JsonWriter.Flush();
					pooledByteBufferWriter.WriteToStream(utf8Json);
					return;
				}
				finally
				{
					this.OnRootLevelAsyncSerializationCompleted(utf8JsonWriter.BytesCommitted + (long)utf8JsonWriter.BytesPending);
					Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
				}
			}
			JsonTypeInfo jsonTypeInfo;
			if (base.Converter.CanBePolymorphic && rootValue != null && base.Options.TryGetPolymorphicTypeInfoForRootType(rootValue, out jsonTypeInfo))
			{
				jsonTypeInfo.SerializeAsObject(utf8Json, rootValue);
				return;
			}
			WriteStack writeStack = default(WriteStack);
			writeStack.Initialize(this, rootValueBoxed, true, false);
			using (PooledByteBufferWriter pooledByteBufferWriter2 = new PooledByteBufferWriter(base.Options.DefaultBufferSize))
			{
				using (Utf8JsonWriter utf8JsonWriter2 = new Utf8JsonWriter(pooledByteBufferWriter2, base.Options.GetWriterOptions()))
				{
					bool flag;
					do
					{
						writeStack.FlushThreshold = (int)((float)pooledByteBufferWriter2.Capacity * 0.9f);
						flag = this.EffectiveConverter.WriteCore(utf8JsonWriter2, in rootValue, base.Options, ref writeStack);
						utf8JsonWriter2.Flush();
						pooledByteBufferWriter2.WriteToStream(utf8Json);
						pooledByteBufferWriter2.Clear();
					}
					while (!flag);
					if (base.CanUseSerializeHandler)
					{
						this.OnRootLevelAsyncSerializationCompleted(utf8JsonWriter2.BytesCommitted);
					}
				}
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000286D0 File Offset: 0x000268D0
		internal sealed override void SerializeAsObject(Utf8JsonWriter writer, object rootValue)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(rootValue);
			this.Serialize(writer, in t, rootValue);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000286EE File Offset: 0x000268EE
		internal sealed override Task SerializeAsObjectAsync(Stream utf8Json, object rootValue, CancellationToken cancellationToken)
		{
			return this.SerializeAsync(utf8Json, JsonSerializer.UnboxOnWrite<T>(rootValue), cancellationToken, rootValue);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00028700 File Offset: 0x00026900
		internal sealed override void SerializeAsObject(Stream utf8Json, object rootValue)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(rootValue);
			this.Serialize(utf8Json, in t, rootValue);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002871E File Offset: 0x0002691E
		private bool CanUseSerializeHandlerInStreaming
		{
			get
			{
				return this._canUseSerializeHandlerInStreamingState == 1;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002872C File Offset: 0x0002692C
		private void OnRootLevelAsyncSerializationCompleted(long serializationSize)
		{
			if (this._canUseSerializeHandlerInStreamingState != 2)
			{
				if (serializationSize > (long)(base.Options.DefaultBufferSize / 2))
				{
					this._canUseSerializeHandlerInStreamingState = 2;
					return;
				}
				if (this._serializationCount < 10 && Interlocked.Increment(ref this._serializationCount) == 10)
				{
					Interlocked.CompareExchange(ref this._canUseSerializeHandlerInStreamingState, 1, 0);
				}
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00028788 File Offset: 0x00026988
		internal JsonTypeInfo(JsonConverter converter, JsonSerializerOptions options)
			: base(typeof(T), converter, options)
		{
			this.EffectiveConverter = converter.CreateCastingConverter<T>();
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x000287A8 File Offset: 0x000269A8
		[Nullable(1)]
		internal JsonConverter<T> EffectiveConverter { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000287B0 File Offset: 0x000269B0
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x000287B8 File Offset: 0x000269B8
		[Nullable(new byte[] { 2, 1 })]
		public new Func<T> CreateObject
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._typedCreateObject;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this.SetCreateObject(value);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000287C4 File Offset: 0x000269C4
		private protected override void SetCreateObject(Delegate createObject)
		{
			JsonTypeInfo<T>.<>c__DisplayClass29_0 CS$<>8__locals1 = new JsonTypeInfo<T>.<>c__DisplayClass29_0();
			base.VerifyMutable();
			if (base.Kind == JsonTypeInfoKind.None)
			{
				ThrowHelper.ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(base.Kind);
			}
			if (!base.Converter.SupportsCreateObjectDelegate)
			{
				ThrowHelper.ThrowInvalidOperationException_CreateObjectConverterNotCompatible(base.Type);
			}
			Func<T> func;
			if (createObject == null)
			{
				CS$<>8__locals1.untypedCreateObject = null;
				func = null;
			}
			else
			{
				Func<T> typedDelegate = createObject as Func<T>;
				if (typedDelegate != null)
				{
					func = typedDelegate;
					JsonTypeInfo<T>.<>c__DisplayClass29_0 CS$<>8__locals3 = CS$<>8__locals1;
					Func<object> func2 = createObject as Func<object>;
					CS$<>8__locals3.untypedCreateObject = ((func2 != null) ? func2 : (() => typedDelegate()));
				}
				else
				{
					CS$<>8__locals1.untypedCreateObject = (Func<object>)createObject;
					func = () => (T)((object)CS$<>8__locals1.untypedCreateObject());
				}
			}
			this._createObject = CS$<>8__locals1.untypedCreateObject;
			this._typedCreateObject = func;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00028884 File Offset: 0x00026A84
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0002888C File Offset: 0x00026A8C
		[Nullable(new byte[] { 2, 1, 1 })]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Action<Utf8JsonWriter, T> SerializeHandler
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get
			{
				return this._serialize;
			}
			internal set
			{
				this._serialize = value;
				base.HasSerializeHandler = value != null;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002889F File Offset: 0x00026A9F
		private protected override JsonPropertyInfo CreatePropertyInfoForTypeInfo()
		{
			return new JsonPropertyInfo<T>(typeof(T), this, base.Options)
			{
				JsonTypeInfo = this,
				IsForTypeInfo = true
			};
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000288C5 File Offset: 0x00026AC5
		private protected override JsonPropertyInfo CreateJsonPropertyInfo(JsonTypeInfo declaringTypeInfo, Type declaringType, JsonSerializerOptions options)
		{
			return new JsonPropertyInfo<T>(declaringType ?? declaringTypeInfo.Type, declaringTypeInfo, options)
			{
				JsonTypeInfo = this
			};
		}

		// Token: 0x0400031F RID: 799
		internal JsonTypeInfo _asyncEnumerableQueueTypeInfo;

		// Token: 0x04000320 RID: 800
		private volatile int _canUseSerializeHandlerInStreamingState;

		// Token: 0x04000321 RID: 801
		private const int MinSerializationsSampleSize = 10;

		// Token: 0x04000322 RID: 802
		private volatile int _serializationCount;

		// Token: 0x04000323 RID: 803
		private Action<Utf8JsonWriter, T> _serialize;

		// Token: 0x04000324 RID: 804
		private Func<T> _typedCreateObject;
	}
}
