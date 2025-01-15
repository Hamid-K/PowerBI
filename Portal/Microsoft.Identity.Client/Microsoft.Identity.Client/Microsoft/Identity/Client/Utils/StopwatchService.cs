using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D0 RID: 464
	internal static class StopwatchService
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0004548C File Offset: 0x0004368C
		internal static long CurrentElapsedMilliseconds
		{
			get
			{
				return StopwatchService.Watch.ElapsedMilliseconds;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00045498 File Offset: 0x00043698
		internal static MeasureDurationResult MeasureCodeBlock(Action codeBlock)
		{
			if (codeBlock == null)
			{
				throw new ArgumentNullException("codeBlock");
			}
			long elapsedTicks = StopwatchService.Watch.ElapsedTicks;
			codeBlock();
			return new MeasureDurationResult(StopwatchService.Watch.ElapsedTicks - elapsedTicks);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000454D8 File Offset: 0x000436D8
		internal static async Task<MeasureDurationResult> MeasureCodeBlockAsync(Func<Task> codeBlock)
		{
			if (codeBlock == null)
			{
				throw new ArgumentNullException("codeBlock");
			}
			long startTicks = StopwatchService.Watch.ElapsedTicks;
			await codeBlock().ConfigureAwait(false);
			return new MeasureDurationResult(StopwatchService.Watch.ElapsedTicks - startTicks);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004551C File Offset: 0x0004371C
		internal static async Task<MeasureDurationResult<TResult>> MeasureCodeBlockAsync<TResult>(Func<Task<TResult>> codeBlock)
		{
			if (codeBlock == null)
			{
				throw new ArgumentNullException("codeBlock");
			}
			long startTicks = StopwatchService.Watch.ElapsedTicks;
			TResult tresult = await codeBlock().ConfigureAwait(false);
			return new MeasureDurationResult<TResult>(tresult, StopwatchService.Watch.ElapsedTicks - startTicks);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00045560 File Offset: 0x00043760
		internal static async Task<MeasureDurationResult> MeasureAsync(this Task task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			long startTicks = StopwatchService.Watch.ElapsedTicks;
			await task.ConfigureAwait(false);
			return new MeasureDurationResult(StopwatchService.Watch.ElapsedTicks - startTicks);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000455A4 File Offset: 0x000437A4
		internal static async Task<MeasureDurationResult<TResult>> MeasureAsync<TResult>(this Task<TResult> task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			long startTicks = StopwatchService.Watch.ElapsedTicks;
			TResult tresult = await task.ConfigureAwait(true);
			return new MeasureDurationResult<TResult>(tresult, StopwatchService.Watch.ElapsedTicks - startTicks);
		}

		// Token: 0x04000853 RID: 2131
		internal static readonly Stopwatch Watch = Stopwatch.StartNew();
	}
}
