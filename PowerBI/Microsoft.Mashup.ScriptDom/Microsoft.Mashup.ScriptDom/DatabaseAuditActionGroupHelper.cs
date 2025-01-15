using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000B3 RID: 179
	internal class DatabaseAuditActionGroupHelper : OptionsHelper<AuditActionGroup>
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		private DatabaseAuditActionGroupHelper()
		{
			base.AddOptionMapping(AuditActionGroup.DatabasePermissionChange, "DATABASE_PERMISSION_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.SchemaObjectPermissionChange, "SCHEMA_OBJECT_PERMISSION_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseRoleMemberChange, "DATABASE_ROLE_MEMBER_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.ApplicationRoleChangePassword, "APPLICATION_ROLE_CHANGE_PASSWORD_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.SchemaObjectAccess, "SCHEMA_OBJECT_ACCESS_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.BackupRestore, "BACKUP_RESTORE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.Dbcc, "DBCC_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.AuditChange, "AUDIT_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseChange, "DATABASE_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectChange, "DATABASE_OBJECT_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabasePrincipalChange, "DATABASE_PRINCIPAL_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.SchemaObjectChange, "SCHEMA_OBJECT_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabasePrincipalImpersonation, "DATABASE_PRINCIPAL_IMPERSONATION_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectOwnershipChange, "DATABASE_OBJECT_OWNERSHIP_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseOwnershipChange, "DATABASE_OWNERSHIP_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.SchemaObjectOwnershipChange, "SCHEMA_OBJECT_OWNERSHIP_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectPermissionChange, "DATABASE_OBJECT_PERMISSION_CHANGE_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseOperation, "DATABASE_OPERATION_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.DatabaseObjectAccess, "DATABASE_OBJECT_ACCESS_GROUP", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(AuditActionGroup.SuccessfulDatabaseAuthenticationGroup, "SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.FailedDatabaseAuthenticationGroup, "FAILED_DATABASE_AUTHENTICATION_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.DatabaseLogoutGroup, "DATABASE_LOGOUT_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.UserChangePasswordGroup, "USER_CHANGE_PASSWORD_GROUP", SqlVersionFlags.TSql110);
			base.AddOptionMapping(AuditActionGroup.UserDefinedAuditGroup, "USER_DEFINED_AUDIT_GROUP", SqlVersionFlags.TSql110);
		}

		// Token: 0x04000419 RID: 1049
		internal static readonly DatabaseAuditActionGroupHelper Instance = new DatabaseAuditActionGroupHelper();
	}
}
