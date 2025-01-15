using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD1 RID: 3537
	internal sealed class HTMLMarqueeElementEvents2_EventProvider : HTMLMarqueeElementEvents2_Event, IDisposable
	{
		// Token: 0x06017ABB RID: 96955 RVA: 0x00036740 File Offset: 0x00035740
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				31, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017ABC RID: 96956 RVA: 0x00036854 File Offset: 0x00035854
		public override void add_onstart(HTMLMarqueeElementEvents2_onstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ABD RID: 96957 RVA: 0x000368E4 File Offset: 0x000358E4
		public override void remove_onstart(HTMLMarqueeElementEvents2_onstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onstartDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ABE RID: 96958 RVA: 0x000369D4 File Offset: 0x000359D4
		public override void add_onfinish(HTMLMarqueeElementEvents2_onfinishEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onfinishDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ABF RID: 96959 RVA: 0x00036A64 File Offset: 0x00035A64
		public override void remove_onfinish(HTMLMarqueeElementEvents2_onfinishEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onfinishDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onfinishDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AC0 RID: 96960 RVA: 0x00036B54 File Offset: 0x00035B54
		public override void add_onbounce(HTMLMarqueeElementEvents2_onbounceEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbounceDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AC1 RID: 96961 RVA: 0x00036BE4 File Offset: 0x00035BE4
		public override void remove_onbounce(HTMLMarqueeElementEvents2_onbounceEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbounceDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbounceDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AC2 RID: 96962 RVA: 0x00036CD4 File Offset: 0x00035CD4
		public override void add_onselect(HTMLMarqueeElementEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AC3 RID: 96963 RVA: 0x00036D64 File Offset: 0x00035D64
		public override void remove_onselect(HTMLMarqueeElementEvents2_onselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onselectDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AC4 RID: 96964 RVA: 0x00036E54 File Offset: 0x00035E54
		public override void add_onchange(HTMLMarqueeElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AC5 RID: 96965 RVA: 0x00036EE4 File Offset: 0x00035EE4
		public override void remove_onchange(HTMLMarqueeElementEvents2_onchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onchangeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AC6 RID: 96966 RVA: 0x00036FD4 File Offset: 0x00035FD4
		public override void add_onmousewheel(HTMLMarqueeElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AC7 RID: 96967 RVA: 0x00037064 File Offset: 0x00036064
		public override void remove_onmousewheel(HTMLMarqueeElementEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AC8 RID: 96968 RVA: 0x00037154 File Offset: 0x00036154
		public override void add_onresizeend(HTMLMarqueeElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AC9 RID: 96969 RVA: 0x000371E4 File Offset: 0x000361E4
		public override void remove_onresizeend(HTMLMarqueeElementEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ACA RID: 96970 RVA: 0x000372D4 File Offset: 0x000362D4
		public override void add_onresizestart(HTMLMarqueeElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ACB RID: 96971 RVA: 0x00037364 File Offset: 0x00036364
		public override void remove_onresizestart(HTMLMarqueeElementEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ACC RID: 96972 RVA: 0x00037454 File Offset: 0x00036454
		public override void add_onmoveend(HTMLMarqueeElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ACD RID: 96973 RVA: 0x000374E4 File Offset: 0x000364E4
		public override void remove_onmoveend(HTMLMarqueeElementEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ACE RID: 96974 RVA: 0x000375D4 File Offset: 0x000365D4
		public override void add_onmovestart(HTMLMarqueeElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ACF RID: 96975 RVA: 0x00037664 File Offset: 0x00036664
		public override void remove_onmovestart(HTMLMarqueeElementEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AD0 RID: 96976 RVA: 0x00037754 File Offset: 0x00036754
		public override void add_oncontrolselect(HTMLMarqueeElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AD1 RID: 96977 RVA: 0x000377E4 File Offset: 0x000367E4
		public override void remove_oncontrolselect(HTMLMarqueeElementEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AD2 RID: 96978 RVA: 0x000378D4 File Offset: 0x000368D4
		public override void add_onmove(HTMLMarqueeElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AD3 RID: 96979 RVA: 0x00037964 File Offset: 0x00036964
		public override void remove_onmove(HTMLMarqueeElementEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmoveDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AD4 RID: 96980 RVA: 0x00037A54 File Offset: 0x00036A54
		public override void add_onfocusout(HTMLMarqueeElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AD5 RID: 96981 RVA: 0x00037AE4 File Offset: 0x00036AE4
		public override void remove_onfocusout(HTMLMarqueeElementEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AD6 RID: 96982 RVA: 0x00037BD4 File Offset: 0x00036BD4
		public override void add_onfocusin(HTMLMarqueeElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AD7 RID: 96983 RVA: 0x00037C64 File Offset: 0x00036C64
		public override void remove_onfocusin(HTMLMarqueeElementEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AD8 RID: 96984 RVA: 0x00037D54 File Offset: 0x00036D54
		public override void add_onbeforeactivate(HTMLMarqueeElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AD9 RID: 96985 RVA: 0x00037DE4 File Offset: 0x00036DE4
		public override void remove_onbeforeactivate(HTMLMarqueeElementEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ADA RID: 96986 RVA: 0x00037ED4 File Offset: 0x00036ED4
		public override void add_onbeforedeactivate(HTMLMarqueeElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ADB RID: 96987 RVA: 0x00037F64 File Offset: 0x00036F64
		public override void remove_onbeforedeactivate(HTMLMarqueeElementEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ADC RID: 96988 RVA: 0x00038054 File Offset: 0x00037054
		public override void add_ondeactivate(HTMLMarqueeElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ADD RID: 96989 RVA: 0x000380E4 File Offset: 0x000370E4
		public override void remove_ondeactivate(HTMLMarqueeElementEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017ADE RID: 96990 RVA: 0x000381D4 File Offset: 0x000371D4
		public override void add_onactivate(HTMLMarqueeElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017ADF RID: 96991 RVA: 0x00038264 File Offset: 0x00037264
		public override void remove_onactivate(HTMLMarqueeElementEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onactivateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AE0 RID: 96992 RVA: 0x00038354 File Offset: 0x00037354
		public override void add_onmouseleave(HTMLMarqueeElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AE1 RID: 96993 RVA: 0x000383E4 File Offset: 0x000373E4
		public override void remove_onmouseleave(HTMLMarqueeElementEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AE2 RID: 96994 RVA: 0x000384D4 File Offset: 0x000374D4
		public override void add_onmouseenter(HTMLMarqueeElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AE3 RID: 96995 RVA: 0x00038564 File Offset: 0x00037564
		public override void remove_onmouseenter(HTMLMarqueeElementEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AE4 RID: 96996 RVA: 0x00038654 File Offset: 0x00037654
		public override void add_onpage(HTMLMarqueeElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AE5 RID: 96997 RVA: 0x000386E4 File Offset: 0x000376E4
		public override void remove_onpage(HTMLMarqueeElementEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onpageDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AE6 RID: 96998 RVA: 0x000387D4 File Offset: 0x000377D4
		public override void add_onlayoutcomplete(HTMLMarqueeElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AE7 RID: 96999 RVA: 0x00038864 File Offset: 0x00037864
		public override void remove_onlayoutcomplete(HTMLMarqueeElementEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AE8 RID: 97000 RVA: 0x00038954 File Offset: 0x00037954
		public override void add_onreadystatechange(HTMLMarqueeElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AE9 RID: 97001 RVA: 0x000389E4 File Offset: 0x000379E4
		public override void remove_onreadystatechange(HTMLMarqueeElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AEA RID: 97002 RVA: 0x00038AD4 File Offset: 0x00037AD4
		public override void add_oncellchange(HTMLMarqueeElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AEB RID: 97003 RVA: 0x00038B64 File Offset: 0x00037B64
		public override void remove_oncellchange(HTMLMarqueeElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AEC RID: 97004 RVA: 0x00038C54 File Offset: 0x00037C54
		public override void add_onrowsinserted(HTMLMarqueeElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AED RID: 97005 RVA: 0x00038CE4 File Offset: 0x00037CE4
		public override void remove_onrowsinserted(HTMLMarqueeElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AEE RID: 97006 RVA: 0x00038DD4 File Offset: 0x00037DD4
		public override void add_onrowsdelete(HTMLMarqueeElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AEF RID: 97007 RVA: 0x00038E64 File Offset: 0x00037E64
		public override void remove_onrowsdelete(HTMLMarqueeElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AF0 RID: 97008 RVA: 0x00038F54 File Offset: 0x00037F54
		public override void add_oncontextmenu(HTMLMarqueeElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AF1 RID: 97009 RVA: 0x00038FE4 File Offset: 0x00037FE4
		public override void remove_oncontextmenu(HTMLMarqueeElementEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AF2 RID: 97010 RVA: 0x000390D4 File Offset: 0x000380D4
		public override void add_onpaste(HTMLMarqueeElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AF3 RID: 97011 RVA: 0x00039164 File Offset: 0x00038164
		public override void remove_onpaste(HTMLMarqueeElementEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onpasteDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AF4 RID: 97012 RVA: 0x00039254 File Offset: 0x00038254
		public override void add_onbeforepaste(HTMLMarqueeElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AF5 RID: 97013 RVA: 0x000392E4 File Offset: 0x000382E4
		public override void remove_onbeforepaste(HTMLMarqueeElementEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AF6 RID: 97014 RVA: 0x000393D4 File Offset: 0x000383D4
		public override void add_oncopy(HTMLMarqueeElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AF7 RID: 97015 RVA: 0x00039464 File Offset: 0x00038464
		public override void remove_oncopy(HTMLMarqueeElementEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_oncopyDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AF8 RID: 97016 RVA: 0x00039554 File Offset: 0x00038554
		public override void add_onbeforecopy(HTMLMarqueeElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AF9 RID: 97017 RVA: 0x000395E4 File Offset: 0x000385E4
		public override void remove_onbeforecopy(HTMLMarqueeElementEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AFA RID: 97018 RVA: 0x000396D4 File Offset: 0x000386D4
		public override void add_oncut(HTMLMarqueeElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AFB RID: 97019 RVA: 0x00039764 File Offset: 0x00038764
		public override void remove_oncut(HTMLMarqueeElementEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_oncutDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AFC RID: 97020 RVA: 0x00039854 File Offset: 0x00038854
		public override void add_onbeforecut(HTMLMarqueeElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AFD RID: 97021 RVA: 0x000398E4 File Offset: 0x000388E4
		public override void remove_onbeforecut(HTMLMarqueeElementEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017AFE RID: 97022 RVA: 0x000399D4 File Offset: 0x000389D4
		public override void add_ondrop(HTMLMarqueeElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017AFF RID: 97023 RVA: 0x00039A64 File Offset: 0x00038A64
		public override void remove_ondrop(HTMLMarqueeElementEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondropDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B00 RID: 97024 RVA: 0x00039B54 File Offset: 0x00038B54
		public override void add_ondragleave(HTMLMarqueeElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B01 RID: 97025 RVA: 0x00039BE4 File Offset: 0x00038BE4
		public override void remove_ondragleave(HTMLMarqueeElementEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B02 RID: 97026 RVA: 0x00039CD4 File Offset: 0x00038CD4
		public override void add_ondragover(HTMLMarqueeElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B03 RID: 97027 RVA: 0x00039D64 File Offset: 0x00038D64
		public override void remove_ondragover(HTMLMarqueeElementEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B04 RID: 97028 RVA: 0x00039E54 File Offset: 0x00038E54
		public override void add_ondragenter(HTMLMarqueeElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B05 RID: 97029 RVA: 0x00039EE4 File Offset: 0x00038EE4
		public override void remove_ondragenter(HTMLMarqueeElementEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B06 RID: 97030 RVA: 0x00039FD4 File Offset: 0x00038FD4
		public override void add_ondragend(HTMLMarqueeElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B07 RID: 97031 RVA: 0x0003A064 File Offset: 0x00039064
		public override void remove_ondragend(HTMLMarqueeElementEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragendDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B08 RID: 97032 RVA: 0x0003A154 File Offset: 0x00039154
		public override void add_ondrag(HTMLMarqueeElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B09 RID: 97033 RVA: 0x0003A1E4 File Offset: 0x000391E4
		public override void remove_ondrag(HTMLMarqueeElementEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B0A RID: 97034 RVA: 0x0003A2D4 File Offset: 0x000392D4
		public override void add_onresize(HTMLMarqueeElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B0B RID: 97035 RVA: 0x0003A364 File Offset: 0x00039364
		public override void remove_onresize(HTMLMarqueeElementEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B0C RID: 97036 RVA: 0x0003A454 File Offset: 0x00039454
		public override void add_onblur(HTMLMarqueeElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B0D RID: 97037 RVA: 0x0003A4E4 File Offset: 0x000394E4
		public override void remove_onblur(HTMLMarqueeElementEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onblurDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B0E RID: 97038 RVA: 0x0003A5D4 File Offset: 0x000395D4
		public override void add_onfocus(HTMLMarqueeElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B0F RID: 97039 RVA: 0x0003A664 File Offset: 0x00039664
		public override void remove_onfocus(HTMLMarqueeElementEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B10 RID: 97040 RVA: 0x0003A754 File Offset: 0x00039754
		public override void add_onscroll(HTMLMarqueeElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B11 RID: 97041 RVA: 0x0003A7E4 File Offset: 0x000397E4
		public override void remove_onscroll(HTMLMarqueeElementEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B12 RID: 97042 RVA: 0x0003A8D4 File Offset: 0x000398D4
		public override void add_onpropertychange(HTMLMarqueeElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B13 RID: 97043 RVA: 0x0003A964 File Offset: 0x00039964
		public override void remove_onpropertychange(HTMLMarqueeElementEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B14 RID: 97044 RVA: 0x0003AA54 File Offset: 0x00039A54
		public override void add_onlosecapture(HTMLMarqueeElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B15 RID: 97045 RVA: 0x0003AAE4 File Offset: 0x00039AE4
		public override void remove_onlosecapture(HTMLMarqueeElementEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B16 RID: 97046 RVA: 0x0003ABD4 File Offset: 0x00039BD4
		public override void add_ondatasetcomplete(HTMLMarqueeElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B17 RID: 97047 RVA: 0x0003AC64 File Offset: 0x00039C64
		public override void remove_ondatasetcomplete(HTMLMarqueeElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B18 RID: 97048 RVA: 0x0003AD54 File Offset: 0x00039D54
		public override void add_ondataavailable(HTMLMarqueeElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B19 RID: 97049 RVA: 0x0003ADE4 File Offset: 0x00039DE4
		public override void remove_ondataavailable(HTMLMarqueeElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B1A RID: 97050 RVA: 0x0003AED4 File Offset: 0x00039ED4
		public override void add_ondatasetchanged(HTMLMarqueeElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B1B RID: 97051 RVA: 0x0003AF64 File Offset: 0x00039F64
		public override void remove_ondatasetchanged(HTMLMarqueeElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B1C RID: 97052 RVA: 0x0003B054 File Offset: 0x0003A054
		public override void add_onrowenter(HTMLMarqueeElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B1D RID: 97053 RVA: 0x0003B0E4 File Offset: 0x0003A0E4
		public override void remove_onrowenter(HTMLMarqueeElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B1E RID: 97054 RVA: 0x0003B1D4 File Offset: 0x0003A1D4
		public override void add_onrowexit(HTMLMarqueeElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B1F RID: 97055 RVA: 0x0003B264 File Offset: 0x0003A264
		public override void remove_onrowexit(HTMLMarqueeElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B20 RID: 97056 RVA: 0x0003B354 File Offset: 0x0003A354
		public override void add_onerrorupdate(HTMLMarqueeElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B21 RID: 97057 RVA: 0x0003B3E4 File Offset: 0x0003A3E4
		public override void remove_onerrorupdate(HTMLMarqueeElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B22 RID: 97058 RVA: 0x0003B4D4 File Offset: 0x0003A4D4
		public override void add_onafterupdate(HTMLMarqueeElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B23 RID: 97059 RVA: 0x0003B564 File Offset: 0x0003A564
		public override void remove_onafterupdate(HTMLMarqueeElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B24 RID: 97060 RVA: 0x0003B654 File Offset: 0x0003A654
		public override void add_onbeforeupdate(HTMLMarqueeElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B25 RID: 97061 RVA: 0x0003B6E4 File Offset: 0x0003A6E4
		public override void remove_onbeforeupdate(HTMLMarqueeElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B26 RID: 97062 RVA: 0x0003B7D4 File Offset: 0x0003A7D4
		public override void add_ondragstart(HTMLMarqueeElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B27 RID: 97063 RVA: 0x0003B864 File Offset: 0x0003A864
		public override void remove_ondragstart(HTMLMarqueeElementEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B28 RID: 97064 RVA: 0x0003B954 File Offset: 0x0003A954
		public override void add_onfilterchange(HTMLMarqueeElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B29 RID: 97065 RVA: 0x0003B9E4 File Offset: 0x0003A9E4
		public override void remove_onfilterchange(HTMLMarqueeElementEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B2A RID: 97066 RVA: 0x0003BAD4 File Offset: 0x0003AAD4
		public override void add_onselectstart(HTMLMarqueeElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B2B RID: 97067 RVA: 0x0003BB64 File Offset: 0x0003AB64
		public override void remove_onselectstart(HTMLMarqueeElementEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B2C RID: 97068 RVA: 0x0003BC54 File Offset: 0x0003AC54
		public override void add_onmouseup(HTMLMarqueeElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B2D RID: 97069 RVA: 0x0003BCE4 File Offset: 0x0003ACE4
		public override void remove_onmouseup(HTMLMarqueeElementEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B2E RID: 97070 RVA: 0x0003BDD4 File Offset: 0x0003ADD4
		public override void add_onmousedown(HTMLMarqueeElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B2F RID: 97071 RVA: 0x0003BE64 File Offset: 0x0003AE64
		public override void remove_onmousedown(HTMLMarqueeElementEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B30 RID: 97072 RVA: 0x0003BF54 File Offset: 0x0003AF54
		public override void add_onmousemove(HTMLMarqueeElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B31 RID: 97073 RVA: 0x0003BFE4 File Offset: 0x0003AFE4
		public override void remove_onmousemove(HTMLMarqueeElementEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B32 RID: 97074 RVA: 0x0003C0D4 File Offset: 0x0003B0D4
		public override void add_onmouseover(HTMLMarqueeElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B33 RID: 97075 RVA: 0x0003C164 File Offset: 0x0003B164
		public override void remove_onmouseover(HTMLMarqueeElementEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B34 RID: 97076 RVA: 0x0003C254 File Offset: 0x0003B254
		public override void add_onmouseout(HTMLMarqueeElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B35 RID: 97077 RVA: 0x0003C2E4 File Offset: 0x0003B2E4
		public override void remove_onmouseout(HTMLMarqueeElementEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B36 RID: 97078 RVA: 0x0003C3D4 File Offset: 0x0003B3D4
		public override void add_onkeyup(HTMLMarqueeElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B37 RID: 97079 RVA: 0x0003C464 File Offset: 0x0003B464
		public override void remove_onkeyup(HTMLMarqueeElementEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B38 RID: 97080 RVA: 0x0003C554 File Offset: 0x0003B554
		public override void add_onkeydown(HTMLMarqueeElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B39 RID: 97081 RVA: 0x0003C5E4 File Offset: 0x0003B5E4
		public override void remove_onkeydown(HTMLMarqueeElementEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B3A RID: 97082 RVA: 0x0003C6D4 File Offset: 0x0003B6D4
		public override void add_onkeypress(HTMLMarqueeElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B3B RID: 97083 RVA: 0x0003C764 File Offset: 0x0003B764
		public override void remove_onkeypress(HTMLMarqueeElementEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B3C RID: 97084 RVA: 0x0003C854 File Offset: 0x0003B854
		public override void add_ondblclick(HTMLMarqueeElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B3D RID: 97085 RVA: 0x0003C8E4 File Offset: 0x0003B8E4
		public override void remove_ondblclick(HTMLMarqueeElementEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B3E RID: 97086 RVA: 0x0003C9D4 File Offset: 0x0003B9D4
		public override void add_onclick(HTMLMarqueeElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B3F RID: 97087 RVA: 0x0003CA64 File Offset: 0x0003BA64
		public override void remove_onclick(HTMLMarqueeElementEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onclickDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B40 RID: 97088 RVA: 0x0003CB54 File Offset: 0x0003BB54
		public override void add_onhelp(HTMLMarqueeElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = new HTMLMarqueeElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlmarqueeElementEvents2_SinkHelper, ref num);
				htmlmarqueeElementEvents2_SinkHelper.m_dwCookie = num;
				htmlmarqueeElementEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlmarqueeElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017B41 RID: 97089 RVA: 0x0003CBE4 File Offset: 0x0003BBE4
		public override void remove_onhelp(HTMLMarqueeElementEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper;
					for (;;)
					{
						htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlmarqueeElementEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlmarqueeElementEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017B42 RID: 97090 RVA: 0x0003CCD4 File Offset: 0x0003BCD4
		public HTMLMarqueeElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017B43 RID: 97091 RVA: 0x0003CCFC File Offset: 0x0003BCFC
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
								HTMLMarqueeElementEvents2_SinkHelper htmlmarqueeElementEvents2_SinkHelper = (HTMLMarqueeElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlmarqueeElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06017B44 RID: 97092 RVA: 0x0003CDB0 File Offset: 0x0003BDB0
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040006F6 RID: 1782
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040006F7 RID: 1783
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040006F8 RID: 1784
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
