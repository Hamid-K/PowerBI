using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E59 RID: 11865
	[GeneratedCode("DomGen", "2.0")]
	internal class DeletedText : TextType
	{
		// Token: 0x17008A4F RID: 35407
		// (get) Token: 0x06019378 RID: 103288 RVA: 0x00347970 File Offset: 0x00345B70
		public override string LocalName
		{
			get
			{
				return "delText";
			}
		}

		// Token: 0x17008A50 RID: 35408
		// (get) Token: 0x06019379 RID: 103289 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A51 RID: 35409
		// (get) Token: 0x0601937A RID: 103290 RVA: 0x00347977 File Offset: 0x00345B77
		internal override int ElementTypeId
		{
			get
			{
				return 11545;
			}
		}

		// Token: 0x0601937B RID: 103291 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601937C RID: 103292 RVA: 0x0034793A File Offset: 0x00345B3A
		public DeletedText()
		{
		}

		// Token: 0x0601937D RID: 103293 RVA: 0x00347942 File Offset: 0x00345B42
		public DeletedText(string text)
			: base(text)
		{
		}

		// Token: 0x0601937E RID: 103294 RVA: 0x00347980 File Offset: 0x00345B80
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601937F RID: 103295 RVA: 0x0034799B File Offset: 0x00345B9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DeletedText>(deep);
		}

		// Token: 0x0400A7A6 RID: 42918
		private const string tagName = "delText";

		// Token: 0x0400A7A7 RID: 42919
		private const byte tagNsId = 23;

		// Token: 0x0400A7A8 RID: 42920
		internal const int ElementTypeIdConst = 11545;
	}
}
