using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002DF RID: 735
	internal sealed class TaskDelegationStatePropertyDefinition : GenericPropertyDefinition<TaskDelegationState>
	{
		// Token: 0x060019F4 RID: 6644 RVA: 0x000468A5 File Offset: 0x000458A5
		internal TaskDelegationStatePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000468B4 File Offset: 0x000458B4
		internal override object Parse(string value)
		{
			if (value != null)
			{
				if (value == "NoMatch")
				{
					return TaskDelegationState.NoDelegation;
				}
				if (value == "OwnNew")
				{
					return TaskDelegationState.Unknown;
				}
				if (value == "Owned")
				{
					return TaskDelegationState.Accepted;
				}
				if (value == "Accepted")
				{
					return TaskDelegationState.Declined;
				}
			}
			EwsUtilities.Assert(false, "TaskDelegationStatePropertyDefinition.Parse", string.Format("TaskDelegationStatePropertyDefinition.Parse(): value {0} cannot be handled.", value));
			return null;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00046930 File Offset: 0x00045930
		internal override string ToString(object value)
		{
			switch ((TaskDelegationState)value)
			{
			case TaskDelegationState.NoDelegation:
				return "NoMatch";
			case TaskDelegationState.Unknown:
				return "OwnNew";
			case TaskDelegationState.Accepted:
				return "Owned";
			case TaskDelegationState.Declined:
				return "Accepted";
			default:
				EwsUtilities.Assert(false, "TaskDelegationStatePropertyDefinition.ToString", "Invalid TaskDelegationState value.");
				return null;
			}
		}

		// Token: 0x04001409 RID: 5129
		private const string NoMatch = "NoMatch";

		// Token: 0x0400140A RID: 5130
		private const string OwnNew = "OwnNew";

		// Token: 0x0400140B RID: 5131
		private const string Owned = "Owned";

		// Token: 0x0400140C RID: 5132
		private const string Accepted = "Accepted";
	}
}
