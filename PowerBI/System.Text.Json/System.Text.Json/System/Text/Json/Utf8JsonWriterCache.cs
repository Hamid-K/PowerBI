using System;

namespace System.Text.Json
{
	// Token: 0x0200004D RID: 77
	internal static class Utf8JsonWriterCache
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x00013CD0 File Offset: 0x00011ED0
		public static Utf8JsonWriter RentWriterAndBuffer(JsonSerializerOptions options, out PooledByteBufferWriter bufferWriter)
		{
			Utf8JsonWriterCache.ThreadLocalState threadLocalState;
			if ((threadLocalState = Utf8JsonWriterCache.t_threadLocalState) == null)
			{
				threadLocalState = (Utf8JsonWriterCache.t_threadLocalState = new Utf8JsonWriterCache.ThreadLocalState());
			}
			Utf8JsonWriterCache.ThreadLocalState threadLocalState2 = threadLocalState;
			Utf8JsonWriterCache.ThreadLocalState threadLocalState3 = threadLocalState2;
			int rentedWriters = threadLocalState3.RentedWriters;
			threadLocalState3.RentedWriters = rentedWriters + 1;
			Utf8JsonWriter utf8JsonWriter;
			if (rentedWriters == 0)
			{
				bufferWriter = threadLocalState2.BufferWriter;
				utf8JsonWriter = threadLocalState2.Writer;
				bufferWriter.InitializeEmptyInstance(options.DefaultBufferSize);
				utf8JsonWriter.Reset(bufferWriter, options.GetWriterOptions());
			}
			else
			{
				bufferWriter = new PooledByteBufferWriter(options.DefaultBufferSize);
				utf8JsonWriter = new Utf8JsonWriter(bufferWriter, options.GetWriterOptions());
			}
			return utf8JsonWriter;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00013D50 File Offset: 0x00011F50
		public static Utf8JsonWriter RentWriter(JsonSerializerOptions options, PooledByteBufferWriter bufferWriter)
		{
			Utf8JsonWriterCache.ThreadLocalState threadLocalState;
			if ((threadLocalState = Utf8JsonWriterCache.t_threadLocalState) == null)
			{
				threadLocalState = (Utf8JsonWriterCache.t_threadLocalState = new Utf8JsonWriterCache.ThreadLocalState());
			}
			Utf8JsonWriterCache.ThreadLocalState threadLocalState2 = threadLocalState;
			Utf8JsonWriterCache.ThreadLocalState threadLocalState3 = threadLocalState2;
			int rentedWriters = threadLocalState3.RentedWriters;
			threadLocalState3.RentedWriters = rentedWriters + 1;
			Utf8JsonWriter utf8JsonWriter;
			if (rentedWriters == 0)
			{
				utf8JsonWriter = threadLocalState2.Writer;
				utf8JsonWriter.Reset(bufferWriter, options.GetWriterOptions());
			}
			else
			{
				utf8JsonWriter = new Utf8JsonWriter(bufferWriter, options.GetWriterOptions());
			}
			return utf8JsonWriter;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00013DAC File Offset: 0x00011FAC
		public static void ReturnWriterAndBuffer(Utf8JsonWriter writer, PooledByteBufferWriter bufferWriter)
		{
			Utf8JsonWriterCache.ThreadLocalState threadLocalState = Utf8JsonWriterCache.t_threadLocalState;
			writer.ResetAllStateForCacheReuse();
			bufferWriter.ClearAndReturnBuffers();
			Utf8JsonWriterCache.ThreadLocalState threadLocalState2 = threadLocalState;
			int num = threadLocalState2.RentedWriters - 1;
			threadLocalState2.RentedWriters = num;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public static void ReturnWriter(Utf8JsonWriter writer)
		{
			Utf8JsonWriterCache.ThreadLocalState threadLocalState = Utf8JsonWriterCache.t_threadLocalState;
			writer.ResetAllStateForCacheReuse();
			Utf8JsonWriterCache.ThreadLocalState threadLocalState2 = threadLocalState;
			int num = threadLocalState2.RentedWriters - 1;
			threadLocalState2.RentedWriters = num;
		}

		// Token: 0x040001A3 RID: 419
		[ThreadStatic]
		private static Utf8JsonWriterCache.ThreadLocalState t_threadLocalState;

		// Token: 0x02000120 RID: 288
		private sealed class ThreadLocalState
		{
			// Token: 0x06000D94 RID: 3476 RVA: 0x00034959 File Offset: 0x00032B59
			public ThreadLocalState()
			{
				this.BufferWriter = PooledByteBufferWriter.CreateEmptyInstanceForCaching();
				this.Writer = Utf8JsonWriter.CreateEmptyInstanceForCaching();
			}

			// Token: 0x04000489 RID: 1161
			public readonly PooledByteBufferWriter BufferWriter;

			// Token: 0x0400048A RID: 1162
			public readonly Utf8JsonWriter Writer;

			// Token: 0x0400048B RID: 1163
			public int RentedWriters;
		}
	}
}
