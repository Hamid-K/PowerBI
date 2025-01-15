using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6C RID: 10860
	[ChildElementInfo(typeof(GroupShapeLocks))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualGroupShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170072CA RID: 29386
		// (get) Token: 0x06015F6A RID: 89962 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x170072CB RID: 29387
		// (get) Token: 0x06015F6B RID: 89963 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072CC RID: 29388
		// (get) Token: 0x06015F6C RID: 89964 RVA: 0x00324F6F File Offset: 0x0032316F
		internal override int ElementTypeId
		{
			get
			{
				return 12278;
			}
		}

		// Token: 0x06015F6D RID: 89965 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015F6E RID: 89966 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeDrawingProperties()
		{
		}

		// Token: 0x06015F6F RID: 89967 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F70 RID: 89968 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F71 RID: 89969 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F72 RID: 89970 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "grpSpLocks" == name)
			{
				return new GroupShapeLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170072CD RID: 29389
		// (get) Token: 0x06015F73 RID: 89971 RVA: 0x00324F76 File Offset: 0x00323176
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170072CE RID: 29390
		// (get) Token: 0x06015F74 RID: 89972 RVA: 0x00324F7D File Offset: 0x0032317D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170072CF RID: 29391
		// (get) Token: 0x06015F75 RID: 89973 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072D0 RID: 29392
		// (get) Token: 0x06015F76 RID: 89974 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x06015F77 RID: 89975 RVA: 0x002DF341 File Offset: 0x002DD541
		public GroupShapeLocks GroupShapeLocks
		{
			get
			{
				return base.GetElement<GroupShapeLocks>(0);
			}
			set
			{
				base.SetElement<GroupShapeLocks>(0, value);
			}
		}

		// Token: 0x170072D1 RID: 29393
		// (get) Token: 0x06015F78 RID: 89976 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06015F79 RID: 89977 RVA: 0x002DEB73 File Offset: 0x002DCD73
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06015F7A RID: 89978 RVA: 0x00324F84 File Offset: 0x00323184
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeDrawingProperties>(deep);
		}

		// Token: 0x0400959A RID: 38298
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x0400959B RID: 38299
		private const byte tagNsId = 24;

		// Token: 0x0400959C RID: 38300
		internal const int ElementTypeIdConst = 12278;

		// Token: 0x0400959D RID: 38301
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x0400959E RID: 38302
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
