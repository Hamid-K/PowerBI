using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000634 RID: 1588
	internal class ColumnMapKeyBuilder : ColumnMapVisitor<int>
	{
		// Token: 0x06004C66 RID: 19558 RVA: 0x0010DFD9 File Offset: 0x0010C1D9
		private ColumnMapKeyBuilder(SpanIndex spanIndex)
		{
			this._spanIndex = spanIndex;
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x0010DFF4 File Offset: 0x0010C1F4
		internal static string GetColumnMapKey(ColumnMap columnMap, SpanIndex spanIndex)
		{
			ColumnMapKeyBuilder columnMapKeyBuilder = new ColumnMapKeyBuilder(spanIndex);
			columnMap.Accept<int>(columnMapKeyBuilder, 0);
			return columnMapKeyBuilder._builder.ToString();
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0010E01B File Offset: 0x0010C21B
		internal void Append(string value)
		{
			this._builder.Append(value);
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0010E02A File Offset: 0x0010C22A
		internal void Append(string prefix, Type type)
		{
			this.Append(prefix, type.AssemblyQualifiedName);
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0010E03C File Offset: 0x0010C23C
		internal void Append(string prefix, TypeUsage type)
		{
			if (type != null)
			{
				InitializerMetadata initializerMetadata;
				if (InitializerMetadata.TryGetInitializerMetadata(type, out initializerMetadata))
				{
					initializerMetadata.AppendColumnMapKey(this);
				}
				this.Append(prefix, type.EdmType);
			}
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0010E06C File Offset: 0x0010C26C
		internal void Append(string prefix, EdmType type)
		{
			if (type != null)
			{
				this.Append(prefix, type.NamespaceName);
				this.Append(".", type.Name);
				if (type.BuiltInTypeKind == BuiltInTypeKind.RowType && this._spanIndex != null)
				{
					this.Append("<<");
					Dictionary<int, AssociationEndMember> spanMap = this._spanIndex.GetSpanMap((RowType)type);
					if (spanMap != null)
					{
						string text = string.Empty;
						foreach (KeyValuePair<int, AssociationEndMember> keyValuePair in spanMap)
						{
							this.Append(text);
							this.AppendValue("C", keyValuePair.Key);
							this.Append(":", keyValuePair.Value.DeclaringType);
							this.Append(".", keyValuePair.Value.Name);
							text = ",";
						}
					}
					this.Append(">>");
				}
			}
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0010E178 File Offset: 0x0010C378
		private void Append(string prefix, string value)
		{
			this.Append(prefix);
			this.Append("'");
			this.Append(value);
			this.Append("'");
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0010E19E File Offset: 0x0010C39E
		private void Append(string prefix, ColumnMap columnMap)
		{
			this.Append(prefix);
			this.Append("[");
			if (columnMap != null)
			{
				columnMap.Accept<int>(this, 0);
			}
			this.Append("]");
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0010E1C8 File Offset: 0x0010C3C8
		private void Append(string prefix, IEnumerable<ColumnMap> elements)
		{
			this.Append(prefix);
			this.Append("{");
			if (elements != null)
			{
				string text = string.Empty;
				foreach (ColumnMap columnMap in elements)
				{
					this.Append(text, columnMap);
					text = ",";
				}
			}
			this.Append("}");
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0010E240 File Offset: 0x0010C440
		private void Append(string prefix, EntityIdentity entityIdentity)
		{
			this.Append(prefix);
			this.Append("[");
			this.Append(",K", entityIdentity.Keys);
			SimpleEntityIdentity simpleEntityIdentity = entityIdentity as SimpleEntityIdentity;
			if (simpleEntityIdentity != null)
			{
				this.Append(",", simpleEntityIdentity.EntitySet);
			}
			else
			{
				DiscriminatedEntityIdentity discriminatedEntityIdentity = (DiscriminatedEntityIdentity)entityIdentity;
				this.Append("CM", discriminatedEntityIdentity.EntitySetColumnMap);
				foreach (EntitySet entitySet in discriminatedEntityIdentity.EntitySetMap)
				{
					this.Append(",E", entitySet);
				}
			}
			this.Append("]");
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0010E2D7 File Offset: 0x0010C4D7
		private void Append(string prefix, EntitySet entitySet)
		{
			if (entitySet != null)
			{
				this.Append(prefix, entitySet.EntityContainer.Name);
				this.Append(".", entitySet.Name);
			}
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0010E2FF File Offset: 0x0010C4FF
		private void AppendValue(string prefix, object value)
		{
			this.Append(prefix, string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { value }));
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0010E321 File Offset: 0x0010C521
		internal override void Visit(ComplexTypeColumnMap columnMap, int dummy)
		{
			this.Append("C-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0010E358 File Offset: 0x0010C558
		internal override void Visit(DiscriminatedCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-D", columnMap.Discriminator);
			this.AppendValue(",DV", columnMap.DiscriminatorValue);
			this.Append(",FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0010E3BC File Offset: 0x0010C5BC
		internal override void Visit(EntityColumnMap columnMap, int dummy)
		{
			this.Append("E-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
			this.Append(",I", columnMap.EntityIdentity);
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0010E410 File Offset: 0x0010C610
		internal override void Visit(SimplePolymorphicColumnMap columnMap, int dummy)
		{
			this.Append("SP-", columnMap.Type);
			this.Append(",D", columnMap.TypeDiscriminator);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				this.AppendValue(",K", keyValuePair.Key);
				this.Append(":", keyValuePair.Value);
			}
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0010E4C4 File Offset: 0x0010C6C4
		internal override void Visit(RecordColumnMap columnMap, int dummy)
		{
			this.Append("R-", columnMap.Type);
			this.Append(",N", columnMap.NullSentinel);
			this.Append(",P", columnMap.Properties);
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0010E4FC File Offset: 0x0010C6FC
		internal override void Visit(RefColumnMap columnMap, int dummy)
		{
			this.Append("Ref-", columnMap.EntityIdentity);
			EntityType entityType;
			TypeHelpers.TryGetRefEntityType(columnMap.Type, out entityType);
			this.Append(",T", entityType);
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0010E534 File Offset: 0x0010C734
		internal override void Visit(ScalarColumnMap columnMap, int dummy)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "S({0}-{1}:{2})", new object[]
			{
				columnMap.CommandId,
				columnMap.ColumnPos,
				columnMap.Type.Identity
			});
			this.Append(text);
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0010E588 File Offset: 0x0010C788
		internal override void Visit(SimpleCollectionColumnMap columnMap, int dummy)
		{
			this.Append("DC-FK", columnMap.ForeignKeys);
			this.Append(",K", columnMap.Keys);
			this.Append(",E", columnMap.Element);
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0010E5BD File Offset: 0x0010C7BD
		internal override void Visit(VarRefColumnMap columnMap, int dummy)
		{
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0010E5BF File Offset: 0x0010C7BF
		internal override void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, int dummy)
		{
			this.Append(string.Format(CultureInfo.InvariantCulture, "MD-{0}", new object[] { Guid.NewGuid() }));
		}

		// Token: 0x04001B0B RID: 6923
		private readonly StringBuilder _builder = new StringBuilder();

		// Token: 0x04001B0C RID: 6924
		private readonly SpanIndex _spanIndex;
	}
}
