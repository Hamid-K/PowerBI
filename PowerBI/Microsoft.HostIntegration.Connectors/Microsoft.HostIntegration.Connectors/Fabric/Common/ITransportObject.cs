using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200043D RID: 1085
	internal interface ITransportObject
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060025B6 RID: 9654
		TransportState State { get; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060025B7 RID: 9655
		// (set) Token: 0x060025B8 RID: 9656
		object Context { get; set; }

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060025B9 RID: 9657
		// (remove) Token: 0x060025BA RID: 9658
		event EventHandler StateChange;

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060025BB RID: 9659
		Exception FaultingException { get; }

		// Token: 0x060025BC RID: 9660
		void Open();

		// Token: 0x060025BD RID: 9661
		void Open(TimeSpan timeout);

		// Token: 0x060025BE RID: 9662
		IAsyncResult BeginOpen(AsyncCallback callback, object state);

		// Token: 0x060025BF RID: 9663
		IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060025C0 RID: 9664
		void EndOpen(IAsyncResult ar);

		// Token: 0x060025C1 RID: 9665
		void Close();

		// Token: 0x060025C2 RID: 9666
		void Close(TimeSpan timeout);

		// Token: 0x060025C3 RID: 9667
		IAsyncResult BeginClose(AsyncCallback callback, object state);

		// Token: 0x060025C4 RID: 9668
		IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060025C5 RID: 9669
		void EndClose(IAsyncResult ar);

		// Token: 0x060025C6 RID: 9670
		void Abort();

		// Token: 0x060025C7 RID: 9671
		void PoolAsyncResult(IAsyncResult ar);
	}
}
