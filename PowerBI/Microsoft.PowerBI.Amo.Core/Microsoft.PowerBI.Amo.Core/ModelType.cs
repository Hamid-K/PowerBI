using System;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A3 RID: 163
	public enum ModelType
	{
		// Token: 0x040004A1 RID: 1185
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Multidimensional,
		// Token: 0x040004A2 RID: 1186
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Tabular,
		// Token: 0x040004A3 RID: 1187
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Default
	}
}
