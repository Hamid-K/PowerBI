using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002582 RID: 9602
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StringPoint))]
	internal class Level : OpenXmlCompositeElement
	{
		// Token: 0x17005625 RID: 22053
		// (get) Token: 0x06011EBC RID: 73404 RVA: 0x002F3AB9 File Offset: 0x002F1CB9
		public override string LocalName
		{
			get
			{
				return "lvl";
			}
		}

		// Token: 0x17005626 RID: 22054
		// (get) Token: 0x06011EBD RID: 73405 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005627 RID: 22055
		// (get) Token: 0x06011EBE RID: 73406 RVA: 0x002F3AC0 File Offset: 0x002F1CC0
		internal override int ElementTypeId
		{
			get
			{
				return 10401;
			}
		}

		// Token: 0x06011EBF RID: 73407 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011EC0 RID: 73408 RVA: 0x00293ECF File Offset: 0x002920CF
		public Level()
		{
		}

		// Token: 0x06011EC1 RID: 73409 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Level(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EC2 RID: 73410 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Level(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EC3 RID: 73411 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Level(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011EC4 RID: 73412 RVA: 0x002F3AC7 File Offset: 0x002F1CC7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "pt" == name)
			{
				return new StringPoint();
			}
			return null;
		}

		// Token: 0x06011EC5 RID: 73413 RVA: 0x002F3AE2 File Offset: 0x002F1CE2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Level>(deep);
		}

		// Token: 0x04007D41 RID: 32065
		private const string tagName = "lvl";

		// Token: 0x04007D42 RID: 32066
		private const byte tagNsId = 11;

		// Token: 0x04007D43 RID: 32067
		internal const int ElementTypeIdConst = 10401;
	}
}
