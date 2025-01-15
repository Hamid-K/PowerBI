using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3E RID: 10814
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BuildDiagram))]
	[ChildElementInfo(typeof(BuildChart))]
	internal class BuildSubElement : OpenXmlCompositeElement
	{
		// Token: 0x17007137 RID: 28983
		// (get) Token: 0x06015C03 RID: 89091 RVA: 0x00322B60 File Offset: 0x00320D60
		public override string LocalName
		{
			get
			{
				return "bldSub";
			}
		}

		// Token: 0x17007138 RID: 28984
		// (get) Token: 0x06015C04 RID: 89092 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007139 RID: 28985
		// (get) Token: 0x06015C05 RID: 89093 RVA: 0x00322B67 File Offset: 0x00320D67
		internal override int ElementTypeId
		{
			get
			{
				return 12233;
			}
		}

		// Token: 0x06015C06 RID: 89094 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015C07 RID: 89095 RVA: 0x00293ECF File Offset: 0x002920CF
		public BuildSubElement()
		{
		}

		// Token: 0x06015C08 RID: 89096 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BuildSubElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C09 RID: 89097 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BuildSubElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C0A RID: 89098 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BuildSubElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C0B RID: 89099 RVA: 0x00322B6E File Offset: 0x00320D6E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bldDgm" == name)
			{
				return new BuildDiagram();
			}
			if (10 == namespaceId && "bldChart" == name)
			{
				return new BuildChart();
			}
			return null;
		}

		// Token: 0x1700713A RID: 28986
		// (get) Token: 0x06015C0C RID: 89100 RVA: 0x00322BA1 File Offset: 0x00320DA1
		internal override string[] ElementTagNames
		{
			get
			{
				return BuildSubElement.eleTagNames;
			}
		}

		// Token: 0x1700713B RID: 28987
		// (get) Token: 0x06015C0D RID: 89101 RVA: 0x00322BA8 File Offset: 0x00320DA8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BuildSubElement.eleNamespaceIds;
			}
		}

		// Token: 0x1700713C RID: 28988
		// (get) Token: 0x06015C0E RID: 89102 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700713D RID: 28989
		// (get) Token: 0x06015C0F RID: 89103 RVA: 0x00322BAF File Offset: 0x00320DAF
		// (set) Token: 0x06015C10 RID: 89104 RVA: 0x00322BB8 File Offset: 0x00320DB8
		public BuildDiagram BuildDiagram
		{
			get
			{
				return base.GetElement<BuildDiagram>(0);
			}
			set
			{
				base.SetElement<BuildDiagram>(0, value);
			}
		}

		// Token: 0x1700713E RID: 28990
		// (get) Token: 0x06015C11 RID: 89105 RVA: 0x00322BC2 File Offset: 0x00320DC2
		// (set) Token: 0x06015C12 RID: 89106 RVA: 0x00322BCB File Offset: 0x00320DCB
		public BuildChart BuildChart
		{
			get
			{
				return base.GetElement<BuildChart>(1);
			}
			set
			{
				base.SetElement<BuildChart>(1, value);
			}
		}

		// Token: 0x06015C13 RID: 89107 RVA: 0x00322BD5 File Offset: 0x00320DD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildSubElement>(deep);
		}

		// Token: 0x040094AA RID: 38058
		private const string tagName = "bldSub";

		// Token: 0x040094AB RID: 38059
		private const byte tagNsId = 24;

		// Token: 0x040094AC RID: 38060
		internal const int ElementTypeIdConst = 12233;

		// Token: 0x040094AD RID: 38061
		private static readonly string[] eleTagNames = new string[] { "bldDgm", "bldChart" };

		// Token: 0x040094AE RID: 38062
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
