using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256E RID: 9582
	[GeneratedCode("DomGen", "2.0")]
	internal class Extension : OpenXmlCompositeElement
	{
		// Token: 0x170055CE RID: 21966
		// (get) Token: 0x06011DDE RID: 73182 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170055CF RID: 21967
		// (get) Token: 0x06011DDF RID: 73183 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055D0 RID: 21968
		// (get) Token: 0x06011DE0 RID: 73184 RVA: 0x002F3362 File Offset: 0x002F1562
		internal override int ElementTypeId
		{
			get
			{
				return 10389;
			}
		}

		// Token: 0x06011DE1 RID: 73185 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170055D1 RID: 21969
		// (get) Token: 0x06011DE2 RID: 73186 RVA: 0x002F3369 File Offset: 0x002F1569
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extension.attributeTagNames;
			}
		}

		// Token: 0x170055D2 RID: 21970
		// (get) Token: 0x06011DE3 RID: 73187 RVA: 0x002F3370 File Offset: 0x002F1570
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extension.attributeNamespaceIds;
			}
		}

		// Token: 0x170055D3 RID: 21971
		// (get) Token: 0x06011DE4 RID: 73188 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011DE5 RID: 73189 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011DE6 RID: 73190 RVA: 0x00293ECF File Offset: 0x002920CF
		public Extension()
		{
		}

		// Token: 0x06011DE7 RID: 73191 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Extension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DE8 RID: 73192 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Extension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011DE9 RID: 73193 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Extension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011DEA RID: 73194 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x06011DEB RID: 73195 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011DEC RID: 73196 RVA: 0x002F3397 File Offset: 0x002F1597
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extension>(deep);
		}

		// Token: 0x06011DED RID: 73197 RVA: 0x002F33A0 File Offset: 0x002F15A0
		// Note: this type is marked as 'beforefieldinit'.
		static Extension()
		{
			byte[] array = new byte[1];
			Extension.attributeNamespaceIds = array;
		}

		// Token: 0x04007CFB RID: 31995
		private const string tagName = "ext";

		// Token: 0x04007CFC RID: 31996
		private const byte tagNsId = 11;

		// Token: 0x04007CFD RID: 31997
		internal const int ElementTypeIdConst = 10389;

		// Token: 0x04007CFE RID: 31998
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04007CFF RID: 31999
		private static byte[] attributeNamespaceIds;
	}
}
