using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal sealed class RenderingInfoRoot
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00007CDC File Offset: 0x00005EDC
		internal RenderingInfoRoot()
		{
			this.m_renderingInfo = new Hashtable();
			this.m_sharedRenderingInfo = new Hashtable();
			this.m_pageSectionRenderingInfo = new Hashtable();
			this.m_paginationInfo = null;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00007D0C File Offset: 0x00005F0C
		internal Hashtable RenderingInfo
		{
			get
			{
				return this.m_renderingInfo;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00007D14 File Offset: 0x00005F14
		internal Hashtable SharedRenderingInfo
		{
			get
			{
				return this.m_sharedRenderingInfo;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00007D1C File Offset: 0x00005F1C
		internal Hashtable PageSectionRenderingInfo
		{
			get
			{
				return this.m_pageSectionRenderingInfo;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00007D24 File Offset: 0x00005F24
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00007D3F File Offset: 0x00005F3F
		internal PaginationInfo PaginationInfo
		{
			get
			{
				if (this.m_paginationInfo == null)
				{
					this.m_paginationInfo = new PaginationInfo();
				}
				return this.m_paginationInfo;
			}
			set
			{
				this.m_paginationInfo = value;
			}
		}

		// Token: 0x04000035 RID: 53
		private Hashtable m_renderingInfo;

		// Token: 0x04000036 RID: 54
		private Hashtable m_sharedRenderingInfo;

		// Token: 0x04000037 RID: 55
		private Hashtable m_pageSectionRenderingInfo;

		// Token: 0x04000038 RID: 56
		private PaginationInfo m_paginationInfo;
	}
}
