using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF9 RID: 3577
	internal sealed class HTMLAnchorEvents_EventProvider : HTMLAnchorEvents_Event, IDisposable
	{
		// Token: 0x0601881D RID: 100381 RVA: 0x000B06EC File Offset: 0x000AF6EC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				157, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601881E RID: 100382 RVA: 0x000B0800 File Offset: 0x000AF800
		public override void add_onfocusout(HTMLAnchorEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601881F RID: 100383 RVA: 0x000B0890 File Offset: 0x000AF890
		public override void remove_onfocusout(HTMLAnchorEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlanchorEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018820 RID: 100384 RVA: 0x000B0980 File Offset: 0x000AF980
		public override void add_onfocusin(HTMLAnchorEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018821 RID: 100385 RVA: 0x000B0A10 File Offset: 0x000AFA10
		public override void remove_onfocusin(HTMLAnchorEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlanchorEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018822 RID: 100386 RVA: 0x000B0B00 File Offset: 0x000AFB00
		public override void add_ondeactivate(HTMLAnchorEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018823 RID: 100387 RVA: 0x000B0B90 File Offset: 0x000AFB90
		public override void remove_ondeactivate(HTMLAnchorEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018824 RID: 100388 RVA: 0x000B0C80 File Offset: 0x000AFC80
		public override void add_onactivate(HTMLAnchorEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018825 RID: 100389 RVA: 0x000B0D10 File Offset: 0x000AFD10
		public override void remove_onactivate(HTMLAnchorEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onactivateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018826 RID: 100390 RVA: 0x000B0E00 File Offset: 0x000AFE00
		public override void add_onmousewheel(HTMLAnchorEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018827 RID: 100391 RVA: 0x000B0E90 File Offset: 0x000AFE90
		public override void remove_onmousewheel(HTMLAnchorEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018828 RID: 100392 RVA: 0x000B0F80 File Offset: 0x000AFF80
		public override void add_onmouseleave(HTMLAnchorEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018829 RID: 100393 RVA: 0x000B1010 File Offset: 0x000B0010
		public override void remove_onmouseleave(HTMLAnchorEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601882A RID: 100394 RVA: 0x000B1100 File Offset: 0x000B0100
		public override void add_onmouseenter(HTMLAnchorEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601882B RID: 100395 RVA: 0x000B1190 File Offset: 0x000B0190
		public override void remove_onmouseenter(HTMLAnchorEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601882C RID: 100396 RVA: 0x000B1280 File Offset: 0x000B0280
		public override void add_onresizeend(HTMLAnchorEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601882D RID: 100397 RVA: 0x000B1310 File Offset: 0x000B0310
		public override void remove_onresizeend(HTMLAnchorEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlanchorEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601882E RID: 100398 RVA: 0x000B1400 File Offset: 0x000B0400
		public override void add_onresizestart(HTMLAnchorEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601882F RID: 100399 RVA: 0x000B1490 File Offset: 0x000B0490
		public override void remove_onresizestart(HTMLAnchorEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlanchorEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018830 RID: 100400 RVA: 0x000B1580 File Offset: 0x000B0580
		public override void add_onmoveend(HTMLAnchorEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018831 RID: 100401 RVA: 0x000B1610 File Offset: 0x000B0610
		public override void remove_onmoveend(HTMLAnchorEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018832 RID: 100402 RVA: 0x000B1700 File Offset: 0x000B0700
		public override void add_onmovestart(HTMLAnchorEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018833 RID: 100403 RVA: 0x000B1790 File Offset: 0x000B0790
		public override void remove_onmovestart(HTMLAnchorEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018834 RID: 100404 RVA: 0x000B1880 File Offset: 0x000B0880
		public override void add_oncontrolselect(HTMLAnchorEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018835 RID: 100405 RVA: 0x000B1910 File Offset: 0x000B0910
		public override void remove_oncontrolselect(HTMLAnchorEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlanchorEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018836 RID: 100406 RVA: 0x000B1A00 File Offset: 0x000B0A00
		public override void add_onmove(HTMLAnchorEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018837 RID: 100407 RVA: 0x000B1A90 File Offset: 0x000B0A90
		public override void remove_onmove(HTMLAnchorEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmoveDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018838 RID: 100408 RVA: 0x000B1B80 File Offset: 0x000B0B80
		public override void add_onbeforeactivate(HTMLAnchorEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018839 RID: 100409 RVA: 0x000B1C10 File Offset: 0x000B0C10
		public override void remove_onbeforeactivate(HTMLAnchorEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601883A RID: 100410 RVA: 0x000B1D00 File Offset: 0x000B0D00
		public override void add_onbeforedeactivate(HTMLAnchorEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601883B RID: 100411 RVA: 0x000B1D90 File Offset: 0x000B0D90
		public override void remove_onbeforedeactivate(HTMLAnchorEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601883C RID: 100412 RVA: 0x000B1E80 File Offset: 0x000B0E80
		public override void add_onpage(HTMLAnchorEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601883D RID: 100413 RVA: 0x000B1F10 File Offset: 0x000B0F10
		public override void remove_onpage(HTMLAnchorEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onpageDelegate != null && ((htmlanchorEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601883E RID: 100414 RVA: 0x000B2000 File Offset: 0x000B1000
		public override void add_onlayoutcomplete(HTMLAnchorEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601883F RID: 100415 RVA: 0x000B2090 File Offset: 0x000B1090
		public override void remove_onlayoutcomplete(HTMLAnchorEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlanchorEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018840 RID: 100416 RVA: 0x000B2180 File Offset: 0x000B1180
		public override void add_onbeforeeditfocus(HTMLAnchorEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018841 RID: 100417 RVA: 0x000B2210 File Offset: 0x000B1210
		public override void remove_onbeforeeditfocus(HTMLAnchorEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018842 RID: 100418 RVA: 0x000B2300 File Offset: 0x000B1300
		public override void add_onreadystatechange(HTMLAnchorEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018843 RID: 100419 RVA: 0x000B2390 File Offset: 0x000B1390
		public override void remove_onreadystatechange(HTMLAnchorEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlanchorEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018844 RID: 100420 RVA: 0x000B2480 File Offset: 0x000B1480
		public override void add_oncellchange(HTMLAnchorEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018845 RID: 100421 RVA: 0x000B2510 File Offset: 0x000B1510
		public override void remove_oncellchange(HTMLAnchorEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlanchorEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018846 RID: 100422 RVA: 0x000B2600 File Offset: 0x000B1600
		public override void add_onrowsinserted(HTMLAnchorEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018847 RID: 100423 RVA: 0x000B2690 File Offset: 0x000B1690
		public override void remove_onrowsinserted(HTMLAnchorEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlanchorEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018848 RID: 100424 RVA: 0x000B2780 File Offset: 0x000B1780
		public override void add_onrowsdelete(HTMLAnchorEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018849 RID: 100425 RVA: 0x000B2810 File Offset: 0x000B1810
		public override void remove_onrowsdelete(HTMLAnchorEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlanchorEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601884A RID: 100426 RVA: 0x000B2900 File Offset: 0x000B1900
		public override void add_oncontextmenu(HTMLAnchorEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601884B RID: 100427 RVA: 0x000B2990 File Offset: 0x000B1990
		public override void remove_oncontextmenu(HTMLAnchorEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlanchorEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601884C RID: 100428 RVA: 0x000B2A80 File Offset: 0x000B1A80
		public override void add_onpaste(HTMLAnchorEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601884D RID: 100429 RVA: 0x000B2B10 File Offset: 0x000B1B10
		public override void remove_onpaste(HTMLAnchorEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onpasteDelegate != null && ((htmlanchorEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601884E RID: 100430 RVA: 0x000B2C00 File Offset: 0x000B1C00
		public override void add_onbeforepaste(HTMLAnchorEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601884F RID: 100431 RVA: 0x000B2C90 File Offset: 0x000B1C90
		public override void remove_onbeforepaste(HTMLAnchorEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018850 RID: 100432 RVA: 0x000B2D80 File Offset: 0x000B1D80
		public override void add_oncopy(HTMLAnchorEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018851 RID: 100433 RVA: 0x000B2E10 File Offset: 0x000B1E10
		public override void remove_oncopy(HTMLAnchorEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_oncopyDelegate != null && ((htmlanchorEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018852 RID: 100434 RVA: 0x000B2F00 File Offset: 0x000B1F00
		public override void add_onbeforecopy(HTMLAnchorEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018853 RID: 100435 RVA: 0x000B2F90 File Offset: 0x000B1F90
		public override void remove_onbeforecopy(HTMLAnchorEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018854 RID: 100436 RVA: 0x000B3080 File Offset: 0x000B2080
		public override void add_oncut(HTMLAnchorEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018855 RID: 100437 RVA: 0x000B3110 File Offset: 0x000B2110
		public override void remove_oncut(HTMLAnchorEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_oncutDelegate != null && ((htmlanchorEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018856 RID: 100438 RVA: 0x000B3200 File Offset: 0x000B2200
		public override void add_onbeforecut(HTMLAnchorEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018857 RID: 100439 RVA: 0x000B3290 File Offset: 0x000B2290
		public override void remove_onbeforecut(HTMLAnchorEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018858 RID: 100440 RVA: 0x000B3380 File Offset: 0x000B2380
		public override void add_ondrop(HTMLAnchorEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018859 RID: 100441 RVA: 0x000B3410 File Offset: 0x000B2410
		public override void remove_ondrop(HTMLAnchorEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondropDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601885A RID: 100442 RVA: 0x000B3500 File Offset: 0x000B2500
		public override void add_ondragleave(HTMLAnchorEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601885B RID: 100443 RVA: 0x000B3590 File Offset: 0x000B2590
		public override void remove_ondragleave(HTMLAnchorEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601885C RID: 100444 RVA: 0x000B3680 File Offset: 0x000B2680
		public override void add_ondragover(HTMLAnchorEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601885D RID: 100445 RVA: 0x000B3710 File Offset: 0x000B2710
		public override void remove_ondragover(HTMLAnchorEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601885E RID: 100446 RVA: 0x000B3800 File Offset: 0x000B2800
		public override void add_ondragenter(HTMLAnchorEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601885F RID: 100447 RVA: 0x000B3890 File Offset: 0x000B2890
		public override void remove_ondragenter(HTMLAnchorEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018860 RID: 100448 RVA: 0x000B3980 File Offset: 0x000B2980
		public override void add_ondragend(HTMLAnchorEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018861 RID: 100449 RVA: 0x000B3A10 File Offset: 0x000B2A10
		public override void remove_ondragend(HTMLAnchorEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragendDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018862 RID: 100450 RVA: 0x000B3B00 File Offset: 0x000B2B00
		public override void add_ondrag(HTMLAnchorEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018863 RID: 100451 RVA: 0x000B3B90 File Offset: 0x000B2B90
		public override void remove_ondrag(HTMLAnchorEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018864 RID: 100452 RVA: 0x000B3C80 File Offset: 0x000B2C80
		public override void add_onresize(HTMLAnchorEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018865 RID: 100453 RVA: 0x000B3D10 File Offset: 0x000B2D10
		public override void remove_onresize(HTMLAnchorEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onresizeDelegate != null && ((htmlanchorEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018866 RID: 100454 RVA: 0x000B3E00 File Offset: 0x000B2E00
		public override void add_onblur(HTMLAnchorEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018867 RID: 100455 RVA: 0x000B3E90 File Offset: 0x000B2E90
		public override void remove_onblur(HTMLAnchorEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onblurDelegate != null && ((htmlanchorEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018868 RID: 100456 RVA: 0x000B3F80 File Offset: 0x000B2F80
		public override void add_onfocus(HTMLAnchorEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018869 RID: 100457 RVA: 0x000B4010 File Offset: 0x000B3010
		public override void remove_onfocus(HTMLAnchorEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onfocusDelegate != null && ((htmlanchorEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601886A RID: 100458 RVA: 0x000B4100 File Offset: 0x000B3100
		public override void add_onscroll(HTMLAnchorEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601886B RID: 100459 RVA: 0x000B4190 File Offset: 0x000B3190
		public override void remove_onscroll(HTMLAnchorEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onscrollDelegate != null && ((htmlanchorEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601886C RID: 100460 RVA: 0x000B4280 File Offset: 0x000B3280
		public override void add_onpropertychange(HTMLAnchorEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601886D RID: 100461 RVA: 0x000B4310 File Offset: 0x000B3310
		public override void remove_onpropertychange(HTMLAnchorEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlanchorEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601886E RID: 100462 RVA: 0x000B4400 File Offset: 0x000B3400
		public override void add_onlosecapture(HTMLAnchorEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601886F RID: 100463 RVA: 0x000B4490 File Offset: 0x000B3490
		public override void remove_onlosecapture(HTMLAnchorEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlanchorEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018870 RID: 100464 RVA: 0x000B4580 File Offset: 0x000B3580
		public override void add_ondatasetcomplete(HTMLAnchorEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018871 RID: 100465 RVA: 0x000B4610 File Offset: 0x000B3610
		public override void remove_ondatasetcomplete(HTMLAnchorEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018872 RID: 100466 RVA: 0x000B4700 File Offset: 0x000B3700
		public override void add_ondataavailable(HTMLAnchorEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018873 RID: 100467 RVA: 0x000B4790 File Offset: 0x000B3790
		public override void remove_ondataavailable(HTMLAnchorEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018874 RID: 100468 RVA: 0x000B4880 File Offset: 0x000B3880
		public override void add_ondatasetchanged(HTMLAnchorEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018875 RID: 100469 RVA: 0x000B4910 File Offset: 0x000B3910
		public override void remove_ondatasetchanged(HTMLAnchorEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018876 RID: 100470 RVA: 0x000B4A00 File Offset: 0x000B3A00
		public override void add_onrowenter(HTMLAnchorEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018877 RID: 100471 RVA: 0x000B4A90 File Offset: 0x000B3A90
		public override void remove_onrowenter(HTMLAnchorEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlanchorEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018878 RID: 100472 RVA: 0x000B4B80 File Offset: 0x000B3B80
		public override void add_onrowexit(HTMLAnchorEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018879 RID: 100473 RVA: 0x000B4C10 File Offset: 0x000B3C10
		public override void remove_onrowexit(HTMLAnchorEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlanchorEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601887A RID: 100474 RVA: 0x000B4D00 File Offset: 0x000B3D00
		public override void add_onerrorupdate(HTMLAnchorEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601887B RID: 100475 RVA: 0x000B4D90 File Offset: 0x000B3D90
		public override void remove_onerrorupdate(HTMLAnchorEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601887C RID: 100476 RVA: 0x000B4E80 File Offset: 0x000B3E80
		public override void add_onafterupdate(HTMLAnchorEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601887D RID: 100477 RVA: 0x000B4F10 File Offset: 0x000B3F10
		public override void remove_onafterupdate(HTMLAnchorEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601887E RID: 100478 RVA: 0x000B5000 File Offset: 0x000B4000
		public override void add_onbeforeupdate(HTMLAnchorEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601887F RID: 100479 RVA: 0x000B5090 File Offset: 0x000B4090
		public override void remove_onbeforeupdate(HTMLAnchorEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlanchorEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018880 RID: 100480 RVA: 0x000B5180 File Offset: 0x000B4180
		public override void add_ondragstart(HTMLAnchorEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018881 RID: 100481 RVA: 0x000B5210 File Offset: 0x000B4210
		public override void remove_ondragstart(HTMLAnchorEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018882 RID: 100482 RVA: 0x000B5300 File Offset: 0x000B4300
		public override void add_onfilterchange(HTMLAnchorEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018883 RID: 100483 RVA: 0x000B5390 File Offset: 0x000B4390
		public override void remove_onfilterchange(HTMLAnchorEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlanchorEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018884 RID: 100484 RVA: 0x000B5480 File Offset: 0x000B4480
		public override void add_onselectstart(HTMLAnchorEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018885 RID: 100485 RVA: 0x000B5510 File Offset: 0x000B4510
		public override void remove_onselectstart(HTMLAnchorEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlanchorEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018886 RID: 100486 RVA: 0x000B5600 File Offset: 0x000B4600
		public override void add_onmouseup(HTMLAnchorEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018887 RID: 100487 RVA: 0x000B5690 File Offset: 0x000B4690
		public override void remove_onmouseup(HTMLAnchorEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018888 RID: 100488 RVA: 0x000B5780 File Offset: 0x000B4780
		public override void add_onmousedown(HTMLAnchorEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018889 RID: 100489 RVA: 0x000B5810 File Offset: 0x000B4810
		public override void remove_onmousedown(HTMLAnchorEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601888A RID: 100490 RVA: 0x000B5900 File Offset: 0x000B4900
		public override void add_onmousemove(HTMLAnchorEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601888B RID: 100491 RVA: 0x000B5990 File Offset: 0x000B4990
		public override void remove_onmousemove(HTMLAnchorEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601888C RID: 100492 RVA: 0x000B5A80 File Offset: 0x000B4A80
		public override void add_onmouseover(HTMLAnchorEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601888D RID: 100493 RVA: 0x000B5B10 File Offset: 0x000B4B10
		public override void remove_onmouseover(HTMLAnchorEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601888E RID: 100494 RVA: 0x000B5C00 File Offset: 0x000B4C00
		public override void add_onmouseout(HTMLAnchorEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601888F RID: 100495 RVA: 0x000B5C90 File Offset: 0x000B4C90
		public override void remove_onmouseout(HTMLAnchorEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlanchorEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018890 RID: 100496 RVA: 0x000B5D80 File Offset: 0x000B4D80
		public override void add_onkeyup(HTMLAnchorEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018891 RID: 100497 RVA: 0x000B5E10 File Offset: 0x000B4E10
		public override void remove_onkeyup(HTMLAnchorEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlanchorEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018892 RID: 100498 RVA: 0x000B5F00 File Offset: 0x000B4F00
		public override void add_onkeydown(HTMLAnchorEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018893 RID: 100499 RVA: 0x000B5F90 File Offset: 0x000B4F90
		public override void remove_onkeydown(HTMLAnchorEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlanchorEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018894 RID: 100500 RVA: 0x000B6080 File Offset: 0x000B5080
		public override void add_onkeypress(HTMLAnchorEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018895 RID: 100501 RVA: 0x000B6110 File Offset: 0x000B5110
		public override void remove_onkeypress(HTMLAnchorEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlanchorEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018896 RID: 100502 RVA: 0x000B6200 File Offset: 0x000B5200
		public override void add_ondblclick(HTMLAnchorEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018897 RID: 100503 RVA: 0x000B6290 File Offset: 0x000B5290
		public override void remove_ondblclick(HTMLAnchorEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlanchorEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018898 RID: 100504 RVA: 0x000B6380 File Offset: 0x000B5380
		public override void add_onclick(HTMLAnchorEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x06018899 RID: 100505 RVA: 0x000B6410 File Offset: 0x000B5410
		public override void remove_onclick(HTMLAnchorEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onclickDelegate != null && ((htmlanchorEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601889A RID: 100506 RVA: 0x000B6500 File Offset: 0x000B5500
		public override void add_onhelp(HTMLAnchorEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = new HTMLAnchorEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents_SinkHelper, ref num);
				htmlanchorEvents_SinkHelper.m_dwCookie = num;
				htmlanchorEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents_SinkHelper);
			}
		}

		// Token: 0x0601889B RID: 100507 RVA: 0x000B6590 File Offset: 0x000B5590
		public override void remove_onhelp(HTMLAnchorEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper;
					for (;;)
					{
						htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents_SinkHelper.m_onhelpDelegate != null && ((htmlanchorEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601889C RID: 100508 RVA: 0x000B6680 File Offset: 0x000B5680
		public HTMLAnchorEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601889D RID: 100509 RVA: 0x000B66A8 File Offset: 0x000B56A8
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
								HTMLAnchorEvents_SinkHelper htmlanchorEvents_SinkHelper = (HTMLAnchorEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlanchorEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x0601889E RID: 100510 RVA: 0x000B675C File Offset: 0x000B575C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B98 RID: 2968
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000B99 RID: 2969
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000B9A RID: 2970
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
