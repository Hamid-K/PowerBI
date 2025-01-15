using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E88 RID: 11912
	[GeneratedCode("DomGen", "2.0")]
	internal class BasedOn : String253Type
	{
		// Token: 0x17008B01 RID: 35585
		// (get) Token: 0x060194F2 RID: 103666 RVA: 0x003486C4 File Offset: 0x003468C4
		public override string LocalName
		{
			get
			{
				return "basedOn";
			}
		}

		// Token: 0x17008B02 RID: 35586
		// (get) Token: 0x060194F3 RID: 103667 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B03 RID: 35587
		// (get) Token: 0x060194F4 RID: 103668 RVA: 0x003486CB File Offset: 0x003468CB
		internal override int ElementTypeId
		{
			get
			{
				return 11894;
			}
		}

		// Token: 0x060194F5 RID: 103669 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194F7 RID: 103671 RVA: 0x003486D2 File Offset: 0x003468D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BasedOn>(deep);
		}

		// Token: 0x0400A83E RID: 43070
		private const string tagName = "basedOn";

		// Token: 0x0400A83F RID: 43071
		private const byte tagNsId = 23;

		// Token: 0x0400A840 RID: 43072
		internal const int ElementTypeIdConst = 11894;
	}
}
