using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A5 RID: 9637
	[GeneratedCode("DomGen", "2.0")]
	internal class GapWidth : GapAmountType
	{
		// Token: 0x170056F3 RID: 22259
		// (get) Token: 0x06012080 RID: 73856 RVA: 0x002F4E7F File Offset: 0x002F307F
		public override string LocalName
		{
			get
			{
				return "gapWidth";
			}
		}

		// Token: 0x170056F4 RID: 22260
		// (get) Token: 0x06012081 RID: 73857 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056F5 RID: 22261
		// (get) Token: 0x06012082 RID: 73858 RVA: 0x002F4E86 File Offset: 0x002F3086
		internal override int ElementTypeId
		{
			get
			{
				return 10453;
			}
		}

		// Token: 0x06012083 RID: 73859 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012085 RID: 73861 RVA: 0x002F4E95 File Offset: 0x002F3095
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GapWidth>(deep);
		}

		// Token: 0x04007DD3 RID: 32211
		private const string tagName = "gapWidth";

		// Token: 0x04007DD4 RID: 32212
		private const byte tagNsId = 11;

		// Token: 0x04007DD5 RID: 32213
		internal const int ElementTypeIdConst = 10453;
	}
}
