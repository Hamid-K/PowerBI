using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A93 RID: 10899
	[ChildElementInfo(typeof(BrowseMode), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LaserColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShowMediaControls), FileFormatVersions.Office2010)]
	internal class ShowPropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073DE RID: 29662
		// (get) Token: 0x060161DD RID: 90589 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073DF RID: 29663
		// (get) Token: 0x060161DE RID: 90590 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073E0 RID: 29664
		// (get) Token: 0x060161DF RID: 90591 RVA: 0x00326AA3 File Offset: 0x00324CA3
		internal override int ElementTypeId
		{
			get
			{
				return 12312;
			}
		}

		// Token: 0x060161E0 RID: 90592 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073E1 RID: 29665
		// (get) Token: 0x060161E1 RID: 90593 RVA: 0x00326AAA File Offset: 0x00324CAA
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShowPropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x170073E2 RID: 29666
		// (get) Token: 0x060161E2 RID: 90594 RVA: 0x00326AB1 File Offset: 0x00324CB1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShowPropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073E3 RID: 29667
		// (get) Token: 0x060161E3 RID: 90595 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161E4 RID: 90596 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x060161E5 RID: 90597 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShowPropertiesExtension()
		{
		}

		// Token: 0x060161E6 RID: 90598 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShowPropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161E7 RID: 90599 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShowPropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161E8 RID: 90600 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShowPropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161E9 RID: 90601 RVA: 0x00326AB8 File Offset: 0x00324CB8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "browseMode" == name)
			{
				return new BrowseMode();
			}
			if (49 == namespaceId && "laserClr" == name)
			{
				return new LaserColor();
			}
			if (49 == namespaceId && "showMediaCtrls" == name)
			{
				return new ShowMediaControls();
			}
			return null;
		}

		// Token: 0x060161EA RID: 90602 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161EB RID: 90603 RVA: 0x00326B0E File Offset: 0x00324D0E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowPropertiesExtension>(deep);
		}

		// Token: 0x060161EC RID: 90604 RVA: 0x00326B18 File Offset: 0x00324D18
		// Note: this type is marked as 'beforefieldinit'.
		static ShowPropertiesExtension()
		{
			byte[] array = new byte[1];
			ShowPropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400964A RID: 38474
		private const string tagName = "ext";

		// Token: 0x0400964B RID: 38475
		private const byte tagNsId = 24;

		// Token: 0x0400964C RID: 38476
		internal const int ElementTypeIdConst = 12312;

		// Token: 0x0400964D RID: 38477
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400964E RID: 38478
		private static byte[] attributeNamespaceIds;
	}
}
