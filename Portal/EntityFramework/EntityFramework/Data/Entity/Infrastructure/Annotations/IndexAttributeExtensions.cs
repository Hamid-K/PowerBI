﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C7 RID: 711
	internal static class IndexAttributeExtensions
	{
		// Token: 0x06002239 RID: 8761 RVA: 0x00060800 File Offset: 0x0005EA00
		internal static CompatibilityResult IsCompatibleWith(this IndexAttribute me, IndexAttribute other, bool ignoreOrder = false)
		{
			if (me == other || other == null)
			{
				return new CompatibilityResult(true, null);
			}
			string text = null;
			if (me.Name != other.Name)
			{
				text = Strings.ConflictingIndexAttributeProperty("Name", me.Name, other.Name);
			}
			if (!ignoreOrder && me.Order != -1 && other.Order != -1 && me.Order != other.Order)
			{
				text = ((text == null) ? "" : (text + Environment.NewLine + "\t"));
				text += Strings.ConflictingIndexAttributeProperty("Order", me.Order, other.Order);
			}
			if (me.IsClusteredConfigured && other.IsClusteredConfigured && me.IsClustered != other.IsClustered)
			{
				text = ((text == null) ? "" : (text + Environment.NewLine + "\t"));
				text += Strings.ConflictingIndexAttributeProperty("IsClustered", me.IsClustered, other.IsClustered);
			}
			if (me.IsUniqueConfigured && other.IsUniqueConfigured && me.IsUnique != other.IsUnique)
			{
				text = ((text == null) ? "" : (text + Environment.NewLine + "\t"));
				text += Strings.ConflictingIndexAttributeProperty("IsUnique", me.IsUnique, other.IsUnique);
			}
			return new CompatibilityResult(text == null, text);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00060978 File Offset: 0x0005EB78
		internal static IndexAttribute MergeWith(this IndexAttribute me, IndexAttribute other, bool ignoreOrder = false)
		{
			if (me == other || other == null)
			{
				return me;
			}
			CompatibilityResult compatibilityResult = me.IsCompatibleWith(other, ignoreOrder);
			if (!compatibilityResult)
			{
				throw new InvalidOperationException(Strings.ConflictingIndexAttribute(me.Name, Environment.NewLine + "\t" + compatibilityResult.ErrorMessage));
			}
			IndexAttribute indexAttribute = ((me.Name != null) ? new IndexAttribute(me.Name) : ((other.Name != null) ? new IndexAttribute(other.Name) : new IndexAttribute()));
			if (!ignoreOrder)
			{
				if (me.Order != -1)
				{
					indexAttribute.Order = me.Order;
				}
				else if (other.Order != -1)
				{
					indexAttribute.Order = other.Order;
				}
			}
			if (me.IsClusteredConfigured)
			{
				indexAttribute.IsClustered = me.IsClustered;
			}
			else if (other.IsClusteredConfigured)
			{
				indexAttribute.IsClustered = other.IsClustered;
			}
			if (me.IsUniqueConfigured)
			{
				indexAttribute.IsUnique = me.IsUnique;
			}
			else if (other.IsUniqueConfigured)
			{
				indexAttribute.IsUnique = other.IsUnique;
			}
			return indexAttribute;
		}
	}
}
