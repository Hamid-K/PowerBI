using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002898 RID: 10392
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GroupShapeLocks))]
	internal class NonVisualGroupShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067EA RID: 26602
		// (get) Token: 0x060146C5 RID: 83653 RVA: 0x002DF2E9 File Offset: 0x002DD4E9
		public override string LocalName
		{
			get
			{
				return "cNvGrpSpPr";
			}
		}

		// Token: 0x170067EB RID: 26603
		// (get) Token: 0x060146C6 RID: 83654 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067EC RID: 26604
		// (get) Token: 0x060146C7 RID: 83655 RVA: 0x00313075 File Offset: 0x00311275
		internal override int ElementTypeId
		{
			get
			{
				return 10753;
			}
		}

		// Token: 0x060146C8 RID: 83656 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060146C9 RID: 83657 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeDrawingProperties()
		{
		}

		// Token: 0x060146CA RID: 83658 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146CB RID: 83659 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146CC RID: 83660 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060146CD RID: 83661 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
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

		// Token: 0x170067ED RID: 26605
		// (get) Token: 0x060146CE RID: 83662 RVA: 0x0031307C File Offset: 0x0031127C
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067EE RID: 26606
		// (get) Token: 0x060146CF RID: 83663 RVA: 0x00313083 File Offset: 0x00311283
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067EF RID: 26607
		// (get) Token: 0x060146D0 RID: 83664 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067F0 RID: 26608
		// (get) Token: 0x060146D1 RID: 83665 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x060146D2 RID: 83666 RVA: 0x002DF341 File Offset: 0x002DD541
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

		// Token: 0x170067F1 RID: 26609
		// (get) Token: 0x060146D3 RID: 83667 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060146D4 RID: 83668 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060146D5 RID: 83669 RVA: 0x0031308A File Offset: 0x0031128A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeDrawingProperties>(deep);
		}

		// Token: 0x04008E08 RID: 36360
		private const string tagName = "cNvGrpSpPr";

		// Token: 0x04008E09 RID: 36361
		private const byte tagNsId = 18;

		// Token: 0x04008E0A RID: 36362
		internal const int ElementTypeIdConst = 10753;

		// Token: 0x04008E0B RID: 36363
		private static readonly string[] eleTagNames = new string[] { "grpSpLocks", "extLst" };

		// Token: 0x04008E0C RID: 36364
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
