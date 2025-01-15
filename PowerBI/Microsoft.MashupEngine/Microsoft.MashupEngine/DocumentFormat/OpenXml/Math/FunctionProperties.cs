using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AC RID: 10668
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class FunctionProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D5A RID: 27994
		// (get) Token: 0x06015377 RID: 86903 RVA: 0x0031CF99 File Offset: 0x0031B199
		public override string LocalName
		{
			get
			{
				return "funcPr";
			}
		}

		// Token: 0x17006D5B RID: 27995
		// (get) Token: 0x06015378 RID: 86904 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D5C RID: 27996
		// (get) Token: 0x06015379 RID: 86905 RVA: 0x0031CFA0 File Offset: 0x0031B1A0
		internal override int ElementTypeId
		{
			get
			{
				return 10905;
			}
		}

		// Token: 0x0601537A RID: 86906 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601537B RID: 86907 RVA: 0x00293ECF File Offset: 0x002920CF
		public FunctionProperties()
		{
		}

		// Token: 0x0601537C RID: 86908 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FunctionProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601537D RID: 86909 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FunctionProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601537E RID: 86910 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FunctionProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601537F RID: 86911 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D5D RID: 27997
		// (get) Token: 0x06015380 RID: 86912 RVA: 0x0031CFC2 File Offset: 0x0031B1C2
		internal override string[] ElementTagNames
		{
			get
			{
				return FunctionProperties.eleTagNames;
			}
		}

		// Token: 0x17006D5E RID: 27998
		// (get) Token: 0x06015381 RID: 86913 RVA: 0x0031CFC9 File Offset: 0x0031B1C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FunctionProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D5F RID: 27999
		// (get) Token: 0x06015382 RID: 86914 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D60 RID: 28000
		// (get) Token: 0x06015383 RID: 86915 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x06015384 RID: 86916 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(0);
			}
			set
			{
				base.SetElement<ControlProperties>(0, value);
			}
		}

		// Token: 0x06015385 RID: 86917 RVA: 0x0031CFE3 File Offset: 0x0031B1E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FunctionProperties>(deep);
		}

		// Token: 0x04009225 RID: 37413
		private const string tagName = "funcPr";

		// Token: 0x04009226 RID: 37414
		private const byte tagNsId = 21;

		// Token: 0x04009227 RID: 37415
		internal const int ElementTypeIdConst = 10905;

		// Token: 0x04009228 RID: 37416
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x04009229 RID: 37417
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
