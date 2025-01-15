using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5F RID: 11615
	[GeneratedCode("DomGen", "2.0")]
	internal class AddressFieldName : StringType
	{
		// Token: 0x170086B9 RID: 34489
		// (get) Token: 0x06018C31 RID: 101425 RVA: 0x003448A1 File Offset: 0x00342AA1
		public override string LocalName
		{
			get
			{
				return "addressFieldName";
			}
		}

		// Token: 0x170086BA RID: 34490
		// (get) Token: 0x06018C32 RID: 101426 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086BB RID: 34491
		// (get) Token: 0x06018C33 RID: 101427 RVA: 0x003448A8 File Offset: 0x00342AA8
		internal override int ElementTypeId
		{
			get
			{
				return 11821;
			}
		}

		// Token: 0x06018C34 RID: 101428 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C36 RID: 101430 RVA: 0x003448AF File Offset: 0x00342AAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AddressFieldName>(deep);
		}

		// Token: 0x0400A489 RID: 42121
		private const string tagName = "addressFieldName";

		// Token: 0x0400A48A RID: 42122
		private const byte tagNsId = 23;

		// Token: 0x0400A48B RID: 42123
		internal const int ElementTypeIdConst = 11821;
	}
}
