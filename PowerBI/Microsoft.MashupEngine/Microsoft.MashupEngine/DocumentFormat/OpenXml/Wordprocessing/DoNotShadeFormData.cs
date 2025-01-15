using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCE RID: 11726
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotShadeFormData : OnOffType
	{
		// Token: 0x17008806 RID: 34822
		// (get) Token: 0x06018ECC RID: 102092 RVA: 0x0034528F File Offset: 0x0034348F
		public override string LocalName
		{
			get
			{
				return "doNotShadeFormData";
			}
		}

		// Token: 0x17008807 RID: 34823
		// (get) Token: 0x06018ECD RID: 102093 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008808 RID: 34824
		// (get) Token: 0x06018ECE RID: 102094 RVA: 0x00345296 File Offset: 0x00343496
		internal override int ElementTypeId
		{
			get
			{
				return 12016;
			}
		}

		// Token: 0x06018ECF RID: 102095 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018ED1 RID: 102097 RVA: 0x0034529D File Offset: 0x0034349D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotShadeFormData>(deep);
		}

		// Token: 0x0400A5D5 RID: 42453
		private const string tagName = "doNotShadeFormData";

		// Token: 0x0400A5D6 RID: 42454
		private const byte tagNsId = 23;

		// Token: 0x0400A5D7 RID: 42455
		internal const int ElementTypeIdConst = 12016;
	}
}
