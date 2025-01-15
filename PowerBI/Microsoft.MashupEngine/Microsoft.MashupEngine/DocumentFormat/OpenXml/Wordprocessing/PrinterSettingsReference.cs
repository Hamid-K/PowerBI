using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4D RID: 11853
	[GeneratedCode("DomGen", "2.0")]
	internal class PrinterSettingsReference : RelationshipType
	{
		// Token: 0x17008A27 RID: 35367
		// (get) Token: 0x06019323 RID: 103203 RVA: 0x002AD8FA File Offset: 0x002ABAFA
		public override string LocalName
		{
			get
			{
				return "printerSettings";
			}
		}

		// Token: 0x17008A28 RID: 35368
		// (get) Token: 0x06019324 RID: 103204 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A29 RID: 35369
		// (get) Token: 0x06019325 RID: 103205 RVA: 0x00347754 File Offset: 0x00345954
		internal override int ElementTypeId
		{
			get
			{
				return 11542;
			}
		}

		// Token: 0x06019326 RID: 103206 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019328 RID: 103208 RVA: 0x00347763 File Offset: 0x00345963
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrinterSettingsReference>(deep);
		}

		// Token: 0x0400A781 RID: 42881
		private const string tagName = "printerSettings";

		// Token: 0x0400A782 RID: 42882
		private const byte tagNsId = 23;

		// Token: 0x0400A783 RID: 42883
		internal const int ElementTypeIdConst = 11542;
	}
}
