using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD9 RID: 3545
	internal sealed class HTMLObjectElementEvents2_EventProvider : HTMLObjectElementEvents2_Event, IDisposable
	{
		// Token: 0x06017DA5 RID: 97701 RVA: 0x000522BC File Offset: 0x000512BC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				32, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x06017DA6 RID: 97702 RVA: 0x000523D0 File Offset: 0x000513D0
		public override void add_onreadystatechange(HTMLObjectElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DA7 RID: 97703 RVA: 0x00052460 File Offset: 0x00051460
		public override void remove_onreadystatechange(HTMLObjectElementEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DA8 RID: 97704 RVA: 0x00052550 File Offset: 0x00051550
		public override void add_oncellchange(HTMLObjectElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DA9 RID: 97705 RVA: 0x000525E0 File Offset: 0x000515E0
		public override void remove_oncellchange(HTMLObjectElementEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DAA RID: 97706 RVA: 0x000526D0 File Offset: 0x000516D0
		public override void add_onrowsinserted(HTMLObjectElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DAB RID: 97707 RVA: 0x00052760 File Offset: 0x00051760
		public override void remove_onrowsinserted(HTMLObjectElementEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DAC RID: 97708 RVA: 0x00052850 File Offset: 0x00051850
		public override void add_onrowsdelete(HTMLObjectElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DAD RID: 97709 RVA: 0x000528E0 File Offset: 0x000518E0
		public override void remove_onrowsdelete(HTMLObjectElementEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DAE RID: 97710 RVA: 0x000529D0 File Offset: 0x000519D0
		public override void add_onerror(HTMLObjectElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DAF RID: 97711 RVA: 0x00052A60 File Offset: 0x00051A60
		public override void remove_onerror(HTMLObjectElementEvents2_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onerrorDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DB0 RID: 97712 RVA: 0x00052B50 File Offset: 0x00051B50
		public override void add_ondatasetcomplete(HTMLObjectElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DB1 RID: 97713 RVA: 0x00052BE0 File Offset: 0x00051BE0
		public override void remove_ondatasetcomplete(HTMLObjectElementEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DB2 RID: 97714 RVA: 0x00052CD0 File Offset: 0x00051CD0
		public override void add_ondataavailable(HTMLObjectElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DB3 RID: 97715 RVA: 0x00052D60 File Offset: 0x00051D60
		public override void remove_ondataavailable(HTMLObjectElementEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DB4 RID: 97716 RVA: 0x00052E50 File Offset: 0x00051E50
		public override void add_ondatasetchanged(HTMLObjectElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DB5 RID: 97717 RVA: 0x00052EE0 File Offset: 0x00051EE0
		public override void remove_ondatasetchanged(HTMLObjectElementEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DB6 RID: 97718 RVA: 0x00052FD0 File Offset: 0x00051FD0
		public override void add_onrowenter(HTMLObjectElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DB7 RID: 97719 RVA: 0x00053060 File Offset: 0x00052060
		public override void remove_onrowenter(HTMLObjectElementEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DB8 RID: 97720 RVA: 0x00053150 File Offset: 0x00052150
		public override void add_onrowexit(HTMLObjectElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DB9 RID: 97721 RVA: 0x000531E0 File Offset: 0x000521E0
		public override void remove_onrowexit(HTMLObjectElementEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DBA RID: 97722 RVA: 0x000532D0 File Offset: 0x000522D0
		public override void add_onerrorupdate(HTMLObjectElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DBB RID: 97723 RVA: 0x00053360 File Offset: 0x00052360
		public override void remove_onerrorupdate(HTMLObjectElementEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DBC RID: 97724 RVA: 0x00053450 File Offset: 0x00052450
		public override void add_onafterupdate(HTMLObjectElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DBD RID: 97725 RVA: 0x000534E0 File Offset: 0x000524E0
		public override void remove_onafterupdate(HTMLObjectElementEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DBE RID: 97726 RVA: 0x000535D0 File Offset: 0x000525D0
		public override void add_onbeforeupdate(HTMLObjectElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = new HTMLObjectElementEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents2_SinkHelper, ref num);
				htmlobjectElementEvents2_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents2_SinkHelper);
			}
		}

		// Token: 0x06017DBF RID: 97727 RVA: 0x00053660 File Offset: 0x00052660
		public override void remove_onbeforeupdate(HTMLObjectElementEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlobjectElementEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06017DC0 RID: 97728 RVA: 0x00053750 File Offset: 0x00052750
		public HTMLObjectElementEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06017DC1 RID: 97729 RVA: 0x00053778 File Offset: 0x00052778
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
								HTMLObjectElementEvents2_SinkHelper htmlobjectElementEvents2_SinkHelper = (HTMLObjectElementEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x06017DC2 RID: 97730 RVA: 0x0005382C File Offset: 0x0005282C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040007D4 RID: 2004
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040007D5 RID: 2005
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040007D6 RID: 2006
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
