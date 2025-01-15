using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000114 RID: 276
	internal sealed class QueryNamingContext
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0002BF46 File Offset: 0x0002A146
		public static StringComparer NameComparer
		{
			get
			{
				return DaxRef.NameComparer;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002BF50 File Offset: 0x0002A150
		internal QueryNamingContext(IEnumerable<string> reservedNames = null)
		{
			this._currentNames = ((reservedNames != null) ? new HashSet<string>(reservedNames, QueryNamingContext.NameComparer) : new HashSet<string>(QueryNamingContext.NameComparer));
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		public string CreateNameForVariableDeclaration(string candidateName)
		{
			string text = QueryNamingContext.NormalizeDeclarationName(candidateName);
			return this.CreateNameForDeclaration(text, "__");
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		public string CreateNameForTableDeclaration(string candidateName)
		{
			return this.CreateNameForDeclaration(candidateName, "__");
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		internal static string NormalizeDeclarationName(string candidateName)
		{
			string text = QueryNamingContext.UnsupportedCharactersRegx.Replace(candidateName, "_");
			if (char.IsDigit(text[0]))
			{
				text = "_" + text;
			}
			return text;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002C01D File Offset: 0x0002A21D
		private string CreateNameForDeclaration(string candidateName, string prefix)
		{
			ArgumentValidation.CheckNotNullOrEmpty(candidateName, "candidateName");
			ArgumentValidation.CheckNotNullOrEmpty(prefix, "prefix");
			if (!candidateName.StartsWith(prefix, StringComparison.Ordinal))
			{
				candidateName = prefix + candidateName;
			}
			return QueryNamingContext.CreateAndRegisterUniqueName(this._declarationNames, candidateName, null, null);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002C058 File Offset: 0x0002A258
		public string CreateAndRegisterUniqueName(string candidateName)
		{
			ArgumentValidation.CheckNotNullOrEmpty(candidateName, "candidateName");
			return this.CreateAndRegisterUniqueName(candidateName, null, null);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002C06F File Offset: 0x0002A26F
		internal string CreateOrReuseNameForGroupKey(QueryExpression expression, string candidateName = null, string fallbackCandidateName = null)
		{
			return this.CreateOrReuseName(expression, this._groupKeyNames, candidateName, fallbackCandidateName);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002C080 File Offset: 0x0002A280
		internal string CreateOrReuseNameForDetail(IEnumerable<QueryExpression> groupKeyExpressions, QueryExpression detailExpression, string candidateName = null, string fallbackCandidateName = null)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<QueryExpression>>(groupKeyExpressions, "groupKeyExpressions");
			return this.CreateOrReuseName<Tuple<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression>>(detailExpression, Tuple.Create<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression>(ReadOnlyEquatableHashSet<QueryExpression>.CopyFrom(groupKeyExpressions), detailExpression), this._detailNames, candidateName, fallbackCandidateName);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002C0AA File Offset: 0x0002A2AA
		public string CreateOrReuseNameForMeasure(QueryExpression expression, string candidateName = null, string fallbackCandidateName = null)
		{
			return this.CreateOrReuseName(expression, this._measureNames, candidateName, fallbackCandidateName);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002C0BB File Offset: 0x0002A2BB
		private string CreateOrReuseName(QueryExpression expression, Dictionary<QueryExpression, string> nameDictionary, string candidateName, string fallbackCandidateName)
		{
			return this.CreateOrReuseName<QueryExpression>(expression, expression, nameDictionary, candidateName, fallbackCandidateName);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002C0CC File Offset: 0x0002A2CC
		private string CreateOrReuseName<TKey>(QueryExpression expression, TKey key, Dictionary<TKey, string> nameDictionary, string candidateName, string fallbackCandidateName) where TKey : class
		{
			ArgumentValidation.CheckNotNull<TKey>(key, "key");
			string text;
			if (!nameDictionary.TryGetValue(key, out text))
			{
				if (string.IsNullOrEmpty(candidateName))
				{
					candidateName = expression.GetDefaultName();
					if (candidateName == null)
					{
						candidateName = fallbackCandidateName;
					}
				}
				text = this.CreateAndRegisterUniqueName(candidateName, expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).FirstOrDefault<EntitySet>(), expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).FirstOrDefault<IConceptualEntity>());
				nameDictionary.Add(key, text);
			}
			else
			{
				this._currentNames.Add(text);
			}
			return text;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002C142 File Offset: 0x0002A342
		private string CreateAndRegisterUniqueName(string candidateName, EntitySet entitySet = null, IConceptualEntity entity = null)
		{
			return QueryNamingContext.CreateAndRegisterUniqueName(this._currentNames, candidateName, entitySet, entity);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002C152 File Offset: 0x0002A352
		internal static string CreateUniqueName(IEnumerable<string> currentNames, string candidateName, EntitySet entitySet = null)
		{
			return QueryNamingContext.CreateAndRegisterUniqueName(currentNames.ToSet(QueryNamingContext.NameComparer), candidateName, entitySet, null);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002C168 File Offset: 0x0002A368
		public static string CreateAndRegisterUniqueName(HashSet<string> currentNames, string candidateName, EntitySet entitySet = null, IConceptualEntity entity = null)
		{
			string text = ArgumentValidation.CheckNotNullOrEmpty(candidateName, "candidateName");
			if (currentNames.Contains(text))
			{
				if (entity != null)
				{
					text = entity.EdmName + text;
				}
				else if (entitySet != null)
				{
					text = entitySet.Name + text;
				}
				while (currentNames.Contains(text))
				{
					text = StringUtil.IncrementDigitSuffix(text);
				}
			}
			currentNames.Add(text);
			return text;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		internal void AddGroupDetailNameForReusedGroup(IEnumerable<QueryExpression> groupKeyExpressions, QueryExpression detailExpression, string detailName)
		{
			Tuple<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression> tuple = Tuple.Create<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression>(ReadOnlyEquatableHashSet<QueryExpression>.CopyFrom(groupKeyExpressions), detailExpression);
			string text;
			if (!this._detailNames.TryGetValue(tuple, out text))
			{
				this._detailNames.Add(tuple, detailName);
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002C1FF File Offset: 0x0002A3FF
		internal void Remove(string name)
		{
			this._currentNames.Remove(name);
		}

		// Token: 0x04000A46 RID: 2630
		private const string UnsupportedCharacterPattern = "[^a-zA-Z0-9_]";

		// Token: 0x04000A47 RID: 2631
		private const string NormalizerReplacementChar = "_";

		// Token: 0x04000A48 RID: 2632
		private static readonly Regex UnsupportedCharactersRegx = new Regex("[^a-zA-Z0-9_]");

		// Token: 0x04000A49 RID: 2633
		private readonly Dictionary<Tuple<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression>, string> _detailNames = new Dictionary<Tuple<ReadOnlyEquatableHashSet<QueryExpression>, QueryExpression>, string>();

		// Token: 0x04000A4A RID: 2634
		private readonly Dictionary<QueryExpression, string> _groupKeyNames = new Dictionary<QueryExpression, string>();

		// Token: 0x04000A4B RID: 2635
		private readonly Dictionary<QueryExpression, string> _measureNames = new Dictionary<QueryExpression, string>();

		// Token: 0x04000A4C RID: 2636
		private readonly HashSet<string> _declarationNames = new HashSet<string>(QueryNamingContext.NameComparer);

		// Token: 0x04000A4D RID: 2637
		private readonly HashSet<string> _currentNames;
	}
}
