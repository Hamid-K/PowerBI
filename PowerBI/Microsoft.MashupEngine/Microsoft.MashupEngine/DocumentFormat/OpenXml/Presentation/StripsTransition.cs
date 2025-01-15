using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ADB RID: 10971
	[GeneratedCode("DomGen", "2.0")]
	internal class StripsTransition : OpenXmlLeafElement
	{
		// Token: 0x1700758B RID: 30091
		// (get) Token: 0x060165B4 RID: 91572 RVA: 0x003293F7 File Offset: 0x003275F7
		public override string LocalName
		{
			get
			{
				return "strips";
			}
		}

		// Token: 0x1700758C RID: 30092
		// (get) Token: 0x060165B5 RID: 91573 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700758D RID: 30093
		// (get) Token: 0x060165B6 RID: 91574 RVA: 0x003293FE File Offset: 0x003275FE
		internal override int ElementTypeId
		{
			get
			{
				return 12391;
			}
		}

		// Token: 0x060165B7 RID: 91575 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700758E RID: 30094
		// (get) Token: 0x060165B8 RID: 91576 RVA: 0x00329405 File Offset: 0x00327605
		internal override string[] AttributeTagNames
		{
			get
			{
				return StripsTransition.attributeTagNames;
			}
		}

		// Token: 0x1700758F RID: 30095
		// (get) Token: 0x060165B9 RID: 91577 RVA: 0x0032940C File Offset: 0x0032760C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StripsTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17007590 RID: 30096
		// (get) Token: 0x060165BA RID: 91578 RVA: 0x00329413 File Offset: 0x00327613
		// (set) Token: 0x060165BB RID: 91579 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionCornerDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionCornerDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060165BD RID: 91581 RVA: 0x00329422 File Offset: 0x00327622
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionCornerDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060165BE RID: 91582 RVA: 0x00329442 File Offset: 0x00327642
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StripsTransition>(deep);
		}

		// Token: 0x060165BF RID: 91583 RVA: 0x0032944C File Offset: 0x0032764C
		// Note: this type is marked as 'beforefieldinit'.
		static StripsTransition()
		{
			byte[] array = new byte[1];
			StripsTransition.attributeNamespaceIds = array;
		}

		// Token: 0x0400976A RID: 38762
		private const string tagName = "strips";

		// Token: 0x0400976B RID: 38763
		private const byte tagNsId = 24;

		// Token: 0x0400976C RID: 38764
		internal const int ElementTypeIdConst = 12391;

		// Token: 0x0400976D RID: 38765
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x0400976E RID: 38766
		private static byte[] attributeNamespaceIds;
	}
}
