using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002832 RID: 10290
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CompatExtension), FileFormatVersions.Office2010)]
	internal class NonVisualDrawingPropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x1700660D RID: 26125
		// (get) Token: 0x060142A3 RID: 82595 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x1700660E RID: 26126
		// (get) Token: 0x060142A4 RID: 82596 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700660F RID: 26127
		// (get) Token: 0x060142A5 RID: 82597 RVA: 0x0030FE7F File Offset: 0x0030E07F
		internal override int ElementTypeId
		{
			get
			{
				return 10323;
			}
		}

		// Token: 0x060142A6 RID: 82598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006610 RID: 26128
		// (get) Token: 0x060142A7 RID: 82599 RVA: 0x0030FE86 File Offset: 0x0030E086
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingPropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x17006611 RID: 26129
		// (get) Token: 0x060142A8 RID: 82600 RVA: 0x0030FE8D File Offset: 0x0030E08D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingPropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17006612 RID: 26130
		// (get) Token: 0x060142A9 RID: 82601 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060142AA RID: 82602 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060142AB RID: 82603 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingPropertiesExtension()
		{
		}

		// Token: 0x060142AC RID: 82604 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingPropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142AD RID: 82605 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingPropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060142AE RID: 82606 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingPropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060142AF RID: 82607 RVA: 0x0030FE94 File Offset: 0x0030E094
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "compatExt" == name)
			{
				return new CompatExtension();
			}
			return null;
		}

		// Token: 0x060142B0 RID: 82608 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060142B1 RID: 82609 RVA: 0x0030FEAF File Offset: 0x0030E0AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingPropertiesExtension>(deep);
		}

		// Token: 0x060142B2 RID: 82610 RVA: 0x0030FEB8 File Offset: 0x0030E0B8
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingPropertiesExtension()
		{
			byte[] array = new byte[1];
			NonVisualDrawingPropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04008954 RID: 35156
		private const string tagName = "ext";

		// Token: 0x04008955 RID: 35157
		private const byte tagNsId = 10;

		// Token: 0x04008956 RID: 35158
		internal const int ElementTypeIdConst = 10323;

		// Token: 0x04008957 RID: 35159
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008958 RID: 35160
		private static byte[] attributeNamespaceIds;
	}
}
