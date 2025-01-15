using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.ExceptionUtilities;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Dataflow
{
	// Token: 0x02000009 RID: 9
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class TDFHelpers
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002188 File Offset: 0x00000388
		internal static ActionBlock<TInput> CreateActionBlock<[global::System.Runtime.CompilerServices.Nullable(2)] TInput>(Func<TInput, Task> action, string name)
		{
			ExecutionDataflowBlockOptions executionDataflowBlockOptions = new ExecutionDataflowBlockOptions
			{
				SingleProducerConstrained = false,
				NameFormat = name + " {0} {1}"
			};
			return new ActionBlock<TInput>(action, executionDataflowBlockOptions);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021BA File Offset: 0x000003BA
		internal static BufferBlock<TInput> CreateBufferBlock<[global::System.Runtime.CompilerServices.Nullable(2)] TInput>(string name)
		{
			return new BufferBlock<TInput>(new DataflowBlockOptions
			{
				NameFormat = name + " {0} {1}"
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021D8 File Offset: 0x000003D8
		internal static IDisposable LinkWithCompletionPropagation<[global::System.Runtime.CompilerServices.Nullable(2)] TContext>(this ISourceBlock<TContext> sourceBlock, ITargetBlock<TContext> targetBlock)
		{
			DataflowLinkOptions dataflowLinkOptions = new DataflowLinkOptions
			{
				PropagateCompletion = true
			};
			return sourceBlock.LinkTo(targetBlock, dataflowLinkOptions);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021FC File Offset: 0x000003FC
		internal static void Chain<[global::System.Runtime.CompilerServices.Nullable(2)] TInput>(ActionBlock<TInput> action, IDataflowBlock buffer)
		{
			action.Completion.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					buffer.Fault(t.Exception);
					return;
				}
				if (!t.IsCanceled)
				{
					buffer.Complete();
				}
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000222E File Offset: 0x0000042E
		internal static IPropagatorBlock<TInput, TOutput> ChainAndEncapsulate<[global::System.Runtime.CompilerServices.Nullable(2)] TInput, [global::System.Runtime.CompilerServices.Nullable(2)] TOutput>(ActionBlock<TInput> action, ISourceBlock<TOutput> buffer)
		{
			TDFHelpers.Chain<TInput>(action, buffer);
			return DataflowBlock.Encapsulate<TInput, TOutput>(action, buffer);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002240 File Offset: 0x00000440
		internal static IPropagatorBlock<TInput, TOutput> CreateUnorderedTransformBlock<[global::System.Runtime.CompilerServices.Nullable(2)] TInput, [global::System.Runtime.CompilerServices.Nullable(2)] TOutput>(Func<TInput, TOutput> func)
		{
			TDFHelpers.<>c__DisplayClass5_0<TInput, TOutput> CS$<>8__locals1 = new TDFHelpers.<>c__DisplayClass5_0<TInput, TOutput>();
			CS$<>8__locals1.func = func;
			ExecutionDataflowBlockOptions executionDataflowBlockOptions = new ExecutionDataflowBlockOptions
			{
				SingleProducerConstrained = false
			};
			CS$<>8__locals1.buffer = new BufferBlock<TOutput>(executionDataflowBlockOptions);
			return TDFHelpers.ChainAndEncapsulate<TInput, TOutput>(new ActionBlock<TInput>(delegate(TInput input)
			{
				TDFHelpers.<>c__DisplayClass5_0<TInput, TOutput>.<<CreateUnorderedTransformBlock>b__0>d <<CreateUnorderedTransformBlock>b__0>d;
				<<CreateUnorderedTransformBlock>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<CreateUnorderedTransformBlock>b__0>d.<>4__this = CS$<>8__locals1;
				<<CreateUnorderedTransformBlock>b__0>d.input = input;
				<<CreateUnorderedTransformBlock>b__0>d.<>1__state = -1;
				<<CreateUnorderedTransformBlock>b__0>d.<>t__builder.Start<TDFHelpers.<>c__DisplayClass5_0<TInput, TOutput>.<<CreateUnorderedTransformBlock>b__0>d>(ref <<CreateUnorderedTransformBlock>b__0>d);
				return <<CreateUnorderedTransformBlock>b__0>d.<>t__builder.Task;
			}, executionDataflowBlockOptions), CS$<>8__locals1.buffer);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002290 File Offset: 0x00000490
		internal static async Task<T> GetNextResponseAsync<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ISourceBlock<T> sourceBlock, CancellationToken cancellationToken)
		{
			TDFHelpers.<>c__DisplayClass6_0<T> CS$<>8__locals1 = new TDFHelpers.<>c__DisplayClass6_0<T>();
			CS$<>8__locals1.sourceBlock = sourceBlock;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			RuntimeChecks.CheckValue(CS$<>8__locals1.sourceBlock, "sourceBlock");
			CS$<>8__locals1.response = default(T);
			await TDFHelpers.ExecuteBlockOperationAsync(CS$<>8__locals1.sourceBlock, PipelineActivityType.MDGR, delegate
			{
				TDFHelpers.<>c__DisplayClass6_0<T>.<<GetNextResponseAsync>b__0>d <<GetNextResponseAsync>b__0>d;
				<<GetNextResponseAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<GetNextResponseAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetNextResponseAsync>b__0>d.<>1__state = -1;
				<<GetNextResponseAsync>b__0>d.<>t__builder.Start<TDFHelpers.<>c__DisplayClass6_0<T>.<<GetNextResponseAsync>b__0>d>(ref <<GetNextResponseAsync>b__0>d);
				return <<GetNextResponseAsync>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.response;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022DC File Offset: 0x000004DC
		internal static async Task<T> GetNextResponseAsync<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ISourceBlock<T> sourceBlock, TimeSpan timeout, CancellationToken cancellationToken)
		{
			TDFHelpers.<>c__DisplayClass7_0<T> CS$<>8__locals1 = new TDFHelpers.<>c__DisplayClass7_0<T>();
			CS$<>8__locals1.sourceBlock = sourceBlock;
			CS$<>8__locals1.timeout = timeout;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			RuntimeChecks.CheckValue(CS$<>8__locals1.sourceBlock, "sourceBlock");
			CS$<>8__locals1.response = default(T);
			await TDFHelpers.ExecuteBlockOperationAsync(CS$<>8__locals1.sourceBlock, PipelineActivityType.MDGR, delegate
			{
				TDFHelpers.<>c__DisplayClass7_0<T>.<<GetNextResponseAsync>b__0>d <<GetNextResponseAsync>b__0>d;
				<<GetNextResponseAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<GetNextResponseAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<GetNextResponseAsync>b__0>d.<>1__state = -1;
				<<GetNextResponseAsync>b__0>d.<>t__builder.Start<TDFHelpers.<>c__DisplayClass7_0<T>.<<GetNextResponseAsync>b__0>d>(ref <<GetNextResponseAsync>b__0>d);
				return <<GetNextResponseAsync>b__0>d.<>t__builder.Task;
			});
			return CS$<>8__locals1.response;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000232F File Offset: 0x0000052F
		internal static Task<T> GetNextResponseAsync<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ISourceBlock<T> sourceBlock)
		{
			return sourceBlock.GetNextResponseAsync(CancellationToken.None);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000233C File Offset: 0x0000053C
		internal static Task SendRequestAsync<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this ITargetBlock<T> target, T item, CancellationToken? cancellationToken = null)
		{
			TDFHelpers.<>c__DisplayClass9_0<T> CS$<>8__locals1 = new TDFHelpers.<>c__DisplayClass9_0<T>();
			CS$<>8__locals1.target = target;
			CS$<>8__locals1.item = item;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			RuntimeChecks.CheckValue(CS$<>8__locals1.target, "target");
			return TDFHelpers.ExecuteBlockOperationAsync(CS$<>8__locals1.target, PipelineActivityType.MDSR, delegate
			{
				TDFHelpers.<>c__DisplayClass9_0<T>.<<SendRequestAsync>b__0>d <<SendRequestAsync>b__0>d;
				<<SendRequestAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<SendRequestAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<SendRequestAsync>b__0>d.<>1__state = -1;
				<<SendRequestAsync>b__0>d.<>t__builder.Start<TDFHelpers.<>c__DisplayClass9_0<T>.<<SendRequestAsync>b__0>d>(ref <<SendRequestAsync>b__0>d);
				return <<SendRequestAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002390 File Offset: 0x00000590
		internal static bool PostRequest<[global::System.Runtime.CompilerServices.Nullable(2)] TInput>(this ITargetBlock<TInput> target, TInput item)
		{
			return target.Post(item);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000239C File Offset: 0x0000059C
		internal static Task ExecuteBlockOperationAsync(IDataflowBlock block, PipelineActivityType activityType, Func<Task<bool>> func)
		{
			TDFHelpers.<>c__DisplayClass11_0 CS$<>8__locals1 = new TDFHelpers.<>c__DisplayClass11_0();
			CS$<>8__locals1.func = func;
			CS$<>8__locals1.block = block;
			return DiagnosticsContext.TelemetryService.ExecuteInActivity(activityType, delegate
			{
				TDFHelpers.<>c__DisplayClass11_0.<<ExecuteBlockOperationAsync>b__0>d <<ExecuteBlockOperationAsync>b__0>d;
				<<ExecuteBlockOperationAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ExecuteBlockOperationAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteBlockOperationAsync>b__0>d.<>1__state = -1;
				<<ExecuteBlockOperationAsync>b__0>d.<>t__builder.Start<TDFHelpers.<>c__DisplayClass11_0.<<ExecuteBlockOperationAsync>b__0>d>(ref <<ExecuteBlockOperationAsync>b__0>d);
				return <<ExecuteBlockOperationAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D4 File Offset: 0x000005D4
		private static void CheckBlockCompletionStatus(IDataflowBlock block, Exception caughtException = null)
		{
			if (block.Completion.IsFaulted)
			{
				block.Completion.Exception.Unwrap().Throw();
			}
			throw new DataflowPipelineSendOrReceiveException(string.Empty, caughtException, Array.Empty<PowerBIErrorDetail>());
		}
	}
}
