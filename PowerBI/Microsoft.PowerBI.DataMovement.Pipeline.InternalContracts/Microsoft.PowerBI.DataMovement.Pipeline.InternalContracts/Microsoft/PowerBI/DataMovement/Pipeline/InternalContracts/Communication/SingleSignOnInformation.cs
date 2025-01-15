using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class SingleSignOnInformation : OperationDataContract
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00003107 File Offset: 0x00001307
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000310F File Offset: 0x0000130F
		[DataMember(Name = "userPrincipalName", IsRequired = true, EmitDefaultValue = false)]
		public string UserPrincipalName { get; set; }

		// Token: 0x060001CF RID: 463 RVA: 0x00003118 File Offset: 0x00001318
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			SingleSignOnInformation singleSignOnInformation = obj as SingleSignOnInformation;
			return singleSignOnInformation != null && string.Equals(this.UserPrincipalName, singleSignOnInformation.UserPrincipalName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003149 File Offset: 0x00001349
		public override int GetHashCode()
		{
			if (this.UserPrincipalName == null)
			{
				return 0;
			}
			return this.UserPrincipalName.GetHashCode();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003160 File Offset: 0x00001360
		public static bool Equals(SingleSignOnInformation x, SingleSignOnInformation y)
		{
			return (x == null && y == null) || (x != null && y != null && x.Equals(y));
		}
	}
}
