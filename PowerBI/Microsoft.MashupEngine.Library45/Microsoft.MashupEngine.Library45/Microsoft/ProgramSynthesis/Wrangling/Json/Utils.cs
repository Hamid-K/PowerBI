using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json
{
	// Token: 0x0200018A RID: 394
	public static class Utils
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001A31C File Offset: 0x0001851C
		public static IEnumerable<JToken> SelectTokens(this JToken token, JPath path)
		{
			JToken[] array = new JToken[] { token };
			JPathStep[] steps = path.Steps;
			for (int i = 0; i < steps.Length; i++)
			{
				JPathStep step = steps[i];
				array = array.SelectMany(delegate(JToken t)
				{
					if (t != null)
					{
						return step.Apply(t);
					}
					return Enumerable.Empty<JToken>();
				}).ToArray<JToken>();
				if (array.Length == 0)
				{
					return Enumerable.Empty<JToken>();
				}
			}
			return array;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001A37C File Offset: 0x0001857C
		public static JToken SelectFirstToken(this JToken token, JPath path)
		{
			JToken jtoken = token;
			JPathStep[] steps = path.Steps;
			for (int i = 0; i < steps.Length; i++)
			{
				jtoken = steps[i].Apply(jtoken).FirstOrDefault((JToken t) => t != null);
				if (jtoken == null)
				{
					return null;
				}
			}
			return jtoken;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001A3D4 File Offset: 0x000185D4
		public static JProperty SelectFirstProperty(this JToken token, JPath path)
		{
			JToken jtoken = token.SelectFirstToken(path);
			return ((jtoken != null) ? jtoken.Parent : null) as JProperty;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public static IEnumerable<JPath> GetAllPathsTo(this JToken src, JToken dest)
		{
			if (src == dest)
			{
				return new JPath[]
				{
					new JPath()
				};
			}
			JToken[] array = src.AncestorsAndSelf().ToArray<JToken>();
			JToken[] array2 = dest.AncestorsAndSelf().ToArray<JToken>();
			JToken jtoken = null;
			JToken[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				JToken sAnc = array3[i];
				if (array2.Any((JToken dAnc) => sAnc == dAnc))
				{
					jtoken = sAnc;
				}
				if (jtoken != null)
				{
					break;
				}
			}
			if (jtoken == null)
			{
				return null;
			}
			List<JPathStep> srcToCommonAncestorSteps = new List<JPathStep>();
			JToken jtoken2 = src;
			while (jtoken2 != null && jtoken2 != jtoken)
			{
				srcToCommonAncestorSteps.Add(new ParentStep());
				jtoken2 = jtoken2.Parent;
			}
			return (from p in Utils.PathToDescendant(jtoken, dest)
				select new JPath(srcToCommonAncestorSteps.Concat(p))).ToArray<JPath>();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public static IEnumerable<JToken> DescendantsAndSelf(this JToken token)
		{
			JValue jvalue = token as JValue;
			if (jvalue != null)
			{
				return new JValue[] { jvalue };
			}
			JContainer jcontainer = token as JContainer;
			if (jcontainer == null)
			{
				throw new InvalidOperationException("Token must be either a value or a container!");
			}
			return jcontainer.DescendantsAndSelf();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001A507 File Offset: 0x00018707
		public static bool IsNullOrNullType(this JToken token)
		{
			return token == null || token.Type == JTokenType.Null;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001A518 File Offset: 0x00018718
		private static IEnumerable<IEnumerable<JPathStep>> PathToDescendant(JToken src, JToken dest)
		{
			List<List<JPathStep>> list = new List<List<JPathStep>>
			{
				new List<JPathStep>()
			};
			if (src == dest)
			{
				return list;
			}
			List<JToken> list2 = new List<JToken>();
			for (JToken jtoken = dest; jtoken != null; jtoken = jtoken.Parent)
			{
				if (jtoken.Type != JTokenType.Property)
				{
					list2.Add(jtoken);
					if (jtoken == src)
					{
						break;
					}
				}
			}
			int i = list2.Count - 1;
			while (i > 0)
			{
				JToken jtoken2 = list2[i];
				JToken jtoken3 = list2[i - 1];
				JArray jarray = jtoken2 as JArray;
				JObject jobject;
				if (jarray == null)
				{
					jobject = jtoken2 as JObject;
					if (jobject != null)
					{
						goto IL_00D3;
					}
				}
				else
				{
					int num = jarray.IndexOf(jtoken3);
					if (num == -1)
					{
						return new List<List<JPathStep>>();
					}
					IndexStep indexStep = new IndexStep(num);
					using (List<List<JPathStep>>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							List<JPathStep> list3 = enumerator.Current;
							list3.Add(indexStep);
						}
						goto IL_0181;
					}
					goto IL_00D3;
				}
				IL_0181:
				i--;
				continue;
				IL_00D3:
				JProperty jproperty = jtoken3.Parent as JProperty;
				if (jproperty == null || jproperty.Parent != jtoken2)
				{
					return new List<List<JPathStep>>();
				}
				AccessStep accessStep = new AccessStep(jproperty.Name);
				List<List<JPathStep>> list4 = new List<List<JPathStep>>(list.Count);
				if (jobject.Count == 1)
				{
					SinglePropertyStep singletonStep = new SinglePropertyStep();
					list4.AddRange(list.Select((List<JPathStep> steps) => new List<JPathStep>(steps) { singletonStep }));
				}
				foreach (List<JPathStep> list5 in list)
				{
					list5.Add(accessStep);
				}
				list.AddRange(list4);
				goto IL_0181;
			}
			return list;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001A6D0 File Offset: 0x000188D0
		public static JToken Sample(this JToken token, int arrayElementLimit = 2, int valueInObjectLimit = 10)
		{
			JArray jarray = token as JArray;
			if (jarray == null)
			{
				JObject jobject = token as JObject;
				if (jobject == null)
				{
					return token;
				}
				int num = 0;
				JObject jobject2 = new JObject();
				foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
				{
					if (keyValuePair.Value is JValue)
					{
						num++;
						if (num <= valueInObjectLimit)
						{
							jobject2.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					else
					{
						jobject2.Add(keyValuePair.Key, keyValuePair.Value.Sample(arrayElementLimit, valueInObjectLimit));
					}
				}
				return jobject2;
			}
			else
			{
				if (jarray.Count < arrayElementLimit)
				{
					return jarray;
				}
				return new JArray(from e in jarray.Take(arrayElementLimit)
					select e.Sample(arrayElementLimit, valueInObjectLimit));
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001A7DC File Offset: 0x000189DC
		public static bool TryParse(string document, out ParsedJson parsedJson)
		{
			JsonRegion jsonRegion = Utils.ParseImpl(document);
			if (jsonRegion != null)
			{
				parsedJson = new ParsedJson(jsonRegion, null, null, Utils.FindErrors(document));
				return true;
			}
			jsonRegion = Utils.RepairAndParseTruncatedJson(document);
			if (jsonRegion != null)
			{
				JsonErrors jsonErrors = Utils.FindErrors(jsonRegion.Value);
				jsonErrors |= JsonErrors.Truncated;
				parsedJson = new ParsedJson(jsonRegion, null, null, jsonErrors);
				return true;
			}
			return Utils.TryParseDelimitedJson(document, out parsedJson);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001A844 File Offset: 0x00018A44
		private static JsonErrors FindErrors(string document)
		{
			JsonErrors jsonErrors = JsonErrors.None;
			if (Utils.TrailingCommaRegex.IsMatch(document))
			{
				jsonErrors |= JsonErrors.TrailingCommas;
			}
			Stack<JsonToken> stack = new Stack<JsonToken>();
			try
			{
				using (JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(document)))
				{
					while (jsonTextReader.Read())
					{
						JsonToken tokenType = jsonTextReader.TokenType;
						switch (tokenType)
						{
						case JsonToken.StartObject:
						case JsonToken.StartArray:
							stack.Push(jsonTextReader.TokenType);
							break;
						case JsonToken.StartConstructor:
							break;
						case JsonToken.PropertyName:
							if (jsonTextReader.QuoteChar == '\0')
							{
								jsonErrors |= JsonErrors.EmptyQuote;
							}
							break;
						case JsonToken.Comment:
							jsonErrors |= JsonErrors.HasComment;
							break;
						default:
							if (tokenType != JsonToken.EndObject)
							{
								if (tokenType == JsonToken.EndArray)
								{
									if (stack.Count == 0 || stack.Pop() != JsonToken.StartArray)
									{
										jsonErrors |= JsonErrors.MismatchedBrackets;
									}
								}
							}
							else if (stack.Count == 0 || stack.Pop() != JsonToken.StartObject)
							{
								jsonErrors |= JsonErrors.MismatchedBrackets;
							}
							break;
						}
					}
				}
			}
			catch (Exception)
			{
				return JsonErrors.All;
			}
			if (stack.Count > 0)
			{
				jsonErrors |= JsonErrors.MismatchedBrackets;
			}
			return jsonErrors;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001A944 File Offset: 0x00018B44
		public static JsonRegion Parse(string document)
		{
			return Utils.ParseImpl(document) ?? Utils.RepairAndParseTruncatedJson(document);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001A958 File Offset: 0x00018B58
		private static JsonRegion RepairAndParseTruncatedJson(string document)
		{
			Stack<int> stack = new Stack<int>();
			bool flag = false;
			int num = 0;
			using (StringReader stringReader = new StringReader(document))
			{
				int num2 = -1;
				int num3;
				while ((num3 = stringReader.Read()) != -1)
				{
					num2++;
					if (num3 <= 44)
					{
						if (num3 != 34)
						{
							if (num3 == 44)
							{
								if (!flag)
								{
									num = num2 - 1;
								}
							}
						}
						else
						{
							flag = !flag;
						}
					}
					else
					{
						switch (num3)
						{
						case 91:
							if (!flag)
							{
								num = num2;
								stack.Push(93);
								continue;
							}
							continue;
						case 92:
							stringReader.Read();
							num2++;
							continue;
						case 93:
							break;
						default:
							if (num3 != 123)
							{
								if (num3 != 125)
								{
									continue;
								}
							}
							else
							{
								if (!flag)
								{
									num = num2;
									stack.Push(125);
									continue;
								}
								continue;
							}
							break;
						}
						if (!flag)
						{
							num = num2;
							if (stack.IsEmpty<int>())
							{
								return null;
							}
							stack.Pop();
						}
					}
				}
			}
			if (stack.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (stack.Count > 0)
			{
				stringBuilder.Append((char)stack.Pop());
			}
			int i = document.Length - 1;
			while (i >= 0)
			{
				char c = document[i];
				if (!char.IsWhiteSpace(c))
				{
					if (c == ':')
					{
						stringBuilder.Insert(0, "null ");
						break;
					}
					break;
				}
				else
				{
					i--;
				}
			}
			string text = document;
			StringBuilder stringBuilder2 = stringBuilder;
			JsonRegion jsonRegion = Utils.ParseImpl(text + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null));
			if (jsonRegion != null)
			{
				return jsonRegion;
			}
			document = document.Substring(0, num + 1);
			string text2 = document;
			StringBuilder stringBuilder3 = stringBuilder;
			return Utils.ParseImpl(text2 + ((stringBuilder3 != null) ? stringBuilder3.ToString() : null));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001AB04 File Offset: 0x00018D04
		private static JsonRegion ParseImpl(string document)
		{
			JsonRegion jsonRegion;
			try
			{
				if (string.IsNullOrWhiteSpace(document))
				{
					jsonRegion = JsonRegion.Create(new JValue(null));
				}
				else
				{
					JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
					{
						DateParseHandling = DateParseHandling.None,
						MissingMemberHandling = MissingMemberHandling.Error
					};
					jsonRegion = JsonRegion.Create(JsonConvert.DeserializeObject<JToken>(document, jsonSerializerSettings));
				}
			}
			catch (Exception)
			{
				jsonRegion = null;
			}
			return jsonRegion;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001AB60 File Offset: 0x00018D60
		private static bool TryParseDelimitedJson(string document, out ParsedJson parsedJson)
		{
			string text = null;
			string text2 = null;
			parsedJson = null;
			List<JsonRegion> list = new List<JsonRegion>();
			JsonErrors jsonErrors = JsonErrors.None;
			using (StringReader stringReader = new StringReader(document))
			{
				bool flag = false;
				int num = 0;
				int num2 = 0;
				string text3;
				while ((text3 = stringReader.ReadLine()) != null)
				{
					if (!string.IsNullOrWhiteSpace(text3))
					{
						JsonRegion jsonRegion;
						if (!flag)
						{
							jsonRegion = Utils.ParseImpl(text3);
							if (jsonRegion == null)
							{
								int num3 = text3.Length - 1;
								while (num3 >= 0 && text3[num3] != '}' && text3[num3] != ']')
								{
									num3--;
								}
								if (num3 < 0)
								{
									return false;
								}
								num = 0;
								while (num < text3.Length && text3[num] != '{' && text3[num] != '[')
								{
									num++;
								}
								if (num == text3.Length || num3 <= num)
								{
									return false;
								}
								text = text3.Substring(0, num);
								if (string.IsNullOrWhiteSpace(text))
								{
									text = null;
								}
								text2 = text3.Substring(num3 + 1);
								if (string.IsNullOrWhiteSpace(text2))
								{
									text2 = null;
								}
								num2 = text3.Length - num3 - 1;
							}
							if (num > 0 || num2 > 0)
							{
								jsonErrors |= JsonErrors.ExtraCharacters;
							}
							flag = true;
						}
						if (text != null && !text3.StartsWith(text))
						{
							return false;
						}
						if (text2 != null && !text3.EndsWith(text2))
						{
							return false;
						}
						int num4 = text3.Length - num - num2;
						if (num4 < 0)
						{
							return false;
						}
						string text4 = text3.Substring(num, num4);
						jsonErrors |= Utils.FindErrors(text4);
						jsonRegion = Utils.ParseImpl(text4);
						if (jsonRegion != null)
						{
							list.Add(jsonRegion);
						}
						else
						{
							if (!string.IsNullOrWhiteSpace(stringReader.ReadLine()))
							{
								return false;
							}
							JsonRegion jsonRegion2 = Utils.RepairAndParseTruncatedJson(text3);
							if (jsonRegion2 == null)
							{
								return false;
							}
							list.Add(jsonRegion2);
							jsonErrors |= JsonErrors.Truncated;
						}
					}
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			if (list.Count != 1)
			{
				parsedJson = new ParsedJson(list, text, text2, jsonErrors);
				return true;
			}
			if (text == null && text2 == null)
			{
				return false;
			}
			parsedJson = new ParsedJson(list.Single<JsonRegion>(), text, text2, jsonErrors);
			return true;
		}

		// Token: 0x0400044B RID: 1099
		private static readonly Regex TrailingCommaRegex = new Regex(",\\s*(\\}|\\])", RegexOptions.Compiled);
	}
}
