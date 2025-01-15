using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E13 RID: 3603
	internal sealed class HTMLTableEvents_EventProvider : HTMLTableEvents_Event, IDisposable
	{
		// Token: 0x060191E2 RID: 102882 RVA: 0x0010967C File Offset: 0x0010867C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				7, 244, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060191E3 RID: 102883 RVA: 0x00109790 File Offset: 0x00108790
		public override void add_onfocusout(HTMLTableEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191E4 RID: 102884 RVA: 0x00109820 File Offset: 0x00108820
		public override void remove_onfocusout(HTMLTableEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmltableEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191E5 RID: 102885 RVA: 0x00109910 File Offset: 0x00108910
		public override void add_onfocusin(HTMLTableEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191E6 RID: 102886 RVA: 0x001099A0 File Offset: 0x001089A0
		public override void remove_onfocusin(HTMLTableEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onfocusinDelegate != null && ((htmltableEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191E7 RID: 102887 RVA: 0x00109A90 File Offset: 0x00108A90
		public override void add_ondeactivate(HTMLTableEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191E8 RID: 102888 RVA: 0x00109B20 File Offset: 0x00108B20
		public override void remove_ondeactivate(HTMLTableEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmltableEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191E9 RID: 102889 RVA: 0x00109C10 File Offset: 0x00108C10
		public override void add_onactivate(HTMLTableEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191EA RID: 102890 RVA: 0x00109CA0 File Offset: 0x00108CA0
		public override void remove_onactivate(HTMLTableEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onactivateDelegate != null && ((htmltableEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191EB RID: 102891 RVA: 0x00109D90 File Offset: 0x00108D90
		public override void add_onmousewheel(HTMLTableEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191EC RID: 102892 RVA: 0x00109E20 File Offset: 0x00108E20
		public override void remove_onmousewheel(HTMLTableEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmltableEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191ED RID: 102893 RVA: 0x00109F10 File Offset: 0x00108F10
		public override void add_onmouseleave(HTMLTableEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191EE RID: 102894 RVA: 0x00109FA0 File Offset: 0x00108FA0
		public override void remove_onmouseleave(HTMLTableEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmltableEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191EF RID: 102895 RVA: 0x0010A090 File Offset: 0x00109090
		public override void add_onmouseenter(HTMLTableEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191F0 RID: 102896 RVA: 0x0010A120 File Offset: 0x00109120
		public override void remove_onmouseenter(HTMLTableEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmltableEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191F1 RID: 102897 RVA: 0x0010A210 File Offset: 0x00109210
		public override void add_onresizeend(HTMLTableEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191F2 RID: 102898 RVA: 0x0010A2A0 File Offset: 0x001092A0
		public override void remove_onresizeend(HTMLTableEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onresizeendDelegate != null && ((htmltableEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191F3 RID: 102899 RVA: 0x0010A390 File Offset: 0x00109390
		public override void add_onresizestart(HTMLTableEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191F4 RID: 102900 RVA: 0x0010A420 File Offset: 0x00109420
		public override void remove_onresizestart(HTMLTableEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onresizestartDelegate != null && ((htmltableEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191F5 RID: 102901 RVA: 0x0010A510 File Offset: 0x00109510
		public override void add_onmoveend(HTMLTableEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191F6 RID: 102902 RVA: 0x0010A5A0 File Offset: 0x001095A0
		public override void remove_onmoveend(HTMLTableEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmoveendDelegate != null && ((htmltableEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191F7 RID: 102903 RVA: 0x0010A690 File Offset: 0x00109690
		public override void add_onmovestart(HTMLTableEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191F8 RID: 102904 RVA: 0x0010A720 File Offset: 0x00109720
		public override void remove_onmovestart(HTMLTableEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmovestartDelegate != null && ((htmltableEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191F9 RID: 102905 RVA: 0x0010A810 File Offset: 0x00109810
		public override void add_oncontrolselect(HTMLTableEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191FA RID: 102906 RVA: 0x0010A8A0 File Offset: 0x001098A0
		public override void remove_oncontrolselect(HTMLTableEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmltableEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191FB RID: 102907 RVA: 0x0010A990 File Offset: 0x00109990
		public override void add_onmove(HTMLTableEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191FC RID: 102908 RVA: 0x0010AA20 File Offset: 0x00109A20
		public override void remove_onmove(HTMLTableEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmoveDelegate != null && ((htmltableEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191FD RID: 102909 RVA: 0x0010AB10 File Offset: 0x00109B10
		public override void add_onbeforeactivate(HTMLTableEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x060191FE RID: 102910 RVA: 0x0010ABA0 File Offset: 0x00109BA0
		public override void remove_onbeforeactivate(HTMLTableEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060191FF RID: 102911 RVA: 0x0010AC90 File Offset: 0x00109C90
		public override void add_onbeforedeactivate(HTMLTableEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019200 RID: 102912 RVA: 0x0010AD20 File Offset: 0x00109D20
		public override void remove_onbeforedeactivate(HTMLTableEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019201 RID: 102913 RVA: 0x0010AE10 File Offset: 0x00109E10
		public override void add_onpage(HTMLTableEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019202 RID: 102914 RVA: 0x0010AEA0 File Offset: 0x00109EA0
		public override void remove_onpage(HTMLTableEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onpageDelegate != null && ((htmltableEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019203 RID: 102915 RVA: 0x0010AF90 File Offset: 0x00109F90
		public override void add_onlayoutcomplete(HTMLTableEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019204 RID: 102916 RVA: 0x0010B020 File Offset: 0x0010A020
		public override void remove_onlayoutcomplete(HTMLTableEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmltableEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019205 RID: 102917 RVA: 0x0010B110 File Offset: 0x0010A110
		public override void add_onbeforeeditfocus(HTMLTableEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019206 RID: 102918 RVA: 0x0010B1A0 File Offset: 0x0010A1A0
		public override void remove_onbeforeeditfocus(HTMLTableEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019207 RID: 102919 RVA: 0x0010B290 File Offset: 0x0010A290
		public override void add_onreadystatechange(HTMLTableEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019208 RID: 102920 RVA: 0x0010B320 File Offset: 0x0010A320
		public override void remove_onreadystatechange(HTMLTableEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmltableEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019209 RID: 102921 RVA: 0x0010B410 File Offset: 0x0010A410
		public override void add_oncellchange(HTMLTableEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601920A RID: 102922 RVA: 0x0010B4A0 File Offset: 0x0010A4A0
		public override void remove_oncellchange(HTMLTableEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmltableEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601920B RID: 102923 RVA: 0x0010B590 File Offset: 0x0010A590
		public override void add_onrowsinserted(HTMLTableEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601920C RID: 102924 RVA: 0x0010B620 File Offset: 0x0010A620
		public override void remove_onrowsinserted(HTMLTableEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmltableEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601920D RID: 102925 RVA: 0x0010B710 File Offset: 0x0010A710
		public override void add_onrowsdelete(HTMLTableEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601920E RID: 102926 RVA: 0x0010B7A0 File Offset: 0x0010A7A0
		public override void remove_onrowsdelete(HTMLTableEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmltableEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601920F RID: 102927 RVA: 0x0010B890 File Offset: 0x0010A890
		public override void add_oncontextmenu(HTMLTableEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019210 RID: 102928 RVA: 0x0010B920 File Offset: 0x0010A920
		public override void remove_oncontextmenu(HTMLTableEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmltableEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019211 RID: 102929 RVA: 0x0010BA10 File Offset: 0x0010AA10
		public override void add_onpaste(HTMLTableEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019212 RID: 102930 RVA: 0x0010BAA0 File Offset: 0x0010AAA0
		public override void remove_onpaste(HTMLTableEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onpasteDelegate != null && ((htmltableEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019213 RID: 102931 RVA: 0x0010BB90 File Offset: 0x0010AB90
		public override void add_onbeforepaste(HTMLTableEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019214 RID: 102932 RVA: 0x0010BC20 File Offset: 0x0010AC20
		public override void remove_onbeforepaste(HTMLTableEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019215 RID: 102933 RVA: 0x0010BD10 File Offset: 0x0010AD10
		public override void add_oncopy(HTMLTableEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019216 RID: 102934 RVA: 0x0010BDA0 File Offset: 0x0010ADA0
		public override void remove_oncopy(HTMLTableEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_oncopyDelegate != null && ((htmltableEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019217 RID: 102935 RVA: 0x0010BE90 File Offset: 0x0010AE90
		public override void add_onbeforecopy(HTMLTableEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019218 RID: 102936 RVA: 0x0010BF20 File Offset: 0x0010AF20
		public override void remove_onbeforecopy(HTMLTableEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019219 RID: 102937 RVA: 0x0010C010 File Offset: 0x0010B010
		public override void add_oncut(HTMLTableEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601921A RID: 102938 RVA: 0x0010C0A0 File Offset: 0x0010B0A0
		public override void remove_oncut(HTMLTableEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_oncutDelegate != null && ((htmltableEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601921B RID: 102939 RVA: 0x0010C190 File Offset: 0x0010B190
		public override void add_onbeforecut(HTMLTableEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601921C RID: 102940 RVA: 0x0010C220 File Offset: 0x0010B220
		public override void remove_onbeforecut(HTMLTableEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601921D RID: 102941 RVA: 0x0010C310 File Offset: 0x0010B310
		public override void add_ondrop(HTMLTableEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601921E RID: 102942 RVA: 0x0010C3A0 File Offset: 0x0010B3A0
		public override void remove_ondrop(HTMLTableEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondropDelegate != null && ((htmltableEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601921F RID: 102943 RVA: 0x0010C490 File Offset: 0x0010B490
		public override void add_ondragleave(HTMLTableEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019220 RID: 102944 RVA: 0x0010C520 File Offset: 0x0010B520
		public override void remove_ondragleave(HTMLTableEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmltableEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019221 RID: 102945 RVA: 0x0010C610 File Offset: 0x0010B610
		public override void add_ondragover(HTMLTableEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019222 RID: 102946 RVA: 0x0010C6A0 File Offset: 0x0010B6A0
		public override void remove_ondragover(HTMLTableEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragoverDelegate != null && ((htmltableEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019223 RID: 102947 RVA: 0x0010C790 File Offset: 0x0010B790
		public override void add_ondragenter(HTMLTableEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019224 RID: 102948 RVA: 0x0010C820 File Offset: 0x0010B820
		public override void remove_ondragenter(HTMLTableEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragenterDelegate != null && ((htmltableEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019225 RID: 102949 RVA: 0x0010C910 File Offset: 0x0010B910
		public override void add_ondragend(HTMLTableEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019226 RID: 102950 RVA: 0x0010C9A0 File Offset: 0x0010B9A0
		public override void remove_ondragend(HTMLTableEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragendDelegate != null && ((htmltableEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019227 RID: 102951 RVA: 0x0010CA90 File Offset: 0x0010BA90
		public override void add_ondrag(HTMLTableEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019228 RID: 102952 RVA: 0x0010CB20 File Offset: 0x0010BB20
		public override void remove_ondrag(HTMLTableEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragDelegate != null && ((htmltableEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019229 RID: 102953 RVA: 0x0010CC10 File Offset: 0x0010BC10
		public override void add_onresize(HTMLTableEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601922A RID: 102954 RVA: 0x0010CCA0 File Offset: 0x0010BCA0
		public override void remove_onresize(HTMLTableEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onresizeDelegate != null && ((htmltableEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601922B RID: 102955 RVA: 0x0010CD90 File Offset: 0x0010BD90
		public override void add_onblur(HTMLTableEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601922C RID: 102956 RVA: 0x0010CE20 File Offset: 0x0010BE20
		public override void remove_onblur(HTMLTableEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onblurDelegate != null && ((htmltableEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601922D RID: 102957 RVA: 0x0010CF10 File Offset: 0x0010BF10
		public override void add_onfocus(HTMLTableEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601922E RID: 102958 RVA: 0x0010CFA0 File Offset: 0x0010BFA0
		public override void remove_onfocus(HTMLTableEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onfocusDelegate != null && ((htmltableEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601922F RID: 102959 RVA: 0x0010D090 File Offset: 0x0010C090
		public override void add_onscroll(HTMLTableEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019230 RID: 102960 RVA: 0x0010D120 File Offset: 0x0010C120
		public override void remove_onscroll(HTMLTableEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onscrollDelegate != null && ((htmltableEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019231 RID: 102961 RVA: 0x0010D210 File Offset: 0x0010C210
		public override void add_onpropertychange(HTMLTableEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019232 RID: 102962 RVA: 0x0010D2A0 File Offset: 0x0010C2A0
		public override void remove_onpropertychange(HTMLTableEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmltableEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019233 RID: 102963 RVA: 0x0010D390 File Offset: 0x0010C390
		public override void add_onlosecapture(HTMLTableEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019234 RID: 102964 RVA: 0x0010D420 File Offset: 0x0010C420
		public override void remove_onlosecapture(HTMLTableEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmltableEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019235 RID: 102965 RVA: 0x0010D510 File Offset: 0x0010C510
		public override void add_ondatasetcomplete(HTMLTableEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019236 RID: 102966 RVA: 0x0010D5A0 File Offset: 0x0010C5A0
		public override void remove_ondatasetcomplete(HTMLTableEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmltableEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019237 RID: 102967 RVA: 0x0010D690 File Offset: 0x0010C690
		public override void add_ondataavailable(HTMLTableEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019238 RID: 102968 RVA: 0x0010D720 File Offset: 0x0010C720
		public override void remove_ondataavailable(HTMLTableEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmltableEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019239 RID: 102969 RVA: 0x0010D810 File Offset: 0x0010C810
		public override void add_ondatasetchanged(HTMLTableEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601923A RID: 102970 RVA: 0x0010D8A0 File Offset: 0x0010C8A0
		public override void remove_ondatasetchanged(HTMLTableEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmltableEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601923B RID: 102971 RVA: 0x0010D990 File Offset: 0x0010C990
		public override void add_onrowenter(HTMLTableEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601923C RID: 102972 RVA: 0x0010DA20 File Offset: 0x0010CA20
		public override void remove_onrowenter(HTMLTableEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onrowenterDelegate != null && ((htmltableEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601923D RID: 102973 RVA: 0x0010DB10 File Offset: 0x0010CB10
		public override void add_onrowexit(HTMLTableEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601923E RID: 102974 RVA: 0x0010DBA0 File Offset: 0x0010CBA0
		public override void remove_onrowexit(HTMLTableEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onrowexitDelegate != null && ((htmltableEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601923F RID: 102975 RVA: 0x0010DC90 File Offset: 0x0010CC90
		public override void add_onerrorupdate(HTMLTableEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019240 RID: 102976 RVA: 0x0010DD20 File Offset: 0x0010CD20
		public override void remove_onerrorupdate(HTMLTableEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmltableEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019241 RID: 102977 RVA: 0x0010DE10 File Offset: 0x0010CE10
		public override void add_onafterupdate(HTMLTableEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019242 RID: 102978 RVA: 0x0010DEA0 File Offset: 0x0010CEA0
		public override void remove_onafterupdate(HTMLTableEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmltableEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019243 RID: 102979 RVA: 0x0010DF90 File Offset: 0x0010CF90
		public override void add_onbeforeupdate(HTMLTableEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019244 RID: 102980 RVA: 0x0010E020 File Offset: 0x0010D020
		public override void remove_onbeforeupdate(HTMLTableEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmltableEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019245 RID: 102981 RVA: 0x0010E110 File Offset: 0x0010D110
		public override void add_ondragstart(HTMLTableEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019246 RID: 102982 RVA: 0x0010E1A0 File Offset: 0x0010D1A0
		public override void remove_ondragstart(HTMLTableEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondragstartDelegate != null && ((htmltableEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019247 RID: 102983 RVA: 0x0010E290 File Offset: 0x0010D290
		public override void add_onfilterchange(HTMLTableEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019248 RID: 102984 RVA: 0x0010E320 File Offset: 0x0010D320
		public override void remove_onfilterchange(HTMLTableEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmltableEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019249 RID: 102985 RVA: 0x0010E410 File Offset: 0x0010D410
		public override void add_onselectstart(HTMLTableEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601924A RID: 102986 RVA: 0x0010E4A0 File Offset: 0x0010D4A0
		public override void remove_onselectstart(HTMLTableEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onselectstartDelegate != null && ((htmltableEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601924B RID: 102987 RVA: 0x0010E590 File Offset: 0x0010D590
		public override void add_onmouseup(HTMLTableEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601924C RID: 102988 RVA: 0x0010E620 File Offset: 0x0010D620
		public override void remove_onmouseup(HTMLTableEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmouseupDelegate != null && ((htmltableEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601924D RID: 102989 RVA: 0x0010E710 File Offset: 0x0010D710
		public override void add_onmousedown(HTMLTableEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601924E RID: 102990 RVA: 0x0010E7A0 File Offset: 0x0010D7A0
		public override void remove_onmousedown(HTMLTableEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmousedownDelegate != null && ((htmltableEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601924F RID: 102991 RVA: 0x0010E890 File Offset: 0x0010D890
		public override void add_onmousemove(HTMLTableEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019250 RID: 102992 RVA: 0x0010E920 File Offset: 0x0010D920
		public override void remove_onmousemove(HTMLTableEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmltableEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019251 RID: 102993 RVA: 0x0010EA10 File Offset: 0x0010DA10
		public override void add_onmouseover(HTMLTableEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019252 RID: 102994 RVA: 0x0010EAA0 File Offset: 0x0010DAA0
		public override void remove_onmouseover(HTMLTableEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmltableEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019253 RID: 102995 RVA: 0x0010EB90 File Offset: 0x0010DB90
		public override void add_onmouseout(HTMLTableEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019254 RID: 102996 RVA: 0x0010EC20 File Offset: 0x0010DC20
		public override void remove_onmouseout(HTMLTableEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmltableEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019255 RID: 102997 RVA: 0x0010ED10 File Offset: 0x0010DD10
		public override void add_onkeyup(HTMLTableEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019256 RID: 102998 RVA: 0x0010EDA0 File Offset: 0x0010DDA0
		public override void remove_onkeyup(HTMLTableEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onkeyupDelegate != null && ((htmltableEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019257 RID: 102999 RVA: 0x0010EE90 File Offset: 0x0010DE90
		public override void add_onkeydown(HTMLTableEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019258 RID: 103000 RVA: 0x0010EF20 File Offset: 0x0010DF20
		public override void remove_onkeydown(HTMLTableEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onkeydownDelegate != null && ((htmltableEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019259 RID: 103001 RVA: 0x0010F010 File Offset: 0x0010E010
		public override void add_onkeypress(HTMLTableEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601925A RID: 103002 RVA: 0x0010F0A0 File Offset: 0x0010E0A0
		public override void remove_onkeypress(HTMLTableEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onkeypressDelegate != null && ((htmltableEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601925B RID: 103003 RVA: 0x0010F190 File Offset: 0x0010E190
		public override void add_ondblclick(HTMLTableEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601925C RID: 103004 RVA: 0x0010F220 File Offset: 0x0010E220
		public override void remove_ondblclick(HTMLTableEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_ondblclickDelegate != null && ((htmltableEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601925D RID: 103005 RVA: 0x0010F310 File Offset: 0x0010E310
		public override void add_onclick(HTMLTableEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x0601925E RID: 103006 RVA: 0x0010F3A0 File Offset: 0x0010E3A0
		public override void remove_onclick(HTMLTableEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onclickDelegate != null && ((htmltableEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601925F RID: 103007 RVA: 0x0010F490 File Offset: 0x0010E490
		public override void add_onhelp(HTMLTableEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = new HTMLTableEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents_SinkHelper, ref num);
				htmltableEvents_SinkHelper.m_dwCookie = num;
				htmltableEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents_SinkHelper);
			}
		}

		// Token: 0x06019260 RID: 103008 RVA: 0x0010F520 File Offset: 0x0010E520
		public override void remove_onhelp(HTMLTableEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper;
					for (;;)
					{
						htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents_SinkHelper.m_onhelpDelegate != null && ((htmltableEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019261 RID: 103009 RVA: 0x0010F610 File Offset: 0x0010E610
		public HTMLTableEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06019262 RID: 103010 RVA: 0x0010F638 File Offset: 0x0010E638
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
								HTMLTableEvents_SinkHelper htmltableEvents_SinkHelper = (HTMLTableEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmltableEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06019263 RID: 103011 RVA: 0x0010F6EC File Offset: 0x0010E6EC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000EF8 RID: 3832
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000EF9 RID: 3833
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000EFA RID: 3834
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
