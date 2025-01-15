using System;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B9 RID: 185
	public enum ServerLocation
	{
		// Token: 0x040004FA RID: 1274
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2012/engine/400")]
		OnPremise,
		// Token: 0x040004FB RID: 1275
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2012/engine/400")]
		Azure
	}
}
