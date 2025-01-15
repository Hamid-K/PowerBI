using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CD RID: 9421
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtrusionColor : ColorType
	{
		// Token: 0x170052FD RID: 21245
		// (get) Token: 0x060117A8 RID: 71592 RVA: 0x002EEDCD File Offset: 0x002ECFCD
		public override string LocalName
		{
			get
			{
				return "extrusionClr";
			}
		}

		// Token: 0x170052FE RID: 21246
		// (get) Token: 0x060117A9 RID: 71593 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052FF RID: 21247
		// (get) Token: 0x060117AA RID: 71594 RVA: 0x002EEDD4 File Offset: 0x002ECFD4
		internal override int ElementTypeId
		{
			get
			{
				return 12891;
			}
		}

		// Token: 0x060117AB RID: 71595 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117AC RID: 71596 RVA: 0x002EEDDB File Offset: 0x002ECFDB
		public ExtrusionColor()
		{
		}

		// Token: 0x060117AD RID: 71597 RVA: 0x002EEDE3 File Offset: 0x002ECFE3
		public ExtrusionColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117AE RID: 71598 RVA: 0x002EEDEC File Offset: 0x002ECFEC
		public ExtrusionColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117AF RID: 71599 RVA: 0x002EEDF5 File Offset: 0x002ECFF5
		public ExtrusionColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060117B0 RID: 71600 RVA: 0x002EEDFE File Offset: 0x002ECFFE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtrusionColor>(deep);
		}

		// Token: 0x04007A05 RID: 31237
		private const string tagName = "extrusionClr";

		// Token: 0x04007A06 RID: 31238
		private const byte tagNsId = 52;

		// Token: 0x04007A07 RID: 31239
		internal const int ElementTypeIdConst = 12891;
	}
}
