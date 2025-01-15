using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAA RID: 11946
	[GeneratedCode("DomGen", "2.0")]
	internal class FitText : OpenXmlLeafElement
	{
		// Token: 0x17008B99 RID: 35737
		// (get) Token: 0x06019624 RID: 103972 RVA: 0x003490F8 File Offset: 0x003472F8
		public override string LocalName
		{
			get
			{
				return "fitText";
			}
		}

		// Token: 0x17008B9A RID: 35738
		// (get) Token: 0x06019625 RID: 103973 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B9B RID: 35739
		// (get) Token: 0x06019626 RID: 103974 RVA: 0x003490FF File Offset: 0x003472FF
		internal override int ElementTypeId
		{
			get
			{
				return 11603;
			}
		}

		// Token: 0x06019627 RID: 103975 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B9C RID: 35740
		// (get) Token: 0x06019628 RID: 103976 RVA: 0x00349106 File Offset: 0x00347306
		internal override string[] AttributeTagNames
		{
			get
			{
				return FitText.attributeTagNames;
			}
		}

		// Token: 0x17008B9D RID: 35741
		// (get) Token: 0x06019629 RID: 103977 RVA: 0x0034910D File Offset: 0x0034730D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FitText.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B9E RID: 35742
		// (get) Token: 0x0601962A RID: 103978 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601962B RID: 103979 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt32Value Val
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008B9F RID: 35743
		// (get) Token: 0x0601962C RID: 103980 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601962D RID: 103981 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601962F RID: 103983 RVA: 0x00349114 File Offset: 0x00347314
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019630 RID: 103984 RVA: 0x0034914E File Offset: 0x0034734E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FitText>(deep);
		}

		// Token: 0x0400A8B4 RID: 43188
		private const string tagName = "fitText";

		// Token: 0x0400A8B5 RID: 43189
		private const byte tagNsId = 23;

		// Token: 0x0400A8B6 RID: 43190
		internal const int ElementTypeIdConst = 11603;

		// Token: 0x0400A8B7 RID: 43191
		private static string[] attributeTagNames = new string[] { "val", "id" };

		// Token: 0x0400A8B8 RID: 43192
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
