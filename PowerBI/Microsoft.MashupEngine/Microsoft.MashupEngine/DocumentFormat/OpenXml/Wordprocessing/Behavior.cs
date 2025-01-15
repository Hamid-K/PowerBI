using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC7 RID: 12231
	[GeneratedCode("DomGen", "2.0")]
	internal class Behavior : OpenXmlLeafElement
	{
		// Token: 0x17009407 RID: 37895
		// (get) Token: 0x0601A887 RID: 108679 RVA: 0x00363C38 File Offset: 0x00361E38
		public override string LocalName
		{
			get
			{
				return "behavior";
			}
		}

		// Token: 0x17009408 RID: 37896
		// (get) Token: 0x0601A888 RID: 108680 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009409 RID: 37897
		// (get) Token: 0x0601A889 RID: 108681 RVA: 0x00363C3F File Offset: 0x00361E3F
		internal override int ElementTypeId
		{
			get
			{
				return 11939;
			}
		}

		// Token: 0x0601A88A RID: 108682 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700940A RID: 37898
		// (get) Token: 0x0601A88B RID: 108683 RVA: 0x00363C46 File Offset: 0x00361E46
		internal override string[] AttributeTagNames
		{
			get
			{
				return Behavior.attributeTagNames;
			}
		}

		// Token: 0x1700940B RID: 37899
		// (get) Token: 0x0601A88C RID: 108684 RVA: 0x00363C4D File Offset: 0x00361E4D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Behavior.attributeNamespaceIds;
			}
		}

		// Token: 0x1700940C RID: 37900
		// (get) Token: 0x0601A88D RID: 108685 RVA: 0x00363C54 File Offset: 0x00361E54
		// (set) Token: 0x0601A88E RID: 108686 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DocPartBehaviorValues> Val
		{
			get
			{
				return (EnumValue<DocPartBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A890 RID: 108688 RVA: 0x00363C63 File Offset: 0x00361E63
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DocPartBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A891 RID: 108689 RVA: 0x00363C85 File Offset: 0x00361E85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Behavior>(deep);
		}

		// Token: 0x0400AD5F RID: 44383
		private const string tagName = "behavior";

		// Token: 0x0400AD60 RID: 44384
		private const byte tagNsId = 23;

		// Token: 0x0400AD61 RID: 44385
		internal const int ElementTypeIdConst = 11939;

		// Token: 0x0400AD62 RID: 44386
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD63 RID: 44387
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
