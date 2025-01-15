using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B14 RID: 11028
	[GeneratedCode("DomGen", "2.0")]
	internal class Extension : OpenXmlCompositeElement
	{
		// Token: 0x170075B0 RID: 30128
		// (get) Token: 0x06016604 RID: 91652 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170075B1 RID: 30129
		// (get) Token: 0x06016605 RID: 91653 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075B2 RID: 30130
		// (get) Token: 0x06016606 RID: 91654 RVA: 0x003296E4 File Offset: 0x003278E4
		internal override int ElementTypeId
		{
			get
			{
				return 11026;
			}
		}

		// Token: 0x06016607 RID: 91655 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170075B3 RID: 30131
		// (get) Token: 0x06016608 RID: 91656 RVA: 0x003296EB File Offset: 0x003278EB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extension.attributeTagNames;
			}
		}

		// Token: 0x170075B4 RID: 30132
		// (get) Token: 0x06016609 RID: 91657 RVA: 0x003296F2 File Offset: 0x003278F2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extension.attributeNamespaceIds;
			}
		}

		// Token: 0x170075B5 RID: 30133
		// (get) Token: 0x0601660A RID: 91658 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601660B RID: 91659 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601660C RID: 91660 RVA: 0x00293ECF File Offset: 0x002920CF
		public Extension()
		{
		}

		// Token: 0x0601660D RID: 91661 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Extension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601660E RID: 91662 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Extension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601660F RID: 91663 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Extension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016610 RID: 91664 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x06016611 RID: 91665 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016612 RID: 91666 RVA: 0x003296F9 File Offset: 0x003278F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extension>(deep);
		}

		// Token: 0x06016613 RID: 91667 RVA: 0x00329704 File Offset: 0x00327904
		// Note: this type is marked as 'beforefieldinit'.
		static Extension()
		{
			byte[] array = new byte[1];
			Extension.attributeNamespaceIds = array;
		}

		// Token: 0x040098C5 RID: 39109
		private const string tagName = "ext";

		// Token: 0x040098C6 RID: 39110
		private const byte tagNsId = 22;

		// Token: 0x040098C7 RID: 39111
		internal const int ElementTypeIdConst = 11026;

		// Token: 0x040098C8 RID: 39112
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x040098C9 RID: 39113
		private static byte[] attributeNamespaceIds;
	}
}
