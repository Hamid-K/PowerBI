using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dotless.Core;
using dotless.Core.configuration;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000030 RID: 48
	[SystemResourceType(SystemResourceType.UniversalBrand)]
	internal sealed class UniversalBrandProcessor : ISystemResourceProcessor
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public UniversalBrandProcessor()
		{
			string text = string.Format("{0}.{1}", EmbeddedSystemResourceManager.ResourceRoot, "UniversalBrandV2.less");
			using (Stream manifestResourceStream = EmbeddedSystemResourceManager.ResourceAssembly.GetManifestResourceStream(text))
			{
				byte[] array = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
				this._lessTemplate = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E32C File Offset: 0x0000C52C
		public bool IsProcessed(Microsoft.ReportingServices.Library.SystemResource resource)
		{
			if (this._processedResourceId != null)
			{
				Guid? processedResourceId = this._processedResourceId;
				Guid id = resource.Id;
				return processedResourceId != null && (processedResourceId == null || processedResourceId.GetValueOrDefault() == id);
			}
			return false;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E37C File Offset: 0x0000C57C
		public void Process(Microsoft.ReportingServices.Library.SystemResource resource, IEnumerable<ISystemResourceManager> resourceManagers)
		{
			if (SystemResourceService.GetSystemResourceType(resource.TypeName) != SystemResourceType.UniversalBrand)
			{
				return;
			}
			if (this.IsProcessed(resource))
			{
				resource.Items["stylesheet"] = Guid.Empty.ToString();
				return;
			}
			byte[] array = null;
			if (!UniversalBrandProcessor.TryLoadFile(resourceManagers, "colors", out array))
			{
				return;
			}
			string @string = Encoding.UTF8.GetString(array);
			string text;
			try
			{
				text = UniversalBrandProcessor.ConvertColorsToLess(@string);
			}
			catch
			{
				throw new SystemResourceProcessingException(SR.Error_UniversalBrandInvalidColorsFile);
			}
			string text2 = this._lessTemplate.Replace("/* interface:colors */", text);
			if (resource.Items.ContainsKey("logo"))
			{
				text2 = text2.Replace("@showLogo:false;", "@showLogo:true;");
			}
			DotlessConfiguration dotlessConfiguration = new DotlessConfiguration
			{
				MinifyOutput = true
			};
			string text3 = Less.Parse(text2, dotlessConfiguration);
			this._processedStylesheet = Encoding.UTF8.GetBytes(text3);
			this._processedResourceId = new Guid?(resource.Id);
			resource.Items["stylesheet"] = Guid.Empty.ToString();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		public bool TryLoadItem(Microsoft.ReportingServices.Library.SystemResource resource, string itemName, out byte[] bytes, out string contentType, out string filename)
		{
			bytes = null;
			contentType = null;
			filename = null;
			if (resource == null || !string.Equals(resource.TypeName, "UniversalBrand", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.IsNullOrEmpty(itemName) || !string.Equals(itemName, "stylesheet", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			bytes = this._processedStylesheet;
			contentType = "text/css";
			filename = "stylesheet.css";
			return true;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E504 File Offset: 0x0000C704
		internal static bool TryLoadFile(IEnumerable<ISystemResourceManager> resourceManagers, string itemName, out byte[] bytes)
		{
			bytes = null;
			using (IEnumerator<ISystemResourceManager> enumerator = resourceManagers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryLoadContentItem("UniversalBrand", itemName, out bytes))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E55C File Offset: 0x0000C75C
		internal static string ConvertColorsToLess(string colorsFileJson)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(colorsFileJson))
			{
				using (JsonTextReader reader = new JsonTextReader(stringReader))
				{
					Func<string, bool> <>9__0;
					while (reader.Read())
					{
						if (reader.Value != null && reader.TokenType == JsonToken.PropertyName && reader.Depth == 1 && reader.Value as string == "interface")
						{
							Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
							foreach (string text in UniversalBrandProcessor.DefaultColors.Keys)
							{
								dictionary[text] = false;
							}
							reader.Read();
							while (reader.Read() && reader.TokenType != JsonToken.EndObject)
							{
								IEnumerable<string> keys = dictionary.Keys;
								Func<string, bool> func;
								if ((func = <>9__0) == null)
								{
									func = (<>9__0 = (string x) => x == (string)reader.Value);
								}
								if (keys.Any(func))
								{
									dictionary[(string)reader.Value] = true;
								}
								stringBuilder.AppendFormat("    @{0}:", reader.Value);
								reader.Read();
								stringBuilder.AppendFormat("{0};", reader.Value);
								stringBuilder.AppendLine();
							}
							foreach (KeyValuePair<string, bool> keyValuePair in dictionary.Where((KeyValuePair<string, bool> x) => !x.Value))
							{
								stringBuilder.AppendFormat("    @{0}:", keyValuePair.Key);
								stringBuilder.AppendFormat("{0};", UniversalBrandProcessor.DefaultColors[keyValuePair.Key]);
								stringBuilder.AppendLine();
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000097 RID: 151
		private const string _colorFileInterfaceProperty = "interface";

		// Token: 0x04000098 RID: 152
		private const string _embeddedResourceV2Name = "UniversalBrandV2.less";

		// Token: 0x04000099 RID: 153
		private const string _lessColorsStartToken = "/* interface:colors */";

		// Token: 0x0400009A RID: 154
		private const string _lessHideLogoToken = "@showLogo:false;";

		// Token: 0x0400009B RID: 155
		private const string _lessShowLogoToken = "@showLogo:true;";

		// Token: 0x0400009C RID: 156
		private const string _packageColorsItemKey = "colors";

		// Token: 0x0400009D RID: 157
		private const string _packageLogoItemKey = "logo";

		// Token: 0x0400009E RID: 158
		private const string _processedStylesheetKey = "stylesheet";

		// Token: 0x0400009F RID: 159
		public static Dictionary<string, string> DefaultColors = new Dictionary<string, string>
		{
			{ "primary", "#117865" },
			{ "primaryAlt", "#0c695a" },
			{ "primaryAlt2", "#0a5c50" },
			{ "primaryAlt3", "#666666" },
			{ "primaryAlt4", "#0c695a" },
			{ "primaryContrast", "#ffffff" },
			{ "secondary", "#292929" },
			{ "secondaryAlt", "#333333" },
			{ "secondaryAlt2", "#edebe9" },
			{ "secondaryAlt3", "#117865" },
			{ "secondaryContrast", "#f3f2f1" },
			{ "neutralPrimary", "#FFFFFF" },
			{ "neutralPrimaryAlt", "#FFFFFF" },
			{ "neutralPrimaryAlt2", "#edebe9" },
			{ "neutralPrimaryAlt3", "#8A8886" },
			{ "neutralPrimaryContrast", "#222222" },
			{ "neutralSecondary", "#FFFFFF" },
			{ "neutralSecondaryAlt", "#eeeeee" },
			{ "neutralSecondaryAlt2", "#dddddd" },
			{ "neutralSecondaryAlt3", "#c8c8c8" },
			{ "neutralSecondaryContrast", "#252423" },
			{ "neutralTertiary", "#b7b7b7" },
			{ "neutralTertiaryAlt", "#c8c8c8" },
			{ "neutralTertiaryAlt2", "#eaeaea" },
			{ "neutralTertiaryAlt3", "#FFFFFF" },
			{ "neutralTertiaryContrast", "#252423" },
			{ "danger", "#bb2124" },
			{ "success", "#2b3" },
			{ "warning", "#f0ad4e" },
			{ "info", "#5bc0de" },
			{ "dangerContrast", "null" },
			{ "successContrast", "null" },
			{ "warningContrast", "null" },
			{ "infoContrast", "#FFFFFF" },
			{ "kpiGood", "#4fb443" },
			{ "kpiBad", "#de061a" },
			{ "kpiNeutral", "#d9b42c" },
			{ "kpiNone", "#333" },
			{ "kpiGoodContrast", "#FFFFFF" },
			{ "kpiBadContrast", "#FFFFFF" },
			{ "kpiNeutralContrast", "#FFFFFF" },
			{ "kpiNoneContrast", "#FFFFFF" },
			{ "itemTypeIconColor", "#FFFFFF" },
			{ "reportIconBackground", "#12239E" },
			{ "excelIconBackground", "#217346" },
			{ "folderIconBackground", "#4668C5" },
			{ "datasetIconBackground", "#C94F0F" },
			{ "otherIconBackground", "#000000" },
			{ "primaryButton", "#117865" },
			{ "primaryButtonHover", "#0C695A" },
			{ "primaryButtonPressed", "#033F38" },
			{ "link", "#0C695A" },
			{ "linkHover", "#0A5C50" },
			{ "linkVisited", "#033F38" },
			{ "radioButtonCheckBox", "#117865" },
			{ "radioButtonCheckBoxHover", "#0C695A" }
		};

		// Token: 0x040000A0 RID: 160
		private readonly string _lessTemplate;

		// Token: 0x040000A1 RID: 161
		private Guid? _processedResourceId;

		// Token: 0x040000A2 RID: 162
		private byte[] _processedStylesheet;
	}
}
