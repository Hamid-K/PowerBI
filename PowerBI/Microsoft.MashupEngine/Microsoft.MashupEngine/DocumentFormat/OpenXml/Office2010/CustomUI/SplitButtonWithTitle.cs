using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C3 RID: 8899
	[ChildElementInfo(typeof(VisibleToggleButton), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuWithTitle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VisibleButton), FileFormatVersions.Office2010)]
	internal class SplitButtonWithTitle : OpenXmlCompositeElement
	{
		// Token: 0x170042AC RID: 17068
		// (get) Token: 0x0600F3F1 RID: 62449 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x170042AD RID: 17069
		// (get) Token: 0x0600F3F2 RID: 62450 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170042AE RID: 17070
		// (get) Token: 0x0600F3F3 RID: 62451 RVA: 0x002D3A03 File Offset: 0x002D1C03
		internal override int ElementTypeId
		{
			get
			{
				return 13044;
			}
		}

		// Token: 0x0600F3F4 RID: 62452 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170042AF RID: 17071
		// (get) Token: 0x0600F3F5 RID: 62453 RVA: 0x002D3A0A File Offset: 0x002D1C0A
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitButtonWithTitle.attributeTagNames;
			}
		}

		// Token: 0x170042B0 RID: 17072
		// (get) Token: 0x0600F3F6 RID: 62454 RVA: 0x002D3A11 File Offset: 0x002D1C11
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitButtonWithTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x170042B1 RID: 17073
		// (get) Token: 0x0600F3F7 RID: 62455 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600F3F8 RID: 62456 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170042B2 RID: 17074
		// (get) Token: 0x0600F3F9 RID: 62457 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F3FA RID: 62458 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170042B3 RID: 17075
		// (get) Token: 0x0600F3FB RID: 62459 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F3FC RID: 62460 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170042B4 RID: 17076
		// (get) Token: 0x0600F3FD RID: 62461 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F3FE RID: 62462 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170042B5 RID: 17077
		// (get) Token: 0x0600F3FF RID: 62463 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F400 RID: 62464 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170042B6 RID: 17078
		// (get) Token: 0x0600F401 RID: 62465 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F402 RID: 62466 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170042B7 RID: 17079
		// (get) Token: 0x0600F403 RID: 62467 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F404 RID: 62468 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170042B8 RID: 17080
		// (get) Token: 0x0600F405 RID: 62469 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F406 RID: 62470 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170042B9 RID: 17081
		// (get) Token: 0x0600F407 RID: 62471 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F408 RID: 62472 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170042BA RID: 17082
		// (get) Token: 0x0600F409 RID: 62473 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F40A RID: 62474 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170042BB RID: 17083
		// (get) Token: 0x0600F40B RID: 62475 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600F40C RID: 62476 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170042BC RID: 17084
		// (get) Token: 0x0600F40D RID: 62477 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F40E RID: 62478 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170042BD RID: 17085
		// (get) Token: 0x0600F40F RID: 62479 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F410 RID: 62480 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170042BE RID: 17086
		// (get) Token: 0x0600F411 RID: 62481 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F412 RID: 62482 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170042BF RID: 17087
		// (get) Token: 0x0600F413 RID: 62483 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F414 RID: 62484 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170042C0 RID: 17088
		// (get) Token: 0x0600F415 RID: 62485 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F416 RID: 62486 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x0600F417 RID: 62487 RVA: 0x00293ECF File Offset: 0x002920CF
		public SplitButtonWithTitle()
		{
		}

		// Token: 0x0600F418 RID: 62488 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SplitButtonWithTitle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F419 RID: 62489 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SplitButtonWithTitle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F41A RID: 62490 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SplitButtonWithTitle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F41B RID: 62491 RVA: 0x002D3A18 File Offset: 0x002D1C18
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new VisibleButton();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new VisibleToggleButton();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuWithTitle();
			}
			return null;
		}

		// Token: 0x0600F41C RID: 62492 RVA: 0x002D3A70 File Offset: 0x002D1C70
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F41D RID: 62493 RVA: 0x002D3BE5 File Offset: 0x002D1DE5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitButtonWithTitle>(deep);
		}

		// Token: 0x0600F41E RID: 62494 RVA: 0x002D3BF0 File Offset: 0x002D1DF0
		// Note: this type is marked as 'beforefieldinit'.
		static SplitButtonWithTitle()
		{
			byte[] array = new byte[16];
			SplitButtonWithTitle.attributeNamespaceIds = array;
		}

		// Token: 0x040070FC RID: 28924
		private const string tagName = "splitButton";

		// Token: 0x040070FD RID: 28925
		private const byte tagNsId = 57;

		// Token: 0x040070FE RID: 28926
		internal const int ElementTypeIdConst = 13044;

		// Token: 0x040070FF RID: 28927
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x04007100 RID: 28928
		private static byte[] attributeNamespaceIds;
	}
}
