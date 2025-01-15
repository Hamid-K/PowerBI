using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F2 RID: 242
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003339E File Offset: 0x0003159E
		[NullableContext(1)]
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000333AE File Offset: 0x000315AE
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000333BB File Offset: 0x000315BB
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x000333C8 File Offset: 0x000315C8
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000333D6 File Offset: 0x000315D6
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x000333E3 File Offset: 0x000315E3
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

		// Token: 0x04000407 RID: 1031
		[Nullable(1)]
		private readonly XmlDeclaration _declaration;
	}
}
