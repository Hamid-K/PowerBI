using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000096 RID: 150
	internal class QueryComponents
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00010524 File Offset: 0x0000E724
		internal QueryComponents(Uri uri, Version version, Type lastSegmentType, LambdaExpression projection, Dictionary<Expression, Expression> normalizerRewrites)
		{
			this.projection = projection;
			this.normalizerRewrites = normalizerRewrites;
			this.lastSegmentType = lastSegmentType;
			this.Uri = uri;
			this.version = version;
			this.httpMethod = "GET";
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001055C File Offset: 0x0000E75C
		internal QueryComponents(Uri uri, Version version, Type lastSegmentType, LambdaExpression projection, Dictionary<Expression, Expression> normalizerRewrites, string httpMethod, bool? singleResult, List<BodyOperationParameter> bodyOperationParameters, List<UriOperationParameter> uriOperationParameters)
		{
			this.projection = projection;
			this.normalizerRewrites = normalizerRewrites;
			this.lastSegmentType = lastSegmentType;
			this.Uri = uri;
			this.version = version;
			this.httpMethod = httpMethod;
			this.uriOperationParameters = uriOperationParameters;
			this.bodyOperationParameters = bodyOperationParameters;
			this.singleResult = singleResult;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000105B4 File Offset: 0x0000E7B4
		internal Dictionary<Expression, Expression> NormalizerRewrites
		{
			get
			{
				return this.normalizerRewrites;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000105BC File Offset: 0x0000E7BC
		internal LambdaExpression Projection
		{
			get
			{
				return this.projection;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000105C4 File Offset: 0x0000E7C4
		internal Type LastSegmentType
		{
			get
			{
				return this.lastSegmentType;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000105CC File Offset: 0x0000E7CC
		internal Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000105D4 File Offset: 0x0000E7D4
		internal string HttpMethod
		{
			get
			{
				return this.httpMethod;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000105DC File Offset: 0x0000E7DC
		internal List<UriOperationParameter> UriOperationParameters
		{
			get
			{
				return this.uriOperationParameters;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000105E4 File Offset: 0x0000E7E4
		internal List<BodyOperationParameter> BodyOperationParameters
		{
			get
			{
				return this.bodyOperationParameters;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000105EC File Offset: 0x0000E7EC
		internal bool? SingleResult
		{
			get
			{
				return this.singleResult;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000105F4 File Offset: 0x0000E7F4
		internal bool HasSelectQueryOption
		{
			get
			{
				return this.Uri != null && QueryComponents.ContainsSelectQueryOption(UriUtil.UriToString(this.Uri));
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00010616 File Offset: 0x0000E816
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0001061E File Offset: 0x0000E81E
		internal Uri Uri { get; set; }

		// Token: 0x06000499 RID: 1177 RVA: 0x00010627 File Offset: 0x0000E827
		private static bool ContainsSelectQueryOption(string queryString)
		{
			return queryString.Contains("?$select=") || queryString.Contains("&$select=") || queryString.Contains("($select=") || queryString.Contains(";$select=");
		}

		// Token: 0x04000153 RID: 339
		private readonly Type lastSegmentType;

		// Token: 0x04000154 RID: 340
		private readonly Dictionary<Expression, Expression> normalizerRewrites;

		// Token: 0x04000155 RID: 341
		private readonly LambdaExpression projection;

		// Token: 0x04000156 RID: 342
		private readonly string httpMethod;

		// Token: 0x04000157 RID: 343
		private readonly List<UriOperationParameter> uriOperationParameters;

		// Token: 0x04000158 RID: 344
		private readonly List<BodyOperationParameter> bodyOperationParameters;

		// Token: 0x04000159 RID: 345
		private readonly bool? singleResult;

		// Token: 0x0400015A RID: 346
		private const string SelectQueryOption = "$select=";

		// Token: 0x0400015B RID: 347
		private const string SelectQueryOptionWithQuestionMark = "?$select=";

		// Token: 0x0400015C RID: 348
		private const string SelectQueryOptionWithAmpersand = "&$select=";

		// Token: 0x0400015D RID: 349
		private const string SelectQueryOptionWithLeftParen = "($select=";

		// Token: 0x0400015E RID: 350
		private const string SelectQueryOptionWithSemi = ";$select=";

		// Token: 0x0400015F RID: 351
		private Version version;
	}
}
