using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000E1B RID: 3611
	internal sealed class HTMLObjectElementEvents_EventProvider : HTMLObjectElementEvents_Event, IDisposable
	{
		// Token: 0x0601947C RID: 103548 RVA: 0x001221B4 File Offset: 0x001211B4
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				196, 243, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601947D RID: 103549 RVA: 0x001222C8 File Offset: 0x001212C8
		public override void add_onreadystatechange(HTMLObjectElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601947E RID: 103550 RVA: 0x00122358 File Offset: 0x00121358
		public override void remove_onreadystatechange(HTMLObjectElementEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601947F RID: 103551 RVA: 0x00122448 File Offset: 0x00121448
		public override void add_oncellchange(HTMLObjectElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019480 RID: 103552 RVA: 0x001224D8 File Offset: 0x001214D8
		public override void remove_oncellchange(HTMLObjectElementEvents_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_oncellchangeDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019481 RID: 103553 RVA: 0x001225C8 File Offset: 0x001215C8
		public override void add_onrowsinserted(HTMLObjectElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019482 RID: 103554 RVA: 0x00122658 File Offset: 0x00121658
		public override void remove_onrowsinserted(HTMLObjectElementEvents_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onrowsinsertedDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019483 RID: 103555 RVA: 0x00122748 File Offset: 0x00121748
		public override void add_onrowsdelete(HTMLObjectElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019484 RID: 103556 RVA: 0x001227D8 File Offset: 0x001217D8
		public override void remove_onrowsdelete(HTMLObjectElementEvents_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onrowsdeleteDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019485 RID: 103557 RVA: 0x001228C8 File Offset: 0x001218C8
		public override void add_onerror(HTMLObjectElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onerrorDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019486 RID: 103558 RVA: 0x00122958 File Offset: 0x00121958
		public override void remove_onerror(HTMLObjectElementEvents_onerrorEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onerrorDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onerrorDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019487 RID: 103559 RVA: 0x00122A48 File Offset: 0x00121A48
		public override void add_ondatasetcomplete(HTMLObjectElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019488 RID: 103560 RVA: 0x00122AD8 File Offset: 0x00121AD8
		public override void remove_ondatasetcomplete(HTMLObjectElementEvents_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019489 RID: 103561 RVA: 0x00122BC8 File Offset: 0x00121BC8
		public override void add_ondataavailable(HTMLObjectElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601948A RID: 103562 RVA: 0x00122C58 File Offset: 0x00121C58
		public override void remove_ondataavailable(HTMLObjectElementEvents_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_ondataavailableDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601948B RID: 103563 RVA: 0x00122D48 File Offset: 0x00121D48
		public override void add_ondatasetchanged(HTMLObjectElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601948C RID: 103564 RVA: 0x00122DD8 File Offset: 0x00121DD8
		public override void remove_ondatasetchanged(HTMLObjectElementEvents_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_ondatasetchangedDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601948D RID: 103565 RVA: 0x00122EC8 File Offset: 0x00121EC8
		public override void add_onrowenter(HTMLObjectElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x0601948E RID: 103566 RVA: 0x00122F58 File Offset: 0x00121F58
		public override void remove_onrowenter(HTMLObjectElementEvents_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onrowenterDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601948F RID: 103567 RVA: 0x00123048 File Offset: 0x00122048
		public override void add_onrowexit(HTMLObjectElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019490 RID: 103568 RVA: 0x001230D8 File Offset: 0x001220D8
		public override void remove_onrowexit(HTMLObjectElementEvents_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onrowexitDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019491 RID: 103569 RVA: 0x001231C8 File Offset: 0x001221C8
		public override void add_onerrorupdate(HTMLObjectElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019492 RID: 103570 RVA: 0x00123258 File Offset: 0x00122258
		public override void remove_onerrorupdate(HTMLObjectElementEvents_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onerrorupdateDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019493 RID: 103571 RVA: 0x00123348 File Offset: 0x00122348
		public override void add_onafterupdate(HTMLObjectElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019494 RID: 103572 RVA: 0x001233D8 File Offset: 0x001223D8
		public override void remove_onafterupdate(HTMLObjectElementEvents_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onafterupdateDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019495 RID: 103573 RVA: 0x001234C8 File Offset: 0x001224C8
		public override void add_onbeforeupdate(HTMLObjectElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = new HTMLObjectElementEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlobjectElementEvents_SinkHelper, ref num);
				htmlobjectElementEvents_SinkHelper.m_dwCookie = num;
				htmlobjectElementEvents_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlobjectElementEvents_SinkHelper);
			}
		}

		// Token: 0x06019496 RID: 103574 RVA: 0x00123558 File Offset: 0x00122558
		public override void remove_onbeforeupdate(HTMLObjectElementEvents_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper;
					for (;;)
					{
						htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlobjectElementEvents_SinkHelper.m_onbeforeupdateDelegate != null && ((htmlobjectElementEvents_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06019497 RID: 103575 RVA: 0x00123648 File Offset: 0x00122648
		public HTMLObjectElementEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x06019498 RID: 103576 RVA: 0x00123670 File Offset: 0x00122670
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
								HTMLObjectElementEvents_SinkHelper htmlobjectElementEvents_SinkHelper = (HTMLObjectElementEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlobjectElementEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x06019499 RID: 103577 RVA: 0x00123724 File Offset: 0x00122724
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000FBE RID: 4030
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000FBF RID: 4031
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000FC0 RID: 4032
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
