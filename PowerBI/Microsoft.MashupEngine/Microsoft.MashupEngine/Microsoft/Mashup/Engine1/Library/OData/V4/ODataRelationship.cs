using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200088E RID: 2190
	internal static class ODataRelationship
	{
		// Token: 0x06003EFC RID: 16124 RVA: 0x000CE4C0 File Offset: 0x000CC6C0
		public static TableValue AddRelationships(TableValue table, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
		{
			TableValue tableValue;
			try
			{
				tableValue = ODataRelationship.AddRelationships(source.Name, table, source.NavigationPropertyBindings, environment);
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(environment.Host, "ODataRelationshipDetection/AddRelationships", TraceEventType.Information, environment.Resource))
				{
					hostTrace.Add(ex, true);
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
				tableValue = table;
			}
			return tableValue;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x000CE53C File Offset: 0x000CC73C
		private static string GetRelationshipIdentity(ODataEnvironment environment, string navSource)
		{
			return ODataConstants.ODataCacheKey.Qualify(environment.ServiceUri.OriginalString, navSource);
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x000CE564 File Offset: 0x000CC764
		private static TableValue AddRelationships(string navSource, TableValue table, IEnumerable<Microsoft.OData.Edm.IEdmNavigationPropertyBinding> navBindings, ODataEnvironment environment)
		{
			foreach (Microsoft.OData.Edm.IEdmNavigationPropertyBinding edmNavigationPropertyBinding in navBindings)
			{
				Microsoft.OData.Edm.IEdmReferentialConstraint referentialConstraint = edmNavigationPropertyBinding.NavigationProperty.ReferentialConstraint;
				if (referentialConstraint != null)
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					List<int> list = new List<int>();
					foreach (Microsoft.OData.Edm.EdmReferentialConstraintPropertyPair edmReferentialConstraintPropertyPair in referentialConstraint.PropertyPairs)
					{
						keysBuilder.Add(edmReferentialConstraintPropertyPair.PrincipalProperty.Name);
						int num = table.Columns.IndexOfKey(edmReferentialConstraintPropertyPair.DependentProperty.Name);
						list.Add(num);
					}
					Microsoft.OData.Edm.IEdmNavigationSource targetSource = edmNavigationPropertyBinding.Target;
					TypeValue typeValue;
					if (environment.EdmTypeValueLookup.TryGetValue(targetSource.EntityType(), out typeValue))
					{
						RecordTypeValue targetRecordType = typeValue.AsRecordType;
						bool flag = false;
						foreach (string text in keysBuilder.ToKeys())
						{
							if (targetRecordType.Fields.Keys.IndexOfKey(text) == -1)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							Value value = new LinkTableFunctionValue(delegate
							{
								ODataQuery odataQuery;
								if (targetSource.NavigationSourceKind() == Microsoft.OData.Edm.EdmNavigationSourceKind.EntitySet)
								{
									odataQuery = new ODataQuery(environment, (Microsoft.OData.Edm.IEdmEntitySet)targetSource, targetRecordType);
								}
								else
								{
									odataQuery = new ODataQuery(environment, (Microsoft.OData.Edm.IEdmSingleton)targetSource, targetRecordType);
								}
								return new QueryTableValue(odataQuery).ReplaceRelationshipIdentity(ODataRelationship.GetRelationshipIdentity(environment, targetSource.Name));
							});
							table = RelatedTablesTableValue.New(table, table.RelatedTables, table.ColumnIdentities, Relationships.NestedJoin(table.Relationships, list.ToArray(), value, keysBuilder.ToKeys()));
						}
					}
				}
			}
			table = table.ReplaceRelationshipIdentity(ODataRelationship.GetRelationshipIdentity(environment, navSource));
			return table;
		}
	}
}
