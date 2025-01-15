using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A6 RID: 9382
	[ChildElementInfo(typeof(SolidColorFillProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NoFillEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GradientFillProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PresetLineDashProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RoundEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BevelEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LineJoinMiterProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TextOutlineEffect : OpenXmlCompositeElement
	{
		// Token: 0x1700520A RID: 21002
		// (get) Token: 0x0601158F RID: 71055 RVA: 0x002ED782 File Offset: 0x002EB982
		public override string LocalName
		{
			get
			{
				return "textOutline";
			}
		}

		// Token: 0x1700520B RID: 21003
		// (get) Token: 0x06011590 RID: 71056 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700520C RID: 21004
		// (get) Token: 0x06011591 RID: 71057 RVA: 0x002ED789 File Offset: 0x002EB989
		internal override int ElementTypeId
		{
			get
			{
				return 12856;
			}
		}

		// Token: 0x06011592 RID: 71058 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700520D RID: 21005
		// (get) Token: 0x06011593 RID: 71059 RVA: 0x002ED790 File Offset: 0x002EB990
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextOutlineEffect.attributeTagNames;
			}
		}

		// Token: 0x1700520E RID: 21006
		// (get) Token: 0x06011594 RID: 71060 RVA: 0x002ED797 File Offset: 0x002EB997
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextOutlineEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x1700520F RID: 21007
		// (get) Token: 0x06011595 RID: 71061 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011596 RID: 71062 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "w")]
		public Int32Value LineWidth
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005210 RID: 21008
		// (get) Token: 0x06011597 RID: 71063 RVA: 0x002ED79E File Offset: 0x002EB99E
		// (set) Token: 0x06011598 RID: 71064 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "cap")]
		public EnumValue<LineCapValues> CapType
		{
			get
			{
				return (EnumValue<LineCapValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005211 RID: 21009
		// (get) Token: 0x06011599 RID: 71065 RVA: 0x002ED7AD File Offset: 0x002EB9AD
		// (set) Token: 0x0601159A RID: 71066 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "cmpd")]
		public EnumValue<CompoundLineValues> Compound
		{
			get
			{
				return (EnumValue<CompoundLineValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005212 RID: 21010
		// (get) Token: 0x0601159B RID: 71067 RVA: 0x002ED7BC File Offset: 0x002EB9BC
		// (set) Token: 0x0601159C RID: 71068 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(52, "algn")]
		public EnumValue<PenAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<PenAlignmentValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601159D RID: 71069 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextOutlineEffect()
		{
		}

		// Token: 0x0601159E RID: 71070 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextOutlineEffect(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601159F RID: 71071 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextOutlineEffect(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115A0 RID: 71072 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextOutlineEffect(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060115A1 RID: 71073 RVA: 0x002ED7CC File Offset: 0x002EB9CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "noFill" == name)
			{
				return new NoFillEmpty();
			}
			if (52 == namespaceId && "solidFill" == name)
			{
				return new SolidColorFillProperties();
			}
			if (52 == namespaceId && "gradFill" == name)
			{
				return new GradientFillProperties();
			}
			if (52 == namespaceId && "prstDash" == name)
			{
				return new PresetLineDashProperties();
			}
			if (52 == namespaceId && "round" == name)
			{
				return new RoundEmpty();
			}
			if (52 == namespaceId && "bevel" == name)
			{
				return new BevelEmpty();
			}
			if (52 == namespaceId && "miter" == name)
			{
				return new LineJoinMiterProperties();
			}
			return null;
		}

		// Token: 0x060115A2 RID: 71074 RVA: 0x002ED884 File Offset: 0x002EBA84
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "w" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "cap" == name)
			{
				return new EnumValue<LineCapValues>();
			}
			if (52 == namespaceId && "cmpd" == name)
			{
				return new EnumValue<CompoundLineValues>();
			}
			if (52 == namespaceId && "algn" == name)
			{
				return new EnumValue<PenAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060115A3 RID: 71075 RVA: 0x002ED8F9 File Offset: 0x002EBAF9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextOutlineEffect>(deep);
		}

		// Token: 0x04007964 RID: 31076
		private const string tagName = "textOutline";

		// Token: 0x04007965 RID: 31077
		private const byte tagNsId = 52;

		// Token: 0x04007966 RID: 31078
		internal const int ElementTypeIdConst = 12856;

		// Token: 0x04007967 RID: 31079
		private static string[] attributeTagNames = new string[] { "w", "cap", "cmpd", "algn" };

		// Token: 0x04007968 RID: 31080
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52, 52 };
	}
}
