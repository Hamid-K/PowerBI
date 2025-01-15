using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F2 RID: 242
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000334F6 File Offset: 0x000316F6
		[NullableContext(1)]
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00033506 File Offset: 0x00031706
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00033513 File Offset: 0x00031713
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00033520 File Offset: 0x00031720
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

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0003352E File Offset: 0x0003172E
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0003353B File Offset: 0x0003173B
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

		// Token: 0x04000408 RID: 1032
		[Nullable(1)]
		private readonly XmlDeclaration _declaration;
	}
}
