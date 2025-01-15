using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0D RID: 7693
	public interface IMessenger : IDisposable
	{
		// Token: 0x17002EBA RID: 11962
		// (get) Token: 0x0600BDD7 RID: 48599
		IMessageSerializer Serializer { get; }

		// Token: 0x17002EBB RID: 11963
		// (get) Token: 0x0600BDD8 RID: 48600
		IMessageHandlers Handlers { get; }

		// Token: 0x0600BDD9 RID: 48601
		IMessageChannel CreateChannel();
	}
}
