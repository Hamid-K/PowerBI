using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D3 RID: 9171
	[ChildElementInfo(typeof(ConditionalFormatting), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormattings : OpenXmlCompositeElement
	{
		// Token: 0x17004D20 RID: 19744
		// (get) Token: 0x06010A67 RID: 68199 RVA: 0x002E5ABB File Offset: 0x002E3CBB
		public override string LocalName
		{
			get
			{
				return "conditionalFormattings";
			}
		}

		// Token: 0x17004D21 RID: 19745
		// (get) Token: 0x06010A68 RID: 68200 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D22 RID: 19746
		// (get) Token: 0x06010A69 RID: 68201 RVA: 0x002E5AC6 File Offset: 0x002E3CC6
		internal override int ElementTypeId
		{
			get
			{
				return 12897;
			}
		}

		// Token: 0x06010A6A RID: 68202 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A6B RID: 68203 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattings()
		{
		}

		// Token: 0x06010A6C RID: 68204 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattings(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A6D RID: 68205 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattings(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A6E RID: 68206 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattings(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010A6F RID: 68207 RVA: 0x002E5ACD File Offset: 0x002E3CCD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "conditionalFormatting" == name)
			{
				return new ConditionalFormatting();
			}
			return null;
		}

		// Token: 0x06010A70 RID: 68208 RVA: 0x002E5AE8 File Offset: 0x002E3CE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattings>(deep);
		}

		// Token: 0x040075C0 RID: 30144
		private const string tagName = "conditionalFormattings";

		// Token: 0x040075C1 RID: 30145
		private const byte tagNsId = 53;

		// Token: 0x040075C2 RID: 30146
		internal const int ElementTypeIdConst = 12897;
	}
}
