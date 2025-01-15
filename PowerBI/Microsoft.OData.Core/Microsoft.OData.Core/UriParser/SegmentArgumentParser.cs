using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015D RID: 349
	internal sealed class SegmentArgumentParser
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x000036A9 File Offset: 0x000018A9
		private SegmentArgumentParser()
		{
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00034113 File Offset: 0x00032313
		private SegmentArgumentParser(Dictionary<string, string> namedValues, List<string> positionalValues, bool keysAsSegment, bool enableUriTemplateParsing)
		{
			this.namedValues = namedValues;
			this.positionalValues = positionalValues;
			this.keysAsSegment = keysAsSegment;
			this.enableUriTemplateParsing = enableUriTemplateParsing;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00034138 File Offset: 0x00032338
		public bool AreValuesNamed
		{
			get
			{
				return this.namedValues != null;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00034143 File Offset: 0x00032343
		public bool IsEmpty
		{
			get
			{
				return this == SegmentArgumentParser.Empty;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003414D File Offset: 0x0003234D
		public IDictionary<string, string> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00034155 File Offset: 0x00032355
		public IList<string> PositionalValues
		{
			get
			{
				return this.positionalValues;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0003415D File Offset: 0x0003235D
		public bool KeyAsSegment
		{
			get
			{
				return this.keysAsSegment;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00034165 File Offset: 0x00032365
		public int ValueCount
		{
			get
			{
				if (this == SegmentArgumentParser.Empty)
				{
					return 0;
				}
				if (this.namedValues != null)
				{
					return this.namedValues.Count;
				}
				return this.positionalValues.Count;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00034190 File Offset: 0x00032390
		public void AddNamedValue(string key, string value)
		{
			SegmentArgumentParser.CreateIfNull<Dictionary<string, string>>(ref this.namedValues);
			if (!this.namedValues.ContainsKey(key))
			{
				this.namedValues[key] = value;
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000341B8 File Offset: 0x000323B8
		public static bool TryParseKeysFromUri(string text, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
		{
			return SegmentArgumentParser.TryParseFromUri(text, out instance, enableUriTemplateParsing);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000341C2 File Offset: 0x000323C2
		public static SegmentArgumentParser FromSegment(string segmentText, bool enableUriTemplateParsing)
		{
			return new SegmentArgumentParser(null, new List<string> { segmentText }, true, enableUriTemplateParsing);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000341D8 File Offset: 0x000323D8
		public static bool TryParseNullableTokens(string text, out SegmentArgumentParser instance)
		{
			return SegmentArgumentParser.TryParseFromUri(text, out instance, false);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000341E4 File Offset: 0x000323E4
		public bool TryConvertValues(IEdmEntityType targetEntityType, out IEnumerable<KeyValuePair<string, object>> keyPairs, ODataUriResolver resolver)
		{
			if (this.NamedValues != null)
			{
				keyPairs = resolver.ResolveKeys(targetEntityType, this.NamedValues, new Func<IEdmTypeReference, string, object>(this.ConvertValueWrapper));
			}
			else
			{
				keyPairs = resolver.ResolveKeys(targetEntityType, this.PositionalValues, new Func<IEdmTypeReference, string, object>(this.ConvertValueWrapper));
			}
			return true;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00034234 File Offset: 0x00032434
		private object ConvertValueWrapper(IEdmTypeReference typeReference, string valueText)
		{
			object obj;
			if (!this.TryConvertValue(typeReference, valueText, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00034250 File Offset: 0x00032450
		private bool TryConvertValue(IEdmTypeReference typeReference, string valueText, out object convertedValue)
		{
			UriTemplateExpression uriTemplateExpression;
			if (this.enableUriTemplateParsing && UriTemplateParser.TryParseLiteral(valueText, typeReference, out uriTemplateExpression))
			{
				convertedValue = uriTemplateExpression;
				return true;
			}
			if (!typeReference.IsEnum())
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitive();
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType((IEdmPrimitiveType)edmPrimitiveTypeReference.Definition, edmPrimitiveTypeReference.IsNullable);
				LiteralParser literalParser = LiteralParser.ForKeys(this.keysAsSegment);
				return literalParser.TryParseLiteral(primitiveClrType, valueText, out convertedValue);
			}
			QueryNode queryNode = null;
			if (EnumBinder.TryBindIdentifier(valueText, typeReference.AsEnum(), null, out queryNode))
			{
				convertedValue = ((ConstantNode)queryNode).Value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000342DC File Offset: 0x000324DC
		private static bool TryParseFromUri(string text, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
		{
			Dictionary<string, string> dictionary = null;
			List<string> list = null;
			ExpressionLexer expressionLexer = new ExpressionLexer("(" + text + ")", true, false);
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(800, expressionLexer);
			FunctionParameterToken[] array = new FunctionCallParser(expressionLexer, uriQueryExpressionParser).ParseArgumentListOrEntityKeyList(null);
			if (expressionLexer.CurrentToken.Kind != ExpressionTokenKind.End)
			{
				instance = null;
				return false;
			}
			if (array.Length == 0)
			{
				instance = SegmentArgumentParser.Empty;
				return true;
			}
			foreach (FunctionParameterToken functionParameterToken in array)
			{
				string text2 = null;
				LiteralToken literalToken = functionParameterToken.ValueToken as LiteralToken;
				if (literalToken != null)
				{
					text2 = literalToken.OriginalText;
					if (!enableUriTemplateParsing && UriTemplateParser.IsValidTemplateLiteral(text2))
					{
						instance = null;
						return false;
					}
				}
				else
				{
					DottedIdentifierToken dottedIdentifierToken = functionParameterToken.ValueToken as DottedIdentifierToken;
					if (dottedIdentifierToken != null)
					{
						text2 = dottedIdentifierToken.Identifier;
					}
				}
				if (text2 == null)
				{
					instance = null;
					return false;
				}
				if (functionParameterToken.ParameterName == null)
				{
					if (dictionary != null)
					{
						instance = null;
						return false;
					}
					SegmentArgumentParser.CreateIfNull<List<string>>(ref list);
					list.Add(text2);
				}
				else
				{
					if (list != null)
					{
						instance = null;
						return false;
					}
					SegmentArgumentParser.CreateIfNull<Dictionary<string, string>>(ref dictionary);
					dictionary.Add(functionParameterToken.ParameterName, text2);
				}
			}
			instance = new SegmentArgumentParser(dictionary, list, false, enableUriTemplateParsing);
			return true;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00034406 File Offset: 0x00032606
		private static void CreateIfNull<T>(ref T value) where T : new()
		{
			if (value == null)
			{
				value = new T();
			}
		}

		// Token: 0x04000820 RID: 2080
		private static readonly SegmentArgumentParser Empty = new SegmentArgumentParser();

		// Token: 0x04000821 RID: 2081
		private readonly bool keysAsSegment;

		// Token: 0x04000822 RID: 2082
		private readonly bool enableUriTemplateParsing;

		// Token: 0x04000823 RID: 2083
		private Dictionary<string, string> namedValues;

		// Token: 0x04000824 RID: 2084
		private List<string> positionalValues;
	}
}
