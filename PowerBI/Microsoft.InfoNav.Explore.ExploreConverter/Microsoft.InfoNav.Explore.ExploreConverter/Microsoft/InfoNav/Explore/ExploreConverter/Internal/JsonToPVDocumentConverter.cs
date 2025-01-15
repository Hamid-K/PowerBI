using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000F RID: 15
	internal static class JsonToPVDocumentConverter
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000027E8 File Offset: 0x000009E8
		internal static PVDocumentRoot ParseFromJson(Stream stream)
		{
			JToken jtoken = JsonToPVDocumentConverter.ParseFromStream(stream);
			if (jtoken == null)
			{
				return null;
			}
			JToken jtoken2 = jtoken["microsoft.powerbi-rev1.Document"];
			JToken jtoken3 = jtoken2["RootVisual"];
			return new PVDocumentRoot
			{
				Document = new PVDocument
				{
					RootVisual = JsonToPVDocumentConverter.ParseVisual(jtoken3),
					Context = JsonToPVDocumentConverter.ParseDocumentContext(jtoken2["Context"])
				}
			};
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000284C File Offset: 0x00000A4C
		internal static JToken ParseFromStream(Stream stream)
		{
			if (stream == null)
			{
				return null;
			}
			string text;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				text = streamReader.ReadToEnd();
			}
			JToken jtoken;
			try
			{
				jtoken = JToken.Parse(text);
			}
			catch (JsonReaderException)
			{
				jtoken = JToken.Parse(JsonToPVDocumentConverter.FixInvalidArray(text));
			}
			return jtoken;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028B0 File Offset: 0x00000AB0
		internal static string FixInvalidArray(string jsonString)
		{
			MatchCollection matchCollection = Regex.Matches(jsonString, ",\"Params\":\\[(?<args>(\"[^\"]+\"){2,})+\\]");
			for (int i = matchCollection.Count - 1; i >= 0; i--)
			{
				Match match = matchCollection[i];
				if (match.Success)
				{
					Group group = match.Groups["args"];
					if (group.Success)
					{
						for (int j = group.Captures.Count - 1; j >= 0; j--)
						{
							Capture capture = group.Captures[j];
							jsonString = jsonString.Remove(capture.Index, capture.Length);
						}
					}
				}
			}
			return jsonString;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002947 File Offset: 0x00000B47
		internal static PVDocumentContext ParseDocumentContext(JToken documentContext)
		{
			if (documentContext == null)
			{
				return null;
			}
			return new PVDocumentContext
			{
				ImageResourceMap = JsonToPVDocumentConverter.ParseResourceMap(documentContext["ImageResourceMap"] as JArray)
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002970 File Offset: 0x00000B70
		internal static List<PVResourceEntry> ParseResourceMap(JArray imageResourceMap)
		{
			if (imageResourceMap == null)
			{
				return null;
			}
			List<PVResourceEntry> list = new List<PVResourceEntry>(imageResourceMap.Count);
			for (int i = 0; i < imageResourceMap.Count; i++)
			{
				PVResourceEntry pvresourceEntry = JsonToPVDocumentConverter.ParsePVResourceEntry(imageResourceMap[i]);
				if (pvresourceEntry != null)
				{
					list.Add(pvresourceEntry);
				}
			}
			return list;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029B7 File Offset: 0x00000BB7
		internal static PVResourceEntry ParsePVResourceEntry(JToken resourceEntry)
		{
			if (resourceEntry == null)
			{
				return null;
			}
			return new PVResourceEntry
			{
				ImageBytes = JTokenUtils.ParseString(resourceEntry["ImageBytes"]),
				ResourceId = JTokenUtils.ParseString(resourceEntry["ResourceId"])
			};
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029F0 File Offset: 0x00000BF0
		internal static PVFrame ParseFrame(JToken frame)
		{
			if (frame != null)
			{
				JToken jtoken = frame["Position"];
				decimal num = JTokenUtils.ParseDecimal((jtoken != null) ? jtoken["X"] : null, 0m);
				decimal num2 = JTokenUtils.ParseDecimal((jtoken != null) ? jtoken["Y"] : null, 0m);
				decimal num3 = JTokenUtils.ParseDecimal(frame["Width"], 0m);
				decimal num4 = JTokenUtils.ParseDecimal(frame["Height"], 0m);
				return new PVFrame
				{
					Position = new PVPosition
					{
						X = num,
						Y = num2
					},
					Width = num3,
					Height = num4
				};
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AA4 File Offset: 0x00000CA4
		internal static PVTextStyle ParseTextStyle(JToken textStyle)
		{
			if (textStyle == null)
			{
				return null;
			}
			return new PVTextStyle
			{
				FontFamily = JTokenUtils.ParseString(textStyle["FontFamily"]),
				FontSize = JTokenUtils.ParseString(textStyle["FontSize"]),
				FontStyle = JTokenUtils.ParseString(textStyle["FontStyle"]),
				FontWeight = JTokenUtils.ParseString(textStyle["FontWeight"]),
				TextDecoration = JTokenUtils.ParseString(textStyle["TextDecoration"])
			};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B2C File Offset: 0x00000D2C
		internal static List<PVTextRun> ParseTextRuns(JArray textRuns)
		{
			if (textRuns == null)
			{
				return null;
			}
			List<PVTextRun> list = new List<PVTextRun>(textRuns.Count);
			int i = 0;
			int count = textRuns.Count;
			while (i < count)
			{
				JToken jtoken = textRuns[i];
				list.Add(new PVTextRun
				{
					Value = JTokenUtils.ParseString(jtoken["Value"]),
					TextStyle = JsonToPVDocumentConverter.ParseTextStyle(jtoken["TextStyle"])
				});
				i++;
			}
			return list;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal static List<PVParagraph> ParseParagraphs(JArray paragraphs)
		{
			if (paragraphs == null)
			{
				return null;
			}
			List<PVParagraph> list = new List<PVParagraph>(paragraphs.Count);
			int i = 0;
			int count = paragraphs.Count;
			while (i < count)
			{
				JToken jtoken = paragraphs[i];
				list.Add(new PVParagraph
				{
					HorizontalTextAlignment = JTokenUtils.ParseString(jtoken["HorizontalTextAlignment"]),
					TextRuns = JsonToPVDocumentConverter.ParseTextRuns(jtoken["TextRuns"] as JArray)
				});
				i++;
			}
			return list;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C18 File Offset: 0x00000E18
		internal static List<PVColumnInfo> ParseColumns(JArray columns)
		{
			if (columns == null)
			{
				return null;
			}
			List<PVColumnInfo> list = new List<PVColumnInfo>(columns.Count);
			foreach (JToken jtoken in columns)
			{
				list.Add(new PVColumnInfo
				{
					CustomFormatString = JTokenUtils.ParseString(jtoken["CustomFormatString"])
				});
			}
			return list;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C8C File Offset: 0x00000E8C
		internal static LayoutContext ParseLayoutContext(JToken layoutContextElement)
		{
			if (layoutContextElement == null)
			{
				return null;
			}
			return new LayoutContext
			{
				ChartLayoutType = JTokenUtils.ParseString(layoutContextElement["ChartLayoutType"]),
				Type = JTokenUtils.ParseString(layoutContextElement["Type"]),
				VerticalAlignment = JTokenUtils.ParseString(layoutContextElement["VerticalAlignment"]),
				Paragraphs = JsonToPVDocumentConverter.ParseParagraphs(layoutContextElement["Paragraphs"] as JArray),
				Theme = JTokenUtils.ParseString(layoutContextElement["Theme"]),
				ResourceId = JTokenUtils.ParseString(layoutContextElement["ResourceId"]),
				CardStyles = JTokenUtils.ParseString(layoutContextElement["CardStyles"]),
				IsLegendHidden = JTokenUtils.ParseBoolNullable(layoutContextElement["IsLegendHidden"]),
				IsChartTitleHidden = JTokenUtils.ParseBoolNullable(layoutContextElement["IsChartTitleHidden"]),
				AreLabelsVisible = JTokenUtils.ParseBoolNullable(layoutContextElement["AreLabelsVisible"]),
				LegendPosition = JTokenUtils.ParseString(layoutContextElement["LegendPosition"]),
				LabelsPosition = JTokenUtils.ParseString(layoutContextElement["LabelsPosition"]),
				Columns = JsonToPVDocumentConverter.ParseColumns(layoutContextElement["Columns"] as JArray)
			};
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DCC File Offset: 0x00000FCC
		internal static Formula ParseFormula(JToken formulaElement)
		{
			if (formulaElement != null)
			{
				JArray jarray = formulaElement["QualifiedName"] as JArray;
				Formula formula = new Formula();
				if (jarray != null)
				{
					formula.QualifiedName = new List<string>(jarray.Count);
					foreach (JToken jtoken in jarray)
					{
						formula.QualifiedName.Add(jtoken.Value<string>());
					}
				}
				JToken jtoken2 = formulaElement["Function"];
				if (jtoken2 != null)
				{
					formula.Function = jtoken2.Value<string>();
				}
				JArray jarray2 = formulaElement["Arguments"] as JArray;
				if (jarray2 != null)
				{
					formula.Arguments = new List<Formula>(jarray2.Count);
					foreach (JToken jtoken3 in jarray2)
					{
						formula.Arguments.Add(JsonToPVDocumentConverter.ParseFormula(jtoken3));
					}
				}
				return formula;
			}
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002EE4 File Offset: 0x000010E4
		internal static BucketProperty ParseBucketProperty(JToken bucketPropertyElement)
		{
			return new BucketProperty
			{
				Name = JTokenUtils.ParseString(bucketPropertyElement["Name"]),
				Value = JTokenUtils.ParseBool(bucketPropertyElement["Value"], false)
			};
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F18 File Offset: 0x00001118
		internal static Bucket ParseBucket(JToken bucketElement)
		{
			string text = JTokenUtils.ParseString(bucketElement["Name"]);
			JArray jarray = bucketElement["BucketItems"] as JArray;
			Bucket bucket = new Bucket
			{
				Name = text
			};
			if (jarray != null)
			{
				bucket.BucketItems = new List<BucketItem>(jarray.Count);
				foreach (JToken jtoken in jarray)
				{
					BucketItem bucketItem = new BucketItem
					{
						Formula = JsonToPVDocumentConverter.ParseFormula(jtoken["Formula"]),
						ShowItemsWithNoData = JTokenUtils.ParseBoolNullable(jtoken["ShowItemsWithNoData"])
					};
					JsonToPVDocumentConverter.ParseBucketItemProperties(bucketItem, jtoken["Properties"] as JArray);
					bucket.BucketItems.Add(bucketItem);
				}
			}
			JArray jarray2 = bucketElement["Properties"] as JArray;
			if (jarray2 != null)
			{
				bucket.Properties = new List<BucketProperty>(jarray2.Count);
				for (int i = 0; i < jarray2.Count; i++)
				{
					bucket.Properties.Add(JsonToPVDocumentConverter.ParseBucketProperty(jarray2[i]));
				}
			}
			return bucket;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003054 File Offset: 0x00001254
		private static void ParseBucketItemProperties(BucketItem bucketItem, JArray properties)
		{
			if (properties.IsNullOrEmptyCollection<JToken>())
			{
				return;
			}
			for (int i = 0; i < properties.Count; i++)
			{
				JToken jtoken = properties[i];
				if (jtoken != null)
				{
					string text = JTokenUtils.ParseString(jtoken["Name"]);
					string text2 = JTokenUtils.ParseString(jtoken["Value"]);
					if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && text == "DrillingState")
					{
						if (!(text2 == "Invisible"))
						{
							if (text2 == "Visible")
							{
								bucketItem.IsDrilledItem = new bool?(true);
							}
						}
						else
						{
							bucketItem.IsDrilledItem = new bool?(false);
						}
					}
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003104 File Offset: 0x00001304
		internal static DataContext ParseDataContext(JToken dataContextElement)
		{
			if (dataContextElement != null)
			{
				DataContext dataContext = new DataContext
				{
					Type = JTokenUtils.ParseString(dataContextElement["Type"])
				};
				JToken jtoken = dataContextElement["Buckets"];
				if (jtoken != null)
				{
					dataContext.Buckets = new List<Bucket>();
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
					{
						Bucket bucket = JsonToPVDocumentConverter.ParseBucket(jtoken2);
						dataContext.Buckets.Add(bucket);
					}
				}
				JToken jtoken3 = dataContextElement["Formula"];
				if (jtoken3 != null)
				{
					dataContext.Formula = JsonToPVDocumentConverter.ParseFormula(jtoken3);
				}
				return dataContext;
			}
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000031B4 File Offset: 0x000013B4
		internal static FilterValue ParseFilterValue(JToken propertyJson)
		{
			FilterValue filterValue = null;
			if (propertyJson != null)
			{
				filterValue = new FilterValue();
				JToken jtoken = propertyJson["Value"];
				if (jtoken == null)
				{
					filterValue.Type = FilterValueType.Null;
				}
				else
				{
					string text = JTokenUtils.ParseString(jtoken);
					bool flag;
					decimal num;
					if (text == null)
					{
						filterValue.Type = FilterValueType.Null;
					}
					else if (bool.TryParse(text, out flag))
					{
						filterValue.Type = FilterValueType.Bool;
						filterValue.BoolValue = flag;
					}
					else if (decimal.TryParse(text, out num))
					{
						filterValue.Type = FilterValueType.Number;
						filterValue.NumberValue = num;
					}
					else
					{
						filterValue.Type = FilterValueType.String;
						filterValue.StringValue = text;
						if (!string.IsNullOrEmpty(text))
						{
							DateTime dateTime;
							if (text[0] == '\'')
							{
								text = text.Substring(1, text.Length - 2).Replace("''", "'");
								filterValue.Type = FilterValueType.String;
								filterValue.StringValue = text;
							}
							else if (text[text.Length - 1] == 'M' || text[text.Length - 1] == 'D' || text[text.Length - 1] == 'L')
							{
								filterValue.Type = FilterValueType.NumberODataEncoded;
								filterValue.NumberODataEncodedValue = text;
							}
							else if (JsonToPVDocumentConverter.TryParseODataDateTime(text, out dateTime))
							{
								filterValue.Type = FilterValueType.DateTime;
								filterValue.DateTimeValue = dateTime;
							}
						}
					}
				}
			}
			return filterValue;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000032F0 File Offset: 0x000014F0
		internal static bool TryParseODataDateTime(string value, out DateTime dateTimeValue)
		{
			string text = "datetime'";
			if (value.StartsWith(text) && value.EndsWith("'", StringComparison.Ordinal) && DateTime.TryParse(value.Substring(text.Length, value.Length - 1 - text.Length), CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeValue))
			{
				return true;
			}
			dateTimeValue = DateTime.Now;
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003354 File Offset: 0x00001554
		internal static PVFilter ParseFilter(JToken propertyJson)
		{
			if (propertyJson != null)
			{
				PVFilter pvfilter = new PVFilter();
				pvfilter.Type = JTokenUtils.ParseString(propertyJson["Type"]);
				pvfilter.Operator = JTokenUtils.ParseString(propertyJson["Operator"]);
				JToken jtoken = propertyJson["FilterConditions"];
				if (jtoken != null)
				{
					pvfilter.FilterConditions = new List<PVFilter>();
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
					{
						pvfilter.FilterConditions.Add(JsonToPVDocumentConverter.ParseFilter(jtoken2));
					}
				}
				pvfilter.LeftExpression = JsonToPVDocumentConverter.ParseFormula(propertyJson["LeftExpression"]);
				pvfilter.RightExpression = JsonToPVDocumentConverter.ParseFilterValue(propertyJson["RightExpression"]);
				pvfilter.Not = JTokenUtils.ParseBool(propertyJson["Not"], false);
				return pvfilter;
			}
			return null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003440 File Offset: 0x00001640
		internal static CustomPVProperties ParsePropertyValue(JToken propertyJson)
		{
			if (propertyJson == null)
			{
				return null;
			}
			CustomPVProperties customPVProperties = new CustomPVProperties();
			if (propertyJson.Type == JTokenType.Array)
			{
				customPVProperties.SortValue = new List<PVPropertyValue>();
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)propertyJson).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken = enumerator.Current;
						PVPropertyValue pvpropertyValue = new PVPropertyValue();
						pvpropertyValue.Direction = JTokenUtils.ParseString(jtoken["Direction"]);
						pvpropertyValue.Formula = JsonToPVDocumentConverter.ParseFormula(jtoken["Formula"]);
						customPVProperties.SortValue.Add(pvpropertyValue);
					}
					return customPVProperties;
				}
			}
			if (propertyJson.Type == JTokenType.String)
			{
				customPVProperties.StringValue = propertyJson.Value<string>();
			}
			else if (propertyJson.Type == JTokenType.Boolean)
			{
				customPVProperties.BooleanValue = propertyJson.Value<bool>();
			}
			else
			{
				if (propertyJson.Type != JTokenType.Object)
				{
					throw new ArgumentException("Unknown PropertyValue");
				}
				customPVProperties.FilterValue = JsonToPVDocumentConverter.ParseFilter(propertyJson);
			}
			return customPVProperties;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003530 File Offset: 0x00001730
		internal static List<PVProperty> ParseProperties(JToken propertiesJson)
		{
			if (propertiesJson != null)
			{
				List<PVProperty> list = new List<PVProperty>();
				foreach (JToken jtoken in ((IEnumerable<JToken>)propertiesJson))
				{
					PVProperty pvproperty = new PVProperty
					{
						Name = JTokenUtils.ParseString(jtoken["Name"]),
						Value = JsonToPVDocumentConverter.ParsePropertyValue(jtoken["Value"])
					};
					list.Add(pvproperty);
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000035B8 File Offset: 0x000017B8
		internal static PVVisual ParseVisual(JToken visualJson)
		{
			if (visualJson == null)
			{
				return null;
			}
			PVVisual pvvisual = new PVVisual();
			pvvisual.Name = JTokenUtils.ParseString(visualJson["Name"]);
			pvvisual.Type = JTokenUtils.ParseString(visualJson["Type"]);
			int num;
			pvvisual.ZIndex = (int.TryParse(JTokenUtils.ParseString(visualJson["ZIndex"]), out num) ? num : 0);
			pvvisual.Frame = JsonToPVDocumentConverter.ParseFrame(visualJson["Frame"]);
			pvvisual.LayoutContext = JsonToPVDocumentConverter.ParseLayoutContext(visualJson["LayoutContext"]);
			pvvisual.DataContext = JsonToPVDocumentConverter.ParseDataContext(visualJson["DataContext"]);
			JToken jtoken = visualJson["Visuals"];
			if (jtoken != null)
			{
				pvvisual.Visuals = new List<PVVisual>();
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
				{
					pvvisual.Visuals.Add(JsonToPVDocumentConverter.ParseVisual(jtoken2));
				}
			}
			pvvisual.Properties = JsonToPVDocumentConverter.ParseProperties(visualJson["Properties"]);
			return pvvisual;
		}
	}
}
