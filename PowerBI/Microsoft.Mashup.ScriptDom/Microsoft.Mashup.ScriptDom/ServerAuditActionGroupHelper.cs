using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000B1 RID: 177
	internal class ServerAuditActionGroupHelper : OptionsHelper<AuditActionGroup>
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000BC8C File Offset: 0x00009E8C
		private ServerAuditActionGroupHelper()
		{
			base.AddOptionMapping(AuditActionGroup.SuccessfulLogin, "SUCCESSFUL_LOGIN_GROUP");
			base.AddOptionMapping(AuditActionGroup.Logout, "LOGOUT_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerStateChange, "SERVER_STATE_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.FailedLogin, "FAILED_LOGIN_GROUP");
			base.AddOptionMapping(AuditActionGroup.LoginChangePassword, "LOGIN_CHANGE_PASSWORD_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerRoleMemberChange, "SERVER_ROLE_MEMBER_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerPrincipalImpersonation, "SERVER_PRINCIPAL_IMPERSONATION_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerObjectOwnershipChange, "SERVER_OBJECT_OWNERSHIP_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseMirroringLogin, "DATABASE_MIRRORING_LOGIN_GROUP");
			base.AddOptionMapping(AuditActionGroup.BrokerLogin, "BROKER_LOGIN_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerPermissionChange, "SERVER_PERMISSION_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerObjectPermissionChange, "SERVER_OBJECT_PERMISSION_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerOperation, "SERVER_OPERATION_GROUP");
			base.AddOptionMapping(AuditActionGroup.TraceChange, "TRACE_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerObjectChange, "SERVER_OBJECT_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ServerPrincipalChange, "SERVER_PRINCIPAL_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabasePermissionChange, "DATABASE_PERMISSION_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.SchemaObjectPermissionChange, "SCHEMA_OBJECT_PERMISSION_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseRoleMemberChange, "DATABASE_ROLE_MEMBER_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.ApplicationRoleChangePassword, "APPLICATION_ROLE_CHANGE_PASSWORD_GROUP");
			base.AddOptionMapping(AuditActionGroup.SchemaObjectAccess, "SCHEMA_OBJECT_ACCESS_GROUP");
			base.AddOptionMapping(AuditActionGroup.BackupRestore, "BACKUP_RESTORE_GROUP");
			base.AddOptionMapping(AuditActionGroup.Dbcc, "DBCC_GROUP");
			base.AddOptionMapping(AuditActionGroup.AuditChange, "AUDIT_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseChange, "DATABASE_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectChange, "DATABASE_OBJECT_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabasePrincipalChange, "DATABASE_PRINCIPAL_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.SchemaObjectChange, "SCHEMA_OBJECT_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabasePrincipalImpersonation, "DATABASE_PRINCIPAL_IMPERSONATION_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectOwnershipChange, "DATABASE_OBJECT_OWNERSHIP_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseOwnershipChange, "DATABASE_OWNERSHIP_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.SchemaObjectOwnershipChange, "SCHEMA_OBJECT_OWNERSHIP_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectPermissionChange, "DATABASE_OBJECT_PERMISSION_CHANGE_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseOperation, "DATABASE_OPERATION_GROUP");
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectAccess, "DATABASE_OBJECT_ACCESS_GROUP");
			base.AddOptionMapping(AuditActionGroup.SuccessfulDatabaseAuthenticationGroup, "SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.FailedDatabaseAuthenticationGroup, "FAILED_DATABASE_AUTHENTICATION_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.DatabaseLogoutGroup, "DATABASE_LOGOUT_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.UserChangePasswordGroup, "USER_CHANGE_PASSWORD_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.UserDefinedAuditGroup, "USER_DEFINED_AUDIT_GROUP", SqlVersionFlags.TSql110);
		}

		// Token: 0x04000410 RID: 1040
		internal static readonly ServerAuditActionGroupHelper Instance = new ServerAuditActionGroupHelper();
	}
}
