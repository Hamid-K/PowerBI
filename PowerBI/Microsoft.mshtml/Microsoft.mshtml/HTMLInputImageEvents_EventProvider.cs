using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E15 RID: 3605
	internal sealed class HTMLInputImageEvents_EventProvider : HTMLInputImageEvents_Event, IDisposable
	{
		// Token: 0x060192A7 RID: 103079 RVA: 0x0011051C File Offset: 0x0010F51C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				195, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060192A8 RID: 103080 RVA: 0x00110630 File Offset: 0x0010F630
		public override void add_onabort(HTMLInputImageEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192A9 RID: 103081 RVA: 0x001106C0 File Offset: 0x0010F6C0
		public override void remove_onabort(HTMLInputImageEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onabortDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192AA RID: 103082 RVA: 0x001107B0 File Offset: 0x0010F7B0
		public override void add_onerror(HTMLInputImageEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192AB RID: 103083 RVA: 0x00110840 File Offset: 0x0010F840
		public override void remove_onerror(HTMLInputImageEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onerrorDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192AC RID: 103084 RVA: 0x00110930 File Offset: 0x0010F930
		public override void add_onload(HTMLInputImageEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192AD RID: 103085 RVA: 0x001109C0 File Offset: 0x0010F9C0
		public override void remove_onload(HTMLInputImageEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onloadDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192AE RID: 103086 RVA: 0x00110AB0 File Offset: 0x0010FAB0
		public override void add_onfocusout(HTMLInputImageEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192AF RID: 103087 RVA: 0x00110B40 File Offset: 0x0010FB40
		public override void remove_onfocusout(HTMLInputImageEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192B0 RID: 103088 RVA: 0x00110C30 File Offset: 0x0010FC30
		public override void add_onfocusin(HTMLInputImageEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192B1 RID: 103089 RVA: 0x00110CC0 File Offset: 0x0010FCC0
		public override void remove_onfocusin(HTMLInputImageEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192B2 RID: 103090 RVA: 0x00110DB0 File Offset: 0x0010FDB0
		public override void add_ondeactivate(HTMLInputImageEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192B3 RID: 103091 RVA: 0x00110E40 File Offset: 0x0010FE40
		public override void remove_ondeactivate(HTMLInputImageEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192B4 RID: 103092 RVA: 0x00110F30 File Offset: 0x0010FF30
		public override void add_onactivate(HTMLInputImageEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192B5 RID: 103093 RVA: 0x00110FC0 File Offset: 0x0010FFC0
		public override void remove_onactivate(HTMLInputImageEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onactivateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192B6 RID: 103094 RVA: 0x001110B0 File Offset: 0x001100B0
		public override void add_onmousewheel(HTMLInputImageEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192B7 RID: 103095 RVA: 0x00111140 File Offset: 0x00110140
		public override void remove_onmousewheel(HTMLInputImageEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192B8 RID: 103096 RVA: 0x00111230 File Offset: 0x00110230
		public override void add_onmouseleave(HTMLInputImageEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192B9 RID: 103097 RVA: 0x001112C0 File Offset: 0x001102C0
		public override void remove_onmouseleave(HTMLInputImageEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192BA RID: 103098 RVA: 0x001113B0 File Offset: 0x001103B0
		public override void add_onmouseenter(HTMLInputImageEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192BB RID: 103099 RVA: 0x00111440 File Offset: 0x00110440
		public override void remove_onmouseenter(HTMLInputImageEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192BC RID: 103100 RVA: 0x00111530 File Offset: 0x00110530
		public override void add_onresizeend(HTMLInputImageEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192BD RID: 103101 RVA: 0x001115C0 File Offset: 0x001105C0
		public override void remove_onresizeend(HTMLInputImageEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192BE RID: 103102 RVA: 0x001116B0 File Offset: 0x001106B0
		public override void add_onresizestart(HTMLInputImageEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192BF RID: 103103 RVA: 0x00111740 File Offset: 0x00110740
		public override void remove_onresizestart(HTMLInputImageEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192C0 RID: 103104 RVA: 0x00111830 File Offset: 0x00110830
		public override void add_onmoveend(HTMLInputImageEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192C1 RID: 103105 RVA: 0x001118C0 File Offset: 0x001108C0
		public override void remove_onmoveend(HTMLInputImageEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192C2 RID: 103106 RVA: 0x001119B0 File Offset: 0x001109B0
		public override void add_onmovestart(HTMLInputImageEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192C3 RID: 103107 RVA: 0x00111A40 File Offset: 0x00110A40
		public override void remove_onmovestart(HTMLInputImageEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192C4 RID: 103108 RVA: 0x00111B30 File Offset: 0x00110B30
		public override void add_oncontrolselect(HTMLInputImageEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192C5 RID: 103109 RVA: 0x00111BC0 File Offset: 0x00110BC0
		public override void remove_oncontrolselect(HTMLInputImageEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlinputImageEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192C6 RID: 103110 RVA: 0x00111CB0 File Offset: 0x00110CB0
		public override void add_onmove(HTMLInputImageEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192C7 RID: 103111 RVA: 0x00111D40 File Offset: 0x00110D40
		public override void remove_onmove(HTMLInputImageEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmoveDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192C8 RID: 103112 RVA: 0x00111E30 File Offset: 0x00110E30
		public override void add_onbeforeactivate(HTMLInputImageEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192C9 RID: 103113 RVA: 0x00111EC0 File Offset: 0x00110EC0
		public override void remove_onbeforeactivate(HTMLInputImageEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192CA RID: 103114 RVA: 0x00111FB0 File Offset: 0x00110FB0
		public override void add_onbeforedeactivate(HTMLInputImageEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192CB RID: 103115 RVA: 0x00112040 File Offset: 0x00111040
		public override void remove_onbeforedeactivate(HTMLInputImageEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192CC RID: 103116 RVA: 0x00112130 File Offset: 0x00111130
		public override void add_onpage(HTMLInputImageEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192CD RID: 103117 RVA: 0x001121C0 File Offset: 0x001111C0
		public override void remove_onpage(HTMLInputImageEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onpageDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192CE RID: 103118 RVA: 0x001122B0 File Offset: 0x001112B0
		public override void add_onlayoutcomplete(HTMLInputImageEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192CF RID: 103119 RVA: 0x00112340 File Offset: 0x00111340
		public override void remove_onlayoutcomplete(HTMLInputImageEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192D0 RID: 103120 RVA: 0x00112430 File Offset: 0x00111430
		public override void add_onbeforeeditfocus(HTMLInputImageEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192D1 RID: 103121 RVA: 0x001124C0 File Offset: 0x001114C0
		public override void remove_onbeforeeditfocus(HTMLInputImageEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192D2 RID: 103122 RVA: 0x001125B0 File Offset: 0x001115B0
		public override void add_onreadystatechange(HTMLInputImageEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192D3 RID: 103123 RVA: 0x00112640 File Offset: 0x00111640
		public override void remove_onreadystatechange(HTMLInputImageEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192D4 RID: 103124 RVA: 0x00112730 File Offset: 0x00111730
		public override void add_oncellchange(HTMLInputImageEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192D5 RID: 103125 RVA: 0x001127C0 File Offset: 0x001117C0
		public override void remove_oncellchange(HTMLInputImageEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlinputImageEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192D6 RID: 103126 RVA: 0x001128B0 File Offset: 0x001118B0
		public override void add_onrowsinserted(HTMLInputImageEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192D7 RID: 103127 RVA: 0x00112940 File Offset: 0x00111940
		public override void remove_onrowsinserted(HTMLInputImageEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192D8 RID: 103128 RVA: 0x00112A30 File Offset: 0x00111A30
		public override void add_onrowsdelete(HTMLInputImageEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192D9 RID: 103129 RVA: 0x00112AC0 File Offset: 0x00111AC0
		public override void remove_onrowsdelete(HTMLInputImageEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192DA RID: 103130 RVA: 0x00112BB0 File Offset: 0x00111BB0
		public override void add_oncontextmenu(HTMLInputImageEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192DB RID: 103131 RVA: 0x00112C40 File Offset: 0x00111C40
		public override void remove_oncontextmenu(HTMLInputImageEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlinputImageEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192DC RID: 103132 RVA: 0x00112D30 File Offset: 0x00111D30
		public override void add_onpaste(HTMLInputImageEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192DD RID: 103133 RVA: 0x00112DC0 File Offset: 0x00111DC0
		public override void remove_onpaste(HTMLInputImageEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onpasteDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192DE RID: 103134 RVA: 0x00112EB0 File Offset: 0x00111EB0
		public override void add_onbeforepaste(HTMLInputImageEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192DF RID: 103135 RVA: 0x00112F40 File Offset: 0x00111F40
		public override void remove_onbeforepaste(HTMLInputImageEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192E0 RID: 103136 RVA: 0x00113030 File Offset: 0x00112030
		public override void add_oncopy(HTMLInputImageEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192E1 RID: 103137 RVA: 0x001130C0 File Offset: 0x001120C0
		public override void remove_oncopy(HTMLInputImageEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_oncopyDelegate != null && ((htmlinputImageEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192E2 RID: 103138 RVA: 0x001131B0 File Offset: 0x001121B0
		public override void add_onbeforecopy(HTMLInputImageEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192E3 RID: 103139 RVA: 0x00113240 File Offset: 0x00112240
		public override void remove_onbeforecopy(HTMLInputImageEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192E4 RID: 103140 RVA: 0x00113330 File Offset: 0x00112330
		public override void add_oncut(HTMLInputImageEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192E5 RID: 103141 RVA: 0x001133C0 File Offset: 0x001123C0
		public override void remove_oncut(HTMLInputImageEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_oncutDelegate != null && ((htmlinputImageEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192E6 RID: 103142 RVA: 0x001134B0 File Offset: 0x001124B0
		public override void add_onbeforecut(HTMLInputImageEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192E7 RID: 103143 RVA: 0x00113540 File Offset: 0x00112540
		public override void remove_onbeforecut(HTMLInputImageEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192E8 RID: 103144 RVA: 0x00113630 File Offset: 0x00112630
		public override void add_ondrop(HTMLInputImageEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192E9 RID: 103145 RVA: 0x001136C0 File Offset: 0x001126C0
		public override void remove_ondrop(HTMLInputImageEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondropDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192EA RID: 103146 RVA: 0x001137B0 File Offset: 0x001127B0
		public override void add_ondragleave(HTMLInputImageEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192EB RID: 103147 RVA: 0x00113840 File Offset: 0x00112840
		public override void remove_ondragleave(HTMLInputImageEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192EC RID: 103148 RVA: 0x00113930 File Offset: 0x00112930
		public override void add_ondragover(HTMLInputImageEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192ED RID: 103149 RVA: 0x001139C0 File Offset: 0x001129C0
		public override void remove_ondragover(HTMLInputImageEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192EE RID: 103150 RVA: 0x00113AB0 File Offset: 0x00112AB0
		public override void add_ondragenter(HTMLInputImageEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192EF RID: 103151 RVA: 0x00113B40 File Offset: 0x00112B40
		public override void remove_ondragenter(HTMLInputImageEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192F0 RID: 103152 RVA: 0x00113C30 File Offset: 0x00112C30
		public override void add_ondragend(HTMLInputImageEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192F1 RID: 103153 RVA: 0x00113CC0 File Offset: 0x00112CC0
		public override void remove_ondragend(HTMLInputImageEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragendDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192F2 RID: 103154 RVA: 0x00113DB0 File Offset: 0x00112DB0
		public override void add_ondrag(HTMLInputImageEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192F3 RID: 103155 RVA: 0x00113E40 File Offset: 0x00112E40
		public override void remove_ondrag(HTMLInputImageEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192F4 RID: 103156 RVA: 0x00113F30 File Offset: 0x00112F30
		public override void add_onresize(HTMLInputImageEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192F5 RID: 103157 RVA: 0x00113FC0 File Offset: 0x00112FC0
		public override void remove_onresize(HTMLInputImageEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onresizeDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192F6 RID: 103158 RVA: 0x001140B0 File Offset: 0x001130B0
		public override void add_onblur(HTMLInputImageEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192F7 RID: 103159 RVA: 0x00114140 File Offset: 0x00113140
		public override void remove_onblur(HTMLInputImageEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onblurDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192F8 RID: 103160 RVA: 0x00114230 File Offset: 0x00113230
		public override void add_onfocus(HTMLInputImageEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192F9 RID: 103161 RVA: 0x001142C0 File Offset: 0x001132C0
		public override void remove_onfocus(HTMLInputImageEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onfocusDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192FA RID: 103162 RVA: 0x001143B0 File Offset: 0x001133B0
		public override void add_onscroll(HTMLInputImageEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192FB RID: 103163 RVA: 0x00114440 File Offset: 0x00113440
		public override void remove_onscroll(HTMLInputImageEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onscrollDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192FC RID: 103164 RVA: 0x00114530 File Offset: 0x00113530
		public override void add_onpropertychange(HTMLInputImageEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192FD RID: 103165 RVA: 0x001145C0 File Offset: 0x001135C0
		public override void remove_onpropertychange(HTMLInputImageEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060192FE RID: 103166 RVA: 0x001146B0 File Offset: 0x001136B0
		public override void add_onlosecapture(HTMLInputImageEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x060192FF RID: 103167 RVA: 0x00114740 File Offset: 0x00113740
		public override void remove_onlosecapture(HTMLInputImageEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019300 RID: 103168 RVA: 0x00114830 File Offset: 0x00113830
		public override void add_ondatasetcomplete(HTMLInputImageEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019301 RID: 103169 RVA: 0x001148C0 File Offset: 0x001138C0
		public override void remove_ondatasetcomplete(HTMLInputImageEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019302 RID: 103170 RVA: 0x001149B0 File Offset: 0x001139B0
		public override void add_ondataavailable(HTMLInputImageEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019303 RID: 103171 RVA: 0x00114A40 File Offset: 0x00113A40
		public override void remove_ondataavailable(HTMLInputImageEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019304 RID: 103172 RVA: 0x00114B30 File Offset: 0x00113B30
		public override void add_ondatasetchanged(HTMLInputImageEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019305 RID: 103173 RVA: 0x00114BC0 File Offset: 0x00113BC0
		public override void remove_ondatasetchanged(HTMLInputImageEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019306 RID: 103174 RVA: 0x00114CB0 File Offset: 0x00113CB0
		public override void add_onrowenter(HTMLInputImageEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019307 RID: 103175 RVA: 0x00114D40 File Offset: 0x00113D40
		public override void remove_onrowenter(HTMLInputImageEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019308 RID: 103176 RVA: 0x00114E30 File Offset: 0x00113E30
		public override void add_onrowexit(HTMLInputImageEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019309 RID: 103177 RVA: 0x00114EC0 File Offset: 0x00113EC0
		public override void remove_onrowexit(HTMLInputImageEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601930A RID: 103178 RVA: 0x00114FB0 File Offset: 0x00113FB0
		public override void add_onerrorupdate(HTMLInputImageEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601930B RID: 103179 RVA: 0x00115040 File Offset: 0x00114040
		public override void remove_onerrorupdate(HTMLInputImageEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601930C RID: 103180 RVA: 0x00115130 File Offset: 0x00114130
		public override void add_onafterupdate(HTMLInputImageEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601930D RID: 103181 RVA: 0x001151C0 File Offset: 0x001141C0
		public override void remove_onafterupdate(HTMLInputImageEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601930E RID: 103182 RVA: 0x001152B0 File Offset: 0x001142B0
		public override void add_onbeforeupdate(HTMLInputImageEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601930F RID: 103183 RVA: 0x00115340 File Offset: 0x00114340
		public override void remove_onbeforeupdate(HTMLInputImageEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019310 RID: 103184 RVA: 0x00115430 File Offset: 0x00114430
		public override void add_ondragstart(HTMLInputImageEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019311 RID: 103185 RVA: 0x001154C0 File Offset: 0x001144C0
		public override void remove_ondragstart(HTMLInputImageEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019312 RID: 103186 RVA: 0x001155B0 File Offset: 0x001145B0
		public override void add_onfilterchange(HTMLInputImageEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019313 RID: 103187 RVA: 0x00115640 File Offset: 0x00114640
		public override void remove_onfilterchange(HTMLInputImageEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019314 RID: 103188 RVA: 0x00115730 File Offset: 0x00114730
		public override void add_onselectstart(HTMLInputImageEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019315 RID: 103189 RVA: 0x001157C0 File Offset: 0x001147C0
		public override void remove_onselectstart(HTMLInputImageEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019316 RID: 103190 RVA: 0x001158B0 File Offset: 0x001148B0
		public override void add_onmouseup(HTMLInputImageEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019317 RID: 103191 RVA: 0x00115940 File Offset: 0x00114940
		public override void remove_onmouseup(HTMLInputImageEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019318 RID: 103192 RVA: 0x00115A30 File Offset: 0x00114A30
		public override void add_onmousedown(HTMLInputImageEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019319 RID: 103193 RVA: 0x00115AC0 File Offset: 0x00114AC0
		public override void remove_onmousedown(HTMLInputImageEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601931A RID: 103194 RVA: 0x00115BB0 File Offset: 0x00114BB0
		public override void add_onmousemove(HTMLInputImageEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601931B RID: 103195 RVA: 0x00115C40 File Offset: 0x00114C40
		public override void remove_onmousemove(HTMLInputImageEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601931C RID: 103196 RVA: 0x00115D30 File Offset: 0x00114D30
		public override void add_onmouseover(HTMLInputImageEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601931D RID: 103197 RVA: 0x00115DC0 File Offset: 0x00114DC0
		public override void remove_onmouseover(HTMLInputImageEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601931E RID: 103198 RVA: 0x00115EB0 File Offset: 0x00114EB0
		public override void add_onmouseout(HTMLInputImageEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601931F RID: 103199 RVA: 0x00115F40 File Offset: 0x00114F40
		public override void remove_onmouseout(HTMLInputImageEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019320 RID: 103200 RVA: 0x00116030 File Offset: 0x00115030
		public override void add_onkeyup(HTMLInputImageEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019321 RID: 103201 RVA: 0x001160C0 File Offset: 0x001150C0
		public override void remove_onkeyup(HTMLInputImageEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019322 RID: 103202 RVA: 0x001161B0 File Offset: 0x001151B0
		public override void add_onkeydown(HTMLInputImageEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019323 RID: 103203 RVA: 0x00116240 File Offset: 0x00115240
		public override void remove_onkeydown(HTMLInputImageEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019324 RID: 103204 RVA: 0x00116330 File Offset: 0x00115330
		public override void add_onkeypress(HTMLInputImageEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019325 RID: 103205 RVA: 0x001163C0 File Offset: 0x001153C0
		public override void remove_onkeypress(HTMLInputImageEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019326 RID: 103206 RVA: 0x001164B0 File Offset: 0x001154B0
		public override void add_ondblclick(HTMLInputImageEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019327 RID: 103207 RVA: 0x00116540 File Offset: 0x00115540
		public override void remove_ondblclick(HTMLInputImageEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlinputImageEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019328 RID: 103208 RVA: 0x00116630 File Offset: 0x00115630
		public override void add_onclick(HTMLInputImageEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x06019329 RID: 103209 RVA: 0x001166C0 File Offset: 0x001156C0
		public override void remove_onclick(HTMLInputImageEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onclickDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601932A RID: 103210 RVA: 0x001167B0 File Offset: 0x001157B0
		public override void add_onhelp(HTMLInputImageEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = new HTMLInputImageEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlinputImageEvents_SinkHelper, ref num);
				htmlinputImageEvents_SinkHelper.m_dwCookie = num;
				htmlinputImageEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlinputImageEvents_SinkHelper);
			}
		}

		// Token: 0x0601932B RID: 103211 RVA: 0x00116840 File Offset: 0x00115840
		public override void remove_onhelp(HTMLInputImageEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper;
					for (;;)
					{
						htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlinputImageEvents_SinkHelper.m_onhelpDelegate != null && ((htmlinputImageEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601932C RID: 103212 RVA: 0x00116930 File Offset: 0x00115930
		public HTMLInputImageEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601932D RID: 103213 RVA: 0x00116958 File Offset: 0x00115958
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
								HTMLInputImageEvents_SinkHelper htmlinputImageEvents_SinkHelper = (HTMLInputImageEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlinputImageEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x0601932E RID: 103214 RVA: 0x00116A0C File Offset: 0x00115A0C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000F3E RID: 3902
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000F3F RID: 3903
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000F40 RID: 3904
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
