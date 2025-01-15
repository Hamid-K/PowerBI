using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000220 RID: 544
	public class DbComplexPropertyEntry : DbPropertyEntry
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x00051C32 File Offset: 0x0004FE32
		internal new static DbComplexPropertyEntry Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbComplexPropertyEntry)internalPropertyEntry.CreateDbMemberEntry();
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00051C3F File Offset: 0x0004FE3F
		internal DbComplexPropertyEntry(InternalPropertyEntry internalPropertyEntry)
			: base(internalPropertyEntry)
		{
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00051C48 File Offset: 0x0004FE48
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(((InternalPropertyEntry)this.InternalMemberEntry).Property(propertyName, null, false));
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00051C6E File Offset: 0x0004FE6E
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(((InternalPropertyEntry)this.InternalMemberEntry).Property(propertyName, null, true));
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00051C94 File Offset: 0x0004FE94
		public new DbComplexPropertyEntry<TEntity, TComplexProperty> Cast<TEntity, TComplexProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this.InternalMemberEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TComplexProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbComplexPropertyEntry).Name, typeof(TEntity).Name, typeof(TComplexProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbComplexPropertyEntry<TEntity, TComplexProperty>.Create((InternalPropertyEntry)this.InternalMemberEntry);
		}
	}
}
