using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000284 RID: 644
	public sealed class SequencerInvokerAsyncResult<TSequencer> : ChainedAsyncResult<WorkTicket, TSequencer> where TSequencer : ISequencer
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x0003C8AE File Offset: 0x0003AAAE
		public SequencerInvokerAsyncResult(AsyncCallback callback, object context, WorkTicket ticket, [NotNull] TSequencer sequencer)
			: base(callback, context, ticket, sequencer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TSequencer>(sequencer, "sequencer");
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0003C8C7 File Offset: 0x0003AAC7
		public TSequencer Sequencer
		{
			get
			{
				return base.Data;
			}
		}
	}
}
