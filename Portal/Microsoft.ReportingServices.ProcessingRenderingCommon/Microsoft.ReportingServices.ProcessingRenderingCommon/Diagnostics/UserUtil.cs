using System;
using System.Data;
using System.Security.Principal;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002F RID: 47
	internal static class UserUtil
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005EAE File Offset: 0x000040AE
		public static string CustomAuthSystemUserName
		{
			get
			{
				return "SYSTEM_USER";
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005EB8 File Offset: 0x000040B8
		public static string GetUserNameBySid(IDataRecord record, int indexNameBasedOnSid, int indexBackupUserName)
		{
			string text;
			if (!record.IsDBNull(indexNameBasedOnSid))
			{
				text = record.GetString(indexNameBasedOnSid);
			}
			else if (!record.IsDBNull(indexBackupUserName))
			{
				text = record.GetString(indexBackupUserName);
			}
			else
			{
				text = ErrorStrings.UserNameUnknown;
			}
			return text;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005EF4 File Offset: 0x000040F4
		public static string GetCurrentWindowsUserName()
		{
			string text = null;
			RequestContext reqContext = ProcessingContext.ReqContext;
			if (reqContext != null && reqContext.User != null && reqContext.User.Identity != null)
			{
				text = reqContext.User.Identity.Name;
			}
			if (text == null || text.Length == 0)
			{
				text = UserUtil.GetWindowsIdentityName();
			}
			return text;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005F44 File Offset: 0x00004144
		public static string GetWindowsIdentityName()
		{
			string name;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				name = current.Name;
			}
			return name;
		}
	}
}
