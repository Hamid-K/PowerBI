using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A2 RID: 9122
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PrismTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C09 RID: 19465
		// (get) Token: 0x06010811 RID: 67601 RVA: 0x002E4289 File Offset: 0x002E2489
		public override string LocalName
		{
			get
			{
				return "prism";
			}
		}

		// Token: 0x17004C0A RID: 19466
		// (get) Token: 0x06010812 RID: 67602 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C0B RID: 19467
		// (get) Token: 0x06010813 RID: 67603 RVA: 0x002E4290 File Offset: 0x002E2490
		internal override int ElementTypeId
		{
			get
			{
				return 12773;
			}
		}

		// Token: 0x06010814 RID: 67604 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C0C RID: 19468
		// (get) Token: 0x06010815 RID: 67605 RVA: 0x002E4297 File Offset: 0x002E2497
		internal override string[] AttributeTagNames
		{
			get
			{
				return PrismTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C0D RID: 19469
		// (get) Token: 0x06010816 RID: 67606 RVA: 0x002E429E File Offset: 0x002E249E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PrismTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C0E RID: 19470
		// (get) Token: 0x06010817 RID: 67607 RVA: 0x002E4066 File Offset: 0x002E2266
		// (set) Token: 0x06010818 RID: 67608 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionSlideDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionSlideDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004C0F RID: 19471
		// (get) Token: 0x06010819 RID: 67609 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601081A RID: 67610 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "isContent")]
		public BooleanValue IsContent
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004C10 RID: 19472
		// (get) Token: 0x0601081B RID: 67611 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601081C RID: 67612 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "isInverted")]
		public BooleanValue IsInverted
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601081E RID: 67614 RVA: 0x002E42A8 File Offset: 0x002E24A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionSlideDirectionValues>();
			}
			if (namespaceId == 0 && "isContent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "isInverted" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601081F RID: 67615 RVA: 0x002E42FF File Offset: 0x002E24FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrismTransition>(deep);
		}

		// Token: 0x06010820 RID: 67616 RVA: 0x002E4308 File Offset: 0x002E2508
		// Note: this type is marked as 'beforefieldinit'.
		static PrismTransition()
		{
			byte[] array = new byte[3];
			PrismTransition.attributeNamespaceIds = array;
		}

		// Token: 0x040074F1 RID: 29937
		private const string tagName = "prism";

		// Token: 0x040074F2 RID: 29938
		private const byte tagNsId = 49;

		// Token: 0x040074F3 RID: 29939
		internal const int ElementTypeIdConst = 12773;

		// Token: 0x040074F4 RID: 29940
		private static string[] attributeTagNames = new string[] { "dir", "isContent", "isInverted" };

		// Token: 0x040074F5 RID: 29941
		private static byte[] attributeNamespaceIds;
	}
}
