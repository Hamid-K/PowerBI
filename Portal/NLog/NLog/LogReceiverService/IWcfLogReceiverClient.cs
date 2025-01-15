using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace NLog.LogReceiverService
{
	// Token: 0x02000093 RID: 147
	public interface IWcfLogReceiverClient : ICommunicationObject
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060009BD RID: 2493
		// (remove) Token: 0x060009BE RID: 2494
		event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060009BF RID: 2495
		// (remove) Token: 0x060009C0 RID: 2496
		event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060009C1 RID: 2497
		// (remove) Token: 0x060009C2 RID: 2498
		event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060009C3 RID: 2499
		ClientCredentials ClientCredentials { get; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060009C4 RID: 2500
		IClientChannel InnerChannel { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060009C5 RID: 2501
		ServiceEndpoint Endpoint { get; }

		// Token: 0x060009C6 RID: 2502
		void OpenAsync();

		// Token: 0x060009C7 RID: 2503
		void OpenAsync(object userState);

		// Token: 0x060009C8 RID: 2504
		void CloseAsync();

		// Token: 0x060009C9 RID: 2505
		void CloseAsync(object userState);

		// Token: 0x060009CA RID: 2506
		void ProcessLogMessagesAsync(NLogEvents events);

		// Token: 0x060009CB RID: 2507
		void ProcessLogMessagesAsync(NLogEvents events, object userState);

		// Token: 0x060009CC RID: 2508
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x060009CD RID: 2509
		void EndProcessLogMessages(IAsyncResult result);

		// Token: 0x060009CE RID: 2510
		void DisplayInitializationUI();

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009CF RID: 2511
		// (set) Token: 0x060009D0 RID: 2512
		CookieContainer CookieContainer { get; set; }
	}
}
