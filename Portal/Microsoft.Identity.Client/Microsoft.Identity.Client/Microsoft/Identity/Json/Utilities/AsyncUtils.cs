using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000041 RID: 65
	internal static class AsyncUtils
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00010090 File Offset: 0x0000E290
		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return AsyncUtils.False;
			}
			return AsyncUtils.True;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000100A0 File Offset: 0x0000E2A0
		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000100B3 File Offset: 0x0000E2B3
		[return: Nullable(new byte[] { 2, 0 })]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000100C6 File Offset: 0x0000E2C6
		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(delegate
			{
			}, cancellationToken);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000100ED File Offset: 0x0000E2ED
		public static Task<T> FromCanceled<T>(this CancellationToken cancellationToken)
		{
			return new Task<T>(() => default(T), cancellationToken);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00010114 File Offset: 0x0000E314
		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001012D File Offset: 0x0000E32D
		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00010146 File Offset: 0x0000E346
		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00010162 File Offset: 0x0000E362
		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001017E File Offset: 0x0000E37E
		public static bool IsCompletedSucessfully(this Task task)
		{
			return task.Status == TaskStatus.RanToCompletion;
		}

		// Token: 0x0400014C RID: 332
		public static readonly Task<bool> False = Task.FromResult<bool>(false);

		// Token: 0x0400014D RID: 333
		public static readonly Task<bool> True = Task.FromResult<bool>(true);

		// Token: 0x0400014E RID: 334
		internal static readonly Task CompletedTask = Task.Delay(0);
	}
}
