using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002712 RID: 10002
	[ChildElementInfo(typeof(EffectContainer))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Blend : OpenXmlCompositeElement
	{
		// Token: 0x17005EE3 RID: 24291
		// (get) Token: 0x06013226 RID: 78374 RVA: 0x0030403B File Offset: 0x0030223B
		public override string LocalName
		{
			get
			{
				return "blend";
			}
		}

		// Token: 0x17005EE4 RID: 24292
		// (get) Token: 0x06013227 RID: 78375 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EE5 RID: 24293
		// (get) Token: 0x06013228 RID: 78376 RVA: 0x00304042 File Offset: 0x00302242
		internal override int ElementTypeId
		{
			get
			{
				return 10064;
			}
		}

		// Token: 0x06013229 RID: 78377 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EE6 RID: 24294
		// (get) Token: 0x0601322A RID: 78378 RVA: 0x00304049 File Offset: 0x00302249
		internal override string[] AttributeTagNames
		{
			get
			{
				return Blend.attributeTagNames;
			}
		}

		// Token: 0x17005EE7 RID: 24295
		// (get) Token: 0x0601322B RID: 78379 RVA: 0x00304050 File Offset: 0x00302250
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Blend.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EE8 RID: 24296
		// (get) Token: 0x0601322C RID: 78380 RVA: 0x00304057 File Offset: 0x00302257
		// (set) Token: 0x0601322D RID: 78381 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blend")]
		public EnumValue<BlendModeValues> BlendMode
		{
			get
			{
				return (EnumValue<BlendModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601322E RID: 78382 RVA: 0x00293ECF File Offset: 0x002920CF
		public Blend()
		{
		}

		// Token: 0x0601322F RID: 78383 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Blend(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013230 RID: 78384 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Blend(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013231 RID: 78385 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Blend(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013232 RID: 78386 RVA: 0x00303E0F File Offset: 0x0030200F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cont" == name)
			{
				return new EffectContainer();
			}
			return null;
		}

		// Token: 0x17005EE9 RID: 24297
		// (get) Token: 0x06013233 RID: 78387 RVA: 0x00304066 File Offset: 0x00302266
		internal override string[] ElementTagNames
		{
			get
			{
				return Blend.eleTagNames;
			}
		}

		// Token: 0x17005EEA RID: 24298
		// (get) Token: 0x06013234 RID: 78388 RVA: 0x0030406D File Offset: 0x0030226D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Blend.eleNamespaceIds;
			}
		}

		// Token: 0x17005EEB RID: 24299
		// (get) Token: 0x06013235 RID: 78389 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005EEC RID: 24300
		// (get) Token: 0x06013236 RID: 78390 RVA: 0x00303E38 File Offset: 0x00302038
		// (set) Token: 0x06013237 RID: 78391 RVA: 0x00303E41 File Offset: 0x00302041
		public EffectContainer EffectContainer
		{
			get
			{
				return base.GetElement<EffectContainer>(0);
			}
			set
			{
				base.SetElement<EffectContainer>(0, value);
			}
		}

		// Token: 0x06013238 RID: 78392 RVA: 0x00304074 File Offset: 0x00302274
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blend" == name)
			{
				return new EnumValue<BlendModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013239 RID: 78393 RVA: 0x00304094 File Offset: 0x00302294
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Blend>(deep);
		}

		// Token: 0x0601323A RID: 78394 RVA: 0x003040A0 File Offset: 0x003022A0
		// Note: this type is marked as 'beforefieldinit'.
		static Blend()
		{
			byte[] array = new byte[1];
			Blend.attributeNamespaceIds = array;
			Blend.eleTagNames = new string[] { "cont" };
			Blend.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040084DB RID: 34011
		private const string tagName = "blend";

		// Token: 0x040084DC RID: 34012
		private const byte tagNsId = 10;

		// Token: 0x040084DD RID: 34013
		internal const int ElementTypeIdConst = 10064;

		// Token: 0x040084DE RID: 34014
		private static string[] attributeTagNames = new string[] { "blend" };

		// Token: 0x040084DF RID: 34015
		private static byte[] attributeNamespaceIds;

		// Token: 0x040084E0 RID: 34016
		private static readonly string[] eleTagNames;

		// Token: 0x040084E1 RID: 34017
		private static readonly byte[] eleNamespaceIds;
	}
}
