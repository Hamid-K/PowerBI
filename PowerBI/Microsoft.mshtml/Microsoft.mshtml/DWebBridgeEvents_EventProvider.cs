using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC3 RID: 3523
	internal sealed class DWebBridgeEvents_EventProvider : DWebBridgeEvents_Event, IDisposable
	{
		// Token: 0x0601765D RID: 95837 RVA: 0x0001008C File Offset: 0x0000F08C
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				byte.MaxValue, 151, 216, 166, 149, 10, 209, 17, 176, 186,
				0, 96, 8, 22, 110, 17
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601765E RID: 95838 RVA: 0x000101A0 File Offset: 0x0000F1A0
		public override void add_onmouseup(DWebBridgeEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x0601765F RID: 95839 RVA: 0x00010230 File Offset: 0x0000F230
		public override void remove_onmouseup(DWebBridgeEvents_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onmouseupDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017660 RID: 95840 RVA: 0x00010320 File Offset: 0x0000F320
		public override void add_onmousemove(DWebBridgeEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017661 RID: 95841 RVA: 0x000103B0 File Offset: 0x0000F3B0
		public override void remove_onmousemove(DWebBridgeEvents_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onmousemoveDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017662 RID: 95842 RVA: 0x000104A0 File Offset: 0x0000F4A0
		public override void add_onmousedown(DWebBridgeEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017663 RID: 95843 RVA: 0x00010530 File Offset: 0x0000F530
		public override void remove_onmousedown(DWebBridgeEvents_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onmousedownDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017664 RID: 95844 RVA: 0x00010620 File Offset: 0x0000F620
		public override void add_onkeypress(DWebBridgeEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017665 RID: 95845 RVA: 0x000106B0 File Offset: 0x0000F6B0
		public override void remove_onkeypress(DWebBridgeEvents_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onkeypressDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017666 RID: 95846 RVA: 0x000107A0 File Offset: 0x0000F7A0
		public override void add_onkeyup(DWebBridgeEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017667 RID: 95847 RVA: 0x00010830 File Offset: 0x0000F830
		public override void remove_onkeyup(DWebBridgeEvents_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onkeyupDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017668 RID: 95848 RVA: 0x00010920 File Offset: 0x0000F920
		public override void add_onkeydown(DWebBridgeEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017669 RID: 95849 RVA: 0x000109B0 File Offset: 0x0000F9B0
		public override void remove_onkeydown(DWebBridgeEvents_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onkeydownDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601766A RID: 95850 RVA: 0x00010AA0 File Offset: 0x0000FAA0
		public override void add_ondblclick(DWebBridgeEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x0601766B RID: 95851 RVA: 0x00010B30 File Offset: 0x0000FB30
		public override void remove_ondblclick(DWebBridgeEvents_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_ondblclickDelegate != null && ((dwebBridgeEvents_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601766C RID: 95852 RVA: 0x00010C20 File Offset: 0x0000FC20
		public override void add_onclick(DWebBridgeEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x0601766D RID: 95853 RVA: 0x00010CB0 File Offset: 0x0000FCB0
		public override void remove_onclick(DWebBridgeEvents_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onclickDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601766E RID: 95854 RVA: 0x00010DA0 File Offset: 0x0000FDA0
		public override void add_onreadystatechange(DWebBridgeEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x0601766F RID: 95855 RVA: 0x00010E30 File Offset: 0x0000FE30
		public override void remove_onreadystatechange(DWebBridgeEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017670 RID: 95856 RVA: 0x00010F20 File Offset: 0x0000FF20
		public override void add_onscriptletevent(DWebBridgeEvents_onscriptleteventEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = new DWebBridgeEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)dwebBridgeEvents_SinkHelper, ref num);
				dwebBridgeEvents_SinkHelper.m_dwCookie = num;
				dwebBridgeEvents_SinkHelper.m_onscriptleteventDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)dwebBridgeEvents_SinkHelper);
			}
		}

		// Token: 0x06017671 RID: 95857 RVA: 0x00010FB0 File Offset: 0x0000FFB0
		public override void remove_onscriptletevent(DWebBridgeEvents_onscriptleteventEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper;
					for (;;)
					{
						dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (dwebBridgeEvents_SinkHelper.m_onscriptleteventDelegate != null && ((dwebBridgeEvents_SinkHelper.m_onscriptleteventDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017672 RID: 95858 RVA: 0x000110A0 File Offset: 0x000100A0
		public DWebBridgeEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017673 RID: 95859 RVA: 0x000110C8 File Offset: 0x000100C8
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
								DWebBridgeEvents_SinkHelper dwebBridgeEvents_SinkHelper = (DWebBridgeEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(dwebBridgeEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06017674 RID: 95860 RVA: 0x0001117C File Offset: 0x0001017C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400054B RID: 1355
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x0400054C RID: 1356
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x0400054D RID: 1357
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
