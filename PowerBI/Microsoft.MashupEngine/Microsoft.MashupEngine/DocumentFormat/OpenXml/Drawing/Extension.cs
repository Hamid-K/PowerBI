using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F2 RID: 9970
	[GeneratedCode("DomGen", "2.0")]
	internal class Extension : OpenXmlCompositeElement
	{
		// Token: 0x17005DFE RID: 24062
		// (get) Token: 0x06013028 RID: 77864 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17005DFF RID: 24063
		// (get) Token: 0x06013029 RID: 77865 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E00 RID: 24064
		// (get) Token: 0x0601302A RID: 77866 RVA: 0x003019B4 File Offset: 0x002FFBB4
		internal override int ElementTypeId
		{
			get
			{
				return 10034;
			}
		}

		// Token: 0x0601302B RID: 77867 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E01 RID: 24065
		// (get) Token: 0x0601302C RID: 77868 RVA: 0x003019BB File Offset: 0x002FFBBB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extension.attributeTagNames;
			}
		}

		// Token: 0x17005E02 RID: 24066
		// (get) Token: 0x0601302D RID: 77869 RVA: 0x003019C2 File Offset: 0x002FFBC2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extension.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E03 RID: 24067
		// (get) Token: 0x0601302E RID: 77870 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601302F RID: 77871 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06013030 RID: 77872 RVA: 0x00293ECF File Offset: 0x002920CF
		public Extension()
		{
		}

		// Token: 0x06013031 RID: 77873 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Extension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013032 RID: 77874 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Extension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013033 RID: 77875 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Extension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013034 RID: 77876 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x06013035 RID: 77877 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013036 RID: 77878 RVA: 0x003019C9 File Offset: 0x002FFBC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extension>(deep);
		}

		// Token: 0x06013037 RID: 77879 RVA: 0x003019D4 File Offset: 0x002FFBD4
		// Note: this type is marked as 'beforefieldinit'.
		static Extension()
		{
			byte[] array = new byte[1];
			Extension.attributeNamespaceIds = array;
		}

		// Token: 0x04008440 RID: 33856
		private const string tagName = "ext";

		// Token: 0x04008441 RID: 33857
		private const byte tagNsId = 10;

		// Token: 0x04008442 RID: 33858
		internal const int ElementTypeIdConst = 10034;

		// Token: 0x04008443 RID: 33859
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008444 RID: 33860
		private static byte[] attributeNamespaceIds;
	}
}
