using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002600 RID: 9728
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorMapOverride : OpenXmlCompositeElement
	{
		// Token: 0x1700598E RID: 22926
		// (get) Token: 0x06012639 RID: 75321 RVA: 0x002FA64B File Offset: 0x002F884B
		public override string LocalName
		{
			get
			{
				return "clrMapOvr";
			}
		}

		// Token: 0x1700598F RID: 22927
		// (get) Token: 0x0601263A RID: 75322 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005990 RID: 22928
		// (get) Token: 0x0601263B RID: 75323 RVA: 0x002FA652 File Offset: 0x002F8852
		internal override int ElementTypeId
		{
			get
			{
				return 10575;
			}
		}

		// Token: 0x0601263C RID: 75324 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005991 RID: 22929
		// (get) Token: 0x0601263D RID: 75325 RVA: 0x002FA659 File Offset: 0x002F8859
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorMapOverride.attributeTagNames;
			}
		}

		// Token: 0x17005992 RID: 22930
		// (get) Token: 0x0601263E RID: 75326 RVA: 0x002FA660 File Offset: 0x002F8860
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorMapOverride.attributeNamespaceIds;
			}
		}

		// Token: 0x17005993 RID: 22931
		// (get) Token: 0x0601263F RID: 75327 RVA: 0x002FA667 File Offset: 0x002F8867
		// (set) Token: 0x06012640 RID: 75328 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bg1")]
		public EnumValue<ColorSchemeIndexValues> Background1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005994 RID: 22932
		// (get) Token: 0x06012641 RID: 75329 RVA: 0x002FA676 File Offset: 0x002F8876
		// (set) Token: 0x06012642 RID: 75330 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "tx1")]
		public EnumValue<ColorSchemeIndexValues> Text1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005995 RID: 22933
		// (get) Token: 0x06012643 RID: 75331 RVA: 0x002FA685 File Offset: 0x002F8885
		// (set) Token: 0x06012644 RID: 75332 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "bg2")]
		public EnumValue<ColorSchemeIndexValues> Background2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005996 RID: 22934
		// (get) Token: 0x06012645 RID: 75333 RVA: 0x002FA694 File Offset: 0x002F8894
		// (set) Token: 0x06012646 RID: 75334 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tx2")]
		public EnumValue<ColorSchemeIndexValues> Text2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005997 RID: 22935
		// (get) Token: 0x06012647 RID: 75335 RVA: 0x002FA6A3 File Offset: 0x002F88A3
		// (set) Token: 0x06012648 RID: 75336 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "accent1")]
		public EnumValue<ColorSchemeIndexValues> Accent1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005998 RID: 22936
		// (get) Token: 0x06012649 RID: 75337 RVA: 0x002FA6B2 File Offset: 0x002F88B2
		// (set) Token: 0x0601264A RID: 75338 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "accent2")]
		public EnumValue<ColorSchemeIndexValues> Accent2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005999 RID: 22937
		// (get) Token: 0x0601264B RID: 75339 RVA: 0x002FA6C1 File Offset: 0x002F88C1
		// (set) Token: 0x0601264C RID: 75340 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "accent3")]
		public EnumValue<ColorSchemeIndexValues> Accent3
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700599A RID: 22938
		// (get) Token: 0x0601264D RID: 75341 RVA: 0x002FA6D0 File Offset: 0x002F88D0
		// (set) Token: 0x0601264E RID: 75342 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "accent4")]
		public EnumValue<ColorSchemeIndexValues> Accent4
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700599B RID: 22939
		// (get) Token: 0x0601264F RID: 75343 RVA: 0x002FA6DF File Offset: 0x002F88DF
		// (set) Token: 0x06012650 RID: 75344 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "accent5")]
		public EnumValue<ColorSchemeIndexValues> Accent5
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700599C RID: 22940
		// (get) Token: 0x06012651 RID: 75345 RVA: 0x002FA6EE File Offset: 0x002F88EE
		// (set) Token: 0x06012652 RID: 75346 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "accent6")]
		public EnumValue<ColorSchemeIndexValues> Accent6
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700599D RID: 22941
		// (get) Token: 0x06012653 RID: 75347 RVA: 0x002FA6FE File Offset: 0x002F88FE
		// (set) Token: 0x06012654 RID: 75348 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "hlink")]
		public EnumValue<ColorSchemeIndexValues> Hyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700599E RID: 22942
		// (get) Token: 0x06012655 RID: 75349 RVA: 0x002FA70E File Offset: 0x002F890E
		// (set) Token: 0x06012656 RID: 75350 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "folHlink")]
		public EnumValue<ColorSchemeIndexValues> FollowedHyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06012657 RID: 75351 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorMapOverride()
		{
		}

		// Token: 0x06012658 RID: 75352 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorMapOverride(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012659 RID: 75353 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorMapOverride(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601265A RID: 75354 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorMapOverride(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601265B RID: 75355 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700599F RID: 22943
		// (get) Token: 0x0601265C RID: 75356 RVA: 0x002FA739 File Offset: 0x002F8939
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorMapOverride.eleTagNames;
			}
		}

		// Token: 0x170059A0 RID: 22944
		// (get) Token: 0x0601265D RID: 75357 RVA: 0x002FA740 File Offset: 0x002F8940
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorMapOverride.eleNamespaceIds;
			}
		}

		// Token: 0x170059A1 RID: 22945
		// (get) Token: 0x0601265E RID: 75358 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059A2 RID: 22946
		// (get) Token: 0x0601265F RID: 75359 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06012660 RID: 75360 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06012661 RID: 75361 RVA: 0x002FA75C File Offset: 0x002F895C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bg1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "tx1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "bg2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "tx2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent3" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent4" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent5" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent6" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "hlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "folHlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012662 RID: 75362 RVA: 0x002FA879 File Offset: 0x002F8A79
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMapOverride>(deep);
		}

		// Token: 0x06012663 RID: 75363 RVA: 0x002FA884 File Offset: 0x002F8A84
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMapOverride()
		{
			byte[] array = new byte[12];
			ColorMapOverride.attributeNamespaceIds = array;
			ColorMapOverride.eleTagNames = new string[] { "extLst" };
			ColorMapOverride.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04007F64 RID: 32612
		private const string tagName = "clrMapOvr";

		// Token: 0x04007F65 RID: 32613
		private const byte tagNsId = 11;

		// Token: 0x04007F66 RID: 32614
		internal const int ElementTypeIdConst = 10575;

		// Token: 0x04007F67 RID: 32615
		private static string[] attributeTagNames = new string[]
		{
			"bg1", "tx1", "bg2", "tx2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
			"hlink", "folHlink"
		};

		// Token: 0x04007F68 RID: 32616
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007F69 RID: 32617
		private static readonly string[] eleTagNames;

		// Token: 0x04007F6A RID: 32618
		private static readonly byte[] eleNamespaceIds;
	}
}
