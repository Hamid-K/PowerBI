using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DE9 RID: 3561
	internal sealed class HTMLNamespaceEvents_EventProvider : HTMLNamespaceEvents_Event, IDisposable
	{
		// Token: 0x06018319 RID: 99097 RVA: 0x000841FC File Offset: 0x000831FC
		private void Init()
		{
			UCOMIConnectionPoint ucomiconnectionPoint = null;
			Guid guid;
			guid..ctor(new byte[]
			{
				189, 246, 80, 48, 181, 152, 207, 17, 187, 130,
				0, 170, 0, 189, 206, 11
			});
			this.m_ConnectionPointContainer.FindConnectionPoint(ref guid, ref ucomiconnectionPoint);
			this.m_ConnectionPoint = (UCOMIConnectionPoint)ucomiconnectionPoint;
			this.m_aEventSinkHelpers = new ArrayList();
		}

		// Token: 0x0601831A RID: 99098 RVA: 0x00084310 File Offset: 0x00083310
		public override void add_onreadystatechange(HTMLNamespaceEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				if (this.m_ConnectionPoint == null)
				{
					this.Init();
				}
				HTMLNamespaceEvents_SinkHelper htmlnamespaceEvents_SinkHelper = new HTMLNamespaceEvents_SinkHelper();
				int num = 0;
				this.m_ConnectionPoint.Advise((object)htmlnamespaceEvents_SinkHelper, ref num);
				htmlnamespaceEvents_SinkHelper.m_dwCookie = num;
				htmlnamespaceEvents_SinkHelper.m_onreadystatechangeDelegate = A_1;
				this.m_aEventSinkHelpers.Add((object)htmlnamespaceEvents_SinkHelper);
			}
		}

		// Token: 0x0601831B RID: 99099 RVA: 0x000843A0 File Offset: 0x000833A0
		public override void remove_onreadystatechange(HTMLNamespaceEvents_onreadystatechangeEventHandler A_1)
		{
			lock (this)
			{
				int count = this.m_aEventSinkHelpers.Count;
				int num = 0;
				if (0 < count)
				{
					HTMLNamespaceEvents_SinkHelper htmlnamespaceEvents_SinkHelper;
					for (;;)
					{
						htmlnamespaceEvents_SinkHelper = (HTMLNamespaceEvents_SinkHelper)this.m_aEventSinkHelpers[num];
						if (htmlnamespaceEvents_SinkHelper.m_onreadystatechangeDelegate != null && ((htmlnamespaceEvents_SinkHelper.m_onreadystatechangeDelegate.Equals((object)A_1) ? 1 : 0) & 255) != 0)
						{
							break;
						}
						num++;
						if (num >= count)
						{
							goto IL_00C4;
						}
					}
					this.m_aEventSinkHelpers.RemoveAt(num);
					this.m_ConnectionPoint.Unadvise(htmlnamespaceEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						this.m_ConnectionPoint = null;
						this.m_aEventSinkHelpers = null;
					}
				}
				IL_00C4:;
			}
		}

		// Token: 0x0601831C RID: 99100 RVA: 0x00084490 File Offset: 0x00083490
		public HTMLNamespaceEvents_EventProvider(object A_1)
		{
			this.m_ConnectionPointContainer = (UCOMIConnectionPointContainer)A_1;
		}

		// Token: 0x0601831D RID: 99101 RVA: 0x000844B8 File Offset: 0x000834B8
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
								HTMLNamespaceEvents_SinkHelper htmlnamespaceEvents_SinkHelper = (HTMLNamespaceEvents_SinkHelper)this.m_aEventSinkHelpers[num];
								this.m_ConnectionPoint.Unadvise(htmlnamespaceEvents_SinkHelper.m_dwCookie);
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

		// Token: 0x0601831E RID: 99102 RVA: 0x0008456C File Offset: 0x0008356C
		public override void Dispose()
		{
			this.Finalize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x040009B0 RID: 2480
		private UCOMIConnectionPointContainer m_ConnectionPointContainer;

		// Token: 0x040009B1 RID: 2481
		private ArrayList m_aEventSinkHelpers;

		// Token: 0x040009B2 RID: 2482
		private UCOMIConnectionPoint m_ConnectionPoint;
	}
}
