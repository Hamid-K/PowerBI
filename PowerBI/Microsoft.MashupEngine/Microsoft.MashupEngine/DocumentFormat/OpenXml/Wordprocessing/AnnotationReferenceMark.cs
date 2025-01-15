using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E65 RID: 11877
	[GeneratedCode("DomGen", "2.0")]
	internal class AnnotationReferenceMark : EmptyType
	{
		// Token: 0x17008A70 RID: 35440
		// (get) Token: 0x060193C1 RID: 103361 RVA: 0x00347ACC File Offset: 0x00345CCC
		public override string LocalName
		{
			get
			{
				return "annotationRef";
			}
		}

		// Token: 0x17008A71 RID: 35441
		// (get) Token: 0x060193C2 RID: 103362 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A72 RID: 35442
		// (get) Token: 0x060193C3 RID: 103363 RVA: 0x00347AD3 File Offset: 0x00345CD3
		internal override int ElementTypeId
		{
			get
			{
				return 11556;
			}
		}

		// Token: 0x060193C4 RID: 103364 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193C6 RID: 103366 RVA: 0x00347ADA File Offset: 0x00345CDA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnnotationReferenceMark>(deep);
		}

		// Token: 0x0400A7C7 RID: 42951
		private const string tagName = "annotationRef";

		// Token: 0x0400A7C8 RID: 42952
		private const byte tagNsId = 23;

		// Token: 0x0400A7C9 RID: 42953
		internal const int ElementTypeIdConst = 11556;
	}
}
