using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E0D RID: 3597
	internal sealed class HTMLElementEvents_EventProvider : HTMLElementEvents_Event, IDisposable
	{
		// Token: 0x06018FE7 RID: 102375 RVA: 0x000F75B0 File Offset: 0x000F65B0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				60, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06018FE8 RID: 102376 RVA: 0x000F76C4 File Offset: 0x000F66C4
		public override void add_onfocusout(HTMLElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FE9 RID: 102377 RVA: 0x000F7754 File Offset: 0x000F6754
		public override void remove_onfocusout(HTMLElementEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlelementEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FEA RID: 102378 RVA: 0x000F7844 File Offset: 0x000F6844
		public override void add_onfocusin(HTMLElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FEB RID: 102379 RVA: 0x000F78D4 File Offset: 0x000F68D4
		public override void remove_onfocusin(HTMLElementEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlelementEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FEC RID: 102380 RVA: 0x000F79C4 File Offset: 0x000F69C4
		public override void add_ondeactivate(HTMLElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FED RID: 102381 RVA: 0x000F7A54 File Offset: 0x000F6A54
		public override void remove_ondeactivate(HTMLElementEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlelementEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FEE RID: 102382 RVA: 0x000F7B44 File Offset: 0x000F6B44
		public override void add_onactivate(HTMLElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FEF RID: 102383 RVA: 0x000F7BD4 File Offset: 0x000F6BD4
		public override void remove_onactivate(HTMLElementEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onactivateDelegate != null && ((htmlelementEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FF0 RID: 102384 RVA: 0x000F7CC4 File Offset: 0x000F6CC4
		public override void add_onmousewheel(HTMLElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FF1 RID: 102385 RVA: 0x000F7D54 File Offset: 0x000F6D54
		public override void remove_onmousewheel(HTMLElementEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlelementEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FF2 RID: 102386 RVA: 0x000F7E44 File Offset: 0x000F6E44
		public override void add_onmouseleave(HTMLElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FF3 RID: 102387 RVA: 0x000F7ED4 File Offset: 0x000F6ED4
		public override void remove_onmouseleave(HTMLElementEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlelementEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FF4 RID: 102388 RVA: 0x000F7FC4 File Offset: 0x000F6FC4
		public override void add_onmouseenter(HTMLElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FF5 RID: 102389 RVA: 0x000F8054 File Offset: 0x000F7054
		public override void remove_onmouseenter(HTMLElementEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlelementEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FF6 RID: 102390 RVA: 0x000F8144 File Offset: 0x000F7144
		public override void add_onresizeend(HTMLElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FF7 RID: 102391 RVA: 0x000F81D4 File Offset: 0x000F71D4
		public override void remove_onresizeend(HTMLElementEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlelementEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FF8 RID: 102392 RVA: 0x000F82C4 File Offset: 0x000F72C4
		public override void add_onresizestart(HTMLElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FF9 RID: 102393 RVA: 0x000F8354 File Offset: 0x000F7354
		public override void remove_onresizestart(HTMLElementEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlelementEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FFA RID: 102394 RVA: 0x000F8444 File Offset: 0x000F7444
		public override void add_onmoveend(HTMLElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FFB RID: 102395 RVA: 0x000F84D4 File Offset: 0x000F74D4
		public override void remove_onmoveend(HTMLElementEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlelementEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FFC RID: 102396 RVA: 0x000F85C4 File Offset: 0x000F75C4
		public override void add_onmovestart(HTMLElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FFD RID: 102397 RVA: 0x000F8654 File Offset: 0x000F7654
		public override void remove_onmovestart(HTMLElementEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlelementEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018FFE RID: 102398 RVA: 0x000F8744 File Offset: 0x000F7744
		public override void add_oncontrolselect(HTMLElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06018FFF RID: 102399 RVA: 0x000F87D4 File Offset: 0x000F77D4
		public override void remove_oncontrolselect(HTMLElementEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlelementEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019000 RID: 102400 RVA: 0x000F88C4 File Offset: 0x000F78C4
		public override void add_onmove(HTMLElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019001 RID: 102401 RVA: 0x000F8954 File Offset: 0x000F7954
		public override void remove_onmove(HTMLElementEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmoveDelegate != null && ((htmlelementEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019002 RID: 102402 RVA: 0x000F8A44 File Offset: 0x000F7A44
		public override void add_onbeforeactivate(HTMLElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019003 RID: 102403 RVA: 0x000F8AD4 File Offset: 0x000F7AD4
		public override void remove_onbeforeactivate(HTMLElementEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019004 RID: 102404 RVA: 0x000F8BC4 File Offset: 0x000F7BC4
		public override void add_onbeforedeactivate(HTMLElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019005 RID: 102405 RVA: 0x000F8C54 File Offset: 0x000F7C54
		public override void remove_onbeforedeactivate(HTMLElementEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019006 RID: 102406 RVA: 0x000F8D44 File Offset: 0x000F7D44
		public override void add_onpage(HTMLElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019007 RID: 102407 RVA: 0x000F8DD4 File Offset: 0x000F7DD4
		public override void remove_onpage(HTMLElementEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onpageDelegate != null && ((htmlelementEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019008 RID: 102408 RVA: 0x000F8EC4 File Offset: 0x000F7EC4
		public override void add_onlayoutcomplete(HTMLElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019009 RID: 102409 RVA: 0x000F8F54 File Offset: 0x000F7F54
		public override void remove_onlayoutcomplete(HTMLElementEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlelementEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601900A RID: 102410 RVA: 0x000F9044 File Offset: 0x000F8044
		public override void add_onbeforeeditfocus(HTMLElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601900B RID: 102411 RVA: 0x000F90D4 File Offset: 0x000F80D4
		public override void remove_onbeforeeditfocus(HTMLElementEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601900C RID: 102412 RVA: 0x000F91C4 File Offset: 0x000F81C4
		public override void add_onreadystatechange(HTMLElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601900D RID: 102413 RVA: 0x000F9254 File Offset: 0x000F8254
		public override void remove_onreadystatechange(HTMLElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlelementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601900E RID: 102414 RVA: 0x000F9344 File Offset: 0x000F8344
		public override void add_oncellchange(HTMLElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601900F RID: 102415 RVA: 0x000F93D4 File Offset: 0x000F83D4
		public override void remove_oncellchange(HTMLElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlelementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019010 RID: 102416 RVA: 0x000F94C4 File Offset: 0x000F84C4
		public override void add_onrowsinserted(HTMLElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019011 RID: 102417 RVA: 0x000F9554 File Offset: 0x000F8554
		public override void remove_onrowsinserted(HTMLElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlelementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019012 RID: 102418 RVA: 0x000F9644 File Offset: 0x000F8644
		public override void add_onrowsdelete(HTMLElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019013 RID: 102419 RVA: 0x000F96D4 File Offset: 0x000F86D4
		public override void remove_onrowsdelete(HTMLElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlelementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019014 RID: 102420 RVA: 0x000F97C4 File Offset: 0x000F87C4
		public override void add_oncontextmenu(HTMLElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019015 RID: 102421 RVA: 0x000F9854 File Offset: 0x000F8854
		public override void remove_oncontextmenu(HTMLElementEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlelementEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019016 RID: 102422 RVA: 0x000F9944 File Offset: 0x000F8944
		public override void add_onpaste(HTMLElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019017 RID: 102423 RVA: 0x000F99D4 File Offset: 0x000F89D4
		public override void remove_onpaste(HTMLElementEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onpasteDelegate != null && ((htmlelementEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019018 RID: 102424 RVA: 0x000F9AC4 File Offset: 0x000F8AC4
		public override void add_onbeforepaste(HTMLElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019019 RID: 102425 RVA: 0x000F9B54 File Offset: 0x000F8B54
		public override void remove_onbeforepaste(HTMLElementEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601901A RID: 102426 RVA: 0x000F9C44 File Offset: 0x000F8C44
		public override void add_oncopy(HTMLElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601901B RID: 102427 RVA: 0x000F9CD4 File Offset: 0x000F8CD4
		public override void remove_oncopy(HTMLElementEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_oncopyDelegate != null && ((htmlelementEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601901C RID: 102428 RVA: 0x000F9DC4 File Offset: 0x000F8DC4
		public override void add_onbeforecopy(HTMLElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601901D RID: 102429 RVA: 0x000F9E54 File Offset: 0x000F8E54
		public override void remove_onbeforecopy(HTMLElementEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601901E RID: 102430 RVA: 0x000F9F44 File Offset: 0x000F8F44
		public override void add_oncut(HTMLElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601901F RID: 102431 RVA: 0x000F9FD4 File Offset: 0x000F8FD4
		public override void remove_oncut(HTMLElementEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_oncutDelegate != null && ((htmlelementEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019020 RID: 102432 RVA: 0x000FA0C4 File Offset: 0x000F90C4
		public override void add_onbeforecut(HTMLElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019021 RID: 102433 RVA: 0x000FA154 File Offset: 0x000F9154
		public override void remove_onbeforecut(HTMLElementEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019022 RID: 102434 RVA: 0x000FA244 File Offset: 0x000F9244
		public override void add_ondrop(HTMLElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019023 RID: 102435 RVA: 0x000FA2D4 File Offset: 0x000F92D4
		public override void remove_ondrop(HTMLElementEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondropDelegate != null && ((htmlelementEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019024 RID: 102436 RVA: 0x000FA3C4 File Offset: 0x000F93C4
		public override void add_ondragleave(HTMLElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019025 RID: 102437 RVA: 0x000FA454 File Offset: 0x000F9454
		public override void remove_ondragleave(HTMLElementEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019026 RID: 102438 RVA: 0x000FA544 File Offset: 0x000F9544
		public override void add_ondragover(HTMLElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019027 RID: 102439 RVA: 0x000FA5D4 File Offset: 0x000F95D4
		public override void remove_ondragover(HTMLElementEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019028 RID: 102440 RVA: 0x000FA6C4 File Offset: 0x000F96C4
		public override void add_ondragenter(HTMLElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019029 RID: 102441 RVA: 0x000FA754 File Offset: 0x000F9754
		public override void remove_ondragenter(HTMLElementEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601902A RID: 102442 RVA: 0x000FA844 File Offset: 0x000F9844
		public override void add_ondragend(HTMLElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601902B RID: 102443 RVA: 0x000FA8D4 File Offset: 0x000F98D4
		public override void remove_ondragend(HTMLElementEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragendDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601902C RID: 102444 RVA: 0x000FA9C4 File Offset: 0x000F99C4
		public override void add_ondrag(HTMLElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601902D RID: 102445 RVA: 0x000FAA54 File Offset: 0x000F9A54
		public override void remove_ondrag(HTMLElementEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601902E RID: 102446 RVA: 0x000FAB44 File Offset: 0x000F9B44
		public override void add_onresize(HTMLElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601902F RID: 102447 RVA: 0x000FABD4 File Offset: 0x000F9BD4
		public override void remove_onresize(HTMLElementEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onresizeDelegate != null && ((htmlelementEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019030 RID: 102448 RVA: 0x000FACC4 File Offset: 0x000F9CC4
		public override void add_onblur(HTMLElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019031 RID: 102449 RVA: 0x000FAD54 File Offset: 0x000F9D54
		public override void remove_onblur(HTMLElementEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onblurDelegate != null && ((htmlelementEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019032 RID: 102450 RVA: 0x000FAE44 File Offset: 0x000F9E44
		public override void add_onfocus(HTMLElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019033 RID: 102451 RVA: 0x000FAED4 File Offset: 0x000F9ED4
		public override void remove_onfocus(HTMLElementEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onfocusDelegate != null && ((htmlelementEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019034 RID: 102452 RVA: 0x000FAFC4 File Offset: 0x000F9FC4
		public override void add_onscroll(HTMLElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019035 RID: 102453 RVA: 0x000FB054 File Offset: 0x000FA054
		public override void remove_onscroll(HTMLElementEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onscrollDelegate != null && ((htmlelementEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019036 RID: 102454 RVA: 0x000FB144 File Offset: 0x000FA144
		public override void add_onpropertychange(HTMLElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019037 RID: 102455 RVA: 0x000FB1D4 File Offset: 0x000FA1D4
		public override void remove_onpropertychange(HTMLElementEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlelementEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019038 RID: 102456 RVA: 0x000FB2C4 File Offset: 0x000FA2C4
		public override void add_onlosecapture(HTMLElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019039 RID: 102457 RVA: 0x000FB354 File Offset: 0x000FA354
		public override void remove_onlosecapture(HTMLElementEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlelementEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601903A RID: 102458 RVA: 0x000FB444 File Offset: 0x000FA444
		public override void add_ondatasetcomplete(HTMLElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601903B RID: 102459 RVA: 0x000FB4D4 File Offset: 0x000FA4D4
		public override void remove_ondatasetcomplete(HTMLElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlelementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601903C RID: 102460 RVA: 0x000FB5C4 File Offset: 0x000FA5C4
		public override void add_ondataavailable(HTMLElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601903D RID: 102461 RVA: 0x000FB654 File Offset: 0x000FA654
		public override void remove_ondataavailable(HTMLElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlelementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601903E RID: 102462 RVA: 0x000FB744 File Offset: 0x000FA744
		public override void add_ondatasetchanged(HTMLElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601903F RID: 102463 RVA: 0x000FB7D4 File Offset: 0x000FA7D4
		public override void remove_ondatasetchanged(HTMLElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlelementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019040 RID: 102464 RVA: 0x000FB8C4 File Offset: 0x000FA8C4
		public override void add_onrowenter(HTMLElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019041 RID: 102465 RVA: 0x000FB954 File Offset: 0x000FA954
		public override void remove_onrowenter(HTMLElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlelementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019042 RID: 102466 RVA: 0x000FBA44 File Offset: 0x000FAA44
		public override void add_onrowexit(HTMLElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019043 RID: 102467 RVA: 0x000FBAD4 File Offset: 0x000FAAD4
		public override void remove_onrowexit(HTMLElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlelementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019044 RID: 102468 RVA: 0x000FBBC4 File Offset: 0x000FABC4
		public override void add_onerrorupdate(HTMLElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019045 RID: 102469 RVA: 0x000FBC54 File Offset: 0x000FAC54
		public override void remove_onerrorupdate(HTMLElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlelementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019046 RID: 102470 RVA: 0x000FBD44 File Offset: 0x000FAD44
		public override void add_onafterupdate(HTMLElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019047 RID: 102471 RVA: 0x000FBDD4 File Offset: 0x000FADD4
		public override void remove_onafterupdate(HTMLElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlelementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019048 RID: 102472 RVA: 0x000FBEC4 File Offset: 0x000FAEC4
		public override void add_onbeforeupdate(HTMLElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019049 RID: 102473 RVA: 0x000FBF54 File Offset: 0x000FAF54
		public override void remove_onbeforeupdate(HTMLElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlelementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601904A RID: 102474 RVA: 0x000FC044 File Offset: 0x000FB044
		public override void add_ondragstart(HTMLElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601904B RID: 102475 RVA: 0x000FC0D4 File Offset: 0x000FB0D4
		public override void remove_ondragstart(HTMLElementEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlelementEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601904C RID: 102476 RVA: 0x000FC1C4 File Offset: 0x000FB1C4
		public override void add_onfilterchange(HTMLElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601904D RID: 102477 RVA: 0x000FC254 File Offset: 0x000FB254
		public override void remove_onfilterchange(HTMLElementEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlelementEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601904E RID: 102478 RVA: 0x000FC344 File Offset: 0x000FB344
		public override void add_onselectstart(HTMLElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601904F RID: 102479 RVA: 0x000FC3D4 File Offset: 0x000FB3D4
		public override void remove_onselectstart(HTMLElementEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlelementEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019050 RID: 102480 RVA: 0x000FC4C4 File Offset: 0x000FB4C4
		public override void add_onmouseup(HTMLElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019051 RID: 102481 RVA: 0x000FC554 File Offset: 0x000FB554
		public override void remove_onmouseup(HTMLElementEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlelementEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019052 RID: 102482 RVA: 0x000FC644 File Offset: 0x000FB644
		public override void add_onmousedown(HTMLElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019053 RID: 102483 RVA: 0x000FC6D4 File Offset: 0x000FB6D4
		public override void remove_onmousedown(HTMLElementEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlelementEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019054 RID: 102484 RVA: 0x000FC7C4 File Offset: 0x000FB7C4
		public override void add_onmousemove(HTMLElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019055 RID: 102485 RVA: 0x000FC854 File Offset: 0x000FB854
		public override void remove_onmousemove(HTMLElementEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlelementEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019056 RID: 102486 RVA: 0x000FC944 File Offset: 0x000FB944
		public override void add_onmouseover(HTMLElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019057 RID: 102487 RVA: 0x000FC9D4 File Offset: 0x000FB9D4
		public override void remove_onmouseover(HTMLElementEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlelementEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019058 RID: 102488 RVA: 0x000FCAC4 File Offset: 0x000FBAC4
		public override void add_onmouseout(HTMLElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019059 RID: 102489 RVA: 0x000FCB54 File Offset: 0x000FBB54
		public override void remove_onmouseout(HTMLElementEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlelementEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601905A RID: 102490 RVA: 0x000FCC44 File Offset: 0x000FBC44
		public override void add_onkeyup(HTMLElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601905B RID: 102491 RVA: 0x000FCCD4 File Offset: 0x000FBCD4
		public override void remove_onkeyup(HTMLElementEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlelementEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601905C RID: 102492 RVA: 0x000FCDC4 File Offset: 0x000FBDC4
		public override void add_onkeydown(HTMLElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601905D RID: 102493 RVA: 0x000FCE54 File Offset: 0x000FBE54
		public override void remove_onkeydown(HTMLElementEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlelementEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601905E RID: 102494 RVA: 0x000FCF44 File Offset: 0x000FBF44
		public override void add_onkeypress(HTMLElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x0601905F RID: 102495 RVA: 0x000FCFD4 File Offset: 0x000FBFD4
		public override void remove_onkeypress(HTMLElementEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlelementEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019060 RID: 102496 RVA: 0x000FD0C4 File Offset: 0x000FC0C4
		public override void add_ondblclick(HTMLElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019061 RID: 102497 RVA: 0x000FD154 File Offset: 0x000FC154
		public override void remove_ondblclick(HTMLElementEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlelementEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019062 RID: 102498 RVA: 0x000FD244 File Offset: 0x000FC244
		public override void add_onclick(HTMLElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019063 RID: 102499 RVA: 0x000FD2D4 File Offset: 0x000FC2D4
		public override void remove_onclick(HTMLElementEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onclickDelegate != null && ((htmlelementEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019064 RID: 102500 RVA: 0x000FD3C4 File Offset: 0x000FC3C4
		public override void add_onhelp(HTMLElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = new HTMLElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlelementEvents_SinkHelper, ref num);
				htmlelementEvents_SinkHelper.m_dwCookie = num;
				htmlelementEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlelementEvents_SinkHelper);
			}
		}

		// Token: 0x06019065 RID: 102501 RVA: 0x000FD454 File Offset: 0x000FC454
		public override void remove_onhelp(HTMLElementEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper;
					for (;;)
					{
						htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlelementEvents_SinkHelper.m_onhelpDelegate != null && ((htmlelementEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019066 RID: 102502 RVA: 0x000FD544 File Offset: 0x000FC544
		public HTMLElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06019067 RID: 102503 RVA: 0x000FD56C File Offset: 0x000FC56C
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
								HTMLElementEvents_SinkHelper htmlelementEvents_SinkHelper = (HTMLElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlelementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06019068 RID: 102504 RVA: 0x000FD620 File Offset: 0x000FC620
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000E48 RID: 3656
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000E49 RID: 3657
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000E4A RID: 3658
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
