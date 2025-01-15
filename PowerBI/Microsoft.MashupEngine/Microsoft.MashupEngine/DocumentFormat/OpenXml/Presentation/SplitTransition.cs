using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ADA RID: 10970
	[GeneratedCode("DomGen", "2.0")]
	internal class SplitTransition : OpenXmlLeafElement
	{
		// Token: 0x17007584 RID: 30084
		// (get) Token: 0x060165A6 RID: 91558 RVA: 0x00329365 File Offset: 0x00327565
		public override string LocalName
		{
			get
			{
				return "split";
			}
		}

		// Token: 0x17007585 RID: 30085
		// (get) Token: 0x060165A7 RID: 91559 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007586 RID: 30086
		// (get) Token: 0x060165A8 RID: 91560 RVA: 0x0032936C File Offset: 0x0032756C
		internal override int ElementTypeId
		{
			get
			{
				return 12390;
			}
		}

		// Token: 0x060165A9 RID: 91561 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007587 RID: 30087
		// (get) Token: 0x060165AA RID: 91562 RVA: 0x00329373 File Offset: 0x00327573
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitTransition.attributeTagNames;
			}
		}

		// Token: 0x17007588 RID: 30088
		// (get) Token: 0x060165AB RID: 91563 RVA: 0x0032937A File Offset: 0x0032757A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17007589 RID: 30089
		// (get) Token: 0x060165AC RID: 91564 RVA: 0x002E4355 File Offset: 0x002E2555
		// (set) Token: 0x060165AD RID: 91565 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "orient")]
		public EnumValue<DirectionValues> Orientation
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700758A RID: 30090
		// (get) Token: 0x060165AE RID: 91566 RVA: 0x002E45CE File Offset: 0x002E27CE
		// (set) Token: 0x060165AF RID: 91567 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x060165B1 RID: 91569 RVA: 0x00329381 File Offset: 0x00327581
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "orient" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionInOutDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060165B2 RID: 91570 RVA: 0x003293B7 File Offset: 0x003275B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitTransition>(deep);
		}

		// Token: 0x060165B3 RID: 91571 RVA: 0x003293C0 File Offset: 0x003275C0
		// Note: this type is marked as 'beforefieldinit'.
		static SplitTransition()
		{
			byte[] array = new byte[2];
			SplitTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04009765 RID: 38757
		private const string tagName = "split";

		// Token: 0x04009766 RID: 38758
		private const byte tagNsId = 24;

		// Token: 0x04009767 RID: 38759
		internal const int ElementTypeIdConst = 12390;

		// Token: 0x04009768 RID: 38760
		private static string[] attributeTagNames = new string[] { "orient", "dir" };

		// Token: 0x04009769 RID: 38761
		private static byte[] attributeNamespaceIds;
	}
}
