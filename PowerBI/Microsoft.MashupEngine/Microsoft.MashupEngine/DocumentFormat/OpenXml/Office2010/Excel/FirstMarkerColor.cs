using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002401 RID: 9217
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class FirstMarkerColor : ColorType
	{
		// Token: 0x17004EAB RID: 20139
		// (get) Token: 0x06010DDF RID: 69087 RVA: 0x002E8423 File Offset: 0x002E6623
		public override string LocalName
		{
			get
			{
				return "colorFirst";
			}
		}

		// Token: 0x17004EAC RID: 20140
		// (get) Token: 0x06010DE0 RID: 69088 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EAD RID: 20141
		// (get) Token: 0x06010DE1 RID: 69089 RVA: 0x002E842A File Offset: 0x002E662A
		internal override int ElementTypeId
		{
			get
			{
				return 12941;
			}
		}

		// Token: 0x06010DE2 RID: 69090 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DE4 RID: 69092 RVA: 0x002E8431 File Offset: 0x002E6631
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstMarkerColor>(deep);
		}

		// Token: 0x04007692 RID: 30354
		private const string tagName = "colorFirst";

		// Token: 0x04007693 RID: 30355
		private const byte tagNsId = 53;

		// Token: 0x04007694 RID: 30356
		internal const int ElementTypeIdConst = 12941;
	}
}
