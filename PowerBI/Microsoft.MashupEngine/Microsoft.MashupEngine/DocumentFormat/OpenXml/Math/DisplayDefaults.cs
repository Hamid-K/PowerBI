using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002985 RID: 10629
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayDefaults : OnOffType
	{
		// Token: 0x17006C9A RID: 27802
		// (get) Token: 0x060151C2 RID: 86466 RVA: 0x0031B658 File Offset: 0x00319858
		public override string LocalName
		{
			get
			{
				return "dispDef";
			}
		}

		// Token: 0x17006C9B RID: 27803
		// (get) Token: 0x060151C3 RID: 86467 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C9C RID: 27804
		// (get) Token: 0x060151C4 RID: 86468 RVA: 0x0031B65F File Offset: 0x0031985F
		internal override int ElementTypeId
		{
			get
			{
				return 10950;
			}
		}

		// Token: 0x060151C5 RID: 86469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151C7 RID: 86471 RVA: 0x0031B666 File Offset: 0x00319866
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayDefaults>(deep);
		}

		// Token: 0x0400919B RID: 37275
		private const string tagName = "dispDef";

		// Token: 0x0400919C RID: 37276
		private const byte tagNsId = 21;

		// Token: 0x0400919D RID: 37277
		internal const int ElementTypeIdConst = 10950;
	}
}
