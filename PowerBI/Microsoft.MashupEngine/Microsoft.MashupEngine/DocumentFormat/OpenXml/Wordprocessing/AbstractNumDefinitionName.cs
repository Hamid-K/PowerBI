using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E84 RID: 11908
	[GeneratedCode("DomGen", "2.0")]
	internal class AbstractNumDefinitionName : String253Type
	{
		// Token: 0x17008AF5 RID: 35573
		// (get) Token: 0x060194DA RID: 103642 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17008AF6 RID: 35574
		// (get) Token: 0x060194DB RID: 103643 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AF7 RID: 35575
		// (get) Token: 0x060194DC RID: 103644 RVA: 0x0034866F File Offset: 0x0034686F
		internal override int ElementTypeId
		{
			get
			{
				return 11877;
			}
		}

		// Token: 0x060194DD RID: 103645 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194DF RID: 103647 RVA: 0x00348676 File Offset: 0x00346876
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbstractNumDefinitionName>(deep);
		}

		// Token: 0x0400A832 RID: 43058
		private const string tagName = "name";

		// Token: 0x0400A833 RID: 43059
		private const byte tagNsId = 23;

		// Token: 0x0400A834 RID: 43060
		internal const int ElementTypeIdConst = 11877;
	}
}
