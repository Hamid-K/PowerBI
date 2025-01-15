using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000FA RID: 250
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000336FE File Offset: 0x000318FE
		[Nullable(1)]
		internal XDeclaration Declaration
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00033706 File Offset: 0x00031906
		[NullableContext(1)]
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00033716 File Offset: 0x00031916
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003371A File Offset: 0x0003191A
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00033727 File Offset: 0x00031927
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00033734 File Offset: 0x00031934
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

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00033742 File Offset: 0x00031942
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0003374F File Offset: 0x0003194F
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
