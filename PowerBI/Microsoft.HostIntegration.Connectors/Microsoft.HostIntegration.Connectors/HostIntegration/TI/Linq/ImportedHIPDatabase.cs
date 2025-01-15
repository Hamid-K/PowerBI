using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000759 RID: 1881
	public class ImportedHIPDatabase
	{
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000CC0B0 File Offset: 0x000CA2B0
		public List<LocalEnvironment> LocalEnvironments
		{
			get
			{
				return this.les;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000CC0B8 File Offset: 0x000CA2B8
		public List<LEEndpoint> LocalEnvironmentEndPoints
		{
			get
			{
				return this.leeps;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000CC0C0 File Offset: 0x000CA2C0
		public List<LinqHostEnvironment> HostEnvironments
		{
			get
			{
				return this.hes;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000CC0C8 File Offset: 0x000CA2C8
		public List<SecurityPolicy> SecurityPolicies
		{
			get
			{
				return this.sps;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000CC0D0 File Offset: 0x000CA2D0
		public List<AffiliatedApplication> AffiliatedApplications
		{
			get
			{
				return this.aas;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000CC0D8 File Offset: 0x000CA2D8
		public List<Object> Objects
		{
			get
			{
				return this.os;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000CC0E0 File Offset: 0x000CA2E0
		public List<TIMFile> TIMFiles
		{
			get
			{
				return this.tfs;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
		public List<Method> Methods
		{
			get
			{
				return this.ms;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000CC0F0 File Offset: 0x000CA2F0
		public List<View> Views
		{
			get
			{
				return this.vs;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000CC0F8 File Offset: 0x000CA2F8
		public List<Determinant> Determinants
		{
			get
			{
				return this.ds;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000CC100 File Offset: 0x000CA300
		public List<HEPermission> HostEnvironmentPermissions
		{
			get
			{
				return this.heps;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000CC108 File Offset: 0x000CA308
		public List<Computer> Computers
		{
			get
			{
				return this.cs;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000CC110 File Offset: 0x000CA310
		public List<Application> Applications
		{
			get
			{
				return this.apps;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000CC118 File Offset: 0x000CA318
		public List<Listener> Listeners
		{
			get
			{
				return this.ls;
			}
		}

		// Token: 0x040023BE RID: 9150
		private List<LocalEnvironment> les = new List<LocalEnvironment>();

		// Token: 0x040023BF RID: 9151
		private List<LEEndpoint> leeps = new List<LEEndpoint>();

		// Token: 0x040023C0 RID: 9152
		private List<LinqHostEnvironment> hes = new List<LinqHostEnvironment>();

		// Token: 0x040023C1 RID: 9153
		private List<SecurityPolicy> sps = new List<SecurityPolicy>();

		// Token: 0x040023C2 RID: 9154
		private List<AffiliatedApplication> aas = new List<AffiliatedApplication>();

		// Token: 0x040023C3 RID: 9155
		private List<Object> os = new List<Object>();

		// Token: 0x040023C4 RID: 9156
		private List<TIMFile> tfs = new List<TIMFile>();

		// Token: 0x040023C5 RID: 9157
		private List<Method> ms = new List<Method>();

		// Token: 0x040023C6 RID: 9158
		private List<View> vs = new List<View>();

		// Token: 0x040023C7 RID: 9159
		private List<Determinant> ds = new List<Determinant>();

		// Token: 0x040023C8 RID: 9160
		private List<HEPermission> heps = new List<HEPermission>();

		// Token: 0x040023C9 RID: 9161
		private List<Computer> cs = new List<Computer>();

		// Token: 0x040023CA RID: 9162
		private List<Application> apps = new List<Application>();

		// Token: 0x040023CB RID: 9163
		private List<Listener> ls = new List<Listener>();
	}
}
