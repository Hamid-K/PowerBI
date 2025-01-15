using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000285 RID: 645
	public static class SequencerInvoker<TSequencer> where TSequencer : ISequencer
	{
		// Token: 0x0600115F RID: 4447 RVA: 0x0003C8D0 File Offset: 0x0003AAD0
		public static SequencerInvokerAsyncResult<TSequencer> BeginExecute(TSequencer sequencer, WorkTicket ticket, AsyncCallback callback, object context)
		{
			SequencerInvokerAsyncResult<TSequencer> sequencerInvokerAsyncResult2;
			using (DisposeController disposeController = new DisposeController(ticket))
			{
				SequencerInvokerAsyncResult<TSequencer> sequencerInvokerAsyncResult = new SequencerInvokerAsyncResult<TSequencer>(callback, context, ticket, sequencer);
				sequencerInvokerAsyncResult.InnerResult = sequencer.BeginExecute(new AsyncCallback(sequencerInvokerAsyncResult.BeginAsyncFunctionCallback), null);
				disposeController.PreventDispose();
				sequencerInvokerAsyncResult2 = sequencerInvokerAsyncResult;
			}
			return sequencerInvokerAsyncResult2;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003C934 File Offset: 0x0003AB34
		public static TSequencer EndExecute(IAsyncResult asyncResult)
		{
			SequencerInvokerAsyncResult<TSequencer> sequencerInvokerAsyncResult = (SequencerInvokerAsyncResult<TSequencer>)asyncResult;
			TSequencer tsequencer;
			using (sequencerInvokerAsyncResult.WorkTicket)
			{
				sequencerInvokerAsyncResult.End();
				tsequencer = sequencerInvokerAsyncResult.Data;
				tsequencer.EndExecute(sequencerInvokerAsyncResult.InnerResult);
				tsequencer = sequencerInvokerAsyncResult.Sequencer;
			}
			return tsequencer;
		}
	}
}
