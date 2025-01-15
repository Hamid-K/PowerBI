using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A8 RID: 9128
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class FlythroughTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C27 RID: 19495
		// (get) Token: 0x0601084E RID: 67662 RVA: 0x002E450F File Offset: 0x002E270F
		public override string LocalName
		{
			get
			{
				return "flythrough";
			}
		}

		// Token: 0x17004C28 RID: 19496
		// (get) Token: 0x0601084F RID: 67663 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C29 RID: 19497
		// (get) Token: 0x06010850 RID: 67664 RVA: 0x002E4516 File Offset: 0x002E2716
		internal override int ElementTypeId
		{
			get
			{
				return 12782;
			}
		}

		// Token: 0x06010851 RID: 67665 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C2A RID: 19498
		// (get) Token: 0x06010852 RID: 67666 RVA: 0x002E451D File Offset: 0x002E271D
		internal override string[] AttributeTagNames
		{
			get
			{
				return FlythroughTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C2B RID: 19499
		// (get) Token: 0x06010853 RID: 67667 RVA: 0x002E4524 File Offset: 0x002E2724
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FlythroughTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C2C RID: 19500
		// (get) Token: 0x06010854 RID: 67668 RVA: 0x002E44A7 File Offset: 0x002E26A7
		// (set) Token: 0x06010855 RID: 67669 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionInOutDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionInOutDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004C2D RID: 19501
		// (get) Token: 0x06010856 RID: 67670 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010857 RID: 67671 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hasBounce")]
		public BooleanValue HasBounce
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

		// Token: 0x06010859 RID: 67673 RVA: 0x002E452B File Offset: 0x002E272B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionInOutDirectionValues>();
			}
			if (namespaceId == 0 && "hasBounce" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601085A RID: 67674 RVA: 0x002E4561 File Offset: 0x002E2761
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FlythroughTransition>(deep);
		}

		// Token: 0x0601085B RID: 67675 RVA: 0x002E456C File Offset: 0x002E276C
		// Note: this type is marked as 'beforefieldinit'.
		static FlythroughTransition()
		{
			byte[] array = new byte[2];
			FlythroughTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04007508 RID: 29960
		private const string tagName = "flythrough";

		// Token: 0x04007509 RID: 29961
		private const byte tagNsId = 49;

		// Token: 0x0400750A RID: 29962
		internal const int ElementTypeIdConst = 12782;

		// Token: 0x0400750B RID: 29963
		private static string[] attributeTagNames = new string[] { "dir", "hasBounce" };

		// Token: 0x0400750C RID: 29964
		private static byte[] attributeNamespaceIds;
	}
}
