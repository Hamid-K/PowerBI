using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002957 RID: 10583
	[ChildElementInfo(typeof(EquationArrayProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Base))]
	internal class EquationArray : OpenXmlCompositeElement
	{
		// Token: 0x17006BAC RID: 27564
		// (get) Token: 0x06014FC1 RID: 85953 RVA: 0x00319961 File Offset: 0x00317B61
		public override string LocalName
		{
			get
			{
				return "eqArr";
			}
		}

		// Token: 0x17006BAD RID: 27565
		// (get) Token: 0x06014FC2 RID: 85954 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BAE RID: 27566
		// (get) Token: 0x06014FC3 RID: 85955 RVA: 0x00319968 File Offset: 0x00317B68
		internal override int ElementTypeId
		{
			get
			{
				return 10847;
			}
		}

		// Token: 0x06014FC4 RID: 85956 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FC5 RID: 85957 RVA: 0x00293ECF File Offset: 0x002920CF
		public EquationArray()
		{
		}

		// Token: 0x06014FC6 RID: 85958 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EquationArray(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FC7 RID: 85959 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EquationArray(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FC8 RID: 85960 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EquationArray(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014FC9 RID: 85961 RVA: 0x0031996F File Offset: 0x00317B6F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "eqArrPr" == name)
			{
				return new EquationArrayProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BAF RID: 27567
		// (get) Token: 0x06014FCA RID: 85962 RVA: 0x003199A2 File Offset: 0x00317BA2
		internal override string[] ElementTagNames
		{
			get
			{
				return EquationArray.eleTagNames;
			}
		}

		// Token: 0x17006BB0 RID: 27568
		// (get) Token: 0x06014FCB RID: 85963 RVA: 0x003199A9 File Offset: 0x00317BA9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EquationArray.eleNamespaceIds;
			}
		}

		// Token: 0x17006BB1 RID: 27569
		// (get) Token: 0x06014FCC RID: 85964 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BB2 RID: 27570
		// (get) Token: 0x06014FCD RID: 85965 RVA: 0x003199B0 File Offset: 0x00317BB0
		// (set) Token: 0x06014FCE RID: 85966 RVA: 0x003199B9 File Offset: 0x00317BB9
		public EquationArrayProperties EquationArrayProperties
		{
			get
			{
				return base.GetElement<EquationArrayProperties>(0);
			}
			set
			{
				base.SetElement<EquationArrayProperties>(0, value);
			}
		}

		// Token: 0x06014FCF RID: 85967 RVA: 0x003199C3 File Offset: 0x00317BC3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EquationArray>(deep);
		}

		// Token: 0x040090F2 RID: 37106
		private const string tagName = "eqArr";

		// Token: 0x040090F3 RID: 37107
		private const byte tagNsId = 21;

		// Token: 0x040090F4 RID: 37108
		internal const int ElementTypeIdConst = 10847;

		// Token: 0x040090F5 RID: 37109
		private static readonly string[] eleTagNames = new string[] { "eqArrPr", "e" };

		// Token: 0x040090F6 RID: 37110
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
