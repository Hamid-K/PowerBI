using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD5 RID: 3541
	internal sealed class HTMLImgEvents_EventProvider : HTMLImgEvents_Event, IDisposable
	{
		// Token: 0x06017C4A RID: 97354 RVA: 0x000449E0 File Offset: 0x000439E0
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				91, 242, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017C4B RID: 97355 RVA: 0x00044AF4 File Offset: 0x00043AF4
		public override void add_onabort(HTMLImgEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onabortDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C4C RID: 97356 RVA: 0x00044B84 File Offset: 0x00043B84
		public override void remove_onabort(HTMLImgEvents_onabortEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onabortDelegate != null && ((htmlimgEvents_SinkHelper.m_onabortDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C4D RID: 97357 RVA: 0x00044C74 File Offset: 0x00043C74
		public override void add_onerror(HTMLImgEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C4E RID: 97358 RVA: 0x00044D04 File Offset: 0x00043D04
		public override void remove_onerror(HTMLImgEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onerrorDelegate != null && ((htmlimgEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C4F RID: 97359 RVA: 0x00044DF4 File Offset: 0x00043DF4
		public override void add_onload(HTMLImgEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C50 RID: 97360 RVA: 0x00044E84 File Offset: 0x00043E84
		public override void remove_onload(HTMLImgEvents_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onloadDelegate != null && ((htmlimgEvents_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C51 RID: 97361 RVA: 0x00044F74 File Offset: 0x00043F74
		public override void add_onfocusout(HTMLImgEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C52 RID: 97362 RVA: 0x00045004 File Offset: 0x00044004
		public override void remove_onfocusout(HTMLImgEvents_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onfocusoutDelegate != null && ((htmlimgEvents_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C53 RID: 97363 RVA: 0x000450F4 File Offset: 0x000440F4
		public override void add_onfocusin(HTMLImgEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C54 RID: 97364 RVA: 0x00045184 File Offset: 0x00044184
		public override void remove_onfocusin(HTMLImgEvents_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onfocusinDelegate != null && ((htmlimgEvents_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C55 RID: 97365 RVA: 0x00045274 File Offset: 0x00044274
		public override void add_ondeactivate(HTMLImgEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C56 RID: 97366 RVA: 0x00045304 File Offset: 0x00044304
		public override void remove_ondeactivate(HTMLImgEvents_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondeactivateDelegate != null && ((htmlimgEvents_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C57 RID: 97367 RVA: 0x000453F4 File Offset: 0x000443F4
		public override void add_onactivate(HTMLImgEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C58 RID: 97368 RVA: 0x00045484 File Offset: 0x00044484
		public override void remove_onactivate(HTMLImgEvents_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onactivateDelegate != null && ((htmlimgEvents_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C59 RID: 97369 RVA: 0x00045574 File Offset: 0x00044574
		public override void add_onmousewheel(HTMLImgEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C5A RID: 97370 RVA: 0x00045604 File Offset: 0x00044604
		public override void remove_onmousewheel(HTMLImgEvents_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmousewheelDelegate != null && ((htmlimgEvents_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C5B RID: 97371 RVA: 0x000456F4 File Offset: 0x000446F4
		public override void add_onmouseleave(HTMLImgEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C5C RID: 97372 RVA: 0x00045784 File Offset: 0x00044784
		public override void remove_onmouseleave(HTMLImgEvents_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmouseleaveDelegate != null && ((htmlimgEvents_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C5D RID: 97373 RVA: 0x00045874 File Offset: 0x00044874
		public override void add_onmouseenter(HTMLImgEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C5E RID: 97374 RVA: 0x00045904 File Offset: 0x00044904
		public override void remove_onmouseenter(HTMLImgEvents_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmouseenterDelegate != null && ((htmlimgEvents_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C5F RID: 97375 RVA: 0x000459F4 File Offset: 0x000449F4
		public override void add_onresizeend(HTMLImgEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C60 RID: 97376 RVA: 0x00045A84 File Offset: 0x00044A84
		public override void remove_onresizeend(HTMLImgEvents_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onresizeendDelegate != null && ((htmlimgEvents_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C61 RID: 97377 RVA: 0x00045B74 File Offset: 0x00044B74
		public override void add_onresizestart(HTMLImgEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C62 RID: 97378 RVA: 0x00045C04 File Offset: 0x00044C04
		public override void remove_onresizestart(HTMLImgEvents_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onresizestartDelegate != null && ((htmlimgEvents_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C63 RID: 97379 RVA: 0x00045CF4 File Offset: 0x00044CF4
		public override void add_onmoveend(HTMLImgEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C64 RID: 97380 RVA: 0x00045D84 File Offset: 0x00044D84
		public override void remove_onmoveend(HTMLImgEvents_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmoveendDelegate != null && ((htmlimgEvents_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C65 RID: 97381 RVA: 0x00045E74 File Offset: 0x00044E74
		public override void add_onmovestart(HTMLImgEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C66 RID: 97382 RVA: 0x00045F04 File Offset: 0x00044F04
		public override void remove_onmovestart(HTMLImgEvents_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmovestartDelegate != null && ((htmlimgEvents_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C67 RID: 97383 RVA: 0x00045FF4 File Offset: 0x00044FF4
		public override void add_oncontrolselect(HTMLImgEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C68 RID: 97384 RVA: 0x00046084 File Offset: 0x00045084
		public override void remove_oncontrolselect(HTMLImgEvents_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_oncontrolselectDelegate != null && ((htmlimgEvents_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C69 RID: 97385 RVA: 0x00046174 File Offset: 0x00045174
		public override void add_onmove(HTMLImgEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C6A RID: 97386 RVA: 0x00046204 File Offset: 0x00045204
		public override void remove_onmove(HTMLImgEvents_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmoveDelegate != null && ((htmlimgEvents_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C6B RID: 97387 RVA: 0x000462F4 File Offset: 0x000452F4
		public override void add_onbeforeactivate(HTMLImgEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C6C RID: 97388 RVA: 0x00046384 File Offset: 0x00045384
		public override void remove_onbeforeactivate(HTMLImgEvents_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C6D RID: 97389 RVA: 0x00046474 File Offset: 0x00045474
		public override void add_onbeforedeactivate(HTMLImgEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C6E RID: 97390 RVA: 0x00046504 File Offset: 0x00045504
		public override void remove_onbeforedeactivate(HTMLImgEvents_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C6F RID: 97391 RVA: 0x000465F4 File Offset: 0x000455F4
		public override void add_onpage(HTMLImgEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C70 RID: 97392 RVA: 0x00046684 File Offset: 0x00045684
		public override void remove_onpage(HTMLImgEvents_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onpageDelegate != null && ((htmlimgEvents_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C71 RID: 97393 RVA: 0x00046774 File Offset: 0x00045774
		public override void add_onlayoutcomplete(HTMLImgEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C72 RID: 97394 RVA: 0x00046804 File Offset: 0x00045804
		public override void remove_onlayoutcomplete(HTMLImgEvents_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlimgEvents_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C73 RID: 97395 RVA: 0x000468F4 File Offset: 0x000458F4
		public override void add_onbeforeeditfocus(HTMLImgEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforeeditfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C74 RID: 97396 RVA: 0x00046984 File Offset: 0x00045984
		public override void remove_onbeforeeditfocus(HTMLImgEvents_onbeforeeditfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforeeditfocusDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforeeditfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C75 RID: 97397 RVA: 0x00046A74 File Offset: 0x00045A74
		public override void add_onreadystatechange(HTMLImgEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C76 RID: 97398 RVA: 0x00046B04 File Offset: 0x00045B04
		public override void remove_onreadystatechange(HTMLImgEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlimgEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C77 RID: 97399 RVA: 0x00046BF4 File Offset: 0x00045BF4
		public override void add_oncellchange(HTMLImgEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C78 RID: 97400 RVA: 0x00046C84 File Offset: 0x00045C84
		public override void remove_oncellchange(HTMLImgEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlimgEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C79 RID: 97401 RVA: 0x00046D74 File Offset: 0x00045D74
		public override void add_onrowsinserted(HTMLImgEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C7A RID: 97402 RVA: 0x00046E04 File Offset: 0x00045E04
		public override void remove_onrowsinserted(HTMLImgEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlimgEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C7B RID: 97403 RVA: 0x00046EF4 File Offset: 0x00045EF4
		public override void add_onrowsdelete(HTMLImgEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C7C RID: 97404 RVA: 0x00046F84 File Offset: 0x00045F84
		public override void remove_onrowsdelete(HTMLImgEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlimgEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C7D RID: 97405 RVA: 0x00047074 File Offset: 0x00046074
		public override void add_oncontextmenu(HTMLImgEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C7E RID: 97406 RVA: 0x00047104 File Offset: 0x00046104
		public override void remove_oncontextmenu(HTMLImgEvents_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_oncontextmenuDelegate != null && ((htmlimgEvents_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C7F RID: 97407 RVA: 0x000471F4 File Offset: 0x000461F4
		public override void add_onpaste(HTMLImgEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C80 RID: 97408 RVA: 0x00047284 File Offset: 0x00046284
		public override void remove_onpaste(HTMLImgEvents_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onpasteDelegate != null && ((htmlimgEvents_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C81 RID: 97409 RVA: 0x00047374 File Offset: 0x00046374
		public override void add_onbeforepaste(HTMLImgEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C82 RID: 97410 RVA: 0x00047404 File Offset: 0x00046404
		public override void remove_onbeforepaste(HTMLImgEvents_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforepasteDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C83 RID: 97411 RVA: 0x000474F4 File Offset: 0x000464F4
		public override void add_oncopy(HTMLImgEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C84 RID: 97412 RVA: 0x00047584 File Offset: 0x00046584
		public override void remove_oncopy(HTMLImgEvents_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_oncopyDelegate != null && ((htmlimgEvents_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C85 RID: 97413 RVA: 0x00047674 File Offset: 0x00046674
		public override void add_onbeforecopy(HTMLImgEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C86 RID: 97414 RVA: 0x00047704 File Offset: 0x00046704
		public override void remove_onbeforecopy(HTMLImgEvents_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforecopyDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C87 RID: 97415 RVA: 0x000477F4 File Offset: 0x000467F4
		public override void add_oncut(HTMLImgEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C88 RID: 97416 RVA: 0x00047884 File Offset: 0x00046884
		public override void remove_oncut(HTMLImgEvents_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_oncutDelegate != null && ((htmlimgEvents_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C89 RID: 97417 RVA: 0x00047974 File Offset: 0x00046974
		public override void add_onbeforecut(HTMLImgEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C8A RID: 97418 RVA: 0x00047A04 File Offset: 0x00046A04
		public override void remove_onbeforecut(HTMLImgEvents_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforecutDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C8B RID: 97419 RVA: 0x00047AF4 File Offset: 0x00046AF4
		public override void add_ondrop(HTMLImgEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C8C RID: 97420 RVA: 0x00047B84 File Offset: 0x00046B84
		public override void remove_ondrop(HTMLImgEvents_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondropDelegate != null && ((htmlimgEvents_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C8D RID: 97421 RVA: 0x00047C74 File Offset: 0x00046C74
		public override void add_ondragleave(HTMLImgEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C8E RID: 97422 RVA: 0x00047D04 File Offset: 0x00046D04
		public override void remove_ondragleave(HTMLImgEvents_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragleaveDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C8F RID: 97423 RVA: 0x00047DF4 File Offset: 0x00046DF4
		public override void add_ondragover(HTMLImgEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C90 RID: 97424 RVA: 0x00047E84 File Offset: 0x00046E84
		public override void remove_ondragover(HTMLImgEvents_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragoverDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C91 RID: 97425 RVA: 0x00047F74 File Offset: 0x00046F74
		public override void add_ondragenter(HTMLImgEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C92 RID: 97426 RVA: 0x00048004 File Offset: 0x00047004
		public override void remove_ondragenter(HTMLImgEvents_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragenterDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C93 RID: 97427 RVA: 0x000480F4 File Offset: 0x000470F4
		public override void add_ondragend(HTMLImgEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C94 RID: 97428 RVA: 0x00048184 File Offset: 0x00047184
		public override void remove_ondragend(HTMLImgEvents_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragendDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C95 RID: 97429 RVA: 0x00048274 File Offset: 0x00047274
		public override void add_ondrag(HTMLImgEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C96 RID: 97430 RVA: 0x00048304 File Offset: 0x00047304
		public override void remove_ondrag(HTMLImgEvents_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C97 RID: 97431 RVA: 0x000483F4 File Offset: 0x000473F4
		public override void add_onresize(HTMLImgEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C98 RID: 97432 RVA: 0x00048484 File Offset: 0x00047484
		public override void remove_onresize(HTMLImgEvents_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onresizeDelegate != null && ((htmlimgEvents_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C99 RID: 97433 RVA: 0x00048574 File Offset: 0x00047574
		public override void add_onblur(HTMLImgEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C9A RID: 97434 RVA: 0x00048604 File Offset: 0x00047604
		public override void remove_onblur(HTMLImgEvents_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onblurDelegate != null && ((htmlimgEvents_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C9B RID: 97435 RVA: 0x000486F4 File Offset: 0x000476F4
		public override void add_onfocus(HTMLImgEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C9C RID: 97436 RVA: 0x00048784 File Offset: 0x00047784
		public override void remove_onfocus(HTMLImgEvents_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onfocusDelegate != null && ((htmlimgEvents_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C9D RID: 97437 RVA: 0x00048874 File Offset: 0x00047874
		public override void add_onscroll(HTMLImgEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017C9E RID: 97438 RVA: 0x00048904 File Offset: 0x00047904
		public override void remove_onscroll(HTMLImgEvents_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onscrollDelegate != null && ((htmlimgEvents_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017C9F RID: 97439 RVA: 0x000489F4 File Offset: 0x000479F4
		public override void add_onpropertychange(HTMLImgEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CA0 RID: 97440 RVA: 0x00048A84 File Offset: 0x00047A84
		public override void remove_onpropertychange(HTMLImgEvents_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onpropertychangeDelegate != null && ((htmlimgEvents_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CA1 RID: 97441 RVA: 0x00048B74 File Offset: 0x00047B74
		public override void add_onlosecapture(HTMLImgEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CA2 RID: 97442 RVA: 0x00048C04 File Offset: 0x00047C04
		public override void remove_onlosecapture(HTMLImgEvents_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onlosecaptureDelegate != null && ((htmlimgEvents_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CA3 RID: 97443 RVA: 0x00048CF4 File Offset: 0x00047CF4
		public override void add_ondatasetcomplete(HTMLImgEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CA4 RID: 97444 RVA: 0x00048D84 File Offset: 0x00047D84
		public override void remove_ondatasetcomplete(HTMLImgEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlimgEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CA5 RID: 97445 RVA: 0x00048E74 File Offset: 0x00047E74
		public override void add_ondataavailable(HTMLImgEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CA6 RID: 97446 RVA: 0x00048F04 File Offset: 0x00047F04
		public override void remove_ondataavailable(HTMLImgEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlimgEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CA7 RID: 97447 RVA: 0x00048FF4 File Offset: 0x00047FF4
		public override void add_ondatasetchanged(HTMLImgEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CA8 RID: 97448 RVA: 0x00049084 File Offset: 0x00048084
		public override void remove_ondatasetchanged(HTMLImgEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlimgEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CA9 RID: 97449 RVA: 0x00049174 File Offset: 0x00048174
		public override void add_onrowenter(HTMLImgEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CAA RID: 97450 RVA: 0x00049204 File Offset: 0x00048204
		public override void remove_onrowenter(HTMLImgEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlimgEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CAB RID: 97451 RVA: 0x000492F4 File Offset: 0x000482F4
		public override void add_onrowexit(HTMLImgEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CAC RID: 97452 RVA: 0x00049384 File Offset: 0x00048384
		public override void remove_onrowexit(HTMLImgEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlimgEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CAD RID: 97453 RVA: 0x00049474 File Offset: 0x00048474
		public override void add_onerrorupdate(HTMLImgEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CAE RID: 97454 RVA: 0x00049504 File Offset: 0x00048504
		public override void remove_onerrorupdate(HTMLImgEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlimgEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CAF RID: 97455 RVA: 0x000495F4 File Offset: 0x000485F4
		public override void add_onafterupdate(HTMLImgEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CB0 RID: 97456 RVA: 0x00049684 File Offset: 0x00048684
		public override void remove_onafterupdate(HTMLImgEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlimgEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CB1 RID: 97457 RVA: 0x00049774 File Offset: 0x00048774
		public override void add_onbeforeupdate(HTMLImgEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CB2 RID: 97458 RVA: 0x00049804 File Offset: 0x00048804
		public override void remove_onbeforeupdate(HTMLImgEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlimgEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CB3 RID: 97459 RVA: 0x000498F4 File Offset: 0x000488F4
		public override void add_ondragstart(HTMLImgEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CB4 RID: 97460 RVA: 0x00049984 File Offset: 0x00048984
		public override void remove_ondragstart(HTMLImgEvents_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondragstartDelegate != null && ((htmlimgEvents_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CB5 RID: 97461 RVA: 0x00049A74 File Offset: 0x00048A74
		public override void add_onfilterchange(HTMLImgEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CB6 RID: 97462 RVA: 0x00049B04 File Offset: 0x00048B04
		public override void remove_onfilterchange(HTMLImgEvents_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onfilterchangeDelegate != null && ((htmlimgEvents_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CB7 RID: 97463 RVA: 0x00049BF4 File Offset: 0x00048BF4
		public override void add_onselectstart(HTMLImgEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CB8 RID: 97464 RVA: 0x00049C84 File Offset: 0x00048C84
		public override void remove_onselectstart(HTMLImgEvents_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onselectstartDelegate != null && ((htmlimgEvents_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CB9 RID: 97465 RVA: 0x00049D74 File Offset: 0x00048D74
		public override void add_onmouseup(HTMLImgEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CBA RID: 97466 RVA: 0x00049E04 File Offset: 0x00048E04
		public override void remove_onmouseup(HTMLImgEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmouseupDelegate != null && ((htmlimgEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CBB RID: 97467 RVA: 0x00049EF4 File Offset: 0x00048EF4
		public override void add_onmousedown(HTMLImgEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CBC RID: 97468 RVA: 0x00049F84 File Offset: 0x00048F84
		public override void remove_onmousedown(HTMLImgEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmousedownDelegate != null && ((htmlimgEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CBD RID: 97469 RVA: 0x0004A074 File Offset: 0x00049074
		public override void add_onmousemove(HTMLImgEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CBE RID: 97470 RVA: 0x0004A104 File Offset: 0x00049104
		public override void remove_onmousemove(HTMLImgEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmousemoveDelegate != null && ((htmlimgEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CBF RID: 97471 RVA: 0x0004A1F4 File Offset: 0x000491F4
		public override void add_onmouseover(HTMLImgEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CC0 RID: 97472 RVA: 0x0004A284 File Offset: 0x00049284
		public override void remove_onmouseover(HTMLImgEvents_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmouseoverDelegate != null && ((htmlimgEvents_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CC1 RID: 97473 RVA: 0x0004A374 File Offset: 0x00049374
		public override void add_onmouseout(HTMLImgEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CC2 RID: 97474 RVA: 0x0004A404 File Offset: 0x00049404
		public override void remove_onmouseout(HTMLImgEvents_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onmouseoutDelegate != null && ((htmlimgEvents_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CC3 RID: 97475 RVA: 0x0004A4F4 File Offset: 0x000494F4
		public override void add_onkeyup(HTMLImgEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CC4 RID: 97476 RVA: 0x0004A584 File Offset: 0x00049584
		public override void remove_onkeyup(HTMLImgEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onkeyupDelegate != null && ((htmlimgEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CC5 RID: 97477 RVA: 0x0004A674 File Offset: 0x00049674
		public override void add_onkeydown(HTMLImgEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CC6 RID: 97478 RVA: 0x0004A704 File Offset: 0x00049704
		public override void remove_onkeydown(HTMLImgEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onkeydownDelegate != null && ((htmlimgEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CC7 RID: 97479 RVA: 0x0004A7F4 File Offset: 0x000497F4
		public override void add_onkeypress(HTMLImgEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CC8 RID: 97480 RVA: 0x0004A884 File Offset: 0x00049884
		public override void remove_onkeypress(HTMLImgEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onkeypressDelegate != null && ((htmlimgEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CC9 RID: 97481 RVA: 0x0004A974 File Offset: 0x00049974
		public override void add_ondblclick(HTMLImgEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CCA RID: 97482 RVA: 0x0004AA04 File Offset: 0x00049A04
		public override void remove_ondblclick(HTMLImgEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_ondblclickDelegate != null && ((htmlimgEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CCB RID: 97483 RVA: 0x0004AAF4 File Offset: 0x00049AF4
		public override void add_onclick(HTMLImgEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CCC RID: 97484 RVA: 0x0004AB84 File Offset: 0x00049B84
		public override void remove_onclick(HTMLImgEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onclickDelegate != null && ((htmlimgEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CCD RID: 97485 RVA: 0x0004AC74 File Offset: 0x00049C74
		public override void add_onhelp(HTMLImgEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = new HTMLImgEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlimgEvents_SinkHelper, ref num);
				htmlimgEvents_SinkHelper.m_dwCookie = num;
				htmlimgEvents_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlimgEvents_SinkHelper);
			}
		}

		// Token: 0x06017CCE RID: 97486 RVA: 0x0004AD04 File Offset: 0x00049D04
		public override void remove_onhelp(HTMLImgEvents_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper;
					for (;;)
					{
						htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlimgEvents_SinkHelper.m_onhelpDelegate != null && ((htmlimgEvents_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017CCF RID: 97487 RVA: 0x0004ADF4 File Offset: 0x00049DF4
		public HTMLImgEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017CD0 RID: 97488 RVA: 0x0004AE1C File Offset: 0x00049E1C
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
								HTMLImgEvents_SinkHelper htmlimgEvents_SinkHelper = (HTMLImgEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlimgEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017CD1 RID: 97489 RVA: 0x0004AED0 File Offset: 0x00049ED0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400077F RID: 1919
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000780 RID: 1920
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000781 RID: 1921
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
