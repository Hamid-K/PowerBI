using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FB RID: 251
	[NullableContext(2)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CF8 RID: 3320 RVA: 0x000338B5 File Offset: 0x00031AB5
		[NullableContext(1)]
		public XDocumentTypeWrapper(XDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000338C5 File Offset: 0x00031AC5
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000338D2 File Offset: 0x00031AD2
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000338DF File Offset: 0x00031ADF
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000338EC File Offset: 0x00031AEC
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000338F9 File Offset: 0x00031AF9
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x0400040E RID: 1038
		[Nullable(1)]
		private readonly XDocumentType _documentType;
	}
}
