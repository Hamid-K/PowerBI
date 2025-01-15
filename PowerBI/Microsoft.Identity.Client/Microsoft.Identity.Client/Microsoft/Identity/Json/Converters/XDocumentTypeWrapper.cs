using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FA RID: 250
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x00032FA9 File Offset: 0x000311A9
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00032FB9 File Offset: 0x000311B9
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00032FC6 File Offset: 0x000311C6
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00032FD3 File Offset: 0x000311D3
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00032FE0 File Offset: 0x000311E0
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00032FED File Offset: 0x000311ED
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040003F0 RID: 1008
		private readonly XDocumentType _documentType;
	}
}
