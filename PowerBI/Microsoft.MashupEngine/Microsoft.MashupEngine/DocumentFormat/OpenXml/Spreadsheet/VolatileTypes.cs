using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B29 RID: 11049
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VolatileType))]
	internal class VolatileTypes : OpenXmlPartRootElement
	{
		// Token: 0x1700771F RID: 30495
		// (get) Token: 0x0601695A RID: 92506 RVA: 0x0032CBF5 File Offset: 0x0032ADF5
		public override string LocalName
		{
			get
			{
				return "volTypes";
			}
		}

		// Token: 0x17007720 RID: 30496
		// (get) Token: 0x0601695B RID: 92507 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007721 RID: 30497
		// (get) Token: 0x0601695C RID: 92508 RVA: 0x0032CBFC File Offset: 0x0032ADFC
		internal override int ElementTypeId
		{
			get
			{
				return 11047;
			}
		}

		// Token: 0x0601695D RID: 92509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601695E RID: 92510 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal VolatileTypes(VolatileDependenciesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601695F RID: 92511 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(VolatileDependenciesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007722 RID: 30498
		// (get) Token: 0x06016960 RID: 92512 RVA: 0x0032CC03 File Offset: 0x0032AE03
		// (set) Token: 0x06016961 RID: 92513 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public VolatileDependenciesPart VolatileDependenciesPart
		{
			get
			{
				return base.OpenXmlPart as VolatileDependenciesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016962 RID: 92514 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public VolatileTypes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016963 RID: 92515 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public VolatileTypes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016964 RID: 92516 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public VolatileTypes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016965 RID: 92517 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public VolatileTypes()
		{
		}

		// Token: 0x06016966 RID: 92518 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(VolatileDependenciesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016967 RID: 92519 RVA: 0x0032CC10 File Offset: 0x0032AE10
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "volType" == name)
			{
				return new VolatileType();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06016968 RID: 92520 RVA: 0x0032CC43 File Offset: 0x0032AE43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VolatileTypes>(deep);
		}

		// Token: 0x0400992C RID: 39212
		private const string tagName = "volTypes";

		// Token: 0x0400992D RID: 39213
		private const byte tagNsId = 22;

		// Token: 0x0400992E RID: 39214
		internal const int ElementTypeIdConst = 11047;
	}
}
