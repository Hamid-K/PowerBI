using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB9 RID: 3513
	[DataContract]
	internal abstract class CdpaPropertyFilter : IEquatable<CdpaPropertyFilter>
	{
		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06005F86 RID: 24454
		[DataMember(Name = "operator", IsRequired = true)]
		public abstract string Operator { get; }

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x001488F6 File Offset: 0x00146AF6
		// (set) Token: 0x06005F88 RID: 24456 RVA: 0x001488FE File Offset: 0x00146AFE
		[DataMember(Name = "propertyName", IsRequired = true)]
		public string PropertyName { get; set; }

		// Token: 0x06005F89 RID: 24457 RVA: 0x000033E7 File Offset: 0x000015E7
		public virtual CdpaPropertyFilter Not()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x00148908 File Offset: 0x00146B08
		public CdpaPropertyFilterAndGroup ToAndGroup()
		{
			return new CdpaPropertyFilterAndGroup
			{
				PropertyFilters = new CdpaPropertyFilter[] { this }
			};
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x0014892C File Offset: 0x00146B2C
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaPropertyFilter);
		}

		// Token: 0x06005F8C RID: 24460
		public abstract bool Equals(CdpaPropertyFilter other);

		// Token: 0x06005F8D RID: 24461
		public abstract override int GetHashCode();
	}
}
