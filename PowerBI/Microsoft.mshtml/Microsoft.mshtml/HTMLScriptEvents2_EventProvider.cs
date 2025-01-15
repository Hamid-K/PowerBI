using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE7 RID: 3559
	internal sealed class HTMLScriptEvents2_EventProvider : HTMLScriptEvents2_Event, IDisposable
	{
		// Token: 0x06018295 RID: 98965 RVA: 0x0007E10C File Offset: 0x0007D10C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				33, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018296 RID: 98966 RVA: 0x0007E220 File Offset: 0x0007D220
		public override void add_onerror(HTMLScriptEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018297 RID: 98967 RVA: 0x0007E2B0 File Offset: 0x0007D2B0
		public override void remove_onerror(HTMLScriptEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018298 RID: 98968 RVA: 0x0007E3A0 File Offset: 0x0007D3A0
		public override void add_onmousewheel(HTMLScriptEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018299 RID: 98969 RVA: 0x0007E430 File Offset: 0x0007D430
		public override void remove_onmousewheel(HTMLScriptEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601829A RID: 98970 RVA: 0x0007E520 File Offset: 0x0007D520
		public override void add_onresizeend(HTMLScriptEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601829B RID: 98971 RVA: 0x0007E5B0 File Offset: 0x0007D5B0
		public override void remove_onresizeend(HTMLScriptEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601829C RID: 98972 RVA: 0x0007E6A0 File Offset: 0x0007D6A0
		public override void add_onresizestart(HTMLScriptEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601829D RID: 98973 RVA: 0x0007E730 File Offset: 0x0007D730
		public override void remove_onresizestart(HTMLScriptEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601829E RID: 98974 RVA: 0x0007E820 File Offset: 0x0007D820
		public override void add_onmoveend(HTMLScriptEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601829F RID: 98975 RVA: 0x0007E8B0 File Offset: 0x0007D8B0
		public override void remove_onmoveend(HTMLScriptEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182A0 RID: 98976 RVA: 0x0007E9A0 File Offset: 0x0007D9A0
		public override void add_onmovestart(HTMLScriptEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182A1 RID: 98977 RVA: 0x0007EA30 File Offset: 0x0007DA30
		public override void remove_onmovestart(HTMLScriptEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182A2 RID: 98978 RVA: 0x0007EB20 File Offset: 0x0007DB20
		public override void add_oncontrolselect(HTMLScriptEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182A3 RID: 98979 RVA: 0x0007EBB0 File Offset: 0x0007DBB0
		public override void remove_oncontrolselect(HTMLScriptEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlscriptEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182A4 RID: 98980 RVA: 0x0007ECA0 File Offset: 0x0007DCA0
		public override void add_onmove(HTMLScriptEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182A5 RID: 98981 RVA: 0x0007ED30 File Offset: 0x0007DD30
		public override void remove_onmove(HTMLScriptEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182A6 RID: 98982 RVA: 0x0007EE20 File Offset: 0x0007DE20
		public override void add_onfocusout(HTMLScriptEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182A7 RID: 98983 RVA: 0x0007EEB0 File Offset: 0x0007DEB0
		public override void remove_onfocusout(HTMLScriptEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182A8 RID: 98984 RVA: 0x0007EFA0 File Offset: 0x0007DFA0
		public override void add_onfocusin(HTMLScriptEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182A9 RID: 98985 RVA: 0x0007F030 File Offset: 0x0007E030
		public override void remove_onfocusin(HTMLScriptEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182AA RID: 98986 RVA: 0x0007F120 File Offset: 0x0007E120
		public override void add_onbeforeactivate(HTMLScriptEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182AB RID: 98987 RVA: 0x0007F1B0 File Offset: 0x0007E1B0
		public override void remove_onbeforeactivate(HTMLScriptEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182AC RID: 98988 RVA: 0x0007F2A0 File Offset: 0x0007E2A0
		public override void add_onbeforedeactivate(HTMLScriptEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182AD RID: 98989 RVA: 0x0007F330 File Offset: 0x0007E330
		public override void remove_onbeforedeactivate(HTMLScriptEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182AE RID: 98990 RVA: 0x0007F420 File Offset: 0x0007E420
		public override void add_ondeactivate(HTMLScriptEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182AF RID: 98991 RVA: 0x0007F4B0 File Offset: 0x0007E4B0
		public override void remove_ondeactivate(HTMLScriptEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182B0 RID: 98992 RVA: 0x0007F5A0 File Offset: 0x0007E5A0
		public override void add_onactivate(HTMLScriptEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182B1 RID: 98993 RVA: 0x0007F630 File Offset: 0x0007E630
		public override void remove_onactivate(HTMLScriptEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182B2 RID: 98994 RVA: 0x0007F720 File Offset: 0x0007E720
		public override void add_onmouseleave(HTMLScriptEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182B3 RID: 98995 RVA: 0x0007F7B0 File Offset: 0x0007E7B0
		public override void remove_onmouseleave(HTMLScriptEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182B4 RID: 98996 RVA: 0x0007F8A0 File Offset: 0x0007E8A0
		public override void add_onmouseenter(HTMLScriptEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182B5 RID: 98997 RVA: 0x0007F930 File Offset: 0x0007E930
		public override void remove_onmouseenter(HTMLScriptEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182B6 RID: 98998 RVA: 0x0007FA20 File Offset: 0x0007EA20
		public override void add_onpage(HTMLScriptEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182B7 RID: 98999 RVA: 0x0007FAB0 File Offset: 0x0007EAB0
		public override void remove_onpage(HTMLScriptEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onpageDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182B8 RID: 99000 RVA: 0x0007FBA0 File Offset: 0x0007EBA0
		public override void add_onlayoutcomplete(HTMLScriptEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182B9 RID: 99001 RVA: 0x0007FC30 File Offset: 0x0007EC30
		public override void remove_onlayoutcomplete(HTMLScriptEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182BA RID: 99002 RVA: 0x0007FD20 File Offset: 0x0007ED20
		public override void add_onreadystatechange(HTMLScriptEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182BB RID: 99003 RVA: 0x0007FDB0 File Offset: 0x0007EDB0
		public override void remove_onreadystatechange(HTMLScriptEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182BC RID: 99004 RVA: 0x0007FEA0 File Offset: 0x0007EEA0
		public override void add_oncellchange(HTMLScriptEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182BD RID: 99005 RVA: 0x0007FF30 File Offset: 0x0007EF30
		public override void remove_oncellchange(HTMLScriptEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlscriptEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182BE RID: 99006 RVA: 0x00080020 File Offset: 0x0007F020
		public override void add_onrowsinserted(HTMLScriptEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182BF RID: 99007 RVA: 0x000800B0 File Offset: 0x0007F0B0
		public override void remove_onrowsinserted(HTMLScriptEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182C0 RID: 99008 RVA: 0x000801A0 File Offset: 0x0007F1A0
		public override void add_onrowsdelete(HTMLScriptEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182C1 RID: 99009 RVA: 0x00080230 File Offset: 0x0007F230
		public override void remove_onrowsdelete(HTMLScriptEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182C2 RID: 99010 RVA: 0x00080320 File Offset: 0x0007F320
		public override void add_oncontextmenu(HTMLScriptEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182C3 RID: 99011 RVA: 0x000803B0 File Offset: 0x0007F3B0
		public override void remove_oncontextmenu(HTMLScriptEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlscriptEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182C4 RID: 99012 RVA: 0x000804A0 File Offset: 0x0007F4A0
		public override void add_onpaste(HTMLScriptEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182C5 RID: 99013 RVA: 0x00080530 File Offset: 0x0007F530
		public override void remove_onpaste(HTMLScriptEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182C6 RID: 99014 RVA: 0x00080620 File Offset: 0x0007F620
		public override void add_onbeforepaste(HTMLScriptEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182C7 RID: 99015 RVA: 0x000806B0 File Offset: 0x0007F6B0
		public override void remove_onbeforepaste(HTMLScriptEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182C8 RID: 99016 RVA: 0x000807A0 File Offset: 0x0007F7A0
		public override void add_oncopy(HTMLScriptEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182C9 RID: 99017 RVA: 0x00080830 File Offset: 0x0007F830
		public override void remove_oncopy(HTMLScriptEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlscriptEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182CA RID: 99018 RVA: 0x00080920 File Offset: 0x0007F920
		public override void add_onbeforecopy(HTMLScriptEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182CB RID: 99019 RVA: 0x000809B0 File Offset: 0x0007F9B0
		public override void remove_onbeforecopy(HTMLScriptEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182CC RID: 99020 RVA: 0x00080AA0 File Offset: 0x0007FAA0
		public override void add_oncut(HTMLScriptEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182CD RID: 99021 RVA: 0x00080B30 File Offset: 0x0007FB30
		public override void remove_oncut(HTMLScriptEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_oncutDelegate != null && ((htmlscriptEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182CE RID: 99022 RVA: 0x00080C20 File Offset: 0x0007FC20
		public override void add_onbeforecut(HTMLScriptEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182CF RID: 99023 RVA: 0x00080CB0 File Offset: 0x0007FCB0
		public override void remove_onbeforecut(HTMLScriptEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182D0 RID: 99024 RVA: 0x00080DA0 File Offset: 0x0007FDA0
		public override void add_ondrop(HTMLScriptEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182D1 RID: 99025 RVA: 0x00080E30 File Offset: 0x0007FE30
		public override void remove_ondrop(HTMLScriptEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondropDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182D2 RID: 99026 RVA: 0x00080F20 File Offset: 0x0007FF20
		public override void add_ondragleave(HTMLScriptEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182D3 RID: 99027 RVA: 0x00080FB0 File Offset: 0x0007FFB0
		public override void remove_ondragleave(HTMLScriptEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182D4 RID: 99028 RVA: 0x000810A0 File Offset: 0x000800A0
		public override void add_ondragover(HTMLScriptEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182D5 RID: 99029 RVA: 0x00081130 File Offset: 0x00080130
		public override void remove_ondragover(HTMLScriptEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182D6 RID: 99030 RVA: 0x00081220 File Offset: 0x00080220
		public override void add_ondragenter(HTMLScriptEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182D7 RID: 99031 RVA: 0x000812B0 File Offset: 0x000802B0
		public override void remove_ondragenter(HTMLScriptEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182D8 RID: 99032 RVA: 0x000813A0 File Offset: 0x000803A0
		public override void add_ondragend(HTMLScriptEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182D9 RID: 99033 RVA: 0x00081430 File Offset: 0x00080430
		public override void remove_ondragend(HTMLScriptEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182DA RID: 99034 RVA: 0x00081520 File Offset: 0x00080520
		public override void add_ondrag(HTMLScriptEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182DB RID: 99035 RVA: 0x000815B0 File Offset: 0x000805B0
		public override void remove_ondrag(HTMLScriptEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182DC RID: 99036 RVA: 0x000816A0 File Offset: 0x000806A0
		public override void add_onresize(HTMLScriptEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182DD RID: 99037 RVA: 0x00081730 File Offset: 0x00080730
		public override void remove_onresize(HTMLScriptEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182DE RID: 99038 RVA: 0x00081820 File Offset: 0x00080820
		public override void add_onblur(HTMLScriptEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182DF RID: 99039 RVA: 0x000818B0 File Offset: 0x000808B0
		public override void remove_onblur(HTMLScriptEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onblurDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182E0 RID: 99040 RVA: 0x000819A0 File Offset: 0x000809A0
		public override void add_onfocus(HTMLScriptEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182E1 RID: 99041 RVA: 0x00081A30 File Offset: 0x00080A30
		public override void remove_onfocus(HTMLScriptEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182E2 RID: 99042 RVA: 0x00081B20 File Offset: 0x00080B20
		public override void add_onscroll(HTMLScriptEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182E3 RID: 99043 RVA: 0x00081BB0 File Offset: 0x00080BB0
		public override void remove_onscroll(HTMLScriptEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182E4 RID: 99044 RVA: 0x00081CA0 File Offset: 0x00080CA0
		public override void add_onpropertychange(HTMLScriptEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182E5 RID: 99045 RVA: 0x00081D30 File Offset: 0x00080D30
		public override void remove_onpropertychange(HTMLScriptEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182E6 RID: 99046 RVA: 0x00081E20 File Offset: 0x00080E20
		public override void add_onlosecapture(HTMLScriptEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182E7 RID: 99047 RVA: 0x00081EB0 File Offset: 0x00080EB0
		public override void remove_onlosecapture(HTMLScriptEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182E8 RID: 99048 RVA: 0x00081FA0 File Offset: 0x00080FA0
		public override void add_ondatasetcomplete(HTMLScriptEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182E9 RID: 99049 RVA: 0x00082030 File Offset: 0x00081030
		public override void remove_ondatasetcomplete(HTMLScriptEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182EA RID: 99050 RVA: 0x00082120 File Offset: 0x00081120
		public override void add_ondataavailable(HTMLScriptEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182EB RID: 99051 RVA: 0x000821B0 File Offset: 0x000811B0
		public override void remove_ondataavailable(HTMLScriptEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182EC RID: 99052 RVA: 0x000822A0 File Offset: 0x000812A0
		public override void add_ondatasetchanged(HTMLScriptEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182ED RID: 99053 RVA: 0x00082330 File Offset: 0x00081330
		public override void remove_ondatasetchanged(HTMLScriptEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182EE RID: 99054 RVA: 0x00082420 File Offset: 0x00081420
		public override void add_onrowenter(HTMLScriptEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182EF RID: 99055 RVA: 0x000824B0 File Offset: 0x000814B0
		public override void remove_onrowenter(HTMLScriptEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182F0 RID: 99056 RVA: 0x000825A0 File Offset: 0x000815A0
		public override void add_onrowexit(HTMLScriptEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182F1 RID: 99057 RVA: 0x00082630 File Offset: 0x00081630
		public override void remove_onrowexit(HTMLScriptEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182F2 RID: 99058 RVA: 0x00082720 File Offset: 0x00081720
		public override void add_onerrorupdate(HTMLScriptEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182F3 RID: 99059 RVA: 0x000827B0 File Offset: 0x000817B0
		public override void remove_onerrorupdate(HTMLScriptEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182F4 RID: 99060 RVA: 0x000828A0 File Offset: 0x000818A0
		public override void add_onafterupdate(HTMLScriptEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182F5 RID: 99061 RVA: 0x00082930 File Offset: 0x00081930
		public override void remove_onafterupdate(HTMLScriptEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182F6 RID: 99062 RVA: 0x00082A20 File Offset: 0x00081A20
		public override void add_onbeforeupdate(HTMLScriptEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182F7 RID: 99063 RVA: 0x00082AB0 File Offset: 0x00081AB0
		public override void remove_onbeforeupdate(HTMLScriptEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182F8 RID: 99064 RVA: 0x00082BA0 File Offset: 0x00081BA0
		public override void add_ondragstart(HTMLScriptEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182F9 RID: 99065 RVA: 0x00082C30 File Offset: 0x00081C30
		public override void remove_ondragstart(HTMLScriptEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182FA RID: 99066 RVA: 0x00082D20 File Offset: 0x00081D20
		public override void add_onfilterchange(HTMLScriptEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182FB RID: 99067 RVA: 0x00082DB0 File Offset: 0x00081DB0
		public override void remove_onfilterchange(HTMLScriptEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182FC RID: 99068 RVA: 0x00082EA0 File Offset: 0x00081EA0
		public override void add_onselectstart(HTMLScriptEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182FD RID: 99069 RVA: 0x00082F30 File Offset: 0x00081F30
		public override void remove_onselectstart(HTMLScriptEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060182FE RID: 99070 RVA: 0x00083020 File Offset: 0x00082020
		public override void add_onmouseup(HTMLScriptEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x060182FF RID: 99071 RVA: 0x000830B0 File Offset: 0x000820B0
		public override void remove_onmouseup(HTMLScriptEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018300 RID: 99072 RVA: 0x000831A0 File Offset: 0x000821A0
		public override void add_onmousedown(HTMLScriptEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018301 RID: 99073 RVA: 0x00083230 File Offset: 0x00082230
		public override void remove_onmousedown(HTMLScriptEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018302 RID: 99074 RVA: 0x00083320 File Offset: 0x00082320
		public override void add_onmousemove(HTMLScriptEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018303 RID: 99075 RVA: 0x000833B0 File Offset: 0x000823B0
		public override void remove_onmousemove(HTMLScriptEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018304 RID: 99076 RVA: 0x000834A0 File Offset: 0x000824A0
		public override void add_onmouseover(HTMLScriptEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018305 RID: 99077 RVA: 0x00083530 File Offset: 0x00082530
		public override void remove_onmouseover(HTMLScriptEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018306 RID: 99078 RVA: 0x00083620 File Offset: 0x00082620
		public override void add_onmouseout(HTMLScriptEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018307 RID: 99079 RVA: 0x000836B0 File Offset: 0x000826B0
		public override void remove_onmouseout(HTMLScriptEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018308 RID: 99080 RVA: 0x000837A0 File Offset: 0x000827A0
		public override void add_onkeyup(HTMLScriptEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018309 RID: 99081 RVA: 0x00083830 File Offset: 0x00082830
		public override void remove_onkeyup(HTMLScriptEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601830A RID: 99082 RVA: 0x00083920 File Offset: 0x00082920
		public override void add_onkeydown(HTMLScriptEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601830B RID: 99083 RVA: 0x000839B0 File Offset: 0x000829B0
		public override void remove_onkeydown(HTMLScriptEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601830C RID: 99084 RVA: 0x00083AA0 File Offset: 0x00082AA0
		public override void add_onkeypress(HTMLScriptEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601830D RID: 99085 RVA: 0x00083B30 File Offset: 0x00082B30
		public override void remove_onkeypress(HTMLScriptEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601830E RID: 99086 RVA: 0x00083C20 File Offset: 0x00082C20
		public override void add_ondblclick(HTMLScriptEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x0601830F RID: 99087 RVA: 0x00083CB0 File Offset: 0x00082CB0
		public override void remove_ondblclick(HTMLScriptEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlscriptEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018310 RID: 99088 RVA: 0x00083DA0 File Offset: 0x00082DA0
		public override void add_onclick(HTMLScriptEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018311 RID: 99089 RVA: 0x00083E30 File Offset: 0x00082E30
		public override void remove_onclick(HTMLScriptEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onclickDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018312 RID: 99090 RVA: 0x00083F20 File Offset: 0x00082F20
		public override void add_onhelp(HTMLScriptEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = new HTMLScriptEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlscriptEvents2_SinkHelper, ref num);
				htmlscriptEvents2_SinkHelper.m_dwCookie = num;
				htmlscriptEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlscriptEvents2_SinkHelper);
			}
		}

		// Token: 0x06018313 RID: 99091 RVA: 0x00083FB0 File Offset: 0x00082FB0
		public override void remove_onhelp(HTMLScriptEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper;
					for (;;)
					{
						htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlscriptEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlscriptEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018314 RID: 99092 RVA: 0x000840A0 File Offset: 0x000830A0
		public HTMLScriptEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018315 RID: 99093 RVA: 0x000840C8 File Offset: 0x000830C8
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
								HTMLScriptEvents2_SinkHelper htmlscriptEvents2_SinkHelper = (HTMLScriptEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlscriptEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018316 RID: 99094 RVA: 0x0008417C File Offset: 0x0008317C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040009AB RID: 2475
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040009AC RID: 2476
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040009AD RID: 2477
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
