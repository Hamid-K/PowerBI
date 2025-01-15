using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x000333F1 File Offset: 0x000315F1
		[NullableContext(1)]
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00033401 File Offset: 0x00031601
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003340E File Offset: 0x0003160E
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0003341B File Offset: 0x0003161B
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00033428 File Offset: 0x00031628
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00033435 File Offset: 0x00031635
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000408 RID: 1032
		[Nullable(1)]
		private readonly XmlDocumentType _documentType;
	}
}
