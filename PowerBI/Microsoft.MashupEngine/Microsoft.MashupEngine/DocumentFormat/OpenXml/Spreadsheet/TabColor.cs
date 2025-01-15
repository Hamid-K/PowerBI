using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA4 RID: 11172
	[GeneratedCode("DomGen", "2.0")]
	internal class TabColor : ColorType
	{
		// Token: 0x17007B55 RID: 31573
		// (get) Token: 0x060172A9 RID: 94889 RVA: 0x0033367F File Offset: 0x0033187F
		public override string LocalName
		{
			get
			{
				return "tabColor";
			}
		}

		// Token: 0x17007B56 RID: 31574
		// (get) Token: 0x060172AA RID: 94890 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B57 RID: 31575
		// (get) Token: 0x060172AB RID: 94891 RVA: 0x00333686 File Offset: 0x00331886
		internal override int ElementTypeId
		{
			get
			{
				return 11186;
			}
		}

		// Token: 0x060172AC RID: 94892 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172AE RID: 94894 RVA: 0x0033368D File Offset: 0x0033188D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabColor>(deep);
		}

		// Token: 0x04009B60 RID: 39776
		private const string tagName = "tabColor";

		// Token: 0x04009B61 RID: 39777
		private const byte tagNsId = 22;

		// Token: 0x04009B62 RID: 39778
		internal const int ElementTypeIdConst = 11186;
	}
}
