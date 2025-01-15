using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4B RID: 11851
	[GeneratedCode("DomGen", "2.0")]
	internal class DocGrid : OpenXmlLeafElement
	{
		// Token: 0x17008A1C RID: 35356
		// (get) Token: 0x0601930C RID: 103180 RVA: 0x00347634 File Offset: 0x00345834
		public override string LocalName
		{
			get
			{
				return "docGrid";
			}
		}

		// Token: 0x17008A1D RID: 35357
		// (get) Token: 0x0601930D RID: 103181 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A1E RID: 35358
		// (get) Token: 0x0601930E RID: 103182 RVA: 0x0034763B File Offset: 0x0034583B
		internal override int ElementTypeId
		{
			get
			{
				return 11541;
			}
		}

		// Token: 0x0601930F RID: 103183 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008A1F RID: 35359
		// (get) Token: 0x06019310 RID: 103184 RVA: 0x00347642 File Offset: 0x00345842
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocGrid.attributeTagNames;
			}
		}

		// Token: 0x17008A20 RID: 35360
		// (get) Token: 0x06019311 RID: 103185 RVA: 0x00347649 File Offset: 0x00345849
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocGrid.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A21 RID: 35361
		// (get) Token: 0x06019312 RID: 103186 RVA: 0x00347650 File Offset: 0x00345850
		// (set) Token: 0x06019313 RID: 103187 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<DocGridValues> Type
		{
			get
			{
				return (EnumValue<DocGridValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008A22 RID: 35362
		// (get) Token: 0x06019314 RID: 103188 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06019315 RID: 103189 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "linePitch")]
		public Int32Value LinePitch
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008A23 RID: 35363
		// (get) Token: 0x06019316 RID: 103190 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06019317 RID: 103191 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "charSpace")]
		public Int32Value CharacterSpace
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06019319 RID: 103193 RVA: 0x00347660 File Offset: 0x00345860
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<DocGridValues>();
			}
			if (23 == namespaceId && "linePitch" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "charSpace" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601931A RID: 103194 RVA: 0x003476BD File Offset: 0x003458BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocGrid>(deep);
		}

		// Token: 0x0400A77A RID: 42874
		private const string tagName = "docGrid";

		// Token: 0x0400A77B RID: 42875
		private const byte tagNsId = 23;

		// Token: 0x0400A77C RID: 42876
		internal const int ElementTypeIdConst = 11541;

		// Token: 0x0400A77D RID: 42877
		private static string[] attributeTagNames = new string[] { "type", "linePitch", "charSpace" };

		// Token: 0x0400A77E RID: 42878
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
