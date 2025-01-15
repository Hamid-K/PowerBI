using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC0 RID: 11456
	[ChildElementInfo(typeof(GroupLevel))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupLevels : OpenXmlCompositeElement
	{
		// Token: 0x170084E7 RID: 34023
		// (get) Token: 0x06018835 RID: 100405 RVA: 0x003420E3 File Offset: 0x003402E3
		public override string LocalName
		{
			get
			{
				return "groupLevels";
			}
		}

		// Token: 0x170084E8 RID: 34024
		// (get) Token: 0x06018836 RID: 100406 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084E9 RID: 34025
		// (get) Token: 0x06018837 RID: 100407 RVA: 0x003420EA File Offset: 0x003402EA
		internal override int ElementTypeId
		{
			get
			{
				return 11436;
			}
		}

		// Token: 0x06018838 RID: 100408 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084EA RID: 34026
		// (get) Token: 0x06018839 RID: 100409 RVA: 0x003420F1 File Offset: 0x003402F1
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupLevels.attributeTagNames;
			}
		}

		// Token: 0x170084EB RID: 34027
		// (get) Token: 0x0601883A RID: 100410 RVA: 0x003420F8 File Offset: 0x003402F8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupLevels.attributeNamespaceIds;
			}
		}

		// Token: 0x170084EC RID: 34028
		// (get) Token: 0x0601883B RID: 100411 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601883C RID: 100412 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601883D RID: 100413 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupLevels()
		{
		}

		// Token: 0x0601883E RID: 100414 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupLevels(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601883F RID: 100415 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupLevels(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018840 RID: 100416 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupLevels(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018841 RID: 100417 RVA: 0x003420FF File Offset: 0x003402FF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "groupLevel" == name)
			{
				return new GroupLevel();
			}
			return null;
		}

		// Token: 0x06018842 RID: 100418 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018843 RID: 100419 RVA: 0x0034211A File Offset: 0x0034031A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupLevels>(deep);
		}

		// Token: 0x06018844 RID: 100420 RVA: 0x00342124 File Offset: 0x00340324
		// Note: this type is marked as 'beforefieldinit'.
		static GroupLevels()
		{
			byte[] array = new byte[1];
			GroupLevels.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0A1 RID: 41121
		private const string tagName = "groupLevels";

		// Token: 0x0400A0A2 RID: 41122
		private const byte tagNsId = 22;

		// Token: 0x0400A0A3 RID: 41123
		internal const int ElementTypeIdConst = 11436;

		// Token: 0x0400A0A4 RID: 41124
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0A5 RID: 41125
		private static byte[] attributeNamespaceIds;
	}
}
