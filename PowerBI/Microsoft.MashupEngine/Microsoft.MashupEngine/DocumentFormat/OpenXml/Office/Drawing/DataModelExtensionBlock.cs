using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002325 RID: 8997
	[GeneratedCode("DomGen", "2.0")]
	internal class DataModelExtensionBlock : OpenXmlLeafElement
	{
		// Token: 0x17004873 RID: 18547
		// (get) Token: 0x0601003B RID: 65595 RVA: 0x002DE884 File Offset: 0x002DCA84
		public override string LocalName
		{
			get
			{
				return "dataModelExt";
			}
		}

		// Token: 0x17004874 RID: 18548
		// (get) Token: 0x0601003C RID: 65596 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x17004875 RID: 18549
		// (get) Token: 0x0601003D RID: 65597 RVA: 0x002DE88B File Offset: 0x002DCA8B
		internal override int ElementTypeId
		{
			get
			{
				return 13020;
			}
		}

		// Token: 0x0601003E RID: 65598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004876 RID: 18550
		// (get) Token: 0x0601003F RID: 65599 RVA: 0x002DE892 File Offset: 0x002DCA92
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataModelExtensionBlock.attributeTagNames;
			}
		}

		// Token: 0x17004877 RID: 18551
		// (get) Token: 0x06010040 RID: 65600 RVA: 0x002DE899 File Offset: 0x002DCA99
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataModelExtensionBlock.attributeNamespaceIds;
			}
		}

		// Token: 0x17004878 RID: 18552
		// (get) Token: 0x06010041 RID: 65601 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010042 RID: 65602 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "relId")]
		public StringValue RelId
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

		// Token: 0x17004879 RID: 18553
		// (get) Token: 0x06010043 RID: 65603 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010044 RID: 65604 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minVer")]
		public StringValue MinVer
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

		// Token: 0x06010046 RID: 65606 RVA: 0x002DE8A0 File Offset: 0x002DCAA0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "relId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minVer" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010047 RID: 65607 RVA: 0x002DE8D6 File Offset: 0x002DCAD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataModelExtensionBlock>(deep);
		}

		// Token: 0x06010048 RID: 65608 RVA: 0x002DE8E0 File Offset: 0x002DCAE0
		// Note: this type is marked as 'beforefieldinit'.
		static DataModelExtensionBlock()
		{
			byte[] array = new byte[2];
			DataModelExtensionBlock.attributeNamespaceIds = array;
		}

		// Token: 0x040072B5 RID: 29365
		private const string tagName = "dataModelExt";

		// Token: 0x040072B6 RID: 29366
		private const byte tagNsId = 56;

		// Token: 0x040072B7 RID: 29367
		internal const int ElementTypeIdConst = 13020;

		// Token: 0x040072B8 RID: 29368
		private static string[] attributeTagNames = new string[] { "relId", "minVer" };

		// Token: 0x040072B9 RID: 29369
		private static byte[] attributeNamespaceIds;
	}
}
