using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F5 RID: 245
	[NullableContext(1)]
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x06000CC3 RID: 3267
		IXmlNode CreateComment([Nullable(2)] string text);

		// Token: 0x06000CC4 RID: 3268
		IXmlNode CreateTextNode([Nullable(2)] string text);

		// Token: 0x06000CC5 RID: 3269
		IXmlNode CreateCDataSection([Nullable(2)] string data);

		// Token: 0x06000CC6 RID: 3270
		IXmlNode CreateWhitespace([Nullable(2)] string text);

		// Token: 0x06000CC7 RID: 3271
		IXmlNode CreateSignificantWhitespace([Nullable(2)] string text);

		// Token: 0x06000CC8 RID: 3272
		IXmlNode CreateXmlDeclaration(string version, [Nullable(2)] string encoding, [Nullable(2)] string standalone);

		// Token: 0x06000CC9 RID: 3273
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDocumentType([Nullable(1)] string name, string publicId, string systemId, string internalSubset);

		// Token: 0x06000CCA RID: 3274
		IXmlNode CreateProcessingInstruction(string target, string data);

		// Token: 0x06000CCB RID: 3275
		IXmlElement CreateElement(string elementName);

		// Token: 0x06000CCC RID: 3276
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x06000CCD RID: 3277
		IXmlNode CreateAttribute(string name, string value);

		// Token: 0x06000CCE RID: 3278
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value);

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CCF RID: 3279
		[Nullable(2)]
		IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get;
		}
	}
}
