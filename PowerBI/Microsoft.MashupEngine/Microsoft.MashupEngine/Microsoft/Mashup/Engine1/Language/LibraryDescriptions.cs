using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Web;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Help;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001794 RID: 6036
	public static class LibraryDescriptions
	{
		// Token: 0x060098B8 RID: 39096 RVA: 0x001F80F0 File Offset: 0x001F62F0
		public static bool TryGetDocumentation(Value value, out IDocumentation documentation)
		{
			TypeValue typeValue = (value.IsType ? value.AsType : value.Type);
			Value value2 = null;
			Value value3 = null;
			if (value != null)
			{
				typeValue.TryGetMetaField("Documentation.Name", out value2);
				typeValue.TryGetMetaField("Documentation.Description", out value3);
			}
			if ((value2 == null || !value2.IsText) && (value3 == null || !value3.IsText))
			{
				documentation = null;
				return false;
			}
			if (value2 == null || !value2.IsText)
			{
				value2 = value3;
			}
			if (value3 == null || !value3.IsText)
			{
				value3 = value2;
			}
			Value value4;
			string text;
			if (typeValue.TryGetMetaField("Documentation.LongDescription", out value4) && value4.IsText)
			{
				text = value4.AsString;
			}
			else
			{
				text = value3.AsString;
			}
			string text2 = string.Empty;
			if (typeValue.TryGetMetaField("Documentation.Category", out value4) && value4.IsText)
			{
				text2 = value4.AsString;
			}
			string text3 = null;
			if (typeValue.TryGetMetaField("Documentation.Caption", out value4) && value4.IsText)
			{
				text3 = value4.AsString;
			}
			IList<DocumentationExample> list;
			if (typeValue.TryGetMetaField("Documentation.Examples", out value4) && value4.IsList)
			{
				list = new List<DocumentationExample>(value4.AsList.Count);
				for (int i = 0; i < value4.AsList.Count; i++)
				{
					Value value5;
					Value value6;
					Value value7;
					if (value4.AsList[i].IsRecord && value4.AsList[i].AsRecord.TryGetValue("Description", out value5) && value5.IsText && value4.AsList[i].AsRecord.TryGetValue("Code", out value6) && value6.IsText && value4.AsList[i].AsRecord.TryGetValue("Result", out value7) && value7.IsText)
					{
						list.Add(new DocumentationExample(LibraryDescriptions.HtmlSanitizer.Sanitize(value5.AsString), value6.AsString, value7.AsString));
					}
				}
			}
			else
			{
				list = EmptyArray<DocumentationExample>.Instance;
			}
			documentation = new LibraryDescriptions.LibraryDescription(typeValue, value2.AsString, text3, value3.AsString, LibraryDescriptions.HtmlSanitizer.Sanitize(text), text2, list);
			return true;
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x001F8312 File Offset: 0x001F6512
		public static TypeValue NewEnumType(string enumName)
		{
			return TypeValue.NewNumberType().NonNullable.AddHelpMetadata(enumName, null);
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x001F8325 File Offset: 0x001F6525
		public static TypeValue AddHelpMetadata(this TypeValue baseType, string functionName, ResourceManager resourceManager)
		{
			return baseType.NewMeta(new LibraryDescriptions.LibraryDescription(baseType, functionName, resourceManager).AsMetadata()).AsType;
		}

		// Token: 0x060098BB RID: 39099 RVA: 0x001F8340 File Offset: 0x001F6540
		public static TypeValue AddOptionItemMetadata(TypeValue baseType, string optionName, string helpQualifier, bool requiresActions, ResourceManager resourceManager)
		{
			string resourceName = "Option_" + optionName;
			if (helpQualifier != null)
			{
				resourceName = resourceName + "_" + helpQualifier;
			}
			if (resourceManager == null)
			{
				resourceManager = FunctionDescriptionStrings.ResourceManager;
			}
			RecordValue recordValue = RecordValue.New(requiresActions ? LibraryDescriptions.ActionOptionItemKeys : LibraryDescriptions.OptionItemKeys, delegate(int i)
			{
				if (i == 0)
				{
					return TextValue.NewOrNull(resourceManager.GetString(resourceName + "_Description"));
				}
				if (i != 1)
				{
					return LogicalValue.True;
				}
				return TextValue.NewOrNull(resourceManager.GetString(resourceName + "_Caption"));
			});
			return baseType.NewMeta(recordValue).AsType;
		}

		// Token: 0x060098BC RID: 39100 RVA: 0x001F83C8 File Offset: 0x001F65C8
		public static Value AddHelp(this Value value, string functionName, ResourceManager resourceManager = null)
		{
			if (value.IsType && value.MetaValue.Keys.IndexOfKey("Documentation.AllowedValues") == -1)
			{
				return value.AsType.AddHelpMetadata(functionName, resourceManager);
			}
			int num = value.Type.MetaValue.Keys.IndexOfKey("Documentation.Name");
			if (num >= 0 && value.Type.MetaValue[num].IsText && value.Type.MetaValue[num].AsString == functionName)
			{
				return value;
			}
			return value.ReplaceType(value.Type.AddHelpMetadata(functionName, resourceManager));
		}

		// Token: 0x060098BD RID: 39101 RVA: 0x001F846D File Offset: 0x001F666D
		private static string HtmlEncode(string text)
		{
			return HttpUtility.HtmlEncode(text).Replace("'", "&#39;").Replace("\0", "&#0;");
		}

		// Token: 0x040050E9 RID: 20713
		private static readonly Keys DescriptionKeys = Keys.New(new string[] { "Documentation.Name", "Documentation.Description", "Documentation.LongDescription", "Documentation.Category", "Documentation.Examples" });

		// Token: 0x040050EA RID: 20714
		private static readonly Keys OptionItemKeys = Keys.New("Documentation.Description", "Documentation.Caption");

		// Token: 0x040050EB RID: 20715
		private static readonly Keys ActionOptionItemKeys = Keys.New("Documentation.Description", "Documentation.Caption", "Documentation.RequiresActions");

		// Token: 0x040050EC RID: 20716
		private const string FunctionCodeKey = "Code";

		// Token: 0x040050ED RID: 20717
		private const string FunctionDescriptionKey = "Description";

		// Token: 0x040050EE RID: 20718
		private const string FunctionResultKey = "Result";

		// Token: 0x040050EF RID: 20719
		private static readonly Keys ExampleKeys = Keys.New("Description", "Code", "Result");

		// Token: 0x02001795 RID: 6037
		private sealed class Parameter : IFormattable
		{
			// Token: 0x060098BF RID: 39103 RVA: 0x001F851F File Offset: 0x001F671F
			public Parameter(string name, Value type)
			{
				this.name = name;
				this.type = (type.IsType ? type.AsType : TypeValue.Any);
			}

			// Token: 0x060098C0 RID: 39104 RVA: 0x001F8549 File Offset: 0x001F6749
			public override string ToString()
			{
				return "<code>" + LibraryDescriptions.HtmlEncode(this.name) + "</code>";
			}

			// Token: 0x060098C1 RID: 39105 RVA: 0x001F8568 File Offset: 0x001F6768
			public string ToString(string format, IFormatProvider formatProvider)
			{
				if (!(format == "type"))
				{
					return this.ToString();
				}
				if (this.type.IsRecordType)
				{
					return LibraryDescriptions.Parameter.FormatRecordType(this.type.NonNullable.AsRecordType);
				}
				return LibraryDescriptions.Parameter.FormatEnum(this.type);
			}

			// Token: 0x060098C2 RID: 39106 RVA: 0x001F85B8 File Offset: 0x001F67B8
			private static string FormatRecordType(RecordTypeValue type)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("<ul>");
				for (int i = 0; i < type.Fields.Keys.Length; i++)
				{
					Value value;
					if (!type.Fields[i].AsRecord["Type"].MetaValue.TryGetValue("Documentation.RequiresActions", out value) || !value.IsLogical || !value.AsBoolean)
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<li><code>{0}</code>", LibraryDescriptions.HtmlEncode(type.Fields.Keys[i]));
						Value value2;
						if (type.Fields[i].AsRecord["Type"].MetaValue.TryGetValue("Documentation.Description", out value2) && value2.IsText)
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " : {0}", LibraryDescriptions.HtmlEncode(value2.AsString));
						}
						stringBuilder.AppendLine("</li>");
					}
				}
				stringBuilder.AppendLine("</ul>");
				return stringBuilder.ToString();
			}

			// Token: 0x060098C3 RID: 39107 RVA: 0x001F86D0 File Offset: 0x001F68D0
			private static string FormatEnum(TypeValue type)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Value value;
				if (type.MetaValue.TryGetValue("Documentation.AllowedValues", out value) && value.IsList)
				{
					stringBuilder.AppendLine("<ul>");
					using (IEnumerator<IValueReference> enumerator = value.AsList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Value value2;
							if (enumerator.Current.Value.MetaValue.TryGetValue("Documentation.Name", out value2) && value2.IsText)
							{
								stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<li><code>{0}</code></li>\n", LibraryDescriptions.HtmlEncode(value2.AsString));
							}
						}
					}
					stringBuilder.AppendLine("</ul>");
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040050F0 RID: 20720
			private readonly string name;

			// Token: 0x040050F1 RID: 20721
			private readonly TypeValue type;
		}

		// Token: 0x02001796 RID: 6038
		private sealed class LibraryDescription : IDocumentation
		{
			// Token: 0x060098C4 RID: 39108 RVA: 0x001F8794 File Offset: 0x001F6994
			public LibraryDescription(TypeValue baseType, string name, ResourceManager resourceManager = null)
			{
				if (resourceManager != null)
				{
					this.languageStrings = resourceManager;
					this.codeStrings = new LibraryDescriptions.LibraryDescription.ResourceManagerStrings(resourceManager);
				}
				else
				{
					this.languageStrings = FunctionDescriptionStrings.ResourceManager;
					this.codeStrings = new LibraryDescriptions.LibraryDescription.ConstStrings();
				}
				this.functionName = name;
				this.baseType = baseType;
			}

			// Token: 0x060098C5 RID: 39109 RVA: 0x001F87E3 File Offset: 0x001F69E3
			public LibraryDescription(TypeValue baseType, string name, string caption, string shortDescription, string longDescription, string category, IList<DocumentationExample> examples)
			{
				this.caption = caption;
				this.baseType = baseType;
				this.functionName = name;
				this.shortDescription = shortDescription;
				this.longDescription = longDescription;
				this.category = category;
				this.examples = examples;
			}

			// Token: 0x17002788 RID: 10120
			// (get) Token: 0x060098C6 RID: 39110 RVA: 0x001F8820 File Offset: 0x001F6A20
			public string Name
			{
				get
				{
					return this.functionName;
				}
			}

			// Token: 0x17002789 RID: 10121
			// (get) Token: 0x060098C7 RID: 39111 RVA: 0x001F8828 File Offset: 0x001F6A28
			public string Caption
			{
				get
				{
					return this.caption;
				}
			}

			// Token: 0x1700278A RID: 10122
			// (get) Token: 0x060098C8 RID: 39112 RVA: 0x001F8830 File Offset: 0x001F6A30
			public string ShortDescription
			{
				get
				{
					if (this.shortDescription == null)
					{
						this.shortDescription = this.languageStrings.GetString(this.ResourceName);
						if (this.shortDescription == null)
						{
							this.shortDescription = this.functionName;
						}
					}
					return this.shortDescription;
				}
			}

			// Token: 0x1700278B RID: 10123
			// (get) Token: 0x060098C9 RID: 39113 RVA: 0x001F886C File Offset: 0x001F6A6C
			public string LongDescription
			{
				get
				{
					if (this.longDescription == null)
					{
						string @string = this.languageStrings.GetString(this.ResourceName + "_Description");
						this.longDescription = @string ?? this.ShortDescription;
						if (this.baseType.TypeKind == ValueKind.Function && !this.baseType.AsFunctionType.Abstract)
						{
							RecordValue parameters = this.baseType.AsFunctionType.Parameters;
							LibraryDescriptions.Parameter[] array = new LibraryDescriptions.Parameter[parameters.Keys.Length];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = new LibraryDescriptions.Parameter(parameters.Keys[i], parameters[i]);
							}
							try
							{
								IFormatProvider currentCulture = CultureInfo.CurrentCulture;
								string text = this.longDescription;
								object[] array2 = array;
								this.longDescription = string.Format(currentCulture, text, array2);
							}
							catch (FormatException ex)
							{
								throw new FormatException(this.ResourceName + " " + ex.Message, ex);
							}
						}
					}
					return this.longDescription;
				}
			}

			// Token: 0x1700278C RID: 10124
			// (get) Token: 0x060098CA RID: 39114 RVA: 0x001F8978 File Offset: 0x001F6B78
			public string Category
			{
				get
				{
					if (this.category == null)
					{
						this.category = this.codeStrings.GetString(this.ResourceName + "_Category");
						if (this.category == null)
						{
							this.category = string.Empty;
						}
					}
					return this.category;
				}
			}

			// Token: 0x1700278D RID: 10125
			// (get) Token: 0x060098CB RID: 39115 RVA: 0x001F89C8 File Offset: 0x001F6BC8
			public IList<DocumentationExample> Examples
			{
				get
				{
					if (this.examples == null)
					{
						List<DocumentationExample> list = new List<DocumentationExample>();
						int num = 1;
						string text = this.ResourceName + "_Example";
						for (string text2 = this.languageStrings.GetString(text + num.ToString()); text2 != null; text2 = this.languageStrings.GetString(text + num.ToString()))
						{
							list.Add(new DocumentationExample(text2, this.codeStrings.GetString(text + num.ToString() + "_Code"), this.codeStrings.GetString(text + num.ToString() + "_Result")));
							num++;
						}
						this.examples = list;
					}
					return this.examples;
				}
			}

			// Token: 0x1700278E RID: 10126
			// (get) Token: 0x060098CC RID: 39116 RVA: 0x001F8A87 File Offset: 0x001F6C87
			private string ResourceName
			{
				get
				{
					if (this.resourceName == null)
					{
						this.resourceName = this.functionName.Replace('.', '_').Replace("#", "_Pound_");
					}
					return this.resourceName;
				}
			}

			// Token: 0x060098CD RID: 39117 RVA: 0x001F8ABB File Offset: 0x001F6CBB
			public RecordValue AsMetadata()
			{
				return RecordValue.New(LibraryDescriptions.DescriptionKeys, delegate(int i)
				{
					switch (i)
					{
					case 0:
						return TextValue.New(this.Name);
					case 1:
						return TextValue.New(this.ShortDescription);
					case 2:
						return TextValue.New(this.LongDescription);
					case 3:
						return TextValue.New(this.Category);
					default:
						return LibraryDescriptions.LibraryDescription.AsMetadata(this.Examples);
					}
				});
			}

			// Token: 0x060098CE RID: 39118 RVA: 0x001F8AD4 File Offset: 0x001F6CD4
			private static ListValue AsMetadata(IList<DocumentationExample> examples)
			{
				Value[] array = new Value[examples.Count];
				for (int i = 0; i < examples.Count; i++)
				{
					array[i] = RecordValue.New(LibraryDescriptions.ExampleKeys, new Value[]
					{
						TextValue.New(examples[i].Description),
						TextValue.New(examples[i].Code),
						TextValue.New(examples[i].Result)
					});
				}
				return ListValue.New(array);
			}

			// Token: 0x040050F2 RID: 20722
			private readonly ResourceManager languageStrings;

			// Token: 0x040050F3 RID: 20723
			private readonly LibraryDescriptions.LibraryDescription.IResourceStrings codeStrings;

			// Token: 0x040050F4 RID: 20724
			private readonly string functionName;

			// Token: 0x040050F5 RID: 20725
			private readonly string caption;

			// Token: 0x040050F6 RID: 20726
			private string resourceName;

			// Token: 0x040050F7 RID: 20727
			private string shortDescription;

			// Token: 0x040050F8 RID: 20728
			private string longDescription;

			// Token: 0x040050F9 RID: 20729
			private string category;

			// Token: 0x040050FA RID: 20730
			private IList<DocumentationExample> examples;

			// Token: 0x040050FB RID: 20731
			private readonly TypeValue baseType;

			// Token: 0x02001797 RID: 6039
			private interface IResourceStrings
			{
				// Token: 0x060098D0 RID: 39120
				string GetString(string name);
			}

			// Token: 0x02001798 RID: 6040
			private class ResourceManagerStrings : LibraryDescriptions.LibraryDescription.IResourceStrings
			{
				// Token: 0x060098D1 RID: 39121 RVA: 0x001F8BBC File Offset: 0x001F6DBC
				public ResourceManagerStrings(ResourceManager resourceManager)
				{
					this.resourceManager = resourceManager;
				}

				// Token: 0x060098D2 RID: 39122 RVA: 0x001F8BCB File Offset: 0x001F6DCB
				public string GetString(string name)
				{
					return this.resourceManager.GetString(name);
				}

				// Token: 0x040050FC RID: 20732
				private readonly ResourceManager resourceManager;
			}

			// Token: 0x02001799 RID: 6041
			private class ConstStrings : LibraryDescriptions.LibraryDescription.IResourceStrings
			{
				// Token: 0x060098D3 RID: 39123 RVA: 0x001F8BD9 File Offset: 0x001F6DD9
				public string GetString(string name)
				{
					return FunctionConstStrings.GetString(name);
				}
			}
		}

		// Token: 0x0200179A RID: 6042
		private class HtmlSanitizer
		{
			// Token: 0x060098D5 RID: 39125 RVA: 0x001F8BE4 File Offset: 0x001F6DE4
			public static string Sanitize(string input)
			{
				StringBuilder stringBuilder = new StringBuilder(input);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					if (stringBuilder[i] == '<')
					{
						bool flag = false;
						foreach (string text in LibraryDescriptions.HtmlSanitizer.allowedTags)
						{
							int num;
							flag = LibraryDescriptions.HtmlSanitizer.TryMatch(stringBuilder, i + 1, text, out num);
							if (flag)
							{
								i += num;
								break;
							}
						}
						if (!flag)
						{
							stringBuilder.Remove(i, 1);
							stringBuilder.Insert(i, "&lt;");
							i += 3;
						}
					}
					else if (stringBuilder[i] == '>')
					{
						stringBuilder.Remove(i, 1);
						stringBuilder.Insert(i, "&gt;");
						i += 3;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060098D6 RID: 39126 RVA: 0x001F8C9C File Offset: 0x001F6E9C
			private static bool TryMatch(StringBuilder builder, int position, string tag, out int skip)
			{
				if (position < builder.Length && builder[position] == '/')
				{
					bool flag = LibraryDescriptions.HtmlSanitizer.TryMatch(builder, position + 1, tag, out skip);
					skip++;
					return flag;
				}
				skip = 0;
				if (position + tag.Length >= builder.Length || builder[position + tag.Length] != '>')
				{
					return false;
				}
				for (int i = 0; i < tag.Length; i++)
				{
					if (char.ToLowerInvariant(builder[position + i]) != tag[i])
					{
						return false;
					}
				}
				skip = tag.Length + 1;
				return true;
			}

			// Token: 0x040050FD RID: 20733
			private static readonly string[] allowedTags = new string[]
			{
				"code", "ul", "li", "i", "b", "div", "p", "br", "table", "tr",
				"td"
			};
		}
	}
}
