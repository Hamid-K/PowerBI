using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000FD RID: 253
	internal interface IXmlObjectFactory
	{
		// Token: 0x06000CA7 RID: 3239
		Rule CreateRule(ModelingXmlReader xr);

		// Token: 0x06000CA8 RID: 3240
		Filter CreateFilter(ModelingXmlReader xr);
	}
}
