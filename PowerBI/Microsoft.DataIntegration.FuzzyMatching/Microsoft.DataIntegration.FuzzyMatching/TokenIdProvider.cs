using System;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class TokenIdProvider : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000051E3 File Offset: 0x000033E3
		public override string GetXmlElementName()
		{
			return "TokenIdProvider";
		}
	}
}
