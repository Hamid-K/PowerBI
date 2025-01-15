using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002561 RID: 9569
	[GeneratedCode("DomGen", "2.0")]
	internal class Backward : DoubleType
	{
		// Token: 0x1700559F RID: 21919
		// (get) Token: 0x06011D73 RID: 73075 RVA: 0x002F2F5A File Offset: 0x002F115A
		public override string LocalName
		{
			get
			{
				return "backward";
			}
		}

		// Token: 0x170055A0 RID: 21920
		// (get) Token: 0x06011D74 RID: 73076 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055A1 RID: 21921
		// (get) Token: 0x06011D75 RID: 73077 RVA: 0x002F2F61 File Offset: 0x002F1161
		internal override int ElementTypeId
		{
			get
			{
				return 10441;
			}
		}

		// Token: 0x06011D76 RID: 73078 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D78 RID: 73080 RVA: 0x002F2F68 File Offset: 0x002F1168
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Backward>(deep);
		}

		// Token: 0x04007CD3 RID: 31955
		private const string tagName = "backward";

		// Token: 0x04007CD4 RID: 31956
		private const byte tagNsId = 11;

		// Token: 0x04007CD5 RID: 31957
		internal const int ElementTypeIdConst = 10441;
	}
}
