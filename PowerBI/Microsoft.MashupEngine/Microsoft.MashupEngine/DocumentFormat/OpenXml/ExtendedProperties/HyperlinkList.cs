using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200294C RID: 10572
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkList : VectorVariantType
	{
		// Token: 0x17006B60 RID: 27488
		// (get) Token: 0x06014F16 RID: 85782 RVA: 0x00318EE1 File Offset: 0x003170E1
		public override string LocalName
		{
			get
			{
				return "HLinks";
			}
		}

		// Token: 0x17006B61 RID: 27489
		// (get) Token: 0x06014F17 RID: 85783 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B62 RID: 27490
		// (get) Token: 0x06014F18 RID: 85784 RVA: 0x00318EE8 File Offset: 0x003170E8
		internal override int ElementTypeId
		{
			get
			{
				return 11020;
			}
		}

		// Token: 0x06014F19 RID: 85785 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F1A RID: 85786 RVA: 0x00318EB5 File Offset: 0x003170B5
		public HyperlinkList()
		{
		}

		// Token: 0x06014F1B RID: 85787 RVA: 0x00318EBD File Offset: 0x003170BD
		public HyperlinkList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F1C RID: 85788 RVA: 0x00318EC6 File Offset: 0x003170C6
		public HyperlinkList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F1D RID: 85789 RVA: 0x00318ECF File Offset: 0x003170CF
		public HyperlinkList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F1E RID: 85790 RVA: 0x00318EEF File Offset: 0x003170EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkList>(deep);
		}

		// Token: 0x040090BD RID: 37053
		private const string tagName = "HLinks";

		// Token: 0x040090BE RID: 37054
		private const byte tagNsId = 3;

		// Token: 0x040090BF RID: 37055
		internal const int ElementTypeIdConst = 11020;
	}
}
