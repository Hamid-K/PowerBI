using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomXsn
{
	// Token: 0x020022B4 RID: 8884
	[GeneratedCode("DomGen", "2.0")]
	internal class OpenByDefault : OpenXmlLeafTextElement
	{
		// Token: 0x17004164 RID: 16740
		// (get) Token: 0x0600F14B RID: 61771 RVA: 0x002D139C File Offset: 0x002CF59C
		public override string LocalName
		{
			get
			{
				return "openByDefault";
			}
		}

		// Token: 0x17004165 RID: 16741
		// (get) Token: 0x0600F14C RID: 61772 RVA: 0x002D1203 File Offset: 0x002CF403
		internal override byte NamespaceId
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x17004166 RID: 16742
		// (get) Token: 0x0600F14D RID: 61773 RVA: 0x002D13A3 File Offset: 0x002CF5A3
		internal override int ElementTypeId
		{
			get
			{
				return 12638;
			}
		}

		// Token: 0x0600F14E RID: 61774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F14F RID: 61775 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public OpenByDefault()
		{
		}

		// Token: 0x0600F150 RID: 61776 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public OpenByDefault(string text)
			: base(text)
		{
		}

		// Token: 0x0600F151 RID: 61777 RVA: 0x002D13AC File Offset: 0x002CF5AC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F152 RID: 61778 RVA: 0x002D13C7 File Offset: 0x002CF5C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OpenByDefault>(deep);
		}

		// Token: 0x040070BA RID: 28858
		private const string tagName = "openByDefault";

		// Token: 0x040070BB RID: 28859
		private const byte tagNsId = 39;

		// Token: 0x040070BC RID: 28860
		internal const int ElementTypeIdConst = 12638;
	}
}
