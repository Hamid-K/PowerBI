using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025A6 RID: 9638
	[GeneratedCode("DomGen", "2.0")]
	internal class GapDepth : GapAmountType
	{
		// Token: 0x170056F6 RID: 22262
		// (get) Token: 0x06012086 RID: 73862 RVA: 0x002F4E9E File Offset: 0x002F309E
		public override string LocalName
		{
			get
			{
				return "gapDepth";
			}
		}

		// Token: 0x170056F7 RID: 22263
		// (get) Token: 0x06012087 RID: 73863 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056F8 RID: 22264
		// (get) Token: 0x06012088 RID: 73864 RVA: 0x002F4EA5 File Offset: 0x002F30A5
		internal override int ElementTypeId
		{
			get
			{
				return 10460;
			}
		}

		// Token: 0x06012089 RID: 73865 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601208B RID: 73867 RVA: 0x002F4EAC File Offset: 0x002F30AC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GapDepth>(deep);
		}

		// Token: 0x04007DD6 RID: 32214
		private const string tagName = "gapDepth";

		// Token: 0x04007DD7 RID: 32215
		private const byte tagNsId = 11;

		// Token: 0x04007DD8 RID: 32216
		internal const int ElementTypeIdConst = 10460;
	}
}
