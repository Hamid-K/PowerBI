using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF7 RID: 3575
	internal sealed class HTMLStyleElementEvents2_EventProvider : HTMLStyleElementEvents2_Event, IDisposable
	{
		// Token: 0x06018759 RID: 100185 RVA: 0x000A976C File Offset: 0x000A876C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				21, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601875A RID: 100186 RVA: 0x000A9880 File Offset: 0x000A8880
		public override void add_onerror(HTMLStyleElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601875B RID: 100187 RVA: 0x000A9910 File Offset: 0x000A8910
		public override void remove_onerror(HTMLStyleElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601875C RID: 100188 RVA: 0x000A9A00 File Offset: 0x000A8A00
		public override void add_onload(HTMLStyleElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601875D RID: 100189 RVA: 0x000A9A90 File Offset: 0x000A8A90
		public override void remove_onload(HTMLStyleElementEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onloadDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601875E RID: 100190 RVA: 0x000A9B80 File Offset: 0x000A8B80
		public override void add_onmousewheel(HTMLStyleElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601875F RID: 100191 RVA: 0x000A9C10 File Offset: 0x000A8C10
		public override void remove_onmousewheel(HTMLStyleElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018760 RID: 100192 RVA: 0x000A9D00 File Offset: 0x000A8D00
		public override void add_onresizeend(HTMLStyleElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018761 RID: 100193 RVA: 0x000A9D90 File Offset: 0x000A8D90
		public override void remove_onresizeend(HTMLStyleElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018762 RID: 100194 RVA: 0x000A9E80 File Offset: 0x000A8E80
		public override void add_onresizestart(HTMLStyleElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018763 RID: 100195 RVA: 0x000A9F10 File Offset: 0x000A8F10
		public override void remove_onresizestart(HTMLStyleElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018764 RID: 100196 RVA: 0x000AA000 File Offset: 0x000A9000
		public override void add_onmoveend(HTMLStyleElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018765 RID: 100197 RVA: 0x000AA090 File Offset: 0x000A9090
		public override void remove_onmoveend(HTMLStyleElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018766 RID: 100198 RVA: 0x000AA180 File Offset: 0x000A9180
		public override void add_onmovestart(HTMLStyleElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018767 RID: 100199 RVA: 0x000AA210 File Offset: 0x000A9210
		public override void remove_onmovestart(HTMLStyleElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018768 RID: 100200 RVA: 0x000AA300 File Offset: 0x000A9300
		public override void add_oncontrolselect(HTMLStyleElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018769 RID: 100201 RVA: 0x000AA390 File Offset: 0x000A9390
		public override void remove_oncontrolselect(HTMLStyleElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601876A RID: 100202 RVA: 0x000AA480 File Offset: 0x000A9480
		public override void add_onmove(HTMLStyleElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601876B RID: 100203 RVA: 0x000AA510 File Offset: 0x000A9510
		public override void remove_onmove(HTMLStyleElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601876C RID: 100204 RVA: 0x000AA600 File Offset: 0x000A9600
		public override void add_onfocusout(HTMLStyleElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601876D RID: 100205 RVA: 0x000AA690 File Offset: 0x000A9690
		public override void remove_onfocusout(HTMLStyleElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601876E RID: 100206 RVA: 0x000AA780 File Offset: 0x000A9780
		public override void add_onfocusin(HTMLStyleElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601876F RID: 100207 RVA: 0x000AA810 File Offset: 0x000A9810
		public override void remove_onfocusin(HTMLStyleElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018770 RID: 100208 RVA: 0x000AA900 File Offset: 0x000A9900
		public override void add_onbeforeactivate(HTMLStyleElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018771 RID: 100209 RVA: 0x000AA990 File Offset: 0x000A9990
		public override void remove_onbeforeactivate(HTMLStyleElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018772 RID: 100210 RVA: 0x000AAA80 File Offset: 0x000A9A80
		public override void add_onbeforedeactivate(HTMLStyleElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018773 RID: 100211 RVA: 0x000AAB10 File Offset: 0x000A9B10
		public override void remove_onbeforedeactivate(HTMLStyleElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018774 RID: 100212 RVA: 0x000AAC00 File Offset: 0x000A9C00
		public override void add_ondeactivate(HTMLStyleElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018775 RID: 100213 RVA: 0x000AAC90 File Offset: 0x000A9C90
		public override void remove_ondeactivate(HTMLStyleElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018776 RID: 100214 RVA: 0x000AAD80 File Offset: 0x000A9D80
		public override void add_onactivate(HTMLStyleElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018777 RID: 100215 RVA: 0x000AAE10 File Offset: 0x000A9E10
		public override void remove_onactivate(HTMLStyleElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018778 RID: 100216 RVA: 0x000AAF00 File Offset: 0x000A9F00
		public override void add_onmouseleave(HTMLStyleElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018779 RID: 100217 RVA: 0x000AAF90 File Offset: 0x000A9F90
		public override void remove_onmouseleave(HTMLStyleElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601877A RID: 100218 RVA: 0x000AB080 File Offset: 0x000AA080
		public override void add_onmouseenter(HTMLStyleElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601877B RID: 100219 RVA: 0x000AB110 File Offset: 0x000AA110
		public override void remove_onmouseenter(HTMLStyleElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601877C RID: 100220 RVA: 0x000AB200 File Offset: 0x000AA200
		public override void add_onpage(HTMLStyleElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601877D RID: 100221 RVA: 0x000AB290 File Offset: 0x000AA290
		public override void remove_onpage(HTMLStyleElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601877E RID: 100222 RVA: 0x000AB380 File Offset: 0x000AA380
		public override void add_onlayoutcomplete(HTMLStyleElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601877F RID: 100223 RVA: 0x000AB410 File Offset: 0x000AA410
		public override void remove_onlayoutcomplete(HTMLStyleElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018780 RID: 100224 RVA: 0x000AB500 File Offset: 0x000AA500
		public override void add_onreadystatechange(HTMLStyleElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018781 RID: 100225 RVA: 0x000AB590 File Offset: 0x000AA590
		public override void remove_onreadystatechange(HTMLStyleElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018782 RID: 100226 RVA: 0x000AB680 File Offset: 0x000AA680
		public override void add_oncellchange(HTMLStyleElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018783 RID: 100227 RVA: 0x000AB710 File Offset: 0x000AA710
		public override void remove_oncellchange(HTMLStyleElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018784 RID: 100228 RVA: 0x000AB800 File Offset: 0x000AA800
		public override void add_onrowsinserted(HTMLStyleElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018785 RID: 100229 RVA: 0x000AB890 File Offset: 0x000AA890
		public override void remove_onrowsinserted(HTMLStyleElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018786 RID: 100230 RVA: 0x000AB980 File Offset: 0x000AA980
		public override void add_onrowsdelete(HTMLStyleElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018787 RID: 100231 RVA: 0x000ABA10 File Offset: 0x000AAA10
		public override void remove_onrowsdelete(HTMLStyleElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018788 RID: 100232 RVA: 0x000ABB00 File Offset: 0x000AAB00
		public override void add_oncontextmenu(HTMLStyleElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018789 RID: 100233 RVA: 0x000ABB90 File Offset: 0x000AAB90
		public override void remove_oncontextmenu(HTMLStyleElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601878A RID: 100234 RVA: 0x000ABC80 File Offset: 0x000AAC80
		public override void add_onpaste(HTMLStyleElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601878B RID: 100235 RVA: 0x000ABD10 File Offset: 0x000AAD10
		public override void remove_onpaste(HTMLStyleElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601878C RID: 100236 RVA: 0x000ABE00 File Offset: 0x000AAE00
		public override void add_onbeforepaste(HTMLStyleElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601878D RID: 100237 RVA: 0x000ABE90 File Offset: 0x000AAE90
		public override void remove_onbeforepaste(HTMLStyleElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601878E RID: 100238 RVA: 0x000ABF80 File Offset: 0x000AAF80
		public override void add_oncopy(HTMLStyleElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601878F RID: 100239 RVA: 0x000AC010 File Offset: 0x000AB010
		public override void remove_oncopy(HTMLStyleElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018790 RID: 100240 RVA: 0x000AC100 File Offset: 0x000AB100
		public override void add_onbeforecopy(HTMLStyleElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018791 RID: 100241 RVA: 0x000AC190 File Offset: 0x000AB190
		public override void remove_onbeforecopy(HTMLStyleElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018792 RID: 100242 RVA: 0x000AC280 File Offset: 0x000AB280
		public override void add_oncut(HTMLStyleElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018793 RID: 100243 RVA: 0x000AC310 File Offset: 0x000AB310
		public override void remove_oncut(HTMLStyleElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018794 RID: 100244 RVA: 0x000AC400 File Offset: 0x000AB400
		public override void add_onbeforecut(HTMLStyleElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018795 RID: 100245 RVA: 0x000AC490 File Offset: 0x000AB490
		public override void remove_onbeforecut(HTMLStyleElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018796 RID: 100246 RVA: 0x000AC580 File Offset: 0x000AB580
		public override void add_ondrop(HTMLStyleElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018797 RID: 100247 RVA: 0x000AC610 File Offset: 0x000AB610
		public override void remove_ondrop(HTMLStyleElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018798 RID: 100248 RVA: 0x000AC700 File Offset: 0x000AB700
		public override void add_ondragleave(HTMLStyleElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018799 RID: 100249 RVA: 0x000AC790 File Offset: 0x000AB790
		public override void remove_ondragleave(HTMLStyleElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601879A RID: 100250 RVA: 0x000AC880 File Offset: 0x000AB880
		public override void add_ondragover(HTMLStyleElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601879B RID: 100251 RVA: 0x000AC910 File Offset: 0x000AB910
		public override void remove_ondragover(HTMLStyleElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601879C RID: 100252 RVA: 0x000ACA00 File Offset: 0x000ABA00
		public override void add_ondragenter(HTMLStyleElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601879D RID: 100253 RVA: 0x000ACA90 File Offset: 0x000ABA90
		public override void remove_ondragenter(HTMLStyleElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601879E RID: 100254 RVA: 0x000ACB80 File Offset: 0x000ABB80
		public override void add_ondragend(HTMLStyleElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601879F RID: 100255 RVA: 0x000ACC10 File Offset: 0x000ABC10
		public override void remove_ondragend(HTMLStyleElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187A0 RID: 100256 RVA: 0x000ACD00 File Offset: 0x000ABD00
		public override void add_ondrag(HTMLStyleElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187A1 RID: 100257 RVA: 0x000ACD90 File Offset: 0x000ABD90
		public override void remove_ondrag(HTMLStyleElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187A2 RID: 100258 RVA: 0x000ACE80 File Offset: 0x000ABE80
		public override void add_onresize(HTMLStyleElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187A3 RID: 100259 RVA: 0x000ACF10 File Offset: 0x000ABF10
		public override void remove_onresize(HTMLStyleElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187A4 RID: 100260 RVA: 0x000AD000 File Offset: 0x000AC000
		public override void add_onblur(HTMLStyleElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187A5 RID: 100261 RVA: 0x000AD090 File Offset: 0x000AC090
		public override void remove_onblur(HTMLStyleElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187A6 RID: 100262 RVA: 0x000AD180 File Offset: 0x000AC180
		public override void add_onfocus(HTMLStyleElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187A7 RID: 100263 RVA: 0x000AD210 File Offset: 0x000AC210
		public override void remove_onfocus(HTMLStyleElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187A8 RID: 100264 RVA: 0x000AD300 File Offset: 0x000AC300
		public override void add_onscroll(HTMLStyleElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187A9 RID: 100265 RVA: 0x000AD390 File Offset: 0x000AC390
		public override void remove_onscroll(HTMLStyleElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187AA RID: 100266 RVA: 0x000AD480 File Offset: 0x000AC480
		public override void add_onpropertychange(HTMLStyleElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187AB RID: 100267 RVA: 0x000AD510 File Offset: 0x000AC510
		public override void remove_onpropertychange(HTMLStyleElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187AC RID: 100268 RVA: 0x000AD600 File Offset: 0x000AC600
		public override void add_onlosecapture(HTMLStyleElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187AD RID: 100269 RVA: 0x000AD690 File Offset: 0x000AC690
		public override void remove_onlosecapture(HTMLStyleElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187AE RID: 100270 RVA: 0x000AD780 File Offset: 0x000AC780
		public override void add_ondatasetcomplete(HTMLStyleElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187AF RID: 100271 RVA: 0x000AD810 File Offset: 0x000AC810
		public override void remove_ondatasetcomplete(HTMLStyleElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187B0 RID: 100272 RVA: 0x000AD900 File Offset: 0x000AC900
		public override void add_ondataavailable(HTMLStyleElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187B1 RID: 100273 RVA: 0x000AD990 File Offset: 0x000AC990
		public override void remove_ondataavailable(HTMLStyleElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187B2 RID: 100274 RVA: 0x000ADA80 File Offset: 0x000ACA80
		public override void add_ondatasetchanged(HTMLStyleElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187B3 RID: 100275 RVA: 0x000ADB10 File Offset: 0x000ACB10
		public override void remove_ondatasetchanged(HTMLStyleElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187B4 RID: 100276 RVA: 0x000ADC00 File Offset: 0x000ACC00
		public override void add_onrowenter(HTMLStyleElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187B5 RID: 100277 RVA: 0x000ADC90 File Offset: 0x000ACC90
		public override void remove_onrowenter(HTMLStyleElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187B6 RID: 100278 RVA: 0x000ADD80 File Offset: 0x000ACD80
		public override void add_onrowexit(HTMLStyleElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187B7 RID: 100279 RVA: 0x000ADE10 File Offset: 0x000ACE10
		public override void remove_onrowexit(HTMLStyleElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187B8 RID: 100280 RVA: 0x000ADF00 File Offset: 0x000ACF00
		public override void add_onerrorupdate(HTMLStyleElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187B9 RID: 100281 RVA: 0x000ADF90 File Offset: 0x000ACF90
		public override void remove_onerrorupdate(HTMLStyleElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187BA RID: 100282 RVA: 0x000AE080 File Offset: 0x000AD080
		public override void add_onafterupdate(HTMLStyleElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187BB RID: 100283 RVA: 0x000AE110 File Offset: 0x000AD110
		public override void remove_onafterupdate(HTMLStyleElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187BC RID: 100284 RVA: 0x000AE200 File Offset: 0x000AD200
		public override void add_onbeforeupdate(HTMLStyleElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187BD RID: 100285 RVA: 0x000AE290 File Offset: 0x000AD290
		public override void remove_onbeforeupdate(HTMLStyleElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187BE RID: 100286 RVA: 0x000AE380 File Offset: 0x000AD380
		public override void add_ondragstart(HTMLStyleElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187BF RID: 100287 RVA: 0x000AE410 File Offset: 0x000AD410
		public override void remove_ondragstart(HTMLStyleElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187C0 RID: 100288 RVA: 0x000AE500 File Offset: 0x000AD500
		public override void add_onfilterchange(HTMLStyleElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187C1 RID: 100289 RVA: 0x000AE590 File Offset: 0x000AD590
		public override void remove_onfilterchange(HTMLStyleElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187C2 RID: 100290 RVA: 0x000AE680 File Offset: 0x000AD680
		public override void add_onselectstart(HTMLStyleElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187C3 RID: 100291 RVA: 0x000AE710 File Offset: 0x000AD710
		public override void remove_onselectstart(HTMLStyleElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187C4 RID: 100292 RVA: 0x000AE800 File Offset: 0x000AD800
		public override void add_onmouseup(HTMLStyleElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187C5 RID: 100293 RVA: 0x000AE890 File Offset: 0x000AD890
		public override void remove_onmouseup(HTMLStyleElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187C6 RID: 100294 RVA: 0x000AE980 File Offset: 0x000AD980
		public override void add_onmousedown(HTMLStyleElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187C7 RID: 100295 RVA: 0x000AEA10 File Offset: 0x000ADA10
		public override void remove_onmousedown(HTMLStyleElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187C8 RID: 100296 RVA: 0x000AEB00 File Offset: 0x000ADB00
		public override void add_onmousemove(HTMLStyleElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187C9 RID: 100297 RVA: 0x000AEB90 File Offset: 0x000ADB90
		public override void remove_onmousemove(HTMLStyleElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187CA RID: 100298 RVA: 0x000AEC80 File Offset: 0x000ADC80
		public override void add_onmouseover(HTMLStyleElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187CB RID: 100299 RVA: 0x000AED10 File Offset: 0x000ADD10
		public override void remove_onmouseover(HTMLStyleElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187CC RID: 100300 RVA: 0x000AEE00 File Offset: 0x000ADE00
		public override void add_onmouseout(HTMLStyleElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187CD RID: 100301 RVA: 0x000AEE90 File Offset: 0x000ADE90
		public override void remove_onmouseout(HTMLStyleElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187CE RID: 100302 RVA: 0x000AEF80 File Offset: 0x000ADF80
		public override void add_onkeyup(HTMLStyleElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187CF RID: 100303 RVA: 0x000AF010 File Offset: 0x000AE010
		public override void remove_onkeyup(HTMLStyleElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187D0 RID: 100304 RVA: 0x000AF100 File Offset: 0x000AE100
		public override void add_onkeydown(HTMLStyleElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187D1 RID: 100305 RVA: 0x000AF190 File Offset: 0x000AE190
		public override void remove_onkeydown(HTMLStyleElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187D2 RID: 100306 RVA: 0x000AF280 File Offset: 0x000AE280
		public override void add_onkeypress(HTMLStyleElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187D3 RID: 100307 RVA: 0x000AF310 File Offset: 0x000AE310
		public override void remove_onkeypress(HTMLStyleElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187D4 RID: 100308 RVA: 0x000AF400 File Offset: 0x000AE400
		public override void add_ondblclick(HTMLStyleElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187D5 RID: 100309 RVA: 0x000AF490 File Offset: 0x000AE490
		public override void remove_ondblclick(HTMLStyleElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187D6 RID: 100310 RVA: 0x000AF580 File Offset: 0x000AE580
		public override void add_onclick(HTMLStyleElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187D7 RID: 100311 RVA: 0x000AF610 File Offset: 0x000AE610
		public override void remove_onclick(HTMLStyleElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187D8 RID: 100312 RVA: 0x000AF700 File Offset: 0x000AE700
		public override void add_onhelp(HTMLStyleElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = new HTMLStyleElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlstyleElementEvents2_SinkHelper, ref num);
				htmlstyleElementEvents2_SinkHelper.m_dwCookie = num;
				htmlstyleElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlstyleElementEvents2_SinkHelper);
			}
		}

		// Token: 0x060187D9 RID: 100313 RVA: 0x000AF790 File Offset: 0x000AE790
		public override void remove_onhelp(HTMLStyleElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper;
					for (;;)
					{
						htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlstyleElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlstyleElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
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
					this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060187DA RID: 100314 RVA: 0x000AF880 File Offset: 0x000AE880
		public HTMLStyleElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060187DB RID: 100315 RVA: 0x000AF8A8 File Offset: 0x000AE8A8
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
								HTMLStyleElementEvents2_SinkHelper htmlstyleElementEvents2_SinkHelper = (HTMLStyleElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlstyleElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x060187DC RID: 100316 RVA: 0x000AF95C File Offset: 0x000AE95C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B55 RID: 2901
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000B56 RID: 2902
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000B57 RID: 2903
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
