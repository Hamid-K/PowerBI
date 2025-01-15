using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C9 RID: 201
	internal sealed class IAsyncEnumerableOfTConverter<TAsyncEnumerable, TElement> : JsonCollectionConverter<TAsyncEnumerable, TElement> where TAsyncEnumerable : IAsyncEnumerable<TElement>
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0002E59D File Offset: 0x0002C79D
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out TAsyncEnumerable value)
		{
			if (!typeToConvert.IsAssignableFrom(typeof(IAsyncEnumerable<TElement>)))
			{
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
			return base.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((IAsyncEnumerableOfTConverter<TAsyncEnumerable, TElement>.BufferedAsyncEnumerable)state.Current.ReturnValue)._buffer.Add(value);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002E5EE File Offset: 0x0002C7EE
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002E5F1 File Offset: 0x0002C7F1
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			state.Current.ReturnValue = new IAsyncEnumerableOfTConverter<TAsyncEnumerable, TElement>.BufferedAsyncEnumerable();
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002E603 File Offset: 0x0002C803
		internal override bool OnTryWrite(Utf8JsonWriter writer, TAsyncEnumerable value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (!state.SupportAsync)
			{
				ThrowHelper.ThrowNotSupportedException_TypeRequiresAsyncSerialization(this.Type);
			}
			return base.OnTryWrite(writer, value, options, ref state);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002E624 File Offset: 0x0002C824
		protected override bool OnWriteResume(Utf8JsonWriter writer, TAsyncEnumerable value, JsonSerializerOptions options, ref WriteStack state)
		{
			IAsyncEnumerator<TElement> asyncEnumerator;
			ValueTask<bool> valueTask;
			if (state.Current.AsyncDisposable == null)
			{
				asyncEnumerator = value.GetAsyncEnumerator(state.CancellationToken);
				state.Current.AsyncDisposable = asyncEnumerator;
				valueTask = asyncEnumerator.MoveNextAsync();
				if (!valueTask.IsCompleted)
				{
					state.SuppressFlush = true;
					goto IL_0106;
				}
			}
			else
			{
				asyncEnumerator = (IAsyncEnumerator<TElement>)state.Current.AsyncDisposable;
				if (state.Current.AsyncEnumeratorIsPendingCompletion)
				{
					valueTask = new ValueTask<bool>((Task<bool>)state.PendingTask);
					state.Current.AsyncEnumeratorIsPendingCompletion = false;
					state.PendingTask = null;
				}
				else
				{
					valueTask = new ValueTask<bool>(true);
				}
			}
			JsonConverter<TElement> elementConverter = JsonCollectionConverter<TAsyncEnumerable, TElement>.GetElementConverter(ref state);
			while (valueTask.Result)
			{
				if (JsonConverter.ShouldFlush(writer, ref state))
				{
					return false;
				}
				TElement telement = asyncEnumerator.Current;
				if (!elementConverter.TryWrite(writer, in telement, options, ref state))
				{
					return false;
				}
				state.Current.EndCollectionElement();
				valueTask = asyncEnumerator.MoveNextAsync();
				if (!valueTask.IsCompleted)
				{
					goto IL_0106;
				}
			}
			state.Current.AsyncDisposable = null;
			state.AddCompletedAsyncDisposable(asyncEnumerator);
			return true;
			IL_0106:
			state.PendingTask = valueTask.AsTask();
			state.Current.AsyncEnumeratorIsPendingCompletion = true;
			return false;
		}

		// Token: 0x02000161 RID: 353
		private sealed class BufferedAsyncEnumerable : IAsyncEnumerable<TElement>
		{
			// Token: 0x06000E4D RID: 3661 RVA: 0x00037251 File Offset: 0x00035451
			[AsyncIteratorStateMachine(typeof(IAsyncEnumerableOfTConverter<, >.BufferedAsyncEnumerable.<GetAsyncEnumerator>d__1))]
			public IAsyncEnumerator<TElement> GetAsyncEnumerator(CancellationToken _)
			{
				IAsyncEnumerableOfTConverter<TAsyncEnumerable, TElement>.BufferedAsyncEnumerable.<GetAsyncEnumerator>d__1 <GetAsyncEnumerator>d__ = new IAsyncEnumerableOfTConverter<TAsyncEnumerable, TElement>.BufferedAsyncEnumerable.<GetAsyncEnumerator>d__1(-3);
				<GetAsyncEnumerator>d__.<>4__this = this;
				return <GetAsyncEnumerator>d__;
			}

			// Token: 0x04000549 RID: 1353
			public readonly List<TElement> _buffer = new List<TElement>();
		}
	}
}
