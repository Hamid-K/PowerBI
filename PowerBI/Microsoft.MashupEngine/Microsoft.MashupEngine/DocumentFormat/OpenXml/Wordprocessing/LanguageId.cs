using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4A RID: 12106
	[GeneratedCode("DomGen", "2.0")]
	internal class LanguageId : OpenXmlLeafElement
	{
		// Token: 0x17008FFB RID: 36859
		// (get) Token: 0x06019FD6 RID: 106454 RVA: 0x0035A95C File Offset: 0x00358B5C
		public override string LocalName
		{
			get
			{
				return "lid";
			}
		}

		// Token: 0x17008FFC RID: 36860
		// (get) Token: 0x06019FD7 RID: 106455 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FFD RID: 36861
		// (get) Token: 0x06019FD8 RID: 106456 RVA: 0x0035A963 File Offset: 0x00358B63
		internal override int ElementTypeId
		{
			get
			{
				return 11756;
			}
		}

		// Token: 0x06019FD9 RID: 106457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FFE RID: 36862
		// (get) Token: 0x06019FDA RID: 106458 RVA: 0x0035A96A File Offset: 0x00358B6A
		internal override string[] AttributeTagNames
		{
			get
			{
				return LanguageId.attributeTagNames;
			}
		}

		// Token: 0x17008FFF RID: 36863
		// (get) Token: 0x06019FDB RID: 106459 RVA: 0x0035A971 File Offset: 0x00358B71
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LanguageId.attributeNamespaceIds;
			}
		}

		// Token: 0x17009000 RID: 36864
		// (get) Token: 0x06019FDC RID: 106460 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019FDD RID: 106461 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x06019FDF RID: 106463 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019FE0 RID: 106464 RVA: 0x0035A978 File Offset: 0x00358B78
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LanguageId>(deep);
		}

		// Token: 0x0400AB48 RID: 43848
		private const string tagName = "lid";

		// Token: 0x0400AB49 RID: 43849
		private const byte tagNsId = 23;

		// Token: 0x0400AB4A RID: 43850
		internal const int ElementTypeIdConst = 11756;

		// Token: 0x0400AB4B RID: 43851
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB4C RID: 43852
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
