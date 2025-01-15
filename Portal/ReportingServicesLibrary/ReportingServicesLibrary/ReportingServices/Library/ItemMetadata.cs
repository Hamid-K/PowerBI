using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002AD RID: 685
	internal sealed class ItemMetadata : Dictionary<string, string>
	{
		// Token: 0x060018FA RID: 6394 RVA: 0x00064A19 File Offset: 0x00062C19
		public ItemMetadata()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00064A26 File Offset: 0x00062C26
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x00064A44 File Offset: 0x00062C44
		public DateTime ExecutionDateTime
		{
			get
			{
				return this.GetValueOrDefault<DateTime>("ExecutionDateTime", DateTime.MinValue, new Converter<string, DateTime>(Globals.ParsePublicDateTimeFormat));
			}
			set
			{
				if (value != DateTime.MinValue)
				{
					base["ExecutionDateTime"] = Globals.ToPublicDateTimeFormat(value);
					return;
				}
				base.Remove("ExecutionDateTime");
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00064A71 File Offset: 0x00062C71
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x00064AA2 File Offset: 0x00062CA2
		public Guid ComponentID
		{
			get
			{
				return this.GetValueOrDefault<Guid>("ComponentID", Guid.Empty, (string s) => new Guid(s));
			}
			set
			{
				base["ComponentID"] = ItemProperties.ComponentIDToString(value);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00064AB5 File Offset: 0x00062CB5
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x00064AE6 File Offset: 0x00062CE6
		public Guid ParentID
		{
			get
			{
				return this.GetValueOrDefault<Guid>("ParentID", Guid.Empty, (string s) => new Guid(s));
			}
			set
			{
				base["ParentID"] = ItemProperties.ParentIDToString(new Guid?(value));
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x00064AFE File Offset: 0x00062CFE
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x00064B0B File Offset: 0x00062D0B
		public string MimeType
		{
			get
			{
				return this.GetStringValueOrNull("MIMEType");
			}
			set
			{
				base["MIMEType"] = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00064B19 File Offset: 0x00062D19
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x00064B26 File Offset: 0x00062D26
		public string SubType
		{
			get
			{
				return this.GetStringValueOrNull("Subtype");
			}
			set
			{
				base["Subtype"] = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00064B34 File Offset: 0x00062D34
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x00064B42 File Offset: 0x00062D42
		public bool HasDataSources
		{
			get
			{
				return this.GetBooleanValueOrDefault("HasDataSources", false);
			}
			set
			{
				base["HasDataSources"] = value.ToString();
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00064B56 File Offset: 0x00062D56
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x00064B64 File Offset: 0x00062D64
		public bool HasSharedDataSets
		{
			get
			{
				return this.GetBooleanValueOrDefault("HasSharedDataSets", false);
			}
			set
			{
				base["HasSharedDataSets"] = value.ToString();
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x00064B78 File Offset: 0x00062D78
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x00064B86 File Offset: 0x00062D86
		public bool HasParameters
		{
			get
			{
				return this.GetBooleanValueOrDefault("HasParameters", false);
			}
			set
			{
				base["HasParameters"] = value.ToString();
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00064B9C File Offset: 0x00062D9C
		private T GetValueOrDefault<T>(string key, T defaultValue, Converter<string, T> converter)
		{
			string text;
			if (base.TryGetValue(key, out text))
			{
				return converter(text);
			}
			return defaultValue;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00064BBD File Offset: 0x00062DBD
		private string GetStringValueOrNull(string key)
		{
			return this.GetValueOrDefault<string>(key, null, (string s) => s);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00064BE8 File Offset: 0x00062DE8
		private bool GetBooleanValueOrDefault(string key, bool defaultValue)
		{
			string stringValueOrNull = this.GetStringValueOrNull(key);
			if (string.IsNullOrEmpty(stringValueOrNull))
			{
				return defaultValue;
			}
			bool flag;
			if (!bool.TryParse(stringValueOrNull, out flag))
			{
				return defaultValue;
			}
			return flag;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00064C14 File Offset: 0x00062E14
		public Property[] ConvertToProperties()
		{
			Property[] array = new Property[base.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				Property property = new Property();
				property.Name = keyValuePair.Key;
				property.Value = keyValuePair.Value;
				array[num++] = property;
			}
			return array;
		}
	}
}
