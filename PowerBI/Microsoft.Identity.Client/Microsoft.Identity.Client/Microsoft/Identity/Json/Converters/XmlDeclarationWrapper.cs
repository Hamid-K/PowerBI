using System;
using System.Xml;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F1 RID: 241
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x00032BEA File Offset: 0x00030DEA
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00032BFA File Offset: 0x00030DFA
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00032C07 File Offset: 0x00030E07
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x00032C14 File Offset: 0x00030E14
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00032C22 File Offset: 0x00030E22
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00032C2F File Offset: 0x00030E2F
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x040003EA RID: 1002
		private readonly XmlDeclaration _declaration;
	}
}
