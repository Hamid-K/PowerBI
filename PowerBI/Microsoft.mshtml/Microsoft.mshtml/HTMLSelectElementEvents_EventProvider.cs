using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DCF RID: 3535
	internal sealed class HTMLSelectElementEvents_EventProvider : HTMLSelectElementEvents_Event, IDisposable
	{
		// Token: 0x060179F3 RID: 96755 RVA: 0x0002F5DC File Offset: 0x0002E5DC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				2, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060179F4 RID: 96756 RVA: 0x0002F6F0 File Offset: 0x0002E6F0
		public override void add_onchange(HTMLSelectElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179F5 RID: 96757 RVA: 0x0002F780 File Offset: 0x0002E780
		public override void remove_onchange(HTMLSelectElementEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onchangeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179F6 RID: 96758 RVA: 0x0002F870 File Offset: 0x0002E870
		public override void add_onfocusout(HTMLSelectElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179F7 RID: 96759 RVA: 0x0002F900 File Offset: 0x0002E900
		public override void remove_onfocusout(HTMLSelectElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179F8 RID: 96760 RVA: 0x0002F9F0 File Offset: 0x0002E9F0
		public override void add_onfocusin(HTMLSelectElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179F9 RID: 96761 RVA: 0x0002FA80 File Offset: 0x0002EA80
		public override void remove_onfocusin(HTMLSelectElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179FA RID: 96762 RVA: 0x0002FB70 File Offset: 0x0002EB70
		public override void add_ondeactivate(HTMLSelectElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179FB RID: 96763 RVA: 0x0002FC00 File Offset: 0x0002EC00
		public override void remove_ondeactivate(HTMLSelectElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179FC RID: 96764 RVA: 0x0002FCF0 File Offset: 0x0002ECF0
		public override void add_onactivate(HTMLSelectElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179FD RID: 96765 RVA: 0x0002FD80 File Offset: 0x0002ED80
		public override void remove_onactivate(HTMLSelectElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060179FE RID: 96766 RVA: 0x0002FE70 File Offset: 0x0002EE70
		public override void add_onmousewheel(HTMLSelectElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x060179FF RID: 96767 RVA: 0x0002FF00 File Offset: 0x0002EF00
		public override void remove_onmousewheel(HTMLSelectElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A00 RID: 96768 RVA: 0x0002FFF0 File Offset: 0x0002EFF0
		public override void add_onmouseleave(HTMLSelectElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A01 RID: 96769 RVA: 0x00030080 File Offset: 0x0002F080
		public override void remove_onmouseleave(HTMLSelectElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A02 RID: 96770 RVA: 0x00030170 File Offset: 0x0002F170
		public override void add_onmouseenter(HTMLSelectElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A03 RID: 96771 RVA: 0x00030200 File Offset: 0x0002F200
		public override void remove_onmouseenter(HTMLSelectElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A04 RID: 96772 RVA: 0x000302F0 File Offset: 0x0002F2F0
		public override void add_onresizeend(HTMLSelectElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A05 RID: 96773 RVA: 0x00030380 File Offset: 0x0002F380
		public override void remove_onresizeend(HTMLSelectElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A06 RID: 96774 RVA: 0x00030470 File Offset: 0x0002F470
		public override void add_onresizestart(HTMLSelectElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A07 RID: 96775 RVA: 0x00030500 File Offset: 0x0002F500
		public override void remove_onresizestart(HTMLSelectElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A08 RID: 96776 RVA: 0x000305F0 File Offset: 0x0002F5F0
		public override void add_onmoveend(HTMLSelectElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A09 RID: 96777 RVA: 0x00030680 File Offset: 0x0002F680
		public override void remove_onmoveend(HTMLSelectElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A0A RID: 96778 RVA: 0x00030770 File Offset: 0x0002F770
		public override void add_onmovestart(HTMLSelectElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A0B RID: 96779 RVA: 0x00030800 File Offset: 0x0002F800
		public override void remove_onmovestart(HTMLSelectElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A0C RID: 96780 RVA: 0x000308F0 File Offset: 0x0002F8F0
		public override void add_oncontrolselect(HTMLSelectElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A0D RID: 96781 RVA: 0x00030980 File Offset: 0x0002F980
		public override void remove_oncontrolselect(HTMLSelectElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlselectElementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A0E RID: 96782 RVA: 0x00030A70 File Offset: 0x0002FA70
		public override void add_onmove(HTMLSelectElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A0F RID: 96783 RVA: 0x00030B00 File Offset: 0x0002FB00
		public override void remove_onmove(HTMLSelectElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A10 RID: 96784 RVA: 0x00030BF0 File Offset: 0x0002FBF0
		public override void add_onbeforeactivate(HTMLSelectElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A11 RID: 96785 RVA: 0x00030C80 File Offset: 0x0002FC80
		public override void remove_onbeforeactivate(HTMLSelectElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A12 RID: 96786 RVA: 0x00030D70 File Offset: 0x0002FD70
		public override void add_onbeforedeactivate(HTMLSelectElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A13 RID: 96787 RVA: 0x00030E00 File Offset: 0x0002FE00
		public override void remove_onbeforedeactivate(HTMLSelectElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A14 RID: 96788 RVA: 0x00030EF0 File Offset: 0x0002FEF0
		public override void add_onpage(HTMLSelectElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A15 RID: 96789 RVA: 0x00030F80 File Offset: 0x0002FF80
		public override void remove_onpage(HTMLSelectElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onpageDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A16 RID: 96790 RVA: 0x00031070 File Offset: 0x00030070
		public override void add_onlayoutcomplete(HTMLSelectElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A17 RID: 96791 RVA: 0x00031100 File Offset: 0x00030100
		public override void remove_onlayoutcomplete(HTMLSelectElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A18 RID: 96792 RVA: 0x000311F0 File Offset: 0x000301F0
		public override void add_onbeforeeditfocus(HTMLSelectElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A19 RID: 96793 RVA: 0x00031280 File Offset: 0x00030280
		public override void remove_onbeforeeditfocus(HTMLSelectElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A1A RID: 96794 RVA: 0x00031370 File Offset: 0x00030370
		public override void add_onreadystatechange(HTMLSelectElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A1B RID: 96795 RVA: 0x00031400 File Offset: 0x00030400
		public override void remove_onreadystatechange(HTMLSelectElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A1C RID: 96796 RVA: 0x000314F0 File Offset: 0x000304F0
		public override void add_oncellchange(HTMLSelectElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A1D RID: 96797 RVA: 0x00031580 File Offset: 0x00030580
		public override void remove_oncellchange(HTMLSelectElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A1E RID: 96798 RVA: 0x00031670 File Offset: 0x00030670
		public override void add_onrowsinserted(HTMLSelectElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A1F RID: 96799 RVA: 0x00031700 File Offset: 0x00030700
		public override void remove_onrowsinserted(HTMLSelectElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A20 RID: 96800 RVA: 0x000317F0 File Offset: 0x000307F0
		public override void add_onrowsdelete(HTMLSelectElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A21 RID: 96801 RVA: 0x00031880 File Offset: 0x00030880
		public override void remove_onrowsdelete(HTMLSelectElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A22 RID: 96802 RVA: 0x00031970 File Offset: 0x00030970
		public override void add_oncontextmenu(HTMLSelectElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A23 RID: 96803 RVA: 0x00031A00 File Offset: 0x00030A00
		public override void remove_oncontextmenu(HTMLSelectElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlselectElementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A24 RID: 96804 RVA: 0x00031AF0 File Offset: 0x00030AF0
		public override void add_onpaste(HTMLSelectElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A25 RID: 96805 RVA: 0x00031B80 File Offset: 0x00030B80
		public override void remove_onpaste(HTMLSelectElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A26 RID: 96806 RVA: 0x00031C70 File Offset: 0x00030C70
		public override void add_onbeforepaste(HTMLSelectElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A27 RID: 96807 RVA: 0x00031D00 File Offset: 0x00030D00
		public override void remove_onbeforepaste(HTMLSelectElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A28 RID: 96808 RVA: 0x00031DF0 File Offset: 0x00030DF0
		public override void add_oncopy(HTMLSelectElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A29 RID: 96809 RVA: 0x00031E80 File Offset: 0x00030E80
		public override void remove_oncopy(HTMLSelectElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlselectElementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A2A RID: 96810 RVA: 0x00031F70 File Offset: 0x00030F70
		public override void add_onbeforecopy(HTMLSelectElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A2B RID: 96811 RVA: 0x00032000 File Offset: 0x00031000
		public override void remove_onbeforecopy(HTMLSelectElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A2C RID: 96812 RVA: 0x000320F0 File Offset: 0x000310F0
		public override void add_oncut(HTMLSelectElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A2D RID: 96813 RVA: 0x00032180 File Offset: 0x00031180
		public override void remove_oncut(HTMLSelectElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_oncutDelegate != null && ((htmlselectElementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A2E RID: 96814 RVA: 0x00032270 File Offset: 0x00031270
		public override void add_onbeforecut(HTMLSelectElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A2F RID: 96815 RVA: 0x00032300 File Offset: 0x00031300
		public override void remove_onbeforecut(HTMLSelectElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A30 RID: 96816 RVA: 0x000323F0 File Offset: 0x000313F0
		public override void add_ondrop(HTMLSelectElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A31 RID: 96817 RVA: 0x00032480 File Offset: 0x00031480
		public override void remove_ondrop(HTMLSelectElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondropDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A32 RID: 96818 RVA: 0x00032570 File Offset: 0x00031570
		public override void add_ondragleave(HTMLSelectElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A33 RID: 96819 RVA: 0x00032600 File Offset: 0x00031600
		public override void remove_ondragleave(HTMLSelectElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A34 RID: 96820 RVA: 0x000326F0 File Offset: 0x000316F0
		public override void add_ondragover(HTMLSelectElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A35 RID: 96821 RVA: 0x00032780 File Offset: 0x00031780
		public override void remove_ondragover(HTMLSelectElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A36 RID: 96822 RVA: 0x00032870 File Offset: 0x00031870
		public override void add_ondragenter(HTMLSelectElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A37 RID: 96823 RVA: 0x00032900 File Offset: 0x00031900
		public override void remove_ondragenter(HTMLSelectElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A38 RID: 96824 RVA: 0x000329F0 File Offset: 0x000319F0
		public override void add_ondragend(HTMLSelectElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A39 RID: 96825 RVA: 0x00032A80 File Offset: 0x00031A80
		public override void remove_ondragend(HTMLSelectElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A3A RID: 96826 RVA: 0x00032B70 File Offset: 0x00031B70
		public override void add_ondrag(HTMLSelectElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A3B RID: 96827 RVA: 0x00032C00 File Offset: 0x00031C00
		public override void remove_ondrag(HTMLSelectElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A3C RID: 96828 RVA: 0x00032CF0 File Offset: 0x00031CF0
		public override void add_onresize(HTMLSelectElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A3D RID: 96829 RVA: 0x00032D80 File Offset: 0x00031D80
		public override void remove_onresize(HTMLSelectElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A3E RID: 96830 RVA: 0x00032E70 File Offset: 0x00031E70
		public override void add_onblur(HTMLSelectElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A3F RID: 96831 RVA: 0x00032F00 File Offset: 0x00031F00
		public override void remove_onblur(HTMLSelectElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onblurDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A40 RID: 96832 RVA: 0x00032FF0 File Offset: 0x00031FF0
		public override void add_onfocus(HTMLSelectElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A41 RID: 96833 RVA: 0x00033080 File Offset: 0x00032080
		public override void remove_onfocus(HTMLSelectElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A42 RID: 96834 RVA: 0x00033170 File Offset: 0x00032170
		public override void add_onscroll(HTMLSelectElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A43 RID: 96835 RVA: 0x00033200 File Offset: 0x00032200
		public override void remove_onscroll(HTMLSelectElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A44 RID: 96836 RVA: 0x000332F0 File Offset: 0x000322F0
		public override void add_onpropertychange(HTMLSelectElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A45 RID: 96837 RVA: 0x00033380 File Offset: 0x00032380
		public override void remove_onpropertychange(HTMLSelectElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A46 RID: 96838 RVA: 0x00033470 File Offset: 0x00032470
		public override void add_onlosecapture(HTMLSelectElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A47 RID: 96839 RVA: 0x00033500 File Offset: 0x00032500
		public override void remove_onlosecapture(HTMLSelectElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A48 RID: 96840 RVA: 0x000335F0 File Offset: 0x000325F0
		public override void add_ondatasetcomplete(HTMLSelectElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A49 RID: 96841 RVA: 0x00033680 File Offset: 0x00032680
		public override void remove_ondatasetcomplete(HTMLSelectElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A4A RID: 96842 RVA: 0x00033770 File Offset: 0x00032770
		public override void add_ondataavailable(HTMLSelectElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A4B RID: 96843 RVA: 0x00033800 File Offset: 0x00032800
		public override void remove_ondataavailable(HTMLSelectElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A4C RID: 96844 RVA: 0x000338F0 File Offset: 0x000328F0
		public override void add_ondatasetchanged(HTMLSelectElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A4D RID: 96845 RVA: 0x00033980 File Offset: 0x00032980
		public override void remove_ondatasetchanged(HTMLSelectElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A4E RID: 96846 RVA: 0x00033A70 File Offset: 0x00032A70
		public override void add_onrowenter(HTMLSelectElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A4F RID: 96847 RVA: 0x00033B00 File Offset: 0x00032B00
		public override void remove_onrowenter(HTMLSelectElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A50 RID: 96848 RVA: 0x00033BF0 File Offset: 0x00032BF0
		public override void add_onrowexit(HTMLSelectElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A51 RID: 96849 RVA: 0x00033C80 File Offset: 0x00032C80
		public override void remove_onrowexit(HTMLSelectElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A52 RID: 96850 RVA: 0x00033D70 File Offset: 0x00032D70
		public override void add_onerrorupdate(HTMLSelectElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A53 RID: 96851 RVA: 0x00033E00 File Offset: 0x00032E00
		public override void remove_onerrorupdate(HTMLSelectElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A54 RID: 96852 RVA: 0x00033EF0 File Offset: 0x00032EF0
		public override void add_onafterupdate(HTMLSelectElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A55 RID: 96853 RVA: 0x00033F80 File Offset: 0x00032F80
		public override void remove_onafterupdate(HTMLSelectElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A56 RID: 96854 RVA: 0x00034070 File Offset: 0x00033070
		public override void add_onbeforeupdate(HTMLSelectElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A57 RID: 96855 RVA: 0x00034100 File Offset: 0x00033100
		public override void remove_onbeforeupdate(HTMLSelectElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A58 RID: 96856 RVA: 0x000341F0 File Offset: 0x000331F0
		public override void add_ondragstart(HTMLSelectElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A59 RID: 96857 RVA: 0x00034280 File Offset: 0x00033280
		public override void remove_ondragstart(HTMLSelectElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A5A RID: 96858 RVA: 0x00034370 File Offset: 0x00033370
		public override void add_onfilterchange(HTMLSelectElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A5B RID: 96859 RVA: 0x00034400 File Offset: 0x00033400
		public override void remove_onfilterchange(HTMLSelectElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A5C RID: 96860 RVA: 0x000344F0 File Offset: 0x000334F0
		public override void add_onselectstart(HTMLSelectElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A5D RID: 96861 RVA: 0x00034580 File Offset: 0x00033580
		public override void remove_onselectstart(HTMLSelectElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A5E RID: 96862 RVA: 0x00034670 File Offset: 0x00033670
		public override void add_onmouseup(HTMLSelectElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A5F RID: 96863 RVA: 0x00034700 File Offset: 0x00033700
		public override void remove_onmouseup(HTMLSelectElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A60 RID: 96864 RVA: 0x000347F0 File Offset: 0x000337F0
		public override void add_onmousedown(HTMLSelectElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A61 RID: 96865 RVA: 0x00034880 File Offset: 0x00033880
		public override void remove_onmousedown(HTMLSelectElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A62 RID: 96866 RVA: 0x00034970 File Offset: 0x00033970
		public override void add_onmousemove(HTMLSelectElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A63 RID: 96867 RVA: 0x00034A00 File Offset: 0x00033A00
		public override void remove_onmousemove(HTMLSelectElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A64 RID: 96868 RVA: 0x00034AF0 File Offset: 0x00033AF0
		public override void add_onmouseover(HTMLSelectElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A65 RID: 96869 RVA: 0x00034B80 File Offset: 0x00033B80
		public override void remove_onmouseover(HTMLSelectElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A66 RID: 96870 RVA: 0x00034C70 File Offset: 0x00033C70
		public override void add_onmouseout(HTMLSelectElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A67 RID: 96871 RVA: 0x00034D00 File Offset: 0x00033D00
		public override void remove_onmouseout(HTMLSelectElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A68 RID: 96872 RVA: 0x00034DF0 File Offset: 0x00033DF0
		public override void add_onkeyup(HTMLSelectElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A69 RID: 96873 RVA: 0x00034E80 File Offset: 0x00033E80
		public override void remove_onkeyup(HTMLSelectElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A6A RID: 96874 RVA: 0x00034F70 File Offset: 0x00033F70
		public override void add_onkeydown(HTMLSelectElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A6B RID: 96875 RVA: 0x00035000 File Offset: 0x00034000
		public override void remove_onkeydown(HTMLSelectElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A6C RID: 96876 RVA: 0x000350F0 File Offset: 0x000340F0
		public override void add_onkeypress(HTMLSelectElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A6D RID: 96877 RVA: 0x00035180 File Offset: 0x00034180
		public override void remove_onkeypress(HTMLSelectElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A6E RID: 96878 RVA: 0x00035270 File Offset: 0x00034270
		public override void add_ondblclick(HTMLSelectElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A6F RID: 96879 RVA: 0x00035300 File Offset: 0x00034300
		public override void remove_ondblclick(HTMLSelectElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlselectElementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A70 RID: 96880 RVA: 0x000353F0 File Offset: 0x000343F0
		public override void add_onclick(HTMLSelectElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A71 RID: 96881 RVA: 0x00035480 File Offset: 0x00034480
		public override void remove_onclick(HTMLSelectElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onclickDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A72 RID: 96882 RVA: 0x00035570 File Offset: 0x00034570
		public override void add_onhelp(HTMLSelectElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = new HTMLSelectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlselectElementEvents_SinkHelper, ref num);
				htmlselectElementEvents_SinkHelper.m_dwCookie = num;
				htmlselectElementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlselectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06017A73 RID: 96883 RVA: 0x00035600 File Offset: 0x00034600
		public override void remove_onhelp(HTMLSelectElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper;
					for (;;)
					{
						htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlselectElementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlselectElementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017A74 RID: 96884 RVA: 0x000356F0 File Offset: 0x000346F0
		public HTMLSelectElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017A75 RID: 96885 RVA: 0x00035718 File Offset: 0x00034718
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
								HTMLSelectElementEvents_SinkHelper htmlselectElementEvents_SinkHelper = (HTMLSelectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlselectElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017A76 RID: 96886 RVA: 0x000357CC File Offset: 0x000347CC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040006AF RID: 1711
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040006B0 RID: 1712
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040006B1 RID: 1713
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
