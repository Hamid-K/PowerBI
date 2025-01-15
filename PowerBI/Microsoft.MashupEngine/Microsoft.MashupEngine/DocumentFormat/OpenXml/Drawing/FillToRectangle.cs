using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BE RID: 10174
	[GeneratedCode("DomGen", "2.0")]
	internal class FillToRectangle : RelativeRectangleType
	{
		// Token: 0x1700635A RID: 25434
		// (get) Token: 0x06013C21 RID: 80929 RVA: 0x002EE8E4 File Offset: 0x002ECAE4
		public override string LocalName
		{
			get
			{
				return "fillToRect";
			}
		}

		// Token: 0x1700635B RID: 25435
		// (get) Token: 0x06013C22 RID: 80930 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700635C RID: 25436
		// (get) Token: 0x06013C23 RID: 80931 RVA: 0x0030B73F File Offset: 0x0030993F
		internal override int ElementTypeId
		{
			get
			{
				return 10207;
			}
		}

		// Token: 0x06013C24 RID: 80932 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C26 RID: 80934 RVA: 0x0030B74E File Offset: 0x0030994E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillToRectangle>(deep);
		}

		// Token: 0x040087A1 RID: 34721
		private const string tagName = "fillToRect";

		// Token: 0x040087A2 RID: 34722
		private const byte tagNsId = 10;

		// Token: 0x040087A3 RID: 34723
		internal const int ElementTypeIdConst = 10207;
	}
}
