using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x0200239E RID: 9118
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RippleTransition : OpenXmlLeafElement
	{
		// Token: 0x17004BFD RID: 19453
		// (get) Token: 0x060107F8 RID: 67576 RVA: 0x002E41DF File Offset: 0x002E23DF
		public override string LocalName
		{
			get
			{
				return "ripple";
			}
		}

		// Token: 0x17004BFE RID: 19454
		// (get) Token: 0x060107F9 RID: 67577 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BFF RID: 19455
		// (get) Token: 0x060107FA RID: 67578 RVA: 0x002E41E6 File Offset: 0x002E23E6
		internal override int ElementTypeId
		{
			get
			{
				return 12771;
			}
		}

		// Token: 0x060107FB RID: 67579 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C00 RID: 19456
		// (get) Token: 0x060107FC RID: 67580 RVA: 0x002E41ED File Offset: 0x002E23ED
		internal override string[] AttributeTagNames
		{
			get
			{
				return RippleTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C01 RID: 19457
		// (get) Token: 0x060107FD RID: 67581 RVA: 0x002E41F4 File Offset: 0x002E23F4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RippleTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C02 RID: 19458
		// (get) Token: 0x060107FE RID: 67582 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060107FF RID: 67583 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public StringValue Direction
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

		// Token: 0x06010801 RID: 67585 RVA: 0x002E41FB File Offset: 0x002E23FB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010802 RID: 67586 RVA: 0x002E421B File Offset: 0x002E241B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RippleTransition>(deep);
		}

		// Token: 0x06010803 RID: 67587 RVA: 0x002E4224 File Offset: 0x002E2424
		// Note: this type is marked as 'beforefieldinit'.
		static RippleTransition()
		{
			byte[] array = new byte[1];
			RippleTransition.attributeNamespaceIds = array;
		}

		// Token: 0x040074E6 RID: 29926
		private const string tagName = "ripple";

		// Token: 0x040074E7 RID: 29927
		private const byte tagNsId = 49;

		// Token: 0x040074E8 RID: 29928
		internal const int ElementTypeIdConst = 12771;

		// Token: 0x040074E9 RID: 29929
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x040074EA RID: 29930
		private static byte[] attributeNamespaceIds;
	}
}
