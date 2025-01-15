using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000057 RID: 87
	internal interface IXmlLoadable
	{
		// Token: 0x06000391 RID: 913
		bool LoadXmlAttribute(ModelingXmlReader xr);

		// Token: 0x06000392 RID: 914
		bool LoadXmlElement(ModelingXmlReader xr);
	}
}
