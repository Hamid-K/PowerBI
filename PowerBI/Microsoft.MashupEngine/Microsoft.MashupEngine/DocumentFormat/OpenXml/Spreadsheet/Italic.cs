using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B99 RID: 11161
	[GeneratedCode("DomGen", "2.0")]
	internal class Italic : BooleanPropertyType
	{
		// Token: 0x17007B27 RID: 31527
		// (get) Token: 0x0601724C RID: 94796 RVA: 0x002EAA6B File Offset: 0x002E8C6B
		public override string LocalName
		{
			get
			{
				return "i";
			}
		}

		// Token: 0x17007B28 RID: 31528
		// (get) Token: 0x0601724D RID: 94797 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B29 RID: 31529
		// (get) Token: 0x0601724E RID: 94798 RVA: 0x003333AB File Offset: 0x003315AB
		internal override int ElementTypeId
		{
			get
			{
				return 11136;
			}
		}

		// Token: 0x0601724F RID: 94799 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017251 RID: 94801 RVA: 0x003333B2 File Offset: 0x003315B2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Italic>(deep);
		}

		// Token: 0x04009B3A RID: 39738
		private const string tagName = "i";

		// Token: 0x04009B3B RID: 39739
		private const byte tagNsId = 22;

		// Token: 0x04009B3C RID: 39740
		internal const int ElementTypeIdConst = 11136;
	}
}
