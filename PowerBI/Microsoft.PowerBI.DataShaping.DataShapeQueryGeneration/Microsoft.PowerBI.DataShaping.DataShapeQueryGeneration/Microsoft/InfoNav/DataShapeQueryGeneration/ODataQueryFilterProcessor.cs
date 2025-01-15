using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;
using Microsoft.InfoNav.Utils;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000092 RID: 146
	internal sealed class ODataQueryFilterProcessor
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0001497C File Offset: 0x00012B7C
		private ODataQueryFilterProcessor(IConceptualSchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001498C File Offset: 0x00012B8C
		internal static bool TryApplyFilter(ResolvedQueryDefinition query, IConceptualSchema schema, string odataFilter, IErrorContext errorContext, out ResolvedQueryDefinition filteredQuery)
		{
			bool flag;
			try
			{
				FilterClause filterClause = ODataQueryFilterProcessor.ParseODataQueryFilter(odataFilter);
				if (filterClause == null)
				{
					filteredQuery = query;
					flag = true;
				}
				else
				{
					ODataQueryFilterProcessor odataQueryFilterProcessor = new ODataQueryFilterProcessor(schema);
					filteredQuery = odataQueryFilterProcessor.ApplyODataFilter(query, filterClause);
					flag = true;
				}
			}
			catch (ODataFilterProcessingException ex)
			{
				errorContext.RegisterError(ex.MessageTemplate, ex.Args);
				filteredQuery = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000149F0 File Offset: 0x00012BF0
		private static FilterClause ParseODataQueryFilter(string odataFilter)
		{
			FilterClause filterClause;
			try
			{
				filterClause = new ODataQueryOptionParser(ODataQueryFilterProcessor.OpenEdmModelInstance.EdmModel, ODataQueryFilterProcessor.OpenEdmModelInstance.OpenEdmType, null, new Dictionary<string, string> { { "$filter", odataFilter } }).ParseFilter();
			}
			catch (ODataException ex)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.QueryFilterCouldNotBeParsed, new object[] { ex.Message.MarkAsCustomerContent() });
			}
			return filterClause;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00014A60 File Offset: 0x00012C60
		private ResolvedQueryDefinition ApplyODataFilter(ResolvedQueryDefinition query, FilterClause odataFilterClause)
		{
			BinaryOperatorNode binaryOperatorNode = odataFilterClause.Expression as BinaryOperatorNode;
			if (binaryOperatorNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.FilterClauseRootMustBeBinaryNode, Array.Empty<object>());
			}
			ODataQueryFilterProcessor.ODataQueryProcessingContext odataQueryProcessingContext = new ODataQueryFilterProcessor.ODataQueryProcessingContext(odataFilterClause, query);
			ResolvedQueryFilter resolvedQueryFilter = this.Visit(binaryOperatorNode, odataQueryProcessingContext).Filter(null, null);
			ResolvedQueryFilter[] array = query.Where.Concat(resolvedQueryFilter).ToArray<ResolvedQueryFilter>();
			return new ResolvedQueryDefinition(query.Parameters, query.Let, odataQueryProcessingContext.QuerySources, array, query.Transform, query.OrderBy, query.Select, query.VisualShape, query.GroupBy, query.Top, query.Skip, query.Name);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014AFC File Offset: 0x00012CFC
		private ResolvedQueryExpression Visit(BinaryOperatorNode binaryOperatorNode, ODataQueryFilterProcessor.ODataQueryProcessingContext context)
		{
			if (binaryOperatorNode.OperatorKind != 2)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.BinaryNodeOperatorMustBeEquality, Array.Empty<object>());
			}
			ConstantNode constantNode = binaryOperatorNode.Right as ConstantNode;
			if (constantNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.BinaryNodeRightMustBeLiteral, Array.Empty<object>());
			}
			ConvertNode convertNode = binaryOperatorNode.Left as ConvertNode;
			if (convertNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.BinaryNodeLeftMustBeConvert, Array.Empty<object>());
			}
			ResolvedQueryExpression resolvedQueryExpression = this.Visit(convertNode, context);
			ResolvedQueryExpression resolvedQueryExpression2 = this.Visit(constantNode, context);
			return resolvedQueryExpression.Equal(resolvedQueryExpression2);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00014B6B File Offset: 0x00012D6B
		private ResolvedQueryExpression Visit(ConstantNode constantNode, ODataQueryFilterProcessor.ODataQueryProcessingContext context)
		{
			if (constantNode.TypeReference.GetType() != typeof(EdmStringTypeReference))
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.LiteralNodeTypeMustBeString, Array.Empty<object>());
			}
			return ((string)constantNode.Value).Literal();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00014BAC File Offset: 0x00012DAC
		private ResolvedQueryExpression Visit(ConvertNode convertNode, ODataQueryFilterProcessor.ODataQueryProcessingContext context)
		{
			if (convertNode.TypeReference.GetType() != typeof(EdmStringTypeReference))
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.ConvertNodeTypeMustBeString, Array.Empty<object>());
			}
			SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = convertNode.Source as SingleValueOpenPropertyAccessNode;
			if (singleValueOpenPropertyAccessNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.ConvertNodeSourceMustBeProperty, Array.Empty<object>());
			}
			return this.Visit(singleValueOpenPropertyAccessNode, context);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00014C04 File Offset: 0x00012E04
		private ResolvedQueryExpression Visit(SingleValueOpenPropertyAccessNode openPropertyNode, ODataQueryFilterProcessor.ODataQueryProcessingContext context)
		{
			SingleValueNode source = openPropertyNode.Source;
			if (source == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeMustHaveParentSource, Array.Empty<object>());
			}
			SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = source as SingleValueOpenPropertyAccessNode;
			if (singleValueOpenPropertyAccessNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeSourceMustBeProperty, Array.Empty<object>());
			}
			SingleValueNode singleValueNode = singleValueOpenPropertyAccessNode.Source;
			if (singleValueNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeSourceMustHaveParentSource, Array.Empty<object>());
			}
			SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode2 = singleValueNode as SingleValueOpenPropertyAccessNode;
			if (singleValueOpenPropertyAccessNode2 != null)
			{
				singleValueNode = singleValueOpenPropertyAccessNode2.Source;
				if (singleValueNode == null)
				{
					throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeSchemaMustHaveParentSource, Array.Empty<object>());
				}
			}
			NonentityRangeVariableReferenceNode nonentityRangeVariableReferenceNode = singleValueNode as NonentityRangeVariableReferenceNode;
			if (nonentityRangeVariableReferenceNode == null)
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeRootMustBeRangeVariableReference, Array.Empty<object>());
			}
			if (!nonentityRangeVariableReferenceNode.Name.Equals(context.ODataRangeVariable.Name))
			{
				throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyNodeRootMustBeIterator, Array.Empty<object>());
			}
			ResolvedEntitySource orAddEntitySource = context.GetOrAddEntitySource(this._schema, singleValueOpenPropertyAccessNode.Name);
			IConceptualProperty conceptualProperty;
			if (!orAddEntitySource.Entity.TryGetProperty(openPropertyNode.Name, out conceptualProperty))
			{
				string text = ODataQueryFilterProcessor.UnescapeSpecialCharacters(openPropertyNode.Name);
				if (text.Equals(openPropertyNode.Name) || !orAddEntitySource.Entity.TryGetProperty(text, out conceptualProperty))
				{
					throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.PropertyCouldNotBeResolved, new object[] { text.MarkAsCustomerContent() });
				}
			}
			return orAddEntitySource.Entity.SourceRef(orAddEntitySource.Name).Property(conceptualProperty);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00014D34 File Offset: 0x00012F34
		private ResolvedQueryExpression Visit(QueryNode queryNode, ODataQueryFilterProcessor.ODataQueryProcessingContext context)
		{
			throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.UnsupportedNodeType, new object[] { queryNode.GetType().FullName });
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00014D51 File Offset: 0x00012F51
		private static string UnescapeSpecialCharacters(string input)
		{
			return Regex.Replace(input, "_[xX][0-9A-Fa-f]{4}_", (Match match) => Convert.ToChar(Convert.ToInt32(match.ToString().Substring(2, 4), 16)).ToString());
		}

		// Token: 0x04000321 RID: 801
		private const string FilterQueryParameterName = "$filter";

		// Token: 0x04000322 RID: 802
		private static readonly ODataQueryFilterProcessor.OpenEdmModel OpenEdmModelInstance = new ODataQueryFilterProcessor.OpenEdmModel();

		// Token: 0x04000323 RID: 803
		private readonly IConceptualSchema _schema;

		// Token: 0x0200013F RID: 319
		private sealed class ODataQueryProcessingContext
		{
			// Token: 0x06000992 RID: 2450 RVA: 0x00025958 File Offset: 0x00023B58
			internal ODataQueryProcessingContext(FilterClause odataFilterClause, ResolvedQueryDefinition queryDefinition)
			{
				this._odataFilterClause = odataFilterClause;
				this._queryDefinition = queryDefinition;
				this._querySources = new List<ResolvedEntitySource>(queryDefinition.From.OfType<ResolvedEntitySource>());
				this._queryAndFilterSourceNames = new HashSet<string>(queryDefinition.From.Select((ResolvedQuerySource s) => s.Name));
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000993 RID: 2451 RVA: 0x000259C4 File Offset: 0x00023BC4
			internal IReadOnlyList<ResolvedEntitySource> QuerySources
			{
				get
				{
					return this._querySources;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000994 RID: 2452 RVA: 0x000259CC File Offset: 0x00023BCC
			internal RangeVariable ODataRangeVariable
			{
				get
				{
					return this._odataFilterClause.RangeVariable;
				}
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x000259DC File Offset: 0x00023BDC
			internal ResolvedEntitySource GetOrAddEntitySource(IConceptualSchema schema, string entityReferenceName)
			{
				IConceptualEntity conceptualEntity;
				if (!schema.TryGetEntity(entityReferenceName, out conceptualEntity))
				{
					string text = ODataQueryFilterProcessor.UnescapeSpecialCharacters(entityReferenceName);
					if (text.Equals(entityReferenceName) || !schema.TryGetEntity(text, out conceptualEntity))
					{
						throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.EntityCouldNotBeResolved, new object[] { text.MarkAsCustomerContent() });
					}
				}
				ResolvedEntitySource resolvedEntitySource;
				try
				{
					resolvedEntitySource = this._querySources.SingleOrDefault((ResolvedEntitySource s) => s.Entity == conceptualEntity);
				}
				catch (InvalidOperationException)
				{
					throw new ODataFilterProcessingException(ODataQueryFilterProcessingErrorCode.EntityIsPresentMoreThanOnceInQuerySources, new object[] { entityReferenceName.MarkAsCustomerContent() });
				}
				if (resolvedEntitySource == null)
				{
					string text2 = conceptualEntity.EdmName ?? string.Empty;
					char c = 'a';
					if (text2.Length > 0 && char.IsLetter(text2[0]))
					{
						c = char.ToLowerInvariant(text2[0]);
					}
					string text3 = StringUtil.MakeUniqueName(string.Empty + c.ToString(), this._queryAndFilterSourceNames);
					resolvedEntitySource = conceptualEntity.EntitySource(text3, null);
					this._queryAndFilterSourceNames.Add(resolvedEntitySource.Name);
					this._querySources.Add(resolvedEntitySource);
				}
				return resolvedEntitySource;
			}

			// Token: 0x0400050C RID: 1292
			private readonly FilterClause _odataFilterClause;

			// Token: 0x0400050D RID: 1293
			private readonly ResolvedQueryDefinition _queryDefinition;

			// Token: 0x0400050E RID: 1294
			private readonly List<ResolvedEntitySource> _querySources;

			// Token: 0x0400050F RID: 1295
			private readonly ISet<string> _queryAndFilterSourceNames;
		}

		// Token: 0x02000140 RID: 320
		private sealed class OpenEdmModel
		{
			// Token: 0x06000996 RID: 2454 RVA: 0x00025B08 File Offset: 0x00023D08
			internal OpenEdmModel()
			{
				this.EdmModel = new EdmModel();
				this.OpenEdmType = new EdmComplexType("Default", "OpenType", null, false, true);
				((EdmModel)this.EdmModel).AddElement(this.OpenEdmType);
			}

			// Token: 0x04000510 RID: 1296
			private const string DefaultNamespace = "Default";

			// Token: 0x04000511 RID: 1297
			private const string OpenTypeName = "OpenType";

			// Token: 0x04000512 RID: 1298
			internal readonly IEdmModel EdmModel;

			// Token: 0x04000513 RID: 1299
			internal readonly IEdmComplexType OpenEdmType;
		}
	}
}
