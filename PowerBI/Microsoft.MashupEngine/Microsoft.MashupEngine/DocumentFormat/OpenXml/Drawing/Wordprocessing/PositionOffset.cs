using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AF RID: 10415
	[GeneratedCode("DomGen", "2.0")]
	internal class PositionOffset : OpenXmlLeafTextElement
	{
		// Token: 0x170068BF RID: 26815
		// (get) Token: 0x06014883 RID: 84099 RVA: 0x0031468C File Offset: 0x0031288C
		public override string LocalName
		{
			get
			{
				return "posOffset";
			}
		}

		// Token: 0x170068C0 RID: 26816
		// (get) Token: 0x06014884 RID: 84100 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170068C1 RID: 26817
		// (get) Token: 0x06014885 RID: 84101 RVA: 0x00314693 File Offset: 0x00312893
		internal override int ElementTypeId
		{
			get
			{
				return 10712;
			}
		}

		// Token: 0x06014886 RID: 84102 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014887 RID: 84103 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PositionOffset()
		{
		}

		// Token: 0x06014888 RID: 84104 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PositionOffset(string text)
			: base(text)
		{
		}

		// Token: 0x06014889 RID: 84105 RVA: 0x0031469C File Offset: 0x0031289C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x0601488A RID: 84106 RVA: 0x003146B7 File Offset: 0x003128B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PositionOffset>(deep);
		}

		// Token: 0x04008E83 RID: 36483
		private const string tagName = "posOffset";

		// Token: 0x04008E84 RID: 36484
		private const byte tagNsId = 16;

		// Token: 0x04008E85 RID: 36485
		internal const int ElementTypeIdConst = 10712;
	}
}
