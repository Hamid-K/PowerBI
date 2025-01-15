using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002680 RID: 9856
	[GeneratedCode("DomGen", "2.0")]
	internal class OrganizationChart : OpenXmlLeafElement
	{
		// Token: 0x17005CA1 RID: 23713
		// (get) Token: 0x06012D4D RID: 77133 RVA: 0x002FFDD0 File Offset: 0x002FDFD0
		public override string LocalName
		{
			get
			{
				return "orgChart";
			}
		}

		// Token: 0x17005CA2 RID: 23714
		// (get) Token: 0x06012D4E RID: 77134 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CA3 RID: 23715
		// (get) Token: 0x06012D4F RID: 77135 RVA: 0x002FFDD7 File Offset: 0x002FDFD7
		internal override int ElementTypeId
		{
			get
			{
				return 10671;
			}
		}

		// Token: 0x06012D50 RID: 77136 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CA4 RID: 23716
		// (get) Token: 0x06012D51 RID: 77137 RVA: 0x002FFDDE File Offset: 0x002FDFDE
		internal override string[] AttributeTagNames
		{
			get
			{
				return OrganizationChart.attributeTagNames;
			}
		}

		// Token: 0x17005CA5 RID: 23717
		// (get) Token: 0x06012D52 RID: 77138 RVA: 0x002FFDE5 File Offset: 0x002FDFE5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OrganizationChart.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CA6 RID: 23718
		// (get) Token: 0x06012D53 RID: 77139 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06012D54 RID: 77140 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
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

		// Token: 0x06012D56 RID: 77142 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D57 RID: 77143 RVA: 0x002FFDEC File Offset: 0x002FDFEC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OrganizationChart>(deep);
		}

		// Token: 0x06012D58 RID: 77144 RVA: 0x002FFDF8 File Offset: 0x002FDFF8
		// Note: this type is marked as 'beforefieldinit'.
		static OrganizationChart()
		{
			byte[] array = new byte[1];
			OrganizationChart.attributeNamespaceIds = array;
		}

		// Token: 0x040081C6 RID: 33222
		private const string tagName = "orgChart";

		// Token: 0x040081C7 RID: 33223
		private const byte tagNsId = 14;

		// Token: 0x040081C8 RID: 33224
		internal const int ElementTypeIdConst = 10671;

		// Token: 0x040081C9 RID: 33225
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081CA RID: 33226
		private static byte[] attributeNamespaceIds;
	}
}
