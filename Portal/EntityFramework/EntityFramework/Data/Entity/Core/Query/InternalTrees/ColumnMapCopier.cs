using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.PlanCompiler;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000388 RID: 904
	internal class ColumnMapCopier : ColumnMapVisitorWithResults<ColumnMap, VarMap>
	{
		// Token: 0x06002BDA RID: 11226 RVA: 0x0008DF9C File Offset: 0x0008C19C
		private ColumnMapCopier()
		{
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0008DFA4 File Offset: 0x0008C1A4
		internal static ColumnMap Copy(ColumnMap columnMap, VarMap replacementVarMap)
		{
			return columnMap.Accept<ColumnMap, VarMap>(ColumnMapCopier._instance, replacementVarMap);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0008DFB4 File Offset: 0x0008C1B4
		private static Var GetReplacementVar(Var originalVar, VarMap replacementVarMap)
		{
			Var var = originalVar;
			while (replacementVarMap.TryGetValue(var, out originalVar) && originalVar != var)
			{
				var = originalVar;
			}
			return var;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0008DFD8 File Offset: 0x0008C1D8
		internal TListType[] VisitList<TListType>(TListType[] tList, VarMap replacementVarMap) where TListType : ColumnMap
		{
			TListType[] array = new TListType[tList.Length];
			for (int i = 0; i < tList.Length; i++)
			{
				array[i] = (TListType)((object)tList[i].Accept<ColumnMap, VarMap>(this, replacementVarMap));
			}
			return array;
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0008E01C File Offset: 0x0008C21C
		protected override EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = (SimpleColumnMap)entityIdentity.EntitySetColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] array = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new DiscriminatedEntityIdentity(simpleColumnMap, entityIdentity.EntitySetMap, array);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x0008E058 File Offset: 0x0008C258
		protected override EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, VarMap replacementVarMap)
		{
			SimpleColumnMap[] array = this.VisitList<SimpleColumnMap>(entityIdentity.Keys, replacementVarMap);
			return new SimpleEntityIdentity(entityIdentity.EntitySet, array);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0008E080 File Offset: 0x0008C280
		internal override ColumnMap Visit(ComplexTypeColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)simpleColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			}
			ColumnMap[] array = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new ComplexTypeColumnMap(columnMap.Type, columnMap.Name, array, simpleColumnMap);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0008E0C8 File Offset: 0x0008C2C8
		internal override ColumnMap Visit(DiscriminatedCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap columnMap2 = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap simpleColumnMap = (SimpleColumnMap)columnMap.Discriminator.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] array = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] array2 = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new DiscriminatedCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap2, array, array2, simpleColumnMap, columnMap.DiscriminatorValue);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0008E130 File Offset: 0x0008C330
		internal override ColumnMap Visit(EntityColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			ColumnMap[] array = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new EntityColumnMap(columnMap.Type, columnMap.Name, array, entityIdentity);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x0008E16C File Offset: 0x0008C36C
		internal override ColumnMap Visit(SimplePolymorphicColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = (SimpleColumnMap)columnMap.TypeDiscriminator.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			Dictionary<object, TypedColumnMap> dictionary = new Dictionary<object, TypedColumnMap>(columnMap.TypeChoices.Comparer);
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				TypedColumnMap typedColumnMap = (TypedColumnMap)keyValuePair.Value.Accept<ColumnMap, VarMap>(this, replacementVarMap);
				dictionary[keyValuePair.Key] = typedColumnMap;
			}
			ColumnMap[] array = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new SimplePolymorphicColumnMap(columnMap.Type, columnMap.Name, array, simpleColumnMap, dictionary);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x0008E224 File Offset: 0x0008C424
		internal override ColumnMap Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, VarMap replacementVarMap)
		{
			PlanCompiler.Assert(false, "unexpected MultipleDiscriminatorPolymorphicColumnMap in ColumnMapCopier");
			return null;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0008E234 File Offset: 0x0008C434
		internal override ColumnMap Visit(RecordColumnMap columnMap, VarMap replacementVarMap)
		{
			SimpleColumnMap simpleColumnMap = columnMap.NullSentinel;
			if (simpleColumnMap != null)
			{
				simpleColumnMap = (SimpleColumnMap)simpleColumnMap.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			}
			ColumnMap[] array = this.VisitList<ColumnMap>(columnMap.Properties, replacementVarMap);
			return new RecordColumnMap(columnMap.Type, columnMap.Name, array, simpleColumnMap);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0008E27C File Offset: 0x0008C47C
		internal override ColumnMap Visit(RefColumnMap columnMap, VarMap replacementVarMap)
		{
			EntityIdentity entityIdentity = base.VisitEntityIdentity(columnMap.EntityIdentity, replacementVarMap);
			return new RefColumnMap(columnMap.Type, columnMap.Name, entityIdentity);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0008E2A9 File Offset: 0x0008C4A9
		internal override ColumnMap Visit(ScalarColumnMap columnMap, VarMap replacementVarMap)
		{
			return new ScalarColumnMap(columnMap.Type, columnMap.Name, columnMap.CommandId, columnMap.ColumnPos);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0008E2C8 File Offset: 0x0008C4C8
		internal override ColumnMap Visit(SimpleCollectionColumnMap columnMap, VarMap replacementVarMap)
		{
			ColumnMap columnMap2 = columnMap.Element.Accept<ColumnMap, VarMap>(this, replacementVarMap);
			SimpleColumnMap[] array = this.VisitList<SimpleColumnMap>(columnMap.Keys, replacementVarMap);
			SimpleColumnMap[] array2 = this.VisitList<SimpleColumnMap>(columnMap.ForeignKeys, replacementVarMap);
			return new SimpleCollectionColumnMap(columnMap.Type, columnMap.Name, columnMap2, array, array2);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0008E314 File Offset: 0x0008C514
		internal override ColumnMap Visit(VarRefColumnMap columnMap, VarMap replacementVarMap)
		{
			Var replacementVar = ColumnMapCopier.GetReplacementVar(columnMap.Var, replacementVarMap);
			return new VarRefColumnMap(columnMap.Type, columnMap.Name, replacementVar);
		}

		// Token: 0x04000EEA RID: 3818
		private static readonly ColumnMapCopier _instance = new ColumnMapCopier();
	}
}
