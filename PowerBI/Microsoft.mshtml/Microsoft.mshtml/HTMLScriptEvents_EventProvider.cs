using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF3 RID: 3571
	internal sealed class HTMLScriptEvents_EventProvider : HTMLScriptEvents_Event, IDisposable
	{
		// Token: 0x0601866E RID: 99950 RVA: 0x000A117C File Offset: 0x000A017C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				226, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601866F RID: 99951 RVA: 0x000A1290 File Offset: 0x000A0290
		public override void add_onerror(HTMLScriptEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018670 RID: 99952 RVA: 0x000A1320 File Offset: 0x000A0320
		public override void remove_onerror(HTMLScriptEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onerrorDelegate != null && ((htmlscriptEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018671 RID: 99953 RVA: 0x000A1410 File Offset: 0x000A0410
		public override void add_onfocusout(HTMLScriptEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018672 RID: 99954 RVA: 0x000A14A0 File Offset: 0x000A04A0
		public override void remove_onfocusout(HTMLScriptEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlscriptEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018673 RID: 99955 RVA: 0x000A1590 File Offset: 0x000A0590
		public override void add_onfocusin(HTMLScriptEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018674 RID: 99956 RVA: 0x000A1620 File Offset: 0x000A0620
		public override void remove_onfocusin(HTMLScriptEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlscriptEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018675 RID: 99957 RVA: 0x000A1710 File Offset: 0x000A0710
		public override void add_ondeactivate(HTMLScriptEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018676 RID: 99958 RVA: 0x000A17A0 File Offset: 0x000A07A0
		public override void remove_ondeactivate(HTMLScriptEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018677 RID: 99959 RVA: 0x000A1890 File Offset: 0x000A0890
		public override void add_onactivate(HTMLScriptEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018678 RID: 99960 RVA: 0x000A1920 File Offset: 0x000A0920
		public override void remove_onactivate(HTMLScriptEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onactivateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018679 RID: 99961 RVA: 0x000A1A10 File Offset: 0x000A0A10
		public override void add_onmousewheel(HTMLScriptEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601867A RID: 99962 RVA: 0x000A1AA0 File Offset: 0x000A0AA0
		public override void remove_onmousewheel(HTMLScriptEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601867B RID: 99963 RVA: 0x000A1B90 File Offset: 0x000A0B90
		public override void add_onmouseleave(HTMLScriptEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601867C RID: 99964 RVA: 0x000A1C20 File Offset: 0x000A0C20
		public override void remove_onmouseleave(HTMLScriptEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601867D RID: 99965 RVA: 0x000A1D10 File Offset: 0x000A0D10
		public override void add_onmouseenter(HTMLScriptEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601867E RID: 99966 RVA: 0x000A1DA0 File Offset: 0x000A0DA0
		public override void remove_onmouseenter(HTMLScriptEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601867F RID: 99967 RVA: 0x000A1E90 File Offset: 0x000A0E90
		public override void add_onresizeend(HTMLScriptEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018680 RID: 99968 RVA: 0x000A1F20 File Offset: 0x000A0F20
		public override void remove_onresizeend(HTMLScriptEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlscriptEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018681 RID: 99969 RVA: 0x000A2010 File Offset: 0x000A1010
		public override void add_onresizestart(HTMLScriptEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018682 RID: 99970 RVA: 0x000A20A0 File Offset: 0x000A10A0
		public override void remove_onresizestart(HTMLScriptEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlscriptEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018683 RID: 99971 RVA: 0x000A2190 File Offset: 0x000A1190
		public override void add_onmoveend(HTMLScriptEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018684 RID: 99972 RVA: 0x000A2220 File Offset: 0x000A1220
		public override void remove_onmoveend(HTMLScriptEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018685 RID: 99973 RVA: 0x000A2310 File Offset: 0x000A1310
		public override void add_onmovestart(HTMLScriptEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018686 RID: 99974 RVA: 0x000A23A0 File Offset: 0x000A13A0
		public override void remove_onmovestart(HTMLScriptEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018687 RID: 99975 RVA: 0x000A2490 File Offset: 0x000A1490
		public override void add_oncontrolselect(HTMLScriptEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018688 RID: 99976 RVA: 0x000A2520 File Offset: 0x000A1520
		public override void remove_oncontrolselect(HTMLScriptEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlscriptEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018689 RID: 99977 RVA: 0x000A2610 File Offset: 0x000A1610
		public override void add_onmove(HTMLScriptEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601868A RID: 99978 RVA: 0x000A26A0 File Offset: 0x000A16A0
		public override void remove_onmove(HTMLScriptEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmoveDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601868B RID: 99979 RVA: 0x000A2790 File Offset: 0x000A1790
		public override void add_onbeforeactivate(HTMLScriptEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601868C RID: 99980 RVA: 0x000A2820 File Offset: 0x000A1820
		public override void remove_onbeforeactivate(HTMLScriptEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601868D RID: 99981 RVA: 0x000A2910 File Offset: 0x000A1910
		public override void add_onbeforedeactivate(HTMLScriptEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601868E RID: 99982 RVA: 0x000A29A0 File Offset: 0x000A19A0
		public override void remove_onbeforedeactivate(HTMLScriptEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601868F RID: 99983 RVA: 0x000A2A90 File Offset: 0x000A1A90
		public override void add_onpage(HTMLScriptEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018690 RID: 99984 RVA: 0x000A2B20 File Offset: 0x000A1B20
		public override void remove_onpage(HTMLScriptEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onpageDelegate != null && ((htmlscriptEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018691 RID: 99985 RVA: 0x000A2C10 File Offset: 0x000A1C10
		public override void add_onlayoutcomplete(HTMLScriptEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018692 RID: 99986 RVA: 0x000A2CA0 File Offset: 0x000A1CA0
		public override void remove_onlayoutcomplete(HTMLScriptEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlscriptEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018693 RID: 99987 RVA: 0x000A2D90 File Offset: 0x000A1D90
		public override void add_onbeforeeditfocus(HTMLScriptEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018694 RID: 99988 RVA: 0x000A2E20 File Offset: 0x000A1E20
		public override void remove_onbeforeeditfocus(HTMLScriptEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018695 RID: 99989 RVA: 0x000A2F10 File Offset: 0x000A1F10
		public override void add_onreadystatechange(HTMLScriptEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018696 RID: 99990 RVA: 0x000A2FA0 File Offset: 0x000A1FA0
		public override void remove_onreadystatechange(HTMLScriptEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlscriptEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018697 RID: 99991 RVA: 0x000A3090 File Offset: 0x000A2090
		public override void add_oncellchange(HTMLScriptEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x06018698 RID: 99992 RVA: 0x000A3120 File Offset: 0x000A2120
		public override void remove_oncellchange(HTMLScriptEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlscriptEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018699 RID: 99993 RVA: 0x000A3210 File Offset: 0x000A2210
		public override void add_onrowsinserted(HTMLScriptEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601869A RID: 99994 RVA: 0x000A32A0 File Offset: 0x000A22A0
		public override void remove_onrowsinserted(HTMLScriptEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlscriptEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601869B RID: 99995 RVA: 0x000A3390 File Offset: 0x000A2390
		public override void add_onrowsdelete(HTMLScriptEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601869C RID: 99996 RVA: 0x000A3420 File Offset: 0x000A2420
		public override void remove_onrowsdelete(HTMLScriptEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlscriptEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601869D RID: 99997 RVA: 0x000A3510 File Offset: 0x000A2510
		public override void add_oncontextmenu(HTMLScriptEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x0601869E RID: 99998 RVA: 0x000A35A0 File Offset: 0x000A25A0
		public override void remove_oncontextmenu(HTMLScriptEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlscriptEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601869F RID: 99999 RVA: 0x000A3690 File Offset: 0x000A2690
		public override void add_onpaste(HTMLScriptEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186A0 RID: 100000 RVA: 0x000A3720 File Offset: 0x000A2720
		public override void remove_onpaste(HTMLScriptEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onpasteDelegate != null && ((htmlscriptEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186A1 RID: 100001 RVA: 0x000A3810 File Offset: 0x000A2810
		public override void add_onbeforepaste(HTMLScriptEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186A2 RID: 100002 RVA: 0x000A38A0 File Offset: 0x000A28A0
		public override void remove_onbeforepaste(HTMLScriptEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186A3 RID: 100003 RVA: 0x000A3990 File Offset: 0x000A2990
		public override void add_oncopy(HTMLScriptEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186A4 RID: 100004 RVA: 0x000A3A20 File Offset: 0x000A2A20
		public override void remove_oncopy(HTMLScriptEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_oncopyDelegate != null && ((htmlscriptEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186A5 RID: 100005 RVA: 0x000A3B10 File Offset: 0x000A2B10
		public override void add_onbeforecopy(HTMLScriptEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186A6 RID: 100006 RVA: 0x000A3BA0 File Offset: 0x000A2BA0
		public override void remove_onbeforecopy(HTMLScriptEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186A7 RID: 100007 RVA: 0x000A3C90 File Offset: 0x000A2C90
		public override void add_oncut(HTMLScriptEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186A8 RID: 100008 RVA: 0x000A3D20 File Offset: 0x000A2D20
		public override void remove_oncut(HTMLScriptEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_oncutDelegate != null && ((htmlscriptEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186A9 RID: 100009 RVA: 0x000A3E10 File Offset: 0x000A2E10
		public override void add_onbeforecut(HTMLScriptEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186AA RID: 100010 RVA: 0x000A3EA0 File Offset: 0x000A2EA0
		public override void remove_onbeforecut(HTMLScriptEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186AB RID: 100011 RVA: 0x000A3F90 File Offset: 0x000A2F90
		public override void add_ondrop(HTMLScriptEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186AC RID: 100012 RVA: 0x000A4020 File Offset: 0x000A3020
		public override void remove_ondrop(HTMLScriptEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondropDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186AD RID: 100013 RVA: 0x000A4110 File Offset: 0x000A3110
		public override void add_ondragleave(HTMLScriptEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186AE RID: 100014 RVA: 0x000A41A0 File Offset: 0x000A31A0
		public override void remove_ondragleave(HTMLScriptEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186AF RID: 100015 RVA: 0x000A4290 File Offset: 0x000A3290
		public override void add_ondragover(HTMLScriptEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186B0 RID: 100016 RVA: 0x000A4320 File Offset: 0x000A3320
		public override void remove_ondragover(HTMLScriptEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186B1 RID: 100017 RVA: 0x000A4410 File Offset: 0x000A3410
		public override void add_ondragenter(HTMLScriptEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186B2 RID: 100018 RVA: 0x000A44A0 File Offset: 0x000A34A0
		public override void remove_ondragenter(HTMLScriptEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186B3 RID: 100019 RVA: 0x000A4590 File Offset: 0x000A3590
		public override void add_ondragend(HTMLScriptEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186B4 RID: 100020 RVA: 0x000A4620 File Offset: 0x000A3620
		public override void remove_ondragend(HTMLScriptEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragendDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186B5 RID: 100021 RVA: 0x000A4710 File Offset: 0x000A3710
		public override void add_ondrag(HTMLScriptEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186B6 RID: 100022 RVA: 0x000A47A0 File Offset: 0x000A37A0
		public override void remove_ondrag(HTMLScriptEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186B7 RID: 100023 RVA: 0x000A4890 File Offset: 0x000A3890
		public override void add_onresize(HTMLScriptEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186B8 RID: 100024 RVA: 0x000A4920 File Offset: 0x000A3920
		public override void remove_onresize(HTMLScriptEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onresizeDelegate != null && ((htmlscriptEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186B9 RID: 100025 RVA: 0x000A4A10 File Offset: 0x000A3A10
		public override void add_onblur(HTMLScriptEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186BA RID: 100026 RVA: 0x000A4AA0 File Offset: 0x000A3AA0
		public override void remove_onblur(HTMLScriptEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onblurDelegate != null && ((htmlscriptEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186BB RID: 100027 RVA: 0x000A4B90 File Offset: 0x000A3B90
		public override void add_onfocus(HTMLScriptEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186BC RID: 100028 RVA: 0x000A4C20 File Offset: 0x000A3C20
		public override void remove_onfocus(HTMLScriptEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onfocusDelegate != null && ((htmlscriptEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186BD RID: 100029 RVA: 0x000A4D10 File Offset: 0x000A3D10
		public override void add_onscroll(HTMLScriptEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186BE RID: 100030 RVA: 0x000A4DA0 File Offset: 0x000A3DA0
		public override void remove_onscroll(HTMLScriptEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onscrollDelegate != null && ((htmlscriptEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186BF RID: 100031 RVA: 0x000A4E90 File Offset: 0x000A3E90
		public override void add_onpropertychange(HTMLScriptEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186C0 RID: 100032 RVA: 0x000A4F20 File Offset: 0x000A3F20
		public override void remove_onpropertychange(HTMLScriptEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlscriptEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186C1 RID: 100033 RVA: 0x000A5010 File Offset: 0x000A4010
		public override void add_onlosecapture(HTMLScriptEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186C2 RID: 100034 RVA: 0x000A50A0 File Offset: 0x000A40A0
		public override void remove_onlosecapture(HTMLScriptEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlscriptEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186C3 RID: 100035 RVA: 0x000A5190 File Offset: 0x000A4190
		public override void add_ondatasetcomplete(HTMLScriptEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186C4 RID: 100036 RVA: 0x000A5220 File Offset: 0x000A4220
		public override void remove_ondatasetcomplete(HTMLScriptEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186C5 RID: 100037 RVA: 0x000A5310 File Offset: 0x000A4310
		public override void add_ondataavailable(HTMLScriptEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186C6 RID: 100038 RVA: 0x000A53A0 File Offset: 0x000A43A0
		public override void remove_ondataavailable(HTMLScriptEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186C7 RID: 100039 RVA: 0x000A5490 File Offset: 0x000A4490
		public override void add_ondatasetchanged(HTMLScriptEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186C8 RID: 100040 RVA: 0x000A5520 File Offset: 0x000A4520
		public override void remove_ondatasetchanged(HTMLScriptEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186C9 RID: 100041 RVA: 0x000A5610 File Offset: 0x000A4610
		public override void add_onrowenter(HTMLScriptEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186CA RID: 100042 RVA: 0x000A56A0 File Offset: 0x000A46A0
		public override void remove_onrowenter(HTMLScriptEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlscriptEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186CB RID: 100043 RVA: 0x000A5790 File Offset: 0x000A4790
		public override void add_onrowexit(HTMLScriptEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186CC RID: 100044 RVA: 0x000A5820 File Offset: 0x000A4820
		public override void remove_onrowexit(HTMLScriptEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlscriptEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186CD RID: 100045 RVA: 0x000A5910 File Offset: 0x000A4910
		public override void add_onerrorupdate(HTMLScriptEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186CE RID: 100046 RVA: 0x000A59A0 File Offset: 0x000A49A0
		public override void remove_onerrorupdate(HTMLScriptEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186CF RID: 100047 RVA: 0x000A5A90 File Offset: 0x000A4A90
		public override void add_onafterupdate(HTMLScriptEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186D0 RID: 100048 RVA: 0x000A5B20 File Offset: 0x000A4B20
		public override void remove_onafterupdate(HTMLScriptEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186D1 RID: 100049 RVA: 0x000A5C10 File Offset: 0x000A4C10
		public override void add_onbeforeupdate(HTMLScriptEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186D2 RID: 100050 RVA: 0x000A5CA0 File Offset: 0x000A4CA0
		public override void remove_onbeforeupdate(HTMLScriptEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlscriptEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186D3 RID: 100051 RVA: 0x000A5D90 File Offset: 0x000A4D90
		public override void add_ondragstart(HTMLScriptEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186D4 RID: 100052 RVA: 0x000A5E20 File Offset: 0x000A4E20
		public override void remove_ondragstart(HTMLScriptEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186D5 RID: 100053 RVA: 0x000A5F10 File Offset: 0x000A4F10
		public override void add_onfilterchange(HTMLScriptEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186D6 RID: 100054 RVA: 0x000A5FA0 File Offset: 0x000A4FA0
		public override void remove_onfilterchange(HTMLScriptEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlscriptEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186D7 RID: 100055 RVA: 0x000A6090 File Offset: 0x000A5090
		public override void add_onselectstart(HTMLScriptEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186D8 RID: 100056 RVA: 0x000A6120 File Offset: 0x000A5120
		public override void remove_onselectstart(HTMLScriptEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlscriptEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186D9 RID: 100057 RVA: 0x000A6210 File Offset: 0x000A5210
		public override void add_onmouseup(HTMLScriptEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186DA RID: 100058 RVA: 0x000A62A0 File Offset: 0x000A52A0
		public override void remove_onmouseup(HTMLScriptEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186DB RID: 100059 RVA: 0x000A6390 File Offset: 0x000A5390
		public override void add_onmousedown(HTMLScriptEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186DC RID: 100060 RVA: 0x000A6420 File Offset: 0x000A5420
		public override void remove_onmousedown(HTMLScriptEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186DD RID: 100061 RVA: 0x000A6510 File Offset: 0x000A5510
		public override void add_onmousemove(HTMLScriptEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186DE RID: 100062 RVA: 0x000A65A0 File Offset: 0x000A55A0
		public override void remove_onmousemove(HTMLScriptEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186DF RID: 100063 RVA: 0x000A6690 File Offset: 0x000A5690
		public override void add_onmouseover(HTMLScriptEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186E0 RID: 100064 RVA: 0x000A6720 File Offset: 0x000A5720
		public override void remove_onmouseover(HTMLScriptEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186E1 RID: 100065 RVA: 0x000A6810 File Offset: 0x000A5810
		public override void add_onmouseout(HTMLScriptEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186E2 RID: 100066 RVA: 0x000A68A0 File Offset: 0x000A58A0
		public override void remove_onmouseout(HTMLScriptEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlscriptEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186E3 RID: 100067 RVA: 0x000A6990 File Offset: 0x000A5990
		public override void add_onkeyup(HTMLScriptEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186E4 RID: 100068 RVA: 0x000A6A20 File Offset: 0x000A5A20
		public override void remove_onkeyup(HTMLScriptEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlscriptEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186E5 RID: 100069 RVA: 0x000A6B10 File Offset: 0x000A5B10
		public override void add_onkeydown(HTMLScriptEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186E6 RID: 100070 RVA: 0x000A6BA0 File Offset: 0x000A5BA0
		public override void remove_onkeydown(HTMLScriptEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlscriptEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186E7 RID: 100071 RVA: 0x000A6C90 File Offset: 0x000A5C90
		public override void add_onkeypress(HTMLScriptEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186E8 RID: 100072 RVA: 0x000A6D20 File Offset: 0x000A5D20
		public override void remove_onkeypress(HTMLScriptEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlscriptEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186E9 RID: 100073 RVA: 0x000A6E10 File Offset: 0x000A5E10
		public override void add_ondblclick(HTMLScriptEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186EA RID: 100074 RVA: 0x000A6EA0 File Offset: 0x000A5EA0
		public override void remove_ondblclick(HTMLScriptEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlscriptEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186EB RID: 100075 RVA: 0x000A6F90 File Offset: 0x000A5F90
		public override void add_onclick(HTMLScriptEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186EC RID: 100076 RVA: 0x000A7020 File Offset: 0x000A6020
		public override void remove_onclick(HTMLScriptEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onclickDelegate != null && ((htmlscriptEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186ED RID: 100077 RVA: 0x000A7110 File Offset: 0x000A6110
		public override void add_onhelp(HTMLScriptEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = new HTMLScriptEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents_SinkHelper, ref num);
				htmlscriptEvents_SinkHelper.m_dwCookie = num;
				htmlscriptEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents_SinkHelper);
			}
		}

		// Token: 0x060186EE RID: 100078 RVA: 0x000A71A0 File Offset: 0x000A61A0
		public override void remove_onhelp(HTMLScriptEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper;
					for (;;)
					{
						htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents_SinkHelper.m_onhelpDelegate != null && ((htmlscriptEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060186EF RID: 100079 RVA: 0x000A7290 File Offset: 0x000A6290
		public HTMLScriptEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060186F0 RID: 100080 RVA: 0x000A72B8 File Offset: 0x000A62B8
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
								HTMLScriptEvents_SinkHelper htmlscriptEvents_SinkHelper = (HTMLScriptEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlscriptEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x060186F1 RID: 100081 RVA: 0x000A736C File Offset: 0x000A636C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B02 RID: 2818
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000B03 RID: 2819
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000B04 RID: 2820
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
