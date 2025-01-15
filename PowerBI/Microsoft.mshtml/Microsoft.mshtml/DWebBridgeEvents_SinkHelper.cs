using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC2 RID: 3522
	[ClassInterface(0)]
	internal sealed class DWebBridgeEvents_SinkHelper : DWebBridgeEvents
	{
		// Token: 0x06017652 RID: 95826 RVA: 0x0000FE44 File Offset: 0x0000EE44
		public override void onmouseup()
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate();
				return;
			}
		}

		// Token: 0x06017653 RID: 95827 RVA: 0x0000FE70 File Offset: 0x0000EE70
		public override void onmousemove()
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate();
				return;
			}
		}

		// Token: 0x06017654 RID: 95828 RVA: 0x0000FE9C File Offset: 0x0000EE9C
		public override void onmousedown()
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate();
				return;
			}
		}

		// Token: 0x06017655 RID: 95829 RVA: 0x0000FEC8 File Offset: 0x0000EEC8
		public override bool onkeypress()
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate();
		}

		// Token: 0x06017656 RID: 95830 RVA: 0x0000FEF4 File Offset: 0x0000EEF4
		public override void onkeyup()
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate();
				return;
			}
		}

		// Token: 0x06017657 RID: 95831 RVA: 0x0000FF20 File Offset: 0x0000EF20
		public override void onkeydown()
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate();
				return;
			}
		}

		// Token: 0x06017658 RID: 95832 RVA: 0x0000FF4C File Offset: 0x0000EF4C
		public override bool ondblclick()
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate();
		}

		// Token: 0x06017659 RID: 95833 RVA: 0x0000FF78 File Offset: 0x0000EF78
		public override bool onclick()
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate();
		}

		// Token: 0x0601765A RID: 95834 RVA: 0x0000FFA4 File Offset: 0x0000EFA4
		public override void onreadystatechange()
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate();
				return;
			}
		}

		// Token: 0x0601765B RID: 95835 RVA: 0x0000FFD0 File Offset: 0x0000EFD0
		public override void onscriptletevent(string A_1, object A_2)
		{
			if (this.m_onscriptleteventDelegate != null)
			{
				this.m_onscriptleteventDelegate(A_1, A_2);
				return;
			}
		}

		// Token: 0x0601765C RID: 95836 RVA: 0x00010004 File Offset: 0x0000F004
		internal DWebBridgeEvents_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onmouseupDelegate = null;
			this.m_onmousemoveDelegate = null;
			this.m_onmousedownDelegate = null;
			this.m_onkeypressDelegate = null;
			this.m_onkeyupDelegate = null;
			this.m_onkeydownDelegate = null;
			this.m_ondblclickDelegate = null;
			this.m_onclickDelegate = null;
			this.m_onreadystatechangeDelegate = null;
			this.m_onscriptleteventDelegate = null;
		}

		// Token: 0x04000540 RID: 1344
		public DWebBridgeEvents_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x04000541 RID: 1345
		public DWebBridgeEvents_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x04000542 RID: 1346
		public DWebBridgeEvents_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x04000543 RID: 1347
		public DWebBridgeEvents_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x04000544 RID: 1348
		public DWebBridgeEvents_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x04000545 RID: 1349
		public DWebBridgeEvents_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x04000546 RID: 1350
		public DWebBridgeEvents_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x04000547 RID: 1351
		public DWebBridgeEvents_onclickEventHandler m_onclickDelegate;

		// Token: 0x04000548 RID: 1352
		public DWebBridgeEvents_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x04000549 RID: 1353
		public DWebBridgeEvents_onscriptleteventEventHandler m_onscriptleteventDelegate;

		// Token: 0x0400054A RID: 1354
		public int m_dwCookie;
	}
}
