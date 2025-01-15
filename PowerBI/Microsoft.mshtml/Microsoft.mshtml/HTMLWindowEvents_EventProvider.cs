using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCB RID: 3531
	internal sealed class HTMLWindowEvents_EventProvider : HTMLWindowEvents_Event, IDisposable
	{
		// Token: 0x060178C7 RID: 96455 RVA: 0x00025F1C File Offset: 0x00024F1C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				224, 164, 160, 150, 98, 208, 207, 17, 148, 182,
				0, 170, 0, 96, 39, 92
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060178C8 RID: 96456 RVA: 0x00026030 File Offset: 0x00025030
		public override void add_onafterprint(HTMLWindowEvents_onafterprintEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onafterprintDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178C9 RID: 96457 RVA: 0x000260C0 File Offset: 0x000250C0
		public override void remove_onafterprint(HTMLWindowEvents_onafterprintEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onafterprintDelegate != null && ((htmlwindowEvents_SinkHelper.m_onafterprintDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178CA RID: 96458 RVA: 0x000261B0 File Offset: 0x000251B0
		public override void add_onbeforeprint(HTMLWindowEvents_onbeforeprintEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onbeforeprintDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178CB RID: 96459 RVA: 0x00026240 File Offset: 0x00025240
		public override void remove_onbeforeprint(HTMLWindowEvents_onbeforeprintEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onbeforeprintDelegate != null && ((htmlwindowEvents_SinkHelper.m_onbeforeprintDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178CC RID: 96460 RVA: 0x00026330 File Offset: 0x00025330
		public override void add_onbeforeunload(HTMLWindowEvents_onbeforeunloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onbeforeunloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178CD RID: 96461 RVA: 0x000263C0 File Offset: 0x000253C0
		public override void remove_onbeforeunload(HTMLWindowEvents_onbeforeunloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onbeforeunloadDelegate != null && ((htmlwindowEvents_SinkHelper.m_onbeforeunloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178CE RID: 96462 RVA: 0x000264B0 File Offset: 0x000254B0
		public override void add_onscroll(HTMLWindowEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178CF RID: 96463 RVA: 0x00026540 File Offset: 0x00025540
		public override void remove_onscroll(HTMLWindowEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onscrollDelegate != null && ((htmlwindowEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178D0 RID: 96464 RVA: 0x00026630 File Offset: 0x00025630
		public override void add_onresize(HTMLWindowEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178D1 RID: 96465 RVA: 0x000266C0 File Offset: 0x000256C0
		public override void remove_onresize(HTMLWindowEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onresizeDelegate != null && ((htmlwindowEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178D2 RID: 96466 RVA: 0x000267B0 File Offset: 0x000257B0
		public override void add_onerror(HTMLWindowEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178D3 RID: 96467 RVA: 0x00026840 File Offset: 0x00025840
		public override void remove_onerror(HTMLWindowEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onerrorDelegate != null && ((htmlwindowEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178D4 RID: 96468 RVA: 0x00026930 File Offset: 0x00025930
		public override void add_onblur(HTMLWindowEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178D5 RID: 96469 RVA: 0x000269C0 File Offset: 0x000259C0
		public override void remove_onblur(HTMLWindowEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onblurDelegate != null && ((htmlwindowEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178D6 RID: 96470 RVA: 0x00026AB0 File Offset: 0x00025AB0
		public override void add_onfocus(HTMLWindowEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178D7 RID: 96471 RVA: 0x00026B40 File Offset: 0x00025B40
		public override void remove_onfocus(HTMLWindowEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onfocusDelegate != null && ((htmlwindowEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178D8 RID: 96472 RVA: 0x00026C30 File Offset: 0x00025C30
		public override void add_onhelp(HTMLWindowEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178D9 RID: 96473 RVA: 0x00026CC0 File Offset: 0x00025CC0
		public override void remove_onhelp(HTMLWindowEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onhelpDelegate != null && ((htmlwindowEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178DA RID: 96474 RVA: 0x00026DB0 File Offset: 0x00025DB0
		public override void add_onunload(HTMLWindowEvents_onunloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onunloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178DB RID: 96475 RVA: 0x00026E40 File Offset: 0x00025E40
		public override void remove_onunload(HTMLWindowEvents_onunloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onunloadDelegate != null && ((htmlwindowEvents_SinkHelper.m_onunloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178DC RID: 96476 RVA: 0x00026F30 File Offset: 0x00025F30
		public override void add_onload(HTMLWindowEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = new HTMLWindowEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents_SinkHelper, ref num);
				htmlwindowEvents_SinkHelper.m_dwCookie = num;
				htmlwindowEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents_SinkHelper);
			}
		}

		// Token: 0x060178DD RID: 96477 RVA: 0x00026FC0 File Offset: 0x00025FC0
		public override void remove_onload(HTMLWindowEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper;
					for (;;)
					{
						htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents_SinkHelper.m_onloadDelegate != null && ((htmlwindowEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060178DE RID: 96478 RVA: 0x000270B0 File Offset: 0x000260B0
		public HTMLWindowEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060178DF RID: 96479 RVA: 0x000270D8 File Offset: 0x000260D8
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
								HTMLWindowEvents_SinkHelper htmlwindowEvents_SinkHelper = (HTMLWindowEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlwindowEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060178E0 RID: 96480 RVA: 0x0002718C File Offset: 0x0002618C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000623 RID: 1571
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000624 RID: 1572
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000625 RID: 1573
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
