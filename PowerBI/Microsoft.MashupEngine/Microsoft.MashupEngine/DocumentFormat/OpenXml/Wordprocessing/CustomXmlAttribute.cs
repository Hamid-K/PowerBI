using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5B RID: 12123
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlAttribute : OpenXmlLeafElement
	{
		// Token: 0x17009045 RID: 36933
		// (get) Token: 0x0601A099 RID: 106649 RVA: 0x0035CB14 File Offset: 0x0035AD14
		public override string LocalName
		{
			get
			{
				return "attr";
			}
		}

		// Token: 0x17009046 RID: 36934
		// (get) Token: 0x0601A09A RID: 106650 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009047 RID: 36935
		// (get) Token: 0x0601A09B RID: 106651 RVA: 0x0035CB1B File Offset: 0x0035AD1B
		internal override int ElementTypeId
		{
			get
			{
				return 11779;
			}
		}

		// Token: 0x0601A09C RID: 106652 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009048 RID: 36936
		// (get) Token: 0x0601A09D RID: 106653 RVA: 0x0035CB22 File Offset: 0x0035AD22
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomXmlAttribute.attributeTagNames;
			}
		}

		// Token: 0x17009049 RID: 36937
		// (get) Token: 0x0601A09E RID: 106654 RVA: 0x0035CB29 File Offset: 0x0035AD29
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomXmlAttribute.attributeNamespaceIds;
			}
		}

		// Token: 0x1700904A RID: 36938
		// (get) Token: 0x0601A09F RID: 106655 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A0A0 RID: 106656 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "uri")]
		public StringValue Uri
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

		// Token: 0x1700904B RID: 36939
		// (get) Token: 0x0601A0A1 RID: 106657 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A0A2 RID: 106658 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700904C RID: 36940
		// (get) Token: 0x0601A0A3 RID: 106659 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A0A4 RID: 106660 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601A0A6 RID: 106662 RVA: 0x0035CB30 File Offset: 0x0035AD30
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A0A7 RID: 106663 RVA: 0x0035CB8D File Offset: 0x0035AD8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlAttribute>(deep);
		}

		// Token: 0x0400AB84 RID: 43908
		private const string tagName = "attr";

		// Token: 0x0400AB85 RID: 43909
		private const byte tagNsId = 23;

		// Token: 0x0400AB86 RID: 43910
		internal const int ElementTypeIdConst = 11779;

		// Token: 0x0400AB87 RID: 43911
		private static string[] attributeTagNames = new string[] { "uri", "name", "val" };

		// Token: 0x0400AB88 RID: 43912
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
