using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000569 RID: 1385
	internal sealed class GeneratedView : InternalBase
	{
		// Token: 0x0600437E RID: 17278 RVA: 0x000EA200 File Offset: 0x000E8400
		internal static GeneratedView CreateGeneratedView(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, string eSQL, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			DiscriminatorMap discriminatorMap = null;
			if (commandTree != null)
			{
				commandTree = ViewSimplifier.SimplifyView(extent, commandTree);
				if (extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					DiscriminatorMap.TryCreateDiscriminatorMap((EntitySet)extent, commandTree.Query, out discriminatorMap);
				}
			}
			return new GeneratedView(extent, type, commandTree, eSQL, discriminatorMap, mappingItemCollection, config);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000EA247 File Offset: 0x000E8447
		internal static GeneratedView CreateGeneratedViewForFKAssociationSet(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			return new GeneratedView(extent, type, commandTree, null, null, mappingItemCollection, config);
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000EA258 File Offset: 0x000E8458
		internal static bool TryParseUserSpecifiedView(EntitySetBaseMapping setMapping, EntityTypeBase type, string eSQL, bool includeSubtypes, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config, IList<EdmSchemaError> errors, out GeneratedView generatedView)
		{
			bool flag = false;
			DbQueryCommandTree dbQueryCommandTree;
			DiscriminatorMap discriminatorMap;
			Exception ex;
			if (!GeneratedView.TryParseView(eSQL, true, setMapping.Set, mappingItemCollection, config, out dbQueryCommandTree, out discriminatorMap, out ex))
			{
				EdmSchemaError edmSchemaError = new EdmSchemaError(Strings.Mapping_Invalid_QueryView2(setMapping.Set.Name, ex.Message), 2068, EdmSchemaErrorSeverity.Error, setMapping.EntityContainerMapping.SourceLocation, setMapping.StartLineNumber, setMapping.StartLinePosition, ex);
				errors.Add(edmSchemaError);
				flag = true;
			}
			else
			{
				foreach (EdmSchemaError edmSchemaError2 in ViewValidator.ValidateQueryView(dbQueryCommandTree, setMapping, type, includeSubtypes))
				{
					errors.Add(edmSchemaError2);
					flag = true;
				}
				CollectionType collectionType = dbQueryCommandTree.Query.ResultType.EdmType as CollectionType;
				if (collectionType == null || !setMapping.Set.ElementType.IsAssignableFrom(collectionType.TypeUsage.EdmType))
				{
					EdmSchemaError edmSchemaError3 = new EdmSchemaError(Strings.Mapping_Invalid_QueryView_Type(setMapping.Set.Name), 2069, EdmSchemaErrorSeverity.Error, setMapping.EntityContainerMapping.SourceLocation, setMapping.StartLineNumber, setMapping.StartLinePosition);
					errors.Add(edmSchemaError3);
					flag = true;
				}
			}
			if (!flag)
			{
				generatedView = new GeneratedView(setMapping.Set, type, dbQueryCommandTree, eSQL, discriminatorMap, mappingItemCollection, config);
				return true;
			}
			generatedView = null;
			return false;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000EA3B0 File Offset: 0x000E85B0
		private GeneratedView(EntitySetBase extent, EdmType type, DbQueryCommandTree commandTree, string eSQL, DiscriminatorMap discriminatorMap, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config)
		{
			this.m_extent = extent;
			this.m_type = type;
			this.m_commandTree = commandTree;
			this.m_eSQL = eSQL;
			this.m_discriminatorMap = discriminatorMap;
			this.m_mappingItemCollection = mappingItemCollection;
			this.m_config = config;
			if (this.m_config.IsViewTracing)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.ToCompactString(stringBuilder);
				Helpers.FormatTraceLine("CQL view for {0}", new object[] { stringBuilder.ToString() });
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x000EA430 File Offset: 0x000E8630
		internal string eSQL
		{
			get
			{
				return this.m_eSQL;
			}
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000EA438 File Offset: 0x000E8638
		internal DbQueryCommandTree GetCommandTree()
		{
			if (this.m_commandTree != null)
			{
				return this.m_commandTree;
			}
			Exception ex;
			if (GeneratedView.TryParseView(this.m_eSQL, false, this.m_extent, this.m_mappingItemCollection, this.m_config, out this.m_commandTree, out this.m_discriminatorMap, out ex))
			{
				return this.m_commandTree;
			}
			throw new MappingException(Strings.Mapping_Invalid_QueryView(this.m_extent.Name, ex.Message));
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000EA4A4 File Offset: 0x000E86A4
		internal Node GetInternalTree(Command targetIqtCommand)
		{
			if (this.m_internalTreeNode == null)
			{
				Command command = ITreeGenerator.Generate(this.GetCommandTree(), this.m_discriminatorMap);
				PlanCompiler.Assert(command.Root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + command.Root.Op.OpType.ToString());
				command.DisableVarVecEnumCaching();
				this.m_internalTreeNode = command.Root.Child0;
			}
			return OpCopier.Copy(targetIqtCommand, this.m_internalTreeNode);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000EA530 File Offset: 0x000E8730
		private static bool TryParseView(string eSQL, bool isUserSpecified, EntitySetBase extent, StorageMappingItemCollection mappingItemCollection, ConfigViewGenerator config, out DbQueryCommandTree commandTree, out DiscriminatorMap discriminatorMap, out Exception parserException)
		{
			commandTree = null;
			discriminatorMap = null;
			parserException = null;
			config.StartSingleWatch(PerfType.ViewParsing);
			try
			{
				ParserOptions.CompilationMode compilationMode = ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (isUserSpecified)
				{
					compilationMode = ParserOptions.CompilationMode.UserViewGenerationMode;
				}
				commandTree = (DbQueryCommandTree)ExternalCalls.CompileView(eSQL, mappingItemCollection, compilationMode);
				commandTree = ViewSimplifier.SimplifyView(extent, commandTree);
				if (extent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
				{
					DiscriminatorMap.TryCreateDiscriminatorMap((EntitySet)extent, commandTree.Query, out discriminatorMap);
				}
			}
			catch (Exception ex)
			{
				if (!ex.IsCatchableExceptionType())
				{
					throw;
				}
				parserException = ex;
			}
			finally
			{
				config.StopSingleWatch(PerfType.ViewParsing);
			}
			return parserException == null;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000EA5D8 File Offset: 0x000E87D8
		internal override void ToCompactString(StringBuilder builder)
		{
			bool flag = this.m_type != this.m_extent.ElementType;
			if (flag)
			{
				builder.Append("OFTYPE(");
			}
			builder.AppendFormat("{0}.{1}", this.m_extent.EntityContainer.Name, this.m_extent.Name);
			if (flag)
			{
				builder.Append(", ").Append(this.m_type.Name).Append(')');
			}
			builder.AppendLine(" = ");
			if (!string.IsNullOrEmpty(this.m_eSQL))
			{
				builder.Append(this.m_eSQL);
				return;
			}
			builder.Append(this.m_commandTree.Print());
		}

		// Token: 0x0400181F RID: 6175
		private readonly EntitySetBase m_extent;

		// Token: 0x04001820 RID: 6176
		private readonly EdmType m_type;

		// Token: 0x04001821 RID: 6177
		private DbQueryCommandTree m_commandTree;

		// Token: 0x04001822 RID: 6178
		private readonly string m_eSQL;

		// Token: 0x04001823 RID: 6179
		private Node m_internalTreeNode;

		// Token: 0x04001824 RID: 6180
		private DiscriminatorMap m_discriminatorMap;

		// Token: 0x04001825 RID: 6181
		private readonly StorageMappingItemCollection m_mappingItemCollection;

		// Token: 0x04001826 RID: 6182
		private readonly ConfigViewGenerator m_config;
	}
}
