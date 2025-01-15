using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E60 RID: 11872
	[GeneratedCode("DomGen", "2.0")]
	internal class MonthShort : EmptyType
	{
		// Token: 0x17008A61 RID: 35425
		// (get) Token: 0x060193A3 RID: 103331 RVA: 0x00347A59 File Offset: 0x00345C59
		public override string LocalName
		{
			get
			{
				return "monthShort";
			}
		}

		// Token: 0x17008A62 RID: 35426
		// (get) Token: 0x060193A4 RID: 103332 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A63 RID: 35427
		// (get) Token: 0x060193A5 RID: 103333 RVA: 0x00347A60 File Offset: 0x00345C60
		internal override int ElementTypeId
		{
			get
			{
				return 11551;
			}
		}

		// Token: 0x060193A6 RID: 103334 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193A8 RID: 103336 RVA: 0x00347A67 File Offset: 0x00345C67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MonthShort>(deep);
		}

		// Token: 0x0400A7B8 RID: 42936
		private const string tagName = "monthShort";

		// Token: 0x0400A7B9 RID: 42937
		private const byte tagNsId = 23;

		// Token: 0x0400A7BA RID: 42938
		internal const int ElementTypeIdConst = 11551;
	}
}
