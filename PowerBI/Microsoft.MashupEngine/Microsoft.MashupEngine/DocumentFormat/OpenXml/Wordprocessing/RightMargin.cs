using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE5 RID: 12005
	[GeneratedCode("DomGen", "2.0")]
	internal class RightMargin : TableWidthType
	{
		// Token: 0x17008D50 RID: 36176
		// (get) Token: 0x060199ED RID: 104941 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x17008D51 RID: 36177
		// (get) Token: 0x060199EE RID: 104942 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D52 RID: 36178
		// (get) Token: 0x060199EF RID: 104943 RVA: 0x003534B0 File Offset: 0x003516B0
		internal override int ElementTypeId
		{
			get
			{
				return 12135;
			}
		}

		// Token: 0x060199F0 RID: 104944 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199F2 RID: 104946 RVA: 0x003534B7 File Offset: 0x003516B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightMargin>(deep);
		}

		// Token: 0x0400A9B7 RID: 43447
		private const string tagName = "right";

		// Token: 0x0400A9B8 RID: 43448
		private const byte tagNsId = 23;

		// Token: 0x0400A9B9 RID: 43449
		internal const int ElementTypeIdConst = 12135;
	}
}
