using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8A RID: 11146
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BooleanItem))]
	[ChildElementInfo(typeof(DateTimeItem))]
	[ChildElementInfo(typeof(NumberItem))]
	[ChildElementInfo(typeof(MissingItem))]
	[ChildElementInfo(typeof(ErrorItem))]
	[ChildElementInfo(typeof(StringItem))]
	internal class GroupItems : OpenXmlCompositeElement
	{
		// Token: 0x17007AAF RID: 31407
		// (get) Token: 0x06017138 RID: 94520 RVA: 0x003327CF File Offset: 0x003309CF
		public override string LocalName
		{
			get
			{
				return "groupItems";
			}
		}

		// Token: 0x17007AB0 RID: 31408
		// (get) Token: 0x06017139 RID: 94521 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AB1 RID: 31409
		// (get) Token: 0x0601713A RID: 94522 RVA: 0x003327D6 File Offset: 0x003309D6
		internal override int ElementTypeId
		{
			get
			{
				return 11124;
			}
		}

		// Token: 0x0601713B RID: 94523 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AB2 RID: 31410
		// (get) Token: 0x0601713C RID: 94524 RVA: 0x003327DD File Offset: 0x003309DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupItems.attributeTagNames;
			}
		}

		// Token: 0x17007AB3 RID: 31411
		// (get) Token: 0x0601713D RID: 94525 RVA: 0x003327E4 File Offset: 0x003309E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupItems.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AB4 RID: 31412
		// (get) Token: 0x0601713E RID: 94526 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601713F RID: 94527 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06017140 RID: 94528 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupItems()
		{
		}

		// Token: 0x06017141 RID: 94529 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017142 RID: 94530 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017143 RID: 94531 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017144 RID: 94532 RVA: 0x003327EC File Offset: 0x003309EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "m" == name)
			{
				return new MissingItem();
			}
			if (22 == namespaceId && "n" == name)
			{
				return new NumberItem();
			}
			if (22 == namespaceId && "b" == name)
			{
				return new BooleanItem();
			}
			if (22 == namespaceId && "e" == name)
			{
				return new ErrorItem();
			}
			if (22 == namespaceId && "s" == name)
			{
				return new StringItem();
			}
			if (22 == namespaceId && "d" == name)
			{
				return new DateTimeItem();
			}
			return null;
		}

		// Token: 0x06017145 RID: 94533 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017146 RID: 94534 RVA: 0x0033288A File Offset: 0x00330A8A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupItems>(deep);
		}

		// Token: 0x06017147 RID: 94535 RVA: 0x00332894 File Offset: 0x00330A94
		// Note: this type is marked as 'beforefieldinit'.
		static GroupItems()
		{
			byte[] array = new byte[1];
			GroupItems.attributeNamespaceIds = array;
		}

		// Token: 0x04009AF7 RID: 39671
		private const string tagName = "groupItems";

		// Token: 0x04009AF8 RID: 39672
		private const byte tagNsId = 22;

		// Token: 0x04009AF9 RID: 39673
		internal const int ElementTypeIdConst = 11124;

		// Token: 0x04009AFA RID: 39674
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009AFB RID: 39675
		private static byte[] attributeNamespaceIds;
	}
}
