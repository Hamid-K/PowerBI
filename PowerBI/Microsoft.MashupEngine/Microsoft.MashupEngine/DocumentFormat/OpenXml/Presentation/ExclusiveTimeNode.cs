using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A17 RID: 10775
	[ChildElementInfo(typeof(CommonTimeNode))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExclusiveTimeNode : OpenXmlCompositeElement
	{
		// Token: 0x17006FEA RID: 28650
		// (get) Token: 0x06015924 RID: 88356 RVA: 0x00320B52 File Offset: 0x0031ED52
		public override string LocalName
		{
			get
			{
				return "excl";
			}
		}

		// Token: 0x17006FEB RID: 28651
		// (get) Token: 0x06015925 RID: 88357 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FEC RID: 28652
		// (get) Token: 0x06015926 RID: 88358 RVA: 0x00320B59 File Offset: 0x0031ED59
		internal override int ElementTypeId
		{
			get
			{
				return 12201;
			}
		}

		// Token: 0x06015927 RID: 88359 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015928 RID: 88360 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExclusiveTimeNode()
		{
		}

		// Token: 0x06015929 RID: 88361 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExclusiveTimeNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601592A RID: 88362 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExclusiveTimeNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601592B RID: 88363 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExclusiveTimeNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601592C RID: 88364 RVA: 0x00320935 File Offset: 0x0031EB35
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cTn" == name)
			{
				return new CommonTimeNode();
			}
			return null;
		}

		// Token: 0x17006FED RID: 28653
		// (get) Token: 0x0601592D RID: 88365 RVA: 0x00320B60 File Offset: 0x0031ED60
		internal override string[] ElementTagNames
		{
			get
			{
				return ExclusiveTimeNode.eleTagNames;
			}
		}

		// Token: 0x17006FEE RID: 28654
		// (get) Token: 0x0601592E RID: 88366 RVA: 0x00320B67 File Offset: 0x0031ED67
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ExclusiveTimeNode.eleNamespaceIds;
			}
		}

		// Token: 0x17006FEF RID: 28655
		// (get) Token: 0x0601592F RID: 88367 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006FF0 RID: 28656
		// (get) Token: 0x06015930 RID: 88368 RVA: 0x0032095E File Offset: 0x0031EB5E
		// (set) Token: 0x06015931 RID: 88369 RVA: 0x00320967 File Offset: 0x0031EB67
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

		// Token: 0x06015932 RID: 88370 RVA: 0x00320B6E File Offset: 0x0031ED6E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExclusiveTimeNode>(deep);
		}

		// Token: 0x040093EE RID: 37870
		private const string tagName = "excl";

		// Token: 0x040093EF RID: 37871
		private const byte tagNsId = 24;

		// Token: 0x040093F0 RID: 37872
		internal const int ElementTypeIdConst = 12201;

		// Token: 0x040093F1 RID: 37873
		private static readonly string[] eleTagNames = new string[] { "cTn" };

		// Token: 0x040093F2 RID: 37874
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
