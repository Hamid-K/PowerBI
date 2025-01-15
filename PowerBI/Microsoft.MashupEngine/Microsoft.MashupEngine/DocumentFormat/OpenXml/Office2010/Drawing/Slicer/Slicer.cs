using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Slicer
{
	// Token: 0x0200237B RID: 9083
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Slicer : OpenXmlCompositeElement
	{
		// Token: 0x17004B29 RID: 19241
		// (get) Token: 0x06010612 RID: 67090 RVA: 0x002AED9A File Offset: 0x002ACF9A
		public override string LocalName
		{
			get
			{
				return "slicer";
			}
		}

		// Token: 0x17004B2A RID: 19242
		// (get) Token: 0x06010613 RID: 67091 RVA: 0x002E2DA8 File Offset: 0x002E0FA8
		internal override byte NamespaceId
		{
			get
			{
				return 62;
			}
		}

		// Token: 0x17004B2B RID: 19243
		// (get) Token: 0x06010614 RID: 67092 RVA: 0x002E2DAC File Offset: 0x002E0FAC
		internal override int ElementTypeId
		{
			get
			{
				return 13141;
			}
		}

		// Token: 0x06010615 RID: 67093 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B2C RID: 19244
		// (get) Token: 0x06010616 RID: 67094 RVA: 0x002E2DB3 File Offset: 0x002E0FB3
		internal override string[] AttributeTagNames
		{
			get
			{
				return Slicer.attributeTagNames;
			}
		}

		// Token: 0x17004B2D RID: 19245
		// (get) Token: 0x06010617 RID: 67095 RVA: 0x002E2DBA File Offset: 0x002E0FBA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Slicer.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B2E RID: 19246
		// (get) Token: 0x06010618 RID: 67096 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010619 RID: 67097 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x0601061A RID: 67098 RVA: 0x00293ECF File Offset: 0x002920CF
		public Slicer()
		{
		}

		// Token: 0x0601061B RID: 67099 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Slicer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601061C RID: 67100 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Slicer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601061D RID: 67101 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Slicer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601061E RID: 67102 RVA: 0x002E2DC1 File Offset: 0x002E0FC1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (62 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004B2F RID: 19247
		// (get) Token: 0x0601061F RID: 67103 RVA: 0x002E2DDC File Offset: 0x002E0FDC
		internal override string[] ElementTagNames
		{
			get
			{
				return Slicer.eleTagNames;
			}
		}

		// Token: 0x17004B30 RID: 19248
		// (get) Token: 0x06010620 RID: 67104 RVA: 0x002E2DE3 File Offset: 0x002E0FE3
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Slicer.eleNamespaceIds;
			}
		}

		// Token: 0x17004B31 RID: 19249
		// (get) Token: 0x06010621 RID: 67105 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B32 RID: 19250
		// (get) Token: 0x06010622 RID: 67106 RVA: 0x002E2DEA File Offset: 0x002E0FEA
		// (set) Token: 0x06010623 RID: 67107 RVA: 0x002E2DF3 File Offset: 0x002E0FF3
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(0);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(0, value);
			}
		}

		// Token: 0x06010624 RID: 67108 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010625 RID: 67109 RVA: 0x002E2DFD File Offset: 0x002E0FFD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Slicer>(deep);
		}

		// Token: 0x06010626 RID: 67110 RVA: 0x002E2E08 File Offset: 0x002E1008
		// Note: this type is marked as 'beforefieldinit'.
		static Slicer()
		{
			byte[] array = new byte[1];
			Slicer.attributeNamespaceIds = array;
			Slicer.eleTagNames = new string[] { "extLst" };
			Slicer.eleNamespaceIds = new byte[] { 62 };
		}

		// Token: 0x0400745A RID: 29786
		private const string tagName = "slicer";

		// Token: 0x0400745B RID: 29787
		private const byte tagNsId = 62;

		// Token: 0x0400745C RID: 29788
		internal const int ElementTypeIdConst = 13141;

		// Token: 0x0400745D RID: 29789
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400745E RID: 29790
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400745F RID: 29791
		private static readonly string[] eleTagNames;

		// Token: 0x04007460 RID: 29792
		private static readonly byte[] eleNamespaceIds;
	}
}
