using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000141 RID: 321
	internal sealed class TmdlParser
	{
		// Token: 0x06001509 RID: 5385 RVA: 0x0008DAA9 File Offset: 0x0008BCA9
		public TmdlParser()
		{
			this.schema = MetadataObjectConfiguration.Default.Schema;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0008DAC1 File Offset: 0x0008BCC1
		public TmdlParser(TmdlSchema schema)
		{
			this.schema = schema;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0008DAD0 File Offset: 0x0008BCD0
		public TmdlDocument ParseFile(string path)
		{
			return TmdlParser.ParseFile(this.schema, path);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0008DADE File Offset: 0x0008BCDE
		public TmdlDocument ParseDocument(string tmdl)
		{
			return TmdlParser.ParseDocument(this.schema, tmdl);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0008DAEC File Offset: 0x0008BCEC
		public TmdlDocument ParseDocument(Stream tmdl, string path = null)
		{
			return TmdlParser.ParseDocument(this.schema, tmdl, path);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0008DAFC File Offset: 0x0008BCFC
		internal static TmdlProperty ParseProperty(TmdlTextLine line, TmdlPropertyInfo propertyInfo)
		{
			TmdlParser.ParsingState parsingState;
			return TmdlParser.ParsePropertyImpl(line, propertyInfo, -1, out parsingState);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0008DB14 File Offset: 0x0008BD14
		internal static string[] TrimExpressionWhitespaces(TmdlExpressionTrimStyle trimStyle, Indentation? trimIndentation, IEnumerable<string> lines)
		{
			List<StringBuilder> list = lines.Select((string l) => new StringBuilder(l)).ToList<StringBuilder>();
			if ((trimStyle & TmdlExpressionTrimStyle.TrimTrailingWhitespaces) == TmdlExpressionTrimStyle.TrimTrailingWhitespaces)
			{
				for (int i = 0; i < list.Count; i++)
				{
					int num = list[i].Length;
					while (num > 0 && char.IsWhiteSpace(list[i][num - 1]))
					{
						num--;
					}
					if (num < list[i].Length)
					{
						if (num == 0)
						{
							list[i].Clear();
						}
						else
						{
							list[i].Remove(num, list[i].Length - num);
						}
					}
				}
				while (list.Count > 0 && list[list.Count - 1].Length == 0)
				{
					list.RemoveAt(list.Count - 1);
				}
			}
			bool flag = (trimStyle & TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces) == TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces;
			int num2 = 0;
			int count = list.Count;
			if (count == 0)
			{
				return new string[0];
			}
			if (count != 1)
			{
				string[] array = new string[list.Count];
				if (!flag)
				{
					for (int j = 0; j < list.Count; j++)
					{
						array[j] = list[j].ToString();
					}
					return array;
				}
				int num3 = list.IndexOf((StringBuilder sb) => sb.Length > 0);
				if (num3 == -1)
				{
					for (int k = 0; k < list.Count; k++)
					{
						array[k] = string.Empty;
					}
				}
				else
				{
					bool flag2 = true;
					while (flag2 && num2 < list[num3].Length)
					{
						char c = list[num3][num2];
						if (!char.IsWhiteSpace(c) || (trimIndentation != null && (trimIndentation.Value.Text.Length <= num2 || trimIndentation.Value.Text[num2] != c)))
						{
							flag2 = false;
						}
						else
						{
							int num4 = 0;
							while (flag2 && num4 < list.Count)
							{
								if (num4 != num3 && list[num4].Length > num2 && list[num4][num2] != c)
								{
									flag2 = false;
								}
								num4++;
							}
							if (flag2)
							{
								num2++;
							}
						}
					}
					if (num2 > 0)
					{
						for (int n = 0; n < list.Count; n++)
						{
							if (num2 < list[n].Length)
							{
								array[n] = list[n].Remove(0, num2).ToString();
							}
							else
							{
								array[n] = string.Empty;
							}
						}
					}
					else
					{
						for (int m = 0; m < list.Count; m++)
						{
							array[m] = list[m].ToString();
						}
					}
				}
				return array;
			}
			else
			{
				if (!flag)
				{
					return new string[] { list[0].ToString() };
				}
				while (num2 < list[0].Length && char.IsWhiteSpace(list[0][num2]) && (trimIndentation == null || (num2 < trimIndentation.Value.Text.Length && list[0][num2] == trimIndentation.Value.Text[num2])))
				{
					num2++;
				}
				if (num2 == 0)
				{
					return new string[] { list[0].ToString() };
				}
				if (num2 < list[0].Length)
				{
					return new string[] { list[0].Remove(0, num2).ToString() };
				}
				return new string[] { string.Empty };
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0008DEC0 File Offset: 0x0008C0C0
		internal static TmdlDocument ParseFile(TmdlSchema schema, string path)
		{
			TmdlDocument tmdlDocument;
			using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				tmdlDocument = TmdlParser.ParseDocument(schema, stream, path);
			}
			return tmdlDocument;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0008DF00 File Offset: 0x0008C100
		internal static TmdlDocument ParseDocument(TmdlSchema schema, string tmdl)
		{
			return TmdlParser.ParseDocumentImpl(schema, new TmdlTextReader(new StringReader(tmdl)), null, true);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0008DF15 File Offset: 0x0008C115
		internal static TmdlDocument ParseDocument(TmdlSchema schema, Stream tmdl, string path)
		{
			return TmdlParser.ParseDocumentImpl(schema, new TmdlTextReader(tmdl), path, true);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0008DF25 File Offset: 0x0008C125
		internal static TmdlDocument ParseDocument(TmdlSchema schema, ITmdlReader reader, string path)
		{
			return TmdlParser.ParseDocumentImpl(schema, reader, path, false);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0008DF30 File Offset: 0x0008C130
		internal static TmdlProject ParseProject(TmdlContentSource contentSource, TmdlSchema schema, IMetadataDeserializationController controller, object context)
		{
			object operationContext;
			IReadOnlyCollection<string> readOnlyCollection;
			controller.OnDeserializationStart(context, out operationContext, out readOnlyCollection);
			TmdlProject tmdlProject = null;
			try
			{
				tmdlProject = new TmdlProject(contentSource, readOnlyCollection.Select(delegate(string logicalPath)
				{
					object obj;
					Stream stream;
					controller.OnDocumentDeserializationStart(context, operationContext, logicalPath, out obj, out stream);
					TmdlDocument tmdlDocument = null;
					try
					{
						tmdlDocument = TmdlParser.ParseDocumentImpl(schema, new TmdlTextReader(stream), logicalPath, true);
					}
					finally
					{
						controller.OnDocumentDeserializationEnd(context, operationContext, logicalPath, obj, stream, tmdlDocument != null);
					}
					return tmdlDocument;
				}));
			}
			catch (Exception ex)
			{
				tmdlProject = null;
				controller.OnDeserializationError(context, operationContext, ex);
				throw;
			}
			finally
			{
				controller.OnDeserializationEnd(context, operationContext, tmdlProject != null);
			}
			return tmdlProject;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
		private static TmdlDocument ParseDocumentImpl(TmdlSchema schema, ITmdlReader reader, string path, bool disposeReader)
		{
			TmdlDocument document;
			try
			{
				TmdlParser.DocumentContext documentContext = new TmdlParser.DocumentContext(reader, path);
				do
				{
					TmdlObject tmdlObject = TmdlParser.ParseObject(documentContext, schema, documentContext.Document.Objects.Count == 0);
					if (tmdlObject != null)
					{
						documentContext.Document.Objects.Add(tmdlObject);
					}
				}
				while (!documentContext.EOF);
				document = documentContext.Document;
			}
			finally
			{
				if (disposeReader)
				{
					((IDisposable)reader).Dispose();
				}
			}
			return document;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0008E05C File Offset: 0x0008C25C
		private static TmdlObject ParseObject(TmdlParser.DocumentContext context, TmdlSchema schema, bool isNewDoc)
		{
			if (isNewDoc && !context.MoveToContent())
			{
				return null;
			}
			return new TmdlParser.ObjectContext(context, schema, null, null).ReadObject(null);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0008E090 File Offset: 0x0008C290
		private static TmdlProperty ParseProperty(TmdlParser.DocumentContext context, TmdlPropertyInfo propertyInfo, int startIndex, out TmdlParser.ParsingState state)
		{
			TmdlProperty tmdlProperty;
			try
			{
				tmdlProperty = TmdlParser.ParsePropertyImpl(context.CurrentLine, propertyInfo, startIndex, out state);
			}
			catch (TmdlParserException ex)
			{
				throw TmdlParser.CreateFormatException(ex.Error, ex.Message, context, ex);
			}
			return tmdlProperty;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0008E0D4 File Offset: 0x0008C2D4
		private static TmdlProperty ParsePropertyImpl(TmdlTextLine line, TmdlPropertyInfo propertyInfo, int startIndex, out TmdlParser.ParsingState state)
		{
			string text;
			bool flag;
			if (startIndex == -1)
			{
				switch (line.Type)
				{
				case TmdlLineType.Property:
					text = line.Text.SubstringAndTrim(line.FirstTokenIndex, false, true);
					flag = false;
					break;
				case TmdlLineType.OldSyntaxExpressionProperty:
					if (propertyInfo.Type != TmdlValueType.String)
					{
						throw new TmdlParserException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(line.Type.ToString("G"), TomSR.TmdlFormatError_NotStringPropertyLine));
					}
					text = line.Text.SubstringAndTrim(line.FirstTokenIndex, false, true);
					flag = true;
					break;
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
					text = line.Text.SubstringAndTrim(line.FirstTokenIndex, false, true);
					flag = true;
					break;
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					text = string.Empty;
					flag = false;
					break;
				default:
					throw new TmdlParserException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(line.Type.ToString("G"), TomSR.TmdlFormatError_NotPropertyLine));
				}
			}
			else
			{
				text = line.Text.SubstringAndTrim(startIndex, false, true);
				flag = true;
			}
			switch (propertyInfo.Type)
			{
			case TmdlValueType.String:
				if (!string.IsNullOrEmpty(text) && string.Compare(text, "```", StringComparison.Ordinal) != 0)
				{
					state = TmdlParser.ParsingState.NormalContent;
					if (flag)
					{
						return new TmdlProperty(propertyInfo.Name, TmdlValue.FromExpression(text));
					}
					return new TmdlProperty(propertyInfo.Name, TmdlValue.FromString(text));
				}
				else
				{
					if (!flag)
					{
						throw new TmdlParserException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlParserStringPropertyWithNoValue(propertyInfo.Name));
					}
					state = (string.IsNullOrEmpty(text) ? TmdlParser.ParsingState.Expression : TmdlParser.ParsingState.QuotedExpression);
					return new TmdlProperty(propertyInfo.Name, new TmdlStringValue(new string[0], TmdlStringFormat.Block, true));
				}
				break;
			case TmdlValueType.Scalar:
			{
				state = TmdlParser.ParsingState.NormalContent;
				TmdlScalarValueType? scalarValueType = propertyInfo.ScalarValueType;
				if (scalarValueType != null)
				{
					switch (scalarValueType.GetValueOrDefault())
					{
					case TmdlScalarValueType.Int:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseScalar<int>(text));
					case TmdlScalarValueType.Long:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseScalar<long>(text));
					case TmdlScalarValueType.Double:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseScalar<double>(text));
					case TmdlScalarValueType.Date:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseScalar<DateTime>(text));
					case TmdlScalarValueType.Bool:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseScalar<bool>(text));
					case TmdlScalarValueType.Enum:
						return new TmdlProperty(propertyInfo.Name, TmdlValue.ParseEnum(propertyInfo.EnumType, text));
					}
				}
				throw new TmdlParserException(ParsingError.UnsupportedScalarType, TomSR.Exception_TmdlParserInvalidScalarType(propertyInfo.ScalarValueType.Value.ToString("G")));
			}
			case TmdlValueType.Struct:
				state = TmdlParser.ParsingState.NormalContent;
				return new TmdlProperty(propertyInfo.Name, new TmdlStructValue());
			case TmdlValueType.Collection:
				state = TmdlParser.ParsingState.NormalContent;
				return new TmdlProperty(propertyInfo.Name, new TmdlCollectionValue());
			case TmdlValueType.MetadataObject:
				state = TmdlParser.ParsingState.NormalContent;
				return new TmdlProperty(propertyInfo.Name, new TmdlMetadataObjectValue(propertyInfo.MetadataObjectType.Value));
			case TmdlValueType.ModelReference:
				state = TmdlParser.ParsingState.NormalContent;
				return new TmdlProperty(propertyInfo.Name, new TmdlModelReferenceValue(ObjectName.Parse(text)));
			case TmdlValueType.TranslationRoot:
				state = TmdlParser.ParsingState.NormalContent;
				return new TmdlProperty(propertyInfo.Name, new TmdlTranslationRootValue());
			default:
				throw new TmdlParserException(ParsingError.UnsupportedPropertyType, TomSR.Exception_TmdlParserPropertyWithUnsupportedType(propertyInfo.Name, propertyInfo.Type.ToString("G")));
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0008E3F0 File Offset: 0x0008C5F0
		private static bool IsValidPropertyLine(TmdlTextLine line, ITmdlPropertiesContainer container, Indentation baseIndentation, Indentation? bodyIndentation = null)
		{
			TmdlLineType type = line.Type;
			if (type - TmdlLineType.Property > 3)
			{
				return false;
			}
			TmdlPropertyInfo tmdlPropertyInfo;
			if (!container.TryGetPropertyInfo(line.Keyword, out tmdlPropertyInfo) || (line.Type == TmdlLineType.OldSyntaxExpressionProperty && tmdlPropertyInfo.Type != TmdlValueType.String))
			{
				return false;
			}
			if (bodyIndentation == null)
			{
				return line.Indentation > baseIndentation;
			}
			return line.Indentation == bodyIndentation;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0008E468 File Offset: 0x0008C668
		private static bool IsValidChildObjectLine(TmdlTextLine line, ITmdlObjectContainer currentObject, Indentation objectIndentation, Indentation? bodyIndentation = null)
		{
			switch (line.Type)
			{
			case TmdlLineType.Description:
				if (bodyIndentation == null)
				{
					return line.Indentation > objectIndentation;
				}
				return line.Indentation == bodyIndentation;
			case TmdlLineType.NamedObject:
			case TmdlLineType.NamedObjectWithDefaultProperty:
			case TmdlLineType.ElementWithDefaultPropertyOrExpression:
			case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
			{
				TmdlObjectInfo tmdlObjectInfo;
				if (!currentObject.TryGetObjectInfo(line.Keyword, out tmdlObjectInfo))
				{
					return false;
				}
				if (bodyIndentation == null)
				{
					return line.Indentation > objectIndentation;
				}
				return line.Indentation == bodyIndentation;
			}
			}
			return false;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0008E52C File Offset: 0x0008C72C
		private static bool IsValidSiblingObjectLine(TmdlTextLine line, ITmdlObjectContainer parent, Indentation objectIndentation)
		{
			switch (line.Type)
			{
			case TmdlLineType.Description:
				return line.Indentation == objectIndentation;
			case TmdlLineType.NamedObject:
			case TmdlLineType.NamedObjectWithDefaultProperty:
			case TmdlLineType.ElementWithDefaultPropertyOrExpression:
			case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
			{
				TmdlObjectInfo tmdlObjectInfo;
				return parent.TryGetObjectInfo(line.Keyword, out tmdlObjectInfo) && line.Indentation == objectIndentation;
			}
			}
			return false;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0008E598 File Offset: 0x0008C798
		private static TmdlFormatException CreateFormatException(ParsingError error, string message, TmdlParser.DocumentContext context, Exception innerException = null)
		{
			if (context != null)
			{
				return new TmdlFormatException(error, message, context.GetCurrentLocation(), context.CurrentLine.OriginalText, innerException);
			}
			return new TmdlFormatException(error, message, default(TmdlSourceLocation), null, innerException);
		}

		// Token: 0x04000392 RID: 914
		private readonly TmdlSchema schema;

		// Token: 0x02000323 RID: 803
		private enum ParsingState
		{
			// Token: 0x04000DC4 RID: 3524
			Start,
			// Token: 0x04000DC5 RID: 3525
			NormalContent,
			// Token: 0x04000DC6 RID: 3526
			Expression,
			// Token: 0x04000DC7 RID: 3527
			QuotedExpression,
			// Token: 0x04000DC8 RID: 3528
			EOF
		}

		// Token: 0x02000324 RID: 804
		private interface IExpressionLineEvaluator
		{
			// Token: 0x060024EE RID: 9454
			void Initialize(TmdlTextLine firstLine);

			// Token: 0x060024EF RID: 9455
			bool IsExpressionLine(TmdlTextLine line);

			// Token: 0x060024F0 RID: 9456
			bool IsKnownElemement(TmdlTextLine line);
		}

		// Token: 0x02000325 RID: 805
		private sealed class DocumentContext
		{
			// Token: 0x060024F1 RID: 9457 RVA: 0x000E5725 File Offset: 0x000E3925
			public DocumentContext(ITmdlReader reader, string path)
			{
				this.reader = reader;
				this.Document = new TmdlDocument(path);
			}

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000E5740 File Offset: 0x000E3940
			public TmdlDocument Document { get; }

			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000E5748 File Offset: 0x000E3948
			// (set) Token: 0x060024F4 RID: 9460 RVA: 0x000E5750 File Offset: 0x000E3950
			public TmdlTextLine CurrentLine { get; private set; }

			// Token: 0x1700078B RID: 1931
			// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000E5759 File Offset: 0x000E3959
			public bool IsInExpression
			{
				get
				{
					return this.state == TmdlParser.ParsingState.Expression || this.state == TmdlParser.ParsingState.QuotedExpression;
				}
			}

			// Token: 0x1700078C RID: 1932
			// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000E576F File Offset: 0x000E396F
			public bool EOF
			{
				get
				{
					return this.state == TmdlParser.ParsingState.EOF;
				}
			}

			// Token: 0x060024F7 RID: 9463 RVA: 0x000E577A File Offset: 0x000E397A
			public bool MoveToContent()
			{
				if (!this.ReadLineSkipBlanksImpl())
				{
					return false;
				}
				this.state = TmdlParser.ParsingState.NormalContent;
				return true;
			}

			// Token: 0x060024F8 RID: 9464 RVA: 0x000E5790 File Offset: 0x000E3990
			public bool ReadLine()
			{
				this.CurrentLine = this.reader.ReadLine();
				if (!this.CurrentLine.IsValid)
				{
					this.state = TmdlParser.ParsingState.EOF;
				}
				return this.CurrentLine.IsValid;
			}

			// Token: 0x060024F9 RID: 9465 RVA: 0x000E57D3 File Offset: 0x000E39D3
			public bool ReadLineSkipBlanks()
			{
				return this.ReadLineSkipBlanksImpl();
			}

			// Token: 0x060024FA RID: 9466 RVA: 0x000E57DB File Offset: 0x000E39DB
			public void CollectDescriptionLines(ICollection<TmdlTextLine> lines, Indentation expectedIndentation)
			{
				while (this.CurrentLine.Type == TmdlLineType.Description)
				{
					this.VerifyIndentation(expectedIndentation);
					lines.Add(this.CurrentLine);
					if (!this.ReadLine())
					{
						return;
					}
				}
			}

			// Token: 0x060024FB RID: 9467 RVA: 0x000E580C File Offset: 0x000E3A0C
			public void ReadProperty(ITmdlPropertiesContainer container, ICollection<TmdlProperty> properties, ICollection<TmdlProperty> deprecatedProperties, Indentation baseIndentation, TmdlParser.IExpressionLineEvaluator evaluator, TmdlPropertyInfo propertyInfo)
			{
				if (propertyInfo != null)
				{
					if (!this.CurrentLine.Keyword.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						throw TmdlParser.CreateFormatException(ParsingError.MismatchPropertyName, TomSR.Exception_TmdlFormatPropertyMismatch(propertyInfo.Name, this.CurrentLine.Keyword), this, null);
					}
				}
				else if (!container.TryGetPropertyInfo(this.CurrentLine.Keyword, out propertyInfo))
				{
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatPropertyUnsupported(this.CurrentLine.Keyword), this, null);
				}
				TmdlProperty tmdlProperty = TmdlParser.ParseProperty(this, propertyInfo, -1, out this.state);
				tmdlProperty.SourceLocation = this.GetCurrentLocation();
				if (!propertyInfo.IsDeprecated)
				{
					properties.Add(tmdlProperty);
				}
				else if (deprecatedProperties != null)
				{
					deprecatedProperties.Add(tmdlProperty);
				}
				switch (this.state)
				{
				case TmdlParser.ParsingState.NormalContent:
					if (!this.ReadLineSkipBlanks())
					{
						return;
					}
					break;
				case TmdlParser.ParsingState.Expression:
				case TmdlParser.ParsingState.QuotedExpression:
					if (!this.ReadLine())
					{
						throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatMissingExpressionValue, this, null);
					}
					break;
				case TmdlParser.ParsingState.EOF:
					return;
				}
				switch (propertyInfo.Type)
				{
				case TmdlValueType.String:
				{
					TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
					if (tmdlStringValue != null && tmdlStringValue.Format == TmdlStringFormat.Block)
					{
						List<TmdlTextLine> list = new List<TmdlTextLine> { this.CurrentLine };
						Indentation? indentation;
						this.CollectExpressionBlockLines(list, evaluator, out indentation);
						TmdlExpressionTrimStyle tmdlExpressionTrimStyle = TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces;
						if (indentation == null)
						{
							tmdlExpressionTrimStyle |= TmdlExpressionTrimStyle.TrimTrailingWhitespaces;
						}
						tmdlStringValue.UpdateLines(TmdlParser.TrimExpressionWhitespaces(tmdlExpressionTrimStyle, indentation, list.Select((TmdlTextLine x) => x.OriginalText)));
						return;
					}
					return;
				}
				case TmdlValueType.Scalar:
				case TmdlValueType.ModelReference:
					return;
				case TmdlValueType.Struct:
				{
					TmdlStructValue tmdlStructValue = (TmdlStructValue)tmdlProperty.Value;
					if (this.CurrentLine.Indentation <= baseIndentation)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this, null);
					}
					new TmdlParser.StructPropertiesContext(this, propertyInfo, baseIndentation, new Indentation?(this.CurrentLine.Indentation), evaluator).ReadNestedProperties(tmdlStructValue.Properties, null);
					return;
				}
				case TmdlValueType.Collection:
				{
					if (this.CurrentLine.Type != TmdlLineType.Property)
					{
						throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoCollectionItem, this, null);
					}
					if (this.CurrentLine.Indentation <= baseIndentation)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this, null);
					}
					TmdlCollectionValue tmdlCollectionValue = (TmdlCollectionValue)tmdlProperty.Value;
					new TmdlParser.CollectionPropertiesContext(this, propertyInfo, this.CurrentLine.Indentation).ReadCollectionPropertiesBatches(tmdlCollectionValue.Items);
					return;
				}
				case TmdlValueType.MetadataObject:
				{
					if (this.CurrentLine.Indentation <= baseIndentation)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this, null);
					}
					TmdlMetadataObjectValue tmdlMetadataObjectValue = (TmdlMetadataObjectValue)tmdlProperty.Value;
					ICollection<TmdlProperty> collection = new List<TmdlProperty>();
					new TmdlParser.StructPropertiesContext(this, propertyInfo, baseIndentation, new Indentation?(this.CurrentLine.Indentation), evaluator).ReadNestedProperties(tmdlMetadataObjectValue.Object.Properties, collection);
					if (collection.Count <= 0)
					{
						return;
					}
					using (IEnumerator<TmdlProperty> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TmdlProperty tmdlProperty2 = enumerator.Current;
							tmdlMetadataObjectValue.Object.AddDeprecatedProperty(tmdlProperty2);
						}
						return;
					}
					break;
				}
				case TmdlValueType.TranslationRoot:
					break;
				default:
					return;
				}
				TmdlTranslationRootValue tmdlTranslationRootValue = (TmdlTranslationRootValue)tmdlProperty.Value;
				TmdlTranslationElement tmdlTranslationElement = new TmdlParser.TranslationElementContext(this, propertyInfo.RootElementInfo, this.CurrentLine.Indentation, null).ReadElement(propertyInfo.RootElementInfo);
				if (tmdlTranslationElement != null)
				{
					tmdlTranslationRootValue.Root = tmdlTranslationElement;
				}
			}

			// Token: 0x060024FC RID: 9468 RVA: 0x000E5B70 File Offset: 0x000E3D70
			public TmdlProperty ReadDefaultProperty(TmdlPropertyInfo defaultProperty, int defaultPropertyStartIndex, TmdlParser.IExpressionLineEvaluator evaluator, out bool advanceReader)
			{
				if (this.CurrentLine.Type != TmdlLineType.ElementWithDefaultPropertyOrExpression)
				{
					defaultPropertyStartIndex++;
					while (defaultPropertyStartIndex < this.CurrentLine.Text.Length && char.IsWhiteSpace(this.CurrentLine.Text, defaultPropertyStartIndex))
					{
						defaultPropertyStartIndex++;
					}
				}
				TmdlProperty tmdlProperty;
				if (defaultPropertyStartIndex == this.CurrentLine.Text.Length)
				{
					if (defaultProperty.Type != TmdlValueType.String)
					{
						throw TmdlParser.CreateFormatException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlFormatDefaultPropertyNotInline, this, null);
					}
					this.state = TmdlParser.ParsingState.Expression;
					List<TmdlTextLine> list = new List<TmdlTextLine>();
					Indentation? indentation;
					this.CollectExpressionBlockLines(list, evaluator, out indentation);
					TmdlExpressionTrimStyle tmdlExpressionTrimStyle = TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces;
					if (indentation == null)
					{
						tmdlExpressionTrimStyle |= TmdlExpressionTrimStyle.TrimTrailingWhitespaces;
					}
					tmdlProperty = new TmdlProperty(defaultProperty.Name, new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(tmdlExpressionTrimStyle, indentation, list.Select((TmdlTextLine x) => x.OriginalText)), TmdlStringFormat.Block, true));
					advanceReader = false;
				}
				else
				{
					tmdlProperty = TmdlParser.ParseProperty(this, defaultProperty, defaultPropertyStartIndex, out this.state);
					TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
					if (tmdlStringValue != null && tmdlStringValue.Format == TmdlStringFormat.Block)
					{
						List<TmdlTextLine> list2 = new List<TmdlTextLine>();
						Indentation? indentation2;
						this.CollectExpressionBlockLines(list2, evaluator, out indentation2);
						TmdlExpressionTrimStyle tmdlExpressionTrimStyle2 = TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces;
						if (indentation2 == null)
						{
							tmdlExpressionTrimStyle2 |= TmdlExpressionTrimStyle.TrimTrailingWhitespaces;
						}
						tmdlStringValue.UpdateLines(TmdlParser.TrimExpressionWhitespaces(tmdlExpressionTrimStyle2, indentation2, list2.Select((TmdlTextLine x) => x.OriginalText)));
						advanceReader = false;
					}
					else
					{
						advanceReader = true;
					}
				}
				return tmdlProperty;
			}

			// Token: 0x060024FD RID: 9469 RVA: 0x000E5CE8 File Offset: 0x000E3EE8
			internal TmdlSourceLocation GetCurrentLocation()
			{
				return new TmdlSourceLocation(this.Document, this.CurrentLine.IsValid ? this.reader.LineNumber : 0);
			}

			// Token: 0x060024FE RID: 9470 RVA: 0x000E5D1E File Offset: 0x000E3F1E
			internal TmdlTextLine VerifyIndentation(Indentation indentation)
			{
				if (this.CurrentLine.Indentation != indentation)
				{
					throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this, null);
				}
				return this.CurrentLine;
			}

			// Token: 0x060024FF RID: 9471 RVA: 0x000E5D47 File Offset: 0x000E3F47
			private bool ReadLineSkipBlanksImpl()
			{
				while (this.ReadLine())
				{
					if (this.CurrentLine.Type != TmdlLineType.Empty)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002500 RID: 9472 RVA: 0x000E5D64 File Offset: 0x000E3F64
			private void CollectExpressionBlockLines(IList<TmdlTextLine> lines, TmdlParser.IExpressionLineEvaluator evaluator, out Indentation? expressionTrimIndentation)
			{
				expressionTrimIndentation = null;
				bool flag = this.state == TmdlParser.ParsingState.QuotedExpression;
				bool flag2 = !flag;
				if (lines.Count > 0)
				{
					if (flag)
					{
						if (lines[0].Type == TmdlLineType.Other && string.Compare(lines[0].Text, "```", StringComparison.Ordinal) == 0)
						{
							expressionTrimIndentation = new Indentation?(this.CurrentLine.Indentation);
							lines.Clear();
							this.ReadLineSkipBlanksImpl();
							if (this.state != TmdlParser.ParsingState.EOF)
							{
								this.state = TmdlParser.ParsingState.NormalContent;
							}
							return;
						}
					}
					else if (lines[0].Type != TmdlLineType.Empty)
					{
						if (string.Compare(lines[0].OriginalText, "```", StringComparison.Ordinal) == 0)
						{
							this.state = TmdlParser.ParsingState.QuotedExpression;
							flag = true;
							lines.RemoveAt(0);
						}
						else
						{
							evaluator.Initialize(lines[0]);
						}
						flag2 = false;
					}
				}
				while (this.ReadLine())
				{
					if (this.CurrentLine.Type != TmdlLineType.Empty)
					{
						if (flag2)
						{
							if (lines.Count == 0 && string.Compare(this.CurrentLine.OriginalText, "```", StringComparison.Ordinal) == 0)
							{
								this.state = TmdlParser.ParsingState.QuotedExpression;
								flag = true;
								flag2 = false;
								continue;
							}
							evaluator.Initialize(this.CurrentLine);
							flag2 = false;
						}
						else if (!flag && !evaluator.IsExpressionLine(this.CurrentLine))
						{
							break;
						}
					}
					if (flag && this.CurrentLine.Type == TmdlLineType.Other && string.Compare(this.CurrentLine.Text, "```", StringComparison.Ordinal) == 0)
					{
						expressionTrimIndentation = new Indentation?(this.CurrentLine.Indentation);
						this.ReadLineSkipBlanksImpl();
						break;
					}
					lines.Add(this.CurrentLine);
				}
				if (!flag)
				{
					while (lines.Count > 0 && lines[lines.Count - 1].IsValid && lines[lines.Count - 1].Type == TmdlLineType.Empty)
					{
						lines.RemoveAt(lines.Count - 1);
					}
				}
				if (this.state != TmdlParser.ParsingState.EOF)
				{
					this.state = TmdlParser.ParsingState.NormalContent;
				}
			}

			// Token: 0x04000DC9 RID: 3529
			private readonly ITmdlReader reader;

			// Token: 0x04000DCA RID: 3530
			private TmdlParser.ParsingState state;
		}

		// Token: 0x02000326 RID: 806
		private sealed class StructPropertiesContext : TmdlParser.IExpressionLineEvaluator
		{
			// Token: 0x06002501 RID: 9473 RVA: 0x000E5F63 File Offset: 0x000E4163
			public StructPropertiesContext(TmdlParser.DocumentContext context, ITmdlPropertiesContainer container, Indentation baseIndentation, Indentation? bodyIndentation, TmdlParser.IExpressionLineEvaluator parent)
			{
				this.context = context;
				this.container = container;
				this.baseIndentation = baseIndentation;
				this.parent = parent;
				if (bodyIndentation != null)
				{
					this.bodyIndentation = new Indentation?(bodyIndentation.Value);
				}
			}

			// Token: 0x06002502 RID: 9474 RVA: 0x000E5FA4 File Offset: 0x000E41A4
			public void ReadNestedProperties(ICollection<TmdlProperty> properties, ICollection<TmdlProperty> deprecatedProperties)
			{
				TmdlTextLine tmdlTextLine;
				if (this.bodyIndentation != null)
				{
					tmdlTextLine = this.context.VerifyIndentation(this.bodyIndentation.Value);
				}
				else
				{
					tmdlTextLine = this.context.CurrentLine;
					if (this.expressionsIndentation != null)
					{
						if (tmdlTextLine.Indentation <= this.baseIndentation || tmdlTextLine.Indentation >= this.expressionsIndentation.Value)
						{
							throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
						}
					}
					else if (tmdlTextLine.Indentation <= this.baseIndentation)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
					}
					this.bodyIndentation = new Indentation?(tmdlTextLine.Indentation);
				}
				for (;;)
				{
					TmdlLineType type = tmdlTextLine.Type;
					if (type - TmdlLineType.Property > 3)
					{
						break;
					}
					this.context.ReadProperty(this.container, properties, deprecatedProperties, this.bodyIndentation.Value, this, null);
					tmdlTextLine = this.context.CurrentLine;
					if (this.context.EOF || !(tmdlTextLine.Indentation == this.bodyIndentation.Value))
					{
						return;
					}
				}
				throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(tmdlTextLine.Type.ToString("G")), this.context, null);
			}

			// Token: 0x06002503 RID: 9475 RVA: 0x000E60F4 File Offset: 0x000E42F4
			void TmdlParser.IExpressionLineEvaluator.Initialize(TmdlTextLine firstLine)
			{
				if (this.expressionsIndentation != null)
				{
					if (firstLine.Indentation != this.expressionsIndentation.Value)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
					}
				}
				else
				{
					if (this.bodyIndentation != null)
					{
						if (firstLine.Indentation <= this.bodyIndentation.Value)
						{
							throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
						}
					}
					else if (firstLine.Indentation.Size < this.baseIndentation.Size + 2)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
					}
					this.expressionsIndentation = new Indentation?(firstLine.Indentation);
				}
			}

			// Token: 0x06002504 RID: 9476 RVA: 0x000E61B4 File Offset: 0x000E43B4
			bool TmdlParser.IExpressionLineEvaluator.IsExpressionLine(TmdlTextLine line)
			{
				if (line.Indentation >= this.expressionsIndentation.Value)
				{
					return true;
				}
				if (TmdlParser.IsValidPropertyLine(line, this.container, this.baseIndentation, this.bodyIndentation))
				{
					return false;
				}
				if (this.parent != null && this.parent.IsKnownElemement(line))
				{
					return false;
				}
				switch (line.Type)
				{
				case TmdlLineType.ReferenceObject:
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatChildUnsupported(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				case TmdlLineType.Property:
				case TmdlLineType.OldSyntaxExpressionProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatPropertyUnsupported(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatUnknownKeyword(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				default:
					throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
				}
			}

			// Token: 0x06002505 RID: 9477 RVA: 0x000E62AB File Offset: 0x000E44AB
			bool TmdlParser.IExpressionLineEvaluator.IsKnownElemement(TmdlTextLine line)
			{
				return TmdlParser.IsValidPropertyLine(line, this.container, this.baseIndentation, this.bodyIndentation) || (this.parent != null && this.parent.IsKnownElemement(line));
			}

			// Token: 0x04000DCD RID: 3533
			private readonly TmdlParser.DocumentContext context;

			// Token: 0x04000DCE RID: 3534
			private readonly ITmdlPropertiesContainer container;

			// Token: 0x04000DCF RID: 3535
			private readonly Indentation baseIndentation;

			// Token: 0x04000DD0 RID: 3536
			private readonly TmdlParser.IExpressionLineEvaluator parent;

			// Token: 0x04000DD1 RID: 3537
			private Indentation? bodyIndentation;

			// Token: 0x04000DD2 RID: 3538
			private Indentation? expressionsIndentation;
		}

		// Token: 0x02000327 RID: 807
		private sealed class CollectionPropertiesContext : TmdlParser.IExpressionLineEvaluator
		{
			// Token: 0x06002506 RID: 9478 RVA: 0x000E62DF File Offset: 0x000E44DF
			public CollectionPropertiesContext(TmdlParser.DocumentContext context, ITmdlPropertiesContainer container, Indentation indentation)
			{
				this.context = context;
				this.container = container;
				this.indentation = indentation;
			}

			// Token: 0x06002507 RID: 9479 RVA: 0x000E62FC File Offset: 0x000E44FC
			public void ReadCollectionPropertiesBatches(ICollection<TmdlProperty[]> batches)
			{
				List<TmdlProperty> list = new List<TmdlProperty>();
				TmdlTextLine tmdlTextLine;
				do
				{
					tmdlTextLine = this.context.VerifyIndentation(this.indentation);
					bool flag;
					do
					{
						this.context.ReadProperty(this.container, list, null, tmdlTextLine.Indentation, this, null);
						tmdlTextLine = this.context.CurrentLine;
						TmdlLineType type = tmdlTextLine.Type;
						flag = (type == TmdlLineType.Property || type == TmdlLineType.UnnamedObjectOrSimplifiedProperty) && tmdlTextLine.Indentation > this.indentation;
					}
					while (!this.context.EOF && flag);
					if (list.Count > 0)
					{
						batches.Add(list.ToArray());
						list.Clear();
					}
				}
				while (!this.context.EOF && tmdlTextLine.Type == TmdlLineType.Property && tmdlTextLine.Indentation == this.indentation);
			}

			// Token: 0x06002508 RID: 9480 RVA: 0x000E63C7 File Offset: 0x000E45C7
			void TmdlParser.IExpressionLineEvaluator.Initialize(TmdlTextLine firstLine)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoCollectionExpression, this.context, null);
			}

			// Token: 0x06002509 RID: 9481 RVA: 0x000E63DC File Offset: 0x000E45DC
			bool TmdlParser.IExpressionLineEvaluator.IsExpressionLine(TmdlTextLine line)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoCollectionExpression, this.context, null);
			}

			// Token: 0x0600250A RID: 9482 RVA: 0x000E63F1 File Offset: 0x000E45F1
			bool TmdlParser.IExpressionLineEvaluator.IsKnownElemement(TmdlTextLine line)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoCollectionExpression, this.context, null);
			}

			// Token: 0x04000DD3 RID: 3539
			private readonly TmdlParser.DocumentContext context;

			// Token: 0x04000DD4 RID: 3540
			private readonly ITmdlPropertiesContainer container;

			// Token: 0x04000DD5 RID: 3541
			private readonly Indentation indentation;
		}

		// Token: 0x02000328 RID: 808
		private sealed class TranslationElementContext : TmdlParser.IExpressionLineEvaluator
		{
			// Token: 0x0600250B RID: 9483 RVA: 0x000E6406 File Offset: 0x000E4606
			public TranslationElementContext(TmdlParser.DocumentContext context, ITmdlTranslationElementContainer container, Indentation baseIndentation, TmdlParser.TranslationElementContext parent = null)
			{
				this.context = context;
				this.container = container;
				this.baseIndentation = baseIndentation;
				this.parent = parent;
			}

			// Token: 0x0600250C RID: 9484 RVA: 0x000E642C File Offset: 0x000E462C
			public TmdlTranslationElement ReadElement(TmdlTranslationElementInfo elementInfo)
			{
				TmdlSourceLocation currentLocation = this.context.GetCurrentLocation();
				TmdlLineType tmdlLineType = this.context.CurrentLine.Type;
				if (tmdlLineType != TmdlLineType.NamedObject && tmdlLineType != TmdlLineType.UnnamedObjectOrSimplifiedProperty)
				{
					throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(this.context.CurrentLine.Type.ToString("G")), this.context, null);
				}
				TmdlTextLine tmdlTextLine = this.context.VerifyIndentation(this.baseIndentation);
				if (elementInfo != null)
				{
					if (!tmdlTextLine.Keyword.Equals(elementInfo.ObjectType.ToString("G"), StringComparison.InvariantCultureIgnoreCase))
					{
						throw TmdlParser.CreateFormatException(ParsingError.MismatchObjectType, TomSR.Exception_TmdlFormatObjectMismatch(elementInfo.ObjectType.ToString("G"), tmdlTextLine.Keyword), this.context, null);
					}
				}
				else if (!this.container.TryGetTranslationElementInfo(tmdlTextLine.Keyword, out elementInfo))
				{
					throw TmdlParser.CreateFormatException(ParsingError.UnsupportedObjectType, TomSR.Exception_TmdlFormatObjectUnsupported(ClientHostingManager.MarkAsRestrictedInformation(tmdlTextLine.Keyword, InfoRestrictionType.CCON)), this.context, null);
				}
				this.elementInfo = elementInfo;
				ObjectName objectName;
				if (this.elementInfo.RequiresName)
				{
					if (this.context.CurrentLine.Type == TmdlLineType.UnnamedObjectOrSimplifiedProperty)
					{
						throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(tmdlTextLine.Type.ToString("G"), TomSR.TmdlFormatError_TypeNotIndicateName(elementInfo.ObjectType.ToString("G"))), this.context, null);
					}
					int num;
					string text;
					if (!ObjectName.TryParse(tmdlTextLine.Text, tmdlTextLine.FirstTokenIndex, out objectName, out num, out text))
					{
						throw TmdlParser.CreateFormatException(ParsingError.InvalidName, TomSR.Exception_TmdlFormatObjectNameParseError(ClientHostingManager.MarkAsRestrictedInformation(tmdlTextLine.Text, InfoRestrictionType.CCON), text), this.context, null);
					}
				}
				else
				{
					objectName = default(ObjectName);
				}
				TmdlTranslationElement tmdlTranslationElement = new TmdlTranslationElement(this.elementInfo.ObjectType)
				{
					Name = objectName,
					SourceLocation = currentLocation
				};
				if (!this.context.ReadLineSkipBlanks())
				{
					return tmdlTranslationElement;
				}
				TmdlTextLine currentLine = this.context.CurrentLine;
				tmdlLineType = currentLine.Type;
				if (tmdlLineType != TmdlLineType.NamedObject && tmdlLineType != TmdlLineType.Property && tmdlLineType != TmdlLineType.UnnamedObjectOrSimplifiedProperty)
				{
					throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(currentLine.Type.ToString("G")), this.context, null);
				}
				TmdlPropertyInfo tmdlPropertyInfo;
				TmdlParser.TranslationElementContext.NextLineType nextLineType = this.IdentifyAndValidateNextLine(currentLine, true, out tmdlPropertyInfo, out elementInfo);
				while (nextLineType != TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement)
				{
					if (nextLineType != TmdlParser.TranslationElementContext.NextLineType.Property)
					{
						if (nextLineType != TmdlParser.TranslationElementContext.NextLineType.ChildElement)
						{
							throw TomInternalException.Create("Invalid result coming out of IdentifyAndValidateNextLine - lineType={0}", new object[] { nextLineType });
						}
						nextLineType = this.ReadChildElement(tmdlTranslationElement, elementInfo, out tmdlPropertyInfo, out elementInfo);
					}
					else
					{
						nextLineType = this.ReadProperty(tmdlTranslationElement, tmdlPropertyInfo, out tmdlPropertyInfo, out elementInfo);
					}
				}
				return tmdlTranslationElement;
			}

			// Token: 0x0600250D RID: 9485 RVA: 0x000E66BC File Offset: 0x000E48BC
			void TmdlParser.IExpressionLineEvaluator.Initialize(TmdlTextLine firstLine)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoTranslationExpression, this.context, null);
			}

			// Token: 0x0600250E RID: 9486 RVA: 0x000E66D1 File Offset: 0x000E48D1
			bool TmdlParser.IExpressionLineEvaluator.IsExpressionLine(TmdlTextLine line)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoTranslationExpression, this.context, null);
			}

			// Token: 0x0600250F RID: 9487 RVA: 0x000E66E6 File Offset: 0x000E48E6
			bool TmdlParser.IExpressionLineEvaluator.IsKnownElemement(TmdlTextLine line)
			{
				throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatNoTranslationExpression, this.context, null);
			}

			// Token: 0x06002510 RID: 9488 RVA: 0x000E66FC File Offset: 0x000E48FC
			private TmdlParser.TranslationElementContext.NextLineType ReadProperty(TmdlTranslationElement result, TmdlPropertyInfo propertyInfo, out TmdlPropertyInfo nextPropertyInfo, out TmdlTranslationElementInfo nextElementInfo)
			{
				this.context.VerifyIndentation(this.bodyIndentation.Value);
				this.context.ReadProperty(this.elementInfo, result.Properties, null, this.bodyIndentation.Value, this, propertyInfo);
				if (this.context.EOF)
				{
					nextPropertyInfo = null;
					nextElementInfo = null;
					return TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement;
				}
				return this.IdentifyAndValidateNextLine(this.context.CurrentLine, true, out nextPropertyInfo, out nextElementInfo);
			}

			// Token: 0x06002511 RID: 9489 RVA: 0x000E6770 File Offset: 0x000E4970
			private TmdlParser.TranslationElementContext.NextLineType ReadChildElement(TmdlTranslationElement result, TmdlTranslationElementInfo elementInfo, out TmdlPropertyInfo nextPropertyInfo, out TmdlTranslationElementInfo nextElementInfo)
			{
				this.context.VerifyIndentation(this.bodyIndentation.Value);
				TmdlTranslationElement tmdlTranslationElement = new TmdlParser.TranslationElementContext(this.context, this.elementInfo, this.bodyIndentation.Value, this).ReadElement(elementInfo);
				if (tmdlTranslationElement != null)
				{
					result.Children.Add(tmdlTranslationElement);
				}
				if (this.context.EOF)
				{
					nextPropertyInfo = null;
					nextElementInfo = null;
					return TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement;
				}
				return this.IdentifyAndValidateNextLine(this.context.CurrentLine, false, out nextPropertyInfo, out nextElementInfo);
			}

			// Token: 0x06002512 RID: 9490 RVA: 0x000E67F4 File Offset: 0x000E49F4
			private TmdlParser.TranslationElementContext.NextLineType IdentifyAndValidateNextLine(TmdlTextLine line, bool allowProperty, out TmdlPropertyInfo propertyInfo, out TmdlTranslationElementInfo elementInfo)
			{
				propertyInfo = null;
				elementInfo = null;
				switch (line.Type)
				{
				case TmdlLineType.Description:
				case TmdlLineType.ReferenceObject:
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
					if (line.Indentation <= this.baseIndentation)
					{
						return TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement;
					}
					if (this.bodyIndentation == null)
					{
						this.bodyIndentation = new Indentation?(line.Indentation);
						return TmdlParser.TranslationElementContext.NextLineType.ChildElement;
					}
					if (line.Indentation == this.bodyIndentation.Value)
					{
						return TmdlParser.TranslationElementContext.NextLineType.ChildElement;
					}
					break;
				case TmdlLineType.Property:
					if (line.Indentation <= this.baseIndentation)
					{
						return TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement;
					}
					if (allowProperty && line.Indentation > this.baseIndentation)
					{
						if (this.bodyIndentation == null)
						{
							this.bodyIndentation = new Indentation?(line.Indentation);
							return TmdlParser.TranslationElementContext.NextLineType.Property;
						}
						if (line.Indentation == this.bodyIndentation.Value)
						{
							return TmdlParser.TranslationElementContext.NextLineType.Property;
						}
					}
					break;
				case TmdlLineType.OldSyntaxExpressionProperty:
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					if (line.Indentation <= this.baseIndentation)
					{
						return TmdlParser.TranslationElementContext.NextLineType.NotInCurrentElement;
					}
					if (this.bodyIndentation == null)
					{
						this.bodyIndentation = new Indentation?(line.Indentation);
						return this.IdentifyKeyword(line.Keyword, allowProperty, out propertyInfo, out elementInfo);
					}
					if (line.Indentation == this.bodyIndentation.Value)
					{
						return this.IdentifyKeyword(line.Keyword, allowProperty, out propertyInfo, out elementInfo);
					}
					break;
				}
				throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
			}

			// Token: 0x06002513 RID: 9491 RVA: 0x000E6978 File Offset: 0x000E4B78
			private TmdlParser.TranslationElementContext.NextLineType IdentifyKeyword(string keyword, bool allowProperty, out TmdlPropertyInfo propertyInfo, out TmdlTranslationElementInfo elementInfo)
			{
				if (this.elementInfo.TryGetTranslationElementInfo(keyword, out elementInfo))
				{
					propertyInfo = null;
					return TmdlParser.TranslationElementContext.NextLineType.ChildElement;
				}
				if (allowProperty && this.elementInfo.TryGetPropertyInfo(keyword, out propertyInfo))
				{
					elementInfo = null;
					return TmdlParser.TranslationElementContext.NextLineType.Property;
				}
				throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatUnknownKeyword(ClientHostingManager.MarkAsRestrictedInformation(keyword, InfoRestrictionType.CCON)), this.context, null);
			}

			// Token: 0x04000DD6 RID: 3542
			private readonly TmdlParser.DocumentContext context;

			// Token: 0x04000DD7 RID: 3543
			private readonly ITmdlTranslationElementContainer container;

			// Token: 0x04000DD8 RID: 3544
			private readonly Indentation baseIndentation;

			// Token: 0x04000DD9 RID: 3545
			private readonly TmdlParser.TranslationElementContext parent;

			// Token: 0x04000DDA RID: 3546
			private Indentation? bodyIndentation;

			// Token: 0x04000DDB RID: 3547
			private TmdlTranslationElementInfo elementInfo;

			// Token: 0x0200045C RID: 1116
			private enum NextLineType
			{
				// Token: 0x0400146E RID: 5230
				NotInCurrentElement,
				// Token: 0x0400146F RID: 5231
				Property,
				// Token: 0x04001470 RID: 5232
				ChildElement
			}
		}

		// Token: 0x02000329 RID: 809
		private sealed class ObjectContext : TmdlParser.IExpressionLineEvaluator
		{
			// Token: 0x06002514 RID: 9492 RVA: 0x000E69CB File Offset: 0x000E4BCB
			public ObjectContext(TmdlParser.DocumentContext context, ITmdlObjectContainer container, Indentation? baseIndentation = null, TmdlParser.ObjectContext parent = null)
			{
				this.context = context;
				this.parent = parent;
				this.container = container;
				this.baseIndentation = ((baseIndentation != null) ? baseIndentation.Value : Indentation.Empty);
			}

			// Token: 0x06002515 RID: 9493 RVA: 0x000E6A08 File Offset: 0x000E4C08
			public TmdlObject ReadObject(TmdlObjectInfo objectInfo)
			{
				TmdlSourceLocation currentLocation = this.context.GetCurrentLocation();
				ICollection<TmdlTextLine> collection = new List<TmdlTextLine>();
				if (this.context.CurrentLine.Type == TmdlLineType.Description)
				{
					this.context.CollectDescriptionLines(collection, this.baseIndentation);
					if (this.context.EOF)
					{
						throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(TmdlLineType.Description.ToString("G"), TomSR.TmdlFormatError_DescriptionWithoutObject), this.context, null);
					}
				}
				bool flag;
				switch (this.context.CurrentLine.Type)
				{
				case TmdlLineType.ReferenceObject:
					if (collection.Count > 0)
					{
						throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(TmdlLineType.ReferenceObject.ToString("G"), TomSR.TmdlFormatError_RefAfterDescription), this.context, null);
					}
					flag = true;
					goto IL_0113;
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					flag = false;
					goto IL_0113;
				}
				throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(this.context.CurrentLine.Type.ToString("G")), this.context, null);
				IL_0113:
				TmdlTextLine tmdlTextLine = this.context.VerifyIndentation(this.baseIndentation);
				if (objectInfo != null)
				{
					if (!tmdlTextLine.Keyword.Equals(objectInfo.Keyword, StringComparison.InvariantCultureIgnoreCase))
					{
						throw TmdlParser.CreateFormatException(ParsingError.MismatchObjectType, TomSR.Exception_TmdlFormatObjectMismatch(objectInfo.ObjectType.ToString("G"), tmdlTextLine.Keyword), this.context, null);
					}
				}
				else if (!this.container.TryGetObjectInfo(tmdlTextLine.Keyword, out objectInfo))
				{
					throw TmdlParser.CreateFormatException(ParsingError.UnsupportedObjectType, TomSR.Exception_TmdlFormatObjectUnsupported(ClientHostingManager.MarkAsRestrictedInformation(tmdlTextLine.Keyword, InfoRestrictionType.CCON)), this.context, null);
				}
				if (objectInfo.HasVariants && objectInfo.Variants.Count == 1)
				{
					objectInfo = objectInfo.Variants.Single<KeyValuePair<string, TmdlObjectInfo>>().Value;
				}
				this.objectInfo = objectInfo;
				ObjectName objectName;
				int num;
				if (objectInfo.RequiresName || objectInfo.ObjectType == ObjectType.Database)
				{
					switch (tmdlTextLine.Type)
					{
					case TmdlLineType.ReferenceObject:
					case TmdlLineType.NamedObject:
					case TmdlLineType.NamedObjectWithDefaultProperty:
					{
						string text;
						if (!ObjectName.TryParse(tmdlTextLine.Text, tmdlTextLine.FirstTokenIndex, out objectName, out num, out text))
						{
							throw TmdlParser.CreateFormatException(ParsingError.InvalidName, TomSR.Exception_TmdlFormatObjectNameParseError(ClientHostingManager.MarkAsRestrictedInformation(tmdlTextLine.Text, InfoRestrictionType.CCON), text), this.context, null);
						}
						if (num == -1)
						{
							goto IL_046E;
						}
						while (num < tmdlTextLine.Text.Length && char.IsWhiteSpace(tmdlTextLine.Text, num))
						{
							num++;
						}
						if (num >= tmdlTextLine.Text.Length)
						{
							num = -1;
							goto IL_046E;
						}
						if (tmdlTextLine.Text[num] != '=')
						{
							throw TmdlParser.CreateFormatException(ParsingError.InvalidObjectHeader, TomSR.Exception_TmdlFormatObjectNameParseError(ClientHostingManager.MarkAsRestrictedInformation(tmdlTextLine.Text, InfoRestrictionType.CCON), TomSR.ObjetNameParseError_NameIsFollowedByInvalidToken), this.context, null);
						}
						goto IL_046E;
					}
					case TmdlLineType.ElementWithDefaultPropertyOrExpression:
						throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(tmdlTextLine.Type.ToString("G"), TomSR.TmdlFormatError_TypeNotIndicateName(objectInfo.ObjectType.ToString("G"))), this.context, null);
					case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
						if (objectInfo.ObjectType != ObjectType.Database)
						{
							throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(tmdlTextLine.Type.ToString("G"), TomSR.TmdlFormatError_TypeNotIndicateName(objectInfo.ObjectType.ToString("G"))), this.context, null);
						}
						objectName = new ObjectName(new string[1]);
						num = -1;
						goto IL_046E;
					}
					throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(tmdlTextLine.Type.ToString("G")), this.context, null);
				}
				switch (tmdlTextLine.Type)
				{
				case TmdlLineType.ReferenceObject:
					num = tmdlTextLine.Text.IndexOf('=', tmdlTextLine.FirstTokenIndex);
					goto IL_0466;
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
					throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(tmdlTextLine.Type.ToString("G"), TomSR.TmdlFormatError_TypeIndicateName(objectInfo.ObjectType.ToString("G"))), this.context, null);
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
					num = tmdlTextLine.FirstTokenIndex;
					goto IL_0466;
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					num = -1;
					goto IL_0466;
				}
				throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(tmdlTextLine.Type.ToString("G")), this.context, null);
				IL_0466:
				objectName = default(ObjectName);
				IL_046E:
				TmdlObject tmdlObject;
				if (this.objectInfo.ObjectType == ObjectType.Null)
				{
					(tmdlObject = new TmdlObject(this.objectInfo.PropertyName)).SourceLocation = currentLocation;
				}
				else
				{
					TmdlObject tmdlObject2 = new TmdlObject(this.objectInfo.ObjectType);
					tmdlObject2.Name = objectName;
					tmdlObject2.IsReference = flag;
					tmdlObject = tmdlObject2;
					tmdlObject2.SourceLocation = currentLocation;
				}
				TmdlObject tmdlObject3 = tmdlObject;
				if (collection.Count > 0)
				{
					tmdlObject3.Description = collection.Select((TmdlTextLine x) => x.Text).ToArray<string>();
				}
				TmdlPropertyInfo tmdlPropertyInfo;
				bool flag2 = objectInfo.IsDefaultPropertyAllowed(out tmdlPropertyInfo);
				if (num != -1 && flag)
				{
					throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(TmdlLineType.ReferenceObject.ToString("G"), TomSR.TmdlFormatError_RefWithDefaultProperty), this.context, null);
				}
				if (num != -1 && !flag2)
				{
					throw TmdlParser.CreateFormatException(ParsingError.PropertyValueMismatch, TomSR.Exception_TmdlFormatObjectDefaultProperty(this.objectInfo.ObjectType.ToString("G")), this.context, null);
				}
				bool flag3;
				if (num != -1)
				{
					tmdlObject3.DefaultProperty = this.context.ReadDefaultProperty(tmdlPropertyInfo, num, this, out flag3);
					if (this.context.EOF)
					{
						return tmdlObject3;
					}
				}
				else
				{
					flag3 = true;
				}
				TmdlTextLine tmdlTextLine2;
				if (flag3)
				{
					if (!this.context.ReadLineSkipBlanks())
					{
						return tmdlObject3;
					}
					tmdlTextLine2 = this.context.CurrentLine;
					switch (tmdlTextLine2.Type)
					{
					case TmdlLineType.Description:
					case TmdlLineType.ReferenceObject:
					case TmdlLineType.NamedObject:
					case TmdlLineType.NamedObjectWithDefaultProperty:
					case TmdlLineType.ElementWithDefaultPropertyOrExpression:
					case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
						break;
					case TmdlLineType.Property:
					case TmdlLineType.OldSyntaxExpressionProperty:
						if (flag)
						{
							throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType2(tmdlTextLine2.Type.ToString("G"), TomSR.TmdlFormatError_RefWithProperty), this.context, null);
						}
						break;
					default:
						throw TmdlParser.CreateFormatException(ParsingError.InvalidLineType, TomSR.Exception_TmdlFormatUnexpectedLineType(tmdlTextLine2.Type.ToString("G")), this.context, null);
					}
				}
				else
				{
					tmdlTextLine2 = this.context.CurrentLine;
				}
				TmdlPropertyInfo tmdlPropertyInfo2;
				TmdlParser.ObjectContext.NextLineType nextLineType = this.IdentifyAndValidateNextLine(tmdlTextLine2, !flag, out tmdlPropertyInfo2, out objectInfo);
				while (nextLineType != TmdlParser.ObjectContext.NextLineType.NotInCurrentObject)
				{
					if (nextLineType != TmdlParser.ObjectContext.NextLineType.Property)
					{
						if (nextLineType != TmdlParser.ObjectContext.NextLineType.ChildObject)
						{
							throw TomInternalException.Create("Invalid result coming out of IdentifyAndValidateNextLine - lineType={0}", new object[] { nextLineType });
						}
						nextLineType = this.ReadChildObject(tmdlObject3, objectInfo, out tmdlPropertyInfo2, out objectInfo);
					}
					else
					{
						nextLineType = this.ReadProperty(tmdlObject3, tmdlPropertyInfo2, out tmdlPropertyInfo2, out objectInfo);
					}
				}
				if (this.deprecatedProperties != null && this.deprecatedProperties.Count > 0)
				{
					foreach (TmdlProperty tmdlProperty in this.deprecatedProperties)
					{
						if (tmdlProperty.Value.Type == TmdlValueType.MetadataObject)
						{
							TmdlObject @object = ((TmdlMetadataObjectValue)tmdlProperty.Value).Object;
							if (string.Compare(@object.ObjectType.ToString("G"), tmdlProperty.Name, StringComparison.InvariantCultureIgnoreCase) != 0)
							{
								@object.Name = new ObjectName(new string[] { tmdlProperty.Name });
							}
							tmdlObject3.Children.Add(@object);
						}
						else
						{
							tmdlObject3.AddDeprecatedProperty(tmdlProperty);
						}
					}
				}
				return tmdlObject3;
			}

			// Token: 0x06002516 RID: 9494 RVA: 0x000E71B4 File Offset: 0x000E53B4
			void TmdlParser.IExpressionLineEvaluator.Initialize(TmdlTextLine firstLine)
			{
				if (this.expressionsIndentation != null)
				{
					if (firstLine.Indentation != this.expressionsIndentation.Value)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
					}
				}
				else
				{
					if (this.bodyIndentation != null)
					{
						if (firstLine.Indentation <= this.bodyIndentation.Value)
						{
							throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
						}
					}
					else if (firstLine.Indentation.Size < this.baseIndentation.Size + 2)
					{
						throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
					}
					this.expressionsIndentation = new Indentation?(firstLine.Indentation);
				}
			}

			// Token: 0x06002517 RID: 9495 RVA: 0x000E7274 File Offset: 0x000E5474
			bool TmdlParser.IExpressionLineEvaluator.IsExpressionLine(TmdlTextLine line)
			{
				if (line.Indentation >= this.expressionsIndentation.Value)
				{
					return true;
				}
				if (TmdlParser.IsValidPropertyLine(line, this.objectInfo, this.baseIndentation, this.bodyIndentation))
				{
					return false;
				}
				if (TmdlParser.IsValidChildObjectLine(line, this.objectInfo, this.baseIndentation, this.bodyIndentation))
				{
					return false;
				}
				if (TmdlParser.IsValidSiblingObjectLine(line, this.container, this.baseIndentation))
				{
					return false;
				}
				if (this.parent != null && ((TmdlParser.IExpressionLineEvaluator)this.parent).IsKnownElemement(line))
				{
					return false;
				}
				switch (line.Type)
				{
				case TmdlLineType.ReferenceObject:
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatChildUnsupported(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				case TmdlLineType.Property:
				case TmdlLineType.OldSyntaxExpressionProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatPropertyUnsupported(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatUnknownKeyword(ClientHostingManager.MarkAsRestrictedInformation(line.Keyword, InfoRestrictionType.CCON)), this.context, null);
				default:
					throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
				}
			}

			// Token: 0x06002518 RID: 9496 RVA: 0x000E73A0 File Offset: 0x000E55A0
			bool TmdlParser.IExpressionLineEvaluator.IsKnownElemement(TmdlTextLine line)
			{
				return TmdlParser.IsValidPropertyLine(line, this.objectInfo, this.baseIndentation, this.bodyIndentation) || TmdlParser.IsValidChildObjectLine(line, this.objectInfo, this.baseIndentation, this.bodyIndentation) || TmdlParser.IsValidSiblingObjectLine(line, this.container, this.baseIndentation) || (this.parent != null && ((TmdlParser.IExpressionLineEvaluator)this.parent).IsKnownElemement(line));
			}

			// Token: 0x06002519 RID: 9497 RVA: 0x000E7414 File Offset: 0x000E5614
			private TmdlParser.ObjectContext.NextLineType ReadProperty(TmdlObject result, TmdlPropertyInfo propertyInfo, out TmdlPropertyInfo nextPropertyInfo, out TmdlObjectInfo nextObjectInfo)
			{
				if (this.deprecatedProperties == null)
				{
					this.deprecatedProperties = new List<TmdlProperty>();
				}
				this.context.VerifyIndentation(this.bodyIndentation.Value);
				this.context.ReadProperty(this.objectInfo, result.Properties, this.deprecatedProperties, this.bodyIndentation.Value, this, propertyInfo);
				if (this.context.EOF)
				{
					nextPropertyInfo = null;
					nextObjectInfo = null;
					return TmdlParser.ObjectContext.NextLineType.NotInCurrentObject;
				}
				return this.IdentifyAndValidateNextLine(this.context.CurrentLine, true, out nextPropertyInfo, out nextObjectInfo);
			}

			// Token: 0x0600251A RID: 9498 RVA: 0x000E74A0 File Offset: 0x000E56A0
			private TmdlParser.ObjectContext.NextLineType ReadChildObject(TmdlObject result, TmdlObjectInfo objectInfo, out TmdlPropertyInfo nextPropertyInfo, out TmdlObjectInfo nextObjectInfo)
			{
				this.context.VerifyIndentation(this.bodyIndentation.Value);
				TmdlObject tmdlObject = new TmdlParser.ObjectContext(this.context, this.objectInfo, new Indentation?(this.bodyIndentation.Value), this).ReadObject(objectInfo);
				if (tmdlObject != null)
				{
					result.Children.Add(tmdlObject);
				}
				if (this.context.EOF)
				{
					nextPropertyInfo = null;
					nextObjectInfo = null;
					return TmdlParser.ObjectContext.NextLineType.NotInCurrentObject;
				}
				return this.IdentifyAndValidateNextLine(this.context.CurrentLine, false, out nextPropertyInfo, out nextObjectInfo);
			}

			// Token: 0x0600251B RID: 9499 RVA: 0x000E7528 File Offset: 0x000E5728
			private TmdlParser.ObjectContext.NextLineType IdentifyAndValidateNextLine(TmdlTextLine line, bool allowProperty, out TmdlPropertyInfo propertyInfo, out TmdlObjectInfo objectInfo)
			{
				propertyInfo = null;
				objectInfo = null;
				switch (line.Type)
				{
				case TmdlLineType.Description:
				case TmdlLineType.ReferenceObject:
				case TmdlLineType.NamedObject:
				case TmdlLineType.NamedObjectWithDefaultProperty:
					if (line.Indentation <= this.baseIndentation)
					{
						return TmdlParser.ObjectContext.NextLineType.NotInCurrentObject;
					}
					if (this.bodyIndentation == null)
					{
						if (this.expressionsIndentation != null && (line.Indentation <= this.baseIndentation || line.Indentation >= this.expressionsIndentation.Value))
						{
							throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
						}
						this.bodyIndentation = new Indentation?(line.Indentation);
						return TmdlParser.ObjectContext.NextLineType.ChildObject;
					}
					else if (line.Indentation == this.bodyIndentation.Value)
					{
						return TmdlParser.ObjectContext.NextLineType.ChildObject;
					}
					break;
				case TmdlLineType.Property:
				case TmdlLineType.OldSyntaxExpressionProperty:
					if (allowProperty && line.Indentation > this.baseIndentation)
					{
						if (this.bodyIndentation == null)
						{
							if (this.expressionsIndentation != null && (line.Indentation <= this.baseIndentation || line.Indentation >= this.expressionsIndentation.Value))
							{
								throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
							}
							this.bodyIndentation = new Indentation?(line.Indentation);
							return TmdlParser.ObjectContext.NextLineType.Property;
						}
						else if (line.Indentation == this.bodyIndentation.Value)
						{
							return TmdlParser.ObjectContext.NextLineType.Property;
						}
					}
					break;
				case TmdlLineType.ElementWithDefaultPropertyOrExpression:
				case TmdlLineType.UnnamedObjectOrSimplifiedProperty:
					if (line.Indentation <= this.baseIndentation)
					{
						return TmdlParser.ObjectContext.NextLineType.NotInCurrentObject;
					}
					if (this.bodyIndentation == null)
					{
						if (this.expressionsIndentation != null && (line.Indentation <= this.baseIndentation || line.Indentation >= this.expressionsIndentation.Value))
						{
							throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
						}
						this.bodyIndentation = new Indentation?(line.Indentation);
						return this.IdentifyKeyword(line.Keyword, allowProperty, out propertyInfo, out objectInfo);
					}
					else if (line.Indentation == this.bodyIndentation.Value)
					{
						return this.IdentifyKeyword(line.Keyword, allowProperty, out propertyInfo, out objectInfo);
					}
					break;
				}
				throw TmdlParser.CreateFormatException(ParsingError.Indentation, TomSR.Exception_TmdlFormatIndentation, this.context, null);
			}

			// Token: 0x0600251C RID: 9500 RVA: 0x000E777C File Offset: 0x000E597C
			private TmdlParser.ObjectContext.NextLineType IdentifyKeyword(string keyword, bool allowProperty, out TmdlPropertyInfo propertyInfo, out TmdlObjectInfo objectInfo)
			{
				if (this.objectInfo.TryGetObjectInfo(keyword, out objectInfo))
				{
					propertyInfo = null;
					return TmdlParser.ObjectContext.NextLineType.ChildObject;
				}
				if (allowProperty && this.objectInfo.TryGetPropertyInfo(keyword, out propertyInfo))
				{
					objectInfo = null;
					return TmdlParser.ObjectContext.NextLineType.Property;
				}
				throw TmdlParser.CreateFormatException(ParsingError.UnknownKeyword, TomSR.Exception_TmdlFormatUnknownKeyword(ClientHostingManager.MarkAsRestrictedInformation(keyword, InfoRestrictionType.CCON)), this.context, null);
			}

			// Token: 0x04000DDC RID: 3548
			private readonly TmdlParser.DocumentContext context;

			// Token: 0x04000DDD RID: 3549
			private readonly TmdlParser.ObjectContext parent;

			// Token: 0x04000DDE RID: 3550
			private readonly ITmdlObjectContainer container;

			// Token: 0x04000DDF RID: 3551
			private readonly Indentation baseIndentation;

			// Token: 0x04000DE0 RID: 3552
			private TmdlObjectInfo objectInfo;

			// Token: 0x04000DE1 RID: 3553
			private Indentation? bodyIndentation;

			// Token: 0x04000DE2 RID: 3554
			private Indentation? expressionsIndentation;

			// Token: 0x04000DE3 RID: 3555
			private ICollection<TmdlProperty> deprecatedProperties;

			// Token: 0x0200045D RID: 1117
			private enum NextLineType
			{
				// Token: 0x04001472 RID: 5234
				NotInCurrentObject,
				// Token: 0x04001473 RID: 5235
				Property,
				// Token: 0x04001474 RID: 5236
				ChildObject
			}
		}
	}
}
