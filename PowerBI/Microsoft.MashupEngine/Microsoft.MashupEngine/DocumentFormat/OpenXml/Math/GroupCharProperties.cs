using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AD RID: 10669
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VerticalJustification))]
	[ChildElementInfo(typeof(AccentChar))]
	internal class GroupCharProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D61 RID: 28001
		// (get) Token: 0x06015387 RID: 86919 RVA: 0x0031D020 File Offset: 0x0031B220
		public override string LocalName
		{
			get
			{
				return "groupChrPr";
			}
		}

		// Token: 0x17006D62 RID: 28002
		// (get) Token: 0x06015388 RID: 86920 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D63 RID: 28003
		// (get) Token: 0x06015389 RID: 86921 RVA: 0x0031D027 File Offset: 0x0031B227
		internal override int ElementTypeId
		{
			get
			{
				return 10908;
			}
		}

		// Token: 0x0601538A RID: 86922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601538B RID: 86923 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupCharProperties()
		{
		}

		// Token: 0x0601538C RID: 86924 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupCharProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601538D RID: 86925 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupCharProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601538E RID: 86926 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupCharProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601538F RID: 86927 RVA: 0x0031D030 File Offset: 0x0031B230
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "chr" == name)
			{
				return new AccentChar();
			}
			if (21 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			if (21 == namespaceId && "vertJc" == name)
			{
				return new VerticalJustification();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D64 RID: 28004
		// (get) Token: 0x06015390 RID: 86928 RVA: 0x0031D09E File Offset: 0x0031B29E
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupCharProperties.eleTagNames;
			}
		}

		// Token: 0x17006D65 RID: 28005
		// (get) Token: 0x06015391 RID: 86929 RVA: 0x0031D0A5 File Offset: 0x0031B2A5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupCharProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D66 RID: 28006
		// (get) Token: 0x06015392 RID: 86930 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D67 RID: 28007
		// (get) Token: 0x06015393 RID: 86931 RVA: 0x0031BAAE File Offset: 0x00319CAE
		// (set) Token: 0x06015394 RID: 86932 RVA: 0x0031BAB7 File Offset: 0x00319CB7
		public AccentChar AccentChar
		{
			get
			{
				return base.GetElement<AccentChar>(0);
			}
			set
			{
				base.SetElement<AccentChar>(0, value);
			}
		}

		// Token: 0x17006D68 RID: 28008
		// (get) Token: 0x06015395 RID: 86933 RVA: 0x0031D0AC File Offset: 0x0031B2AC
		// (set) Token: 0x06015396 RID: 86934 RVA: 0x0031D0B5 File Offset: 0x0031B2B5
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(1);
			}
			set
			{
				base.SetElement<Position>(1, value);
			}
		}

		// Token: 0x17006D69 RID: 28009
		// (get) Token: 0x06015397 RID: 86935 RVA: 0x0031D0BF File Offset: 0x0031B2BF
		// (set) Token: 0x06015398 RID: 86936 RVA: 0x0031D0C8 File Offset: 0x0031B2C8
		public VerticalJustification VerticalJustification
		{
			get
			{
				return base.GetElement<VerticalJustification>(2);
			}
			set
			{
				base.SetElement<VerticalJustification>(2, value);
			}
		}

		// Token: 0x17006D6A RID: 28010
		// (get) Token: 0x06015399 RID: 86937 RVA: 0x0031D0D2 File Offset: 0x0031B2D2
		// (set) Token: 0x0601539A RID: 86938 RVA: 0x0031D0DB File Offset: 0x0031B2DB
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(3);
			}
			set
			{
				base.SetElement<ControlProperties>(3, value);
			}
		}

		// Token: 0x0601539B RID: 86939 RVA: 0x0031D0E5 File Offset: 0x0031B2E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupCharProperties>(deep);
		}

		// Token: 0x0400922A RID: 37418
		private const string tagName = "groupChrPr";

		// Token: 0x0400922B RID: 37419
		private const byte tagNsId = 21;

		// Token: 0x0400922C RID: 37420
		internal const int ElementTypeIdConst = 10908;

		// Token: 0x0400922D RID: 37421
		private static readonly string[] eleTagNames = new string[] { "chr", "pos", "vertJc", "ctrlPr" };

		// Token: 0x0400922E RID: 37422
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21 };
	}
}
