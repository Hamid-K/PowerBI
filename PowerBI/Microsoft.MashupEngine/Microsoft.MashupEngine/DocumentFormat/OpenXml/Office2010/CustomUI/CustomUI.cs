using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E5 RID: 8933
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Backstage), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Ribbon), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ContextMenus), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Commands), FileFormatVersions.Office2010)]
	internal class CustomUI : OpenXmlPartRootElement
	{
		// Token: 0x1700461F RID: 17951
		// (get) Token: 0x0600FB1F RID: 64287 RVA: 0x002ADC57 File Offset: 0x002ABE57
		public override string LocalName
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x17004620 RID: 17952
		// (get) Token: 0x0600FB20 RID: 64288 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004621 RID: 17953
		// (get) Token: 0x0600FB21 RID: 64289 RVA: 0x002DA597 File Offset: 0x002D8797
		internal override int ElementTypeId
		{
			get
			{
				return 13078;
			}
		}

		// Token: 0x0600FB22 RID: 64290 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004622 RID: 17954
		// (get) Token: 0x0600FB23 RID: 64291 RVA: 0x002DA59E File Offset: 0x002D879E
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomUI.attributeTagNames;
			}
		}

		// Token: 0x17004623 RID: 17955
		// (get) Token: 0x0600FB24 RID: 64292 RVA: 0x002DA5A5 File Offset: 0x002D87A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomUI.attributeNamespaceIds;
			}
		}

		// Token: 0x17004624 RID: 17956
		// (get) Token: 0x0600FB25 RID: 64293 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FB26 RID: 64294 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onLoad")]
		public StringValue OnLoad
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

		// Token: 0x17004625 RID: 17957
		// (get) Token: 0x0600FB27 RID: 64295 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FB28 RID: 64296 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "loadImage")]
		public StringValue LoadImage
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

		// Token: 0x0600FB29 RID: 64297 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal CustomUI(RibbonAndBackstageCustomizationsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0600FB2A RID: 64298 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(RibbonAndBackstageCustomizationsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004626 RID: 17958
		// (get) Token: 0x0600FB2B RID: 64299 RVA: 0x002DA5AC File Offset: 0x002D87AC
		// (set) Token: 0x0600FB2C RID: 64300 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public RibbonAndBackstageCustomizationsPart RibbonAndBackstageCustomizationsPart
		{
			get
			{
				return base.OpenXmlPart as RibbonAndBackstageCustomizationsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0600FB2D RID: 64301 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public CustomUI(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB2E RID: 64302 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public CustomUI(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB2F RID: 64303 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public CustomUI(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FB30 RID: 64304 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public CustomUI()
		{
		}

		// Token: 0x0600FB31 RID: 64305 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(RibbonAndBackstageCustomizationsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0600FB32 RID: 64306 RVA: 0x002DA5BC File Offset: 0x002D87BC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "commands" == name)
			{
				return new Commands();
			}
			if (57 == namespaceId && "ribbon" == name)
			{
				return new Ribbon();
			}
			if (57 == namespaceId && "backstage" == name)
			{
				return new Backstage();
			}
			if (57 == namespaceId && "contextMenus" == name)
			{
				return new ContextMenus();
			}
			return null;
		}

		// Token: 0x17004627 RID: 17959
		// (get) Token: 0x0600FB33 RID: 64307 RVA: 0x002DA62A File Offset: 0x002D882A
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomUI.eleTagNames;
			}
		}

		// Token: 0x17004628 RID: 17960
		// (get) Token: 0x0600FB34 RID: 64308 RVA: 0x002DA631 File Offset: 0x002D8831
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomUI.eleNamespaceIds;
			}
		}

		// Token: 0x17004629 RID: 17961
		// (get) Token: 0x0600FB35 RID: 64309 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700462A RID: 17962
		// (get) Token: 0x0600FB36 RID: 64310 RVA: 0x002DA638 File Offset: 0x002D8838
		// (set) Token: 0x0600FB37 RID: 64311 RVA: 0x002DA641 File Offset: 0x002D8841
		public Commands Commands
		{
			get
			{
				return base.GetElement<Commands>(0);
			}
			set
			{
				base.SetElement<Commands>(0, value);
			}
		}

		// Token: 0x1700462B RID: 17963
		// (get) Token: 0x0600FB38 RID: 64312 RVA: 0x002DA64B File Offset: 0x002D884B
		// (set) Token: 0x0600FB39 RID: 64313 RVA: 0x002DA654 File Offset: 0x002D8854
		public Ribbon Ribbon
		{
			get
			{
				return base.GetElement<Ribbon>(1);
			}
			set
			{
				base.SetElement<Ribbon>(1, value);
			}
		}

		// Token: 0x1700462C RID: 17964
		// (get) Token: 0x0600FB3A RID: 64314 RVA: 0x002DA65E File Offset: 0x002D885E
		// (set) Token: 0x0600FB3B RID: 64315 RVA: 0x002DA667 File Offset: 0x002D8867
		public Backstage Backstage
		{
			get
			{
				return base.GetElement<Backstage>(2);
			}
			set
			{
				base.SetElement<Backstage>(2, value);
			}
		}

		// Token: 0x1700462D RID: 17965
		// (get) Token: 0x0600FB3C RID: 64316 RVA: 0x002DA671 File Offset: 0x002D8871
		// (set) Token: 0x0600FB3D RID: 64317 RVA: 0x002DA67A File Offset: 0x002D887A
		public ContextMenus ContextMenus
		{
			get
			{
				return base.GetElement<ContextMenus>(3);
			}
			set
			{
				base.SetElement<ContextMenus>(3, value);
			}
		}

		// Token: 0x0600FB3E RID: 64318 RVA: 0x002CEB9A File Offset: 0x002CCD9A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "onLoad" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "loadImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FB3F RID: 64319 RVA: 0x002DA684 File Offset: 0x002D8884
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomUI>(deep);
		}

		// Token: 0x0600FB40 RID: 64320 RVA: 0x002DA690 File Offset: 0x002D8890
		// Note: this type is marked as 'beforefieldinit'.
		static CustomUI()
		{
			byte[] array = new byte[2];
			CustomUI.attributeNamespaceIds = array;
			CustomUI.eleTagNames = new string[] { "commands", "ribbon", "backstage", "contextMenus" };
			CustomUI.eleNamespaceIds = new byte[] { 57, 57, 57, 57 };
		}

		// Token: 0x040071A6 RID: 29094
		private const string tagName = "customUI";

		// Token: 0x040071A7 RID: 29095
		private const byte tagNsId = 57;

		// Token: 0x040071A8 RID: 29096
		internal const int ElementTypeIdConst = 13078;

		// Token: 0x040071A9 RID: 29097
		private static string[] attributeTagNames = new string[] { "onLoad", "loadImage" };

		// Token: 0x040071AA RID: 29098
		private static byte[] attributeNamespaceIds;

		// Token: 0x040071AB RID: 29099
		private static readonly string[] eleTagNames;

		// Token: 0x040071AC RID: 29100
		private static readonly byte[] eleNamespaceIds;
	}
}
