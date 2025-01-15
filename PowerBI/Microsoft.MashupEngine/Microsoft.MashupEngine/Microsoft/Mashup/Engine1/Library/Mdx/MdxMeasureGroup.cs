using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B7 RID: 2487
	internal sealed class MdxMeasureGroup
	{
		// Token: 0x06004701 RID: 18177 RVA: 0x000EE05A File Offset: 0x000EC25A
		public MdxMeasureGroup(string name, string caption)
		{
			this.name = name;
			this.caption = caption;
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x000EE070 File Offset: 0x000EC270
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x000EE078 File Offset: 0x000EC278
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x040025C2 RID: 9666
		private readonly string name;

		// Token: 0x040025C3 RID: 9667
		private readonly string caption;
	}
}
