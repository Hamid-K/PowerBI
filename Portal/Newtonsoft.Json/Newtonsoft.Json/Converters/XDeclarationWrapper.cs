using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FA RID: 250
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00033856 File Offset: 0x00031A56
		[Nullable(1)]
		internal XDeclaration Declaration
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003385E File Offset: 0x00031A5E
		[NullableContext(1)]
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0003386E File Offset: 0x00031A6E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00033872 File Offset: 0x00031A72
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003387F File Offset: 0x00031A7F
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0003388C File Offset: 0x00031A8C
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

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003389A File Offset: 0x00031A9A
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x000338A7 File Offset: 0x00031AA7
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
