using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002755 RID: 10069
	[ChildElementInfo(typeof(PresetDash))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(CustomDash))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(Miter))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(LineJoinBevel))]
	[ChildElementInfo(typeof(Round))]
	[ChildElementInfo(typeof(HeadEnd))]
	[ChildElementInfo(typeof(TailEnd))]
	internal abstract class LinePropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x170060B1 RID: 24753
		// (get) Token: 0x06013615 RID: 79381 RVA: 0x0030675B File Offset: 0x0030495B
		internal override string[] AttributeTagNames
		{
			get
			{
				return LinePropertiesType.attributeTagNames;
			}
		}

		// Token: 0x170060B2 RID: 24754
		// (get) Token: 0x06013616 RID: 79382 RVA: 0x00306762 File Offset: 0x00304962
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LinePropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x170060B3 RID: 24755
		// (get) Token: 0x06013617 RID: 79383 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013618 RID: 79384 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170060B4 RID: 24756
		// (get) Token: 0x06013619 RID: 79385 RVA: 0x002E0884 File Offset: 0x002DEA84
		// (set) Token: 0x0601361A RID: 79386 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170060B5 RID: 24757
		// (get) Token: 0x0601361B RID: 79387 RVA: 0x002E0893 File Offset: 0x002DEA93
		// (set) Token: 0x0601361C RID: 79388 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170060B6 RID: 24758
		// (get) Token: 0x0601361D RID: 79389 RVA: 0x002E08A2 File Offset: 0x002DEAA2
		// (set) Token: 0x0601361E RID: 79390 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0601361F RID: 79391 RVA: 0x0030676C File Offset: 0x0030496C
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

		// Token: 0x06013620 RID: 79392 RVA: 0x0030689C File Offset: 0x00304A9C
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

		// Token: 0x06013621 RID: 79393 RVA: 0x00293ECF File Offset: 0x002920CF
		protected LinePropertiesType()
		{
		}

		// Token: 0x06013622 RID: 79394 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected LinePropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013623 RID: 79395 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected LinePropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013624 RID: 79396 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected LinePropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013625 RID: 79397 RVA: 0x0030690C File Offset: 0x00304B0C
		// Note: this type is marked as 'beforefieldinit'.
		static LinePropertiesType()
		{
			byte[] array = new byte[4];
			LinePropertiesType.attributeNamespaceIds = array;
		}

		// Token: 0x040085F6 RID: 34294
		private static string[] attributeTagNames = new string[] { "w", "cap", "cmpd", "algn" };

		// Token: 0x040085F7 RID: 34295
		private static byte[] attributeNamespaceIds;
	}
}
