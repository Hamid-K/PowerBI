using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A0 RID: 10656
	[GeneratedCode("DomGen", "2.0")]
	internal class Shape : OpenXmlLeafElement
	{
		// Token: 0x17006D16 RID: 27926
		// (get) Token: 0x060152E7 RID: 86759 RVA: 0x0031C90E File Offset: 0x0031AB0E
		public override string LocalName
		{
			get
			{
				return "shp";
			}
		}

		// Token: 0x17006D17 RID: 27927
		// (get) Token: 0x060152E8 RID: 86760 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D18 RID: 27928
		// (get) Token: 0x060152E9 RID: 86761 RVA: 0x0031C915 File Offset: 0x0031AB15
		internal override int ElementTypeId
		{
			get
			{
				return 10893;
			}
		}

		// Token: 0x060152EA RID: 86762 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006D19 RID: 27929
		// (get) Token: 0x060152EB RID: 86763 RVA: 0x0031C91C File Offset: 0x0031AB1C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x17006D1A RID: 27930
		// (get) Token: 0x060152EC RID: 86764 RVA: 0x0031C923 File Offset: 0x0031AB23
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D1B RID: 27931
		// (get) Token: 0x060152ED RID: 86765 RVA: 0x0031C92A File Offset: 0x0031AB2A
		// (set) Token: 0x060152EE RID: 86766 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<ShapeDelimiterValues> Val
		{
			get
			{
				return (EnumValue<ShapeDelimiterValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060152F0 RID: 86768 RVA: 0x0031C939 File Offset: 0x0031AB39
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<ShapeDelimiterValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060152F1 RID: 86769 RVA: 0x0031C95B File Offset: 0x0031AB5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x040091F7 RID: 37367
		private const string tagName = "shp";

		// Token: 0x040091F8 RID: 37368
		private const byte tagNsId = 21;

		// Token: 0x040091F9 RID: 37369
		internal const int ElementTypeIdConst = 10893;

		// Token: 0x040091FA RID: 37370
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040091FB RID: 37371
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
