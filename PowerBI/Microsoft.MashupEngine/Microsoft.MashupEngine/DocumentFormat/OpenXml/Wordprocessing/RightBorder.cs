using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA1 RID: 11937
	[GeneratedCode("DomGen", "2.0")]
	internal class RightBorder : BorderType
	{
		// Token: 0x17008B7E RID: 35710
		// (get) Token: 0x060195EE RID: 103918 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x17008B7F RID: 35711
		// (get) Token: 0x060195EF RID: 103919 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B80 RID: 35712
		// (get) Token: 0x060195F0 RID: 103920 RVA: 0x00349061 File Offset: 0x00347261
		internal override int ElementTypeId
		{
			get
			{
				return 11718;
			}
		}

		// Token: 0x060195F1 RID: 103921 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195F3 RID: 103923 RVA: 0x00349068 File Offset: 0x00347268
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightBorder>(deep);
		}

		// Token: 0x0400A899 RID: 43161
		private const string tagName = "right";

		// Token: 0x0400A89A RID: 43162
		private const byte tagNsId = 23;

		// Token: 0x0400A89B RID: 43163
		internal const int ElementTypeIdConst = 11718;
	}
}
