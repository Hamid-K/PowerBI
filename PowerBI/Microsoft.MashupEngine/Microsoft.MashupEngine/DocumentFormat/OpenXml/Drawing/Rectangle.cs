using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D7 RID: 10199
	[GeneratedCode("DomGen", "2.0")]
	internal class Rectangle : OpenXmlLeafElement
	{
		// Token: 0x170063E7 RID: 25575
		// (get) Token: 0x06013D67 RID: 81255 RVA: 0x002C6E31 File Offset: 0x002C5031
		public override string LocalName
		{
			get
			{
				return "rect";
			}
		}

		// Token: 0x170063E8 RID: 25576
		// (get) Token: 0x06013D68 RID: 81256 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063E9 RID: 25577
		// (get) Token: 0x06013D69 RID: 81257 RVA: 0x0030C268 File Offset: 0x0030A468
		internal override int ElementTypeId
		{
			get
			{
				return 10232;
			}
		}

		// Token: 0x06013D6A RID: 81258 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170063EA RID: 25578
		// (get) Token: 0x06013D6B RID: 81259 RVA: 0x0030C26F File Offset: 0x0030A46F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rectangle.attributeTagNames;
			}
		}

		// Token: 0x170063EB RID: 25579
		// (get) Token: 0x06013D6C RID: 81260 RVA: 0x0030C276 File Offset: 0x0030A476
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rectangle.attributeNamespaceIds;
			}
		}

		// Token: 0x170063EC RID: 25580
		// (get) Token: 0x06013D6D RID: 81261 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013D6E RID: 81262 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "l")]
		public StringValue Left
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

		// Token: 0x170063ED RID: 25581
		// (get) Token: 0x06013D6F RID: 81263 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013D70 RID: 81264 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "t")]
		public StringValue Top
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

		// Token: 0x170063EE RID: 25582
		// (get) Token: 0x06013D71 RID: 81265 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06013D72 RID: 81266 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "r")]
		public StringValue Right
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170063EF RID: 25583
		// (get) Token: 0x06013D73 RID: 81267 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06013D74 RID: 81268 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "b")]
		public StringValue Bottom
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06013D76 RID: 81270 RVA: 0x0030C280 File Offset: 0x0030A480
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "l" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013D77 RID: 81271 RVA: 0x0030C2ED File Offset: 0x0030A4ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rectangle>(deep);
		}

		// Token: 0x06013D78 RID: 81272 RVA: 0x0030C2F8 File Offset: 0x0030A4F8
		// Note: this type is marked as 'beforefieldinit'.
		static Rectangle()
		{
			byte[] array = new byte[4];
			Rectangle.attributeNamespaceIds = array;
		}

		// Token: 0x04008802 RID: 34818
		private const string tagName = "rect";

		// Token: 0x04008803 RID: 34819
		private const byte tagNsId = 10;

		// Token: 0x04008804 RID: 34820
		internal const int ElementTypeIdConst = 10232;

		// Token: 0x04008805 RID: 34821
		private static string[] attributeTagNames = new string[] { "l", "t", "r", "b" };

		// Token: 0x04008806 RID: 34822
		private static byte[] attributeNamespaceIds;
	}
}
