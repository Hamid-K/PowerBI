using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC5 RID: 3525
	internal sealed class HTMLLabelEvents_EventProvider : HTMLLabelEvents_Event, IDisposable
	{
		// Token: 0x060176B5 RID: 95925 RVA: 0x00011F0C File Offset: 0x00010F0C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				41, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060176B6 RID: 95926 RVA: 0x00012020 File Offset: 0x00011020
		public override void add_onfocusout(HTMLLabelEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176B7 RID: 95927 RVA: 0x000120B0 File Offset: 0x000110B0
		public override void remove_onfocusout(HTMLLabelEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmllabelEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176B8 RID: 95928 RVA: 0x000121A0 File Offset: 0x000111A0
		public override void add_onfocusin(HTMLLabelEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176B9 RID: 95929 RVA: 0x00012230 File Offset: 0x00011230
		public override void remove_onfocusin(HTMLLabelEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onfocusinDelegate != null && ((htmllabelEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176BA RID: 95930 RVA: 0x00012320 File Offset: 0x00011320
		public override void add_ondeactivate(HTMLLabelEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176BB RID: 95931 RVA: 0x000123B0 File Offset: 0x000113B0
		public override void remove_ondeactivate(HTMLLabelEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmllabelEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176BC RID: 95932 RVA: 0x000124A0 File Offset: 0x000114A0
		public override void add_onactivate(HTMLLabelEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176BD RID: 95933 RVA: 0x00012530 File Offset: 0x00011530
		public override void remove_onactivate(HTMLLabelEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onactivateDelegate != null && ((htmllabelEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176BE RID: 95934 RVA: 0x00012620 File Offset: 0x00011620
		public override void add_onmousewheel(HTMLLabelEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176BF RID: 95935 RVA: 0x000126B0 File Offset: 0x000116B0
		public override void remove_onmousewheel(HTMLLabelEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmllabelEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176C0 RID: 95936 RVA: 0x000127A0 File Offset: 0x000117A0
		public override void add_onmouseleave(HTMLLabelEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176C1 RID: 95937 RVA: 0x00012830 File Offset: 0x00011830
		public override void remove_onmouseleave(HTMLLabelEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmllabelEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176C2 RID: 95938 RVA: 0x00012920 File Offset: 0x00011920
		public override void add_onmouseenter(HTMLLabelEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176C3 RID: 95939 RVA: 0x000129B0 File Offset: 0x000119B0
		public override void remove_onmouseenter(HTMLLabelEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmllabelEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176C4 RID: 95940 RVA: 0x00012AA0 File Offset: 0x00011AA0
		public override void add_onresizeend(HTMLLabelEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176C5 RID: 95941 RVA: 0x00012B30 File Offset: 0x00011B30
		public override void remove_onresizeend(HTMLLabelEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onresizeendDelegate != null && ((htmllabelEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176C6 RID: 95942 RVA: 0x00012C20 File Offset: 0x00011C20
		public override void add_onresizestart(HTMLLabelEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176C7 RID: 95943 RVA: 0x00012CB0 File Offset: 0x00011CB0
		public override void remove_onresizestart(HTMLLabelEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onresizestartDelegate != null && ((htmllabelEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176C8 RID: 95944 RVA: 0x00012DA0 File Offset: 0x00011DA0
		public override void add_onmoveend(HTMLLabelEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176C9 RID: 95945 RVA: 0x00012E30 File Offset: 0x00011E30
		public override void remove_onmoveend(HTMLLabelEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmoveendDelegate != null && ((htmllabelEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176CA RID: 95946 RVA: 0x00012F20 File Offset: 0x00011F20
		public override void add_onmovestart(HTMLLabelEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176CB RID: 95947 RVA: 0x00012FB0 File Offset: 0x00011FB0
		public override void remove_onmovestart(HTMLLabelEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmovestartDelegate != null && ((htmllabelEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176CC RID: 95948 RVA: 0x000130A0 File Offset: 0x000120A0
		public override void add_oncontrolselect(HTMLLabelEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176CD RID: 95949 RVA: 0x00013130 File Offset: 0x00012130
		public override void remove_oncontrolselect(HTMLLabelEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmllabelEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176CE RID: 95950 RVA: 0x00013220 File Offset: 0x00012220
		public override void add_onmove(HTMLLabelEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176CF RID: 95951 RVA: 0x000132B0 File Offset: 0x000122B0
		public override void remove_onmove(HTMLLabelEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmoveDelegate != null && ((htmllabelEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176D0 RID: 95952 RVA: 0x000133A0 File Offset: 0x000123A0
		public override void add_onbeforeactivate(HTMLLabelEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176D1 RID: 95953 RVA: 0x00013430 File Offset: 0x00012430
		public override void remove_onbeforeactivate(HTMLLabelEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176D2 RID: 95954 RVA: 0x00013520 File Offset: 0x00012520
		public override void add_onbeforedeactivate(HTMLLabelEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176D3 RID: 95955 RVA: 0x000135B0 File Offset: 0x000125B0
		public override void remove_onbeforedeactivate(HTMLLabelEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176D4 RID: 95956 RVA: 0x000136A0 File Offset: 0x000126A0
		public override void add_onpage(HTMLLabelEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176D5 RID: 95957 RVA: 0x00013730 File Offset: 0x00012730
		public override void remove_onpage(HTMLLabelEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onpageDelegate != null && ((htmllabelEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176D6 RID: 95958 RVA: 0x00013820 File Offset: 0x00012820
		public override void add_onlayoutcomplete(HTMLLabelEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176D7 RID: 95959 RVA: 0x000138B0 File Offset: 0x000128B0
		public override void remove_onlayoutcomplete(HTMLLabelEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmllabelEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176D8 RID: 95960 RVA: 0x000139A0 File Offset: 0x000129A0
		public override void add_onbeforeeditfocus(HTMLLabelEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176D9 RID: 95961 RVA: 0x00013A30 File Offset: 0x00012A30
		public override void remove_onbeforeeditfocus(HTMLLabelEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176DA RID: 95962 RVA: 0x00013B20 File Offset: 0x00012B20
		public override void add_onreadystatechange(HTMLLabelEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176DB RID: 95963 RVA: 0x00013BB0 File Offset: 0x00012BB0
		public override void remove_onreadystatechange(HTMLLabelEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmllabelEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176DC RID: 95964 RVA: 0x00013CA0 File Offset: 0x00012CA0
		public override void add_oncellchange(HTMLLabelEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176DD RID: 95965 RVA: 0x00013D30 File Offset: 0x00012D30
		public override void remove_oncellchange(HTMLLabelEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmllabelEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176DE RID: 95966 RVA: 0x00013E20 File Offset: 0x00012E20
		public override void add_onrowsinserted(HTMLLabelEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176DF RID: 95967 RVA: 0x00013EB0 File Offset: 0x00012EB0
		public override void remove_onrowsinserted(HTMLLabelEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmllabelEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176E0 RID: 95968 RVA: 0x00013FA0 File Offset: 0x00012FA0
		public override void add_onrowsdelete(HTMLLabelEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176E1 RID: 95969 RVA: 0x00014030 File Offset: 0x00013030
		public override void remove_onrowsdelete(HTMLLabelEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmllabelEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176E2 RID: 95970 RVA: 0x00014120 File Offset: 0x00013120
		public override void add_oncontextmenu(HTMLLabelEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176E3 RID: 95971 RVA: 0x000141B0 File Offset: 0x000131B0
		public override void remove_oncontextmenu(HTMLLabelEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmllabelEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176E4 RID: 95972 RVA: 0x000142A0 File Offset: 0x000132A0
		public override void add_onpaste(HTMLLabelEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176E5 RID: 95973 RVA: 0x00014330 File Offset: 0x00013330
		public override void remove_onpaste(HTMLLabelEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onpasteDelegate != null && ((htmllabelEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176E6 RID: 95974 RVA: 0x00014420 File Offset: 0x00013420
		public override void add_onbeforepaste(HTMLLabelEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176E7 RID: 95975 RVA: 0x000144B0 File Offset: 0x000134B0
		public override void remove_onbeforepaste(HTMLLabelEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176E8 RID: 95976 RVA: 0x000145A0 File Offset: 0x000135A0
		public override void add_oncopy(HTMLLabelEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176E9 RID: 95977 RVA: 0x00014630 File Offset: 0x00013630
		public override void remove_oncopy(HTMLLabelEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_oncopyDelegate != null && ((htmllabelEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176EA RID: 95978 RVA: 0x00014720 File Offset: 0x00013720
		public override void add_onbeforecopy(HTMLLabelEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176EB RID: 95979 RVA: 0x000147B0 File Offset: 0x000137B0
		public override void remove_onbeforecopy(HTMLLabelEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176EC RID: 95980 RVA: 0x000148A0 File Offset: 0x000138A0
		public override void add_oncut(HTMLLabelEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176ED RID: 95981 RVA: 0x00014930 File Offset: 0x00013930
		public override void remove_oncut(HTMLLabelEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_oncutDelegate != null && ((htmllabelEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176EE RID: 95982 RVA: 0x00014A20 File Offset: 0x00013A20
		public override void add_onbeforecut(HTMLLabelEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176EF RID: 95983 RVA: 0x00014AB0 File Offset: 0x00013AB0
		public override void remove_onbeforecut(HTMLLabelEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176F0 RID: 95984 RVA: 0x00014BA0 File Offset: 0x00013BA0
		public override void add_ondrop(HTMLLabelEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176F1 RID: 95985 RVA: 0x00014C30 File Offset: 0x00013C30
		public override void remove_ondrop(HTMLLabelEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondropDelegate != null && ((htmllabelEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176F2 RID: 95986 RVA: 0x00014D20 File Offset: 0x00013D20
		public override void add_ondragleave(HTMLLabelEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176F3 RID: 95987 RVA: 0x00014DB0 File Offset: 0x00013DB0
		public override void remove_ondragleave(HTMLLabelEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176F4 RID: 95988 RVA: 0x00014EA0 File Offset: 0x00013EA0
		public override void add_ondragover(HTMLLabelEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176F5 RID: 95989 RVA: 0x00014F30 File Offset: 0x00013F30
		public override void remove_ondragover(HTMLLabelEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragoverDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176F6 RID: 95990 RVA: 0x00015020 File Offset: 0x00014020
		public override void add_ondragenter(HTMLLabelEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176F7 RID: 95991 RVA: 0x000150B0 File Offset: 0x000140B0
		public override void remove_ondragenter(HTMLLabelEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragenterDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176F8 RID: 95992 RVA: 0x000151A0 File Offset: 0x000141A0
		public override void add_ondragend(HTMLLabelEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176F9 RID: 95993 RVA: 0x00015230 File Offset: 0x00014230
		public override void remove_ondragend(HTMLLabelEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragendDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176FA RID: 95994 RVA: 0x00015320 File Offset: 0x00014320
		public override void add_ondrag(HTMLLabelEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176FB RID: 95995 RVA: 0x000153B0 File Offset: 0x000143B0
		public override void remove_ondrag(HTMLLabelEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176FC RID: 95996 RVA: 0x000154A0 File Offset: 0x000144A0
		public override void add_onresize(HTMLLabelEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176FD RID: 95997 RVA: 0x00015530 File Offset: 0x00014530
		public override void remove_onresize(HTMLLabelEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onresizeDelegate != null && ((htmllabelEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060176FE RID: 95998 RVA: 0x00015620 File Offset: 0x00014620
		public override void add_onblur(HTMLLabelEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x060176FF RID: 95999 RVA: 0x000156B0 File Offset: 0x000146B0
		public override void remove_onblur(HTMLLabelEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onblurDelegate != null && ((htmllabelEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017700 RID: 96000 RVA: 0x000157A0 File Offset: 0x000147A0
		public override void add_onfocus(HTMLLabelEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017701 RID: 96001 RVA: 0x00015830 File Offset: 0x00014830
		public override void remove_onfocus(HTMLLabelEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onfocusDelegate != null && ((htmllabelEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017702 RID: 96002 RVA: 0x00015920 File Offset: 0x00014920
		public override void add_onscroll(HTMLLabelEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017703 RID: 96003 RVA: 0x000159B0 File Offset: 0x000149B0
		public override void remove_onscroll(HTMLLabelEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onscrollDelegate != null && ((htmllabelEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017704 RID: 96004 RVA: 0x00015AA0 File Offset: 0x00014AA0
		public override void add_onpropertychange(HTMLLabelEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017705 RID: 96005 RVA: 0x00015B30 File Offset: 0x00014B30
		public override void remove_onpropertychange(HTMLLabelEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmllabelEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017706 RID: 96006 RVA: 0x00015C20 File Offset: 0x00014C20
		public override void add_onlosecapture(HTMLLabelEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017707 RID: 96007 RVA: 0x00015CB0 File Offset: 0x00014CB0
		public override void remove_onlosecapture(HTMLLabelEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmllabelEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017708 RID: 96008 RVA: 0x00015DA0 File Offset: 0x00014DA0
		public override void add_ondatasetcomplete(HTMLLabelEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017709 RID: 96009 RVA: 0x00015E30 File Offset: 0x00014E30
		public override void remove_ondatasetcomplete(HTMLLabelEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmllabelEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601770A RID: 96010 RVA: 0x00015F20 File Offset: 0x00014F20
		public override void add_ondataavailable(HTMLLabelEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601770B RID: 96011 RVA: 0x00015FB0 File Offset: 0x00014FB0
		public override void remove_ondataavailable(HTMLLabelEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmllabelEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601770C RID: 96012 RVA: 0x000160A0 File Offset: 0x000150A0
		public override void add_ondatasetchanged(HTMLLabelEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601770D RID: 96013 RVA: 0x00016130 File Offset: 0x00015130
		public override void remove_ondatasetchanged(HTMLLabelEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmllabelEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601770E RID: 96014 RVA: 0x00016220 File Offset: 0x00015220
		public override void add_onrowenter(HTMLLabelEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601770F RID: 96015 RVA: 0x000162B0 File Offset: 0x000152B0
		public override void remove_onrowenter(HTMLLabelEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onrowenterDelegate != null && ((htmllabelEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017710 RID: 96016 RVA: 0x000163A0 File Offset: 0x000153A0
		public override void add_onrowexit(HTMLLabelEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017711 RID: 96017 RVA: 0x00016430 File Offset: 0x00015430
		public override void remove_onrowexit(HTMLLabelEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onrowexitDelegate != null && ((htmllabelEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017712 RID: 96018 RVA: 0x00016520 File Offset: 0x00015520
		public override void add_onerrorupdate(HTMLLabelEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017713 RID: 96019 RVA: 0x000165B0 File Offset: 0x000155B0
		public override void remove_onerrorupdate(HTMLLabelEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmllabelEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017714 RID: 96020 RVA: 0x000166A0 File Offset: 0x000156A0
		public override void add_onafterupdate(HTMLLabelEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017715 RID: 96021 RVA: 0x00016730 File Offset: 0x00015730
		public override void remove_onafterupdate(HTMLLabelEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmllabelEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017716 RID: 96022 RVA: 0x00016820 File Offset: 0x00015820
		public override void add_onbeforeupdate(HTMLLabelEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017717 RID: 96023 RVA: 0x000168B0 File Offset: 0x000158B0
		public override void remove_onbeforeupdate(HTMLLabelEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmllabelEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017718 RID: 96024 RVA: 0x000169A0 File Offset: 0x000159A0
		public override void add_ondragstart(HTMLLabelEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017719 RID: 96025 RVA: 0x00016A30 File Offset: 0x00015A30
		public override void remove_ondragstart(HTMLLabelEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondragstartDelegate != null && ((htmllabelEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601771A RID: 96026 RVA: 0x00016B20 File Offset: 0x00015B20
		public override void add_onfilterchange(HTMLLabelEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601771B RID: 96027 RVA: 0x00016BB0 File Offset: 0x00015BB0
		public override void remove_onfilterchange(HTMLLabelEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmllabelEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601771C RID: 96028 RVA: 0x00016CA0 File Offset: 0x00015CA0
		public override void add_onselectstart(HTMLLabelEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601771D RID: 96029 RVA: 0x00016D30 File Offset: 0x00015D30
		public override void remove_onselectstart(HTMLLabelEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onselectstartDelegate != null && ((htmllabelEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601771E RID: 96030 RVA: 0x00016E20 File Offset: 0x00015E20
		public override void add_onmouseup(HTMLLabelEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601771F RID: 96031 RVA: 0x00016EB0 File Offset: 0x00015EB0
		public override void remove_onmouseup(HTMLLabelEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmouseupDelegate != null && ((htmllabelEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017720 RID: 96032 RVA: 0x00016FA0 File Offset: 0x00015FA0
		public override void add_onmousedown(HTMLLabelEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017721 RID: 96033 RVA: 0x00017030 File Offset: 0x00016030
		public override void remove_onmousedown(HTMLLabelEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmousedownDelegate != null && ((htmllabelEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017722 RID: 96034 RVA: 0x00017120 File Offset: 0x00016120
		public override void add_onmousemove(HTMLLabelEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017723 RID: 96035 RVA: 0x000171B0 File Offset: 0x000161B0
		public override void remove_onmousemove(HTMLLabelEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmllabelEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017724 RID: 96036 RVA: 0x000172A0 File Offset: 0x000162A0
		public override void add_onmouseover(HTMLLabelEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017725 RID: 96037 RVA: 0x00017330 File Offset: 0x00016330
		public override void remove_onmouseover(HTMLLabelEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmllabelEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017726 RID: 96038 RVA: 0x00017420 File Offset: 0x00016420
		public override void add_onmouseout(HTMLLabelEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017727 RID: 96039 RVA: 0x000174B0 File Offset: 0x000164B0
		public override void remove_onmouseout(HTMLLabelEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmllabelEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017728 RID: 96040 RVA: 0x000175A0 File Offset: 0x000165A0
		public override void add_onkeyup(HTMLLabelEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017729 RID: 96041 RVA: 0x00017630 File Offset: 0x00016630
		public override void remove_onkeyup(HTMLLabelEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onkeyupDelegate != null && ((htmllabelEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601772A RID: 96042 RVA: 0x00017720 File Offset: 0x00016720
		public override void add_onkeydown(HTMLLabelEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601772B RID: 96043 RVA: 0x000177B0 File Offset: 0x000167B0
		public override void remove_onkeydown(HTMLLabelEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onkeydownDelegate != null && ((htmllabelEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601772C RID: 96044 RVA: 0x000178A0 File Offset: 0x000168A0
		public override void add_onkeypress(HTMLLabelEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601772D RID: 96045 RVA: 0x00017930 File Offset: 0x00016930
		public override void remove_onkeypress(HTMLLabelEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onkeypressDelegate != null && ((htmllabelEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601772E RID: 96046 RVA: 0x00017A20 File Offset: 0x00016A20
		public override void add_ondblclick(HTMLLabelEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x0601772F RID: 96047 RVA: 0x00017AB0 File Offset: 0x00016AB0
		public override void remove_ondblclick(HTMLLabelEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_ondblclickDelegate != null && ((htmllabelEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017730 RID: 96048 RVA: 0x00017BA0 File Offset: 0x00016BA0
		public override void add_onclick(HTMLLabelEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017731 RID: 96049 RVA: 0x00017C30 File Offset: 0x00016C30
		public override void remove_onclick(HTMLLabelEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onclickDelegate != null && ((htmllabelEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017732 RID: 96050 RVA: 0x00017D20 File Offset: 0x00016D20
		public override void add_onhelp(HTMLLabelEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = new HTMLLabelEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmllabelEvents_SinkHelper, ref num);
				htmllabelEvents_SinkHelper.m_dwCookie = num;
				htmllabelEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmllabelEvents_SinkHelper);
			}
		}

		// Token: 0x06017733 RID: 96051 RVA: 0x00017DB0 File Offset: 0x00016DB0
		public override void remove_onhelp(HTMLLabelEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper;
					for (;;)
					{
						htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmllabelEvents_SinkHelper.m_onhelpDelegate != null && ((htmllabelEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017734 RID: 96052 RVA: 0x00017EA0 File Offset: 0x00016EA0
		public HTMLLabelEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017735 RID: 96053 RVA: 0x00017EC8 File Offset: 0x00016EC8
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
								HTMLLabelEvents_SinkHelper htmllabelEvents_SinkHelper = (HTMLLabelEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmllabelEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017736 RID: 96054 RVA: 0x00017F7C File Offset: 0x00016F7C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400058E RID: 1422
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400058F RID: 1423
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000590 RID: 1424
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
