using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F3 RID: 10739
	[GeneratedCode("DomGen", "2.0")]
	internal class Extension : OpenXmlCompositeElement
	{
		// Token: 0x17006E76 RID: 28278
		// (get) Token: 0x060155DC RID: 87516 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17006E77 RID: 28279
		// (get) Token: 0x060155DD RID: 87517 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E78 RID: 28280
		// (get) Token: 0x060155DE RID: 87518 RVA: 0x0031E393 File Offset: 0x0031C593
		internal override int ElementTypeId
		{
			get
			{
				return 12165;
			}
		}

		// Token: 0x060155DF RID: 87519 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E79 RID: 28281
		// (get) Token: 0x060155E0 RID: 87520 RVA: 0x0031E39A File Offset: 0x0031C59A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extension.attributeTagNames;
			}
		}

		// Token: 0x17006E7A RID: 28282
		// (get) Token: 0x060155E1 RID: 87521 RVA: 0x0031E3A1 File Offset: 0x0031C5A1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extension.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E7B RID: 28283
		// (get) Token: 0x060155E2 RID: 87522 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060155E3 RID: 87523 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060155E4 RID: 87524 RVA: 0x00293ECF File Offset: 0x002920CF
		public Extension()
		{
		}

		// Token: 0x060155E5 RID: 87525 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Extension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060155E6 RID: 87526 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Extension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060155E7 RID: 87527 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Extension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060155E8 RID: 87528 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x060155E9 RID: 87529 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060155EA RID: 87530 RVA: 0x0031E3A8 File Offset: 0x0031C5A8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extension>(deep);
		}

		// Token: 0x060155EB RID: 87531 RVA: 0x0031E3B4 File Offset: 0x0031C5B4
		// Note: this type is marked as 'beforefieldinit'.
		static Extension()
		{
			byte[] array = new byte[1];
			Extension.attributeNamespaceIds = array;
		}

		// Token: 0x04009334 RID: 37684
		private const string tagName = "ext";

		// Token: 0x04009335 RID: 37685
		private const byte tagNsId = 24;

		// Token: 0x04009336 RID: 37686
		internal const int ElementTypeIdConst = 12165;

		// Token: 0x04009337 RID: 37687
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009338 RID: 37688
		private static byte[] attributeNamespaceIds;
	}
}
