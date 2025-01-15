using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFD RID: 3581
	internal sealed class HTMLTextContainerEvents_EventProvider : HTMLTextContainerEvents_Event, IDisposable
	{
		// Token: 0x060189A0 RID: 100768 RVA: 0x000BE298 File Offset: 0x000BD298
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				114, 170, 246, 31, 66, 88, 207, 17, 167, 7,
				0, 170, 0, 192, 9, 141
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060189A1 RID: 100769 RVA: 0x000BE3AC File Offset: 0x000BD3AC
		public override void add_onselect(HTMLTextContainerEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189A2 RID: 100770 RVA: 0x000BE43C File Offset: 0x000BD43C
		public override void remove_onselect(HTMLTextContainerEvents_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onselectDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189A3 RID: 100771 RVA: 0x000BE52C File Offset: 0x000BD52C
		public override void add_onchange(HTMLTextContainerEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189A4 RID: 100772 RVA: 0x000BE5BC File Offset: 0x000BD5BC
		public override void remove_onchange(HTMLTextContainerEvents_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onchangeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189A5 RID: 100773 RVA: 0x000BE6AC File Offset: 0x000BD6AC
		public override void add_onfocusout(HTMLTextContainerEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189A6 RID: 100774 RVA: 0x000BE73C File Offset: 0x000BD73C
		public override void remove_onfocusout(HTMLTextContainerEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189A7 RID: 100775 RVA: 0x000BE82C File Offset: 0x000BD82C
		public override void add_onfocusin(HTMLTextContainerEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189A8 RID: 100776 RVA: 0x000BE8BC File Offset: 0x000BD8BC
		public override void remove_onfocusin(HTMLTextContainerEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onfocusinDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189A9 RID: 100777 RVA: 0x000BE9AC File Offset: 0x000BD9AC
		public override void add_ondeactivate(HTMLTextContainerEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189AA RID: 100778 RVA: 0x000BEA3C File Offset: 0x000BDA3C
		public override void remove_ondeactivate(HTMLTextContainerEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189AB RID: 100779 RVA: 0x000BEB2C File Offset: 0x000BDB2C
		public override void add_onactivate(HTMLTextContainerEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189AC RID: 100780 RVA: 0x000BEBBC File Offset: 0x000BDBBC
		public override void remove_onactivate(HTMLTextContainerEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onactivateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189AD RID: 100781 RVA: 0x000BECAC File Offset: 0x000BDCAC
		public override void add_onmousewheel(HTMLTextContainerEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189AE RID: 100782 RVA: 0x000BED3C File Offset: 0x000BDD3C
		public override void remove_onmousewheel(HTMLTextContainerEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189AF RID: 100783 RVA: 0x000BEE2C File Offset: 0x000BDE2C
		public override void add_onmouseleave(HTMLTextContainerEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189B0 RID: 100784 RVA: 0x000BEEBC File Offset: 0x000BDEBC
		public override void remove_onmouseleave(HTMLTextContainerEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189B1 RID: 100785 RVA: 0x000BEFAC File Offset: 0x000BDFAC
		public override void add_onmouseenter(HTMLTextContainerEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189B2 RID: 100786 RVA: 0x000BF03C File Offset: 0x000BE03C
		public override void remove_onmouseenter(HTMLTextContainerEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189B3 RID: 100787 RVA: 0x000BF12C File Offset: 0x000BE12C
		public override void add_onresizeend(HTMLTextContainerEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189B4 RID: 100788 RVA: 0x000BF1BC File Offset: 0x000BE1BC
		public override void remove_onresizeend(HTMLTextContainerEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onresizeendDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189B5 RID: 100789 RVA: 0x000BF2AC File Offset: 0x000BE2AC
		public override void add_onresizestart(HTMLTextContainerEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189B6 RID: 100790 RVA: 0x000BF33C File Offset: 0x000BE33C
		public override void remove_onresizestart(HTMLTextContainerEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onresizestartDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189B7 RID: 100791 RVA: 0x000BF42C File Offset: 0x000BE42C
		public override void add_onmoveend(HTMLTextContainerEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189B8 RID: 100792 RVA: 0x000BF4BC File Offset: 0x000BE4BC
		public override void remove_onmoveend(HTMLTextContainerEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmoveendDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189B9 RID: 100793 RVA: 0x000BF5AC File Offset: 0x000BE5AC
		public override void add_onmovestart(HTMLTextContainerEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189BA RID: 100794 RVA: 0x000BF63C File Offset: 0x000BE63C
		public override void remove_onmovestart(HTMLTextContainerEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmovestartDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189BB RID: 100795 RVA: 0x000BF72C File Offset: 0x000BE72C
		public override void add_oncontrolselect(HTMLTextContainerEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189BC RID: 100796 RVA: 0x000BF7BC File Offset: 0x000BE7BC
		public override void remove_oncontrolselect(HTMLTextContainerEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmltextContainerEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189BD RID: 100797 RVA: 0x000BF8AC File Offset: 0x000BE8AC
		public override void add_onmove(HTMLTextContainerEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189BE RID: 100798 RVA: 0x000BF93C File Offset: 0x000BE93C
		public override void remove_onmove(HTMLTextContainerEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmoveDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189BF RID: 100799 RVA: 0x000BFA2C File Offset: 0x000BEA2C
		public override void add_onbeforeactivate(HTMLTextContainerEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189C0 RID: 100800 RVA: 0x000BFABC File Offset: 0x000BEABC
		public override void remove_onbeforeactivate(HTMLTextContainerEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189C1 RID: 100801 RVA: 0x000BFBAC File Offset: 0x000BEBAC
		public override void add_onbeforedeactivate(HTMLTextContainerEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189C2 RID: 100802 RVA: 0x000BFC3C File Offset: 0x000BEC3C
		public override void remove_onbeforedeactivate(HTMLTextContainerEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189C3 RID: 100803 RVA: 0x000BFD2C File Offset: 0x000BED2C
		public override void add_onpage(HTMLTextContainerEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189C4 RID: 100804 RVA: 0x000BFDBC File Offset: 0x000BEDBC
		public override void remove_onpage(HTMLTextContainerEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onpageDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189C5 RID: 100805 RVA: 0x000BFEAC File Offset: 0x000BEEAC
		public override void add_onlayoutcomplete(HTMLTextContainerEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189C6 RID: 100806 RVA: 0x000BFF3C File Offset: 0x000BEF3C
		public override void remove_onlayoutcomplete(HTMLTextContainerEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189C7 RID: 100807 RVA: 0x000C002C File Offset: 0x000BF02C
		public override void add_onbeforeeditfocus(HTMLTextContainerEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189C8 RID: 100808 RVA: 0x000C00BC File Offset: 0x000BF0BC
		public override void remove_onbeforeeditfocus(HTMLTextContainerEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189C9 RID: 100809 RVA: 0x000C01AC File Offset: 0x000BF1AC
		public override void add_onreadystatechange(HTMLTextContainerEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189CA RID: 100810 RVA: 0x000C023C File Offset: 0x000BF23C
		public override void remove_onreadystatechange(HTMLTextContainerEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189CB RID: 100811 RVA: 0x000C032C File Offset: 0x000BF32C
		public override void add_oncellchange(HTMLTextContainerEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189CC RID: 100812 RVA: 0x000C03BC File Offset: 0x000BF3BC
		public override void remove_oncellchange(HTMLTextContainerEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189CD RID: 100813 RVA: 0x000C04AC File Offset: 0x000BF4AC
		public override void add_onrowsinserted(HTMLTextContainerEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189CE RID: 100814 RVA: 0x000C053C File Offset: 0x000BF53C
		public override void remove_onrowsinserted(HTMLTextContainerEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189CF RID: 100815 RVA: 0x000C062C File Offset: 0x000BF62C
		public override void add_onrowsdelete(HTMLTextContainerEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189D0 RID: 100816 RVA: 0x000C06BC File Offset: 0x000BF6BC
		public override void remove_onrowsdelete(HTMLTextContainerEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189D1 RID: 100817 RVA: 0x000C07AC File Offset: 0x000BF7AC
		public override void add_oncontextmenu(HTMLTextContainerEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189D2 RID: 100818 RVA: 0x000C083C File Offset: 0x000BF83C
		public override void remove_oncontextmenu(HTMLTextContainerEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmltextContainerEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189D3 RID: 100819 RVA: 0x000C092C File Offset: 0x000BF92C
		public override void add_onpaste(HTMLTextContainerEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189D4 RID: 100820 RVA: 0x000C09BC File Offset: 0x000BF9BC
		public override void remove_onpaste(HTMLTextContainerEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onpasteDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189D5 RID: 100821 RVA: 0x000C0AAC File Offset: 0x000BFAAC
		public override void add_onbeforepaste(HTMLTextContainerEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189D6 RID: 100822 RVA: 0x000C0B3C File Offset: 0x000BFB3C
		public override void remove_onbeforepaste(HTMLTextContainerEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189D7 RID: 100823 RVA: 0x000C0C2C File Offset: 0x000BFC2C
		public override void add_oncopy(HTMLTextContainerEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189D8 RID: 100824 RVA: 0x000C0CBC File Offset: 0x000BFCBC
		public override void remove_oncopy(HTMLTextContainerEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_oncopyDelegate != null && ((htmltextContainerEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189D9 RID: 100825 RVA: 0x000C0DAC File Offset: 0x000BFDAC
		public override void add_onbeforecopy(HTMLTextContainerEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189DA RID: 100826 RVA: 0x000C0E3C File Offset: 0x000BFE3C
		public override void remove_onbeforecopy(HTMLTextContainerEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189DB RID: 100827 RVA: 0x000C0F2C File Offset: 0x000BFF2C
		public override void add_oncut(HTMLTextContainerEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189DC RID: 100828 RVA: 0x000C0FBC File Offset: 0x000BFFBC
		public override void remove_oncut(HTMLTextContainerEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_oncutDelegate != null && ((htmltextContainerEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189DD RID: 100829 RVA: 0x000C10AC File Offset: 0x000C00AC
		public override void add_onbeforecut(HTMLTextContainerEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189DE RID: 100830 RVA: 0x000C113C File Offset: 0x000C013C
		public override void remove_onbeforecut(HTMLTextContainerEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189DF RID: 100831 RVA: 0x000C122C File Offset: 0x000C022C
		public override void add_ondrop(HTMLTextContainerEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189E0 RID: 100832 RVA: 0x000C12BC File Offset: 0x000C02BC
		public override void remove_ondrop(HTMLTextContainerEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondropDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189E1 RID: 100833 RVA: 0x000C13AC File Offset: 0x000C03AC
		public override void add_ondragleave(HTMLTextContainerEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189E2 RID: 100834 RVA: 0x000C143C File Offset: 0x000C043C
		public override void remove_ondragleave(HTMLTextContainerEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189E3 RID: 100835 RVA: 0x000C152C File Offset: 0x000C052C
		public override void add_ondragover(HTMLTextContainerEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189E4 RID: 100836 RVA: 0x000C15BC File Offset: 0x000C05BC
		public override void remove_ondragover(HTMLTextContainerEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragoverDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189E5 RID: 100837 RVA: 0x000C16AC File Offset: 0x000C06AC
		public override void add_ondragenter(HTMLTextContainerEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189E6 RID: 100838 RVA: 0x000C173C File Offset: 0x000C073C
		public override void remove_ondragenter(HTMLTextContainerEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragenterDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189E7 RID: 100839 RVA: 0x000C182C File Offset: 0x000C082C
		public override void add_ondragend(HTMLTextContainerEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189E8 RID: 100840 RVA: 0x000C18BC File Offset: 0x000C08BC
		public override void remove_ondragend(HTMLTextContainerEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragendDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189E9 RID: 100841 RVA: 0x000C19AC File Offset: 0x000C09AC
		public override void add_ondrag(HTMLTextContainerEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189EA RID: 100842 RVA: 0x000C1A3C File Offset: 0x000C0A3C
		public override void remove_ondrag(HTMLTextContainerEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189EB RID: 100843 RVA: 0x000C1B2C File Offset: 0x000C0B2C
		public override void add_onresize(HTMLTextContainerEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189EC RID: 100844 RVA: 0x000C1BBC File Offset: 0x000C0BBC
		public override void remove_onresize(HTMLTextContainerEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onresizeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189ED RID: 100845 RVA: 0x000C1CAC File Offset: 0x000C0CAC
		public override void add_onblur(HTMLTextContainerEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189EE RID: 100846 RVA: 0x000C1D3C File Offset: 0x000C0D3C
		public override void remove_onblur(HTMLTextContainerEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onblurDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189EF RID: 100847 RVA: 0x000C1E2C File Offset: 0x000C0E2C
		public override void add_onfocus(HTMLTextContainerEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189F0 RID: 100848 RVA: 0x000C1EBC File Offset: 0x000C0EBC
		public override void remove_onfocus(HTMLTextContainerEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onfocusDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189F1 RID: 100849 RVA: 0x000C1FAC File Offset: 0x000C0FAC
		public override void add_onscroll(HTMLTextContainerEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189F2 RID: 100850 RVA: 0x000C203C File Offset: 0x000C103C
		public override void remove_onscroll(HTMLTextContainerEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onscrollDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189F3 RID: 100851 RVA: 0x000C212C File Offset: 0x000C112C
		public override void add_onpropertychange(HTMLTextContainerEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189F4 RID: 100852 RVA: 0x000C21BC File Offset: 0x000C11BC
		public override void remove_onpropertychange(HTMLTextContainerEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189F5 RID: 100853 RVA: 0x000C22AC File Offset: 0x000C12AC
		public override void add_onlosecapture(HTMLTextContainerEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189F6 RID: 100854 RVA: 0x000C233C File Offset: 0x000C133C
		public override void remove_onlosecapture(HTMLTextContainerEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189F7 RID: 100855 RVA: 0x000C242C File Offset: 0x000C142C
		public override void add_ondatasetcomplete(HTMLTextContainerEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189F8 RID: 100856 RVA: 0x000C24BC File Offset: 0x000C14BC
		public override void remove_ondatasetcomplete(HTMLTextContainerEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189F9 RID: 100857 RVA: 0x000C25AC File Offset: 0x000C15AC
		public override void add_ondataavailable(HTMLTextContainerEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189FA RID: 100858 RVA: 0x000C263C File Offset: 0x000C163C
		public override void remove_ondataavailable(HTMLTextContainerEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189FB RID: 100859 RVA: 0x000C272C File Offset: 0x000C172C
		public override void add_ondatasetchanged(HTMLTextContainerEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189FC RID: 100860 RVA: 0x000C27BC File Offset: 0x000C17BC
		public override void remove_ondatasetchanged(HTMLTextContainerEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189FD RID: 100861 RVA: 0x000C28AC File Offset: 0x000C18AC
		public override void add_onrowenter(HTMLTextContainerEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x060189FE RID: 100862 RVA: 0x000C293C File Offset: 0x000C193C
		public override void remove_onrowenter(HTMLTextContainerEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onrowenterDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060189FF RID: 100863 RVA: 0x000C2A2C File Offset: 0x000C1A2C
		public override void add_onrowexit(HTMLTextContainerEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A00 RID: 100864 RVA: 0x000C2ABC File Offset: 0x000C1ABC
		public override void remove_onrowexit(HTMLTextContainerEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onrowexitDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A01 RID: 100865 RVA: 0x000C2BAC File Offset: 0x000C1BAC
		public override void add_onerrorupdate(HTMLTextContainerEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A02 RID: 100866 RVA: 0x000C2C3C File Offset: 0x000C1C3C
		public override void remove_onerrorupdate(HTMLTextContainerEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A03 RID: 100867 RVA: 0x000C2D2C File Offset: 0x000C1D2C
		public override void add_onafterupdate(HTMLTextContainerEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A04 RID: 100868 RVA: 0x000C2DBC File Offset: 0x000C1DBC
		public override void remove_onafterupdate(HTMLTextContainerEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A05 RID: 100869 RVA: 0x000C2EAC File Offset: 0x000C1EAC
		public override void add_onbeforeupdate(HTMLTextContainerEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A06 RID: 100870 RVA: 0x000C2F3C File Offset: 0x000C1F3C
		public override void remove_onbeforeupdate(HTMLTextContainerEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A07 RID: 100871 RVA: 0x000C302C File Offset: 0x000C202C
		public override void add_ondragstart(HTMLTextContainerEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A08 RID: 100872 RVA: 0x000C30BC File Offset: 0x000C20BC
		public override void remove_ondragstart(HTMLTextContainerEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondragstartDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A09 RID: 100873 RVA: 0x000C31AC File Offset: 0x000C21AC
		public override void add_onfilterchange(HTMLTextContainerEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A0A RID: 100874 RVA: 0x000C323C File Offset: 0x000C223C
		public override void remove_onfilterchange(HTMLTextContainerEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A0B RID: 100875 RVA: 0x000C332C File Offset: 0x000C232C
		public override void add_onselectstart(HTMLTextContainerEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A0C RID: 100876 RVA: 0x000C33BC File Offset: 0x000C23BC
		public override void remove_onselectstart(HTMLTextContainerEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onselectstartDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A0D RID: 100877 RVA: 0x000C34AC File Offset: 0x000C24AC
		public override void add_onmouseup(HTMLTextContainerEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A0E RID: 100878 RVA: 0x000C353C File Offset: 0x000C253C
		public override void remove_onmouseup(HTMLTextContainerEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmouseupDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A0F RID: 100879 RVA: 0x000C362C File Offset: 0x000C262C
		public override void add_onmousedown(HTMLTextContainerEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A10 RID: 100880 RVA: 0x000C36BC File Offset: 0x000C26BC
		public override void remove_onmousedown(HTMLTextContainerEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmousedownDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A11 RID: 100881 RVA: 0x000C37AC File Offset: 0x000C27AC
		public override void add_onmousemove(HTMLTextContainerEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A12 RID: 100882 RVA: 0x000C383C File Offset: 0x000C283C
		public override void remove_onmousemove(HTMLTextContainerEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A13 RID: 100883 RVA: 0x000C392C File Offset: 0x000C292C
		public override void add_onmouseover(HTMLTextContainerEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A14 RID: 100884 RVA: 0x000C39BC File Offset: 0x000C29BC
		public override void remove_onmouseover(HTMLTextContainerEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A15 RID: 100885 RVA: 0x000C3AAC File Offset: 0x000C2AAC
		public override void add_onmouseout(HTMLTextContainerEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A16 RID: 100886 RVA: 0x000C3B3C File Offset: 0x000C2B3C
		public override void remove_onmouseout(HTMLTextContainerEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A17 RID: 100887 RVA: 0x000C3C2C File Offset: 0x000C2C2C
		public override void add_onkeyup(HTMLTextContainerEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A18 RID: 100888 RVA: 0x000C3CBC File Offset: 0x000C2CBC
		public override void remove_onkeyup(HTMLTextContainerEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onkeyupDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A19 RID: 100889 RVA: 0x000C3DAC File Offset: 0x000C2DAC
		public override void add_onkeydown(HTMLTextContainerEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A1A RID: 100890 RVA: 0x000C3E3C File Offset: 0x000C2E3C
		public override void remove_onkeydown(HTMLTextContainerEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onkeydownDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A1B RID: 100891 RVA: 0x000C3F2C File Offset: 0x000C2F2C
		public override void add_onkeypress(HTMLTextContainerEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A1C RID: 100892 RVA: 0x000C3FBC File Offset: 0x000C2FBC
		public override void remove_onkeypress(HTMLTextContainerEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onkeypressDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A1D RID: 100893 RVA: 0x000C40AC File Offset: 0x000C30AC
		public override void add_ondblclick(HTMLTextContainerEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A1E RID: 100894 RVA: 0x000C413C File Offset: 0x000C313C
		public override void remove_ondblclick(HTMLTextContainerEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_ondblclickDelegate != null && ((htmltextContainerEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A1F RID: 100895 RVA: 0x000C422C File Offset: 0x000C322C
		public override void add_onclick(HTMLTextContainerEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A20 RID: 100896 RVA: 0x000C42BC File Offset: 0x000C32BC
		public override void remove_onclick(HTMLTextContainerEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onclickDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A21 RID: 100897 RVA: 0x000C43AC File Offset: 0x000C33AC
		public override void add_onhelp(HTMLTextContainerEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = new HTMLTextContainerEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltextContainerEvents_SinkHelper, ref num);
				htmltextContainerEvents_SinkHelper.m_dwCookie = num;
				htmltextContainerEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltextContainerEvents_SinkHelper);
			}
		}

		// Token: 0x06018A22 RID: 100898 RVA: 0x000C443C File Offset: 0x000C343C
		public override void remove_onhelp(HTMLTextContainerEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper;
					for (;;)
					{
						htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltextContainerEvents_SinkHelper.m_onhelpDelegate != null && ((htmltextContainerEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018A23 RID: 100899 RVA: 0x000C452C File Offset: 0x000C352C
		public HTMLTextContainerEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018A24 RID: 100900 RVA: 0x000C4554 File Offset: 0x000C3554
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
								HTMLTextContainerEvents_SinkHelper htmltextContainerEvents_SinkHelper = (HTMLTextContainerEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmltextContainerEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06018A25 RID: 100901 RVA: 0x000C4608 File Offset: 0x000C3608
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000C1F RID: 3103
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000C20 RID: 3104
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000C21 RID: 3105
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
