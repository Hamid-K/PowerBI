using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F2 RID: 242
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x00032C3D File Offset: 0x00030E3D
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00032C4D File Offset: 0x00030E4D
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00032C5A File Offset: 0x00030E5A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00032C67 File Offset: 0x00030E67
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00032C74 File Offset: 0x00030E74
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00032C81 File Offset: 0x00030E81
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003EB RID: 1003
		private readonly XmlDocumentType _documentType;
	}
}
