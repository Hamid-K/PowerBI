using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD7 RID: 11735
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotDemarcateInvalidXml : OnOffType
	{
		// Token: 0x17008821 RID: 34849
		// (get) Token: 0x06018F02 RID: 102146 RVA: 0x0034535E File Offset: 0x0034355E
		public override string LocalName
		{
			get
			{
				return "doNotDemarcateInvalidXml";
			}
		}

		// Token: 0x17008822 RID: 34850
		// (get) Token: 0x06018F03 RID: 102147 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008823 RID: 34851
		// (get) Token: 0x06018F04 RID: 102148 RVA: 0x00345365 File Offset: 0x00343565
		internal override int ElementTypeId
		{
			get
			{
				return 12028;
			}
		}

		// Token: 0x06018F05 RID: 102149 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F07 RID: 102151 RVA: 0x0034536C File Offset: 0x0034356C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotDemarcateInvalidXml>(deep);
		}

		// Token: 0x0400A5F0 RID: 42480
		private const string tagName = "doNotDemarcateInvalidXml";

		// Token: 0x0400A5F1 RID: 42481
		private const byte tagNsId = 23;

		// Token: 0x0400A5F2 RID: 42482
		internal const int ElementTypeIdConst = 12028;
	}
}
