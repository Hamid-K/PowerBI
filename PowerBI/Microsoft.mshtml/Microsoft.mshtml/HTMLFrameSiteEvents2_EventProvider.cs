using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E11 RID: 3601
	internal sealed class HTMLFrameSiteEvents2_EventProvider : HTMLFrameSiteEvents2_Event, IDisposable
	{
		// Token: 0x06019120 RID: 102688 RVA: 0x0010287C File Offset: 0x0010187C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				byte.MaxValue, 247, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06019121 RID: 102689 RVA: 0x00102990 File Offset: 0x00101990
		public override void add_onload(HTMLFrameSiteEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019122 RID: 102690 RVA: 0x00102A20 File Offset: 0x00101A20
		public override void remove_onload(HTMLFrameSiteEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onloadDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019123 RID: 102691 RVA: 0x00102B10 File Offset: 0x00101B10
		public override void add_onmousewheel(HTMLFrameSiteEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019124 RID: 102692 RVA: 0x00102BA0 File Offset: 0x00101BA0
		public override void remove_onmousewheel(HTMLFrameSiteEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019125 RID: 102693 RVA: 0x00102C90 File Offset: 0x00101C90
		public override void add_onresizeend(HTMLFrameSiteEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019126 RID: 102694 RVA: 0x00102D20 File Offset: 0x00101D20
		public override void remove_onresizeend(HTMLFrameSiteEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019127 RID: 102695 RVA: 0x00102E10 File Offset: 0x00101E10
		public override void add_onresizestart(HTMLFrameSiteEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019128 RID: 102696 RVA: 0x00102EA0 File Offset: 0x00101EA0
		public override void remove_onresizestart(HTMLFrameSiteEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019129 RID: 102697 RVA: 0x00102F90 File Offset: 0x00101F90
		public override void add_onmoveend(HTMLFrameSiteEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601912A RID: 102698 RVA: 0x00103020 File Offset: 0x00102020
		public override void remove_onmoveend(HTMLFrameSiteEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601912B RID: 102699 RVA: 0x00103110 File Offset: 0x00102110
		public override void add_onmovestart(HTMLFrameSiteEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601912C RID: 102700 RVA: 0x001031A0 File Offset: 0x001021A0
		public override void remove_onmovestart(HTMLFrameSiteEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601912D RID: 102701 RVA: 0x00103290 File Offset: 0x00102290
		public override void add_oncontrolselect(HTMLFrameSiteEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601912E RID: 102702 RVA: 0x00103320 File Offset: 0x00102320
		public override void remove_oncontrolselect(HTMLFrameSiteEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601912F RID: 102703 RVA: 0x00103410 File Offset: 0x00102410
		public override void add_onmove(HTMLFrameSiteEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019130 RID: 102704 RVA: 0x001034A0 File Offset: 0x001024A0
		public override void remove_onmove(HTMLFrameSiteEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019131 RID: 102705 RVA: 0x00103590 File Offset: 0x00102590
		public override void add_onfocusout(HTMLFrameSiteEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019132 RID: 102706 RVA: 0x00103620 File Offset: 0x00102620
		public override void remove_onfocusout(HTMLFrameSiteEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019133 RID: 102707 RVA: 0x00103710 File Offset: 0x00102710
		public override void add_onfocusin(HTMLFrameSiteEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019134 RID: 102708 RVA: 0x001037A0 File Offset: 0x001027A0
		public override void remove_onfocusin(HTMLFrameSiteEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019135 RID: 102709 RVA: 0x00103890 File Offset: 0x00102890
		public override void add_onbeforeactivate(HTMLFrameSiteEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019136 RID: 102710 RVA: 0x00103920 File Offset: 0x00102920
		public override void remove_onbeforeactivate(HTMLFrameSiteEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019137 RID: 102711 RVA: 0x00103A10 File Offset: 0x00102A10
		public override void add_onbeforedeactivate(HTMLFrameSiteEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019138 RID: 102712 RVA: 0x00103AA0 File Offset: 0x00102AA0
		public override void remove_onbeforedeactivate(HTMLFrameSiteEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019139 RID: 102713 RVA: 0x00103B90 File Offset: 0x00102B90
		public override void add_ondeactivate(HTMLFrameSiteEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601913A RID: 102714 RVA: 0x00103C20 File Offset: 0x00102C20
		public override void remove_ondeactivate(HTMLFrameSiteEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601913B RID: 102715 RVA: 0x00103D10 File Offset: 0x00102D10
		public override void add_onactivate(HTMLFrameSiteEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601913C RID: 102716 RVA: 0x00103DA0 File Offset: 0x00102DA0
		public override void remove_onactivate(HTMLFrameSiteEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601913D RID: 102717 RVA: 0x00103E90 File Offset: 0x00102E90
		public override void add_onmouseleave(HTMLFrameSiteEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601913E RID: 102718 RVA: 0x00103F20 File Offset: 0x00102F20
		public override void remove_onmouseleave(HTMLFrameSiteEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601913F RID: 102719 RVA: 0x00104010 File Offset: 0x00103010
		public override void add_onmouseenter(HTMLFrameSiteEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019140 RID: 102720 RVA: 0x001040A0 File Offset: 0x001030A0
		public override void remove_onmouseenter(HTMLFrameSiteEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019141 RID: 102721 RVA: 0x00104190 File Offset: 0x00103190
		public override void add_onpage(HTMLFrameSiteEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019142 RID: 102722 RVA: 0x00104220 File Offset: 0x00103220
		public override void remove_onpage(HTMLFrameSiteEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onpageDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019143 RID: 102723 RVA: 0x00104310 File Offset: 0x00103310
		public override void add_onlayoutcomplete(HTMLFrameSiteEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019144 RID: 102724 RVA: 0x001043A0 File Offset: 0x001033A0
		public override void remove_onlayoutcomplete(HTMLFrameSiteEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019145 RID: 102725 RVA: 0x00104490 File Offset: 0x00103490
		public override void add_onreadystatechange(HTMLFrameSiteEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019146 RID: 102726 RVA: 0x00104520 File Offset: 0x00103520
		public override void remove_onreadystatechange(HTMLFrameSiteEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019147 RID: 102727 RVA: 0x00104610 File Offset: 0x00103610
		public override void add_oncellchange(HTMLFrameSiteEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019148 RID: 102728 RVA: 0x001046A0 File Offset: 0x001036A0
		public override void remove_oncellchange(HTMLFrameSiteEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019149 RID: 102729 RVA: 0x00104790 File Offset: 0x00103790
		public override void add_onrowsinserted(HTMLFrameSiteEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601914A RID: 102730 RVA: 0x00104820 File Offset: 0x00103820
		public override void remove_onrowsinserted(HTMLFrameSiteEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601914B RID: 102731 RVA: 0x00104910 File Offset: 0x00103910
		public override void add_onrowsdelete(HTMLFrameSiteEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601914C RID: 102732 RVA: 0x001049A0 File Offset: 0x001039A0
		public override void remove_onrowsdelete(HTMLFrameSiteEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601914D RID: 102733 RVA: 0x00104A90 File Offset: 0x00103A90
		public override void add_oncontextmenu(HTMLFrameSiteEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601914E RID: 102734 RVA: 0x00104B20 File Offset: 0x00103B20
		public override void remove_oncontextmenu(HTMLFrameSiteEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601914F RID: 102735 RVA: 0x00104C10 File Offset: 0x00103C10
		public override void add_onpaste(HTMLFrameSiteEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019150 RID: 102736 RVA: 0x00104CA0 File Offset: 0x00103CA0
		public override void remove_onpaste(HTMLFrameSiteEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019151 RID: 102737 RVA: 0x00104D90 File Offset: 0x00103D90
		public override void add_onbeforepaste(HTMLFrameSiteEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019152 RID: 102738 RVA: 0x00104E20 File Offset: 0x00103E20
		public override void remove_onbeforepaste(HTMLFrameSiteEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019153 RID: 102739 RVA: 0x00104F10 File Offset: 0x00103F10
		public override void add_oncopy(HTMLFrameSiteEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019154 RID: 102740 RVA: 0x00104FA0 File Offset: 0x00103FA0
		public override void remove_oncopy(HTMLFrameSiteEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019155 RID: 102741 RVA: 0x00105090 File Offset: 0x00104090
		public override void add_onbeforecopy(HTMLFrameSiteEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019156 RID: 102742 RVA: 0x00105120 File Offset: 0x00104120
		public override void remove_onbeforecopy(HTMLFrameSiteEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019157 RID: 102743 RVA: 0x00105210 File Offset: 0x00104210
		public override void add_oncut(HTMLFrameSiteEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019158 RID: 102744 RVA: 0x001052A0 File Offset: 0x001042A0
		public override void remove_oncut(HTMLFrameSiteEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_oncutDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019159 RID: 102745 RVA: 0x00105390 File Offset: 0x00104390
		public override void add_onbeforecut(HTMLFrameSiteEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601915A RID: 102746 RVA: 0x00105420 File Offset: 0x00104420
		public override void remove_onbeforecut(HTMLFrameSiteEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601915B RID: 102747 RVA: 0x00105510 File Offset: 0x00104510
		public override void add_ondrop(HTMLFrameSiteEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601915C RID: 102748 RVA: 0x001055A0 File Offset: 0x001045A0
		public override void remove_ondrop(HTMLFrameSiteEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondropDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601915D RID: 102749 RVA: 0x00105690 File Offset: 0x00104690
		public override void add_ondragleave(HTMLFrameSiteEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601915E RID: 102750 RVA: 0x00105720 File Offset: 0x00104720
		public override void remove_ondragleave(HTMLFrameSiteEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601915F RID: 102751 RVA: 0x00105810 File Offset: 0x00104810
		public override void add_ondragover(HTMLFrameSiteEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019160 RID: 102752 RVA: 0x001058A0 File Offset: 0x001048A0
		public override void remove_ondragover(HTMLFrameSiteEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019161 RID: 102753 RVA: 0x00105990 File Offset: 0x00104990
		public override void add_ondragenter(HTMLFrameSiteEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019162 RID: 102754 RVA: 0x00105A20 File Offset: 0x00104A20
		public override void remove_ondragenter(HTMLFrameSiteEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019163 RID: 102755 RVA: 0x00105B10 File Offset: 0x00104B10
		public override void add_ondragend(HTMLFrameSiteEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019164 RID: 102756 RVA: 0x00105BA0 File Offset: 0x00104BA0
		public override void remove_ondragend(HTMLFrameSiteEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019165 RID: 102757 RVA: 0x00105C90 File Offset: 0x00104C90
		public override void add_ondrag(HTMLFrameSiteEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019166 RID: 102758 RVA: 0x00105D20 File Offset: 0x00104D20
		public override void remove_ondrag(HTMLFrameSiteEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019167 RID: 102759 RVA: 0x00105E10 File Offset: 0x00104E10
		public override void add_onresize(HTMLFrameSiteEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019168 RID: 102760 RVA: 0x00105EA0 File Offset: 0x00104EA0
		public override void remove_onresize(HTMLFrameSiteEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019169 RID: 102761 RVA: 0x00105F90 File Offset: 0x00104F90
		public override void add_onblur(HTMLFrameSiteEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601916A RID: 102762 RVA: 0x00106020 File Offset: 0x00105020
		public override void remove_onblur(HTMLFrameSiteEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onblurDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601916B RID: 102763 RVA: 0x00106110 File Offset: 0x00105110
		public override void add_onfocus(HTMLFrameSiteEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601916C RID: 102764 RVA: 0x001061A0 File Offset: 0x001051A0
		public override void remove_onfocus(HTMLFrameSiteEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601916D RID: 102765 RVA: 0x00106290 File Offset: 0x00105290
		public override void add_onscroll(HTMLFrameSiteEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601916E RID: 102766 RVA: 0x00106320 File Offset: 0x00105320
		public override void remove_onscroll(HTMLFrameSiteEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601916F RID: 102767 RVA: 0x00106410 File Offset: 0x00105410
		public override void add_onpropertychange(HTMLFrameSiteEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019170 RID: 102768 RVA: 0x001064A0 File Offset: 0x001054A0
		public override void remove_onpropertychange(HTMLFrameSiteEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019171 RID: 102769 RVA: 0x00106590 File Offset: 0x00105590
		public override void add_onlosecapture(HTMLFrameSiteEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019172 RID: 102770 RVA: 0x00106620 File Offset: 0x00105620
		public override void remove_onlosecapture(HTMLFrameSiteEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019173 RID: 102771 RVA: 0x00106710 File Offset: 0x00105710
		public override void add_ondatasetcomplete(HTMLFrameSiteEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019174 RID: 102772 RVA: 0x001067A0 File Offset: 0x001057A0
		public override void remove_ondatasetcomplete(HTMLFrameSiteEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019175 RID: 102773 RVA: 0x00106890 File Offset: 0x00105890
		public override void add_ondataavailable(HTMLFrameSiteEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019176 RID: 102774 RVA: 0x00106920 File Offset: 0x00105920
		public override void remove_ondataavailable(HTMLFrameSiteEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019177 RID: 102775 RVA: 0x00106A10 File Offset: 0x00105A10
		public override void add_ondatasetchanged(HTMLFrameSiteEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019178 RID: 102776 RVA: 0x00106AA0 File Offset: 0x00105AA0
		public override void remove_ondatasetchanged(HTMLFrameSiteEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019179 RID: 102777 RVA: 0x00106B90 File Offset: 0x00105B90
		public override void add_onrowenter(HTMLFrameSiteEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601917A RID: 102778 RVA: 0x00106C20 File Offset: 0x00105C20
		public override void remove_onrowenter(HTMLFrameSiteEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601917B RID: 102779 RVA: 0x00106D10 File Offset: 0x00105D10
		public override void add_onrowexit(HTMLFrameSiteEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601917C RID: 102780 RVA: 0x00106DA0 File Offset: 0x00105DA0
		public override void remove_onrowexit(HTMLFrameSiteEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601917D RID: 102781 RVA: 0x00106E90 File Offset: 0x00105E90
		public override void add_onerrorupdate(HTMLFrameSiteEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601917E RID: 102782 RVA: 0x00106F20 File Offset: 0x00105F20
		public override void remove_onerrorupdate(HTMLFrameSiteEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601917F RID: 102783 RVA: 0x00107010 File Offset: 0x00106010
		public override void add_onafterupdate(HTMLFrameSiteEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019180 RID: 102784 RVA: 0x001070A0 File Offset: 0x001060A0
		public override void remove_onafterupdate(HTMLFrameSiteEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019181 RID: 102785 RVA: 0x00107190 File Offset: 0x00106190
		public override void add_onbeforeupdate(HTMLFrameSiteEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019182 RID: 102786 RVA: 0x00107220 File Offset: 0x00106220
		public override void remove_onbeforeupdate(HTMLFrameSiteEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019183 RID: 102787 RVA: 0x00107310 File Offset: 0x00106310
		public override void add_ondragstart(HTMLFrameSiteEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019184 RID: 102788 RVA: 0x001073A0 File Offset: 0x001063A0
		public override void remove_ondragstart(HTMLFrameSiteEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019185 RID: 102789 RVA: 0x00107490 File Offset: 0x00106490
		public override void add_onfilterchange(HTMLFrameSiteEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019186 RID: 102790 RVA: 0x00107520 File Offset: 0x00106520
		public override void remove_onfilterchange(HTMLFrameSiteEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019187 RID: 102791 RVA: 0x00107610 File Offset: 0x00106610
		public override void add_onselectstart(HTMLFrameSiteEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019188 RID: 102792 RVA: 0x001076A0 File Offset: 0x001066A0
		public override void remove_onselectstart(HTMLFrameSiteEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019189 RID: 102793 RVA: 0x00107790 File Offset: 0x00106790
		public override void add_onmouseup(HTMLFrameSiteEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601918A RID: 102794 RVA: 0x00107820 File Offset: 0x00106820
		public override void remove_onmouseup(HTMLFrameSiteEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601918B RID: 102795 RVA: 0x00107910 File Offset: 0x00106910
		public override void add_onmousedown(HTMLFrameSiteEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601918C RID: 102796 RVA: 0x001079A0 File Offset: 0x001069A0
		public override void remove_onmousedown(HTMLFrameSiteEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601918D RID: 102797 RVA: 0x00107A90 File Offset: 0x00106A90
		public override void add_onmousemove(HTMLFrameSiteEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601918E RID: 102798 RVA: 0x00107B20 File Offset: 0x00106B20
		public override void remove_onmousemove(HTMLFrameSiteEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601918F RID: 102799 RVA: 0x00107C10 File Offset: 0x00106C10
		public override void add_onmouseover(HTMLFrameSiteEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019190 RID: 102800 RVA: 0x00107CA0 File Offset: 0x00106CA0
		public override void remove_onmouseover(HTMLFrameSiteEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019191 RID: 102801 RVA: 0x00107D90 File Offset: 0x00106D90
		public override void add_onmouseout(HTMLFrameSiteEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019192 RID: 102802 RVA: 0x00107E20 File Offset: 0x00106E20
		public override void remove_onmouseout(HTMLFrameSiteEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019193 RID: 102803 RVA: 0x00107F10 File Offset: 0x00106F10
		public override void add_onkeyup(HTMLFrameSiteEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019194 RID: 102804 RVA: 0x00107FA0 File Offset: 0x00106FA0
		public override void remove_onkeyup(HTMLFrameSiteEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019195 RID: 102805 RVA: 0x00108090 File Offset: 0x00107090
		public override void add_onkeydown(HTMLFrameSiteEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019196 RID: 102806 RVA: 0x00108120 File Offset: 0x00107120
		public override void remove_onkeydown(HTMLFrameSiteEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019197 RID: 102807 RVA: 0x00108210 File Offset: 0x00107210
		public override void add_onkeypress(HTMLFrameSiteEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x06019198 RID: 102808 RVA: 0x001082A0 File Offset: 0x001072A0
		public override void remove_onkeypress(HTMLFrameSiteEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019199 RID: 102809 RVA: 0x00108390 File Offset: 0x00107390
		public override void add_ondblclick(HTMLFrameSiteEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601919A RID: 102810 RVA: 0x00108420 File Offset: 0x00107420
		public override void remove_ondblclick(HTMLFrameSiteEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601919B RID: 102811 RVA: 0x00108510 File Offset: 0x00107510
		public override void add_onclick(HTMLFrameSiteEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601919C RID: 102812 RVA: 0x001085A0 File Offset: 0x001075A0
		public override void remove_onclick(HTMLFrameSiteEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onclickDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601919D RID: 102813 RVA: 0x00108690 File Offset: 0x00107690
		public override void add_onhelp(HTMLFrameSiteEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = new HTMLFrameSiteEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents2_SinkHelper, ref num);
				htmlframeSiteEvents2_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents2_SinkHelper);
			}
		}

		// Token: 0x0601919E RID: 102814 RVA: 0x00108720 File Offset: 0x00107720
		public override void remove_onhelp(HTMLFrameSiteEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlframeSiteEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601919F RID: 102815 RVA: 0x00108810 File Offset: 0x00107810
		public HTMLFrameSiteEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060191A0 RID: 102816 RVA: 0x00108838 File Offset: 0x00107838
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
								HTMLFrameSiteEvents2_SinkHelper htmlframeSiteEvents2_SinkHelper = (HTMLFrameSiteEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x060191A1 RID: 102817 RVA: 0x001088EC File Offset: 0x001078EC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000EB5 RID: 3765
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000EB6 RID: 3766
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000EB7 RID: 3767
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
