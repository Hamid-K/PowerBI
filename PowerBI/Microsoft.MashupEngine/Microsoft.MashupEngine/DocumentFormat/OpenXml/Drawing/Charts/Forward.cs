using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002560 RID: 9568
	[GeneratedCode("DomGen", "2.0")]
	internal class Forward : DoubleType
	{
		// Token: 0x1700559C RID: 21916
		// (get) Token: 0x06011D6D RID: 73069 RVA: 0x002F2F43 File Offset: 0x002F1143
		public override string LocalName
		{
			get
			{
				return "forward";
			}
		}

		// Token: 0x1700559D RID: 21917
		// (get) Token: 0x06011D6E RID: 73070 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700559E RID: 21918
		// (get) Token: 0x06011D6F RID: 73071 RVA: 0x002F2F4A File Offset: 0x002F114A
		internal override int ElementTypeId
		{
			get
			{
				return 10440;
			}
		}

		// Token: 0x06011D70 RID: 73072 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D72 RID: 73074 RVA: 0x002F2F51 File Offset: 0x002F1151
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Forward>(deep);
		}

		// Token: 0x04007CD0 RID: 31952
		private const string tagName = "forward";

		// Token: 0x04007CD1 RID: 31953
		private const byte tagNsId = 11;

		// Token: 0x04007CD2 RID: 31954
		internal const int ElementTypeIdConst = 10440;
	}
}
