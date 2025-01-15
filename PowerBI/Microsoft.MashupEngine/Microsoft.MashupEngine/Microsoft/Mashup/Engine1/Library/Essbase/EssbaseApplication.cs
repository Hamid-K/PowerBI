using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C61 RID: 3169
	internal class EssbaseApplication
	{
		// Token: 0x06005611 RID: 22033 RVA: 0x0012A741 File Offset: 0x00128941
		public EssbaseApplication(string name, EssbaseService service)
		{
			this.Name = name;
			this.service = service;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0012A758 File Offset: 0x00128958
		public IEnumerable<EssbaseCube> GetCubes(EssbaseServer server)
		{
			return from cube in EssbaseXmlaParser.ParseCubesResponse(this.service.ExecuteDiscoverRequest("MDSCHEMA_CUBES", server.Info, this.Name, "", null))
				select new EssbaseCube(this.service, server.Info, this.Name, cube);
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06005613 RID: 22035 RVA: 0x0012A7B6 File Offset: 0x001289B6
		// (set) Token: 0x06005614 RID: 22036 RVA: 0x0012A7BE File Offset: 0x001289BE
		public string Name { get; private set; }

		// Token: 0x04003078 RID: 12408
		private readonly EssbaseService service;
	}
}
