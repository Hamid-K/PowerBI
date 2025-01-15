using System;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class TransformationFilter : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x060000FF RID: 255 RVA: 0x000051D4 File Offset: 0x000033D4
		public override string GetXmlElementName()
		{
			return "TransformationFilter";
		}
	}
}
