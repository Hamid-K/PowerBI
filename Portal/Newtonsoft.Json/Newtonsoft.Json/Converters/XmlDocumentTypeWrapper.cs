using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F3 RID: 243
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00033549 File Offset: 0x00031749
		[NullableContext(1)]
		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00033559 File Offset: 0x00031759
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00033566 File Offset: 0x00031766
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00033573 File Offset: 0x00031773
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00033580 File Offset: 0x00031780
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0003358D File Offset: 0x0003178D
		public override string LocalName
		{
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000409 RID: 1033
		[Nullable(1)]
		private readonly XmlDocumentType _documentType;
	}
}
