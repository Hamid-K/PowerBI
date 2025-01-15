using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000FB RID: 251
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x0003375D File Offset: 0x0003195D
		[NullableContext(1)]
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0003376D File Offset: 0x0003196D
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0003377A File Offset: 0x0003197A
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00033787 File Offset: 0x00031987
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00033794 File Offset: 0x00031994
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000337A1 File Offset: 0x000319A1
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x0400040D RID: 1037
		[Nullable(1)]
		private readonly XDocumentType _documentType;
	}
}
