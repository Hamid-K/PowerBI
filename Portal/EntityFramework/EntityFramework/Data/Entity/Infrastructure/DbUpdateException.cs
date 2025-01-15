using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023A RID: 570
	[Serializable]
	public class DbUpdateException : DataException
	{
		// Token: 0x06001E1F RID: 7711 RVA: 0x0005424B File Offset: 0x0005244B
		internal DbUpdateException(InternalContext internalContext, UpdateException innerException, bool involvesIndependentAssociations)
			: base(involvesIndependentAssociations ? Strings.DbContext_IndependentAssociationUpdateException : innerException.Message, innerException)
		{
			this._internalContext = internalContext;
			this._involvesIndependentAssociations = involvesIndependentAssociations;
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00054274 File Offset: 0x00052474
		public IEnumerable<DbEntityEntry> Entries
		{
			get
			{
				UpdateException ex = base.InnerException as UpdateException;
				if (this._involvesIndependentAssociations || this._internalContext == null || ex == null || ex.StateEntries == null)
				{
					return Enumerable.Empty<DbEntityEntry>();
				}
				return ex.StateEntries.Select((ObjectStateEntry e) => new DbEntityEntry(new InternalEntityEntry(this._internalContext, new StateEntryAdapter(e))));
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000542C5 File Offset: 0x000524C5
		public DbUpdateException()
		{
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000542CD File Offset: 0x000524CD
		public DbUpdateException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000542D6 File Offset: 0x000524D6
		public DbUpdateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000542E0 File Offset: 0x000524E0
		protected DbUpdateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._involvesIndependentAssociations = info.GetBoolean("InvolvesIndependentAssociations");
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000542FB File Offset: 0x000524FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvolvesIndependentAssociations", this._involvesIndependentAssociations);
		}

		// Token: 0x04000B2A RID: 2858
		[NonSerialized]
		private readonly InternalContext _internalContext;

		// Token: 0x04000B2B RID: 2859
		private readonly bool _involvesIndependentAssociations;
	}
}
