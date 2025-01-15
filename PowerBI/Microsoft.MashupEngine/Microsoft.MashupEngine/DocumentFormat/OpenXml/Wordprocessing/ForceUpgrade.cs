using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6F RID: 11887
	[GeneratedCode("DomGen", "2.0")]
	internal class ForceUpgrade : EmptyType
	{
		// Token: 0x17008A8E RID: 35470
		// (get) Token: 0x060193FD RID: 103421 RVA: 0x00347BA4 File Offset: 0x00345DA4
		public override string LocalName
		{
			get
			{
				return "forceUpgrade";
			}
		}

		// Token: 0x17008A8F RID: 35471
		// (get) Token: 0x060193FE RID: 103422 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A90 RID: 35472
		// (get) Token: 0x060193FF RID: 103423 RVA: 0x00347BAB File Offset: 0x00345DAB
		internal override int ElementTypeId
		{
			get
			{
				return 12047;
			}
		}

		// Token: 0x06019400 RID: 103424 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019402 RID: 103426 RVA: 0x00347BB2 File Offset: 0x00345DB2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForceUpgrade>(deep);
		}

		// Token: 0x0400A7E5 RID: 42981
		private const string tagName = "forceUpgrade";

		// Token: 0x0400A7E6 RID: 42982
		private const byte tagNsId = 23;

		// Token: 0x0400A7E7 RID: 42983
		internal const int ElementTypeIdConst = 12047;
	}
}
