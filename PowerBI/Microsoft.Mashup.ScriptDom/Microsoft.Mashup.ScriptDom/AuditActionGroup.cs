using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200003A RID: 58
	internal enum AuditActionGroup
	{
		// Token: 0x040000D6 RID: 214
		None,
		// Token: 0x040000D7 RID: 215
		SuccessfulLogin,
		// Token: 0x040000D8 RID: 216
		Logout,
		// Token: 0x040000D9 RID: 217
		ServerStateChange,
		// Token: 0x040000DA RID: 218
		FailedLogin,
		// Token: 0x040000DB RID: 219
		LoginChangePassword,
		// Token: 0x040000DC RID: 220
		ServerRoleMemberChange,
		// Token: 0x040000DD RID: 221
		ServerPrincipalImpersonation,
		// Token: 0x040000DE RID: 222
		ServerObjectOwnershipChange,
		// Token: 0x040000DF RID: 223
		DatabaseMirroringLogin,
		// Token: 0x040000E0 RID: 224
		BrokerLogin,
		// Token: 0x040000E1 RID: 225
		ServerPermissionChange,
		// Token: 0x040000E2 RID: 226
		ServerObjectPermissionChange,
		// Token: 0x040000E3 RID: 227
		ServerOperation,
		// Token: 0x040000E4 RID: 228
		TraceChange,
		// Token: 0x040000E5 RID: 229
		ServerObjectChange,
		// Token: 0x040000E6 RID: 230
		ServerPrincipalChange,
		// Token: 0x040000E7 RID: 231
		DatabasePermissionChange,
		// Token: 0x040000E8 RID: 232
		SchemaObjectPermissionChange,
		// Token: 0x040000E9 RID: 233
		DatabaseRoleMemberChange,
		// Token: 0x040000EA RID: 234
		ApplicationRoleChangePassword,
		// Token: 0x040000EB RID: 235
		SchemaObjectAccess,
		// Token: 0x040000EC RID: 236
		BackupRestore,
		// Token: 0x040000ED RID: 237
		Dbcc,
		// Token: 0x040000EE RID: 238
		AuditChange,
		// Token: 0x040000EF RID: 239
		DatabaseChange,
		// Token: 0x040000F0 RID: 240
		DatabaseObjectChange,
		// Token: 0x040000F1 RID: 241
		DatabasePrincipalChange,
		// Token: 0x040000F2 RID: 242
		SchemaObjectChange,
		// Token: 0x040000F3 RID: 243
		DatabasePrincipalImpersonation,
		// Token: 0x040000F4 RID: 244
		DatabaseObjectOwnershipChange,
		// Token: 0x040000F5 RID: 245
		DatabaseOwnershipChange,
		// Token: 0x040000F6 RID: 246
		SchemaObjectOwnershipChange,
		// Token: 0x040000F7 RID: 247
		DatabaseObjectPermissionChange,
		// Token: 0x040000F8 RID: 248
		DatabaseOperation,
		// Token: 0x040000F9 RID: 249
		DatabaseObjectAccess,
		// Token: 0x040000FA RID: 250
		SuccessfulDatabaseAuthenticationGroup,
		// Token: 0x040000FB RID: 251
		FailedDatabaseAuthenticationGroup,
		// Token: 0x040000FC RID: 252
		DatabaseLogoutGroup,
		// Token: 0x040000FD RID: 253
		UserChangePasswordGroup,
		// Token: 0x040000FE RID: 254
		UserDefinedAuditGroup
	}
}
