using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A1 RID: 1185
	internal interface IChannelInvoker<T>
	{
		// Token: 0x06002481 RID: 9345
		IAsyncResult BeginInvoke(T channel, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Sequencer.AsyncBeginFunction<T> beginExecuteOperation, Sequencer.AsyncEndFunction endExecuteOperation, AsyncCallback cb, object state);

		// Token: 0x06002482 RID: 9346
		void EndInvoke(IAsyncResult result);
	}
}
