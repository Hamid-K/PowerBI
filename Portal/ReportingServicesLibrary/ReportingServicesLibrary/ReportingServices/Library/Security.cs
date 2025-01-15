using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Hybrid.OAuth;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000221 RID: 545
	internal class Security : Storage
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x00046A23 File Offset: 0x00044C23
		internal Security(UserContext userContext, bool checkSecurity)
		{
			this.m_checkSecurity = checkSecurity;
			if (checkSecurity && userContext == null)
			{
				throw new InternalCatalogException("checkSecurity without credentials");
			}
			this.m_userContext = userContext;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00046A5C File Offset: 0x00044C5C
		public static IAuthorizationExtension AuthorizationExtension
		{
			get
			{
				IAuthorizationExtension authorizationExtension = ExtensionClassFactory.GetNewInstanceExtensionClass(WebConfigUtil.WebServerAuthMode.ToString(), "Security") as IAuthorizationExtension;
				if (authorizationExtension == null)
				{
					throw new ServerConfigurationErrorException("Could not load Authorization extension");
				}
				return authorizationExtension;
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00046A9C File Offset: 0x00044C9C
		private byte[] GetPrincipalSid(string principalName)
		{
			byte[] array = null;
			IWindowsAuthenticationExtension2 windowsAuthenticationExtension = AuthenticationExtensionFactory.GetAuthenticationExtension(this.UserContext.AuthenticationType) as IWindowsAuthenticationExtension2;
			if (windowsAuthenticationExtension != null)
			{
				array = windowsAuthenticationExtension.PrincipalNameToSid(principalName);
				if (array != null && array.Length > 85)
				{
					throw new ServerConfigurationErrorException("Invalid SID returned by authentication extension" + array);
				}
			}
			return array;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00046AE8 File Offset: 0x00044CE8
		private string PrincipalNameFromSid(byte[] sid)
		{
			string text = null;
			IWindowsAuthenticationExtension2 windowsAuthenticationExtension = AuthenticationExtensionFactory.GetAuthenticationExtension(this.UserContext.AuthenticationType) as IWindowsAuthenticationExtension2;
			if (windowsAuthenticationExtension != null)
			{
				text = windowsAuthenticationExtension.SidToPrincipalName(sid);
				if (text != null && text.Length > 260)
				{
					throw new ServerConfigurationErrorException("Invalid name returned by authentication extension" + text);
				}
			}
			return text;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00046B3C File Offset: 0x00044D3C
		internal virtual Microsoft.ReportingServices.Library.Soap.Task[] GetTaskList(bool limitScope, AuthzData.SecurityScope securityScope)
		{
			if (!limitScope)
			{
				AuthzData.TaskList taskList = new AuthzData.TaskList();
				taskList.AddRange(AuthzData.m_CatalogTasks);
				taskList.AddRange(AuthzData.m_CatalogItemTasks);
				taskList.AddRange(AuthzData.m_ModelItemTasks);
				return AuthzData.TaskList.GetTaskList(taskList.ToArray());
			}
			switch (securityScope)
			{
			case AuthzData.SecurityScope.CatalogItem:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_CatalogItemTasks;
				return AuthzData.TaskList.GetTaskList(array);
			}
			case AuthzData.SecurityScope.Catalog:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_CatalogTasks;
				return AuthzData.TaskList.GetTaskList(array);
			}
			case AuthzData.SecurityScope.ModelItem:
			{
				AuthzData.SecurityTask[] array = AuthzData.m_ModelItemTasks;
				return AuthzData.TaskList.GetTaskList(array);
			}
			default:
				throw new InternalCatalogException("Unsupported SecurityScope.");
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00046BC2 File Offset: 0x00044DC2
		internal virtual Role[] GetRoleList(AuthzData.SecurityScope securityScope, bool limitScope, ExternalItemPath path)
		{
			return this.CatalogGetRoleList(securityScope, limitScope);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00046BCC File Offset: 0x00044DCC
		internal Role[] CatalogGetRoleList(AuthzData.SecurityScope securityScope, bool limitScope)
		{
			List<Role> list = new List<Role>();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetRoles", null))
			{
				if (limitScope)
				{
					instrumentedSqlCommand.AddParameter("@RoleFlags", SqlDbType.TinyInt, (int)securityScope);
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Role role = new Role();
						string @string = dataReader.GetString(0);
						role.Name = @string;
						string text = null;
						if (!dataReader.IsDBNull(1))
						{
							text = dataReader.GetString(1);
						}
						role.Description = text;
						list.Add(role);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00046C88 File Offset: 0x00044E88
		internal void CreateRole(string roleName, string roleDescription, string taskMask, AuthzData.SecurityScope scope)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateRole", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@RoleID", Guid.NewGuid());
					instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
					if (roleDescription != null)
					{
						instrumentedSqlCommand.AddParameter("@Description", SqlDbType.NVarChar, roleDescription);
					}
					instrumentedSqlCommand.AddParameter("@TaskMask", SqlDbType.NVarChar, taskMask);
					byte b = (byte)scope;
					instrumentedSqlCommand.Parameters.AddWithValue("@RoleFlags", b);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.IsSqlException && ex.SqlErrorNumber == Native.SqlUniqueIndexViolationCode)
				{
					throw new RoleAlreadyExistsException(roleName);
				}
				throw;
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00046D58 File Offset: 0x00044F58
		internal bool RoleExists(string roleName)
		{
			bool flag;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ReadRoleProperties", null))
			{
				instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					flag = dataReader.Read();
				}
			}
			return flag;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00046DC4 File Offset: 0x00044FC4
		internal virtual void GetRoleProperties(ExternalItemPath path, string roleName, out string roleDescription, out Microsoft.ReportingServices.Library.Soap.Task[] tasks)
		{
			this.CatalogGetRoleProperties(roleName, out roleDescription, out tasks);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00046DD0 File Offset: 0x00044FD0
		internal void CatalogGetRoleProperties(string roleName, out string roleDescription, out Microsoft.ReportingServices.Library.Soap.Task[] tasks)
		{
			roleDescription = null;
			string text = null;
			AuthzData.SecurityScope securityScope = AuthzData.SecurityScope.CatalogItem;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ReadRoleProperties", null))
			{
				instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new RoleNotFoundException(roleName);
					}
					if (!dataReader.IsDBNull(0))
					{
						roleDescription = dataReader.GetString(0);
					}
					text = dataReader.GetString(1);
					securityScope = (AuthzData.SecurityScope)dataReader.GetByte(2);
				}
			}
			tasks = AuthzData.TaskMaskToTaskList(text, securityScope);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00046E74 File Offset: 0x00045074
		internal void SetRoleProperties(string roleName, string roleDescription, string taskMask, AuthzData.SecurityScope scope)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetRoleProperties", null))
				{
					instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
					if (roleDescription != null)
					{
						instrumentedSqlCommand.AddParameter("@Description", SqlDbType.NVarChar, roleDescription);
					}
					instrumentedSqlCommand.AddParameter("@TaskMask", SqlDbType.NVarChar, taskMask);
					byte b = (byte)scope;
					instrumentedSqlCommand.Parameters.AddWithValue("@RoleFlags", b);
					if (instrumentedSqlCommand.ExecuteNonQuery() != 1)
					{
						throw new RoleNotFoundException(roleName);
					}
				}
				Dictionary<Guid, AuthzData.SecurityPolicy> dictionary = new Dictionary<Guid, AuthzData.SecurityPolicy>();
				using (InstrumentedSqlCommand instrumentedSqlCommand2 = base.NewStandardSqlCommandQuery("\r\nSELECT  \r\n    PrimeRoles.PolicyID,\r\n    SecData.XmlDescription, \r\n    PrimeRoles.PolicyFlag,\r\n    Catalog.Type,\r\n    Catalog.Path,\r\n    ModelItemPolicy.CatalogItemID,\r\n    ModelItemPolicy.ModelItemID,\r\n    RelatedRoles.RoleID,\r\n    RelatedRoles.RoleName,\r\n    RelatedRoles.TaskMask,\r\n    RelatedRoles.RoleFlags\r\nFROM\r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies ON \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) PrimeRoles\r\nINNER JOIN \r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies on \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) RelatedRoles \r\n\tON PrimeRoles.PolicyID = RelatedRoles.PolicyID\r\nLEFT OUTER JOIN SecData ON PrimeRoles.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\nLEFT OUTER JOIN Catalog ON PrimeRoles.PolicyID = Catalog.PolicyID AND Catalog.PolicyRoot = 1\r\nLEFT OUTER JOIN ModelItemPolicy ON PrimeRoles.PolicyID = ModelItemPolicy.PolicyID\r\nWHERE PrimeRoles.RoleName = @RoleName\r\nORDER BY PrimeRoles.PolicyID\r\n"))
				{
					instrumentedSqlCommand2.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
					instrumentedSqlCommand2.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					using (IDataReader dataReader = instrumentedSqlCommand2.ExecuteReader())
					{
						AuthzData.SecurityPolicy securityPolicy = null;
						while (dataReader.Read())
						{
							Guid guid = dataReader.GetGuid(0);
							if (dictionary.TryGetValue(guid, out securityPolicy))
							{
								securityPolicy.AddRelatedRole(dataReader);
							}
							else
							{
								securityPolicy = new AuthzData.SecurityPolicy(this, dataReader);
								if (securityPolicy.SecItemType == SecurityItemType.Unknown)
								{
									RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Policy record with unknown SecurityItemType. Id : {0}", new object[] { securityPolicy.PolicyId });
								}
								else
								{
									dictionary.Add(securityPolicy.PolicyId, securityPolicy);
								}
							}
						}
					}
				}
				foreach (AuthzData.SecurityPolicy securityPolicy2 in dictionary.Values)
				{
					try
					{
						byte[] array;
						string text;
						this.CompileXmlPolicy(securityPolicy2, out array, out text);
						using (InstrumentedSqlCommand instrumentedSqlCommand3 = this.NewStandardSqlCommand("UpdatePolicy", null))
						{
							instrumentedSqlCommand3.Parameters.AddWithValue("@PolicyID", securityPolicy2.PolicyId);
							instrumentedSqlCommand3.Parameters.AddWithValue("@PrimarySecDesc", array);
							instrumentedSqlCommand3.Parameters.AddWithValue("@SecondarySecDesc", text);
							instrumentedSqlCommand3.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
							instrumentedSqlCommand3.ExecuteNonQuery();
						}
					}
					catch (UnknownUserNameException ex)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, ex.ToString());
					}
				}
			}
			catch (ReportServerStorageException ex2)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Set role properties failed: " + ex2.SqlErrorMessage);
				if (ex2.IsSqlException && ex2.SqlErrorMessage == "Bad role flags")
				{
					throw new MixedTasksException();
				}
				throw;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000471AC File Offset: 0x000453AC
		internal void SetRolePropertiesAndInvalidatePolicies(string roleName, string roleDescription, string taskMask, AuthzData.SecurityScope scope)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetRolePropertiesAndInvalidatePolicies", null))
			{
				instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
				if (roleDescription != null)
				{
					instrumentedSqlCommand.AddParameter("@Description", SqlDbType.NVarChar, roleDescription);
				}
				instrumentedSqlCommand.AddParameter("@TaskMask", SqlDbType.NVarChar, taskMask);
				byte b = (byte)scope;
				instrumentedSqlCommand.Parameters.AddWithValue("@RoleFlags", b);
				if (instrumentedSqlCommand.ExecuteNonQuery() < 1)
				{
					throw new RoleNotFoundException(roleName);
				}
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00047240 File Offset: 0x00045440
		internal int UpdateSecurityPolicies(int updatePoliciesChunkSize)
		{
			Dictionary<Guid, AuthzData.SecurityPolicy> dictionary = new Dictionary<Guid, AuthzData.SecurityPolicy>();
			Dictionary<Guid, AuthzData.SecurityPolicy> dictionary2 = new Dictionary<Guid, AuthzData.SecurityPolicy>();
			int num = 0;
			Func<AuthzData.SecurityPolicy, string> func = delegate(AuthzData.SecurityPolicy policy)
			{
				if (policy.CatalogItemPath != null)
				{
					return policy.CatalogItemPath.ToString();
				}
				return string.Empty;
			};
			if (!this.HasDirtyPolicies())
			{
				return 0;
			}
			Dictionary<int, int> policyStates = this.GetPolicyStates();
			if (policyStates.ContainsKey(1))
			{
				num = policyStates[1];
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Validating {0} out-of-date security policies.", new object[] { num });
			bool flag = false;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetInvalidPolicies", null))
			{
				instrumentedSqlCommand.AddParameter("@TopCount", SqlDbType.Int, updatePoliciesChunkSize);
				instrumentedSqlCommand.AddParameter("@AuthType", SqlDbType.Int, (int)this.m_userContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					AuthzData.SecurityPolicy securityPolicy = null;
					while (dataReader.Read())
					{
						flag = true;
						Guid guid = dataReader.GetGuid(0);
						if (dictionary.TryGetValue(guid, out securityPolicy))
						{
							securityPolicy.AddRelatedRole(dataReader);
						}
						else
						{
							try
							{
								securityPolicy = new AuthzData.SecurityPolicy(this, dataReader);
							}
							catch (Exception ex)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Invalid security policy {0}: Exception on load: {1}", new object[]
								{
									dataReader.GetGuid(0),
									ex.Message
								});
								if (!dictionary2.ContainsKey(securityPolicy.PolicyId))
								{
									dictionary2.Add(securityPolicy.PolicyId, securityPolicy);
								}
							}
							if (securityPolicy.SecItemType == SecurityItemType.Unknown)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Invalid security policy {0}:[{1}]: Unknown SecurityItemType", new object[]
								{
									securityPolicy.PolicyId,
									func(securityPolicy)
								});
								if (!dictionary2.ContainsKey(securityPolicy.PolicyId))
								{
									dictionary2.Add(securityPolicy.PolicyId, securityPolicy);
								}
							}
							else
							{
								dictionary.Add(securityPolicy.PolicyId, securityPolicy);
							}
						}
					}
				}
			}
			if (!flag)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Marking {0} invalid security policies as orphaned.", new object[] { num });
				this.UpdateInvalidPoliciesToOrphaned();
				return 0;
			}
			int num2 = 0;
			int num3 = 0;
			foreach (AuthzData.SecurityPolicy securityPolicy2 in dictionary.Values)
			{
				try
				{
					byte[] array;
					string text;
					this.CompileXmlPolicy(securityPolicy2, out array, out text);
					using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("UpdatePolicy", null))
					{
						instrumentedSqlCommand2.Parameters.AddWithValue("@PolicyID", securityPolicy2.PolicyId);
						instrumentedSqlCommand2.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
						instrumentedSqlCommand2.Parameters.AddWithValue("@PrimarySecDesc", array);
						instrumentedSqlCommand2.Parameters.AddWithValue("@SecondarySecDesc", text);
						instrumentedSqlCommand2.ExecuteNonQuery();
					}
					num2++;
				}
				catch (WindowsAuthz1355ApiException ex2)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "AD domain is not acessible {0}", new object[] { ex2.ToString() });
					return 0;
				}
				catch (WindowsAuthz5ApiException ex3)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "AD domain access denied {0}", new object[] { ex3.ToString() });
					return 0;
				}
				catch (ReportCatalogException ex4)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Invalid security policy {0}:[{1}]: {2}", new object[]
					{
						securityPolicy2.PolicyId,
						func(securityPolicy2),
						ex4.Message
					});
					this.UpdatePolicyState(securityPolicy2.PolicyId, SecurityDescriptorState.Invalid);
					num3++;
				}
			}
			foreach (AuthzData.SecurityPolicy securityPolicy3 in dictionary2.Values)
			{
				this.UpdatePolicyState(securityPolicy3.PolicyId, SecurityDescriptorState.Invalid);
				num3++;
			}
			int num4 = num2 + num3;
			if (num4 > 0)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Validated {0} security policies: valid:{1}, invalid:{2}, out-of-date left to be validated:{3}", new object[]
				{
					num4,
					num2,
					num3,
					num - num4
				});
			}
			return num;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004774C File Offset: 0x0004594C
		private Dictionary<int, int> GetPolicyStates()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int authenticationType = (int)this.m_userContext.AuthenticationType;
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery("select NtSecDescState, count(*) as count  from SecData where AuthType = " + authenticationType + " group by NtSecDescState"))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						int @int = dataReader.GetInt32(0);
						int int2 = dataReader.GetInt32(1);
						dictionary[@int] = int2;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000477E8 File Offset: 0x000459E8
		private bool HasDirtyPolicies()
		{
			int num = 1;
			int authenticationType = (int)this.m_userContext.AuthenticationType;
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(string.Concat(new object[] { "select top 1 NtSecDescState from SecData where NtSecDescState = ", num, " and AuthType = ", authenticationType })))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00047884 File Offset: 0x00045A84
		private void UpdatePolicyState(Guid policyId, SecurityDescriptorState state)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdatePolicyStatus", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@PolicyID", policyId);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@Status", (int)state);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00047910 File Offset: 0x00045B10
		private void UpdateInvalidPoliciesToOrphaned()
		{
			int num = 1;
			int num2 = 3;
			int authenticationType = (int)this.m_userContext.AuthenticationType;
			string text = string.Concat(new object[] { "update secData set NtSecDescState = ", num2, " where NtSecDescState = ", num, " and AuthType = ", authenticationType });
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery(text))
			{
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004799C File Offset: 0x00045B9C
		internal void DeleteRole(string roleName)
		{
			Dictionary<Guid, AuthzData.SecurityPolicy> dictionary = new Dictionary<Guid, AuthzData.SecurityPolicy>();
			using (InstrumentedSqlCommand instrumentedSqlCommand = base.NewStandardSqlCommandQuery("\r\nSELECT  \r\n    PrimeRoles.PolicyID,\r\n    SecData.XmlDescription, \r\n    PrimeRoles.PolicyFlag,\r\n    Catalog.Type,\r\n    Catalog.Path,\r\n    ModelItemPolicy.CatalogItemID,\r\n    ModelItemPolicy.ModelItemID,\r\n    RelatedRoles.RoleID,\r\n    RelatedRoles.RoleName,\r\n    RelatedRoles.TaskMask,\r\n    RelatedRoles.RoleFlags\r\nFROM\r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies ON \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) PrimeRoles\r\nINNER JOIN \r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies on \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) RelatedRoles \r\n\tON PrimeRoles.PolicyID = RelatedRoles.PolicyID\r\nLEFT OUTER JOIN SecData ON PrimeRoles.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\nLEFT OUTER JOIN Catalog ON PrimeRoles.PolicyID = Catalog.PolicyID AND Catalog.PolicyRoot = 1\r\nLEFT OUTER JOIN ModelItemPolicy ON PrimeRoles.PolicyID = ModelItemPolicy.PolicyID\r\nWHERE PrimeRoles.RoleName = @RoleName\r\nORDER BY PrimeRoles.PolicyID\r\n"))
			{
				instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					AuthzData.SecurityPolicy securityPolicy = null;
					while (dataReader.Read())
					{
						Guid guid = dataReader.GetGuid(0);
						if (dictionary.TryGetValue(guid, out securityPolicy))
						{
							securityPolicy.AddRelatedRole(dataReader);
						}
						else
						{
							securityPolicy = new AuthzData.SecurityPolicy(this, dataReader);
							dictionary.Add(securityPolicy.PolicyId, securityPolicy);
						}
					}
				}
			}
			foreach (AuthzData.SecurityPolicy securityPolicy2 in dictionary.Values)
			{
				securityPolicy2.XmlPolicy = this.RemoveRoleFromPolicy(securityPolicy2.XmlPolicy, roleName);
				this.InnerSetPolicies(securityPolicy2);
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("DeleteRole", null))
			{
				instrumentedSqlCommand2.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
				if (instrumentedSqlCommand2.ExecuteNonQuery() == 0)
				{
					throw new RoleNotFoundException(roleName);
				}
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00047B08 File Offset: 0x00045D08
		private string RemoveRoleFromPolicy(string xmlPolicy, string role)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xmlPolicy);
			Hashtable hashtable = null;
			foreach (object obj in xmlDocument.DocumentElement.SelectNodes("/Policies/Policy"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("GroupUserName");
				if (xmlNode2 == null)
				{
					throw new MissingElementException("GroupUserName");
				}
				string innerText = xmlNode2.InnerText;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				AuthzData.PrincipalAndRoles principalAndRoles = new AuthzData.PrincipalAndRoles(innerText);
				foreach (object obj2 in xmlNode.SelectNodes("Roles/Role/Name"))
				{
					string innerText2 = ((XmlNode)obj2).InnerText;
					if (Localization.CatalogCultureCompare(innerText2, role) != 0)
					{
						if (principalAndRoles.m_Roles == null)
						{
							principalAndRoles.m_Roles = new ArrayList();
						}
						principalAndRoles.m_Roles.Add(innerText2);
					}
				}
				if (principalAndRoles.m_Roles != null)
				{
					hashtable.Add(innerText, principalAndRoles);
				}
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement("Policies");
			foreach (object obj3 in hashtable.Values)
			{
				AuthzData.PrincipalAndRoles principalAndRoles2 = (AuthzData.PrincipalAndRoles)obj3;
				xmlTextWriter.WriteStartElement("Policy");
				xmlTextWriter.WriteElementString("GroupUserName", principalAndRoles2.m_principal);
				xmlTextWriter.WriteStartElement("Roles");
				foreach (object obj4 in principalAndRoles2.m_Roles)
				{
					string text = (string)obj4;
					xmlTextWriter.WriteStartElement("Role");
					xmlTextWriter.WriteElementString("Name", text);
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00047D58 File Offset: 0x00045F58
		internal static string ProduceEmptyPolicy()
		{
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement("Policies");
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00047D88 File Offset: 0x00045F88
		internal virtual void SetCatalogItemPolicies(ExternalItemPath catalogItemPath, ItemType catItemType, string xmlPolicy)
		{
			if (xmlPolicy == null)
			{
				bool flag;
				string text;
				this.GetPolicies(catalogItemPath, out flag, out text);
				if (!flag)
				{
					throw new MissingParameterException("Policies");
				}
				string text2;
				string text3;
				CatalogItemNameUtility.SplitPath(catalogItemPath.Value, out text2, out text3);
				this.GetPolicies(new ExternalItemPath(text3), out flag, out xmlPolicy);
			}
			AuthzData.SecurityPolicy securityPolicy = new AuthzData.SecurityPolicy(xmlPolicy, AuthzData.GetSecType(catItemType), AuthzData.SecurityScope.CatalogItem, catalogItemPath, Guid.Empty, null);
			this.InnerSetPolicies(securityPolicy);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00047DEC File Offset: 0x00045FEC
		internal void SetSystemPolicies(string xmlPolicy)
		{
			AuthzData.SecurityPolicy securityPolicy = new AuthzData.SecurityPolicy(xmlPolicy, SecurityItemType.Catalog, AuthzData.SecurityScope.Catalog, ExternalItemPath.Empty, Guid.Empty, null);
			this.InnerSetPolicies(securityPolicy);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00047E14 File Offset: 0x00046014
		internal void SetModelItemPolicies(Guid catalogItemID, string modelItemID, string xmlPolicy, ExternalItemPath catalogItemPath)
		{
			AuthzData.SecurityPolicy securityPolicy = new AuthzData.SecurityPolicy(xmlPolicy, SecurityItemType.ModelItem, AuthzData.SecurityScope.ModelItem, catalogItemPath, catalogItemID, modelItemID);
			this.InnerSetPolicies(securityPolicy);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00047E38 File Offset: 0x00046038
		private void InnerSetPolicies(AuthzData.SecurityPolicy policy)
		{
			AuthzData.SecurityScope scope = policy.Scope;
			ExternalItemPath catalogItemPath = policy.CatalogItemPath;
			byte[] array = null;
			string text = null;
			Hashtable hashtable = this.CompileXmlPolicy(policy, out array, out text);
			Guid guid;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.BuildSqlCommandFromScope(policy, scope, catalogItemPath))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				instrumentedSqlCommand.Parameters.AddWithValue("@PrimarySecDesc", array);
				if (text != null)
				{
					instrumentedSqlCommand.AddParameter("@SecondarySecDesc", SqlDbType.NText, text);
				}
				instrumentedSqlCommand.AddParameter("@XmlPolicy", SqlDbType.NText, policy.XmlPolicy);
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@PolicyID", SqlDbType.UniqueIdentifier);
				sqlParameter.Direction = ParameterDirection.Output;
				instrumentedSqlCommand.ExecuteNonQuery();
				guid = (Guid)sqlParameter.Value;
			}
			if (hashtable != null)
			{
				foreach (object obj in hashtable.Values)
				{
					AuthzData.PrincipalAndRoles principalAndRoles = (AuthzData.PrincipalAndRoles)obj;
					Guid guid2;
					using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("UpdatePolicyPrincipal", null))
					{
						instrumentedSqlCommand2.Parameters.AddWithValue("@PolicyID", guid);
						byte[] principalSid = this.GetPrincipalSid(principalAndRoles.m_principal);
						if (principalSid != null)
						{
							instrumentedSqlCommand2.Parameters.AddWithValue("@PrincipalSid", principalSid).SqlDbType = SqlDbType.VarBinary;
						}
						instrumentedSqlCommand2.AddParameter("@PrincipalName", SqlDbType.NVarChar, principalAndRoles.m_principal);
						instrumentedSqlCommand2.Parameters.AddWithValue("@PrincipalAuthType", (int)this.m_userContext.AuthenticationType);
						instrumentedSqlCommand2.AddParameter("@RoleName", SqlDbType.NVarChar, principalAndRoles.m_Roles[0]);
						SqlParameter sqlParameter2 = instrumentedSqlCommand2.Parameters.Add("@PrincipalID", SqlDbType.UniqueIdentifier);
						sqlParameter2.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter3 = instrumentedSqlCommand2.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier);
						sqlParameter3.Direction = ParameterDirection.Output;
						instrumentedSqlCommand2.ExecuteNonQuery();
						guid2 = (Guid)sqlParameter2.Value;
						Guid guid3 = (Guid)sqlParameter3.Value;
					}
					for (int i = 1; i < principalAndRoles.m_Roles.Count; i++)
					{
						using (InstrumentedSqlCommand instrumentedSqlCommand3 = this.NewStandardSqlCommand("UpdatePolicyRole", null))
						{
							instrumentedSqlCommand3.Parameters.AddWithValue("@PolicyID", guid);
							instrumentedSqlCommand3.Parameters.AddWithValue("@PrincipalID", guid2);
							instrumentedSqlCommand3.AddParameter("@RoleName", SqlDbType.NVarChar, principalAndRoles.m_Roles[i]);
							SqlParameter sqlParameter4 = instrumentedSqlCommand3.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier);
							sqlParameter4.Direction = ParameterDirection.Output;
							instrumentedSqlCommand3.ExecuteNonQuery();
							Guid guid4 = (Guid)sqlParameter4.Value;
						}
					}
				}
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00048180 File Offset: 0x00046380
		private InstrumentedSqlCommand BuildSqlCommandFromScope(AuthzData.SecurityPolicy policy, AuthzData.SecurityScope scope, ExternalItemPath itemName)
		{
			InstrumentedSqlCommand instrumentedSqlCommand = null;
			InstrumentedSqlCommand instrumentedSqlCommand2;
			try
			{
				switch (scope)
				{
				case AuthzData.SecurityScope.CatalogItem:
					instrumentedSqlCommand = this.NewStandardSqlCommand("SetPolicy", null);
					instrumentedSqlCommand.AddParameter("@ItemName", SqlDbType.NVarChar, itemName.Value);
					instrumentedSqlCommand.AddParameter("@ItemNameLike", SqlDbType.NVarChar, Storage.EncodeForLike(itemName.Value) + "/%");
					break;
				case AuthzData.SecurityScope.Catalog:
					instrumentedSqlCommand = this.NewStandardSqlCommand("SetSystemPolicy", null);
					break;
				case AuthzData.SecurityScope.ModelItem:
					instrumentedSqlCommand = this.NewStandardSqlCommand("SetModelItemPolicy", null);
					instrumentedSqlCommand.AddParameter("@CatalogItemID", SqlDbType.UniqueIdentifier, policy.CatalogItemID);
					instrumentedSqlCommand.AddParameter("@ModelItemID", SqlDbType.NVarChar, policy.ModelItemID);
					break;
				default:
					throw new InternalCatalogException("Unknown security scope.");
				}
				instrumentedSqlCommand2 = instrumentedSqlCommand;
			}
			catch (Exception ex)
			{
				try
				{
					if (instrumentedSqlCommand != null && instrumentedSqlCommand != null)
					{
						((IDisposable)instrumentedSqlCommand).Dispose();
					}
				}
				catch (Exception ex2)
				{
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.TraceException(TraceLevel.Error, ex2.ToString());
					}
					throw new InternalCatalogException(ex, "Exception while disposing command object.");
				}
				throw;
			}
			return instrumentedSqlCommand2;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000482A4 File Offset: 0x000464A4
		internal virtual void GetPolicies(ExternalItemPath itemPath, out bool inheritParent, out string xmlPolicy)
		{
			string nativeCatalogPath = itemPath.NativeCatalogPath;
			xmlPolicy = null;
			inheritParent = false;
			byte[] array = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetPolicy", null))
			{
				instrumentedSqlCommand.AddParameter("@ItemName", SqlDbType.NVarChar, nativeCatalogPath);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new ItemNotFoundException(nativeCatalogPath);
					}
					if (!dataReader.IsDBNull(0))
					{
						xmlPolicy = dataReader.GetString(0);
						this.GetValidXmlPolicies(ref xmlPolicy);
					}
					else
					{
						xmlPolicy = Security.ProduceEmptyPolicy();
					}
					inheritParent = !dataReader.GetBoolean(1);
					if (!dataReader.IsDBNull(2))
					{
						array = DataReaderHelper.ReadAllBytes(dataReader, 2);
					}
					ItemType @int = (ItemType)dataReader.GetInt32(3);
					if (!this.CheckAccess(@int, array, CommonOperation.ReadAuthorizationPolicy, itemPath))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
				}
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000483A8 File Offset: 0x000465A8
		internal Policy[] GetSystemPolicies()
		{
			string text = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSystemPolicy", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new InternalCatalogException("System policy was not found in the catalog.");
					}
					if (!dataReader.IsDBNull(1))
					{
						text = dataReader.GetString(1);
						this.GetValidXmlPolicies(ref text);
					}
					else
					{
						text = Security.ProduceEmptyPolicy();
					}
				}
			}
			return Policy.XmlToPolicyArray(text);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00048458 File Offset: 0x00046658
		internal virtual void DeletePolicy(ExternalItemPath itemName)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeletePolicy", null))
				{
					instrumentedSqlCommand.AddParameter("@ItemName", SqlDbType.NVarChar, itemName.NativeCatalogPath);
					if (instrumentedSqlCommand.ExecuteNonQuery() == 0)
					{
						throw new InternalCatalogException("No policies found in internal DeletePolicy");
					}
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.IsSqlException && ex.SqlErrorNumber == Native.SqlConstraintViolationCode)
				{
					throw new InheritedPolicyException(itemName.Value);
				}
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000484E8 File Offset: 0x000466E8
		internal void DeleteModelItemPolicy(Guid catalogItemID, string modelItemID, string modelItemName)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteModelItemPolicy", null))
			{
				instrumentedSqlCommand.AddParameter("@CatalogItemID", SqlDbType.UniqueIdentifier, catalogItemID);
				instrumentedSqlCommand.AddParameter("@ModelItemID", SqlDbType.NVarChar, modelItemID);
				if (instrumentedSqlCommand.ExecuteNonQuery() == 0)
				{
					throw new InheritedPolicyException(modelItemName, modelItemID);
				}
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00048554 File Offset: 0x00046754
		internal void DeleteAllModelItemPolicies(CatalogItemPath itemPath)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteAllModelItemPolicies", null))
			{
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, itemPath.Value);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000485A8 File Offset: 0x000467A8
		internal virtual StringCollection GetCatalogItemPermissions(ItemType catItemType, byte[] secDesc, ExternalItemPath itemPath)
		{
			SecurityItemType itemType = AuthzData.GetSecType(catItemType);
			StringCollection rawPerms = null;
			if (SystemResourceManager.IsSystemResourcePath(itemPath.Value))
			{
				rawPerms = this.GetSystemResourcePermissions(catItemType);
			}
			else
			{
				object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
				lock (cachedAuthExtensionSync)
				{
					ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
					{
						rawPerms = this.CachedAuthorizationExtension.GetPermissions(this.UserName, this.UserToken, itemType, secDesc);
					});
				}
			}
			return Sku.RevokePermissionsSku(rawPerms, itemType, Globals.Configuration.InstanceID);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00048658 File Offset: 0x00046858
		private StringCollection GetSystemResourcePermissions(ItemType itemType)
		{
			StringCollection stringCollection = new StringCollection { "Read Properties" };
			if (itemType == ItemType.Resource)
			{
				stringCollection.Add("Read Content");
			}
			return stringCollection;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00048688 File Offset: 0x00046888
		internal virtual StringCollection GetModelItemPermissions(byte[] secDesc, ExternalItemPath itemPath)
		{
			StringCollection rawPerms = null;
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			lock (cachedAuthExtensionSync)
			{
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					rawPerms = this.CachedAuthorizationExtension.GetPermissions(this.UserName, this.UserToken, SecurityItemType.ModelItem, secDesc);
				});
			}
			return Sku.RevokePermissionsSku(rawPerms, SecurityItemType.ModelItem, Globals.Configuration.InstanceID);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004870C File Offset: 0x0004690C
		internal StringCollection GetSystemPermissions()
		{
			byte[] secDesc = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSystemPolicy", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new InternalCatalogException("System Policy was not found.");
					}
					if (!dataReader.IsDBNull(0))
					{
						secDesc = DataReaderHelper.ReadAllBytes(dataReader, 0);
					}
				}
			}
			StringCollection rawPerms = null;
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			lock (cachedAuthExtensionSync)
			{
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					rawPerms = this.CachedAuthorizationExtension.GetPermissions(this.UserName, this.UserToken, SecurityItemType.Catalog, secDesc);
				});
			}
			return Sku.RevokePermissionsSku(rawPerms, SecurityItemType.Catalog, Globals.Configuration.InstanceID);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00048820 File Offset: 0x00046A20
		internal bool CheckAccess(ItemType catItemType, byte[] secDesc, CommonOperation oper, ExternalItemPath itemPath)
		{
			if (!this.m_checkSecurity)
			{
				return true;
			}
			switch (catItemType)
			{
			case ItemType.Folder:
			case ItemType.Site:
				return this.CheckAccess(catItemType, secDesc, AuthzData.CommonOperationToFolderOperation(oper), itemPath);
			case ItemType.Report:
			case ItemType.LinkedReport:
			case ItemType.DataSet:
			case ItemType.RdlxReport:
			case ItemType.MobileReport:
			case ItemType.PowerBIReport:
			case ItemType.ExcelWorkbook:
				return this.CheckAccess(catItemType, secDesc, AuthzData.CommonOperationToReportOperation(oper), itemPath);
			case ItemType.Resource:
			case ItemType.Component:
			case ItemType.Kpi:
				return this.CheckAccess(catItemType, secDesc, AuthzData.CommonOperationToResourceOperation(oper), itemPath);
			case ItemType.DataSource:
				return this.CheckAccess(catItemType, secDesc, AuthzData.CommonOperationToDatasourceOperation(oper), itemPath);
			case ItemType.Model:
				return this.CheckAccess(catItemType, secDesc, AuthzData.CommonOperationToModelOperation(oper), itemPath);
			default:
				throw new InternalCatalogException("Invalid item type.");
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000488D8 File Offset: 0x00046AD8
		protected bool SkipSecurityCheck(ExternalItemPath itemPath)
		{
			return !this.m_checkSecurity || (itemPath != null && itemPath.IsEditSession);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000488F4 File Offset: 0x00046AF4
		internal virtual bool CheckAccess(byte[] secDesc, CatalogOperation catOper, ExternalItemPath itemPath)
		{
			if (this.SkipSecurityCheck(itemPath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, catOper);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00048988 File Offset: 0x00046B88
		internal virtual bool CheckAccess(byte[] secDesc, CatalogOperation[] catOpers, ExternalItemPath itemPath)
		{
			if (this.SkipSecurityCheck(itemPath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, catOpers);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00048A1C File Offset: 0x00046C1C
		internal virtual bool CheckAccess(byte[] secDesc, ExternalItemPath itemPath, ModelItemOperation modelItemOperation)
		{
			if (SystemResourceManager.IsSystemResourcePath(itemPath.Value))
			{
				return false;
			}
			if (this.SkipSecurityCheck(itemPath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, modelItemOperation);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, FolderOperation fldOper, ExternalItemPath folderPath)
		{
			if (SystemResourceManager.IsSystemResourcePath(folderPath.Value))
			{
				return catItemType == ItemType.Folder && fldOper == FolderOperation.ReadProperties;
			}
			if (this.SkipSecurityCheck(folderPath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, fldOper);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00048B74 File Offset: 0x00046D74
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, FolderOperation[] fldOpers, ExternalItemPath folderPath)
		{
			if (SystemResourceManager.IsSystemResourcePath(folderPath.Value))
			{
				if (catItemType == ItemType.Folder)
				{
					return fldOpers.All((FolderOperation op) => op == FolderOperation.ReadProperties);
				}
				return false;
			}
			else
			{
				if (this.SkipSecurityCheck(folderPath))
				{
					return true;
				}
				object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
				bool flag = false;
				bool bResult2;
				try
				{
					Monitor.Enter(cachedAuthExtensionSync, ref flag);
					bool bResult = false;
					ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
					{
						bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, fldOpers);
					});
					bResult2 = bResult;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(cachedAuthExtensionSync);
					}
				}
				return bResult2;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00048C48 File Offset: 0x00046E48
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, ReportOperation rptOper, ExternalItemPath reportPath)
		{
			if (SystemResourceManager.IsSystemResourcePath(reportPath.Value))
			{
				return false;
			}
			if (this.SkipSecurityCheck(reportPath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, rptOper);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00048CF0 File Offset: 0x00046EF0
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, ResourceOperation resOper, ExternalItemPath resourcePath)
		{
			if (SystemResourceManager.IsSystemResourcePath(resourcePath.Value))
			{
				return catItemType == ItemType.Resource && (resOper == ResourceOperation.ReadProperties || resOper == ResourceOperation.ReadContent);
			}
			if (this.SkipSecurityCheck(resourcePath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, resOper);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00048DB0 File Offset: 0x00046FB0
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, DatasourceOperation dsOper, ExternalItemPath datasourcePath)
		{
			if (SystemResourceManager.IsSystemResourcePath(datasourcePath.Value))
			{
				return false;
			}
			if (this.SkipSecurityCheck(datasourcePath))
			{
				return true;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, dsOper);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00048E58 File Offset: 0x00047058
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, ResourceOperation[] resOpers, ExternalItemPath datasourcePath)
		{
			if (SystemResourceManager.IsSystemResourcePath(datasourcePath.Value))
			{
				if (catItemType == ItemType.Resource)
				{
					return resOpers.All((ResourceOperation op) => op == ResourceOperation.ReadProperties || op == ResourceOperation.ReadContent);
				}
				return false;
			}
			else
			{
				if (this.SkipSecurityCheck(datasourcePath))
				{
					return true;
				}
				object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
				bool flag = false;
				bool bResult2;
				try
				{
					Monitor.Enter(cachedAuthExtensionSync, ref flag);
					bool bResult = false;
					ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
					{
						bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, resOpers);
					});
					bResult2 = bResult;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(cachedAuthExtensionSync);
					}
				}
				return bResult2;
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00048F2C File Offset: 0x0004712C
		internal virtual bool CheckAccess(ItemType catItemType, byte[] secDesc, ModelOperation modelOperation, ExternalItemPath path)
		{
			if (SystemResourceManager.IsSystemResourcePath(path.Value))
			{
				return false;
			}
			if (this.SkipSecurityCheck(path))
			{
				return true;
			}
			if (!Sku.CheckAccessOverideSku(Globals.Configuration.InstanceID, modelOperation))
			{
				return false;
			}
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			bool bResult2;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				bool bResult = false;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					bResult = this.CachedAuthorizationExtension.CheckAccess(this.UserName, this.UserToken, secDesc, modelOperation);
				});
				bResult2 = bResult;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
			return bResult2;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00048FEC File Offset: 0x000471EC
		public ServiceToken GetServiceTokenFromCatalog()
		{
			string text = null;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetUserServiceToken", null))
				{
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.m_userContext.UserName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.m_userContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					object obj = instrumentedSqlCommand.ExecuteScalar();
					base.Commit();
					if (obj is DBNull)
					{
						return null;
					}
					text = (string)obj;
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				base.Disconnect();
			}
			return ServiceToken.FromJson(CatalogEncryption.Instance.DecryptToString(text, null));
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000490E8 File Offset: 0x000472E8
		public UserProperties GetUserSettings()
		{
			UserProperties userProperties;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetUserSettings", null))
				{
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.m_userContext.UserName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.m_userContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					object obj = instrumentedSqlCommand.ExecuteScalar();
					base.Commit();
					if (obj is DBNull)
					{
						userProperties = null;
					}
					else
					{
						userProperties = new UserProperties((string)obj);
					}
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				base.Disconnect();
			}
			return userProperties;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000491D0 File Offset: 0x000473D0
		public string GetLatestAADToken()
		{
			string text;
			try
			{
				ServiceToken serviceToken = new AadOAuthProvider(new AADCatalogCache(this), Globals.Configuration.PowerBIConfiguration).AcquireToken();
				if (serviceToken == null || serviceToken.AccessToken == null)
				{
					text = null;
				}
				else
				{
					text = serviceToken.ToJson();
				}
			}
			catch (AccessDeniedException)
			{
				RSTrace.SecurityTracer.Trace("Renewal of token for User {0} failed", new object[] { this.UserName });
				text = null;
			}
			return text;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00049244 File Offset: 0x00047444
		public void SetUserSettings(UserProperties properties)
		{
			string text = ((properties != null) ? properties.PrepareForSaving() : null);
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetUserSettings", null))
				{
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.m_userContext.UserName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.m_userContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					instrumentedSqlCommand.AddParameter("@Setting", SqlDbType.NText, text);
					instrumentedSqlCommand.ExecuteNonQuery();
					base.Commit();
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				base.Disconnect();
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00049334 File Offset: 0x00047534
		public void SetServiceToken(ServiceToken serviceToken)
		{
			if (!SymmetricKeyEncryption.IsInitialized)
			{
				this.ConnectionManager.VerifyConnection(true);
			}
			string text = ((serviceToken != null) ? CatalogEncryption.Instance.EncryptToString(serviceToken.ToJson(), null) : null);
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetUserServiceToken", null))
				{
					instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, this.m_userContext.UserName);
					instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(this.m_userContext));
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					instrumentedSqlCommand.AddParameter("@ServiceToken", SqlDbType.NText, text ?? DBNull.Value);
					instrumentedSqlCommand.ExecuteNonQuery();
					base.Commit();
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				base.Disconnect();
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00049448 File Offset: 0x00047648
		internal void GetSystemSecurityDescriptor(ref byte[] secDesc)
		{
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSystemPolicy", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)this.m_userContext.AuthenticationType);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							throw new InternalCatalogException("Could not find security descriptor.");
						}
						if (!dataReader.IsDBNull(0))
						{
							secDesc = DataReaderHelper.ReadAllBytes(dataReader, 0);
						}
					}
					base.Commit();
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				base.Disconnect();
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00049524 File Offset: 0x00047724
		internal bool GetEnableLoadReportDefinitionFlag()
		{
			bool flag;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetOneConfigurationInfo", null))
				{
					instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, "EnableLoadReportDefinition");
					flag = bool.Parse((string)instrumentedSqlCommand.ExecuteScalar());
				}
			}
			catch (Exception ex)
			{
				base.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return flag;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000495A4 File Offset: 0x000477A4
		internal byte[] SystemSecDesc
		{
			get
			{
				if (this.m_SysSecDesc == null)
				{
					this.GetSystemSecurityDescriptor(ref this.m_SysSecDesc);
				}
				return this.m_SysSecDesc;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000495C0 File Offset: 0x000477C0
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x000495C8 File Offset: 0x000477C8
		internal bool SecurityCheckFlag
		{
			get
			{
				return this.m_checkSecurity;
			}
			set
			{
				this.m_checkSecurity = value;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000495D1 File Offset: 0x000477D1
		internal static bool IsSameUser(UserContext uc1, string user2)
		{
			return Security.IsSameUserByName(uc1.UserName, user2, uc1.AuthenticationType);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000495E5 File Offset: 0x000477E5
		internal static bool IsSameUser(UserContext uc1, UserContext uc2)
		{
			return uc1.AuthenticationType == uc2.AuthenticationType && Security.IsSameUserByName(uc1.UserName, uc2.UserName, uc1.AuthenticationType);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00049610 File Offset: 0x00047810
		private static bool IsSameUserByName(string user1, string user2, AuthenticationType authType)
		{
			if (AuthenticationType.Windows == authType)
			{
				SafeLocalFree safeLocalFree = null;
				SafeLocalFree safeLocalFree2 = null;
				try
				{
					int num;
					safeLocalFree = Native.GetSid(user1, out num);
					if (safeLocalFree == null)
					{
						return false;
					}
					int num2;
					safeLocalFree2 = Native.GetSid(user2, out num2);
					if (safeLocalFree2 == null)
					{
						return false;
					}
					if (Native.EqualSid(safeLocalFree.DangerousGetHandle(), safeLocalFree2.DangerousGetHandle()) != 0)
					{
						return true;
					}
					return false;
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
					if (safeLocalFree2 != null)
					{
						safeLocalFree2.Close();
					}
				}
			}
			return Localization.CatalogCultureCompare(user1, user2) == 0;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00049694 File Offset: 0x00047894
		protected IAuthorizationExtension CachedAuthorizationExtension
		{
			get
			{
				if (this.m_cachedAuthorizationExtension == null)
				{
					this.m_cachedAuthorizationExtension = Security.AuthorizationExtension;
				}
				return this.m_cachedAuthorizationExtension;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000496AF File Offset: 0x000478AF
		protected object CachedAuthExtensionSync
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_cachedAuthSync;
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000496B8 File Offset: 0x000478B8
		private Hashtable CompileXmlPolicy(AuthzData.SecurityPolicy policy, out byte[] realSecDesc, out string stringSecDesc)
		{
			string xmlPolicy = policy.XmlPolicy;
			Hashtable hashtable;
			AceCollection aceCollection = this.XmlPolicyToAclStruct(ref xmlPolicy, policy.Scope, policy.RelatedRoles, out hashtable);
			policy.XmlPolicy = xmlPolicy;
			this.AclToSecDesc(aceCollection, policy.CatalogItemPath, policy.SecItemType, out realSecDesc, out stringSecDesc);
			return hashtable;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00049700 File Offset: 0x00047900
		protected virtual void AclToSecDesc(AceCollection acl, ExternalItemPath itemPath, SecurityItemType itemType, out byte[] realSecDesc, out string stringSecDesc)
		{
			object cachedAuthExtensionSync = this.CachedAuthExtensionSync;
			bool flag = false;
			try
			{
				Monitor.Enter(cachedAuthExtensionSync, ref flag);
				byte[] realSecDescLocal = null;
				string stringSecDescLocal = null;
				ExtensionBoundary.AuthorizationExtensionBoundary.Invoke(delegate
				{
					realSecDescLocal = this.CachedAuthorizationExtension.CreateSecurityDescriptor(acl, itemType, out stringSecDescLocal);
				});
				realSecDesc = realSecDescLocal;
				stringSecDesc = stringSecDescLocal;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cachedAuthExtensionSync);
				}
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00049798 File Offset: 0x00047998
		private AceCollection XmlPolicyToAclStruct(ref string xmlPolicy, AuthzData.SecurityScope scope, Hashtable relatedRoles, out Hashtable principalsAndRoles)
		{
			principalsAndRoles = null;
			AceCollection aceCollection2;
			try
			{
				AceCollection aceCollection = new AceCollection();
				XmlDocument xmlDocument = new XmlDocument();
				XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xmlPolicy);
				XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("/Policies/Policy");
				List<Sid> list = new List<Sid>();
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("GroupUserName");
					if (xmlNode2 == null)
					{
						throw new MissingElementException("GroupUserName");
					}
					string innerText = xmlNode2.InnerText;
					Sid sid = null;
					if (!AuthenticationExtensionFactory.GetAuthenticationExtension(this.UserContext.AuthenticationType).IsValidPrincipalName(innerText))
					{
						throw new UnknownUserNameException(innerText);
					}
					byte[] principalSid = this.GetPrincipalSid(innerText);
					if (principalSid != null)
					{
						sid = new Sid(principalSid);
					}
					AceStruct aceStruct = new AceStruct(innerText);
					if (principalsAndRoles == null)
					{
						principalsAndRoles = new Hashtable();
					}
					AuthzData.PrincipalAndRoles principalAndRoles = new AuthzData.PrincipalAndRoles(innerText);
					foreach (object obj2 in xmlNode.SelectNodes("Roles/Role/Name"))
					{
						string innerText2 = ((XmlNode)obj2).InnerText;
						if (principalAndRoles.ContainsRole(innerText2))
						{
							throw new InvalidPolicyDefinitionException(innerText);
						}
						this.RoleToOperations(innerText2, scope, relatedRoles, ref aceStruct);
						if (principalAndRoles.m_Roles == null)
						{
							principalAndRoles.m_Roles = new ArrayList();
						}
						principalAndRoles.m_Roles.Add(innerText2);
					}
					if (principalAndRoles.m_Roles == null || principalAndRoles.m_Roles.Count <= 0)
					{
						throw new InvalidPolicyDefinitionException(innerText);
					}
					if (principalsAndRoles.Contains(innerText))
					{
						throw new InvalidPolicyDefinitionException(innerText);
					}
					if (sid != null)
					{
						if (list.Contains(sid))
						{
							throw new InvalidPolicyDefinitionException(innerText);
						}
						list.Add(sid);
					}
					principalsAndRoles.Add(innerText, principalAndRoles);
					aceCollection.Add(aceStruct);
				}
				if (this.m_userContext.AuthenticationType == AuthenticationType.Windows)
				{
					this.UpdateSidByUserNameInXmlPolicy(xmlDocument, ref xmlPolicy);
				}
				aceCollection2 = aceCollection;
			}
			catch (XmlException ex)
			{
				ex.ToString();
				throw new MalformedXmlException(ex);
			}
			return aceCollection2;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000499EC File Offset: 0x00047BEC
		private void RoleToOperations(string roleName, AuthzData.SecurityScope policyScope, Hashtable relatedRoles, ref AceStruct ace)
		{
			AuthzData.SecurityRole securityRole = null;
			if (relatedRoles != null)
			{
				foreach (object obj in relatedRoles.Values)
				{
					AuthzData.SecurityRole securityRole2 = (AuthzData.SecurityRole)obj;
					if (securityRole2.RoleName == roleName)
					{
						securityRole = securityRole2;
						break;
					}
				}
			}
			if (securityRole == null)
			{
				string text;
				AuthzData.SecurityScope @byte;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ReadRoleProperties", null))
				{
					instrumentedSqlCommand.AddParameter("@RoleName", SqlDbType.NVarChar, roleName);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							throw new RoleNotFoundException(roleName);
						}
						object value = dataReader.GetValue(1);
						if (value == null)
						{
							throw new RoleNotFoundException(roleName);
						}
						text = (string)value;
						@byte = (AuthzData.SecurityScope)dataReader.GetByte(2);
						if (@byte != policyScope)
						{
							throw new MixedTasksException();
						}
					}
				}
				securityRole = new AuthzData.SecurityRole(roleName, text, @byte);
			}
			this.InnerRoleToOperations(securityRole, ref ace);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00049B08 File Offset: 0x00047D08
		private void InnerRoleToOperations(AuthzData.SecurityRole role, ref AceStruct ace)
		{
			string taskMask = role.TaskMask;
			switch (role.Scope)
			{
			case AuthzData.SecurityScope.CatalogItem:
			{
				if (taskMask.Length != AuthzData.m_CatalogItemTaskMap.Count)
				{
					throw new InternalCatalogException("Wrong number of tasks found for role" + role.RoleName);
				}
				for (int i = 0; i < taskMask.Length; i++)
				{
					if (taskMask[i] == '1')
					{
						AuthzData.CatalogItemTaskEnum catalogItemTaskEnum = (AuthzData.CatalogItemTaskEnum)i;
						AuthzData.TaskOperationMap taskOperationMap = (AuthzData.TaskOperationMap)AuthzData.m_CatalogItemTaskMap[catalogItemTaskEnum];
						if (taskOperationMap.m_DSOper != null)
						{
							for (int j = 0; j < taskOperationMap.m_DSOper.Length; j++)
							{
								ace.DatasourceOperations.Add(taskOperationMap.m_DSOper[j]);
							}
						}
						if (taskOperationMap.m_ResOper != null)
						{
							for (int k = 0; k < taskOperationMap.m_ResOper.Length; k++)
							{
								ace.ResourceOperations.Add(taskOperationMap.m_ResOper[k]);
							}
						}
						if (taskOperationMap.m_RptOper != null)
						{
							for (int l = 0; l < taskOperationMap.m_RptOper.Length; l++)
							{
								ace.ReportOperations.Add(taskOperationMap.m_RptOper[l]);
							}
						}
						if (taskOperationMap.m_FldOper != null)
						{
							for (int m = 0; m < taskOperationMap.m_FldOper.Length; m++)
							{
								ace.FolderOperations.Add(taskOperationMap.m_FldOper[m]);
							}
						}
						if (taskOperationMap.m_ModelOper != null)
						{
							for (int n = 0; n < taskOperationMap.m_ModelOper.Length; n++)
							{
								ace.ModelOperations.Add(taskOperationMap.m_ModelOper[n]);
							}
						}
					}
				}
				return;
			}
			case AuthzData.SecurityScope.Catalog:
			{
				if (taskMask.Length != AuthzData.m_CatalogTaskMap.Count)
				{
					throw new InternalCatalogException("Wrong number of tasks found for role" + role.RoleName);
				}
				for (int num = 0; num < taskMask.Length; num++)
				{
					if (taskMask[num] == '1')
					{
						AuthzData.CatalogTaskEnum catalogTaskEnum = (AuthzData.CatalogTaskEnum)num;
						AuthzData.TaskOperationMap taskOperationMap2 = (AuthzData.TaskOperationMap)AuthzData.m_CatalogTaskMap[catalogTaskEnum];
						if (taskOperationMap2.m_CatOper != null)
						{
							for (int num2 = 0; num2 < taskOperationMap2.m_CatOper.Length; num2++)
							{
								ace.CatalogOperations.Add(taskOperationMap2.m_CatOper[num2]);
							}
						}
					}
				}
				return;
			}
			case AuthzData.SecurityScope.ModelItem:
			{
				if (taskMask.Length != AuthzData.m_ModelItemTaskMap.Count)
				{
					throw new InternalCatalogException("Wrong number of tasks found for role" + role.RoleName);
				}
				for (int num3 = 0; num3 < taskMask.Length; num3++)
				{
					if (taskMask[num3] == '1')
					{
						AuthzData.ModelItemTaskEnum modelItemTaskEnum = (AuthzData.ModelItemTaskEnum)num3;
						AuthzData.TaskOperationMap taskOperationMap3 = (AuthzData.TaskOperationMap)AuthzData.m_ModelItemTaskMap[modelItemTaskEnum];
						if (taskOperationMap3.m_ModelItemOper != null)
						{
							for (int num4 = 0; num4 < taskOperationMap3.m_ModelItemOper.Length; num4++)
							{
								ace.ModelItemOperations.Add(taskOperationMap3.m_ModelItemOper[num4]);
							}
						}
					}
				}
				return;
			}
			default:
				throw new InternalCatalogException("Unknown security scope");
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00049DFF File Offset: 0x00047FFF
		internal void RemoveBadUserNames(ref string xmlPolicy)
		{
			this.UpdateUserNameBySidInXmlPolicy(ref xmlPolicy, true);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00049E09 File Offset: 0x00048009
		internal void UpdateUserNames(ref string xmlPolicy)
		{
			this.UpdateUserNameBySidInXmlPolicy(ref xmlPolicy, false);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00049E14 File Offset: 0x00048014
		internal static void SafeCheckExecuteReportDefinitionPermission(RSService service, ExternalItemPath sitePath, bool skipAccessCheck)
		{
			if (skipAccessCheck)
			{
				if (!service.SecMgr.GetEnableLoadReportDefinitionFlag())
				{
					throw new AccessDeniedException(service.UserName, ErrorCode.rsAccessDenied);
				}
			}
			else if (!service.SecMgr.CheckAccess(service.SecMgr.SystemSecDesc, CatalogOperation.ExecuteReportDefinition, sitePath))
			{
				throw new AccessDeniedException(service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00049E68 File Offset: 0x00048068
		private void UpdateUserNameBySidInXmlPolicy(ref string xmlPolicy, bool removeBadUserNamesOnly)
		{
			bool flag = false;
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xmlPolicy);
			XmlElement documentElement = xmlDocument.DocumentElement;
			foreach (object obj in documentElement.SelectNodes("/Policies/Policy"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("GroupUserId");
				if (removeBadUserNamesOnly || (xmlNode2 != null && xmlNode2.InnerText != null))
				{
					XmlNode xmlNode3 = xmlNode.SelectSingleNode("GroupUserName");
					if (xmlNode3 == null)
					{
						throw new MissingElementException("GroupUserName");
					}
					if (removeBadUserNamesOnly)
					{
						if (!AuthenticationExtensionFactory.GetAuthenticationExtension(this.UserContext.AuthenticationType).IsValidPrincipalName(xmlNode3.InnerText))
						{
							documentElement.RemoveChild(xmlNode);
							flag = true;
						}
					}
					else
					{
						byte[] array = Convert.FromBase64String(xmlNode2.InnerText);
						string text = this.PrincipalNameFromSid(array);
						if (text == null)
						{
							documentElement.RemoveChild(xmlNode);
							flag = true;
						}
						else if (text != xmlNode3.InnerText)
						{
							xmlNode3.InnerText = text;
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				xmlPolicy = xmlDocument.InnerXml;
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00049FA0 File Offset: 0x000481A0
		private void UpdateSidByUserNameInXmlPolicy(XmlDocument doc, ref string xmlPolicy)
		{
			foreach (object obj in doc.DocumentElement.SelectNodes("/Policies/Policy"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("GroupUserName");
				string innerText = xmlNode2.InnerText;
				byte[] principalSid = this.GetPrincipalSid(innerText);
				Security.m_Tracer.Assert(principalSid != null, "Null SID was found");
				string text = Convert.ToBase64String(principalSid);
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("GroupUserId");
				if (xmlNode3 == null)
				{
					xmlNode3 = doc.CreateElement("GroupUserId");
					xmlNode.InsertAfter(xmlNode3, xmlNode2);
				}
				xmlNode3.InnerText = text;
			}
			xmlPolicy = doc.InnerXml;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004A074 File Offset: 0x00048274
		private void GetValidXmlPolicies(ref string xmlPolicy)
		{
			if (this.m_userContext.AuthenticationType == AuthenticationType.Windows)
			{
				this.UpdateUserNames(ref xmlPolicy);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0004A08B File Offset: 0x0004828B
		protected string UserName
		{
			get
			{
				if (this.UserContext != null)
				{
					Security.m_Tracer.Assert(this.UserContext.IsInitialized, "User Context is not initialized");
					return this.UserContext.UserName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004A0C0 File Offset: 0x000482C0
		private IntPtr UserToken
		{
			get
			{
				if (this.UserContext == null || this.UserContext.UserToken == null)
				{
					return IntPtr.Zero;
				}
				Security.m_Tracer.Assert(this.UserContext.IsInitialized, "User Context is not initialized");
				return (IntPtr)this.UserContext.UserToken;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0004A112 File Offset: 0x00048312
		internal UserContext UserContext
		{
			get
			{
				return this.m_userContext;
			}
		}

		// Token: 0x040006EA RID: 1770
		private static RSTrace m_Tracer = RSTrace.SecurityTracer;

		// Token: 0x040006EB RID: 1771
		private IAuthorizationExtension m_cachedAuthorizationExtension;

		// Token: 0x040006EC RID: 1772
		private readonly Security.AuthorizationExtensionSync m_cachedAuthSync = new Security.AuthorizationExtensionSync();

		// Token: 0x040006ED RID: 1773
		private bool m_checkSecurity = true;

		// Token: 0x040006EE RID: 1774
		protected readonly UserContext m_userContext;

		// Token: 0x040006EF RID: 1775
		private byte[] m_SysSecDesc;

		// Token: 0x040006F0 RID: 1776
		private const string m_systemAdministratorRoleName = "System Administrator";

		// Token: 0x040006F1 RID: 1777
		private const string m_getPoliciesForRoleQuery = "\r\nSELECT  \r\n    PrimeRoles.PolicyID,\r\n    SecData.XmlDescription, \r\n    PrimeRoles.PolicyFlag,\r\n    Catalog.Type,\r\n    Catalog.Path,\r\n    ModelItemPolicy.CatalogItemID,\r\n    ModelItemPolicy.ModelItemID,\r\n    RelatedRoles.RoleID,\r\n    RelatedRoles.RoleName,\r\n    RelatedRoles.TaskMask,\r\n    RelatedRoles.RoleFlags\r\nFROM\r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies ON \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) PrimeRoles\r\nINNER JOIN \r\n\t(SELECT * FROM Roles \r\n\tINNER JOIN Policies on \r\n\t\tEXISTS (\r\n\t\t\tSELECT * FROM PolicyUserRole \r\n\t\t\tWHERE PolicyUserRole.RoleID = Roles.RoleID\r\n\t\t\tAND PolicyUserRole.PolicyID = Policies.PolicyID)) RelatedRoles \r\n\tON PrimeRoles.PolicyID = RelatedRoles.PolicyID\r\nLEFT OUTER JOIN SecData ON PrimeRoles.PolicyID = SecData.PolicyID AND SecData.AuthType = @AuthType\r\nLEFT OUTER JOIN Catalog ON PrimeRoles.PolicyID = Catalog.PolicyID AND Catalog.PolicyRoot = 1\r\nLEFT OUTER JOIN ModelItemPolicy ON PrimeRoles.PolicyID = ModelItemPolicy.PolicyID\r\nWHERE PrimeRoles.RoleName = @RoleName\r\nORDER BY PrimeRoles.PolicyID\r\n";

		// Token: 0x040006F2 RID: 1778
		internal const string RSSharePoinAuthorizationExtensionClassName = "Microsoft.ReportingServices.SharePoint.Server.SharePointAuthorizationExtension";

		// Token: 0x02000481 RID: 1153
		private sealed class AuthorizationExtensionSync
		{
		}
	}
}
