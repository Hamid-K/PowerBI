using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DF1 RID: 3569
	internal sealed class HTMLTableEvents2_EventProvider : HTMLTableEvents2_Event, IDisposable
	{
		// Token: 0x060185AD RID: 99757 RVA: 0x0009A4C8 File Offset: 0x000994C8
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				35, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x060185AE RID: 99758 RVA: 0x0009A5DC File Offset: 0x000995DC
		public override void add_onmousewheel(HTMLTableEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmousewheelDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185AF RID: 99759 RVA: 0x0009A66C File Offset: 0x0009966C
		public override void remove_onmousewheel(HTMLTableEvents2_onmousewheelEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmousewheelDelegate != null && ((htmltableEvents2_SinkHelper.m_onmousewheelDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185B0 RID: 99760 RVA: 0x0009A75C File Offset: 0x0009975C
		public override void add_onresizeend(HTMLTableEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onresizeendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185B1 RID: 99761 RVA: 0x0009A7EC File Offset: 0x000997EC
		public override void remove_onresizeend(HTMLTableEvents2_onresizeendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onresizeendDelegate != null && ((htmltableEvents2_SinkHelper.m_onresizeendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185B2 RID: 99762 RVA: 0x0009A8DC File Offset: 0x000998DC
		public override void add_onresizestart(HTMLTableEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onresizestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185B3 RID: 99763 RVA: 0x0009A96C File Offset: 0x0009996C
		public override void remove_onresizestart(HTMLTableEvents2_onresizestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onresizestartDelegate != null && ((htmltableEvents2_SinkHelper.m_onresizestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185B4 RID: 99764 RVA: 0x0009AA5C File Offset: 0x00099A5C
		public override void add_onmoveend(HTMLTableEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmoveendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185B5 RID: 99765 RVA: 0x0009AAEC File Offset: 0x00099AEC
		public override void remove_onmoveend(HTMLTableEvents2_onmoveendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmoveendDelegate != null && ((htmltableEvents2_SinkHelper.m_onmoveendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185B6 RID: 99766 RVA: 0x0009ABDC File Offset: 0x00099BDC
		public override void add_onmovestart(HTMLTableEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmovestartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185B7 RID: 99767 RVA: 0x0009AC6C File Offset: 0x00099C6C
		public override void remove_onmovestart(HTMLTableEvents2_onmovestartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmovestartDelegate != null && ((htmltableEvents2_SinkHelper.m_onmovestartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185B8 RID: 99768 RVA: 0x0009AD5C File Offset: 0x00099D5C
		public override void add_oncontrolselect(HTMLTableEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_oncontrolselectDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185B9 RID: 99769 RVA: 0x0009ADEC File Offset: 0x00099DEC
		public override void remove_oncontrolselect(HTMLTableEvents2_oncontrolselectEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_oncontrolselectDelegate != null && ((htmltableEvents2_SinkHelper.m_oncontrolselectDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185BA RID: 99770 RVA: 0x0009AEDC File Offset: 0x00099EDC
		public override void add_onmove(HTMLTableEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185BB RID: 99771 RVA: 0x0009AF6C File Offset: 0x00099F6C
		public override void remove_onmove(HTMLTableEvents2_onmoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmoveDelegate != null && ((htmltableEvents2_SinkHelper.m_onmoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185BC RID: 99772 RVA: 0x0009B05C File Offset: 0x0009A05C
		public override void add_onfocusout(HTMLTableEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onfocusoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185BD RID: 99773 RVA: 0x0009B0EC File Offset: 0x0009A0EC
		public override void remove_onfocusout(HTMLTableEvents2_onfocusoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onfocusoutDelegate != null && ((htmltableEvents2_SinkHelper.m_onfocusoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185BE RID: 99774 RVA: 0x0009B1DC File Offset: 0x0009A1DC
		public override void add_onfocusin(HTMLTableEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onfocusinDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185BF RID: 99775 RVA: 0x0009B26C File Offset: 0x0009A26C
		public override void remove_onfocusin(HTMLTableEvents2_onfocusinEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onfocusinDelegate != null && ((htmltableEvents2_SinkHelper.m_onfocusinDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185C0 RID: 99776 RVA: 0x0009B35C File Offset: 0x0009A35C
		public override void add_onbeforeactivate(HTMLTableEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185C1 RID: 99777 RVA: 0x0009B3EC File Offset: 0x0009A3EC
		public override void remove_onbeforeactivate(HTMLTableEvents2_onbeforeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforeactivateDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185C2 RID: 99778 RVA: 0x0009B4DC File Offset: 0x0009A4DC
		public override void add_onbeforedeactivate(HTMLTableEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforedeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185C3 RID: 99779 RVA: 0x0009B56C File Offset: 0x0009A56C
		public override void remove_onbeforedeactivate(HTMLTableEvents2_onbeforedeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforedeactivateDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforedeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185C4 RID: 99780 RVA: 0x0009B65C File Offset: 0x0009A65C
		public override void add_ondeactivate(HTMLTableEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondeactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185C5 RID: 99781 RVA: 0x0009B6EC File Offset: 0x0009A6EC
		public override void remove_ondeactivate(HTMLTableEvents2_ondeactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondeactivateDelegate != null && ((htmltableEvents2_SinkHelper.m_ondeactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185C6 RID: 99782 RVA: 0x0009B7DC File Offset: 0x0009A7DC
		public override void add_onactivate(HTMLTableEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onactivateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185C7 RID: 99783 RVA: 0x0009B86C File Offset: 0x0009A86C
		public override void remove_onactivate(HTMLTableEvents2_onactivateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onactivateDelegate != null && ((htmltableEvents2_SinkHelper.m_onactivateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185C8 RID: 99784 RVA: 0x0009B95C File Offset: 0x0009A95C
		public override void add_onmouseleave(HTMLTableEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmouseleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185C9 RID: 99785 RVA: 0x0009B9EC File Offset: 0x0009A9EC
		public override void remove_onmouseleave(HTMLTableEvents2_onmouseleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmouseleaveDelegate != null && ((htmltableEvents2_SinkHelper.m_onmouseleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185CA RID: 99786 RVA: 0x0009BADC File Offset: 0x0009AADC
		public override void add_onmouseenter(HTMLTableEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmouseenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185CB RID: 99787 RVA: 0x0009BB6C File Offset: 0x0009AB6C
		public override void remove_onmouseenter(HTMLTableEvents2_onmouseenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmouseenterDelegate != null && ((htmltableEvents2_SinkHelper.m_onmouseenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185CC RID: 99788 RVA: 0x0009BC5C File Offset: 0x0009AC5C
		public override void add_onpage(HTMLTableEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onpageDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185CD RID: 99789 RVA: 0x0009BCEC File Offset: 0x0009ACEC
		public override void remove_onpage(HTMLTableEvents2_onpageEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onpageDelegate != null && ((htmltableEvents2_SinkHelper.m_onpageDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185CE RID: 99790 RVA: 0x0009BDDC File Offset: 0x0009ADDC
		public override void add_onlayoutcomplete(HTMLTableEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onlayoutcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185CF RID: 99791 RVA: 0x0009BE6C File Offset: 0x0009AE6C
		public override void remove_onlayoutcomplete(HTMLTableEvents2_onlayoutcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onlayoutcompleteDelegate != null && ((htmltableEvents2_SinkHelper.m_onlayoutcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185D0 RID: 99792 RVA: 0x0009BF5C File Offset: 0x0009AF5C
		public override void add_onreadystatechange(HTMLTableEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185D1 RID: 99793 RVA: 0x0009BFEC File Offset: 0x0009AFEC
		public override void remove_onreadystatechange(HTMLTableEvents2_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onreadystatechangeDelegate != null && ((htmltableEvents2_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185D2 RID: 99794 RVA: 0x0009C0DC File Offset: 0x0009B0DC
		public override void add_oncellchange(HTMLTableEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_oncellchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185D3 RID: 99795 RVA: 0x0009C16C File Offset: 0x0009B16C
		public override void remove_oncellchange(HTMLTableEvents2_oncellchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_oncellchangeDelegate != null && ((htmltableEvents2_SinkHelper.m_oncellchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185D4 RID: 99796 RVA: 0x0009C25C File Offset: 0x0009B25C
		public override void add_onrowsinserted(HTMLTableEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onrowsinsertedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185D5 RID: 99797 RVA: 0x0009C2EC File Offset: 0x0009B2EC
		public override void remove_onrowsinserted(HTMLTableEvents2_onrowsinsertedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onrowsinsertedDelegate != null && ((htmltableEvents2_SinkHelper.m_onrowsinsertedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185D6 RID: 99798 RVA: 0x0009C3DC File Offset: 0x0009B3DC
		public override void add_onrowsdelete(HTMLTableEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onrowsdeleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185D7 RID: 99799 RVA: 0x0009C46C File Offset: 0x0009B46C
		public override void remove_onrowsdelete(HTMLTableEvents2_onrowsdeleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onrowsdeleteDelegate != null && ((htmltableEvents2_SinkHelper.m_onrowsdeleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185D8 RID: 99800 RVA: 0x0009C55C File Offset: 0x0009B55C
		public override void add_oncontextmenu(HTMLTableEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_oncontextmenuDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185D9 RID: 99801 RVA: 0x0009C5EC File Offset: 0x0009B5EC
		public override void remove_oncontextmenu(HTMLTableEvents2_oncontextmenuEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_oncontextmenuDelegate != null && ((htmltableEvents2_SinkHelper.m_oncontextmenuDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185DA RID: 99802 RVA: 0x0009C6DC File Offset: 0x0009B6DC
		public override void add_onpaste(HTMLTableEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onpasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185DB RID: 99803 RVA: 0x0009C76C File Offset: 0x0009B76C
		public override void remove_onpaste(HTMLTableEvents2_onpasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onpasteDelegate != null && ((htmltableEvents2_SinkHelper.m_onpasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185DC RID: 99804 RVA: 0x0009C85C File Offset: 0x0009B85C
		public override void add_onbeforepaste(HTMLTableEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforepasteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185DD RID: 99805 RVA: 0x0009C8EC File Offset: 0x0009B8EC
		public override void remove_onbeforepaste(HTMLTableEvents2_onbeforepasteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforepasteDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforepasteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185DE RID: 99806 RVA: 0x0009C9DC File Offset: 0x0009B9DC
		public override void add_oncopy(HTMLTableEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_oncopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185DF RID: 99807 RVA: 0x0009CA6C File Offset: 0x0009BA6C
		public override void remove_oncopy(HTMLTableEvents2_oncopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_oncopyDelegate != null && ((htmltableEvents2_SinkHelper.m_oncopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185E0 RID: 99808 RVA: 0x0009CB5C File Offset: 0x0009BB5C
		public override void add_onbeforecopy(HTMLTableEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforecopyDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185E1 RID: 99809 RVA: 0x0009CBEC File Offset: 0x0009BBEC
		public override void remove_onbeforecopy(HTMLTableEvents2_onbeforecopyEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforecopyDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforecopyDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185E2 RID: 99810 RVA: 0x0009CCDC File Offset: 0x0009BCDC
		public override void add_oncut(HTMLTableEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_oncutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185E3 RID: 99811 RVA: 0x0009CD6C File Offset: 0x0009BD6C
		public override void remove_oncut(HTMLTableEvents2_oncutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_oncutDelegate != null && ((htmltableEvents2_SinkHelper.m_oncutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185E4 RID: 99812 RVA: 0x0009CE5C File Offset: 0x0009BE5C
		public override void add_onbeforecut(HTMLTableEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforecutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185E5 RID: 99813 RVA: 0x0009CEEC File Offset: 0x0009BEEC
		public override void remove_onbeforecut(HTMLTableEvents2_onbeforecutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforecutDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforecutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185E6 RID: 99814 RVA: 0x0009CFDC File Offset: 0x0009BFDC
		public override void add_ondrop(HTMLTableEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondropDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185E7 RID: 99815 RVA: 0x0009D06C File Offset: 0x0009C06C
		public override void remove_ondrop(HTMLTableEvents2_ondropEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondropDelegate != null && ((htmltableEvents2_SinkHelper.m_ondropDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185E8 RID: 99816 RVA: 0x0009D15C File Offset: 0x0009C15C
		public override void add_ondragleave(HTMLTableEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragleaveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185E9 RID: 99817 RVA: 0x0009D1EC File Offset: 0x0009C1EC
		public override void remove_ondragleave(HTMLTableEvents2_ondragleaveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragleaveDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragleaveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185EA RID: 99818 RVA: 0x0009D2DC File Offset: 0x0009C2DC
		public override void add_ondragover(HTMLTableEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185EB RID: 99819 RVA: 0x0009D36C File Offset: 0x0009C36C
		public override void remove_ondragover(HTMLTableEvents2_ondragoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragoverDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185EC RID: 99820 RVA: 0x0009D45C File Offset: 0x0009C45C
		public override void add_ondragenter(HTMLTableEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185ED RID: 99821 RVA: 0x0009D4EC File Offset: 0x0009C4EC
		public override void remove_ondragenter(HTMLTableEvents2_ondragenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragenterDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185EE RID: 99822 RVA: 0x0009D5DC File Offset: 0x0009C5DC
		public override void add_ondragend(HTMLTableEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragendDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185EF RID: 99823 RVA: 0x0009D66C File Offset: 0x0009C66C
		public override void remove_ondragend(HTMLTableEvents2_ondragendEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragendDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragendDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185F0 RID: 99824 RVA: 0x0009D75C File Offset: 0x0009C75C
		public override void add_ondrag(HTMLTableEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185F1 RID: 99825 RVA: 0x0009D7EC File Offset: 0x0009C7EC
		public override void remove_ondrag(HTMLTableEvents2_ondragEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185F2 RID: 99826 RVA: 0x0009D8DC File Offset: 0x0009C8DC
		public override void add_onresize(HTMLTableEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onresizeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185F3 RID: 99827 RVA: 0x0009D96C File Offset: 0x0009C96C
		public override void remove_onresize(HTMLTableEvents2_onresizeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onresizeDelegate != null && ((htmltableEvents2_SinkHelper.m_onresizeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185F4 RID: 99828 RVA: 0x0009DA5C File Offset: 0x0009CA5C
		public override void add_onblur(HTMLTableEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onblurDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185F5 RID: 99829 RVA: 0x0009DAEC File Offset: 0x0009CAEC
		public override void remove_onblur(HTMLTableEvents2_onblurEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onblurDelegate != null && ((htmltableEvents2_SinkHelper.m_onblurDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185F6 RID: 99830 RVA: 0x0009DBDC File Offset: 0x0009CBDC
		public override void add_onfocus(HTMLTableEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onfocusDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185F7 RID: 99831 RVA: 0x0009DC6C File Offset: 0x0009CC6C
		public override void remove_onfocus(HTMLTableEvents2_onfocusEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onfocusDelegate != null && ((htmltableEvents2_SinkHelper.m_onfocusDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185F8 RID: 99832 RVA: 0x0009DD5C File Offset: 0x0009CD5C
		public override void add_onscroll(HTMLTableEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onscrollDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185F9 RID: 99833 RVA: 0x0009DDEC File Offset: 0x0009CDEC
		public override void remove_onscroll(HTMLTableEvents2_onscrollEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onscrollDelegate != null && ((htmltableEvents2_SinkHelper.m_onscrollDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185FA RID: 99834 RVA: 0x0009DEDC File Offset: 0x0009CEDC
		public override void add_onpropertychange(HTMLTableEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onpropertychangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185FB RID: 99835 RVA: 0x0009DF6C File Offset: 0x0009CF6C
		public override void remove_onpropertychange(HTMLTableEvents2_onpropertychangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onpropertychangeDelegate != null && ((htmltableEvents2_SinkHelper.m_onpropertychangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185FC RID: 99836 RVA: 0x0009E05C File Offset: 0x0009D05C
		public override void add_onlosecapture(HTMLTableEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onlosecaptureDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185FD RID: 99837 RVA: 0x0009E0EC File Offset: 0x0009D0EC
		public override void remove_onlosecapture(HTMLTableEvents2_onlosecaptureEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onlosecaptureDelegate != null && ((htmltableEvents2_SinkHelper.m_onlosecaptureDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x060185FE RID: 99838 RVA: 0x0009E1DC File Offset: 0x0009D1DC
		public override void add_ondatasetcomplete(HTMLTableEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondatasetcompleteDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x060185FF RID: 99839 RVA: 0x0009E26C File Offset: 0x0009D26C
		public override void remove_ondatasetcomplete(HTMLTableEvents2_ondatasetcompleteEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondatasetcompleteDelegate != null && ((htmltableEvents2_SinkHelper.m_ondatasetcompleteDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018600 RID: 99840 RVA: 0x0009E35C File Offset: 0x0009D35C
		public override void add_ondataavailable(HTMLTableEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondataavailableDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018601 RID: 99841 RVA: 0x0009E3EC File Offset: 0x0009D3EC
		public override void remove_ondataavailable(HTMLTableEvents2_ondataavailableEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondataavailableDelegate != null && ((htmltableEvents2_SinkHelper.m_ondataavailableDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018602 RID: 99842 RVA: 0x0009E4DC File Offset: 0x0009D4DC
		public override void add_ondatasetchanged(HTMLTableEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondatasetchangedDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018603 RID: 99843 RVA: 0x0009E56C File Offset: 0x0009D56C
		public override void remove_ondatasetchanged(HTMLTableEvents2_ondatasetchangedEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondatasetchangedDelegate != null && ((htmltableEvents2_SinkHelper.m_ondatasetchangedDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018604 RID: 99844 RVA: 0x0009E65C File Offset: 0x0009D65C
		public override void add_onrowenter(HTMLTableEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onrowenterDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018605 RID: 99845 RVA: 0x0009E6EC File Offset: 0x0009D6EC
		public override void remove_onrowenter(HTMLTableEvents2_onrowenterEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onrowenterDelegate != null && ((htmltableEvents2_SinkHelper.m_onrowenterDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018606 RID: 99846 RVA: 0x0009E7DC File Offset: 0x0009D7DC
		public override void add_onrowexit(HTMLTableEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onrowexitDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018607 RID: 99847 RVA: 0x0009E86C File Offset: 0x0009D86C
		public override void remove_onrowexit(HTMLTableEvents2_onrowexitEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onrowexitDelegate != null && ((htmltableEvents2_SinkHelper.m_onrowexitDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018608 RID: 99848 RVA: 0x0009E95C File Offset: 0x0009D95C
		public override void add_onerrorupdate(HTMLTableEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onerrorupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018609 RID: 99849 RVA: 0x0009E9EC File Offset: 0x0009D9EC
		public override void remove_onerrorupdate(HTMLTableEvents2_onerrorupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onerrorupdateDelegate != null && ((htmltableEvents2_SinkHelper.m_onerrorupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601860A RID: 99850 RVA: 0x0009EADC File Offset: 0x0009DADC
		public override void add_onafterupdate(HTMLTableEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onafterupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601860B RID: 99851 RVA: 0x0009EB6C File Offset: 0x0009DB6C
		public override void remove_onafterupdate(HTMLTableEvents2_onafterupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onafterupdateDelegate != null && ((htmltableEvents2_SinkHelper.m_onafterupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601860C RID: 99852 RVA: 0x0009EC5C File Offset: 0x0009DC5C
		public override void add_onbeforeupdate(HTMLTableEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onbeforeupdateDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601860D RID: 99853 RVA: 0x0009ECEC File Offset: 0x0009DCEC
		public override void remove_onbeforeupdate(HTMLTableEvents2_onbeforeupdateEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onbeforeupdateDelegate != null && ((htmltableEvents2_SinkHelper.m_onbeforeupdateDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601860E RID: 99854 RVA: 0x0009EDDC File Offset: 0x0009DDDC
		public override void add_ondragstart(HTMLTableEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondragstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601860F RID: 99855 RVA: 0x0009EE6C File Offset: 0x0009DE6C
		public override void remove_ondragstart(HTMLTableEvents2_ondragstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondragstartDelegate != null && ((htmltableEvents2_SinkHelper.m_ondragstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018610 RID: 99856 RVA: 0x0009EF5C File Offset: 0x0009DF5C
		public override void add_onfilterchange(HTMLTableEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onfilterchangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018611 RID: 99857 RVA: 0x0009EFEC File Offset: 0x0009DFEC
		public override void remove_onfilterchange(HTMLTableEvents2_onfilterchangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onfilterchangeDelegate != null && ((htmltableEvents2_SinkHelper.m_onfilterchangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018612 RID: 99858 RVA: 0x0009F0DC File Offset: 0x0009E0DC
		public override void add_onselectstart(HTMLTableEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onselectstartDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018613 RID: 99859 RVA: 0x0009F16C File Offset: 0x0009E16C
		public override void remove_onselectstart(HTMLTableEvents2_onselectstartEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onselectstartDelegate != null && ((htmltableEvents2_SinkHelper.m_onselectstartDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018614 RID: 99860 RVA: 0x0009F25C File Offset: 0x0009E25C
		public override void add_onmouseup(HTMLTableEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmouseupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018615 RID: 99861 RVA: 0x0009F2EC File Offset: 0x0009E2EC
		public override void remove_onmouseup(HTMLTableEvents2_onmouseupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmouseupDelegate != null && ((htmltableEvents2_SinkHelper.m_onmouseupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018616 RID: 99862 RVA: 0x0009F3DC File Offset: 0x0009E3DC
		public override void add_onmousedown(HTMLTableEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmousedownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018617 RID: 99863 RVA: 0x0009F46C File Offset: 0x0009E46C
		public override void remove_onmousedown(HTMLTableEvents2_onmousedownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmousedownDelegate != null && ((htmltableEvents2_SinkHelper.m_onmousedownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018618 RID: 99864 RVA: 0x0009F55C File Offset: 0x0009E55C
		public override void add_onmousemove(HTMLTableEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmousemoveDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018619 RID: 99865 RVA: 0x0009F5EC File Offset: 0x0009E5EC
		public override void remove_onmousemove(HTMLTableEvents2_onmousemoveEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmousemoveDelegate != null && ((htmltableEvents2_SinkHelper.m_onmousemoveDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601861A RID: 99866 RVA: 0x0009F6DC File Offset: 0x0009E6DC
		public override void add_onmouseover(HTMLTableEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmouseoverDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601861B RID: 99867 RVA: 0x0009F76C File Offset: 0x0009E76C
		public override void remove_onmouseover(HTMLTableEvents2_onmouseoverEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmouseoverDelegate != null && ((htmltableEvents2_SinkHelper.m_onmouseoverDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601861C RID: 99868 RVA: 0x0009F85C File Offset: 0x0009E85C
		public override void add_onmouseout(HTMLTableEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onmouseoutDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601861D RID: 99869 RVA: 0x0009F8EC File Offset: 0x0009E8EC
		public override void remove_onmouseout(HTMLTableEvents2_onmouseoutEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onmouseoutDelegate != null && ((htmltableEvents2_SinkHelper.m_onmouseoutDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601861E RID: 99870 RVA: 0x0009F9DC File Offset: 0x0009E9DC
		public override void add_onkeyup(HTMLTableEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onkeyupDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x0601861F RID: 99871 RVA: 0x0009FA6C File Offset: 0x0009EA6C
		public override void remove_onkeyup(HTMLTableEvents2_onkeyupEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onkeyupDelegate != null && ((htmltableEvents2_SinkHelper.m_onkeyupDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018620 RID: 99872 RVA: 0x0009FB5C File Offset: 0x0009EB5C
		public override void add_onkeydown(HTMLTableEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onkeydownDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018621 RID: 99873 RVA: 0x0009FBEC File Offset: 0x0009EBEC
		public override void remove_onkeydown(HTMLTableEvents2_onkeydownEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onkeydownDelegate != null && ((htmltableEvents2_SinkHelper.m_onkeydownDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018622 RID: 99874 RVA: 0x0009FCDC File Offset: 0x0009ECDC
		public override void add_onkeypress(HTMLTableEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onkeypressDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018623 RID: 99875 RVA: 0x0009FD6C File Offset: 0x0009ED6C
		public override void remove_onkeypress(HTMLTableEvents2_onkeypressEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onkeypressDelegate != null && ((htmltableEvents2_SinkHelper.m_onkeypressDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018624 RID: 99876 RVA: 0x0009FE5C File Offset: 0x0009EE5C
		public override void add_ondblclick(HTMLTableEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_ondblclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018625 RID: 99877 RVA: 0x0009FEEC File Offset: 0x0009EEEC
		public override void remove_ondblclick(HTMLTableEvents2_ondblclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_ondblclickDelegate != null && ((htmltableEvents2_SinkHelper.m_ondblclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018626 RID: 99878 RVA: 0x0009FFDC File Offset: 0x0009EFDC
		public override void add_onclick(HTMLTableEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onclickDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018627 RID: 99879 RVA: 0x000A006C File Offset: 0x0009F06C
		public override void remove_onclick(HTMLTableEvents2_onclickEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onclickDelegate != null && ((htmltableEvents2_SinkHelper.m_onclickDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x06018628 RID: 99880 RVA: 0x000A015C File Offset: 0x0009F15C
		public override void add_onhelp(HTMLTableEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = new HTMLTableEvents2_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmltableEvents2_SinkHelper, ref num);
				htmltableEvents2_SinkHelper.m_dwCookie = num;
				htmltableEvents2_SinkHelper.m_onhelpDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmltableEvents2_SinkHelper);
			}
		}

		// Token: 0x06018629 RID: 99881 RVA: 0x000A01EC File Offset: 0x0009F1EC
		public override void remove_onhelp(HTMLTableEvents2_onhelpEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper;
					for (;;)
					{
						htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmltableEvents2_SinkHelper.m_onhelpDelegate != null && ((htmltableEvents2_SinkHelper.m_onhelpDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601862A RID: 99882 RVA: 0x000A02DC File Offset: 0x0009F2DC
		public HTMLTableEvents2_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601862B RID: 99883 RVA: 0x000A0304 File Offset: 0x0009F304
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
								HTMLTableEvents2_SinkHelper htmltableEvents2_SinkHelper = (HTMLTableEvents2_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmltableEvents2_SinkHelper.m_dwCookie);
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

		// Token: 0x0601862C RID: 99884 RVA: 0x000A03B8 File Offset: 0x0009F3B8
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000ABE RID: 2750
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x04000ABF RID: 2751
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x04000AC0 RID: 2752
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
