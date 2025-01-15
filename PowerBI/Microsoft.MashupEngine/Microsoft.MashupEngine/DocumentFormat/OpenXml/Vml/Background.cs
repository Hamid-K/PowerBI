using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224E RID: 8782
	[ChildElementInfo(typeof(Fill))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Background : OpenXmlCompositeElement
	{
		// Token: 0x17003ABD RID: 15037
		// (get) Token: 0x0600E344 RID: 58180 RVA: 0x002C30A6 File Offset: 0x002C12A6
		public override string LocalName
		{
			get
			{
				return "background";
			}
		}

		// Token: 0x17003ABE RID: 15038
		// (get) Token: 0x0600E345 RID: 58181 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003ABF RID: 15039
		// (get) Token: 0x0600E346 RID: 58182 RVA: 0x002C30AD File Offset: 0x002C12AD
		internal override int ElementTypeId
		{
			get
			{
				return 12518;
			}
		}

		// Token: 0x0600E347 RID: 58183 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003AC0 RID: 15040
		// (get) Token: 0x0600E348 RID: 58184 RVA: 0x002C30B4 File Offset: 0x002C12B4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Background.attributeTagNames;
			}
		}

		// Token: 0x17003AC1 RID: 15041
		// (get) Token: 0x0600E349 RID: 58185 RVA: 0x002C30BB File Offset: 0x002C12BB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Background.attributeNamespaceIds;
			}
		}

		// Token: 0x17003AC2 RID: 15042
		// (get) Token: 0x0600E34A RID: 58186 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E34B RID: 58187 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003AC3 RID: 15043
		// (get) Token: 0x0600E34C RID: 58188 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600E34D RID: 58189 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fill")]
		public TrueFalseValue Filled
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003AC4 RID: 15044
		// (get) Token: 0x0600E34E RID: 58190 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E34F RID: 58191 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fillcolor")]
		public StringValue Fillcolor
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003AC5 RID: 15045
		// (get) Token: 0x0600E350 RID: 58192 RVA: 0x002C30C2 File Offset: 0x002C12C2
		// (set) Token: 0x0600E351 RID: 58193 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(27, "bwmode")]
		public EnumValue<BlackAndWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003AC6 RID: 15046
		// (get) Token: 0x0600E352 RID: 58194 RVA: 0x002C30D1 File Offset: 0x002C12D1
		// (set) Token: 0x0600E353 RID: 58195 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(27, "bwpure")]
		public EnumValue<BlackAndWhiteModeValues> PureBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003AC7 RID: 15047
		// (get) Token: 0x0600E354 RID: 58196 RVA: 0x002C30E0 File Offset: 0x002C12E0
		// (set) Token: 0x0600E355 RID: 58197 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(27, "bwnormal")]
		public EnumValue<BlackAndWhiteModeValues> NormalBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003AC8 RID: 15048
		// (get) Token: 0x0600E356 RID: 58198 RVA: 0x002C30EF File Offset: 0x002C12EF
		// (set) Token: 0x0600E357 RID: 58199 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(27, "targetscreensize")]
		public EnumValue<ScreenSizeValues> TargetScreenSize
		{
			get
			{
				return (EnumValue<ScreenSizeValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0600E358 RID: 58200 RVA: 0x00293ECF File Offset: 0x002920CF
		public Background()
		{
		}

		// Token: 0x0600E359 RID: 58201 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Background(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E35A RID: 58202 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Background(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E35B RID: 58203 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Background(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E35C RID: 58204 RVA: 0x002C30FE File Offset: 0x002C12FE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			return null;
		}

		// Token: 0x17003AC9 RID: 15049
		// (get) Token: 0x0600E35D RID: 58205 RVA: 0x002C3119 File Offset: 0x002C1319
		internal override string[] ElementTagNames
		{
			get
			{
				return Background.eleTagNames;
			}
		}

		// Token: 0x17003ACA RID: 15050
		// (get) Token: 0x0600E35E RID: 58206 RVA: 0x002C3120 File Offset: 0x002C1320
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Background.eleNamespaceIds;
			}
		}

		// Token: 0x17003ACB RID: 15051
		// (get) Token: 0x0600E35F RID: 58207 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17003ACC RID: 15052
		// (get) Token: 0x0600E360 RID: 58208 RVA: 0x002BD67C File Offset: 0x002BB87C
		// (set) Token: 0x0600E361 RID: 58209 RVA: 0x002BD685 File Offset: 0x002BB885
		public Fill Fill
		{
			get
			{
				return base.GetElement<Fill>(0);
			}
			set
			{
				base.SetElement<Fill>(0, value);
			}
		}

		// Token: 0x0600E362 RID: 58210 RVA: 0x002C3128 File Offset: 0x002C1328
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fill" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "fillcolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "bwmode" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "bwpure" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "bwnormal" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "targetscreensize" == name)
			{
				return new EnumValue<ScreenSizeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E363 RID: 58211 RVA: 0x002C31DF File Offset: 0x002C13DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Background>(deep);
		}

		// Token: 0x04006ECC RID: 28364
		private const string tagName = "background";

		// Token: 0x04006ECD RID: 28365
		private const byte tagNsId = 26;

		// Token: 0x04006ECE RID: 28366
		internal const int ElementTypeIdConst = 12518;

		// Token: 0x04006ECF RID: 28367
		private static string[] attributeTagNames = new string[] { "id", "fill", "fillcolor", "bwmode", "bwpure", "bwnormal", "targetscreensize" };

		// Token: 0x04006ED0 RID: 28368
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 27, 27, 27, 27 };

		// Token: 0x04006ED1 RID: 28369
		private static readonly string[] eleTagNames = new string[] { "fill" };

		// Token: 0x04006ED2 RID: 28370
		private static readonly byte[] eleNamespaceIds = new byte[] { 26 };
	}
}
