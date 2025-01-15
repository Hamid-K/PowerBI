using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000055 RID: 85
	[NLogConfigurationItem]
	[ThreadAgnostic]
	public class TargetPropertyWithContext
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001403B File Offset: 0x0001223B
		public TargetPropertyWithContext()
			: this(null, null)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00014045 File Offset: 0x00012245
		public TargetPropertyWithContext(string name, Layout layout)
		{
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00014072 File Offset: 0x00012272
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001407A File Offset: 0x0001227A
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00014083 File Offset: 0x00012283
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001408B File Offset: 0x0001228B
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00014094 File Offset: 0x00012294
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001409C File Offset: 0x0001229C
		[DefaultValue(true)]
		public bool IncludeEmptyValue { get; set; } = true;

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000140A5 File Offset: 0x000122A5
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x000140AD File Offset: 0x000122AD
		[DefaultValue(typeof(string))]
		public Type PropertyType { get; set; } = typeof(string);
	}
}
