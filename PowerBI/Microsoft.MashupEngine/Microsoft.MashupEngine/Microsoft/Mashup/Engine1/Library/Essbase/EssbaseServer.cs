using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C80 RID: 3200
	internal class EssbaseServer
	{
		// Token: 0x060056AC RID: 22188 RVA: 0x0012C732 File Offset: 0x0012A932
		public EssbaseServer(string name, string info, EssbaseService service)
		{
			this.Name = name;
			this.Info = info;
			this.service = service;
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x0012C74F File Offset: 0x0012A94F
		public IEnumerable<EssbaseApplication> GetApplications()
		{
			return from application in EssbaseXmlaParser.ParseApplicationsResponse(this.service.ExecuteDiscoverRequest("DBSCHEMA_CATALOGS", this.Info, "", "", null))
				select new EssbaseApplication(application, this.service);
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x060056AE RID: 22190 RVA: 0x0012C788 File Offset: 0x0012A988
		// (set) Token: 0x060056AF RID: 22191 RVA: 0x0012C790 File Offset: 0x0012A990
		public string Name { get; private set; }

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x060056B0 RID: 22192 RVA: 0x0012C799 File Offset: 0x0012A999
		// (set) Token: 0x060056B1 RID: 22193 RVA: 0x0012C7A1 File Offset: 0x0012A9A1
		public string Info { get; private set; }

		// Token: 0x040030CE RID: 12494
		private readonly EssbaseService service;
	}
}
