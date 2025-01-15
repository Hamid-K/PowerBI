using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021FC RID: 8700
	[GeneratedCode("DomGen", "2.0")]
	internal class ScriptLanguage : OpenXmlLeafTextElement
	{
		// Token: 0x17003801 RID: 14337
		// (get) Token: 0x0600DD92 RID: 56722 RVA: 0x002BD3D8 File Offset: 0x002BB5D8
		public override string LocalName
		{
			get
			{
				return "ScriptLanguage";
			}
		}

		// Token: 0x17003802 RID: 14338
		// (get) Token: 0x0600DD93 RID: 56723 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003803 RID: 14339
		// (get) Token: 0x0600DD94 RID: 56724 RVA: 0x002BD3DF File Offset: 0x002BB5DF
		internal override int ElementTypeId
		{
			get
			{
				return 12501;
			}
		}

		// Token: 0x0600DD95 RID: 56725 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD96 RID: 56726 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScriptLanguage()
		{
		}

		// Token: 0x0600DD97 RID: 56727 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScriptLanguage(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD98 RID: 56728 RVA: 0x002BD3E8 File Offset: 0x002BB5E8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD99 RID: 56729 RVA: 0x002BD403 File Offset: 0x002BB603
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScriptLanguage>(deep);
		}

		// Token: 0x04006D28 RID: 27944
		private const string tagName = "ScriptLanguage";

		// Token: 0x04006D29 RID: 27945
		private const byte tagNsId = 29;

		// Token: 0x04006D2A RID: 27946
		internal const int ElementTypeIdConst = 12501;
	}
}
