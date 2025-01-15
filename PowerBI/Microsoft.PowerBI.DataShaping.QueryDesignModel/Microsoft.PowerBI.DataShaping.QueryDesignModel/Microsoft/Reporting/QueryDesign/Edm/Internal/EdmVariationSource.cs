using System;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000219 RID: 537
	internal sealed class EdmVariationSource : IEntityMemberItem
	{
		// Token: 0x060018D0 RID: 6352 RVA: 0x00043AB8 File Offset: 0x00041CB8
		internal EdmVariationSource(XElement element)
		{
			ArgumentValidation.CheckNotNull<XElement>(element, "element");
			this._caption = element.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			XAttribute xattribute = ArgumentValidation.CheckNotNull<XAttribute>(element.Attribute(Extensions.NameAttr), "Name");
			this._name = ArgumentValidation.CheckNotNullOrEmpty(xattribute.Value, "Name");
			this._isDefault = element.GetBooleanAttributeOrDefault(Extensions.DefaultAttr, false);
			this._referenceName = element.GetStringAttributeOrDefault(Extensions.ReferenceNameAttr, null);
			XElement elementOrNull = element.GetElementOrNull(Extensions.NavigationPropertyRefElem);
			if (elementOrNull != null)
			{
				this._navPropName = elementOrNull.GetStringAttributeOrDefault(Extensions.NameAttr, null);
				ArgumentValidation.CheckNotNullOrEmpty(this._navPropName, "NavigationPropertyRef Name");
			}
			XElement elementOrNull2 = element.GetElementOrNull(Extensions.VariationDefaultHierarchyRefElem);
			if (elementOrNull2 != null)
			{
				this._defaultHierarchyName = elementOrNull2.GetStringAttributeOrDefault(Extensions.NameAttr, null);
				ArgumentValidation.CheckNotNullOrEmpty(this._defaultHierarchyName, "DefaultHierarchyRef Name");
			}
			XElement elementOrNull3 = element.GetElementOrNull(Extensions.VariationDefaultPropertyRefElem);
			if (elementOrNull3 != null)
			{
				this._defaultPropertyName = elementOrNull3.GetStringAttributeOrDefault(Extensions.NameAttr, null);
				ArgumentValidation.CheckNotNullOrEmpty(this._defaultPropertyName, "DefaultPropertyRef Name");
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00043BCF File Offset: 0x00041DCF
		public string Caption
		{
			get
			{
				return this._caption ?? this.Name;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00043BE1 File Offset: 0x00041DE1
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00043BE9 File Offset: 0x00041DE9
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? this.Name;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00043BFB File Offset: 0x00041DFB
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00043C03 File Offset: 0x00041E03
		public string NavigationPropertyName
		{
			get
			{
				return this._navPropName;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00043C0B File Offset: 0x00041E0B
		public string DefaultHierarchyName
		{
			get
			{
				return this._defaultHierarchyName;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00043C13 File Offset: 0x00041E13
		public string DefaultPropertyName
		{
			get
			{
				return this._defaultPropertyName;
			}
		}

		// Token: 0x04000D31 RID: 3377
		private readonly string _caption;

		// Token: 0x04000D32 RID: 3378
		private readonly string _name;

		// Token: 0x04000D33 RID: 3379
		private readonly string _referenceName;

		// Token: 0x04000D34 RID: 3380
		private readonly bool _isDefault;

		// Token: 0x04000D35 RID: 3381
		private readonly string _navPropName;

		// Token: 0x04000D36 RID: 3382
		private readonly string _defaultHierarchyName;

		// Token: 0x04000D37 RID: 3383
		private readonly string _defaultPropertyName;
	}
}
