using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9C RID: 11164
	[GeneratedCode("DomGen", "2.0")]
	internal class Extend : BooleanPropertyType
	{
		// Token: 0x17007B30 RID: 31536
		// (get) Token: 0x0601725E RID: 94814 RVA: 0x003333E9 File Offset: 0x003315E9
		public override string LocalName
		{
			get
			{
				return "extend";
			}
		}

		// Token: 0x17007B31 RID: 31537
		// (get) Token: 0x0601725F RID: 94815 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B32 RID: 31538
		// (get) Token: 0x06017260 RID: 94816 RVA: 0x003333F0 File Offset: 0x003315F0
		internal override int ElementTypeId
		{
			get
			{
				return 11139;
			}
		}

		// Token: 0x06017261 RID: 94817 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017263 RID: 94819 RVA: 0x003333F7 File Offset: 0x003315F7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extend>(deep);
		}

		// Token: 0x04009B43 RID: 39747
		private const string tagName = "extend";

		// Token: 0x04009B44 RID: 39748
		private const byte tagNsId = 22;

		// Token: 0x04009B45 RID: 39749
		internal const int ElementTypeIdConst = 11139;
	}
}
