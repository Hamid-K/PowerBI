using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9C RID: 10908
	[ChildElementInfo(typeof(BackgroundStyleReference))]
	[ChildElementInfo(typeof(BackgroundProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Background : OpenXmlCompositeElement
	{
		// Token: 0x17007420 RID: 29728
		// (get) Token: 0x0601627A RID: 90746 RVA: 0x002EF0F4 File Offset: 0x002ED2F4
		public override string LocalName
		{
			get
			{
				return "bg";
			}
		}

		// Token: 0x17007421 RID: 29729
		// (get) Token: 0x0601627B RID: 90747 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007422 RID: 29730
		// (get) Token: 0x0601627C RID: 90748 RVA: 0x00327076 File Offset: 0x00325276
		internal override int ElementTypeId
		{
			get
			{
				return 12323;
			}
		}

		// Token: 0x0601627D RID: 90749 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007423 RID: 29731
		// (get) Token: 0x0601627E RID: 90750 RVA: 0x0032707D File Offset: 0x0032527D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Background.attributeTagNames;
			}
		}

		// Token: 0x17007424 RID: 29732
		// (get) Token: 0x0601627F RID: 90751 RVA: 0x00327084 File Offset: 0x00325284
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Background.attributeNamespaceIds;
			}
		}

		// Token: 0x17007425 RID: 29733
		// (get) Token: 0x06016280 RID: 90752 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06016281 RID: 90753 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016282 RID: 90754 RVA: 0x00293ECF File Offset: 0x002920CF
		public Background()
		{
		}

		// Token: 0x06016283 RID: 90755 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Background(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016284 RID: 90756 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Background(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016285 RID: 90757 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Background(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016286 RID: 90758 RVA: 0x0032708B File Offset: 0x0032528B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "bgPr" == name)
			{
				return new BackgroundProperties();
			}
			if (24 == namespaceId && "bgRef" == name)
			{
				return new BackgroundStyleReference();
			}
			return null;
		}

		// Token: 0x17007426 RID: 29734
		// (get) Token: 0x06016287 RID: 90759 RVA: 0x003270BE File Offset: 0x003252BE
		internal override string[] ElementTagNames
		{
			get
			{
				return Background.eleTagNames;
			}
		}

		// Token: 0x17007427 RID: 29735
		// (get) Token: 0x06016288 RID: 90760 RVA: 0x003270C5 File Offset: 0x003252C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Background.eleNamespaceIds;
			}
		}

		// Token: 0x17007428 RID: 29736
		// (get) Token: 0x06016289 RID: 90761 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007429 RID: 29737
		// (get) Token: 0x0601628A RID: 90762 RVA: 0x003270CC File Offset: 0x003252CC
		// (set) Token: 0x0601628B RID: 90763 RVA: 0x003270D5 File Offset: 0x003252D5
		public BackgroundProperties BackgroundProperties
		{
			get
			{
				return base.GetElement<BackgroundProperties>(0);
			}
			set
			{
				base.SetElement<BackgroundProperties>(0, value);
			}
		}

		// Token: 0x1700742A RID: 29738
		// (get) Token: 0x0601628C RID: 90764 RVA: 0x003270DF File Offset: 0x003252DF
		// (set) Token: 0x0601628D RID: 90765 RVA: 0x003270E8 File Offset: 0x003252E8
		public BackgroundStyleReference BackgroundStyleReference
		{
			get
			{
				return base.GetElement<BackgroundStyleReference>(1);
			}
			set
			{
				base.SetElement<BackgroundStyleReference>(1, value);
			}
		}

		// Token: 0x0601628E RID: 90766 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601628F RID: 90767 RVA: 0x003270F2 File Offset: 0x003252F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Background>(deep);
		}

		// Token: 0x06016290 RID: 90768 RVA: 0x003270FC File Offset: 0x003252FC
		// Note: this type is marked as 'beforefieldinit'.
		static Background()
		{
			byte[] array = new byte[1];
			Background.attributeNamespaceIds = array;
			Background.eleTagNames = new string[] { "bgPr", "bgRef" };
			Background.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009677 RID: 38519
		private const string tagName = "bg";

		// Token: 0x04009678 RID: 38520
		private const byte tagNsId = 24;

		// Token: 0x04009679 RID: 38521
		internal const int ElementTypeIdConst = 12323;

		// Token: 0x0400967A RID: 38522
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x0400967B RID: 38523
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400967C RID: 38524
		private static readonly string[] eleTagNames;

		// Token: 0x0400967D RID: 38525
		private static readonly byte[] eleNamespaceIds;
	}
}
