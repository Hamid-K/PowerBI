using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B1 RID: 9393
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DiscardImageEditingData : OnOffType
	{
		// Token: 0x17005251 RID: 21073
		// (get) Token: 0x0601162B RID: 71211 RVA: 0x002EDF7E File Offset: 0x002EC17E
		public override string LocalName
		{
			get
			{
				return "discardImageEditingData";
			}
		}

		// Token: 0x17005252 RID: 21074
		// (get) Token: 0x0601162C RID: 71212 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005253 RID: 21075
		// (get) Token: 0x0601162D RID: 71213 RVA: 0x002EDF85 File Offset: 0x002EC185
		internal override int ElementTypeId
		{
			get
			{
				return 12872;
			}
		}

		// Token: 0x0601162E RID: 71214 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011630 RID: 71216 RVA: 0x002EDF8C File Offset: 0x002EC18C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiscardImageEditingData>(deep);
		}

		// Token: 0x04007994 RID: 31124
		private const string tagName = "discardImageEditingData";

		// Token: 0x04007995 RID: 31125
		private const byte tagNsId = 52;

		// Token: 0x04007996 RID: 31126
		internal const int ElementTypeIdConst = 12872;
	}
}
