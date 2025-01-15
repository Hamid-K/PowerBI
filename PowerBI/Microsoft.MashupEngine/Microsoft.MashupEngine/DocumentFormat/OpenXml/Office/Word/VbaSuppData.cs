using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200245D RID: 9309
	[ChildElementInfo(typeof(Mcds))]
	[ChildElementInfo(typeof(DocEvents))]
	[GeneratedCode("DomGen", "2.0")]
	internal class VbaSuppData : OpenXmlPartRootElement
	{
		// Token: 0x170050A0 RID: 20640
		// (get) Token: 0x06011259 RID: 70233 RVA: 0x002EB091 File Offset: 0x002E9291
		public override string LocalName
		{
			get
			{
				return "vbaSuppData";
			}
		}

		// Token: 0x170050A1 RID: 20641
		// (get) Token: 0x0601125A RID: 70234 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050A2 RID: 20642
		// (get) Token: 0x0601125B RID: 70235 RVA: 0x002EB098 File Offset: 0x002E9298
		internal override int ElementTypeId
		{
			get
			{
				return 12539;
			}
		}

		// Token: 0x0601125C RID: 70236 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601125D RID: 70237 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal VbaSuppData(VbaDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601125E RID: 70238 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(VbaDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170050A3 RID: 20643
		// (get) Token: 0x0601125F RID: 70239 RVA: 0x002EB09F File Offset: 0x002E929F
		// (set) Token: 0x06011260 RID: 70240 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public VbaDataPart VbaDataPart
		{
			get
			{
				return base.OpenXmlPart as VbaDataPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06011261 RID: 70241 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public VbaSuppData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011262 RID: 70242 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public VbaSuppData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011263 RID: 70243 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public VbaSuppData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011264 RID: 70244 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public VbaSuppData()
		{
		}

		// Token: 0x06011265 RID: 70245 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(VbaDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06011266 RID: 70246 RVA: 0x002EB0AC File Offset: 0x002E92AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "docEvents" == name)
			{
				return new DocEvents();
			}
			if (33 == namespaceId && "mcds" == name)
			{
				return new Mcds();
			}
			return null;
		}

		// Token: 0x170050A4 RID: 20644
		// (get) Token: 0x06011267 RID: 70247 RVA: 0x002EB0DF File Offset: 0x002E92DF
		internal override string[] ElementTagNames
		{
			get
			{
				return VbaSuppData.eleTagNames;
			}
		}

		// Token: 0x170050A5 RID: 20645
		// (get) Token: 0x06011268 RID: 70248 RVA: 0x002EB0E6 File Offset: 0x002E92E6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return VbaSuppData.eleNamespaceIds;
			}
		}

		// Token: 0x170050A6 RID: 20646
		// (get) Token: 0x06011269 RID: 70249 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170050A7 RID: 20647
		// (get) Token: 0x0601126A RID: 70250 RVA: 0x002EB0ED File Offset: 0x002E92ED
		// (set) Token: 0x0601126B RID: 70251 RVA: 0x002EB0F6 File Offset: 0x002E92F6
		public DocEvents DocEvents
		{
			get
			{
				return base.GetElement<DocEvents>(0);
			}
			set
			{
				base.SetElement<DocEvents>(0, value);
			}
		}

		// Token: 0x170050A8 RID: 20648
		// (get) Token: 0x0601126C RID: 70252 RVA: 0x002EB100 File Offset: 0x002E9300
		// (set) Token: 0x0601126D RID: 70253 RVA: 0x002EB109 File Offset: 0x002E9309
		public Mcds Mcds
		{
			get
			{
				return base.GetElement<Mcds>(1);
			}
			set
			{
				base.SetElement<Mcds>(1, value);
			}
		}

		// Token: 0x0601126E RID: 70254 RVA: 0x002EB113 File Offset: 0x002E9313
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VbaSuppData>(deep);
		}

		// Token: 0x0400785F RID: 30815
		private const string tagName = "vbaSuppData";

		// Token: 0x04007860 RID: 30816
		private const byte tagNsId = 33;

		// Token: 0x04007861 RID: 30817
		internal const int ElementTypeIdConst = 12539;

		// Token: 0x04007862 RID: 30818
		private static readonly string[] eleTagNames = new string[] { "docEvents", "mcds" };

		// Token: 0x04007863 RID: 30819
		private static readonly byte[] eleNamespaceIds = new byte[] { 33, 33 };
	}
}
