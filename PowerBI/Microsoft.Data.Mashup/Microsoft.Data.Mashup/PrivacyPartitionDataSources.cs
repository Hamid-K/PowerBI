using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004F RID: 79
	public static class PrivacyPartitionDataSources
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000E2F2 File Offset: 0x0000C4F2
		public static string Create(IDictionary<string, DataSource[]> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverter(new PrivacyPartitionDataSources.DataSourceJsonConverter());
			return javaScriptSerializer.Serialize(dictionary);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E30F File Offset: 0x0000C50F
		public static string Create(IDictionary<MashupPartitionCoordinate, DataSource[]> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}
			return PrivacyPartitionDataSources.Create(dictionary.TransformKeys((MashupPartitionCoordinate c) => c.PartitionKey));
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E340 File Offset: 0x0000C540
		public static IDictionary<string, DataSource[]> ToDictionary(string json)
		{
			if (json == null)
			{
				return null;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverter(new PrivacyPartitionDataSources.DataSourceJsonConverter());
			return javaScriptSerializer.Deserialize<IDictionary<string, DataSource[]>>(json);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E35D File Offset: 0x0000C55D
		public static IDictionary<MashupPartitionCoordinate, DataSource[]> ToCoordinateDictionary(string json)
		{
			IDictionary<string, DataSource[]> dictionary = PrivacyPartitionDataSources.ToDictionary(json);
			if (dictionary == null)
			{
				return null;
			}
			return dictionary.TransformKeys(new Func<string, MashupPartitionCoordinate>(MashupPartitionCoordinate.Create));
		}

		// Token: 0x02000085 RID: 133
		private abstract class JsonConverter<T> : JavaScriptConverter<T>
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x00012A8B File Offset: 0x00010C8B
			protected static void Extract<TValue>(KeyValuePair<string, object> kvp, string key, ref TValue value)
			{
				if (string.Compare(kvp.Key, key, StringComparison.OrdinalIgnoreCase) == 0)
				{
					value = (TValue)((object)kvp.Value);
				}
			}
		}

		// Token: 0x02000086 RID: 134
		private class DataSourceJsonConverter : PrivacyPartitionDataSources.JsonConverter<DataSource>
		{
			// Token: 0x06000504 RID: 1284 RVA: 0x00012AB7 File Offset: 0x00010CB7
			public override IDictionary<string, object> Serialize(DataSource dataSource)
			{
				return new Dictionary<string, object>
				{
					{ "Kind", dataSource.Kind },
					{ "Path", dataSource.Path }
				};
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x00012AE0 File Offset: 0x00010CE0
			public override DataSource Deserialize(IDictionary<string, object> dictionary)
			{
				string text = null;
				string text2 = null;
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					PrivacyPartitionDataSources.JsonConverter<DataSource>.Extract<string>(keyValuePair, "Kind", ref text);
					PrivacyPartitionDataSources.JsonConverter<DataSource>.Extract<string>(keyValuePair, "Path", ref text2);
				}
				return new DataSource(text, text2);
			}
		}
	}
}
