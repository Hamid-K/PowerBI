using System;
using System.IO;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000059 RID: 89
	internal interface IModelParser
	{
		// Token: 0x06000400 RID: 1024
		object Parse(Stream modelStream);
	}
}
