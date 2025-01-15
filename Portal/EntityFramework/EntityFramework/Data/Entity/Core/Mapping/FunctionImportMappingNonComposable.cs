using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000539 RID: 1337
	public sealed class FunctionImportMappingNonComposable : FunctionImportMapping
	{
		// Token: 0x060041C9 RID: 16841 RVA: 0x000DEB60 File Offset: 0x000DCD60
		public FunctionImportMappingNonComposable(EdmFunction functionImport, EdmFunction targetFunction, IEnumerable<FunctionImportResultMapping> resultMappings, EntityContainerMapping containerMapping)
			: base(Check.NotNull<EdmFunction>(functionImport, "functionImport"), Check.NotNull<EdmFunction>(targetFunction, "targetFunction"))
		{
			Check.NotNull<IEnumerable<FunctionImportResultMapping>>(resultMappings, "resultMappings");
			Check.NotNull<EntityContainerMapping>(containerMapping, "containerMapping");
			if (!resultMappings.Any<FunctionImportResultMapping>())
			{
				EdmItemCollection edmItemCollection = ((containerMapping.StorageMappingItemCollection != null) ? containerMapping.StorageMappingItemCollection.EdmItemCollection : new EdmItemCollection(new EdmModel(DataSpace.CSpace, 3.0)));
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(new FunctionImportStructuralTypeMappingKB[]
				{
					new FunctionImportStructuralTypeMappingKB(new List<FunctionImportStructuralTypeMapping>(), edmItemCollection)
				});
				this.noExplicitResultMappings = true;
			}
			else
			{
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(resultMappings.Select((FunctionImportResultMapping resultMapping) => new FunctionImportStructuralTypeMappingKB(resultMapping.TypeMappings, containerMapping.StorageMappingItemCollection.EdmItemCollection)).ToArray<FunctionImportStructuralTypeMappingKB>());
				this.noExplicitResultMappings = false;
			}
			this._resultMappings = new ReadOnlyCollection<FunctionImportResultMapping>(resultMappings.ToList<FunctionImportResultMapping>());
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000DEC54 File Offset: 0x000DCE54
		internal FunctionImportMappingNonComposable(EdmFunction functionImport, EdmFunction targetFunction, List<List<FunctionImportStructuralTypeMapping>> structuralTypeMappingsList, ItemCollection itemCollection)
			: base(functionImport, targetFunction)
		{
			if (structuralTypeMappingsList.Count == 0)
			{
				this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(new FunctionImportStructuralTypeMappingKB[]
				{
					new FunctionImportStructuralTypeMappingKB(new List<FunctionImportStructuralTypeMapping>(), itemCollection)
				});
				this.noExplicitResultMappings = true;
				return;
			}
			this._internalResultMappings = new ReadOnlyCollection<FunctionImportStructuralTypeMappingKB>(structuralTypeMappingsList.Select((List<FunctionImportStructuralTypeMapping> structuralTypeMappings) => new FunctionImportStructuralTypeMappingKB(structuralTypeMappings, itemCollection)).ToArray<FunctionImportStructuralTypeMappingKB>());
			this.noExplicitResultMappings = false;
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x000DECD4 File Offset: 0x000DCED4
		internal ReadOnlyCollection<FunctionImportStructuralTypeMappingKB> InternalResultMappings
		{
			get
			{
				return this._internalResultMappings;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x000DECDC File Offset: 0x000DCEDC
		public ReadOnlyCollection<FunctionImportResultMapping> ResultMappings
		{
			get
			{
				return this._resultMappings;
			}
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000DECE4 File Offset: 0x000DCEE4
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._resultMappings);
			base.SetReadOnly();
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000DECF7 File Offset: 0x000DCEF7
		internal FunctionImportStructuralTypeMappingKB GetResultMapping(int resultSetIndex)
		{
			if (this.noExplicitResultMappings)
			{
				return this.InternalResultMappings[0];
			}
			if (this.InternalResultMappings.Count <= resultSetIndex)
			{
				throw new ArgumentOutOfRangeException("resultSetIndex");
			}
			return this.InternalResultMappings[resultSetIndex];
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000DED33 File Offset: 0x000DCF33
		internal IList<string> GetDiscriminatorColumns(int resultSetIndex)
		{
			return this.GetResultMapping(resultSetIndex).DiscriminatorColumns;
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000DED44 File Offset: 0x000DCF44
		internal EntityType Discriminate(object[] discriminatorValues, int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			BitArray bitArray = new BitArray(resultMapping.MappedEntityTypes.Count, true);
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in resultMapping.NormalizedEntityTypeMappings)
			{
				bool flag = true;
				ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> columnConditions = functionImportNormalizedEntityTypeMapping.ColumnConditions;
				for (int i = 0; i < columnConditions.Count; i++)
				{
					if (columnConditions[i] != null && !columnConditions[i].ColumnValueMatchesCondition(discriminatorValues[i]))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					bitArray = bitArray.And(functionImportNormalizedEntityTypeMapping.ImpliedEntityTypes);
				}
				else
				{
					bitArray = bitArray.And(functionImportNormalizedEntityTypeMapping.ComplementImpliedEntityTypes);
				}
			}
			EntityType entityType = null;
			for (int j = 0; j < bitArray.Length; j++)
			{
				if (bitArray[j])
				{
					if (entityType != null)
					{
						throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderUnableToDetermineType);
					}
					entityType = resultMapping.MappedEntityTypes[j];
				}
			}
			if (entityType == null)
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderUnableToDetermineType);
			}
			return entityType;
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000DEE5C File Offset: 0x000DD05C
		internal TypeUsage GetExpectedTargetResultType(int resultSetIndex)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = this.GetResultMapping(resultSetIndex);
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>();
			IEnumerable<StructuralType> enumerable;
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				StructuralType structuralType;
				MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(base.FunctionImport, resultSetIndex, out structuralType);
				enumerable = new StructuralType[] { structuralType };
			}
			else
			{
				enumerable = resultMapping.MappedEntityTypes.Cast<StructuralType>();
			}
			foreach (StructuralType structuralType2 in enumerable)
			{
				foreach (object obj in TypeHelpers.GetAllStructuralMembers(structuralType2))
				{
					EdmProperty edmProperty = (EdmProperty)obj;
					dictionary[edmProperty.Name] = edmProperty.TypeUsage;
				}
			}
			foreach (string text in this.GetDiscriminatorColumns(resultSetIndex))
			{
				if (!dictionary.ContainsKey(text))
				{
					TypeUsage typeUsage = TypeUsage.CreateStringTypeUsage(MetadataWorkspace.GetModelPrimitiveType(PrimitiveTypeKind.String), true, false);
					dictionary.Add(text, typeUsage);
				}
			}
			return TypeUsage.Create(new CollectionType(TypeUsage.Create(new RowType(dictionary.Select((KeyValuePair<string, TypeUsage> c) => new EdmProperty(c.Key, c.Value))))));
		}

		// Token: 0x040016D0 RID: 5840
		private readonly ReadOnlyCollection<FunctionImportResultMapping> _resultMappings;

		// Token: 0x040016D1 RID: 5841
		private readonly bool noExplicitResultMappings;

		// Token: 0x040016D2 RID: 5842
		private readonly ReadOnlyCollection<FunctionImportStructuralTypeMappingKB> _internalResultMappings;
	}
}
