using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B24 RID: 11044
	[ChildElementInfo(typeof(FutureMetadata))]
	[ChildElementInfo(typeof(CellMetadata))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MetadataTypes))]
	[ChildElementInfo(typeof(MetadataStrings))]
	[ChildElementInfo(typeof(MdxMetadata))]
	[ChildElementInfo(typeof(ValueMetadata))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Metadata : OpenXmlPartRootElement
	{
		// Token: 0x170076D7 RID: 30423
		// (get) Token: 0x060168AE RID: 92334 RVA: 0x002A80EF File Offset: 0x002A62EF
		public override string LocalName
		{
			get
			{
				return "metadata";
			}
		}

		// Token: 0x170076D8 RID: 30424
		// (get) Token: 0x060168AF RID: 92335 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076D9 RID: 30425
		// (get) Token: 0x060168B0 RID: 92336 RVA: 0x0032C285 File Offset: 0x0032A485
		internal override int ElementTypeId
		{
			get
			{
				return 11042;
			}
		}

		// Token: 0x060168B1 RID: 92337 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060168B2 RID: 92338 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Metadata(CellMetadataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060168B3 RID: 92339 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CellMetadataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076DA RID: 30426
		// (get) Token: 0x060168B4 RID: 92340 RVA: 0x0032C28C File Offset: 0x0032A48C
		// (set) Token: 0x060168B5 RID: 92341 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CellMetadataPart CellMetadataPart
		{
			get
			{
				return base.OpenXmlPart as CellMetadataPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060168B6 RID: 92342 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Metadata(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168B7 RID: 92343 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Metadata(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168B8 RID: 92344 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Metadata(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060168B9 RID: 92345 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Metadata()
		{
		}

		// Token: 0x060168BA RID: 92346 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CellMetadataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060168BB RID: 92347 RVA: 0x0032C29C File Offset: 0x0032A49C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "metadataTypes" == name)
			{
				return new MetadataTypes();
			}
			if (22 == namespaceId && "metadataStrings" == name)
			{
				return new MetadataStrings();
			}
			if (22 == namespaceId && "mdxMetadata" == name)
			{
				return new MdxMetadata();
			}
			if (22 == namespaceId && "futureMetadata" == name)
			{
				return new FutureMetadata();
			}
			if (22 == namespaceId && "cellMetadata" == name)
			{
				return new CellMetadata();
			}
			if (22 == namespaceId && "valueMetadata" == name)
			{
				return new ValueMetadata();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170076DB RID: 30427
		// (get) Token: 0x060168BC RID: 92348 RVA: 0x0032C352 File Offset: 0x0032A552
		internal override string[] ElementTagNames
		{
			get
			{
				return Metadata.eleTagNames;
			}
		}

		// Token: 0x170076DC RID: 30428
		// (get) Token: 0x060168BD RID: 92349 RVA: 0x0032C359 File Offset: 0x0032A559
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Metadata.eleNamespaceIds;
			}
		}

		// Token: 0x170076DD RID: 30429
		// (get) Token: 0x060168BE RID: 92350 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170076DE RID: 30430
		// (get) Token: 0x060168BF RID: 92351 RVA: 0x0032C360 File Offset: 0x0032A560
		// (set) Token: 0x060168C0 RID: 92352 RVA: 0x0032C369 File Offset: 0x0032A569
		public MetadataTypes MetadataTypes
		{
			get
			{
				return base.GetElement<MetadataTypes>(0);
			}
			set
			{
				base.SetElement<MetadataTypes>(0, value);
			}
		}

		// Token: 0x170076DF RID: 30431
		// (get) Token: 0x060168C1 RID: 92353 RVA: 0x0032C373 File Offset: 0x0032A573
		// (set) Token: 0x060168C2 RID: 92354 RVA: 0x0032C37C File Offset: 0x0032A57C
		public MetadataStrings MetadataStrings
		{
			get
			{
				return base.GetElement<MetadataStrings>(1);
			}
			set
			{
				base.SetElement<MetadataStrings>(1, value);
			}
		}

		// Token: 0x170076E0 RID: 30432
		// (get) Token: 0x060168C3 RID: 92355 RVA: 0x0032C386 File Offset: 0x0032A586
		// (set) Token: 0x060168C4 RID: 92356 RVA: 0x0032C38F File Offset: 0x0032A58F
		public MdxMetadata MdxMetadata
		{
			get
			{
				return base.GetElement<MdxMetadata>(2);
			}
			set
			{
				base.SetElement<MdxMetadata>(2, value);
			}
		}

		// Token: 0x060168C5 RID: 92357 RVA: 0x0032C399 File Offset: 0x0032A599
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Metadata>(deep);
		}

		// Token: 0x04009915 RID: 39189
		private const string tagName = "metadata";

		// Token: 0x04009916 RID: 39190
		private const byte tagNsId = 22;

		// Token: 0x04009917 RID: 39191
		internal const int ElementTypeIdConst = 11042;

		// Token: 0x04009918 RID: 39192
		private static readonly string[] eleTagNames = new string[] { "metadataTypes", "metadataStrings", "mdxMetadata", "futureMetadata", "cellMetadata", "valueMetadata", "extLst" };

		// Token: 0x04009919 RID: 39193
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22, 22 };
	}
}
