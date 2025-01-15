using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Validation
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class DbEntityValidationException : DataException
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0000F613 File Offset: 0x0000D813
		public DbEntityValidationException()
			: this(Strings.DbEntityValidationException_ValidationFailed)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000F620 File Offset: 0x0000D820
		public DbEntityValidationException(string message)
			: this(message, Enumerable.Empty<DbEntityValidationResult>())
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000F62E File Offset: 0x0000D82E
		public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults)
			: base(message)
		{
			Check.NotNull<IEnumerable<DbEntityValidationResult>>(entityValidationResults, "entityValidationResults");
			this.InititializeValidationResults(entityValidationResults);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000F64A File Offset: 0x0000D84A
		public DbEntityValidationException(string message, Exception innerException)
			: this(message, Enumerable.Empty<DbEntityValidationResult>(), innerException)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000F659 File Offset: 0x0000D859
		public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults, Exception innerException)
			: base(message, innerException)
		{
			Check.NotNull<IEnumerable<DbEntityValidationResult>>(entityValidationResults, "entityValidationResults");
			this.InititializeValidationResults(entityValidationResults);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000F676 File Offset: 0x0000D876
		protected DbEntityValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._entityValidationResults = (List<DbEntityValidationResult>)info.GetValue("EntityValidationErrors", typeof(List<DbEntityValidationResult>));
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		public IEnumerable<DbEntityValidationResult> EntityValidationErrors
		{
			get
			{
				return this._entityValidationResults;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("EntityValidationErrors", this._entityValidationResults);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000F6C3 File Offset: 0x0000D8C3
		private void InititializeValidationResults(IEnumerable<DbEntityValidationResult> entityValidationResults)
		{
			this._entityValidationResults = ((entityValidationResults == null) ? new List<DbEntityValidationResult>() : entityValidationResults.ToList<DbEntityValidationResult>());
		}

		// Token: 0x0400010D RID: 269
		private IList<DbEntityValidationResult> _entityValidationResults;
	}
}
