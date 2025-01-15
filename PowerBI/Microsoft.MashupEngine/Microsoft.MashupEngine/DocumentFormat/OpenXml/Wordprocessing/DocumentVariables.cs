using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF6 RID: 12278
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocumentVariable))]
	internal class DocumentVariables : OpenXmlCompositeElement
	{
		// Token: 0x170095A0 RID: 38304
		// (get) Token: 0x0601ABF0 RID: 109552 RVA: 0x0036706B File Offset: 0x0036526B
		public override string LocalName
		{
			get
			{
				return "docVars";
			}
		}

		// Token: 0x170095A1 RID: 38305
		// (get) Token: 0x0601ABF1 RID: 109553 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095A2 RID: 38306
		// (get) Token: 0x0601ABF2 RID: 109554 RVA: 0x00367072 File Offset: 0x00365272
		internal override int ElementTypeId
		{
			get
			{
				return 12039;
			}
		}

		// Token: 0x0601ABF3 RID: 109555 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ABF4 RID: 109556 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocumentVariables()
		{
		}

		// Token: 0x0601ABF5 RID: 109557 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocumentVariables(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ABF6 RID: 109558 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocumentVariables(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ABF7 RID: 109559 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocumentVariables(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ABF8 RID: 109560 RVA: 0x00367079 File Offset: 0x00365279
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docVar" == name)
			{
				return new DocumentVariable();
			}
			return null;
		}

		// Token: 0x0601ABF9 RID: 109561 RVA: 0x00367094 File Offset: 0x00365294
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentVariables>(deep);
		}

		// Token: 0x0400AE26 RID: 44582
		private const string tagName = "docVars";

		// Token: 0x0400AE27 RID: 44583
		private const byte tagNsId = 23;

		// Token: 0x0400AE28 RID: 44584
		internal const int ElementTypeIdConst = 12039;
	}
}
