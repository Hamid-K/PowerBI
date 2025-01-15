using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9A RID: 11162
	[GeneratedCode("DomGen", "2.0")]
	internal class Strike : BooleanPropertyType
	{
		// Token: 0x17007B2A RID: 31530
		// (get) Token: 0x06017252 RID: 94802 RVA: 0x003333BB File Offset: 0x003315BB
		public override string LocalName
		{
			get
			{
				return "strike";
			}
		}

		// Token: 0x17007B2B RID: 31531
		// (get) Token: 0x06017253 RID: 94803 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B2C RID: 31532
		// (get) Token: 0x06017254 RID: 94804 RVA: 0x003333C2 File Offset: 0x003315C2
		internal override int ElementTypeId
		{
			get
			{
				return 11137;
			}
		}

		// Token: 0x06017255 RID: 94805 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017257 RID: 94807 RVA: 0x003333C9 File Offset: 0x003315C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Strike>(deep);
		}

		// Token: 0x04009B3D RID: 39741
		private const string tagName = "strike";

		// Token: 0x04009B3E RID: 39742
		private const byte tagNsId = 22;

		// Token: 0x04009B3F RID: 39743
		internal const int ElementTypeIdConst = 11137;
	}
}
