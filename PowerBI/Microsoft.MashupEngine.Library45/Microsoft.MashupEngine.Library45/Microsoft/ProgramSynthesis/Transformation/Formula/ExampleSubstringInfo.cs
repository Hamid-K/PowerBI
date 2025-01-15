using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014E2 RID: 5346
	public class ExampleSubstringInfo
	{
		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x0600A3CC RID: 41932 RVA: 0x0022AB30 File Offset: 0x00228D30
		// (set) Token: 0x0600A3CD RID: 41933 RVA: 0x0022AB38 File Offset: 0x00228D38
		public Example Example { get; set; }

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x0600A3CE RID: 41934 RVA: 0x0022AB41 File Offset: 0x00228D41
		// (set) Token: 0x0600A3CF RID: 41935 RVA: 0x0022AB49 File Offset: 0x00228D49
		public int ExampleId { get; set; }

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x0600A3D0 RID: 41936 RVA: 0x0022AB52 File Offset: 0x00228D52
		// (set) Token: 0x0600A3D1 RID: 41937 RVA: 0x0022AB5A File Offset: 0x00228D5A
		public IReadOnlyCollection<ExampleSubstring> Tokens { get; set; }

		// Token: 0x0600A3D2 RID: 41938 RVA: 0x0022AB64 File Offset: 0x00228D64
		public string Render()
		{
			if (this._toString != null)
			{
				return this._toString;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<ExampleInputToken> list = (from p in this.Tokens.Where(delegate(ExampleSubstring p)
				{
					ExampleInputToken input = p.Input;
					return ((input != null) ? input.ColumnName : null) != null;
				})
				select p.Input).ToList<ExampleInputToken>();
			foreach (IGrouping<string, ExampleInputToken> grouping in list.Where((ExampleInputToken p) => p.ColumnName != null).ToLookup((ExampleInputToken t) => t.ColumnName, (ExampleInputToken t) => t))
			{
				string columnName = grouping.Key;
				object obj;
				if (this.Example.Input.TryGetValue(columnName, out obj))
				{
					string text = obj as string;
					if (text != null)
					{
						dictionary[columnName] = ExampleSubstringInfo.Render(text, list.Where((ExampleInputToken t) => t.ColumnName == columnName).ToList<ExampleInputToken>());
					}
				}
			}
			string text2 = null;
			string text3 = this.Example.Output as string;
			if (text3 != null)
			{
				List<ExampleToken> list2 = this.Tokens.Select((ExampleSubstring p) => p.Output).Collect<ExampleToken>().ToList<ExampleToken>();
				text2 = ExampleSubstringInfo.Render(text3, list2);
			}
			string text4 = JsonConvertUtils.SerializeObject(dictionary);
			string text5 = ((text4.Length > 100) ? (Environment.NewLine + "    ") : string.Empty);
			return this._toString = text4 + text5 + " -> " + text2.ToLiteral(null);
		}

		// Token: 0x0600A3D3 RID: 41939 RVA: 0x0022AD84 File Offset: 0x00228F84
		public object ToAnonymizedJson()
		{
			return this.ToJsonInternal(true);
		}

		// Token: 0x0600A3D4 RID: 41940 RVA: 0x0022AD90 File Offset: 0x00228F90
		public string ToAnonymizedString()
		{
			string text;
			if ((text = this._toAnonymizedString) == null)
			{
				text = (this._toAnonymizedString = JsonConvertUtils.SerializeObject(this.ToAnonymizedJson()));
			}
			return text;
		}

		// Token: 0x0600A3D5 RID: 41941 RVA: 0x0022ADBB File Offset: 0x00228FBB
		public object ToJson()
		{
			return this.ToJsonInternal(false);
		}

		// Token: 0x0600A3D6 RID: 41942 RVA: 0x0022ADC4 File Offset: 0x00228FC4
		public override string ToString()
		{
			return this.Render();
		}

		// Token: 0x0600A3D7 RID: 41943 RVA: 0x0022ADCC File Offset: 0x00228FCC
		private static string Render(string source, IReadOnlyCollection<ExampleToken> tokens)
		{
			char c = '[';
			char c2 = ']';
			if (source.Contains('[') || source.Contains('['))
			{
				c = '{';
				c2 = '}';
			}
			var list = (from token in tokens
				where !token.IsConstant
				select new
				{
					Id = token.Id,
					Position = token.Start
				}).ToList();
			List<int?> list2 = (from token in tokens
				where !token.IsConstant
				select token.End).ToList<int?>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (var <7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType in list.Where(i => i.Position == null))
			{
				stringBuilder.Append(string.Format("{0}{1}:", c, (<7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType != null) ? <7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType.Id : null));
			}
			int position = 1;
			Func<int?, bool> <>9__6;
			Func<<7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType5<string, int?>, bool> <>9__7;
			foreach (char c3 in source)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				char c4 = c2;
				IEnumerable<int?> enumerable = list2;
				Func<int?, bool> func;
				if ((func = <>9__6) == null)
				{
					func = (<>9__6 = delegate(int? e)
					{
						int? num = e;
						int position2 = position;
						return (num.GetValueOrDefault() == position2) & (num != null);
					});
				}
				stringBuilder2.Append(new string(c4, enumerable.Count(func)));
				var enumerable2 = list;
				var func2;
				if ((func2 = <>9__7) == null)
				{
					func2 = (<>9__7 = delegate(i)
					{
						int? position3 = i.Position;
						int position4 = position;
						return (position3.GetValueOrDefault() == position4) & (position3 != null);
					});
				}
				foreach (var <7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType2 in enumerable2.Where(func2))
				{
					stringBuilder.Append(string.Format("{0}{1}:", c, <7c4b26e8-2c83-404d-97a5-85ee9fdc4e01><>f__AnonymousType2.Id));
				}
				stringBuilder.Append(c3);
				int position5 = position;
				position = position5 + 1;
			}
			stringBuilder.Append(new string(c2, list2.Count((int? e) => e == null)));
			return stringBuilder.ToString();
		}

		// Token: 0x0600A3D8 RID: 41944 RVA: 0x0022B064 File Offset: 0x00229264
		private JObject ToJsonInternal(bool anonymized)
		{
			List<ExampleInputToken> list = (from p in this.Tokens.Where(delegate(ExampleSubstring p)
				{
					ExampleInputToken input = p.Input;
					return ((input != null) ? input.ColumnName : null) != null;
				})
				select p.Input).ToList<ExampleInputToken>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			IEnumerable<IGrouping<string, ExampleInputToken>> enumerable = list.ToLookup((ExampleInputToken t) => t.ColumnName, (ExampleInputToken t) => t);
			int num = 1;
			foreach (IGrouping<string, ExampleInputToken> grouping in enumerable)
			{
				string columnName = grouping.Key;
				string text = columnName;
				object obj;
				if (this.Example.Input.TryGetValue(columnName, out obj))
				{
					string text2 = obj as string;
					if (text2 != null)
					{
						if (anonymized)
						{
							text = string.Format("c{0}", num++);
							text2 = text2.ToAnonymizedString();
						}
						dictionary[text] = ExampleSubstringInfo.Render(text2, list.Where((ExampleInputToken t) => t.ColumnName == columnName).ToList<ExampleInputToken>());
					}
				}
			}
			string text3 = null;
			string text4 = this.Example.Output as string;
			if (text4 != null)
			{
				if (anonymized)
				{
					text4 = text4.ToAnonymizedString();
				}
				List<ExampleToken> list2 = this.Tokens.Select((ExampleSubstring p) => p.Output).Collect<ExampleToken>().ToList<ExampleToken>();
				text3 = ExampleSubstringInfo.Render(text4, list2);
			}
			IEnumerable<JObject> enumerable2 = this.Tokens.Select(delegate(ExampleSubstring t)
			{
				if (!anonymized)
				{
					return t.ToJson();
				}
				return t.ToAnonymizedJson();
			});
			return JObject.FromObject(new
			{
				ExampleId = this.ExampleId,
				Tokens = enumerable2,
				Input = dictionary,
				Output = text3
			});
		}

		// Token: 0x0400427C RID: 17020
		private string _toAnonymizedString;

		// Token: 0x0400427D RID: 17021
		private string _toString;
	}
}
