using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200043C RID: 1084
	internal sealed class SapHanaDimension
	{
		// Token: 0x060024DD RID: 9437 RVA: 0x000695C6 File Offset: 0x000677C6
		public SapHanaDimension(string name, string caption)
		{
			this.name = name;
			this.caption = caption ?? name;
			this.hierarchies = new Dictionary<string, SapHanaHierarchy>();
			this.attributes = new Dictionary<string, SapHanaDimensionAttribute>();
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000695F7 File Offset: 0x000677F7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000695FF File Offset: 0x000677FF
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00069607 File Offset: 0x00067807
		public Dictionary<string, SapHanaHierarchy> Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0006960F File Offset: 0x0006780F
		public Dictionary<string, SapHanaDimensionAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x04000EE1 RID: 3809
		private readonly string name;

		// Token: 0x04000EE2 RID: 3810
		private readonly string caption;

		// Token: 0x04000EE3 RID: 3811
		private readonly Dictionary<string, SapHanaHierarchy> hierarchies;

		// Token: 0x04000EE4 RID: 3812
		private readonly Dictionary<string, SapHanaDimensionAttribute> attributes;
	}
}
