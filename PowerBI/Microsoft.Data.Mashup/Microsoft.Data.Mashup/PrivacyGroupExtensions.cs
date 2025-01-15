using System;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004E RID: 78
	internal static class PrivacyGroupExtensions
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		internal static string ToPrivacyGroup(this FirewallGroupType rule)
		{
			switch (rule)
			{
			case FirewallGroupType.None:
			case FirewallGroupType.SingleUnclassified:
			case FirewallGroupType.MultipleUnclassified:
				return "None";
			case FirewallGroupType.Public:
				return "Public";
			case FirewallGroupType.Organizational:
				return "Organizational";
			case FirewallGroupType.SeparatePrivate:
			case FirewallGroupType.CombinedPrivate:
				return "Private";
			}
			throw new NotSupportedException(ProviderErrorStrings.UnrecognizedPrivacy);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		internal static FirewallGroupType ToFirewallGroupType(this string rule, string groupName)
		{
			if (rule == "None")
			{
				PrivacyGroupExtensions.EnsureGroupNameIsNull(groupName);
				return FirewallGroupType.None;
			}
			if (rule == "Public")
			{
				PrivacyGroupExtensions.EnsureGroupNameIsNull(groupName);
				return FirewallGroupType.Public;
			}
			if (rule == "Organizational")
			{
				PrivacyGroupExtensions.EnsureGroupNameIsNull(groupName);
				return FirewallGroupType.Organizational;
			}
			if (!(rule == "Private"))
			{
				throw new MashupPrivacyException(ProviderErrorStrings.UnrecognizedPrivacy);
			}
			if (groupName == null)
			{
				return FirewallGroupType.SeparatePrivate;
			}
			return FirewallGroupType.Named;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E268 File Offset: 0x0000C468
		internal static FirewallRule ToFirewallRule(this DataSourceSetting dataSourceSetting, DataSource dataSource)
		{
			FirewallGroupType firewallGroupType = dataSourceSetting.PrivacySetting.ToFirewallGroupType(dataSourceSetting.PrivateGroupName);
			string text = ((firewallGroupType == FirewallGroupType.Named) ? dataSourceSetting.PrivateGroupName : null);
			return new FirewallRule(dataSource.NormalizedResource, firewallGroupType, text, dataSourceSetting.IsTrusted);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		internal static DataSourceSetting ToDataSourceSetting(this FirewallGroup2 firewallGroup)
		{
			return new DataSourceSetting
			{
				IsTrusted = new bool?(firewallGroup.IsTrusted),
				PrivacySetting = ((FirewallGroupType)firewallGroup.GroupType).ToPrivacyGroup(),
				PrivateGroupName = firewallGroup.GroupName
			};
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E2DD File Offset: 0x0000C4DD
		private static void EnsureGroupNameIsNull(string groupName)
		{
			if (groupName != null)
			{
				throw new MashupPrivacyException(ProviderErrorStrings.InvalidPrivacyWithGroupName("Private"));
			}
		}
	}
}
