using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002244 RID: 8772
	[ChildElementInfo(typeof(ShapeHandle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeHandles : OpenXmlCompositeElement
	{
		// Token: 0x17003992 RID: 14738
		// (get) Token: 0x0600E0D8 RID: 57560 RVA: 0x002C0246 File Offset: 0x002BE446
		public override string LocalName
		{
			get
			{
				return "handles";
			}
		}

		// Token: 0x17003993 RID: 14739
		// (get) Token: 0x0600E0D9 RID: 57561 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003994 RID: 14740
		// (get) Token: 0x0600E0DA RID: 57562 RVA: 0x002C024D File Offset: 0x002BE44D
		internal override int ElementTypeId
		{
			get
			{
				return 12508;
			}
		}

		// Token: 0x0600E0DB RID: 57563 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E0DC RID: 57564 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeHandles()
		{
		}

		// Token: 0x0600E0DD RID: 57565 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeHandles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E0DE RID: 57566 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeHandles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E0DF RID: 57567 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeHandles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E0E0 RID: 57568 RVA: 0x002C0254 File Offset: 0x002BE454
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "h" == name)
			{
				return new ShapeHandle();
			}
			return null;
		}

		// Token: 0x0600E0E1 RID: 57569 RVA: 0x002C026F File Offset: 0x002BE46F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeHandles>(deep);
		}

		// Token: 0x04006E98 RID: 28312
		private const string tagName = "handles";

		// Token: 0x04006E99 RID: 28313
		private const byte tagNsId = 26;

		// Token: 0x04006E9A RID: 28314
		internal const int ElementTypeIdConst = 12508;
	}
}
