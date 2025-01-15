using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE3 RID: 3555
	internal sealed class HTMLButtonElementEvents2_EventProvider : HTMLButtonElementEvents2_Event, IDisposable
	{
		// Token: 0x06018113 RID: 98579 RVA: 0x00070590 File Offset: 0x0006F590
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				23, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018114 RID: 98580 RVA: 0x000706A4 File Offset: 0x0006F6A4
		public override void add_onmousewheel(HTMLButtonElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018115 RID: 98581 RVA: 0x00070734 File Offset: 0x0006F734
		public override void remove_onmousewheel(HTMLButtonElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018116 RID: 98582 RVA: 0x00070824 File Offset: 0x0006F824
		public override void add_onresizeend(HTMLButtonElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018117 RID: 98583 RVA: 0x000708B4 File Offset: 0x0006F8B4
		public override void remove_onresizeend(HTMLButtonElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018118 RID: 98584 RVA: 0x000709A4 File Offset: 0x0006F9A4
		public override void add_onresizestart(HTMLButtonElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018119 RID: 98585 RVA: 0x00070A34 File Offset: 0x0006FA34
		public override void remove_onresizestart(HTMLButtonElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601811A RID: 98586 RVA: 0x00070B24 File Offset: 0x0006FB24
		public override void add_onmoveend(HTMLButtonElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601811B RID: 98587 RVA: 0x00070BB4 File Offset: 0x0006FBB4
		public override void remove_onmoveend(HTMLButtonElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601811C RID: 98588 RVA: 0x00070CA4 File Offset: 0x0006FCA4
		public override void add_onmovestart(HTMLButtonElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601811D RID: 98589 RVA: 0x00070D34 File Offset: 0x0006FD34
		public override void remove_onmovestart(HTMLButtonElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601811E RID: 98590 RVA: 0x00070E24 File Offset: 0x0006FE24
		public override void add_oncontrolselect(HTMLButtonElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601811F RID: 98591 RVA: 0x00070EB4 File Offset: 0x0006FEB4
		public override void remove_oncontrolselect(HTMLButtonElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018120 RID: 98592 RVA: 0x00070FA4 File Offset: 0x0006FFA4
		public override void add_onmove(HTMLButtonElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018121 RID: 98593 RVA: 0x00071034 File Offset: 0x00070034
		public override void remove_onmove(HTMLButtonElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018122 RID: 98594 RVA: 0x00071124 File Offset: 0x00070124
		public override void add_onfocusout(HTMLButtonElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018123 RID: 98595 RVA: 0x000711B4 File Offset: 0x000701B4
		public override void remove_onfocusout(HTMLButtonElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018124 RID: 98596 RVA: 0x000712A4 File Offset: 0x000702A4
		public override void add_onfocusin(HTMLButtonElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018125 RID: 98597 RVA: 0x00071334 File Offset: 0x00070334
		public override void remove_onfocusin(HTMLButtonElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018126 RID: 98598 RVA: 0x00071424 File Offset: 0x00070424
		public override void add_onbeforeactivate(HTMLButtonElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018127 RID: 98599 RVA: 0x000714B4 File Offset: 0x000704B4
		public override void remove_onbeforeactivate(HTMLButtonElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018128 RID: 98600 RVA: 0x000715A4 File Offset: 0x000705A4
		public override void add_onbeforedeactivate(HTMLButtonElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018129 RID: 98601 RVA: 0x00071634 File Offset: 0x00070634
		public override void remove_onbeforedeactivate(HTMLButtonElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601812A RID: 98602 RVA: 0x00071724 File Offset: 0x00070724
		public override void add_ondeactivate(HTMLButtonElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601812B RID: 98603 RVA: 0x000717B4 File Offset: 0x000707B4
		public override void remove_ondeactivate(HTMLButtonElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601812C RID: 98604 RVA: 0x000718A4 File Offset: 0x000708A4
		public override void add_onactivate(HTMLButtonElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601812D RID: 98605 RVA: 0x00071934 File Offset: 0x00070934
		public override void remove_onactivate(HTMLButtonElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601812E RID: 98606 RVA: 0x00071A24 File Offset: 0x00070A24
		public override void add_onmouseleave(HTMLButtonElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601812F RID: 98607 RVA: 0x00071AB4 File Offset: 0x00070AB4
		public override void remove_onmouseleave(HTMLButtonElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018130 RID: 98608 RVA: 0x00071BA4 File Offset: 0x00070BA4
		public override void add_onmouseenter(HTMLButtonElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018131 RID: 98609 RVA: 0x00071C34 File Offset: 0x00070C34
		public override void remove_onmouseenter(HTMLButtonElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018132 RID: 98610 RVA: 0x00071D24 File Offset: 0x00070D24
		public override void add_onpage(HTMLButtonElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018133 RID: 98611 RVA: 0x00071DB4 File Offset: 0x00070DB4
		public override void remove_onpage(HTMLButtonElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018134 RID: 98612 RVA: 0x00071EA4 File Offset: 0x00070EA4
		public override void add_onlayoutcomplete(HTMLButtonElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018135 RID: 98613 RVA: 0x00071F34 File Offset: 0x00070F34
		public override void remove_onlayoutcomplete(HTMLButtonElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018136 RID: 98614 RVA: 0x00072024 File Offset: 0x00071024
		public override void add_onreadystatechange(HTMLButtonElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018137 RID: 98615 RVA: 0x000720B4 File Offset: 0x000710B4
		public override void remove_onreadystatechange(HTMLButtonElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018138 RID: 98616 RVA: 0x000721A4 File Offset: 0x000711A4
		public override void add_oncellchange(HTMLButtonElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018139 RID: 98617 RVA: 0x00072234 File Offset: 0x00071234
		public override void remove_oncellchange(HTMLButtonElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601813A RID: 98618 RVA: 0x00072324 File Offset: 0x00071324
		public override void add_onrowsinserted(HTMLButtonElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601813B RID: 98619 RVA: 0x000723B4 File Offset: 0x000713B4
		public override void remove_onrowsinserted(HTMLButtonElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601813C RID: 98620 RVA: 0x000724A4 File Offset: 0x000714A4
		public override void add_onrowsdelete(HTMLButtonElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601813D RID: 98621 RVA: 0x00072534 File Offset: 0x00071534
		public override void remove_onrowsdelete(HTMLButtonElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601813E RID: 98622 RVA: 0x00072624 File Offset: 0x00071624
		public override void add_oncontextmenu(HTMLButtonElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601813F RID: 98623 RVA: 0x000726B4 File Offset: 0x000716B4
		public override void remove_oncontextmenu(HTMLButtonElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018140 RID: 98624 RVA: 0x000727A4 File Offset: 0x000717A4
		public override void add_onpaste(HTMLButtonElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018141 RID: 98625 RVA: 0x00072834 File Offset: 0x00071834
		public override void remove_onpaste(HTMLButtonElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018142 RID: 98626 RVA: 0x00072924 File Offset: 0x00071924
		public override void add_onbeforepaste(HTMLButtonElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018143 RID: 98627 RVA: 0x000729B4 File Offset: 0x000719B4
		public override void remove_onbeforepaste(HTMLButtonElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018144 RID: 98628 RVA: 0x00072AA4 File Offset: 0x00071AA4
		public override void add_oncopy(HTMLButtonElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018145 RID: 98629 RVA: 0x00072B34 File Offset: 0x00071B34
		public override void remove_oncopy(HTMLButtonElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018146 RID: 98630 RVA: 0x00072C24 File Offset: 0x00071C24
		public override void add_onbeforecopy(HTMLButtonElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018147 RID: 98631 RVA: 0x00072CB4 File Offset: 0x00071CB4
		public override void remove_onbeforecopy(HTMLButtonElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018148 RID: 98632 RVA: 0x00072DA4 File Offset: 0x00071DA4
		public override void add_oncut(HTMLButtonElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018149 RID: 98633 RVA: 0x00072E34 File Offset: 0x00071E34
		public override void remove_oncut(HTMLButtonElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601814A RID: 98634 RVA: 0x00072F24 File Offset: 0x00071F24
		public override void add_onbeforecut(HTMLButtonElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601814B RID: 98635 RVA: 0x00072FB4 File Offset: 0x00071FB4
		public override void remove_onbeforecut(HTMLButtonElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601814C RID: 98636 RVA: 0x000730A4 File Offset: 0x000720A4
		public override void add_ondrop(HTMLButtonElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601814D RID: 98637 RVA: 0x00073134 File Offset: 0x00072134
		public override void remove_ondrop(HTMLButtonElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601814E RID: 98638 RVA: 0x00073224 File Offset: 0x00072224
		public override void add_ondragleave(HTMLButtonElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601814F RID: 98639 RVA: 0x000732B4 File Offset: 0x000722B4
		public override void remove_ondragleave(HTMLButtonElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018150 RID: 98640 RVA: 0x000733A4 File Offset: 0x000723A4
		public override void add_ondragover(HTMLButtonElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018151 RID: 98641 RVA: 0x00073434 File Offset: 0x00072434
		public override void remove_ondragover(HTMLButtonElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018152 RID: 98642 RVA: 0x00073524 File Offset: 0x00072524
		public override void add_ondragenter(HTMLButtonElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018153 RID: 98643 RVA: 0x000735B4 File Offset: 0x000725B4
		public override void remove_ondragenter(HTMLButtonElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018154 RID: 98644 RVA: 0x000736A4 File Offset: 0x000726A4
		public override void add_ondragend(HTMLButtonElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018155 RID: 98645 RVA: 0x00073734 File Offset: 0x00072734
		public override void remove_ondragend(HTMLButtonElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018156 RID: 98646 RVA: 0x00073824 File Offset: 0x00072824
		public override void add_ondrag(HTMLButtonElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018157 RID: 98647 RVA: 0x000738B4 File Offset: 0x000728B4
		public override void remove_ondrag(HTMLButtonElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018158 RID: 98648 RVA: 0x000739A4 File Offset: 0x000729A4
		public override void add_onresize(HTMLButtonElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018159 RID: 98649 RVA: 0x00073A34 File Offset: 0x00072A34
		public override void remove_onresize(HTMLButtonElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601815A RID: 98650 RVA: 0x00073B24 File Offset: 0x00072B24
		public override void add_onblur(HTMLButtonElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601815B RID: 98651 RVA: 0x00073BB4 File Offset: 0x00072BB4
		public override void remove_onblur(HTMLButtonElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601815C RID: 98652 RVA: 0x00073CA4 File Offset: 0x00072CA4
		public override void add_onfocus(HTMLButtonElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601815D RID: 98653 RVA: 0x00073D34 File Offset: 0x00072D34
		public override void remove_onfocus(HTMLButtonElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601815E RID: 98654 RVA: 0x00073E24 File Offset: 0x00072E24
		public override void add_onscroll(HTMLButtonElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601815F RID: 98655 RVA: 0x00073EB4 File Offset: 0x00072EB4
		public override void remove_onscroll(HTMLButtonElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018160 RID: 98656 RVA: 0x00073FA4 File Offset: 0x00072FA4
		public override void add_onpropertychange(HTMLButtonElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018161 RID: 98657 RVA: 0x00074034 File Offset: 0x00073034
		public override void remove_onpropertychange(HTMLButtonElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018162 RID: 98658 RVA: 0x00074124 File Offset: 0x00073124
		public override void add_onlosecapture(HTMLButtonElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018163 RID: 98659 RVA: 0x000741B4 File Offset: 0x000731B4
		public override void remove_onlosecapture(HTMLButtonElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018164 RID: 98660 RVA: 0x000742A4 File Offset: 0x000732A4
		public override void add_ondatasetcomplete(HTMLButtonElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018165 RID: 98661 RVA: 0x00074334 File Offset: 0x00073334
		public override void remove_ondatasetcomplete(HTMLButtonElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018166 RID: 98662 RVA: 0x00074424 File Offset: 0x00073424
		public override void add_ondataavailable(HTMLButtonElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018167 RID: 98663 RVA: 0x000744B4 File Offset: 0x000734B4
		public override void remove_ondataavailable(HTMLButtonElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018168 RID: 98664 RVA: 0x000745A4 File Offset: 0x000735A4
		public override void add_ondatasetchanged(HTMLButtonElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018169 RID: 98665 RVA: 0x00074634 File Offset: 0x00073634
		public override void remove_ondatasetchanged(HTMLButtonElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601816A RID: 98666 RVA: 0x00074724 File Offset: 0x00073724
		public override void add_onrowenter(HTMLButtonElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601816B RID: 98667 RVA: 0x000747B4 File Offset: 0x000737B4
		public override void remove_onrowenter(HTMLButtonElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601816C RID: 98668 RVA: 0x000748A4 File Offset: 0x000738A4
		public override void add_onrowexit(HTMLButtonElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601816D RID: 98669 RVA: 0x00074934 File Offset: 0x00073934
		public override void remove_onrowexit(HTMLButtonElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601816E RID: 98670 RVA: 0x00074A24 File Offset: 0x00073A24
		public override void add_onerrorupdate(HTMLButtonElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601816F RID: 98671 RVA: 0x00074AB4 File Offset: 0x00073AB4
		public override void remove_onerrorupdate(HTMLButtonElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018170 RID: 98672 RVA: 0x00074BA4 File Offset: 0x00073BA4
		public override void add_onafterupdate(HTMLButtonElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018171 RID: 98673 RVA: 0x00074C34 File Offset: 0x00073C34
		public override void remove_onafterupdate(HTMLButtonElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018172 RID: 98674 RVA: 0x00074D24 File Offset: 0x00073D24
		public override void add_onbeforeupdate(HTMLButtonElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018173 RID: 98675 RVA: 0x00074DB4 File Offset: 0x00073DB4
		public override void remove_onbeforeupdate(HTMLButtonElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018174 RID: 98676 RVA: 0x00074EA4 File Offset: 0x00073EA4
		public override void add_ondragstart(HTMLButtonElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018175 RID: 98677 RVA: 0x00074F34 File Offset: 0x00073F34
		public override void remove_ondragstart(HTMLButtonElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018176 RID: 98678 RVA: 0x00075024 File Offset: 0x00074024
		public override void add_onfilterchange(HTMLButtonElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018177 RID: 98679 RVA: 0x000750B4 File Offset: 0x000740B4
		public override void remove_onfilterchange(HTMLButtonElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018178 RID: 98680 RVA: 0x000751A4 File Offset: 0x000741A4
		public override void add_onselectstart(HTMLButtonElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018179 RID: 98681 RVA: 0x00075234 File Offset: 0x00074234
		public override void remove_onselectstart(HTMLButtonElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601817A RID: 98682 RVA: 0x00075324 File Offset: 0x00074324
		public override void add_onmouseup(HTMLButtonElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601817B RID: 98683 RVA: 0x000753B4 File Offset: 0x000743B4
		public override void remove_onmouseup(HTMLButtonElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601817C RID: 98684 RVA: 0x000754A4 File Offset: 0x000744A4
		public override void add_onmousedown(HTMLButtonElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601817D RID: 98685 RVA: 0x00075534 File Offset: 0x00074534
		public override void remove_onmousedown(HTMLButtonElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601817E RID: 98686 RVA: 0x00075624 File Offset: 0x00074624
		public override void add_onmousemove(HTMLButtonElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601817F RID: 98687 RVA: 0x000756B4 File Offset: 0x000746B4
		public override void remove_onmousemove(HTMLButtonElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018180 RID: 98688 RVA: 0x000757A4 File Offset: 0x000747A4
		public override void add_onmouseover(HTMLButtonElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018181 RID: 98689 RVA: 0x00075834 File Offset: 0x00074834
		public override void remove_onmouseover(HTMLButtonElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018182 RID: 98690 RVA: 0x00075924 File Offset: 0x00074924
		public override void add_onmouseout(HTMLButtonElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018183 RID: 98691 RVA: 0x000759B4 File Offset: 0x000749B4
		public override void remove_onmouseout(HTMLButtonElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018184 RID: 98692 RVA: 0x00075AA4 File Offset: 0x00074AA4
		public override void add_onkeyup(HTMLButtonElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018185 RID: 98693 RVA: 0x00075B34 File Offset: 0x00074B34
		public override void remove_onkeyup(HTMLButtonElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018186 RID: 98694 RVA: 0x00075C24 File Offset: 0x00074C24
		public override void add_onkeydown(HTMLButtonElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018187 RID: 98695 RVA: 0x00075CB4 File Offset: 0x00074CB4
		public override void remove_onkeydown(HTMLButtonElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018188 RID: 98696 RVA: 0x00075DA4 File Offset: 0x00074DA4
		public override void add_onkeypress(HTMLButtonElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06018189 RID: 98697 RVA: 0x00075E34 File Offset: 0x00074E34
		public override void remove_onkeypress(HTMLButtonElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601818A RID: 98698 RVA: 0x00075F24 File Offset: 0x00074F24
		public override void add_ondblclick(HTMLButtonElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601818B RID: 98699 RVA: 0x00075FB4 File Offset: 0x00074FB4
		public override void remove_ondblclick(HTMLButtonElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601818C RID: 98700 RVA: 0x000760A4 File Offset: 0x000750A4
		public override void add_onclick(HTMLButtonElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601818D RID: 98701 RVA: 0x00076134 File Offset: 0x00075134
		public override void remove_onclick(HTMLButtonElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601818E RID: 98702 RVA: 0x00076224 File Offset: 0x00075224
		public override void add_onhelp(HTMLButtonElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = new HTMLButtonElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlbuttonElementEvents2_SinkHelper, ref num);
				htmlbuttonElementEvents2_SinkHelper.m_dwCookie = num;
				htmlbuttonElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlbuttonElementEvents2_SinkHelper);
			}
		}

		// Token: 0x0601818F RID: 98703 RVA: 0x000762B4 File Offset: 0x000752B4
		public override void remove_onhelp(HTMLButtonElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper;
					for (;;)
					{
						htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlbuttonElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlbuttonElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018190 RID: 98704 RVA: 0x000763A4 File Offset: 0x000753A4
		public HTMLButtonElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018191 RID: 98705 RVA: 0x000763CC File Offset: 0x000753CC
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
								HTMLButtonElementEvents2_SinkHelper htmlbuttonElementEvents2_SinkHelper = (HTMLButtonElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlbuttonElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018192 RID: 98706 RVA: 0x00076480 File Offset: 0x00075480
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000925 RID: 2341
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000926 RID: 2342
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000927 RID: 2343
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
