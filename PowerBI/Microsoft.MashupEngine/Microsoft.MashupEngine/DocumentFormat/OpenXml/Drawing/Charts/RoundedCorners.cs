using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002531 RID: 9521
	[GeneratedCode("DomGen", "2.0")]
	internal class RoundedCorners : BooleanType
	{
		// Token: 0x170054AC RID: 21676
		// (get) Token: 0x06011B53 RID: 72531 RVA: 0x002F1582 File Offset: 0x002EF782
		public override string LocalName
		{
			get
			{
				return "roundedCorners";
			}
		}

		// Token: 0x170054AD RID: 21677
		// (get) Token: 0x06011B54 RID: 72532 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054AE RID: 21678
		// (get) Token: 0x06011B55 RID: 72533 RVA: 0x002F1589 File Offset: 0x002EF789
		internal override int ElementTypeId
		{
			get
			{
				return 10573;
			}
		}

		// Token: 0x06011B56 RID: 72534 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B58 RID: 72536 RVA: 0x002F1590 File Offset: 0x002EF790
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RoundedCorners>(deep);
		}

		// Token: 0x04007C27 RID: 31783
		private const string tagName = "roundedCorners";

		// Token: 0x04007C28 RID: 31784
		private const byte tagNsId = 11;

		// Token: 0x04007C29 RID: 31785
		internal const int ElementTypeIdConst = 10573;
	}
}
