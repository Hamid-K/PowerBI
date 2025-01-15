using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002607 RID: 9735
	[ChildElementInfo(typeof(BubbleSerExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BubbleSerExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170059E1 RID: 23009
		// (get) Token: 0x060126ED RID: 75501 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170059E2 RID: 23010
		// (get) Token: 0x060126EE RID: 75502 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059E3 RID: 23011
		// (get) Token: 0x060126EF RID: 75503 RVA: 0x002FB0A7 File Offset: 0x002F92A7
		internal override int ElementTypeId
		{
			get
			{
				return 10584;
			}
		}

		// Token: 0x060126F0 RID: 75504 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060126F1 RID: 75505 RVA: 0x00293ECF File Offset: 0x002920CF
		public BubbleSerExtensionList()
		{
		}

		// Token: 0x060126F2 RID: 75506 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BubbleSerExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126F3 RID: 75507 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BubbleSerExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126F4 RID: 75508 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BubbleSerExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060126F5 RID: 75509 RVA: 0x002FB0AE File Offset: 0x002F92AE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ext" == name)
			{
				return new BubbleSerExtension();
			}
			return null;
		}

		// Token: 0x060126F6 RID: 75510 RVA: 0x002FB0C9 File Offset: 0x002F92C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleSerExtensionList>(deep);
		}

		// Token: 0x04007F89 RID: 32649
		private const string tagName = "extLst";

		// Token: 0x04007F8A RID: 32650
		private const byte tagNsId = 11;

		// Token: 0x04007F8B RID: 32651
		internal const int ElementTypeIdConst = 10584;
	}
}
