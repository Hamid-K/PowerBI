using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ADC RID: 10972
	[GeneratedCode("DomGen", "2.0")]
	internal class WheelTransition : OpenXmlLeafElement
	{
		// Token: 0x17007591 RID: 30097
		// (get) Token: 0x060165C0 RID: 91584 RVA: 0x0032947B File Offset: 0x0032767B
		public override string LocalName
		{
			get
			{
				return "wheel";
			}
		}

		// Token: 0x17007592 RID: 30098
		// (get) Token: 0x060165C1 RID: 91585 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007593 RID: 30099
		// (get) Token: 0x060165C2 RID: 91586 RVA: 0x00329482 File Offset: 0x00327682
		internal override int ElementTypeId
		{
			get
			{
				return 12393;
			}
		}

		// Token: 0x060165C3 RID: 91587 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007594 RID: 30100
		// (get) Token: 0x060165C4 RID: 91588 RVA: 0x00329489 File Offset: 0x00327689
		internal override string[] AttributeTagNames
		{
			get
			{
				return WheelTransition.attributeTagNames;
			}
		}

		// Token: 0x17007595 RID: 30101
		// (get) Token: 0x060165C5 RID: 91589 RVA: 0x00329490 File Offset: 0x00327690
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WheelTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17007596 RID: 30102
		// (get) Token: 0x060165C6 RID: 91590 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060165C7 RID: 91591 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spokes")]
		public UInt32Value Spokes
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060165C9 RID: 91593 RVA: 0x002E4713 File Offset: 0x002E2913
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spokes" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060165CA RID: 91594 RVA: 0x00329497 File Offset: 0x00327697
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WheelTransition>(deep);
		}

		// Token: 0x060165CB RID: 91595 RVA: 0x003294A0 File Offset: 0x003276A0
		// Note: this type is marked as 'beforefieldinit'.
		static WheelTransition()
		{
			byte[] array = new byte[1];
			WheelTransition.attributeNamespaceIds = array;
		}

		// Token: 0x0400976F RID: 38767
		private const string tagName = "wheel";

		// Token: 0x04009770 RID: 38768
		private const byte tagNsId = 24;

		// Token: 0x04009771 RID: 38769
		internal const int ElementTypeIdConst = 12393;

		// Token: 0x04009772 RID: 38770
		private static string[] attributeTagNames = new string[] { "spokes" };

		// Token: 0x04009773 RID: 38771
		private static byte[] attributeNamespaceIds;
	}
}
