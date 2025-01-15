using System;
using System.Collections;
using System.DirectoryServices;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000018 RID: 24
	public sealed class GroupManager
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000429C File Offset: 0x0000249C
		public static void AddAccountToPerformanceMonitorUsers(AccountCredentials accountCredentials)
		{
			DirectoryEntry perfMonDirectoryEntry = GroupManager.GetPerfMonDirectoryEntry();
			if (!GroupManager.DoesGroupContainAccount(perfMonDirectoryEntry, accountCredentials))
			{
				GroupManager.AddUserToGroup(perfMonDirectoryEntry, accountCredentials);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000042C0 File Offset: 0x000024C0
		private static DirectoryEntry GetPerfMonDirectoryEntry()
		{
			string text = ((NTAccount)new SecurityIdentifier(WellKnownSidType.BuiltinPerformanceMonitoringUsersSid, null).Translate(typeof(NTAccount))).Value;
			int num = text.IndexOf('\\');
			if (num > -1)
			{
				text = text.Substring(num + 1);
			}
			return new DirectoryEntry(string.Format("WinNT://{0}/{1}", Environment.MachineName, text));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000431C File Offset: 0x0000251C
		private static bool DoesGroupContainAccount(DirectoryEntry group, AccountCredentials accountCredentials)
		{
			SecurityIdentifier securityIdentifier = accountCredentials.GetSecurityIdentifier();
			foreach (object obj in ((IEnumerable)group.Invoke("members", Array.Empty<object>())))
			{
				PropertyValueCollection propertyValueCollection = new DirectoryEntry(obj).Properties["objectSid"];
				if (propertyValueCollection.Count > 0 && new SecurityIdentifier((byte[])propertyValueCollection[0], 0).Equals(securityIdentifier))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000043C0 File Offset: 0x000025C0
		private static void AddUserToGroup(DirectoryEntry group, AccountCredentials accountCredentials)
		{
			string text = string.Format("WinNT://{0}/{1},user", accountCredentials.Domain, accountCredentials.UserId);
			group.Invoke("Add", new object[] { text });
			group.CommitChanges();
		}
	}
}
