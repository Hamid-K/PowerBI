using System;
using System.Collections.Generic;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x0200004E RID: 78
	[Target("NLogViewer")]
	public class NLogViewerTarget : NetworkTarget, IIncludeContext
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00012B18 File Offset: 0x00010D18
		public NLogViewerTarget()
		{
			this.Parameters = new List<NLogViewerParameterInfo>();
			this.Renderer.Parameters = this.Parameters;
			base.OnConnectionOverflow = NetworkTargetConnectionsOverflowAction.Block;
			base.MaxConnections = 16;
			base.NewLine = false;
			base.OptimizeBufferReuse = base.GetType() == typeof(NLogViewerTarget);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00012B83 File Offset: 0x00010D83
		public NLogViewerTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00012B92 File Offset: 0x00010D92
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00012B9F File Offset: 0x00010D9F
		public bool IncludeNLogData
		{
			get
			{
				return this.Renderer.IncludeNLogData;
			}
			set
			{
				this.Renderer.IncludeNLogData = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00012BAD File Offset: 0x00010DAD
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00012BBA File Offset: 0x00010DBA
		public string AppInfo
		{
			get
			{
				return this.Renderer.AppInfo;
			}
			set
			{
				this.Renderer.AppInfo = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00012BC8 File Offset: 0x00010DC8
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00012BD5 File Offset: 0x00010DD5
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00012BE3 File Offset: 0x00010DE3
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x00012BF0 File Offset: 0x00010DF0
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00012BFE File Offset: 0x00010DFE
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00012C0B File Offset: 0x00010E0B
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

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00012C19 File Offset: 0x00010E19
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x00012C26 File Offset: 0x00010E26
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00012C34 File Offset: 0x00010E34
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x00012C41 File Offset: 0x00010E41
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00012C4F File Offset: 0x00010E4F
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x00012C5C File Offset: 0x00010E5C
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00012C6A File Offset: 0x00010E6A
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x00012C77 File Offset: 0x00010E77
		public string NdlcItemSeparator
		{
			get
			{
				return this.Renderer.NdlcItemSeparator;
			}
			set
			{
				this.Renderer.NdlcItemSeparator = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00012C85 File Offset: 0x00010E85
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00012C92 File Offset: 0x00010E92
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00012CA0 File Offset: 0x00010EA0
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00012CAD File Offset: 0x00010EAD
		public string NdcItemSeparator
		{
			get
			{
				return this.Renderer.NdcItemSeparator;
			}
			set
			{
				this.Renderer.NdcItemSeparator = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00012CBB File Offset: 0x00010EBB
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public Layout LoggerName
		{
			get
			{
				return this.Renderer.LoggerName;
			}
			set
			{
				this.Renderer.LoggerName = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00012CD6 File Offset: 0x00010ED6
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00012CDE File Offset: 0x00010EDE
		[ArrayParameter(typeof(NLogViewerParameterInfo), "parameter")]
		public IList<NLogViewerParameterInfo> Parameters { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00012CE7 File Offset: 0x00010EE7
		public Log4JXmlEventLayoutRenderer Renderer
		{
			get
			{
				return this._layout.Renderer;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00012CF4 File Offset: 0x00010EF4
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00012CFC File Offset: 0x00010EFC
		public override Layout Layout
		{
			get
			{
				return this._layout;
			}
			set
			{
			}
		}

		// Token: 0x04000170 RID: 368
		private readonly Log4JXmlEventLayout _layout = new Log4JXmlEventLayout();
	}
}
