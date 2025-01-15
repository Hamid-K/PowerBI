using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002830 RID: 10288
	[ChildElementInfo(typeof(IsCanvas), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class GvmlGroupShapeExtension : OpenXmlCompositeElement
	{
		// Token: 0x17006601 RID: 26113
		// (get) Token: 0x06014283 RID: 82563 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17006602 RID: 26114
		// (get) Token: 0x06014284 RID: 82564 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006603 RID: 26115
		// (get) Token: 0x06014285 RID: 82565 RVA: 0x0030FDAF File Offset: 0x0030DFAF
		internal override int ElementTypeId
		{
			get
			{
				return 10321;
			}
		}

		// Token: 0x06014286 RID: 82566 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006604 RID: 26116
		// (get) Token: 0x06014287 RID: 82567 RVA: 0x0030FDB6 File Offset: 0x0030DFB6
		internal override string[] AttributeTagNames
		{
			get
			{
				return GvmlGroupShapeExtension.attributeTagNames;
			}
		}

		// Token: 0x17006605 RID: 26117
		// (get) Token: 0x06014288 RID: 82568 RVA: 0x0030FDBD File Offset: 0x0030DFBD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GvmlGroupShapeExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17006606 RID: 26118
		// (get) Token: 0x06014289 RID: 82569 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601428A RID: 82570 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601428B RID: 82571 RVA: 0x00293ECF File Offset: 0x002920CF
		public GvmlGroupShapeExtension()
		{
		}

		// Token: 0x0601428C RID: 82572 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GvmlGroupShapeExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601428D RID: 82573 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GvmlGroupShapeExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601428E RID: 82574 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GvmlGroupShapeExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601428F RID: 82575 RVA: 0x0030FDC4 File Offset: 0x0030DFC4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "isCanvas" == name)
			{
				return new IsCanvas();
			}
			return null;
		}

		// Token: 0x06014290 RID: 82576 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014291 RID: 82577 RVA: 0x0030FDDF File Offset: 0x0030DFDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GvmlGroupShapeExtension>(deep);
		}

		// Token: 0x06014292 RID: 82578 RVA: 0x0030FDE8 File Offset: 0x0030DFE8
		// Note: this type is marked as 'beforefieldinit'.
		static GvmlGroupShapeExtension()
		{
			byte[] array = new byte[1];
			GvmlGroupShapeExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400894A RID: 35146
		private const string tagName = "ext";

		// Token: 0x0400894B RID: 35147
		private const byte tagNsId = 10;

		// Token: 0x0400894C RID: 35148
		internal const int ElementTypeIdConst = 10321;

		// Token: 0x0400894D RID: 35149
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400894E RID: 35150
		private static byte[] attributeNamespaceIds;
	}
}
