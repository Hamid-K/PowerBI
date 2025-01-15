using System;
using System.IO;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Edm;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000042 RID: 66
	// (Invoke) Token: 0x06000175 RID: 373
	public delegate IConceptualSchema ParseConceptualSchema(Stream stream, ConceptualSchemaBuilderOptions options, out ModelDaxCapabilities capabilities);
}
