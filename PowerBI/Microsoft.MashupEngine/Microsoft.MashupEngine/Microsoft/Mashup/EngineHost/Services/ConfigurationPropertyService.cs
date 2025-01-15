using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200199C RID: 6556
	public class ConfigurationPropertyService : IConfigurationPropertyService
	{
		// Token: 0x0600A645 RID: 42565 RVA: 0x00226568 File Offset: 0x00224768
		public ConfigurationPropertyService(IDictionary<string, object> properties = null)
		{
			Dictionary<string, object> dictionary = ConfigurationPropertyService.defaultValues;
			lock (dictionary)
			{
				this.values = new Dictionary<string, object>(ConfigurationPropertyService.defaultValues);
			}
			if (properties != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in properties)
				{
					this.values[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x17002A6B RID: 10859
		// (get) Token: 0x0600A646 RID: 42566 RVA: 0x00226604 File Offset: 0x00224804
		public IDictionary<string, object> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17002A6C RID: 10860
		// (get) Token: 0x0600A647 RID: 42567 RVA: 0x0022660C File Offset: 0x0022480C
		public static IDictionary<string, object> DefaultValues
		{
			get
			{
				Dictionary<string, object> dictionary = ConfigurationPropertyService.defaultValues;
				IDictionary<string, object> dictionary2;
				lock (dictionary)
				{
					dictionary2 = new Dictionary<string, object>(ConfigurationPropertyService.defaultValues);
				}
				return dictionary2;
			}
		}

		// Token: 0x0600A648 RID: 42568 RVA: 0x00226654 File Offset: 0x00224854
		public static void SetDefaultValue(string propertyName, object value)
		{
			Dictionary<string, object> dictionary = ConfigurationPropertyService.defaultValues;
			lock (dictionary)
			{
				ConfigurationPropertyService.defaultValues[propertyName] = value;
			}
		}

		// Token: 0x0600A649 RID: 42569 RVA: 0x0022669C File Offset: 0x0022489C
		public static void ClearDefaultValues()
		{
			Dictionary<string, object> dictionary = ConfigurationPropertyService.defaultValues;
			lock (dictionary)
			{
				ConfigurationPropertyService.defaultValues.Clear();
			}
		}

		// Token: 0x0600A64A RID: 42570 RVA: 0x002266E0 File Offset: 0x002248E0
		public static void RemoveDefaultValue(string propertyName)
		{
			Dictionary<string, object> dictionary = ConfigurationPropertyService.defaultValues;
			lock (dictionary)
			{
				ConfigurationPropertyService.defaultValues.Remove(propertyName);
			}
		}

		// Token: 0x04005687 RID: 22151
		private static readonly Dictionary<string, object> defaultValues = new Dictionary<string, object>();

		// Token: 0x04005688 RID: 22152
		private readonly IDictionary<string, object> values;
	}
}
