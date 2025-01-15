using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000058 RID: 88
	public abstract class TargetWithLayoutHeaderAndFooter : TargetWithLayout
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00014B08 File Offset: 0x00012D08
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00014B15 File Offset: 0x00012D15
		[RequiredParameter]
		public override Layout Layout
		{
			get
			{
				return this.LHF.Layout;
			}
			set
			{
				if (value is LayoutWithHeaderAndFooter)
				{
					base.Layout = value;
					return;
				}
				if (this.LHF == null)
				{
					this.LHF = new LayoutWithHeaderAndFooter
					{
						Layout = value
					};
					return;
				}
				this.LHF.Layout = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00014B4E File Offset: 0x00012D4E
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00014B5B File Offset: 0x00012D5B
		public Layout Footer
		{
			get
			{
				return this.LHF.Footer;
			}
			set
			{
				this.LHF.Footer = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00014B69 File Offset: 0x00012D69
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00014B76 File Offset: 0x00012D76
		public Layout Header
		{
			get
			{
				return this.LHF.Header;
			}
			set
			{
				this.LHF.Header = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00014B84 File Offset: 0x00012D84
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00014B91 File Offset: 0x00012D91
		private LayoutWithHeaderAndFooter LHF
		{
			get
			{
				return (LayoutWithHeaderAndFooter)base.Layout;
			}
			set
			{
				base.Layout = value;
			}
		}
	}
}
