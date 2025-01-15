using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F51 RID: 3921
	internal class AdobeAnalyticsCompany
	{
		// Token: 0x06006790 RID: 26512 RVA: 0x00164BFC File Offset: 0x00162DFC
		public AdobeAnalyticsCompany(string id, string name, AdobeAnalyticsService service)
		{
			this.Id = id;
			this.Name = name;
			this.service = service;
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06006791 RID: 26513 RVA: 0x00164C19 File Offset: 0x00162E19
		public IList<AdobeAnalyticsCube> Cubes
		{
			get
			{
				if (this.cubes == null)
				{
					this.cubes = this.GetCubes();
				}
				return this.cubes;
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06006792 RID: 26514 RVA: 0x00164C35 File Offset: 0x00162E35
		// (set) Token: 0x06006793 RID: 26515 RVA: 0x00164C3D File Offset: 0x00162E3D
		public virtual string Id { get; private set; }

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x00164C46 File Offset: 0x00162E46
		// (set) Token: 0x06006795 RID: 26517 RVA: 0x00164C4E File Offset: 0x00162E4E
		public virtual string Name { get; private set; }

		// Token: 0x06006796 RID: 26518 RVA: 0x00164C57 File Offset: 0x00162E57
		public static AdobeAnalyticsCompany NewDefaultCompany(AdobeAnalyticsService service)
		{
			return new AdobeAnalyticsCompany.DefaultAdobeAnalyticsCompany(service);
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x00164C5F File Offset: 0x00162E5F
		protected virtual IList<AdobeAnalyticsCube> GetCubes()
		{
			return this.service.GetCubes(this);
		}

		// Token: 0x04003906 RID: 14598
		public const string DefaultCompanyId = "__defaultCompany";

		// Token: 0x04003907 RID: 14599
		private readonly AdobeAnalyticsService service;

		// Token: 0x04003908 RID: 14600
		private IList<AdobeAnalyticsCube> cubes;

		// Token: 0x02000F52 RID: 3922
		private class DefaultAdobeAnalyticsCompany : AdobeAnalyticsCompany
		{
			// Token: 0x06006798 RID: 26520 RVA: 0x00164C6D File Offset: 0x00162E6D
			public DefaultAdobeAnalyticsCompany(AdobeAnalyticsService service)
				: base(string.Empty, string.Empty, service)
			{
			}

			// Token: 0x17001DF4 RID: 7668
			// (get) Token: 0x06006799 RID: 26521 RVA: 0x00164C80 File Offset: 0x00162E80
			public override string Id
			{
				get
				{
					return "__defaultCompany";
				}
			}

			// Token: 0x17001DF5 RID: 7669
			// (get) Token: 0x0600679A RID: 26522 RVA: 0x000020FA File Offset: 0x000002FA
			public override string Name
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600679B RID: 26523 RVA: 0x00164C5F File Offset: 0x00162E5F
			protected override IList<AdobeAnalyticsCube> GetCubes()
			{
				return this.service.GetCubes(this);
			}
		}
	}
}
