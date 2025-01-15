using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A6 RID: 9126
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class GlitterTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C1A RID: 19482
		// (get) Token: 0x06010834 RID: 67636 RVA: 0x002E43E9 File Offset: 0x002E25E9
		public override string LocalName
		{
			get
			{
				return "glitter";
			}
		}

		// Token: 0x17004C1B RID: 19483
		// (get) Token: 0x06010835 RID: 67637 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C1C RID: 19484
		// (get) Token: 0x06010836 RID: 67638 RVA: 0x002E43F0 File Offset: 0x002E25F0
		internal override int ElementTypeId
		{
			get
			{
				return 12780;
			}
		}

		// Token: 0x06010837 RID: 67639 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C1D RID: 19485
		// (get) Token: 0x06010838 RID: 67640 RVA: 0x002E43F7 File Offset: 0x002E25F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return GlitterTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C1E RID: 19486
		// (get) Token: 0x06010839 RID: 67641 RVA: 0x002E43FE File Offset: 0x002E25FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GlitterTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C1F RID: 19487
		// (get) Token: 0x0601083A RID: 67642 RVA: 0x002E4066 File Offset: 0x002E2266
		// (set) Token: 0x0601083B RID: 67643 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004C20 RID: 19488
		// (get) Token: 0x0601083C RID: 67644 RVA: 0x002E4405 File Offset: 0x002E2605
		// (set) Token: 0x0601083D RID: 67645 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pattern")]
		public EnumValue<TransitionPatternValues> Pattern
		{
			get
			{
				return (EnumValue<TransitionPatternValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601083F RID: 67647 RVA: 0x002E4414 File Offset: 0x002E2614
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionSlideDirectionValues>();
			}
			if (namespaceId == 0 && "pattern" == name)
			{
				return new EnumValue<TransitionPatternValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010840 RID: 67648 RVA: 0x002E444A File Offset: 0x002E264A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GlitterTransition>(deep);
		}

		// Token: 0x06010841 RID: 67649 RVA: 0x002E4454 File Offset: 0x002E2654
		// Note: this type is marked as 'beforefieldinit'.
		static GlitterTransition()
		{
			byte[] array = new byte[2];
			GlitterTransition.attributeNamespaceIds = array;
		}

		// Token: 0x040074FE RID: 29950
		private const string tagName = "glitter";

		// Token: 0x040074FF RID: 29951
		private const byte tagNsId = 49;

		// Token: 0x04007500 RID: 29952
		internal const int ElementTypeIdConst = 12780;

		// Token: 0x04007501 RID: 29953
		private static string[] attributeTagNames = new string[] { "dir", "pattern" };

		// Token: 0x04007502 RID: 29954
		private static byte[] attributeNamespaceIds;
	}
}
