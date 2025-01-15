using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F74 RID: 12148
	[GeneratedCode("DomGen", "2.0")]
	internal class DataType : OpenXmlLeafElement
	{
		// Token: 0x17009107 RID: 37127
		// (get) Token: 0x0601A22F RID: 107055 RVA: 0x0035DDF0 File Offset: 0x0035BFF0
		public override string LocalName
		{
			get
			{
				return "dataType";
			}
		}

		// Token: 0x17009108 RID: 37128
		// (get) Token: 0x0601A230 RID: 107056 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009109 RID: 37129
		// (get) Token: 0x0601A231 RID: 107057 RVA: 0x0035DDF7 File Offset: 0x0035BFF7
		internal override int ElementTypeId
		{
			get
			{
				return 11814;
			}
		}

		// Token: 0x0601A232 RID: 107058 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700910A RID: 37130
		// (get) Token: 0x0601A233 RID: 107059 RVA: 0x0035DDFE File Offset: 0x0035BFFE
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataType.attributeTagNames;
			}
		}

		// Token: 0x1700910B RID: 37131
		// (get) Token: 0x0601A234 RID: 107060 RVA: 0x0035DE05 File Offset: 0x0035C005
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700910C RID: 37132
		// (get) Token: 0x0601A235 RID: 107061 RVA: 0x0035DE0C File Offset: 0x0035C00C
		// (set) Token: 0x0601A236 RID: 107062 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MailMergeDataValues> Val
		{
			get
			{
				return (EnumValue<MailMergeDataValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A238 RID: 107064 RVA: 0x0035DE1B File Offset: 0x0035C01B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MailMergeDataValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A239 RID: 107065 RVA: 0x0035DE3D File Offset: 0x0035C03D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataType>(deep);
		}

		// Token: 0x0400ABF9 RID: 44025
		private const string tagName = "dataType";

		// Token: 0x0400ABFA RID: 44026
		private const byte tagNsId = 23;

		// Token: 0x0400ABFB RID: 44027
		internal const int ElementTypeIdConst = 11814;

		// Token: 0x0400ABFC RID: 44028
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABFD RID: 44029
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
