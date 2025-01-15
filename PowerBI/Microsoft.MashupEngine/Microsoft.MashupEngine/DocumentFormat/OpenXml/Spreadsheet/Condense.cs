using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9B RID: 11163
	[GeneratedCode("DomGen", "2.0")]
	internal class Condense : BooleanPropertyType
	{
		// Token: 0x17007B2D RID: 31533
		// (get) Token: 0x06017258 RID: 94808 RVA: 0x003333D2 File Offset: 0x003315D2
		public override string LocalName
		{
			get
			{
				return "condense";
			}
		}

		// Token: 0x17007B2E RID: 31534
		// (get) Token: 0x06017259 RID: 94809 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B2F RID: 31535
		// (get) Token: 0x0601725A RID: 94810 RVA: 0x003333D9 File Offset: 0x003315D9
		internal override int ElementTypeId
		{
			get
			{
				return 11138;
			}
		}

		// Token: 0x0601725B RID: 94811 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601725D RID: 94813 RVA: 0x003333E0 File Offset: 0x003315E0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Condense>(deep);
		}

		// Token: 0x04009B40 RID: 39744
		private const string tagName = "condense";

		// Token: 0x04009B41 RID: 39745
		private const byte tagNsId = 22;

		// Token: 0x04009B42 RID: 39746
		internal const int ElementTypeIdConst = 11138;
	}
}
