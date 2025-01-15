using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000337 RID: 823
	internal class ColumnMapTranslator : ColumnMapVisitorWithResults<ColumnMap, ColumnMapTranslatorTranslationDelegate>
	{
		// Token: 0x0600272A RID: 10026 RVA: 0x00071E55 File Offset: 0x00070055
		private ColumnMapTranslator()
		{
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00071E60 File Offset: 0x00070060
		private static Var GetReplacementVar(Var originalVar, Dictionary<Var, Var> replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x00071E83 File Offset: 0x00070083
		internal static ColumnMap Translate(ColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return columnMap.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(ColumnMapTranslator._instance, translationDelegate);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00071E94 File Offset: 0x00070094
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, ColumnMap> varToColumnMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					if (varToColumnMap.TryGetValue(varRefColumnMap.Var, out columnMap))
					{
						if (!columnMap.IsNamed && varRefColumnMap.IsNamed)
						{
							columnMap.Name = varRefColumnMap.Name;
						}
						if (Helper.IsEnumType(varRefColumnMap.Type.EdmType) && varRefColumnMap.Type.EdmType != columnMap.Type.EdmType)
						{
							columnMap.Type = varRefColumnMap.Type;
						}
					}
					else
					{
						columnMap = varRefColumnMap;
					}
				}
				return columnMap;
			});
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00071EC0 File Offset: 0x000700C0
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, Var> varToVarMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					Var replacementVar = ColumnMapTranslator.GetReplacementVar(varRefColumnMap.Var, varToVarMap);
					if (varRefColumnMap.Var != replacementVar)
					{
						columnMap = new VarRefColumnMap(varRefColumnMap.Type, varRefColumnMap.Name, replacementVar);
					}
				}
				return columnMap;
			});
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00071EEC File Offset: 0x000700EC
		internal static ColumnMap Translate(ColumnMap columnMapToTranslate, Dictionary<Var, KeyValuePair<int, int>> varToCommandColumnMap)
		{
			return ColumnMapTranslator.Translate(columnMapToTranslate, delegate(ColumnMap columnMap)
			{
				VarRefColumnMap varRefColumnMap = columnMap as VarRefColumnMap;
				if (varRefColumnMap != null)
				{
					KeyValuePair<int, int> keyValuePair;
					if (!varToCommandColumnMap.TryGetValue(varRefColumnMap.Var, out keyValuePair))
					{
						throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownVar, 1, varRefColumnMap.Var.Id);
					}
					columnMap = new ScalarColumnMap(varRefColumnMap.Type, varRefColumnMap.Name, keyValuePair.Key, keyValuePair.Value);
				}
				if (!columnMap.IsNamed)
				{
					columnMap.Name = "Value";
				}
				return columnMap;
			});
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00071F18 File Offset: 0x00070118
		private void VisitList<TResultType>(TResultType[] tList, ColumnMapTranslatorTranslationDelegate translationDelegate) where TResultType : ColumnMap
		{
			for (int i = 0; i < tList.Length; i++)
			{
				tList[i] = (TResultType)((object)tList[i].Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate));
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x00071F54 File Offset: 0x00070154
		protected override EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap = entityIdentity.EntitySetColumnMap.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			this.VisitList<SimpleColumnMap>(entityIdentity.Keys, translationDelegate);
			if (columnMap != entityIdentity.EntitySetColumnMap)
			{
				entityIdentity = new DiscriminatedEntityIdentity((SimpleColumnMap)columnMap, entityIdentity.EntitySetMap, entityIdentity.Keys);
			}
			return entityIdentity;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00071F9F File Offset: 0x0007019F
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			this.VisitList<SimpleColumnMap>(entityIdentity.Keys, translationDelegate);
			return entityIdentity;
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00071FB0 File Offset: 0x000701B0
		internal override ColumnMap Visit(ComplexTypeColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)translationDelegate(simpleColumnMap);
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap.NullSentinel != simpleColumnMap)
			{
				columnMap = new ComplexTypeColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, simpleColumnMap);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0007200C File Offset: 0x0007020C
		internal override ColumnMap Visit(DiscriminatedCollectionColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap2 = columnMap.Discriminator.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.Keys, translationDelegate);
			ColumnMap columnMap3 = columnMap.Element.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			if (columnMap2 != columnMap.Discriminator || columnMap3 != columnMap.Element)
			{
				columnMap = new DiscriminatedCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap3, columnMap.Keys, columnMap.ForeignKeys, (SimpleColumnMap)columnMap2, columnMap.DiscriminatorValue);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x00072094 File Offset: 0x00070294
		internal override ColumnMap Visit(EntityColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, translationDelegate);
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (entityIdentity != columnMap.EntityIdentity)
			{
				columnMap = new EntityColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, entityIdentity);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000720E8 File Offset: 0x000702E8
		internal override ColumnMap Visit(SimplePolymorphicColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			ColumnMap columnMap2 = columnMap.TypeDiscriminator.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			Dictionary<object, TypedColumnMap> dictionary = columnMap.TypeChoices;
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				TypedColumnMap typedColumnMap = (TypedColumnMap)keyValuePair.Value.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
				if (typedColumnMap != keyValuePair.Value)
				{
					if (dictionary == columnMap.TypeChoices)
					{
						dictionary = new Dictionary<object, TypedColumnMap>(columnMap.TypeChoices);
					}
					dictionary[keyValuePair.Key] = typedColumnMap;
				}
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap2 != columnMap.TypeDiscriminator || dictionary != columnMap.TypeChoices)
			{
				columnMap = new SimplePolymorphicColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, (SimpleColumnMap)columnMap2, dictionary);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000721D4 File Offset: 0x000703D4
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapTranslator");
			return null;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000721E4 File Offset: 0x000703E4
		internal override ColumnMap Visit(RecordColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)translationDelegate(simpleColumnMap);
			}
			this.VisitList<ColumnMap>(columnMap.Properties, translationDelegate);
			if (columnMap.NullSentinel != simpleColumnMap)
			{
				columnMap = new RecordColumnMap(columnMap.Type, columnMap.Name, columnMap.Properties, simpleColumnMap);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x00072240 File Offset: 0x00070440
		internal override ColumnMap Visit(RefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, translationDelegate);
			if (entityIdentity != columnMap.EntityIdentity)
			{
				columnMap = new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0007227F File Offset: 0x0007047F
		internal override ColumnMap Visit(ScalarColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x00072288 File Offset: 0x00070488
		internal override ColumnMap Visit(SimpleCollectionColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, translationDelegate);
			this.VisitList<SimpleColumnMap>(columnMap.Keys, translationDelegate);
			ColumnMap columnMap2 = columnMap.Element.Accept<ColumnMap, ColumnMapTranslatorTranslationDelegate>(this, translationDelegate);
			if (columnMap2 != columnMap.Element)
			{
				columnMap = new SimpleCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap2, columnMap.Keys, columnMap.ForeignKeys);
			}
			return translationDelegate(columnMap);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000722ED File Offset: 0x000704ED
		internal override ColumnMap Visit(VarRefColumnMap columnMap, ColumnMapTranslatorTranslationDelegate translationDelegate)
		{
			return translationDelegate(columnMap);
		}

		// Token: 0x04000DAB RID: 3499
		private static readonly ColumnMapTranslator _instance = new ColumnMapTranslator();
	}
}
