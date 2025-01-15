using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000123 RID: 291
	internal interface IObjectRowsetSchema
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600146E RID: 5230
		XElement XmlSchema { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600146F RID: 5231
		IList<string> OrderedPropertyList { get; }
	}
}
