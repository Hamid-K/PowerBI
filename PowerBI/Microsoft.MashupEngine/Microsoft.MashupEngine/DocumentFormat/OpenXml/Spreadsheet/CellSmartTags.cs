using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD5 RID: 11221
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CellSmartTag))]
	internal class CellSmartTags : OpenXmlCompositeElement
	{
		// Token: 0x17007D4C RID: 32076
		// (get) Token: 0x060176D1 RID: 95953 RVA: 0x00336A88 File Offset: 0x00334C88
		public override string LocalName
		{
			get
			{
				return "cellSmartTags";
			}
		}

		// Token: 0x17007D4D RID: 32077
		// (get) Token: 0x060176D2 RID: 95954 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D4E RID: 32078
		// (get) Token: 0x060176D3 RID: 95955 RVA: 0x00336A8F File Offset: 0x00334C8F
		internal override int ElementTypeId
		{
			get
			{
				return 11194;
			}
		}

		// Token: 0x060176D4 RID: 95956 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D4F RID: 32079
		// (get) Token: 0x060176D5 RID: 95957 RVA: 0x00336A96 File Offset: 0x00334C96
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellSmartTags.attributeTagNames;
			}
		}

		// Token: 0x17007D50 RID: 32080
		// (get) Token: 0x060176D6 RID: 95958 RVA: 0x00336A9D File Offset: 0x00334C9D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellSmartTags.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D51 RID: 32081
		// (get) Token: 0x060176D7 RID: 95959 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060176D8 RID: 95960 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060176D9 RID: 95961 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellSmartTags()
		{
		}

		// Token: 0x060176DA RID: 95962 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellSmartTags(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176DB RID: 95963 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellSmartTags(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176DC RID: 95964 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellSmartTags(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060176DD RID: 95965 RVA: 0x00336AA4 File Offset: 0x00334CA4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cellSmartTag" == name)
			{
				return new CellSmartTag();
			}
			return null;
		}

		// Token: 0x17007D52 RID: 32082
		// (get) Token: 0x060176DE RID: 95966 RVA: 0x00336ABF File Offset: 0x00334CBF
		internal override string[] ElementTagNames
		{
			get
			{
				return CellSmartTags.eleTagNames;
			}
		}

		// Token: 0x17007D53 RID: 32083
		// (get) Token: 0x060176DF RID: 95967 RVA: 0x00336AC6 File Offset: 0x00334CC6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CellSmartTags.eleNamespaceIds;
			}
		}

		// Token: 0x17007D54 RID: 32084
		// (get) Token: 0x060176E0 RID: 95968 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007D55 RID: 32085
		// (get) Token: 0x060176E1 RID: 95969 RVA: 0x00336ACD File Offset: 0x00334CCD
		// (set) Token: 0x060176E2 RID: 95970 RVA: 0x00336AD6 File Offset: 0x00334CD6
		public CellSmartTag CellSmartTag
		{
			get
			{
				return base.GetElement<CellSmartTag>(0);
			}
			set
			{
				base.SetElement<CellSmartTag>(0, value);
			}
		}

		// Token: 0x060176E3 RID: 95971 RVA: 0x00336AE0 File Offset: 0x00334CE0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176E4 RID: 95972 RVA: 0x00336B00 File Offset: 0x00334D00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellSmartTags>(deep);
		}

		// Token: 0x060176E5 RID: 95973 RVA: 0x00336B0C File Offset: 0x00334D0C
		// Note: this type is marked as 'beforefieldinit'.
		static CellSmartTags()
		{
			byte[] array = new byte[1];
			CellSmartTags.attributeNamespaceIds = array;
			CellSmartTags.eleTagNames = new string[] { "cellSmartTag" };
			CellSmartTags.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009C4A RID: 40010
		private const string tagName = "cellSmartTags";

		// Token: 0x04009C4B RID: 40011
		private const byte tagNsId = 22;

		// Token: 0x04009C4C RID: 40012
		internal const int ElementTypeIdConst = 11194;

		// Token: 0x04009C4D RID: 40013
		private static string[] attributeTagNames = new string[] { "r" };

		// Token: 0x04009C4E RID: 40014
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009C4F RID: 40015
		private static readonly string[] eleTagNames;

		// Token: 0x04009C50 RID: 40016
		private static readonly byte[] eleNamespaceIds;
	}
}
