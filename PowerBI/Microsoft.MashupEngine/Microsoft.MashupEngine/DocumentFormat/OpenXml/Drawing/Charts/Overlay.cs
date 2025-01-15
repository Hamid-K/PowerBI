using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002514 RID: 9492
	[GeneratedCode("DomGen", "2.0")]
	internal class Overlay : BooleanType
	{
		// Token: 0x17005455 RID: 21589
		// (get) Token: 0x06011AA5 RID: 72357 RVA: 0x002F12F5 File Offset: 0x002EF4F5
		public override string LocalName
		{
			get
			{
				return "overlay";
			}
		}

		// Token: 0x17005456 RID: 21590
		// (get) Token: 0x06011AA6 RID: 72358 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005457 RID: 21591
		// (get) Token: 0x06011AA7 RID: 72359 RVA: 0x002F12FC File Offset: 0x002EF4FC
		internal override int ElementTypeId
		{
			get
			{
				return 10416;
			}
		}

		// Token: 0x06011AA8 RID: 72360 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AAA RID: 72362 RVA: 0x002F1303 File Offset: 0x002EF503
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Overlay>(deep);
		}

		// Token: 0x04007BD0 RID: 31696
		private const string tagName = "overlay";

		// Token: 0x04007BD1 RID: 31697
		private const byte tagNsId = 11;

		// Token: 0x04007BD2 RID: 31698
		internal const int ElementTypeIdConst = 10416;
	}
}
