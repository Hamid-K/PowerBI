using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Validation
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class DbEntityValidationResult
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000F6DB File Offset: 0x0000D8DB
		public DbEntityValidationResult(DbEntityEntry entry, IEnumerable<DbValidationError> validationErrors)
		{
			Check.NotNull<DbEntityEntry>(entry, "entry");
			Check.NotNull<IEnumerable<DbValidationError>>(validationErrors, "validationErrors");
			this._entry = entry.InternalEntry;
			this._validationErrors = validationErrors.ToList<DbValidationError>();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000F713 File Offset: 0x0000D913
		internal DbEntityValidationResult(InternalEntityEntry entry, IEnumerable<DbValidationError> validationErrors)
		{
			this._entry = entry;
			this._validationErrors = validationErrors.ToList<DbValidationError>();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000F72E File Offset: 0x0000D92E
		public DbEntityEntry Entry
		{
			get
			{
				if (this._entry == null)
				{
					return null;
				}
				return new DbEntityEntry(this._entry);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000F745 File Offset: 0x0000D945
		public ICollection<DbValidationError> ValidationErrors
		{
			get
			{
				return this._validationErrors;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000F74D File Offset: 0x0000D94D
		public bool IsValid
		{
			get
			{
				return !this._validationErrors.Any<DbValidationError>();
			}
		}

		// Token: 0x0400010E RID: 270
		[NonSerialized]
		private readonly InternalEntityEntry _entry;

		// Token: 0x0400010F RID: 271
		private readonly List<DbValidationError> _validationErrors;
	}
}
