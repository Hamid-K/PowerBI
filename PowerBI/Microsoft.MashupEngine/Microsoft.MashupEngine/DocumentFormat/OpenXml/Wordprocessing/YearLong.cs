using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E64 RID: 11876
	[GeneratedCode("DomGen", "2.0")]
	internal class YearLong : EmptyType
	{
		// Token: 0x17008A6D RID: 35437
		// (get) Token: 0x060193BB RID: 103355 RVA: 0x00347AB5 File Offset: 0x00345CB5
		public override string LocalName
		{
			get
			{
				return "yearLong";
			}
		}

		// Token: 0x17008A6E RID: 35438
		// (get) Token: 0x060193BC RID: 103356 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A6F RID: 35439
		// (get) Token: 0x060193BD RID: 103357 RVA: 0x00347ABC File Offset: 0x00345CBC
		internal override int ElementTypeId
		{
			get
			{
				return 11555;
			}
		}

		// Token: 0x060193BE RID: 103358 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193C0 RID: 103360 RVA: 0x00347AC3 File Offset: 0x00345CC3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<YearLong>(deep);
		}

		// Token: 0x0400A7C4 RID: 42948
		private const string tagName = "yearLong";

		// Token: 0x0400A7C5 RID: 42949
		private const byte tagNsId = 23;

		// Token: 0x0400A7C6 RID: 42950
		internal const int ElementTypeIdConst = 11555;
	}
}
