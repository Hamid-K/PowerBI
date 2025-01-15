using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002703 RID: 9987
	[ChildElementInfo(typeof(ForegroundColor))]
	[ChildElementInfo(typeof(BackgroundColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PatternFill : OpenXmlCompositeElement
	{
		// Token: 0x17005E8E RID: 24206
		// (get) Token: 0x0601316C RID: 78188 RVA: 0x003036D6 File Offset: 0x003018D6
		public override string LocalName
		{
			get
			{
				return "pattFill";
			}
		}

		// Token: 0x17005E8F RID: 24207
		// (get) Token: 0x0601316D RID: 78189 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E90 RID: 24208
		// (get) Token: 0x0601316E RID: 78190 RVA: 0x003036DD File Offset: 0x003018DD
		internal override int ElementTypeId
		{
			get
			{
				return 10051;
			}
		}

		// Token: 0x0601316F RID: 78191 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E91 RID: 24209
		// (get) Token: 0x06013170 RID: 78192 RVA: 0x003036E4 File Offset: 0x003018E4
		internal override string[] AttributeTagNames
		{
			get
			{
				return PatternFill.attributeTagNames;
			}
		}

		// Token: 0x17005E92 RID: 24210
		// (get) Token: 0x06013171 RID: 78193 RVA: 0x003036EB File Offset: 0x003018EB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PatternFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E93 RID: 24211
		// (get) Token: 0x06013172 RID: 78194 RVA: 0x003036F2 File Offset: 0x003018F2
		// (set) Token: 0x06013173 RID: 78195 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prst")]
		public EnumValue<PresetPatternValues> Preset
		{
			get
			{
				return (EnumValue<PresetPatternValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013174 RID: 78196 RVA: 0x00293ECF File Offset: 0x002920CF
		public PatternFill()
		{
		}

		// Token: 0x06013175 RID: 78197 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PatternFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013176 RID: 78198 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PatternFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013177 RID: 78199 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PatternFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013178 RID: 78200 RVA: 0x00303701 File Offset: 0x00301901
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "fgClr" == name)
			{
				return new ForegroundColor();
			}
			if (10 == namespaceId && "bgClr" == name)
			{
				return new BackgroundColor();
			}
			return null;
		}

		// Token: 0x17005E94 RID: 24212
		// (get) Token: 0x06013179 RID: 78201 RVA: 0x00303734 File Offset: 0x00301934
		internal override string[] ElementTagNames
		{
			get
			{
				return PatternFill.eleTagNames;
			}
		}

		// Token: 0x17005E95 RID: 24213
		// (get) Token: 0x0601317A RID: 78202 RVA: 0x0030373B File Offset: 0x0030193B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PatternFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005E96 RID: 24214
		// (get) Token: 0x0601317B RID: 78203 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E97 RID: 24215
		// (get) Token: 0x0601317C RID: 78204 RVA: 0x00303742 File Offset: 0x00301942
		// (set) Token: 0x0601317D RID: 78205 RVA: 0x0030374B File Offset: 0x0030194B
		public ForegroundColor ForegroundColor
		{
			get
			{
				return base.GetElement<ForegroundColor>(0);
			}
			set
			{
				base.SetElement<ForegroundColor>(0, value);
			}
		}

		// Token: 0x17005E98 RID: 24216
		// (get) Token: 0x0601317E RID: 78206 RVA: 0x00303755 File Offset: 0x00301955
		// (set) Token: 0x0601317F RID: 78207 RVA: 0x0030375E File Offset: 0x0030195E
		public BackgroundColor BackgroundColor
		{
			get
			{
				return base.GetElement<BackgroundColor>(1);
			}
			set
			{
				base.SetElement<BackgroundColor>(1, value);
			}
		}

		// Token: 0x06013180 RID: 78208 RVA: 0x00303768 File Offset: 0x00301968
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<PresetPatternValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013181 RID: 78209 RVA: 0x00303788 File Offset: 0x00301988
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PatternFill>(deep);
		}

		// Token: 0x06013182 RID: 78210 RVA: 0x00303794 File Offset: 0x00301994
		// Note: this type is marked as 'beforefieldinit'.
		static PatternFill()
		{
			byte[] array = new byte[1];
			PatternFill.attributeNamespaceIds = array;
			PatternFill.eleTagNames = new string[] { "fgClr", "bgClr" };
			PatternFill.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400849B RID: 33947
		private const string tagName = "pattFill";

		// Token: 0x0400849C RID: 33948
		private const byte tagNsId = 10;

		// Token: 0x0400849D RID: 33949
		internal const int ElementTypeIdConst = 10051;

		// Token: 0x0400849E RID: 33950
		private static string[] attributeTagNames = new string[] { "prst" };

		// Token: 0x0400849F RID: 33951
		private static byte[] attributeNamespaceIds;

		// Token: 0x040084A0 RID: 33952
		private static readonly string[] eleTagNames;

		// Token: 0x040084A1 RID: 33953
		private static readonly byte[] eleNamespaceIds;
	}
}
