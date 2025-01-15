using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E03 RID: 3587
	internal sealed class HTMLAreaEvents2_EventProvider : HTMLAreaEvents2_Event, IDisposable
	{
		// Token: 0x06018BF8 RID: 101368 RVA: 0x000D38E4 File Offset: 0x000D28E4
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				17, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018BF9 RID: 101369 RVA: 0x000D39F8 File Offset: 0x000D29F8
		public override void add_onmousewheel(HTMLAreaEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018BFA RID: 101370 RVA: 0x000D3A88 File Offset: 0x000D2A88
		public override void remove_onmousewheel(HTMLAreaEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BFB RID: 101371 RVA: 0x000D3B78 File Offset: 0x000D2B78
		public override void add_onresizeend(HTMLAreaEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018BFC RID: 101372 RVA: 0x000D3C08 File Offset: 0x000D2C08
		public override void remove_onresizeend(HTMLAreaEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlareaEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BFD RID: 101373 RVA: 0x000D3CF8 File Offset: 0x000D2CF8
		public override void add_onresizestart(HTMLAreaEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018BFE RID: 101374 RVA: 0x000D3D88 File Offset: 0x000D2D88
		public override void remove_onresizestart(HTMLAreaEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlareaEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018BFF RID: 101375 RVA: 0x000D3E78 File Offset: 0x000D2E78
		public override void add_onmoveend(HTMLAreaEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C00 RID: 101376 RVA: 0x000D3F08 File Offset: 0x000D2F08
		public override void remove_onmoveend(HTMLAreaEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C01 RID: 101377 RVA: 0x000D3FF8 File Offset: 0x000D2FF8
		public override void add_onmovestart(HTMLAreaEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C02 RID: 101378 RVA: 0x000D4088 File Offset: 0x000D3088
		public override void remove_onmovestart(HTMLAreaEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C03 RID: 101379 RVA: 0x000D4178 File Offset: 0x000D3178
		public override void add_oncontrolselect(HTMLAreaEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C04 RID: 101380 RVA: 0x000D4208 File Offset: 0x000D3208
		public override void remove_oncontrolselect(HTMLAreaEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlareaEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C05 RID: 101381 RVA: 0x000D42F8 File Offset: 0x000D32F8
		public override void add_onmove(HTMLAreaEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C06 RID: 101382 RVA: 0x000D4388 File Offset: 0x000D3388
		public override void remove_onmove(HTMLAreaEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C07 RID: 101383 RVA: 0x000D4478 File Offset: 0x000D3478
		public override void add_onfocusout(HTMLAreaEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C08 RID: 101384 RVA: 0x000D4508 File Offset: 0x000D3508
		public override void remove_onfocusout(HTMLAreaEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlareaEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C09 RID: 101385 RVA: 0x000D45F8 File Offset: 0x000D35F8
		public override void add_onfocusin(HTMLAreaEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C0A RID: 101386 RVA: 0x000D4688 File Offset: 0x000D3688
		public override void remove_onfocusin(HTMLAreaEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlareaEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C0B RID: 101387 RVA: 0x000D4778 File Offset: 0x000D3778
		public override void add_onbeforeactivate(HTMLAreaEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C0C RID: 101388 RVA: 0x000D4808 File Offset: 0x000D3808
		public override void remove_onbeforeactivate(HTMLAreaEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C0D RID: 101389 RVA: 0x000D48F8 File Offset: 0x000D38F8
		public override void add_onbeforedeactivate(HTMLAreaEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C0E RID: 101390 RVA: 0x000D4988 File Offset: 0x000D3988
		public override void remove_onbeforedeactivate(HTMLAreaEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C0F RID: 101391 RVA: 0x000D4A78 File Offset: 0x000D3A78
		public override void add_ondeactivate(HTMLAreaEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C10 RID: 101392 RVA: 0x000D4B08 File Offset: 0x000D3B08
		public override void remove_ondeactivate(HTMLAreaEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C11 RID: 101393 RVA: 0x000D4BF8 File Offset: 0x000D3BF8
		public override void add_onactivate(HTMLAreaEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C12 RID: 101394 RVA: 0x000D4C88 File Offset: 0x000D3C88
		public override void remove_onactivate(HTMLAreaEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C13 RID: 101395 RVA: 0x000D4D78 File Offset: 0x000D3D78
		public override void add_onmouseleave(HTMLAreaEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C14 RID: 101396 RVA: 0x000D4E08 File Offset: 0x000D3E08
		public override void remove_onmouseleave(HTMLAreaEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C15 RID: 101397 RVA: 0x000D4EF8 File Offset: 0x000D3EF8
		public override void add_onmouseenter(HTMLAreaEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C16 RID: 101398 RVA: 0x000D4F88 File Offset: 0x000D3F88
		public override void remove_onmouseenter(HTMLAreaEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C17 RID: 101399 RVA: 0x000D5078 File Offset: 0x000D4078
		public override void add_onpage(HTMLAreaEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C18 RID: 101400 RVA: 0x000D5108 File Offset: 0x000D4108
		public override void remove_onpage(HTMLAreaEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onpageDelegate != null && ((htmlareaEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C19 RID: 101401 RVA: 0x000D51F8 File Offset: 0x000D41F8
		public override void add_onlayoutcomplete(HTMLAreaEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C1A RID: 101402 RVA: 0x000D5288 File Offset: 0x000D4288
		public override void remove_onlayoutcomplete(HTMLAreaEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlareaEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C1B RID: 101403 RVA: 0x000D5378 File Offset: 0x000D4378
		public override void add_onreadystatechange(HTMLAreaEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C1C RID: 101404 RVA: 0x000D5408 File Offset: 0x000D4408
		public override void remove_onreadystatechange(HTMLAreaEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlareaEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C1D RID: 101405 RVA: 0x000D54F8 File Offset: 0x000D44F8
		public override void add_oncellchange(HTMLAreaEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C1E RID: 101406 RVA: 0x000D5588 File Offset: 0x000D4588
		public override void remove_oncellchange(HTMLAreaEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlareaEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C1F RID: 101407 RVA: 0x000D5678 File Offset: 0x000D4678
		public override void add_onrowsinserted(HTMLAreaEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C20 RID: 101408 RVA: 0x000D5708 File Offset: 0x000D4708
		public override void remove_onrowsinserted(HTMLAreaEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlareaEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C21 RID: 101409 RVA: 0x000D57F8 File Offset: 0x000D47F8
		public override void add_onrowsdelete(HTMLAreaEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C22 RID: 101410 RVA: 0x000D5888 File Offset: 0x000D4888
		public override void remove_onrowsdelete(HTMLAreaEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlareaEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C23 RID: 101411 RVA: 0x000D5978 File Offset: 0x000D4978
		public override void add_oncontextmenu(HTMLAreaEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C24 RID: 101412 RVA: 0x000D5A08 File Offset: 0x000D4A08
		public override void remove_oncontextmenu(HTMLAreaEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlareaEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C25 RID: 101413 RVA: 0x000D5AF8 File Offset: 0x000D4AF8
		public override void add_onpaste(HTMLAreaEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C26 RID: 101414 RVA: 0x000D5B88 File Offset: 0x000D4B88
		public override void remove_onpaste(HTMLAreaEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlareaEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C27 RID: 101415 RVA: 0x000D5C78 File Offset: 0x000D4C78
		public override void add_onbeforepaste(HTMLAreaEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C28 RID: 101416 RVA: 0x000D5D08 File Offset: 0x000D4D08
		public override void remove_onbeforepaste(HTMLAreaEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C29 RID: 101417 RVA: 0x000D5DF8 File Offset: 0x000D4DF8
		public override void add_oncopy(HTMLAreaEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C2A RID: 101418 RVA: 0x000D5E88 File Offset: 0x000D4E88
		public override void remove_oncopy(HTMLAreaEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlareaEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C2B RID: 101419 RVA: 0x000D5F78 File Offset: 0x000D4F78
		public override void add_onbeforecopy(HTMLAreaEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C2C RID: 101420 RVA: 0x000D6008 File Offset: 0x000D5008
		public override void remove_onbeforecopy(HTMLAreaEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C2D RID: 101421 RVA: 0x000D60F8 File Offset: 0x000D50F8
		public override void add_oncut(HTMLAreaEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C2E RID: 101422 RVA: 0x000D6188 File Offset: 0x000D5188
		public override void remove_oncut(HTMLAreaEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_oncutDelegate != null && ((htmlareaEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C2F RID: 101423 RVA: 0x000D6278 File Offset: 0x000D5278
		public override void add_onbeforecut(HTMLAreaEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C30 RID: 101424 RVA: 0x000D6308 File Offset: 0x000D5308
		public override void remove_onbeforecut(HTMLAreaEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C31 RID: 101425 RVA: 0x000D63F8 File Offset: 0x000D53F8
		public override void add_ondrop(HTMLAreaEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C32 RID: 101426 RVA: 0x000D6488 File Offset: 0x000D5488
		public override void remove_ondrop(HTMLAreaEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondropDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C33 RID: 101427 RVA: 0x000D6578 File Offset: 0x000D5578
		public override void add_ondragleave(HTMLAreaEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C34 RID: 101428 RVA: 0x000D6608 File Offset: 0x000D5608
		public override void remove_ondragleave(HTMLAreaEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C35 RID: 101429 RVA: 0x000D66F8 File Offset: 0x000D56F8
		public override void add_ondragover(HTMLAreaEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C36 RID: 101430 RVA: 0x000D6788 File Offset: 0x000D5788
		public override void remove_ondragover(HTMLAreaEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C37 RID: 101431 RVA: 0x000D6878 File Offset: 0x000D5878
		public override void add_ondragenter(HTMLAreaEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C38 RID: 101432 RVA: 0x000D6908 File Offset: 0x000D5908
		public override void remove_ondragenter(HTMLAreaEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C39 RID: 101433 RVA: 0x000D69F8 File Offset: 0x000D59F8
		public override void add_ondragend(HTMLAreaEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C3A RID: 101434 RVA: 0x000D6A88 File Offset: 0x000D5A88
		public override void remove_ondragend(HTMLAreaEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C3B RID: 101435 RVA: 0x000D6B78 File Offset: 0x000D5B78
		public override void add_ondrag(HTMLAreaEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C3C RID: 101436 RVA: 0x000D6C08 File Offset: 0x000D5C08
		public override void remove_ondrag(HTMLAreaEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C3D RID: 101437 RVA: 0x000D6CF8 File Offset: 0x000D5CF8
		public override void add_onresize(HTMLAreaEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C3E RID: 101438 RVA: 0x000D6D88 File Offset: 0x000D5D88
		public override void remove_onresize(HTMLAreaEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlareaEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C3F RID: 101439 RVA: 0x000D6E78 File Offset: 0x000D5E78
		public override void add_onblur(HTMLAreaEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C40 RID: 101440 RVA: 0x000D6F08 File Offset: 0x000D5F08
		public override void remove_onblur(HTMLAreaEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onblurDelegate != null && ((htmlareaEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C41 RID: 101441 RVA: 0x000D6FF8 File Offset: 0x000D5FF8
		public override void add_onfocus(HTMLAreaEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C42 RID: 101442 RVA: 0x000D7088 File Offset: 0x000D6088
		public override void remove_onfocus(HTMLAreaEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlareaEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C43 RID: 101443 RVA: 0x000D7178 File Offset: 0x000D6178
		public override void add_onscroll(HTMLAreaEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C44 RID: 101444 RVA: 0x000D7208 File Offset: 0x000D6208
		public override void remove_onscroll(HTMLAreaEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlareaEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C45 RID: 101445 RVA: 0x000D72F8 File Offset: 0x000D62F8
		public override void add_onpropertychange(HTMLAreaEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C46 RID: 101446 RVA: 0x000D7388 File Offset: 0x000D6388
		public override void remove_onpropertychange(HTMLAreaEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlareaEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C47 RID: 101447 RVA: 0x000D7478 File Offset: 0x000D6478
		public override void add_onlosecapture(HTMLAreaEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C48 RID: 101448 RVA: 0x000D7508 File Offset: 0x000D6508
		public override void remove_onlosecapture(HTMLAreaEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlareaEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C49 RID: 101449 RVA: 0x000D75F8 File Offset: 0x000D65F8
		public override void add_ondatasetcomplete(HTMLAreaEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C4A RID: 101450 RVA: 0x000D7688 File Offset: 0x000D6688
		public override void remove_ondatasetcomplete(HTMLAreaEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C4B RID: 101451 RVA: 0x000D7778 File Offset: 0x000D6778
		public override void add_ondataavailable(HTMLAreaEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C4C RID: 101452 RVA: 0x000D7808 File Offset: 0x000D6808
		public override void remove_ondataavailable(HTMLAreaEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C4D RID: 101453 RVA: 0x000D78F8 File Offset: 0x000D68F8
		public override void add_ondatasetchanged(HTMLAreaEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C4E RID: 101454 RVA: 0x000D7988 File Offset: 0x000D6988
		public override void remove_ondatasetchanged(HTMLAreaEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C4F RID: 101455 RVA: 0x000D7A78 File Offset: 0x000D6A78
		public override void add_onrowenter(HTMLAreaEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C50 RID: 101456 RVA: 0x000D7B08 File Offset: 0x000D6B08
		public override void remove_onrowenter(HTMLAreaEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlareaEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C51 RID: 101457 RVA: 0x000D7BF8 File Offset: 0x000D6BF8
		public override void add_onrowexit(HTMLAreaEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C52 RID: 101458 RVA: 0x000D7C88 File Offset: 0x000D6C88
		public override void remove_onrowexit(HTMLAreaEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlareaEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C53 RID: 101459 RVA: 0x000D7D78 File Offset: 0x000D6D78
		public override void add_onerrorupdate(HTMLAreaEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C54 RID: 101460 RVA: 0x000D7E08 File Offset: 0x000D6E08
		public override void remove_onerrorupdate(HTMLAreaEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C55 RID: 101461 RVA: 0x000D7EF8 File Offset: 0x000D6EF8
		public override void add_onafterupdate(HTMLAreaEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C56 RID: 101462 RVA: 0x000D7F88 File Offset: 0x000D6F88
		public override void remove_onafterupdate(HTMLAreaEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C57 RID: 101463 RVA: 0x000D8078 File Offset: 0x000D7078
		public override void add_onbeforeupdate(HTMLAreaEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C58 RID: 101464 RVA: 0x000D8108 File Offset: 0x000D7108
		public override void remove_onbeforeupdate(HTMLAreaEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlareaEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C59 RID: 101465 RVA: 0x000D81F8 File Offset: 0x000D71F8
		public override void add_ondragstart(HTMLAreaEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C5A RID: 101466 RVA: 0x000D8288 File Offset: 0x000D7288
		public override void remove_ondragstart(HTMLAreaEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C5B RID: 101467 RVA: 0x000D8378 File Offset: 0x000D7378
		public override void add_onfilterchange(HTMLAreaEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C5C RID: 101468 RVA: 0x000D8408 File Offset: 0x000D7408
		public override void remove_onfilterchange(HTMLAreaEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlareaEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C5D RID: 101469 RVA: 0x000D84F8 File Offset: 0x000D74F8
		public override void add_onselectstart(HTMLAreaEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C5E RID: 101470 RVA: 0x000D8588 File Offset: 0x000D7588
		public override void remove_onselectstart(HTMLAreaEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlareaEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C5F RID: 101471 RVA: 0x000D8678 File Offset: 0x000D7678
		public override void add_onmouseup(HTMLAreaEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C60 RID: 101472 RVA: 0x000D8708 File Offset: 0x000D7708
		public override void remove_onmouseup(HTMLAreaEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C61 RID: 101473 RVA: 0x000D87F8 File Offset: 0x000D77F8
		public override void add_onmousedown(HTMLAreaEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C62 RID: 101474 RVA: 0x000D8888 File Offset: 0x000D7888
		public override void remove_onmousedown(HTMLAreaEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C63 RID: 101475 RVA: 0x000D8978 File Offset: 0x000D7978
		public override void add_onmousemove(HTMLAreaEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C64 RID: 101476 RVA: 0x000D8A08 File Offset: 0x000D7A08
		public override void remove_onmousemove(HTMLAreaEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C65 RID: 101477 RVA: 0x000D8AF8 File Offset: 0x000D7AF8
		public override void add_onmouseover(HTMLAreaEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C66 RID: 101478 RVA: 0x000D8B88 File Offset: 0x000D7B88
		public override void remove_onmouseover(HTMLAreaEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C67 RID: 101479 RVA: 0x000D8C78 File Offset: 0x000D7C78
		public override void add_onmouseout(HTMLAreaEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C68 RID: 101480 RVA: 0x000D8D08 File Offset: 0x000D7D08
		public override void remove_onmouseout(HTMLAreaEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlareaEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C69 RID: 101481 RVA: 0x000D8DF8 File Offset: 0x000D7DF8
		public override void add_onkeyup(HTMLAreaEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C6A RID: 101482 RVA: 0x000D8E88 File Offset: 0x000D7E88
		public override void remove_onkeyup(HTMLAreaEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlareaEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C6B RID: 101483 RVA: 0x000D8F78 File Offset: 0x000D7F78
		public override void add_onkeydown(HTMLAreaEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C6C RID: 101484 RVA: 0x000D9008 File Offset: 0x000D8008
		public override void remove_onkeydown(HTMLAreaEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlareaEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C6D RID: 101485 RVA: 0x000D90F8 File Offset: 0x000D80F8
		public override void add_onkeypress(HTMLAreaEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C6E RID: 101486 RVA: 0x000D9188 File Offset: 0x000D8188
		public override void remove_onkeypress(HTMLAreaEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlareaEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C6F RID: 101487 RVA: 0x000D9278 File Offset: 0x000D8278
		public override void add_ondblclick(HTMLAreaEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C70 RID: 101488 RVA: 0x000D9308 File Offset: 0x000D8308
		public override void remove_ondblclick(HTMLAreaEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlareaEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C71 RID: 101489 RVA: 0x000D93F8 File Offset: 0x000D83F8
		public override void add_onclick(HTMLAreaEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C72 RID: 101490 RVA: 0x000D9488 File Offset: 0x000D8488
		public override void remove_onclick(HTMLAreaEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onclickDelegate != null && ((htmlareaEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C73 RID: 101491 RVA: 0x000D9578 File Offset: 0x000D8578
		public override void add_onhelp(HTMLAreaEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = new HTMLAreaEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlareaEvents2_SinkHelper, ref num);
				htmlareaEvents2_SinkHelper.m_dwCookie = num;
				htmlareaEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlareaEvents2_SinkHelper);
			}
		}

		// Token: 0x06018C74 RID: 101492 RVA: 0x000D9608 File Offset: 0x000D8608
		public override void remove_onhelp(HTMLAreaEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper;
					for (;;)
					{
						htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlareaEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlareaEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018C75 RID: 101493 RVA: 0x000D96F8 File Offset: 0x000D86F8
		public HTMLAreaEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018C76 RID: 101494 RVA: 0x000D9720 File Offset: 0x000D8720
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
								HTMLAreaEvents2_SinkHelper htmlareaEvents2_SinkHelper = (HTMLAreaEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlareaEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018C77 RID: 101495 RVA: 0x000D97D4 File Offset: 0x000D87D4
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000CEC RID: 3308
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000CED RID: 3309
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000CEE RID: 3310
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
