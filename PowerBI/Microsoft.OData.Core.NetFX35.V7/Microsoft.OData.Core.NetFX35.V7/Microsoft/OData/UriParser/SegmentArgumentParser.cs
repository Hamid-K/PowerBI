using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011A RID: 282
	internal sealed class SegmentArgumentParser
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x00002CFE File Offset: 0x00000EFE
		private SegmentArgumentParser()
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00025EF3 File Offset: 0x000240F3
		private SegmentArgumentParser(Dictionary<string, string> namedValues, List<string> positionalValues, bool keysAsSegment, bool enableUriTemplateParsing)
		{
			this.namedValues = namedValues;
			this.positionalValues = positionalValues;
			this.keysAsSegment = keysAsSegment;
			this.enableUriTemplateParsing = enableUriTemplateParsing;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00025F18 File Offset: 0x00024118
		public bool AreValuesNamed
		{
			get
			{
				return this.namedValues != null;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00025F23 File Offset: 0x00024123
		public bool IsEmpty
		{
			get
			{
				return this == SegmentArgumentParser.Empty;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00025F2D File Offset: 0x0002412D
		public IDictionary<string, string> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00025F35 File Offset: 0x00024135
		public IList<string> PositionalValues
		{
			get
			{
				return this.positionalValues;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00025F3D File Offset: 0x0002413D
		public bool KeyAsSegment
		{
			get
			{
				return this.keysAsSegment;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00025F45 File Offset: 0x00024145
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

		// Token: 0x06000D20 RID: 3360 RVA: 0x00025F70 File Offset: 0x00024170
		public void AddNamedValue(string key, string value)
		{
			SegmentArgumentParser.CreateIfNull<Dictionary<string, string>>(ref this.namedValues);
			if (!this.namedValues.ContainsKey(key))
			{
				this.namedValues[key] = value;
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00025F98 File Offset: 0x00024198
		public static bool TryParseKeysFromUri(string text, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
		{
			return SegmentArgumentParser.TryParseFromUri(text, out instance, enableUriTemplateParsing);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00025FA2 File Offset: 0x000241A2
		public static SegmentArgumentParser FromSegment(string segmentText, bool enableUriTemplateParsing)
		{
			Dictionary<string, string> dictionary = null;
			List<string> list = new List<string>();
			list.Add(segmentText);
			return new SegmentArgumentParser(dictionary, list, true, enableUriTemplateParsing);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00025FB8 File Offset: 0x000241B8
		public static bool TryParseNullableTokens(string text, out SegmentArgumentParser instance)
		{
			return SegmentArgumentParser.TryParseFromUri(text, out instance, false);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00025FC4 File Offset: 0x000241C4
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

		// Token: 0x06000D25 RID: 3365 RVA: 0x00026014 File Offset: 0x00024214
		private object ConvertValueWrapper(IEdmTypeReference typeReference, string valueText)
		{
			object obj;
			if (!this.TryConvertValue(typeReference, valueText, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00026030 File Offset: 0x00024230
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
				convertedValue = queryNode;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000260B0 File Offset: 0x000242B0
		private static bool TryParseFromUri(string text, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
		{
			Dictionary<string, string> dictionary = null;
			List<string> list = null;
			ExpressionLexer expressionLexer = new ExpressionLexer("(" + text + ")", true, false);
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(800, expressionLexer);
			FunctionParameterToken[] array = new FunctionCallParser(expressionLexer, uriQueryExpressionParser).ParseArgumentListOrEntityKeyList();
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

		// Token: 0x06000D28 RID: 3368 RVA: 0x000261D9 File Offset: 0x000243D9
		private static void CreateIfNull<T>(ref T value) where T : new()
		{
			if (value == null)
			{
				value = new T();
			}
		}

		// Token: 0x0400070A RID: 1802
		private static readonly SegmentArgumentParser Empty = new SegmentArgumentParser();

		// Token: 0x0400070B RID: 1803
		private readonly bool keysAsSegment;

		// Token: 0x0400070C RID: 1804
		private readonly bool enableUriTemplateParsing;

		// Token: 0x0400070D RID: 1805
		private Dictionary<string, string> namedValues;

		// Token: 0x0400070E RID: 1806
		private List<string> positionalValues;
	}
}
