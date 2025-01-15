using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002378 RID: 9080
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageEffect), FileFormatVersions.Office2010)]
	internal class ImageLayer : OpenXmlCompositeElement
	{
		// Token: 0x17004B16 RID: 19222
		// (get) Token: 0x060105E2 RID: 67042 RVA: 0x002E2C1E File Offset: 0x002E0E1E
		public override string LocalName
		{
			get
			{
				return "imgLayer";
			}
		}

		// Token: 0x17004B17 RID: 19223
		// (get) Token: 0x060105E3 RID: 67043 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004B18 RID: 19224
		// (get) Token: 0x060105E4 RID: 67044 RVA: 0x002E2C25 File Offset: 0x002E0E25
		internal override int ElementTypeId
		{
			get
			{
				return 12763;
			}
		}

		// Token: 0x060105E5 RID: 67045 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B19 RID: 19225
		// (get) Token: 0x060105E6 RID: 67046 RVA: 0x002E2C2C File Offset: 0x002E0E2C
		internal override string[] AttributeTagNames
		{
			get
			{
				return ImageLayer.attributeTagNames;
			}
		}

		// Token: 0x17004B1A RID: 19226
		// (get) Token: 0x060105E7 RID: 67047 RVA: 0x002E2C33 File Offset: 0x002E0E33
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ImageLayer.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B1B RID: 19227
		// (get) Token: 0x060105E8 RID: 67048 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060105E9 RID: 67049 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "embed")]
		public StringValue Embed
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

		// Token: 0x060105EA RID: 67050 RVA: 0x00293ECF File Offset: 0x002920CF
		public ImageLayer()
		{
		}

		// Token: 0x060105EB RID: 67051 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ImageLayer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105EC RID: 67052 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ImageLayer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105ED RID: 67053 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ImageLayer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060105EE RID: 67054 RVA: 0x002E2C3A File Offset: 0x002E0E3A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "imgEffect" == name)
			{
				return new ImageEffect();
			}
			return null;
		}

		// Token: 0x060105EF RID: 67055 RVA: 0x002E2C55 File Offset: 0x002E0E55
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "embed" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060105F0 RID: 67056 RVA: 0x002E2C77 File Offset: 0x002E0E77
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageLayer>(deep);
		}

		// Token: 0x0400744D RID: 29773
		private const string tagName = "imgLayer";

		// Token: 0x0400744E RID: 29774
		private const byte tagNsId = 48;

		// Token: 0x0400744F RID: 29775
		internal const int ElementTypeIdConst = 12763;

		// Token: 0x04007450 RID: 29776
		private static string[] attributeTagNames = new string[] { "embed" };

		// Token: 0x04007451 RID: 29777
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
