using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295E RID: 10590
	[ChildElementInfo(typeof(SuperArgument))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NaryProperties))]
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(SubArgument))]
	internal class Nary : OpenXmlCompositeElement
	{
		// Token: 0x17006BE6 RID: 27622
		// (get) Token: 0x06015043 RID: 86083 RVA: 0x00319F19 File Offset: 0x00318119
		public override string LocalName
		{
			get
			{
				return "nary";
			}
		}

		// Token: 0x17006BE7 RID: 27623
		// (get) Token: 0x06015044 RID: 86084 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BE8 RID: 27624
		// (get) Token: 0x06015045 RID: 86085 RVA: 0x00319F20 File Offset: 0x00318120
		internal override int ElementTypeId
		{
			get
			{
				return 10854;
			}
		}

		// Token: 0x06015046 RID: 86086 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015047 RID: 86087 RVA: 0x00293ECF File Offset: 0x002920CF
		public Nary()
		{
		}

		// Token: 0x06015048 RID: 86088 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Nary(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015049 RID: 86089 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Nary(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601504A RID: 86090 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Nary(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601504B RID: 86091 RVA: 0x00319F28 File Offset: 0x00318128
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "naryPr" == name)
			{
				return new NaryProperties();
			}
			if (21 == namespaceId && "sub" == name)
			{
				return new SubArgument();
			}
			if (21 == namespaceId && "sup" == name)
			{
				return new SuperArgument();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BE9 RID: 27625
		// (get) Token: 0x0601504C RID: 86092 RVA: 0x00319F96 File Offset: 0x00318196
		internal override string[] ElementTagNames
		{
			get
			{
				return Nary.eleTagNames;
			}
		}

		// Token: 0x17006BEA RID: 27626
		// (get) Token: 0x0601504D RID: 86093 RVA: 0x00319F9D File Offset: 0x0031819D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Nary.eleNamespaceIds;
			}
		}

		// Token: 0x17006BEB RID: 27627
		// (get) Token: 0x0601504E RID: 86094 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BEC RID: 27628
		// (get) Token: 0x0601504F RID: 86095 RVA: 0x00319FA4 File Offset: 0x003181A4
		// (set) Token: 0x06015050 RID: 86096 RVA: 0x00319FAD File Offset: 0x003181AD
		public NaryProperties NaryProperties
		{
			get
			{
				return base.GetElement<NaryProperties>(0);
			}
			set
			{
				base.SetElement<NaryProperties>(0, value);
			}
		}

		// Token: 0x17006BED RID: 27629
		// (get) Token: 0x06015051 RID: 86097 RVA: 0x00319FB7 File Offset: 0x003181B7
		// (set) Token: 0x06015052 RID: 86098 RVA: 0x00319FC0 File Offset: 0x003181C0
		public SubArgument SubArgument
		{
			get
			{
				return base.GetElement<SubArgument>(1);
			}
			set
			{
				base.SetElement<SubArgument>(1, value);
			}
		}

		// Token: 0x17006BEE RID: 27630
		// (get) Token: 0x06015053 RID: 86099 RVA: 0x00319FCA File Offset: 0x003181CA
		// (set) Token: 0x06015054 RID: 86100 RVA: 0x00319FD3 File Offset: 0x003181D3
		public SuperArgument SuperArgument
		{
			get
			{
				return base.GetElement<SuperArgument>(2);
			}
			set
			{
				base.SetElement<SuperArgument>(2, value);
			}
		}

		// Token: 0x17006BEF RID: 27631
		// (get) Token: 0x06015055 RID: 86101 RVA: 0x00319FDD File Offset: 0x003181DD
		// (set) Token: 0x06015056 RID: 86102 RVA: 0x00319FE6 File Offset: 0x003181E6
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(3);
			}
			set
			{
				base.SetElement<Base>(3, value);
			}
		}

		// Token: 0x06015057 RID: 86103 RVA: 0x00319FF0 File Offset: 0x003181F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Nary>(deep);
		}

		// Token: 0x04009115 RID: 37141
		private const string tagName = "nary";

		// Token: 0x04009116 RID: 37142
		private const byte tagNsId = 21;

		// Token: 0x04009117 RID: 37143
		internal const int ElementTypeIdConst = 10854;

		// Token: 0x04009118 RID: 37144
		private static readonly string[] eleTagNames = new string[] { "naryPr", "sub", "sup", "e" };

		// Token: 0x04009119 RID: 37145
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21 };
	}
}
