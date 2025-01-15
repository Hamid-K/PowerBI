using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDC RID: 11740
	[GeneratedCode("DomGen", "2.0")]
	internal class UpdateFieldsOnOpen : OnOffType
	{
		// Token: 0x17008830 RID: 34864
		// (get) Token: 0x06018F20 RID: 102176 RVA: 0x003453D1 File Offset: 0x003435D1
		public override string LocalName
		{
			get
			{
				return "updateFields";
			}
		}

		// Token: 0x17008831 RID: 34865
		// (get) Token: 0x06018F21 RID: 102177 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008832 RID: 34866
		// (get) Token: 0x06018F22 RID: 102178 RVA: 0x003453D8 File Offset: 0x003435D8
		internal override int ElementTypeId
		{
			get
			{
				return 12034;
			}
		}

		// Token: 0x06018F23 RID: 102179 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F25 RID: 102181 RVA: 0x003453DF File Offset: 0x003435DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UpdateFieldsOnOpen>(deep);
		}

		// Token: 0x0400A5FF RID: 42495
		private const string tagName = "updateFields";

		// Token: 0x0400A600 RID: 42496
		private const byte tagNsId = 23;

		// Token: 0x0400A601 RID: 42497
		internal const int ElementTypeIdConst = 12034;
	}
}
