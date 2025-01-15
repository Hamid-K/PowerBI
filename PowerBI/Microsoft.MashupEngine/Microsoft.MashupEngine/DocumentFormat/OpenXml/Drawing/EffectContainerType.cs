using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002705 RID: 9989
	[ChildElementInfo(typeof(AlphaModulationFixed))]
	[ChildElementInfo(typeof(Effect))]
	[ChildElementInfo(typeof(AlphaBiLevel))]
	[ChildElementInfo(typeof(AlphaCeiling))]
	[ChildElementInfo(typeof(AlphaFloor))]
	[ChildElementInfo(typeof(AlphaInverse))]
	[ChildElementInfo(typeof(AlphaModulationEffect))]
	[ChildElementInfo(typeof(EffectContainer))]
	[ChildElementInfo(typeof(AlphaOutset))]
	[ChildElementInfo(typeof(AlphaReplace))]
	[ChildElementInfo(typeof(BiLevel))]
	[ChildElementInfo(typeof(Blend))]
	[ChildElementInfo(typeof(Blur))]
	[ChildElementInfo(typeof(ColorChange))]
	[ChildElementInfo(typeof(ColorReplacement))]
	[ChildElementInfo(typeof(Duotone))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(FillOverlay))]
	[ChildElementInfo(typeof(Glow))]
	[ChildElementInfo(typeof(Grayscale))]
	[ChildElementInfo(typeof(Hsl))]
	[ChildElementInfo(typeof(InnerShadow))]
	[ChildElementInfo(typeof(LuminanceEffect))]
	[ChildElementInfo(typeof(OuterShadow))]
	[ChildElementInfo(typeof(PresetShadow))]
	[ChildElementInfo(typeof(Reflection))]
	[ChildElementInfo(typeof(RelativeOffset))]
	[ChildElementInfo(typeof(SoftEdge))]
	[ChildElementInfo(typeof(TintEffect))]
	[ChildElementInfo(typeof(TransformEffect))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class EffectContainerType : OpenXmlCompositeElement
	{
		// Token: 0x17005E9C RID: 24220
		// (get) Token: 0x06013189 RID: 78217 RVA: 0x0030380E File Offset: 0x00301A0E
		internal override string[] AttributeTagNames
		{
			get
			{
				return EffectContainerType.attributeTagNames;
			}
		}

		// Token: 0x17005E9D RID: 24221
		// (get) Token: 0x0601318A RID: 78218 RVA: 0x00303815 File Offset: 0x00301A15
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EffectContainerType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E9E RID: 24222
		// (get) Token: 0x0601318B RID: 78219 RVA: 0x0030381C File Offset: 0x00301A1C
		// (set) Token: 0x0601318C RID: 78220 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<EffectContainerValues> Type
		{
			get
			{
				return (EnumValue<EffectContainerValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E9F RID: 24223
		// (get) Token: 0x0601318D RID: 78221 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601318E RID: 78222 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601318F RID: 78223 RVA: 0x0030382C File Offset: 0x00301A2C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cont" == name)
			{
				return new EffectContainer();
			}
			if (10 == namespaceId && "effect" == name)
			{
				return new Effect();
			}
			if (10 == namespaceId && "alphaBiLevel" == name)
			{
				return new AlphaBiLevel();
			}
			if (10 == namespaceId && "alphaCeiling" == name)
			{
				return new AlphaCeiling();
			}
			if (10 == namespaceId && "alphaFloor" == name)
			{
				return new AlphaFloor();
			}
			if (10 == namespaceId && "alphaInv" == name)
			{
				return new AlphaInverse();
			}
			if (10 == namespaceId && "alphaMod" == name)
			{
				return new AlphaModulationEffect();
			}
			if (10 == namespaceId && "alphaModFix" == name)
			{
				return new AlphaModulationFixed();
			}
			if (10 == namespaceId && "alphaOutset" == name)
			{
				return new AlphaOutset();
			}
			if (10 == namespaceId && "alphaRepl" == name)
			{
				return new AlphaReplace();
			}
			if (10 == namespaceId && "biLevel" == name)
			{
				return new BiLevel();
			}
			if (10 == namespaceId && "blend" == name)
			{
				return new Blend();
			}
			if (10 == namespaceId && "blur" == name)
			{
				return new Blur();
			}
			if (10 == namespaceId && "clrChange" == name)
			{
				return new ColorChange();
			}
			if (10 == namespaceId && "clrRepl" == name)
			{
				return new ColorReplacement();
			}
			if (10 == namespaceId && "duotone" == name)
			{
				return new Duotone();
			}
			if (10 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			if (10 == namespaceId && "fillOverlay" == name)
			{
				return new FillOverlay();
			}
			if (10 == namespaceId && "glow" == name)
			{
				return new Glow();
			}
			if (10 == namespaceId && "grayscl" == name)
			{
				return new Grayscale();
			}
			if (10 == namespaceId && "hsl" == name)
			{
				return new Hsl();
			}
			if (10 == namespaceId && "innerShdw" == name)
			{
				return new InnerShadow();
			}
			if (10 == namespaceId && "lum" == name)
			{
				return new LuminanceEffect();
			}
			if (10 == namespaceId && "outerShdw" == name)
			{
				return new OuterShadow();
			}
			if (10 == namespaceId && "prstShdw" == name)
			{
				return new PresetShadow();
			}
			if (10 == namespaceId && "reflection" == name)
			{
				return new Reflection();
			}
			if (10 == namespaceId && "relOff" == name)
			{
				return new RelativeOffset();
			}
			if (10 == namespaceId && "softEdge" == name)
			{
				return new SoftEdge();
			}
			if (10 == namespaceId && "tint" == name)
			{
				return new TintEffect();
			}
			if (10 == namespaceId && "xfrm" == name)
			{
				return new TransformEffect();
			}
			return null;
		}

		// Token: 0x06013190 RID: 78224 RVA: 0x00303B0A File Offset: 0x00301D0A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<EffectContainerValues>();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013191 RID: 78225 RVA: 0x00293ECF File Offset: 0x002920CF
		protected EffectContainerType()
		{
		}

		// Token: 0x06013192 RID: 78226 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected EffectContainerType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013193 RID: 78227 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected EffectContainerType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013194 RID: 78228 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected EffectContainerType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013195 RID: 78229 RVA: 0x00303B40 File Offset: 0x00301D40
		// Note: this type is marked as 'beforefieldinit'.
		static EffectContainerType()
		{
			byte[] array = new byte[2];
			EffectContainerType.attributeNamespaceIds = array;
		}

		// Token: 0x040084A5 RID: 33957
		private static string[] attributeTagNames = new string[] { "type", "name" };

		// Token: 0x040084A6 RID: 33958
		private static byte[] attributeNamespaceIds;
	}
}
