using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F88 RID: 12168
	[GeneratedCode("DomGen", "2.0")]
	internal class MarginWidth : PixelsMeasureType
	{
		// Token: 0x170091AD RID: 37293
		// (get) Token: 0x0601A394 RID: 107412 RVA: 0x0035F43C File Offset: 0x0035D63C
		public override string LocalName
		{
			get
			{
				return "marW";
			}
		}

		// Token: 0x170091AE RID: 37294
		// (get) Token: 0x0601A395 RID: 107413 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091AF RID: 37295
		// (get) Token: 0x0601A396 RID: 107414 RVA: 0x0035F443 File Offset: 0x0035D643
		internal override int ElementTypeId
		{
			get
			{
				return 11851;
			}
		}

		// Token: 0x0601A397 RID: 107415 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A399 RID: 107417 RVA: 0x0035F452 File Offset: 0x0035D652
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MarginWidth>(deep);
		}

		// Token: 0x0400AC46 RID: 44102
		private const string tagName = "marW";

		// Token: 0x0400AC47 RID: 44103
		private const byte tagNsId = 23;

		// Token: 0x0400AC48 RID: 44104
		internal const int ElementTypeIdConst = 11851;
	}
}
