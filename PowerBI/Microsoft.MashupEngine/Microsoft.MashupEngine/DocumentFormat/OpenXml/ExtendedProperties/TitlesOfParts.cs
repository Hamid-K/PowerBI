using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.VariantTypes;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200294D RID: 10573
	[ChildElementInfo(typeof(VTVector))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TitlesOfParts : OpenXmlCompositeElement
	{
		// Token: 0x17006B63 RID: 27491
		// (get) Token: 0x06014F1F RID: 85791 RVA: 0x00318EF8 File Offset: 0x003170F8
		public override string LocalName
		{
			get
			{
				return "TitlesOfParts";
			}
		}

		// Token: 0x17006B64 RID: 27492
		// (get) Token: 0x06014F20 RID: 85792 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B65 RID: 27493
		// (get) Token: 0x06014F21 RID: 85793 RVA: 0x00318EFF File Offset: 0x003170FF
		internal override int ElementTypeId
		{
			get
			{
				return 11015;
			}
		}

		// Token: 0x06014F22 RID: 85794 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F23 RID: 85795 RVA: 0x00293ECF File Offset: 0x002920CF
		public TitlesOfParts()
		{
		}

		// Token: 0x06014F24 RID: 85796 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TitlesOfParts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F25 RID: 85797 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TitlesOfParts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F26 RID: 85798 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TitlesOfParts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F27 RID: 85799 RVA: 0x00318E4C File Offset: 0x0031704C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "vector" == name)
			{
				return new VTVector();
			}
			return null;
		}

		// Token: 0x17006B66 RID: 27494
		// (get) Token: 0x06014F28 RID: 85800 RVA: 0x00318F06 File Offset: 0x00317106
		internal override string[] ElementTagNames
		{
			get
			{
				return TitlesOfParts.eleTagNames;
			}
		}

		// Token: 0x17006B67 RID: 27495
		// (get) Token: 0x06014F29 RID: 85801 RVA: 0x00318F0D File Offset: 0x0031710D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TitlesOfParts.eleNamespaceIds;
			}
		}

		// Token: 0x17006B68 RID: 27496
		// (get) Token: 0x06014F2A RID: 85802 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B69 RID: 27497
		// (get) Token: 0x06014F2B RID: 85803 RVA: 0x003169D2 File Offset: 0x00314BD2
		// (set) Token: 0x06014F2C RID: 85804 RVA: 0x003169DB File Offset: 0x00314BDB
		public VTVector VTVector
		{
			get
			{
				return base.GetElement<VTVector>(0);
			}
			set
			{
				base.SetElement<VTVector>(0, value);
			}
		}

		// Token: 0x06014F2D RID: 85805 RVA: 0x00318F14 File Offset: 0x00317114
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TitlesOfParts>(deep);
		}

		// Token: 0x040090C0 RID: 37056
		private const string tagName = "TitlesOfParts";

		// Token: 0x040090C1 RID: 37057
		private const byte tagNsId = 3;

		// Token: 0x040090C2 RID: 37058
		internal const int ElementTypeIdConst = 11015;

		// Token: 0x040090C3 RID: 37059
		private static readonly string[] eleTagNames = new string[] { "vector" };

		// Token: 0x040090C4 RID: 37060
		private static readonly byte[] eleNamespaceIds = new byte[] { 5 };
	}
}
