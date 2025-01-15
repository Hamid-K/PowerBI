using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8A RID: 11914
	[GeneratedCode("DomGen", "2.0")]
	internal class LinkedStyle : String253Type
	{
		// Token: 0x17008B07 RID: 35591
		// (get) Token: 0x060194FE RID: 103678 RVA: 0x00326DCE File Offset: 0x00324FCE
		public override string LocalName
		{
			get
			{
				return "link";
			}
		}

		// Token: 0x17008B08 RID: 35592
		// (get) Token: 0x060194FF RID: 103679 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B09 RID: 35593
		// (get) Token: 0x06019500 RID: 103680 RVA: 0x003486F2 File Offset: 0x003468F2
		internal override int ElementTypeId
		{
			get
			{
				return 11896;
			}
		}

		// Token: 0x06019501 RID: 103681 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019503 RID: 103683 RVA: 0x003486F9 File Offset: 0x003468F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkedStyle>(deep);
		}

		// Token: 0x0400A844 RID: 43076
		private const string tagName = "link";

		// Token: 0x0400A845 RID: 43077
		private const byte tagNsId = 23;

		// Token: 0x0400A846 RID: 43078
		internal const int ElementTypeIdConst = 11896;
	}
}
