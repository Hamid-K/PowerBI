using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B76 RID: 11126
	[ChildElementInfo(typeof(MemberPropertyIndex))]
	[GeneratedCode("DomGen", "2.0")]
	internal class RowItem : OpenXmlCompositeElement
	{
		// Token: 0x170079DF RID: 31199
		// (get) Token: 0x06016F81 RID: 94081 RVA: 0x002EAA6B File Offset: 0x002E8C6B
		public override string LocalName
		{
			get
			{
				return "i";
			}
		}

		// Token: 0x170079E0 RID: 31200
		// (get) Token: 0x06016F82 RID: 94082 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079E1 RID: 31201
		// (get) Token: 0x06016F83 RID: 94083 RVA: 0x003313D2 File Offset: 0x0032F5D2
		internal override int ElementTypeId
		{
			get
			{
				return 11106;
			}
		}

		// Token: 0x06016F84 RID: 94084 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079E2 RID: 31202
		// (get) Token: 0x06016F85 RID: 94085 RVA: 0x003313D9 File Offset: 0x0032F5D9
		internal override string[] AttributeTagNames
		{
			get
			{
				return RowItem.attributeTagNames;
			}
		}

		// Token: 0x170079E3 RID: 31203
		// (get) Token: 0x06016F86 RID: 94086 RVA: 0x003313E0 File Offset: 0x0032F5E0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RowItem.attributeNamespaceIds;
			}
		}

		// Token: 0x170079E4 RID: 31204
		// (get) Token: 0x06016F87 RID: 94087 RVA: 0x003313E7 File Offset: 0x0032F5E7
		// (set) Token: 0x06016F88 RID: 94088 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public EnumValue<ItemValues> ItemType
		{
			get
			{
				return (EnumValue<ItemValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170079E5 RID: 31205
		// (get) Token: 0x06016F89 RID: 94089 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016F8A RID: 94090 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "r")]
		public UInt32Value RepeatedItemCount
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170079E6 RID: 31206
		// (get) Token: 0x06016F8B RID: 94091 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06016F8C RID: 94092 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "i")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06016F8D RID: 94093 RVA: 0x00293ECF File Offset: 0x002920CF
		public RowItem()
		{
		}

		// Token: 0x06016F8E RID: 94094 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RowItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F8F RID: 94095 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RowItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F90 RID: 94096 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RowItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016F91 RID: 94097 RVA: 0x0032F0D8 File Offset: 0x0032D2D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "x" == name)
			{
				return new MemberPropertyIndex();
			}
			return null;
		}

		// Token: 0x06016F92 RID: 94098 RVA: 0x003313F8 File Offset: 0x0032F5F8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<ItemValues>();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016F93 RID: 94099 RVA: 0x0033144F File Offset: 0x0032F64F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowItem>(deep);
		}

		// Token: 0x06016F94 RID: 94100 RVA: 0x00331458 File Offset: 0x0032F658
		// Note: this type is marked as 'beforefieldinit'.
		static RowItem()
		{
			byte[] array = new byte[3];
			RowItem.attributeNamespaceIds = array;
		}

		// Token: 0x04009A8E RID: 39566
		private const string tagName = "i";

		// Token: 0x04009A8F RID: 39567
		private const byte tagNsId = 22;

		// Token: 0x04009A90 RID: 39568
		internal const int ElementTypeIdConst = 11106;

		// Token: 0x04009A91 RID: 39569
		private static string[] attributeTagNames = new string[] { "t", "r", "i" };

		// Token: 0x04009A92 RID: 39570
		private static byte[] attributeNamespaceIds;
	}
}
