using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000210 RID: 528
	internal sealed class SegmentArgumentParser
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x00046133 File Offset: 0x00044333
		private SegmentArgumentParser()
		{
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004613B File Offset: 0x0004433B
		private SegmentArgumentParser(Dictionary<string, string> namedValues, List<string> positionalValues, bool keysAsSegment, bool enableUriTemplateParsing)
		{
			this.namedValues = namedValues;
			this.positionalValues = positionalValues;
			this.keysAsSegment = keysAsSegment;
			this.enableUriTemplateParsing = enableUriTemplateParsing;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00046160 File Offset: 0x00044360
		public bool AreValuesNamed
		{
			get
			{
				return this.namedValues != null;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004616E File Offset: 0x0004436E
		public bool IsEmpty
		{
			get
			{
				return this == SegmentArgumentParser.Empty;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00046178 File Offset: 0x00044378
		public IDictionary<string, string> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00046180 File Offset: 0x00044380
		public IList<string> PositionalValues
		{
			get
			{
				return this.positionalValues;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00046188 File Offset: 0x00044388
		public bool KeyAsSegment
		{
			get
			{
				return this.keysAsSegment;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00046190 File Offset: 0x00044390
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

		// Token: 0x06001341 RID: 4929 RVA: 0x000461BB File Offset: 0x000443BB
		public void AddNamedValue(string key, string value)
		{
			SegmentArgumentParser.CreateIfNull<Dictionary<string, string>>(ref this.namedValues);
			if (!this.namedValues.ContainsKey(key))
			{
				this.namedValues[key] = value;
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000461E3 File Offset: 0x000443E3
		public static bool TryParseKeysFromUri(string text, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
		{
			return SegmentArgumentParser.TryParseFromUri(text, true, false, out instance, enableUriTemplateParsing);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000461F0 File Offset: 0x000443F0
		public static SegmentArgumentParser FromSegment(string segmentText, bool enableUriTemplateParsing)
		{
			Dictionary<string, string> dictionary = null;
			List<string> list = new List<string>();
			list.Add(segmentText);
			return new SegmentArgumentParser(dictionary, list, true, enableUriTemplateParsing);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00046213 File Offset: 0x00044413
		public static bool TryParseNullableTokens(string text, out SegmentArgumentParser instance)
		{
			return SegmentArgumentParser.TryParseFromUri(text, false, true, out instance, false);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00046220 File Offset: 0x00044420
		public bool TryConvertValues(IEdmEntityType targetEntityType, out IEnumerable<KeyValuePair<string, object>> keyPairs, ODataUriResolver resolver)
		{
			Enumerable.ToList<IEdmStructuralProperty>(targetEntityType.Key());
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

		// Token: 0x06001346 RID: 4934 RVA: 0x0004627C File Offset: 0x0004447C
		private object ConvertValueWrapper(IEdmTypeReference typeReference, string valueText)
		{
			object obj;
			if (!this.TryConvertValue(typeReference, valueText, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00046298 File Offset: 0x00044498
		private bool TryConvertValue(IEdmTypeReference typeReference, string valueText, out object convertedValue)
		{
			if (typeReference.IsEnum())
			{
				QueryNode queryNode = null;
				if (EnumBinder.TryBindIdentifier(valueText, typeReference.AsEnum(), null, out queryNode))
				{
					convertedValue = queryNode;
					return true;
				}
				convertedValue = null;
				return false;
			}
			else
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitive();
				UriTemplateExpression uriTemplateExpression;
				if (this.enableUriTemplateParsing && UriTemplateParser.TryParseLiteral(valueText, edmPrimitiveTypeReference, out uriTemplateExpression))
				{
					convertedValue = uriTemplateExpression;
					return true;
				}
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType((IEdmPrimitiveType)edmPrimitiveTypeReference.Definition, edmPrimitiveTypeReference.IsNullable);
				LiteralParser literalParser = LiteralParser.ForKeys(this.keysAsSegment);
				return literalParser.TryParseLiteral(primitiveClrType, valueText, out convertedValue);
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00046318 File Offset: 0x00044518
		private static bool TryParseFromUri(string text, bool allowNamedValues, bool allowNull, out SegmentArgumentParser instance, bool enableUriTemplateParsing)
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
			FunctionParameterToken[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				FunctionParameterToken functionParameterToken = array2[i];
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
				if (text2 != null)
				{
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
					i++;
					continue;
				}
				instance = null;
				return false;
			}
			instance = new SegmentArgumentParser(dictionary, list, false, enableUriTemplateParsing);
			return true;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004645C File Offset: 0x0004465C
		private static void CreateIfNull<T>(ref T value) where T : new()
		{
			if (value == null)
			{
				value = ((default(T) == null) ? new T() : default(T));
			}
		}

		// Token: 0x04000832 RID: 2098
		private static readonly SegmentArgumentParser Empty = new SegmentArgumentParser();

		// Token: 0x04000833 RID: 2099
		private readonly bool keysAsSegment;

		// Token: 0x04000834 RID: 2100
		private readonly bool enableUriTemplateParsing;

		// Token: 0x04000835 RID: 2101
		private Dictionary<string, string> namedValues;

		// Token: 0x04000836 RID: 2102
		private List<string> positionalValues;
	}
}
