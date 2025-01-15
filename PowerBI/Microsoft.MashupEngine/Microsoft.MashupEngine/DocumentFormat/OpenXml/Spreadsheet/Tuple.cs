using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6D RID: 11117
	[GeneratedCode("DomGen", "2.0")]
	internal class Tuple : OpenXmlLeafElement
	{
		// Token: 0x17007936 RID: 31030
		// (get) Token: 0x06016E25 RID: 93733 RVA: 0x003301B7 File Offset: 0x0032E3B7
		public override string LocalName
		{
			get
			{
				return "tpl";
			}
		}

		// Token: 0x17007937 RID: 31031
		// (get) Token: 0x06016E26 RID: 93734 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007938 RID: 31032
		// (get) Token: 0x06016E27 RID: 93735 RVA: 0x003301BE File Offset: 0x0032E3BE
		internal override int ElementTypeId
		{
			get
			{
				return 11096;
			}
		}

		// Token: 0x06016E28 RID: 93736 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007939 RID: 31033
		// (get) Token: 0x06016E29 RID: 93737 RVA: 0x003301C5 File Offset: 0x0032E3C5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tuple.attributeTagNames;
			}
		}

		// Token: 0x1700793A RID: 31034
		// (get) Token: 0x06016E2A RID: 93738 RVA: 0x003301CC File Offset: 0x0032E3CC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tuple.attributeNamespaceIds;
			}
		}

		// Token: 0x1700793B RID: 31035
		// (get) Token: 0x06016E2B RID: 93739 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016E2C RID: 93740 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fld")]
		public UInt32Value Field
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

		// Token: 0x1700793C RID: 31036
		// (get) Token: 0x06016E2D RID: 93741 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016E2E RID: 93742 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hier")]
		public UInt32Value Hierarchy
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

		// Token: 0x1700793D RID: 31037
		// (get) Token: 0x06016E2F RID: 93743 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06016E30 RID: 93744 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "item")]
		public UInt32Value Item
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

		// Token: 0x06016E32 RID: 93746 RVA: 0x003301D4 File Offset: 0x0032E3D4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fld" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hier" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "item" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E33 RID: 93747 RVA: 0x0033022B File Offset: 0x0032E42B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tuple>(deep);
		}

		// Token: 0x06016E34 RID: 93748 RVA: 0x00330234 File Offset: 0x0032E434
		// Note: this type is marked as 'beforefieldinit'.
		static Tuple()
		{
			byte[] array = new byte[3];
			Tuple.attributeNamespaceIds = array;
		}

		// Token: 0x04009A55 RID: 39509
		private const string tagName = "tpl";

		// Token: 0x04009A56 RID: 39510
		private const byte tagNsId = 22;

		// Token: 0x04009A57 RID: 39511
		internal const int ElementTypeIdConst = 11096;

		// Token: 0x04009A58 RID: 39512
		private static string[] attributeTagNames = new string[] { "fld", "hier", "item" };

		// Token: 0x04009A59 RID: 39513
		private static byte[] attributeNamespaceIds;
	}
}
