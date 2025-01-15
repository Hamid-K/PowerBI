using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6F RID: 10863
	[ChildElementInfo(typeof(BuildList))]
	[ChildElementInfo(typeof(TimeNodeList))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Timing : OpenXmlCompositeElement
	{
		// Token: 0x170072E9 RID: 29417
		// (get) Token: 0x06015FAF RID: 90031 RVA: 0x00325658 File Offset: 0x00323858
		public override string LocalName
		{
			get
			{
				return "timing";
			}
		}

		// Token: 0x170072EA RID: 29418
		// (get) Token: 0x06015FB0 RID: 90032 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072EB RID: 29419
		// (get) Token: 0x06015FB1 RID: 90033 RVA: 0x0032565F File Offset: 0x0032385F
		internal override int ElementTypeId
		{
			get
			{
				return 12281;
			}
		}

		// Token: 0x06015FB2 RID: 90034 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015FB3 RID: 90035 RVA: 0x00293ECF File Offset: 0x002920CF
		public Timing()
		{
		}

		// Token: 0x06015FB4 RID: 90036 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Timing(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FB5 RID: 90037 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Timing(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FB6 RID: 90038 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Timing(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015FB7 RID: 90039 RVA: 0x00325668 File Offset: 0x00323868
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tnLst" == name)
			{
				return new TimeNodeList();
			}
			if (24 == namespaceId && "bldLst" == name)
			{
				return new BuildList();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x170072EC RID: 29420
		// (get) Token: 0x06015FB8 RID: 90040 RVA: 0x003256BE File Offset: 0x003238BE
		internal override string[] ElementTagNames
		{
			get
			{
				return Timing.eleTagNames;
			}
		}

		// Token: 0x170072ED RID: 29421
		// (get) Token: 0x06015FB9 RID: 90041 RVA: 0x003256C5 File Offset: 0x003238C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Timing.eleNamespaceIds;
			}
		}

		// Token: 0x170072EE RID: 29422
		// (get) Token: 0x06015FBA RID: 90042 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072EF RID: 29423
		// (get) Token: 0x06015FBB RID: 90043 RVA: 0x00322A99 File Offset: 0x00320C99
		// (set) Token: 0x06015FBC RID: 90044 RVA: 0x00322AA2 File Offset: 0x00320CA2
		public TimeNodeList TimeNodeList
		{
			get
			{
				return base.GetElement<TimeNodeList>(0);
			}
			set
			{
				base.SetElement<TimeNodeList>(0, value);
			}
		}

		// Token: 0x170072F0 RID: 29424
		// (get) Token: 0x06015FBD RID: 90045 RVA: 0x003256CC File Offset: 0x003238CC
		// (set) Token: 0x06015FBE RID: 90046 RVA: 0x003256D5 File Offset: 0x003238D5
		public BuildList BuildList
		{
			get
			{
				return base.GetElement<BuildList>(1);
			}
			set
			{
				base.SetElement<BuildList>(1, value);
			}
		}

		// Token: 0x170072F1 RID: 29425
		// (get) Token: 0x06015FBF RID: 90047 RVA: 0x0031FCA4 File Offset: 0x0031DEA4
		// (set) Token: 0x06015FC0 RID: 90048 RVA: 0x0031FCAD File Offset: 0x0031DEAD
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(2);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(2, value);
			}
		}

		// Token: 0x06015FC1 RID: 90049 RVA: 0x003256DF File Offset: 0x003238DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Timing>(deep);
		}

		// Token: 0x040095AB RID: 38315
		private const string tagName = "timing";

		// Token: 0x040095AC RID: 38316
		private const byte tagNsId = 24;

		// Token: 0x040095AD RID: 38317
		internal const int ElementTypeIdConst = 12281;

		// Token: 0x040095AE RID: 38318
		private static readonly string[] eleTagNames = new string[] { "tnLst", "bldLst", "extLst" };

		// Token: 0x040095AF RID: 38319
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
