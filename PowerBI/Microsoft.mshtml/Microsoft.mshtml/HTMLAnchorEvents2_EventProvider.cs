using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DEB RID: 3563
	internal sealed class HTMLAnchorEvents2_EventProvider : HTMLAnchorEvents2_Event, IDisposable
	{
		// Token: 0x0601835E RID: 99166 RVA: 0x000853BC File Offset: 0x000843BC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				16, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601835F RID: 99167 RVA: 0x000854D0 File Offset: 0x000844D0
		public override void add_onmousewheel(HTMLAnchorEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018360 RID: 99168 RVA: 0x00085560 File Offset: 0x00084560
		public override void remove_onmousewheel(HTMLAnchorEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018361 RID: 99169 RVA: 0x00085650 File Offset: 0x00084650
		public override void add_onresizeend(HTMLAnchorEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018362 RID: 99170 RVA: 0x000856E0 File Offset: 0x000846E0
		public override void remove_onresizeend(HTMLAnchorEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018363 RID: 99171 RVA: 0x000857D0 File Offset: 0x000847D0
		public override void add_onresizestart(HTMLAnchorEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018364 RID: 99172 RVA: 0x00085860 File Offset: 0x00084860
		public override void remove_onresizestart(HTMLAnchorEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018365 RID: 99173 RVA: 0x00085950 File Offset: 0x00084950
		public override void add_onmoveend(HTMLAnchorEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018366 RID: 99174 RVA: 0x000859E0 File Offset: 0x000849E0
		public override void remove_onmoveend(HTMLAnchorEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018367 RID: 99175 RVA: 0x00085AD0 File Offset: 0x00084AD0
		public override void add_onmovestart(HTMLAnchorEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018368 RID: 99176 RVA: 0x00085B60 File Offset: 0x00084B60
		public override void remove_onmovestart(HTMLAnchorEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018369 RID: 99177 RVA: 0x00085C50 File Offset: 0x00084C50
		public override void add_oncontrolselect(HTMLAnchorEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601836A RID: 99178 RVA: 0x00085CE0 File Offset: 0x00084CE0
		public override void remove_oncontrolselect(HTMLAnchorEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlanchorEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601836B RID: 99179 RVA: 0x00085DD0 File Offset: 0x00084DD0
		public override void add_onmove(HTMLAnchorEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601836C RID: 99180 RVA: 0x00085E60 File Offset: 0x00084E60
		public override void remove_onmove(HTMLAnchorEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601836D RID: 99181 RVA: 0x00085F50 File Offset: 0x00084F50
		public override void add_onfocusout(HTMLAnchorEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601836E RID: 99182 RVA: 0x00085FE0 File Offset: 0x00084FE0
		public override void remove_onfocusout(HTMLAnchorEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601836F RID: 99183 RVA: 0x000860D0 File Offset: 0x000850D0
		public override void add_onfocusin(HTMLAnchorEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018370 RID: 99184 RVA: 0x00086160 File Offset: 0x00085160
		public override void remove_onfocusin(HTMLAnchorEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018371 RID: 99185 RVA: 0x00086250 File Offset: 0x00085250
		public override void add_onbeforeactivate(HTMLAnchorEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018372 RID: 99186 RVA: 0x000862E0 File Offset: 0x000852E0
		public override void remove_onbeforeactivate(HTMLAnchorEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018373 RID: 99187 RVA: 0x000863D0 File Offset: 0x000853D0
		public override void add_onbeforedeactivate(HTMLAnchorEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018374 RID: 99188 RVA: 0x00086460 File Offset: 0x00085460
		public override void remove_onbeforedeactivate(HTMLAnchorEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018375 RID: 99189 RVA: 0x00086550 File Offset: 0x00085550
		public override void add_ondeactivate(HTMLAnchorEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018376 RID: 99190 RVA: 0x000865E0 File Offset: 0x000855E0
		public override void remove_ondeactivate(HTMLAnchorEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018377 RID: 99191 RVA: 0x000866D0 File Offset: 0x000856D0
		public override void add_onactivate(HTMLAnchorEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018378 RID: 99192 RVA: 0x00086760 File Offset: 0x00085760
		public override void remove_onactivate(HTMLAnchorEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018379 RID: 99193 RVA: 0x00086850 File Offset: 0x00085850
		public override void add_onmouseleave(HTMLAnchorEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601837A RID: 99194 RVA: 0x000868E0 File Offset: 0x000858E0
		public override void remove_onmouseleave(HTMLAnchorEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601837B RID: 99195 RVA: 0x000869D0 File Offset: 0x000859D0
		public override void add_onmouseenter(HTMLAnchorEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601837C RID: 99196 RVA: 0x00086A60 File Offset: 0x00085A60
		public override void remove_onmouseenter(HTMLAnchorEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601837D RID: 99197 RVA: 0x00086B50 File Offset: 0x00085B50
		public override void add_onpage(HTMLAnchorEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601837E RID: 99198 RVA: 0x00086BE0 File Offset: 0x00085BE0
		public override void remove_onpage(HTMLAnchorEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onpageDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601837F RID: 99199 RVA: 0x00086CD0 File Offset: 0x00085CD0
		public override void add_onlayoutcomplete(HTMLAnchorEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018380 RID: 99200 RVA: 0x00086D60 File Offset: 0x00085D60
		public override void remove_onlayoutcomplete(HTMLAnchorEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018381 RID: 99201 RVA: 0x00086E50 File Offset: 0x00085E50
		public override void add_onreadystatechange(HTMLAnchorEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018382 RID: 99202 RVA: 0x00086EE0 File Offset: 0x00085EE0
		public override void remove_onreadystatechange(HTMLAnchorEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018383 RID: 99203 RVA: 0x00086FD0 File Offset: 0x00085FD0
		public override void add_oncellchange(HTMLAnchorEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018384 RID: 99204 RVA: 0x00087060 File Offset: 0x00086060
		public override void remove_oncellchange(HTMLAnchorEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlanchorEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018385 RID: 99205 RVA: 0x00087150 File Offset: 0x00086150
		public override void add_onrowsinserted(HTMLAnchorEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018386 RID: 99206 RVA: 0x000871E0 File Offset: 0x000861E0
		public override void remove_onrowsinserted(HTMLAnchorEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018387 RID: 99207 RVA: 0x000872D0 File Offset: 0x000862D0
		public override void add_onrowsdelete(HTMLAnchorEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018388 RID: 99208 RVA: 0x00087360 File Offset: 0x00086360
		public override void remove_onrowsdelete(HTMLAnchorEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018389 RID: 99209 RVA: 0x00087450 File Offset: 0x00086450
		public override void add_oncontextmenu(HTMLAnchorEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601838A RID: 99210 RVA: 0x000874E0 File Offset: 0x000864E0
		public override void remove_oncontextmenu(HTMLAnchorEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlanchorEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601838B RID: 99211 RVA: 0x000875D0 File Offset: 0x000865D0
		public override void add_onpaste(HTMLAnchorEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601838C RID: 99212 RVA: 0x00087660 File Offset: 0x00086660
		public override void remove_onpaste(HTMLAnchorEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601838D RID: 99213 RVA: 0x00087750 File Offset: 0x00086750
		public override void add_onbeforepaste(HTMLAnchorEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601838E RID: 99214 RVA: 0x000877E0 File Offset: 0x000867E0
		public override void remove_onbeforepaste(HTMLAnchorEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601838F RID: 99215 RVA: 0x000878D0 File Offset: 0x000868D0
		public override void add_oncopy(HTMLAnchorEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018390 RID: 99216 RVA: 0x00087960 File Offset: 0x00086960
		public override void remove_oncopy(HTMLAnchorEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlanchorEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018391 RID: 99217 RVA: 0x00087A50 File Offset: 0x00086A50
		public override void add_onbeforecopy(HTMLAnchorEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018392 RID: 99218 RVA: 0x00087AE0 File Offset: 0x00086AE0
		public override void remove_onbeforecopy(HTMLAnchorEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018393 RID: 99219 RVA: 0x00087BD0 File Offset: 0x00086BD0
		public override void add_oncut(HTMLAnchorEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018394 RID: 99220 RVA: 0x00087C60 File Offset: 0x00086C60
		public override void remove_oncut(HTMLAnchorEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_oncutDelegate != null && ((htmlanchorEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018395 RID: 99221 RVA: 0x00087D50 File Offset: 0x00086D50
		public override void add_onbeforecut(HTMLAnchorEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018396 RID: 99222 RVA: 0x00087DE0 File Offset: 0x00086DE0
		public override void remove_onbeforecut(HTMLAnchorEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018397 RID: 99223 RVA: 0x00087ED0 File Offset: 0x00086ED0
		public override void add_ondrop(HTMLAnchorEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x06018398 RID: 99224 RVA: 0x00087F60 File Offset: 0x00086F60
		public override void remove_ondrop(HTMLAnchorEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondropDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018399 RID: 99225 RVA: 0x00088050 File Offset: 0x00087050
		public override void add_ondragleave(HTMLAnchorEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601839A RID: 99226 RVA: 0x000880E0 File Offset: 0x000870E0
		public override void remove_ondragleave(HTMLAnchorEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601839B RID: 99227 RVA: 0x000881D0 File Offset: 0x000871D0
		public override void add_ondragover(HTMLAnchorEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601839C RID: 99228 RVA: 0x00088260 File Offset: 0x00087260
		public override void remove_ondragover(HTMLAnchorEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601839D RID: 99229 RVA: 0x00088350 File Offset: 0x00087350
		public override void add_ondragenter(HTMLAnchorEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x0601839E RID: 99230 RVA: 0x000883E0 File Offset: 0x000873E0
		public override void remove_ondragenter(HTMLAnchorEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601839F RID: 99231 RVA: 0x000884D0 File Offset: 0x000874D0
		public override void add_ondragend(HTMLAnchorEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183A0 RID: 99232 RVA: 0x00088560 File Offset: 0x00087560
		public override void remove_ondragend(HTMLAnchorEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183A1 RID: 99233 RVA: 0x00088650 File Offset: 0x00087650
		public override void add_ondrag(HTMLAnchorEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183A2 RID: 99234 RVA: 0x000886E0 File Offset: 0x000876E0
		public override void remove_ondrag(HTMLAnchorEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183A3 RID: 99235 RVA: 0x000887D0 File Offset: 0x000877D0
		public override void add_onresize(HTMLAnchorEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183A4 RID: 99236 RVA: 0x00088860 File Offset: 0x00087860
		public override void remove_onresize(HTMLAnchorEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183A5 RID: 99237 RVA: 0x00088950 File Offset: 0x00087950
		public override void add_onblur(HTMLAnchorEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183A6 RID: 99238 RVA: 0x000889E0 File Offset: 0x000879E0
		public override void remove_onblur(HTMLAnchorEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onblurDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183A7 RID: 99239 RVA: 0x00088AD0 File Offset: 0x00087AD0
		public override void add_onfocus(HTMLAnchorEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183A8 RID: 99240 RVA: 0x00088B60 File Offset: 0x00087B60
		public override void remove_onfocus(HTMLAnchorEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183A9 RID: 99241 RVA: 0x00088C50 File Offset: 0x00087C50
		public override void add_onscroll(HTMLAnchorEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183AA RID: 99242 RVA: 0x00088CE0 File Offset: 0x00087CE0
		public override void remove_onscroll(HTMLAnchorEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183AB RID: 99243 RVA: 0x00088DD0 File Offset: 0x00087DD0
		public override void add_onpropertychange(HTMLAnchorEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183AC RID: 99244 RVA: 0x00088E60 File Offset: 0x00087E60
		public override void remove_onpropertychange(HTMLAnchorEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183AD RID: 99245 RVA: 0x00088F50 File Offset: 0x00087F50
		public override void add_onlosecapture(HTMLAnchorEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183AE RID: 99246 RVA: 0x00088FE0 File Offset: 0x00087FE0
		public override void remove_onlosecapture(HTMLAnchorEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183AF RID: 99247 RVA: 0x000890D0 File Offset: 0x000880D0
		public override void add_ondatasetcomplete(HTMLAnchorEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183B0 RID: 99248 RVA: 0x00089160 File Offset: 0x00088160
		public override void remove_ondatasetcomplete(HTMLAnchorEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183B1 RID: 99249 RVA: 0x00089250 File Offset: 0x00088250
		public override void add_ondataavailable(HTMLAnchorEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183B2 RID: 99250 RVA: 0x000892E0 File Offset: 0x000882E0
		public override void remove_ondataavailable(HTMLAnchorEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183B3 RID: 99251 RVA: 0x000893D0 File Offset: 0x000883D0
		public override void add_ondatasetchanged(HTMLAnchorEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183B4 RID: 99252 RVA: 0x00089460 File Offset: 0x00088460
		public override void remove_ondatasetchanged(HTMLAnchorEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183B5 RID: 99253 RVA: 0x00089550 File Offset: 0x00088550
		public override void add_onrowenter(HTMLAnchorEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183B6 RID: 99254 RVA: 0x000895E0 File Offset: 0x000885E0
		public override void remove_onrowenter(HTMLAnchorEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183B7 RID: 99255 RVA: 0x000896D0 File Offset: 0x000886D0
		public override void add_onrowexit(HTMLAnchorEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183B8 RID: 99256 RVA: 0x00089760 File Offset: 0x00088760
		public override void remove_onrowexit(HTMLAnchorEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183B9 RID: 99257 RVA: 0x00089850 File Offset: 0x00088850
		public override void add_onerrorupdate(HTMLAnchorEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183BA RID: 99258 RVA: 0x000898E0 File Offset: 0x000888E0
		public override void remove_onerrorupdate(HTMLAnchorEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183BB RID: 99259 RVA: 0x000899D0 File Offset: 0x000889D0
		public override void add_onafterupdate(HTMLAnchorEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183BC RID: 99260 RVA: 0x00089A60 File Offset: 0x00088A60
		public override void remove_onafterupdate(HTMLAnchorEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183BD RID: 99261 RVA: 0x00089B50 File Offset: 0x00088B50
		public override void add_onbeforeupdate(HTMLAnchorEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183BE RID: 99262 RVA: 0x00089BE0 File Offset: 0x00088BE0
		public override void remove_onbeforeupdate(HTMLAnchorEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183BF RID: 99263 RVA: 0x00089CD0 File Offset: 0x00088CD0
		public override void add_ondragstart(HTMLAnchorEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183C0 RID: 99264 RVA: 0x00089D60 File Offset: 0x00088D60
		public override void remove_ondragstart(HTMLAnchorEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183C1 RID: 99265 RVA: 0x00089E50 File Offset: 0x00088E50
		public override void add_onfilterchange(HTMLAnchorEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183C2 RID: 99266 RVA: 0x00089EE0 File Offset: 0x00088EE0
		public override void remove_onfilterchange(HTMLAnchorEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183C3 RID: 99267 RVA: 0x00089FD0 File Offset: 0x00088FD0
		public override void add_onselectstart(HTMLAnchorEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183C4 RID: 99268 RVA: 0x0008A060 File Offset: 0x00089060
		public override void remove_onselectstart(HTMLAnchorEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183C5 RID: 99269 RVA: 0x0008A150 File Offset: 0x00089150
		public override void add_onmouseup(HTMLAnchorEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183C6 RID: 99270 RVA: 0x0008A1E0 File Offset: 0x000891E0
		public override void remove_onmouseup(HTMLAnchorEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183C7 RID: 99271 RVA: 0x0008A2D0 File Offset: 0x000892D0
		public override void add_onmousedown(HTMLAnchorEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183C8 RID: 99272 RVA: 0x0008A360 File Offset: 0x00089360
		public override void remove_onmousedown(HTMLAnchorEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183C9 RID: 99273 RVA: 0x0008A450 File Offset: 0x00089450
		public override void add_onmousemove(HTMLAnchorEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183CA RID: 99274 RVA: 0x0008A4E0 File Offset: 0x000894E0
		public override void remove_onmousemove(HTMLAnchorEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183CB RID: 99275 RVA: 0x0008A5D0 File Offset: 0x000895D0
		public override void add_onmouseover(HTMLAnchorEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183CC RID: 99276 RVA: 0x0008A660 File Offset: 0x00089660
		public override void remove_onmouseover(HTMLAnchorEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183CD RID: 99277 RVA: 0x0008A750 File Offset: 0x00089750
		public override void add_onmouseout(HTMLAnchorEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183CE RID: 99278 RVA: 0x0008A7E0 File Offset: 0x000897E0
		public override void remove_onmouseout(HTMLAnchorEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183CF RID: 99279 RVA: 0x0008A8D0 File Offset: 0x000898D0
		public override void add_onkeyup(HTMLAnchorEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183D0 RID: 99280 RVA: 0x0008A960 File Offset: 0x00089960
		public override void remove_onkeyup(HTMLAnchorEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183D1 RID: 99281 RVA: 0x0008AA50 File Offset: 0x00089A50
		public override void add_onkeydown(HTMLAnchorEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183D2 RID: 99282 RVA: 0x0008AAE0 File Offset: 0x00089AE0
		public override void remove_onkeydown(HTMLAnchorEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183D3 RID: 99283 RVA: 0x0008ABD0 File Offset: 0x00089BD0
		public override void add_onkeypress(HTMLAnchorEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183D4 RID: 99284 RVA: 0x0008AC60 File Offset: 0x00089C60
		public override void remove_onkeypress(HTMLAnchorEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183D5 RID: 99285 RVA: 0x0008AD50 File Offset: 0x00089D50
		public override void add_ondblclick(HTMLAnchorEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183D6 RID: 99286 RVA: 0x0008ADE0 File Offset: 0x00089DE0
		public override void remove_ondblclick(HTMLAnchorEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlanchorEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183D7 RID: 99287 RVA: 0x0008AED0 File Offset: 0x00089ED0
		public override void add_onclick(HTMLAnchorEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183D8 RID: 99288 RVA: 0x0008AF60 File Offset: 0x00089F60
		public override void remove_onclick(HTMLAnchorEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onclickDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183D9 RID: 99289 RVA: 0x0008B050 File Offset: 0x0008A050
		public override void add_onhelp(HTMLAnchorEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = new HTMLAnchorEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlanchorEvents2_SinkHelper, ref num);
				htmlanchorEvents2_SinkHelper.m_dwCookie = num;
				htmlanchorEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlanchorEvents2_SinkHelper);
			}
		}

		// Token: 0x060183DA RID: 99290 RVA: 0x0008B0E0 File Offset: 0x0008A0E0
		public override void remove_onhelp(HTMLAnchorEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper;
					for (;;)
					{
						htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlanchorEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlanchorEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060183DB RID: 99291 RVA: 0x0008B1D0 File Offset: 0x0008A1D0
		public HTMLAnchorEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x060183DC RID: 99292 RVA: 0x0008B1F8 File Offset: 0x0008A1F8
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
								HTMLAnchorEvents2_SinkHelper htmlanchorEvents2_SinkHelper = (HTMLAnchorEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlanchorEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x060183DD RID: 99293 RVA: 0x0008B2AC File Offset: 0x0008A2AC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040009F2 RID: 2546
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040009F3 RID: 2547
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040009F4 RID: 2548
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
