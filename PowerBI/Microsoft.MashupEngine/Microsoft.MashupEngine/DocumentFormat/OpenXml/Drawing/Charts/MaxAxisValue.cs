using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002566 RID: 9574
	[GeneratedCode("DomGen", "2.0")]
	internal class MaxAxisValue : DoubleType
	{
		// Token: 0x170055AE RID: 21934
		// (get) Token: 0x06011D91 RID: 73105 RVA: 0x0014965A File Offset: 0x0014785A
		public override string LocalName
		{
			get
			{
				return "max";
			}
		}

		// Token: 0x170055AF RID: 21935
		// (get) Token: 0x06011D92 RID: 73106 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055B0 RID: 21936
		// (get) Token: 0x06011D93 RID: 73107 RVA: 0x002F2FCD File Offset: 0x002F11CD
		internal override int ElementTypeId
		{
			get
			{
				return 10479;
			}
		}

		// Token: 0x06011D94 RID: 73108 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D96 RID: 73110 RVA: 0x002F2FD4 File Offset: 0x002F11D4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MaxAxisValue>(deep);
		}

		// Token: 0x04007CE2 RID: 31970
		private const string tagName = "max";

		// Token: 0x04007CE3 RID: 31971
		private const byte tagNsId = 11;

		// Token: 0x04007CE4 RID: 31972
		internal const int ElementTypeIdConst = 10479;
	}
}
