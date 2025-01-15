using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD6 RID: 12246
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartId : OpenXmlLeafElement
	{
		// Token: 0x1700945D RID: 37981
		// (get) Token: 0x0601A951 RID: 108881 RVA: 0x00364772 File Offset: 0x00362972
		public override string LocalName
		{
			get
			{
				return "guid";
			}
		}

		// Token: 0x1700945E RID: 37982
		// (get) Token: 0x0601A952 RID: 108882 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700945F RID: 37983
		// (get) Token: 0x0601A953 RID: 108883 RVA: 0x00364779 File Offset: 0x00362979
		internal override int ElementTypeId
		{
			get
			{
				return 11954;
			}
		}

		// Token: 0x0601A954 RID: 108884 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009460 RID: 37984
		// (get) Token: 0x0601A955 RID: 108885 RVA: 0x00364780 File Offset: 0x00362980
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocPartId.attributeTagNames;
			}
		}

		// Token: 0x17009461 RID: 37985
		// (get) Token: 0x0601A956 RID: 108886 RVA: 0x00364787 File Offset: 0x00362987
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocPartId.attributeNamespaceIds;
			}
		}

		// Token: 0x17009462 RID: 37986
		// (get) Token: 0x0601A957 RID: 108887 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A958 RID: 108888 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601A95A RID: 108890 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A95B RID: 108891 RVA: 0x0036478E File Offset: 0x0036298E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartId>(deep);
		}

		// Token: 0x0400AD9D RID: 44445
		private const string tagName = "guid";

		// Token: 0x0400AD9E RID: 44446
		private const byte tagNsId = 23;

		// Token: 0x0400AD9F RID: 44447
		internal const int ElementTypeIdConst = 11954;

		// Token: 0x0400ADA0 RID: 44448
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADA1 RID: 44449
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
