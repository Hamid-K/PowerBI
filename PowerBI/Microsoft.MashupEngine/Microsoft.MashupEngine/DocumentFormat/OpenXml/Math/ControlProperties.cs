using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298F RID: 10639
	[ChildElementInfo(typeof(InsertedMathControl))]
	[ChildElementInfo(typeof(MoveToMathControl))]
	[ChildElementInfo(typeof(RunProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DeletedMathControl))]
	[ChildElementInfo(typeof(MoveFromMathControl))]
	internal class ControlProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006CC3 RID: 27843
		// (get) Token: 0x06015219 RID: 86553 RVA: 0x0031B9C0 File Offset: 0x00319BC0
		public override string LocalName
		{
			get
			{
				return "ctrlPr";
			}
		}

		// Token: 0x17006CC4 RID: 27844
		// (get) Token: 0x0601521A RID: 86554 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CC5 RID: 27845
		// (get) Token: 0x0601521B RID: 86555 RVA: 0x0031B9C7 File Offset: 0x00319BC7
		internal override int ElementTypeId
		{
			get
			{
				return 10871;
			}
		}

		// Token: 0x0601521C RID: 86556 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601521D RID: 86557 RVA: 0x00293ECF File Offset: 0x002920CF
		public ControlProperties()
		{
		}

		// Token: 0x0601521E RID: 86558 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ControlProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601521F RID: 86559 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ControlProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015220 RID: 86560 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ControlProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015221 RID: 86561 RVA: 0x0031B9D0 File Offset: 0x00319BD0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (23 == namespaceId && "ins" == name)
			{
				return new InsertedMathControl();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new DeletedMathControl();
			}
			if (23 == namespaceId && "moveFrom" == name)
			{
				return new MoveFromMathControl();
			}
			if (23 == namespaceId && "moveTo" == name)
			{
				return new MoveToMathControl();
			}
			return null;
		}

		// Token: 0x06015222 RID: 86562 RVA: 0x0031BA56 File Offset: 0x00319C56
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlProperties>(deep);
		}

		// Token: 0x040091BE RID: 37310
		private const string tagName = "ctrlPr";

		// Token: 0x040091BF RID: 37311
		private const byte tagNsId = 21;

		// Token: 0x040091C0 RID: 37312
		internal const int ElementTypeIdConst = 10871;
	}
}
