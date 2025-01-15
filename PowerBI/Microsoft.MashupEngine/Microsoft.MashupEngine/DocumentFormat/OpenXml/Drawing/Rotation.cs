using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B1 RID: 10161
	[GeneratedCode("DomGen", "2.0")]
	internal class Rotation : OpenXmlLeafElement
	{
		// Token: 0x1700630A RID: 25354
		// (get) Token: 0x06013B7A RID: 80762 RVA: 0x002EEA02 File Offset: 0x002ECC02
		public override string LocalName
		{
			get
			{
				return "rot";
			}
		}

		// Token: 0x1700630B RID: 25355
		// (get) Token: 0x06013B7B RID: 80763 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700630C RID: 25356
		// (get) Token: 0x06013B7C RID: 80764 RVA: 0x0030B015 File Offset: 0x00309215
		internal override int ElementTypeId
		{
			get
			{
				return 10194;
			}
		}

		// Token: 0x06013B7D RID: 80765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700630D RID: 25357
		// (get) Token: 0x06013B7E RID: 80766 RVA: 0x0030B01C File Offset: 0x0030921C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rotation.attributeTagNames;
			}
		}

		// Token: 0x1700630E RID: 25358
		// (get) Token: 0x06013B7F RID: 80767 RVA: 0x0030B023 File Offset: 0x00309223
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rotation.attributeNamespaceIds;
			}
		}

		// Token: 0x1700630F RID: 25359
		// (get) Token: 0x06013B80 RID: 80768 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013B81 RID: 80769 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lat")]
		public Int32Value Latitude
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006310 RID: 25360
		// (get) Token: 0x06013B82 RID: 80770 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013B83 RID: 80771 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lon")]
		public Int32Value Longitude
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

		// Token: 0x17006311 RID: 25361
		// (get) Token: 0x06013B84 RID: 80772 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013B85 RID: 80773 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "rev")]
		public Int32Value Revolution
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

		// Token: 0x06013B87 RID: 80775 RVA: 0x0030B02C File Offset: 0x0030922C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lat" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "lon" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "rev" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013B88 RID: 80776 RVA: 0x0030B083 File Offset: 0x00309283
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rotation>(deep);
		}

		// Token: 0x06013B89 RID: 80777 RVA: 0x0030B08C File Offset: 0x0030928C
		// Note: this type is marked as 'beforefieldinit'.
		static Rotation()
		{
			byte[] array = new byte[3];
			Rotation.attributeNamespaceIds = array;
		}

		// Token: 0x0400876F RID: 34671
		private const string tagName = "rot";

		// Token: 0x04008770 RID: 34672
		private const byte tagNsId = 10;

		// Token: 0x04008771 RID: 34673
		internal const int ElementTypeIdConst = 10194;

		// Token: 0x04008772 RID: 34674
		private static string[] attributeTagNames = new string[] { "lat", "lon", "rev" };

		// Token: 0x04008773 RID: 34675
		private static byte[] attributeNamespaceIds;
	}
}
