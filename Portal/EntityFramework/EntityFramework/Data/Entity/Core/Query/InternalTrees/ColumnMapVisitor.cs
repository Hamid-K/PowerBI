using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038A RID: 906
	internal abstract class ColumnMapVisitor<TArgType>
	{
		// Token: 0x06002BF6 RID: 11254 RVA: 0x0008EBA8 File Offset: 0x0008CDA8
		protected void VisitList<TListType>(TListType[] columnMaps, TArgType arg) where TListType : ColumnMap
		{
			for (int i = 0; i < columnMaps.Length; i++)
			{
				columnMaps[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0008EBD8 File Offset: 0x0008CDD8
		protected void VisitEntityIdentity(EntityIdentity entityIdentity, TArgType arg)
		{
			DiscriminatedEntityIdentity discriminatedEntityIdentity = entityIdentity as DiscriminatedEntityIdentity;
			if (discriminatedEntityIdentity != null)
			{
				this.VisitEntityIdentity(discriminatedEntityIdentity, arg);
				return;
			}
			this.VisitEntityIdentity((SimpleEntityIdentity)entityIdentity, arg);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0008EC08 File Offset: 0x0008CE08
		protected virtual void VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			entityIdentity.EntitySetColumnMap.Accept<TArgType>(this, arg);
			SimpleColumnMap[] keys = entityIdentity.Keys;
			for (int i = 0; i < keys.Length; i++)
			{
				keys[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0008EC44 File Offset: 0x0008CE44
		protected virtual void VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			SimpleColumnMap[] keys = entityIdentity.Keys;
			for (int i = 0; i < keys.Length; i++)
			{
				keys[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x0008EC70 File Offset: 0x0008CE70
		internal virtual void Visit(ComplexTypeColumnMap columnMap, TArgType arg)
		{
			ColumnMap nullSentinel = columnMap.NullSentinel;
			if (nullSentinel != null)
			{
				nullSentinel.Accept<TArgType>(this, arg);
			}
			ColumnMap[] properties = columnMap.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x0008ECB0 File Offset: 0x0008CEB0
		internal virtual void Visit(DiscriminatedCollectionColumnMap columnMap, TArgType arg)
		{
			columnMap.Discriminator.Accept<TArgType>(this, arg);
			SimpleColumnMap[] array = columnMap.ForeignKeys;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Accept<TArgType>(this, arg);
			}
			array = columnMap.Keys;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Accept<TArgType>(this, arg);
			}
			columnMap.Element.Accept<TArgType>(this, arg);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0008ED18 File Offset: 0x0008CF18
		internal virtual void Visit(EntityColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
			ColumnMap[] properties = columnMap.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0008ED54 File Offset: 0x0008CF54
		internal virtual void Visit(SimplePolymorphicColumnMap columnMap, TArgType arg)
		{
			columnMap.TypeDiscriminator.Accept<TArgType>(this, arg);
			foreach (TypedColumnMap typedColumnMap in columnMap.TypeChoices.Values)
			{
				typedColumnMap.Accept<TArgType>(this, arg);
			}
			ColumnMap[] properties = columnMap.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x0008EDD8 File Offset: 0x0008CFD8
		internal virtual void Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TArgType arg)
		{
			SimpleColumnMap[] typeDiscriminators = columnMap.TypeDiscriminators;
			for (int i = 0; i < typeDiscriminators.Length; i++)
			{
				typeDiscriminators[i].Accept<TArgType>(this, arg);
			}
			foreach (TypedColumnMap typedColumnMap in columnMap.TypeChoices.Values)
			{
				typedColumnMap.Accept<TArgType>(this, arg);
			}
			ColumnMap[] properties = columnMap.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0008EE70 File Offset: 0x0008D070
		internal virtual void Visit(RecordColumnMap columnMap, TArgType arg)
		{
			ColumnMap nullSentinel = columnMap.NullSentinel;
			if (nullSentinel != null)
			{
				nullSentinel.Accept<TArgType>(this, arg);
			}
			ColumnMap[] properties = columnMap.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				properties[i].Accept<TArgType>(this, arg);
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0008EEAE File Offset: 0x0008D0AE
		internal virtual void Visit(RefColumnMap columnMap, TArgType arg)
		{
			this.VisitEntityIdentity(columnMap.EntityIdentity, arg);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0008EEBD File Offset: 0x0008D0BD
		internal virtual void Visit(ScalarColumnMap columnMap, TArgType arg)
		{
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0008EEC0 File Offset: 0x0008D0C0
		internal virtual void Visit(SimpleCollectionColumnMap columnMap, TArgType arg)
		{
			SimpleColumnMap[] array = columnMap.ForeignKeys;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Accept<TArgType>(this, arg);
			}
			array = columnMap.Keys;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Accept<TArgType>(this, arg);
			}
			columnMap.Element.Accept<TArgType>(this, arg);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0008EF18 File Offset: 0x0008D118
		internal virtual void Visit(VarRefColumnMap columnMap, TArgType arg)
		{
		}
	}
}
