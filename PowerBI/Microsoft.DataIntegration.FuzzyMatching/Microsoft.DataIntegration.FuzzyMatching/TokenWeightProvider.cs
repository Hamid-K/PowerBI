using System;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class TokenWeightProvider : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000051B3 File Offset: 0x000033B3
		public static TokenWeightProvider Default
		{
			get
			{
				return new TokenWeightProvider
				{
					TypeName = "IdfTokenWeightProvider"
				};
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000051C5 File Offset: 0x000033C5
		public override string GetXmlElementName()
		{
			return "TokenWeightProvider";
		}
	}
}
