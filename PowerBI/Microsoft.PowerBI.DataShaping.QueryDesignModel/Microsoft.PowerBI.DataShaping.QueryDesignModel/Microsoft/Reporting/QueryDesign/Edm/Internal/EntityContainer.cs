using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200021A RID: 538
	internal sealed class EntityContainer : EdmItem
	{
		// Token: 0x060018D8 RID: 6360 RVA: 0x00043C1C File Offset: 0x00041E1C
		internal EntityContainer(EntityContainer entityContainer, ModelCapabilities overrideModelCapabilities)
		{
			this._entityContainer = ArgumentValidation.CheckNotNull<EntityContainer>(entityContainer, "entityContainer");
			XElement xelementMetadataProperty = this.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntityContainer");
			this._caption = xelementMetadataProperty.GetStringAttributeOrDefault(Extensions.CaptionAttr, string.Empty);
			this._culture = xelementMetadataProperty.GetStringAttributeOrDefault(Extensions.CultureAttr, null);
			XElement elementOrNull = xelementMetadataProperty.GetElementOrNull(Extensions.DefaultMeasureElem);
			if (elementOrNull != null)
			{
				XElement elementOrNull2 = elementOrNull.GetElementOrNull(Extensions.PropertyRefElem);
				if (elementOrNull2 != null)
				{
					this._defaultMeasureName = elementOrNull2.GetStringAttributeOrDefault(Extensions.NameAttr, null);
				}
			}
			XElement elementOrNull3 = xelementMetadataProperty.GetElementOrNull(Extensions.CompareOptionsElem);
			bool booleanAttributeOrDefault = elementOrNull3.GetBooleanAttributeOrDefault(Extensions.IgnoreCaseAttr, false);
			bool booleanAttributeOrDefault2 = elementOrNull3.GetBooleanAttributeOrDefault(Extensions.IgnoreKanaTypeAttr, false);
			bool booleanAttributeOrDefault3 = elementOrNull3.GetBooleanAttributeOrDefault(Extensions.IgnoreNonSpaceAttr, false);
			bool booleanAttributeOrDefault4 = elementOrNull3.GetBooleanAttributeOrDefault(Extensions.IgnoreWidthAttr, false);
			this._compareOptions = (booleanAttributeOrDefault ? CompareOptions.IgnoreCase : CompareOptions.None);
			this._compareOptions |= (booleanAttributeOrDefault2 ? CompareOptions.IgnoreKanaType : CompareOptions.None);
			this._compareOptions |= (booleanAttributeOrDefault3 ? CompareOptions.IgnoreNonSpace : CompareOptions.None);
			this._compareOptions |= (booleanAttributeOrDefault4 ? CompareOptions.IgnoreWidth : CompareOptions.None);
			this._preferOrdinalStringEquality = xelementMetadataProperty.GetBooleanAttributeOrDefault(Extensions.PreferOrdinalStringEqualityAttr, false);
			if (overrideModelCapabilities == null)
			{
				XElement elementOrNull4 = xelementMetadataProperty.GetElementOrNull(Extensions.ModelCapabilitiesElem);
				this._modelCapabilities = new ModelCapabilities(elementOrNull4);
			}
			else
			{
				this._modelCapabilities = overrideModelCapabilities;
			}
			this._mappedMPrameters = null;
			XElement elementOrNull5 = xelementMetadataProperty.GetElementOrNull(Extensions.MParametersElem);
			if (elementOrNull5 != null)
			{
				this._mappedMPrameters = MappedMParametersParser.Load(elementOrNull5);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00043D90 File Offset: 0x00041F90
		internal override MetadataItem InternalEdmItem
		{
			get
			{
				return this._entityContainer;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00043D98 File Offset: 0x00041F98
		internal string Caption
		{
			get
			{
				return this._caption;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00043DA0 File Offset: 0x00041FA0
		internal string Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00043DA8 File Offset: 0x00041FA8
		internal string DefaultMeasureName
		{
			get
			{
				return this._defaultMeasureName;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00043DB0 File Offset: 0x00041FB0
		internal CompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00043DB8 File Offset: 0x00041FB8
		internal bool PreferOrdinalStringEquality
		{
			get
			{
				return this._preferOrdinalStringEquality;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00043DC0 File Offset: 0x00041FC0
		internal string Name
		{
			get
			{
				return this._entityContainer.Name;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00043DCD File Offset: 0x00041FCD
		internal ModelCapabilities ModelCapabilities
		{
			get
			{
				return this._modelCapabilities;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00043DD5 File Offset: 0x00041FD5
		internal Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> MappedMPrameters
		{
			get
			{
				return this._mappedMPrameters;
			}
		}

		// Token: 0x04000D38 RID: 3384
		private readonly EntityContainer _entityContainer;

		// Token: 0x04000D39 RID: 3385
		private readonly string _caption;

		// Token: 0x04000D3A RID: 3386
		private readonly string _culture;

		// Token: 0x04000D3B RID: 3387
		private readonly string _defaultMeasureName;

		// Token: 0x04000D3C RID: 3388
		private readonly CompareOptions _compareOptions;

		// Token: 0x04000D3D RID: 3389
		private readonly bool _preferOrdinalStringEquality;

		// Token: 0x04000D3E RID: 3390
		private readonly ModelCapabilities _modelCapabilities;

		// Token: 0x04000D3F RID: 3391
		private readonly Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> _mappedMPrameters;
	}
}
