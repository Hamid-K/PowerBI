using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002391 RID: 9105
	[ChildElementInfo(typeof(NonVisualInkContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualContentPartProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004BB8 RID: 19384
		// (get) Token: 0x06010764 RID: 67428 RVA: 0x002DFF82 File Offset: 0x002DE182
		public override string LocalName
		{
			get
			{
				return "nvContentPartPr";
			}
		}

		// Token: 0x17004BB9 RID: 19385
		// (get) Token: 0x06010765 RID: 67429 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BBA RID: 19386
		// (get) Token: 0x06010766 RID: 67430 RVA: 0x002E3C3B File Offset: 0x002E1E3B
		internal override int ElementTypeId
		{
			get
			{
				return 12764;
			}
		}

		// Token: 0x06010767 RID: 67431 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010768 RID: 67432 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualContentPartProperties()
		{
		}

		// Token: 0x06010769 RID: 67433 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601076A RID: 67434 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601076B RID: 67435 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601076C RID: 67436 RVA: 0x002E3C44 File Offset: 0x002E1E44
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (49 == namespaceId && "cNvContentPartPr" == name)
			{
				return new NonVisualInkContentPartProperties();
			}
			if (49 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x17004BBB RID: 19387
		// (get) Token: 0x0601076D RID: 67437 RVA: 0x002E3C9A File Offset: 0x002E1E9A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualContentPartProperties.eleTagNames;
			}
		}

		// Token: 0x17004BBC RID: 19388
		// (get) Token: 0x0601076E RID: 67438 RVA: 0x002E3CA1 File Offset: 0x002E1EA1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualContentPartProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004BBD RID: 19389
		// (get) Token: 0x0601076F RID: 67439 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004BBE RID: 19390
		// (get) Token: 0x06010770 RID: 67440 RVA: 0x002E3CA8 File Offset: 0x002E1EA8
		// (set) Token: 0x06010771 RID: 67441 RVA: 0x002E3CB1 File Offset: 0x002E1EB1
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x17004BBF RID: 19391
		// (get) Token: 0x06010772 RID: 67442 RVA: 0x002E3CBB File Offset: 0x002E1EBB
		// (set) Token: 0x06010773 RID: 67443 RVA: 0x002E3CC4 File Offset: 0x002E1EC4
		public NonVisualInkContentPartProperties NonVisualInkContentPartProperties
		{
			get
			{
				return base.GetElement<NonVisualInkContentPartProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualInkContentPartProperties>(1, value);
			}
		}

		// Token: 0x17004BC0 RID: 19392
		// (get) Token: 0x06010774 RID: 67444 RVA: 0x002E3CCE File Offset: 0x002E1ECE
		// (set) Token: 0x06010775 RID: 67445 RVA: 0x002E3CD7 File Offset: 0x002E1ED7
		public ApplicationNonVisualDrawingProperties ApplicationNonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<ApplicationNonVisualDrawingProperties>(2);
			}
			set
			{
				base.SetElement<ApplicationNonVisualDrawingProperties>(2, value);
			}
		}

		// Token: 0x06010776 RID: 67446 RVA: 0x002E3CE1 File Offset: 0x002E1EE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualContentPartProperties>(deep);
		}

		// Token: 0x040074B5 RID: 29877
		private const string tagName = "nvContentPartPr";

		// Token: 0x040074B6 RID: 29878
		private const byte tagNsId = 49;

		// Token: 0x040074B7 RID: 29879
		internal const int ElementTypeIdConst = 12764;

		// Token: 0x040074B8 RID: 29880
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvContentPartPr", "nvPr" };

		// Token: 0x040074B9 RID: 29881
		private static readonly byte[] eleNamespaceIds = new byte[] { 49, 49, 49 };
	}
}
