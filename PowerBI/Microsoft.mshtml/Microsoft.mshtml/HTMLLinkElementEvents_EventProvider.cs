using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E05 RID: 3589
	internal sealed class HTMLLinkElementEvents_EventProvider : HTMLLinkElementEvents_Event, IDisposable
	{
		// Token: 0x06018CBA RID: 101562 RVA: 0x000DA5D0 File Offset: 0x000D95D0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				204, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018CBB RID: 101563 RVA: 0x000DA6E4 File Offset: 0x000D96E4
		public override void add_onerror(HTMLLinkElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CBC RID: 101564 RVA: 0x000DA774 File Offset: 0x000D9774
		public override void remove_onerror(HTMLLinkElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CBD RID: 101565 RVA: 0x000DA864 File Offset: 0x000D9864
		public override void add_onload(HTMLLinkElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CBE RID: 101566 RVA: 0x000DA8F4 File Offset: 0x000D98F4
		public override void remove_onload(HTMLLinkElementEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onloadDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CBF RID: 101567 RVA: 0x000DA9E4 File Offset: 0x000D99E4
		public override void add_onfocusout(HTMLLinkElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CC0 RID: 101568 RVA: 0x000DAA74 File Offset: 0x000D9A74
		public override void remove_onfocusout(HTMLLinkElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CC1 RID: 101569 RVA: 0x000DAB64 File Offset: 0x000D9B64
		public override void add_onfocusin(HTMLLinkElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CC2 RID: 101570 RVA: 0x000DABF4 File Offset: 0x000D9BF4
		public override void remove_onfocusin(HTMLLinkElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CC3 RID: 101571 RVA: 0x000DACE4 File Offset: 0x000D9CE4
		public override void add_ondeactivate(HTMLLinkElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CC4 RID: 101572 RVA: 0x000DAD74 File Offset: 0x000D9D74
		public override void remove_ondeactivate(HTMLLinkElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CC5 RID: 101573 RVA: 0x000DAE64 File Offset: 0x000D9E64
		public override void add_onactivate(HTMLLinkElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CC6 RID: 101574 RVA: 0x000DAEF4 File Offset: 0x000D9EF4
		public override void remove_onactivate(HTMLLinkElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CC7 RID: 101575 RVA: 0x000DAFE4 File Offset: 0x000D9FE4
		public override void add_onmousewheel(HTMLLinkElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CC8 RID: 101576 RVA: 0x000DB074 File Offset: 0x000DA074
		public override void remove_onmousewheel(HTMLLinkElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CC9 RID: 101577 RVA: 0x000DB164 File Offset: 0x000DA164
		public override void add_onmouseleave(HTMLLinkElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CCA RID: 101578 RVA: 0x000DB1F4 File Offset: 0x000DA1F4
		public override void remove_onmouseleave(HTMLLinkElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CCB RID: 101579 RVA: 0x000DB2E4 File Offset: 0x000DA2E4
		public override void add_onmouseenter(HTMLLinkElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CCC RID: 101580 RVA: 0x000DB374 File Offset: 0x000DA374
		public override void remove_onmouseenter(HTMLLinkElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CCD RID: 101581 RVA: 0x000DB464 File Offset: 0x000DA464
		public override void add_onresizeend(HTMLLinkElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CCE RID: 101582 RVA: 0x000DB4F4 File Offset: 0x000DA4F4
		public override void remove_onresizeend(HTMLLinkElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CCF RID: 101583 RVA: 0x000DB5E4 File Offset: 0x000DA5E4
		public override void add_onresizestart(HTMLLinkElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CD0 RID: 101584 RVA: 0x000DB674 File Offset: 0x000DA674
		public override void remove_onresizestart(HTMLLinkElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CD1 RID: 101585 RVA: 0x000DB764 File Offset: 0x000DA764
		public override void add_onmoveend(HTMLLinkElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CD2 RID: 101586 RVA: 0x000DB7F4 File Offset: 0x000DA7F4
		public override void remove_onmoveend(HTMLLinkElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CD3 RID: 101587 RVA: 0x000DB8E4 File Offset: 0x000DA8E4
		public override void add_onmovestart(HTMLLinkElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CD4 RID: 101588 RVA: 0x000DB974 File Offset: 0x000DA974
		public override void remove_onmovestart(HTMLLinkElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CD5 RID: 101589 RVA: 0x000DBA64 File Offset: 0x000DAA64
		public override void add_oncontrolselect(HTMLLinkElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CD6 RID: 101590 RVA: 0x000DBAF4 File Offset: 0x000DAAF4
		public override void remove_oncontrolselect(HTMLLinkElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmllinkElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CD7 RID: 101591 RVA: 0x000DBBE4 File Offset: 0x000DABE4
		public override void add_onmove(HTMLLinkElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CD8 RID: 101592 RVA: 0x000DBC74 File Offset: 0x000DAC74
		public override void remove_onmove(HTMLLinkElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CD9 RID: 101593 RVA: 0x000DBD64 File Offset: 0x000DAD64
		public override void add_onbeforeactivate(HTMLLinkElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CDA RID: 101594 RVA: 0x000DBDF4 File Offset: 0x000DADF4
		public override void remove_onbeforeactivate(HTMLLinkElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CDB RID: 101595 RVA: 0x000DBEE4 File Offset: 0x000DAEE4
		public override void add_onbeforedeactivate(HTMLLinkElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CDC RID: 101596 RVA: 0x000DBF74 File Offset: 0x000DAF74
		public override void remove_onbeforedeactivate(HTMLLinkElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CDD RID: 101597 RVA: 0x000DC064 File Offset: 0x000DB064
		public override void add_onpage(HTMLLinkElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CDE RID: 101598 RVA: 0x000DC0F4 File Offset: 0x000DB0F4
		public override void remove_onpage(HTMLLinkElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onpageDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CDF RID: 101599 RVA: 0x000DC1E4 File Offset: 0x000DB1E4
		public override void add_onlayoutcomplete(HTMLLinkElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CE0 RID: 101600 RVA: 0x000DC274 File Offset: 0x000DB274
		public override void remove_onlayoutcomplete(HTMLLinkElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CE1 RID: 101601 RVA: 0x000DC364 File Offset: 0x000DB364
		public override void add_onbeforeeditfocus(HTMLLinkElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CE2 RID: 101602 RVA: 0x000DC3F4 File Offset: 0x000DB3F4
		public override void remove_onbeforeeditfocus(HTMLLinkElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CE3 RID: 101603 RVA: 0x000DC4E4 File Offset: 0x000DB4E4
		public override void add_onreadystatechange(HTMLLinkElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CE4 RID: 101604 RVA: 0x000DC574 File Offset: 0x000DB574
		public override void remove_onreadystatechange(HTMLLinkElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CE5 RID: 101605 RVA: 0x000DC664 File Offset: 0x000DB664
		public override void add_oncellchange(HTMLLinkElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CE6 RID: 101606 RVA: 0x000DC6F4 File Offset: 0x000DB6F4
		public override void remove_oncellchange(HTMLLinkElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmllinkElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CE7 RID: 101607 RVA: 0x000DC7E4 File Offset: 0x000DB7E4
		public override void add_onrowsinserted(HTMLLinkElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CE8 RID: 101608 RVA: 0x000DC874 File Offset: 0x000DB874
		public override void remove_onrowsinserted(HTMLLinkElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CE9 RID: 101609 RVA: 0x000DC964 File Offset: 0x000DB964
		public override void add_onrowsdelete(HTMLLinkElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CEA RID: 101610 RVA: 0x000DC9F4 File Offset: 0x000DB9F4
		public override void remove_onrowsdelete(HTMLLinkElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CEB RID: 101611 RVA: 0x000DCAE4 File Offset: 0x000DBAE4
		public override void add_oncontextmenu(HTMLLinkElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CEC RID: 101612 RVA: 0x000DCB74 File Offset: 0x000DBB74
		public override void remove_oncontextmenu(HTMLLinkElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmllinkElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CED RID: 101613 RVA: 0x000DCC64 File Offset: 0x000DBC64
		public override void add_onpaste(HTMLLinkElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CEE RID: 101614 RVA: 0x000DCCF4 File Offset: 0x000DBCF4
		public override void remove_onpaste(HTMLLinkElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CEF RID: 101615 RVA: 0x000DCDE4 File Offset: 0x000DBDE4
		public override void add_onbeforepaste(HTMLLinkElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CF0 RID: 101616 RVA: 0x000DCE74 File Offset: 0x000DBE74
		public override void remove_onbeforepaste(HTMLLinkElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CF1 RID: 101617 RVA: 0x000DCF64 File Offset: 0x000DBF64
		public override void add_oncopy(HTMLLinkElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CF2 RID: 101618 RVA: 0x000DCFF4 File Offset: 0x000DBFF4
		public override void remove_oncopy(HTMLLinkElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmllinkElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CF3 RID: 101619 RVA: 0x000DD0E4 File Offset: 0x000DC0E4
		public override void add_onbeforecopy(HTMLLinkElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CF4 RID: 101620 RVA: 0x000DD174 File Offset: 0x000DC174
		public override void remove_onbeforecopy(HTMLLinkElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CF5 RID: 101621 RVA: 0x000DD264 File Offset: 0x000DC264
		public override void add_oncut(HTMLLinkElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CF6 RID: 101622 RVA: 0x000DD2F4 File Offset: 0x000DC2F4
		public override void remove_oncut(HTMLLinkElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_oncutDelegate != null && ((htmllinkElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CF7 RID: 101623 RVA: 0x000DD3E4 File Offset: 0x000DC3E4
		public override void add_onbeforecut(HTMLLinkElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CF8 RID: 101624 RVA: 0x000DD474 File Offset: 0x000DC474
		public override void remove_onbeforecut(HTMLLinkElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CF9 RID: 101625 RVA: 0x000DD564 File Offset: 0x000DC564
		public override void add_ondrop(HTMLLinkElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CFA RID: 101626 RVA: 0x000DD5F4 File Offset: 0x000DC5F4
		public override void remove_ondrop(HTMLLinkElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondropDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CFB RID: 101627 RVA: 0x000DD6E4 File Offset: 0x000DC6E4
		public override void add_ondragleave(HTMLLinkElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CFC RID: 101628 RVA: 0x000DD774 File Offset: 0x000DC774
		public override void remove_ondragleave(HTMLLinkElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CFD RID: 101629 RVA: 0x000DD864 File Offset: 0x000DC864
		public override void add_ondragover(HTMLLinkElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018CFE RID: 101630 RVA: 0x000DD8F4 File Offset: 0x000DC8F4
		public override void remove_ondragover(HTMLLinkElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018CFF RID: 101631 RVA: 0x000DD9E4 File Offset: 0x000DC9E4
		public override void add_ondragenter(HTMLLinkElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D00 RID: 101632 RVA: 0x000DDA74 File Offset: 0x000DCA74
		public override void remove_ondragenter(HTMLLinkElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D01 RID: 101633 RVA: 0x000DDB64 File Offset: 0x000DCB64
		public override void add_ondragend(HTMLLinkElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D02 RID: 101634 RVA: 0x000DDBF4 File Offset: 0x000DCBF4
		public override void remove_ondragend(HTMLLinkElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D03 RID: 101635 RVA: 0x000DDCE4 File Offset: 0x000DCCE4
		public override void add_ondrag(HTMLLinkElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D04 RID: 101636 RVA: 0x000DDD74 File Offset: 0x000DCD74
		public override void remove_ondrag(HTMLLinkElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D05 RID: 101637 RVA: 0x000DDE64 File Offset: 0x000DCE64
		public override void add_onresize(HTMLLinkElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D06 RID: 101638 RVA: 0x000DDEF4 File Offset: 0x000DCEF4
		public override void remove_onresize(HTMLLinkElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D07 RID: 101639 RVA: 0x000DDFE4 File Offset: 0x000DCFE4
		public override void add_onblur(HTMLLinkElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D08 RID: 101640 RVA: 0x000DE074 File Offset: 0x000DD074
		public override void remove_onblur(HTMLLinkElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onblurDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D09 RID: 101641 RVA: 0x000DE164 File Offset: 0x000DD164
		public override void add_onfocus(HTMLLinkElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D0A RID: 101642 RVA: 0x000DE1F4 File Offset: 0x000DD1F4
		public override void remove_onfocus(HTMLLinkElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D0B RID: 101643 RVA: 0x000DE2E4 File Offset: 0x000DD2E4
		public override void add_onscroll(HTMLLinkElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D0C RID: 101644 RVA: 0x000DE374 File Offset: 0x000DD374
		public override void remove_onscroll(HTMLLinkElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D0D RID: 101645 RVA: 0x000DE464 File Offset: 0x000DD464
		public override void add_onpropertychange(HTMLLinkElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D0E RID: 101646 RVA: 0x000DE4F4 File Offset: 0x000DD4F4
		public override void remove_onpropertychange(HTMLLinkElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D0F RID: 101647 RVA: 0x000DE5E4 File Offset: 0x000DD5E4
		public override void add_onlosecapture(HTMLLinkElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D10 RID: 101648 RVA: 0x000DE674 File Offset: 0x000DD674
		public override void remove_onlosecapture(HTMLLinkElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D11 RID: 101649 RVA: 0x000DE764 File Offset: 0x000DD764
		public override void add_ondatasetcomplete(HTMLLinkElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D12 RID: 101650 RVA: 0x000DE7F4 File Offset: 0x000DD7F4
		public override void remove_ondatasetcomplete(HTMLLinkElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D13 RID: 101651 RVA: 0x000DE8E4 File Offset: 0x000DD8E4
		public override void add_ondataavailable(HTMLLinkElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D14 RID: 101652 RVA: 0x000DE974 File Offset: 0x000DD974
		public override void remove_ondataavailable(HTMLLinkElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D15 RID: 101653 RVA: 0x000DEA64 File Offset: 0x000DDA64
		public override void add_ondatasetchanged(HTMLLinkElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D16 RID: 101654 RVA: 0x000DEAF4 File Offset: 0x000DDAF4
		public override void remove_ondatasetchanged(HTMLLinkElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D17 RID: 101655 RVA: 0x000DEBE4 File Offset: 0x000DDBE4
		public override void add_onrowenter(HTMLLinkElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D18 RID: 101656 RVA: 0x000DEC74 File Offset: 0x000DDC74
		public override void remove_onrowenter(HTMLLinkElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D19 RID: 101657 RVA: 0x000DED64 File Offset: 0x000DDD64
		public override void add_onrowexit(HTMLLinkElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D1A RID: 101658 RVA: 0x000DEDF4 File Offset: 0x000DDDF4
		public override void remove_onrowexit(HTMLLinkElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D1B RID: 101659 RVA: 0x000DEEE4 File Offset: 0x000DDEE4
		public override void add_onerrorupdate(HTMLLinkElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D1C RID: 101660 RVA: 0x000DEF74 File Offset: 0x000DDF74
		public override void remove_onerrorupdate(HTMLLinkElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D1D RID: 101661 RVA: 0x000DF064 File Offset: 0x000DE064
		public override void add_onafterupdate(HTMLLinkElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D1E RID: 101662 RVA: 0x000DF0F4 File Offset: 0x000DE0F4
		public override void remove_onafterupdate(HTMLLinkElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D1F RID: 101663 RVA: 0x000DF1E4 File Offset: 0x000DE1E4
		public override void add_onbeforeupdate(HTMLLinkElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D20 RID: 101664 RVA: 0x000DF274 File Offset: 0x000DE274
		public override void remove_onbeforeupdate(HTMLLinkElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D21 RID: 101665 RVA: 0x000DF364 File Offset: 0x000DE364
		public override void add_ondragstart(HTMLLinkElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D22 RID: 101666 RVA: 0x000DF3F4 File Offset: 0x000DE3F4
		public override void remove_ondragstart(HTMLLinkElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D23 RID: 101667 RVA: 0x000DF4E4 File Offset: 0x000DE4E4
		public override void add_onfilterchange(HTMLLinkElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D24 RID: 101668 RVA: 0x000DF574 File Offset: 0x000DE574
		public override void remove_onfilterchange(HTMLLinkElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D25 RID: 101669 RVA: 0x000DF664 File Offset: 0x000DE664
		public override void add_onselectstart(HTMLLinkElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D26 RID: 101670 RVA: 0x000DF6F4 File Offset: 0x000DE6F4
		public override void remove_onselectstart(HTMLLinkElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D27 RID: 101671 RVA: 0x000DF7E4 File Offset: 0x000DE7E4
		public override void add_onmouseup(HTMLLinkElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D28 RID: 101672 RVA: 0x000DF874 File Offset: 0x000DE874
		public override void remove_onmouseup(HTMLLinkElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D29 RID: 101673 RVA: 0x000DF964 File Offset: 0x000DE964
		public override void add_onmousedown(HTMLLinkElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D2A RID: 101674 RVA: 0x000DF9F4 File Offset: 0x000DE9F4
		public override void remove_onmousedown(HTMLLinkElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D2B RID: 101675 RVA: 0x000DFAE4 File Offset: 0x000DEAE4
		public override void add_onmousemove(HTMLLinkElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D2C RID: 101676 RVA: 0x000DFB74 File Offset: 0x000DEB74
		public override void remove_onmousemove(HTMLLinkElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D2D RID: 101677 RVA: 0x000DFC64 File Offset: 0x000DEC64
		public override void add_onmouseover(HTMLLinkElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D2E RID: 101678 RVA: 0x000DFCF4 File Offset: 0x000DECF4
		public override void remove_onmouseover(HTMLLinkElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D2F RID: 101679 RVA: 0x000DFDE4 File Offset: 0x000DEDE4
		public override void add_onmouseout(HTMLLinkElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D30 RID: 101680 RVA: 0x000DFE74 File Offset: 0x000DEE74
		public override void remove_onmouseout(HTMLLinkElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D31 RID: 101681 RVA: 0x000DFF64 File Offset: 0x000DEF64
		public override void add_onkeyup(HTMLLinkElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D32 RID: 101682 RVA: 0x000DFFF4 File Offset: 0x000DEFF4
		public override void remove_onkeyup(HTMLLinkElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D33 RID: 101683 RVA: 0x000E00E4 File Offset: 0x000DF0E4
		public override void add_onkeydown(HTMLLinkElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D34 RID: 101684 RVA: 0x000E0174 File Offset: 0x000DF174
		public override void remove_onkeydown(HTMLLinkElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D35 RID: 101685 RVA: 0x000E0264 File Offset: 0x000DF264
		public override void add_onkeypress(HTMLLinkElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D36 RID: 101686 RVA: 0x000E02F4 File Offset: 0x000DF2F4
		public override void remove_onkeypress(HTMLLinkElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D37 RID: 101687 RVA: 0x000E03E4 File Offset: 0x000DF3E4
		public override void add_ondblclick(HTMLLinkElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D38 RID: 101688 RVA: 0x000E0474 File Offset: 0x000DF474
		public override void remove_ondblclick(HTMLLinkElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmllinkElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D39 RID: 101689 RVA: 0x000E0564 File Offset: 0x000DF564
		public override void add_onclick(HTMLLinkElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D3A RID: 101690 RVA: 0x000E05F4 File Offset: 0x000DF5F4
		public override void remove_onclick(HTMLLinkElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onclickDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D3B RID: 101691 RVA: 0x000E06E4 File Offset: 0x000DF6E4
		public override void add_onhelp(HTMLLinkElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = new HTMLLinkElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents_SinkHelper, ref num);
				htmllinkElementEvents_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents_SinkHelper);
			}
		}

		// Token: 0x06018D3C RID: 101692 RVA: 0x000E0774 File Offset: 0x000DF774
		public override void remove_onhelp(HTMLLinkElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmllinkElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018D3D RID: 101693 RVA: 0x000E0864 File Offset: 0x000DF864
		public HTMLLinkElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018D3E RID: 101694 RVA: 0x000E088C File Offset: 0x000DF88C
		public override void Finalize()
		{
			lock (this)
			{
				try
				{
					if (this.m_ConnectionPoint != null)
					{
						int count = this.m_aEventSinkHelpers.Count;
						int num = 0;
						if (0 < count)
						{
							do
							{
								HTMLLinkElementEvents_SinkHelper htmllinkElementEvents_SinkHelper = (HTMLLinkElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmllinkElementEvents_SinkHelper.m_dwCookie);
								num++;
							}
							while (num < count);
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06018D3F RID: 101695 RVA: 0x000E0940 File Offset: 0x000DF940
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000D31 RID: 3377
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000D32 RID: 3378
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000D33 RID: 3379
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
