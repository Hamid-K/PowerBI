using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002831 RID: 10289
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CameraTool), FileFormatVersions.Office2010)]
	internal class NonVisualPicturePropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x17006607 RID: 26119
		// (get) Token: 0x06014293 RID: 82579 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17006608 RID: 26120
		// (get) Token: 0x06014294 RID: 82580 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006609 RID: 26121
		// (get) Token: 0x06014295 RID: 82581 RVA: 0x0030FE17 File Offset: 0x0030E017
		internal override int ElementTypeId
		{
			get
			{
				return 10322;
			}
		}

		// Token: 0x06014296 RID: 82582 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700660A RID: 26122
		// (get) Token: 0x06014297 RID: 82583 RVA: 0x0030FE1E File Offset: 0x0030E01E
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPicturePropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x1700660B RID: 26123
		// (get) Token: 0x06014298 RID: 82584 RVA: 0x0030FE25 File Offset: 0x0030E025
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPicturePropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x1700660C RID: 26124
		// (get) Token: 0x06014299 RID: 82585 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601429A RID: 82586 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601429B RID: 82587 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPicturePropertiesExtension()
		{
		}

		// Token: 0x0601429C RID: 82588 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPicturePropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601429D RID: 82589 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPicturePropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601429E RID: 82590 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPicturePropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601429F RID: 82591 RVA: 0x0030FE2C File Offset: 0x0030E02C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "cameraTool" == name)
			{
				return new CameraTool();
			}
			return null;
		}

		// Token: 0x060142A0 RID: 82592 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060142A1 RID: 82593 RVA: 0x0030FE47 File Offset: 0x0030E047
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPicturePropertiesExtension>(deep);
		}

		// Token: 0x060142A2 RID: 82594 RVA: 0x0030FE50 File Offset: 0x0030E050
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPicturePropertiesExtension()
		{
			byte[] array = new byte[1];
			NonVisualPicturePropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400894F RID: 35151
		private const string tagName = "ext";

		// Token: 0x04008950 RID: 35152
		private const byte tagNsId = 10;

		// Token: 0x04008951 RID: 35153
		internal const int ElementTypeIdConst = 10322;

		// Token: 0x04008952 RID: 35154
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008953 RID: 35155
		private static byte[] attributeNamespaceIds;
	}
}
