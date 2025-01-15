using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Targets;

namespace NLog.Layouts
{
	// Token: 0x020000AA RID: 170
	[Layout("Log4JXmlEventLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class Log4JXmlEventLayout : Layout, IIncludeContext
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x0001C7AF File Offset: 0x0001A9AF
		public Log4JXmlEventLayout()
		{
			this.Renderer = new Log4JXmlEventLayoutRenderer();
			this.Parameters = new List<NLogViewerParameterInfo>();
			this.Renderer.Parameters = this.Parameters;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001C7DE File Offset: 0x0001A9DE
		public Log4JXmlEventLayoutRenderer Renderer { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001C7E6 File Offset: 0x0001A9E6
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0001C7F3 File Offset: 0x0001A9F3
		[ArrayParameter(typeof(NLogViewerParameterInfo), "parameter")]
		public IList<NLogViewerParameterInfo> Parameters
		{
			get
			{
				return this.Renderer.Parameters;
			}
			set
			{
				this.Renderer.Parameters = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0001C801 File Offset: 0x0001AA01
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x0001C80E File Offset: 0x0001AA0E
		public bool IncludeMdc
		{
			get
			{
				return this.Renderer.IncludeMdc;
			}
			set
			{
				this.Renderer.IncludeMdc = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0001C81C File Offset: 0x0001AA1C
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x0001C829 File Offset: 0x0001AA29
		public bool IncludeAllProperties
		{
			get
			{
				return this.Renderer.IncludeAllProperties;
			}
			set
			{
				this.Renderer.IncludeAllProperties = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0001C837 File Offset: 0x0001AA37
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x0001C844 File Offset: 0x0001AA44
		public bool IncludeNdc
		{
			get
			{
				return this.Renderer.IncludeNdc;
			}
			set
			{
				this.Renderer.IncludeNdc = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001C852 File Offset: 0x0001AA52
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0001C85F File Offset: 0x0001AA5F
		public bool IncludeMdlc
		{
			get
			{
				return this.Renderer.IncludeMdlc;
			}
			set
			{
				this.Renderer.IncludeMdlc = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001C86D File Offset: 0x0001AA6D
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0001C87A File Offset: 0x0001AA7A
		public bool IncludeNdlc
		{
			get
			{
				return this.Renderer.IncludeNdlc;
			}
			set
			{
				this.Renderer.IncludeNdlc = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001C888 File Offset: 0x0001AA88
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0001C895 File Offset: 0x0001AA95
		public bool IncludeCallSite
		{
			get
			{
				return this.Renderer.IncludeCallSite;
			}
			set
			{
				this.Renderer.IncludeCallSite = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001C8A3 File Offset: 0x0001AAA3
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public bool IncludeSourceInfo
		{
			get
			{
				return this.Renderer.IncludeSourceInfo;
			}
			set
			{
				this.Renderer.IncludeSourceInfo = value;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001C8BE File Offset: 0x0001AABE
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001C8D2 File Offset: 0x0001AAD2
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			this.Renderer.RenderAppendBuilder(logEvent, target);
		}
	}
}
