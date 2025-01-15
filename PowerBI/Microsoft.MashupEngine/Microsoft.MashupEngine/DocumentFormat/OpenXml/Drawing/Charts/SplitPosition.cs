using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002564 RID: 9572
	[GeneratedCode("DomGen", "2.0")]
	internal class SplitPosition : DoubleType
	{
		// Token: 0x170055A8 RID: 21928
		// (get) Token: 0x06011D85 RID: 73093 RVA: 0x002F2F9F File Offset: 0x002F119F
		public override string LocalName
		{
			get
			{
				return "splitPos";
			}
		}

		// Token: 0x170055A9 RID: 21929
		// (get) Token: 0x06011D86 RID: 73094 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055AA RID: 21930
		// (get) Token: 0x06011D87 RID: 73095 RVA: 0x002F2FA6 File Offset: 0x002F11A6
		internal override int ElementTypeId
		{
			get
			{
				return 10464;
			}
		}

		// Token: 0x06011D88 RID: 73096 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D8A RID: 73098 RVA: 0x002F2FAD File Offset: 0x002F11AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitPosition>(deep);
		}

		// Token: 0x04007CDC RID: 31964
		private const string tagName = "splitPos";

		// Token: 0x04007CDD RID: 31965
		private const byte tagNsId = 11;

		// Token: 0x04007CDE RID: 31966
		internal const int ElementTypeIdConst = 10464;
	}
}
