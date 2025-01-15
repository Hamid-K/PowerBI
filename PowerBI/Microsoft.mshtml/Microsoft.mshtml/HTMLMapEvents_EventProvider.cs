using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE5 RID: 3557
	internal sealed class HTMLMapEvents_EventProvider : HTMLMapEvents_Event, IDisposable
	{
		// Token: 0x060181D3 RID: 98771 RVA: 0x00077210 File Offset: 0x00076210
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				186, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060181D4 RID: 98772 RVA: 0x00077324 File Offset: 0x00076324
		public override void add_onfocusout(HTMLMapEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181D5 RID: 98773 RVA: 0x000773B4 File Offset: 0x000763B4
		public override void remove_onfocusout(HTMLMapEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlmapEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181D6 RID: 98774 RVA: 0x000774A4 File Offset: 0x000764A4
		public override void add_onfocusin(HTMLMapEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181D7 RID: 98775 RVA: 0x00077534 File Offset: 0x00076534
		public override void remove_onfocusin(HTMLMapEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlmapEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181D8 RID: 98776 RVA: 0x00077624 File Offset: 0x00076624
		public override void add_ondeactivate(HTMLMapEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181D9 RID: 98777 RVA: 0x000776B4 File Offset: 0x000766B4
		public override void remove_ondeactivate(HTMLMapEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlmapEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181DA RID: 98778 RVA: 0x000777A4 File Offset: 0x000767A4
		public override void add_onactivate(HTMLMapEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181DB RID: 98779 RVA: 0x00077834 File Offset: 0x00076834
		public override void remove_onactivate(HTMLMapEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onactivateDelegate != null && ((htmlmapEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181DC RID: 98780 RVA: 0x00077924 File Offset: 0x00076924
		public override void add_onmousewheel(HTMLMapEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181DD RID: 98781 RVA: 0x000779B4 File Offset: 0x000769B4
		public override void remove_onmousewheel(HTMLMapEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlmapEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181DE RID: 98782 RVA: 0x00077AA4 File Offset: 0x00076AA4
		public override void add_onmouseleave(HTMLMapEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181DF RID: 98783 RVA: 0x00077B34 File Offset: 0x00076B34
		public override void remove_onmouseleave(HTMLMapEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlmapEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181E0 RID: 98784 RVA: 0x00077C24 File Offset: 0x00076C24
		public override void add_onmouseenter(HTMLMapEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181E1 RID: 98785 RVA: 0x00077CB4 File Offset: 0x00076CB4
		public override void remove_onmouseenter(HTMLMapEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlmapEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181E2 RID: 98786 RVA: 0x00077DA4 File Offset: 0x00076DA4
		public override void add_onresizeend(HTMLMapEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181E3 RID: 98787 RVA: 0x00077E34 File Offset: 0x00076E34
		public override void remove_onresizeend(HTMLMapEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlmapEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181E4 RID: 98788 RVA: 0x00077F24 File Offset: 0x00076F24
		public override void add_onresizestart(HTMLMapEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181E5 RID: 98789 RVA: 0x00077FB4 File Offset: 0x00076FB4
		public override void remove_onresizestart(HTMLMapEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlmapEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181E6 RID: 98790 RVA: 0x000780A4 File Offset: 0x000770A4
		public override void add_onmoveend(HTMLMapEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181E7 RID: 98791 RVA: 0x00078134 File Offset: 0x00077134
		public override void remove_onmoveend(HTMLMapEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlmapEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181E8 RID: 98792 RVA: 0x00078224 File Offset: 0x00077224
		public override void add_onmovestart(HTMLMapEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181E9 RID: 98793 RVA: 0x000782B4 File Offset: 0x000772B4
		public override void remove_onmovestart(HTMLMapEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlmapEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181EA RID: 98794 RVA: 0x000783A4 File Offset: 0x000773A4
		public override void add_oncontrolselect(HTMLMapEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181EB RID: 98795 RVA: 0x00078434 File Offset: 0x00077434
		public override void remove_oncontrolselect(HTMLMapEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlmapEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181EC RID: 98796 RVA: 0x00078524 File Offset: 0x00077524
		public override void add_onmove(HTMLMapEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181ED RID: 98797 RVA: 0x000785B4 File Offset: 0x000775B4
		public override void remove_onmove(HTMLMapEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmoveDelegate != null && ((htmlmapEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181EE RID: 98798 RVA: 0x000786A4 File Offset: 0x000776A4
		public override void add_onbeforeactivate(HTMLMapEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181EF RID: 98799 RVA: 0x00078734 File Offset: 0x00077734
		public override void remove_onbeforeactivate(HTMLMapEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181F0 RID: 98800 RVA: 0x00078824 File Offset: 0x00077824
		public override void add_onbeforedeactivate(HTMLMapEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181F1 RID: 98801 RVA: 0x000788B4 File Offset: 0x000778B4
		public override void remove_onbeforedeactivate(HTMLMapEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181F2 RID: 98802 RVA: 0x000789A4 File Offset: 0x000779A4
		public override void add_onpage(HTMLMapEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181F3 RID: 98803 RVA: 0x00078A34 File Offset: 0x00077A34
		public override void remove_onpage(HTMLMapEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onpageDelegate != null && ((htmlmapEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181F4 RID: 98804 RVA: 0x00078B24 File Offset: 0x00077B24
		public override void add_onlayoutcomplete(HTMLMapEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181F5 RID: 98805 RVA: 0x00078BB4 File Offset: 0x00077BB4
		public override void remove_onlayoutcomplete(HTMLMapEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlmapEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181F6 RID: 98806 RVA: 0x00078CA4 File Offset: 0x00077CA4
		public override void add_onbeforeeditfocus(HTMLMapEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181F7 RID: 98807 RVA: 0x00078D34 File Offset: 0x00077D34
		public override void remove_onbeforeeditfocus(HTMLMapEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181F8 RID: 98808 RVA: 0x00078E24 File Offset: 0x00077E24
		public override void add_onreadystatechange(HTMLMapEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181F9 RID: 98809 RVA: 0x00078EB4 File Offset: 0x00077EB4
		public override void remove_onreadystatechange(HTMLMapEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlmapEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181FA RID: 98810 RVA: 0x00078FA4 File Offset: 0x00077FA4
		public override void add_oncellchange(HTMLMapEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181FB RID: 98811 RVA: 0x00079034 File Offset: 0x00078034
		public override void remove_oncellchange(HTMLMapEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlmapEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181FC RID: 98812 RVA: 0x00079124 File Offset: 0x00078124
		public override void add_onrowsinserted(HTMLMapEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181FD RID: 98813 RVA: 0x000791B4 File Offset: 0x000781B4
		public override void remove_onrowsinserted(HTMLMapEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlmapEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060181FE RID: 98814 RVA: 0x000792A4 File Offset: 0x000782A4
		public override void add_onrowsdelete(HTMLMapEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x060181FF RID: 98815 RVA: 0x00079334 File Offset: 0x00078334
		public override void remove_onrowsdelete(HTMLMapEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlmapEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018200 RID: 98816 RVA: 0x00079424 File Offset: 0x00078424
		public override void add_oncontextmenu(HTMLMapEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018201 RID: 98817 RVA: 0x000794B4 File Offset: 0x000784B4
		public override void remove_oncontextmenu(HTMLMapEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlmapEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018202 RID: 98818 RVA: 0x000795A4 File Offset: 0x000785A4
		public override void add_onpaste(HTMLMapEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018203 RID: 98819 RVA: 0x00079634 File Offset: 0x00078634
		public override void remove_onpaste(HTMLMapEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onpasteDelegate != null && ((htmlmapEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018204 RID: 98820 RVA: 0x00079724 File Offset: 0x00078724
		public override void add_onbeforepaste(HTMLMapEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018205 RID: 98821 RVA: 0x000797B4 File Offset: 0x000787B4
		public override void remove_onbeforepaste(HTMLMapEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018206 RID: 98822 RVA: 0x000798A4 File Offset: 0x000788A4
		public override void add_oncopy(HTMLMapEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018207 RID: 98823 RVA: 0x00079934 File Offset: 0x00078934
		public override void remove_oncopy(HTMLMapEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_oncopyDelegate != null && ((htmlmapEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018208 RID: 98824 RVA: 0x00079A24 File Offset: 0x00078A24
		public override void add_onbeforecopy(HTMLMapEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018209 RID: 98825 RVA: 0x00079AB4 File Offset: 0x00078AB4
		public override void remove_onbeforecopy(HTMLMapEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601820A RID: 98826 RVA: 0x00079BA4 File Offset: 0x00078BA4
		public override void add_oncut(HTMLMapEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601820B RID: 98827 RVA: 0x00079C34 File Offset: 0x00078C34
		public override void remove_oncut(HTMLMapEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_oncutDelegate != null && ((htmlmapEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601820C RID: 98828 RVA: 0x00079D24 File Offset: 0x00078D24
		public override void add_onbeforecut(HTMLMapEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601820D RID: 98829 RVA: 0x00079DB4 File Offset: 0x00078DB4
		public override void remove_onbeforecut(HTMLMapEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601820E RID: 98830 RVA: 0x00079EA4 File Offset: 0x00078EA4
		public override void add_ondrop(HTMLMapEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601820F RID: 98831 RVA: 0x00079F34 File Offset: 0x00078F34
		public override void remove_ondrop(HTMLMapEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondropDelegate != null && ((htmlmapEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018210 RID: 98832 RVA: 0x0007A024 File Offset: 0x00079024
		public override void add_ondragleave(HTMLMapEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018211 RID: 98833 RVA: 0x0007A0B4 File Offset: 0x000790B4
		public override void remove_ondragleave(HTMLMapEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018212 RID: 98834 RVA: 0x0007A1A4 File Offset: 0x000791A4
		public override void add_ondragover(HTMLMapEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018213 RID: 98835 RVA: 0x0007A234 File Offset: 0x00079234
		public override void remove_ondragover(HTMLMapEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018214 RID: 98836 RVA: 0x0007A324 File Offset: 0x00079324
		public override void add_ondragenter(HTMLMapEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018215 RID: 98837 RVA: 0x0007A3B4 File Offset: 0x000793B4
		public override void remove_ondragenter(HTMLMapEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018216 RID: 98838 RVA: 0x0007A4A4 File Offset: 0x000794A4
		public override void add_ondragend(HTMLMapEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018217 RID: 98839 RVA: 0x0007A534 File Offset: 0x00079534
		public override void remove_ondragend(HTMLMapEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragendDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018218 RID: 98840 RVA: 0x0007A624 File Offset: 0x00079624
		public override void add_ondrag(HTMLMapEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018219 RID: 98841 RVA: 0x0007A6B4 File Offset: 0x000796B4
		public override void remove_ondrag(HTMLMapEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601821A RID: 98842 RVA: 0x0007A7A4 File Offset: 0x000797A4
		public override void add_onresize(HTMLMapEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601821B RID: 98843 RVA: 0x0007A834 File Offset: 0x00079834
		public override void remove_onresize(HTMLMapEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onresizeDelegate != null && ((htmlmapEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601821C RID: 98844 RVA: 0x0007A924 File Offset: 0x00079924
		public override void add_onblur(HTMLMapEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601821D RID: 98845 RVA: 0x0007A9B4 File Offset: 0x000799B4
		public override void remove_onblur(HTMLMapEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onblurDelegate != null && ((htmlmapEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601821E RID: 98846 RVA: 0x0007AAA4 File Offset: 0x00079AA4
		public override void add_onfocus(HTMLMapEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601821F RID: 98847 RVA: 0x0007AB34 File Offset: 0x00079B34
		public override void remove_onfocus(HTMLMapEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onfocusDelegate != null && ((htmlmapEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018220 RID: 98848 RVA: 0x0007AC24 File Offset: 0x00079C24
		public override void add_onscroll(HTMLMapEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018221 RID: 98849 RVA: 0x0007ACB4 File Offset: 0x00079CB4
		public override void remove_onscroll(HTMLMapEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onscrollDelegate != null && ((htmlmapEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018222 RID: 98850 RVA: 0x0007ADA4 File Offset: 0x00079DA4
		public override void add_onpropertychange(HTMLMapEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018223 RID: 98851 RVA: 0x0007AE34 File Offset: 0x00079E34
		public override void remove_onpropertychange(HTMLMapEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlmapEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018224 RID: 98852 RVA: 0x0007AF24 File Offset: 0x00079F24
		public override void add_onlosecapture(HTMLMapEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018225 RID: 98853 RVA: 0x0007AFB4 File Offset: 0x00079FB4
		public override void remove_onlosecapture(HTMLMapEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlmapEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018226 RID: 98854 RVA: 0x0007B0A4 File Offset: 0x0007A0A4
		public override void add_ondatasetcomplete(HTMLMapEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018227 RID: 98855 RVA: 0x0007B134 File Offset: 0x0007A134
		public override void remove_ondatasetcomplete(HTMLMapEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlmapEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018228 RID: 98856 RVA: 0x0007B224 File Offset: 0x0007A224
		public override void add_ondataavailable(HTMLMapEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018229 RID: 98857 RVA: 0x0007B2B4 File Offset: 0x0007A2B4
		public override void remove_ondataavailable(HTMLMapEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlmapEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601822A RID: 98858 RVA: 0x0007B3A4 File Offset: 0x0007A3A4
		public override void add_ondatasetchanged(HTMLMapEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601822B RID: 98859 RVA: 0x0007B434 File Offset: 0x0007A434
		public override void remove_ondatasetchanged(HTMLMapEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlmapEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601822C RID: 98860 RVA: 0x0007B524 File Offset: 0x0007A524
		public override void add_onrowenter(HTMLMapEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601822D RID: 98861 RVA: 0x0007B5B4 File Offset: 0x0007A5B4
		public override void remove_onrowenter(HTMLMapEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlmapEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601822E RID: 98862 RVA: 0x0007B6A4 File Offset: 0x0007A6A4
		public override void add_onrowexit(HTMLMapEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601822F RID: 98863 RVA: 0x0007B734 File Offset: 0x0007A734
		public override void remove_onrowexit(HTMLMapEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlmapEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018230 RID: 98864 RVA: 0x0007B824 File Offset: 0x0007A824
		public override void add_onerrorupdate(HTMLMapEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018231 RID: 98865 RVA: 0x0007B8B4 File Offset: 0x0007A8B4
		public override void remove_onerrorupdate(HTMLMapEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlmapEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018232 RID: 98866 RVA: 0x0007B9A4 File Offset: 0x0007A9A4
		public override void add_onafterupdate(HTMLMapEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018233 RID: 98867 RVA: 0x0007BA34 File Offset: 0x0007AA34
		public override void remove_onafterupdate(HTMLMapEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlmapEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018234 RID: 98868 RVA: 0x0007BB24 File Offset: 0x0007AB24
		public override void add_onbeforeupdate(HTMLMapEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018235 RID: 98869 RVA: 0x0007BBB4 File Offset: 0x0007ABB4
		public override void remove_onbeforeupdate(HTMLMapEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlmapEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018236 RID: 98870 RVA: 0x0007BCA4 File Offset: 0x0007ACA4
		public override void add_ondragstart(HTMLMapEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018237 RID: 98871 RVA: 0x0007BD34 File Offset: 0x0007AD34
		public override void remove_ondragstart(HTMLMapEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlmapEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018238 RID: 98872 RVA: 0x0007BE24 File Offset: 0x0007AE24
		public override void add_onfilterchange(HTMLMapEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018239 RID: 98873 RVA: 0x0007BEB4 File Offset: 0x0007AEB4
		public override void remove_onfilterchange(HTMLMapEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlmapEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601823A RID: 98874 RVA: 0x0007BFA4 File Offset: 0x0007AFA4
		public override void add_onselectstart(HTMLMapEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601823B RID: 98875 RVA: 0x0007C034 File Offset: 0x0007B034
		public override void remove_onselectstart(HTMLMapEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlmapEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601823C RID: 98876 RVA: 0x0007C124 File Offset: 0x0007B124
		public override void add_onmouseup(HTMLMapEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601823D RID: 98877 RVA: 0x0007C1B4 File Offset: 0x0007B1B4
		public override void remove_onmouseup(HTMLMapEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlmapEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601823E RID: 98878 RVA: 0x0007C2A4 File Offset: 0x0007B2A4
		public override void add_onmousedown(HTMLMapEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601823F RID: 98879 RVA: 0x0007C334 File Offset: 0x0007B334
		public override void remove_onmousedown(HTMLMapEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlmapEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018240 RID: 98880 RVA: 0x0007C424 File Offset: 0x0007B424
		public override void add_onmousemove(HTMLMapEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018241 RID: 98881 RVA: 0x0007C4B4 File Offset: 0x0007B4B4
		public override void remove_onmousemove(HTMLMapEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlmapEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018242 RID: 98882 RVA: 0x0007C5A4 File Offset: 0x0007B5A4
		public override void add_onmouseover(HTMLMapEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018243 RID: 98883 RVA: 0x0007C634 File Offset: 0x0007B634
		public override void remove_onmouseover(HTMLMapEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlmapEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018244 RID: 98884 RVA: 0x0007C724 File Offset: 0x0007B724
		public override void add_onmouseout(HTMLMapEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018245 RID: 98885 RVA: 0x0007C7B4 File Offset: 0x0007B7B4
		public override void remove_onmouseout(HTMLMapEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlmapEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018246 RID: 98886 RVA: 0x0007C8A4 File Offset: 0x0007B8A4
		public override void add_onkeyup(HTMLMapEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018247 RID: 98887 RVA: 0x0007C934 File Offset: 0x0007B934
		public override void remove_onkeyup(HTMLMapEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlmapEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018248 RID: 98888 RVA: 0x0007CA24 File Offset: 0x0007BA24
		public override void add_onkeydown(HTMLMapEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018249 RID: 98889 RVA: 0x0007CAB4 File Offset: 0x0007BAB4
		public override void remove_onkeydown(HTMLMapEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlmapEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601824A RID: 98890 RVA: 0x0007CBA4 File Offset: 0x0007BBA4
		public override void add_onkeypress(HTMLMapEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601824B RID: 98891 RVA: 0x0007CC34 File Offset: 0x0007BC34
		public override void remove_onkeypress(HTMLMapEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlmapEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601824C RID: 98892 RVA: 0x0007CD24 File Offset: 0x0007BD24
		public override void add_ondblclick(HTMLMapEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601824D RID: 98893 RVA: 0x0007CDB4 File Offset: 0x0007BDB4
		public override void remove_ondblclick(HTMLMapEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlmapEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601824E RID: 98894 RVA: 0x0007CEA4 File Offset: 0x0007BEA4
		public override void add_onclick(HTMLMapEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x0601824F RID: 98895 RVA: 0x0007CF34 File Offset: 0x0007BF34
		public override void remove_onclick(HTMLMapEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onclickDelegate != null && ((htmlmapEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018250 RID: 98896 RVA: 0x0007D024 File Offset: 0x0007C024
		public override void add_onhelp(HTMLMapEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = new HTMLMapEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmapEvents_SinkHelper, ref num);
				htmlmapEvents_SinkHelper.m_dwCookie = num;
				htmlmapEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmapEvents_SinkHelper);
			}
		}

		// Token: 0x06018251 RID: 98897 RVA: 0x0007D0B4 File Offset: 0x0007C0B4
		public override void remove_onhelp(HTMLMapEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper;
					for (;;)
					{
						htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmapEvents_SinkHelper.m_onhelpDelegate != null && ((htmlmapEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018252 RID: 98898 RVA: 0x0007D1A4 File Offset: 0x0007C1A4
		public HTMLMapEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018253 RID: 98899 RVA: 0x0007D1CC File Offset: 0x0007C1CC
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
								HTMLMapEvents_SinkHelper htmlmapEvents_SinkHelper = (HTMLMapEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlmapEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06018254 RID: 98900 RVA: 0x0007D280 File Offset: 0x0007C280
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000968 RID: 2408
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000969 RID: 2409
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400096A RID: 2410
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
