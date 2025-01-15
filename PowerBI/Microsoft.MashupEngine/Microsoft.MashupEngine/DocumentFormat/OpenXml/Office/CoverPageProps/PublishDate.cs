using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A4 RID: 8868
	[GeneratedCode("DomGen", "2.0")]
	internal class PublishDate : OpenXmlLeafTextElement
	{
		// Token: 0x17004119 RID: 16665
		// (get) Token: 0x0600F093 RID: 61587 RVA: 0x002D0CD0 File Offset: 0x002CEED0
		public override string LocalName
		{
			get
			{
				return "PublishDate";
			}
		}

		// Token: 0x1700411A RID: 16666
		// (get) Token: 0x0600F094 RID: 61588 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x1700411B RID: 16667
		// (get) Token: 0x0600F095 RID: 61589 RVA: 0x002D0CD7 File Offset: 0x002CEED7
		internal override int ElementTypeId
		{
			get
			{
				return 12622;
			}
		}

		// Token: 0x0600F096 RID: 61590 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F097 RID: 61591 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PublishDate()
		{
		}

		// Token: 0x0600F098 RID: 61592 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PublishDate(string text)
			: base(text)
		{
		}

		// Token: 0x0600F099 RID: 61593 RVA: 0x002D0CE0 File Offset: 0x002CEEE0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F09A RID: 61594 RVA: 0x002D0CFB File Offset: 0x002CEEFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PublishDate>(deep);
		}

		// Token: 0x04007082 RID: 28802
		private const string tagName = "PublishDate";

		// Token: 0x04007083 RID: 28803
		private const byte tagNsId = 36;

		// Token: 0x04007084 RID: 28804
		internal const int ElementTypeIdConst = 12622;
	}
}
