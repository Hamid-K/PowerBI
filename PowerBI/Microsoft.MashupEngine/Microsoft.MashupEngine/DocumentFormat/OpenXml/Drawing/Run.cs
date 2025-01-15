using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002760 RID: 10080
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(Text))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Run : OpenXmlCompositeElement
	{
		// Token: 0x170060DE RID: 24798
		// (get) Token: 0x0601368E RID: 79502 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x170060DF RID: 24799
		// (get) Token: 0x0601368F RID: 79503 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060E0 RID: 24800
		// (get) Token: 0x06013690 RID: 79504 RVA: 0x00306B6C File Offset: 0x00304D6C
		internal override int ElementTypeId
		{
			get
			{
				return 10117;
			}
		}

		// Token: 0x06013691 RID: 79505 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013692 RID: 79506 RVA: 0x00293ECF File Offset: 0x002920CF
		public Run()
		{
		}

		// Token: 0x06013693 RID: 79507 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Run(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013694 RID: 79508 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Run(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013695 RID: 79509 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Run(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013696 RID: 79510 RVA: 0x00306B73 File Offset: 0x00304D73
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (10 == namespaceId && "t" == name)
			{
				return new Text();
			}
			return null;
		}

		// Token: 0x170060E1 RID: 24801
		// (get) Token: 0x06013697 RID: 79511 RVA: 0x00306BA6 File Offset: 0x00304DA6
		internal override string[] ElementTagNames
		{
			get
			{
				return Run.eleTagNames;
			}
		}

		// Token: 0x170060E2 RID: 24802
		// (get) Token: 0x06013698 RID: 79512 RVA: 0x00306BAD File Offset: 0x00304DAD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Run.eleNamespaceIds;
			}
		}

		// Token: 0x170060E3 RID: 24803
		// (get) Token: 0x06013699 RID: 79513 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170060E4 RID: 24804
		// (get) Token: 0x0601369A RID: 79514 RVA: 0x00306BB4 File Offset: 0x00304DB4
		// (set) Token: 0x0601369B RID: 79515 RVA: 0x00306BBD File Offset: 0x00304DBD
		public RunProperties RunProperties
		{
			get
			{
				return base.GetElement<RunProperties>(0);
			}
			set
			{
				base.SetElement<RunProperties>(0, value);
			}
		}

		// Token: 0x170060E5 RID: 24805
		// (get) Token: 0x0601369C RID: 79516 RVA: 0x00306BC7 File Offset: 0x00304DC7
		// (set) Token: 0x0601369D RID: 79517 RVA: 0x00306BD0 File Offset: 0x00304DD0
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(1);
			}
			set
			{
				base.SetElement<Text>(1, value);
			}
		}

		// Token: 0x0601369E RID: 79518 RVA: 0x00306BDA File Offset: 0x00304DDA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Run>(deep);
		}

		// Token: 0x04008618 RID: 34328
		private const string tagName = "r";

		// Token: 0x04008619 RID: 34329
		private const byte tagNsId = 10;

		// Token: 0x0400861A RID: 34330
		internal const int ElementTypeIdConst = 10117;

		// Token: 0x0400861B RID: 34331
		private static readonly string[] eleTagNames = new string[] { "rPr", "t" };

		// Token: 0x0400861C RID: 34332
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
