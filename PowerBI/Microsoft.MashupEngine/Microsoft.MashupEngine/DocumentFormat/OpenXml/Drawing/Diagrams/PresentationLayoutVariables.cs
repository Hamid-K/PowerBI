using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002670 RID: 9840
	[GeneratedCode("DomGen", "2.0")]
	internal class PresentationLayoutVariables : LayoutVariablePropertySetType
	{
		// Token: 0x17005C2F RID: 23599
		// (get) Token: 0x06012C3F RID: 76863 RVA: 0x002FF024 File Offset: 0x002FD224
		public override string LocalName
		{
			get
			{
				return "presLayoutVars";
			}
		}

		// Token: 0x17005C30 RID: 23600
		// (get) Token: 0x06012C40 RID: 76864 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C31 RID: 23601
		// (get) Token: 0x06012C41 RID: 76865 RVA: 0x002FF02B File Offset: 0x002FD22B
		internal override int ElementTypeId
		{
			get
			{
				return 10669;
			}
		}

		// Token: 0x06012C42 RID: 76866 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012C43 RID: 76867 RVA: 0x002FEFF8 File Offset: 0x002FD1F8
		public PresentationLayoutVariables()
		{
		}

		// Token: 0x06012C44 RID: 76868 RVA: 0x002FF000 File Offset: 0x002FD200
		public PresentationLayoutVariables(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C45 RID: 76869 RVA: 0x002FF009 File Offset: 0x002FD209
		public PresentationLayoutVariables(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C46 RID: 76870 RVA: 0x002FF012 File Offset: 0x002FD212
		public PresentationLayoutVariables(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C47 RID: 76871 RVA: 0x002FF032 File Offset: 0x002FD232
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationLayoutVariables>(deep);
		}

		// Token: 0x04008181 RID: 33153
		private const string tagName = "presLayoutVars";

		// Token: 0x04008182 RID: 33154
		private const byte tagNsId = 14;

		// Token: 0x04008183 RID: 33155
		internal const int ElementTypeIdConst = 10669;
	}
}
