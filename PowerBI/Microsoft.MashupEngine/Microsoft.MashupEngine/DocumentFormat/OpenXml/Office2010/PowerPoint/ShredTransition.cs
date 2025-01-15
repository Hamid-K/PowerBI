using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A9 RID: 9129
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ShredTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C2E RID: 19502
		// (get) Token: 0x0601085C RID: 67676 RVA: 0x002E45A3 File Offset: 0x002E27A3
		public override string LocalName
		{
			get
			{
				return "shred";
			}
		}

		// Token: 0x17004C2F RID: 19503
		// (get) Token: 0x0601085D RID: 67677 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C30 RID: 19504
		// (get) Token: 0x0601085E RID: 67678 RVA: 0x002E45AA File Offset: 0x002E27AA
		internal override int ElementTypeId
		{
			get
			{
				return 12784;
			}
		}

		// Token: 0x0601085F RID: 67679 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C31 RID: 19505
		// (get) Token: 0x06010860 RID: 67680 RVA: 0x002E45B1 File Offset: 0x002E27B1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShredTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C32 RID: 19506
		// (get) Token: 0x06010861 RID: 67681 RVA: 0x002E45B8 File Offset: 0x002E27B8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShredTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C33 RID: 19507
		// (get) Token: 0x06010862 RID: 67682 RVA: 0x002E45BF File Offset: 0x002E27BF
		// (set) Token: 0x06010863 RID: 67683 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pattern")]
		public EnumValue<TransitionShredPatternValues> Pattern
		{
			get
			{
				return (EnumValue<TransitionShredPatternValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004C34 RID: 19508
		// (get) Token: 0x06010864 RID: 67684 RVA: 0x002E45CE File Offset: 0x002E27CE
		// (set) Token: 0x06010865 RID: 67685 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionInOutDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionInOutDirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010867 RID: 67687 RVA: 0x002E45DD File Offset: 0x002E27DD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pattern" == name)
			{
				return new EnumValue<TransitionShredPatternValues>();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionInOutDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010868 RID: 67688 RVA: 0x002E4613 File Offset: 0x002E2813
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShredTransition>(deep);
		}

		// Token: 0x06010869 RID: 67689 RVA: 0x002E461C File Offset: 0x002E281C
		// Note: this type is marked as 'beforefieldinit'.
		static ShredTransition()
		{
			byte[] array = new byte[2];
			ShredTransition.attributeNamespaceIds = array;
		}

		// Token: 0x0400750D RID: 29965
		private const string tagName = "shred";

		// Token: 0x0400750E RID: 29966
		private const byte tagNsId = 49;

		// Token: 0x0400750F RID: 29967
		internal const int ElementTypeIdConst = 12784;

		// Token: 0x04007510 RID: 29968
		private static string[] attributeTagNames = new string[] { "pattern", "dir" };

		// Token: 0x04007511 RID: 29969
		private static byte[] attributeNamespaceIds;
	}
}
