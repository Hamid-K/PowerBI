using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DDB RID: 3547
	internal sealed class HTMLFrameSiteEvents_EventProvider : HTMLFrameSiteEvents_Event, IDisposable
	{
		// Token: 0x06017E04 RID: 97796 RVA: 0x000545F0 File Offset: 0x000535F0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				0, 248, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017E05 RID: 97797 RVA: 0x00054704 File Offset: 0x00053704
		public override void add_onload(HTMLFrameSiteEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E06 RID: 97798 RVA: 0x00054794 File Offset: 0x00053794
		public override void remove_onload(HTMLFrameSiteEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onloadDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E07 RID: 97799 RVA: 0x00054884 File Offset: 0x00053884
		public override void add_onfocusout(HTMLFrameSiteEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E08 RID: 97800 RVA: 0x00054914 File Offset: 0x00053914
		public override void remove_onfocusout(HTMLFrameSiteEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E09 RID: 97801 RVA: 0x00054A04 File Offset: 0x00053A04
		public override void add_onfocusin(HTMLFrameSiteEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E0A RID: 97802 RVA: 0x00054A94 File Offset: 0x00053A94
		public override void remove_onfocusin(HTMLFrameSiteEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E0B RID: 97803 RVA: 0x00054B84 File Offset: 0x00053B84
		public override void add_ondeactivate(HTMLFrameSiteEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E0C RID: 97804 RVA: 0x00054C14 File Offset: 0x00053C14
		public override void remove_ondeactivate(HTMLFrameSiteEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E0D RID: 97805 RVA: 0x00054D04 File Offset: 0x00053D04
		public override void add_onactivate(HTMLFrameSiteEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E0E RID: 97806 RVA: 0x00054D94 File Offset: 0x00053D94
		public override void remove_onactivate(HTMLFrameSiteEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onactivateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E0F RID: 97807 RVA: 0x00054E84 File Offset: 0x00053E84
		public override void add_onmousewheel(HTMLFrameSiteEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E10 RID: 97808 RVA: 0x00054F14 File Offset: 0x00053F14
		public override void remove_onmousewheel(HTMLFrameSiteEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E11 RID: 97809 RVA: 0x00055004 File Offset: 0x00054004
		public override void add_onmouseleave(HTMLFrameSiteEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E12 RID: 97810 RVA: 0x00055094 File Offset: 0x00054094
		public override void remove_onmouseleave(HTMLFrameSiteEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E13 RID: 97811 RVA: 0x00055184 File Offset: 0x00054184
		public override void add_onmouseenter(HTMLFrameSiteEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E14 RID: 97812 RVA: 0x00055214 File Offset: 0x00054214
		public override void remove_onmouseenter(HTMLFrameSiteEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E15 RID: 97813 RVA: 0x00055304 File Offset: 0x00054304
		public override void add_onresizeend(HTMLFrameSiteEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E16 RID: 97814 RVA: 0x00055394 File Offset: 0x00054394
		public override void remove_onresizeend(HTMLFrameSiteEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E17 RID: 97815 RVA: 0x00055484 File Offset: 0x00054484
		public override void add_onresizestart(HTMLFrameSiteEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E18 RID: 97816 RVA: 0x00055514 File Offset: 0x00054514
		public override void remove_onresizestart(HTMLFrameSiteEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E19 RID: 97817 RVA: 0x00055604 File Offset: 0x00054604
		public override void add_onmoveend(HTMLFrameSiteEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E1A RID: 97818 RVA: 0x00055694 File Offset: 0x00054694
		public override void remove_onmoveend(HTMLFrameSiteEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E1B RID: 97819 RVA: 0x00055784 File Offset: 0x00054784
		public override void add_onmovestart(HTMLFrameSiteEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E1C RID: 97820 RVA: 0x00055814 File Offset: 0x00054814
		public override void remove_onmovestart(HTMLFrameSiteEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E1D RID: 97821 RVA: 0x00055904 File Offset: 0x00054904
		public override void add_oncontrolselect(HTMLFrameSiteEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E1E RID: 97822 RVA: 0x00055994 File Offset: 0x00054994
		public override void remove_oncontrolselect(HTMLFrameSiteEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E1F RID: 97823 RVA: 0x00055A84 File Offset: 0x00054A84
		public override void add_onmove(HTMLFrameSiteEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E20 RID: 97824 RVA: 0x00055B14 File Offset: 0x00054B14
		public override void remove_onmove(HTMLFrameSiteEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmoveDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E21 RID: 97825 RVA: 0x00055C04 File Offset: 0x00054C04
		public override void add_onbeforeactivate(HTMLFrameSiteEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E22 RID: 97826 RVA: 0x00055C94 File Offset: 0x00054C94
		public override void remove_onbeforeactivate(HTMLFrameSiteEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E23 RID: 97827 RVA: 0x00055D84 File Offset: 0x00054D84
		public override void add_onbeforedeactivate(HTMLFrameSiteEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E24 RID: 97828 RVA: 0x00055E14 File Offset: 0x00054E14
		public override void remove_onbeforedeactivate(HTMLFrameSiteEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E25 RID: 97829 RVA: 0x00055F04 File Offset: 0x00054F04
		public override void add_onpage(HTMLFrameSiteEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E26 RID: 97830 RVA: 0x00055F94 File Offset: 0x00054F94
		public override void remove_onpage(HTMLFrameSiteEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onpageDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E27 RID: 97831 RVA: 0x00056084 File Offset: 0x00055084
		public override void add_onlayoutcomplete(HTMLFrameSiteEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E28 RID: 97832 RVA: 0x00056114 File Offset: 0x00055114
		public override void remove_onlayoutcomplete(HTMLFrameSiteEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E29 RID: 97833 RVA: 0x00056204 File Offset: 0x00055204
		public override void add_onbeforeeditfocus(HTMLFrameSiteEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E2A RID: 97834 RVA: 0x00056294 File Offset: 0x00055294
		public override void remove_onbeforeeditfocus(HTMLFrameSiteEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E2B RID: 97835 RVA: 0x00056384 File Offset: 0x00055384
		public override void add_onreadystatechange(HTMLFrameSiteEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E2C RID: 97836 RVA: 0x00056414 File Offset: 0x00055414
		public override void remove_onreadystatechange(HTMLFrameSiteEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E2D RID: 97837 RVA: 0x00056504 File Offset: 0x00055504
		public override void add_oncellchange(HTMLFrameSiteEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E2E RID: 97838 RVA: 0x00056594 File Offset: 0x00055594
		public override void remove_oncellchange(HTMLFrameSiteEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E2F RID: 97839 RVA: 0x00056684 File Offset: 0x00055684
		public override void add_onrowsinserted(HTMLFrameSiteEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E30 RID: 97840 RVA: 0x00056714 File Offset: 0x00055714
		public override void remove_onrowsinserted(HTMLFrameSiteEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E31 RID: 97841 RVA: 0x00056804 File Offset: 0x00055804
		public override void add_onrowsdelete(HTMLFrameSiteEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E32 RID: 97842 RVA: 0x00056894 File Offset: 0x00055894
		public override void remove_onrowsdelete(HTMLFrameSiteEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E33 RID: 97843 RVA: 0x00056984 File Offset: 0x00055984
		public override void add_oncontextmenu(HTMLFrameSiteEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E34 RID: 97844 RVA: 0x00056A14 File Offset: 0x00055A14
		public override void remove_oncontextmenu(HTMLFrameSiteEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E35 RID: 97845 RVA: 0x00056B04 File Offset: 0x00055B04
		public override void add_onpaste(HTMLFrameSiteEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E36 RID: 97846 RVA: 0x00056B94 File Offset: 0x00055B94
		public override void remove_onpaste(HTMLFrameSiteEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onpasteDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E37 RID: 97847 RVA: 0x00056C84 File Offset: 0x00055C84
		public override void add_onbeforepaste(HTMLFrameSiteEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E38 RID: 97848 RVA: 0x00056D14 File Offset: 0x00055D14
		public override void remove_onbeforepaste(HTMLFrameSiteEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E39 RID: 97849 RVA: 0x00056E04 File Offset: 0x00055E04
		public override void add_oncopy(HTMLFrameSiteEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E3A RID: 97850 RVA: 0x00056E94 File Offset: 0x00055E94
		public override void remove_oncopy(HTMLFrameSiteEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_oncopyDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E3B RID: 97851 RVA: 0x00056F84 File Offset: 0x00055F84
		public override void add_onbeforecopy(HTMLFrameSiteEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E3C RID: 97852 RVA: 0x00057014 File Offset: 0x00056014
		public override void remove_onbeforecopy(HTMLFrameSiteEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E3D RID: 97853 RVA: 0x00057104 File Offset: 0x00056104
		public override void add_oncut(HTMLFrameSiteEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E3E RID: 97854 RVA: 0x00057194 File Offset: 0x00056194
		public override void remove_oncut(HTMLFrameSiteEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_oncutDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E3F RID: 97855 RVA: 0x00057284 File Offset: 0x00056284
		public override void add_onbeforecut(HTMLFrameSiteEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E40 RID: 97856 RVA: 0x00057314 File Offset: 0x00056314
		public override void remove_onbeforecut(HTMLFrameSiteEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E41 RID: 97857 RVA: 0x00057404 File Offset: 0x00056404
		public override void add_ondrop(HTMLFrameSiteEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E42 RID: 97858 RVA: 0x00057494 File Offset: 0x00056494
		public override void remove_ondrop(HTMLFrameSiteEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondropDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E43 RID: 97859 RVA: 0x00057584 File Offset: 0x00056584
		public override void add_ondragleave(HTMLFrameSiteEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E44 RID: 97860 RVA: 0x00057614 File Offset: 0x00056614
		public override void remove_ondragleave(HTMLFrameSiteEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E45 RID: 97861 RVA: 0x00057704 File Offset: 0x00056704
		public override void add_ondragover(HTMLFrameSiteEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E46 RID: 97862 RVA: 0x00057794 File Offset: 0x00056794
		public override void remove_ondragover(HTMLFrameSiteEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E47 RID: 97863 RVA: 0x00057884 File Offset: 0x00056884
		public override void add_ondragenter(HTMLFrameSiteEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E48 RID: 97864 RVA: 0x00057914 File Offset: 0x00056914
		public override void remove_ondragenter(HTMLFrameSiteEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E49 RID: 97865 RVA: 0x00057A04 File Offset: 0x00056A04
		public override void add_ondragend(HTMLFrameSiteEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E4A RID: 97866 RVA: 0x00057A94 File Offset: 0x00056A94
		public override void remove_ondragend(HTMLFrameSiteEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragendDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E4B RID: 97867 RVA: 0x00057B84 File Offset: 0x00056B84
		public override void add_ondrag(HTMLFrameSiteEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E4C RID: 97868 RVA: 0x00057C14 File Offset: 0x00056C14
		public override void remove_ondrag(HTMLFrameSiteEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E4D RID: 97869 RVA: 0x00057D04 File Offset: 0x00056D04
		public override void add_onresize(HTMLFrameSiteEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E4E RID: 97870 RVA: 0x00057D94 File Offset: 0x00056D94
		public override void remove_onresize(HTMLFrameSiteEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onresizeDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E4F RID: 97871 RVA: 0x00057E84 File Offset: 0x00056E84
		public override void add_onblur(HTMLFrameSiteEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E50 RID: 97872 RVA: 0x00057F14 File Offset: 0x00056F14
		public override void remove_onblur(HTMLFrameSiteEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onblurDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E51 RID: 97873 RVA: 0x00058004 File Offset: 0x00057004
		public override void add_onfocus(HTMLFrameSiteEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E52 RID: 97874 RVA: 0x00058094 File Offset: 0x00057094
		public override void remove_onfocus(HTMLFrameSiteEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onfocusDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E53 RID: 97875 RVA: 0x00058184 File Offset: 0x00057184
		public override void add_onscroll(HTMLFrameSiteEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E54 RID: 97876 RVA: 0x00058214 File Offset: 0x00057214
		public override void remove_onscroll(HTMLFrameSiteEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onscrollDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E55 RID: 97877 RVA: 0x00058304 File Offset: 0x00057304
		public override void add_onpropertychange(HTMLFrameSiteEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E56 RID: 97878 RVA: 0x00058394 File Offset: 0x00057394
		public override void remove_onpropertychange(HTMLFrameSiteEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E57 RID: 97879 RVA: 0x00058484 File Offset: 0x00057484
		public override void add_onlosecapture(HTMLFrameSiteEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E58 RID: 97880 RVA: 0x00058514 File Offset: 0x00057514
		public override void remove_onlosecapture(HTMLFrameSiteEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E59 RID: 97881 RVA: 0x00058604 File Offset: 0x00057604
		public override void add_ondatasetcomplete(HTMLFrameSiteEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E5A RID: 97882 RVA: 0x00058694 File Offset: 0x00057694
		public override void remove_ondatasetcomplete(HTMLFrameSiteEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E5B RID: 97883 RVA: 0x00058784 File Offset: 0x00057784
		public override void add_ondataavailable(HTMLFrameSiteEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E5C RID: 97884 RVA: 0x00058814 File Offset: 0x00057814
		public override void remove_ondataavailable(HTMLFrameSiteEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E5D RID: 97885 RVA: 0x00058904 File Offset: 0x00057904
		public override void add_ondatasetchanged(HTMLFrameSiteEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E5E RID: 97886 RVA: 0x00058994 File Offset: 0x00057994
		public override void remove_ondatasetchanged(HTMLFrameSiteEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E5F RID: 97887 RVA: 0x00058A84 File Offset: 0x00057A84
		public override void add_onrowenter(HTMLFrameSiteEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E60 RID: 97888 RVA: 0x00058B14 File Offset: 0x00057B14
		public override void remove_onrowenter(HTMLFrameSiteEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E61 RID: 97889 RVA: 0x00058C04 File Offset: 0x00057C04
		public override void add_onrowexit(HTMLFrameSiteEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E62 RID: 97890 RVA: 0x00058C94 File Offset: 0x00057C94
		public override void remove_onrowexit(HTMLFrameSiteEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E63 RID: 97891 RVA: 0x00058D84 File Offset: 0x00057D84
		public override void add_onerrorupdate(HTMLFrameSiteEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E64 RID: 97892 RVA: 0x00058E14 File Offset: 0x00057E14
		public override void remove_onerrorupdate(HTMLFrameSiteEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E65 RID: 97893 RVA: 0x00058F04 File Offset: 0x00057F04
		public override void add_onafterupdate(HTMLFrameSiteEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E66 RID: 97894 RVA: 0x00058F94 File Offset: 0x00057F94
		public override void remove_onafterupdate(HTMLFrameSiteEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E67 RID: 97895 RVA: 0x00059084 File Offset: 0x00058084
		public override void add_onbeforeupdate(HTMLFrameSiteEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E68 RID: 97896 RVA: 0x00059114 File Offset: 0x00058114
		public override void remove_onbeforeupdate(HTMLFrameSiteEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E69 RID: 97897 RVA: 0x00059204 File Offset: 0x00058204
		public override void add_ondragstart(HTMLFrameSiteEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E6A RID: 97898 RVA: 0x00059294 File Offset: 0x00058294
		public override void remove_ondragstart(HTMLFrameSiteEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E6B RID: 97899 RVA: 0x00059384 File Offset: 0x00058384
		public override void add_onfilterchange(HTMLFrameSiteEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E6C RID: 97900 RVA: 0x00059414 File Offset: 0x00058414
		public override void remove_onfilterchange(HTMLFrameSiteEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E6D RID: 97901 RVA: 0x00059504 File Offset: 0x00058504
		public override void add_onselectstart(HTMLFrameSiteEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E6E RID: 97902 RVA: 0x00059594 File Offset: 0x00058594
		public override void remove_onselectstart(HTMLFrameSiteEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E6F RID: 97903 RVA: 0x00059684 File Offset: 0x00058684
		public override void add_onmouseup(HTMLFrameSiteEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E70 RID: 97904 RVA: 0x00059714 File Offset: 0x00058714
		public override void remove_onmouseup(HTMLFrameSiteEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E71 RID: 97905 RVA: 0x00059804 File Offset: 0x00058804
		public override void add_onmousedown(HTMLFrameSiteEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E72 RID: 97906 RVA: 0x00059894 File Offset: 0x00058894
		public override void remove_onmousedown(HTMLFrameSiteEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E73 RID: 97907 RVA: 0x00059984 File Offset: 0x00058984
		public override void add_onmousemove(HTMLFrameSiteEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E74 RID: 97908 RVA: 0x00059A14 File Offset: 0x00058A14
		public override void remove_onmousemove(HTMLFrameSiteEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E75 RID: 97909 RVA: 0x00059B04 File Offset: 0x00058B04
		public override void add_onmouseover(HTMLFrameSiteEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E76 RID: 97910 RVA: 0x00059B94 File Offset: 0x00058B94
		public override void remove_onmouseover(HTMLFrameSiteEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E77 RID: 97911 RVA: 0x00059C84 File Offset: 0x00058C84
		public override void add_onmouseout(HTMLFrameSiteEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E78 RID: 97912 RVA: 0x00059D14 File Offset: 0x00058D14
		public override void remove_onmouseout(HTMLFrameSiteEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E79 RID: 97913 RVA: 0x00059E04 File Offset: 0x00058E04
		public override void add_onkeyup(HTMLFrameSiteEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E7A RID: 97914 RVA: 0x00059E94 File Offset: 0x00058E94
		public override void remove_onkeyup(HTMLFrameSiteEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E7B RID: 97915 RVA: 0x00059F84 File Offset: 0x00058F84
		public override void add_onkeydown(HTMLFrameSiteEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E7C RID: 97916 RVA: 0x0005A014 File Offset: 0x00059014
		public override void remove_onkeydown(HTMLFrameSiteEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E7D RID: 97917 RVA: 0x0005A104 File Offset: 0x00059104
		public override void add_onkeypress(HTMLFrameSiteEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E7E RID: 97918 RVA: 0x0005A194 File Offset: 0x00059194
		public override void remove_onkeypress(HTMLFrameSiteEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E7F RID: 97919 RVA: 0x0005A284 File Offset: 0x00059284
		public override void add_ondblclick(HTMLFrameSiteEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E80 RID: 97920 RVA: 0x0005A314 File Offset: 0x00059314
		public override void remove_ondblclick(HTMLFrameSiteEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E81 RID: 97921 RVA: 0x0005A404 File Offset: 0x00059404
		public override void add_onclick(HTMLFrameSiteEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E82 RID: 97922 RVA: 0x0005A494 File Offset: 0x00059494
		public override void remove_onclick(HTMLFrameSiteEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onclickDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E83 RID: 97923 RVA: 0x0005A584 File Offset: 0x00059584
		public override void add_onhelp(HTMLFrameSiteEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = new HTMLFrameSiteEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlframeSiteEvents_SinkHelper, ref num);
				htmlframeSiteEvents_SinkHelper.m_dwCookie = num;
				htmlframeSiteEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlframeSiteEvents_SinkHelper);
			}
		}

		// Token: 0x06017E84 RID: 97924 RVA: 0x0005A614 File Offset: 0x00059614
		public override void remove_onhelp(HTMLFrameSiteEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper;
					for (;;)
					{
						htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlframeSiteEvents_SinkHelper.m_onhelpDelegate != null && ((htmlframeSiteEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017E85 RID: 97925 RVA: 0x0005A704 File Offset: 0x00059704
		public HTMLFrameSiteEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017E86 RID: 97926 RVA: 0x0005A72C File Offset: 0x0005972C
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
								HTMLFrameSiteEvents_SinkHelper htmlframeSiteEvents_SinkHelper = (HTMLFrameSiteEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlframeSiteEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017E87 RID: 97927 RVA: 0x0005A7E0 File Offset: 0x000597E0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000818 RID: 2072
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000819 RID: 2073
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400081A RID: 2074
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
