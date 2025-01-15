using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002834 RID: 10292
	[ChildElementInfo(typeof(ShapePropertiesExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapePropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17006619 RID: 26137
		// (get) Token: 0x060142BF RID: 82623 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700661A RID: 26138
		// (get) Token: 0x060142C0 RID: 82624 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700661B RID: 26139
		// (get) Token: 0x060142C1 RID: 82625 RVA: 0x0030FF3B File Offset: 0x0030E13B
		internal override int ElementTypeId
		{
			get
			{
				return 10328;
			}
		}

		// Token: 0x060142C2 RID: 82626 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060142C3 RID: 82627 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapePropertiesExtensionList()
		{
		}

		// Token: 0x060142C4 RID: 82628 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapePropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142C5 RID: 82629 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapePropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142C6 RID: 82630 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapePropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060142C7 RID: 82631 RVA: 0x0030FF42 File Offset: 0x0030E142
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new ShapePropertiesExtension();
			}
			return null;
		}

		// Token: 0x060142C8 RID: 82632 RVA: 0x0030FF5D File Offset: 0x0030E15D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapePropertiesExtensionList>(deep);
		}

		// Token: 0x0400895E RID: 35166
		private const string tagName = "extLst";

		// Token: 0x0400895F RID: 35167
		private const byte tagNsId = 10;

		// Token: 0x04008960 RID: 35168
		internal const int ElementTypeIdConst = 10328;
	}
}
