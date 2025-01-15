using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Presentation
{
	// Token: 0x02002235 RID: 8757
	[GeneratedCode("DomGen", "2.0")]
	internal class TextData : OpenXmlLeafElement
	{
		// Token: 0x17003958 RID: 14680
		// (get) Token: 0x0600E05F RID: 57439 RVA: 0x002BFCF7 File Offset: 0x002BDEF7
		public override string LocalName
		{
			get
			{
				return "textdata";
			}
		}

		// Token: 0x17003959 RID: 14681
		// (get) Token: 0x0600E060 RID: 57440 RVA: 0x0012AF11 File Offset: 0x00129111
		internal override byte NamespaceId
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x1700395A RID: 14682
		// (get) Token: 0x0600E061 RID: 57441 RVA: 0x002BFCFE File Offset: 0x002BDEFE
		internal override int ElementTypeId
		{
			get
			{
				return 12505;
			}
		}

		// Token: 0x0600E062 RID: 57442 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700395B RID: 14683
		// (get) Token: 0x0600E063 RID: 57443 RVA: 0x002BFD05 File Offset: 0x002BDF05
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextData.attributeTagNames;
			}
		}

		// Token: 0x1700395C RID: 14684
		// (get) Token: 0x0600E064 RID: 57444 RVA: 0x002BFD0C File Offset: 0x002BDF0C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextData.attributeNamespaceIds;
			}
		}

		// Token: 0x1700395D RID: 14685
		// (get) Token: 0x0600E065 RID: 57445 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E066 RID: 57446 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600E068 RID: 57448 RVA: 0x002BFD13 File Offset: 0x002BDF13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E069 RID: 57449 RVA: 0x002BFD33 File Offset: 0x002BDF33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextData>(deep);
		}

		// Token: 0x0600E06A RID: 57450 RVA: 0x002BFD3C File Offset: 0x002BDF3C
		// Note: this type is marked as 'beforefieldinit'.
		static TextData()
		{
			byte[] array = new byte[1];
			TextData.attributeNamespaceIds = array;
		}

		// Token: 0x04006E46 RID: 28230
		private const string tagName = "textdata";

		// Token: 0x04006E47 RID: 28231
		private const byte tagNsId = 30;

		// Token: 0x04006E48 RID: 28232
		internal const int ElementTypeIdConst = 12505;

		// Token: 0x04006E49 RID: 28233
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04006E4A RID: 28234
		private static byte[] attributeNamespaceIds;
	}
}
