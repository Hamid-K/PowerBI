using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC1 RID: 3521
	internal sealed class HTMLLinkElementEvents2_EventProvider : HTMLLinkElementEvents2_Event, IDisposable
	{
		// Token: 0x060175CE RID: 95694 RVA: 0x00009C34 File Offset: 0x00008C34
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				29, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060175CF RID: 95695 RVA: 0x00009D48 File Offset: 0x00008D48
		public override void add_onerror(HTMLLinkElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175D0 RID: 95696 RVA: 0x00009DD8 File Offset: 0x00008DD8
		public override void remove_onerror(HTMLLinkElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onerrorDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175D1 RID: 95697 RVA: 0x00009EC8 File Offset: 0x00008EC8
		public override void add_onload(HTMLLinkElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175D2 RID: 95698 RVA: 0x00009F58 File Offset: 0x00008F58
		public override void remove_onload(HTMLLinkElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onloadDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175D3 RID: 95699 RVA: 0x0000A048 File Offset: 0x00009048
		public override void add_onmousewheel(HTMLLinkElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175D4 RID: 95700 RVA: 0x0000A0D8 File Offset: 0x000090D8
		public override void remove_onmousewheel(HTMLLinkElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175D5 RID: 95701 RVA: 0x0000A1C8 File Offset: 0x000091C8
		public override void add_onresizeend(HTMLLinkElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175D6 RID: 95702 RVA: 0x0000A258 File Offset: 0x00009258
		public override void remove_onresizeend(HTMLLinkElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175D7 RID: 95703 RVA: 0x0000A348 File Offset: 0x00009348
		public override void add_onresizestart(HTMLLinkElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175D8 RID: 95704 RVA: 0x0000A3D8 File Offset: 0x000093D8
		public override void remove_onresizestart(HTMLLinkElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175D9 RID: 95705 RVA: 0x0000A4C8 File Offset: 0x000094C8
		public override void add_onmoveend(HTMLLinkElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175DA RID: 95706 RVA: 0x0000A558 File Offset: 0x00009558
		public override void remove_onmoveend(HTMLLinkElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175DB RID: 95707 RVA: 0x0000A648 File Offset: 0x00009648
		public override void add_onmovestart(HTMLLinkElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175DC RID: 95708 RVA: 0x0000A6D8 File Offset: 0x000096D8
		public override void remove_onmovestart(HTMLLinkElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175DD RID: 95709 RVA: 0x0000A7C8 File Offset: 0x000097C8
		public override void add_oncontrolselect(HTMLLinkElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175DE RID: 95710 RVA: 0x0000A858 File Offset: 0x00009858
		public override void remove_oncontrolselect(HTMLLinkElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175DF RID: 95711 RVA: 0x0000A948 File Offset: 0x00009948
		public override void add_onmove(HTMLLinkElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175E0 RID: 95712 RVA: 0x0000A9D8 File Offset: 0x000099D8
		public override void remove_onmove(HTMLLinkElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175E1 RID: 95713 RVA: 0x0000AAC8 File Offset: 0x00009AC8
		public override void add_onfocusout(HTMLLinkElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175E2 RID: 95714 RVA: 0x0000AB58 File Offset: 0x00009B58
		public override void remove_onfocusout(HTMLLinkElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175E3 RID: 95715 RVA: 0x0000AC48 File Offset: 0x00009C48
		public override void add_onfocusin(HTMLLinkElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175E4 RID: 95716 RVA: 0x0000ACD8 File Offset: 0x00009CD8
		public override void remove_onfocusin(HTMLLinkElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175E5 RID: 95717 RVA: 0x0000ADC8 File Offset: 0x00009DC8
		public override void add_onbeforeactivate(HTMLLinkElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175E6 RID: 95718 RVA: 0x0000AE58 File Offset: 0x00009E58
		public override void remove_onbeforeactivate(HTMLLinkElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175E7 RID: 95719 RVA: 0x0000AF48 File Offset: 0x00009F48
		public override void add_onbeforedeactivate(HTMLLinkElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175E8 RID: 95720 RVA: 0x0000AFD8 File Offset: 0x00009FD8
		public override void remove_onbeforedeactivate(HTMLLinkElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175E9 RID: 95721 RVA: 0x0000B0C8 File Offset: 0x0000A0C8
		public override void add_ondeactivate(HTMLLinkElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175EA RID: 95722 RVA: 0x0000B158 File Offset: 0x0000A158
		public override void remove_ondeactivate(HTMLLinkElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175EB RID: 95723 RVA: 0x0000B248 File Offset: 0x0000A248
		public override void add_onactivate(HTMLLinkElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175EC RID: 95724 RVA: 0x0000B2D8 File Offset: 0x0000A2D8
		public override void remove_onactivate(HTMLLinkElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175ED RID: 95725 RVA: 0x0000B3C8 File Offset: 0x0000A3C8
		public override void add_onmouseleave(HTMLLinkElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175EE RID: 95726 RVA: 0x0000B458 File Offset: 0x0000A458
		public override void remove_onmouseleave(HTMLLinkElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175EF RID: 95727 RVA: 0x0000B548 File Offset: 0x0000A548
		public override void add_onmouseenter(HTMLLinkElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175F0 RID: 95728 RVA: 0x0000B5D8 File Offset: 0x0000A5D8
		public override void remove_onmouseenter(HTMLLinkElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175F1 RID: 95729 RVA: 0x0000B6C8 File Offset: 0x0000A6C8
		public override void add_onpage(HTMLLinkElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175F2 RID: 95730 RVA: 0x0000B758 File Offset: 0x0000A758
		public override void remove_onpage(HTMLLinkElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175F3 RID: 95731 RVA: 0x0000B848 File Offset: 0x0000A848
		public override void add_onlayoutcomplete(HTMLLinkElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175F4 RID: 95732 RVA: 0x0000B8D8 File Offset: 0x0000A8D8
		public override void remove_onlayoutcomplete(HTMLLinkElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175F5 RID: 95733 RVA: 0x0000B9C8 File Offset: 0x0000A9C8
		public override void add_onreadystatechange(HTMLLinkElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175F6 RID: 95734 RVA: 0x0000BA58 File Offset: 0x0000AA58
		public override void remove_onreadystatechange(HTMLLinkElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175F7 RID: 95735 RVA: 0x0000BB48 File Offset: 0x0000AB48
		public override void add_oncellchange(HTMLLinkElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175F8 RID: 95736 RVA: 0x0000BBD8 File Offset: 0x0000ABD8
		public override void remove_oncellchange(HTMLLinkElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175F9 RID: 95737 RVA: 0x0000BCC8 File Offset: 0x0000ACC8
		public override void add_onrowsinserted(HTMLLinkElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175FA RID: 95738 RVA: 0x0000BD58 File Offset: 0x0000AD58
		public override void remove_onrowsinserted(HTMLLinkElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175FB RID: 95739 RVA: 0x0000BE48 File Offset: 0x0000AE48
		public override void add_onrowsdelete(HTMLLinkElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175FC RID: 95740 RVA: 0x0000BED8 File Offset: 0x0000AED8
		public override void remove_onrowsdelete(HTMLLinkElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175FD RID: 95741 RVA: 0x0000BFC8 File Offset: 0x0000AFC8
		public override void add_oncontextmenu(HTMLLinkElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060175FE RID: 95742 RVA: 0x0000C058 File Offset: 0x0000B058
		public override void remove_oncontextmenu(HTMLLinkElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060175FF RID: 95743 RVA: 0x0000C148 File Offset: 0x0000B148
		public override void add_onpaste(HTMLLinkElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017600 RID: 95744 RVA: 0x0000C1D8 File Offset: 0x0000B1D8
		public override void remove_onpaste(HTMLLinkElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017601 RID: 95745 RVA: 0x0000C2C8 File Offset: 0x0000B2C8
		public override void add_onbeforepaste(HTMLLinkElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017602 RID: 95746 RVA: 0x0000C358 File Offset: 0x0000B358
		public override void remove_onbeforepaste(HTMLLinkElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017603 RID: 95747 RVA: 0x0000C448 File Offset: 0x0000B448
		public override void add_oncopy(HTMLLinkElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017604 RID: 95748 RVA: 0x0000C4D8 File Offset: 0x0000B4D8
		public override void remove_oncopy(HTMLLinkElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017605 RID: 95749 RVA: 0x0000C5C8 File Offset: 0x0000B5C8
		public override void add_onbeforecopy(HTMLLinkElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017606 RID: 95750 RVA: 0x0000C658 File Offset: 0x0000B658
		public override void remove_onbeforecopy(HTMLLinkElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017607 RID: 95751 RVA: 0x0000C748 File Offset: 0x0000B748
		public override void add_oncut(HTMLLinkElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017608 RID: 95752 RVA: 0x0000C7D8 File Offset: 0x0000B7D8
		public override void remove_oncut(HTMLLinkElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017609 RID: 95753 RVA: 0x0000C8C8 File Offset: 0x0000B8C8
		public override void add_onbeforecut(HTMLLinkElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601760A RID: 95754 RVA: 0x0000C958 File Offset: 0x0000B958
		public override void remove_onbeforecut(HTMLLinkElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601760B RID: 95755 RVA: 0x0000CA48 File Offset: 0x0000BA48
		public override void add_ondrop(HTMLLinkElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601760C RID: 95756 RVA: 0x0000CAD8 File Offset: 0x0000BAD8
		public override void remove_ondrop(HTMLLinkElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601760D RID: 95757 RVA: 0x0000CBC8 File Offset: 0x0000BBC8
		public override void add_ondragleave(HTMLLinkElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601760E RID: 95758 RVA: 0x0000CC58 File Offset: 0x0000BC58
		public override void remove_ondragleave(HTMLLinkElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601760F RID: 95759 RVA: 0x0000CD48 File Offset: 0x0000BD48
		public override void add_ondragover(HTMLLinkElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017610 RID: 95760 RVA: 0x0000CDD8 File Offset: 0x0000BDD8
		public override void remove_ondragover(HTMLLinkElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017611 RID: 95761 RVA: 0x0000CEC8 File Offset: 0x0000BEC8
		public override void add_ondragenter(HTMLLinkElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017612 RID: 95762 RVA: 0x0000CF58 File Offset: 0x0000BF58
		public override void remove_ondragenter(HTMLLinkElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017613 RID: 95763 RVA: 0x0000D048 File Offset: 0x0000C048
		public override void add_ondragend(HTMLLinkElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017614 RID: 95764 RVA: 0x0000D0D8 File Offset: 0x0000C0D8
		public override void remove_ondragend(HTMLLinkElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017615 RID: 95765 RVA: 0x0000D1C8 File Offset: 0x0000C1C8
		public override void add_ondrag(HTMLLinkElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017616 RID: 95766 RVA: 0x0000D258 File Offset: 0x0000C258
		public override void remove_ondrag(HTMLLinkElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017617 RID: 95767 RVA: 0x0000D348 File Offset: 0x0000C348
		public override void add_onresize(HTMLLinkElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017618 RID: 95768 RVA: 0x0000D3D8 File Offset: 0x0000C3D8
		public override void remove_onresize(HTMLLinkElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017619 RID: 95769 RVA: 0x0000D4C8 File Offset: 0x0000C4C8
		public override void add_onblur(HTMLLinkElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601761A RID: 95770 RVA: 0x0000D558 File Offset: 0x0000C558
		public override void remove_onblur(HTMLLinkElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601761B RID: 95771 RVA: 0x0000D648 File Offset: 0x0000C648
		public override void add_onfocus(HTMLLinkElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601761C RID: 95772 RVA: 0x0000D6D8 File Offset: 0x0000C6D8
		public override void remove_onfocus(HTMLLinkElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601761D RID: 95773 RVA: 0x0000D7C8 File Offset: 0x0000C7C8
		public override void add_onscroll(HTMLLinkElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601761E RID: 95774 RVA: 0x0000D858 File Offset: 0x0000C858
		public override void remove_onscroll(HTMLLinkElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601761F RID: 95775 RVA: 0x0000D948 File Offset: 0x0000C948
		public override void add_onpropertychange(HTMLLinkElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017620 RID: 95776 RVA: 0x0000D9D8 File Offset: 0x0000C9D8
		public override void remove_onpropertychange(HTMLLinkElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017621 RID: 95777 RVA: 0x0000DAC8 File Offset: 0x0000CAC8
		public override void add_onlosecapture(HTMLLinkElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017622 RID: 95778 RVA: 0x0000DB58 File Offset: 0x0000CB58
		public override void remove_onlosecapture(HTMLLinkElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017623 RID: 95779 RVA: 0x0000DC48 File Offset: 0x0000CC48
		public override void add_ondatasetcomplete(HTMLLinkElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017624 RID: 95780 RVA: 0x0000DCD8 File Offset: 0x0000CCD8
		public override void remove_ondatasetcomplete(HTMLLinkElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017625 RID: 95781 RVA: 0x0000DDC8 File Offset: 0x0000CDC8
		public override void add_ondataavailable(HTMLLinkElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017626 RID: 95782 RVA: 0x0000DE58 File Offset: 0x0000CE58
		public override void remove_ondataavailable(HTMLLinkElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017627 RID: 95783 RVA: 0x0000DF48 File Offset: 0x0000CF48
		public override void add_ondatasetchanged(HTMLLinkElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017628 RID: 95784 RVA: 0x0000DFD8 File Offset: 0x0000CFD8
		public override void remove_ondatasetchanged(HTMLLinkElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017629 RID: 95785 RVA: 0x0000E0C8 File Offset: 0x0000D0C8
		public override void add_onrowenter(HTMLLinkElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601762A RID: 95786 RVA: 0x0000E158 File Offset: 0x0000D158
		public override void remove_onrowenter(HTMLLinkElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601762B RID: 95787 RVA: 0x0000E248 File Offset: 0x0000D248
		public override void add_onrowexit(HTMLLinkElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601762C RID: 95788 RVA: 0x0000E2D8 File Offset: 0x0000D2D8
		public override void remove_onrowexit(HTMLLinkElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601762D RID: 95789 RVA: 0x0000E3C8 File Offset: 0x0000D3C8
		public override void add_onerrorupdate(HTMLLinkElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601762E RID: 95790 RVA: 0x0000E458 File Offset: 0x0000D458
		public override void remove_onerrorupdate(HTMLLinkElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601762F RID: 95791 RVA: 0x0000E548 File Offset: 0x0000D548
		public override void add_onafterupdate(HTMLLinkElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017630 RID: 95792 RVA: 0x0000E5D8 File Offset: 0x0000D5D8
		public override void remove_onafterupdate(HTMLLinkElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017631 RID: 95793 RVA: 0x0000E6C8 File Offset: 0x0000D6C8
		public override void add_onbeforeupdate(HTMLLinkElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017632 RID: 95794 RVA: 0x0000E758 File Offset: 0x0000D758
		public override void remove_onbeforeupdate(HTMLLinkElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017633 RID: 95795 RVA: 0x0000E848 File Offset: 0x0000D848
		public override void add_ondragstart(HTMLLinkElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017634 RID: 95796 RVA: 0x0000E8D8 File Offset: 0x0000D8D8
		public override void remove_ondragstart(HTMLLinkElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017635 RID: 95797 RVA: 0x0000E9C8 File Offset: 0x0000D9C8
		public override void add_onfilterchange(HTMLLinkElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017636 RID: 95798 RVA: 0x0000EA58 File Offset: 0x0000DA58
		public override void remove_onfilterchange(HTMLLinkElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017637 RID: 95799 RVA: 0x0000EB48 File Offset: 0x0000DB48
		public override void add_onselectstart(HTMLLinkElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017638 RID: 95800 RVA: 0x0000EBD8 File Offset: 0x0000DBD8
		public override void remove_onselectstart(HTMLLinkElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017639 RID: 95801 RVA: 0x0000ECC8 File Offset: 0x0000DCC8
		public override void add_onmouseup(HTMLLinkElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601763A RID: 95802 RVA: 0x0000ED58 File Offset: 0x0000DD58
		public override void remove_onmouseup(HTMLLinkElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601763B RID: 95803 RVA: 0x0000EE48 File Offset: 0x0000DE48
		public override void add_onmousedown(HTMLLinkElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601763C RID: 95804 RVA: 0x0000EED8 File Offset: 0x0000DED8
		public override void remove_onmousedown(HTMLLinkElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601763D RID: 95805 RVA: 0x0000EFC8 File Offset: 0x0000DFC8
		public override void add_onmousemove(HTMLLinkElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601763E RID: 95806 RVA: 0x0000F058 File Offset: 0x0000E058
		public override void remove_onmousemove(HTMLLinkElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601763F RID: 95807 RVA: 0x0000F148 File Offset: 0x0000E148
		public override void add_onmouseover(HTMLLinkElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017640 RID: 95808 RVA: 0x0000F1D8 File Offset: 0x0000E1D8
		public override void remove_onmouseover(HTMLLinkElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017641 RID: 95809 RVA: 0x0000F2C8 File Offset: 0x0000E2C8
		public override void add_onmouseout(HTMLLinkElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017642 RID: 95810 RVA: 0x0000F358 File Offset: 0x0000E358
		public override void remove_onmouseout(HTMLLinkElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017643 RID: 95811 RVA: 0x0000F448 File Offset: 0x0000E448
		public override void add_onkeyup(HTMLLinkElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017644 RID: 95812 RVA: 0x0000F4D8 File Offset: 0x0000E4D8
		public override void remove_onkeyup(HTMLLinkElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017645 RID: 95813 RVA: 0x0000F5C8 File Offset: 0x0000E5C8
		public override void add_onkeydown(HTMLLinkElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017646 RID: 95814 RVA: 0x0000F658 File Offset: 0x0000E658
		public override void remove_onkeydown(HTMLLinkElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017647 RID: 95815 RVA: 0x0000F748 File Offset: 0x0000E748
		public override void add_onkeypress(HTMLLinkElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017648 RID: 95816 RVA: 0x0000F7D8 File Offset: 0x0000E7D8
		public override void remove_onkeypress(HTMLLinkElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017649 RID: 95817 RVA: 0x0000F8C8 File Offset: 0x0000E8C8
		public override void add_ondblclick(HTMLLinkElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601764A RID: 95818 RVA: 0x0000F958 File Offset: 0x0000E958
		public override void remove_ondblclick(HTMLLinkElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601764B RID: 95819 RVA: 0x0000FA48 File Offset: 0x0000EA48
		public override void add_onclick(HTMLLinkElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601764C RID: 95820 RVA: 0x0000FAD8 File Offset: 0x0000EAD8
		public override void remove_onclick(HTMLLinkElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601764D RID: 95821 RVA: 0x0000FBC8 File Offset: 0x0000EBC8
		public override void add_onhelp(HTMLLinkElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = new HTMLLinkElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllinkElementEvents2_SinkHelper, ref num);
				htmllinkElementEvents2_SinkHelper.m_dwCookie = num;
				htmllinkElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllinkElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601764E RID: 95822 RVA: 0x0000FC58 File Offset: 0x0000EC58
		public override void remove_onhelp(HTMLLinkElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper;
					for (;;)
					{
						htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllinkElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmllinkElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601764F RID: 95823 RVA: 0x0000FD48 File Offset: 0x0000ED48
		public HTMLLinkElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017650 RID: 95824 RVA: 0x0000FD70 File Offset: 0x0000ED70
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
								HTMLLinkElementEvents2_SinkHelper htmllinkElementEvents2_SinkHelper = (HTMLLinkElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmllinkElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06017651 RID: 95825 RVA: 0x0000FE24 File Offset: 0x0000EE24
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400053D RID: 1341
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400053E RID: 1342
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400053F RID: 1343
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
