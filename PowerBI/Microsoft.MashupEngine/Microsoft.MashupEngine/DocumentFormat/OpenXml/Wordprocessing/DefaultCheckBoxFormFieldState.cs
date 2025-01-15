using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D97 RID: 11671
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultCheckBoxFormFieldState : OnOffType
	{
		// Token: 0x17008761 RID: 34657
		// (get) Token: 0x06018D82 RID: 101762 RVA: 0x00344DAC File Offset: 0x00342FAC
		public override string LocalName
		{
			get
			{
				return "default";
			}
		}

		// Token: 0x17008762 RID: 34658
		// (get) Token: 0x06018D83 RID: 101763 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008763 RID: 34659
		// (get) Token: 0x06018D84 RID: 101764 RVA: 0x00344DB3 File Offset: 0x00342FB3
		internal override int ElementTypeId
		{
			get
			{
				return 11738;
			}
		}

		// Token: 0x06018D85 RID: 101765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D87 RID: 101767 RVA: 0x00344DBA File Offset: 0x00342FBA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultCheckBoxFormFieldState>(deep);
		}

		// Token: 0x0400A530 RID: 42288
		private const string tagName = "default";

		// Token: 0x0400A531 RID: 42289
		private const byte tagNsId = 23;

		// Token: 0x0400A532 RID: 42290
		internal const int ElementTypeIdConst = 11738;
	}
}
