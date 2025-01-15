using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000079 RID: 121
	internal sealed class ItemSearchOptions
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00014967 File Offset: 0x00012B67
		public bool Recursive
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_recursive;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001496F File Offset: 0x00012B6F
		public CultureInfo Language
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_lang;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00014977 File Offset: 0x00012B77
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0001497F File Offset: 0x00012B7F
		public ServerCompatLevel CompatLevel
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_compat;
			}
			set
			{
				this.m_compat = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00014988 File Offset: 0x00012B88
		public bool ComponentLookup
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_componentLookup;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00014990 File Offset: 0x00012B90
		public ItemSearchOptions(Property[] options)
		{
			if (options == null)
			{
				return;
			}
			for (int i = 0; i < options.Length; i++)
			{
				string name = options[i].Name;
				if (name == null)
				{
					throw new MissingElementException("Name");
				}
				if (ItemSearchOptions.m_properties == null)
				{
					ItemSearchOptions.m_properties = ItemSearchOptions.BuildPropertyMap();
				}
				ItemSearchOptions.PropertySetter propertySetter;
				if (ItemSearchOptions.m_properties.TryGetValue(name, out propertySetter))
				{
					propertySetter.SetProperty(options[i], this);
				}
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00014A08 File Offset: 0x00012C08
		private static Dictionary<string, ItemSearchOptions.PropertySetter> BuildPropertyMap()
		{
			Dictionary<string, ItemSearchOptions.PropertySetter> dictionary = new Dictionary<string, ItemSearchOptions.PropertySetter>(3, StringComparer.OrdinalIgnoreCase);
			dictionary["Recursive"] = new ItemSearchOptions.BooleanSetter(delegate(bool value, ItemSearchOptions target)
			{
				target.m_recursive = value;
			});
			dictionary["Locale"] = new ItemSearchOptions.LanguageSetter();
			dictionary["ComponentLookup"] = new ItemSearchOptions.BooleanSetter(delegate(bool value, ItemSearchOptions target)
			{
				target.m_componentLookup = value;
			});
			return dictionary;
		}

		// Token: 0x04000257 RID: 599
		private bool m_recursive = true;

		// Token: 0x04000258 RID: 600
		private CultureInfo m_lang = Localization.ClientPrimaryCulture;

		// Token: 0x04000259 RID: 601
		private ServerCompatLevel m_compat;

		// Token: 0x0400025A RID: 602
		private bool m_componentLookup;

		// Token: 0x0400025B RID: 603
		public const string RecursiveOption = "Recursive";

		// Token: 0x0400025C RID: 604
		public const string LanguageOption = "Locale";

		// Token: 0x0400025D RID: 605
		public const string ComponentLookupOption = "ComponentLookup";

		// Token: 0x0400025E RID: 606
		private static Dictionary<string, ItemSearchOptions.PropertySetter> m_properties;

		// Token: 0x02000447 RID: 1095
		// (Invoke) Token: 0x060022FC RID: 8956
		private delegate void Binder<T>(T value, ItemSearchOptions target);

		// Token: 0x02000448 RID: 1096
		private abstract class PropertySetter
		{
			// Token: 0x060022FF RID: 8959
			public abstract void SetProperty(Property property, ItemSearchOptions target);
		}

		// Token: 0x02000449 RID: 1097
		private class BooleanSetter : ItemSearchOptions.PropertySetter
		{
			// Token: 0x06002301 RID: 8961 RVA: 0x00083042 File Offset: 0x00081242
			public BooleanSetter(ItemSearchOptions.Binder<bool> binder)
			{
				RSTrace.CatalogTrace.Assert(binder != null, "binder");
				this.m_binder = binder;
			}

			// Token: 0x06002302 RID: 8962 RVA: 0x00083064 File Offset: 0x00081264
			public override void SetProperty(Property property, ItemSearchOptions target)
			{
				bool flag;
				if (!bool.TryParse(property.Value, out flag))
				{
					throw new InvalidElementException(property.Name);
				}
				this.m_binder(flag, target);
			}

			// Token: 0x04000F63 RID: 3939
			public ItemSearchOptions.Binder<bool> m_binder;
		}

		// Token: 0x0200044A RID: 1098
		private class LanguageSetter : ItemSearchOptions.PropertySetter
		{
			// Token: 0x06002303 RID: 8963 RVA: 0x0008309C File Offset: 0x0008129C
			public override void SetProperty(Property property, ItemSearchOptions target)
			{
				try
				{
					if (property.Value == null)
					{
						throw new InvalidElementException(property.Name);
					}
					CultureInfo cultureInfoNoUserOverrides = Localization.GetCultureInfoNoUserOverrides(property.Value);
					target.m_lang = cultureInfoNoUserOverrides;
				}
				catch (ArgumentException ex)
				{
					throw new InvalidElementException(property.Name, ex);
				}
			}
		}
	}
}
