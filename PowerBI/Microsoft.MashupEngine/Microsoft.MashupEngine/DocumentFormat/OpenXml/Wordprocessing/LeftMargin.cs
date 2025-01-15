using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE4 RID: 12004
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftMargin : TableWidthType
	{
		// Token: 0x17008D4D RID: 36173
		// (get) Token: 0x060199E7 RID: 104935 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x17008D4E RID: 36174
		// (get) Token: 0x060199E8 RID: 104936 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D4F RID: 36175
		// (get) Token: 0x060199E9 RID: 104937 RVA: 0x003534A0 File Offset: 0x003516A0
		internal override int ElementTypeId
		{
			get
			{
				return 12134;
			}
		}

		// Token: 0x060199EA RID: 104938 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199EC RID: 104940 RVA: 0x003534A7 File Offset: 0x003516A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftMargin>(deep);
		}

		// Token: 0x0400A9B4 RID: 43444
		private const string tagName = "left";

		// Token: 0x0400A9B5 RID: 43445
		private const byte tagNsId = 23;

		// Token: 0x0400A9B6 RID: 43446
		internal const int ElementTypeIdConst = 12134;
	}
}
