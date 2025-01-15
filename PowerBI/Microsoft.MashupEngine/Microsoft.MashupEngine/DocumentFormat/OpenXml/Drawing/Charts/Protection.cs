using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002602 RID: 9730
	[ChildElementInfo(typeof(Formatting))]
	[ChildElementInfo(typeof(ChartObject))]
	[ChildElementInfo(typeof(Data))]
	[ChildElementInfo(typeof(Selection))]
	[ChildElementInfo(typeof(UserInterface))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Protection : OpenXmlCompositeElement
	{
		// Token: 0x170059AC RID: 22956
		// (get) Token: 0x06012678 RID: 75384 RVA: 0x002FAA24 File Offset: 0x002F8C24
		public override string LocalName
		{
			get
			{
				return "protection";
			}
		}

		// Token: 0x170059AD RID: 22957
		// (get) Token: 0x06012679 RID: 75385 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059AE RID: 22958
		// (get) Token: 0x0601267A RID: 75386 RVA: 0x002FAA2B File Offset: 0x002F8C2B
		internal override int ElementTypeId
		{
			get
			{
				return 10577;
			}
		}

		// Token: 0x0601267B RID: 75387 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601267C RID: 75388 RVA: 0x00293ECF File Offset: 0x002920CF
		public Protection()
		{
		}

		// Token: 0x0601267D RID: 75389 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Protection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601267E RID: 75390 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Protection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601267F RID: 75391 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Protection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012680 RID: 75392 RVA: 0x002FAA34 File Offset: 0x002F8C34
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "chartObject" == name)
			{
				return new ChartObject();
			}
			if (11 == namespaceId && "data" == name)
			{
				return new Data();
			}
			if (11 == namespaceId && "formatting" == name)
			{
				return new Formatting();
			}
			if (11 == namespaceId && "selection" == name)
			{
				return new Selection();
			}
			if (11 == namespaceId && "userInterface" == name)
			{
				return new UserInterface();
			}
			return null;
		}

		// Token: 0x170059AF RID: 22959
		// (get) Token: 0x06012681 RID: 75393 RVA: 0x002FAABA File Offset: 0x002F8CBA
		internal override string[] ElementTagNames
		{
			get
			{
				return Protection.eleTagNames;
			}
		}

		// Token: 0x170059B0 RID: 22960
		// (get) Token: 0x06012682 RID: 75394 RVA: 0x002FAAC1 File Offset: 0x002F8CC1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Protection.eleNamespaceIds;
			}
		}

		// Token: 0x170059B1 RID: 22961
		// (get) Token: 0x06012683 RID: 75395 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059B2 RID: 22962
		// (get) Token: 0x06012684 RID: 75396 RVA: 0x002FAAC8 File Offset: 0x002F8CC8
		// (set) Token: 0x06012685 RID: 75397 RVA: 0x002FAAD1 File Offset: 0x002F8CD1
		public ChartObject ChartObject
		{
			get
			{
				return base.GetElement<ChartObject>(0);
			}
			set
			{
				base.SetElement<ChartObject>(0, value);
			}
		}

		// Token: 0x170059B3 RID: 22963
		// (get) Token: 0x06012686 RID: 75398 RVA: 0x002FAADB File Offset: 0x002F8CDB
		// (set) Token: 0x06012687 RID: 75399 RVA: 0x002FAAE4 File Offset: 0x002F8CE4
		public Data Data
		{
			get
			{
				return base.GetElement<Data>(1);
			}
			set
			{
				base.SetElement<Data>(1, value);
			}
		}

		// Token: 0x170059B4 RID: 22964
		// (get) Token: 0x06012688 RID: 75400 RVA: 0x002FAAEE File Offset: 0x002F8CEE
		// (set) Token: 0x06012689 RID: 75401 RVA: 0x002FAAF7 File Offset: 0x002F8CF7
		public Formatting Formatting
		{
			get
			{
				return base.GetElement<Formatting>(2);
			}
			set
			{
				base.SetElement<Formatting>(2, value);
			}
		}

		// Token: 0x170059B5 RID: 22965
		// (get) Token: 0x0601268A RID: 75402 RVA: 0x002FAB01 File Offset: 0x002F8D01
		// (set) Token: 0x0601268B RID: 75403 RVA: 0x002FAB0A File Offset: 0x002F8D0A
		public Selection Selection
		{
			get
			{
				return base.GetElement<Selection>(3);
			}
			set
			{
				base.SetElement<Selection>(3, value);
			}
		}

		// Token: 0x170059B6 RID: 22966
		// (get) Token: 0x0601268C RID: 75404 RVA: 0x002FAB14 File Offset: 0x002F8D14
		// (set) Token: 0x0601268D RID: 75405 RVA: 0x002FAB1D File Offset: 0x002F8D1D
		public UserInterface UserInterface
		{
			get
			{
				return base.GetElement<UserInterface>(4);
			}
			set
			{
				base.SetElement<UserInterface>(4, value);
			}
		}

		// Token: 0x0601268E RID: 75406 RVA: 0x002FAB27 File Offset: 0x002F8D27
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Protection>(deep);
		}

		// Token: 0x04007F70 RID: 32624
		private const string tagName = "protection";

		// Token: 0x04007F71 RID: 32625
		private const byte tagNsId = 11;

		// Token: 0x04007F72 RID: 32626
		internal const int ElementTypeIdConst = 10577;

		// Token: 0x04007F73 RID: 32627
		private static readonly string[] eleTagNames = new string[] { "chartObject", "data", "formatting", "selection", "userInterface" };

		// Token: 0x04007F74 RID: 32628
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
