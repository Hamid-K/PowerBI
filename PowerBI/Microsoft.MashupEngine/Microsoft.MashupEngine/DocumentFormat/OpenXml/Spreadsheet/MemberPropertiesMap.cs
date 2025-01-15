using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5F RID: 11103
	[GeneratedCode("DomGen", "2.0")]
	internal class MemberPropertiesMap : XType
	{
		// Token: 0x170078CD RID: 30925
		// (get) Token: 0x06016D35 RID: 93493 RVA: 0x0032F7FB File Offset: 0x0032D9FB
		public override string LocalName
		{
			get
			{
				return "mpMap";
			}
		}

		// Token: 0x170078CE RID: 30926
		// (get) Token: 0x06016D36 RID: 93494 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078CF RID: 30927
		// (get) Token: 0x06016D37 RID: 93495 RVA: 0x0032F802 File Offset: 0x0032DA02
		internal override int ElementTypeId
		{
			get
			{
				return 11443;
			}
		}

		// Token: 0x06016D38 RID: 93496 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016D3A RID: 93498 RVA: 0x0032F809 File Offset: 0x0032DA09
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MemberPropertiesMap>(deep);
		}

		// Token: 0x04009A0F RID: 39439
		private const string tagName = "mpMap";

		// Token: 0x04009A10 RID: 39440
		private const byte tagNsId = 22;

		// Token: 0x04009A11 RID: 39441
		internal const int ElementTypeIdConst = 11443;
	}
}
