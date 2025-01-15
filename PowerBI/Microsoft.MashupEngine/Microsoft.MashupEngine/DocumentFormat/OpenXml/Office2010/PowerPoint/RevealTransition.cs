using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AA RID: 9130
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RevealTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C35 RID: 19509
		// (get) Token: 0x0601086A RID: 67690 RVA: 0x002E4653 File Offset: 0x002E2853
		public override string LocalName
		{
			get
			{
				return "reveal";
			}
		}

		// Token: 0x17004C36 RID: 19510
		// (get) Token: 0x0601086B RID: 67691 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C37 RID: 19511
		// (get) Token: 0x0601086C RID: 67692 RVA: 0x002E465A File Offset: 0x002E285A
		internal override int ElementTypeId
		{
			get
			{
				return 12785;
			}
		}

		// Token: 0x0601086D RID: 67693 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C38 RID: 19512
		// (get) Token: 0x0601086E RID: 67694 RVA: 0x002E4661 File Offset: 0x002E2861
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevealTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C39 RID: 19513
		// (get) Token: 0x0601086F RID: 67695 RVA: 0x002E4668 File Offset: 0x002E2868
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevealTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C3A RID: 19514
		// (get) Token: 0x06010870 RID: 67696 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010871 RID: 67697 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "thruBlk")]
		public BooleanValue ThroughBlack
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

		// Token: 0x17004C3B RID: 19515
		// (get) Token: 0x06010872 RID: 67698 RVA: 0x002E466F File Offset: 0x002E286F
		// (set) Token: 0x06010873 RID: 67699 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionLeftRightDirectionTypeValues> Direction
		{
			get
			{
				return (EnumValue<TransitionLeftRightDirectionTypeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010875 RID: 67701 RVA: 0x002E467E File Offset: 0x002E287E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "thruBlk" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionLeftRightDirectionTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010876 RID: 67702 RVA: 0x002E46B4 File Offset: 0x002E28B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevealTransition>(deep);
		}

		// Token: 0x06010877 RID: 67703 RVA: 0x002E46C0 File Offset: 0x002E28C0
		// Note: this type is marked as 'beforefieldinit'.
		static RevealTransition()
		{
			byte[] array = new byte[2];
			RevealTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04007512 RID: 29970
		private const string tagName = "reveal";

		// Token: 0x04007513 RID: 29971
		private const byte tagNsId = 49;

		// Token: 0x04007514 RID: 29972
		internal const int ElementTypeIdConst = 12785;

		// Token: 0x04007515 RID: 29973
		private static string[] attributeTagNames = new string[] { "thruBlk", "dir" };

		// Token: 0x04007516 RID: 29974
		private static byte[] attributeNamespaceIds;
	}
}
