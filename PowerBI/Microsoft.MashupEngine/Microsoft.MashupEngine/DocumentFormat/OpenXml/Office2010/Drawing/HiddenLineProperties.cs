using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002349 RID: 9033
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(LineJoinBevel))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(PresetDash))]
	[ChildElementInfo(typeof(CustomDash))]
	[ChildElementInfo(typeof(Round))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(Miter))]
	[ChildElementInfo(typeof(HeadEnd))]
	[ChildElementInfo(typeof(TailEnd))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HiddenLineProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700499C RID: 18844
		// (get) Token: 0x060102C6 RID: 66246 RVA: 0x002E0868 File Offset: 0x002DEA68
		public override string LocalName
		{
			get
			{
				return "hiddenLine";
			}
		}

		// Token: 0x1700499D RID: 18845
		// (get) Token: 0x060102C7 RID: 66247 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x1700499E RID: 18846
		// (get) Token: 0x060102C8 RID: 66248 RVA: 0x002E086F File Offset: 0x002DEA6F
		internal override int ElementTypeId
		{
			get
			{
				return 12718;
			}
		}

		// Token: 0x060102C9 RID: 66249 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700499F RID: 18847
		// (get) Token: 0x060102CA RID: 66250 RVA: 0x002E0876 File Offset: 0x002DEA76
		internal override string[] AttributeTagNames
		{
			get
			{
				return HiddenLineProperties.attributeTagNames;
			}
		}

		// Token: 0x170049A0 RID: 18848
		// (get) Token: 0x060102CB RID: 66251 RVA: 0x002E087D File Offset: 0x002DEA7D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HiddenLineProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170049A1 RID: 18849
		// (get) Token: 0x060102CC RID: 66252 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060102CD RID: 66253 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "w")]
		public Int32Value Width
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

		// Token: 0x170049A2 RID: 18850
		// (get) Token: 0x060102CE RID: 66254 RVA: 0x002E0884 File Offset: 0x002DEA84
		// (set) Token: 0x060102CF RID: 66255 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cap")]
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

		// Token: 0x170049A3 RID: 18851
		// (get) Token: 0x060102D0 RID: 66256 RVA: 0x002E0893 File Offset: 0x002DEA93
		// (set) Token: 0x060102D1 RID: 66257 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "cmpd")]
		public EnumValue<CompoundLineValues> CompoundLineType
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

		// Token: 0x170049A4 RID: 18852
		// (get) Token: 0x060102D2 RID: 66258 RVA: 0x002E08A2 File Offset: 0x002DEAA2
		// (set) Token: 0x060102D3 RID: 66259 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "algn")]
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

		// Token: 0x060102D4 RID: 66260 RVA: 0x00293ECF File Offset: 0x002920CF
		public HiddenLineProperties()
		{
		}

		// Token: 0x060102D5 RID: 66261 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HiddenLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102D6 RID: 66262 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HiddenLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102D7 RID: 66263 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HiddenLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060102D8 RID: 66264 RVA: 0x002E08B4 File Offset: 0x002DEAB4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "prstDash" == name)
			{
				return new PresetDash();
			}
			if (10 == namespaceId && "custDash" == name)
			{
				return new CustomDash();
			}
			if (10 == namespaceId && "round" == name)
			{
				return new Round();
			}
			if (10 == namespaceId && "bevel" == name)
			{
				return new LineJoinBevel();
			}
			if (10 == namespaceId && "miter" == name)
			{
				return new Miter();
			}
			if (10 == namespaceId && "headEnd" == name)
			{
				return new HeadEnd();
			}
			if (10 == namespaceId && "tailEnd" == name)
			{
				return new TailEnd();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060102D9 RID: 66265 RVA: 0x002E09E4 File Offset: 0x002DEBE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "w" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "cap" == name)
			{
				return new EnumValue<LineCapValues>();
			}
			if (namespaceId == 0 && "cmpd" == name)
			{
				return new EnumValue<CompoundLineValues>();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<PenAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060102DA RID: 66266 RVA: 0x002E0A51 File Offset: 0x002DEC51
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenLineProperties>(deep);
		}

		// Token: 0x060102DB RID: 66267 RVA: 0x002E0A5C File Offset: 0x002DEC5C
		// Note: this type is marked as 'beforefieldinit'.
		static HiddenLineProperties()
		{
			byte[] array = new byte[4];
			HiddenLineProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04007368 RID: 29544
		private const string tagName = "hiddenLine";

		// Token: 0x04007369 RID: 29545
		private const byte tagNsId = 48;

		// Token: 0x0400736A RID: 29546
		internal const int ElementTypeIdConst = 12718;

		// Token: 0x0400736B RID: 29547
		private static string[] attributeTagNames = new string[] { "w", "cap", "cmpd", "algn" };

		// Token: 0x0400736C RID: 29548
		private static byte[] attributeNamespaceIds;
	}
}
