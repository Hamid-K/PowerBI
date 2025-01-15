using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263F RID: 9791
	[ChildElementInfo(typeof(XPosition))]
	[ChildElementInfo(typeof(YPosition))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MarkerType : OpenXmlCompositeElement
	{
		// Token: 0x060128FD RID: 76029 RVA: 0x002FC9A0 File Offset: 0x002FABA0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "x" == name)
			{
				return new XPosition();
			}
			if (12 == namespaceId && "y" == name)
			{
				return new YPosition();
			}
			return null;
		}

		// Token: 0x17005AD2 RID: 23250
		// (get) Token: 0x060128FE RID: 76030 RVA: 0x002FC9D3 File Offset: 0x002FABD3
		internal override string[] ElementTagNames
		{
			get
			{
				return MarkerType.eleTagNames;
			}
		}

		// Token: 0x17005AD3 RID: 23251
		// (get) Token: 0x060128FF RID: 76031 RVA: 0x002FC9DA File Offset: 0x002FABDA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MarkerType.eleNamespaceIds;
			}
		}

		// Token: 0x17005AD4 RID: 23252
		// (get) Token: 0x06012900 RID: 76032 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AD5 RID: 23253
		// (get) Token: 0x06012901 RID: 76033 RVA: 0x002FC9E1 File Offset: 0x002FABE1
		// (set) Token: 0x06012902 RID: 76034 RVA: 0x002FC9EA File Offset: 0x002FABEA
		public XPosition XPosition
		{
			get
			{
				return base.GetElement<XPosition>(0);
			}
			set
			{
				base.SetElement<XPosition>(0, value);
			}
		}

		// Token: 0x17005AD6 RID: 23254
		// (get) Token: 0x06012903 RID: 76035 RVA: 0x002FC9F4 File Offset: 0x002FABF4
		// (set) Token: 0x06012904 RID: 76036 RVA: 0x002FC9FD File Offset: 0x002FABFD
		public YPosition YPosition
		{
			get
			{
				return base.GetElement<YPosition>(1);
			}
			set
			{
				base.SetElement<YPosition>(1, value);
			}
		}

		// Token: 0x06012905 RID: 76037 RVA: 0x00293ECF File Offset: 0x002920CF
		protected MarkerType()
		{
		}

		// Token: 0x06012906 RID: 76038 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected MarkerType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012907 RID: 76039 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected MarkerType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012908 RID: 76040 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected MarkerType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040080AD RID: 32941
		private static readonly string[] eleTagNames = new string[] { "x", "y" };

		// Token: 0x040080AE RID: 32942
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
