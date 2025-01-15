using System;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000BA RID: 186
	public enum ServerMode
	{
		// Token: 0x040004FD RID: 1277
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Multidimensional,
		// Token: 0x040004FE RID: 1278
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		SharePoint,
		// Token: 0x040004FF RID: 1279
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Tabular,
		// Token: 0x04000500 RID: 1280
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		Default
	}
}
