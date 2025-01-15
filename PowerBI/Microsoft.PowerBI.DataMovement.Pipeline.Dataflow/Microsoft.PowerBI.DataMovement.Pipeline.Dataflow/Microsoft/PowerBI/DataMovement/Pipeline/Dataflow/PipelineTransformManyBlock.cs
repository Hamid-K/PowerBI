using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Dataflow
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class PipelineTransformManyBlock<[global::System.Runtime.CompilerServices.Nullable(2)] TInput, [global::System.Runtime.CompilerServices.Nullable(2)] TOutput> : IPropagatorBlock<TInput, TOutput>, ITargetBlock<TInput>, IDataflowBlock, ISourceBlock<TOutput>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D3 File Offset: 0x000002D3
		protected PipelineTransformManyBlock(int boundedCapacity)
		{
			this.m_underlyingPropagatorBlock = new TransformManyBlock<TInput, TOutput>((TInput input) => this.Execute(input), new ExecutionDataflowBlockOptions
			{
				BoundedCapacity = boundedCapacity,
				SingleProducerConstrained = false
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002105 File Offset: 0x00000305
		public Task Completion
		{
			get
			{
				return this.m_underlyingPropagatorBlock.Completion;
			}
		}

		// Token: 0x0600000C RID: 12
		protected abstract Task<IEnumerable<TOutput>> Execute(TInput input);

		// Token: 0x0600000D RID: 13 RVA: 0x00002112 File Offset: 0x00000312
		public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			return this.m_underlyingPropagatorBlock.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002124 File Offset: 0x00000324
		public void Complete()
		{
			this.m_underlyingPropagatorBlock.Complete();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002131 File Offset: 0x00000331
		public void Fault(Exception exception)
		{
			this.m_underlyingPropagatorBlock.Fault(exception);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000213F File Offset: 0x0000033F
		public TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
		{
			return this.m_underlyingPropagatorBlock.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000214F File Offset: 0x0000034F
		public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
		{
			return this.m_underlyingPropagatorBlock.LinkTo(target, linkOptions);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000215E File Offset: 0x0000035E
		public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			this.m_underlyingPropagatorBlock.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000216D File Offset: 0x0000036D
		public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			return this.m_underlyingPropagatorBlock.ReserveMessage(messageHeader, target);
		}

		// Token: 0x04000012 RID: 18
		private readonly IPropagatorBlock<TInput, TOutput> m_underlyingPropagatorBlock;
	}
}
