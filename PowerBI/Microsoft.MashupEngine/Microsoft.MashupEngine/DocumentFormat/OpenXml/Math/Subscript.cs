using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002962 RID: 10594
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(SubscriptProperties))]
	[ChildElementInfo(typeof(SubArgument))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Subscript : OpenXmlCompositeElement
	{
		// Token: 0x17006C0B RID: 27659
		// (get) Token: 0x06015095 RID: 86165 RVA: 0x0031A2DC File Offset: 0x003184DC
		public override string LocalName
		{
			get
			{
				return "sSub";
			}
		}

		// Token: 0x17006C0C RID: 27660
		// (get) Token: 0x06015096 RID: 86166 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C0D RID: 27661
		// (get) Token: 0x06015097 RID: 86167 RVA: 0x0031A2E3 File Offset: 0x003184E3
		internal override int ElementTypeId
		{
			get
			{
				return 10858;
			}
		}

		// Token: 0x06015098 RID: 86168 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015099 RID: 86169 RVA: 0x00293ECF File Offset: 0x002920CF
		public Subscript()
		{
		}

		// Token: 0x0601509A RID: 86170 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Subscript(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601509B RID: 86171 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Subscript(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601509C RID: 86172 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Subscript(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601509D RID: 86173 RVA: 0x0031A2EC File Offset: 0x003184EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "sSubPr" == name)
			{
				return new SubscriptProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			if (21 == namespaceId && "sub" == name)
			{
				return new SubArgument();
			}
			return null;
		}

		// Token: 0x17006C0E RID: 27662
		// (get) Token: 0x0601509E RID: 86174 RVA: 0x0031A342 File Offset: 0x00318542
		internal override string[] ElementTagNames
		{
			get
			{
				return Subscript.eleTagNames;
			}
		}

		// Token: 0x17006C0F RID: 27663
		// (get) Token: 0x0601509F RID: 86175 RVA: 0x0031A349 File Offset: 0x00318549
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Subscript.eleNamespaceIds;
			}
		}

		// Token: 0x17006C10 RID: 27664
		// (get) Token: 0x060150A0 RID: 86176 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C11 RID: 27665
		// (get) Token: 0x060150A1 RID: 86177 RVA: 0x0031A350 File Offset: 0x00318550
		// (set) Token: 0x060150A2 RID: 86178 RVA: 0x0031A359 File Offset: 0x00318559
		public SubscriptProperties SubscriptProperties
		{
			get
			{
				return base.GetElement<SubscriptProperties>(0);
			}
			set
			{
				base.SetElement<SubscriptProperties>(0, value);
			}
		}

		// Token: 0x17006C12 RID: 27666
		// (get) Token: 0x060150A3 RID: 86179 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x060150A4 RID: 86180 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x17006C13 RID: 27667
		// (get) Token: 0x060150A5 RID: 86181 RVA: 0x0031A363 File Offset: 0x00318563
		// (set) Token: 0x060150A6 RID: 86182 RVA: 0x0031A36C File Offset: 0x0031856C
		public SubArgument SubArgument
		{
			get
			{
				return base.GetElement<SubArgument>(2);
			}
			set
			{
				base.SetElement<SubArgument>(2, value);
			}
		}

		// Token: 0x060150A7 RID: 86183 RVA: 0x0031A376 File Offset: 0x00318576
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Subscript>(deep);
		}

		// Token: 0x04009129 RID: 37161
		private const string tagName = "sSub";

		// Token: 0x0400912A RID: 37162
		private const byte tagNsId = 21;

		// Token: 0x0400912B RID: 37163
		internal const int ElementTypeIdConst = 10858;

		// Token: 0x0400912C RID: 37164
		private static readonly string[] eleTagNames = new string[] { "sSubPr", "e", "sub" };

		// Token: 0x0400912D RID: 37165
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
