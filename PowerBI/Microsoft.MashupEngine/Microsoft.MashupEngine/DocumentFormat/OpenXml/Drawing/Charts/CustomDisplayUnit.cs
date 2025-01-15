using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002565 RID: 9573
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomDisplayUnit : DoubleType
	{
		// Token: 0x170055AB RID: 21931
		// (get) Token: 0x06011D8B RID: 73099 RVA: 0x002F2FB6 File Offset: 0x002F11B6
		public override string LocalName
		{
			get
			{
				return "custUnit";
			}
		}

		// Token: 0x170055AC RID: 21932
		// (get) Token: 0x06011D8C RID: 73100 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055AD RID: 21933
		// (get) Token: 0x06011D8D RID: 73101 RVA: 0x002F2FBD File Offset: 0x002F11BD
		internal override int ElementTypeId
		{
			get
			{
				return 10474;
			}
		}

		// Token: 0x06011D8E RID: 73102 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D90 RID: 73104 RVA: 0x002F2FC4 File Offset: 0x002F11C4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomDisplayUnit>(deep);
		}

		// Token: 0x04007CDF RID: 31967
		private const string tagName = "custUnit";

		// Token: 0x04007CE0 RID: 31968
		private const byte tagNsId = 11;

		// Token: 0x04007CE1 RID: 31969
		internal const int ElementTypeIdConst = 10474;
	}
}
