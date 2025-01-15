using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABD RID: 10941
	[GeneratedCode("DomGen", "2.0")]
	internal class FromPosition : TimeListType
	{
		// Token: 0x1700750E RID: 29966
		// (get) Token: 0x060164A0 RID: 91296 RVA: 0x002FCA49 File Offset: 0x002FAC49
		public override string LocalName
		{
			get
			{
				return "from";
			}
		}

		// Token: 0x1700750F RID: 29967
		// (get) Token: 0x060164A1 RID: 91297 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007510 RID: 29968
		// (get) Token: 0x060164A2 RID: 91298 RVA: 0x003289E3 File Offset: 0x00326BE3
		internal override int ElementTypeId
		{
			get
			{
				return 12356;
			}
		}

		// Token: 0x060164A3 RID: 91299 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164A5 RID: 91301 RVA: 0x003289EA File Offset: 0x00326BEA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FromPosition>(deep);
		}

		// Token: 0x04009708 RID: 38664
		private const string tagName = "from";

		// Token: 0x04009709 RID: 38665
		private const byte tagNsId = 24;

		// Token: 0x0400970A RID: 38666
		internal const int ElementTypeIdConst = 12356;
	}
}
