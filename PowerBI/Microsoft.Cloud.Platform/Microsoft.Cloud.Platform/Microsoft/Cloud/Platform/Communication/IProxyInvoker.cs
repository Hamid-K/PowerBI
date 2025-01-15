using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A9 RID: 1193
	public interface IProxyInvoker<T>
	{
		// Token: 0x0600249D RID: 9373
		IAsyncResult BeginInvoke(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Sequencer.AsyncBeginFunction<T> beginExecute, Sequencer.AsyncEndFunction endExecute, AsyncCallback cb, object state);

		// Token: 0x0600249E RID: 9374
		void EndInvoke(IAsyncResult result);

		// Token: 0x0600249F RID: 9375
		Task<TResult> InvokeAsync<TResult>(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Func<T, Task<TResult>> asyncMethod);

		// Token: 0x060024A0 RID: 9376
		Task InvokeAsync(string methodIdentification, object[] keys, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Func<T, Task> asyncMethod);
	}
}
