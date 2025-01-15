using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F51 RID: 12113
	[GeneratedCode("DomGen", "2.0")]
	internal class ListItem : OpenXmlLeafElement
	{
		// Token: 0x1700901F RID: 36895
		// (get) Token: 0x0601A02B RID: 106539 RVA: 0x0035B220 File Offset: 0x00359420
		public override string LocalName
		{
			get
			{
				return "listItem";
			}
		}

		// Token: 0x17009020 RID: 36896
		// (get) Token: 0x0601A02C RID: 106540 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009021 RID: 36897
		// (get) Token: 0x0601A02D RID: 106541 RVA: 0x0035B227 File Offset: 0x00359427
		internal override int ElementTypeId
		{
			get
			{
				return 11764;
			}
		}

		// Token: 0x0601A02E RID: 106542 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009022 RID: 36898
		// (get) Token: 0x0601A02F RID: 106543 RVA: 0x0035B22E File Offset: 0x0035942E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ListItem.attributeTagNames;
			}
		}

		// Token: 0x17009023 RID: 36899
		// (get) Token: 0x0601A030 RID: 106544 RVA: 0x0035B235 File Offset: 0x00359435
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ListItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17009024 RID: 36900
		// (get) Token: 0x0601A031 RID: 106545 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A032 RID: 106546 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "displayText")]
		public StringValue DisplayText
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

		// Token: 0x17009025 RID: 36901
		// (get) Token: 0x0601A033 RID: 106547 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A034 RID: 106548 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "value")]
		public StringValue Value
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

		// Token: 0x0601A036 RID: 106550 RVA: 0x0035B23C File Offset: 0x0035943C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "displayText" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "value" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A037 RID: 106551 RVA: 0x0035B276 File Offset: 0x00359476
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListItem>(deep);
		}

		// Token: 0x0400AB62 RID: 43874
		private const string tagName = "listItem";

		// Token: 0x0400AB63 RID: 43875
		private const byte tagNsId = 23;

		// Token: 0x0400AB64 RID: 43876
		internal const int ElementTypeIdConst = 11764;

		// Token: 0x0400AB65 RID: 43877
		private static string[] attributeTagNames = new string[] { "displayText", "value" };

		// Token: 0x0400AB66 RID: 43878
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
