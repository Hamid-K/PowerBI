using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E10 RID: 7696
	public interface IMessageChannel : IDisposable
	{
		// Token: 0x17002EBC RID: 11964
		// (get) Token: 0x0600BDDF RID: 48607
		IMessenger Messenger { get; }

		// Token: 0x0600BDE0 RID: 48608
		void Post(Message message);

		// Token: 0x0600BDE1 RID: 48609
		Message Read();

		// Token: 0x0600BDE2 RID: 48610
		void TakeOwnership();
	}
}
