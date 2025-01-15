using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Microsoft.ReportingServices.RdlGeneration.Properties
{
	// Token: 0x02000384 RID: 900
	internal class Resources
	{
		// Token: 0x06001DEA RID: 7658 RVA: 0x000025F4 File Offset: 0x000007F4
		internal Resources()
		{
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0007A68D File Offset: 0x0007888D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (Resources._resMgr == null)
				{
					Resources._resMgr = new ResourceManager("Microsoft.ReportingServices.RdlGeneration.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources._resMgr;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0007A6B9 File Offset: 0x000788B9
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0007A6C0 File Offset: 0x000788C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources._resCulture;
			}
			set
			{
				Resources._resCulture = value;
			}
		}

		// Token: 0x04000C98 RID: 3224
		private static ResourceManager _resMgr;

		// Token: 0x04000C99 RID: 3225
		private static CultureInfo _resCulture;
	}
}
