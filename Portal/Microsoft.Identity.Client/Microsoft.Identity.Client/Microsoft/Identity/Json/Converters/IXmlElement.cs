using System;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F7 RID: 247
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000CC9 RID: 3273
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000CCA RID: 3274
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CCB RID: 3275
		bool IsEmpty { get; }
	}
}
