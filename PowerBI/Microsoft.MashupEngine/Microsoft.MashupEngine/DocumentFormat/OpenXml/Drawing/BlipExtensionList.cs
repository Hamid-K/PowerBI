using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002835 RID: 10293
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BlipExtension))]
	internal class BlipExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700661C RID: 26140
		// (get) Token: 0x060142C9 RID: 82633 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700661D RID: 26141
		// (get) Token: 0x060142CA RID: 82634 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700661E RID: 26142
		// (get) Token: 0x060142CB RID: 82635 RVA: 0x0030FF66 File Offset: 0x0030E166
		internal override int ElementTypeId
		{
			get
			{
				return 10329;
			}
		}

		// Token: 0x060142CC RID: 82636 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060142CD RID: 82637 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipExtensionList()
		{
		}

		// Token: 0x060142CE RID: 82638 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142CF RID: 82639 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142D0 RID: 82640 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060142D1 RID: 82641 RVA: 0x0030FF6D File Offset: 0x0030E16D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new BlipExtension();
			}
			return null;
		}

		// Token: 0x060142D2 RID: 82642 RVA: 0x0030FF88 File Offset: 0x0030E188
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipExtensionList>(deep);
		}

		// Token: 0x04008961 RID: 35169
		private const string tagName = "extLst";

		// Token: 0x04008962 RID: 35170
		private const byte tagNsId = 10;

		// Token: 0x04008963 RID: 35171
		internal const int ElementTypeIdConst = 10329;
	}
}
