using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000219 RID: 537
	internal class EnvironmentContext : NamespaceContext
	{
		// Token: 0x06001228 RID: 4648 RVA: 0x00028E32 File Offset: 0x00027032
		internal EnvironmentContext(ReportObjectModelContext reportOmContext, IEnvironmentFilter filter)
			: this()
		{
			this.m_reportObjectModelContext = reportOmContext;
			this.m_lateBoundContext = new LateBoundContext(filter);
			this.m_filter = filter;
			base.InitEnvironment(filter);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00028E5B File Offset: 0x0002705B
		protected EnvironmentContext()
			: base("EnvironmentContext")
		{
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00028E68 File Offset: 0x00027068
		internal EnvironmentContext InitializeCustomAssemblies(List<Assembly> assemblies)
		{
			return new EnvironmentContext.CustomEnvironmentContext(this, assemblies);
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00028E71 File Offset: 0x00027071
		internal virtual ReportObjectModelContext ReportObjectModelContext
		{
			get
			{
				return this.m_reportObjectModelContext;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00028E79 File Offset: 0x00027079
		internal virtual LateBoundContext LateBoundContext
		{
			get
			{
				return this.m_lateBoundContext;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00028E81 File Offset: 0x00027081
		internal static EnvironmentContext DefaultEnvironment
		{
			get
			{
				return EnvironmentContext.m_instance;
			}
		}

		// Token: 0x040005C4 RID: 1476
		private readonly LateBoundContext m_lateBoundContext;

		// Token: 0x040005C5 RID: 1477
		private readonly ReportObjectModelContext m_reportObjectModelContext;

		// Token: 0x040005C6 RID: 1478
		protected IEnvironmentFilter m_filter;

		// Token: 0x040005C7 RID: 1479
		private static readonly EnvironmentContext m_instance = new EnvironmentContext(new ReportObjectModelContext(), DefaultEnvironmentFilter.Instance);

		// Token: 0x0200040D RID: 1037
		private sealed class CustomEnvironmentContext : EnvironmentContext
		{
			// Token: 0x060018E6 RID: 6374 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
			internal CustomEnvironmentContext(EnvironmentContext defaultEnvironment, List<Assembly> customAssemblies)
			{
				this.m_defaultEnvironment = defaultEnvironment;
				foreach (Assembly assembly in customAssemblies)
				{
					base.ProcessAssembly(assembly, this.m_defaultEnvironment.m_filter, null, null, Array.Empty<string>());
				}
			}

			// Token: 0x060018E7 RID: 6375 RVA: 0x0003C014 File Offset: 0x0003A214
			internal override bool TryMatchMember(string identifier, out MemberContext member)
			{
				return base.TryMatchMember(identifier, out member) || this.m_defaultEnvironment.TryMatchMember(identifier, out member);
			}

			// Token: 0x060018E8 RID: 6376 RVA: 0x0003C02F File Offset: 0x0003A22F
			internal override bool TryMatchSubContext(string identifier, out LookupContext subContext)
			{
				return base.TryMatchSubContext(identifier, out subContext) || this.m_defaultEnvironment.TryMatchSubContext(identifier, out subContext);
			}

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0003C04A File Offset: 0x0003A24A
			internal override LateBoundContext LateBoundContext
			{
				get
				{
					return this.m_defaultEnvironment.LateBoundContext;
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x060018EA RID: 6378 RVA: 0x0003C057 File Offset: 0x0003A257
			internal override ReportObjectModelContext ReportObjectModelContext
			{
				get
				{
					return this.m_defaultEnvironment.ReportObjectModelContext;
				}
			}

			// Token: 0x040007CB RID: 1995
			private readonly EnvironmentContext m_defaultEnvironment;
		}
	}
}
