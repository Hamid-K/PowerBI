using System;
using dotless.Core.configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotless.Core
{
	// Token: 0x02000004 RID: 4
	public class EngineFactory
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002307 File Offset: 0x00000507
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000230F File Offset: 0x0000050F
		public DotlessConfiguration Configuration { get; set; }

		// Token: 0x06000015 RID: 21 RVA: 0x00002318 File Offset: 0x00000518
		public EngineFactory(DotlessConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002327 File Offset: 0x00000527
		public EngineFactory()
			: this(DotlessConfiguration.GetDefault())
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002334 File Offset: 0x00000534
		public ILessEngine GetEngine()
		{
			return this.GetEngine(new ContainerFactory());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002341 File Offset: 0x00000541
		public ILessEngine GetEngine(ContainerFactory containerFactory)
		{
			return containerFactory.GetContainer(this.Configuration).GetRequiredService<ILessEngine>();
		}
	}
}
