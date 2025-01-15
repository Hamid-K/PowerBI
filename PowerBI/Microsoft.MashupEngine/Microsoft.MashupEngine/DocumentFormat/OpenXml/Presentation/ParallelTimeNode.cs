using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A15 RID: 10773
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonTimeNode))]
	internal class ParallelTimeNode : OpenXmlCompositeElement
	{
		// Token: 0x17006FD5 RID: 28629
		// (get) Token: 0x060158F7 RID: 88311 RVA: 0x00320927 File Offset: 0x0031EB27
		public override string LocalName
		{
			get
			{
				return "par";
			}
		}

		// Token: 0x17006FD6 RID: 28630
		// (get) Token: 0x060158F8 RID: 88312 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FD7 RID: 28631
		// (get) Token: 0x060158F9 RID: 88313 RVA: 0x0032092E File Offset: 0x0031EB2E
		internal override int ElementTypeId
		{
			get
			{
				return 12199;
			}
		}

		// Token: 0x060158FA RID: 88314 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060158FB RID: 88315 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParallelTimeNode()
		{
		}

		// Token: 0x060158FC RID: 88316 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParallelTimeNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158FD RID: 88317 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParallelTimeNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158FE RID: 88318 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParallelTimeNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060158FF RID: 88319 RVA: 0x00320935 File Offset: 0x0031EB35
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cTn" == name)
			{
				return new CommonTimeNode();
			}
			return null;
		}

		// Token: 0x17006FD8 RID: 28632
		// (get) Token: 0x06015900 RID: 88320 RVA: 0x00320950 File Offset: 0x0031EB50
		internal override string[] ElementTagNames
		{
			get
			{
				return ParallelTimeNode.eleTagNames;
			}
		}

		// Token: 0x17006FD9 RID: 28633
		// (get) Token: 0x06015901 RID: 88321 RVA: 0x00320957 File Offset: 0x0031EB57
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParallelTimeNode.eleNamespaceIds;
			}
		}

		// Token: 0x17006FDA RID: 28634
		// (get) Token: 0x06015902 RID: 88322 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006FDB RID: 28635
		// (get) Token: 0x06015903 RID: 88323 RVA: 0x0032095E File Offset: 0x0031EB5E
		// (set) Token: 0x06015904 RID: 88324 RVA: 0x00320967 File Offset: 0x0031EB67
		public CommonTimeNode CommonTimeNode
		{
			get
			{
				return base.GetElement<CommonTimeNode>(0);
			}
			set
			{
				base.SetElement<CommonTimeNode>(0, value);
			}
		}

		// Token: 0x06015905 RID: 88325 RVA: 0x00320971 File Offset: 0x0031EB71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParallelTimeNode>(deep);
		}

		// Token: 0x040093E2 RID: 37858
		private const string tagName = "par";

		// Token: 0x040093E3 RID: 37859
		private const byte tagNsId = 24;

		// Token: 0x040093E4 RID: 37860
		internal const int ElementTypeIdConst = 12199;

		// Token: 0x040093E5 RID: 37861
		private static readonly string[] eleTagNames = new string[] { "cTn" };

		// Token: 0x040093E6 RID: 37862
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
