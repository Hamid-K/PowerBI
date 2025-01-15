using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000102 RID: 258
	internal sealed class InBinder
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00025844 File Offset: 0x00023A44
		internal InBinder(Func<QueryToken, QueryNode> bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00025854 File Offset: 0x00023A54
		internal QueryNode BindInOperator(InToken inToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<InToken>(inToken, "inToken");
			SingleValueNode singleValueOperandFromToken = this.GetSingleValueOperandFromToken(inToken.Left);
			CollectionNode collectionOperandFromToken = this.GetCollectionOperandFromToken(inToken.Right, new EdmCollectionTypeReference(new EdmCollectionType(singleValueOperandFromToken.TypeReference)), state.Model);
			return new InNode(singleValueOperandFromToken, collectionOperandFromToken);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000258A4 File Offset: 0x00023AA4
		private SingleValueNode GetSingleValueOperandFromToken(QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_LeftOperandNotSingleValue);
			}
			return singleValueNode;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000258D4 File Offset: 0x00023AD4
		private CollectionNode GetCollectionOperandFromToken(QueryToken queryToken, IEdmTypeReference expectedType, IEdmModel model)
		{
			LiteralToken literalToken = queryToken as LiteralToken;
			CollectionNode collectionNode;
			if (literalToken != null)
			{
				string originalText = literalToken.OriginalText;
				string text = originalText;
				if (text[0] == '(')
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					stringBuilder[0] = '[';
					stringBuilder[stringBuilder.Length - 1] = ']';
					text = stringBuilder.ToString();
					string text2 = expectedType.Definition.AsElementType().FullTypeName();
					if (text2.Equals("Edm.String"))
					{
						text = InBinder.NormalizeCollectionItems(text, new InBinder.NormalizeFunction(InBinder.NormalizeStringItem));
					}
					else if (text2.Equals("Edm.Guid"))
					{
						text = InBinder.NormalizeCollectionItems(text, new InBinder.NormalizeFunction(InBinder.NormalizeGuidItem));
					}
				}
				object obj = ODataUriConversionUtils.ConvertFromCollectionValue(text, model, expectedType);
				LiteralToken literalToken2 = new LiteralToken(obj, originalText, expectedType);
				collectionNode = this.bindMethod(literalToken2) as CollectionConstantNode;
			}
			else
			{
				collectionNode = this.bindMethod(queryToken) as CollectionNode;
			}
			if (collectionNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_RightOperandNotCollectionValue);
			}
			return collectionNode;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000259D4 File Offset: 0x00023BD4
		private static string NormalizeCollectionItems(string bracketLiteralText, InBinder.NormalizeFunction normalizeFunc)
		{
			string[] array = (from s in bracketLiteralText.Substring(1, bracketLiteralText.Length - 2).Split(new char[] { ',' })
				select s.Trim()).ToArray<string>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				string text = normalizeFunc(array[i]);
				if (i != array.Length - 1)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0},", new object[] { text });
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[] { stringBuilder.ToString() });
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00025A94 File Offset: 0x00023C94
		private static string NormalizeStringItem(string str)
		{
			if ((str[0] != '\'' || str[str.Length - 1] != '\'') && (str[0] != '"' || str[str.Length - 1] != '"'))
			{
				throw new ODataException(Strings.StringItemShouldBeQuoted(str));
			}
			string text = str;
			if (str[0] == '\'')
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[] { UriParserHelper.RemoveQuotes(str) });
			}
			return text;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00025B14 File Offset: 0x00023D14
		private static string NormalizeGuidItem(string guid)
		{
			if (guid[0] == '\'' || guid[0] == '"')
			{
				return guid;
			}
			return string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { guid });
		}

		// Token: 0x04000765 RID: 1893
		private readonly Func<QueryToken, QueryNode> bindMethod;

		// Token: 0x02000373 RID: 883
		// (Invoke) Token: 0x06001F1E RID: 7966
		private delegate string NormalizeFunction(string item);
	}
}
