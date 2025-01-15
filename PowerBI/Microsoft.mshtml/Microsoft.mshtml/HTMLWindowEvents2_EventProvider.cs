using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF5 RID: 3573
	internal sealed class HTMLWindowEvents2_EventProvider : HTMLWindowEvents2_Event, IDisposable
	{
		// Token: 0x060186FE RID: 100094 RVA: 0x000A7638 File Offset: 0x000A6638
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				37, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060186FF RID: 100095 RVA: 0x000A774C File Offset: 0x000A674C
		public override void add_onafterprint(HTMLWindowEvents2_onafterprintEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onafterprintDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018700 RID: 100096 RVA: 0x000A77DC File Offset: 0x000A67DC
		public override void remove_onafterprint(HTMLWindowEvents2_onafterprintEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onafterprintDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onafterprintDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018701 RID: 100097 RVA: 0x000A78CC File Offset: 0x000A68CC
		public override void add_onbeforeprint(HTMLWindowEvents2_onbeforeprintEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onbeforeprintDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018702 RID: 100098 RVA: 0x000A795C File Offset: 0x000A695C
		public override void remove_onbeforeprint(HTMLWindowEvents2_onbeforeprintEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onbeforeprintDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onbeforeprintDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018703 RID: 100099 RVA: 0x000A7A4C File Offset: 0x000A6A4C
		public override void add_onbeforeunload(HTMLWindowEvents2_onbeforeunloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onbeforeunloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018704 RID: 100100 RVA: 0x000A7ADC File Offset: 0x000A6ADC
		public override void remove_onbeforeunload(HTMLWindowEvents2_onbeforeunloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onbeforeunloadDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onbeforeunloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018705 RID: 100101 RVA: 0x000A7BCC File Offset: 0x000A6BCC
		public override void add_onscroll(HTMLWindowEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018706 RID: 100102 RVA: 0x000A7C5C File Offset: 0x000A6C5C
		public override void remove_onscroll(HTMLWindowEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onscrollDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018707 RID: 100103 RVA: 0x000A7D4C File Offset: 0x000A6D4C
		public override void add_onresize(HTMLWindowEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018708 RID: 100104 RVA: 0x000A7DDC File Offset: 0x000A6DDC
		public override void remove_onresize(HTMLWindowEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onresizeDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018709 RID: 100105 RVA: 0x000A7ECC File Offset: 0x000A6ECC
		public override void add_onerror(HTMLWindowEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x0601870A RID: 100106 RVA: 0x000A7F5C File Offset: 0x000A6F5C
		public override void remove_onerror(HTMLWindowEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601870B RID: 100107 RVA: 0x000A804C File Offset: 0x000A704C
		public override void add_onblur(HTMLWindowEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x0601870C RID: 100108 RVA: 0x000A80DC File Offset: 0x000A70DC
		public override void remove_onblur(HTMLWindowEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onblurDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601870D RID: 100109 RVA: 0x000A81CC File Offset: 0x000A71CC
		public override void add_onfocus(HTMLWindowEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x0601870E RID: 100110 RVA: 0x000A825C File Offset: 0x000A725C
		public override void remove_onfocus(HTMLWindowEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onfocusDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601870F RID: 100111 RVA: 0x000A834C File Offset: 0x000A734C
		public override void add_onhelp(HTMLWindowEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018710 RID: 100112 RVA: 0x000A83DC File Offset: 0x000A73DC
		public override void remove_onhelp(HTMLWindowEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onhelpDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018711 RID: 100113 RVA: 0x000A84CC File Offset: 0x000A74CC
		public override void add_onunload(HTMLWindowEvents2_onunloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onunloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018712 RID: 100114 RVA: 0x000A855C File Offset: 0x000A755C
		public override void remove_onunload(HTMLWindowEvents2_onunloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onunloadDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onunloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018713 RID: 100115 RVA: 0x000A864C File Offset: 0x000A764C
		public override void add_onload(HTMLWindowEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = new HTMLWindowEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlwindowEvents2_SinkHelper, ref num);
				htmlwindowEvents2_SinkHelper.m_dwCookie = num;
				htmlwindowEvents2_SinkHelper.m_onloadDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlwindowEvents2_SinkHelper);
			}
		}

		// Token: 0x06018714 RID: 100116 RVA: 0x000A86DC File Offset: 0x000A76DC
		public override void remove_onload(HTMLWindowEvents2_onloadEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper;
					for (;;)
					{
						htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlwindowEvents2_SinkHelper.m_onloadDelegate != null && ((htmlwindowEvents2_SinkHelper.m_onloadDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018715 RID: 100117 RVA: 0x000A87CC File Offset: 0x000A77CC
		public HTMLWindowEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06018716 RID: 100118 RVA: 0x000A87F4 File Offset: 0x000A77F4
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
								HTMLWindowEvents2_SinkHelper htmlwindowEvents2_SinkHelper = (HTMLWindowEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlwindowEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06018717 RID: 100119 RVA: 0x000A88A8 File Offset: 0x000A78A8
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000B11 RID: 2833
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000B12 RID: 2834
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000B13 RID: 2835
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
