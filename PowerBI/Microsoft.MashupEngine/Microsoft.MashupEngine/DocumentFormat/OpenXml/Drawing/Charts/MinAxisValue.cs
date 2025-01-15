using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002567 RID: 9575
	[GeneratedCode("DomGen", "2.0")]
	internal class MinAxisValue : DoubleType
	{
		// Token: 0x170055B1 RID: 21937
		// (get) Token: 0x06011D97 RID: 73111 RVA: 0x0014964B File Offset: 0x0014784B
		public override string LocalName
		{
			get
			{
				return "min";
			}
		}

		// Token: 0x170055B2 RID: 21938
		// (get) Token: 0x06011D98 RID: 73112 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055B3 RID: 21939
		// (get) Token: 0x06011D99 RID: 73113 RVA: 0x002F2FDD File Offset: 0x002F11DD
		internal override int ElementTypeId
		{
			get
			{
				return 10480;
			}
		}

		// Token: 0x06011D9A RID: 73114 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D9C RID: 73116 RVA: 0x002F2FE4 File Offset: 0x002F11E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinAxisValue>(deep);
		}

		// Token: 0x04007CE5 RID: 31973
		private const string tagName = "min";

		// Token: 0x04007CE6 RID: 31974
		private const byte tagNsId = 11;

		// Token: 0x04007CE7 RID: 31975
		internal const int ElementTypeIdConst = 10480;
	}
}
