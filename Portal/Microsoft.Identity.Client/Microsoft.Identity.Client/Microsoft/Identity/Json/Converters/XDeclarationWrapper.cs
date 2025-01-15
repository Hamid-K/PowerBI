using System;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F9 RID: 249
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00032F4A File Offset: 0x0003114A
		internal XDeclaration Declaration { get; }

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00032F52 File Offset: 0x00031152
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00032F62 File Offset: 0x00031162
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00032F66 File Offset: 0x00031166
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00032F73 File Offset: 0x00031173
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00032F80 File Offset: 0x00031180
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00032F8E File Offset: 0x0003118E
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00032F9B File Offset: 0x0003119B
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
