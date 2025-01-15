using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8C RID: 11916
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultTableStyle : String253Type
	{
		// Token: 0x17008B0D RID: 35597
		// (get) Token: 0x0601950A RID: 103690 RVA: 0x00348719 File Offset: 0x00346919
		public override string LocalName
		{
			get
			{
				return "defaultTableStyle";
			}
		}

		// Token: 0x17008B0E RID: 35598
		// (get) Token: 0x0601950B RID: 103691 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B0F RID: 35599
		// (get) Token: 0x0601950C RID: 103692 RVA: 0x00348720 File Offset: 0x00346920
		internal override int ElementTypeId
		{
			get
			{
				return 12004;
			}
		}

		// Token: 0x0601950D RID: 103693 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601950F RID: 103695 RVA: 0x00348727 File Offset: 0x00346927
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultTableStyle>(deep);
		}

		// Token: 0x0400A84A RID: 43082
		private const string tagName = "defaultTableStyle";

		// Token: 0x0400A84B RID: 43083
		private const byte tagNsId = 23;

		// Token: 0x0400A84C RID: 43084
		internal const int ElementTypeIdConst = 12004;
	}
}
