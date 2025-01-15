using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F8 RID: 248
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x06000CD9 RID: 3289
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x06000CDA RID: 3290
		[return: Nullable(2)]
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CDB RID: 3291
		bool IsEmpty { get; }
	}
}
