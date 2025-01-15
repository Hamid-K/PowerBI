using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD8 RID: 12248
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocPartProperties))]
	[ChildElementInfo(typeof(DocPartBody))]
	internal class DocPart : OpenXmlCompositeElement
	{
		// Token: 0x17009470 RID: 38000
		// (get) Token: 0x0601A979 RID: 108921 RVA: 0x003447D0 File Offset: 0x003429D0
		public override string LocalName
		{
			get
			{
				return "docPart";
			}
		}

		// Token: 0x17009471 RID: 38001
		// (get) Token: 0x0601A97A RID: 108922 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009472 RID: 38002
		// (get) Token: 0x0601A97B RID: 108923 RVA: 0x00364998 File Offset: 0x00362B98
		internal override int ElementTypeId
		{
			get
			{
				return 11957;
			}
		}

		// Token: 0x0601A97C RID: 108924 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A97D RID: 108925 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocPart()
		{
		}

		// Token: 0x0601A97E RID: 108926 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A97F RID: 108927 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A980 RID: 108928 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A981 RID: 108929 RVA: 0x0036499F File Offset: 0x00362B9F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docPartPr" == name)
			{
				return new DocPartProperties();
			}
			if (23 == namespaceId && "docPartBody" == name)
			{
				return new DocPartBody();
			}
			return null;
		}

		// Token: 0x17009473 RID: 38003
		// (get) Token: 0x0601A982 RID: 108930 RVA: 0x003649D2 File Offset: 0x00362BD2
		internal override string[] ElementTagNames
		{
			get
			{
				return DocPart.eleTagNames;
			}
		}

		// Token: 0x17009474 RID: 38004
		// (get) Token: 0x0601A983 RID: 108931 RVA: 0x003649D9 File Offset: 0x00362BD9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocPart.eleNamespaceIds;
			}
		}

		// Token: 0x17009475 RID: 38005
		// (get) Token: 0x0601A984 RID: 108932 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009476 RID: 38006
		// (get) Token: 0x0601A985 RID: 108933 RVA: 0x003649E0 File Offset: 0x00362BE0
		// (set) Token: 0x0601A986 RID: 108934 RVA: 0x003649E9 File Offset: 0x00362BE9
		public DocPartProperties DocPartProperties
		{
			get
			{
				return base.GetElement<DocPartProperties>(0);
			}
			set
			{
				base.SetElement<DocPartProperties>(0, value);
			}
		}

		// Token: 0x17009477 RID: 38007
		// (get) Token: 0x0601A987 RID: 108935 RVA: 0x003649F3 File Offset: 0x00362BF3
		// (set) Token: 0x0601A988 RID: 108936 RVA: 0x003649FC File Offset: 0x00362BFC
		public DocPartBody DocPartBody
		{
			get
			{
				return base.GetElement<DocPartBody>(1);
			}
			set
			{
				base.SetElement<DocPartBody>(1, value);
			}
		}

		// Token: 0x0601A989 RID: 108937 RVA: 0x00364A06 File Offset: 0x00362C06
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPart>(deep);
		}

		// Token: 0x0400ADA7 RID: 44455
		private const string tagName = "docPart";

		// Token: 0x0400ADA8 RID: 44456
		private const byte tagNsId = 23;

		// Token: 0x0400ADA9 RID: 44457
		internal const int ElementTypeIdConst = 11957;

		// Token: 0x0400ADAA RID: 44458
		private static readonly string[] eleTagNames = new string[] { "docPartPr", "docPartBody" };

		// Token: 0x0400ADAB RID: 44459
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
