using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282F RID: 10287
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ImageProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(UseLocalDpi), FileFormatVersions.Office2010)]
	internal class BlipExtension : OpenXmlCompositeElement
	{
		// Token: 0x170065FB RID: 26107
		// (get) Token: 0x06014273 RID: 82547 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170065FC RID: 26108
		// (get) Token: 0x06014274 RID: 82548 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065FD RID: 26109
		// (get) Token: 0x06014275 RID: 82549 RVA: 0x0030FD2F File Offset: 0x0030DF2F
		internal override int ElementTypeId
		{
			get
			{
				return 10320;
			}
		}

		// Token: 0x06014276 RID: 82550 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065FE RID: 26110
		// (get) Token: 0x06014277 RID: 82551 RVA: 0x0030FD36 File Offset: 0x0030DF36
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipExtension.attributeTagNames;
			}
		}

		// Token: 0x170065FF RID: 26111
		// (get) Token: 0x06014278 RID: 82552 RVA: 0x0030FD3D File Offset: 0x0030DF3D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17006600 RID: 26112
		// (get) Token: 0x06014279 RID: 82553 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601427A RID: 82554 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601427B RID: 82555 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipExtension()
		{
		}

		// Token: 0x0601427C RID: 82556 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601427D RID: 82557 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601427E RID: 82558 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601427F RID: 82559 RVA: 0x0030FD44 File Offset: 0x0030DF44
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "imgProps" == name)
			{
				return new ImageProperties();
			}
			if (48 == namespaceId && "useLocalDpi" == name)
			{
				return new UseLocalDpi();
			}
			return null;
		}

		// Token: 0x06014280 RID: 82560 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014281 RID: 82561 RVA: 0x0030FD77 File Offset: 0x0030DF77
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipExtension>(deep);
		}

		// Token: 0x06014282 RID: 82562 RVA: 0x0030FD80 File Offset: 0x0030DF80
		// Note: this type is marked as 'beforefieldinit'.
		static BlipExtension()
		{
			byte[] array = new byte[1];
			BlipExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04008945 RID: 35141
		private const string tagName = "ext";

		// Token: 0x04008946 RID: 35142
		private const byte tagNsId = 10;

		// Token: 0x04008947 RID: 35143
		internal const int ElementTypeIdConst = 10320;

		// Token: 0x04008948 RID: 35144
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008949 RID: 35145
		private static byte[] attributeNamespaceIds;
	}
}
