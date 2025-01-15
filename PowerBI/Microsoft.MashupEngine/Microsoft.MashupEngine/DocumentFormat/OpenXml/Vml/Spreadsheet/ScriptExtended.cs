using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E9 RID: 8681
	[GeneratedCode("DomGen", "2.0")]
	internal class ScriptExtended : OpenXmlLeafTextElement
	{
		// Token: 0x170037C8 RID: 14280
		// (get) Token: 0x0600DCFA RID: 56570 RVA: 0x002BCFFC File Offset: 0x002BB1FC
		public override string LocalName
		{
			get
			{
				return "ScriptExtended";
			}
		}

		// Token: 0x170037C9 RID: 14281
		// (get) Token: 0x0600DCFB RID: 56571 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037CA RID: 14282
		// (get) Token: 0x0600DCFC RID: 56572 RVA: 0x002BD003 File Offset: 0x002BB203
		internal override int ElementTypeId
		{
			get
			{
				return 12500;
			}
		}

		// Token: 0x0600DCFD RID: 56573 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCFE RID: 56574 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScriptExtended()
		{
		}

		// Token: 0x0600DCFF RID: 56575 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScriptExtended(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD00 RID: 56576 RVA: 0x002BD00C File Offset: 0x002BB20C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD01 RID: 56577 RVA: 0x002BD027 File Offset: 0x002BB227
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScriptExtended>(deep);
		}

		// Token: 0x04006CEF RID: 27887
		private const string tagName = "ScriptExtended";

		// Token: 0x04006CF0 RID: 27888
		private const byte tagNsId = 29;

		// Token: 0x04006CF1 RID: 27889
		internal const int ElementTypeIdConst = 12500;
	}
}
