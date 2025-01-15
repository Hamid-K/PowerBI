using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002955 RID: 10581
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BorderBoxProperties))]
	[ChildElementInfo(typeof(Base))]
	internal class BorderBox : OpenXmlCompositeElement
	{
		// Token: 0x17006B9D RID: 27549
		// (get) Token: 0x06014F9F RID: 85919 RVA: 0x00319809 File Offset: 0x00317A09
		public override string LocalName
		{
			get
			{
				return "borderBox";
			}
		}

		// Token: 0x17006B9E RID: 27550
		// (get) Token: 0x06014FA0 RID: 85920 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B9F RID: 27551
		// (get) Token: 0x06014FA1 RID: 85921 RVA: 0x00319810 File Offset: 0x00317A10
		internal override int ElementTypeId
		{
			get
			{
				return 10845;
			}
		}

		// Token: 0x06014FA2 RID: 85922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FA3 RID: 85923 RVA: 0x00293ECF File Offset: 0x002920CF
		public BorderBox()
		{
		}

		// Token: 0x06014FA4 RID: 85924 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BorderBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FA5 RID: 85925 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BorderBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FA6 RID: 85926 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BorderBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014FA7 RID: 85927 RVA: 0x00319817 File Offset: 0x00317A17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "borderBoxPr" == name)
			{
				return new BorderBoxProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BA0 RID: 27552
		// (get) Token: 0x06014FA8 RID: 85928 RVA: 0x0031984A File Offset: 0x00317A4A
		internal override string[] ElementTagNames
		{
			get
			{
				return BorderBox.eleTagNames;
			}
		}

		// Token: 0x17006BA1 RID: 27553
		// (get) Token: 0x06014FA9 RID: 85929 RVA: 0x00319851 File Offset: 0x00317A51
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BorderBox.eleNamespaceIds;
			}
		}

		// Token: 0x17006BA2 RID: 27554
		// (get) Token: 0x06014FAA RID: 85930 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BA3 RID: 27555
		// (get) Token: 0x06014FAB RID: 85931 RVA: 0x00319858 File Offset: 0x00317A58
		// (set) Token: 0x06014FAC RID: 85932 RVA: 0x00319861 File Offset: 0x00317A61
		public BorderBoxProperties BorderBoxProperties
		{
			get
			{
				return base.GetElement<BorderBoxProperties>(0);
			}
			set
			{
				base.SetElement<BorderBoxProperties>(0, value);
			}
		}

		// Token: 0x17006BA4 RID: 27556
		// (get) Token: 0x06014FAD RID: 85933 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06014FAE RID: 85934 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x06014FAF RID: 85935 RVA: 0x0031986B File Offset: 0x00317A6B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BorderBox>(deep);
		}

		// Token: 0x040090E8 RID: 37096
		private const string tagName = "borderBox";

		// Token: 0x040090E9 RID: 37097
		private const byte tagNsId = 21;

		// Token: 0x040090EA RID: 37098
		internal const int ElementTypeIdConst = 10845;

		// Token: 0x040090EB RID: 37099
		private static readonly string[] eleTagNames = new string[] { "borderBoxPr", "e" };

		// Token: 0x040090EC RID: 37100
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
