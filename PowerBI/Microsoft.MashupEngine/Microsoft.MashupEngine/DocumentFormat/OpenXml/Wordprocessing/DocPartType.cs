using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC8 RID: 12232
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartType : OpenXmlLeafElement
	{
		// Token: 0x1700940D RID: 37901
		// (get) Token: 0x0601A893 RID: 108691 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x1700940E RID: 37902
		// (get) Token: 0x0601A894 RID: 108692 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700940F RID: 37903
		// (get) Token: 0x0601A895 RID: 108693 RVA: 0x00363CC4 File Offset: 0x00361EC4
		internal override int ElementTypeId
		{
			get
			{
				return 11940;
			}
		}

		// Token: 0x0601A896 RID: 108694 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009410 RID: 37904
		// (get) Token: 0x0601A897 RID: 108695 RVA: 0x00363CCB File Offset: 0x00361ECB
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocPartType.attributeTagNames;
			}
		}

		// Token: 0x17009411 RID: 37905
		// (get) Token: 0x0601A898 RID: 108696 RVA: 0x00363CD2 File Offset: 0x00361ED2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocPartType.attributeNamespaceIds;
			}
		}

		// Token: 0x17009412 RID: 37906
		// (get) Token: 0x0601A899 RID: 108697 RVA: 0x00363CD9 File Offset: 0x00361ED9
		// (set) Token: 0x0601A89A RID: 108698 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<DocPartValues> Val
		{
			get
			{
				return (EnumValue<DocPartValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A89C RID: 108700 RVA: 0x00363CE8 File Offset: 0x00361EE8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<DocPartValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A89D RID: 108701 RVA: 0x00363D0A File Offset: 0x00361F0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartType>(deep);
		}

		// Token: 0x0400AD64 RID: 44388
		private const string tagName = "type";

		// Token: 0x0400AD65 RID: 44389
		private const byte tagNsId = 23;

		// Token: 0x0400AD66 RID: 44390
		internal const int ElementTypeIdConst = 11940;

		// Token: 0x0400AD67 RID: 44391
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD68 RID: 44392
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
