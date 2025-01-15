using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A37 RID: 10807
	[ChildElementInfo(typeof(StringVariantValue))]
	[ChildElementInfo(typeof(FloatVariantValue))]
	[ChildElementInfo(typeof(BooleanVariantValue))]
	[ChildElementInfo(typeof(IntegerVariantValue))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColorValue))]
	internal abstract class TimeListAnimationVariantType : OpenXmlCompositeElement
	{
		// Token: 0x06015B92 RID: 88978 RVA: 0x003226EC File Offset: 0x003208EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "boolVal" == name)
			{
				return new BooleanVariantValue();
			}
			if (24 == namespaceId && "intVal" == name)
			{
				return new IntegerVariantValue();
			}
			if (24 == namespaceId && "fltVal" == name)
			{
				return new FloatVariantValue();
			}
			if (24 == namespaceId && "strVal" == name)
			{
				return new StringVariantValue();
			}
			if (24 == namespaceId && "clrVal" == name)
			{
				return new ColorValue();
			}
			return null;
		}

		// Token: 0x17007107 RID: 28935
		// (get) Token: 0x06015B93 RID: 88979 RVA: 0x00322772 File Offset: 0x00320972
		internal override string[] ElementTagNames
		{
			get
			{
				return TimeListAnimationVariantType.eleTagNames;
			}
		}

		// Token: 0x17007108 RID: 28936
		// (get) Token: 0x06015B94 RID: 88980 RVA: 0x00322779 File Offset: 0x00320979
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TimeListAnimationVariantType.eleNamespaceIds;
			}
		}

		// Token: 0x17007109 RID: 28937
		// (get) Token: 0x06015B95 RID: 88981 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700710A RID: 28938
		// (get) Token: 0x06015B96 RID: 88982 RVA: 0x00322780 File Offset: 0x00320980
		// (set) Token: 0x06015B97 RID: 88983 RVA: 0x00322789 File Offset: 0x00320989
		public BooleanVariantValue BooleanVariantValue
		{
			get
			{
				return base.GetElement<BooleanVariantValue>(0);
			}
			set
			{
				base.SetElement<BooleanVariantValue>(0, value);
			}
		}

		// Token: 0x1700710B RID: 28939
		// (get) Token: 0x06015B98 RID: 88984 RVA: 0x00322793 File Offset: 0x00320993
		// (set) Token: 0x06015B99 RID: 88985 RVA: 0x0032279C File Offset: 0x0032099C
		public IntegerVariantValue IntegerVariantValue
		{
			get
			{
				return base.GetElement<IntegerVariantValue>(1);
			}
			set
			{
				base.SetElement<IntegerVariantValue>(1, value);
			}
		}

		// Token: 0x1700710C RID: 28940
		// (get) Token: 0x06015B9A RID: 88986 RVA: 0x003227A6 File Offset: 0x003209A6
		// (set) Token: 0x06015B9B RID: 88987 RVA: 0x003227AF File Offset: 0x003209AF
		public FloatVariantValue FloatVariantValue
		{
			get
			{
				return base.GetElement<FloatVariantValue>(2);
			}
			set
			{
				base.SetElement<FloatVariantValue>(2, value);
			}
		}

		// Token: 0x1700710D RID: 28941
		// (get) Token: 0x06015B9C RID: 88988 RVA: 0x003227B9 File Offset: 0x003209B9
		// (set) Token: 0x06015B9D RID: 88989 RVA: 0x003227C2 File Offset: 0x003209C2
		public StringVariantValue StringVariantValue
		{
			get
			{
				return base.GetElement<StringVariantValue>(3);
			}
			set
			{
				base.SetElement<StringVariantValue>(3, value);
			}
		}

		// Token: 0x1700710E RID: 28942
		// (get) Token: 0x06015B9E RID: 88990 RVA: 0x003227CC File Offset: 0x003209CC
		// (set) Token: 0x06015B9F RID: 88991 RVA: 0x003227D5 File Offset: 0x003209D5
		public ColorValue ColorValue
		{
			get
			{
				return base.GetElement<ColorValue>(4);
			}
			set
			{
				base.SetElement<ColorValue>(4, value);
			}
		}

		// Token: 0x06015BA0 RID: 88992 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TimeListAnimationVariantType()
		{
		}

		// Token: 0x06015BA1 RID: 88993 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TimeListAnimationVariantType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BA2 RID: 88994 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TimeListAnimationVariantType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BA3 RID: 88995 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TimeListAnimationVariantType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400948C RID: 38028
		private static readonly string[] eleTagNames = new string[] { "boolVal", "intVal", "fltVal", "strVal", "clrVal" };

		// Token: 0x0400948D RID: 38029
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
	}
}
