using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295D RID: 10589
	[ChildElementInfo(typeof(MatrixProperties))]
	[ChildElementInfo(typeof(MatrixRow))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Matrix : OpenXmlCompositeElement
	{
		// Token: 0x17006BDF RID: 27615
		// (get) Token: 0x06015033 RID: 86067 RVA: 0x002E0FCF File Offset: 0x002DF1CF
		public override string LocalName
		{
			get
			{
				return "m";
			}
		}

		// Token: 0x17006BE0 RID: 27616
		// (get) Token: 0x06015034 RID: 86068 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BE1 RID: 27617
		// (get) Token: 0x06015035 RID: 86069 RVA: 0x00319E74 File Offset: 0x00318074
		internal override int ElementTypeId
		{
			get
			{
				return 10853;
			}
		}

		// Token: 0x06015036 RID: 86070 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015037 RID: 86071 RVA: 0x00293ECF File Offset: 0x002920CF
		public Matrix()
		{
		}

		// Token: 0x06015038 RID: 86072 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Matrix(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015039 RID: 86073 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Matrix(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601503A RID: 86074 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Matrix(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601503B RID: 86075 RVA: 0x00319E7B File Offset: 0x0031807B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "mPr" == name)
			{
				return new MatrixProperties();
			}
			if (21 == namespaceId && "mr" == name)
			{
				return new MatrixRow();
			}
			return null;
		}

		// Token: 0x17006BE2 RID: 27618
		// (get) Token: 0x0601503C RID: 86076 RVA: 0x00319EAE File Offset: 0x003180AE
		internal override string[] ElementTagNames
		{
			get
			{
				return Matrix.eleTagNames;
			}
		}

		// Token: 0x17006BE3 RID: 27619
		// (get) Token: 0x0601503D RID: 86077 RVA: 0x00319EB5 File Offset: 0x003180B5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Matrix.eleNamespaceIds;
			}
		}

		// Token: 0x17006BE4 RID: 27620
		// (get) Token: 0x0601503E RID: 86078 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BE5 RID: 27621
		// (get) Token: 0x0601503F RID: 86079 RVA: 0x00319EBC File Offset: 0x003180BC
		// (set) Token: 0x06015040 RID: 86080 RVA: 0x00319EC5 File Offset: 0x003180C5
		public MatrixProperties MatrixProperties
		{
			get
			{
				return base.GetElement<MatrixProperties>(0);
			}
			set
			{
				base.SetElement<MatrixProperties>(0, value);
			}
		}

		// Token: 0x06015041 RID: 86081 RVA: 0x00319ECF File Offset: 0x003180CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Matrix>(deep);
		}

		// Token: 0x04009110 RID: 37136
		private const string tagName = "m";

		// Token: 0x04009111 RID: 37137
		private const byte tagNsId = 21;

		// Token: 0x04009112 RID: 37138
		internal const int ElementTypeIdConst = 10853;

		// Token: 0x04009113 RID: 37139
		private static readonly string[] eleTagNames = new string[] { "mPr", "mr" };

		// Token: 0x04009114 RID: 37140
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
