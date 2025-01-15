using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DFB RID: 3579
	internal sealed class HTMLElementEvents2_EventProvider : HTMLElementEvents2_Event, IDisposable
	{
		// Token: 0x060188DE RID: 100574 RVA: 0x000B75AC File Offset: 0x000B65AC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				15, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060188DF RID: 100575 RVA: 0x000B76C0 File Offset: 0x000B66C0
		public override void add_onmousewheel(HTMLElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188E0 RID: 100576 RVA: 0x000B7750 File Offset: 0x000B6750
		public override void remove_onmousewheel(HTMLElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188E1 RID: 100577 RVA: 0x000B7840 File Offset: 0x000B6840
		public override void add_onresizeend(HTMLElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188E2 RID: 100578 RVA: 0x000B78D0 File Offset: 0x000B68D0
		public override void remove_onresizeend(HTMLElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlelementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188E3 RID: 100579 RVA: 0x000B79C0 File Offset: 0x000B69C0
		public override void add_onresizestart(HTMLElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188E4 RID: 100580 RVA: 0x000B7A50 File Offset: 0x000B6A50
		public override void remove_onresizestart(HTMLElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlelementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188E5 RID: 100581 RVA: 0x000B7B40 File Offset: 0x000B6B40
		public override void add_onmoveend(HTMLElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188E6 RID: 100582 RVA: 0x000B7BD0 File Offset: 0x000B6BD0
		public override void remove_onmoveend(HTMLElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188E7 RID: 100583 RVA: 0x000B7CC0 File Offset: 0x000B6CC0
		public override void add_onmovestart(HTMLElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188E8 RID: 100584 RVA: 0x000B7D50 File Offset: 0x000B6D50
		public override void remove_onmovestart(HTMLElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188E9 RID: 100585 RVA: 0x000B7E40 File Offset: 0x000B6E40
		public override void add_oncontrolselect(HTMLElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188EA RID: 100586 RVA: 0x000B7ED0 File Offset: 0x000B6ED0
		public override void remove_oncontrolselect(HTMLElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlelementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188EB RID: 100587 RVA: 0x000B7FC0 File Offset: 0x000B6FC0
		public override void add_onmove(HTMLElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188EC RID: 100588 RVA: 0x000B8050 File Offset: 0x000B7050
		public override void remove_onmove(HTMLElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188ED RID: 100589 RVA: 0x000B8140 File Offset: 0x000B7140
		public override void add_onfocusout(HTMLElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188EE RID: 100590 RVA: 0x000B81D0 File Offset: 0x000B71D0
		public override void remove_onfocusout(HTMLElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlelementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188EF RID: 100591 RVA: 0x000B82C0 File Offset: 0x000B72C0
		public override void add_onfocusin(HTMLElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188F0 RID: 100592 RVA: 0x000B8350 File Offset: 0x000B7350
		public override void remove_onfocusin(HTMLElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlelementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188F1 RID: 100593 RVA: 0x000B8440 File Offset: 0x000B7440
		public override void add_onbeforeactivate(HTMLElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188F2 RID: 100594 RVA: 0x000B84D0 File Offset: 0x000B74D0
		public override void remove_onbeforeactivate(HTMLElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188F3 RID: 100595 RVA: 0x000B85C0 File Offset: 0x000B75C0
		public override void add_onbeforedeactivate(HTMLElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188F4 RID: 100596 RVA: 0x000B8650 File Offset: 0x000B7650
		public override void remove_onbeforedeactivate(HTMLElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188F5 RID: 100597 RVA: 0x000B8740 File Offset: 0x000B7740
		public override void add_ondeactivate(HTMLElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188F6 RID: 100598 RVA: 0x000B87D0 File Offset: 0x000B77D0
		public override void remove_ondeactivate(HTMLElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188F7 RID: 100599 RVA: 0x000B88C0 File Offset: 0x000B78C0
		public override void add_onactivate(HTMLElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188F8 RID: 100600 RVA: 0x000B8950 File Offset: 0x000B7950
		public override void remove_onactivate(HTMLElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188F9 RID: 100601 RVA: 0x000B8A40 File Offset: 0x000B7A40
		public override void add_onmouseleave(HTMLElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188FA RID: 100602 RVA: 0x000B8AD0 File Offset: 0x000B7AD0
		public override void remove_onmouseleave(HTMLElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188FB RID: 100603 RVA: 0x000B8BC0 File Offset: 0x000B7BC0
		public override void add_onmouseenter(HTMLElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188FC RID: 100604 RVA: 0x000B8C50 File Offset: 0x000B7C50
		public override void remove_onmouseenter(HTMLElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188FD RID: 100605 RVA: 0x000B8D40 File Offset: 0x000B7D40
		public override void add_onpage(HTMLElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x060188FE RID: 100606 RVA: 0x000B8DD0 File Offset: 0x000B7DD0
		public override void remove_onpage(HTMLElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlelementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060188FF RID: 100607 RVA: 0x000B8EC0 File Offset: 0x000B7EC0
		public override void add_onlayoutcomplete(HTMLElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018900 RID: 100608 RVA: 0x000B8F50 File Offset: 0x000B7F50
		public override void remove_onlayoutcomplete(HTMLElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlelementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018901 RID: 100609 RVA: 0x000B9040 File Offset: 0x000B8040
		public override void add_onreadystatechange(HTMLElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018902 RID: 100610 RVA: 0x000B90D0 File Offset: 0x000B80D0
		public override void remove_onreadystatechange(HTMLElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlelementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018903 RID: 100611 RVA: 0x000B91C0 File Offset: 0x000B81C0
		public override void add_oncellchange(HTMLElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018904 RID: 100612 RVA: 0x000B9250 File Offset: 0x000B8250
		public override void remove_oncellchange(HTMLElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlelementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018905 RID: 100613 RVA: 0x000B9340 File Offset: 0x000B8340
		public override void add_onrowsinserted(HTMLElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018906 RID: 100614 RVA: 0x000B93D0 File Offset: 0x000B83D0
		public override void remove_onrowsinserted(HTMLElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlelementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018907 RID: 100615 RVA: 0x000B94C0 File Offset: 0x000B84C0
		public override void add_onrowsdelete(HTMLElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018908 RID: 100616 RVA: 0x000B9550 File Offset: 0x000B8550
		public override void remove_onrowsdelete(HTMLElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlelementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018909 RID: 100617 RVA: 0x000B9640 File Offset: 0x000B8640
		public override void add_oncontextmenu(HTMLElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601890A RID: 100618 RVA: 0x000B96D0 File Offset: 0x000B86D0
		public override void remove_oncontextmenu(HTMLElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlelementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601890B RID: 100619 RVA: 0x000B97C0 File Offset: 0x000B87C0
		public override void add_onpaste(HTMLElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601890C RID: 100620 RVA: 0x000B9850 File Offset: 0x000B8850
		public override void remove_onpaste(HTMLElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlelementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601890D RID: 100621 RVA: 0x000B9940 File Offset: 0x000B8940
		public override void add_onbeforepaste(HTMLElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601890E RID: 100622 RVA: 0x000B99D0 File Offset: 0x000B89D0
		public override void remove_onbeforepaste(HTMLElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601890F RID: 100623 RVA: 0x000B9AC0 File Offset: 0x000B8AC0
		public override void add_oncopy(HTMLElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018910 RID: 100624 RVA: 0x000B9B50 File Offset: 0x000B8B50
		public override void remove_oncopy(HTMLElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlelementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018911 RID: 100625 RVA: 0x000B9C40 File Offset: 0x000B8C40
		public override void add_onbeforecopy(HTMLElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018912 RID: 100626 RVA: 0x000B9CD0 File Offset: 0x000B8CD0
		public override void remove_onbeforecopy(HTMLElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018913 RID: 100627 RVA: 0x000B9DC0 File Offset: 0x000B8DC0
		public override void add_oncut(HTMLElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018914 RID: 100628 RVA: 0x000B9E50 File Offset: 0x000B8E50
		public override void remove_oncut(HTMLElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlelementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018915 RID: 100629 RVA: 0x000B9F40 File Offset: 0x000B8F40
		public override void add_onbeforecut(HTMLElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018916 RID: 100630 RVA: 0x000B9FD0 File Offset: 0x000B8FD0
		public override void remove_onbeforecut(HTMLElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018917 RID: 100631 RVA: 0x000BA0C0 File Offset: 0x000B90C0
		public override void add_ondrop(HTMLElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018918 RID: 100632 RVA: 0x000BA150 File Offset: 0x000B9150
		public override void remove_ondrop(HTMLElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018919 RID: 100633 RVA: 0x000BA240 File Offset: 0x000B9240
		public override void add_ondragleave(HTMLElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601891A RID: 100634 RVA: 0x000BA2D0 File Offset: 0x000B92D0
		public override void remove_ondragleave(HTMLElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601891B RID: 100635 RVA: 0x000BA3C0 File Offset: 0x000B93C0
		public override void add_ondragover(HTMLElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601891C RID: 100636 RVA: 0x000BA450 File Offset: 0x000B9450
		public override void remove_ondragover(HTMLElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601891D RID: 100637 RVA: 0x000BA540 File Offset: 0x000B9540
		public override void add_ondragenter(HTMLElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601891E RID: 100638 RVA: 0x000BA5D0 File Offset: 0x000B95D0
		public override void remove_ondragenter(HTMLElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601891F RID: 100639 RVA: 0x000BA6C0 File Offset: 0x000B96C0
		public override void add_ondragend(HTMLElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018920 RID: 100640 RVA: 0x000BA750 File Offset: 0x000B9750
		public override void remove_ondragend(HTMLElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018921 RID: 100641 RVA: 0x000BA840 File Offset: 0x000B9840
		public override void add_ondrag(HTMLElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018922 RID: 100642 RVA: 0x000BA8D0 File Offset: 0x000B98D0
		public override void remove_ondrag(HTMLElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018923 RID: 100643 RVA: 0x000BA9C0 File Offset: 0x000B99C0
		public override void add_onresize(HTMLElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018924 RID: 100644 RVA: 0x000BAA50 File Offset: 0x000B9A50
		public override void remove_onresize(HTMLElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlelementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018925 RID: 100645 RVA: 0x000BAB40 File Offset: 0x000B9B40
		public override void add_onblur(HTMLElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018926 RID: 100646 RVA: 0x000BABD0 File Offset: 0x000B9BD0
		public override void remove_onblur(HTMLElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlelementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018927 RID: 100647 RVA: 0x000BACC0 File Offset: 0x000B9CC0
		public override void add_onfocus(HTMLElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018928 RID: 100648 RVA: 0x000BAD50 File Offset: 0x000B9D50
		public override void remove_onfocus(HTMLElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlelementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018929 RID: 100649 RVA: 0x000BAE40 File Offset: 0x000B9E40
		public override void add_onscroll(HTMLElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601892A RID: 100650 RVA: 0x000BAED0 File Offset: 0x000B9ED0
		public override void remove_onscroll(HTMLElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlelementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601892B RID: 100651 RVA: 0x000BAFC0 File Offset: 0x000B9FC0
		public override void add_onpropertychange(HTMLElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601892C RID: 100652 RVA: 0x000BB050 File Offset: 0x000BA050
		public override void remove_onpropertychange(HTMLElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlelementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601892D RID: 100653 RVA: 0x000BB140 File Offset: 0x000BA140
		public override void add_onlosecapture(HTMLElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601892E RID: 100654 RVA: 0x000BB1D0 File Offset: 0x000BA1D0
		public override void remove_onlosecapture(HTMLElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlelementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601892F RID: 100655 RVA: 0x000BB2C0 File Offset: 0x000BA2C0
		public override void add_ondatasetcomplete(HTMLElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018930 RID: 100656 RVA: 0x000BB350 File Offset: 0x000BA350
		public override void remove_ondatasetcomplete(HTMLElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018931 RID: 100657 RVA: 0x000BB440 File Offset: 0x000BA440
		public override void add_ondataavailable(HTMLElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018932 RID: 100658 RVA: 0x000BB4D0 File Offset: 0x000BA4D0
		public override void remove_ondataavailable(HTMLElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018933 RID: 100659 RVA: 0x000BB5C0 File Offset: 0x000BA5C0
		public override void add_ondatasetchanged(HTMLElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018934 RID: 100660 RVA: 0x000BB650 File Offset: 0x000BA650
		public override void remove_ondatasetchanged(HTMLElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018935 RID: 100661 RVA: 0x000BB740 File Offset: 0x000BA740
		public override void add_onrowenter(HTMLElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018936 RID: 100662 RVA: 0x000BB7D0 File Offset: 0x000BA7D0
		public override void remove_onrowenter(HTMLElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlelementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018937 RID: 100663 RVA: 0x000BB8C0 File Offset: 0x000BA8C0
		public override void add_onrowexit(HTMLElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018938 RID: 100664 RVA: 0x000BB950 File Offset: 0x000BA950
		public override void remove_onrowexit(HTMLElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlelementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018939 RID: 100665 RVA: 0x000BBA40 File Offset: 0x000BAA40
		public override void add_onerrorupdate(HTMLElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601893A RID: 100666 RVA: 0x000BBAD0 File Offset: 0x000BAAD0
		public override void remove_onerrorupdate(HTMLElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601893B RID: 100667 RVA: 0x000BBBC0 File Offset: 0x000BABC0
		public override void add_onafterupdate(HTMLElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601893C RID: 100668 RVA: 0x000BBC50 File Offset: 0x000BAC50
		public override void remove_onafterupdate(HTMLElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601893D RID: 100669 RVA: 0x000BBD40 File Offset: 0x000BAD40
		public override void add_onbeforeupdate(HTMLElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601893E RID: 100670 RVA: 0x000BBDD0 File Offset: 0x000BADD0
		public override void remove_onbeforeupdate(HTMLElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlelementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601893F RID: 100671 RVA: 0x000BBEC0 File Offset: 0x000BAEC0
		public override void add_ondragstart(HTMLElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018940 RID: 100672 RVA: 0x000BBF50 File Offset: 0x000BAF50
		public override void remove_ondragstart(HTMLElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018941 RID: 100673 RVA: 0x000BC040 File Offset: 0x000BB040
		public override void add_onfilterchange(HTMLElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018942 RID: 100674 RVA: 0x000BC0D0 File Offset: 0x000BB0D0
		public override void remove_onfilterchange(HTMLElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlelementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018943 RID: 100675 RVA: 0x000BC1C0 File Offset: 0x000BB1C0
		public override void add_onselectstart(HTMLElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018944 RID: 100676 RVA: 0x000BC250 File Offset: 0x000BB250
		public override void remove_onselectstart(HTMLElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlelementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018945 RID: 100677 RVA: 0x000BC340 File Offset: 0x000BB340
		public override void add_onmouseup(HTMLElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018946 RID: 100678 RVA: 0x000BC3D0 File Offset: 0x000BB3D0
		public override void remove_onmouseup(HTMLElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018947 RID: 100679 RVA: 0x000BC4C0 File Offset: 0x000BB4C0
		public override void add_onmousedown(HTMLElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018948 RID: 100680 RVA: 0x000BC550 File Offset: 0x000BB550
		public override void remove_onmousedown(HTMLElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018949 RID: 100681 RVA: 0x000BC640 File Offset: 0x000BB640
		public override void add_onmousemove(HTMLElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601894A RID: 100682 RVA: 0x000BC6D0 File Offset: 0x000BB6D0
		public override void remove_onmousemove(HTMLElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601894B RID: 100683 RVA: 0x000BC7C0 File Offset: 0x000BB7C0
		public override void add_onmouseover(HTMLElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601894C RID: 100684 RVA: 0x000BC850 File Offset: 0x000BB850
		public override void remove_onmouseover(HTMLElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601894D RID: 100685 RVA: 0x000BC940 File Offset: 0x000BB940
		public override void add_onmouseout(HTMLElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601894E RID: 100686 RVA: 0x000BC9D0 File Offset: 0x000BB9D0
		public override void remove_onmouseout(HTMLElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlelementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601894F RID: 100687 RVA: 0x000BCAC0 File Offset: 0x000BBAC0
		public override void add_onkeyup(HTMLElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018950 RID: 100688 RVA: 0x000BCB50 File Offset: 0x000BBB50
		public override void remove_onkeyup(HTMLElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlelementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018951 RID: 100689 RVA: 0x000BCC40 File Offset: 0x000BBC40
		public override void add_onkeydown(HTMLElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018952 RID: 100690 RVA: 0x000BCCD0 File Offset: 0x000BBCD0
		public override void remove_onkeydown(HTMLElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlelementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018953 RID: 100691 RVA: 0x000BCDC0 File Offset: 0x000BBDC0
		public override void add_onkeypress(HTMLElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018954 RID: 100692 RVA: 0x000BCE50 File Offset: 0x000BBE50
		public override void remove_onkeypress(HTMLElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlelementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018955 RID: 100693 RVA: 0x000BCF40 File Offset: 0x000BBF40
		public override void add_ondblclick(HTMLElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018956 RID: 100694 RVA: 0x000BCFD0 File Offset: 0x000BBFD0
		public override void remove_ondblclick(HTMLElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlelementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018957 RID: 100695 RVA: 0x000BD0C0 File Offset: 0x000BC0C0
		public override void add_onclick(HTMLElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018958 RID: 100696 RVA: 0x000BD150 File Offset: 0x000BC150
		public override void remove_onclick(HTMLElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlelementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018959 RID: 100697 RVA: 0x000BD240 File Offset: 0x000BC240
		public override void add_onhelp(HTMLElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = new HTMLElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents2_SinkHelper, ref num);
				htmlelementEvents2_SinkHelper.m_dwCookie = num;
				htmlelementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601895A RID: 100698 RVA: 0x000BD2D0 File Offset: 0x000BC2D0
		public override void remove_onhelp(HTMLElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper;
					for (;;)
					{
						htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlelementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601895B RID: 100699 RVA: 0x000BD3C0 File Offset: 0x000BC3C0
		public HTMLElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601895C RID: 100700 RVA: 0x000BD3E8 File Offset: 0x000BC3E8
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
								HTMLElementEvents2_SinkHelper htmlelementEvents2_SinkHelper = (HTMLElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlelementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x0601895D RID: 100701 RVA: 0x000BD49C File Offset: 0x000BC49C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000BDA RID: 3034
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000BDB RID: 3035
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000BDC RID: 3036
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
