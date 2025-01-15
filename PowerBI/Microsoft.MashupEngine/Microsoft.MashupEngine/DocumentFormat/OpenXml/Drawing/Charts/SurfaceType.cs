using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C6 RID: 9670
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(Thickness))]
	internal abstract class SurfaceType : OpenXmlCompositeElement
	{
		// Token: 0x0601220B RID: 74251 RVA: 0x002F5DC8 File Offset: 0x002F3FC8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "thickness" == name)
			{
				return new Thickness();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (11 == namespaceId && "pictureOptions" == name)
			{
				return new PictureOptions();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170057A7 RID: 22439
		// (get) Token: 0x0601220C RID: 74252 RVA: 0x002F5E36 File Offset: 0x002F4036
		internal override string[] ElementTagNames
		{
			get
			{
				return SurfaceType.eleTagNames;
			}
		}

		// Token: 0x170057A8 RID: 22440
		// (get) Token: 0x0601220D RID: 74253 RVA: 0x002F5E3D File Offset: 0x002F403D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SurfaceType.eleNamespaceIds;
			}
		}

		// Token: 0x170057A9 RID: 22441
		// (get) Token: 0x0601220E RID: 74254 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057AA RID: 22442
		// (get) Token: 0x0601220F RID: 74255 RVA: 0x002F5E44 File Offset: 0x002F4044
		// (set) Token: 0x06012210 RID: 74256 RVA: 0x002F5E4D File Offset: 0x002F404D
		public Thickness Thickness
		{
			get
			{
				return base.GetElement<Thickness>(0);
			}
			set
			{
				base.SetElement<Thickness>(0, value);
			}
		}

		// Token: 0x170057AB RID: 22443
		// (get) Token: 0x06012211 RID: 74257 RVA: 0x002F59C4 File Offset: 0x002F3BC4
		// (set) Token: 0x06012212 RID: 74258 RVA: 0x002F59CD File Offset: 0x002F3BCD
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x170057AC RID: 22444
		// (get) Token: 0x06012213 RID: 74259 RVA: 0x002F5E57 File Offset: 0x002F4057
		// (set) Token: 0x06012214 RID: 74260 RVA: 0x002F5E60 File Offset: 0x002F4060
		public PictureOptions PictureOptions
		{
			get
			{
				return base.GetElement<PictureOptions>(2);
			}
			set
			{
				base.SetElement<PictureOptions>(2, value);
			}
		}

		// Token: 0x170057AD RID: 22445
		// (get) Token: 0x06012215 RID: 74261 RVA: 0x002F4721 File Offset: 0x002F2921
		// (set) Token: 0x06012216 RID: 74262 RVA: 0x002F472A File Offset: 0x002F292A
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06012217 RID: 74263 RVA: 0x00293ECF File Offset: 0x002920CF
		protected SurfaceType()
		{
		}

		// Token: 0x06012218 RID: 74264 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected SurfaceType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012219 RID: 74265 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected SurfaceType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601221A RID: 74266 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected SurfaceType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007E59 RID: 32345
		private static readonly string[] eleTagNames = new string[] { "thickness", "spPr", "pictureOptions", "extLst" };

		// Token: 0x04007E5A RID: 32346
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
