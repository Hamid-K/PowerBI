using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A0 RID: 9376
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LinearShadeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PathShadeProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GradientStopList), FileFormatVersions.Office2010)]
	internal class GradientFillProperties : OpenXmlCompositeElement
	{
		// Token: 0x170051C8 RID: 20936
		// (get) Token: 0x06011507 RID: 70919 RVA: 0x002ED0BD File Offset: 0x002EB2BD
		public override string LocalName
		{
			get
			{
				return "gradFill";
			}
		}

		// Token: 0x170051C9 RID: 20937
		// (get) Token: 0x06011508 RID: 70920 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051CA RID: 20938
		// (get) Token: 0x06011509 RID: 70921 RVA: 0x002ED0C4 File Offset: 0x002EB2C4
		internal override int ElementTypeId
		{
			get
			{
				return 12848;
			}
		}

		// Token: 0x0601150A RID: 70922 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601150B RID: 70923 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientFillProperties()
		{
		}

		// Token: 0x0601150C RID: 70924 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientFillProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601150D RID: 70925 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientFillProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601150E RID: 70926 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientFillProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601150F RID: 70927 RVA: 0x002ED0CC File Offset: 0x002EB2CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "gsLst" == name)
			{
				return new GradientStopList();
			}
			if (52 == namespaceId && "lin" == name)
			{
				return new LinearShadeProperties();
			}
			if (52 == namespaceId && "path" == name)
			{
				return new PathShadeProperties();
			}
			return null;
		}

		// Token: 0x170051CB RID: 20939
		// (get) Token: 0x06011510 RID: 70928 RVA: 0x002ED122 File Offset: 0x002EB322
		internal override string[] ElementTagNames
		{
			get
			{
				return GradientFillProperties.eleTagNames;
			}
		}

		// Token: 0x170051CC RID: 20940
		// (get) Token: 0x06011511 RID: 70929 RVA: 0x002ED129 File Offset: 0x002EB329
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GradientFillProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170051CD RID: 20941
		// (get) Token: 0x06011512 RID: 70930 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170051CE RID: 20942
		// (get) Token: 0x06011513 RID: 70931 RVA: 0x002ED130 File Offset: 0x002EB330
		// (set) Token: 0x06011514 RID: 70932 RVA: 0x002ED139 File Offset: 0x002EB339
		public GradientStopList GradientStopList
		{
			get
			{
				return base.GetElement<GradientStopList>(0);
			}
			set
			{
				base.SetElement<GradientStopList>(0, value);
			}
		}

		// Token: 0x06011515 RID: 70933 RVA: 0x002ED143 File Offset: 0x002EB343
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientFillProperties>(deep);
		}

		// Token: 0x04007942 RID: 31042
		private const string tagName = "gradFill";

		// Token: 0x04007943 RID: 31043
		private const byte tagNsId = 52;

		// Token: 0x04007944 RID: 31044
		internal const int ElementTypeIdConst = 12848;

		// Token: 0x04007945 RID: 31045
		private static readonly string[] eleTagNames = new string[] { "gsLst", "lin", "path" };

		// Token: 0x04007946 RID: 31046
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
