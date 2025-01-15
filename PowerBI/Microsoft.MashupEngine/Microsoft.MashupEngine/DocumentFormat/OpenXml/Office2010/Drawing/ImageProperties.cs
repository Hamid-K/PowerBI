using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234D RID: 9037
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageLayer), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ImageProperties : OpenXmlCompositeElement
	{
		// Token: 0x170049C8 RID: 18888
		// (get) Token: 0x06010327 RID: 66343 RVA: 0x002E0EEE File Offset: 0x002DF0EE
		public override string LocalName
		{
			get
			{
				return "imgProps";
			}
		}

		// Token: 0x170049C9 RID: 18889
		// (get) Token: 0x06010328 RID: 66344 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049CA RID: 18890
		// (get) Token: 0x06010329 RID: 66345 RVA: 0x002E0EF5 File Offset: 0x002DF0F5
		internal override int ElementTypeId
		{
			get
			{
				return 12722;
			}
		}

		// Token: 0x0601032A RID: 66346 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601032B RID: 66347 RVA: 0x00293ECF File Offset: 0x002920CF
		public ImageProperties()
		{
		}

		// Token: 0x0601032C RID: 66348 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ImageProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601032D RID: 66349 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ImageProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601032E RID: 66350 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ImageProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601032F RID: 66351 RVA: 0x002E0EFC File Offset: 0x002DF0FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "imgLayer" == name)
			{
				return new ImageLayer();
			}
			return null;
		}

		// Token: 0x170049CB RID: 18891
		// (get) Token: 0x06010330 RID: 66352 RVA: 0x002E0F17 File Offset: 0x002DF117
		internal override string[] ElementTagNames
		{
			get
			{
				return ImageProperties.eleTagNames;
			}
		}

		// Token: 0x170049CC RID: 18892
		// (get) Token: 0x06010331 RID: 66353 RVA: 0x002E0F1E File Offset: 0x002DF11E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ImageProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170049CD RID: 18893
		// (get) Token: 0x06010332 RID: 66354 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170049CE RID: 18894
		// (get) Token: 0x06010333 RID: 66355 RVA: 0x002E0F25 File Offset: 0x002DF125
		// (set) Token: 0x06010334 RID: 66356 RVA: 0x002E0F2E File Offset: 0x002DF12E
		public ImageLayer ImageLayer
		{
			get
			{
				return base.GetElement<ImageLayer>(0);
			}
			set
			{
				base.SetElement<ImageLayer>(0, value);
			}
		}

		// Token: 0x06010335 RID: 66357 RVA: 0x002E0F38 File Offset: 0x002DF138
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageProperties>(deep);
		}

		// Token: 0x0400737E RID: 29566
		private const string tagName = "imgProps";

		// Token: 0x0400737F RID: 29567
		private const byte tagNsId = 48;

		// Token: 0x04007380 RID: 29568
		internal const int ElementTypeIdConst = 12722;

		// Token: 0x04007381 RID: 29569
		private static readonly string[] eleTagNames = new string[] { "imgLayer" };

		// Token: 0x04007382 RID: 29570
		private static readonly byte[] eleNamespaceIds = new byte[] { 48 };
	}
}
