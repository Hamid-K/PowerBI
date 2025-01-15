using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Charts;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FC RID: 9724
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(InvertSolidFillFormat), FileFormatVersions.Office2010)]
	internal class BubbleSerExtension : OpenXmlCompositeElement
	{
		// Token: 0x17005976 RID: 22902
		// (get) Token: 0x06012601 RID: 75265 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17005977 RID: 22903
		// (get) Token: 0x06012602 RID: 75266 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005978 RID: 22904
		// (get) Token: 0x06012603 RID: 75267 RVA: 0x002FA4EF File Offset: 0x002F86EF
		internal override int ElementTypeId
		{
			get
			{
				return 10569;
			}
		}

		// Token: 0x06012604 RID: 75268 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005979 RID: 22905
		// (get) Token: 0x06012605 RID: 75269 RVA: 0x002FA4F6 File Offset: 0x002F86F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return BubbleSerExtension.attributeTagNames;
			}
		}

		// Token: 0x1700597A RID: 22906
		// (get) Token: 0x06012606 RID: 75270 RVA: 0x002FA4FD File Offset: 0x002F86FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BubbleSerExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x1700597B RID: 22907
		// (get) Token: 0x06012607 RID: 75271 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012608 RID: 75272 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012609 RID: 75273 RVA: 0x00293ECF File Offset: 0x002920CF
		public BubbleSerExtension()
		{
		}

		// Token: 0x0601260A RID: 75274 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BubbleSerExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601260B RID: 75275 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BubbleSerExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601260C RID: 75276 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BubbleSerExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601260D RID: 75277 RVA: 0x002FA504 File Offset: 0x002F8704
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "invertSolidFillFmt" == name)
			{
				return new InvertSolidFillFormat();
			}
			return null;
		}

		// Token: 0x0601260E RID: 75278 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601260F RID: 75279 RVA: 0x002FA51F File Offset: 0x002F871F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BubbleSerExtension>(deep);
		}

		// Token: 0x06012610 RID: 75280 RVA: 0x002FA528 File Offset: 0x002F8728
		// Note: this type is marked as 'beforefieldinit'.
		static BubbleSerExtension()
		{
			byte[] array = new byte[1];
			BubbleSerExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04007F50 RID: 32592
		private const string tagName = "ext";

		// Token: 0x04007F51 RID: 32593
		private const byte tagNsId = 11;

		// Token: 0x04007F52 RID: 32594
		internal const int ElementTypeIdConst = 10569;

		// Token: 0x04007F53 RID: 32595
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04007F54 RID: 32596
		private static byte[] attributeNamespaceIds;
	}
}
