using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002873 RID: 10355
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualPictureDrawingProperties))]
	internal class NonVisualPictureProperties : OpenXmlCompositeElement
	{
		// Token: 0x170066A6 RID: 26278
		// (get) Token: 0x060143FF RID: 82943 RVA: 0x002FC4B3 File Offset: 0x002FA6B3
		public override string LocalName
		{
			get
			{
				return "nvPicPr";
			}
		}

		// Token: 0x170066A7 RID: 26279
		// (get) Token: 0x06014400 RID: 82944 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x170066A8 RID: 26280
		// (get) Token: 0x06014401 RID: 82945 RVA: 0x00310E43 File Offset: 0x0030F043
		internal override int ElementTypeId
		{
			get
			{
				return 10717;
			}
		}

		// Token: 0x06014402 RID: 82946 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014403 RID: 82947 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureProperties()
		{
		}

		// Token: 0x06014404 RID: 82948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014405 RID: 82949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014406 RID: 82950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014407 RID: 82951 RVA: 0x00310E4A File Offset: 0x0030F04A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (17 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (17 == namespaceId && "cNvPicPr" == name)
			{
				return new NonVisualPictureDrawingProperties();
			}
			return null;
		}

		// Token: 0x170066A9 RID: 26281
		// (get) Token: 0x06014408 RID: 82952 RVA: 0x00310E7D File Offset: 0x0030F07D
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureProperties.eleTagNames;
			}
		}

		// Token: 0x170066AA RID: 26282
		// (get) Token: 0x06014409 RID: 82953 RVA: 0x00310E84 File Offset: 0x0030F084
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170066AB RID: 26283
		// (get) Token: 0x0601440A RID: 82954 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066AC RID: 26284
		// (get) Token: 0x0601440B RID: 82955 RVA: 0x00310E8B File Offset: 0x0030F08B
		// (set) Token: 0x0601440C RID: 82956 RVA: 0x00310E94 File Offset: 0x0030F094
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x170066AD RID: 26285
		// (get) Token: 0x0601440D RID: 82957 RVA: 0x00310E9E File Offset: 0x0030F09E
		// (set) Token: 0x0601440E RID: 82958 RVA: 0x00310EA7 File Offset: 0x0030F0A7
		public NonVisualPictureDrawingProperties NonVisualPictureDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualPictureDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualPictureDrawingProperties>(1, value);
			}
		}

		// Token: 0x0601440F RID: 82959 RVA: 0x00310EB1 File Offset: 0x0030F0B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureProperties>(deep);
		}

		// Token: 0x04008D44 RID: 36164
		private const string tagName = "nvPicPr";

		// Token: 0x04008D45 RID: 36165
		private const byte tagNsId = 17;

		// Token: 0x04008D46 RID: 36166
		internal const int ElementTypeIdConst = 10717;

		// Token: 0x04008D47 RID: 36167
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvPicPr" };

		// Token: 0x04008D48 RID: 36168
		private static readonly byte[] eleNamespaceIds = new byte[] { 17, 17 };
	}
}
