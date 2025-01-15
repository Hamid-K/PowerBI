using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DED RID: 3565
	internal sealed class HTMLMapEvents2_EventProvider : HTMLMapEvents2_Event, IDisposable
	{
		// Token: 0x0601841D RID: 99357 RVA: 0x0008C0FC File Offset: 0x0008B0FC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				30, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601841E RID: 99358 RVA: 0x0008C210 File Offset: 0x0008B210
		public override void add_onmousewheel(HTMLMapEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601841F RID: 99359 RVA: 0x0008C2A0 File Offset: 0x0008B2A0
		public override void remove_onmousewheel(HTMLMapEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018420 RID: 99360 RVA: 0x0008C390 File Offset: 0x0008B390
		public override void add_onresizeend(HTMLMapEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018421 RID: 99361 RVA: 0x0008C420 File Offset: 0x0008B420
		public override void remove_onresizeend(HTMLMapEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlmapEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018422 RID: 99362 RVA: 0x0008C510 File Offset: 0x0008B510
		public override void add_onresizestart(HTMLMapEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018423 RID: 99363 RVA: 0x0008C5A0 File Offset: 0x0008B5A0
		public override void remove_onresizestart(HTMLMapEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlmapEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018424 RID: 99364 RVA: 0x0008C690 File Offset: 0x0008B690
		public override void add_onmoveend(HTMLMapEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018425 RID: 99365 RVA: 0x0008C720 File Offset: 0x0008B720
		public override void remove_onmoveend(HTMLMapEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018426 RID: 99366 RVA: 0x0008C810 File Offset: 0x0008B810
		public override void add_onmovestart(HTMLMapEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018427 RID: 99367 RVA: 0x0008C8A0 File Offset: 0x0008B8A0
		public override void remove_onmovestart(HTMLMapEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018428 RID: 99368 RVA: 0x0008C990 File Offset: 0x0008B990
		public override void add_oncontrolselect(HTMLMapEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018429 RID: 99369 RVA: 0x0008CA20 File Offset: 0x0008BA20
		public override void remove_oncontrolselect(HTMLMapEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlmapEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601842A RID: 99370 RVA: 0x0008CB10 File Offset: 0x0008BB10
		public override void add_onmove(HTMLMapEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601842B RID: 99371 RVA: 0x0008CBA0 File Offset: 0x0008BBA0
		public override void remove_onmove(HTMLMapEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601842C RID: 99372 RVA: 0x0008CC90 File Offset: 0x0008BC90
		public override void add_onfocusout(HTMLMapEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601842D RID: 99373 RVA: 0x0008CD20 File Offset: 0x0008BD20
		public override void remove_onfocusout(HTMLMapEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlmapEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601842E RID: 99374 RVA: 0x0008CE10 File Offset: 0x0008BE10
		public override void add_onfocusin(HTMLMapEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601842F RID: 99375 RVA: 0x0008CEA0 File Offset: 0x0008BEA0
		public override void remove_onfocusin(HTMLMapEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlmapEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018430 RID: 99376 RVA: 0x0008CF90 File Offset: 0x0008BF90
		public override void add_onbeforeactivate(HTMLMapEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018431 RID: 99377 RVA: 0x0008D020 File Offset: 0x0008C020
		public override void remove_onbeforeactivate(HTMLMapEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018432 RID: 99378 RVA: 0x0008D110 File Offset: 0x0008C110
		public override void add_onbeforedeactivate(HTMLMapEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018433 RID: 99379 RVA: 0x0008D1A0 File Offset: 0x0008C1A0
		public override void remove_onbeforedeactivate(HTMLMapEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018434 RID: 99380 RVA: 0x0008D290 File Offset: 0x0008C290
		public override void add_ondeactivate(HTMLMapEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018435 RID: 99381 RVA: 0x0008D320 File Offset: 0x0008C320
		public override void remove_ondeactivate(HTMLMapEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018436 RID: 99382 RVA: 0x0008D410 File Offset: 0x0008C410
		public override void add_onactivate(HTMLMapEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018437 RID: 99383 RVA: 0x0008D4A0 File Offset: 0x0008C4A0
		public override void remove_onactivate(HTMLMapEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018438 RID: 99384 RVA: 0x0008D590 File Offset: 0x0008C590
		public override void add_onmouseleave(HTMLMapEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018439 RID: 99385 RVA: 0x0008D620 File Offset: 0x0008C620
		public override void remove_onmouseleave(HTMLMapEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601843A RID: 99386 RVA: 0x0008D710 File Offset: 0x0008C710
		public override void add_onmouseenter(HTMLMapEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601843B RID: 99387 RVA: 0x0008D7A0 File Offset: 0x0008C7A0
		public override void remove_onmouseenter(HTMLMapEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601843C RID: 99388 RVA: 0x0008D890 File Offset: 0x0008C890
		public override void add_onpage(HTMLMapEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601843D RID: 99389 RVA: 0x0008D920 File Offset: 0x0008C920
		public override void remove_onpage(HTMLMapEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onpageDelegate != null && ((htmlmapEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601843E RID: 99390 RVA: 0x0008DA10 File Offset: 0x0008CA10
		public override void add_onlayoutcomplete(HTMLMapEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601843F RID: 99391 RVA: 0x0008DAA0 File Offset: 0x0008CAA0
		public override void remove_onlayoutcomplete(HTMLMapEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlmapEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018440 RID: 99392 RVA: 0x0008DB90 File Offset: 0x0008CB90
		public override void add_onreadystatechange(HTMLMapEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018441 RID: 99393 RVA: 0x0008DC20 File Offset: 0x0008CC20
		public override void remove_onreadystatechange(HTMLMapEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlmapEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018442 RID: 99394 RVA: 0x0008DD10 File Offset: 0x0008CD10
		public override void add_oncellchange(HTMLMapEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018443 RID: 99395 RVA: 0x0008DDA0 File Offset: 0x0008CDA0
		public override void remove_oncellchange(HTMLMapEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlmapEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018444 RID: 99396 RVA: 0x0008DE90 File Offset: 0x0008CE90
		public override void add_onrowsinserted(HTMLMapEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018445 RID: 99397 RVA: 0x0008DF20 File Offset: 0x0008CF20
		public override void remove_onrowsinserted(HTMLMapEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlmapEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018446 RID: 99398 RVA: 0x0008E010 File Offset: 0x0008D010
		public override void add_onrowsdelete(HTMLMapEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018447 RID: 99399 RVA: 0x0008E0A0 File Offset: 0x0008D0A0
		public override void remove_onrowsdelete(HTMLMapEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlmapEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018448 RID: 99400 RVA: 0x0008E190 File Offset: 0x0008D190
		public override void add_oncontextmenu(HTMLMapEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018449 RID: 99401 RVA: 0x0008E220 File Offset: 0x0008D220
		public override void remove_oncontextmenu(HTMLMapEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlmapEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601844A RID: 99402 RVA: 0x0008E310 File Offset: 0x0008D310
		public override void add_onpaste(HTMLMapEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601844B RID: 99403 RVA: 0x0008E3A0 File Offset: 0x0008D3A0
		public override void remove_onpaste(HTMLMapEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlmapEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601844C RID: 99404 RVA: 0x0008E490 File Offset: 0x0008D490
		public override void add_onbeforepaste(HTMLMapEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601844D RID: 99405 RVA: 0x0008E520 File Offset: 0x0008D520
		public override void remove_onbeforepaste(HTMLMapEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601844E RID: 99406 RVA: 0x0008E610 File Offset: 0x0008D610
		public override void add_oncopy(HTMLMapEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601844F RID: 99407 RVA: 0x0008E6A0 File Offset: 0x0008D6A0
		public override void remove_oncopy(HTMLMapEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlmapEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018450 RID: 99408 RVA: 0x0008E790 File Offset: 0x0008D790
		public override void add_onbeforecopy(HTMLMapEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018451 RID: 99409 RVA: 0x0008E820 File Offset: 0x0008D820
		public override void remove_onbeforecopy(HTMLMapEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018452 RID: 99410 RVA: 0x0008E910 File Offset: 0x0008D910
		public override void add_oncut(HTMLMapEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018453 RID: 99411 RVA: 0x0008E9A0 File Offset: 0x0008D9A0
		public override void remove_oncut(HTMLMapEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_oncutDelegate != null && ((htmlmapEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018454 RID: 99412 RVA: 0x0008EA90 File Offset: 0x0008DA90
		public override void add_onbeforecut(HTMLMapEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018455 RID: 99413 RVA: 0x0008EB20 File Offset: 0x0008DB20
		public override void remove_onbeforecut(HTMLMapEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018456 RID: 99414 RVA: 0x0008EC10 File Offset: 0x0008DC10
		public override void add_ondrop(HTMLMapEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018457 RID: 99415 RVA: 0x0008ECA0 File Offset: 0x0008DCA0
		public override void remove_ondrop(HTMLMapEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondropDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018458 RID: 99416 RVA: 0x0008ED90 File Offset: 0x0008DD90
		public override void add_ondragleave(HTMLMapEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018459 RID: 99417 RVA: 0x0008EE20 File Offset: 0x0008DE20
		public override void remove_ondragleave(HTMLMapEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601845A RID: 99418 RVA: 0x0008EF10 File Offset: 0x0008DF10
		public override void add_ondragover(HTMLMapEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601845B RID: 99419 RVA: 0x0008EFA0 File Offset: 0x0008DFA0
		public override void remove_ondragover(HTMLMapEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601845C RID: 99420 RVA: 0x0008F090 File Offset: 0x0008E090
		public override void add_ondragenter(HTMLMapEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601845D RID: 99421 RVA: 0x0008F120 File Offset: 0x0008E120
		public override void remove_ondragenter(HTMLMapEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601845E RID: 99422 RVA: 0x0008F210 File Offset: 0x0008E210
		public override void add_ondragend(HTMLMapEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601845F RID: 99423 RVA: 0x0008F2A0 File Offset: 0x0008E2A0
		public override void remove_ondragend(HTMLMapEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018460 RID: 99424 RVA: 0x0008F390 File Offset: 0x0008E390
		public override void add_ondrag(HTMLMapEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018461 RID: 99425 RVA: 0x0008F420 File Offset: 0x0008E420
		public override void remove_ondrag(HTMLMapEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018462 RID: 99426 RVA: 0x0008F510 File Offset: 0x0008E510
		public override void add_onresize(HTMLMapEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018463 RID: 99427 RVA: 0x0008F5A0 File Offset: 0x0008E5A0
		public override void remove_onresize(HTMLMapEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlmapEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018464 RID: 99428 RVA: 0x0008F690 File Offset: 0x0008E690
		public override void add_onblur(HTMLMapEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018465 RID: 99429 RVA: 0x0008F720 File Offset: 0x0008E720
		public override void remove_onblur(HTMLMapEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onblurDelegate != null && ((htmlmapEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018466 RID: 99430 RVA: 0x0008F810 File Offset: 0x0008E810
		public override void add_onfocus(HTMLMapEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018467 RID: 99431 RVA: 0x0008F8A0 File Offset: 0x0008E8A0
		public override void remove_onfocus(HTMLMapEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlmapEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018468 RID: 99432 RVA: 0x0008F990 File Offset: 0x0008E990
		public override void add_onscroll(HTMLMapEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018469 RID: 99433 RVA: 0x0008FA20 File Offset: 0x0008EA20
		public override void remove_onscroll(HTMLMapEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlmapEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601846A RID: 99434 RVA: 0x0008FB10 File Offset: 0x0008EB10
		public override void add_onpropertychange(HTMLMapEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601846B RID: 99435 RVA: 0x0008FBA0 File Offset: 0x0008EBA0
		public override void remove_onpropertychange(HTMLMapEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlmapEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601846C RID: 99436 RVA: 0x0008FC90 File Offset: 0x0008EC90
		public override void add_onlosecapture(HTMLMapEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601846D RID: 99437 RVA: 0x0008FD20 File Offset: 0x0008ED20
		public override void remove_onlosecapture(HTMLMapEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlmapEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601846E RID: 99438 RVA: 0x0008FE10 File Offset: 0x0008EE10
		public override void add_ondatasetcomplete(HTMLMapEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601846F RID: 99439 RVA: 0x0008FEA0 File Offset: 0x0008EEA0
		public override void remove_ondatasetcomplete(HTMLMapEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018470 RID: 99440 RVA: 0x0008FF90 File Offset: 0x0008EF90
		public override void add_ondataavailable(HTMLMapEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018471 RID: 99441 RVA: 0x00090020 File Offset: 0x0008F020
		public override void remove_ondataavailable(HTMLMapEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018472 RID: 99442 RVA: 0x00090110 File Offset: 0x0008F110
		public override void add_ondatasetchanged(HTMLMapEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018473 RID: 99443 RVA: 0x000901A0 File Offset: 0x0008F1A0
		public override void remove_ondatasetchanged(HTMLMapEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018474 RID: 99444 RVA: 0x00090290 File Offset: 0x0008F290
		public override void add_onrowenter(HTMLMapEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018475 RID: 99445 RVA: 0x00090320 File Offset: 0x0008F320
		public override void remove_onrowenter(HTMLMapEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlmapEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018476 RID: 99446 RVA: 0x00090410 File Offset: 0x0008F410
		public override void add_onrowexit(HTMLMapEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018477 RID: 99447 RVA: 0x000904A0 File Offset: 0x0008F4A0
		public override void remove_onrowexit(HTMLMapEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlmapEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018478 RID: 99448 RVA: 0x00090590 File Offset: 0x0008F590
		public override void add_onerrorupdate(HTMLMapEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018479 RID: 99449 RVA: 0x00090620 File Offset: 0x0008F620
		public override void remove_onerrorupdate(HTMLMapEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601847A RID: 99450 RVA: 0x00090710 File Offset: 0x0008F710
		public override void add_onafterupdate(HTMLMapEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601847B RID: 99451 RVA: 0x000907A0 File Offset: 0x0008F7A0
		public override void remove_onafterupdate(HTMLMapEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601847C RID: 99452 RVA: 0x00090890 File Offset: 0x0008F890
		public override void add_onbeforeupdate(HTMLMapEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601847D RID: 99453 RVA: 0x00090920 File Offset: 0x0008F920
		public override void remove_onbeforeupdate(HTMLMapEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlmapEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601847E RID: 99454 RVA: 0x00090A10 File Offset: 0x0008FA10
		public override void add_ondragstart(HTMLMapEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601847F RID: 99455 RVA: 0x00090AA0 File Offset: 0x0008FAA0
		public override void remove_ondragstart(HTMLMapEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018480 RID: 99456 RVA: 0x00090B90 File Offset: 0x0008FB90
		public override void add_onfilterchange(HTMLMapEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018481 RID: 99457 RVA: 0x00090C20 File Offset: 0x0008FC20
		public override void remove_onfilterchange(HTMLMapEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlmapEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018482 RID: 99458 RVA: 0x00090D10 File Offset: 0x0008FD10
		public override void add_onselectstart(HTMLMapEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018483 RID: 99459 RVA: 0x00090DA0 File Offset: 0x0008FDA0
		public override void remove_onselectstart(HTMLMapEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlmapEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018484 RID: 99460 RVA: 0x00090E90 File Offset: 0x0008FE90
		public override void add_onmouseup(HTMLMapEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018485 RID: 99461 RVA: 0x00090F20 File Offset: 0x0008FF20
		public override void remove_onmouseup(HTMLMapEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018486 RID: 99462 RVA: 0x00091010 File Offset: 0x00090010
		public override void add_onmousedown(HTMLMapEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018487 RID: 99463 RVA: 0x000910A0 File Offset: 0x000900A0
		public override void remove_onmousedown(HTMLMapEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018488 RID: 99464 RVA: 0x00091190 File Offset: 0x00090190
		public override void add_onmousemove(HTMLMapEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018489 RID: 99465 RVA: 0x00091220 File Offset: 0x00090220
		public override void remove_onmousemove(HTMLMapEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601848A RID: 99466 RVA: 0x00091310 File Offset: 0x00090310
		public override void add_onmouseover(HTMLMapEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601848B RID: 99467 RVA: 0x000913A0 File Offset: 0x000903A0
		public override void remove_onmouseover(HTMLMapEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601848C RID: 99468 RVA: 0x00091490 File Offset: 0x00090490
		public override void add_onmouseout(HTMLMapEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601848D RID: 99469 RVA: 0x00091520 File Offset: 0x00090520
		public override void remove_onmouseout(HTMLMapEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlmapEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601848E RID: 99470 RVA: 0x00091610 File Offset: 0x00090610
		public override void add_onkeyup(HTMLMapEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x0601848F RID: 99471 RVA: 0x000916A0 File Offset: 0x000906A0
		public override void remove_onkeyup(HTMLMapEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlmapEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018490 RID: 99472 RVA: 0x00091790 File Offset: 0x00090790
		public override void add_onkeydown(HTMLMapEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018491 RID: 99473 RVA: 0x00091820 File Offset: 0x00090820
		public override void remove_onkeydown(HTMLMapEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlmapEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018492 RID: 99474 RVA: 0x00091910 File Offset: 0x00090910
		public override void add_onkeypress(HTMLMapEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018493 RID: 99475 RVA: 0x000919A0 File Offset: 0x000909A0
		public override void remove_onkeypress(HTMLMapEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlmapEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018494 RID: 99476 RVA: 0x00091A90 File Offset: 0x00090A90
		public override void add_ondblclick(HTMLMapEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018495 RID: 99477 RVA: 0x00091B20 File Offset: 0x00090B20
		public override void remove_ondblclick(HTMLMapEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlmapEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018496 RID: 99478 RVA: 0x00091C10 File Offset: 0x00090C10
		public override void add_onclick(HTMLMapEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018497 RID: 99479 RVA: 0x00091CA0 File Offset: 0x00090CA0
		public override void remove_onclick(HTMLMapEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onclickDelegate != null && ((htmlmapEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018498 RID: 99480 RVA: 0x00091D90 File Offset: 0x00090D90
		public override void add_onhelp(HTMLMapEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = new HTMLMapEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents2_SinkHelper, ref num);
				htmlmapEvents2_SinkHelper.m_dwCookie = num;
				htmlmapEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents2_SinkHelper);
			}
		}

		// Token: 0x06018499 RID: 99481 RVA: 0x00091E20 File Offset: 0x00090E20
		public override void remove_onhelp(HTMLMapEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper;
					for (;;)
					{
						htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlmapEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601849A RID: 99482 RVA: 0x00091F10 File Offset: 0x00090F10
		public HTMLMapEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601849B RID: 99483 RVA: 0x00091F38 File Offset: 0x00090F38
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
								HTMLMapEvents2_SinkHelper htmlmapEvents2_SinkHelper = (HTMLMapEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlmapEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x0601849C RID: 99484 RVA: 0x00091FEC File Offset: 0x00090FEC
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000A34 RID: 2612
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000A35 RID: 2613
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000A36 RID: 2614
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
