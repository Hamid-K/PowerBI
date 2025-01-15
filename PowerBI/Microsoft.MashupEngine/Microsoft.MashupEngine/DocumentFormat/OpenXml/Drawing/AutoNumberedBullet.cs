using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002751 RID: 10065
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoNumberedBullet : OpenXmlLeafElement
	{
		// Token: 0x1700609A RID: 24730
		// (get) Token: 0x060135E5 RID: 79333 RVA: 0x003065B9 File Offset: 0x003047B9
		public override string LocalName
		{
			get
			{
				return "buAutoNum";
			}
		}

		// Token: 0x1700609B RID: 24731
		// (get) Token: 0x060135E6 RID: 79334 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700609C RID: 24732
		// (get) Token: 0x060135E7 RID: 79335 RVA: 0x003065C0 File Offset: 0x003047C0
		internal override int ElementTypeId
		{
			get
			{
				return 10110;
			}
		}

		// Token: 0x060135E8 RID: 79336 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700609D RID: 24733
		// (get) Token: 0x060135E9 RID: 79337 RVA: 0x003065C7 File Offset: 0x003047C7
		internal override string[] AttributeTagNames
		{
			get
			{
				return AutoNumberedBullet.attributeTagNames;
			}
		}

		// Token: 0x1700609E RID: 24734
		// (get) Token: 0x060135EA RID: 79338 RVA: 0x003065CE File Offset: 0x003047CE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AutoNumberedBullet.attributeNamespaceIds;
			}
		}

		// Token: 0x1700609F RID: 24735
		// (get) Token: 0x060135EB RID: 79339 RVA: 0x003065D5 File Offset: 0x003047D5
		// (set) Token: 0x060135EC RID: 79340 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<TextAutoNumberSchemeValues> Type
		{
			get
			{
				return (EnumValue<TextAutoNumberSchemeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170060A0 RID: 24736
		// (get) Token: 0x060135ED RID: 79341 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060135EE RID: 79342 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "startAt")]
		public Int32Value StartAt
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

		// Token: 0x060135F0 RID: 79344 RVA: 0x003065E4 File Offset: 0x003047E4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<TextAutoNumberSchemeValues>();
			}
			if (namespaceId == 0 && "startAt" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060135F1 RID: 79345 RVA: 0x0030661A File Offset: 0x0030481A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoNumberedBullet>(deep);
		}

		// Token: 0x060135F2 RID: 79346 RVA: 0x00306624 File Offset: 0x00304824
		// Note: this type is marked as 'beforefieldinit'.
		static AutoNumberedBullet()
		{
			byte[] array = new byte[2];
			AutoNumberedBullet.attributeNamespaceIds = array;
		}

		// Token: 0x040085E4 RID: 34276
		private const string tagName = "buAutoNum";

		// Token: 0x040085E5 RID: 34277
		private const byte tagNsId = 10;

		// Token: 0x040085E6 RID: 34278
		internal const int ElementTypeIdConst = 10110;

		// Token: 0x040085E7 RID: 34279
		private static string[] attributeTagNames = new string[] { "type", "startAt" };

		// Token: 0x040085E8 RID: 34280
		private static byte[] attributeNamespaceIds;
	}
}
