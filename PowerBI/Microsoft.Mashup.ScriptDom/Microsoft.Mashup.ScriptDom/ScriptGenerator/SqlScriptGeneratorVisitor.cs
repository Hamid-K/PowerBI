using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x020000D8 RID: 216
	internal abstract class SqlScriptGeneratorVisitor : TSqlConcreteFragmentVisitor
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x0007B889 File Offset: 0x00079A89
		public override void ExplicitVisit(MaxSizeDatabaseOption node)
		{
			this.GenerateNameEqualsValue("MAXSIZE", node.MaxSize);
			this.GenerateSpaceAndMemoryUnit(node.Units);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0007B8A8 File Offset: 0x00079AA8
		public override void ExplicitVisit(DiskStatement node)
		{
			this.GenerateIdentifier("DISK");
			switch (node.DiskStatementType)
			{
			case DiskStatementType.Init:
				this.GenerateSpaceAndIdentifier("INIT");
				break;
			case DiskStatementType.Resize:
				this.GenerateSpaceAndIdentifier("RESIZE");
				break;
			}
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<DiskStatementOption>(node.Options);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0007B901 File Offset: 0x00079B01
		public override void ExplicitVisit(DiskStatementOption node)
		{
			DiskStatementOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpace();
			this.GenerateSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0007B938 File Offset: 0x00079B38
		public override void ExplicitVisit(FileStreamDatabaseOption node)
		{
			this.GenerateIdentifier("FILESTREAM");
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			if (node.NonTransactedAccess != null)
			{
				string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<NonTransactedFileStreamAccess, string>(SqlScriptGeneratorVisitor._nonTransactedFileStreamAccessNames, node.NonTransactedAccess.Value);
				this.GenerateNameEqualsValue("NON_TRANSACTED_ACCESS", valueForEnumKey);
			}
			if (node.NonTransactedAccess != null && node.DirectoryName != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpace();
			}
			if (node.DirectoryName != null)
			{
				this.GenerateNameEqualsValue("DIRECTORY_NAME", node.DirectoryName);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0007B9DD File Offset: 0x00079BDD
		public override void ExplicitVisit(AlterTableFileTableNamespaceStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndIdentifier(node.IsEnable ? "ENABLE" : "DISABLE");
			this.GenerateSpaceAndIdentifier("FILETABLE_NAMESPACE");
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0007BA0B File Offset: 0x00079C0B
		public override void ExplicitVisit(FileTableCollateFileNameTableOption node)
		{
			this.GenerateNameEqualsValue("FILETABLE_COLLATE_FILENAME", node.Value);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0007BA20 File Offset: 0x00079C20
		public override void ExplicitVisit(FileTableConstraintNameTableOption node)
		{
			switch (node.OptionKind)
			{
			case TableOptionKind.FileTablePrimaryKeyConstraintName:
				this.GenerateNameEqualsValue("FILETABLE_PRIMARY_KEY_CONSTRAINT_NAME", node.Value);
				return;
			case TableOptionKind.FileTableStreamIdUniqueConstraintName:
				this.GenerateNameEqualsValue("FILETABLE_STREAMID_UNIQUE_CONSTRAINT_NAME", node.Value);
				return;
			case TableOptionKind.FileTableFullPathUniqueConstraintName:
				this.GenerateNameEqualsValue("FILETABLE_FULLPATH_UNIQUE_CONSTRAINT_NAME", node.Value);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0007BA7E File Offset: 0x00079C7E
		public override void ExplicitVisit(FileTableDirectoryTableOption node)
		{
			this.GenerateNameEqualsValue("FILETABLE_DIRECTORY", node.Value);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0007BA94 File Offset: 0x00079C94
		public override void ExplicitVisit(CreateColumnStoreIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			if (node.Clustered != null)
			{
				this.GenerateSpaceAndKeyword(node.Clustered.Value ? TSqlTokenType.Clustered : TSqlTokenType.NonClustered);
			}
			this.GenerateSpaceAndIdentifier("COLUMNSTORE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			if (node.Columns != null && node.Columns.Count > 0)
			{
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
			}
			this.GenerateIndexOptions(node.IndexOptions);
			if (node.OnFileGroupOrPartitionScheme != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroupOrPartitionScheme);
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0007BB60 File Offset: 0x00079D60
		public override void ExplicitVisit(HadrDatabaseOption node)
		{
			this.GenerateIdentifier("HADR");
			switch (node.HadrOption)
			{
			case HadrDatabaseOptionKind.Suspend:
				this.GenerateSpaceAndIdentifier("SUSPEND");
				return;
			case HadrDatabaseOptionKind.Resume:
				this.GenerateSpaceAndIdentifier("RESUME");
				return;
			case HadrDatabaseOptionKind.Off:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Off);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0007BBB2 File Offset: 0x00079DB2
		public override void ExplicitVisit(HadrAvailabilityGroupDatabaseOption node)
		{
			this.GenerateIdentifier("HADR");
			this.GenerateSpaceAndIdentifier("AVAILABILITY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.GroupName);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0007BBEC File Offset: 0x00079DEC
		public override void ExplicitVisit(AlterAvailabilityGroupStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateIdentifier("AVAILABILITY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			switch (node.AlterAvailabilityGroupStatementType)
			{
			case AlterAvailabilityGroupStatementType.AddDatabase:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Add);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<Identifier>(node.Databases);
				return;
			case AlterAvailabilityGroupStatementType.RemoveDatabase:
				this.GenerateSpaceAndIdentifier("REMOVE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<Identifier>(node.Databases);
				return;
			case AlterAvailabilityGroupStatementType.AddReplica:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Add);
				this.GenerateSpaceAndIdentifier("REPLICA");
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<AvailabilityReplica>(node.Replicas);
				return;
			case AlterAvailabilityGroupStatementType.ModifyReplica:
				this.GenerateSpaceAndIdentifier("MODIFY");
				this.GenerateSpaceAndIdentifier("REPLICA");
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<AvailabilityReplica>(node.Replicas);
				return;
			case AlterAvailabilityGroupStatementType.RemoveReplica:
				this.GenerateSpaceAndIdentifier("REMOVE");
				this.GenerateSpaceAndIdentifier("REPLICA");
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<AvailabilityReplica>(node.Replicas);
				return;
			case AlterAvailabilityGroupStatementType.Set:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Set);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<AvailabilityGroupOption>(node.Options);
				return;
			case AlterAvailabilityGroupStatementType.Action:
				this.GenerateSpaceAndFragmentIfNotNull(node.Action);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0007BD4A File Offset: 0x00079F4A
		public override void ExplicitVisit(AlterAvailabilityGroupAction node)
		{
			AlterAvailabilityGroupActionTypeHelper.Instance.GenerateSourceForOption(this._writer, node.ActionType);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0007BD62 File Offset: 0x00079F62
		public override void ExplicitVisit(AlterAvailabilityGroupFailoverAction node)
		{
			this.GenerateIdentifier("FAILOVER");
			if (node.Options != null && node.Options.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<AlterAvailabilityGroupFailoverOption>(node.Options);
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0007BDA2 File Offset: 0x00079FA2
		public override void ExplicitVisit(AlterAvailabilityGroupFailoverOption node)
		{
			this.GenerateNameEqualsValue("TARGET", node.Value);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0007BDB5 File Offset: 0x00079FB5
		public override void ExplicitVisit(DropAvailabilityGroupStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateIdentifier("AVAILABILITY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0007BDE0 File Offset: 0x00079FE0
		public override void ExplicitVisit(AvailabilityReplica node)
		{
			this.GenerateFragmentIfNotNull(node.ServerName);
			if (node.Options != null && node.Options.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<AvailabilityReplicaOption>(node.Options);
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0007BE2C File Offset: 0x0007A02C
		public override void ExplicitVisit(AvailabilityModeReplicaOption node)
		{
			string text = ((node.Value == AvailabilityModeOptionKind.AsynchronousCommit) ? "ASYNCHRONOUS_COMMIT" : "SYNCHRONOUS_COMMIT");
			this.GenerateNameEqualsValue("AVAILABILITY_MODE", text);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0007BE5C File Offset: 0x0007A05C
		public override void ExplicitVisit(FailoverModeReplicaOption node)
		{
			string text = ((node.Value == FailoverModeOptionKind.Automatic) ? "AUTOMATIC" : "MANUAL");
			this.GenerateNameEqualsValue("FAILOVER_MODE", text);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0007BE8A File Offset: 0x0007A08A
		public override void ExplicitVisit(LiteralReplicaOption node)
		{
			AvailabilityReplicaOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0007BEBC File Offset: 0x0007A0BC
		public override void ExplicitVisit(SecondaryRoleReplicaOption node)
		{
			this.GenerateIdentifier("SECONDARY_ROLE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateIdentifier("ALLOW_CONNECTIONS");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			AllowConnectionsOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.AllowConnections);
			this.GenerateKeyword(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0007BF1C File Offset: 0x0007A11C
		public override void ExplicitVisit(PrimaryRoleReplicaOption node)
		{
			this.GenerateIdentifier("PRIMARY_ROLE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateIdentifier("ALLOW_CONNECTIONS");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			AllowConnectionsOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.AllowConnections);
			this.GenerateKeyword(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0007BF7C File Offset: 0x0007A17C
		public override void ExplicitVisit(CreateAvailabilityGroupStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateIdentifier("AVAILABILITY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateCommaSeparatedWithClause<AvailabilityGroupOption>(node.Options, false, true);
			this.NewLine();
			this.GenerateKeywordAndSpace(TSqlTokenType.For);
			if (node.Databases != null && node.Databases.Count > 0)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Database);
				this.GenerateCommaSeparatedList<Identifier>(node.Databases);
				this.NewLine();
			}
			this.GenerateIdentifier("REPLICA");
			this.GenerateSpace();
			this.GenerateKeywordAndSpace(TSqlTokenType.On);
			this.GenerateCommaSeparatedList<AvailabilityReplica>(node.Replicas, true);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0007C022 File Offset: 0x0007A222
		public override void ExplicitVisit(LiteralAvailabilityGroupOption node)
		{
			this.GenerateNameEqualsValue("REQUIRED_COPIES_TO_COMMIT", node.Value);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0007C035 File Offset: 0x0007A235
		public override void ExplicitVisit(IdentifierDatabaseOption node)
		{
			DatabaseOptionKindHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0007C064 File Offset: 0x0007A264
		public override void ExplicitVisit(ContainmentDatabaseOption node)
		{
			this.GenerateIdentifier("CONTAINMENT");
			this.GenerateSpace();
			this.GenerateSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			ContainmentOptionKindHelper.Instance.GenerateSourceForOption(this._writer, node.Value);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		public override void ExplicitVisit(DropServerRoleStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Drop, new string[] { "SERVER", "ROLE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0007C0DC File Offset: 0x0007A2DC
		public override void ExplicitVisit(CreateServerRoleStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Create, new string[] { "SERVER", "ROLE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0007C121 File Offset: 0x0007A321
		public override void ExplicitVisit(RenameAlterRoleAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.With);
			this.GenerateNameEqualsValue("NAME", node.NewName);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0007C140 File Offset: 0x0007A340
		public override void ExplicitVisit(AddMemberAlterRoleAction node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Add, new string[] { "MEMBER" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Member);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0007C170 File Offset: 0x0007A370
		public override void ExplicitVisit(DropMemberAlterRoleAction node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Drop, new string[] { "MEMBER" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Member);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0007C1A4 File Offset: 0x0007A3A4
		public override void ExplicitVisit(AlterServerRoleStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "SERVER", "ROLE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0007C1E8 File Offset: 0x0007A3E8
		public override void ExplicitVisit(AlterSearchPropertyListStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "SEARCH", "PROPERTY", "LIST" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0007C234 File Offset: 0x0007A434
		public override void ExplicitVisit(CreateSearchPropertyListStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Create, new string[] { "SEARCH", "PROPERTY", "LIST" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.SourceSearchPropertyList != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.From);
				this.GenerateSpaceAndFragmentIfNotNull(node.SourceSearchPropertyList);
			}
			this.GenerateOwnerIfNotNull(node.Owner);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0007C2A4 File Offset: 0x0007A4A4
		public override void ExplicitVisit(DropSearchPropertyListStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Drop, new string[] { "SEARCH", "PROPERTY", "LIST" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0007C2E8 File Offset: 0x0007A4E8
		public override void ExplicitVisit(NextValueForExpression node)
		{
			this.GenerateSpaceAndIdentifier("NEXT");
			this.GenerateSpaceAndIdentifier("VALUE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndFragmentIfNotNull(node.SequenceName);
			if (node.OverClause != null)
			{
				this.GenerateSpace();
				this.ExplicitVisit(node.OverClause);
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0007C339 File Offset: 0x0007A539
		public override void ExplicitVisit(DropSequenceStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("SEQUENCE");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0007C360 File Offset: 0x0007A560
		public override void ExplicitVisit(CreateSequenceStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("SEQUENCE");
			this.GenerateSequenceStatementBody(node);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0007C37C File Offset: 0x0007A57C
		public override void ExplicitVisit(AddSearchPropertyListAction node)
		{
			this.GenerateKeyword(TSqlTokenType.Add);
			this.GenerateSpaceAndFragmentIfNotNull(node.PropertyName);
			this.GenerateSpaceAndKeyword(TSqlTokenType.With);
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateSpaceAndIdentifier("PROPERTY_SET_GUID");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Guid);
			this.GenerateKeyword(TSqlTokenType.Comma);
			this.GenerateSpaceAndIdentifier("PROPERTY_INT_ID");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Id);
			if (node.Description != null)
			{
				this.GenerateKeyword(TSqlTokenType.Comma);
				this.GenerateSpaceAndIdentifier("PROPERTY_DESCRIPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.Description);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0007C441 File Offset: 0x0007A641
		public override void ExplicitVisit(DropSearchPropertyListAction node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndFragmentIfNotNull(node.PropertyName);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0007C458 File Offset: 0x0007A658
		public void GenerateSequenceStatementBody(SequenceStatement node)
		{
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			foreach (SequenceOption sequenceOption in node.SequenceOptions)
			{
				this.GenerateFragmentIfNotNull(sequenceOption);
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
		public override void ExplicitVisit(DataTypeSequenceOption node)
		{
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0007C4D0 File Offset: 0x0007A6D0
		public override void ExplicitVisit(SequenceOption node)
		{
			this.NewLineAndIndent();
			if (node.NoValue)
			{
				this.GenerateIdentifier("NO");
				this.GenerateSpace();
			}
			switch (node.OptionKind)
			{
			case SequenceOptionKind.MinValue:
				this.GenerateIdentifier("MINVALUE");
				return;
			case SequenceOptionKind.MaxValue:
				this.GenerateIdentifier("MAXVALUE");
				return;
			case SequenceOptionKind.Cache:
				this.GenerateIdentifier("CACHE");
				return;
			case SequenceOptionKind.Cycle:
				this.GenerateIdentifier("Cycle");
				return;
			default:
				return;
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0007C54C File Offset: 0x0007A74C
		public override void ExplicitVisit(ScalarExpressionSequenceOption node)
		{
			this.NewLineAndIndent();
			switch (node.OptionKind)
			{
			case SequenceOptionKind.MinValue:
				this.GenerateIdentifier("MINVALUE");
				break;
			case SequenceOptionKind.MaxValue:
				this.GenerateIdentifier("MAXVALUE");
				break;
			case SequenceOptionKind.Cache:
				this.GenerateIdentifier("CACHE");
				break;
			case SequenceOptionKind.Start:
				this.GenerateIdentifier("START");
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				break;
			case SequenceOptionKind.Increment:
				this.GenerateIdentifier("INCREMENT");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				break;
			case SequenceOptionKind.Restart:
				this.GenerateIdentifier("RESTART");
				if (node.OptionValue != null)
				{
					this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				}
				break;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.OptionValue);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0007C60A File Offset: 0x0007A80A
		public override void ExplicitVisit(AlterSequenceStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("SEQUENCE");
			this.GenerateSequenceStatementBody(node);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0007C628 File Offset: 0x0007A828
		public override void ExplicitVisit(OffsetClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateIdentifier("OFFSET");
			this.GenerateSpaceAndFragmentIfNotNull(node.OffsetExpression);
			this.GenerateSpaceAndIdentifier("ROWS");
			if (node.FetchExpression != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Fetch);
				this.GenerateSpaceAndIdentifier("NEXT");
				this.GenerateSpaceAndFragmentIfNotNull(node.FetchExpression);
				this.GenerateSpaceAndIdentifier("ROWS");
				this.GenerateSpaceAndIdentifier("ONLY");
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0007C6A8 File Offset: 0x0007A8A8
		public override void ExplicitVisit(ThrowStatement node)
		{
			this.GenerateIdentifier("THROW");
			this.GenerateSpaceAndFragmentIfNotNull(node.ErrorNumber);
			if (node.ErrorNumber != null && node.Message != null)
			{
				this.GenerateKeyword(TSqlTokenType.Comma);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Message);
			if (node.Message != null && node.State != null)
			{
				this.GenerateKeyword(TSqlTokenType.Comma);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.State);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0007C71C File Offset: 0x0007A91C
		public override void ExplicitVisit(ResultSetsExecuteOption node)
		{
			this.GenerateIdentifier("RESULT");
			this.GenerateSpaceAndIdentifier("SETS");
			switch (node.ResultSetsOptionKind)
			{
			case ResultSetsOptionKind.Undefined:
				this.GenerateSpaceAndIdentifier("UNDEFINED");
				return;
			case ResultSetsOptionKind.None:
				this.GenerateSpaceAndIdentifier("NONE");
				return;
			case ResultSetsOptionKind.ResultSetsDefined:
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<ResultSetDefinition>(node.Definitions, 2);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0007C77E File Offset: 0x0007A97E
		public override void ExplicitVisit(ResultSetDefinition node)
		{
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndIdentifier("XML");
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0007C79C File Offset: 0x0007A99C
		public override void ExplicitVisit(SchemaObjectResultSetDefinition node)
		{
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			switch (node.ResultSetType)
			{
			case ResultSetType.Object:
				this.GenerateSpaceAndIdentifier("OBJECT");
				break;
			case ResultSetType.Type:
				this.GenerateSpaceAndIdentifier("TYPE");
				break;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0007C7EE File Offset: 0x0007A9EE
		public override void ExplicitVisit(InlineResultSetDefinition node)
		{
			this.GenerateParenthesisedCommaSeparatedList<ResultColumnDefinition>(node.ResultColumnDefinitions);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0007C7FC File Offset: 0x0007A9FC
		public override void ExplicitVisit(ResultColumnDefinition node)
		{
			this.GenerateFragmentIfNotNull(node.ColumnDefinition);
			this.GenerateSpaceAndFragmentIfNotNull(node.Nullable);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0007C816 File Offset: 0x0007AA16
		public override void ExplicitVisit(AdHocTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.DataSource);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.Object);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0007C848 File Offset: 0x0007AA48
		public override void ExplicitVisit(BooleanTernaryExpression node)
		{
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<BooleanTernaryExpressionType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._ternaryExpressionTypeGenerators, node.TernaryExpressionType);
			if (valueForEnumKey != null)
			{
				this.GenerateSpace();
				this.GenerateTokenList(valueForEnumKey);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			this.GenerateSpaceAndKeyword(TSqlTokenType.And);
			this.GenerateSpaceAndFragmentIfNotNull(node.ThirdExpression);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0007C8A1 File Offset: 0x0007AAA1
		public override void ExplicitVisit(BuiltInFunctionTableReference node)
		{
			this.GenerateSymbol(TSqlTokenType.DoubleColon);
			this.GenerateFragmentIfNotNull(node.Name);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0007C8DC File Offset: 0x0007AADC
		public override void ExplicitVisit(ColumnReferenceExpression node)
		{
			this.GenerateFragmentIfNotNull(node.MultiPartIdentifier);
			if (node.ColumnType != ColumnType.Regular)
			{
				if (node.MultiPartIdentifier != null && node.MultiPartIdentifier.Count > 0)
				{
					this.GenerateSymbol(TSqlTokenType.Dot);
				}
				switch (node.ColumnType)
				{
				case ColumnType.IdentityCol:
					this.GenerateKeyword(TSqlTokenType.IdentityColumn);
					break;
				case ColumnType.RowGuidCol:
					this.GenerateKeyword(TSqlTokenType.RowGuidColumn);
					break;
				case ColumnType.Wildcard:
					this.GenerateSymbol(TSqlTokenType.Star);
					break;
				case ColumnType.PseudoColumnIdentity:
					this.GenerateToken(TSqlTokenType.PseudoColumn, "$IDENTITY");
					break;
				case ColumnType.PseudoColumnRowGuid:
					this.GenerateToken(TSqlTokenType.PseudoColumn, "$ROWGUID");
					break;
				case ColumnType.PseudoColumnAction:
					this.GenerateToken(TSqlTokenType.PseudoColumn, "$ACTION");
					break;
				case ColumnType.PseudoColumnCuid:
					this.GenerateToken(TSqlTokenType.PseudoColumn, "$CUID");
					break;
				}
			}
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0007C9C6 File Offset: 0x0007ABC6
		public override void ExplicitVisit(DatabaseOption node)
		{
			SimpleDbOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0007C9DE File Offset: 0x0007ABDE
		public override void ExplicitVisit(DataModificationTableReference node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.DataModificationSpecification);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0007CA0C File Offset: 0x0007AC0C
		public override void ExplicitVisit(DeclareTableVariableBody node)
		{
			this.GenerateFragmentIfNotNull(node.VariableName);
			if (node.AsDefined)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.NewLineAndIndent();
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateCommaSeparatedList<ColumnDefinition>(node.Definition.ColumnDefinitions, true);
			if (node.Definition.ColumnDefinitions != null && node.Definition.ColumnDefinitions.Count > 0 && node.Definition.TableConstraints != null && node.Definition.TableConstraints.Count > 0)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.NewLine();
			}
			this.GenerateCommaSeparatedList<ConstraintDefinition>(node.Definition.TableConstraints, true);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.PopAlignmentPoint();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0007CAE4 File Offset: 0x0007ACE4
		public override void ExplicitVisit(JoinParenthesisTableReference node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.Join);
			this.PopAlignmentPoint();
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0007CB26 File Offset: 0x0007AD26
		public override void ExplicitVisit(NamedTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.SchemaObject);
			this.GenerateSpaceAndAlias(node.Alias);
			this.GenerateSpaceAndFragmentIfNotNull(node.TableSampleClause);
			this.GenerateWithTableHints(node.TableHints);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0007CB58 File Offset: 0x0007AD58
		public override void ExplicitVisit(SchemaObjectFunctionTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.SchemaObject);
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0007CB7C File Offset: 0x0007AD7C
		public override void ExplicitVisit(OnOffDatabaseOption node)
		{
			OnOffSimpleDbOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpace();
			if (OnOffSimpleDbOptionsHelper.Instance.RequiresEqualsSign(node.OptionKind))
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.EqualsSign);
			}
			this.GenerateOptionStateOnOff(node.OptionState);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0007CBD0 File Offset: 0x0007ADD0
		public override void ExplicitVisit(OpenQueryTableReference node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenQuery);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.LinkedServer);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.Query);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0007CC2C File Offset: 0x0007AE2C
		public override void ExplicitVisit(OpenRowsetTableReference node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenRowSet);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.ProviderName);
			this.GenerateSymbol(TSqlTokenType.Comma);
			if (node.ProviderString != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.ProviderString);
			}
			else
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.DataSource);
				this.GenerateSymbol(TSqlTokenType.Semicolon);
				this.GenerateSpaceAndFragmentIfNotNull(node.UserId);
				this.GenerateSymbol(TSqlTokenType.Semicolon);
				this.GenerateSpaceAndFragmentIfNotNull(node.Password);
			}
			this.GenerateSymbol(TSqlTokenType.Comma);
			if (node.Query != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Query);
			}
			else
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Object);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0007CCF8 File Offset: 0x0007AEF8
		public override void ExplicitVisit(OpenXmlTableReference node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenXml);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Variable);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.RowPattern);
			if (node.Flags != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.Flags);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			if (node.TableName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndFragmentIfNotNull(node.TableName);
			}
			else if (node.SchemaDeclarationItems.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<SchemaDeclarationItem>(node.SchemaDeclarationItems);
			}
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0007CDC0 File Offset: 0x0007AFC0
		public override void ExplicitVisit(LiteralOptimizerHint node)
		{
			if (node.HintKind == OptimizerHintKind.UsePlan)
			{
				if (node.Value != null && node.Value.LiteralType == LiteralType.Integer)
				{
					this.GenerateIdentifier("USEPLAN");
				}
				else
				{
					this.GenerateKeyword(TSqlTokenType.Use);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Plan);
				}
			}
			else
			{
				List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<OptimizerHintKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._optimizerHintKindsGenerators, node.HintKind);
				if (valueForEnumKey != null)
				{
					this.GenerateTokenList(valueForEnumKey);
				}
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0007CE38 File Offset: 0x0007B038
		public override void ExplicitVisit(OptimizerHint node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<OptimizerHintKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._optimizerHintKindsGenerators, node.HintKind);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0007CE60 File Offset: 0x0007B060
		public override void ExplicitVisit(PageVerifyDatabaseOption node)
		{
			this.GenerateIdentifier("PAGE_VERIFY");
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PageVerifyDatabaseOptionKind, string>(SqlScriptGeneratorVisitor._pageVerifyDatabaseOptionKindNames, node.Value);
			if (valueForEnumKey != null)
			{
				this.GenerateSpaceAndIdentifier(valueForEnumKey);
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0007CE93 File Offset: 0x0007B093
		public override void ExplicitVisit(ParameterizationDatabaseOption node)
		{
			this.GenerateIdentifier("PARAMETERIZATION");
			this.GenerateSpaceAndIdentifier(node.IsSimple ? "SIMPLE" : "FORCED");
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0007CEBC File Offset: 0x0007B0BC
		public override void ExplicitVisit(PartnerDatabaseOption node)
		{
			this.GenerateIdentifier("PARTNER");
			this.GenerateSpace();
			if (node.PartnerServer != null)
			{
				this.GenerateSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.PartnerServer);
			}
			else if (node.PartnerOption == PartnerDatabaseOptionKind.SafetyFull)
			{
				this.GenerateIdentifier("SAFETY");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Full);
			}
			else if (node.PartnerOption == PartnerDatabaseOptionKind.SafetyOff)
			{
				this.GenerateIdentifier("SAFETY");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Off);
			}
			else
			{
				PartnerDbOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.PartnerOption);
			}
			if (node.PartnerOption == PartnerDatabaseOptionKind.Timeout && node.Timeout != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Timeout);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0007CF6C File Offset: 0x0007B16C
		public override void ExplicitVisit(PasswordAlterPrincipalOption node)
		{
			this.GenerateNameEqualsValue("PASSWORD", node.Password);
			if (node.OldPassword != null)
			{
				this.GenerateSpace();
				this.GenerateNameEqualsValue("OLD_PASSWORD", node.OldPassword);
				return;
			}
			if (node.MustChange)
			{
				this.GenerateSpaceAndIdentifier("MUST_CHANGE");
			}
			if (node.Hashed)
			{
				this.GenerateSpaceAndIdentifier("HASHED");
			}
			if (node.Unlock)
			{
				this.GenerateSpaceAndIdentifier("UNLOCK");
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0007CFE4 File Offset: 0x0007B1E4
		public override void ExplicitVisit(PivotedTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.TableReference);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Pivot);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.AggregateFunctionIdentifier);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateCommaSeparatedList<ColumnReferenceExpression>(node.ValueColumns);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndFragmentIfNotNull(node.PivotColumn);
			this.GenerateSpaceAndKeyword(TSqlTokenType.In);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.InColumns, true);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0007D088 File Offset: 0x0007B288
		public override void ExplicitVisit(QueryParenthesisExpression node)
		{
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.GenerateQueryParenthesisExpression(node, alignmentPointForFragment, null);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0007D0AC File Offset: 0x0007B2AC
		public void GenerateQueryParenthesisExpression(QueryParenthesisExpression node, AlignmentPoint clauseBody, SchemaObjectName intoClause)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateQueryExpression(node.QueryExpression, clauseBody, intoClause);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			if (node.OrderByClause != null)
			{
				this.GenerateSeparatorForOrderBy();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OrderByClause, clauseBody);
			}
			if (node.OffsetClause != null)
			{
				this.GenerateSeparatorForOffsetClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OffsetClause, clauseBody);
			}
			if (node.ForClause != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.For);
				this.MarkClauseBodyAlignmentWhenNecessary(true, clauseBody);
				this.GenerateSpace();
				AlignmentPoint alignmentPoint = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateFragmentIfNotNull(node.ForClause);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0007D158 File Offset: 0x0007B358
		public override void ExplicitVisit(RecoveryDatabaseOption node)
		{
			this.GenerateIdentifier("RECOVERY");
			this.GenerateSpace();
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<RecoveryDatabaseOptionKind, TokenGenerator>(SqlScriptGeneratorVisitor._recoveryDatabaseOptionKindNames, node.Value);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0007D194 File Offset: 0x0007B394
		public override void ExplicitVisit(RestoreOption node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<RestoreOptionKind, TokenGenerator>(SqlScriptGeneratorVisitor._restoreOptionKindGenerators, node.OptionKind);
			if (valueForEnumKey == null)
			{
				return;
			}
			this.GenerateToken(valueForEnumKey);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
		public override void ExplicitVisit(ScalarExpressionRestoreOption node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<RestoreOptionKind, TokenGenerator>(SqlScriptGeneratorVisitor._restoreOptionKindGenerators, node.OptionKind);
			if (valueForEnumKey == null)
			{
				return;
			}
			if (node.Value != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0007D1F7 File Offset: 0x0007B3F7
		public override void ExplicitVisit(ScalarSubquery node)
		{
			this.GenerateQueryExpressionInParentheses(node.QueryExpression);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0007D211 File Offset: 0x0007B411
		public override void ExplicitVisit(MultiPartIdentifier node)
		{
			this.GenerateDotSeparatedList<Identifier>(node.Identifiers);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0007D21F File Offset: 0x0007B41F
		public override void ExplicitVisit(BooleanParenthesisExpression node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Expression);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0007D244 File Offset: 0x0007B444
		public override void ExplicitVisit(BooleanComparisonExpression node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			this.GenerateSpace();
			this.GenerateBinaryOperator(node.ComparisonType);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			this.PopAlignmentPoint();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0007D28E File Offset: 0x0007B48E
		public override void ExplicitVisit(BooleanNotExpression node)
		{
			this.GenerateToken(new KeywordGenerator(TSqlTokenType.Not));
			this.GenerateSpace();
			this.GenerateFragmentIfNotNull(node.Expression);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0007D2AF File Offset: 0x0007B4AF
		public override void ExplicitVisit(BooleanIsNullExpression node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
			this.GenerateSpace();
			this.GenerateKeywordAndSpace(TSqlTokenType.Is);
			if (node.IsNot)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Not);
			}
			this.GenerateKeyword(TSqlTokenType.Null);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
		public override void ExplicitVisit(BooleanBinaryExpression node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			bool flag = this.RightPredicateOnNewline(node);
			this.GenerateNewLineOrSpace(flag);
			this.GenerateBinaryOperator(node.BinaryExpressionType);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			this.PopAlignmentPoint();
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0007D338 File Offset: 0x0007B538
		private bool RightPredicateOnNewline(BooleanBinaryExpression node)
		{
			return this._options.MultilineWherePredicatesList && this._options.NewLineBeforeWhereClause && (node.BinaryExpressionType == BooleanBinaryExpressionType.And || node.BinaryExpressionType == BooleanBinaryExpressionType.Or);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0007D378 File Offset: 0x0007B578
		public override void ExplicitVisit(AlterServerConfigurationStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "SERVER", "CONFIGURATION" });
			this.GenerateSpaceAndKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndIdentifier("PROCESS");
			this.GenerateSpaceAndIdentifier("AFFINITY");
			switch (node.ProcessAffinity)
			{
			case ProcessAffinityType.CpuAuto:
				this.GenerateSpaceAndIdentifier("CPU");
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndIdentifier("AUTO");
				return;
			case ProcessAffinityType.Cpu:
				this.GenerateSpaceAndIdentifier("CPU");
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<ProcessAffinityRange>(node.ProcessAffinityRanges);
				return;
			case ProcessAffinityType.NumaNode:
				this.GenerateSpaceAndIdentifier("NUMANODE");
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<ProcessAffinityRange>(node.ProcessAffinityRanges);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0007D452 File Offset: 0x0007B652
		public override void ExplicitVisit(ProcessAffinityRange node)
		{
			this.GenerateFragmentIfNotNull(node.From);
			if (node.To != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.To);
				this.GenerateSpaceAndFragmentIfNotNull(node.To);
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0007D47F File Offset: 0x0007B67F
		public override void ExplicitVisit(AlterFullTextStopListStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.StopList);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0007D4B8 File Offset: 0x0007B6B8
		public override void ExplicitVisit(AlterResourceGovernorStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateIdentifier("RESOURCE");
			this.GenerateSpaceAndIdentifier("GOVERNOR");
			switch (node.Command)
			{
			case AlterResourceGovernorCommandType.Disable:
				this.GenerateSpaceAndIdentifier("DISABLE");
				return;
			case AlterResourceGovernorCommandType.Reconfigure:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Reconfigure);
				return;
			case AlterResourceGovernorCommandType.ClassifierFunction:
				this.GenerateResourceGovernorClassifierFunction(node);
				return;
			case AlterResourceGovernorCommandType.ResetStatistics:
				this.GenerateSpaceAndIdentifier("RESET");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Statistics);
				return;
			default:
				return;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0007D538 File Offset: 0x0007B738
		public void GenerateResourceGovernorClassifierFunction(AlterResourceGovernorStatement node)
		{
			this.GenerateSpaceAndKeyword(TSqlTokenType.With);
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateIdentifier("CLASSIFIER_FUNCTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			if (node.ClassifierFunction != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.ClassifierFunction);
			}
			else
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Null);
			}
			this.GenerateKeyword(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0007D59C File Offset: 0x0007B79C
		public override void ExplicitVisit(AuditTarget node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.To);
			switch (node.TargetKind)
			{
			case AuditTargetKind.File:
				this.GenerateKeywordAndSpace(TSqlTokenType.File);
				this.GenerateParenthesisedCommaSeparatedList<AuditTargetOption>(node.TargetOptions);
				return;
			case AuditTargetKind.ApplicationLog:
				this.GenerateIdentifier("APPLICATION_LOG");
				return;
			case AuditTargetKind.SecurityLog:
				this.GenerateIdentifier("SECURITY_LOG");
				return;
			default:
				return;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0007D5FC File Offset: 0x0007B7FC
		public override void ExplicitVisit(MaxSizeAuditTargetOption node)
		{
			this.GenerateTokenAndEqualSign("MAXSIZE");
			if (node.IsUnlimited)
			{
				this.GenerateSpaceAndIdentifier("UNLIMITED");
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Size);
			this.GenerateSpace();
			MemoryUnitsHelper.Instance.GenerateSourceForOption(this._writer, node.Unit);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0007D650 File Offset: 0x0007B850
		public override void ExplicitVisit(MaxRolloverFilesAuditTargetOption node)
		{
			this.GenerateTokenAndEqualSign("MAX_ROLLOVER_FILES");
			if (node.IsUnlimited)
			{
				this.GenerateSpaceAndIdentifier("UNLIMITED");
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0007D680 File Offset: 0x0007B880
		public override void ExplicitVisit(LiteralAuditTargetOption node)
		{
			AuditTargetOptionKind optionKind = node.OptionKind;
			if (optionKind == AuditTargetOptionKind.FilePath)
			{
				this.GenerateNameEqualsValue("FILEPATH", node.Value);
				return;
			}
			if (optionKind != AuditTargetOptionKind.MaxFiles)
			{
				return;
			}
			this.GenerateNameEqualsValue("MAX_FILES", node.Value);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0007D6C0 File Offset: 0x0007B8C0
		public override void ExplicitVisit(OnOffAuditTargetOption node)
		{
			this.GenerateOptionStateWithEqualSign("RESERVE_DISK_SPACE", node.Value);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0007D6D3 File Offset: 0x0007B8D3
		public override void ExplicitVisit(ExpressionCallTarget node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
			this.GenerateSymbol(TSqlTokenType.Dot);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0007D6EC File Offset: 0x0007B8EC
		public override void ExplicitVisit(MultiPartIdentifierCallTarget node)
		{
			this.GenerateFragmentIfNotNull(node.MultiPartIdentifier);
			this.GenerateSymbol(TSqlTokenType.Dot);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0007D705 File Offset: 0x0007B905
		public override void ExplicitVisit(UserDefinedTypeCallTarget node)
		{
			this.GenerateFragmentIfNotNull(node.SchemaObjectName);
			this.GenerateSymbol(TSqlTokenType.DoubleColon);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0007D71E File Offset: 0x0007B91E
		protected void GenerateChangeTablePrefix(SchemaObjectName target, string changeTableKind)
		{
			this.GenerateIdentifier("CHANGETABLE");
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateIdentifier(changeTableKind);
			this.GenerateSpaceAndFragmentIfNotNull(target);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0007D74F File Offset: 0x0007B94F
		public override void ExplicitVisit(ChangeTableChangesTableReference node)
		{
			this.GenerateChangeTablePrefix(node.Target, "CHANGES");
			this.GenerateFragmentIfNotNull(node.SinceVersion);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0007D780 File Offset: 0x0007B980
		public override void ExplicitVisit(ChangeTableVersionTableReference node)
		{
			this.GenerateChangeTablePrefix(node.Target, "VERSION");
			this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.PrimaryKeyColumns);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.PrimaryKeyValues);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0007D7D3 File Offset: 0x0007B9D3
		public override void ExplicitVisit(CompressionPartitionRange node)
		{
			this.GenerateFragmentIfNotNull(node.From);
			if (node.To != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.To);
				this.GenerateSpaceAndFragmentIfNotNull(node.To);
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0007D800 File Offset: 0x0007BA00
		public override void ExplicitVisit(CreateFullTextStopListStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.StopList);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.IsSystemStopList)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
				this.GenerateSpaceAndIdentifier("SYSTEM");
				this.GenerateSpaceAndKeyword(TSqlTokenType.StopList);
			}
			else if (node.SourceStopListName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
				if (node.DatabaseName != null)
				{
					this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
					this.GenerateKeyword(TSqlTokenType.Dot);
				}
				else
				{
					this.GenerateSpace();
				}
				this.GenerateFragmentIfNotNull(node.SourceStopListName);
			}
			this.GenerateOwnerIfNotNull(node.Owner);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		public override void ExplicitVisit(CreateSpatialIndexStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateIdentifier("SPATIAL");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.Object);
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.SpatialColumnName);
			this.GenerateKeyword(TSqlTokenType.RightParenthesis);
			this.PopAlignmentPoint();
			if (node.SpatialIndexingScheme != SpatialIndexingSchemeType.None)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateIdentifier("USING");
				this.GenerateSpace();
				SpatialIndexingSchemeTypeHelper.Instance.GenerateSourceForOption(this._writer, node.SpatialIndexingScheme);
				this.PopAlignmentPoint();
			}
			if (node.SpatialIndexOptions != null && node.SpatialIndexOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<SpatialIndexOption>(node.SpatialIndexOptions, 2);
				this.PopAlignmentPoint();
			}
			if (node.OnFileGroup != null)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroup);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0007D9EB File Offset: 0x0007BBEB
		public override void ExplicitVisit(SpatialIndexRegularOption node)
		{
			this.GenerateFragmentIfNotNull(node.Option);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0007D9F9 File Offset: 0x0007BBF9
		public override void ExplicitVisit(BoundingBoxSpatialIndexOption node)
		{
			this.GenerateIdentifier("BOUNDING_BOX");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.BoundingBoxParameters != null && node.BoundingBoxParameters.Count > 0)
			{
				this.GenerateParenthesisedCommaSeparatedList<BoundingBoxParameter>(node.BoundingBoxParameters);
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0007DA39 File Offset: 0x0007BC39
		public override void ExplicitVisit(BoundingBoxParameter node)
		{
			if (node.Parameter != BoundingBoxParameterType.None)
			{
				BoundingBoxParameterTypeHelper.Instance.GenerateSourceForOption(this._writer, node.Parameter);
				this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
			}
			this.GenerateFragmentIfNotNull(node.Value);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0007DA76 File Offset: 0x0007BC76
		public override void ExplicitVisit(GridsSpatialIndexOption node)
		{
			this.GenerateIdentifier("GRIDS");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.GridParameters != null && node.GridParameters.Count > 0)
			{
				this.GenerateParenthesisedCommaSeparatedList<GridParameter>(node.GridParameters);
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0007DAB8 File Offset: 0x0007BCB8
		public override void ExplicitVisit(GridParameter node)
		{
			if (node.Parameter != GridParameterType.None)
			{
				GridParameterTypeHelper.Instance.GenerateSourceForOption(this._writer, node.Parameter);
				this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
			}
			ImportanceParameterHelper.Instance.GenerateSourceForOption(this._writer, node.Value);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0007DB0A File Offset: 0x0007BD0A
		public override void ExplicitVisit(CellsPerObjectSpatialIndexOption node)
		{
			this.GenerateIdentifier("CELLS_PER_OBJECT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0007DB30 File Offset: 0x0007BD30
		protected void GenerateCredentialStatementBody(CredentialStatement node)
		{
			this.GenerateIdentifier("CREDENTIAL");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.Identity != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateNameEqualsValue(TSqlTokenType.Identity, node.Identity);
			}
			if (node.Secret != null)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateNameEqualsValue("SECRET", node.Secret);
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0007DBA8 File Offset: 0x0007BDA8
		public override void ExplicitVisit(CreateCryptographicProviderStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateIdentifier("CRYPTOGRAPHIC");
			this.GenerateSpaceAndIdentifier("PROVIDER");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.CryptographicProviderFile(node.File, alignmentPoint);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0007DBF2 File Offset: 0x0007BDF2
		public void CryptographicProviderFile(Literal node, AlignmentPoint ap)
		{
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(ap);
			this.GenerateKeywordAndSpace(TSqlTokenType.From);
			this.GenerateNameEqualsValue(TSqlTokenType.File, node);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0007DC18 File Offset: 0x0007BE18
		public override void ExplicitVisit(AlterCryptographicProviderStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateIdentifier("CRYPTOGRAPHIC");
			this.GenerateSpaceAndIdentifier("PROVIDER");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.Option == EnableDisableOptionType.None)
			{
				this.CryptographicProviderFile(node.File, alignmentPoint);
				return;
			}
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			EnableDisableOptionTypeHelper.Instance.GenerateSourceForOption(this._writer, node.Option);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0007DC93 File Offset: 0x0007BE93
		public override void ExplicitVisit(DropCryptographicProviderStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateIdentifier("CRYPTOGRAPHIC");
			this.GenerateSpaceAndIdentifier("PROVIDER");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
		public override void ExplicitVisit(DataCompressionOption node)
		{
			this.GenerateTokenAndEqualSign("DATA_COMPRESSION");
			this.GenerateSpace();
			DataCompressionLevelHelper.Instance.GenerateSourceForOption(this._writer, node.CompressionLevel);
			if (node.PartitionRanges.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndIdentifier("PARTITIONS");
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<CompressionPartitionRange>(node.PartitionRanges);
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0007DD27 File Offset: 0x0007BF27
		public override void ExplicitVisit(SchemaObjectNameSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0007DD3D File Offset: 0x0007BF3D
		public override void ExplicitVisit(IdentifierSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0007DD54 File Offset: 0x0007BF54
		public override void ExplicitVisit(QueueStateOption node)
		{
			if (node.OptionKind == QueueOptionKind.PoisonMessageHandlingStatus)
			{
				this.GenerateIdentifier("POISON_MESSAGE_HANDLING");
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateOptionStateWithEqualSign("STATUS", node.OptionState);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				return;
			}
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<QueueOptionKind, string>(SqlScriptGeneratorVisitor._queueOptionTypeNames, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateOptionStateWithEqualSign(valueForEnumKey, node.OptionState);
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0007DDC0 File Offset: 0x0007BFC0
		protected void GenerateQueueOptions(IList<QueueOption> queueOptions)
		{
			List<QueueOption> list = new List<QueueOption>();
			List<QueueOption> list2 = new List<QueueOption>();
			foreach (QueueOption queueOption in queueOptions)
			{
				switch (queueOption.OptionKind)
				{
				case QueueOptionKind.Status:
				case QueueOptionKind.Retention:
				case QueueOptionKind.PoisonMessageHandlingStatus:
					list.Add(queueOption);
					break;
				case QueueOptionKind.ActivationStatus:
				case QueueOptionKind.ActivationProcedureName:
				case QueueOptionKind.ActivationMaxQueueReaders:
				case QueueOptionKind.ActivationExecuteAs:
				case QueueOptionKind.ActivationDrop:
					list2.Add(queueOption);
					break;
				}
			}
			this.GenerateCommaSeparatedList<QueueOption>(list);
			if (list.Count > 0 && list2.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			}
			if (list2.Count > 0)
			{
				this.GenerateIdentifier("ACTIVATION");
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<QueueOption>(list2);
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0007DE94 File Offset: 0x0007C094
		public override void ExplicitVisit(QueueOption node)
		{
			if (node.OptionKind == QueueOptionKind.ActivationDrop)
			{
				this.GenerateKeyword(TSqlTokenType.Drop);
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0007DEA8 File Offset: 0x0007C0A8
		public override void ExplicitVisit(QueueProcedureOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<QueueOptionKind, string>(SqlScriptGeneratorVisitor._queueOptionTypeNames, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.OptionValue);
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0007DED8 File Offset: 0x0007C0D8
		public override void ExplicitVisit(QueueValueOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<QueueOptionKind, string>(SqlScriptGeneratorVisitor._queueOptionTypeNames, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.OptionValue);
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0007DF06 File Offset: 0x0007C106
		public override void ExplicitVisit(QueueExecuteAsOption node)
		{
			this.GenerateFragmentIfNotNull(node.OptionValue);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0007DF14 File Offset: 0x0007C114
		public override void ExplicitVisit(SelectInsertSource node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateFragmentWithAlignmentPointIfNotNull(node.Select, alignmentPoint);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0007DF39 File Offset: 0x0007C139
		public override void ExplicitVisit(SelectScalarExpression node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
			if (node.ColumnName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.ColumnName);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0007DF63 File Offset: 0x0007C163
		public override void ExplicitVisit(SelectStarExpression node)
		{
			this.GenerateFragmentIfNotNull(node.Qualifier);
			if (node.Qualifier != null && node.Qualifier.Count > 0)
			{
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			this.GenerateSymbol(TSqlTokenType.Star);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0007DF9D File Offset: 0x0007C19D
		protected void GenerateSpaceAndAlias(Identifier alias)
		{
			if (alias != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(alias);
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0007DFB1 File Offset: 0x0007C1B1
		protected void GenerateTableAndColumnAliases(TableReferenceWithAliasAndColumns node)
		{
			this.GenerateSpaceAndAlias(node.Alias);
			this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0007DFCB File Offset: 0x0007C1CB
		public override void ExplicitVisit(LiteralDatabaseOption node)
		{
			DatabaseOptionKindHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0007DFFA File Offset: 0x0007C1FA
		public override void ExplicitVisit(VariableTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.Variable);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0007E014 File Offset: 0x0007C214
		public override void ExplicitVisit(VariableMethodCallTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.Variable);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.MethodName);
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0007E050 File Offset: 0x0007C250
		public override void ExplicitVisit(WithCtesAndXmlNamespaces node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.GenerateKeyword(TSqlTokenType.With);
			if (node.ChangeTrackingContext != null)
			{
				this.GenerateSpaceAndIdentifier("CHANGE_TRACKING_CONTEXT");
				this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentIfNotNull(node.ChangeTrackingContext);
				this.GenerateKeyword(TSqlTokenType.RightParenthesis);
				if (node.CommonTableExpressions.Count > 0)
				{
					this.GenerateKeyword(TSqlTokenType.Comma);
				}
			}
			if (node.XmlNamespaces != null)
			{
				this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
				this.GenerateSpaceAndFragmentIfNotNull(node.XmlNamespaces);
			}
			if (node.CommonTableExpressions.Count > 0)
			{
				if (node.XmlNamespaces != null)
				{
					this.GenerateSymbol(TSqlTokenType.Comma);
					this.NewLine();
				}
				foreach (CommonTableExpression commonTableExpression in node.CommonTableExpressions)
				{
					this.AddAlignmentPointForFragment(commonTableExpression, alignmentPointForFragment);
				}
				this.GenerateCommaSeparatedList<CommonTableExpression>(node.CommonTableExpressions, true);
				foreach (CommonTableExpression commonTableExpression2 in node.CommonTableExpressions)
				{
					this.ClearAlignmentPointsForFragment(commonTableExpression2);
				}
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0007E1B0 File Offset: 0x0007C3B0
		public override void ExplicitVisit(WitnessDatabaseOption node)
		{
			this.GenerateIdentifier("WITNESS");
			this.GenerateSpace();
			if (node.WitnessServer != null)
			{
				this.GenerateSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.WitnessServer);
				return;
			}
			if (node.IsOff)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Off);
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0007E1FE File Offset: 0x0007C3FE
		public override void ExplicitVisit(AlterBrokerPriorityStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateBrokerPriorityStatementBody(node);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0007E20E File Offset: 0x0007C40E
		public override void ExplicitVisit(AutoCleanupChangeTrackingOptionDetail node)
		{
			this.GenerateIdentifier("AUTO_CLEANUP");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndKeyword(node.IsOn ? TSqlTokenType.On : TSqlTokenType.Off);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0007E23C File Offset: 0x0007C43C
		public override void ExplicitVisit(BrokerPriorityParameter node)
		{
			BrokerPriorityParameterHelper.Instance.GenerateSourceForOption(this._writer, node.ParameterType);
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			switch (node.IsDefaultOrAny)
			{
			case BrokerPriorityParameterSpecialType.None:
				this.GenerateSpaceAndFragmentIfNotNull(node.ParameterValue);
				return;
			case BrokerPriorityParameterSpecialType.Any:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Any);
				return;
			case BrokerPriorityParameterSpecialType.Default:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
		protected void GenerateBrokerPriorityStatementBody(BrokerPriorityStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateIdentifier("BROKER");
			this.GenerateSpaceAndIdentifier("PRIORITY");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			if (node.BrokerPriorityParameters != null && node.BrokerPriorityParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeyword(TSqlTokenType.Set);
				this.GenerateSpace();
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<BrokerPriorityParameter>(node.BrokerPriorityParameters, 2);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0007E334 File Offset: 0x0007C534
		public override void ExplicitVisit(AlterTableRebuildStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndIdentifier("REBUILD");
			this.GenerateSpaceAndFragmentIfNotNull(node.Partition);
			if (node.IndexOptions.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<IndexOption>(node.IndexOptions);
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0007E38A File Offset: 0x0007C58A
		public override void ExplicitVisit(AlterTableSetStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Set);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<TableOption>(node.Options);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0007E3B0 File Offset: 0x0007C5B0
		public override void ExplicitVisit(AlterTableChangeTrackingModificationStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndIdentifier(node.IsEnable ? "ENABLE" : "DISABLE");
			this.GenerateSpaceAndIdentifier("CHANGE_TRACKING");
			if (node.TrackColumnsUpdated != OptionState.NotSet)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateOptionStateWithEqualSign("TRACK_COLUMNS_UPDATED", node.TrackColumnsUpdated);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0007E423 File Offset: 0x0007C623
		public override void ExplicitVisit(DropFullTextStopListStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.StopList);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0007E44F File Offset: 0x0007C64F
		public override void ExplicitVisit(CreateEventSessionStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateEventSessionParameters(node);
			this.GenerateEventDeclarations(node);
			this.GenerateTargetDeclarations(node);
			this.GenerateEventSessionOptions(node);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0007E475 File Offset: 0x0007C675
		public void GenerateEventSessionParameters(EventSessionStatement node)
		{
			this.GenerateIdentifier("EVENT");
			this.GenerateSpaceAndIdentifier("SESSION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndIdentifier("SERVER");
			this.GenerateSpace();
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0007E4B2 File Offset: 0x0007C6B2
		public void GenerateEventDeclarations(EventSessionStatement node)
		{
			if (node.EventDeclarations != null && node.EventDeclarations.Count > 0)
			{
				this.GenerateCommaSeparatedList<EventDeclaration>(node.EventDeclarations);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0007E4D6 File Offset: 0x0007C6D6
		public void GenerateTargetDeclarations(EventSessionStatement node)
		{
			if (node.TargetDeclarations != null && node.TargetDeclarations.Count > 0)
			{
				if (node is CreateEventSessionStatement)
				{
					this.GenerateSpace();
				}
				this.GenerateCommaSeparatedList<TargetDeclaration>(node.TargetDeclarations);
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0007E508 File Offset: 0x0007C708
		public void GenerateEventSessionOptions(EventSessionStatement node)
		{
			if (node.SessionOptions != null && node.SessionOptions.Count > 0)
			{
				this.NewLine();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<SessionOption>(node.SessionOptions, 2);
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0007E540 File Offset: 0x0007C740
		public override void ExplicitVisit(EventDeclaration node)
		{
			this.NewLine();
			this.GenerateKeywordAndSpace(TSqlTokenType.Add);
			this.GenerateIdentifier("EVENT");
			this.GenerateSpaceAndFragmentIfNotNull(node.ObjectName);
			if (node.EventDeclarationSetParameters.Count > 0 || node.EventDeclarationActionParameters.Count > 0 || node.EventDeclarationPredicateParameter != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.LeftParenthesis);
			}
			if (node.EventDeclarationSetParameters != null && node.EventDeclarationSetParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.Set);
				this.GenerateFragmentList<EventDeclarationSetParameter>(node.EventDeclarationSetParameters, SqlScriptGeneratorVisitor.ListGenerationOption.MultipleLineSelectElementOption);
			}
			if (node.EventDeclarationActionParameters != null && node.EventDeclarationActionParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("ACTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentList<EventSessionObjectName>(node.EventDeclarationActionParameters, SqlScriptGeneratorVisitor.ListGenerationOption.MultipleLineSelectElementOption);
				this.GenerateKeyword(TSqlTokenType.RightParenthesis);
			}
			if (node.EventDeclarationPredicateParameter != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.Where);
				this.GenerateFragmentIfNotNull(node.EventDeclarationPredicateParameter);
			}
			if (node.EventDeclarationSetParameters.Count > 0 || node.EventDeclarationActionParameters.Count > 0 || node.EventDeclarationPredicateParameter != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0007E686 File Offset: 0x0007C886
		public override void ExplicitVisit(EventDeclarationSetParameter node)
		{
			this.GenerateFragmentIfNotNull(node.EventField);
			this.GenerateSpace();
			this.GenerateKeywordAndSpace(TSqlTokenType.EqualsSign);
			this.GenerateFragmentIfNotNull(node.EventValue);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0007E6B1 File Offset: 0x0007C8B1
		public override void ExplicitVisit(EventSessionObjectName node)
		{
			this.GenerateFragmentIfNotNull(node.MultiPartIdentifier);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0007E6C0 File Offset: 0x0007C8C0
		public override void ExplicitVisit(EventDeclarationCompareFunctionParameter node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.SourceDeclaration);
			this.GenerateKeyword(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.EventValue);
			this.GenerateKeyword(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0007E714 File Offset: 0x0007C914
		public override void ExplicitVisit(TargetDeclaration node)
		{
			this.NewLine();
			this.GenerateKeywordAndSpace(TSqlTokenType.Add);
			this.GenerateIdentifier("TARGET");
			this.GenerateSpaceAndFragmentIfNotNull(node.ObjectName);
			if (node.TargetDeclarationParameters != null && node.TargetDeclarationParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.LeftParenthesis);
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.Set);
				this.GenerateFragmentList<EventDeclarationSetParameter>(node.TargetDeclarationParameters, SqlScriptGeneratorVisitor.ListGenerationOption.MultipleLineSelectElementOption);
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0007E7A0 File Offset: 0x0007C9A0
		public override void ExplicitVisit(LiteralSessionOption node)
		{
			switch (node.OptionKind)
			{
			case SessionOptionKind.MaxMemory:
				this.GenerateIdentifier("MAX_MEMORY");
				break;
			case SessionOptionKind.MaxEventSize:
				this.GenerateIdentifier("MAX_EVENT_SIZE");
				break;
			}
			this.GenerateIntegerValueSessionOption(node);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0007E7E8 File Offset: 0x0007C9E8
		public override void ExplicitVisit(MaxDispatchLatencySessionOption node)
		{
			this.GenerateIdentifier("MAX_DISPATCH_LATENCY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.IsInfinite)
			{
				this.GenerateIdentifier("INFINITE");
				return;
			}
			this.GenerateFragmentIfNotNull(node.Value);
			this.GenerateSpace();
			this.GenerateIdentifier("SECONDS");
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0007E842 File Offset: 0x0007CA42
		public void GenerateIntegerValueSessionOption(LiteralSessionOption node)
		{
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			this.GenerateFragmentIfNotNull(node.Value);
			this.GenerateSpace();
			SessionOptionUnitHelper.Instance.GenerateSourceForOption(this._writer, node.Unit);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0007E880 File Offset: 0x0007CA80
		public override void ExplicitVisit(OnOffSessionOption node)
		{
			switch (node.OptionKind)
			{
			case SessionOptionKind.TrackCausality:
				this.GenerateOptionState("TRACK_CAUSALITY", node.OptionState, true);
				return;
			case SessionOptionKind.StartUpState:
				this.GenerateOptionState("STARTUP_STATE", node.OptionState, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0007E8CA File Offset: 0x0007CACA
		public override void ExplicitVisit(EventRetentionSessionOption node)
		{
			this.GenerateIdentifier("EVENT_RETENTION_MODE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			EventSessionEventRetentionModeTypeHelper.Instance.GenerateSourceForOption(this._writer, node.Value);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0007E8FE File Offset: 0x0007CAFE
		public override void ExplicitVisit(MemoryPartitionSessionOption node)
		{
			this.GenerateIdentifier("MEMORY_PARTITION_MODE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			EventSessionMemoryPartitionModeTypeHelper.Instance.GenerateSourceForOption(this._writer, node.Value);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0007E934 File Offset: 0x0007CB34
		public override void ExplicitVisit(AlterEventSessionStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateEventSessionParameters(node);
			switch (node.StatementType)
			{
			case AlterEventSessionStatementType.AddEventDeclarationOptionalSessionOptions:
				this.GenerateEventDeclarations(node);
				this.GenerateEventSessionOptions(node);
				return;
			case AlterEventSessionStatementType.DropEventSpecificationOptionalSessionOptions:
				this.GenerateCommaSeparatedDropDeclarations<EventSessionObjectName>(node.DropEventDeclarations, "EVENT");
				this.GenerateEventSessionOptions(node);
				return;
			case AlterEventSessionStatementType.AddTargetDeclarationOptionalSessionOptions:
				this.GenerateTargetDeclarations(node);
				this.GenerateEventSessionOptions(node);
				return;
			case AlterEventSessionStatementType.DropTargetSpecificationOptionalSessionOptions:
				this.GenerateCommaSeparatedDropDeclarations<EventSessionObjectName>(node.DropTargetDeclarations, "TARGET");
				this.GenerateEventSessionOptions(node);
				return;
			case AlterEventSessionStatementType.RequiredSessionOptions:
				this.GenerateEventSessionOptions(node);
				return;
			case AlterEventSessionStatementType.AlterStateIsStart:
				this.NewLine();
				this.GenerateNameEqualsValue("STATE", "START");
				return;
			case AlterEventSessionStatementType.AlterStateIsStop:
				this.NewLine();
				this.GenerateNameEqualsValue("STATE", "STOP");
				return;
			default:
				return;
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0007EA00 File Offset: 0x0007CC00
		public override void ExplicitVisit(DropEventSessionStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateIdentifier("EVENT");
			this.GenerateSpaceAndIdentifier("SESSION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndIdentifier("SERVER");
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0007EA40 File Offset: 0x0007CC40
		public void GenerateCommaSeparatedDropDeclarations<T>(IList<T> list, string declaration) where T : TSqlFragment
		{
			if (list != null)
			{
				bool flag = true;
				foreach (T t in list)
				{
					if (flag)
					{
						this.NewLine();
						this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
						this.GenerateIdentifier(declaration);
						flag = false;
					}
					else
					{
						this.GenerateKeyword(TSqlTokenType.Comma);
						this.NewLine();
						this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
						this.GenerateIdentifier(declaration);
					}
					this.GenerateSpaceAndFragmentIfNotNull(t);
				}
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0007EAD0 File Offset: 0x0007CCD0
		public override void ExplicitVisit(FileStreamOnTableOption node)
		{
			this.GenerateIdentifier("FILESTREAM_ON");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0007EAF4 File Offset: 0x0007CCF4
		public override void ExplicitVisit(FullTextStopListAction node)
		{
			if (node.IsAdd)
			{
				this.GenerateKeyword(TSqlTokenType.Add);
			}
			else
			{
				this.GenerateKeyword(TSqlTokenType.Drop);
			}
			if (!node.IsAll)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.StopWord);
			}
			else
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
			}
			if (node.LanguageTerm != null)
			{
				this.GenerateSpaceAndIdentifier("LANGUAGE");
				this.GenerateSpaceAndFragmentIfNotNull(node.LanguageTerm);
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0007EB56 File Offset: 0x0007CD56
		public override void ExplicitVisit(LockEscalationTableOption node)
		{
			this.GenerateIdentifier("LOCK_ESCALATION");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			LockEscalationMethodHelper.Instance.GenerateSourceForOption(this._writer, node.Value);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0007EB8A File Offset: 0x0007CD8A
		public override void ExplicitVisit(PartitionSpecifier node)
		{
			this.GenerateIdentifier("PARTITION");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.All)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			}
			this.GenerateFragmentIfNotNull(node.Number);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0007EBC4 File Offset: 0x0007CDC4
		public override void ExplicitVisit(ChangeRetentionChangeTrackingOptionDetail node)
		{
			this.GenerateIdentifier("CHANGE_RETENTION");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			this.GenerateFragmentIfNotNull(node.RetentionPeriod);
			this.GenerateSpace();
			RetentionUnitHelper.Instance.GenerateSourceForOption(this._writer, node.Unit);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0007EC18 File Offset: 0x0007CE18
		public override void ExplicitVisit(ChangeTrackingDatabaseOption node)
		{
			this.GenerateIdentifier("CHANGE_TRACKING");
			this.GenerateSpace();
			switch (node.OptionState)
			{
			case OptionState.On:
				this.GenerateSymbolAndSpace(TSqlTokenType.EqualsSign);
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateParenthesisedCommaSeparatedList<ChangeTrackingOptionDetail>(node.Details);
				return;
			case OptionState.Off:
				this.GenerateSymbolAndSpace(TSqlTokenType.EqualsSign);
				this.GenerateKeyword(TSqlTokenType.Off);
				return;
			}
			this.GenerateParenthesisedCommaSeparatedList<ChangeTrackingOptionDetail>(node.Details);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0007EC91 File Offset: 0x0007CE91
		public override void ExplicitVisit(AlterWorkloadGroupStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateWorkloadGroupStatementBody(node);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0007ECA1 File Offset: 0x0007CEA1
		public override void ExplicitVisit(CreateBrokerPriorityStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateBrokerPriorityStatementBody(node);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0007ECB2 File Offset: 0x0007CEB2
		public override void ExplicitVisit(CreateWorkloadGroupStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateWorkloadGroupStatementBody(node);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0007ECC3 File Offset: 0x0007CEC3
		public override void ExplicitVisit(DropBrokerPriorityStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("BROKER");
			this.GenerateSpaceAndIdentifier("PRIORITY");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0007ECEF File Offset: 0x0007CEEF
		public override void ExplicitVisit(DropWorkloadGroupStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("WORKLOAD");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0007ED18 File Offset: 0x0007CF18
		public override void ExplicitVisit(ResourcePoolParameter node)
		{
			ResourcePoolParameterHelper.Instance.GenerateSourceForOption(this._writer, node.ParameterType);
			if (node.ParameterType != ResourcePoolParameterType.Affinity)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.ParameterValue);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.AffinitySpecification);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0007ED6C File Offset: 0x0007CF6C
		public override void ExplicitVisit(ResourcePoolAffinitySpecification node)
		{
			ResourcePoolAffinityHelper.Instance.GenerateSourceForOption(this._writer, node.AffinityType);
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.IsAuto)
			{
				this.GenerateIdentifier("AUTO");
				return;
			}
			if (node.PoolAffinityRanges != null && node.PoolAffinityRanges.Count > 0)
			{
				this.GenerateParenthesisedCommaSeparatedList<LiteralRange>(node.PoolAffinityRanges);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0007EDD6 File Offset: 0x0007CFD6
		public override void ExplicitVisit(LiteralRange node)
		{
			this.GenerateFragmentIfNotNull(node.From);
			if (node.To != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.To);
				this.GenerateSpaceAndFragmentIfNotNull(node.To);
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0007EE04 File Offset: 0x0007D004
		protected void GenerateResourcePoolStatementBody(ResourcePoolStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateIdentifier("RESOURCE");
			this.GenerateSpaceAndIdentifier("POOL");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.ResourcePoolParameters != null && node.ResourcePoolParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<ResourcePoolParameter>(node.ResourcePoolParameters, 2);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0007EE80 File Offset: 0x0007D080
		public override void ExplicitVisit(CreateResourcePoolStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateResourcePoolStatementBody(node);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0007EE91 File Offset: 0x0007D091
		public override void ExplicitVisit(AlterResourcePoolStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateResourcePoolStatementBody(node);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0007EEA1 File Offset: 0x0007D0A1
		public override void ExplicitVisit(DropResourcePoolStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("RESOURCE");
			this.GenerateSpaceAndIdentifier("POOL");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0007EECD File Offset: 0x0007D0CD
		public override void ExplicitVisit(AlterDatabaseEncryptionKeyStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateDatabaseEncryptionKeyHeader();
			if (node.Regenerate)
			{
				this.GenerateSpaceAndIdentifier("REGENERATE");
			}
			this.GenerateSpace();
			this.GenerateDatabaseEncryptionKeyStatementBody(node);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0007EEFC File Offset: 0x0007D0FC
		public override void ExplicitVisit(CreateDatabaseEncryptionKeyStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateDatabaseEncryptionKeyHeader();
			this.GenerateSpace();
			this.GenerateDatabaseEncryptionKeyStatementBody(node);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0007EF19 File Offset: 0x0007D119
		public override void ExplicitVisit(DropDatabaseEncryptionKeyStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateDatabaseEncryptionKeyHeader();
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0007EF29 File Offset: 0x0007D129
		private void GenerateDatabaseEncryptionKeyHeader()
		{
			this.GenerateKeyword(TSqlTokenType.Database);
			this.GenerateSpaceAndIdentifier("ENCRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0007EF48 File Offset: 0x0007D148
		protected void GenerateDatabaseEncryptionKeyStatementBody(DatabaseEncryptionKeyStatement node)
		{
			if (node.Algorithm != DatabaseEncryptionKeyAlgorithm.None)
			{
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("ALGORITHM");
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
				DatabaseEncryptionKeyAlgorithmHelper.Instance.GenerateSourceForOption(this._writer, node.Algorithm);
			}
			if (node.Encryptor != null)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("ENCRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpaceAndIdentifier("SERVER");
				this.GenerateSpace();
				this.GenerateFragmentIfNotNull(node.Encryptor);
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0007EFD8 File Offset: 0x0007D1D8
		public override void ExplicitVisit(AlterDatabaseRemoveFileGroupStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateIdentifier("REMOVE");
			this.GenerateSpaceAndIdentifier("FILEGROUP");
			this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0007F009 File Offset: 0x0007D209
		public override void ExplicitVisit(AlterDatabaseAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpace();
			this.GenerateAuditSpecificationStatement(node);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0007F027 File Offset: 0x0007D227
		public override void ExplicitVisit(CreateDatabaseAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpace();
			this.GenerateAuditSpecificationStatement(node);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0007F046 File Offset: 0x0007D246
		public override void ExplicitVisit(DropDatabaseAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpaceAndIdentifier("AUDIT");
			this.GenerateSpaceAndIdentifier("SPECIFICATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0007F07A File Offset: 0x0007D27A
		public override void ExplicitVisit(AlterServerAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("SERVER");
			this.GenerateSpace();
			this.GenerateAuditSpecificationStatement(node);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0007F09B File Offset: 0x0007D29B
		public override void ExplicitVisit(CreateServerAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("SERVER");
			this.GenerateSpace();
			this.GenerateAuditSpecificationStatement(node);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0007F0BD File Offset: 0x0007D2BD
		public override void ExplicitVisit(DropServerAuditSpecificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("SERVER");
			this.GenerateSpaceAndIdentifier("AUDIT");
			this.GenerateSpaceAndIdentifier("SPECIFICATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0007F0F4 File Offset: 0x0007D2F4
		public override void ExplicitVisit(AuditActionGroupReference node)
		{
			ServerAuditActionGroupHelper.Instance.GenerateSourceForOption(this._writer, node.Group);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0007F10C File Offset: 0x0007D30C
		public override void ExplicitVisit(AuditActionSpecification node)
		{
			this.GenerateCommaSeparatedList<DatabaseAuditAction>(node.Actions);
			this.GenerateSpaceAndFragmentIfNotNull(node.TargetObject);
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SecurityPrincipal>(node.Principals);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0007F140 File Offset: 0x0007D340
		public override void ExplicitVisit(AuditSpecificationPart node)
		{
			this.GenerateKeywordAndSpace(node.IsDrop ? TSqlTokenType.Drop : TSqlTokenType.Add);
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Details);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0007F18C File Offset: 0x0007D38C
		protected void GenerateAuditSpecificationStatement(AuditSpecificationStatement node)
		{
			this.GenerateIdentifier("AUDIT");
			this.GenerateSpaceAndIdentifier("SPECIFICATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.SpecificationName);
			if (node.AuditName != null || node is CreateServerAuditSpecificationStatement || node is CreateDatabaseAuditSpecificationStatement)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("SERVER");
				this.GenerateSpaceAndIdentifier("AUDIT");
				this.GenerateSpaceAndFragmentIfNotNull(node.AuditName);
			}
			if (node.Parts.Count > 0)
			{
				this.NewLineAndIndent();
			}
			this.GenerateList<AuditSpecificationPart>(node.Parts, delegate
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.NewLineAndIndent();
			});
			if (node.AuditState != OptionState.NotSet)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateOptionStateWithEqualSign("STATE", node.AuditState);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0007F270 File Offset: 0x0007D470
		public override void ExplicitVisit(CreateTypeTableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.NewLineAndIndent();
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateCommaSeparatedList<ColumnDefinition>(node.Definition.ColumnDefinitions, true);
			if (node.Definition.ColumnDefinitions != null && node.Definition.ColumnDefinitions.Count > 0 && node.Definition.TableConstraints != null && node.Definition.TableConstraints.Count > 0)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.NewLine();
			}
			this.GenerateCommaSeparatedList<ConstraintDefinition>(node.Definition.TableConstraints, true);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0007F354 File Offset: 0x0007D554
		public override void ExplicitVisit(DatabaseAuditAction node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DatabaseAuditActionKind, TokenGenerator>(SqlScriptGeneratorVisitor._databaseAuditActionName, node.ActionKind);
			this.GenerateToken(valueForEnumKey);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0007F379 File Offset: 0x0007D579
		public override void ExplicitVisit(EventGroupContainer node)
		{
			if (!AuditEventGroupHelper.Instance.TryGenerateSourceForOption(this._writer, node.EventGroup))
			{
				TriggerEventGroupHelper.Instance.GenerateSourceForOption(this._writer, node.EventGroup);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0007F3A9 File Offset: 0x0007D5A9
		public override void ExplicitVisit(EventTypeContainer node)
		{
			if (!AuditEventTypeHelper.Instance.TryGenerateSourceForOption(this._writer, node.EventType))
			{
				TriggerEventTypeHelper.Instance.GenerateSourceForOption(this._writer, node.EventType);
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0007F3D9 File Offset: 0x0007D5D9
		public override void ExplicitVisit(QueueDelayAuditOption node)
		{
			this.GenerateNameEqualsValue("QUEUE_DELAY", node.Delay);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0007F3EC File Offset: 0x0007D5EC
		public override void ExplicitVisit(AuditGuidAuditOption node)
		{
			this.GenerateNameEqualsValue("AUDIT_GUID", node.Guid);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0007F400 File Offset: 0x0007D600
		public override void ExplicitVisit(OnFailureAuditOption node)
		{
			this.GenerateTokenAndEqualSign("ON_FAILURE");
			switch (node.OnFailureAction)
			{
			case AuditFailureActionType.Continue:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Continue);
				return;
			case AuditFailureActionType.Shutdown:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Shutdown);
				return;
			case AuditFailureActionType.FailOperation:
				this.GenerateSpaceAndIdentifier("FAIL_OPERATION");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0007F452 File Offset: 0x0007D652
		public override void ExplicitVisit(StateAuditOption node)
		{
			this.GenerateOptionStateWithEqualSign("STATE", node.Value);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0007F468 File Offset: 0x0007D668
		public override void ExplicitVisit(CreateServerAuditStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateServerAuditName(node.AuditName);
			this.GenerateSpaceAndFragmentIfNotNull(node.AuditTarget);
			this.GenerateServerAuditOptions(node);
			if (node.PredicateExpression != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.Where);
				this.GenerateFragmentIfNotNull(node.PredicateExpression);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0007F4C4 File Offset: 0x0007D6C4
		public override void ExplicitVisit(AlterServerAuditStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateServerAuditName(node.AuditName);
			if (node.NewName != null)
			{
				this.GenerateSpaceAndIdentifier("MODIFY");
				this.GenerateSpace();
				this.GenerateNameEqualsValue("NAME", node.NewName);
				return;
			}
			if (node.RemoveWhere)
			{
				this.GenerateSpaceAndIdentifier("REMOVE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Where);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.AuditTarget);
			this.GenerateServerAuditOptions(node);
			if (node.PredicateExpression != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.Where);
				this.GenerateFragmentIfNotNull(node.PredicateExpression);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0007F566 File Offset: 0x0007D766
		public override void ExplicitVisit(DropServerAuditStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateServerAuditName(node.Name);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0007F57C File Offset: 0x0007D77C
		private void GenerateServerAuditName(Identifier name)
		{
			this.GenerateIdentifier("SERVER");
			this.GenerateSpaceAndIdentifier("AUDIT");
			this.GenerateSpaceAndFragmentIfNotNull(name);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0007F59B File Offset: 0x0007D79B
		private void GenerateServerAuditOptions(ServerAuditStatement node)
		{
			if (node.Options.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<AuditOption>(node.Options);
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0007F5C8 File Offset: 0x0007D7C8
		public override void ExplicitVisit(MergeActionClause node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.When);
			switch (node.Condition)
			{
			case MergeCondition.Matched:
				this.GenerateIdentifier("MATCHED");
				break;
			case MergeCondition.NotMatched:
				this.GenerateKeyword(TSqlTokenType.Not);
				this.GenerateSpaceAndIdentifier("MATCHED");
				break;
			case MergeCondition.NotMatchedByTarget:
				this.GenerateKeyword(TSqlTokenType.Not);
				this.GenerateSpaceAndIdentifier("MATCHED");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpaceAndIdentifier("TARGET");
				break;
			case MergeCondition.NotMatchedBySource:
				this.GenerateKeyword(TSqlTokenType.Not);
				this.GenerateSpaceAndIdentifier("MATCHED");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpaceAndIdentifier("SOURCE");
				break;
			}
			if (node.SearchCondition != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.And);
				this.GenerateSpaceAndFragmentIfNotNull(node.SearchCondition);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Then);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0007F6A8 File Offset: 0x0007D8A8
		public override void ExplicitVisit(MergeStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (node.WithCtesAndXmlNamespaces != null)
			{
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WithCtesAndXmlNamespaces, alignmentPoint2);
				this.NewLine();
			}
			this.GenerateFragmentIfNotNull(node.MergeSpecification);
			this.GenerateOptimizerHints(node.OptimizerHints);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0007F710 File Offset: 0x0007D910
		public override void ExplicitVisit(MergeSpecification node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateKeyword(TSqlTokenType.Merge);
			this.MarkClauseBodyAlignmentWhenNecessary(false, alignmentPoint);
			if (node.TopRowFilter != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.TopRowFilter);
			}
			this.GenerateSpace();
			this.GenerateKeyword(TSqlTokenType.Into);
			if (node.Target != null)
			{
				this.GenerateSpace();
				this.GenerateFragmentIfNotNull(node.Target);
				this.NewLine();
			}
			if (node.TableAlias != null)
			{
				this.GenerateSpace();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpace();
				this.GenerateFragmentIfNotNull(node.TableAlias);
			}
			this.NewLine();
			this.GenerateIdentifier("USING");
			this.GenerateSpace();
			this.GenerateFragmentIfNotNull(node.TableReference);
			this.GenerateSpace();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpace();
			this.GenerateFragmentIfNotNull(node.SearchCondition);
			this.NewLine();
			if (node.ActionClauses != null)
			{
				this.GenerateList<MergeActionClause>(node.ActionClauses, delegate
				{
					this.NewLine();
				});
			}
			if (node.OutputIntoClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputIntoClause, alignmentPoint);
			}
			if (node.OutputClause != null)
			{
				this.AddAlignmentPointForFragment(node.OutputClause, alignmentPoint);
				this.GenerateSpace();
				this.GenerateFragmentIfNotNull(node.OutputClause);
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0007F850 File Offset: 0x0007DA50
		public override void ExplicitVisit(UpdateMergeAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Update);
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateSetClauses(node.SetClauses, alignmentPoint);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0007F880 File Offset: 0x0007DA80
		public override void ExplicitVisit(DeleteMergeAction node)
		{
			this.GenerateKeyword(TSqlTokenType.Delete);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0007F88C File Offset: 0x0007DA8C
		public override void ExplicitVisit(InsertMergeAction node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("InsertColumns");
			this.GenerateKeyword(TSqlTokenType.Insert);
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.AddAlignmentPointForFragment(node.Source, alignmentPoint2);
			if (node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
			}
			if (node.Source != null)
			{
				this.GenerateSpace();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.Source, alignmentPoint);
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0007F900 File Offset: 0x0007DB00
		public override void ExplicitVisit(CreateTableStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpaceAndFragmentIfNotNull(node.SchemaObjectName);
			if (node.Definition != null)
			{
				List<TSqlFragment> list = new List<TSqlFragment>();
				foreach (ColumnDefinition columnDefinition in node.Definition.ColumnDefinitions)
				{
					list.Add(columnDefinition);
				}
				foreach (ConstraintDefinition constraintDefinition in node.Definition.TableConstraints)
				{
					list.Add(constraintDefinition);
				}
				SqlScriptGeneratorVisitor.ListGenerationOption listGenerationOption = SqlScriptGeneratorVisitor.ListGenerationOption.CreateOptionFromFormattingConfig(this._options);
				this.GenerateFragmentList<TSqlFragment>(list, listGenerationOption);
			}
			if (node.AsFileTable)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndIdentifier("FILETABLE");
			}
			this.PopAlignmentPoint();
			if (node.FederationScheme != null)
			{
				this.GenerateSpaceAndIdentifier("FEDERATED");
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentIfNotNull(node.FederationScheme.DistributionName);
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.FederationScheme.ColumnName);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
			if (node.OnFileGroupOrPartitionScheme != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroupOrPartitionScheme);
			}
			if (node.TextImageOn != null)
			{
				this.GenerateSpaceAndIdentifier("TEXTIMAGE_ON");
				this.GenerateSpaceAndFragmentIfNotNull(node.TextImageOn);
			}
			this.GenerateFileStreamOn(node);
			this.GenerateCommaSeparatedWithClause<TableOption>(node.Options, false, true);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0007FAC0 File Offset: 0x0007DCC0
		public override void ExplicitVisit(BinaryExpression node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			this.GenerateSpace();
			this.GenerateBinaryOperator(node.BinaryExpressionType);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0007FB0C File Offset: 0x0007DD0C
		public override void ExplicitVisit(CommonTableExpression node)
		{
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
			this.GenerateSpaceAndFragmentIfNotNull(node.ExpressionName);
			if (node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			}
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.As);
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
			this.GenerateSpace();
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateQueryExpressionInParentheses(node.QueryExpression);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0007FB98 File Offset: 0x0007DD98
		public override void ExplicitVisit(DeleteStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (node.WithCtesAndXmlNamespaces != null)
			{
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WithCtesAndXmlNamespaces, alignmentPoint2);
				this.NewLine();
			}
			this.GenerateFragmentIfNotNull(node.DeleteSpecification);
			this.GenerateOptimizerHints(node.OptimizerHints);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0007FBF8 File Offset: 0x0007DDF8
		public override void ExplicitVisit(DeleteSpecification node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateKeyword(TSqlTokenType.Delete);
			if (node.TopRowFilter != null)
			{
				this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPoint);
				this.GenerateSpaceAndFragmentIfNotNull(node.TopRowFilter);
				this.NewLine();
			}
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPoint);
			this.GenerateSpaceAndFragmentIfNotNull(node.Target);
			if (node.OutputIntoClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputIntoClause, alignmentPoint);
			}
			if (node.OutputClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputClause, alignmentPoint);
			}
			this.GenerateFromClause(node.FromClause, alignmentPoint);
			if (node.WhereClause != null)
			{
				this.GenerateSeparatorForWhereClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WhereClause, alignmentPoint);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0007FCAC File Offset: 0x0007DEAC
		public override void ExplicitVisit(QueryDerivedTable node)
		{
			this.GenerateQueryExpressionInParentheses(node.QueryExpression);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0007FCC1 File Offset: 0x0007DEC1
		public override void ExplicitVisit(InlineDerivedTable node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateSymbolAndSpace(TSqlTokenType.Values);
			this.GenerateCommaSeparatedList<RowValue>(node.RowValues);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0007FCF8 File Offset: 0x0007DEF8
		public override void ExplicitVisit(ExpressionWithSortOrder node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SortOrder, TokenGenerator>(SqlScriptGeneratorVisitor._sortOrderGenerators, node.SortOrder);
			if (valueForEnumKey != null && node.SortOrder != SortOrder.NotSpecified)
			{
				this.GenerateSpace();
				this.GenerateToken(valueForEnumKey);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0007FD3A File Offset: 0x0007DF3A
		public override void ExplicitVisit(FromClause node)
		{
			this.GenerateFromClause(node, null);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0007FD44 File Offset: 0x0007DF44
		protected void GenerateFromClause(FromClause fromClause, AlignmentPoint clauseBody)
		{
			if (fromClause != null)
			{
				IList<TableReference> tableReferences = fromClause.TableReferences;
				if (tableReferences.Count > 0)
				{
					this.GenerateSeparatorForFromClause();
					AlignmentPoint alignmentPoint = new AlignmentPoint();
					this.MarkAndPushAlignmentPoint(alignmentPoint);
					this.GenerateKeyword(TSqlTokenType.From);
					this.MarkClauseBodyAlignmentWhenNecessary(this._options.NewLineBeforeFromClause, clauseBody);
					this.GenerateSpace();
					AlignmentPoint alignmentPoint2 = new AlignmentPoint();
					this.MarkAndPushAlignmentPoint(alignmentPoint2);
					this.GenerateCommaSeparatedList<TableReference>(tableReferences);
					this.PopAlignmentPoint();
					this.PopAlignmentPoint();
				}
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0007FDB7 File Offset: 0x0007DFB7
		public override void ExplicitVisit(LeftFunctionCall node)
		{
			this.GenerateKeyword(TSqlTokenType.Left);
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0007FDDA File Offset: 0x0007DFDA
		public override void ExplicitVisit(RightFunctionCall node)
		{
			this.GenerateKeyword(TSqlTokenType.Right);
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0007FE00 File Offset: 0x0007E000
		public override void ExplicitVisit(FunctionCall node)
		{
			this.GenerateFragmentIfNotNull(node.CallTarget);
			this.GenerateFragmentIfNotNull(node.FunctionName);
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateUniqueRowFilter(node.UniqueRowFilter, false);
			if (node.UniqueRowFilter != UniqueRowFilter.NotSpecified && node.Parameters.Count > 0)
			{
				this.GenerateSpace();
			}
			this.GenerateCommaSeparatedList<ScalarExpression>(node.Parameters);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndFragmentIfNotNull(node.WithinGroupClause);
			this.GenerateSpaceAndFragmentIfNotNull(node.OverClause);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0007FE94 File Offset: 0x0007E094
		public override void ExplicitVisit(GroupByClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
			}
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(this._options.NewLineBeforeGroupByClause, alignmentPointForFragment);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<GroupingSpecification>(node.GroupingSpecifications);
			if (node.GroupByOption != GroupByOption.None)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				GroupByOptionHelper.Instance.GenerateSourceForOption(this._writer, node.GroupByOption);
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0007FF33 File Offset: 0x0007E133
		public override void ExplicitVisit(ExpressionGroupingSpecification node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0007FF41 File Offset: 0x0007E141
		public override void ExplicitVisit(CompositeGroupingSpecification node)
		{
			this.GenerateParenthesisedCommaSeparatedList<GroupingSpecification>(node.Items);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0007FF4F File Offset: 0x0007E14F
		public override void ExplicitVisit(CubeGroupingSpecification node)
		{
			this.GenerateIdentifier("CUBE");
			this.GenerateParenthesisedCommaSeparatedList<GroupingSpecification>(node.Arguments);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0007FF68 File Offset: 0x0007E168
		public override void ExplicitVisit(RollupGroupingSpecification node)
		{
			this.GenerateIdentifier("ROLLUP");
			this.GenerateParenthesisedCommaSeparatedList<GroupingSpecification>(node.Arguments);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0007FF81 File Offset: 0x0007E181
		public override void ExplicitVisit(GrandTotalGroupingSpecification node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0007FF99 File Offset: 0x0007E199
		public override void ExplicitVisit(GroupingSetsGroupingSpecification node)
		{
			this.GenerateIdentifier("GROUPING");
			this.GenerateSpaceAndIdentifier("SETS");
			this.GenerateParenthesisedCommaSeparatedList<GroupingSpecification>(node.Sets);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0007FFC0 File Offset: 0x0007E1C0
		public override void ExplicitVisit(HavingClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Having);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(this._options.NewLineBeforeHavingClause, alignmentPointForFragment);
			this.GenerateSpaceAndFragmentIfNotNull(node.SearchCondition);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00080013 File Offset: 0x0007E213
		public override void ExplicitVisit(Identifier node)
		{
			if (node.QuoteType == QuoteType.NotQuoted)
			{
				this.GenerateIdentifierWithoutCheck(node.Value);
				return;
			}
			this.GenerateQuotedIdentifier(node.Value, node.QuoteType);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0008003C File Offset: 0x0007E23C
		private void GenerateQuotedIdentifier(string identifier, QuoteType quoteType)
		{
			this.GenerateIdentifierWithoutCheck(Identifier.EncodeIdentifier(identifier, quoteType));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0008004B File Offset: 0x0007E24B
		public override void ExplicitVisit(IndexExpressionOption node)
		{
			IndexOptionHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0008007A File Offset: 0x0007E27A
		public override void ExplicitVisit(IndexStateOption node)
		{
			IndexOptionHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			this.GenerateOptionStateOnOff(node.OptionState);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000800B0 File Offset: 0x0007E2B0
		public override void ExplicitVisit(InPredicate node)
		{
			this.GenerateFragmentIfNotNull(node.Expression);
			if (node.NotDefined)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Not);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.In);
			if (node.Values.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Values);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Subquery);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00080110 File Offset: 0x0007E310
		public override void ExplicitVisit(InsertStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (node.WithCtesAndXmlNamespaces != null)
			{
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WithCtesAndXmlNamespaces, alignmentPoint2);
				this.NewLine();
			}
			this.GenerateFragmentIfNotNull(node.InsertSpecification);
			this.GenerateOptimizerHints(node.OptimizerHints);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00080170 File Offset: 0x0007E370
		public override void ExplicitVisit(InsertSpecification node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("InsertColumns");
			this.GenerateKeyword(TSqlTokenType.Insert);
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPoint);
			if (node.TopRowFilter != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.TopRowFilter);
			}
			this.GenerateSpaceAndInsertOption(node.InsertOption);
			if (node.Target != null)
			{
				this.GenerateSpace();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.Target, alignmentPoint2);
				if (node.Columns.Count > 0)
				{
					this.MarkInsertColumnsAlignmentPointWhenNecessary(alignmentPoint2);
					this.GenerateSpace();
					this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
				}
			}
			if (node.OutputIntoClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputIntoClause, alignmentPoint);
			}
			if (node.OutputClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputClause, alignmentPoint);
			}
			this.NewLine();
			if (node.InsertSource != null)
			{
				this.AddAlignmentPointForFragment(node.InsertSource, alignmentPoint);
				this.AddAlignmentPointForFragment(node.InsertSource, alignmentPoint2);
				bool generateSemiColon = this._generateSemiColon;
				this._generateSemiColon = false;
				this.GenerateFragmentIfNotNull(node.InsertSource);
				this._generateSemiColon = generateSemiColon;
				this.ClearAlignmentPointsForFragment(node.InsertSource);
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00080290 File Offset: 0x0007E490
		private void GenerateSpaceAndInsertOption(InsertOption insertOption)
		{
			if (insertOption != InsertOption.None)
			{
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<InsertOption, TokenGenerator>(SqlScriptGeneratorVisitor._insertOptionGenerators, insertOption);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateToken(valueForEnumKey);
				}
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000802BC File Offset: 0x0007E4BC
		public override void ExplicitVisit(ScalarExpressionSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000802D2 File Offset: 0x0007E4D2
		public override void ExplicitVisit(BooleanExpressionSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000802E8 File Offset: 0x0007E4E8
		public override void ExplicitVisit(StatementListSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000802FE File Offset: 0x0007E4FE
		public override void ExplicitVisit(TSqlFragmentSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00080314 File Offset: 0x0007E514
		public override void ExplicitVisit(TSqlStatementSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0008032C File Offset: 0x0007E52C
		public override void ExplicitVisit(LikePredicate node)
		{
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			if (node.NotDefined)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Not);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Like);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			if (node.EscapeExpression != null)
			{
				this.GenerateSpace();
				if (node.OdbcEscape)
				{
					this.GenerateSymbol(TSqlTokenType.LeftCurly);
				}
				this.GenerateKeyword(TSqlTokenType.Escape);
				this.GenerateSpaceAndFragmentIfNotNull(node.EscapeExpression);
				if (node.OdbcEscape)
				{
					this.GenerateSpaceAndSymbol(TSqlTokenType.RightCurly);
				}
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000803B1 File Offset: 0x0007E5B1
		public override void ExplicitVisit(IntegerLiteral node)
		{
			this.GenerateToken(TSqlTokenType.Integer, node.Value);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000803D0 File Offset: 0x0007E5D0
		public override void ExplicitVisit(NumericLiteral node)
		{
			this.GenerateToken(TSqlTokenType.Numeric, node.Value);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000803F0 File Offset: 0x0007E5F0
		public override void ExplicitVisit(StringLiteral node)
		{
			if (!node.IsNational)
			{
				string text = "'" + node.Value.Replace("'", "''") + "'";
				this.GenerateToken(TSqlTokenType.AsciiStringLiteral, text);
			}
			else
			{
				string text2 = "N'" + node.Value.Replace("'", "''") + "'";
				this.GenerateToken(TSqlTokenType.UnicodeStringLiteral, text2);
			}
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00080475 File Offset: 0x0007E675
		public override void ExplicitVisit(BinaryLiteral node)
		{
			this.GenerateToken(TSqlTokenType.HexLiteral, node.Value);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00080494 File Offset: 0x0007E694
		public override void ExplicitVisit(DefaultLiteral node)
		{
			this.GenerateKeyword(TSqlTokenType.Default);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000804AA File Offset: 0x0007E6AA
		public override void ExplicitVisit(MaxLiteral node)
		{
			this.GenerateIdentifier("MAX");
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000804C3 File Offset: 0x0007E6C3
		public override void ExplicitVisit(MoneyLiteral node)
		{
			this.GenerateToken(TSqlTokenType.Money, node.Value);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000804E2 File Offset: 0x0007E6E2
		public override void ExplicitVisit(NullLiteral node)
		{
			this.GenerateKeyword(TSqlTokenType.Null);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000804F8 File Offset: 0x0007E6F8
		public override void ExplicitVisit(RealLiteral node)
		{
			this.GenerateToken(TSqlTokenType.Real, node.Value);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00080517 File Offset: 0x0007E717
		public override void ExplicitVisit(IdentifierLiteral node)
		{
			if (node.QuoteType == QuoteType.NotQuoted)
			{
				this.GenerateIdentifierWithoutCheck(node.Value);
			}
			else
			{
				this.GenerateQuotedIdentifier(node.Value, node.QuoteType);
			}
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0008054D File Offset: 0x0007E74D
		public override void ExplicitVisit(VariableReference node)
		{
			this.GenerateToken(TSqlTokenType.Variable, node.Name);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0008056C File Offset: 0x0007E76C
		public override void ExplicitVisit(GlobalVariableExpression node)
		{
			this.GenerateToken(TSqlTokenType.Variable, node.Name);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0008058C File Offset: 0x0007E78C
		public override void ExplicitVisit(OdbcLiteral node)
		{
			this.GenerateOdbcLiteralPrefix(node);
			if (!node.IsNational)
			{
				string text = "'" + node.Value.Replace("'", "''") + "'";
				this.GenerateToken(TSqlTokenType.AsciiStringLiteral, text);
			}
			else
			{
				string text2 = "N'" + node.Value.Replace("'", "''") + "'";
				this.GenerateToken(TSqlTokenType.UnicodeStringLiteral, text2);
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.RightCurly);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00080624 File Offset: 0x0007E824
		private void GenerateOdbcLiteralPrefix(OdbcLiteral node)
		{
			this.GenerateSymbolAndSpace(TSqlTokenType.LeftCurly);
			switch (node.OdbcLiteralType)
			{
			case OdbcLiteralType.Time:
				this.GenerateIdentifier("T");
				break;
			case OdbcLiteralType.Date:
				this.GenerateIdentifier("D");
				break;
			case OdbcLiteralType.Timestamp:
				this.GenerateIdentifier("TS");
				break;
			case OdbcLiteralType.Guid:
				this.GenerateIdentifier("GUID");
				break;
			}
			this.GenerateSpace();
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00080694 File Offset: 0x0007E894
		public override void ExplicitVisit(OrderByClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Order);
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(this._options.NewLineBeforeOrderByClause, alignmentPointForFragment);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<ExpressionWithSortOrder>(node.OrderByElements);
			this.PopAlignmentPoint();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000806F8 File Offset: 0x0007E8F8
		public override void ExplicitVisit(ParameterlessCall node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<ParameterlessCallType, TokenGenerator>(SqlScriptGeneratorVisitor._parameterlessCallTypeGenerators, node.ParameterlessCallType);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0008072C File Offset: 0x0007E92C
		public override void ExplicitVisit(ParenthesisExpression node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Expression);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0008075C File Offset: 0x0007E95C
		public override void ExplicitVisit(QualifiedJoin node)
		{
			this.GenerateFragmentIfNotNull(node.FirstTableReference);
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeJoinClause);
			this.GenerateQualifiedJoinType(node.QualifiedJoinType);
			if (node.JoinHint != JoinHint.None)
			{
				this.GenerateSpace();
				JoinHintHelper.Instance.GenerateSourceForOption(this._writer, node.JoinHint);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Join);
			this.NewLine();
			this.GenerateFragmentIfNotNull(node.SecondTableReference);
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.SearchCondition);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000807EC File Offset: 0x0007E9EC
		private void GenerateQualifiedJoinType(QualifiedJoinType qualifiedJoinType)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<QualifiedJoinType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._qualifiedJoinTypeGenerators, qualifiedJoinType);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00080810 File Offset: 0x0007EA10
		public override void ExplicitVisit(QuerySpecification node)
		{
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.GenerateQuerySpecification(node, alignmentPointForFragment, null);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00080834 File Offset: 0x0007EA34
		protected void GenerateQuerySpecification(QuerySpecification node, AlignmentPoint clauseBody, SchemaObjectName intoClause)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Select);
			this.MarkClauseBodyAlignmentWhenNecessary(true, clauseBody);
			this.GenerateUniqueRowFilter(node.UniqueRowFilter, true);
			this.GenerateSpaceAndFragmentIfNotNull(node.TopRowFilter);
			this.GenerateSpace();
			this.GenerateSelectElementsList(node.SelectElements);
			if (intoClause != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.Into);
				this.MarkClauseBodyAlignmentWhenNecessary(true, clauseBody);
				this.GenerateSpaceAndFragmentIfNotNull(intoClause);
			}
			this.GenerateFromClause(node.FromClause, clauseBody);
			if (node.WhereClause != null)
			{
				this.GenerateSeparatorForWhereClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WhereClause, clauseBody);
			}
			if (node.GroupByClause != null)
			{
				this.GenerateSeparatorForGroupByClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.GroupByClause, clauseBody);
			}
			if (node.HavingClause != null)
			{
				this.GenerateSeparatorForHavingClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.HavingClause, clauseBody);
			}
			if (node.OrderByClause != null)
			{
				this.GenerateSeparatorForOrderBy();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OrderByClause, clauseBody);
			}
			if (node.OffsetClause != null)
			{
				this.GenerateSeparatorForOffsetClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OffsetClause, clauseBody);
			}
			if (node.ForClause != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.For);
				this.MarkClauseBodyAlignmentWhenNecessary(true, clauseBody);
				this.GenerateSpace();
				AlignmentPoint alignmentPoint2 = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint2);
				this.GenerateFragmentIfNotNull(node.ForClause);
				this.PopAlignmentPoint();
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00080989 File Offset: 0x0007EB89
		public override void ExplicitVisit(SchemaObjectName node)
		{
			this.GenerateDotSeparatedList<Identifier>(node.Identifiers);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00080998 File Offset: 0x0007EB98
		public override void ExplicitVisit(SelectSetVariable node)
		{
			this.GenerateFragmentIfNotNull(node.Variable);
			TSqlTokenType valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AssignmentKind, TSqlTokenType>(SqlScriptGeneratorVisitor._assignmentKindSymbols, node.AssignmentKind);
			this.GenerateSpaceAndSymbol(valueForEnumKey);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000809D8 File Offset: 0x0007EBD8
		public override void ExplicitVisit(SelectStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (node.WithCtesAndXmlNamespaces != null)
			{
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WithCtesAndXmlNamespaces, alignmentPoint2);
				this.NewLine();
			}
			this.GenerateQueryExpression(node.QueryExpression, alignmentPoint2, node.Into);
			foreach (ComputeClause computeClause in node.ComputeClauses)
			{
				this.NewLine();
				this.GenerateFragmentWithAlignmentPointIfNotNull(computeClause, alignmentPoint2);
			}
			this.GenerateOptimizerHints(node.OptimizerHints);
			this.PopAlignmentPoint();
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00080A84 File Offset: 0x0007EC84
		private void GenerateQueryExpression(QueryExpression queryExpression, AlignmentPoint clauseBody, SchemaObjectName intoClause)
		{
			QuerySpecification querySpecification = queryExpression as QuerySpecification;
			if (querySpecification != null)
			{
				this.GenerateQuerySpecification(querySpecification, clauseBody, intoClause);
				return;
			}
			BinaryQueryExpression binaryQueryExpression = queryExpression as BinaryQueryExpression;
			if (binaryQueryExpression != null)
			{
				this.GenerateBinaryQueryExpression(binaryQueryExpression, clauseBody, intoClause);
				return;
			}
			QueryParenthesisExpression queryParenthesisExpression = queryExpression as QueryParenthesisExpression;
			if (queryParenthesisExpression != null)
			{
				this.GenerateQueryParenthesisExpression(queryParenthesisExpression, clauseBody, intoClause);
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00080ACC File Offset: 0x0007ECCC
		protected void GenerateSetClauses(IList<SetClause> setClauses, AlignmentPoint alignmentPoint)
		{
			this.NewLine();
			if (this._options.IndentSetClause)
			{
				this.Indent();
			}
			this.GenerateKeyword(TSqlTokenType.Set);
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPoint);
			this.GenerateSpace();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint2);
			this.GenerateCommaSeparatedList<SetClause>(setClauses, this._options.MultilineSetClauseItems);
			this.PopAlignmentPoint();
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00080B30 File Offset: 0x0007ED30
		public override void ExplicitVisit(FunctionCallSetClause node)
		{
			this.AlignWhenNecessary("SetClauseItemFirstEqualSign");
			this.GenerateFragmentIfNotNull(node.MutatorFunction);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00080B4C File Offset: 0x0007ED4C
		public override void ExplicitVisit(AssignmentSetClause node)
		{
			if (node.Variable != null)
			{
				this.GenerateFragmentIfNotNull(node.Variable);
				this.AlignWhenNecessary("SetClauseItemFirstEqualSign");
			}
			if (node.Column != null && node.Variable != null)
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
			}
			this.GenerateFragmentIfNotNull(node.Column);
			if (node.Column != null || node.Variable != null)
			{
				this.AlignWhenNecessary("SetClauseItemSecondEqualSign");
				TSqlTokenType valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AssignmentKind, TSqlTokenType>(SqlScriptGeneratorVisitor._assignmentKindSymbols, node.AssignmentKind);
				this.GenerateSpaceAndSymbol(valueForEnumKey);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.NewValue);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00080BE4 File Offset: 0x0007EDE4
		private void AlignWhenNecessary(string apName)
		{
			if (this._options.MultilineSetClauseItems && this._options.AlignSetClauseItem)
			{
				AlignmentPoint alignmentPoint = this.FindOrCreateAlignmentPointByName(apName);
				this.Mark(alignmentPoint);
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00080C1C File Offset: 0x0007EE1C
		public override void ExplicitVisit(SqlDataTypeReference node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SqlDataTypeOption, TokenGenerator>(SqlScriptGeneratorVisitor._sqlDataTypeOptionGenerators, node.SqlDataTypeOption);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			this.GenerateParameters(node);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00080C4C File Offset: 0x0007EE4C
		public override void ExplicitVisit(TSqlBatch node)
		{
			foreach (TSqlStatement tsqlStatement in node.Statements)
			{
				this.GenerateFragmentIfNotNull(tsqlStatement);
				this.GenerateSemiColonWhenNecessary(tsqlStatement);
				if (!(tsqlStatement is TSqlStatementSnippet))
				{
					this.NewLine();
					this.NewLine();
				}
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00080CB4 File Offset: 0x0007EEB4
		public override void ExplicitVisit(TSqlScript node)
		{
			bool flag = true;
			foreach (TSqlBatch tsqlBatch in node.Batches)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.NewLine();
					this.GenerateKeyword(TSqlTokenType.Go);
					this.NewLine();
				}
				this.GenerateFragmentIfNotNull(tsqlBatch);
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00080D24 File Offset: 0x0007EF24
		public override void ExplicitVisit(UnaryExpression node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<UnaryExpressionType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._unaryExpressionTypeGenerators, node.UnaryExpressionType);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
			this.GenerateFragmentIfNotNull(node.Expression);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00080D58 File Offset: 0x0007EF58
		public override void ExplicitVisit(UpdateStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (node.WithCtesAndXmlNamespaces != null)
			{
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WithCtesAndXmlNamespaces, alignmentPoint2);
				this.NewLine();
			}
			this.GenerateFragmentIfNotNull(node.UpdateSpecification);
			this.GenerateOptimizerHints(node.OptimizerHints);
			this.PopAlignmentPoint();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00080DB8 File Offset: 0x0007EFB8
		public override void ExplicitVisit(UpdateSpecification node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateKeyword(TSqlTokenType.Update);
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPoint);
			if (node.TopRowFilter != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.TopRowFilter);
				this.NewLine();
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Target);
			this.GenerateSetClauses(node.SetClauses, alignmentPoint);
			if (node.OutputIntoClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputIntoClause, alignmentPoint);
			}
			if (node.OutputClause != null)
			{
				this.GenerateSeparatorForOutputClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OutputClause, alignmentPoint);
			}
			this.GenerateFromClause(node.FromClause, alignmentPoint);
			if (node.WhereClause != null)
			{
				this.GenerateSeparatorForWhereClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.WhereClause, alignmentPoint);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00080E74 File Offset: 0x0007F074
		public override void ExplicitVisit(UserDataTypeReference node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			this.GenerateParameters(node);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00080E8C File Offset: 0x0007F08C
		public override void ExplicitVisit(ValuesInsertSource node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			if (node.IsDefaultValues)
			{
				this.GenerateKeyword(TSqlTokenType.Default);
				this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Values);
			}
			else
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Values);
				this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
				AlignmentPoint alignmentPointForFragment2 = this.GetAlignmentPointForFragment(node, "InsertColumns");
				this.MarkInsertColumnsAlignmentPointWhenNecessary(alignmentPointForFragment2);
				this.GenerateCommaSeparatedList<RowValue>(node.RowValues, true);
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00080F12 File Offset: 0x0007F112
		public override void ExplicitVisit(RowValue node)
		{
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.ColumnValues);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00080F20 File Offset: 0x0007F120
		public override void ExplicitVisit(WhereClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Where);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(this._options.NewLineBeforeWhereClause, alignmentPointForFragment);
			if (node.SearchCondition != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.SearchCondition);
			}
			else
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Current);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Of);
				this.GenerateSpaceAndFragmentIfNotNull(node.Cursor);
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00080F9C File Offset: 0x0007F19C
		protected void GenerateWorkloadGroupStatementBody(WorkloadGroupStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateIdentifier("WORKLOAD");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.WorkloadGroupParameters != null && node.WorkloadGroupParameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<WorkloadGroupParameter>(node.WorkloadGroupParameters, 2);
				this.PopAlignmentPoint();
			}
			if (node.PoolName != null)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateIdentifier("USING");
				this.GenerateSpaceAndFragmentIfNotNull(node.PoolName);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00081047 File Offset: 0x0007F247
		public override void ExplicitVisit(WorkloadGroupResourceParameter node)
		{
			WorkloadGroupResourceParameterHelper.Instance.GenerateSourceForOption(this._writer, node.ParameterType);
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.ParameterValue);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00081076 File Offset: 0x0007F276
		public override void ExplicitVisit(WorkloadGroupImportanceParameter node)
		{
			this.GenerateIdentifier("IMPORTANCE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			ImportanceParameterHelper.Instance.GenerateSourceForOption(this._writer, node.ParameterValue);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000810AC File Offset: 0x0007F2AC
		public override void ExplicitVisit(XmlDataTypeReference node)
		{
			this.GenerateIdentifier("XML");
			if (node.XmlSchemaCollection != null)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<XmlDataTypeOption, TokenGenerator>(SqlScriptGeneratorVisitor._xmlDataTypeOptionGenerators, node.XmlDataTypeOption);
				if (valueForEnumKey != null)
				{
					this.GenerateToken(valueForEnumKey);
				}
				this.GenerateSpaceAndFragmentIfNotNull(node.XmlSchemaCollection);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00081109 File Offset: 0x0007F309
		public override void ExplicitVisit(AddFileSpec node)
		{
			this.GenerateFragmentIfNotNull(node.File);
			if (node.FileName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.FileName);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00081133 File Offset: 0x0007F333
		public override void ExplicitVisit(AddSignatureStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Add);
			this.GenerateCounterSignature(node);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateModule(node);
			this.NewLineAndIndent();
			this.GenerateCryptos(node);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00081168 File Offset: 0x0007F368
		public override void ExplicitVisit(AdHocDataSource node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenDataSource);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.ProviderName);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateFragmentIfNotNull(node.InitString);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000811B6 File Offset: 0x0007F3B6
		public override void ExplicitVisit(AlterApplicationRoleStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateApplicationRoleStatementBase(node);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000811C8 File Offset: 0x0007F3C8
		public override void ExplicitVisit(AlterAssemblyStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "ASSEMBLY" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.Parameters.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.From);
				this.GenerateSpaceAndFragmentIfNotNull(node.Parameters[0]);
			}
			this.GenerateAssemblyOptions(node.Options);
			if (node.IsDropAll || node.DropFiles.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
				{
					TSqlTokenType.Drop,
					TSqlTokenType.File
				});
				if (node.IsDropAll)
				{
					this.GenerateSpaceAndKeyword(TSqlTokenType.All);
				}
				else
				{
					this.GenerateSpace();
					this.GenerateCommaSeparatedList<Literal>(node.DropFiles);
				}
			}
			if (node.AddFiles.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
				{
					TSqlTokenType.Add,
					TSqlTokenType.File,
					TSqlTokenType.From
				});
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<AddFileSpec>(node.AddFiles);
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000812CB File Offset: 0x0007F4CB
		internal void GenerateAssemblyOptions(IList<AssemblyOption> options)
		{
			if (options != null && options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateCommaSeparatedList<AssemblyOption>(options);
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000812F4 File Offset: 0x0007F4F4
		public override void ExplicitVisit(AssemblyOption node)
		{
			this.GenerateSpaceSeparatedTokens(new string[] { "UNCHECKED", "DATA" });
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00081320 File Offset: 0x0007F520
		public override void ExplicitVisit(PermissionSetAssemblyOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PermissionSetOption, string>(SqlScriptGeneratorVisitor._permissionSetOptionNames, node.PermissionSetOption);
			this.GenerateNameEqualsValue("PERMISSION_SET", valueForEnumKey);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0008134A File Offset: 0x0007F54A
		public override void ExplicitVisit(OnOffAssemblyOption node)
		{
			this.GenerateOptionStateWithEqualSign("VISIBILITY", node.OptionState);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00081360 File Offset: 0x0007F560
		public override void ExplicitVisit(AlterAuthorizationStatement node)
		{
			this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
			{
				TSqlTokenType.Alter,
				TSqlTokenType.Authorization
			});
			this.NewLineAndIndent();
			this.GenerateFragmentIfNotNull(node.SecurityTargetObject);
			this.NewLineAndIndent();
			this.GenerateKeywordAndSpace(TSqlTokenType.To);
			if (node.ToSchemaOwner)
			{
				this.GenerateSpaceSeparatedTokens(TSqlTokenType.Schema, new string[] { "OWNER" });
				return;
			}
			this.GenerateFragmentIfNotNull(node.PrincipalName);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000813D8 File Offset: 0x0007F5D8
		public override void ExplicitVisit(AlterCertificateStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "CERTIFICATE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpace();
			switch (node.Kind)
			{
			case AlterCertificateStatementKind.RemovePrivateKey:
				this.GenerateRemovePrivateKey();
				return;
			case AlterCertificateStatementKind.RemoveAttestedOption:
				this.GenerateRemoteAttestedOption();
				return;
			case AlterCertificateStatementKind.WithPrivateKey:
				this.GenerateWithPrivateKey(node.PrivateKeyPath, node.EncryptionPassword, node.DecryptionPassword);
				return;
			case AlterCertificateStatementKind.WithActiveForBeginDialog:
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("ACTIVE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpace();
				this.GenerateOptionStateWithEqualSign("BEGIN_DIALOG", node.ActiveForBeginDialog);
				return;
			case AlterCertificateStatementKind.AttestedBy:
				this.GenerateAttestedBy(node.AttestedBy);
				return;
			default:
				return;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0008149C File Offset: 0x0007F69C
		protected void GenerateEndpointBody(AlterCreateEndpointStatementBase node)
		{
			if (node.State != EndpointState.NotSpecified)
			{
				string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<EndpointState, string>(SqlScriptGeneratorVisitor._endpointStateNames, node.State);
				if (valueForEnumKey != null)
				{
					this.NewLineAndIndent();
					this.GenerateNameEqualsValue("STATE", valueForEnumKey);
				}
			}
			if (node.Affinity != null)
			{
				if (node.State != EndpointState.NotSpecified)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				}
				this.NewLineAndIndent();
				this.GenerateFragmentIfNotNull(node.Affinity);
			}
			if (node.Protocol != EndpointProtocol.None)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				string valueForEnumKey2 = SqlScriptGeneratorVisitor.GetValueForEnumKey<EndpointProtocol, string>(SqlScriptGeneratorVisitor._endpointProtocolNames, node.Protocol);
				if (valueForEnumKey2 != null)
				{
					this.GenerateSpaceAndIdentifier(valueForEnumKey2);
				}
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<EndpointProtocolOption>(node.ProtocolOptions, 3);
			}
			if (node.EndpointType != EndpointType.NotSpecified)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.For);
				string valueForEnumKey3 = SqlScriptGeneratorVisitor.GetValueForEnumKey<EndpointType, string>(SqlScriptGeneratorVisitor._endpointTypeNames, node.EndpointType);
				if (valueForEnumKey3 != null)
				{
					this.GenerateSpaceAndIdentifier(valueForEnumKey3);
				}
				this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<PayloadOption>(node.PayloadOptions, 3);
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00081580 File Offset: 0x0007F780
		public override void ExplicitVisit(AlterCredentialStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateCredentialStatementBody(node);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00081590 File Offset: 0x0007F790
		public override void ExplicitVisit(AlterDatabaseAddFileStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.Add);
			if (node.IsLog)
			{
				this.GenerateSpaceAndIdentifier("LOG");
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.File);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<FileDeclaration>(node.FileDeclarations);
			if (node.FileGroup != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.To);
				this.GenerateSpaceAndIdentifier("FILEGROUP");
				this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00081608 File Offset: 0x0007F808
		public override void ExplicitVisit(AlterDatabaseAddFileGroupStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.Add);
			this.GenerateSpaceAndIdentifier("FILEGROUP");
			this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
			if (node.ContainsFileStream)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Contains);
				this.GenerateSpaceAndIdentifier("FILESTREAM");
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0008165B File Offset: 0x0007F85B
		public override void ExplicitVisit(AlterDatabaseCollateStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00081670 File Offset: 0x0007F870
		public override void ExplicitVisit(AlterDatabaseModifyFileGroupStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateIdentifier("MODIFY");
			this.GenerateSpaceAndIdentifier("FILEGROUP");
			this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
			this.GenerateSpace();
			if (node.NewFileGroupName != null)
			{
				this.GenerateNameEqualsValue("NAME", node.NewFileGroupName);
				return;
			}
			if (node.MakeDefault)
			{
				this.GenerateKeyword(TSqlTokenType.Default);
				return;
			}
			if (node.UpdatabilityOption != ModifyFileGroupOption.None)
			{
				ModifyFilegroupOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.UpdatabilityOption);
				this.GenerateSpaceAndFragmentIfNotNull(node.Termination);
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00081707 File Offset: 0x0007F907
		public override void ExplicitVisit(AlterDatabaseModifyFileStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateIdentifier("MODIFY");
			this.GenerateSpaceAndKeyword(TSqlTokenType.File);
			this.GenerateSpaceAndFragmentIfNotNull(node.FileDeclaration);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00081735 File Offset: 0x0007F935
		public override void ExplicitVisit(AlterDatabaseModifyNameStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateIdentifier("MODIFY");
			this.GenerateSpace();
			this.GenerateNameEqualsValue("NAME", node.NewDatabaseName);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00081768 File Offset: 0x0007F968
		public override void ExplicitVisit(AlterDatabaseRebuildLogStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateIdentifier("REBUILD");
			this.GenerateSpaceAndIdentifier("LOG");
			if (node.FileDeclaration != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.FileDeclaration);
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000817B4 File Offset: 0x0007F9B4
		public override void ExplicitVisit(AlterDatabaseRemoveFileStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			this.GenerateSpaceAndIdentifier("REMOVE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.File);
			this.GenerateSpaceAndFragmentIfNotNull(node.File);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000817E4 File Offset: 0x0007F9E4
		public override void ExplicitVisit(AlterDatabaseSetStatement node)
		{
			this.GenerateAlterDbStatementHead(node);
			this.NewLineAndIndent();
			bool flag = true;
			foreach (DatabaseOption databaseOption in node.Options)
			{
				if (databaseOption.OptionKind != DatabaseOptionKind.MaxSize && databaseOption.OptionKind != DatabaseOptionKind.Edition)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.GenerateIdentifier("MODIFY");
				this.GenerateSpace();
				AlignmentPoint alignmentPoint = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateParenthesisedCommaSeparatedList<DatabaseOption>(node.Options, true);
				this.PopAlignmentPoint();
			}
			else
			{
				this.GenerateKeyword(TSqlTokenType.Set);
				this.GenerateSpace();
				AlignmentPoint alignmentPoint2 = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint2);
				this.GenerateCommaSeparatedList<DatabaseOption>(node.Options, true);
				this.PopAlignmentPoint();
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Termination);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000818CC File Offset: 0x0007FACC
		protected void GenerateAlterDbStatementHead(AlterDatabaseStatement node)
		{
			this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
			{
				TSqlTokenType.Alter,
				TSqlTokenType.Database
			});
			if (node.UseCurrent)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Current);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00081910 File Offset: 0x0007FB10
		public override void ExplicitVisit(AlterDatabaseTermination node)
		{
			this.NewLineAndIndent();
			this.GenerateKeywordAndSpace(TSqlTokenType.With);
			if (node.ImmediateRollback)
			{
				this.GenerateKeyword(TSqlTokenType.Rollback);
				this.GenerateSpaceAndIdentifier("IMMEDIATE");
				return;
			}
			if (node.RollbackAfter != null)
			{
				this.GenerateKeyword(TSqlTokenType.Rollback);
				this.GenerateSpaceAndIdentifier("AFTER");
				this.GenerateSpaceAndFragmentIfNotNull(node.RollbackAfter);
				this.GenerateSpaceAndIdentifier("SECONDS");
				return;
			}
			if (node.NoWait)
			{
				this.GenerateIdentifier("NO_WAIT");
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00081996 File Offset: 0x0007FB96
		public override void ExplicitVisit(AlterEndpointStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("ENDPOINT");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpace();
			this.GenerateEndpointBody(node);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000819C4 File Offset: 0x0007FBC4
		public override void ExplicitVisit(AlterFullTextCatalogStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "FULLTEXT", "CATALOG" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpace();
			switch (node.Action)
			{
			case AlterFullTextCatalogAction.None:
				break;
			case AlterFullTextCatalogAction.Rebuild:
				this.GenerateIdentifier("REBUILD");
				if (node.Options != null && node.Options.Count > 0)
				{
					this.GenerateSpace();
					this.GenerateSymbolAndSpace(TSqlTokenType.With);
					this.GenerateCommaSeparatedList<FullTextCatalogOption>(node.Options);
					return;
				}
				break;
			case AlterFullTextCatalogAction.Reorganize:
				this.GenerateIdentifier("REORGANIZE");
				break;
			case AlterFullTextCatalogAction.AsDefault:
				this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
				{
					TSqlTokenType.As,
					TSqlTokenType.Default
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00081A83 File Offset: 0x0007FC83
		public override void ExplicitVisit(AlterFullTextIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00081AC0 File Offset: 0x0007FCC0
		public override void ExplicitVisit(SimpleAlterFullTextIndexAction node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._simpleAlterFulltextIndexActionKindActions, node.ActionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00081AE8 File Offset: 0x0007FCE8
		public override void ExplicitVisit(SetStopListAlterFullTextIndexAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Set);
			this.GenerateFragmentIfNotNull(node.StopListOption);
			this.GenerateWithNoPopulation(node.WithNoPopulation);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00081B0D File Offset: 0x0007FD0D
		public override void ExplicitVisit(SetSearchPropertyListAlterFullTextIndexAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Set);
			this.GenerateFragmentIfNotNull(node.SearchPropertyListOption);
			this.GenerateWithNoPopulation(node.WithNoPopulation);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00081B32 File Offset: 0x0007FD32
		public override void ExplicitVisit(DropAlterFullTextIndexAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			this.GenerateWithNoPopulation(node.WithNoPopulation);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00081B54 File Offset: 0x0007FD54
		public override void ExplicitVisit(AddAlterFullTextIndexAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Add);
			this.GenerateParenthesisedCommaSeparatedList<FullTextIndexColumn>(node.Columns);
			this.GenerateWithNoPopulation(node.WithNoPopulation);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00081B78 File Offset: 0x0007FD78
		public override void ExplicitVisit(AlterColumnAlterFullTextIndexAction node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateKeywordAndSpace(TSqlTokenType.Column);
			this.GenerateFragmentIfNotNull(node.Column.Name);
			if (node.Column.StatisticalSemantics)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Add);
			}
			else
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Drop);
			}
			this.GenerateSpaceAndIdentifier("STATISTICAL_SEMANTICS");
			this.GenerateWithNoPopulation(node.WithNoPopulation);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00081BDC File Offset: 0x0007FDDC
		protected void GenerateWithNoPopulation(bool withNoPopulation)
		{
			if (withNoPopulation)
			{
				this.GenerateSpace();
				this.GenerateSpaceSeparatedTokens(TSqlTokenType.With, new string[] { "NO", "POPULATION" });
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00081C15 File Offset: 0x0007FE15
		public override void ExplicitVisit(AlterFunctionStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateFunctionStatementBody(node);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00081C28 File Offset: 0x0007FE28
		public override void ExplicitVisit(AlterIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
			}
			else
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			this.GenerateSpace();
			if (node.AlterIndexType == AlterIndexType.Set)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Set);
				this.GenerateParenthesisedCommaSeparatedList<IndexOption>(node.IndexOptions);
				return;
			}
			AlterIndexTypeHelper.Instance.GenerateSourceForOption(this._writer, node.AlterIndexType);
			this.GenerateSpaceAndFragmentIfNotNull(node.Partition);
			if (node.IndexOptions.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateParenthesisedCommaSeparatedList<IndexOption>(node.IndexOptions);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00081CE9 File Offset: 0x0007FEE9
		public override void ExplicitVisit(AlterLoginEnableDisableStatement node)
		{
			this.GenerateAlterLoginHeader(node);
			this.GenerateIdentifier(node.IsEnable ? "ENABLE" : "DISABLE");
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00081D0C File Offset: 0x0007FF0C
		public override void ExplicitVisit(AlterLoginOptionsStatement node)
		{
			this.GenerateAlterLoginHeader(node);
			this.GenerateKeywordAndSpace(TSqlTokenType.With);
			this.GenerateFragmentList<PrincipalOption>(node.Options);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00081D2C File Offset: 0x0007FF2C
		public override void ExplicitVisit(AlterLoginAddDropCredentialStatement node)
		{
			this.GenerateAlterLoginHeader(node);
			this.GenerateKeywordAndSpace(node.IsAdd ? TSqlTokenType.Add : TSqlTokenType.Drop);
			this.GenerateIdentifier("CREDENTIAL");
			this.GenerateSpaceAndFragmentIfNotNull(node.CredentialName);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00081D5F File Offset: 0x0007FF5F
		private void GenerateAlterLoginHeader(AlterLoginStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("LOGIN");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpace();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00081D88 File Offset: 0x0007FF88
		public override void ExplicitVisit(AlterMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpace();
			switch (node.Option)
			{
			case AlterMasterKeyOption.Regenerate:
				this.GenerateRegenerateOption(node.Password);
				return;
			case AlterMasterKeyOption.ForceRegenerate:
				this.GenerateIdentifier("FORCE");
				this.GenerateSpace();
				this.GenerateRegenerateOption(node.Password);
				return;
			case AlterMasterKeyOption.AddEncryptionByServiceMasterKey:
				this.GenerateKeywordAndSpace(TSqlTokenType.Add);
				this.GenerateEncyptionByServiceMasterKey();
				return;
			case AlterMasterKeyOption.AddEncryptionByPassword:
				this.GenerateKeywordAndSpace(TSqlTokenType.Add);
				this.GenerateEncryptionByPassword(node.Password);
				return;
			case AlterMasterKeyOption.DropEncryptionByServiceMasterKey:
				this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
				this.GenerateEncyptionByServiceMasterKey();
				return;
			case AlterMasterKeyOption.DropEncryptionByPassword:
				this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
				this.GenerateEncryptionByPassword(node.Password);
				return;
			default:
				return;
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00081E4D File Offset: 0x0008004D
		private void GenerateRegenerateOption(Literal password)
		{
			this.GenerateIdentifier("REGENERATE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.With);
			this.GenerateSpaceAndIdentifier("ENCRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpace();
			this.GenerateNameEqualsValue("PASSWORD", password);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00081E8A File Offset: 0x0008008A
		private void GenerateEncyptionByServiceMasterKey()
		{
			this.GenerateIdentifier("ENCRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00081EBD File Offset: 0x000800BD
		public override void ExplicitVisit(AlterMessageTypeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("MESSAGE");
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateValidationMethod(node);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00081EF0 File Offset: 0x000800F0
		public override void ExplicitVisit(AlterPartitionFunctionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Function);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSymbolAndSpace(TSqlTokenType.LeftParenthesis);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.NewLineAndIndent();
			this.GenerateIdentifier(node.IsSplit ? "SPLIT" : "MERGE");
			if (node.Boundary != null)
			{
				this.GenerateSpaceAndIdentifier("RANGE");
				this.GenerateSpace();
				this.GenerateParenthesisedFragmentIfNotNull(node.Boundary);
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00081F80 File Offset: 0x00080180
		public override void ExplicitVisit(AlterPartitionSchemeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndIdentifier("SCHEME");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndIdentifier("NEXT");
			this.GenerateSpaceAndIdentifier("USED");
			this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00081FD8 File Offset: 0x000801D8
		public override void ExplicitVisit(AlterProcedureStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateProcedureStatementBody(node);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00081FE8 File Offset: 0x000801E8
		public override void ExplicitVisit(AlterQueueStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("QUEUE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeywordAndSpace(TSqlTokenType.With);
			this.GenerateQueueOptions(node.QueueOptions);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00082028 File Offset: 0x00080228
		public override void ExplicitVisit(AlterRemoteServiceBindingStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "REMOTE", "SERVICE", "BINDING" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateBindingOptions(node.Options);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00082074 File Offset: 0x00080274
		public override void ExplicitVisit(AlterRoleStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "ROLE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndFragmentIfNotNull(node.Action);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000820B0 File Offset: 0x000802B0
		public override void ExplicitVisit(AlterRouteStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "ROUTE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateRouteOptions(node);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000820E8 File Offset: 0x000802E8
		public override void ExplicitVisit(AlterSchemaStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndIdentifier("TRANSFER");
			if (node.ObjectKind != SecurityObjectKind.NotSpecified)
			{
				this.GenerateSpace();
				this.GenerateSourceForSecurityObjectKind(node.ObjectKind);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.ObjectName);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00082144 File Offset: 0x00080344
		public override void ExplicitVisit(AlterServiceMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpace();
			switch (node.Kind)
			{
			case AlterServiceMasterKeyOption.Regenerate:
				this.GenerateIdentifier("REGENERATE");
				return;
			case AlterServiceMasterKeyOption.ForceRegenerate:
				this.GenerateIdentifier("FORCE");
				this.GenerateSpaceAndIdentifier("REGENERATE");
				return;
			case AlterServiceMasterKeyOption.WithOldAccount:
				this.GenerateWithClause(node, "OLD_ACCOUNT", "OLD_PASSWORD");
				return;
			case AlterServiceMasterKeyOption.WithNewAccount:
				this.GenerateWithClause(node, "NEW_ACCOUNT", "NEW_PASSWORD");
				return;
			default:
				return;
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000821E2 File Offset: 0x000803E2
		private void GenerateWithClause(AlterServiceMasterKeyStatement node, string accountTitle, string passwordTitle)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.With);
			this.GenerateNameEqualsValue(accountTitle, node.Account);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateNameEqualsValue(passwordTitle, node.Password);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00082214 File Offset: 0x00080414
		public override void ExplicitVisit(AlterServiceStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Alter, new string[] { "SERVICE" });
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.QueueName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndIdentifier("QUEUE");
				this.GenerateSpaceAndFragmentIfNotNull(node.QueueName);
			}
			if (node.ServiceContracts.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ServiceContract>(node.ServiceContracts);
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0008228C File Offset: 0x0008048C
		public override void ExplicitVisit(AlterTableAddTableElementStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateAlterTableHead(node);
			if (node.ExistingRowsCheckEnforcement != ConstraintEnforcement.NotSpecified)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateConstraintEnforcement(node.ExistingRowsCheckEnforcement);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.Add);
			this.GenerateSpace();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint2);
			this.GenerateCommaSeparatedList<ColumnDefinition>(node.Definition.ColumnDefinitions, true);
			if (node.Definition.ColumnDefinitions.Count > 0 && node.Definition.TableConstraints.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.NewLine();
			}
			if (node.Definition.TableConstraints.Count > 0)
			{
				this.GenerateCommaSeparatedList<ConstraintDefinition>(node.Definition.TableConstraints, true);
			}
			this.PopAlignmentPoint();
			this.PopAlignmentPoint();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0008236C File Offset: 0x0008056C
		public override void ExplicitVisit(AlterTableAlterColumnStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Column);
			this.GenerateSpaceAndFragmentIfNotNull(node.ColumnIdentifier);
			if (node.DataType != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
				this.GenerateSpaceAndCollation(node.Collation);
				this.GenerateFragmentIfNotNull(node.StorageOptions);
				switch (node.AlterTableAlterColumnOption)
				{
				case AlterTableAlterColumnOption.NoOptionDefined:
				case AlterTableAlterColumnOption.AddRowGuidCol:
				case AlterTableAlterColumnOption.DropRowGuidCol:
					break;
				case AlterTableAlterColumnOption.Null:
					this.GenerateSpaceAndKeyword(TSqlTokenType.Null);
					return;
				case AlterTableAlterColumnOption.NotNull:
					this.GenerateSpaceAndKeyword(TSqlTokenType.Not);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Null);
					return;
				default:
					return;
				}
			}
			else
			{
				List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AlterTableAlterColumnOption, List<TokenGenerator>>(SqlScriptGeneratorVisitor._alterTableAlterColumnOptionNames, node.AlterTableAlterColumnOption);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateTokenList(valueForEnumKey);
				}
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00082424 File Offset: 0x00080624
		public override void ExplicitVisit(AlterTableConstraintModificationStatement node)
		{
			this.GenerateAlterTableHead(node);
			if (node.ExistingRowsCheckEnforcement != ConstraintEnforcement.NotSpecified)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateConstraintEnforcement(node.ExistingRowsCheckEnforcement);
			}
			this.GenerateSpace();
			this.GenerateConstraintEnforcement(node.ConstraintEnforcement);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Constraint);
			this.GenerateSpace();
			if (node.All)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			}
			this.GenerateCommaSeparatedList<Identifier>(node.ConstraintNames);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0008249C File Offset: 0x0008069C
		public override void ExplicitVisit(AlterTableDropTableElement node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<TableElementType, TokenGenerator>(SqlScriptGeneratorVisitor._tableElementTypeGenerators, node.TableElementType);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			if (node.TableElementType != TableElementType.NotSpecified)
			{
				this.GenerateSpace();
			}
			this.GenerateFragmentIfNotNull(node.Name);
			if (node.DropClusteredConstraintOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<DropClusteredConstraintOption>(node.DropClusteredConstraintOptions);
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0008250F File Offset: 0x0008070F
		public override void ExplicitVisit(AlterTableDropTableElementStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Drop);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<AlterTableDropTableElement>(node.AlterTableDropTableElements);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00082532 File Offset: 0x00080732
		protected void GenerateAlterTableHead(AlterTableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpaceAndFragmentIfNotNull(node.SchemaObjectName);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00082554 File Offset: 0x00080754
		protected void GenerateConstraintEnforcement(ConstraintEnforcement enforcement)
		{
			switch (enforcement)
			{
			case ConstraintEnforcement.NotSpecified:
				break;
			case ConstraintEnforcement.NoCheck:
				this.GenerateKeyword(TSqlTokenType.NoCheck);
				return;
			case ConstraintEnforcement.Check:
				this.GenerateKeyword(TSqlTokenType.Check);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00082588 File Offset: 0x00080788
		public override void ExplicitVisit(AlterTableSwitchStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpaceAndIdentifier("SWITCH");
			this.GenerateForPartitionIfNotNull(node.SourcePartitionNumber);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.TargetTable);
			this.GenerateForPartitionIfNotNull(node.TargetPartitionNumber);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000825D6 File Offset: 0x000807D6
		private void GenerateForPartitionIfNotNull(ScalarExpression expression)
		{
			if (expression != null)
			{
				this.GenerateSpaceAndIdentifier("PARTITION");
				this.GenerateSpaceAndFragmentIfNotNull(expression);
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000825F0 File Offset: 0x000807F0
		public override void ExplicitVisit(AlterTableTriggerModificationStatement node)
		{
			this.GenerateAlterTableHead(node);
			this.GenerateSpace();
			this.GenerateTriggerEnforcement(node.TriggerEnforcement);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Trigger);
			this.GenerateSpace();
			if (node.All)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			}
			this.GenerateCommaSeparatedList<Identifier>(node.TriggerNames);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00082643 File Offset: 0x00080843
		public override void ExplicitVisit(AlterTriggerStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpace();
			this.GenerateTriggerStatementBody(node);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0008265C File Offset: 0x0008085C
		public override void ExplicitVisit(AlterUserStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.User);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<PrincipalOption>(node.UserOptions);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000826AA File Offset: 0x000808AA
		public override void ExplicitVisit(AlterViewStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpace();
			this.GenerateViewStatementBody(node);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000826C0 File Offset: 0x000808C0
		public override void ExplicitVisit(AlterXmlSchemaCollectionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSpaceAndIdentifier("XML");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndIdentifier("COLLECTION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Add);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00082714 File Offset: 0x00080914
		public override void ExplicitVisit(ApplicationRoleOption node)
		{
			ApplicationRoleOptionHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00082744 File Offset: 0x00080944
		protected void GenerateApplicationRoleStatementBase(ApplicationRoleStatement node)
		{
			this.GenerateIdentifier("APPLICATION");
			this.GenerateSpaceAndIdentifier("ROLE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<ApplicationRoleOption>(node.ApplicationRoleOptions);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00082796 File Offset: 0x00080996
		public override void ExplicitVisit(AssemblyEncryptionSource node)
		{
			this.GenerateIdentifier("ASSEMBLY");
			this.GenerateSpaceAndFragmentIfNotNull(node.Assembly);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000827AF File Offset: 0x000809AF
		public override void ExplicitVisit(FileEncryptionSource node)
		{
			if (node.IsExecutable)
			{
				this.GenerateIdentifier("EXECUTABLE");
				this.GenerateSpace();
			}
			this.GenerateNameEqualsValue(TSqlTokenType.File, node.File);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000827D8 File Offset: 0x000809D8
		public override void ExplicitVisit(ProviderEncryptionSource node)
		{
			this.GenerateIdentifier("PROVIDER");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.KeyOptions.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<KeyOption>(node.KeyOptions);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00082827 File Offset: 0x00080A27
		public override void ExplicitVisit(AssemblyName node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			if (node.ClassName != null)
			{
				this.GenerateSymbol(TSqlTokenType.Dot);
				this.GenerateFragmentIfNotNull(node.ClassName);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00082854 File Offset: 0x00080A54
		public override void ExplicitVisit(AsymmetricKeyCreateLoginSource node)
		{
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndIdentifier("ASYMMETRIC");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndFragmentIfNotNull(node.Key);
			this.GenerateCredential(node.Credential);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00082889 File Offset: 0x00080A89
		public override void ExplicitVisit(AuthenticationEndpointProtocolOption node)
		{
			this.GenerateIdentifier("AUTHENTICATION");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateCommaSeparatedFlagOpitons<AuthenticationTypes>(SqlScriptGeneratorVisitor._authenticationTypesGenerators, node.AuthenticationTypes);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000828C8 File Offset: 0x00080AC8
		public override void ExplicitVisit(AuthenticationPayloadOption node)
		{
			this.GenerateTokenAndEqualSign("AUTHENTICATION");
			this.GenerateCertificateForAuthenticationPayloadOption(node.TryCertificateFirst, node.Certificate);
			if (node.Protocol != AuthenticationProtocol.NotSpecified)
			{
				this.GenerateSpaceAndIdentifier("WINDOWS");
				if (node.Protocol != AuthenticationProtocol.Windows)
				{
					string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AuthenticationProtocol, string>(SqlScriptGeneratorVisitor._authenticationProtocolNames, node.Protocol);
					if (valueForEnumKey != null)
					{
						this.GenerateSpaceAndIdentifier(valueForEnumKey);
					}
				}
			}
			this.GenerateCertificateForAuthenticationPayloadOption(!node.TryCertificateFirst, node.Certificate);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0008293E File Offset: 0x00080B3E
		protected void GenerateCertificateForAuthenticationPayloadOption(bool generate, Identifier certificateName)
		{
			if (generate && certificateName != null)
			{
				this.GenerateSpaceAndIdentifier("CERTIFICATE");
				this.GenerateSpaceAndFragmentIfNotNull(certificateName);
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00082958 File Offset: 0x00080B58
		public override void ExplicitVisit(BackupCertificateStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Backup);
			this.GenerateSpaceAndIdentifier("CERTIFICATE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateNameEqualsValue(TSqlTokenType.File, node.File);
			if (node.PrivateKeyPath != null || node.DecryptionPassword != null || node.EncryptionPassword != null)
			{
				this.NewLineAndIndent();
				this.GenerateWithPrivateKey(node.PrivateKeyPath, node.EncryptionPassword, node.DecryptionPassword);
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000829DC File Offset: 0x00080BDC
		public override void ExplicitVisit(BackupDatabaseStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Backup);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
			if (node.Files != null && node.Files.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateCommaSeparatedList<BackupRestoreFileInfo>(node.Files);
			}
			this.GenerateDeviceAndOption(node);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00082A34 File Offset: 0x00080C34
		public override void ExplicitVisit(BackupOption node)
		{
			if (BackupOptionsWithValueHelper.Instance.TryGenerateSourceForOption(this._writer, node.OptionKind))
			{
				if (node.Value != null)
				{
					this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
					this.GenerateSpaceAndFragmentIfNotNull(node.Value);
					return;
				}
			}
			else
			{
				BackupOptionsNoValueHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00082A90 File Offset: 0x00080C90
		public override void ExplicitVisit(BackupMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Backup);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateNameEqualsValue(TSqlTokenType.File, node.File);
			this.GenerateSpace();
			this.GenerateEncryptionByPassword(node.Password);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00082AEC File Offset: 0x00080CEC
		protected void GenerateCommonRestorePart(BackupRestoreMasterKeyStatementBase node, bool isService)
		{
			this.GenerateKeyword(TSqlTokenType.Restore);
			if (isService)
			{
				this.GenerateSpaceAndIdentifier("SERVICE");
			}
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpace();
			this.GenerateNameEqualsValue(TSqlTokenType.File, node.File);
			this.GenerateSpace();
			this.GenerateDecryptionByPassword(node.Password);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00082B54 File Offset: 0x00080D54
		public override void ExplicitVisit(BackupRestoreFileInfo node)
		{
			switch (node.ItemKind)
			{
			case BackupRestoreItemKind.None:
				break;
			case BackupRestoreItemKind.Files:
				this.GenerateKeywordAndSpace(TSqlTokenType.File);
				this.GenerateItems(node.Items);
				return;
			case BackupRestoreItemKind.FileGroups:
				this.GenerateIdentifier("FILEGROUP");
				this.GenerateSpace();
				this.GenerateItems(node.Items);
				break;
			case BackupRestoreItemKind.Page:
				if (node.Items.Count == 1)
				{
					this.GenerateIdentifier("PAGE");
					this.GenerateSpace();
					this.GenerateItems(node.Items);
					return;
				}
				break;
			case BackupRestoreItemKind.ReadWriteFileGroups:
				this.GenerateIdentifier("READ_WRITE_FILEGROUPS");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00082BED File Offset: 0x00080DED
		private void GenerateItems(IList<ValueExpression> items)
		{
			if (items != null)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.EqualsSign);
				if (items.Count == 1)
				{
					this.GenerateFragmentIfNotNull(items[0]);
					return;
				}
				this.GenerateParenthesisedCommaSeparatedList<ValueExpression>(items);
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00082C1C File Offset: 0x00080E1C
		public override void ExplicitVisit(BackupServiceMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Backup);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateNameEqualsValue(TSqlTokenType.File, node.File);
			this.GenerateSpace();
			this.GenerateEncryptionByPassword(node.Password);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00082C80 File Offset: 0x00080E80
		protected void GenerateDeviceAndOption(BackupStatement node)
		{
			if (node.Devices.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.To);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<DeviceInfo>(node.Devices);
				if (node.MirrorToClauses != null)
				{
					foreach (MirrorToClause mirrorToClause in node.MirrorToClauses)
					{
						this.NewLineAndIndent();
						this.GenerateFragmentIfNotNull(mirrorToClause);
					}
				}
			}
			if (node.Options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<BackupOption>(node.Options);
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00082D40 File Offset: 0x00080F40
		public override void ExplicitVisit(BackupTransactionLogStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Backup);
			this.GenerateSpaceAndIdentifier("LOG");
			this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
			this.GenerateDeviceAndOption(node);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00082D68 File Offset: 0x00080F68
		public override void ExplicitVisit(BackwardsCompatibleDropIndexClause node)
		{
			this.GenerateFragmentIfNotNull(node.Index);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00082D78 File Offset: 0x00080F78
		public override void ExplicitVisit(BeginConversationTimerStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Begin);
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			this.GenerateSpaceAndIdentifier("TIMER");
			this.GenerateSpace();
			this.GenerateParenthesisedFragmentIfNotNull(node.Handle);
			this.GenerateSpace();
			this.GenerateNameEqualsValue("TIMEOUT", node.Timeout);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00082DCC File Offset: 0x00080FCC
		public override void ExplicitVisit(BeginDialogStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Begin);
			this.GenerateSpaceAndIdentifier("DIALOG");
			if (node.IsConversation)
			{
				this.GenerateSpaceAndIdentifier("CONVERSATION");
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Handle);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.InitiatorServiceName);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.TargetServiceName);
			if (node.InstanceSpec != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.InstanceSpec);
			}
			if (node.ContractName != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndIdentifier("CONTRACT");
				this.GenerateSpaceAndFragmentIfNotNull(node.ContractName);
			}
			this.GenerateDialogOptions(node.Options);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00082EB0 File Offset: 0x000810B0
		private void GenerateDialogOptions(IList<DialogOption> options)
		{
			if (options != null && options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateCommaSeparatedList<DialogOption>(options);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00082ED6 File Offset: 0x000810D6
		public override void ExplicitVisit(OnOffDialogOption node)
		{
			this.GenerateOptionStateWithEqualSign("ENCRYPTION", node.OptionState);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00082EEC File Offset: 0x000810EC
		public override void ExplicitVisit(ScalarExpressionDialogOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DialogOptionKind, string>(SqlScriptGeneratorVisitor._dialogOptionNames, node.OptionKind);
			this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00082F18 File Offset: 0x00081118
		public override void ExplicitVisit(BeginEndBlockStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeyword(TSqlTokenType.Begin);
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.StatementList);
			this.PopAlignmentPoint();
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.End);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00082F60 File Offset: 0x00081160
		public override void ExplicitVisit(BeginTransactionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Begin);
			if (node.Distributed)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Distributed);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Transaction);
			if (node.Name != null)
			{
				this.GenerateSpace();
				this.GenerateTransactionName(node.Name);
			}
			if (node.MarkDefined)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("MARK");
				this.GenerateSpaceAndFragmentIfNotNull(node.MarkDescription);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00082FDC File Offset: 0x000811DC
		public override void ExplicitVisit(BinaryQueryExpression node)
		{
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.GenerateBinaryQueryExpression(node, alignmentPointForFragment, null);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00083000 File Offset: 0x00081200
		public void GenerateBinaryQueryExpression(BinaryQueryExpression node, AlignmentPoint clauseBody, SchemaObjectName intoClause)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateQueryExpression(node.FirstQueryExpression, clauseBody, intoClause);
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<BinaryQueryExpressionType, TokenGenerator>(SqlScriptGeneratorVisitor._binaryQueryExpressionTypeGenerators, node.BinaryQueryExpressionType);
			if (valueForEnumKey != null)
			{
				this.NewLine();
				this.GenerateToken(valueForEnumKey);
			}
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
			}
			if (node.SecondQueryExpression != null)
			{
				this.NewLine();
				AlignmentPoint alignmentPoint2 = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint2);
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.SecondQueryExpression, clauseBody);
				this.PopAlignmentPoint();
			}
			this.PopAlignmentPoint();
			if (node.OrderByClause != null)
			{
				this.GenerateSeparatorForOrderBy();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OrderByClause, clauseBody);
			}
			if (node.OffsetClause != null)
			{
				this.GenerateSeparatorForOffsetClause();
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OffsetClause, clauseBody);
			}
			if (node.ForClause != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.For);
				this.MarkClauseBodyAlignmentWhenNecessary(true, clauseBody);
				this.GenerateSpace();
				AlignmentPoint alignmentPoint3 = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint3);
				this.GenerateFragmentIfNotNull(node.ForClause);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00083105 File Offset: 0x00081305
		public override void ExplicitVisit(BreakStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Break);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0008310F File Offset: 0x0008130F
		public override void ExplicitVisit(BrowseForClause node)
		{
			this.GenerateKeyword(TSqlTokenType.Browse);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00083119 File Offset: 0x00081319
		protected void GenerateOption(BulkInsertBase node)
		{
			if (node.Options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateParenthesisedCommaSeparatedList<BulkInsertOption>(node.Options);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00083146 File Offset: 0x00081346
		public override void ExplicitVisit(OrderBulkInsertOption node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Order);
			this.GenerateParenthesisedCommaSeparatedList<ColumnWithSortOrder>(node.Columns);
			if (node.IsUnique)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Unique);
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0008316F File Offset: 0x0008136F
		public override void ExplicitVisit(BulkInsertOption node)
		{
			if (OpenRowsetBulkHintOptionsHelper.Instance.TryGenerateSourceForOption(this._writer, node.OptionKind))
			{
				return;
			}
			BulkInsertFlagOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000831A0 File Offset: 0x000813A0
		public override void ExplicitVisit(LiteralBulkInsertOption node)
		{
			if (BulkInsertIntOptionsHelper.Instance.TryGenerateSourceForOption(this._writer, node.OptionKind))
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.Value);
				return;
			}
			if (BulkInsertStringOptionsHelper.Instance.TryGenerateSourceForOption(this._writer, node.OptionKind))
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.Value);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0008320C File Offset: 0x0008140C
		public override void ExplicitVisit(BulkInsertStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Bulk);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Insert);
			this.GenerateSpaceAndFragmentIfNotNull(node.To);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.From);
			this.GenerateOption(node);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00083248 File Offset: 0x00081448
		public override void ExplicitVisit(BulkOpenRowset node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenRowSet);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateKeyword(TSqlTokenType.Bulk);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataFile);
			if (node.Options.Count > 0)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<BulkInsertOption>(node.Options);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateTableAndColumnAliases(node);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000832BC File Offset: 0x000814BC
		public override void ExplicitVisit(SimpleCaseExpression node)
		{
			this.GenerateKeyword(TSqlTokenType.Case);
			this.GenerateSpaceAndFragmentIfNotNull(node.InputExpression);
			foreach (SimpleWhenClause simpleWhenClause in node.WhenClauses)
			{
				this.GenerateSpaceAndFragmentIfNotNull(simpleWhenClause);
			}
			if (node.ElseExpression != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Else);
				this.GenerateSpaceAndFragmentIfNotNull(node.ElseExpression);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.End);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00083350 File Offset: 0x00081550
		public override void ExplicitVisit(SearchedCaseExpression node)
		{
			this.GenerateKeyword(TSqlTokenType.Case);
			foreach (SearchedWhenClause searchedWhenClause in node.WhenClauses)
			{
				this.GenerateSpaceAndFragmentIfNotNull(searchedWhenClause);
			}
			if (node.ElseExpression != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Else);
				this.GenerateSpaceAndFragmentIfNotNull(node.ElseExpression);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.End);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000833D8 File Offset: 0x000815D8
		public override void ExplicitVisit(CastCall node)
		{
			this.GenerateIdentifier("CAST");
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Parameter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00083432 File Offset: 0x00081632
		public override void ExplicitVisit(CertificateCreateLoginSource node)
		{
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndIdentifier("CERTIFICATE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Certificate);
			this.GenerateCredential(node.Credential);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00083460 File Offset: 0x00081660
		public override void ExplicitVisit(CertificateOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<CertificateOptionKinds, string>(SqlScriptGeneratorVisitor._certificateOptionNames, node.Kind);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0008348E File Offset: 0x0008168E
		public override void ExplicitVisit(CharacterSetPayloadOption node)
		{
			this.GenerateNameEqualsValue("CHARACTER_SET", node.IsSql ? "SQL" : "XML");
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000834B0 File Offset: 0x000816B0
		public override void ExplicitVisit(CheckConstraintDefinition node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateConstraintHead(node);
			this.GenerateKeyword(TSqlTokenType.Check);
			if (node.NotForReplication)
			{
				this.GenerateSpace();
				this.GenerateNotForReplication();
			}
			this.GenerateSpace();
			this.GenerateParenthesisedFragmentIfNotNull(node.CheckCondition);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00083505 File Offset: 0x00081705
		public override void ExplicitVisit(CheckpointStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Checkpoint);
			this.GenerateSpaceAndFragmentIfNotNull(node.Duration);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0008351C File Offset: 0x0008171C
		public override void ExplicitVisit(ChildObjectName node)
		{
			if (node.ServerIdentifier != null)
			{
				this.GenerateFragmentIfNotNull(node.ServerIdentifier);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			if (node.DatabaseIdentifier != null)
			{
				this.GenerateFragmentIfNotNull(node.DatabaseIdentifier);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			if (node.SchemaIdentifier != null)
			{
				this.GenerateFragmentIfNotNull(node.SchemaIdentifier);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			if (node.BaseIdentifier != null)
			{
				this.GenerateFragmentIfNotNull(node.BaseIdentifier);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			this.GenerateFragmentIfNotNull(node.ChildIdentifier);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000835B1 File Offset: 0x000817B1
		public override void ExplicitVisit(CloseCursorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Close);
			this.GenerateSpaceAndFragmentIfNotNull(node.Cursor);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000835C7 File Offset: 0x000817C7
		public override void ExplicitVisit(CloseMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Close);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000835E4 File Offset: 0x000817E4
		public override void ExplicitVisit(CloseSymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Close);
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
				this.GenerateSpaceAndIdentifier("SYMMETRIC");
				this.GenerateSpaceAndIdentifier("KEYS");
				return;
			}
			this.GenerateSpaceAndIdentifier("SYMMETRIC");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0008363E File Offset: 0x0008183E
		public override void ExplicitVisit(CoalesceExpression node)
		{
			this.GenerateKeyword(TSqlTokenType.Coalesce);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Expressions);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00083668 File Offset: 0x00081868
		public override void ExplicitVisit(ColumnDefinition node)
		{
			this.MarkWhenNecessary("name");
			this.GenerateFragmentIfNotNull(node.ColumnIdentifier);
			bool flag = true;
			this.MarkWhenNecessary("type");
			if (node.ComputedColumnExpression != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.MarkForConstraintsWhenNecessary("constraint", ref flag);
				this.GenerateSpaceAndFragmentIfNotNull(node.ComputedColumnExpression);
			}
			else
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
				if (node.Collation != null)
				{
					this.MarkForConstraintsWhenNecessary("constraint", ref flag);
					this.GenerateSpaceAndCollation(node.Collation);
				}
				this.GenerateFragmentIfNotNull(node.StorageOptions);
				if (node.DefaultConstraint != null)
				{
					this.MarkForConstraintsWhenNecessary("constraint", ref flag);
					this.GenerateSpaceAndFragmentIfNotNull(node.DefaultConstraint);
				}
				this.GenerateIdentity(node.IdentityOptions, "constraint", ref flag);
				if (node.IsRowGuidCol)
				{
					this.MarkForConstraintsWhenNecessary("constraint", ref flag);
					this.GenerateSpaceAndKeyword(TSqlTokenType.RowGuidColumn);
				}
			}
			if (node.IsPersisted)
			{
				this.MarkForConstraintsWhenNecessary("constraint", ref flag);
				this.GenerateSpaceAndIdentifier("PERSISTED");
			}
			foreach (ConstraintDefinition constraintDefinition in node.Constraints)
			{
				this.MarkForConstraintsWhenNecessary("constraint", ref flag);
				this.GenerateSpaceAndFragmentIfNotNull(constraintDefinition);
			}
			this.MarkForConstraintsWhenNecessary("constraint", ref flag);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000837D0 File Offset: 0x000819D0
		public override void ExplicitVisit(ColumnStorageOptions node)
		{
			switch (node.SparseOption)
			{
			case SparseColumnOption.Sparse:
				this.GenerateSpaceAndIdentifier("SPARSE");
				break;
			case SparseColumnOption.ColumnSetForAllSparseColumns:
				this.GenerateSpaceAndIdentifier("COLUMN_SET");
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("ALL_SPARSE_COLUMNS");
				break;
			}
			if (node.IsFileStream)
			{
				this.GenerateSpaceAndIdentifier("FILESTREAM");
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00083838 File Offset: 0x00081A38
		private void GenerateIdentity(IdentityOptions node, string apName, ref bool firstConstraint)
		{
			if (node != null)
			{
				this.MarkForConstraintsWhenNecessary(apName, ref firstConstraint);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Identity);
				if (node.IdentitySeed != null)
				{
					this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
					this.GenerateFragmentIfNotNull(node.IdentitySeed);
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
					this.GenerateFragmentIfNotNull(node.IdentityIncrement);
					this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				}
				if (node.IsIdentityNotForReplication)
				{
					this.GenerateSpace();
					this.GenerateNotForReplication();
				}
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000838AD File Offset: 0x00081AAD
		private void MarkForConstraintsWhenNecessary(string apName, ref bool firstConstraint)
		{
			if (firstConstraint)
			{
				this.MarkWhenNecessary(apName);
				firstConstraint = false;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000838C0 File Offset: 0x00081AC0
		private void MarkWhenNecessary(string apName)
		{
			if (this._options.AlignColumnDefinitionFields)
			{
				AlignmentPoint alignmentPoint = this.FindOrCreateAlignmentPointByName(apName);
				this.Mark(alignmentPoint);
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000838E9 File Offset: 0x00081AE9
		public override void ExplicitVisit(ColumnDefinitionBase node)
		{
			this.GenerateFragmentIfNotNull(node.ColumnIdentifier);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00083910 File Offset: 0x00081B10
		public override void ExplicitVisit(ColumnWithSortOrder node)
		{
			this.GenerateFragmentIfNotNull(node.Column);
			switch (node.SortOrder)
			{
			case SortOrder.NotSpecified:
				break;
			case SortOrder.Ascending:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Asc);
				return;
			case SortOrder.Descending:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Desc);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00083954 File Offset: 0x00081B54
		public override void ExplicitVisit(CommandSecurityElement80 node)
		{
			if (node.All)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			}
			bool flag = true;
			foreach (object obj in SqlScriptGeneratorVisitor._commandOptions)
			{
				CommandOptions commandOptions = (CommandOptions)obj;
				if (commandOptions != CommandOptions.None && (node.CommandOptions & commandOptions) == commandOptions)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this.GenerateSymbol(TSqlTokenType.Comma);
						this.GenerateSpace();
					}
					this.GenerateCommandOptions(commandOptions);
				}
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000839E4 File Offset: 0x00081BE4
		private void GenerateCommandOptions(CommandOptions option)
		{
			if (option <= CommandOptions.CreateRule)
			{
				switch (option)
				{
				case CommandOptions.CreateDatabase:
					this.GenerateKeyword(TSqlTokenType.Create);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
					return;
				case CommandOptions.CreateDefault:
					this.GenerateKeyword(TSqlTokenType.Create);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
					return;
				case CommandOptions.CreateDatabase | CommandOptions.CreateDefault:
					break;
				case CommandOptions.CreateProcedure:
					this.GenerateKeyword(TSqlTokenType.Create);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Procedure);
					return;
				default:
					if (option == CommandOptions.CreateFunction)
					{
						this.GenerateKeyword(TSqlTokenType.Create);
						this.GenerateSpaceAndKeyword(TSqlTokenType.Function);
						return;
					}
					if (option != CommandOptions.CreateRule)
					{
						return;
					}
					this.GenerateKeyword(TSqlTokenType.Create);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Rule);
					return;
				}
			}
			else if (option <= CommandOptions.CreateView)
			{
				if (option == CommandOptions.CreateTable)
				{
					this.GenerateKeyword(TSqlTokenType.Create);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
					return;
				}
				if (option != CommandOptions.CreateView)
				{
					return;
				}
				this.GenerateKeyword(TSqlTokenType.Create);
				this.GenerateSpaceAndKeyword(TSqlTokenType.View);
				return;
			}
			else
			{
				if (option == CommandOptions.BackupDatabase)
				{
					this.GenerateKeyword(TSqlTokenType.Backup);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
					return;
				}
				if (option != CommandOptions.BackupLog)
				{
					return;
				}
				this.GenerateKeyword(TSqlTokenType.Backup);
				this.GenerateSpaceAndIdentifier("LOG");
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00083AE8 File Offset: 0x00081CE8
		public override void ExplicitVisit(CommitTransactionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Commit);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Transaction);
			if (node.Name != null)
			{
				this.GenerateSpace();
				this.GenerateTransactionName(node.Name);
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00083B17 File Offset: 0x00081D17
		protected void GenerateSpaceAndMemoryUnit(MemoryUnit unit)
		{
			if (unit != MemoryUnit.Unspecified)
			{
				this.GenerateSpace();
				MemoryUnitsHelper.Instance.GenerateSourceForOption(this._writer, unit);
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00083B33 File Offset: 0x00081D33
		protected void GenerateOwnerIfNotNull(Identifier owner)
		{
			if (owner != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.Authorization);
				this.GenerateSpace();
				owner.Accept(this);
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00083B53 File Offset: 0x00081D53
		private void GenerateCredential(Identifier identifier)
		{
			if (identifier != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("CREDENTIAL", identifier);
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00083B75 File Offset: 0x00081D75
		protected void GenerateRemovePrivateKey()
		{
			this.GenerateIdentifier("REMOVE");
			this.GenerateSpaceAndIdentifier("PRIVATE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00083B95 File Offset: 0x00081D95
		protected void GenerateAttestedBy(Literal attestedBy)
		{
			this.GenerateIdentifier("ATTESTED");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpaceAndFragmentIfNotNull(attestedBy);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00083BB1 File Offset: 0x00081DB1
		protected void GenerateRemoteAttestedOption()
		{
			this.GenerateIdentifier("REMOVE");
			this.GenerateSpaceAndIdentifier("ATTESTED");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00083BD4 File Offset: 0x00081DD4
		internal void GenerateWithPrivateKey(Literal privateKeyPath, Literal encryptionPassword, Literal decryptionPassword)
		{
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpaceAndIdentifier("PRIVATE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			bool flag = true;
			if (privateKeyPath != null)
			{
				flag = false;
				this.GenerateNameEqualsValue(TSqlTokenType.File, privateKeyPath);
			}
			if (decryptionPassword != null)
			{
				if (!flag)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				}
				else
				{
					flag = false;
				}
				this.GenerateIdentifier("DECRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("PASSWORD", decryptionPassword);
			}
			if (encryptionPassword != null)
			{
				if (!flag)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				}
				this.GenerateIdentifier("ENCRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("PASSWORD", encryptionPassword);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00083C99 File Offset: 0x00081E99
		protected void GenerateSpaceAndCollation(Identifier collation)
		{
			if (collation != null)
			{
				this.GenerateSpace();
				this.GenerateKeyword(TSqlTokenType.Collate);
				this.GenerateSpaceAndFragmentIfNotNull(collation);
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00083CB4 File Offset: 0x00081EB4
		protected void GenerateTriggerEnforcement(TriggerEnforcement triggerEnforcement)
		{
			switch (triggerEnforcement)
			{
			case TriggerEnforcement.Disable:
				this.GenerateIdentifier("DISABLE");
				return;
			case TriggerEnforcement.Enable:
				this.GenerateIdentifier("ENABLE");
				return;
			default:
				return;
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00083CEC File Offset: 0x00081EEC
		protected void GenerateNotForReplication()
		{
			this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
			{
				TSqlTokenType.Not,
				TSqlTokenType.For,
				TSqlTokenType.Replication
			});
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00083D19 File Offset: 0x00081F19
		protected void GenerateDecryptionByPassword(Literal password)
		{
			this.GenerateIdentifier("DECRYPTION");
			this.GenerateSpace();
			this.GenerateByPassword(password);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00083D33 File Offset: 0x00081F33
		protected void GenerateEncryptionByPassword(Literal password)
		{
			this.GenerateIdentifier("ENCRYPTION");
			this.GenerateSpace();
			this.GenerateByPassword(password);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00083D4D File Offset: 0x00081F4D
		protected void GenerateByPassword(Literal password)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.By);
			this.GenerateNameEqualsValue("PASSWORD", password);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00083D64 File Offset: 0x00081F64
		protected void GenerateBinaryOperator(BinaryExpressionType operatorType)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<BinaryExpressionType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._binaryExpressionTypeGenerators, operatorType);
			this.GenerateTokenList(valueForEnumKey);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00083D84 File Offset: 0x00081F84
		protected void GenerateBinaryOperator(BooleanComparisonType operatorType)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<BooleanComparisonType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._booleanComparisonTypeGenerators, operatorType);
			this.GenerateTokenList(valueForEnumKey);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00083DA4 File Offset: 0x00081FA4
		protected void GenerateBinaryOperator(BooleanBinaryExpressionType operatorType)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<BooleanBinaryExpressionType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._booleanBinaryExpressionTypeGenerators, operatorType);
			this.GenerateTokenList(valueForEnumKey);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00083DC4 File Offset: 0x00081FC4
		protected void GenerateUniqueRowFilter(UniqueRowFilter uniqueRowFilter, bool spaceBefore)
		{
			if (uniqueRowFilter != UniqueRowFilter.NotSpecified)
			{
				if (spaceBefore)
				{
					this.GenerateSpace();
				}
				if (uniqueRowFilter == UniqueRowFilter.All)
				{
					this.GenerateKeyword(TSqlTokenType.All);
					return;
				}
				if (uniqueRowFilter == UniqueRowFilter.Distinct)
				{
					this.GenerateKeyword(TSqlTokenType.Distinct);
				}
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00083DEA File Offset: 0x00081FEA
		protected void GenerateNewLineOrSpace(bool newline)
		{
			if (newline)
			{
				this.NewLine();
				return;
			}
			this.GenerateSpace();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00083DFC File Offset: 0x00081FFC
		protected void MarkClauseBodyAlignmentWhenNecessary(bool newline, AlignmentPoint ap)
		{
			if (newline && this._options.AlignClauseBodies && ap != null)
			{
				this.Mark(ap);
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00083E18 File Offset: 0x00082018
		protected void MarkInsertColumnsAlignmentPointWhenNecessary(AlignmentPoint ap)
		{
			if (ap != null)
			{
				this.Mark(ap);
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00083E24 File Offset: 0x00082024
		protected void GenerateSeparatorForOrderBy()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeOrderByClause);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00083E37 File Offset: 0x00082037
		protected void GenerateSeparatorForFromClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeFromClause);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00083E4A File Offset: 0x0008204A
		protected void GenerateSeparatorForWhereClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeWhereClause);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00083E5D File Offset: 0x0008205D
		protected void GenerateSeparatorForGroupByClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeGroupByClause);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00083E70 File Offset: 0x00082070
		protected void GenerateSeparatorForHavingClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeHavingClause);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00083E83 File Offset: 0x00082083
		protected void GenerateSeparatorForOutputClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeOutputClause);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00083E96 File Offset: 0x00082096
		protected void GenerateSeparatorForOffsetClause()
		{
			this.GenerateNewLineOrSpace(this._options.NewLineBeforeOffsetClause);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00083EAC File Offset: 0x000820AC
		protected void GenerateQueryExpressionInParentheses(QueryExpression queryExpression)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			if (queryExpression != null)
			{
				AlignmentPoint alignmentPoint2 = new AlignmentPoint("ClauseBody");
				this.GenerateFragmentWithAlignmentPointIfNotNull(queryExpression, alignmentPoint2);
			}
			this.PopAlignmentPoint();
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00083EF8 File Offset: 0x000820F8
		private void GenerateSelectElementsList(IList<SelectElement> selectElements)
		{
			if (!this._options.MultilineSelectElementsList)
			{
				this.GenerateCommaSeparatedList<SelectElement>(selectElements);
				return;
			}
			this.GenerateFragmentList<SelectElement>(selectElements, SqlScriptGeneratorVisitor.ListGenerationOption.MultipleLineSelectElementOption);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00083F1B File Offset: 0x0008211B
		protected void GenerateParameters(ParameterizedDataTypeReference node)
		{
			if (node.Parameters.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Literal>(node.Parameters);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06001265 RID: 4709
		internal abstract HashSet<Type> StatementsThatCannotHaveSemiColon { get; }

		// Token: 0x06001266 RID: 4710 RVA: 0x00083F3D File Offset: 0x0008213D
		protected void GenerateSemiColonWhenNecessary(TSqlStatement node)
		{
			if (node != null && this._generateSemiColon && !this.StatementsThatCannotHaveSemiColon.Contains(node.GetType()))
			{
				this.GenerateSymbol(TSqlTokenType.Semicolon);
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00083F68 File Offset: 0x00082168
		protected void GenerateCommaSeparatedWithClause<T>(IList<T> fragments, bool indent, bool includeParentheses) where T : TSqlFragment
		{
			if (fragments != null && fragments.Count > 0)
			{
				this.NewLine();
				if (indent)
				{
					this.Indent();
				}
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				if (includeParentheses)
				{
					this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				}
				this.GenerateCommaSeparatedList<T>(fragments);
				if (includeParentheses)
				{
					this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				}
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00083FBE File Offset: 0x000821BE
		public override void ExplicitVisit(CompressionEndpointProtocolOption node)
		{
			this.GenerateNameEqualsValue("COMPRESSION", node.IsEnabled ? "ENABLED" : "DISABLED");
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00083FE0 File Offset: 0x000821E0
		public override void ExplicitVisit(ComputeClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateKeyword(TSqlTokenType.Compute);
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<ComputeFunction>(node.ComputeFunctions);
			if (node.ByExpressions.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<ScalarExpression>(node.ByExpressions);
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00084057 File Offset: 0x00082257
		public override void ExplicitVisit(ComputeFunction node)
		{
			ComputeFunctionTypeHelper.Instance.GenerateSourceForOption(this._writer, node.ComputeFunctionType);
			this.GenerateParenthesisedFragmentIfNotNull(node.Expression);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0008407B File Offset: 0x0008227B
		public override void ExplicitVisit(ContinueStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Continue);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00084088 File Offset: 0x00082288
		public override void ExplicitVisit(ContractMessage node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndIdentifier("SENT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<MessageSender, TokenGenerator>(SqlScriptGeneratorVisitor._messageSenderGenerators, node.SentBy);
			this.GenerateSpace();
			this.GenerateToken(valueForEnumKey);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000840D4 File Offset: 0x000822D4
		public override void ExplicitVisit(ConvertCall node)
		{
			this.GenerateKeyword(TSqlTokenType.Convert);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.DataType);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.Parameter);
			if (node.Style != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.Style);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00084150 File Offset: 0x00082350
		public override void ExplicitVisit(CreateAggregateStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("AGGREGATE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateParenthesisedCommaSeparatedList<ProcedureParameter>(node.Parameters);
			this.NewLineAndIndent();
			this.GenerateIdentifier("RETURNS");
			this.GenerateSpaceAndFragmentIfNotNull(node.ReturnType);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.External);
			this.GenerateSpaceAndIdentifier("NAME");
			this.GenerateSpaceAndFragmentIfNotNull(node.AssemblyName);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000841CD File Offset: 0x000823CD
		public override void ExplicitVisit(CreateApplicationRoleStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateApplicationRoleStatementBase(node);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000841E0 File Offset: 0x000823E0
		public override void ExplicitVisit(CreateAssemblyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("ASSEMBLY");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<ScalarExpression>(node.Parameters);
			this.GenerateAssemblyOptions(node.Options);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00084244 File Offset: 0x00082444
		public override void ExplicitVisit(CreateAsymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateAsymmetricKeyName(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			if (node.KeySource != null)
			{
				this.NewLineAndIndent();
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
				this.GenerateSpaceAndFragmentIfNotNull(node.KeySource);
			}
			if (node.EncryptionAlgorithm != EncryptionAlgorithm.None)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateTokenAndEqualSign("ALGORITHM");
				this.GenerateSpace();
				EncryptionAlgorithmsHelper.Instance.GenerateSourceForOption(this._writer, node.EncryptionAlgorithm);
			}
			if (node.Password != null)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("ENCRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("PASSWORD", node.Password);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00084314 File Offset: 0x00082514
		public override void ExplicitVisit(AlterAsymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateAsymmetricKeyName(node.Name);
			this.GenerateSpace();
			switch (node.Kind)
			{
			case AlterCertificateStatementKind.RemovePrivateKey:
				this.GenerateRemovePrivateKey();
				return;
			case AlterCertificateStatementKind.RemoveAttestedOption:
				this.GenerateRemoteAttestedOption();
				return;
			case AlterCertificateStatementKind.WithPrivateKey:
				this.GenerateWithPrivateKey(null, node.EncryptionPassword, node.DecryptionPassword);
				break;
			case AlterCertificateStatementKind.WithActiveForBeginDialog:
				break;
			case AlterCertificateStatementKind.AttestedBy:
				this.GenerateAttestedBy(node.AttestedBy);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0008438C File Offset: 0x0008258C
		public override void ExplicitVisit(DropAsymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateAsymmetricKeyName(node.Name);
			this.GenerateRemoveProviderKeyOpt(node.RemoveProviderKey);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000843AE File Offset: 0x000825AE
		private void GenerateRemoveProviderKeyOpt(bool generate)
		{
			if (generate)
			{
				this.GenerateSpaceAndIdentifier("REMOVE");
				this.GenerateSpaceAndIdentifier("PROVIDER");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000843D1 File Offset: 0x000825D1
		private void GenerateAsymmetricKeyName(Identifier name)
		{
			this.GenerateSpaceAndIdentifier("ASYMMETRIC");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndFragmentIfNotNull(name);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000843F0 File Offset: 0x000825F0
		public override void ExplicitVisit(CreateCertificateStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("CERTIFICATE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			if (node.CertificateSource != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.From);
				this.GenerateFragmentIfNotNull(node.CertificateSource);
				if (node.PrivateKeyPath != null)
				{
					this.NewLineAndIndent();
					this.GenerateKeyword(TSqlTokenType.With);
					this.GenerateSpaceAndIdentifier("PRIVATE");
					this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
					this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
					this.GenerateNameEqualsValue(TSqlTokenType.File, node.PrivateKeyPath);
					if (node.DecryptionPassword != null)
					{
						this.GenerateSymbol(TSqlTokenType.Comma);
						this.GenerateSpaceAndIdentifier("DECRYPTION");
						this.GenerateSpaceAndKeyword(TSqlTokenType.By);
						this.GenerateSpace();
						this.GenerateNameEqualsValue("PASSWORD", node.DecryptionPassword);
					}
					if (node.EncryptionPassword != null)
					{
						this.GenerateSymbol(TSqlTokenType.Comma);
						this.GenerateSpace();
						this.GenerateEncryptionPassword(node.EncryptionPassword);
					}
					this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				}
			}
			else
			{
				if (node.EncryptionPassword != null)
				{
					this.NewLineAndIndent();
					this.GenerateEncryptionPassword(node.EncryptionPassword);
				}
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<CertificateOption>(node.CertificateOptions);
			}
			if (node.ActiveForBeginDialog != OptionState.NotSet)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("ACTIVE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpace();
				this.GenerateOptionStateWithEqualSign("BEGIN_DIALOG", node.ActiveForBeginDialog);
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00084579 File Offset: 0x00082779
		private void GenerateEncryptionPassword(Literal password)
		{
			if (password != null)
			{
				this.GenerateIdentifier("ENCRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("PASSWORD", password);
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000845A3 File Offset: 0x000827A3
		public override void ExplicitVisit(CreateContractStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("CONTRACT");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.NewLineAndIndent();
			this.GenerateParenthesisedCommaSeparatedList<ContractMessage>(node.Messages);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000845E4 File Offset: 0x000827E4
		public override void ExplicitVisit(CreateCredentialStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateCredentialStatementBody(node);
			if (node.CryptographicProviderName != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("CRYPTOGRAPHIC");
				this.GenerateSpaceAndIdentifier("PROVIDER");
				this.GenerateSpaceAndFragmentIfNotNull(node.CryptographicProviderName);
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00084634 File Offset: 0x00082834
		public override void ExplicitVisit(CreateDatabaseStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
			this.GenerateSpaceAndFragmentIfNotNull(node.Containment);
			if (node.FileGroups != null && node.FileGroups.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<FileGroupDefinition>(node.FileGroups);
			}
			if (node.LogOn.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("LOG");
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<FileDeclaration>(node.LogOn);
			}
			this.GenerateSpaceAndCollation(node.Collation);
			if (node.AttachMode != AttachMode.None)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AttachMode, TokenGenerator>(SqlScriptGeneratorVisitor._attachModeGenerators, node.AttachMode);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateToken(valueForEnumKey);
				}
			}
			if (node.DatabaseSnapshot != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndIdentifier("SNAPSHOT");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Of);
				this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseSnapshot);
			}
			if (node.CopyOf != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndIdentifier("COPY");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Of);
				this.GenerateSpaceAndFragmentIfNotNull(node.CopyOf);
			}
			if (node.Options != null && node.Options.Count > 0)
			{
				bool flag = true;
				foreach (DatabaseOption databaseOption in node.Options)
				{
					if (databaseOption.OptionKind != DatabaseOptionKind.MaxSize && databaseOption.OptionKind != DatabaseOptionKind.Edition)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.NewLineAndIndent();
					this.GenerateParenthesisedCommaSeparatedList<DatabaseOption>(node.Options, true);
					return;
				}
				this.GenerateCommaSeparatedWithClause<DatabaseOption>(node.Options, true, false);
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00084810 File Offset: 0x00082A10
		public override void ExplicitVisit(CreateDefaultStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.Expression != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00084850 File Offset: 0x00082A50
		public override void ExplicitVisit(CreateEndpointStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("ENDPOINT");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.GenerateSpace();
			this.GenerateEndpointBody(node);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0008488C File Offset: 0x00082A8C
		public override void ExplicitVisit(CreateEventNotificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("EVENT");
			this.GenerateSpaceAndIdentifier("NOTIFICATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			if (node.Scope != null)
			{
				switch (node.Scope.Target)
				{
				case EventNotificationTarget.Server:
					this.GenerateSpaceAndIdentifier("SERVER");
					break;
				case EventNotificationTarget.Database:
					this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
					break;
				case EventNotificationTarget.Queue:
					this.GenerateSpaceAndIdentifier("QUEUE");
					this.GenerateSpaceAndFragmentIfNotNull(node.Scope.QueueName);
					break;
				}
			}
			if (node.WithFanIn)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("FAN_IN");
			}
			if (node.EventTypeGroups != null && node.EventTypeGroups.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpace();
				this.GenerateFragmentList<EventTypeGroupContainer>(node.EventTypeGroups);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.BrokerService);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.BrokerInstanceSpecifier);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000849C8 File Offset: 0x00082BC8
		public override void ExplicitVisit(CreateFullTextCatalogStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndIdentifier("CATALOG");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.FileGroup != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndIdentifier("FILEGROUP");
				this.GenerateSpaceAndFragmentIfNotNull(node.FileGroup);
			}
			if (node.Path != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.In);
				this.GenerateSpaceAndIdentifier("PATH");
				this.GenerateSpaceAndFragmentIfNotNull(node.Path);
			}
			this.GenerateCommaSeparatedWithClause<FullTextCatalogOption>(node.Options, true, false);
			if (node.IsDefault)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
			}
			this.GenerateOwnerIfNotNull(node.Owner);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00084A91 File Offset: 0x00082C91
		public override void ExplicitVisit(OnOffFullTextCatalogOption node)
		{
			this.GenerateOptionStateWithEqualSign("ACCENT_SENSITIVITY", node.OptionState);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00084AA4 File Offset: 0x00082CA4
		public override void ExplicitVisit(CreateFullTextIndexStatement node)
		{
			this.GenerateSpaceSeparatedTokens(TSqlTokenType.Create, new string[] { "FULLTEXT" });
			this.GenerateSpace();
			this.GenerateSpaceSeparatedTokens(new TSqlTokenType[]
			{
				TSqlTokenType.Index,
				TSqlTokenType.On
			});
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			if (node.FullTextIndexColumns != null && node.FullTextIndexColumns.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateParenthesisedCommaSeparatedList<FullTextIndexColumn>(node.FullTextIndexColumns);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndFragmentIfNotNull(node.KeyIndexName);
			this.GenerateFragmentIfNotNull(node.CatalogAndFileGroup);
			if (node.Options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateCommaSeparatedList<FullTextIndexOption>(node.Options);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00084B74 File Offset: 0x00082D74
		public override void ExplicitVisit(FullTextCatalogAndFileGroup node)
		{
			this.NewLineAndIndent();
			this.GenerateKeywordAndSpace(TSqlTokenType.On);
			if (node.FileGroupIsFirst)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateIdentifier("FILEGROUP");
				this.GenerateSpaceAndFragmentIfNotNull(node.FileGroupName);
				if (node.CatalogName != null)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
					this.GenerateFragmentIfNotNull(node.CatalogName);
				}
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				return;
			}
			if (node.FileGroupName != null)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			}
			this.GenerateFragmentIfNotNull(node.CatalogName);
			if (node.FileGroupName != null)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateIdentifier("FILEGROUP");
				this.GenerateSpaceAndFragmentIfNotNull(node.FileGroupName);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00084C38 File Offset: 0x00082E38
		public override void ExplicitVisit(ChangeTrackingFullTextIndexOption node)
		{
			this.GenerateIdentifier("CHANGE_TRACKING");
			this.GenerateSpace();
			switch (node.Value)
			{
			case ChangeTrackingOption.Auto:
				this.GenerateIdentifier("AUTO");
				return;
			case ChangeTrackingOption.Manual:
				this.GenerateIdentifier("MANUAL");
				return;
			case ChangeTrackingOption.Off:
				this.GenerateKeyword(TSqlTokenType.Off);
				return;
			case ChangeTrackingOption.OffNoPopulation:
				this.GenerateKeyword(TSqlTokenType.Off);
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndIdentifier("NO");
				this.GenerateSpaceAndIdentifier("POPULATION");
				return;
			default:
				return;
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00084CC0 File Offset: 0x00082EC0
		public override void ExplicitVisit(StopListFullTextIndexOption node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.StopList);
			if (node.IsOff)
			{
				this.GenerateKeyword(TSqlTokenType.Off);
				return;
			}
			this.GenerateFragmentIfNotNull(node.StopListName);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00084CEC File Offset: 0x00082EEC
		public override void ExplicitVisit(SearchPropertyListFullTextIndexOption node)
		{
			this.GenerateIdentifier("SEARCH");
			this.GenerateSpaceAndIdentifier("PROPERTY");
			this.GenerateSpaceAndIdentifier("LIST");
			this.GenerateSpaceAndKeyword(TSqlTokenType.EqualsSign);
			if (node.IsOff)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Off);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.PropertyListName);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00084D42 File Offset: 0x00082F42
		public override void ExplicitVisit(CreateFunctionStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateFunctionStatementBody(node);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00084D54 File Offset: 0x00082F54
		public override void ExplicitVisit(CreateIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			if (node.Unique)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Unique);
			}
			if (node.Clustered != null)
			{
				this.GenerateSpaceAndKeyword(node.Clustered.Value ? TSqlTokenType.Clustered : TSqlTokenType.NonClustered);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			this.GenerateParenthesisedCommaSeparatedList<ColumnWithSortOrder>(node.Columns);
			if (node.IncludeColumns != null && node.IncludeColumns.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("INCLUDE");
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.IncludeColumns);
			}
			if (node.FilterPredicate != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Where);
				this.GenerateSpaceAndFragmentIfNotNull(node.FilterPredicate);
			}
			this.GenerateIndexOptions(node.IndexOptions);
			if (node.OnFileGroupOrPartitionScheme != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroupOrPartitionScheme);
			}
			this.GenerateFileStreamOn(node);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00084E69 File Offset: 0x00083069
		protected virtual void GenerateIndexOptions(IList<IndexOption> options)
		{
			if (options != null && options.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<IndexOption>(options);
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00084E8F File Offset: 0x0008308F
		private void GenerateFileStreamOn(IFileStreamSpecifier node)
		{
			if (node.FileStreamOn != null)
			{
				this.GenerateSpaceAndIdentifier("FILESTREAM_ON");
				this.GenerateSpaceAndFragmentIfNotNull(node.FileStreamOn);
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00084EB0 File Offset: 0x000830B0
		public override void ExplicitVisit(CreateLoginStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("LOGIN");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateFragmentIfNotNull(node.Source);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00084EE4 File Offset: 0x000830E4
		public override void ExplicitVisit(CreateMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndIdentifier("ENCRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpaceAndIdentifier("PASSWORD");
			this.GenerateSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Password);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00084F41 File Offset: 0x00083141
		public override void ExplicitVisit(CreateMessageTypeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("MESSAGE");
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.GenerateValidationMethod(node);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00084F80 File Offset: 0x00083180
		public override void ExplicitVisit(CreatePartitionFunctionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Function);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateParenthesisedFragmentIfNotNull(node.ParameterType);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndIdentifier("RANGE");
			switch (node.Range)
			{
			case PartitionFunctionRange.Left:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Left);
				break;
			case PartitionFunctionRange.Right:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Right);
				break;
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Values);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.BoundaryValues, true);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00085033 File Offset: 0x00083233
		public override void ExplicitVisit(PartitionParameterType node)
		{
			this.GenerateFragmentIfNotNull(node.DataType);
			if (node.Collation != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Collate);
				this.GenerateSpaceAndFragmentIfNotNull(node.Collation);
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00085060 File Offset: 0x00083260
		public override void ExplicitVisit(CreatePartitionSchemeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndIdentifier("SCHEME");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndFragmentIfNotNull(node.PartitionFunction);
			this.NewLineAndIndent();
			if (node.IsAll)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				this.GenerateSpace();
			}
			this.GenerateKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<IdentifierOrValueExpression>(node.FileGroups);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000850F4 File Offset: 0x000832F4
		public override void ExplicitVisit(CreateProcedureStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateProcedureStatementBody(node);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00085108 File Offset: 0x00083308
		public override void ExplicitVisit(CreateQueueStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("QUEUE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.QueueOptions != null && node.QueueOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateQueueOptions(node.QueueOptions);
			}
			if (node.OnFileGroup != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroup);
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00085190 File Offset: 0x00083390
		public override void ExplicitVisit(CreateRemoteServiceBindingStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("REMOTE");
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndIdentifier("BINDING");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Service);
			this.GenerateBindingOptions(node.Options);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00085212 File Offset: 0x00083412
		public override void ExplicitVisit(CreateRoleStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("ROLE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0008523F File Offset: 0x0008343F
		public override void ExplicitVisit(CreateRouteStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("ROUTE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.GenerateRouteOptions(node);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00085273 File Offset: 0x00083473
		public override void ExplicitVisit(CreateRuleStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Rule);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000852B0 File Offset: 0x000834B0
		public override void ExplicitVisit(CreateSchemaStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			if (node.StatementList != null && node.StatementList.Statements != null && node.StatementList.Statements.Count > 0)
			{
				AlignmentPoint alignmentPoint = new AlignmentPoint();
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				bool generateSemiColon = this._generateSemiColon;
				this._generateSemiColon = false;
				this.GenerateFragmentIfNotNull(node.StatementList);
				this._generateSemiColon = generateSemiColon;
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0008534C File Offset: 0x0008354C
		public override void ExplicitVisit(CreateServiceStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndIdentifier("QUEUE");
			this.GenerateSpaceAndFragmentIfNotNull(node.QueueName);
			if (node.ServiceContracts != null && node.ServiceContracts.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateParenthesisedCommaSeparatedList<ServiceContract>(node.ServiceContracts);
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000853D4 File Offset: 0x000835D4
		public override void ExplicitVisit(CreateStatisticsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Statistics);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
			if (node.FilterPredicate != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Where);
				this.GenerateSpaceAndFragmentIfNotNull(node.FilterPredicate);
			}
			if (node.StatisticsOptions != null && node.StatisticsOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<StatisticsOption>(node.StatisticsOptions);
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00085480 File Offset: 0x00083680
		public override void ExplicitVisit(CreateSymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSymmetricKeyName(node.Name);
			this.GenerateOwnerIfNotNull(node.Owner);
			if (node.Provider != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
				this.GenerateSpaceAndIdentifier("PROVIDER");
				this.GenerateSpaceAndFragmentIfNotNull(node.Provider);
			}
			if (node.KeyOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateCommaSeparatedList<KeyOption>(node.KeyOptions);
			}
			if (node.EncryptingMechanisms.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("ENCRYPTION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<CryptoMechanism>(node.EncryptingMechanisms);
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00085538 File Offset: 0x00083738
		public override void ExplicitVisit(AlterSymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Alter);
			this.GenerateSymmetricKeyName(node.Name);
			this.GenerateSpaceAndKeyword(node.IsAdd ? TSqlTokenType.Add : TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("ENCRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<CryptoMechanism>(node.EncryptingMechanisms);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00085590 File Offset: 0x00083790
		public override void ExplicitVisit(DropSymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSymmetricKeyName(node.Name);
			this.GenerateRemoveProviderKeyOpt(node.RemoveProviderKey);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000855B2 File Offset: 0x000837B2
		public override void ExplicitVisit(AlgorithmKeyOption node)
		{
			this.GenerateTokenAndEqualSign("ALGORITHM");
			this.GenerateSpace();
			EncryptionAlgorithmsHelper.Instance.GenerateSourceForOption(this._writer, node.Algorithm);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000855DB File Offset: 0x000837DB
		public override void ExplicitVisit(IdentityValueKeyOption node)
		{
			this.GenerateNameEqualsValue("IDENTITY_VALUE", node.IdentityPhrase);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000855EE File Offset: 0x000837EE
		public override void ExplicitVisit(KeySourceKeyOption node)
		{
			this.GenerateNameEqualsValue("KEY_SOURCE", node.PassPhrase);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00085601 File Offset: 0x00083801
		public override void ExplicitVisit(ProviderKeyNameKeyOption node)
		{
			this.GenerateNameEqualsValue("PROVIDER_KEY_NAME", node.KeyName);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00085614 File Offset: 0x00083814
		public override void ExplicitVisit(CreationDispositionKeyOption node)
		{
			this.GenerateTokenAndEqualSign("CREATION_DISPOSITION");
			this.GenerateSpaceAndIdentifier(node.IsCreateNew ? "CREATE_NEW" : "OPEN_EXISTING");
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0008563B File Offset: 0x0008383B
		private void GenerateSymmetricKeyName(Identifier name)
		{
			this.GenerateSpaceAndIdentifier("SYMMETRIC");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndFragmentIfNotNull(name);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00085657 File Offset: 0x00083857
		public override void ExplicitVisit(CreateSynonymStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("SYNONYM");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndFragmentIfNotNull(node.ForName);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0008568C File Offset: 0x0008388C
		public override void ExplicitVisit(CreateTriggerStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpace();
			this.GenerateTriggerStatementBody(node);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000856A4 File Offset: 0x000838A4
		public override void ExplicitVisit(CreateTypeUddtStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			this.GenerateSpaceAndFragmentIfNotNull(node.NullableConstraint);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000856F8 File Offset: 0x000838F8
		public override void ExplicitVisit(CreateTypeUdtStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateSpaceAndKeyword(TSqlTokenType.External);
			this.GenerateSpaceAndIdentifier("NAME");
			this.GenerateSpaceAndFragmentIfNotNull(node.AssemblyName);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0008574C File Offset: 0x0008394C
		public override void ExplicitVisit(CreateUserStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndKeyword(TSqlTokenType.User);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndFragmentIfNotNull(node.UserLoginOption);
			if (node.UserOptions != null && node.UserOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<PrincipalOption>(node.UserOptions);
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000857BD File Offset: 0x000839BD
		public override void ExplicitVisit(CreateViewStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpace();
			this.GenerateViewStatementBody(node);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000857D4 File Offset: 0x000839D4
		public override void ExplicitVisit(CreateXmlIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			if (node.Primary)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Primary);
			}
			this.GenerateSpaceAndIdentifier("XML");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.OnName);
			this.GenerateParenthesisedFragmentIfNotNull(node.XmlColumn);
			if (node.SecondaryXmlIndexName != null)
			{
				this.NewLineAndIndent();
				this.GenerateIdentifier("USING");
				this.GenerateSpaceAndIdentifier("XML");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
				this.GenerateSpaceAndFragmentIfNotNull(node.SecondaryXmlIndexName);
				if (node.SecondaryXmlIndexType != SecondaryXmlIndexType.NotSpecified)
				{
					string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SecondaryXmlIndexType, string>(SqlScriptGeneratorVisitor._secondaryXmlIndexTypeNames, node.SecondaryXmlIndexType);
					this.GenerateSpaceAndKeyword(TSqlTokenType.For);
					this.GenerateSpaceAndIdentifier(valueForEnumKey);
				}
			}
			if (node.IndexOptions != null && node.IndexOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<IndexOption>(node.IndexOptions);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000858D8 File Offset: 0x00083AD8
		public override void ExplicitVisit(CreateXmlSchemaCollectionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Create);
			this.GenerateSpaceAndIdentifier("XML");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndIdentifier("COLLECTION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00085934 File Offset: 0x00083B34
		public override void ExplicitVisit(CryptoMechanism node)
		{
			switch (node.CryptoMechanismType)
			{
			case CryptoMechanismType.Certificate:
				this.GenerateIdentifier("CERTIFICATE");
				this.GenerateIdentifierWithPassword(node);
				return;
			case CryptoMechanismType.AsymmetricKey:
				this.GenerateIdentifier("ASYMMETRIC");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
				this.GenerateIdentifierWithPassword(node);
				return;
			case CryptoMechanismType.SymmetricKey:
				this.GenerateIdentifier("SYMMETRIC");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
				this.GenerateIdentifierWithPassword(node);
				return;
			case CryptoMechanismType.Password:
				this.GenerateNameEqualsValue("PASSWORD", node.PasswordOrSignature);
				return;
			default:
				return;
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000859BC File Offset: 0x00083BBC
		private void GenerateIdentifierWithPassword(CryptoMechanism node)
		{
			this.GenerateSpaceAndFragmentIfNotNull(node.Identifier);
			if (node.PasswordOrSignature != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				string text = ((node.PasswordOrSignature.LiteralType == LiteralType.Binary) ? "SIGNATURE" : "PASSWORD");
				this.GenerateSpace();
				this.GenerateNameEqualsValue(text, node.PasswordOrSignature);
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00085A16 File Offset: 0x00083C16
		public override void ExplicitVisit(CursorDefaultDatabaseOption node)
		{
			this.GenerateIdentifier("CURSOR_DEFAULT");
			this.GenerateSpaceAndIdentifier(node.IsLocal ? "LOCAL" : "GLOBAL");
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00085A40 File Offset: 0x00083C40
		public override void ExplicitVisit(CursorDefinition node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeyword(TSqlTokenType.Cursor);
			foreach (CursorOption cursorOption in node.Options)
			{
				if (cursorOption.OptionKind != CursorOptionKind.Insensitive)
				{
					this.GenerateFragmentIfNotNull(cursorOption);
				}
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.For);
			this.GenerateSpace();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			bool generateSemiColon = this._generateSemiColon;
			this._generateSemiColon = false;
			this.GenerateFragmentIfNotNull(node.Select);
			this._generateSemiColon = generateSemiColon;
			this.PopAlignmentPoint();
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00085AE8 File Offset: 0x00083CE8
		public override void ExplicitVisit(CursorOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<CursorOptionKind, string>(SqlScriptGeneratorVisitor._cursorOptionsNames, node.OptionKind);
			this.GenerateSpaceAndIdentifier(valueForEnumKey);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00085B0D File Offset: 0x00083D0D
		public override void ExplicitVisit(CursorId node)
		{
			if (node.IsGlobal)
			{
				this.GenerateIdentifier("GLOBAL");
				this.GenerateSpace();
			}
			this.GenerateFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00085B34 File Offset: 0x00083D34
		public override void ExplicitVisit(DbccNamedLiteral node)
		{
			if (node.Name != null)
			{
				this.GenerateNameEqualsValue(node.Name, node.Value);
				return;
			}
			this.GenerateFragmentIfNotNull(node.Value);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00085B60 File Offset: 0x00083D60
		public override void ExplicitVisit(DbccStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Dbcc);
			if (node.Command == DbccCommand.Free)
			{
				this.GenerateSpace();
				this.GenerateIdentifierWithoutCasing(node.DllName);
			}
			else
			{
				string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DbccCommand, string>(SqlScriptGeneratorVisitor._dbccCommandNames, node.Command);
				if (valueForEnumKey != null)
				{
					this.GenerateSpaceAndIdentifier(valueForEnumKey);
				}
			}
			if (node.ParenthesisRequired || node.Literals.Count > 0)
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateCommaSeparatedList<DbccNamedLiteral>(node.Literals);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
			if (node.Options != null && node.Options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				if (node.OptionsUseJoin)
				{
					this.GenerateJoinSeparatedList<DbccOption>(node.Options);
					return;
				}
				this.GenerateCommaSeparatedList<DbccOption>(node.Options);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00085C34 File Offset: 0x00083E34
		public override void ExplicitVisit(DbccOption node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DbccOptionKind, TokenGenerator>(SqlScriptGeneratorVisitor._dbccOptionsGenerators, node.OptionKind);
			this.GenerateToken(valueForEnumKey);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00085C6F File Offset: 0x00083E6F
		protected void GenerateJoinSeparatedList<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateList<T>(list, delegate
			{
				this.GenerateSpace();
				this.GenerateSymbol(TSqlTokenType.Join);
				this.GenerateSpace();
			});
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00085C84 File Offset: 0x00083E84
		public override void ExplicitVisit(DeallocateCursorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Deallocate);
			this.GenerateSpaceAndFragmentIfNotNull(node.Cursor);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00085C9C File Offset: 0x00083E9C
		public override void ExplicitVisit(DeclareCursorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Declare);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.CursorDefinition != null && node.CursorDefinition.Options != null)
			{
				foreach (CursorOption cursorOption in node.CursorDefinition.Options)
				{
					if (cursorOption.OptionKind == CursorOptionKind.Insensitive)
					{
						this.GenerateFragmentIfNotNull(cursorOption);
					}
				}
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.CursorDefinition);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00085D2C File Offset: 0x00083F2C
		public override void ExplicitVisit(DeclareTableVariableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Declare);
			this.GenerateSpaceAndFragmentIfNotNull(node.Body);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00085D44 File Offset: 0x00083F44
		public override void ExplicitVisit(DeclareVariableElement node)
		{
			this.GenerateFragmentIfNotNull(node.VariableName);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			if (node.Value != null)
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.Value);
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00085D90 File Offset: 0x00083F90
		public override void ExplicitVisit(DeclareVariableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Declare);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<DeclareVariableElement>(node.Declarations);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00085DAC File Offset: 0x00083FAC
		public override void ExplicitVisit(DefaultConstraintDefinition node)
		{
			this.GenerateConstraintHead(node);
			this.GenerateKeywordAndSpace(TSqlTokenType.Default);
			this.GenerateFragmentIfNotNull(node.Expression);
			if (node.Column != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndFragmentIfNotNull(node.Column);
			}
			if (node.WithValues)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Values);
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00085E0E File Offset: 0x0008400E
		public override void ExplicitVisit(DenyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Deny);
			this.GenerateSpace();
			this.GeneratePermissionOnToClauses(node);
			if (node.CascadeOption)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Cascade);
			}
			this.GenerateAsClause(node);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00085E3C File Offset: 0x0008403C
		public override void ExplicitVisit(DenyStatement80 node)
		{
			this.GenerateKeyword(TSqlTokenType.Deny);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityElement80);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityUserClause80);
			if (node.CascadeOption)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.Cascade);
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00085E90 File Offset: 0x00084090
		public override void ExplicitVisit(DeviceInfo node)
		{
			if (node.LogicalDevice != null)
			{
				this.GenerateFragmentIfNotNull(node.LogicalDevice);
				return;
			}
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DeviceType, TokenGenerator>(SqlScriptGeneratorVisitor._deviceTypeGenerators, node.DeviceType);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.PhysicalDevice);
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00085ED3 File Offset: 0x000840D3
		public override void ExplicitVisit(MirrorToClause node)
		{
			this.GenerateIdentifier("MIRROR");
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<DeviceInfo>(node.Devices);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00085EFD File Offset: 0x000840FD
		public override void ExplicitVisit(DropAggregateStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("AGGREGATE");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00085F24 File Offset: 0x00084124
		public override void ExplicitVisit(DropApplicationRoleStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("APPLICATION");
			this.GenerateSpaceAndIdentifier("ROLE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00085F50 File Offset: 0x00084150
		public override void ExplicitVisit(DropAssemblyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("ASSEMBLY");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
			if (node.WithNoDependents)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("NO");
				this.GenerateSpaceAndIdentifier("DEPENDENTS");
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00085FB1 File Offset: 0x000841B1
		public override void ExplicitVisit(DropCertificateStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("CERTIFICATE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00085FD4 File Offset: 0x000841D4
		protected void GenerateOptionHeader(DropClusteredConstraintOption node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DropClusteredConstraintOptionKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._dropClusteredConstraintOptionTypeGenerators, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00085FFC File Offset: 0x000841FC
		public override void ExplicitVisit(DropClusteredConstraintValueOption node)
		{
			this.GenerateOptionHeader(node);
			this.GenerateSpaceAndFragmentIfNotNull(node.OptionValue);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00086011 File Offset: 0x00084211
		public override void ExplicitVisit(DropClusteredConstraintMoveOption node)
		{
			this.GenerateOptionHeader(node);
			this.GenerateSpaceAndFragmentIfNotNull(node.OptionValue);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00086026 File Offset: 0x00084226
		public override void ExplicitVisit(DropClusteredConstraintStateOption node)
		{
			this.GenerateOptionHeader(node);
			this.GenerateSpace();
			this.GenerateOptionStateOnOff(node.OptionState);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00086041 File Offset: 0x00084241
		public override void ExplicitVisit(DropContractStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("CONTRACT");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00086062 File Offset: 0x00084262
		public override void ExplicitVisit(DropCredentialStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("CREDENTIAL");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00086083 File Offset: 0x00084283
		public override void ExplicitVisit(DropDatabaseStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<Identifier>(node.Databases);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000860A7 File Offset: 0x000842A7
		public override void ExplicitVisit(DropDefaultStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000860CB File Offset: 0x000842CB
		public override void ExplicitVisit(DropEndpointStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("ENDPOINT");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000860EC File Offset: 0x000842EC
		public override void ExplicitVisit(DropEventNotificationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("EVENT");
			this.GenerateSpaceAndIdentifier("NOTIFICATION");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<Identifier>(node.Notifications);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.Scope);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00086143 File Offset: 0x00084343
		public override void ExplicitVisit(DropFullTextCatalogStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndIdentifier("CATALOG");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0008616F File Offset: 0x0008436F
		public override void ExplicitVisit(DropFullTextIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("FULLTEXT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpaceAndKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.TableName);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000861A0 File Offset: 0x000843A0
		public override void ExplicitVisit(DropFunctionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Function);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000861C4 File Offset: 0x000843C4
		public override void ExplicitVisit(DropIndexClause node)
		{
			this.GenerateFragmentIfNotNull(node.Index);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.Object);
			this.GenerateCommaSeparatedWithClause<IndexOption>(node.Options, true, true);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000861FA File Offset: 0x000843FA
		public override void ExplicitVisit(MoveToDropIndexOption node)
		{
			this.GenerateIdentifier("MOVE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.MoveTo);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0008621E File Offset: 0x0008441E
		public override void ExplicitVisit(FileStreamOnDropIndexOption node)
		{
			this.GenerateIdentifier("FILESTREAM_ON");
			this.GenerateSpaceAndFragmentIfNotNull(node.FileStreamOn);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00086237 File Offset: 0x00084437
		public override void ExplicitVisit(DropIndexStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Index);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<DropIndexClauseBase>(node.DropIndexClauses);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0008625B File Offset: 0x0008445B
		public override void ExplicitVisit(DropLoginStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("LOGIN");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0008627C File Offset: 0x0008447C
		public override void ExplicitVisit(DropMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00086299 File Offset: 0x00084499
		public override void ExplicitVisit(DropMessageTypeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("MESSAGE");
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000862C5 File Offset: 0x000844C5
		public override void ExplicitVisit(DropPartitionFunctionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Function);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000862EE File Offset: 0x000844EE
		public override void ExplicitVisit(DropPartitionSchemeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("PARTITION");
			this.GenerateSpaceAndIdentifier("SCHEME");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0008631A File Offset: 0x0008451A
		public override void ExplicitVisit(DropProcedureStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Procedure);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0008633E File Offset: 0x0008453E
		public override void ExplicitVisit(DropQueueStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("QUEUE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0008635F File Offset: 0x0008455F
		public override void ExplicitVisit(DropRemoteServiceBindingStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("REMOTE");
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndIdentifier("BINDING");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00086396 File Offset: 0x00084596
		public override void ExplicitVisit(DropRoleStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("ROLE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000863B7 File Offset: 0x000845B7
		public override void ExplicitVisit(DropRouteStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("ROUTE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000863D8 File Offset: 0x000845D8
		public override void ExplicitVisit(DropRuleStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Rule);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00086400 File Offset: 0x00084600
		public override void ExplicitVisit(DropSchemaStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndFragmentIfNotNull(node.Schema);
			if (node.DropBehavior == DropSchemaBehavior.Cascade)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Cascade);
				return;
			}
			if (node.DropBehavior == DropSchemaBehavior.Restrict)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Restrict);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00086452 File Offset: 0x00084652
		public override void ExplicitVisit(DropServiceStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("SERVICE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00086473 File Offset: 0x00084673
		public override void ExplicitVisit(DropSignatureStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateCounterSignature(node);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpace();
			this.GenerateModule(node);
			this.NewLineAndIndent();
			this.GenerateCryptos(node);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000864A6 File Offset: 0x000846A6
		public override void ExplicitVisit(DropStatisticsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Statistics);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<ChildObjectName>(node.Objects);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000864CD File Offset: 0x000846CD
		public override void ExplicitVisit(DropSynonymStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("SYNONYM");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000864F4 File Offset: 0x000846F4
		public override void ExplicitVisit(DropTableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0008651C File Offset: 0x0008471C
		public override void ExplicitVisit(DropTriggerStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Trigger);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
			switch (node.TriggerScope)
			{
			case TriggerScope.Normal:
				break;
			case TriggerScope.Database:
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
				return;
			case TriggerScope.AllServer:
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
				this.GenerateSpaceAndIdentifier("SERVER");
				break;
			default:
				return;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0008659F File Offset: 0x0008479F
		public override void ExplicitVisit(DropTypeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("TYPE");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000865C0 File Offset: 0x000847C0
		public override void ExplicitVisit(DropUserStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.User);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000865E1 File Offset: 0x000847E1
		public override void ExplicitVisit(DropViewStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndKeyword(TSqlTokenType.View);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SchemaObjectName>(node.Objects);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00086608 File Offset: 0x00084808
		public override void ExplicitVisit(DropXmlSchemaCollectionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Drop);
			this.GenerateSpaceAndIdentifier("XML");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
			this.GenerateSpaceAndIdentifier("COLLECTION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00086640 File Offset: 0x00084840
		public override void ExplicitVisit(EnabledDisabledPayloadOption node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PayloadOptionKinds, TokenGenerator>(SqlScriptGeneratorVisitor._payloadOptionKindsGenerators, node.Kind);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.IsEnabled ? "ENABLED" : "DISABLED");
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0008667C File Offset: 0x0008487C
		public override void ExplicitVisit(EnableDisableTriggerStatement node)
		{
			this.GenerateTriggerEnforcement(node.TriggerEnforcement);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Trigger);
			this.GenerateSpace();
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
			}
			else
			{
				this.GenerateCommaSeparatedList<SchemaObjectName>(node.TriggerNames);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.TriggerObject);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000866E0 File Offset: 0x000848E0
		public override void ExplicitVisit(EncryptionPayloadOption node)
		{
			this.GenerateTokenAndEqualSign("ENCRYPTION");
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<EndpointEncryptionSupport, TokenGenerator>(SqlScriptGeneratorVisitor._endpointEncryptionSupportGenerators, node.EncryptionSupport);
			if (valueForEnumKey != null)
			{
				this.GenerateSpace();
				this.GenerateToken(valueForEnumKey);
			}
			if (node.EncryptionSupport != EndpointEncryptionSupport.Disabled && node.EncryptionSupport != EndpointEncryptionSupport.NotSpecified)
			{
				if (node.AlgorithmPartOne != EncryptionAlgorithmPreference.NotSpecified || node.AlgorithmPartTwo != EncryptionAlgorithmPreference.NotSpecified)
				{
					this.GenerateSpaceAndIdentifier("ALGORITHM");
				}
				this.GenerateSpaceAndAlgorithm(node.AlgorithmPartOne);
				this.GenerateSpaceAndAlgorithm(node.AlgorithmPartTwo);
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00086760 File Offset: 0x00084960
		private void GenerateSpaceAndAlgorithm(EncryptionAlgorithmPreference alg)
		{
			switch (alg)
			{
			case EncryptionAlgorithmPreference.NotSpecified:
				break;
			case EncryptionAlgorithmPreference.Aes:
				this.GenerateSpaceAndIdentifier("AES");
				return;
			case EncryptionAlgorithmPreference.Rc4:
				this.GenerateSpaceAndIdentifier("RC4");
				break;
			default:
				return;
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0008679C File Offset: 0x0008499C
		public override void ExplicitVisit(EndConversationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.End);
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Conversation);
			if (node.WithCleanup)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("CLEANUP");
				return;
			}
			if (node.ErrorCode != null && node.ErrorDescription != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("ERROR", node.ErrorCode);
				this.GenerateSpace();
				this.GenerateNameEqualsValue("DESCRIPTION", node.ErrorDescription);
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0008683C File Offset: 0x00084A3C
		public override void ExplicitVisit(EndpointAffinity node)
		{
			switch (node.Kind)
			{
			case AffinityKind.None:
				this.GenerateNameEqualsValue("AFFINITY", "NONE");
				return;
			case AffinityKind.Integer:
				this.GenerateNameEqualsValue("AFFINITY", node.Value);
				return;
			case AffinityKind.Admin:
				this.GenerateNameEqualsValue("AFFINITY", "ADMIN");
				return;
			default:
				return;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00086898 File Offset: 0x00084A98
		public override void ExplicitVisit(EventNotificationObjectScope node)
		{
			switch (node.Target)
			{
			case EventNotificationTarget.Server:
				this.GenerateIdentifier("SERVER");
				return;
			case EventNotificationTarget.Database:
				this.GenerateKeyword(TSqlTokenType.Database);
				return;
			case EventNotificationTarget.Queue:
				this.GenerateIdentifier("QUEUE");
				this.GenerateSpaceAndFragmentIfNotNull(node.QueueName);
				return;
			default:
				return;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000868ED File Offset: 0x00084AED
		protected void GenerateParameters(ExecutableEntity node)
		{
			this.GenerateCommaSeparatedList<ExecuteParameter>(node.Parameters);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000868FB File Offset: 0x00084AFB
		public override void ExplicitVisit(ExecutableProcedureReference node)
		{
			if (node.AdHocDataSource != null)
			{
				this.GenerateFragmentIfNotNull(node.AdHocDataSource);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			this.GenerateFragmentIfNotNull(node.ProcedureReference);
			this.GenerateSpace();
			this.GenerateParameters(node);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00086938 File Offset: 0x00084B38
		public override void ExplicitVisit(ExecutableStringList node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			for (int i = 0; i < node.Strings.Count; i++)
			{
				if (i > 0)
				{
					this.GenerateSpaceAndSymbol(TSqlTokenType.Plus);
					this.GenerateSpace();
				}
				this.GenerateFragmentIfNotNull(node.Strings[i]);
			}
			if (node.Parameters.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateParameters(node);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000869B8 File Offset: 0x00084BB8
		public override void ExplicitVisit(ExecuteAsClause node)
		{
			this.GenerateKeyword(TSqlTokenType.Execute);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			switch (node.ExecuteAsOption)
			{
			case ExecuteAsOption.Caller:
				this.GenerateSpaceAndIdentifier("CALLER");
				return;
			case ExecuteAsOption.Self:
				this.GenerateSpaceAndIdentifier("SELF");
				return;
			case ExecuteAsOption.Owner:
				this.GenerateSpaceAndIdentifier("OWNER");
				return;
			case ExecuteAsOption.String:
			case ExecuteAsOption.Login:
			case ExecuteAsOption.User:
				this.GenerateSpaceAndFragmentIfNotNull(node.Literal);
				return;
			default:
				return;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00086A2C File Offset: 0x00084C2C
		public override void ExplicitVisit(ExecuteAsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Execute);
			this.GenerateSpaceAndFragmentIfNotNull(node.ExecuteContext);
			if (node.WithNoRevert)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("NO");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Revert);
				return;
			}
			if (node.Cookie != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("COOKIE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Into);
				this.GenerateSpaceAndFragmentIfNotNull(node.Cookie);
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00086AB8 File Offset: 0x00084CB8
		public override void ExplicitVisit(ExecuteContext node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.As);
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<ExecuteAsOption, TokenGenerator>(SqlScriptGeneratorVisitor._executeAsOptionGenerators, node.Kind);
			if (node.Principal != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.Principal);
				return;
			}
			this.GenerateToken(valueForEnumKey);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00086AFC File Offset: 0x00084CFC
		public override void ExplicitVisit(ExecuteParameter node)
		{
			if (node.Variable != null)
			{
				this.GenerateFragmentIfNotNull(node.Variable);
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpace();
			}
			if (node.ParameterValue != null)
			{
				this.GenerateFragmentIfNotNull(node.ParameterValue);
			}
			if (node.IsOutput)
			{
				this.GenerateSpaceAndIdentifier("OUTPUT");
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00086B55 File Offset: 0x00084D55
		public override void ExplicitVisit(ExecuteStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Execute);
			this.GenerateExecuteSpecificationBody(node.ExecuteSpecification);
			this.GenerateCommaSeparatedWithClause<ExecuteOption>(node.Options, true, false);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00086B79 File Offset: 0x00084D79
		public override void ExplicitVisit(ExecuteSpecification node)
		{
			this.GenerateKeyword(TSqlTokenType.Execute);
			this.GenerateExecuteSpecificationBody(node);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00086B8C File Offset: 0x00084D8C
		private void GenerateExecuteSpecificationBody(ExecuteSpecification node)
		{
			if (node.Variable != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Variable);
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.ExecutableEntity);
			this.GenerateSpaceAndFragmentIfNotNull(node.ExecuteContext);
			if (node.LinkedServer != null)
			{
				this.GenerateSpaceAndIdentifier("AT");
				this.GenerateSpaceAndFragmentIfNotNull(node.LinkedServer);
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00086BEF File Offset: 0x00084DEF
		public override void ExplicitVisit(ExecuteOption node)
		{
			this.GenerateIdentifier("RECOMPILE");
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00086BFC File Offset: 0x00084DFC
		public override void ExplicitVisit(ExistsPredicate node)
		{
			this.GenerateKeyword(TSqlTokenType.Exists);
			this.GenerateSpaceAndFragmentIfNotNull(node.Subquery);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00086C12 File Offset: 0x00084E12
		public override void ExplicitVisit(ExtractFromExpression node)
		{
			this.GenerateIdentifier("EXTRACT");
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00086C34 File Offset: 0x00084E34
		public override void ExplicitVisit(FetchCursorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Fetch);
			if (node.FetchType != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.FetchType);
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Cursor);
			if (node.IntoVariables.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Into);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<VariableReference>(node.IntoVariables);
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00086C9C File Offset: 0x00084E9C
		public override void ExplicitVisit(FetchType node)
		{
			if (node.Orientation != FetchOrientation.None)
			{
				string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<FetchOrientation, string>(SqlScriptGeneratorVisitor._fetchOrientationNames, node.Orientation);
				if (valueForEnumKey != null)
				{
					this.GenerateIdentifier(valueForEnumKey);
					if (node.Orientation == FetchOrientation.Absolute || node.Orientation == FetchOrientation.Relative)
					{
						this.GenerateSpaceAndFragmentIfNotNull(node.RowOffset);
					}
				}
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00086CEA File Offset: 0x00084EEA
		public override void ExplicitVisit(FileDeclaration node)
		{
			if (node.IsPrimary)
			{
				this.GenerateKeyword(TSqlTokenType.Primary);
			}
			this.GenerateParenthesisedCommaSeparatedList<FileDeclarationOption>(node.Options);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00086D08 File Offset: 0x00084F08
		public override void ExplicitVisit(NameFileDeclarationOption node)
		{
			string text = (node.IsNewName ? "NEWNAME" : "NAME");
			this.GenerateNameEqualsValue(text, node.LogicalFileName);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00086D37 File Offset: 0x00084F37
		public override void ExplicitVisit(FileNameFileDeclarationOption node)
		{
			this.GenerateNameEqualsValue("FILENAME", node.OSFileName);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00086D4A File Offset: 0x00084F4A
		public override void ExplicitVisit(SizeFileDeclarationOption node)
		{
			this.GenerateNameEqualsValue("SIZE", node.Size);
			this.GenerateSpaceAndMemoryUnit(node.Units);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00086D69 File Offset: 0x00084F69
		public override void ExplicitVisit(MaxSizeFileDeclarationOption node)
		{
			if (node.MaxSize != null)
			{
				this.GenerateNameEqualsValue("MAXSIZE", node.MaxSize);
				this.GenerateSpaceAndMemoryUnit(node.Units);
			}
			if (node.Unlimited)
			{
				this.GenerateNameEqualsValue("MAXSIZE", "UNLIMITED");
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00086DA8 File Offset: 0x00084FA8
		public override void ExplicitVisit(FileGrowthFileDeclarationOption node)
		{
			this.GenerateNameEqualsValue("FILEGROWTH", node.GrowthIncrement);
			this.GenerateSpaceAndMemoryUnit(node.Units);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00086DC8 File Offset: 0x00084FC8
		public override void ExplicitVisit(FileDeclarationOption node)
		{
			FileDeclarationOptionKind optionKind = node.OptionKind;
			if (optionKind != FileDeclarationOptionKind.Offline)
			{
				return;
			}
			this.GenerateIdentifier("OFFLINE");
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00086DEC File Offset: 0x00084FEC
		public override void ExplicitVisit(FileGroupDefinition node)
		{
			this.NewLineAndIndent();
			if (node.Name != null)
			{
				this.GenerateIdentifier("FILEGROUP");
				this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			}
			if (node.ContainsFileStream)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Contains);
				this.GenerateSpaceAndIdentifier("FILESTREAM");
			}
			if (node.IsDefault)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Default);
			}
			this.GenerateCommaSeparatedList<FileDeclaration>(node.FileDeclarations);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00086E55 File Offset: 0x00085055
		public override void ExplicitVisit(FileGroupOrPartitionScheme node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			if (node.PartitionSchemeColumns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.PartitionSchemeColumns);
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00086E84 File Offset: 0x00085084
		public override void ExplicitVisit(ForeignKeyConstraintDefinition node)
		{
			this.GenerateConstraintHead(node);
			this.GenerateKeyword(TSqlTokenType.Foreign);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			if (node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.References);
			this.GenerateSpaceAndFragmentIfNotNull(node.ReferenceTableName);
			if (node.ReferencedTableColumns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.ReferencedTableColumns);
			}
			if (node.DeleteAction != DeleteUpdateAction.NotSpecified)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Delete);
				this.GenerateSpace();
				this.GenerateDeleteUpdateAction(node.DeleteAction);
			}
			if (node.UpdateAction != DeleteUpdateAction.NotSpecified)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Update);
				this.GenerateSpace();
				this.GenerateDeleteUpdateAction(node.UpdateAction);
			}
			if (node.NotForReplication)
			{
				this.GenerateSpace();
				this.GenerateNotForReplication();
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00086F68 File Offset: 0x00085168
		public override void ExplicitVisit(FullTextIndexColumn node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			if (node.TypeColumn != null)
			{
				this.GenerateSpaceAndIdentifier("TYPE");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Column);
				this.GenerateSpaceAndFragmentIfNotNull(node.TypeColumn);
			}
			if (node.LanguageTerm != null)
			{
				this.GenerateSpaceAndIdentifier("LANGUAGE");
				this.GenerateSpaceAndFragmentIfNotNull(node.LanguageTerm);
			}
			if (node.StatisticalSemantics)
			{
				this.GenerateSpaceAndIdentifier("STATISTICAL_SEMANTICS");
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00086FDC File Offset: 0x000851DC
		public override void ExplicitVisit(FullTextPredicate node)
		{
			if (node.FullTextFunctionType != FullTextFunctionType.None)
			{
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<FullTextFunctionType, TokenGenerator>(SqlScriptGeneratorVisitor._fulltextFunctionTypeGenerators, node.FullTextFunctionType);
				if (valueForEnumKey != null)
				{
					this.GenerateToken(valueForEnumKey);
				}
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			if (node.PropertyName != null)
			{
				this.GenerateIdentifier("PROPERTY");
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.PropertyName);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
			else
			{
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
			}
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateFragmentIfNotNull(node.Value);
			if (node.LanguageTerm != null)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateIdentifier("LANGUAGE");
				this.GenerateSpaceAndFragmentIfNotNull(node.LanguageTerm);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000870C0 File Offset: 0x000852C0
		public override void ExplicitVisit(FullTextTableReference node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			switch (node.FullTextFunctionType)
			{
			case FullTextFunctionType.Contains:
				this.GenerateKeyword(TSqlTokenType.ContainsTable);
				break;
			case FullTextFunctionType.FreeText:
				this.GenerateKeyword(TSqlTokenType.FreeTextTable);
				break;
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.TableName);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			if (node.PropertyName != null)
			{
				this.GenerateIdentifier("PROPERTY");
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.PropertyName);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
			else
			{
				int count = node.Columns.Count;
				if (count == 1)
				{
					this.GenerateFragmentIfNotNull(node.Columns[0]);
				}
				else
				{
					this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
				}
			}
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.SearchCondition);
			if (node.Language != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndIdentifier("LANGUAGE");
				this.GenerateSpaceAndFragmentIfNotNull(node.Language);
			}
			if (node.TopN != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.TopN);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00087224 File Offset: 0x00085424
		protected void GenerateFunctionStatementBody(FunctionStatementBody node)
		{
			this.GenerateKeyword(TSqlTokenType.Function);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLine();
			this.GenerateParenthesisedCommaSeparatedList<ProcedureParameter>(node.Parameters);
			if (node.Parameters == null || node.Parameters.Count == 0)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateSpaceAndSymbol(TSqlTokenType.RightParenthesis);
			}
			this.NewLine();
			this.GenerateIdentifier("RETURNS");
			this.GenerateSpace();
			SelectFunctionReturnType selectFunctionReturnType = node.ReturnType as SelectFunctionReturnType;
			if (selectFunctionReturnType != null)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Table);
			}
			else
			{
				this.GenerateFragmentIfNotNull(node.ReturnType);
			}
			this.GenerateCommaSeparatedWithClause<FunctionOption>(node.Options, false, false);
			if (node.OrderHint != null)
			{
				this.NewLine();
				this.GenerateFragmentIfNotNull(node.OrderHint);
			}
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.As);
			this.NewLine();
			if (selectFunctionReturnType != null)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Return);
				this.GenerateFragmentIfNotNull(selectFunctionReturnType);
				return;
			}
			if (node.MethodSpecifier != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.MethodSpecifier);
				return;
			}
			if (node.StatementList != null)
			{
				this.GenerateFragmentIfNotNull(node.StatementList);
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0008733C File Offset: 0x0008553C
		public override void ExplicitVisit(FunctionOption node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<FunctionOptionKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._functionOptionsGenerators, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00087364 File Offset: 0x00085564
		public override void ExplicitVisit(ExecuteAsFunctionOption node)
		{
			this.GenerateFragmentIfNotNull(node.ExecuteAs);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00087374 File Offset: 0x00085574
		public override void ExplicitVisit(GeneralSetCommand node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<GeneralSetCommandType, TokenGenerator>(SqlScriptGeneratorVisitor._generalSetCommandTypeGenerators, node.CommandType);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Parameter);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000873A8 File Offset: 0x000855A8
		public override void ExplicitVisit(GetConversationGroupStatement node)
		{
			this.GenerateIdentifier("GET");
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndFragmentIfNotNull(node.GroupId);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.Queue);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000873E8 File Offset: 0x000855E8
		public override void ExplicitVisit(GoToStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.GoTo);
			this.GenerateSpaceAndFragmentIfNotNull(node.LabelName);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00087400 File Offset: 0x00085600
		public override void ExplicitVisit(GrantStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Grant);
			this.GeneratePermissionOnToClauses(node);
			if (node.WithGrantOption)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Grant);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
			}
			this.GenerateAsClause(node);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0008744C File Offset: 0x0008564C
		public override void ExplicitVisit(GrantStatement80 node)
		{
			this.GenerateKeyword(TSqlTokenType.Grant);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityElement80);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityUserClause80);
			if (node.WithGrantOption)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Grant);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
			}
			if (node.AsClause != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.AsClause);
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000874C9 File Offset: 0x000856C9
		public override void ExplicitVisit(PrincipalOption node)
		{
			this.GenerateIdentifier("NO");
			this.GenerateSpaceAndIdentifier("CREDENTIAL");
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000874E4 File Offset: 0x000856E4
		public override void ExplicitVisit(IdentifierPrincipalOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PrincipalOptionKind, string>(SqlScriptGeneratorVisitor._loginOptionsNames, node.OptionKind);
			this.GenerateNameEqualsValue(valueForEnumKey, node.Identifier);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00087510 File Offset: 0x00085710
		public override void ExplicitVisit(IdentityFunctionCall node)
		{
			this.GenerateKeyword(TSqlTokenType.Identity);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.DataType);
			if (node.Seed != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.Seed);
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.Increment);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00087580 File Offset: 0x00085780
		public override void ExplicitVisit(IfStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeyword(TSqlTokenType.If);
			this.GenerateSpaceAndFragmentIfNotNull(node.Predicate);
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.ThenStatement);
			this.GenerateSemiColonWhenNecessary(node.ThenStatement);
			this.PopAlignmentPoint();
			if (node.ElseStatement != null)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.Else);
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateFragmentIfNotNull(node.ElseStatement);
				this.GenerateSemiColonWhenNecessary(node.ElseStatement);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00087614 File Offset: 0x00085814
		public override void ExplicitVisit(IIfCall node)
		{
			this.GenerateIdentifier("IIF");
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Predicate);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateFragmentIfNotNull(node.ThenExpression);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateFragmentIfNotNull(node.ElseExpression);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00087688 File Offset: 0x00085888
		public override void ExplicitVisit(InsertBulkColumnDefinition node)
		{
			this.GenerateFragmentIfNotNull(node.Column);
			switch (node.NullNotNull)
			{
			case NullNotNull.NotSpecified:
				break;
			case NullNotNull.Null:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Null);
				return;
			case NullNotNull.NotNull:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Not);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Null);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000876D4 File Offset: 0x000858D4
		public override void ExplicitVisit(InsertBulkStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Insert);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Bulk);
			this.GenerateSpaceAndFragmentIfNotNull(node.To);
			if (node.ColumnDefinitions.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<InsertBulkColumnDefinition>(node.ColumnDefinitions);
			}
			this.GenerateOption(node);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00087724 File Offset: 0x00085924
		public override void ExplicitVisit(InternalOpenRowset node)
		{
			this.GenerateKeyword(TSqlTokenType.OpenRowSet);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Identifier);
			if (node.VarArgs.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			}
			this.GenerateCommaSeparatedList<ScalarExpression>(node.VarArgs);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0008778C File Offset: 0x0008598C
		public override void ExplicitVisit(SelectStatementSnippet node)
		{
			if (node.Script != null)
			{
				this.GenerateIdentifierWithoutCheck(node.Script);
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000877A4 File Offset: 0x000859A4
		public override void ExplicitVisit(IPv4 node)
		{
			this.GenerateFragmentIfNotNull(node.OctetOne);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.OctetTwo);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.OctetThree);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.OctetFour);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00087804 File Offset: 0x00085A04
		public override void ExplicitVisit(KillQueryNotificationSubscriptionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Kill);
			this.GenerateSpaceAndIdentifier("QUERY");
			this.GenerateSpaceAndIdentifier("NOTIFICATION");
			this.GenerateSpaceAndIdentifier("SUBSCRIPTION");
			if (node.All)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.All);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SubscriptionId);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00087856 File Offset: 0x00085A56
		public override void ExplicitVisit(KillStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Kill);
			this.GenerateSpaceAndFragmentIfNotNull(node.Parameter);
			if (node.WithStatusOnly)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("STATUSONLY");
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00087890 File Offset: 0x00085A90
		public override void ExplicitVisit(KillStatsJobStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Kill);
			this.GenerateSpaceAndIdentifier("STATS");
			this.GenerateSpaceAndIdentifier("JOB");
			this.GenerateSpaceAndFragmentIfNotNull(node.JobId);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000878BC File Offset: 0x00085ABC
		public override void ExplicitVisit(LabelStatement node)
		{
			this.GenerateIdentifierWithoutCasing(node.Value);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000878CA File Offset: 0x00085ACA
		public override void ExplicitVisit(LineNoStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.LineNo);
			this.GenerateSpaceAndFragmentIfNotNull(node.LineNo);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000878E0 File Offset: 0x00085AE0
		public override void ExplicitVisit(ListenerIPEndpointProtocolOption node)
		{
			this.GenerateIdentifier("LISTENER_IP");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpace();
			if (node.IsAll)
			{
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			}
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			if (node.IPv6 != null)
			{
				this.GenerateFragmentIfNotNull(node.IPv6);
			}
			else
			{
				this.GenerateFragmentIfNotNull(node.IPv4PartOne);
				if (node.IPv4PartTwo != null)
				{
					this.GenerateSymbol(TSqlTokenType.Colon);
					this.GenerateFragmentIfNotNull(node.IPv4PartTwo);
				}
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00087970 File Offset: 0x00085B70
		public override void ExplicitVisit(LiteralEndpointProtocolOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<EndpointProtocolOptions, string>(SqlScriptGeneratorVisitor._endpointProtocolOptionsNames, node.Kind);
			if (valueForEnumKey != null)
			{
				if (node.Value != null)
				{
					this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
					return;
				}
				this.GenerateNameEqualsValue(valueForEnumKey, "NONE");
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000879B4 File Offset: 0x00085BB4
		public override void ExplicitVisit(LiteralPayloadOption node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PayloadOptionKinds, TokenGenerator>(SqlScriptGeneratorVisitor._payloadOptionKindsGenerators, node.Kind);
			if (valueForEnumKey != null)
			{
				this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000879E2 File Offset: 0x00085BE2
		public override void ExplicitVisit(LoginTypePayloadOption node)
		{
			this.GenerateNameEqualsValue("LOGIN_TYPE", node.IsWindows ? "WINDOWS" : "MIXED");
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00087A04 File Offset: 0x00085C04
		protected void GenerateValidationMethod(MessageTypeStatementBase node)
		{
			if (node.ValidationMethod != MessageValidationMethod.NotSpecified)
			{
				string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<MessageValidationMethod, string>(SqlScriptGeneratorVisitor._MessageValidationMethodNames, node.ValidationMethod);
				this.NewLineAndIndent();
				this.GenerateNameEqualsValue("VALIDATION", valueForEnumKey);
				if (node.ValidationMethod == MessageValidationMethod.ValidXml && node.XmlSchemaCollectionName != null)
				{
					this.GenerateSpaceAndKeyword(TSqlTokenType.With);
					this.GenerateSpaceAndKeyword(TSqlTokenType.Schema);
					this.GenerateSpaceAndIdentifier("COLLECTION");
					this.GenerateSpaceAndFragmentIfNotNull(node.XmlSchemaCollectionName);
				}
			}
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00087A7C File Offset: 0x00085C7C
		public override void ExplicitVisit(MethodSpecifier node)
		{
			this.GenerateKeyword(TSqlTokenType.External);
			this.GenerateSpaceAndIdentifier("NAME");
			this.GenerateSpaceAndFragmentIfNotNull(node.AssemblyName);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.ClassName);
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.MethodName);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00087ADC File Offset: 0x00085CDC
		protected void GenerateBulkColumnTimestamp(TextModificationStatement node)
		{
			if (node.Bulk)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Bulk);
			}
			this.GenerateFragmentIfNotNull(node.Column);
			this.GenerateSpaceAndFragmentIfNotNull(node.TextId);
			if (node.Timestamp != null)
			{
				this.GenerateSpace();
				this.GenerateNameEqualsValue("TIMESTAMP", node.Timestamp);
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00087B30 File Offset: 0x00085D30
		public override void ExplicitVisit(MoveConversationStatement node)
		{
			this.GenerateIdentifier("MOVE");
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Conversation);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.Group);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00087B6B File Offset: 0x00085D6B
		public override void ExplicitVisit(MoveRestoreOption node)
		{
			this.GenerateIdentifier("MOVE");
			this.GenerateSpaceAndFragmentIfNotNull(node.LogicalFileName);
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpaceAndFragmentIfNotNull(node.OSFileName);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00087B9B File Offset: 0x00085D9B
		public override void ExplicitVisit(NullableConstraintDefinition node)
		{
			this.GenerateConstraintHead(node);
			if (!node.Nullable)
			{
				this.GenerateKeywordAndSpace(TSqlTokenType.Not);
			}
			this.GenerateKeyword(TSqlTokenType.Null);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00087BBC File Offset: 0x00085DBC
		public override void ExplicitVisit(NullIfExpression node)
		{
			this.GenerateKeyword(TSqlTokenType.NullIf);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			this.GenerateFragmentIfNotNull(node.SecondExpression);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00087C16 File Offset: 0x00085E16
		public override void ExplicitVisit(OdbcConvertSpecification node)
		{
			this.GenerateFragmentIfNotNull(node.Identifier);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00087C24 File Offset: 0x00085E24
		public override void ExplicitVisit(OdbcFunctionCall node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftCurly);
			this.GenerateSpaceAndIdentifier("FN");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			if (node.ParametersUsed)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters, true);
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.RightCurly);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00087C79 File Offset: 0x00085E79
		public override void ExplicitVisit(OdbcQualifiedJoinTableReference node)
		{
			this.GenerateSymbol(TSqlTokenType.LeftCurly);
			this.GenerateSpaceAndIdentifier("OJ");
			this.GenerateSpaceAndFragmentIfNotNull(node.TableReference);
			this.GenerateSpaceAndSymbol(TSqlTokenType.RightCurly);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00087CA8 File Offset: 0x00085EA8
		public override void ExplicitVisit(OpenCursorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Open);
			this.GenerateSpaceAndFragmentIfNotNull(node.Cursor);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00087CBE File Offset: 0x00085EBE
		public override void ExplicitVisit(OpenMasterKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Open);
			this.GenerateSpaceAndIdentifier("MASTER");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpace();
			this.GenerateDecryptionByPassword(node.Password);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00087CF0 File Offset: 0x00085EF0
		public override void ExplicitVisit(OpenSymmetricKeyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Open);
			this.GenerateSpaceAndIdentifier("SYMMETRIC");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndIdentifier("DECRYPTION");
			this.GenerateSpaceAndKeyword(TSqlTokenType.By);
			this.GenerateSpaceAndFragmentIfNotNull(node.DecryptionMechanism);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00087D44 File Offset: 0x00085F44
		protected void GenerateOptimizerHints(IList<OptimizerHint> hints)
		{
			if (hints != null && hints.Count > 0)
			{
				this.NewLine();
				AlignmentPoint alignmentPoint = new AlignmentPoint();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateKeywordAndSpace(TSqlTokenType.Option);
				this.GenerateParenthesisedCommaSeparatedList<OptimizerHint>(hints);
				this.PopAlignmentPoint();
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00087D85 File Offset: 0x00085F85
		public override void ExplicitVisit(OptimizeForOptimizerHint node)
		{
			this.GenerateIdentifier("OPTIMIZE");
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			if (node.IsForUnknown)
			{
				this.GenerateSpaceAndIdentifier("UNKNOWN");
				return;
			}
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<VariableValuePair>(node.Pairs);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00087DC0 File Offset: 0x00085FC0
		public override void ExplicitVisit(TableHintsOptimizerHint node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Table);
			this.GenerateIdentifier("HINT");
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.ObjectName);
			if (node.TableHints.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateCommaSeparatedList<TableHint>(node.TableHints);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00087E2A File Offset: 0x0008602A
		public override void ExplicitVisit(VariableValuePair node)
		{
			this.GenerateFragmentIfNotNull(node.Variable);
			if (node.IsForUnknown)
			{
				this.GenerateSpaceAndIdentifier("UNKNOWN");
				return;
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Value);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00087E64 File Offset: 0x00086064
		public override void ExplicitVisit(OutputClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateIdentifier("OUTPUT");
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SelectElement>(node.SelectColumns);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00087EB8 File Offset: 0x000860B8
		public override void ExplicitVisit(OutputIntoClause node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateIdentifier("OUTPUT");
			AlignmentPoint alignmentPointForFragment = this.GetAlignmentPointForFragment(node, "ClauseBody");
			this.MarkClauseBodyAlignmentWhenNecessary(true, alignmentPointForFragment);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SelectElement>(node.SelectColumns);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Into);
			this.GenerateSpaceAndFragmentIfNotNull(node.IntoTable);
			if (node.IntoTableColumns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.IntoTableColumns);
			}
			this.PopAlignmentPoint();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00087F40 File Offset: 0x00086140
		public override void ExplicitVisit(OverClause node)
		{
			this.GenerateKeyword(TSqlTokenType.Over);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			bool flag = node.Partitions.Count > 0;
			if (flag)
			{
				this.GenerateIdentifier("PARTITION");
				this.GenerateSpaceAndKeyword(TSqlTokenType.By);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<ScalarExpression>(node.Partitions);
			}
			if (node.OrderByClause != null)
			{
				if (flag)
				{
					this.GenerateSpace();
				}
				AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
				this.GenerateFragmentWithAlignmentPointIfNotNull(node.OrderByClause, alignmentPoint);
				this.GenerateSpaceAndFragmentIfNotNull(node.WindowFrameClause);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00087FD8 File Offset: 0x000861D8
		public override void ExplicitVisit(ParseCall node)
		{
			this.GenerateIdentifier("PARSE");
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.StringValue);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			if (node.Culture != null)
			{
				this.GenerateSpace();
				this.GenerateIdentifier("USING");
				this.GenerateSpaceAndFragmentIfNotNull(node.Culture);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00088058 File Offset: 0x00086258
		public override void ExplicitVisit(PartitionFunctionCall node)
		{
			if (node.DatabaseName != null)
			{
				this.GenerateFragmentIfNotNull(node.DatabaseName);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			this.GenerateIdentifier("$PARTITION");
			this.GenerateSymbol(TSqlTokenType.Dot);
			this.GenerateFragmentIfNotNull(node.FunctionName);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.Parameters);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000880B8 File Offset: 0x000862B8
		public override void ExplicitVisit(PasswordCreateLoginSource node)
		{
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpace();
			this.GenerateNameEqualsValue("PASSWORD", node.Password);
			if (node.Hashed)
			{
				this.GenerateSpaceAndIdentifier("HASHED");
			}
			if (node.MustChange)
			{
				this.GenerateSpaceAndIdentifier("MUST_CHANGE");
			}
			if (node.Options != null && node.Options.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateFragmentList<PrincipalOption>(node.Options);
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0008813A File Offset: 0x0008633A
		public override void ExplicitVisit(Permission node)
		{
			this.GenerateSpaceSeparatedList<Identifier>(node.Identifiers);
			if (node.Columns != null && node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00088170 File Offset: 0x00086370
		public override void ExplicitVisit(PortsEndpointProtocolOption node)
		{
			this.GenerateIdentifier("PORTS");
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateCommaSeparatedFlagOpitons<PortTypes>(SqlScriptGeneratorVisitor._portTypesGenerators, node.PortTypes);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000881AF File Offset: 0x000863AF
		public override void ExplicitVisit(PredicateSetStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Set);
			this.GenerateCommaSeparatedFlagOpitons<SetOptions>(SqlScriptGeneratorVisitor._setOptionsGenerators, node.Options);
			this.GenerateSpaceAndKeyword(node.IsOn ? TSqlTokenType.On : TSqlTokenType.Off);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000881E1 File Offset: 0x000863E1
		public override void ExplicitVisit(PrintStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Print);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000881F8 File Offset: 0x000863F8
		public override void ExplicitVisit(Privilege80 node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PrivilegeType80, TokenGenerator>(SqlScriptGeneratorVisitor._privilegeType80Generators, node.PrivilegeType80);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			if (node.Columns != null && node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00088248 File Offset: 0x00086448
		public override void ExplicitVisit(PrivilegeSecurityElement80 node)
		{
			if (node.Privileges != null && node.Privileges.Count > 0)
			{
				this.GenerateCommaSeparatedList<Privilege80>(node.Privileges);
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.SchemaObjectName);
				if (node.Columns != null && node.Columns.Count > 0)
				{
					this.GenerateSpace();
					this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
				}
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000882BC File Offset: 0x000864BC
		public override void ExplicitVisit(ProcedureParameter node)
		{
			this.GenerateFragmentIfNotNull(node.VariableName);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			if (node.IsVarying)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Varying);
			}
			if (node.Value != null)
			{
				this.GenerateSymbol(TSqlTokenType.EqualsSign);
				this.GenerateFragmentIfNotNull(node.Value);
			}
			switch (node.Modifier)
			{
			case ParameterModifier.None:
				break;
			case ParameterModifier.Output:
				this.GenerateSpaceAndIdentifier("OUTPUT");
				return;
			case ParameterModifier.ReadOnly:
				this.GenerateSpaceAndIdentifier("READONLY");
				break;
			default:
				return;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00088344 File Offset: 0x00086544
		public override void ExplicitVisit(ProcedureReference node)
		{
			this.GenerateFragmentIfNotNull(node.Name);
			if (node.Number != null)
			{
				this.GenerateToken(TSqlTokenType.ProcNameSemicolon, ";");
				this.GenerateFragmentIfNotNull(node.Number);
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00088378 File Offset: 0x00086578
		protected void GenerateProcedureStatementBody(ProcedureStatementBody node)
		{
			this.GenerateKeyword(TSqlTokenType.Procedure);
			this.GenerateSpaceAndFragmentIfNotNull(node.ProcedureReference);
			if (node.Parameters != null && node.Parameters.Count > 0)
			{
				this.NewLine();
				this.GenerateCommaSeparatedList<ProcedureParameter>(node.Parameters);
			}
			this.GenerateCommaSeparatedWithClause<ProcedureOption>(node.Options, false, false);
			if (node.IsForReplication)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Replication);
			}
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.As);
			if (node.StatementList != null)
			{
				this.NewLine();
				this.GenerateFragmentIfNotNull(node.StatementList);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.MethodSpecifier);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00088424 File Offset: 0x00086624
		public override void ExplicitVisit(ProcedureOption node)
		{
			switch (node.OptionKind)
			{
			case ProcedureOptionKind.Encryption:
				this.GenerateIdentifier("ENCRYPTION");
				return;
			case ProcedureOptionKind.Recompile:
				this.GenerateIdentifier("RECOMPILE");
				return;
			default:
				return;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0008845E File Offset: 0x0008665E
		public override void ExplicitVisit(ExecuteAsProcedureOption node)
		{
			this.GenerateFragmentIfNotNull(node.ExecuteAs);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0008846C File Offset: 0x0008666C
		public override void ExplicitVisit(RaiseErrorLegacyStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Raiserror);
			this.GenerateSpaceAndFragmentIfNotNull(node.FirstParameter);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondParameter);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00088490 File Offset: 0x00086690
		public override void ExplicitVisit(RaiseErrorStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Raiserror);
			this.GenerateSpace();
			this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.FirstParameter);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondParameter);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.ThirdParameter);
			if (node.OptionalParameters.Count > 0)
			{
				this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				this.GenerateCommaSeparatedList<ScalarExpression>(node.OptionalParameters);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			if (node.RaiseErrorOptions != RaiseErrorOptions.None)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateCommaSeparatedFlagOpitons<RaiseErrorOptions>(SqlScriptGeneratorVisitor._raiseErrorOptionsGenerators, node.RaiseErrorOptions);
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0008854A File Offset: 0x0008674A
		public override void ExplicitVisit(ReadOnlyForClause node)
		{
			this.GenerateKeyword(TSqlTokenType.Read);
			this.GenerateSpaceAndIdentifier("ONLY");
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00088560 File Offset: 0x00086760
		public override void ExplicitVisit(ReadTextStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.ReadText);
			this.GenerateSpaceAndFragmentIfNotNull(node.Column);
			this.GenerateSpaceAndFragmentIfNotNull(node.TextPointer);
			this.GenerateSpaceAndFragmentIfNotNull(node.Offset);
			this.GenerateSpaceAndFragmentIfNotNull(node.Size);
			if (node.HoldLock)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.HoldLock);
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000885B8 File Offset: 0x000867B8
		public override void ExplicitVisit(ReceiveStatement node)
		{
			this.GenerateIdentifier("RECEIVE");
			if (node.Top != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Top);
				this.GenerateSpace();
				this.GenerateParenthesisedFragmentIfNotNull(node.Top);
			}
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SelectElement>(node.SelectElements);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.Queue);
			if (node.Into != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Into);
				this.GenerateSpaceAndFragmentIfNotNull(node.Into);
			}
			if (node.Where != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Where);
				this.GenerateSpace();
				this.GenerateNameEqualsValue(node.IsConversationGroupIdWhere ? "CONVERSATION_GROUP_ID" : "CONVERSATION_HANDLE", node.Where);
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00088670 File Offset: 0x00086870
		public override void ExplicitVisit(ReconfigureStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Reconfigure);
			if (node.WithOverride)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("OVERRIDE");
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0008869E File Offset: 0x0008689E
		protected void GenerateBindingOptions(IList<RemoteServiceBindingOption> options)
		{
			if (options != null && options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<RemoteServiceBindingOption>(options);
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000886CA File Offset: 0x000868CA
		public override void ExplicitVisit(UserRemoteServiceBindingOption node)
		{
			if (node.User != null)
			{
				this.GenerateNameEqualsValue(TSqlTokenType.User, node.User);
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000886E5 File Offset: 0x000868E5
		public override void ExplicitVisit(OnOffRemoteServiceBindingOption node)
		{
			this.GenerateOptionStateWithEqualSign("ANONYMOUS", node.OptionState);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000886F8 File Offset: 0x000868F8
		public override void ExplicitVisit(RestoreMasterKeyStatement node)
		{
			this.GenerateCommonRestorePart(node, false);
			this.GenerateSpace();
			this.GenerateEncryptionByPassword(node.EncryptionPassword);
			if (node.IsForce)
			{
				this.GenerateSpaceAndIdentifier("FORCE");
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00088727 File Offset: 0x00086927
		public override void ExplicitVisit(RestoreServiceMasterKeyStatement node)
		{
			this.GenerateCommonRestorePart(node, true);
			if (node.IsForce)
			{
				this.GenerateSpaceAndIdentifier("FORCE");
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00088744 File Offset: 0x00086944
		public override void ExplicitVisit(RestoreStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Restore);
			if (node.Kind == RestoreStatementKind.TransactionLog)
			{
				this.GenerateSpaceAndIdentifier("LOG");
				this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
			}
			else if (node.Kind == RestoreStatementKind.Database)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Database);
				this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
			}
			else
			{
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<RestoreStatementKind, TokenGenerator>(SqlScriptGeneratorVisitor._restoreStatementKindGenerators, node.Kind);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateToken(valueForEnumKey);
				}
			}
			if (node.Files.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<BackupRestoreFileInfo>(node.Files);
			}
			if (node.Devices.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.From);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<DeviceInfo>(node.Devices);
			}
			if (node.Options.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<RestoreOption>(node.Options);
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00088837 File Offset: 0x00086A37
		public override void ExplicitVisit(ReturnStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Return);
			if (node.Expression != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00088858 File Offset: 0x00086A58
		public override void ExplicitVisit(RevertStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Revert);
			if (node.Cookie != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeywordAndSpace(TSqlTokenType.With);
				this.GenerateNameEqualsValue("COOKIE", node.Cookie);
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00088890 File Offset: 0x00086A90
		public override void ExplicitVisit(RevokeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Revoke);
			if (node.GrantOptionFor)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Grant);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			}
			this.GenerateSpace();
			this.GeneratePermissionOnToClauses(node);
			if (node.CascadeOption)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Cascade);
			}
			this.GenerateAsClause(node);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000888EC File Offset: 0x00086AEC
		public override void ExplicitVisit(RevokeStatement80 node)
		{
			this.GenerateKeyword(TSqlTokenType.Revoke);
			if (node.GrantOptionFor)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Grant);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityElement80);
			this.GenerateSpaceAndKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecurityUserClause80);
			if (node.CascadeOption)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Cascade);
			}
			if (node.AsClause != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.AsClause);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00088970 File Offset: 0x00086B70
		public override void ExplicitVisit(RolePayloadOption node)
		{
			this.GenerateTokenAndEqualSign("ROLE");
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DatabaseMirroringEndpointRole, TokenGenerator>(SqlScriptGeneratorVisitor._databaseMirroringEndpointRoleGenerators, node.Role);
			if (valueForEnumKey != null)
			{
				this.GenerateSpace();
				this.GenerateToken(valueForEnumKey);
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000889A9 File Offset: 0x00086BA9
		public override void ExplicitVisit(RollbackTransactionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Rollback);
			if (node.Name != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Transaction);
				this.GenerateSpace();
				this.GenerateTransactionName(node.Name);
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000889DC File Offset: 0x00086BDC
		public override void ExplicitVisit(RouteOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<RouteOptionKind, string>(SqlScriptGeneratorVisitor._RouteOptionTypeNames, node.OptionKind);
			this.GenerateNameEqualsValue(valueForEnumKey, node.Literal);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00088A07 File Offset: 0x00086C07
		protected void GenerateRouteOptions(RouteStatement node)
		{
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<RouteOption>(node.RouteOptions);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00088A2C File Offset: 0x00086C2C
		public override void ExplicitVisit(SaveTransactionStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Save);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Transaction);
			this.GenerateSpace();
			this.GenerateTransactionName(node.Name);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00088A56 File Offset: 0x00086C56
		public override void ExplicitVisit(ScalarFunctionReturnType node)
		{
			this.GenerateFragmentIfNotNull(node.DataType);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00088A64 File Offset: 0x00086C64
		public override void ExplicitVisit(SchemaDeclarationItem node)
		{
			this.GenerateFragmentIfNotNull(node.ColumnDefinition);
			this.GenerateSpaceAndFragmentIfNotNull(node.Mapping);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00088A7E File Offset: 0x00086C7E
		public override void ExplicitVisit(SchemaPayloadOption node)
		{
			this.GenerateNameEqualsValue(TSqlTokenType.Schema, node.IsStandard ? "STANDARD" : "NONE");
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00088AA0 File Offset: 0x00086CA0
		public override void ExplicitVisit(OnOffPrincipalOption node)
		{
			string text = string.Empty;
			switch (node.OptionKind)
			{
			case PrincipalOptionKind.CheckExpiration:
				text = "CHECK_EXPIRATION";
				break;
			case PrincipalOptionKind.CheckPolicy:
				text = "CHECK_POLICY";
				break;
			}
			this.GenerateOptionStateWithEqualSign(text, node.OptionState);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00088AE8 File Offset: 0x00086CE8
		public override void ExplicitVisit(SecurityPrincipal node)
		{
			switch (node.PrincipalType)
			{
			case PrincipalType.Null:
				this.GenerateKeyword(TSqlTokenType.Null);
				return;
			case PrincipalType.Public:
				this.GenerateKeyword(TSqlTokenType.Public);
				return;
			case PrincipalType.Identifier:
				this.GenerateFragmentIfNotNull(node.Identifier);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00088B30 File Offset: 0x00086D30
		protected void GeneratePermissionOnToClauses(SecurityStatement node)
		{
			this.GenerateCommaSeparatedList<Permission>(node.Permissions);
			if (node.SecurityTargetObject != null)
			{
				this.NewLineAndIndent();
				this.GenerateFragmentIfNotNull(node.SecurityTargetObject);
			}
			this.GenerateSpaceAndKeyword(TSqlTokenType.To);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SecurityPrincipal>(node.Principals);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00088B80 File Offset: 0x00086D80
		protected void GenerateAsClause(SecurityStatement node)
		{
			if (node.AsClause != null)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.As);
				this.GenerateSpaceAndFragmentIfNotNull(node.AsClause);
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00088BA4 File Offset: 0x00086DA4
		public override void ExplicitVisit(SecurityTargetObject node)
		{
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpace();
			if (node.ObjectKind != SecurityObjectKind.NotSpecified)
			{
				this.GenerateSourceForSecurityObjectKind(node.ObjectKind);
			}
			this.GenerateFragmentIfNotNull(node.ObjectName);
			if (node.Columns != null && node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00088C08 File Offset: 0x00086E08
		protected void GenerateSourceForSecurityObjectKind(SecurityObjectKind type)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SecurityObjectKind, List<TokenGenerator>>(SqlScriptGeneratorVisitor._securityObjectKindGenerators, type);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
			this.GenerateSymbol(TSqlTokenType.DoubleColon);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00088C36 File Offset: 0x00086E36
		public override void ExplicitVisit(SecurityTargetObjectName node)
		{
			this.GenerateFragmentIfNotNull(node.MultiPartIdentifier);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00088C44 File Offset: 0x00086E44
		public override void ExplicitVisit(SecurityUserClause80 node)
		{
			switch (node.UserType80)
			{
			case UserType80.Null:
				this.GenerateKeyword(TSqlTokenType.Null);
				return;
			case UserType80.Public:
				this.GenerateKeyword(TSqlTokenType.Public);
				return;
			case UserType80.Users:
				if (node.Users != null && node.Users.Count > 0)
				{
					this.GenerateCommaSeparatedList<Identifier>(node.Users);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00088C9F File Offset: 0x00086E9F
		public override void ExplicitVisit(SelectFunctionReturnType node)
		{
			if (node.SelectStatement != null)
			{
				this.NewLineAndIndent();
				this.GenerateFragmentIfNotNull(node.SelectStatement);
				this.NewLine();
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00088CC4 File Offset: 0x00086EC4
		public override void ExplicitVisit(SemanticTableReference node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			switch (node.SemanticFunctionType)
			{
			case SemanticFunctionType.SemanticKeyPhraseTable:
				this.GenerateKeyword(TSqlTokenType.SemanticKeyPhraseTable);
				break;
			case SemanticFunctionType.SemanticSimilarityTable:
				this.GenerateKeyword(TSqlTokenType.SemanticSimilarityTable);
				break;
			case SemanticFunctionType.SemanticSimilarityDetailsTable:
				this.GenerateKeyword(TSqlTokenType.SemanticSimilarityDetailsTable);
				break;
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.TableName);
			this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
			int count = node.Columns.Count;
			if (count == 1)
			{
				this.GenerateFragmentIfNotNull(node.Columns[0]);
			}
			else
			{
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
			}
			if (node.SourceKey != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.SourceKey);
			}
			if (node.MatchedColumn != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.MatchedColumn);
			}
			if (node.MatchedKey != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.MatchedKey);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00088DEC File Offset: 0x00086FEC
		public override void ExplicitVisit(SendStatement node)
		{
			this.GenerateIdentifier("SEND");
			this.GenerateSpaceAndKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndIdentifier("CONVERSATION");
			this.GenerateSpace();
			if (this._options.SqlVersion >= SqlVersion.Sql110)
			{
				this.GenerateParenthesisedCommaSeparatedList<ScalarExpression>(node.ConversationHandles);
			}
			else
			{
				this.GenerateCommaSeparatedList<ScalarExpression>(node.ConversationHandles);
			}
			if (node.MessageTypeName != null)
			{
				this.GenerateSpaceAndIdentifier("MESSAGE");
				this.GenerateSpaceAndIdentifier("TYPE");
				this.GenerateSpaceAndFragmentIfNotNull(node.MessageTypeName);
			}
			if (node.MessageBody != null)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedFragmentIfNotNull(node.MessageBody);
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00088E8C File Offset: 0x0008708C
		public override void ExplicitVisit(ServiceContract node)
		{
			switch (node.Action)
			{
			case AlterAction.Add:
				this.GenerateKeyword(TSqlTokenType.Add);
				this.GenerateSpaceAndIdentifier("CONTRACT");
				this.GenerateSpace();
				break;
			case AlterAction.Drop:
				this.GenerateKeyword(TSqlTokenType.Drop);
				this.GenerateSpaceAndIdentifier("CONTRACT");
				this.GenerateSpace();
				break;
			}
			this.GenerateFragmentIfNotNull(node.Name);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00088EF3 File Offset: 0x000870F3
		public override void ExplicitVisit(SessionTimeoutPayloadOption node)
		{
			if (node.IsNever)
			{
				this.GenerateNameEqualsValue("SESSION_TIMEOUT", "NEVER");
				return;
			}
			this.GenerateNameEqualsValue("SESSION_TIMEOUT", node.Timeout);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00088F1F File Offset: 0x0008711F
		public override void ExplicitVisit(SetCommandStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<SetCommand>(node.Commands);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00088F3E File Offset: 0x0008713E
		public override void ExplicitVisit(SetErrorLevelStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Errlvl);
			this.GenerateSpaceAndFragmentIfNotNull(node.Level);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00088F5F File Offset: 0x0008715F
		public override void ExplicitVisit(SetFipsFlaggerCommand node)
		{
			this.GenerateIdentifier("FIPS_FLAGGER");
			this.GenerateSpace();
			FipsComplianceLevelHelper.Instance.GenerateSourceForOption(this._writer, node.ComplianceLevel);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00088F88 File Offset: 0x00087188
		public override void ExplicitVisit(SetIdentityInsertStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.IdentityInsert);
			this.GenerateSpaceAndFragmentIfNotNull(node.Table);
			this.GenerateSpaceAndKeyword(node.IsOn ? TSqlTokenType.On : TSqlTokenType.Off);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00088FBD File Offset: 0x000871BD
		public override void ExplicitVisit(SetOffsetsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Offsets);
			this.GenerateSpace();
			this.GenerateCommaSeparatedFlagOpitons<SetOffsets>(SqlScriptGeneratorVisitor._setOffsetsGenerators, node.Options);
			this.GenerateSpaceAndKeyword(node.IsOn ? TSqlTokenType.On : TSqlTokenType.Off);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00088FFD File Offset: 0x000871FD
		public override void ExplicitVisit(SetRowCountStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.RowCount);
			this.GenerateSpaceAndFragmentIfNotNull(node.NumberRows);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00089024 File Offset: 0x00087224
		public override void ExplicitVisit(SetStatisticsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Statistics);
			this.GenerateSpace();
			SetStatisticsOptionsHelper.Instance.GenerateCommaSeparatedFlagOptions(this._writer, node.Options);
			this.GenerateSpace();
			this.GenerateKeyword(node.IsOn ? TSqlTokenType.On : TSqlTokenType.Off);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0008907D File Offset: 0x0008727D
		public override void ExplicitVisit(SetTextSizeStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.TextSize);
			this.GenerateSpaceAndFragmentIfNotNull(node.TextSize);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000890A4 File Offset: 0x000872A4
		public override void ExplicitVisit(SetTransactionIsolationLevelStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Transaction);
			this.GenerateSpaceAndIdentifier("ISOLATION");
			this.GenerateSpaceAndIdentifier("LEVEL");
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<IsolationLevel, List<TokenGenerator>>(SqlScriptGeneratorVisitor._isolationLevelGenerators, node.Level);
			if (valueForEnumKey != null)
			{
				this.GenerateSpace();
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000890FE File Offset: 0x000872FE
		public override void ExplicitVisit(SetUserStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.SetUser);
			this.GenerateSpaceAndFragmentIfNotNull(node.UserName);
			if (node.WithNoReset)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("NORESET");
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0008913C File Offset: 0x0008733C
		public override void ExplicitVisit(SetVariableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Set);
			this.GenerateSpaceAndFragmentIfNotNull(node.Variable);
			if (node.SeparatorType != SeparatorType.NotSpecified)
			{
				switch (node.SeparatorType)
				{
				case SeparatorType.Dot:
					this.GenerateSymbol(TSqlTokenType.Dot);
					break;
				case SeparatorType.DoubleColon:
					this.GenerateSymbol(TSqlTokenType.DoubleColon);
					break;
				}
				this.GenerateFragmentIfNotNull(node.Identifier);
				if (node.FunctionCallExists)
				{
					this.GenerateSpace();
					this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
					this.GenerateCommaSeparatedList<ScalarExpression>(node.Parameters);
					this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				}
			}
			if (node.Expression != null)
			{
				TSqlTokenType valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<AssignmentKind, TSqlTokenType>(SqlScriptGeneratorVisitor._assignmentKindSymbols, node.AssignmentKind);
				this.GenerateSpaceAndSymbol(valueForEnumKey);
				this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
			}
			if (node.CursorDefinition != null)
			{
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.CursorDefinition);
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00089222 File Offset: 0x00087422
		public override void ExplicitVisit(ShutdownStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Shutdown);
			if (node.WithNoWait)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("NOWAIT");
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00089254 File Offset: 0x00087454
		public override void ExplicitVisit(LiteralPrincipalOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<PrincipalOptionKind, string>(SqlScriptGeneratorVisitor._loginOptionsNames, node.OptionKind);
			this.GenerateNameEqualsValue(valueForEnumKey, node.Value);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0008927F File Offset: 0x0008747F
		protected void GenerateCounterSignature(SignatureStatementBase node)
		{
			if (node.IsCounter)
			{
				this.GenerateIdentifier("COUNTER");
				this.GenerateSpace();
			}
			this.GenerateIdentifier("SIGNATURE");
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000892A8 File Offset: 0x000874A8
		protected void GenerateModule(SignatureStatementBase node)
		{
			switch (node.ElementKind)
			{
			case SignableElementKind.Object:
				this.GenerateIdentifier("OBJECT");
				this.GenerateSymbol(TSqlTokenType.DoubleColon);
				break;
			case SignableElementKind.Assembly:
				this.GenerateIdentifier("ASSEMBLY");
				this.GenerateSymbol(TSqlTokenType.DoubleColon);
				break;
			case SignableElementKind.Database:
				this.GenerateKeyword(TSqlTokenType.Database);
				this.GenerateSymbol(TSqlTokenType.DoubleColon);
				break;
			}
			this.GenerateFragmentIfNotNull(node.Element);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00089323 File Offset: 0x00087523
		protected void GenerateCryptos(SignatureStatementBase node)
		{
			this.GenerateKeyword(TSqlTokenType.By);
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<CryptoMechanism>(node.Cryptos);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00089340 File Offset: 0x00087540
		public override void ExplicitVisit(SoapMethod node)
		{
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SoapMethodAction, TokenGenerator>(SqlScriptGeneratorVisitor._soapMethodActionGenerators, node.Action);
			if (valueForEnumKey != null)
			{
				this.GenerateToken(valueForEnumKey);
			}
			this.GenerateSpaceAndIdentifier("WEBMETHOD");
			if (node.Namespace != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.Namespace);
				this.GenerateSymbol(TSqlTokenType.Dot);
			}
			else
			{
				this.GenerateSpace();
			}
			this.GenerateFragmentIfNotNull(node.Alias);
			if (node.Action != SoapMethodAction.Drop)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				if (node.Name != null)
				{
					this.GenerateNameEqualsValue("NAME", node.Name);
				}
				if (node.Schema != SoapMethodSchemas.NotSpecified)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
					this.GenerateTokenAndEqualSign(TSqlTokenType.Schema);
					TokenGenerator valueForEnumKey2 = SqlScriptGeneratorVisitor.GetValueForEnumKey<SoapMethodSchemas, TokenGenerator>(SqlScriptGeneratorVisitor._soapMethodSchemasGenerators, node.Schema);
					if (valueForEnumKey2 != null)
					{
						this.GenerateSpace();
						this.GenerateToken(valueForEnumKey2);
					}
				}
				if (node.Format != SoapMethodFormat.NotSpecified)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
					string valueForEnumKey3 = SqlScriptGeneratorVisitor.GetValueForEnumKey<SoapMethodFormat, string>(SqlScriptGeneratorVisitor._soapMethodFormatNames, node.Format);
					if (valueForEnumKey3 != null)
					{
						this.GenerateNameEqualsValue("FORMAT", valueForEnumKey3);
					}
				}
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00089454 File Offset: 0x00087654
		public override void ExplicitVisit(StatementList node)
		{
			if (node.Statements != null)
			{
				bool flag = true;
				foreach (TSqlStatement tsqlStatement in node.Statements)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this.NewLine();
					}
					this.GenerateFragmentIfNotNull(tsqlStatement);
					this.GenerateSemiColonWhenNecessary(tsqlStatement);
				}
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000894C0 File Offset: 0x000876C0
		public override void ExplicitVisit(StatisticsOption node)
		{
			switch (node.OptionKind)
			{
			case StatisticsOptionKind.FullScan:
				this.GenerateIdentifier("FULLSCAN");
				return;
			case StatisticsOptionKind.SamplePercent:
			case StatisticsOptionKind.SampleRows:
			case StatisticsOptionKind.StatsStream:
			case StatisticsOptionKind.RowCount:
			case StatisticsOptionKind.PageCount:
				break;
			case StatisticsOptionKind.NoRecompute:
				this.GenerateIdentifier("NORECOMPUTE");
				return;
			case StatisticsOptionKind.Resample:
				this.GenerateIdentifier("RESAMPLE");
				return;
			case StatisticsOptionKind.All:
				this.GenerateKeyword(TSqlTokenType.All);
				return;
			case StatisticsOptionKind.Columns:
				this.GenerateIdentifier("COLUMNS");
				return;
			case StatisticsOptionKind.Index:
				this.GenerateKeyword(TSqlTokenType.Index);
				return;
			case StatisticsOptionKind.Rows:
				this.GenerateIdentifier("ROWS");
				break;
			default:
				return;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00089558 File Offset: 0x00087758
		public override void ExplicitVisit(LiteralStatisticsOption node)
		{
			switch (node.OptionKind)
			{
			case StatisticsOptionKind.SamplePercent:
				this.GenerateIdentifier("SAMPLE");
				this.GenerateSpaceAndFragmentIfNotNull(node.Literal);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Percent);
				return;
			case StatisticsOptionKind.SampleRows:
				this.GenerateIdentifier("SAMPLE");
				this.GenerateSpaceAndFragmentIfNotNull(node.Literal);
				this.GenerateSpaceAndIdentifier("ROWS");
				return;
			case StatisticsOptionKind.StatsStream:
				this.GenerateNameEqualsValue("STATS_STREAM", node.Literal);
				return;
			case StatisticsOptionKind.NoRecompute:
			case StatisticsOptionKind.Resample:
				break;
			case StatisticsOptionKind.RowCount:
				this.GenerateNameEqualsValue(TSqlTokenType.RowCount, node.Literal);
				return;
			case StatisticsOptionKind.PageCount:
				this.GenerateNameEqualsValue("PAGECOUNT", node.Literal);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0008960C File Offset: 0x0008780C
		public override void ExplicitVisit(StopRestoreOption node)
		{
			this.GenerateNameEqualsValue(node.IsStopAt ? "STOPATMARK" : "STOPBEFOREMARK", node.Mark);
			if (node.After != null)
			{
				this.GenerateSpaceAndIdentifier("AFTER");
				this.GenerateSpaceAndFragmentIfNotNull(node.After);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00089658 File Offset: 0x00087858
		public override void ExplicitVisit(SubqueryComparisonPredicate node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.Expression);
			this.PopAlignmentPoint();
			this.GenerateSpace();
			this.GenerateBinaryOperator(node.ComparisonType);
			if (node.SubqueryComparisonPredicateType != SubqueryComparisonPredicateType.None)
			{
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<SubqueryComparisonPredicateType, TokenGenerator>(SqlScriptGeneratorVisitor._subqueryComparisonPredicateTypeGenerators, node.SubqueryComparisonPredicateType);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateToken(valueForEnumKey);
				}
			}
			AlignmentPoint alignmentPoint2 = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint2);
			this.GenerateSpaceAndFragmentIfNotNull(node.Subquery);
			this.PopAlignmentPoint();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000896DE File Offset: 0x000878DE
		protected void GenerateWithTableHints(IList<TableHint> tableHints)
		{
			if (tableHints.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<TableHint>(tableHints);
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00089701 File Offset: 0x00087901
		public override void ExplicitVisit(TableHint node)
		{
			TableHintOptionsHelper.Instance.GenerateSourceForOption(this._writer, node.HintKind);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00089719 File Offset: 0x00087919
		public override void ExplicitVisit(IndexTableHint node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Index);
			this.GenerateParenthesisedCommaSeparatedList<IdentifierOrValueExpression>(node.IndexValues);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0008972F File Offset: 0x0008792F
		public override void ExplicitVisit(LiteralTableHint node)
		{
			this.GenerateNameEqualsValue("SPATIAL_WINDOW_MAX_CELLS", node.Value);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00089744 File Offset: 0x00087944
		public override void ExplicitVisit(ForceSeekTableHint node)
		{
			this.GenerateIdentifier("FORCESEEK");
			if (node.IndexValue != null)
			{
				this.GenerateSpace();
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentIfNotNull(node.IndexValue);
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.ColumnValues);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000897A0 File Offset: 0x000879A0
		public override void ExplicitVisit(TableSampleClause node)
		{
			this.GenerateKeyword(TSqlTokenType.TableSample);
			if (node.System)
			{
				this.GenerateSpaceAndIdentifier("SYSTEM");
			}
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.SampleNumber);
			TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<TableSampleClauseOption, TokenGenerator>(SqlScriptGeneratorVisitor._tableSampleClauseOptionGenerators, node.TableSampleClauseOption);
			if (valueForEnumKey != null && node.TableSampleClauseOption != TableSampleClauseOption.NotSpecified)
			{
				this.GenerateSpace();
				this.GenerateToken(valueForEnumKey);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			if (node.RepeatSeed != null)
			{
				this.GenerateSpaceAndIdentifier("REPEATABLE");
				this.GenerateSpace();
				this.GenerateParenthesisedFragmentIfNotNull(node.RepeatSeed);
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0008983C File Offset: 0x00087A3C
		public override void ExplicitVisit(TableValuedFunctionReturnType node)
		{
			this.NewLineAndIndent();
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.DeclareTableVariableBody);
			this.PopAlignmentPoint();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00089870 File Offset: 0x00087A70
		public override void ExplicitVisit(TargetRecoveryTimeDatabaseOption node)
		{
			this.GenerateIdentifier("TARGET_RECOVERY_TIME");
			this.GenerateSpace();
			this.GenerateSymbolAndSpace(TSqlTokenType.EqualsSign);
			this.GenerateFragmentIfNotNull(node.RecoveryTime);
			this.GenerateSpace();
			TargetRecoveryTimeUnitHelper.Instance.GenerateSourceForOption(this._writer, node.Unit);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000898C4 File Offset: 0x00087AC4
		public override void ExplicitVisit(TopRowFilter node)
		{
			this.GenerateKeyword(TSqlTokenType.Top);
			this.GenerateSpaceAndFragmentIfNotNull(node.Expression);
			if (node.Percent)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Percent);
			}
			if (node.WithTies)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("TIES");
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00089918 File Offset: 0x00087B18
		protected void GenerateTransactionName(object node)
		{
			string text = node as string;
			if (text != null)
			{
				this.GenerateIdentifierWithoutCasing(text);
				return;
			}
			TSqlFragment tsqlFragment = node as TSqlFragment;
			if (tsqlFragment != null)
			{
				this.GenerateFragmentIfNotNull(tsqlFragment);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00089948 File Offset: 0x00087B48
		public override void ExplicitVisit(TriggerAction node)
		{
			switch (node.TriggerActionType)
			{
			case TriggerActionType.Delete:
				this.GenerateKeyword(TSqlTokenType.Delete);
				return;
			case TriggerActionType.Insert:
				this.GenerateKeyword(TSqlTokenType.Insert);
				return;
			case TriggerActionType.Update:
				this.GenerateKeyword(TSqlTokenType.Update);
				return;
			case TriggerActionType.Event:
				this.GenerateFragmentIfNotNull(node.EventTypeGroup);
				return;
			case TriggerActionType.LogOn:
				this.GenerateIdentifier("LOGON");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000899B0 File Offset: 0x00087BB0
		public override void ExplicitVisit(TriggerObject node)
		{
			switch (node.TriggerScope)
			{
			case TriggerScope.Normal:
				this.GenerateFragmentIfNotNull(node.Name);
				return;
			case TriggerScope.Database:
				this.GenerateKeyword(TSqlTokenType.Database);
				return;
			case TriggerScope.AllServer:
				this.GenerateKeyword(TSqlTokenType.All);
				this.GenerateSpaceAndIdentifier("SERVER");
				return;
			default:
				return;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00089A00 File Offset: 0x00087C00
		public override void ExplicitVisit(TriggerOption node)
		{
			TriggerOptionKind optionKind = node.OptionKind;
			if (optionKind != TriggerOptionKind.Encryption)
			{
				return;
			}
			this.GenerateIdentifier("ENCRYPTION");
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00089A24 File Offset: 0x00087C24
		public override void ExplicitVisit(ExecuteAsTriggerOption node)
		{
			this.GenerateFragmentIfNotNull(node.ExecuteAsClause);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00089A34 File Offset: 0x00087C34
		protected void GenerateTriggerStatementBody(TriggerStatementBody node)
		{
			this.GenerateKeyword(TSqlTokenType.Trigger);
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.On);
			this.GenerateSpaceAndFragmentIfNotNull(node.TriggerObject);
			this.GenerateCommaSeparatedWithClause<TriggerOption>(node.Options, true, false);
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<TriggerType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._triggerTypeGenerators, node.TriggerType);
			if (valueForEnumKey != null)
			{
				this.NewLineAndIndent();
				this.GenerateTokenList(valueForEnumKey);
			}
			if (node.TriggerActions != null && node.TriggerActions.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<TriggerAction>(node.TriggerActions);
			}
			if (node.WithAppend)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("APPEND");
			}
			if (node.IsNotForReplication)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.Not);
				this.GenerateSpaceAndKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Replication);
			}
			this.NewLineAndIndent();
			this.GenerateKeyword(TSqlTokenType.As);
			this.GenerateSpace();
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.StatementList);
			this.PopAlignmentPoint();
			this.GenerateSpaceAndFragmentIfNotNull(node.MethodSpecifier);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00089B55 File Offset: 0x00087D55
		public override void ExplicitVisit(TruncateTableStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Truncate);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Table);
			this.GenerateSpaceAndFragmentIfNotNull(node.TableName);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00089B7C File Offset: 0x00087D7C
		public override void ExplicitVisit(TryCastCall node)
		{
			this.GenerateIdentifier("TRY_CAST");
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.Parameter);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00089BD8 File Offset: 0x00087DD8
		public override void ExplicitVisit(TryCatchStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeyword(TSqlTokenType.Begin);
			this.GenerateSpaceAndIdentifier("TRY");
			if (node.TryStatements.Statements.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateFragmentIfNotNull(node.TryStatements);
				this.PopAlignmentPoint();
			}
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.End);
			this.GenerateSpaceAndIdentifier("TRY");
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.Begin);
			this.GenerateSpaceAndIdentifier("CATCH");
			if (node.CatchStatements.Statements.Count > 0)
			{
				this.NewLineAndIndent();
				this.MarkAndPushAlignmentPoint(alignmentPoint);
				this.GenerateFragmentIfNotNull(node.CatchStatements);
				this.PopAlignmentPoint();
			}
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.End);
			this.GenerateSpaceAndIdentifier("CATCH");
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00089CB0 File Offset: 0x00087EB0
		public override void ExplicitVisit(TryConvertCall node)
		{
			this.GenerateKeyword(TSqlTokenType.TryConvert);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.DataType);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.Parameter);
			if (node.Style != null)
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				this.GenerateSpaceAndFragmentIfNotNull(node.Style);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00089D2C File Offset: 0x00087F2C
		public override void ExplicitVisit(TryParseCall node)
		{
			this.GenerateIdentifier("TRY_PARSE");
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.StringValue);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			if (node.Culture != null)
			{
				this.GenerateSpace();
				this.GenerateIdentifier("USING");
				this.GenerateSpaceAndFragmentIfNotNull(node.Culture);
			}
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00089DAC File Offset: 0x00087FAC
		public override void ExplicitVisit(TSEqualCall node)
		{
			this.GenerateKeyword(TSqlTokenType.TSEqual);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.FirstExpression);
			this.GenerateSymbol(TSqlTokenType.Comma);
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondExpression);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00089DFD File Offset: 0x00087FFD
		public override void ExplicitVisit(UserDefinedTypePropertyAccess node)
		{
			this.GenerateFragmentIfNotNull(node.CallTarget);
			this.GenerateFragmentIfNotNull(node.PropertyName);
			this.GenerateSpaceAndCollation(node.Collation);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00089E24 File Offset: 0x00088024
		public override void ExplicitVisit(UniqueConstraintDefinition node)
		{
			this.GenerateConstraintHead(node);
			if (node.IsPrimaryKey)
			{
				this.GenerateKeyword(TSqlTokenType.Primary);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
			}
			else
			{
				this.GenerateKeyword(TSqlTokenType.Unique);
			}
			if (node.Clustered != null)
			{
				this.GenerateSpaceAndKeyword(node.Clustered.Value ? TSqlTokenType.Clustered : TSqlTokenType.NonClustered);
			}
			if (node.Columns.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<ColumnWithSortOrder>(node.Columns);
			}
			if (node.IndexOptions.Count > 0)
			{
				this.GenerateIndexOptions(node.IndexOptions);
			}
			if (node.OnFileGroupOrPartitionScheme != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				this.GenerateSpaceAndFragmentIfNotNull(node.OnFileGroupOrPartitionScheme);
			}
			this.GenerateFileStreamOn(node);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00089EE8 File Offset: 0x000880E8
		public override void ExplicitVisit(UnpivotedTableReference node)
		{
			this.GenerateFragmentIfNotNull(node.TableReference);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Unpivot);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.ValueColumn);
			this.GenerateSpaceAndKeyword(TSqlTokenType.For);
			this.GenerateSpaceAndFragmentIfNotNull(node.PivotColumn);
			this.GenerateSpaceAndKeyword(TSqlTokenType.In);
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<ColumnReferenceExpression>(node.InColumns, true);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			this.GenerateSpaceAndAlias(node.Alias);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00089F6C File Offset: 0x0008816C
		public override void ExplicitVisit(UnqualifiedJoin node)
		{
			this.GenerateFragmentIfNotNull(node.FirstTableReference);
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<UnqualifiedJoinType, List<TokenGenerator>>(SqlScriptGeneratorVisitor._unqualifiedJoinTypeGenerators, node.UnqualifiedJoinType);
			if (valueForEnumKey != null)
			{
				this.GenerateSpace();
				this.GenerateTokenList(valueForEnumKey);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SecondTableReference);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00089FB2 File Offset: 0x000881B2
		public override void ExplicitVisit(UpdateCall node)
		{
			this.GenerateKeyword(TSqlTokenType.Update);
			this.GenerateSpace();
			this.GenerateParenthesisedFragmentIfNotNull(node.Identifier);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00089FD1 File Offset: 0x000881D1
		public override void ExplicitVisit(UpdateForClause node)
		{
			this.GenerateKeyword(TSqlTokenType.Update);
			if (node.Columns.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Of);
				this.GenerateSpace();
			}
			this.GenerateCommaSeparatedList<ColumnReferenceExpression>(node.Columns);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0008A008 File Offset: 0x00088208
		public override void ExplicitVisit(UpdateStatisticsStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Update);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Statistics);
			this.GenerateSpaceAndFragmentIfNotNull(node.SchemaObjectName);
			if (node.SubElements.Count > 0)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.SubElements);
			}
			if (node.StatisticsOptions.Count > 0)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<StatisticsOption>(node.StatisticsOptions);
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0008A088 File Offset: 0x00088288
		public override void ExplicitVisit(UpdateTextStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.UpdateText);
			this.GenerateSpace();
			this.GenerateBulkColumnTimestamp(node);
			this.GenerateSpaceAndFragmentIfNotNull(node.InsertOffset);
			this.GenerateSpaceAndFragmentIfNotNull(node.DeleteLength);
			this.NewLine();
			this.GenerateKeyword(TSqlTokenType.With);
			this.GenerateSpaceAndIdentifier("LOG");
			if (node.SourceColumn != null)
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.SourceColumn);
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SourceParameter);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0008A104 File Offset: 0x00088304
		public override void ExplicitVisit(UserLoginOption node)
		{
			switch (node.UserLoginOptionType)
			{
			case UserLoginOptionType.Login:
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("LOGIN");
				this.GenerateSpaceAndFragmentIfNotNull(node.Identifier);
				return;
			case UserLoginOptionType.Certificate:
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("CERTIFICATE");
				this.GenerateSpaceAndFragmentIfNotNull(node.Identifier);
				return;
			case UserLoginOptionType.AsymmetricKey:
				this.GenerateKeyword(TSqlTokenType.For);
				this.GenerateSpaceAndIdentifier("ASYMMETRIC");
				this.GenerateSpaceAndKeyword(TSqlTokenType.Key);
				this.GenerateSpaceAndFragmentIfNotNull(node.Identifier);
				return;
			case UserLoginOptionType.WithoutLogin:
				this.GenerateIdentifier("WITHOUT");
				this.GenerateSpaceAndIdentifier("LOGIN");
				return;
			default:
				return;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0008A1AD File Offset: 0x000883AD
		public override void ExplicitVisit(UseStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Use);
			this.GenerateSpaceAndFragmentIfNotNull(node.DatabaseName);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0008A1C8 File Offset: 0x000883C8
		public static TValue GetValueForEnumKey<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key) where TKey : struct, IConvertible
		{
			TValue tvalue = default(TValue);
			dict.TryGetValue(key, ref tvalue);
			return tvalue;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0008A1E8 File Offset: 0x000883E8
		protected void GenerateFragmentList<T>(IList<T> fragmentList) where T : TSqlFragment
		{
			bool flag = true;
			foreach (T t in fragmentList)
			{
				TSqlFragment tsqlFragment = t;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
				}
				this.GenerateFragmentIfNotNull(tsqlFragment);
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0008A24C File Offset: 0x0008844C
		protected void GenerateOptionStateWithEqualSign(string optionName, OptionState optionState)
		{
			this.GenerateOptionState(optionName, optionState, true);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0008A257 File Offset: 0x00088457
		protected void GenerateOptionState(string optionName, OptionState optionState)
		{
			this.GenerateOptionState(optionName, optionState, false);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0008A262 File Offset: 0x00088462
		protected void GenerateOptionState(string optionName, OptionState optionState, bool generateEqualSign)
		{
			if (optionState != OptionState.NotSet)
			{
				this.GenerateIdentifier(optionName);
				if (generateEqualSign)
				{
					this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				}
				this.GenerateSpace();
				this.GenerateOptionStateOnOff(optionState);
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0008A289 File Offset: 0x00088489
		protected void GenerateOptionStateOnOff(OptionState optionState)
		{
			if (optionState == OptionState.On)
			{
				this.GenerateKeyword(TSqlTokenType.On);
				return;
			}
			if (optionState == OptionState.Off)
			{
				this.GenerateKeyword(TSqlTokenType.Off);
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0008A2A4 File Offset: 0x000884A4
		protected void GenerateOptionStateInSql80Style(string optionName, OptionState optionState)
		{
			if (optionState == OptionState.On)
			{
				this.GenerateIdentifier(optionName);
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0008A2B1 File Offset: 0x000884B1
		protected void GenerateNameEqualsValue(string name, TSqlFragment value)
		{
			this.GenerateTokenAndEqualSign(name);
			this.GenerateSpaceAndFragmentIfNotNull(value);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0008A2C1 File Offset: 0x000884C1
		protected void GenerateNameEqualsValue(string name, string value)
		{
			this.GenerateTokenAndEqualSign(name);
			this.GenerateSpaceAndIdentifier(value);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0008A2D1 File Offset: 0x000884D1
		protected void GenerateNameEqualsValue(TSqlTokenType keywordId, TSqlFragment value)
		{
			this.GenerateTokenAndEqualSign(keywordId);
			this.GenerateSpaceAndFragmentIfNotNull(value);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0008A2E1 File Offset: 0x000884E1
		protected void GenerateNameEqualsValue(TSqlTokenType keywordId, string value)
		{
			this.GenerateTokenAndEqualSign(keywordId);
			this.GenerateSpaceAndIdentifier(value);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0008A2F1 File Offset: 0x000884F1
		protected void GenerateNameEqualsValue(TokenGenerator generator, TSqlFragment value)
		{
			this.GenerateToken(generator);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(value);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0008A30C File Offset: 0x0008850C
		protected void GenerateNameEqualsValue(TokenGenerator generator, string value)
		{
			this.GenerateToken(generator);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndIdentifier(value);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0008A327 File Offset: 0x00088527
		protected void GenerateTokenAndEqualSign(string idText)
		{
			this.GenerateIdentifierWithoutCasing(idText);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0008A33B File Offset: 0x0008853B
		protected void GenerateTokenAndEqualSign(TSqlTokenType keywordId)
		{
			this.GenerateKeyword(keywordId);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0008A34F File Offset: 0x0008854F
		protected void GenerateFragmentIfNotNull(TSqlFragment fragment)
		{
			if (fragment != null)
			{
				fragment.Accept(this);
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0008A35B File Offset: 0x0008855B
		protected void GenerateSpaceAndFragmentIfNotNull(TSqlFragment fragment)
		{
			if (fragment != null)
			{
				this.GenerateSpace();
				this.GenerateFragmentIfNotNull(fragment);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0008A36D File Offset: 0x0008856D
		protected void GenerateParenthesisedFragmentIfNotNull(TSqlFragment fragment)
		{
			if (fragment != null)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentIfNotNull(fragment);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0008A38F File Offset: 0x0008858F
		protected void GenerateCommaSeparatedList<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateCommaSeparatedList<T>(list, false);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0008A3D4 File Offset: 0x000885D4
		protected void GenerateCommaSeparatedList<T>(IList<T> list, bool insertNewLine) where T : TSqlFragment
		{
			this.GenerateList<T>(list, delegate
			{
				this.GenerateSymbol(TSqlTokenType.Comma);
				if (insertNewLine)
				{
					this.NewLine();
					return;
				}
				this.GenerateSpace();
			});
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0008A415 File Offset: 0x00088615
		protected void GenerateDotSeparatedList<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateList<T>(list, delegate
			{
				this.GenerateSymbol(TSqlTokenType.Dot);
			});
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0008A432 File Offset: 0x00088632
		protected void GenerateSpaceSeparatedList<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateList<T>(list, delegate
			{
				this.GenerateSpace();
			});
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0008A448 File Offset: 0x00088648
		private void GenerateList<T>(IList<T> list, Action gen) where T : TSqlFragment
		{
			if (list != null)
			{
				bool flag = true;
				foreach (T t in list)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						gen.Invoke();
					}
					this.GenerateFragmentIfNotNull(t);
				}
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0008A4A8 File Offset: 0x000886A8
		protected void GenerateParenthesisedCommaSeparatedList<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateParenthesisedCommaSeparatedList<T>(list, false);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0008A4B4 File Offset: 0x000886B4
		protected void GenerateParenthesisedCommaSeparatedList<T>(IList<T> list, bool alwaysGenerateParenthses) where T : TSqlFragment
		{
			if (list != null && list.Count > 0)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateCommaSeparatedList<T>(list);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				return;
			}
			if (alwaysGenerateParenthses)
			{
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0008A504 File Offset: 0x00088704
		protected void GenerateFragmentList<T>(IList<T> list, SqlScriptGeneratorVisitor.ListGenerationOption option) where T : TSqlFragment
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			AlignmentPoint alignmentPoint2 = new AlignmentPoint();
			bool flag = option.AlwaysGenerateParenthesis || (list.Count > 0 && option.Parenthesised);
			if (flag)
			{
				if (option.NewLineBeforeOpenParenthesis)
				{
					this.NewLine();
				}
				else
				{
					this.GenerateSpace();
				}
				if (option.IndentParentheses)
				{
					this.Indent();
				}
				this.Mark(alignmentPoint);
				this.GenerateSymbol(TSqlTokenType.LeftParenthesis);
				if (option.NewLineAfterOpenParenthesis)
				{
					this.NewLine();
				}
			}
			bool flag2 = true;
			foreach (T t in list)
			{
				if (flag2)
				{
					if (option.NewLineBeforeFirstItem && !option.NewLineAfterOpenParenthesis)
					{
						this.NewLine();
					}
					flag2 = false;
				}
				else
				{
					this.GenerateSeparator(option);
					if (option.NewLineBeforeItems)
					{
						this.NewLine();
					}
				}
				for (int i = 0; i < option.MultipleIndentItems; i++)
				{
					this.Indent();
				}
				if (option.NewLineBeforeItems)
				{
					this.Mark(alignmentPoint2);
				}
				this.GenerateFragmentIfNotNull(t);
			}
			if (flag)
			{
				if (option.NewLineBeforeCloseParenthesis)
				{
					this.NewLine();
					if (option.AlignParentheses)
					{
						this.Mark(alignmentPoint);
					}
				}
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0008A654 File Offset: 0x00088854
		protected void GenerateAlignedParenthesizedOptionsWithMultipleIndent<T>(IList<T> list, int indentValue) where T : TSqlFragment
		{
			SqlScriptGeneratorVisitor.ListGenerationOption listGenerationOption = SqlScriptGeneratorVisitor.ListGenerationOption.CreateOptionFromFormattingConfig(this._options);
			listGenerationOption.AlignParentheses = true;
			listGenerationOption.MultipleIndentItems = indentValue;
			if (list.Count > 0)
			{
				this.GenerateFragmentList<T>(list, listGenerationOption);
				return;
			}
			this.GenerateParenthesisedCommaSeparatedList<T>(list, true);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0008A695 File Offset: 0x00088895
		protected void GenerateAlignedParenthesizedOptions<T>(IList<T> list) where T : TSqlFragment
		{
			this.GenerateAlignedParenthesizedOptionsWithMultipleIndent<T>(list, 0);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0008A6A0 File Offset: 0x000888A0
		private void GenerateSeparator(SqlScriptGeneratorVisitor.ListGenerationOption option)
		{
			switch (option.Separator)
			{
			case SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType.Comma:
				this.GenerateSymbol(TSqlTokenType.Comma);
				if (!option.NewLineBeforeItems)
				{
					this.GenerateSpace();
					return;
				}
				break;
			case SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType.Dot:
				this.GenerateSymbol(TSqlTokenType.Dot);
				return;
			case SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType.Space:
				this.GenerateSpace();
				break;
			default:
				return;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0008A6F3 File Offset: 0x000888F3
		protected void GenerateSpace()
		{
			this._writer.AddToken(ScriptGeneratorSupporter.CreateWhitespaceToken(1));
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0008A706 File Offset: 0x00088906
		protected void GenerateKeyword(TSqlTokenType keywordId)
		{
			this._writer.AddKeyword(keywordId);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0008A714 File Offset: 0x00088914
		protected void GenerateKeywordAndSpace(TSqlTokenType keywordId)
		{
			this.GenerateKeyword(keywordId);
			this.GenerateSpace();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0008A723 File Offset: 0x00088923
		protected void GenerateSpaceAndKeyword(TSqlTokenType keywordId)
		{
			this.GenerateSpace();
			this.GenerateKeyword(keywordId);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0008A732 File Offset: 0x00088932
		protected void GenerateSymbol(TSqlTokenType symbolId)
		{
			this.GenerateKeyword(symbolId);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0008A73C File Offset: 0x0008893C
		protected void GenerateToken(TSqlTokenType tokenType, string text)
		{
			TSqlParserToken tsqlParserToken = new TSqlParserToken(tokenType, text);
			this._writer.AddToken(tsqlParserToken);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0008A75D File Offset: 0x0008895D
		protected void GenerateSpaceAndSymbol(TSqlTokenType symbolId)
		{
			this.GenerateSpace();
			this.GenerateSymbol(symbolId);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0008A76C File Offset: 0x0008896C
		protected void GenerateSymbolAndSpace(TSqlTokenType symbolId)
		{
			this.GenerateSymbol(symbolId);
			this.GenerateSpace();
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0008A77B File Offset: 0x0008897B
		protected void GenerateIdentifier(string text)
		{
			this._writer.AddIdentifierWithCasing(text);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0008A789 File Offset: 0x00088989
		protected void GenerateIdentifierWithoutCheck(string text)
		{
			this._writer.AddIdentifierWithoutCasing(text);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0008A797 File Offset: 0x00088997
		protected void GenerateIdentifierWithoutCasing(string text)
		{
			this._writer.AddIdentifierWithoutCasing(text);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0008A7A5 File Offset: 0x000889A5
		protected void GenerateSpaceAndIdentifier(string idText)
		{
			this.GenerateSpace();
			this.GenerateIdentifier(idText);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0008A7B4 File Offset: 0x000889B4
		protected void GenerateToken(TSqlTokenType tokenType, string text, bool applyCasing)
		{
			if (applyCasing)
			{
				text = ScriptGeneratorSupporter.GetCasedString(text, this._options.KeywordCasing);
			}
			TSqlParserToken tsqlParserToken = new TSqlParserToken(tokenType, text);
			this._writer.AddToken(tsqlParserToken);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0008A7EC File Offset: 0x000889EC
		protected void GenerateCommaSeparatedFlagOpitons<TKey>(Dictionary<TKey, TokenGenerator> optionsGenerators, TKey options) where TKey : struct, IConvertible
		{
			bool flag = true;
			ulong num = Convert.ToUInt64(options, CultureInfo.InvariantCulture);
			foreach (TKey tkey in optionsGenerators.Keys)
			{
				ulong num2 = Convert.ToUInt64(tkey, CultureInfo.InvariantCulture);
				if ((num2 & num) == num2)
				{
					TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<TKey, TokenGenerator>(optionsGenerators, tkey);
					if (valueForEnumKey != null)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
						}
						this.GenerateToken(valueForEnumKey);
					}
				}
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0008A88C File Offset: 0x00088A8C
		protected void GenerateToken(TokenGenerator generator)
		{
			generator.Generate(this._writer);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0008A89C File Offset: 0x00088A9C
		protected void GenerateTokenList(List<TokenGenerator> generatorList)
		{
			foreach (TokenGenerator tokenGenerator in generatorList)
			{
				tokenGenerator.Generate(this._writer);
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0008A8F0 File Offset: 0x00088AF0
		protected void GenerateSpaceSeparatedTokens(TSqlTokenType keywordId, params string[] identifiers)
		{
			this.GenerateKeyword(keywordId);
			foreach (string text in identifiers)
			{
				this.GenerateSpaceAndIdentifier(text);
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0008A920 File Offset: 0x00088B20
		protected void GenerateSpaceSeparatedTokens(params TSqlTokenType[] keywords)
		{
			bool flag = true;
			foreach (TSqlTokenType tsqlTokenType in keywords)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.GenerateSpace();
				}
				this.GenerateKeyword(tsqlTokenType);
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0008A958 File Offset: 0x00088B58
		protected void GenerateSpaceSeparatedTokens(params string[] identifiers)
		{
			bool flag = true;
			foreach (string text in identifiers)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.GenerateSpace();
				}
				this.GenerateIdentifier(text);
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0008A98F File Offset: 0x00088B8F
		protected void GenerateFragmentWithAlignmentPointIfNotNull(TSqlFragment node, AlignmentPoint ap)
		{
			if (node != null && ap != null)
			{
				this.AddAlignmentPointForFragment(node, ap);
				this.GenerateFragmentIfNotNull(node);
				this.ClearAlignmentPointsForFragment(node);
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0008A9B0 File Offset: 0x00088BB0
		public override void ExplicitVisit(ViewOption node)
		{
			string valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<ViewOptionKind, string>(SqlScriptGeneratorVisitor._viewOptionTypeNames, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateIdentifier(valueForEnumKey);
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0008A9D8 File Offset: 0x00088BD8
		protected void GenerateViewStatementBody(ViewStatementBody node)
		{
			this.GenerateKeyword(TSqlTokenType.View);
			this.GenerateSpaceAndFragmentIfNotNull(node.SchemaObjectName);
			if (node.Columns.Count > 0)
			{
				if (this._options.MultilineViewColumnsList)
				{
					SqlScriptGeneratorVisitor.ListGenerationOption listGenerationOption = SqlScriptGeneratorVisitor.ListGenerationOption.CreateOptionFromFormattingConfig(this._options);
					this.GenerateFragmentList<Identifier>(node.Columns, listGenerationOption);
				}
				else
				{
					this.GenerateSpace();
					this.GenerateParenthesisedCommaSeparatedList<Identifier>(node.Columns);
				}
			}
			if (node.ViewOptions.Count > 0)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<ViewOption>(node.ViewOptions);
			}
			this.GenerateNewLineOrSpace(this._options.AsKeywordOnOwnLine);
			this.GenerateKeyword(TSqlTokenType.As);
			this.NewLine();
			if (this._options.IndentViewBody)
			{
				this.Indent();
			}
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			bool generateSemiColon = this._generateSemiColon;
			this._generateSemiColon = false;
			this.GenerateFragmentIfNotNull(node.SelectStatement);
			this._generateSemiColon = generateSemiColon;
			this.PopAlignmentPoint();
			if (node.WithCheckOption)
			{
				this.NewLine();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Check);
				this.GenerateSpaceAndKeyword(TSqlTokenType.Option);
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0008AB04 File Offset: 0x00088D04
		public override void ExplicitVisit(WaitForStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.WaitFor);
			if (node.WaitForOption == WaitForOption.Statement)
			{
				this.GenerateSpace();
				bool generateSemiColon = this._generateSemiColon;
				this._generateSemiColon = false;
				this.GenerateParenthesisedFragmentIfNotNull(node.Statement);
				this._generateSemiColon = generateSemiColon;
				if (node.Timeout != null)
				{
					this.GenerateSymbolAndSpace(TSqlTokenType.Comma);
					this.GenerateSpaceAndIdentifier("TIMEOUT");
					this.GenerateSpaceAndFragmentIfNotNull(node.Timeout);
					return;
				}
			}
			else
			{
				TokenGenerator valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<WaitForOption, TokenGenerator>(SqlScriptGeneratorVisitor._waitForOptionGenerators, node.WaitForOption);
				if (valueForEnumKey != null)
				{
					this.GenerateSpace();
					this.GenerateToken(valueForEnumKey);
				}
				this.GenerateSpaceAndFragmentIfNotNull(node.Parameter);
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0008ABA4 File Offset: 0x00088DA4
		public override void ExplicitVisit(SimpleWhenClause node)
		{
			this.GenerateKeyword(TSqlTokenType.When);
			this.GenerateSpaceAndFragmentIfNotNull(node.WhenExpression);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Then);
			this.GenerateSpaceAndFragmentIfNotNull(node.ThenExpression);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0008ABD4 File Offset: 0x00088DD4
		public override void ExplicitVisit(SearchedWhenClause node)
		{
			this.GenerateKeyword(TSqlTokenType.When);
			this.GenerateSpaceAndFragmentIfNotNull(node.WhenExpression);
			this.GenerateSpaceAndKeyword(TSqlTokenType.Then);
			this.GenerateSpaceAndFragmentIfNotNull(node.ThenExpression);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0008AC04 File Offset: 0x00088E04
		public override void ExplicitVisit(WhileStatement node)
		{
			AlignmentPoint alignmentPoint = new AlignmentPoint();
			this.GenerateKeyword(TSqlTokenType.While);
			this.GenerateSpaceAndFragmentIfNotNull(node.Predicate);
			this.NewLineAndIndent();
			this.MarkAndPushAlignmentPoint(alignmentPoint);
			this.GenerateFragmentIfNotNull(node.Statement);
			this.GenerateSemiColonWhenNecessary(node.Statement);
			this.PopAlignmentPoint();
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0008AC5C File Offset: 0x00088E5C
		public override void ExplicitVisit(WindowsCreateLoginSource node)
		{
			this.GenerateKeyword(TSqlTokenType.From);
			this.GenerateSpaceAndIdentifier("WINDOWS");
			if (node.Options != null && node.Options.Count > 0)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpace();
				this.GenerateCommaSeparatedList<PrincipalOption>(node.Options);
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0008ACB0 File Offset: 0x00088EB0
		public override void ExplicitVisit(WindowFrameClause node)
		{
			switch (node.WindowFrameType)
			{
			case WindowFrameType.Rows:
				this.GenerateIdentifier("ROWS");
				break;
			case WindowFrameType.Range:
				this.GenerateIdentifier("RANGE");
				break;
			}
			if (node.Bottom != null)
			{
				this.GenerateSpaceAndKeyword(TSqlTokenType.Between);
				this.GenerateSpaceAndFragmentIfNotNull(node.Top);
				this.GenerateSpaceAndKeyword(TSqlTokenType.And);
				this.GenerateSpaceAndFragmentIfNotNull(node.Bottom);
				return;
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.Top);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0008AD28 File Offset: 0x00088F28
		public override void ExplicitVisit(WindowDelimiter node)
		{
			switch (node.WindowDelimiterType)
			{
			case WindowDelimiterType.UnboundedPreceding:
				this.GenerateIdentifier("UNBOUNDED");
				this.GenerateSpaceAndIdentifier("PRECEDING");
				return;
			case WindowDelimiterType.ValuePreceding:
				this.GenerateFragmentIfNotNull(node.OffsetValue);
				this.GenerateSpaceAndIdentifier("PRECEDING");
				return;
			case WindowDelimiterType.CurrentRow:
				this.GenerateKeyword(TSqlTokenType.Current);
				this.GenerateSpaceAndIdentifier("ROW");
				return;
			case WindowDelimiterType.ValueFollowing:
				this.GenerateFragmentIfNotNull(node.OffsetValue);
				this.GenerateSpaceAndIdentifier("FOLLOWING");
				return;
			case WindowDelimiterType.UnboundedFollowing:
				this.GenerateIdentifier("UNBOUNDED");
				this.GenerateSpaceAndIdentifier("FOLLOWING");
				return;
			default:
				return;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0008ADC8 File Offset: 0x00088FC8
		public override void ExplicitVisit(WithinGroupClause node)
		{
			this.GenerateIdentifier("WITHIN");
			this.GenerateSpaceAndKeyword(TSqlTokenType.Group);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			AlignmentPoint alignmentPoint = new AlignmentPoint("ClauseBody");
			this.GenerateFragmentWithAlignmentPointIfNotNull(node.OrderByClause, alignmentPoint);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0008AE18 File Offset: 0x00089018
		public override void ExplicitVisit(WriteTextStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.WriteText);
			this.GenerateSpace();
			this.GenerateBulkColumnTimestamp(node);
			if (node.WithLog)
			{
				this.NewLineAndIndent();
				this.GenerateKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("LOG");
			}
			this.GenerateSpaceAndFragmentIfNotNull(node.SourceParameter);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0008AE6D File Offset: 0x0008906D
		public override void ExplicitVisit(WsdlPayloadOption node)
		{
			if (node.IsNone)
			{
				this.GenerateNameEqualsValue("WSDL", "NONE");
				return;
			}
			this.GenerateNameEqualsValue("WSDL", node.Value);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0008AE99 File Offset: 0x00089099
		public override void ExplicitVisit(XmlForClause node)
		{
			this.GenerateIdentifier("XML");
			this.GenerateSpace();
			this.GenerateCommaSeparatedList<XmlForClauseOption>(node.Options);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0008AEB8 File Offset: 0x000890B8
		public override void ExplicitVisit(XmlForClauseOption node)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<XmlForClauseOptions, List<TokenGenerator>>(SqlScriptGeneratorVisitor._xmlForClauseOptionsGenerators, node.OptionKind);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
			if (node.Value != null)
			{
				this.GenerateSpace();
				this.GenerateParenthesisedFragmentIfNotNull(node.Value);
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0008AEFA File Offset: 0x000890FA
		public override void ExplicitVisit(XmlNamespaces node)
		{
			this.GenerateIdentifier("XMLNAMESPACES");
			this.GenerateSpace();
			this.GenerateParenthesisedCommaSeparatedList<XmlNamespacesElement>(node.XmlNamespacesElements);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0008AF19 File Offset: 0x00089119
		public override void ExplicitVisit(XmlNamespacesAliasElement node)
		{
			this.GenerateFragmentIfNotNull(node.String);
			this.GenerateSpaceAndKeyword(TSqlTokenType.As);
			this.GenerateSpaceAndFragmentIfNotNull(node.Identifier);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0008AF3B File Offset: 0x0008913B
		public override void ExplicitVisit(XmlNamespacesDefaultElement node)
		{
			this.GenerateKeyword(TSqlTokenType.Default);
			this.GenerateSpaceAndFragmentIfNotNull(node.String);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0008AF51 File Offset: 0x00089151
		protected void GenerateConstraintHead(ConstraintDefinition node)
		{
			if (node.ConstraintIdentifier != null)
			{
				this.GenerateKeyword(TSqlTokenType.Constraint);
				this.GenerateSpaceAndFragmentIfNotNull(node.ConstraintIdentifier);
				this.GenerateSpace();
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0008AF78 File Offset: 0x00089178
		protected void GenerateDeleteUpdateAction(DeleteUpdateAction action)
		{
			List<TokenGenerator> valueForEnumKey = SqlScriptGeneratorVisitor.GetValueForEnumKey<DeleteUpdateAction, List<TokenGenerator>>(SqlScriptGeneratorVisitor._deleteUpdateActionGenerators, action);
			if (valueForEnumKey != null)
			{
				this.GenerateTokenList(valueForEnumKey);
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0008AF9B File Offset: 0x0008919B
		public SqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter writer)
		{
			this._options = options;
			this._writer = writer;
			this._alignmentPointsForFragments = new Dictionary<TSqlFragment, Dictionary<string, AlignmentPoint>>();
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0008AFC3 File Offset: 0x000891C3
		protected void NewLineAndIndent()
		{
			this.NewLine();
			this.Indent();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0008AFD1 File Offset: 0x000891D1
		protected void Indent()
		{
			this.Indent(this._options.IndentationSize);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0008AFE4 File Offset: 0x000891E4
		protected void NewLineAndIndent(int indentSize)
		{
			this.NewLine();
			this.Indent(indentSize);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0008AFF3 File Offset: 0x000891F3
		protected void Indent(int indentSize)
		{
			this._writer.Indent(indentSize);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0008B001 File Offset: 0x00089201
		protected void Mark(AlignmentPoint ap)
		{
			this._writer.Mark(ap);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0008B00F File Offset: 0x0008920F
		protected void NewLine()
		{
			this._writer.NewLine();
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0008B01C File Offset: 0x0008921C
		protected void PushAlignmentPoint(AlignmentPoint ap)
		{
			this._writer.PushNewLineAlignmentPoint(ap);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0008B02A File Offset: 0x0008922A
		protected void PopAlignmentPoint()
		{
			this._writer.PopNewLineAlignmentPoint();
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0008B037 File Offset: 0x00089237
		protected void MarkAndPushAlignmentPoint(AlignmentPoint ap)
		{
			this.Mark(ap);
			this.PushAlignmentPoint(ap);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0008B047 File Offset: 0x00089247
		protected AlignmentPoint FindOrCreateAlignmentPointByName(string apName)
		{
			return this._writer.FindOrCreateAlignmentPoint(apName);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0008B058 File Offset: 0x00089258
		protected void AddAlignmentPointForFragment(TSqlFragment node, AlignmentPoint ap)
		{
			if (node != null && ap != null && !string.IsNullOrEmpty(ap.Name))
			{
				Dictionary<string, AlignmentPoint> dictionary;
				if (!this._alignmentPointsForFragments.TryGetValue(node, ref dictionary))
				{
					dictionary = new Dictionary<string, AlignmentPoint>();
					this._alignmentPointsForFragments.Add(node, dictionary);
				}
				if (!string.IsNullOrEmpty(ap.Name) && !dictionary.ContainsKey(ap.Name))
				{
					dictionary.Add(ap.Name, ap);
				}
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0008B0C4 File Offset: 0x000892C4
		protected AlignmentPoint GetAlignmentPointForFragment(TSqlFragment node, string name)
		{
			AlignmentPoint alignmentPoint = null;
			Dictionary<string, AlignmentPoint> dictionary;
			if (node != null && !string.IsNullOrEmpty(name) && this._alignmentPointsForFragments.TryGetValue(node, ref dictionary) && !dictionary.TryGetValue(name, ref alignmentPoint))
			{
				alignmentPoint = null;
			}
			return alignmentPoint;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0008B0FC File Offset: 0x000892FC
		protected void ClearAlignmentPointsForFragment(TSqlFragment node)
		{
			if (node != null)
			{
				this._alignmentPointsForFragments.Remove(node);
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0008B110 File Offset: 0x00089310
		public override void ExplicitVisit(CreateFederationStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Create);
			this.GenerateIdentifier("FEDERATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
			this.GenerateFragmentIfNotNull(node.DistributionName);
			this.GenerateSpaceAndFragmentIfNotNull(node.DataType);
			this.GenerateSpaceAndIdentifier("RANGE");
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0008B178 File Offset: 0x00089378
		public override void ExplicitVisit(AlterFederationStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Alter);
			this.GenerateIdentifier("FEDERATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
			switch (node.Kind)
			{
			case AlterFederationKind.Split:
				this.GenerateSpaceAndIdentifier("SPLIT");
				this.GenerateSpaceAndIdentifier("AT");
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				break;
			case AlterFederationKind.DropLow:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Drop);
				this.GenerateSpaceAndIdentifier("AT");
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateIdentifier("LOW");
				this.GenerateSpace();
				break;
			case AlterFederationKind.DropHigh:
				this.GenerateSpaceAndKeyword(TSqlTokenType.Drop);
				this.GenerateSpaceAndIdentifier("AT");
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateIdentifier("HIGH");
				this.GenerateSpace();
				break;
			}
			this.GenerateFragmentIfNotNull(node.DistributionName);
			this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
			this.GenerateSpaceAndFragmentIfNotNull(node.Boundary);
			this.GenerateSymbol(TSqlTokenType.RightParenthesis);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0008B274 File Offset: 0x00089474
		public override void ExplicitVisit(DropFederationStatement node)
		{
			this.GenerateKeywordAndSpace(TSqlTokenType.Drop);
			this.GenerateIdentifier("FEDERATION");
			this.GenerateSpaceAndFragmentIfNotNull(node.Name);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0008B298 File Offset: 0x00089498
		public override void ExplicitVisit(UseFederationStatement node)
		{
			this.GenerateKeyword(TSqlTokenType.Use);
			this.GenerateSpaceAndIdentifier("FEDERATION");
			if (node.FederationName == null)
			{
				this.GenerateSpaceAndIdentifier("ROOT");
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
			}
			else
			{
				this.GenerateSpaceAndFragmentIfNotNull(node.FederationName);
				this.GenerateSpaceAndSymbol(TSqlTokenType.LeftParenthesis);
				this.GenerateFragmentIfNotNull(node.DistributionName);
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				this.GenerateSpaceAndFragmentIfNotNull(node.Value);
				this.GenerateSymbol(TSqlTokenType.RightParenthesis);
				this.GenerateSpaceAndKeyword(TSqlTokenType.With);
				this.GenerateSpaceAndIdentifier("FILTERING");
				this.GenerateSpaceAndSymbol(TSqlTokenType.EqualsSign);
				if (node.Filtering)
				{
					this.GenerateSpaceAndKeyword(TSqlTokenType.On);
				}
				else
				{
					this.GenerateSpaceAndKeyword(TSqlTokenType.Off);
				}
				this.GenerateSymbol(TSqlTokenType.Comma);
			}
			this.GenerateSpaceAndIdentifier("RESET");
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0008B374 File Offset: 0x00089574
		// Note: this type is marked as 'beforefieldinit'.
		static SqlScriptGeneratorVisitor()
		{
			Dictionary<NonTransactedFileStreamAccess, string> dictionary = new Dictionary<NonTransactedFileStreamAccess, string>();
			dictionary.Add(NonTransactedFileStreamAccess.Full, "FULL");
			dictionary.Add(NonTransactedFileStreamAccess.Off, "OFF");
			dictionary.Add(NonTransactedFileStreamAccess.ReadOnly, "READ_ONLY");
			SqlScriptGeneratorVisitor._nonTransactedFileStreamAccessNames = dictionary;
			Dictionary<BooleanTernaryExpressionType, List<TokenGenerator>> dictionary2 = new Dictionary<BooleanTernaryExpressionType, List<TokenGenerator>>();
			Dictionary<BooleanTernaryExpressionType, List<TokenGenerator>> dictionary3 = dictionary2;
			BooleanTernaryExpressionType booleanTernaryExpressionType = BooleanTernaryExpressionType.Between;
			List<TokenGenerator> list = new List<TokenGenerator>();
			list.Add(new KeywordGenerator(TSqlTokenType.Between));
			dictionary3.Add(booleanTernaryExpressionType, list);
			Dictionary<BooleanTernaryExpressionType, List<TokenGenerator>> dictionary4 = dictionary2;
			BooleanTernaryExpressionType booleanTernaryExpressionType2 = BooleanTernaryExpressionType.NotBetween;
			List<TokenGenerator> list2 = new List<TokenGenerator>();
			list2.Add(new KeywordGenerator(TSqlTokenType.Not, true));
			list2.Add(new KeywordGenerator(TSqlTokenType.Between));
			dictionary4.Add(booleanTernaryExpressionType2, list2);
			SqlScriptGeneratorVisitor._ternaryExpressionTypeGenerators = dictionary2;
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary5 = new Dictionary<OptimizerHintKind, List<TokenGenerator>>();
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary6 = dictionary5;
			OptimizerHintKind optimizerHintKind = OptimizerHintKind.AlterColumnPlan;
			List<TokenGenerator> list3 = new List<TokenGenerator>();
			list3.Add(new IdentifierGenerator("ALTERCOLUMN", true));
			list3.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary6.Add(optimizerHintKind, list3);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary7 = dictionary5;
			OptimizerHintKind optimizerHintKind2 = OptimizerHintKind.BypassOptimizerQueue;
			List<TokenGenerator> list4 = new List<TokenGenerator>();
			list4.Add(new IdentifierGenerator("BYPASS", true));
			list4.Add(new IdentifierGenerator("OPTIMIZER_QUEUE"));
			dictionary7.Add(optimizerHintKind2, list4);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary8 = dictionary5;
			OptimizerHintKind optimizerHintKind3 = OptimizerHintKind.ConcatUnion;
			List<TokenGenerator> list5 = new List<TokenGenerator>();
			list5.Add(new IdentifierGenerator("CONCAT", true));
			list5.Add(new KeywordGenerator(TSqlTokenType.Union));
			dictionary8.Add(optimizerHintKind3, list5);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary9 = dictionary5;
			OptimizerHintKind optimizerHintKind4 = OptimizerHintKind.ExpandViews;
			List<TokenGenerator> list6 = new List<TokenGenerator>();
			list6.Add(new IdentifierGenerator("EXPAND", true));
			list6.Add(new IdentifierGenerator("VIEWS"));
			dictionary9.Add(optimizerHintKind4, list6);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary10 = dictionary5;
			OptimizerHintKind optimizerHintKind5 = OptimizerHintKind.ForceOrder;
			List<TokenGenerator> list7 = new List<TokenGenerator>();
			list7.Add(new IdentifierGenerator("FORCE", true));
			list7.Add(new KeywordGenerator(TSqlTokenType.Order));
			dictionary10.Add(optimizerHintKind5, list7);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary11 = dictionary5;
			OptimizerHintKind optimizerHintKind6 = OptimizerHintKind.HashGroup;
			List<TokenGenerator> list8 = new List<TokenGenerator>();
			list8.Add(new IdentifierGenerator("HASH", true));
			list8.Add(new KeywordGenerator(TSqlTokenType.Group));
			dictionary11.Add(optimizerHintKind6, list8);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary12 = dictionary5;
			OptimizerHintKind optimizerHintKind7 = OptimizerHintKind.HashJoin;
			List<TokenGenerator> list9 = new List<TokenGenerator>();
			list9.Add(new IdentifierGenerator("HASH", true));
			list9.Add(new KeywordGenerator(TSqlTokenType.Join));
			dictionary12.Add(optimizerHintKind7, list9);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary13 = dictionary5;
			OptimizerHintKind optimizerHintKind8 = OptimizerHintKind.HashUnion;
			List<TokenGenerator> list10 = new List<TokenGenerator>();
			list10.Add(new IdentifierGenerator("HASH", true));
			list10.Add(new KeywordGenerator(TSqlTokenType.Union));
			dictionary13.Add(optimizerHintKind8, list10);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary14 = dictionary5;
			OptimizerHintKind optimizerHintKind9 = OptimizerHintKind.KeepFixedPlan;
			List<TokenGenerator> list11 = new List<TokenGenerator>();
			list11.Add(new IdentifierGenerator("KEEPFIXED", true));
			list11.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary14.Add(optimizerHintKind9, list11);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary15 = dictionary5;
			OptimizerHintKind optimizerHintKind10 = OptimizerHintKind.KeepPlan;
			List<TokenGenerator> list12 = new List<TokenGenerator>();
			list12.Add(new IdentifierGenerator("KEEP", true));
			list12.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary15.Add(optimizerHintKind10, list12);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary16 = dictionary5;
			OptimizerHintKind optimizerHintKind11 = OptimizerHintKind.KeepUnion;
			List<TokenGenerator> list13 = new List<TokenGenerator>();
			list13.Add(new IdentifierGenerator("KEEP", true));
			list13.Add(new KeywordGenerator(TSqlTokenType.Union));
			dictionary16.Add(optimizerHintKind11, list13);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary17 = dictionary5;
			OptimizerHintKind optimizerHintKind12 = OptimizerHintKind.LoopJoin;
			List<TokenGenerator> list14 = new List<TokenGenerator>();
			list14.Add(new IdentifierGenerator("LOOP", true));
			list14.Add(new KeywordGenerator(TSqlTokenType.Join));
			dictionary17.Add(optimizerHintKind12, list14);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary18 = dictionary5;
			OptimizerHintKind optimizerHintKind13 = OptimizerHintKind.MergeJoin;
			List<TokenGenerator> list15 = new List<TokenGenerator>();
			list15.Add(new IdentifierGenerator("MERGE", true));
			list15.Add(new KeywordGenerator(TSqlTokenType.Join));
			dictionary18.Add(optimizerHintKind13, list15);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary19 = dictionary5;
			OptimizerHintKind optimizerHintKind14 = OptimizerHintKind.MergeUnion;
			List<TokenGenerator> list16 = new List<TokenGenerator>();
			list16.Add(new IdentifierGenerator("MERGE", true));
			list16.Add(new KeywordGenerator(TSqlTokenType.Union));
			dictionary19.Add(optimizerHintKind14, list16);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary20 = dictionary5;
			OptimizerHintKind optimizerHintKind15 = OptimizerHintKind.OrderGroup;
			List<TokenGenerator> list17 = new List<TokenGenerator>();
			list17.Add(new KeywordGenerator(TSqlTokenType.Order, true));
			list17.Add(new KeywordGenerator(TSqlTokenType.Group));
			dictionary20.Add(optimizerHintKind15, list17);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary21 = dictionary5;
			OptimizerHintKind optimizerHintKind16 = OptimizerHintKind.RobustPlan;
			List<TokenGenerator> list18 = new List<TokenGenerator>();
			list18.Add(new IdentifierGenerator("ROBUST", true));
			list18.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary21.Add(optimizerHintKind16, list18);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary22 = dictionary5;
			OptimizerHintKind optimizerHintKind17 = OptimizerHintKind.ShrinkDBPlan;
			List<TokenGenerator> list19 = new List<TokenGenerator>();
			list19.Add(new IdentifierGenerator("SHRINKDB", true));
			list19.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary22.Add(optimizerHintKind17, list19);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary23 = dictionary5;
			OptimizerHintKind optimizerHintKind18 = OptimizerHintKind.ParameterizationSimple;
			List<TokenGenerator> list20 = new List<TokenGenerator>();
			list20.Add(new IdentifierGenerator("PARAMETERIZATION", true));
			list20.Add(new IdentifierGenerator("SIMPLE"));
			dictionary23.Add(optimizerHintKind18, list20);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary24 = dictionary5;
			OptimizerHintKind optimizerHintKind19 = OptimizerHintKind.ParameterizationForced;
			List<TokenGenerator> list21 = new List<TokenGenerator>();
			list21.Add(new IdentifierGenerator("PARAMETERIZATION", true));
			list21.Add(new IdentifierGenerator("FORCED"));
			dictionary24.Add(optimizerHintKind19, list21);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary25 = dictionary5;
			OptimizerHintKind optimizerHintKind20 = OptimizerHintKind.OptimizeCorrelatedUnionAll;
			List<TokenGenerator> list22 = new List<TokenGenerator>();
			list22.Add(new IdentifierGenerator("OPTIMIZE", true));
			list22.Add(new IdentifierGenerator("CORRELATED", true));
			list22.Add(new KeywordGenerator(TSqlTokenType.Union, true));
			list22.Add(new KeywordGenerator(TSqlTokenType.All));
			dictionary25.Add(optimizerHintKind20, list22);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary26 = dictionary5;
			OptimizerHintKind optimizerHintKind21 = OptimizerHintKind.Recompile;
			List<TokenGenerator> list23 = new List<TokenGenerator>();
			list23.Add(new IdentifierGenerator("RECOMPILE"));
			dictionary26.Add(optimizerHintKind21, list23);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary27 = dictionary5;
			OptimizerHintKind optimizerHintKind22 = OptimizerHintKind.Fast;
			List<TokenGenerator> list24 = new List<TokenGenerator>();
			list24.Add(new IdentifierGenerator("FAST"));
			dictionary27.Add(optimizerHintKind22, list24);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary28 = dictionary5;
			OptimizerHintKind optimizerHintKind23 = OptimizerHintKind.CheckConstraintsPlan;
			List<TokenGenerator> list25 = new List<TokenGenerator>();
			list25.Add(new IdentifierGenerator("CHECKCONSTRAINTS", true));
			list25.Add(new KeywordGenerator(TSqlTokenType.Plan));
			dictionary28.Add(optimizerHintKind23, list25);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary29 = dictionary5;
			OptimizerHintKind optimizerHintKind24 = OptimizerHintKind.MaxRecursion;
			List<TokenGenerator> list26 = new List<TokenGenerator>();
			list26.Add(new IdentifierGenerator("MAXRECURSION"));
			dictionary29.Add(optimizerHintKind24, list26);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary30 = dictionary5;
			OptimizerHintKind optimizerHintKind25 = OptimizerHintKind.MaxDop;
			List<TokenGenerator> list27 = new List<TokenGenerator>();
			list27.Add(new IdentifierGenerator("MAXDOP"));
			dictionary30.Add(optimizerHintKind25, list27);
			Dictionary<OptimizerHintKind, List<TokenGenerator>> dictionary31 = dictionary5;
			OptimizerHintKind optimizerHintKind26 = OptimizerHintKind.IgnoreNonClusteredColumnStoreIndex;
			List<TokenGenerator> list28 = new List<TokenGenerator>();
			list28.Add(new IdentifierGenerator("IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX"));
			dictionary31.Add(optimizerHintKind26, list28);
			SqlScriptGeneratorVisitor._optimizerHintKindsGenerators = dictionary5;
			Dictionary<PageVerifyDatabaseOptionKind, string> dictionary32 = new Dictionary<PageVerifyDatabaseOptionKind, string>();
			dictionary32.Add(PageVerifyDatabaseOptionKind.Checksum, "CHECKSUM");
			dictionary32.Add(PageVerifyDatabaseOptionKind.None, "NONE");
			dictionary32.Add(PageVerifyDatabaseOptionKind.TornPageDetection, "TORN_PAGE_DETECTION");
			SqlScriptGeneratorVisitor._pageVerifyDatabaseOptionKindNames = dictionary32;
			Dictionary<RecoveryDatabaseOptionKind, TokenGenerator> dictionary33 = new Dictionary<RecoveryDatabaseOptionKind, TokenGenerator>();
			dictionary33.Add(RecoveryDatabaseOptionKind.Full, new KeywordGenerator(TSqlTokenType.Full));
			dictionary33.Add(RecoveryDatabaseOptionKind.BulkLogged, new IdentifierGenerator("BULK_LOGGED"));
			dictionary33.Add(RecoveryDatabaseOptionKind.Simple, new IdentifierGenerator("SIMPLE"));
			SqlScriptGeneratorVisitor._recoveryDatabaseOptionKindNames = dictionary33;
			Dictionary<RestoreOptionKind, TokenGenerator> dictionary34 = new Dictionary<RestoreOptionKind, TokenGenerator>();
			dictionary34.Add(RestoreOptionKind.BlockSize, new IdentifierGenerator("BLOCKSIZE"));
			dictionary34.Add(RestoreOptionKind.BufferCount, new IdentifierGenerator("BUFFERCOUNT"));
			dictionary34.Add(RestoreOptionKind.Checksum, new IdentifierGenerator("CHECKSUM"));
			dictionary34.Add(RestoreOptionKind.CommitDifferentialBase, new IdentifierGenerator("COMMIT_DIFFERENTIAL_BASE"));
			dictionary34.Add(RestoreOptionKind.ContinueAfterError, new IdentifierGenerator("CONTINUE_AFTER_ERROR"));
			dictionary34.Add(RestoreOptionKind.DboOnly, new IdentifierGenerator("DBO_ONLY"));
			dictionary34.Add(RestoreOptionKind.EnableBroker, new IdentifierGenerator("ENABLE_BROKER"));
			dictionary34.Add(RestoreOptionKind.EnhancedIntegrity, new IdentifierGenerator("ENHANCEDINTEGRITY"));
			dictionary34.Add(RestoreOptionKind.ErrorBrokerConversations, new IdentifierGenerator("ERROR_BROKER_CONVERSATIONS"));
			dictionary34.Add(RestoreOptionKind.File, new KeywordGenerator(TSqlTokenType.File));
			dictionary34.Add(RestoreOptionKind.KeepReplication, new IdentifierGenerator("KEEP_REPLICATION"));
			dictionary34.Add(RestoreOptionKind.LoadHistory, new IdentifierGenerator("LOADHISTORY"));
			dictionary34.Add(RestoreOptionKind.MaxTransferSize, new IdentifierGenerator("MAXTRANSFERSIZE"));
			dictionary34.Add(RestoreOptionKind.MediaName, new IdentifierGenerator("MEDIANAME"));
			dictionary34.Add(RestoreOptionKind.MediaPassword, new IdentifierGenerator("MEDIAPASSWORD"));
			dictionary34.Add(RestoreOptionKind.NewBroker, new IdentifierGenerator("NEW_BROKER"));
			dictionary34.Add(RestoreOptionKind.NoChecksum, new IdentifierGenerator("NO_CHECKSUM"));
			dictionary34.Add(RestoreOptionKind.NoLog, new IdentifierGenerator("NO_LOG"));
			dictionary34.Add(RestoreOptionKind.NoRecovery, new IdentifierGenerator("NORECOVERY"));
			dictionary34.Add(RestoreOptionKind.NoRewind, new IdentifierGenerator("NOREWIND"));
			dictionary34.Add(RestoreOptionKind.NoUnload, new IdentifierGenerator("NOUNLOAD"));
			dictionary34.Add(RestoreOptionKind.Online, new IdentifierGenerator("ONLINE"));
			dictionary34.Add(RestoreOptionKind.Partial, new IdentifierGenerator("PARTIAL"));
			dictionary34.Add(RestoreOptionKind.Password, new IdentifierGenerator("PASSWORD"));
			dictionary34.Add(RestoreOptionKind.Recovery, new IdentifierGenerator("RECOVERY"));
			dictionary34.Add(RestoreOptionKind.Replace, new IdentifierGenerator("REPLACE"));
			dictionary34.Add(RestoreOptionKind.Restart, new IdentifierGenerator("RESTART"));
			dictionary34.Add(RestoreOptionKind.RestrictedUser, new IdentifierGenerator("RESTRICTED_USER"));
			dictionary34.Add(RestoreOptionKind.Rewind, new IdentifierGenerator("REWIND"));
			dictionary34.Add(RestoreOptionKind.Snapshot, new IdentifierGenerator("SNAPSHOT"));
			dictionary34.Add(RestoreOptionKind.SnapshotImport, new IdentifierGenerator("SNAPSHOT_IMPORT"));
			dictionary34.Add(RestoreOptionKind.SnapshotRestorePhase, new IdentifierGenerator("SNAPSHOTRESTOREPHASE"));
			dictionary34.Add(RestoreOptionKind.Standby, new IdentifierGenerator("STANDBY"));
			dictionary34.Add(RestoreOptionKind.Stats, new IdentifierGenerator("STATS"));
			dictionary34.Add(RestoreOptionKind.StopAt, new IdentifierGenerator("STOPAT"));
			dictionary34.Add(RestoreOptionKind.StopOnError, new IdentifierGenerator("STOP_ON_ERROR"));
			dictionary34.Add(RestoreOptionKind.Unload, new IdentifierGenerator("UNLOAD"));
			dictionary34.Add(RestoreOptionKind.Verbose, new IdentifierGenerator("VERBOSE"));
			SqlScriptGeneratorVisitor._restoreOptionKindGenerators = dictionary34;
			Dictionary<QueueOptionKind, string> dictionary35 = new Dictionary<QueueOptionKind, string>();
			dictionary35.Add(QueueOptionKind.ActivationDrop, "DROP");
			dictionary35.Add(QueueOptionKind.ActivationMaxQueueReaders, "MAX_QUEUE_READERS");
			dictionary35.Add(QueueOptionKind.ActivationProcedureName, "PROCEDURE_NAME");
			dictionary35.Add(QueueOptionKind.ActivationStatus, "STATUS");
			dictionary35.Add(QueueOptionKind.Retention, "RETENTION");
			dictionary35.Add(QueueOptionKind.Status, "STATUS");
			SqlScriptGeneratorVisitor._queueOptionTypeNames = dictionary35;
			Dictionary<DatabaseAuditActionKind, TokenGenerator> dictionary36 = new Dictionary<DatabaseAuditActionKind, TokenGenerator>();
			dictionary36.Add(DatabaseAuditActionKind.Select, new KeywordGenerator(TSqlTokenType.Select));
			dictionary36.Add(DatabaseAuditActionKind.Update, new KeywordGenerator(TSqlTokenType.Update));
			dictionary36.Add(DatabaseAuditActionKind.Insert, new KeywordGenerator(TSqlTokenType.Insert));
			dictionary36.Add(DatabaseAuditActionKind.Delete, new KeywordGenerator(TSqlTokenType.Delete));
			dictionary36.Add(DatabaseAuditActionKind.Execute, new KeywordGenerator(TSqlTokenType.Execute));
			dictionary36.Add(DatabaseAuditActionKind.Receive, new IdentifierGenerator("RECEIVE"));
			dictionary36.Add(DatabaseAuditActionKind.References, new KeywordGenerator(TSqlTokenType.References));
			SqlScriptGeneratorVisitor._databaseAuditActionName = dictionary36;
			Dictionary<SortOrder, TokenGenerator> dictionary37 = new Dictionary<SortOrder, TokenGenerator>();
			dictionary37.Add(SortOrder.Ascending, new KeywordGenerator(TSqlTokenType.Asc));
			dictionary37.Add(SortOrder.Descending, new KeywordGenerator(TSqlTokenType.Desc));
			dictionary37.Add(SortOrder.NotSpecified, new EmptyGenerator());
			SqlScriptGeneratorVisitor._sortOrderGenerators = dictionary37;
			Dictionary<InsertOption, TokenGenerator> dictionary38 = new Dictionary<InsertOption, TokenGenerator>();
			dictionary38.Add(InsertOption.Into, new KeywordGenerator(TSqlTokenType.Into));
			dictionary38.Add(InsertOption.None, new KeywordGenerator(TSqlTokenType.Into));
			dictionary38.Add(InsertOption.Over, new KeywordGenerator(TSqlTokenType.Over));
			SqlScriptGeneratorVisitor._insertOptionGenerators = dictionary38;
			Dictionary<ParameterlessCallType, TokenGenerator> dictionary39 = new Dictionary<ParameterlessCallType, TokenGenerator>();
			dictionary39.Add(ParameterlessCallType.CurrentTimestamp, new KeywordGenerator(TSqlTokenType.CurrentTimestamp));
			dictionary39.Add(ParameterlessCallType.CurrentUser, new KeywordGenerator(TSqlTokenType.CurrentUser));
			dictionary39.Add(ParameterlessCallType.SessionUser, new KeywordGenerator(TSqlTokenType.SessionUser));
			dictionary39.Add(ParameterlessCallType.SystemUser, new KeywordGenerator(TSqlTokenType.SystemUser));
			dictionary39.Add(ParameterlessCallType.User, new KeywordGenerator(TSqlTokenType.User));
			SqlScriptGeneratorVisitor._parameterlessCallTypeGenerators = dictionary39;
			Dictionary<QualifiedJoinType, List<TokenGenerator>> dictionary40 = new Dictionary<QualifiedJoinType, List<TokenGenerator>>();
			Dictionary<QualifiedJoinType, List<TokenGenerator>> dictionary41 = dictionary40;
			QualifiedJoinType qualifiedJoinType = QualifiedJoinType.FullOuter;
			List<TokenGenerator> list29 = new List<TokenGenerator>();
			list29.Add(new KeywordGenerator(TSqlTokenType.Full, true));
			list29.Add(new KeywordGenerator(TSqlTokenType.Outer));
			dictionary41.Add(qualifiedJoinType, list29);
			Dictionary<QualifiedJoinType, List<TokenGenerator>> dictionary42 = dictionary40;
			QualifiedJoinType qualifiedJoinType2 = QualifiedJoinType.Inner;
			List<TokenGenerator> list30 = new List<TokenGenerator>();
			list30.Add(new KeywordGenerator(TSqlTokenType.Inner));
			dictionary42.Add(qualifiedJoinType2, list30);
			Dictionary<QualifiedJoinType, List<TokenGenerator>> dictionary43 = dictionary40;
			QualifiedJoinType qualifiedJoinType3 = QualifiedJoinType.LeftOuter;
			List<TokenGenerator> list31 = new List<TokenGenerator>();
			list31.Add(new KeywordGenerator(TSqlTokenType.Left, true));
			list31.Add(new KeywordGenerator(TSqlTokenType.Outer));
			dictionary43.Add(qualifiedJoinType3, list31);
			Dictionary<QualifiedJoinType, List<TokenGenerator>> dictionary44 = dictionary40;
			QualifiedJoinType qualifiedJoinType4 = QualifiedJoinType.RightOuter;
			List<TokenGenerator> list32 = new List<TokenGenerator>();
			list32.Add(new KeywordGenerator(TSqlTokenType.Right, true));
			list32.Add(new KeywordGenerator(TSqlTokenType.Outer));
			dictionary44.Add(qualifiedJoinType4, list32);
			SqlScriptGeneratorVisitor._qualifiedJoinTypeGenerators = dictionary40;
			Dictionary<AssignmentKind, TSqlTokenType> dictionary45 = new Dictionary<AssignmentKind, TSqlTokenType>();
			dictionary45.Add(AssignmentKind.Equals, TSqlTokenType.EqualsSign);
			dictionary45.Add(AssignmentKind.AddEquals, TSqlTokenType.AddEquals);
			dictionary45.Add(AssignmentKind.SubtractEquals, TSqlTokenType.SubtractEquals);
			dictionary45.Add(AssignmentKind.MultiplyEquals, TSqlTokenType.MultiplyEquals);
			dictionary45.Add(AssignmentKind.DivideEquals, TSqlTokenType.DivideEquals);
			dictionary45.Add(AssignmentKind.ModEquals, TSqlTokenType.ModEquals);
			dictionary45.Add(AssignmentKind.BitwiseAndEquals, TSqlTokenType.BitwiseAndEquals);
			dictionary45.Add(AssignmentKind.BitwiseOrEquals, TSqlTokenType.BitwiseOrEquals);
			dictionary45.Add(AssignmentKind.BitwiseXorEquals, TSqlTokenType.BitwiseXorEquals);
			SqlScriptGeneratorVisitor._assignmentKindSymbols = dictionary45;
			Dictionary<SqlDataTypeOption, TokenGenerator> dictionary46 = new Dictionary<SqlDataTypeOption, TokenGenerator>();
			dictionary46.Add(SqlDataTypeOption.BigInt, new IdentifierGenerator("BIGINT"));
			dictionary46.Add(SqlDataTypeOption.Binary, new IdentifierGenerator("BINARY"));
			dictionary46.Add(SqlDataTypeOption.Bit, new IdentifierGenerator("BIT"));
			dictionary46.Add(SqlDataTypeOption.Char, new IdentifierGenerator("CHAR"));
			dictionary46.Add(SqlDataTypeOption.Cursor, new KeywordGenerator(TSqlTokenType.Cursor));
			dictionary46.Add(SqlDataTypeOption.DateTime, new IdentifierGenerator("DATETIME"));
			dictionary46.Add(SqlDataTypeOption.Decimal, new IdentifierGenerator("DECIMAL"));
			dictionary46.Add(SqlDataTypeOption.Float, new IdentifierGenerator("FLOAT"));
			dictionary46.Add(SqlDataTypeOption.Image, new IdentifierGenerator("IMAGE"));
			dictionary46.Add(SqlDataTypeOption.Int, new IdentifierGenerator("INT"));
			dictionary46.Add(SqlDataTypeOption.Money, new IdentifierGenerator("MONEY"));
			dictionary46.Add(SqlDataTypeOption.NChar, new IdentifierGenerator("NCHAR"));
			dictionary46.Add(SqlDataTypeOption.NText, new IdentifierGenerator("NTEXT"));
			dictionary46.Add(SqlDataTypeOption.NVarChar, new IdentifierGenerator("NVARCHAR"));
			dictionary46.Add(SqlDataTypeOption.Numeric, new IdentifierGenerator("NUMERIC"));
			dictionary46.Add(SqlDataTypeOption.Real, new IdentifierGenerator("REAL"));
			dictionary46.Add(SqlDataTypeOption.SmallDateTime, new IdentifierGenerator("SMALLDATETIME"));
			dictionary46.Add(SqlDataTypeOption.SmallInt, new IdentifierGenerator("SMALLINT"));
			dictionary46.Add(SqlDataTypeOption.SmallMoney, new IdentifierGenerator("SMALLMONEY"));
			dictionary46.Add(SqlDataTypeOption.Sql_Variant, new IdentifierGenerator("SQL_VARIANT"));
			dictionary46.Add(SqlDataTypeOption.Table, new KeywordGenerator(TSqlTokenType.Table));
			dictionary46.Add(SqlDataTypeOption.Text, new IdentifierGenerator("TEXT"));
			dictionary46.Add(SqlDataTypeOption.Timestamp, new IdentifierGenerator("TIMESTAMP"));
			dictionary46.Add(SqlDataTypeOption.TinyInt, new IdentifierGenerator("TINYINT"));
			dictionary46.Add(SqlDataTypeOption.UniqueIdentifier, new IdentifierGenerator("UNIQUEIDENTIFIER"));
			dictionary46.Add(SqlDataTypeOption.VarBinary, new IdentifierGenerator("VARBINARY"));
			dictionary46.Add(SqlDataTypeOption.VarChar, new IdentifierGenerator("VARCHAR"));
			dictionary46.Add(SqlDataTypeOption.Date, new IdentifierGenerator("DATE"));
			dictionary46.Add(SqlDataTypeOption.Time, new IdentifierGenerator("TIME"));
			dictionary46.Add(SqlDataTypeOption.DateTime2, new IdentifierGenerator("DATETIME2"));
			dictionary46.Add(SqlDataTypeOption.DateTimeOffset, new IdentifierGenerator("DATETIMEOFFSET"));
			dictionary46.Add(SqlDataTypeOption.Rowversion, new IdentifierGenerator("ROWVERSION"));
			SqlScriptGeneratorVisitor._sqlDataTypeOptionGenerators = dictionary46;
			Dictionary<UnaryExpressionType, List<TokenGenerator>> dictionary47 = new Dictionary<UnaryExpressionType, List<TokenGenerator>>();
			Dictionary<UnaryExpressionType, List<TokenGenerator>> dictionary48 = dictionary47;
			UnaryExpressionType unaryExpressionType = UnaryExpressionType.BitwiseNot;
			List<TokenGenerator> list33 = new List<TokenGenerator>();
			list33.Add(new KeywordGenerator(TSqlTokenType.Tilde));
			dictionary48.Add(unaryExpressionType, list33);
			Dictionary<UnaryExpressionType, List<TokenGenerator>> dictionary49 = dictionary47;
			UnaryExpressionType unaryExpressionType2 = UnaryExpressionType.Negative;
			List<TokenGenerator> list34 = new List<TokenGenerator>();
			list34.Add(new KeywordGenerator(TSqlTokenType.Minus));
			dictionary49.Add(unaryExpressionType2, list34);
			Dictionary<UnaryExpressionType, List<TokenGenerator>> dictionary50 = dictionary47;
			UnaryExpressionType unaryExpressionType3 = UnaryExpressionType.Positive;
			List<TokenGenerator> list35 = new List<TokenGenerator>();
			list35.Add(new KeywordGenerator(TSqlTokenType.Plus));
			dictionary50.Add(unaryExpressionType3, list35);
			SqlScriptGeneratorVisitor._unaryExpressionTypeGenerators = dictionary47;
			Dictionary<XmlDataTypeOption, TokenGenerator> dictionary51 = new Dictionary<XmlDataTypeOption, TokenGenerator>();
			dictionary51.Add(XmlDataTypeOption.Content, new IdentifierGenerator("CONTENT"));
			dictionary51.Add(XmlDataTypeOption.Document, new IdentifierGenerator("DOCUMENT"));
			dictionary51.Add(XmlDataTypeOption.None, new EmptyGenerator());
			SqlScriptGeneratorVisitor._xmlDataTypeOptionGenerators = dictionary51;
			Dictionary<EndpointState, string> dictionary52 = new Dictionary<EndpointState, string>();
			dictionary52.Add(EndpointState.Disabled, "DISABLED");
			dictionary52.Add(EndpointState.Started, "STARTED");
			dictionary52.Add(EndpointState.Stopped, "STOPPED");
			SqlScriptGeneratorVisitor._endpointStateNames = dictionary52;
			Dictionary<EndpointProtocol, string> dictionary53 = new Dictionary<EndpointProtocol, string>();
			dictionary53.Add(EndpointProtocol.Http, "HTTP");
			dictionary53.Add(EndpointProtocol.Tcp, "TCP");
			SqlScriptGeneratorVisitor._endpointProtocolNames = dictionary53;
			Dictionary<EndpointType, string> dictionary54 = new Dictionary<EndpointType, string>();
			dictionary54.Add(EndpointType.DatabaseMirroring, "DATABASE_MIRRORING");
			dictionary54.Add(EndpointType.ServiceBroker, "SERVICE_BROKER");
			dictionary54.Add(EndpointType.Soap, "SOAP");
			dictionary54.Add(EndpointType.TSql, "TSQL");
			SqlScriptGeneratorVisitor._endpointTypeNames = dictionary54;
			Dictionary<PayloadOptionKinds, TokenGenerator> dictionary55 = new Dictionary<PayloadOptionKinds, TokenGenerator>();
			dictionary55.Add(PayloadOptionKinds.Authentication, new IdentifierGenerator("AUTHENTICATION"));
			dictionary55.Add(PayloadOptionKinds.Batches, new IdentifierGenerator("BATCHES"));
			dictionary55.Add(PayloadOptionKinds.CharacterSet, new IdentifierGenerator("CHARACTER_SET"));
			dictionary55.Add(PayloadOptionKinds.Database, new KeywordGenerator(TSqlTokenType.Database));
			dictionary55.Add(PayloadOptionKinds.Encryption, new IdentifierGenerator("ENCRYPTION"));
			dictionary55.Add(PayloadOptionKinds.HeaderLimit, new IdentifierGenerator("HEADER_LIMIT"));
			dictionary55.Add(PayloadOptionKinds.LoginType, new IdentifierGenerator("LOGIN_TYPE"));
			dictionary55.Add(PayloadOptionKinds.MessageForwardSize, new IdentifierGenerator("MESSAGE_FORWARD_SIZE"));
			dictionary55.Add(PayloadOptionKinds.MessageForwarding, new IdentifierGenerator("MESSAGE_FORWARDING"));
			dictionary55.Add(PayloadOptionKinds.Namespace, new IdentifierGenerator("NAMESPACE"));
			dictionary55.Add(PayloadOptionKinds.None, new EmptyGenerator());
			dictionary55.Add(PayloadOptionKinds.Role, new IdentifierGenerator("ROLE"));
			dictionary55.Add(PayloadOptionKinds.Schema, new KeywordGenerator(TSqlTokenType.Schema));
			dictionary55.Add(PayloadOptionKinds.SessionTimeout, new IdentifierGenerator("SESSION_TIMEOUT"));
			dictionary55.Add(PayloadOptionKinds.Sessions, new IdentifierGenerator("SESSIONS"));
			dictionary55.Add(PayloadOptionKinds.WebMethod, new IdentifierGenerator("WEBMETHOD"));
			dictionary55.Add(PayloadOptionKinds.Wsdl, new IdentifierGenerator("WSDL"));
			SqlScriptGeneratorVisitor._payloadOptionKindsGenerators = dictionary55;
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary56 = new Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>>();
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary57 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind = SimpleAlterFullTextIndexActionKind.Disable;
			List<TokenGenerator> list36 = new List<TokenGenerator>();
			list36.Add(new IdentifierGenerator("DISABLE"));
			dictionary57.Add(simpleAlterFullTextIndexActionKind, list36);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary58 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind2 = SimpleAlterFullTextIndexActionKind.Enable;
			List<TokenGenerator> list37 = new List<TokenGenerator>();
			list37.Add(new IdentifierGenerator("ENABLE"));
			dictionary58.Add(simpleAlterFullTextIndexActionKind2, list37);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary59 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind3 = SimpleAlterFullTextIndexActionKind.PausePopulation;
			List<TokenGenerator> list38 = new List<TokenGenerator>();
			list38.Add(new IdentifierGenerator("PAUSE", true));
			list38.Add(new IdentifierGenerator("POPULATION"));
			dictionary59.Add(simpleAlterFullTextIndexActionKind3, list38);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary60 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind4 = SimpleAlterFullTextIndexActionKind.ResumePopulation;
			List<TokenGenerator> list39 = new List<TokenGenerator>();
			list39.Add(new IdentifierGenerator("RESUME", true));
			list39.Add(new IdentifierGenerator("POPULATION"));
			dictionary60.Add(simpleAlterFullTextIndexActionKind4, list39);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary61 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind5 = SimpleAlterFullTextIndexActionKind.StopPopulation;
			List<TokenGenerator> list40 = new List<TokenGenerator>();
			list40.Add(new IdentifierGenerator("STOP", true));
			list40.Add(new IdentifierGenerator("POPULATION"));
			dictionary61.Add(simpleAlterFullTextIndexActionKind5, list40);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary62 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind6 = SimpleAlterFullTextIndexActionKind.SetChangeTrackingAuto;
			List<TokenGenerator> list41 = new List<TokenGenerator>();
			list41.Add(new KeywordGenerator(TSqlTokenType.Set, true));
			list41.Add(new IdentifierGenerator("CHANGE_TRACKING", true));
			list41.Add(new IdentifierGenerator("AUTO"));
			dictionary62.Add(simpleAlterFullTextIndexActionKind6, list41);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary63 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind7 = SimpleAlterFullTextIndexActionKind.SetChangeTrackingManual;
			List<TokenGenerator> list42 = new List<TokenGenerator>();
			list42.Add(new KeywordGenerator(TSqlTokenType.Set, true));
			list42.Add(new IdentifierGenerator("CHANGE_TRACKING", true));
			list42.Add(new IdentifierGenerator("MANUAL"));
			dictionary63.Add(simpleAlterFullTextIndexActionKind7, list42);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary64 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind8 = SimpleAlterFullTextIndexActionKind.SetChangeTrackingOff;
			List<TokenGenerator> list43 = new List<TokenGenerator>();
			list43.Add(new KeywordGenerator(TSqlTokenType.Set, true));
			list43.Add(new IdentifierGenerator("CHANGE_TRACKING", true));
			list43.Add(new KeywordGenerator(TSqlTokenType.Off));
			dictionary64.Add(simpleAlterFullTextIndexActionKind8, list43);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary65 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind9 = SimpleAlterFullTextIndexActionKind.StartFullPopulation;
			List<TokenGenerator> list44 = new List<TokenGenerator>();
			list44.Add(new IdentifierGenerator("START", true));
			list44.Add(new KeywordGenerator(TSqlTokenType.Full, true));
			list44.Add(new IdentifierGenerator("POPULATION"));
			dictionary65.Add(simpleAlterFullTextIndexActionKind9, list44);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary66 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind10 = SimpleAlterFullTextIndexActionKind.StartIncrementalPopulation;
			List<TokenGenerator> list45 = new List<TokenGenerator>();
			list45.Add(new IdentifierGenerator("START", true));
			list45.Add(new IdentifierGenerator("INCREMENTAL", true));
			list45.Add(new IdentifierGenerator("POPULATION"));
			dictionary66.Add(simpleAlterFullTextIndexActionKind10, list45);
			Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> dictionary67 = dictionary56;
			SimpleAlterFullTextIndexActionKind simpleAlterFullTextIndexActionKind11 = SimpleAlterFullTextIndexActionKind.StartUpdatePopulation;
			List<TokenGenerator> list46 = new List<TokenGenerator>();
			list46.Add(new IdentifierGenerator("START", true));
			list46.Add(new KeywordGenerator(TSqlTokenType.Update, true));
			list46.Add(new IdentifierGenerator("POPULATION"));
			dictionary67.Add(simpleAlterFullTextIndexActionKind11, list46);
			SqlScriptGeneratorVisitor._simpleAlterFulltextIndexActionKindActions = dictionary56;
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary68 = new Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>>();
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary69 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption = AlterTableAlterColumnOption.AddRowGuidCol;
			List<TokenGenerator> list47 = new List<TokenGenerator>();
			list47.Add(new KeywordGenerator(TSqlTokenType.Add, true));
			list47.Add(new KeywordGenerator(TSqlTokenType.RowGuidColumn));
			dictionary69.Add(alterTableAlterColumnOption, list47);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary70 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption2 = AlterTableAlterColumnOption.DropRowGuidCol;
			List<TokenGenerator> list48 = new List<TokenGenerator>();
			list48.Add(new KeywordGenerator(TSqlTokenType.Drop, true));
			list48.Add(new KeywordGenerator(TSqlTokenType.RowGuidColumn));
			dictionary70.Add(alterTableAlterColumnOption2, list48);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary71 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption3 = AlterTableAlterColumnOption.AddPersisted;
			List<TokenGenerator> list49 = new List<TokenGenerator>();
			list49.Add(new KeywordGenerator(TSqlTokenType.Add, true));
			list49.Add(new IdentifierGenerator("PERSISTED"));
			dictionary71.Add(alterTableAlterColumnOption3, list49);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary72 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption4 = AlterTableAlterColumnOption.DropPersisted;
			List<TokenGenerator> list50 = new List<TokenGenerator>();
			list50.Add(new KeywordGenerator(TSqlTokenType.Drop, true));
			list50.Add(new IdentifierGenerator("PERSISTED"));
			dictionary72.Add(alterTableAlterColumnOption4, list50);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary73 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption5 = AlterTableAlterColumnOption.AddNotForReplication;
			List<TokenGenerator> list51 = new List<TokenGenerator>();
			list51.Add(new KeywordGenerator(TSqlTokenType.Add, true));
			list51.Add(new KeywordGenerator(TSqlTokenType.Not, true));
			list51.Add(new KeywordGenerator(TSqlTokenType.For, true));
			list51.Add(new KeywordGenerator(TSqlTokenType.Replication));
			dictionary73.Add(alterTableAlterColumnOption5, list51);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary74 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption6 = AlterTableAlterColumnOption.DropNotForReplication;
			List<TokenGenerator> list52 = new List<TokenGenerator>();
			list52.Add(new KeywordGenerator(TSqlTokenType.Drop, true));
			list52.Add(new KeywordGenerator(TSqlTokenType.Not, true));
			list52.Add(new KeywordGenerator(TSqlTokenType.For, true));
			list52.Add(new KeywordGenerator(TSqlTokenType.Replication));
			dictionary74.Add(alterTableAlterColumnOption6, list52);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary75 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption7 = AlterTableAlterColumnOption.AddSparse;
			List<TokenGenerator> list53 = new List<TokenGenerator>();
			list53.Add(new KeywordGenerator(TSqlTokenType.Add, true));
			list53.Add(new IdentifierGenerator("SPARSE"));
			dictionary75.Add(alterTableAlterColumnOption7, list53);
			Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> dictionary76 = dictionary68;
			AlterTableAlterColumnOption alterTableAlterColumnOption8 = AlterTableAlterColumnOption.DropSparse;
			List<TokenGenerator> list54 = new List<TokenGenerator>();
			list54.Add(new KeywordGenerator(TSqlTokenType.Drop, true));
			list54.Add(new IdentifierGenerator("SPARSE"));
			dictionary76.Add(alterTableAlterColumnOption8, list54);
			SqlScriptGeneratorVisitor._alterTableAlterColumnOptionNames = dictionary68;
			Dictionary<TableElementType, TokenGenerator> dictionary77 = new Dictionary<TableElementType, TokenGenerator>();
			dictionary77.Add(TableElementType.Column, new KeywordGenerator(TSqlTokenType.Column));
			dictionary77.Add(TableElementType.Constraint, new KeywordGenerator(TSqlTokenType.Constraint));
			dictionary77.Add(TableElementType.NotSpecified, new EmptyGenerator());
			SqlScriptGeneratorVisitor._tableElementTypeGenerators = dictionary77;
			Dictionary<AuthenticationTypes, TokenGenerator> dictionary78 = new Dictionary<AuthenticationTypes, TokenGenerator>();
			dictionary78.Add(AuthenticationTypes.Basic, new IdentifierGenerator("BASIC"));
			dictionary78.Add(AuthenticationTypes.Digest, new IdentifierGenerator("DIGEST"));
			dictionary78.Add(AuthenticationTypes.Integrated, new IdentifierGenerator("INTEGRATED"));
			dictionary78.Add(AuthenticationTypes.Kerberos, new IdentifierGenerator("KERBEROS"));
			dictionary78.Add(AuthenticationTypes.Ntlm, new IdentifierGenerator("NTLM"));
			SqlScriptGeneratorVisitor._authenticationTypesGenerators = dictionary78;
			Dictionary<AuthenticationProtocol, string> dictionary79 = new Dictionary<AuthenticationProtocol, string>();
			dictionary79.Add(AuthenticationProtocol.Windows, "WINDOWS");
			dictionary79.Add(AuthenticationProtocol.WindowsKerberos, "KERBEROS");
			dictionary79.Add(AuthenticationProtocol.WindowsNegotiate, "NEGOTIATE");
			dictionary79.Add(AuthenticationProtocol.WindowsNtlm, "NTLM");
			SqlScriptGeneratorVisitor._authenticationProtocolNames = dictionary79;
			Dictionary<DialogOptionKind, string> dictionary80 = new Dictionary<DialogOptionKind, string>();
			dictionary80.Add(DialogOptionKind.Lifetime, "LIFETIME");
			dictionary80.Add(DialogOptionKind.RelatedConversation, "RELATED_CONVERSATION");
			dictionary80.Add(DialogOptionKind.RelatedConversationGroup, "RELATED_CONVERSATION_GROUP");
			SqlScriptGeneratorVisitor._dialogOptionNames = dictionary80;
			Dictionary<BinaryQueryExpressionType, TokenGenerator> dictionary81 = new Dictionary<BinaryQueryExpressionType, TokenGenerator>();
			dictionary81.Add(BinaryQueryExpressionType.Except, new KeywordGenerator(TSqlTokenType.Except));
			dictionary81.Add(BinaryQueryExpressionType.Intersect, new KeywordGenerator(TSqlTokenType.Intersect));
			dictionary81.Add(BinaryQueryExpressionType.Union, new KeywordGenerator(TSqlTokenType.Union));
			SqlScriptGeneratorVisitor._binaryQueryExpressionTypeGenerators = dictionary81;
			Dictionary<CertificateOptionKinds, string> dictionary82 = new Dictionary<CertificateOptionKinds, string>();
			dictionary82.Add(CertificateOptionKinds.Subject, "SUBJECT");
			dictionary82.Add(CertificateOptionKinds.StartDate, "START_DATE");
			dictionary82.Add(CertificateOptionKinds.ExpiryDate, "EXPIRY_DATE");
			SqlScriptGeneratorVisitor._certificateOptionNames = dictionary82;
			SqlScriptGeneratorVisitor._commandOptions = Enum.GetValues(typeof(CommandOptions));
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary83 = new Dictionary<BinaryExpressionType, List<TokenGenerator>>();
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary84 = dictionary83;
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Add;
			List<TokenGenerator> list55 = new List<TokenGenerator>();
			list55.Add(new KeywordGenerator(TSqlTokenType.Plus));
			dictionary84.Add(binaryExpressionType, list55);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary85 = dictionary83;
			BinaryExpressionType binaryExpressionType2 = BinaryExpressionType.Subtract;
			List<TokenGenerator> list56 = new List<TokenGenerator>();
			list56.Add(new KeywordGenerator(TSqlTokenType.Minus));
			dictionary85.Add(binaryExpressionType2, list56);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary86 = dictionary83;
			BinaryExpressionType binaryExpressionType3 = BinaryExpressionType.Multiply;
			List<TokenGenerator> list57 = new List<TokenGenerator>();
			list57.Add(new KeywordGenerator(TSqlTokenType.Star));
			dictionary86.Add(binaryExpressionType3, list57);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary87 = dictionary83;
			BinaryExpressionType binaryExpressionType4 = BinaryExpressionType.Divide;
			List<TokenGenerator> list58 = new List<TokenGenerator>();
			list58.Add(new KeywordGenerator(TSqlTokenType.Divide));
			dictionary87.Add(binaryExpressionType4, list58);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary88 = dictionary83;
			BinaryExpressionType binaryExpressionType5 = BinaryExpressionType.Modulo;
			List<TokenGenerator> list59 = new List<TokenGenerator>();
			list59.Add(new KeywordGenerator(TSqlTokenType.PercentSign));
			dictionary88.Add(binaryExpressionType5, list59);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary89 = dictionary83;
			BinaryExpressionType binaryExpressionType6 = BinaryExpressionType.BitwiseAnd;
			List<TokenGenerator> list60 = new List<TokenGenerator>();
			list60.Add(new KeywordGenerator(TSqlTokenType.Ampersand));
			dictionary89.Add(binaryExpressionType6, list60);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary90 = dictionary83;
			BinaryExpressionType binaryExpressionType7 = BinaryExpressionType.BitwiseOr;
			List<TokenGenerator> list61 = new List<TokenGenerator>();
			list61.Add(new KeywordGenerator(TSqlTokenType.VerticalLine));
			dictionary90.Add(binaryExpressionType7, list61);
			Dictionary<BinaryExpressionType, List<TokenGenerator>> dictionary91 = dictionary83;
			BinaryExpressionType binaryExpressionType8 = BinaryExpressionType.BitwiseXor;
			List<TokenGenerator> list62 = new List<TokenGenerator>();
			list62.Add(new KeywordGenerator(TSqlTokenType.Circumflex));
			dictionary91.Add(binaryExpressionType8, list62);
			SqlScriptGeneratorVisitor._binaryExpressionTypeGenerators = dictionary83;
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary92 = new Dictionary<BooleanComparisonType, List<TokenGenerator>>();
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary93 = dictionary92;
			BooleanComparisonType booleanComparisonType = BooleanComparisonType.Equals;
			List<TokenGenerator> list63 = new List<TokenGenerator>();
			list63.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary93.Add(booleanComparisonType, list63);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary94 = dictionary92;
			BooleanComparisonType booleanComparisonType2 = BooleanComparisonType.GreaterThan;
			List<TokenGenerator> list64 = new List<TokenGenerator>();
			list64.Add(new KeywordGenerator(TSqlTokenType.GreaterThan));
			dictionary94.Add(booleanComparisonType2, list64);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary95 = dictionary92;
			BooleanComparisonType booleanComparisonType3 = BooleanComparisonType.LessThan;
			List<TokenGenerator> list65 = new List<TokenGenerator>();
			list65.Add(new KeywordGenerator(TSqlTokenType.LessThan));
			dictionary95.Add(booleanComparisonType3, list65);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary96 = dictionary92;
			BooleanComparisonType booleanComparisonType4 = BooleanComparisonType.GreaterThanOrEqualTo;
			List<TokenGenerator> list66 = new List<TokenGenerator>();
			list66.Add(new KeywordGenerator(TSqlTokenType.GreaterThan));
			list66.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary96.Add(booleanComparisonType4, list66);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary97 = dictionary92;
			BooleanComparisonType booleanComparisonType5 = BooleanComparisonType.LessThanOrEqualTo;
			List<TokenGenerator> list67 = new List<TokenGenerator>();
			list67.Add(new KeywordGenerator(TSqlTokenType.LessThan));
			list67.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary97.Add(booleanComparisonType5, list67);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary98 = dictionary92;
			BooleanComparisonType booleanComparisonType6 = BooleanComparisonType.NotEqualToBrackets;
			List<TokenGenerator> list68 = new List<TokenGenerator>();
			list68.Add(new KeywordGenerator(TSqlTokenType.LessThan));
			list68.Add(new KeywordGenerator(TSqlTokenType.GreaterThan));
			dictionary98.Add(booleanComparisonType6, list68);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary99 = dictionary92;
			BooleanComparisonType booleanComparisonType7 = BooleanComparisonType.NotEqualToExclamation;
			List<TokenGenerator> list69 = new List<TokenGenerator>();
			list69.Add(new KeywordGenerator(TSqlTokenType.Bang));
			list69.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary99.Add(booleanComparisonType7, list69);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary100 = dictionary92;
			BooleanComparisonType booleanComparisonType8 = BooleanComparisonType.NotLessThan;
			List<TokenGenerator> list70 = new List<TokenGenerator>();
			list70.Add(new KeywordGenerator(TSqlTokenType.Bang));
			list70.Add(new KeywordGenerator(TSqlTokenType.LessThan));
			dictionary100.Add(booleanComparisonType8, list70);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary101 = dictionary92;
			BooleanComparisonType booleanComparisonType9 = BooleanComparisonType.NotGreaterThan;
			List<TokenGenerator> list71 = new List<TokenGenerator>();
			list71.Add(new KeywordGenerator(TSqlTokenType.Bang));
			list71.Add(new KeywordGenerator(TSqlTokenType.GreaterThan));
			dictionary101.Add(booleanComparisonType9, list71);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary102 = dictionary92;
			BooleanComparisonType booleanComparisonType10 = BooleanComparisonType.LeftOuterJoin;
			List<TokenGenerator> list72 = new List<TokenGenerator>();
			list72.Add(new KeywordGenerator(TSqlTokenType.MultiplyEquals));
			dictionary102.Add(booleanComparisonType10, list72);
			Dictionary<BooleanComparisonType, List<TokenGenerator>> dictionary103 = dictionary92;
			BooleanComparisonType booleanComparisonType11 = BooleanComparisonType.RightOuterJoin;
			List<TokenGenerator> list73 = new List<TokenGenerator>();
			list73.Add(new KeywordGenerator(TSqlTokenType.RightOuterJoin));
			dictionary103.Add(booleanComparisonType11, list73);
			SqlScriptGeneratorVisitor._booleanComparisonTypeGenerators = dictionary92;
			Dictionary<BooleanBinaryExpressionType, List<TokenGenerator>> dictionary104 = new Dictionary<BooleanBinaryExpressionType, List<TokenGenerator>>();
			Dictionary<BooleanBinaryExpressionType, List<TokenGenerator>> dictionary105 = dictionary104;
			BooleanBinaryExpressionType booleanBinaryExpressionType = BooleanBinaryExpressionType.And;
			List<TokenGenerator> list74 = new List<TokenGenerator>();
			list74.Add(new KeywordGenerator(TSqlTokenType.And));
			dictionary105.Add(booleanBinaryExpressionType, list74);
			Dictionary<BooleanBinaryExpressionType, List<TokenGenerator>> dictionary106 = dictionary104;
			BooleanBinaryExpressionType booleanBinaryExpressionType2 = BooleanBinaryExpressionType.Or;
			List<TokenGenerator> list75 = new List<TokenGenerator>();
			list75.Add(new KeywordGenerator(TSqlTokenType.Or));
			dictionary106.Add(booleanBinaryExpressionType2, list75);
			SqlScriptGeneratorVisitor._booleanBinaryExpressionTypeGenerators = dictionary104;
			Dictionary<MessageSender, TokenGenerator> dictionary107 = new Dictionary<MessageSender, TokenGenerator>();
			dictionary107.Add(MessageSender.None, new EmptyGenerator());
			dictionary107.Add(MessageSender.Any, new KeywordGenerator(TSqlTokenType.Any));
			dictionary107.Add(MessageSender.Initiator, new IdentifierGenerator("INITIATOR"));
			dictionary107.Add(MessageSender.Target, new IdentifierGenerator("TARGET"));
			SqlScriptGeneratorVisitor._messageSenderGenerators = dictionary107;
			Dictionary<PermissionSetOption, string> dictionary108 = new Dictionary<PermissionSetOption, string>();
			dictionary108.Add(PermissionSetOption.ExternalAccess, "EXTERNAL_ACCESS");
			dictionary108.Add(PermissionSetOption.Safe, "SAFE");
			dictionary108.Add(PermissionSetOption.Unsafe, "UNSAFE");
			SqlScriptGeneratorVisitor._permissionSetOptionNames = dictionary108;
			Dictionary<AttachMode, TokenGenerator> dictionary109 = new Dictionary<AttachMode, TokenGenerator>();
			dictionary109.Add(AttachMode.Attach, new IdentifierGenerator("ATTACH"));
			dictionary109.Add(AttachMode.AttachForceRebuildLog, new IdentifierGenerator("ATTACH_FORCE_REBUILD_LOG"));
			dictionary109.Add(AttachMode.AttachRebuildLog, new IdentifierGenerator("ATTACH_REBUILD_LOG"));
			dictionary109.Add(AttachMode.Load, new KeywordGenerator(TSqlTokenType.Load));
			SqlScriptGeneratorVisitor._attachModeGenerators = dictionary109;
			Dictionary<SecondaryXmlIndexType, string> dictionary110 = new Dictionary<SecondaryXmlIndexType, string>();
			dictionary110.Add(SecondaryXmlIndexType.Path, "PATH");
			dictionary110.Add(SecondaryXmlIndexType.Property, "PROPERTY");
			dictionary110.Add(SecondaryXmlIndexType.Value, "VALUE");
			SqlScriptGeneratorVisitor._secondaryXmlIndexTypeNames = dictionary110;
			Dictionary<CursorOptionKind, string> dictionary111 = new Dictionary<CursorOptionKind, string>();
			dictionary111.Add(CursorOptionKind.Local, "LOCAL");
			dictionary111.Add(CursorOptionKind.Global, "GLOBAL");
			dictionary111.Add(CursorOptionKind.Scroll, "SCROLL");
			dictionary111.Add(CursorOptionKind.ForwardOnly, "FORWARD_ONLY");
			dictionary111.Add(CursorOptionKind.Insensitive, "INSENSITIVE");
			dictionary111.Add(CursorOptionKind.Keyset, "KEYSET");
			dictionary111.Add(CursorOptionKind.Dynamic, "DYNAMIC");
			dictionary111.Add(CursorOptionKind.FastForward, "FAST_FORWARD");
			dictionary111.Add(CursorOptionKind.ScrollLocks, "SCROLL_LOCKS");
			dictionary111.Add(CursorOptionKind.Optimistic, "OPTIMISTIC");
			dictionary111.Add(CursorOptionKind.ReadOnly, "READ_ONLY");
			dictionary111.Add(CursorOptionKind.Static, "STATIC");
			dictionary111.Add(CursorOptionKind.TypeWarning, "TYPE_WARNING");
			SqlScriptGeneratorVisitor._cursorOptionsNames = dictionary111;
			Dictionary<DbccCommand, string> dictionary112 = new Dictionary<DbccCommand, string>();
			dictionary112.Add(DbccCommand.ActiveCursors, "ACTIVECURSORS");
			dictionary112.Add(DbccCommand.AddExtendedProc, "ADDEXTENDEDPROC");
			dictionary112.Add(DbccCommand.AddInstance, "ADDINSTANCE");
			dictionary112.Add(DbccCommand.AuditEvent, "AUDITEVENT");
			dictionary112.Add(DbccCommand.AutoPilot, "AUTOPILOT");
			dictionary112.Add(DbccCommand.Buffer, "BUFFER");
			dictionary112.Add(DbccCommand.Bytes, "BYTES");
			dictionary112.Add(DbccCommand.CacheProfile, "CACHEPROFILE");
			dictionary112.Add(DbccCommand.CacheStats, "CACHESTATS");
			dictionary112.Add(DbccCommand.CallFullText, "CALLFULLTEXT");
			dictionary112.Add(DbccCommand.CheckAlloc, "CHECKALLOC");
			dictionary112.Add(DbccCommand.CheckCatalog, "CHECKCATALOG");
			dictionary112.Add(DbccCommand.CheckConstraints, "CHECKCONSTRAINTS");
			dictionary112.Add(DbccCommand.CheckDB, "CHECKDB");
			dictionary112.Add(DbccCommand.CheckFileGroup, "CHECKFILEGROUP");
			dictionary112.Add(DbccCommand.CheckIdent, "CHECKIDENT");
			dictionary112.Add(DbccCommand.CheckPrimaryFile, "CHECKPRIMARYFILE");
			dictionary112.Add(DbccCommand.CheckTable, "CHECKTABLE");
			dictionary112.Add(DbccCommand.CleanTable, "CLEANTABLE");
			dictionary112.Add(DbccCommand.ClearSpaceCaches, "CLEARSPACECACHES");
			dictionary112.Add(DbccCommand.CollectStats, "COLLECTSTATS");
			dictionary112.Add(DbccCommand.ConcurrencyViolation, "CONCURRENCYVIOLATION");
			dictionary112.Add(DbccCommand.CursorStats, "CURSORSTATS");
			dictionary112.Add(DbccCommand.DBRecover, "DBRECOVER");
			dictionary112.Add(DbccCommand.DBReindex, "DBREINDEX");
			dictionary112.Add(DbccCommand.DBReindexAll, "DBREINDEXALL");
			dictionary112.Add(DbccCommand.DBRepair, "DBREPAIR");
			dictionary112.Add(DbccCommand.DebugBreak, "DEBUGBREAK");
			dictionary112.Add(DbccCommand.DeleteInstance, "DELETEINSTANCE");
			dictionary112.Add(DbccCommand.DetachDB, "DETACHDB");
			dictionary112.Add(DbccCommand.DropCleanBuffers, "DROPCLEANBUFFERS");
			dictionary112.Add(DbccCommand.DropExtendedProc, "DROPEXTENDEDPROC");
			dictionary112.Add(DbccCommand.DumpConfig, "CONFIG");
			dictionary112.Add(DbccCommand.DumpDBInfo, "DBINFO");
			dictionary112.Add(DbccCommand.DumpDBTable, "DBTABLE");
			dictionary112.Add(DbccCommand.DumpLock, "LOCK");
			dictionary112.Add(DbccCommand.DumpLog, "LOG");
			dictionary112.Add(DbccCommand.DumpPage, "PAGE");
			dictionary112.Add(DbccCommand.DumpResource, "RESOURCE");
			dictionary112.Add(DbccCommand.DumpTrigger, "DUMPTRIGGER");
			dictionary112.Add(DbccCommand.ErrorLog, "ERRORLOG");
			dictionary112.Add(DbccCommand.ExtentInfo, "EXTENTINFO");
			dictionary112.Add(DbccCommand.FileHeader, "FILEHEADER");
			dictionary112.Add(DbccCommand.FixAllocation, "FIXALLOCATION");
			dictionary112.Add(DbccCommand.Flush, "FLUSH");
			dictionary112.Add(DbccCommand.FlushProcInDB, "FLUSHPROCINDB");
			dictionary112.Add(DbccCommand.ForceGhostCleanup, "FORCEGHOSTCLEANUP");
			dictionary112.Add(DbccCommand.Free, "FREE");
			dictionary112.Add(DbccCommand.FreeProcCache, "FREEPROCCACHE");
			dictionary112.Add(DbccCommand.FreeSessionCache, "FREESESSIONCACHE");
			dictionary112.Add(DbccCommand.FreeSystemCache, "FREESYSTEMCACHE");
			dictionary112.Add(DbccCommand.FreezeIO, "FREEZE_IO");
			dictionary112.Add(DbccCommand.Help, "HELP");
			dictionary112.Add(DbccCommand.IcecapQuery, "ICECAPQUERY");
			dictionary112.Add(DbccCommand.IncrementInstance, "INCREMENTINSTANCE");
			dictionary112.Add(DbccCommand.Ind, "IND");
			dictionary112.Add(DbccCommand.IndexDefrag, "INDEXDEFRAG");
			dictionary112.Add(DbccCommand.InputBuffer, "INPUTBUFFER");
			dictionary112.Add(DbccCommand.InvalidateTextptr, "INVALIDATE_TEXTPTR");
			dictionary112.Add(DbccCommand.InvalidateTextptrObjid, "INVALIDATE_TEXTPTR_OBJID");
			dictionary112.Add(DbccCommand.Latch, "LATCH");
			dictionary112.Add(DbccCommand.LogInfo, "LOGINFO");
			dictionary112.Add(DbccCommand.MapAllocUnit, "MAPALLOCUNIT");
			dictionary112.Add(DbccCommand.MemObjList, "MEMOBJLIST");
			dictionary112.Add(DbccCommand.MemoryMap, "MEMORYMAP");
			dictionary112.Add(DbccCommand.MemoryStatus, "MEMORYSTATUS");
			dictionary112.Add(DbccCommand.Metadata, "METADATA");
			dictionary112.Add(DbccCommand.MovePage, "MOVEPAGE");
			dictionary112.Add(DbccCommand.NoTextptr, "NO_TEXTPTR");
			dictionary112.Add(DbccCommand.OpenTran, "OPENTRAN");
			dictionary112.Add(DbccCommand.OptimizerWhatIf, "OPTIMIZER_WHATIF");
			dictionary112.Add(DbccCommand.OutputBuffer, "OUTPUTBUFFER");
			dictionary112.Add(DbccCommand.PerfMonStats, "PERFMON");
			dictionary112.Add(DbccCommand.PersistStackHash, "PERSISTSTACKHASH");
			dictionary112.Add(DbccCommand.PinTable, "PINTABLE");
			dictionary112.Add(DbccCommand.ProcCache, "PROCCACHE");
			dictionary112.Add(DbccCommand.PrtiPage, "PRTIPAGE");
			dictionary112.Add(DbccCommand.ReadPage, "READPAGE");
			dictionary112.Add(DbccCommand.RenameColumn, "RENAMECOLUMN");
			dictionary112.Add(DbccCommand.RuleOff, "RULEOFF");
			dictionary112.Add(DbccCommand.RuleOn, "RULEON");
			dictionary112.Add(DbccCommand.SeMetadata, "SEMETADATA");
			dictionary112.Add(DbccCommand.SetCpuWeight, "SETCPUWEIGHT");
			dictionary112.Add(DbccCommand.SetInstance, "SETINSTANCE");
			dictionary112.Add(DbccCommand.SetIOWeight, "SETIOWEIGHT");
			dictionary112.Add(DbccCommand.ShowStatistics, "SHOW_STATISTICS");
			dictionary112.Add(DbccCommand.ShowContig, "SHOWCONTIG");
			dictionary112.Add(DbccCommand.ShowDBAffinity, "SHOWDBAFFINITY");
			dictionary112.Add(DbccCommand.ShowFileStats, "SHOWFILESTATS");
			dictionary112.Add(DbccCommand.ShowOffRules, "SHOWOFFRULES");
			dictionary112.Add(DbccCommand.ShowOnRules, "SHOWONRULES");
			dictionary112.Add(DbccCommand.ShowTableAffinity, "SHOWTABLEAFFINITY");
			dictionary112.Add(DbccCommand.ShowText, "SHOWTEXT");
			dictionary112.Add(DbccCommand.ShowWeights, "SHOWWEIGHTS");
			dictionary112.Add(DbccCommand.ShrinkDatabase, "SHRINKDATABASE");
			dictionary112.Add(DbccCommand.ShrinkFile, "SHRINKFILE");
			dictionary112.Add(DbccCommand.SqlMgrStats, "SQLMGRSTATS");
			dictionary112.Add(DbccCommand.SqlPerf, "SQLPERF");
			dictionary112.Add(DbccCommand.StackDump, "STACKDUMP");
			dictionary112.Add(DbccCommand.Tec, "TEC");
			dictionary112.Add(DbccCommand.ThawIO, "THAW_IO");
			dictionary112.Add(DbccCommand.TraceOff, "TRACEOFF");
			dictionary112.Add(DbccCommand.TraceOn, "TRACEON");
			dictionary112.Add(DbccCommand.TraceStatus, "TRACESTATUS");
			dictionary112.Add(DbccCommand.UnpinTable, "UNPINTABLE");
			dictionary112.Add(DbccCommand.UpdateUsage, "UPDATEUSAGE");
			dictionary112.Add(DbccCommand.UsePlan, "USEPLAN");
			dictionary112.Add(DbccCommand.UserOptions, "USEROPTIONS");
			dictionary112.Add(DbccCommand.WritePage, "WRITEPAGE");
			SqlScriptGeneratorVisitor._dbccCommandNames = dictionary112;
			Dictionary<DbccOptionKind, TokenGenerator> dictionary113 = new Dictionary<DbccOptionKind, TokenGenerator>();
			dictionary113.Add(DbccOptionKind.AllErrorMessages, new IdentifierGenerator("ALL_ERRORMSGS"));
			dictionary113.Add(DbccOptionKind.CountRows, new IdentifierGenerator("COUNT_ROWS"));
			dictionary113.Add(DbccOptionKind.NoInfoMessages, new IdentifierGenerator("NO_INFOMSGS"));
			dictionary113.Add(DbccOptionKind.TableResults, new IdentifierGenerator("TABLERESULTS"));
			dictionary113.Add(DbccOptionKind.TabLock, new IdentifierGenerator("TABLOCK"));
			dictionary113.Add(DbccOptionKind.StatHeader, new IdentifierGenerator("STAT_HEADER"));
			dictionary113.Add(DbccOptionKind.DensityVector, new IdentifierGenerator("DENSITY_VECTOR"));
			dictionary113.Add(DbccOptionKind.HistogramSteps, new IdentifierGenerator("HISTOGRAM_STEPS"));
			dictionary113.Add(DbccOptionKind.EstimateOnly, new IdentifierGenerator("ESTIMATEONLY"));
			dictionary113.Add(DbccOptionKind.Fast, new IdentifierGenerator("FAST"));
			dictionary113.Add(DbccOptionKind.AllLevels, new IdentifierGenerator("ALL_LEVELS"));
			dictionary113.Add(DbccOptionKind.AllIndexes, new IdentifierGenerator("ALL_INDEXES"));
			dictionary113.Add(DbccOptionKind.PhysicalOnly, new IdentifierGenerator("PHYSICAL_ONLY"));
			dictionary113.Add(DbccOptionKind.AllConstraints, new IdentifierGenerator("ALL_CONSTRAINTS"));
			dictionary113.Add(DbccOptionKind.StatsStream, new IdentifierGenerator("STATS_STREAM"));
			dictionary113.Add(DbccOptionKind.Histogram, new IdentifierGenerator("HISTOGRAM"));
			dictionary113.Add(DbccOptionKind.DataPurity, new IdentifierGenerator("DATA_PURITY"));
			dictionary113.Add(DbccOptionKind.MarkInUseForRemoval, new IdentifierGenerator("MARK_IN_USE_FOR_REMOVAL"));
			dictionary113.Add(DbccOptionKind.ExtendedLogicalChecks, new IdentifierGenerator("EXTENDED_LOGICAL_CHECKS"));
			SqlScriptGeneratorVisitor._dbccOptionsGenerators = dictionary113;
			Dictionary<DeviceType, TokenGenerator> dictionary114 = new Dictionary<DeviceType, TokenGenerator>();
			dictionary114.Add(DeviceType.DatabaseSnapshot, new IdentifierGenerator("DATABASE_SNAPSHOT"));
			dictionary114.Add(DeviceType.Disk, new KeywordGenerator(TSqlTokenType.Disk));
			dictionary114.Add(DeviceType.Tape, new IdentifierGenerator("TAPE"));
			dictionary114.Add(DeviceType.VirtualDevice, new IdentifierGenerator("VIRTUAL_DEVICE"));
			SqlScriptGeneratorVisitor._deviceTypeGenerators = dictionary114;
			Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>> dictionary115 = new Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>>();
			Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>> dictionary116 = dictionary115;
			DropClusteredConstraintOptionKind dropClusteredConstraintOptionKind = DropClusteredConstraintOptionKind.MaxDop;
			List<TokenGenerator> list76 = new List<TokenGenerator>();
			list76.Add(new IdentifierGenerator("MAXDOP", true));
			list76.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary116.Add(dropClusteredConstraintOptionKind, list76);
			Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>> dictionary117 = dictionary115;
			DropClusteredConstraintOptionKind dropClusteredConstraintOptionKind2 = DropClusteredConstraintOptionKind.MoveTo;
			List<TokenGenerator> list77 = new List<TokenGenerator>();
			list77.Add(new IdentifierGenerator("MOVE", true));
			list77.Add(new KeywordGenerator(TSqlTokenType.To));
			dictionary117.Add(dropClusteredConstraintOptionKind2, list77);
			Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>> dictionary118 = dictionary115;
			DropClusteredConstraintOptionKind dropClusteredConstraintOptionKind3 = DropClusteredConstraintOptionKind.Online;
			List<TokenGenerator> list78 = new List<TokenGenerator>();
			list78.Add(new IdentifierGenerator("ONLINE", true));
			list78.Add(new KeywordGenerator(TSqlTokenType.EqualsSign));
			dictionary118.Add(dropClusteredConstraintOptionKind3, list78);
			SqlScriptGeneratorVisitor._dropClusteredConstraintOptionTypeGenerators = dictionary115;
			Dictionary<EndpointEncryptionSupport, TokenGenerator> dictionary119 = new Dictionary<EndpointEncryptionSupport, TokenGenerator>();
			dictionary119.Add(EndpointEncryptionSupport.Disabled, new IdentifierGenerator("DISABLED"));
			dictionary119.Add(EndpointEncryptionSupport.NotSpecified, new EmptyGenerator());
			dictionary119.Add(EndpointEncryptionSupport.Required, new IdentifierGenerator("REQUIRED"));
			dictionary119.Add(EndpointEncryptionSupport.Supported, new IdentifierGenerator("SUPPORTED"));
			SqlScriptGeneratorVisitor._endpointEncryptionSupportGenerators = dictionary119;
			Dictionary<ExecuteAsOption, TokenGenerator> dictionary120 = new Dictionary<ExecuteAsOption, TokenGenerator>();
			dictionary120.Add(ExecuteAsOption.Caller, new IdentifierGenerator("CALLER"));
			dictionary120.Add(ExecuteAsOption.Login, new IdentifierGenerator("LOGIN"));
			dictionary120.Add(ExecuteAsOption.Owner, new IdentifierGenerator("OWNER"));
			dictionary120.Add(ExecuteAsOption.Self, new IdentifierGenerator("SELF"));
			dictionary120.Add(ExecuteAsOption.User, new KeywordGenerator(TSqlTokenType.User));
			SqlScriptGeneratorVisitor._executeAsOptionGenerators = dictionary120;
			Dictionary<FetchOrientation, string> dictionary121 = new Dictionary<FetchOrientation, string>();
			dictionary121.Add(FetchOrientation.Absolute, "ABSOLUTE");
			dictionary121.Add(FetchOrientation.First, "FIRST");
			dictionary121.Add(FetchOrientation.Last, "LAST");
			dictionary121.Add(FetchOrientation.Next, "NEXT");
			dictionary121.Add(FetchOrientation.Prior, "PRIOR");
			dictionary121.Add(FetchOrientation.Relative, "RELATIVE");
			SqlScriptGeneratorVisitor._fetchOrientationNames = dictionary121;
			Dictionary<FullTextFunctionType, TokenGenerator> dictionary122 = new Dictionary<FullTextFunctionType, TokenGenerator>();
			dictionary122.Add(FullTextFunctionType.Contains, new KeywordGenerator(TSqlTokenType.Contains));
			dictionary122.Add(FullTextFunctionType.FreeText, new KeywordGenerator(TSqlTokenType.FreeText));
			SqlScriptGeneratorVisitor._fulltextFunctionTypeGenerators = dictionary122;
			Dictionary<FunctionOptionKind, List<TokenGenerator>> dictionary123 = new Dictionary<FunctionOptionKind, List<TokenGenerator>>();
			Dictionary<FunctionOptionKind, List<TokenGenerator>> dictionary124 = dictionary123;
			FunctionOptionKind functionOptionKind = FunctionOptionKind.CalledOnNullInput;
			List<TokenGenerator> list79 = new List<TokenGenerator>();
			list79.Add(new IdentifierGenerator("CALLED", true));
			list79.Add(new KeywordGenerator(TSqlTokenType.On, true));
			list79.Add(new KeywordGenerator(TSqlTokenType.Null, true));
			list79.Add(new IdentifierGenerator("INPUT"));
			dictionary124.Add(functionOptionKind, list79);
			Dictionary<FunctionOptionKind, List<TokenGenerator>> dictionary125 = dictionary123;
			FunctionOptionKind functionOptionKind2 = FunctionOptionKind.Encryption;
			List<TokenGenerator> list80 = new List<TokenGenerator>();
			list80.Add(new IdentifierGenerator("ENCRYPTION"));
			dictionary125.Add(functionOptionKind2, list80);
			Dictionary<FunctionOptionKind, List<TokenGenerator>> dictionary126 = dictionary123;
			FunctionOptionKind functionOptionKind3 = FunctionOptionKind.ReturnsNullOnNullInput;
			List<TokenGenerator> list81 = new List<TokenGenerator>();
			list81.Add(new IdentifierGenerator("RETURNS", true));
			list81.Add(new KeywordGenerator(TSqlTokenType.Null, true));
			list81.Add(new KeywordGenerator(TSqlTokenType.On, true));
			list81.Add(new KeywordGenerator(TSqlTokenType.Null, true));
			list81.Add(new IdentifierGenerator("INPUT"));
			dictionary126.Add(functionOptionKind3, list81);
			Dictionary<FunctionOptionKind, List<TokenGenerator>> dictionary127 = dictionary123;
			FunctionOptionKind functionOptionKind4 = FunctionOptionKind.SchemaBinding;
			List<TokenGenerator> list82 = new List<TokenGenerator>();
			list82.Add(new IdentifierGenerator("SCHEMABINDING"));
			dictionary127.Add(functionOptionKind4, list82);
			SqlScriptGeneratorVisitor._functionOptionsGenerators = dictionary123;
			Dictionary<GeneralSetCommandType, TokenGenerator> dictionary128 = new Dictionary<GeneralSetCommandType, TokenGenerator>();
			dictionary128.Add(GeneralSetCommandType.ContextInfo, new IdentifierGenerator("CONTEXT_INFO"));
			dictionary128.Add(GeneralSetCommandType.DateFirst, new IdentifierGenerator("DATEFIRST"));
			dictionary128.Add(GeneralSetCommandType.DateFormat, new IdentifierGenerator("DATEFORMAT"));
			dictionary128.Add(GeneralSetCommandType.DeadlockPriority, new IdentifierGenerator("DEADLOCK_PRIORITY"));
			dictionary128.Add(GeneralSetCommandType.Language, new IdentifierGenerator("LANGUAGE"));
			dictionary128.Add(GeneralSetCommandType.LockTimeout, new IdentifierGenerator("LOCK_TIMEOUT"));
			dictionary128.Add(GeneralSetCommandType.None, new EmptyGenerator());
			dictionary128.Add(GeneralSetCommandType.QueryGovernorCostLimit, new IdentifierGenerator("QUERY_GOVERNOR_COST_LIMIT"));
			SqlScriptGeneratorVisitor._generalSetCommandTypeGenerators = dictionary128;
			Dictionary<PrincipalOptionKind, string> dictionary129 = new Dictionary<PrincipalOptionKind, string>();
			dictionary129.Add(PrincipalOptionKind.CheckExpiration, "CHECK_EXPIRATION");
			dictionary129.Add(PrincipalOptionKind.CheckPolicy, "CHECK_POLICY");
			dictionary129.Add(PrincipalOptionKind.Credential, "CREDENTIAL");
			dictionary129.Add(PrincipalOptionKind.DefaultDatabase, "DEFAULT_DATABASE");
			dictionary129.Add(PrincipalOptionKind.DefaultLanguage, "DEFAULT_LANGUAGE");
			dictionary129.Add(PrincipalOptionKind.Name, "NAME");
			dictionary129.Add(PrincipalOptionKind.Password, "PASSWORD");
			dictionary129.Add(PrincipalOptionKind.Sid, "SID");
			dictionary129.Add(PrincipalOptionKind.DefaultSchema, "DEFAULT_SCHEMA");
			dictionary129.Add(PrincipalOptionKind.Login, "LOGIN");
			SqlScriptGeneratorVisitor._loginOptionsNames = dictionary129;
			Dictionary<EndpointProtocolOptions, string> dictionary130 = new Dictionary<EndpointProtocolOptions, string>();
			dictionary130.Add(EndpointProtocolOptions.HttpAuthenticationRealm, "AUTH_REALM");
			dictionary130.Add(EndpointProtocolOptions.HttpClearPort, "CLEAR_PORT");
			dictionary130.Add(EndpointProtocolOptions.HttpDefaultLogonDomain, "DEFAULT_LOGON_DOMAIN");
			dictionary130.Add(EndpointProtocolOptions.HttpPath, "PATH");
			dictionary130.Add(EndpointProtocolOptions.HttpSite, "SITE");
			dictionary130.Add(EndpointProtocolOptions.HttpSslPort, "SSL_PORT");
			dictionary130.Add(EndpointProtocolOptions.TcpListenerPort, "LISTENER_PORT");
			SqlScriptGeneratorVisitor._endpointProtocolOptionsNames = dictionary130;
			Dictionary<MessageValidationMethod, string> dictionary131 = new Dictionary<MessageValidationMethod, string>();
			dictionary131.Add(MessageValidationMethod.Empty, "EMPTY");
			dictionary131.Add(MessageValidationMethod.None, "NONE");
			dictionary131.Add(MessageValidationMethod.ValidXml, "VALID_XML");
			dictionary131.Add(MessageValidationMethod.WellFormedXml, "WELL_FORMED_XML");
			SqlScriptGeneratorVisitor._MessageValidationMethodNames = dictionary131;
			Dictionary<PortTypes, TokenGenerator> dictionary132 = new Dictionary<PortTypes, TokenGenerator>();
			dictionary132.Add(PortTypes.Clear, new IdentifierGenerator("CLEAR"));
			dictionary132.Add(PortTypes.Ssl, new IdentifierGenerator("SSL"));
			SqlScriptGeneratorVisitor._portTypesGenerators = dictionary132;
			Dictionary<SetOptions, TokenGenerator> dictionary133 = new Dictionary<SetOptions, TokenGenerator>();
			dictionary133.Add(SetOptions.AnsiDefaults, new IdentifierGenerator("ANSI_DEFAULTS"));
			dictionary133.Add(SetOptions.AnsiNullDfltOff, new IdentifierGenerator("ANSI_NULL_DFLT_OFF"));
			dictionary133.Add(SetOptions.AnsiNullDfltOn, new IdentifierGenerator("ANSI_NULL_DFLT_ON"));
			dictionary133.Add(SetOptions.AnsiNulls, new IdentifierGenerator("ANSI_NULLS"));
			dictionary133.Add(SetOptions.AnsiPadding, new IdentifierGenerator("ANSI_PADDING"));
			dictionary133.Add(SetOptions.AnsiWarnings, new IdentifierGenerator("ANSI_WARNINGS"));
			dictionary133.Add(SetOptions.ArithAbort, new IdentifierGenerator("ARITHABORT"));
			dictionary133.Add(SetOptions.ArithIgnore, new IdentifierGenerator("ARITHIGNORE"));
			dictionary133.Add(SetOptions.ConcatNullYieldsNull, new IdentifierGenerator("CONCAT_NULL_YIELDS_NULL"));
			dictionary133.Add(SetOptions.CursorCloseOnCommit, new IdentifierGenerator("CURSOR_CLOSE_ON_COMMIT"));
			dictionary133.Add(SetOptions.DisableDefCnstChk, new IdentifierGenerator("DISABLE_DEF_CNST_CHK"));
			dictionary133.Add(SetOptions.FmtOnly, new IdentifierGenerator("FMTONLY"));
			dictionary133.Add(SetOptions.ForcePlan, new IdentifierGenerator("FORCEPLAN"));
			dictionary133.Add(SetOptions.ImplicitTransactions, new IdentifierGenerator("IMPLICIT_TRANSACTIONS"));
			dictionary133.Add(SetOptions.NoCount, new IdentifierGenerator("NOCOUNT"));
			dictionary133.Add(SetOptions.NoExec, new IdentifierGenerator("NOEXEC"));
			dictionary133.Add(SetOptions.NumericRoundAbort, new IdentifierGenerator("NUMERIC_ROUNDABORT"));
			dictionary133.Add(SetOptions.ParseOnly, new IdentifierGenerator("PARSEONLY"));
			dictionary133.Add(SetOptions.QuotedIdentifier, new IdentifierGenerator("QUOTED_IDENTIFIER"));
			dictionary133.Add(SetOptions.RemoteProcTransactions, new IdentifierGenerator("REMOTE_PROC_TRANSACTIONS"));
			dictionary133.Add(SetOptions.ShowPlanAll, new IdentifierGenerator("SHOWPLAN_ALL"));
			dictionary133.Add(SetOptions.ShowPlanText, new IdentifierGenerator("SHOWPLAN_TEXT"));
			dictionary133.Add(SetOptions.ShowPlanXml, new IdentifierGenerator("SHOWPLAN_XML"));
			dictionary133.Add(SetOptions.XactAbort, new IdentifierGenerator("XACT_ABORT"));
			dictionary133.Add(SetOptions.NoBrowsetable, new IdentifierGenerator("NO_BROWSETABLE"));
			SqlScriptGeneratorVisitor._setOptionsGenerators = dictionary133;
			Dictionary<PrivilegeType80, TokenGenerator> dictionary134 = new Dictionary<PrivilegeType80, TokenGenerator>();
			dictionary134.Add(PrivilegeType80.All, new KeywordGenerator(TSqlTokenType.All));
			dictionary134.Add(PrivilegeType80.Delete, new KeywordGenerator(TSqlTokenType.Delete));
			dictionary134.Add(PrivilegeType80.Execute, new KeywordGenerator(TSqlTokenType.Execute));
			dictionary134.Add(PrivilegeType80.Insert, new KeywordGenerator(TSqlTokenType.Insert));
			dictionary134.Add(PrivilegeType80.References, new KeywordGenerator(TSqlTokenType.References));
			dictionary134.Add(PrivilegeType80.Select, new KeywordGenerator(TSqlTokenType.Select));
			dictionary134.Add(PrivilegeType80.Update, new KeywordGenerator(TSqlTokenType.Update));
			SqlScriptGeneratorVisitor._privilegeType80Generators = dictionary134;
			Dictionary<RaiseErrorOptions, TokenGenerator> dictionary135 = new Dictionary<RaiseErrorOptions, TokenGenerator>();
			dictionary135.Add(RaiseErrorOptions.Log, new IdentifierGenerator("LOG"));
			dictionary135.Add(RaiseErrorOptions.NoWait, new IdentifierGenerator("NOWAIT"));
			dictionary135.Add(RaiseErrorOptions.SetError, new IdentifierGenerator("SETERROR"));
			SqlScriptGeneratorVisitor._raiseErrorOptionsGenerators = dictionary135;
			Dictionary<RestoreStatementKind, TokenGenerator> dictionary136 = new Dictionary<RestoreStatementKind, TokenGenerator>();
			dictionary136.Add(RestoreStatementKind.None, new EmptyGenerator());
			dictionary136.Add(RestoreStatementKind.FileListOnly, new IdentifierGenerator("FILELISTONLY"));
			dictionary136.Add(RestoreStatementKind.HeaderOnly, new IdentifierGenerator("HEADERONLY"));
			dictionary136.Add(RestoreStatementKind.LabelOnly, new IdentifierGenerator("LABELONLY"));
			dictionary136.Add(RestoreStatementKind.RewindOnly, new IdentifierGenerator("REWINDONLY"));
			dictionary136.Add(RestoreStatementKind.VerifyOnly, new IdentifierGenerator("VERIFYONLY"));
			SqlScriptGeneratorVisitor._restoreStatementKindGenerators = dictionary136;
			Dictionary<DatabaseMirroringEndpointRole, TokenGenerator> dictionary137 = new Dictionary<DatabaseMirroringEndpointRole, TokenGenerator>();
			dictionary137.Add(DatabaseMirroringEndpointRole.NotSpecified, new EmptyGenerator());
			dictionary137.Add(DatabaseMirroringEndpointRole.All, new KeywordGenerator(TSqlTokenType.All));
			dictionary137.Add(DatabaseMirroringEndpointRole.Partner, new IdentifierGenerator("PARTNER"));
			dictionary137.Add(DatabaseMirroringEndpointRole.Witness, new IdentifierGenerator("WITNESS"));
			SqlScriptGeneratorVisitor._databaseMirroringEndpointRoleGenerators = dictionary137;
			Dictionary<RouteOptionKind, string> dictionary138 = new Dictionary<RouteOptionKind, string>();
			dictionary138.Add(RouteOptionKind.Address, "ADDRESS");
			dictionary138.Add(RouteOptionKind.BrokerInstance, "BROKER_INSTANCE");
			dictionary138.Add(RouteOptionKind.Lifetime, "LIFETIME");
			dictionary138.Add(RouteOptionKind.MirrorAddress, "MIRROR_ADDRESS");
			dictionary138.Add(RouteOptionKind.ServiceName, "SERVICE_NAME");
			SqlScriptGeneratorVisitor._RouteOptionTypeNames = dictionary138;
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary139 = new Dictionary<SecurityObjectKind, List<TokenGenerator>>();
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary140 = dictionary139;
			SecurityObjectKind securityObjectKind = SecurityObjectKind.AvailabilityGroup;
			List<TokenGenerator> list83 = new List<TokenGenerator>();
			list83.Add(new IdentifierGenerator("AVAILABILITY", true));
			list83.Add(new IdentifierGenerator("GROUP"));
			dictionary140.Add(securityObjectKind, list83);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary141 = dictionary139;
			SecurityObjectKind securityObjectKind2 = SecurityObjectKind.ApplicationRole;
			List<TokenGenerator> list84 = new List<TokenGenerator>();
			list84.Add(new IdentifierGenerator("APPLICATION", true));
			list84.Add(new IdentifierGenerator("ROLE"));
			dictionary141.Add(securityObjectKind2, list84);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary142 = dictionary139;
			SecurityObjectKind securityObjectKind3 = SecurityObjectKind.Assembly;
			List<TokenGenerator> list85 = new List<TokenGenerator>();
			list85.Add(new IdentifierGenerator("ASSEMBLY"));
			dictionary142.Add(securityObjectKind3, list85);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary143 = dictionary139;
			SecurityObjectKind securityObjectKind4 = SecurityObjectKind.AsymmetricKey;
			List<TokenGenerator> list86 = new List<TokenGenerator>();
			list86.Add(new IdentifierGenerator("ASYMMETRIC", true));
			list86.Add(new KeywordGenerator(TSqlTokenType.Key));
			dictionary143.Add(securityObjectKind4, list86);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary144 = dictionary139;
			SecurityObjectKind securityObjectKind5 = SecurityObjectKind.Certificate;
			List<TokenGenerator> list87 = new List<TokenGenerator>();
			list87.Add(new IdentifierGenerator("CERTIFICATE"));
			dictionary144.Add(securityObjectKind5, list87);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary145 = dictionary139;
			SecurityObjectKind securityObjectKind6 = SecurityObjectKind.Contract;
			List<TokenGenerator> list88 = new List<TokenGenerator>();
			list88.Add(new IdentifierGenerator("CONTRACT"));
			dictionary145.Add(securityObjectKind6, list88);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary146 = dictionary139;
			SecurityObjectKind securityObjectKind7 = SecurityObjectKind.Database;
			List<TokenGenerator> list89 = new List<TokenGenerator>();
			list89.Add(new KeywordGenerator(TSqlTokenType.Database));
			dictionary146.Add(securityObjectKind7, list89);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary147 = dictionary139;
			SecurityObjectKind securityObjectKind8 = SecurityObjectKind.Endpoint;
			List<TokenGenerator> list90 = new List<TokenGenerator>();
			list90.Add(new IdentifierGenerator("ENDPOINT"));
			dictionary147.Add(securityObjectKind8, list90);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary148 = dictionary139;
			SecurityObjectKind securityObjectKind9 = SecurityObjectKind.FullTextCatalog;
			List<TokenGenerator> list91 = new List<TokenGenerator>();
			list91.Add(new IdentifierGenerator("FULLTEXT", true));
			list91.Add(new IdentifierGenerator("CATALOG"));
			dictionary148.Add(securityObjectKind9, list91);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary149 = dictionary139;
			SecurityObjectKind securityObjectKind10 = SecurityObjectKind.Login;
			List<TokenGenerator> list92 = new List<TokenGenerator>();
			list92.Add(new IdentifierGenerator("LOGIN"));
			dictionary149.Add(securityObjectKind10, list92);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary150 = dictionary139;
			SecurityObjectKind securityObjectKind11 = SecurityObjectKind.MessageType;
			List<TokenGenerator> list93 = new List<TokenGenerator>();
			list93.Add(new IdentifierGenerator("MESSAGE", true));
			list93.Add(new IdentifierGenerator("TYPE"));
			dictionary150.Add(securityObjectKind11, list93);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary151 = dictionary139;
			SecurityObjectKind securityObjectKind12 = SecurityObjectKind.Object;
			List<TokenGenerator> list94 = new List<TokenGenerator>();
			list94.Add(new IdentifierGenerator("OBJECT"));
			dictionary151.Add(securityObjectKind12, list94);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary152 = dictionary139;
			SecurityObjectKind securityObjectKind13 = SecurityObjectKind.RemoteServiceBinding;
			List<TokenGenerator> list95 = new List<TokenGenerator>();
			list95.Add(new IdentifierGenerator("REMOTE", true));
			list95.Add(new IdentifierGenerator("SERVICE", true));
			list95.Add(new IdentifierGenerator("BINDING"));
			dictionary152.Add(securityObjectKind13, list95);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary153 = dictionary139;
			SecurityObjectKind securityObjectKind14 = SecurityObjectKind.Role;
			List<TokenGenerator> list96 = new List<TokenGenerator>();
			list96.Add(new IdentifierGenerator("ROLE"));
			dictionary153.Add(securityObjectKind14, list96);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary154 = dictionary139;
			SecurityObjectKind securityObjectKind15 = SecurityObjectKind.Route;
			List<TokenGenerator> list97 = new List<TokenGenerator>();
			list97.Add(new IdentifierGenerator("ROUTE"));
			dictionary154.Add(securityObjectKind15, list97);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary155 = dictionary139;
			SecurityObjectKind securityObjectKind16 = SecurityObjectKind.Schema;
			List<TokenGenerator> list98 = new List<TokenGenerator>();
			list98.Add(new KeywordGenerator(TSqlTokenType.Schema));
			dictionary155.Add(securityObjectKind16, list98);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary156 = dictionary139;
			SecurityObjectKind securityObjectKind17 = SecurityObjectKind.Server;
			List<TokenGenerator> list99 = new List<TokenGenerator>();
			list99.Add(new IdentifierGenerator("SERVER"));
			dictionary156.Add(securityObjectKind17, list99);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary157 = dictionary139;
			SecurityObjectKind securityObjectKind18 = SecurityObjectKind.ServerRole;
			List<TokenGenerator> list100 = new List<TokenGenerator>();
			list100.Add(new IdentifierGenerator("SERVER", true));
			list100.Add(new IdentifierGenerator("ROLE"));
			dictionary157.Add(securityObjectKind18, list100);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary158 = dictionary139;
			SecurityObjectKind securityObjectKind19 = SecurityObjectKind.Service;
			List<TokenGenerator> list101 = new List<TokenGenerator>();
			list101.Add(new IdentifierGenerator("SERVICE"));
			dictionary158.Add(securityObjectKind19, list101);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary159 = dictionary139;
			SecurityObjectKind securityObjectKind20 = SecurityObjectKind.SymmetricKey;
			List<TokenGenerator> list102 = new List<TokenGenerator>();
			list102.Add(new IdentifierGenerator("SYMMETRIC", true));
			list102.Add(new KeywordGenerator(TSqlTokenType.Key));
			dictionary159.Add(securityObjectKind20, list102);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary160 = dictionary139;
			SecurityObjectKind securityObjectKind21 = SecurityObjectKind.Type;
			List<TokenGenerator> list103 = new List<TokenGenerator>();
			list103.Add(new IdentifierGenerator("TYPE"));
			dictionary160.Add(securityObjectKind21, list103);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary161 = dictionary139;
			SecurityObjectKind securityObjectKind22 = SecurityObjectKind.User;
			List<TokenGenerator> list104 = new List<TokenGenerator>();
			list104.Add(new KeywordGenerator(TSqlTokenType.User));
			dictionary161.Add(securityObjectKind22, list104);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary162 = dictionary139;
			SecurityObjectKind securityObjectKind23 = SecurityObjectKind.XmlSchemaCollection;
			List<TokenGenerator> list105 = new List<TokenGenerator>();
			list105.Add(new IdentifierGenerator("XML", true));
			list105.Add(new KeywordGenerator(TSqlTokenType.Schema, true));
			list105.Add(new IdentifierGenerator("COLLECTION"));
			dictionary162.Add(securityObjectKind23, list105);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary163 = dictionary139;
			SecurityObjectKind securityObjectKind24 = SecurityObjectKind.FullTextStopList;
			List<TokenGenerator> list106 = new List<TokenGenerator>();
			list106.Add(new IdentifierGenerator("FULLTEXT", true));
			list106.Add(new KeywordGenerator(TSqlTokenType.StopList));
			dictionary163.Add(securityObjectKind24, list106);
			Dictionary<SecurityObjectKind, List<TokenGenerator>> dictionary164 = dictionary139;
			SecurityObjectKind securityObjectKind25 = SecurityObjectKind.SearchPropertyList;
			List<TokenGenerator> list107 = new List<TokenGenerator>();
			list107.Add(new IdentifierGenerator("SEARCH", true));
			list107.Add(new IdentifierGenerator("PROPERTY", true));
			list107.Add(new IdentifierGenerator("LIST"));
			dictionary164.Add(securityObjectKind25, list107);
			SqlScriptGeneratorVisitor._securityObjectKindGenerators = dictionary139;
			Dictionary<SetOffsets, TokenGenerator> dictionary165 = new Dictionary<SetOffsets, TokenGenerator>();
			dictionary165.Add(SetOffsets.Compute, new KeywordGenerator(TSqlTokenType.Compute));
			dictionary165.Add(SetOffsets.Execute, new KeywordGenerator(TSqlTokenType.Execute));
			dictionary165.Add(SetOffsets.From, new KeywordGenerator(TSqlTokenType.From));
			dictionary165.Add(SetOffsets.Order, new KeywordGenerator(TSqlTokenType.Order));
			dictionary165.Add(SetOffsets.Param, new IdentifierGenerator("PARAM"));
			dictionary165.Add(SetOffsets.Procedure, new KeywordGenerator(TSqlTokenType.Procedure));
			dictionary165.Add(SetOffsets.Select, new KeywordGenerator(TSqlTokenType.Select));
			dictionary165.Add(SetOffsets.Statement, new IdentifierGenerator("STATEMENT"));
			dictionary165.Add(SetOffsets.Table, new KeywordGenerator(TSqlTokenType.Table));
			SqlScriptGeneratorVisitor._setOffsetsGenerators = dictionary165;
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary166 = new Dictionary<IsolationLevel, List<TokenGenerator>>();
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary167 = dictionary166;
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
			List<TokenGenerator> list108 = new List<TokenGenerator>();
			list108.Add(new KeywordGenerator(TSqlTokenType.Read, true));
			list108.Add(new IdentifierGenerator("COMMITTED"));
			dictionary167.Add(isolationLevel, list108);
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary168 = dictionary166;
			IsolationLevel isolationLevel2 = IsolationLevel.ReadUncommitted;
			List<TokenGenerator> list109 = new List<TokenGenerator>();
			list109.Add(new KeywordGenerator(TSqlTokenType.Read, true));
			list109.Add(new IdentifierGenerator("UNCOMMITTED"));
			dictionary168.Add(isolationLevel2, list109);
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary169 = dictionary166;
			IsolationLevel isolationLevel3 = IsolationLevel.RepeatableRead;
			List<TokenGenerator> list110 = new List<TokenGenerator>();
			list110.Add(new IdentifierGenerator("REPEATABLE", true));
			list110.Add(new KeywordGenerator(TSqlTokenType.Read));
			dictionary169.Add(isolationLevel3, list110);
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary170 = dictionary166;
			IsolationLevel isolationLevel4 = IsolationLevel.Serializable;
			List<TokenGenerator> list111 = new List<TokenGenerator>();
			list111.Add(new IdentifierGenerator("SERIALIZABLE"));
			dictionary170.Add(isolationLevel4, list111);
			Dictionary<IsolationLevel, List<TokenGenerator>> dictionary171 = dictionary166;
			IsolationLevel isolationLevel5 = IsolationLevel.Snapshot;
			List<TokenGenerator> list112 = new List<TokenGenerator>();
			list112.Add(new IdentifierGenerator("SNAPSHOT"));
			dictionary171.Add(isolationLevel5, list112);
			SqlScriptGeneratorVisitor._isolationLevelGenerators = dictionary166;
			Dictionary<SoapMethodAction, TokenGenerator> dictionary172 = new Dictionary<SoapMethodAction, TokenGenerator>();
			dictionary172.Add(SoapMethodAction.None, new EmptyGenerator());
			dictionary172.Add(SoapMethodAction.Add, new KeywordGenerator(TSqlTokenType.Add));
			dictionary172.Add(SoapMethodAction.Alter, new KeywordGenerator(TSqlTokenType.Alter));
			dictionary172.Add(SoapMethodAction.Drop, new KeywordGenerator(TSqlTokenType.Drop));
			SqlScriptGeneratorVisitor._soapMethodActionGenerators = dictionary172;
			Dictionary<SoapMethodSchemas, TokenGenerator> dictionary173 = new Dictionary<SoapMethodSchemas, TokenGenerator>();
			dictionary173.Add(SoapMethodSchemas.Default, new KeywordGenerator(TSqlTokenType.Default));
			dictionary173.Add(SoapMethodSchemas.None, new IdentifierGenerator("NONE"));
			dictionary173.Add(SoapMethodSchemas.Standard, new IdentifierGenerator("STANDARD"));
			SqlScriptGeneratorVisitor._soapMethodSchemasGenerators = dictionary173;
			Dictionary<SoapMethodFormat, string> dictionary174 = new Dictionary<SoapMethodFormat, string>();
			dictionary174.Add(SoapMethodFormat.AllResults, "ALL_RESULTS");
			dictionary174.Add(SoapMethodFormat.None, "NONE");
			dictionary174.Add(SoapMethodFormat.RowsetsOnly, "ROWSETS_ONLY");
			SqlScriptGeneratorVisitor._soapMethodFormatNames = dictionary174;
			Dictionary<SubqueryComparisonPredicateType, TokenGenerator> dictionary175 = new Dictionary<SubqueryComparisonPredicateType, TokenGenerator>();
			dictionary175.Add(SubqueryComparisonPredicateType.All, new KeywordGenerator(TSqlTokenType.All));
			dictionary175.Add(SubqueryComparisonPredicateType.Any, new KeywordGenerator(TSqlTokenType.Any));
			SqlScriptGeneratorVisitor._subqueryComparisonPredicateTypeGenerators = dictionary175;
			Dictionary<TableSampleClauseOption, TokenGenerator> dictionary176 = new Dictionary<TableSampleClauseOption, TokenGenerator>();
			dictionary176.Add(TableSampleClauseOption.NotSpecified, new EmptyGenerator());
			dictionary176.Add(TableSampleClauseOption.Percent, new KeywordGenerator(TSqlTokenType.Percent));
			dictionary176.Add(TableSampleClauseOption.Rows, new IdentifierGenerator("ROWS"));
			SqlScriptGeneratorVisitor._tableSampleClauseOptionGenerators = dictionary176;
			Dictionary<TriggerType, List<TokenGenerator>> dictionary177 = new Dictionary<TriggerType, List<TokenGenerator>>();
			Dictionary<TriggerType, List<TokenGenerator>> dictionary178 = dictionary177;
			TriggerType triggerType = TriggerType.After;
			List<TokenGenerator> list113 = new List<TokenGenerator>();
			list113.Add(new IdentifierGenerator("AFTER"));
			dictionary178.Add(triggerType, list113);
			Dictionary<TriggerType, List<TokenGenerator>> dictionary179 = dictionary177;
			TriggerType triggerType2 = TriggerType.For;
			List<TokenGenerator> list114 = new List<TokenGenerator>();
			list114.Add(new KeywordGenerator(TSqlTokenType.For));
			dictionary179.Add(triggerType2, list114);
			Dictionary<TriggerType, List<TokenGenerator>> dictionary180 = dictionary177;
			TriggerType triggerType3 = TriggerType.InsteadOf;
			List<TokenGenerator> list115 = new List<TokenGenerator>();
			list115.Add(new IdentifierGenerator("INSTEAD", true));
			list115.Add(new KeywordGenerator(TSqlTokenType.Of));
			dictionary180.Add(triggerType3, list115);
			Dictionary<TriggerType, List<TokenGenerator>> dictionary181 = dictionary177;
			TriggerType triggerType4 = TriggerType.Unknown;
			List<TokenGenerator> list116 = new List<TokenGenerator>();
			list116.Add(new EmptyGenerator());
			dictionary181.Add(triggerType4, list116);
			SqlScriptGeneratorVisitor._triggerTypeGenerators = dictionary177;
			Dictionary<UnqualifiedJoinType, List<TokenGenerator>> dictionary182 = new Dictionary<UnqualifiedJoinType, List<TokenGenerator>>();
			Dictionary<UnqualifiedJoinType, List<TokenGenerator>> dictionary183 = dictionary182;
			UnqualifiedJoinType unqualifiedJoinType = UnqualifiedJoinType.CrossApply;
			List<TokenGenerator> list117 = new List<TokenGenerator>();
			list117.Add(new KeywordGenerator(TSqlTokenType.Cross, true));
			list117.Add(new IdentifierGenerator("APPLY"));
			dictionary183.Add(unqualifiedJoinType, list117);
			Dictionary<UnqualifiedJoinType, List<TokenGenerator>> dictionary184 = dictionary182;
			UnqualifiedJoinType unqualifiedJoinType2 = UnqualifiedJoinType.CrossJoin;
			List<TokenGenerator> list118 = new List<TokenGenerator>();
			list118.Add(new KeywordGenerator(TSqlTokenType.Cross, true));
			list118.Add(new KeywordGenerator(TSqlTokenType.Join));
			dictionary184.Add(unqualifiedJoinType2, list118);
			Dictionary<UnqualifiedJoinType, List<TokenGenerator>> dictionary185 = dictionary182;
			UnqualifiedJoinType unqualifiedJoinType3 = UnqualifiedJoinType.OuterApply;
			List<TokenGenerator> list119 = new List<TokenGenerator>();
			list119.Add(new KeywordGenerator(TSqlTokenType.Outer, true));
			list119.Add(new IdentifierGenerator("APPLY"));
			dictionary185.Add(unqualifiedJoinType3, list119);
			SqlScriptGeneratorVisitor._unqualifiedJoinTypeGenerators = dictionary182;
			Dictionary<ViewOptionKind, string> dictionary186 = new Dictionary<ViewOptionKind, string>();
			dictionary186.Add(ViewOptionKind.Encryption, "ENCRYPTION");
			dictionary186.Add(ViewOptionKind.SchemaBinding, "SCHEMABINDING");
			dictionary186.Add(ViewOptionKind.ViewMetadata, "VIEW_METADATA");
			SqlScriptGeneratorVisitor._viewOptionTypeNames = dictionary186;
			Dictionary<WaitForOption, TokenGenerator> dictionary187 = new Dictionary<WaitForOption, TokenGenerator>();
			dictionary187.Add(WaitForOption.Delay, new IdentifierGenerator("DELAY"));
			dictionary187.Add(WaitForOption.Time, new IdentifierGenerator("TIME"));
			SqlScriptGeneratorVisitor._waitForOptionGenerators = dictionary187;
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary188 = new Dictionary<XmlForClauseOptions, List<TokenGenerator>>();
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary189 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions = XmlForClauseOptions.Raw;
			List<TokenGenerator> list120 = new List<TokenGenerator>();
			list120.Add(new IdentifierGenerator("RAW"));
			dictionary189.Add(xmlForClauseOptions, list120);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary190 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions2 = XmlForClauseOptions.Auto;
			List<TokenGenerator> list121 = new List<TokenGenerator>();
			list121.Add(new IdentifierGenerator("AUTO"));
			dictionary190.Add(xmlForClauseOptions2, list121);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary191 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions3 = XmlForClauseOptions.Explicit;
			List<TokenGenerator> list122 = new List<TokenGenerator>();
			list122.Add(new IdentifierGenerator("EXPLICIT"));
			dictionary191.Add(xmlForClauseOptions3, list122);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary192 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions4 = XmlForClauseOptions.Path;
			List<TokenGenerator> list123 = new List<TokenGenerator>();
			list123.Add(new IdentifierGenerator("PATH"));
			dictionary192.Add(xmlForClauseOptions4, list123);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary193 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions5 = XmlForClauseOptions.XmlData;
			List<TokenGenerator> list124 = new List<TokenGenerator>();
			list124.Add(new IdentifierGenerator("XMLDATA"));
			dictionary193.Add(xmlForClauseOptions5, list124);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary194 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions6 = XmlForClauseOptions.XmlSchema;
			List<TokenGenerator> list125 = new List<TokenGenerator>();
			list125.Add(new IdentifierGenerator("XMLSCHEMA"));
			dictionary194.Add(xmlForClauseOptions6, list125);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary195 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions7 = XmlForClauseOptions.Elements;
			List<TokenGenerator> list126 = new List<TokenGenerator>();
			list126.Add(new IdentifierGenerator("ELEMENTS"));
			dictionary195.Add(xmlForClauseOptions7, list126);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary196 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions8 = XmlForClauseOptions.ElementsXsiNil;
			List<TokenGenerator> list127 = new List<TokenGenerator>();
			list127.Add(new IdentifierGenerator("ELEMENTS", true));
			list127.Add(new IdentifierGenerator("XSINIL"));
			dictionary196.Add(xmlForClauseOptions8, list127);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary197 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions9 = XmlForClauseOptions.ElementsAbsent;
			List<TokenGenerator> list128 = new List<TokenGenerator>();
			list128.Add(new IdentifierGenerator("ELEMENTS", true));
			list128.Add(new IdentifierGenerator("ABSENT"));
			dictionary197.Add(xmlForClauseOptions9, list128);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary198 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions10 = XmlForClauseOptions.BinaryBase64;
			List<TokenGenerator> list129 = new List<TokenGenerator>();
			list129.Add(new IdentifierGenerator("BINARY", true));
			list129.Add(new IdentifierGenerator("BASE64"));
			dictionary198.Add(xmlForClauseOptions10, list129);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary199 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions11 = XmlForClauseOptions.Type;
			List<TokenGenerator> list130 = new List<TokenGenerator>();
			list130.Add(new IdentifierGenerator("TYPE"));
			dictionary199.Add(xmlForClauseOptions11, list130);
			Dictionary<XmlForClauseOptions, List<TokenGenerator>> dictionary200 = dictionary188;
			XmlForClauseOptions xmlForClauseOptions12 = XmlForClauseOptions.Root;
			List<TokenGenerator> list131 = new List<TokenGenerator>();
			list131.Add(new IdentifierGenerator("ROOT"));
			dictionary200.Add(xmlForClauseOptions12, list131);
			SqlScriptGeneratorVisitor._xmlForClauseOptionsGenerators = dictionary188;
			Dictionary<DeleteUpdateAction, List<TokenGenerator>> dictionary201 = new Dictionary<DeleteUpdateAction, List<TokenGenerator>>();
			Dictionary<DeleteUpdateAction, List<TokenGenerator>> dictionary202 = dictionary201;
			DeleteUpdateAction deleteUpdateAction = DeleteUpdateAction.Cascade;
			List<TokenGenerator> list132 = new List<TokenGenerator>();
			list132.Add(new KeywordGenerator(TSqlTokenType.Cascade));
			dictionary202.Add(deleteUpdateAction, list132);
			Dictionary<DeleteUpdateAction, List<TokenGenerator>> dictionary203 = dictionary201;
			DeleteUpdateAction deleteUpdateAction2 = DeleteUpdateAction.NoAction;
			List<TokenGenerator> list133 = new List<TokenGenerator>();
			list133.Add(new IdentifierGenerator("NO", true));
			list133.Add(new IdentifierGenerator("ACTION"));
			dictionary203.Add(deleteUpdateAction2, list133);
			Dictionary<DeleteUpdateAction, List<TokenGenerator>> dictionary204 = dictionary201;
			DeleteUpdateAction deleteUpdateAction3 = DeleteUpdateAction.SetDefault;
			List<TokenGenerator> list134 = new List<TokenGenerator>();
			list134.Add(new KeywordGenerator(TSqlTokenType.Set, true));
			list134.Add(new KeywordGenerator(TSqlTokenType.Default));
			dictionary204.Add(deleteUpdateAction3, list134);
			Dictionary<DeleteUpdateAction, List<TokenGenerator>> dictionary205 = dictionary201;
			DeleteUpdateAction deleteUpdateAction4 = DeleteUpdateAction.SetNull;
			List<TokenGenerator> list135 = new List<TokenGenerator>();
			list135.Add(new KeywordGenerator(TSqlTokenType.Set, true));
			list135.Add(new KeywordGenerator(TSqlTokenType.Null));
			dictionary205.Add(deleteUpdateAction4, list135);
			SqlScriptGeneratorVisitor._deleteUpdateActionGenerators = dictionary201;
		}

		// Token: 0x040008D2 RID: 2258
		protected const string ClauseBody = "ClauseBody";

		// Token: 0x040008D3 RID: 2259
		protected const string SetClauseItemFirstEqualSign = "SetClauseItemFirstEqualSign";

		// Token: 0x040008D4 RID: 2260
		protected const string SetClauseItemSecondEqualSign = "SetClauseItemSecondEqualSign";

		// Token: 0x040008D5 RID: 2261
		protected const string InsertColumns = "InsertColumns";

		// Token: 0x040008D6 RID: 2262
		protected static Dictionary<NonTransactedFileStreamAccess, string> _nonTransactedFileStreamAccessNames;

		// Token: 0x040008D7 RID: 2263
		protected static Dictionary<BooleanTernaryExpressionType, List<TokenGenerator>> _ternaryExpressionTypeGenerators;

		// Token: 0x040008D8 RID: 2264
		protected static Dictionary<OptimizerHintKind, List<TokenGenerator>> _optimizerHintKindsGenerators;

		// Token: 0x040008D9 RID: 2265
		private static Dictionary<PageVerifyDatabaseOptionKind, string> _pageVerifyDatabaseOptionKindNames;

		// Token: 0x040008DA RID: 2266
		private static Dictionary<RecoveryDatabaseOptionKind, TokenGenerator> _recoveryDatabaseOptionKindNames;

		// Token: 0x040008DB RID: 2267
		protected static Dictionary<RestoreOptionKind, TokenGenerator> _restoreOptionKindGenerators;

		// Token: 0x040008DC RID: 2268
		private static Dictionary<QueueOptionKind, string> _queueOptionTypeNames;

		// Token: 0x040008DD RID: 2269
		private static Dictionary<DatabaseAuditActionKind, TokenGenerator> _databaseAuditActionName;

		// Token: 0x040008DE RID: 2270
		protected static Dictionary<SortOrder, TokenGenerator> _sortOrderGenerators;

		// Token: 0x040008DF RID: 2271
		protected static Dictionary<InsertOption, TokenGenerator> _insertOptionGenerators;

		// Token: 0x040008E0 RID: 2272
		protected static Dictionary<ParameterlessCallType, TokenGenerator> _parameterlessCallTypeGenerators;

		// Token: 0x040008E1 RID: 2273
		protected static Dictionary<QualifiedJoinType, List<TokenGenerator>> _qualifiedJoinTypeGenerators;

		// Token: 0x040008E2 RID: 2274
		private static Dictionary<AssignmentKind, TSqlTokenType> _assignmentKindSymbols;

		// Token: 0x040008E3 RID: 2275
		protected static Dictionary<SqlDataTypeOption, TokenGenerator> _sqlDataTypeOptionGenerators;

		// Token: 0x040008E4 RID: 2276
		protected static Dictionary<UnaryExpressionType, List<TokenGenerator>> _unaryExpressionTypeGenerators;

		// Token: 0x040008E5 RID: 2277
		protected static Dictionary<XmlDataTypeOption, TokenGenerator> _xmlDataTypeOptionGenerators;

		// Token: 0x040008E6 RID: 2278
		protected static Dictionary<EndpointState, string> _endpointStateNames;

		// Token: 0x040008E7 RID: 2279
		protected static Dictionary<EndpointProtocol, string> _endpointProtocolNames;

		// Token: 0x040008E8 RID: 2280
		protected static Dictionary<EndpointType, string> _endpointTypeNames;

		// Token: 0x040008E9 RID: 2281
		protected static Dictionary<PayloadOptionKinds, TokenGenerator> _payloadOptionKindsGenerators;

		// Token: 0x040008EA RID: 2282
		protected static Dictionary<SimpleAlterFullTextIndexActionKind, List<TokenGenerator>> _simpleAlterFulltextIndexActionKindActions;

		// Token: 0x040008EB RID: 2283
		private static Dictionary<AlterTableAlterColumnOption, List<TokenGenerator>> _alterTableAlterColumnOptionNames;

		// Token: 0x040008EC RID: 2284
		protected static Dictionary<TableElementType, TokenGenerator> _tableElementTypeGenerators;

		// Token: 0x040008ED RID: 2285
		protected static Dictionary<AuthenticationTypes, TokenGenerator> _authenticationTypesGenerators;

		// Token: 0x040008EE RID: 2286
		protected static Dictionary<AuthenticationProtocol, string> _authenticationProtocolNames;

		// Token: 0x040008EF RID: 2287
		protected static Dictionary<DialogOptionKind, string> _dialogOptionNames;

		// Token: 0x040008F0 RID: 2288
		protected static Dictionary<BinaryQueryExpressionType, TokenGenerator> _binaryQueryExpressionTypeGenerators;

		// Token: 0x040008F1 RID: 2289
		private static Dictionary<CertificateOptionKinds, string> _certificateOptionNames;

		// Token: 0x040008F2 RID: 2290
		private static readonly Array _commandOptions;

		// Token: 0x040008F3 RID: 2291
		protected static Dictionary<BinaryExpressionType, List<TokenGenerator>> _binaryExpressionTypeGenerators;

		// Token: 0x040008F4 RID: 2292
		protected static Dictionary<BooleanComparisonType, List<TokenGenerator>> _booleanComparisonTypeGenerators;

		// Token: 0x040008F5 RID: 2293
		protected static Dictionary<BooleanBinaryExpressionType, List<TokenGenerator>> _booleanBinaryExpressionTypeGenerators;

		// Token: 0x040008F6 RID: 2294
		protected bool _generateSemiColon = true;

		// Token: 0x040008F7 RID: 2295
		protected static Dictionary<MessageSender, TokenGenerator> _messageSenderGenerators;

		// Token: 0x040008F8 RID: 2296
		protected static Dictionary<PermissionSetOption, string> _permissionSetOptionNames;

		// Token: 0x040008F9 RID: 2297
		private static Dictionary<AttachMode, TokenGenerator> _attachModeGenerators;

		// Token: 0x040008FA RID: 2298
		private static Dictionary<SecondaryXmlIndexType, string> _secondaryXmlIndexTypeNames;

		// Token: 0x040008FB RID: 2299
		private static Dictionary<CursorOptionKind, string> _cursorOptionsNames;

		// Token: 0x040008FC RID: 2300
		private static Dictionary<DbccCommand, string> _dbccCommandNames;

		// Token: 0x040008FD RID: 2301
		private static Dictionary<DbccOptionKind, TokenGenerator> _dbccOptionsGenerators;

		// Token: 0x040008FE RID: 2302
		private static Dictionary<DeviceType, TokenGenerator> _deviceTypeGenerators;

		// Token: 0x040008FF RID: 2303
		protected static Dictionary<DropClusteredConstraintOptionKind, List<TokenGenerator>> _dropClusteredConstraintOptionTypeGenerators;

		// Token: 0x04000900 RID: 2304
		protected static Dictionary<EndpointEncryptionSupport, TokenGenerator> _endpointEncryptionSupportGenerators;

		// Token: 0x04000901 RID: 2305
		private static Dictionary<ExecuteAsOption, TokenGenerator> _executeAsOptionGenerators;

		// Token: 0x04000902 RID: 2306
		private static Dictionary<FetchOrientation, string> _fetchOrientationNames;

		// Token: 0x04000903 RID: 2307
		protected static Dictionary<FullTextFunctionType, TokenGenerator> _fulltextFunctionTypeGenerators;

		// Token: 0x04000904 RID: 2308
		protected static Dictionary<FunctionOptionKind, List<TokenGenerator>> _functionOptionsGenerators;

		// Token: 0x04000905 RID: 2309
		protected static Dictionary<GeneralSetCommandType, TokenGenerator> _generalSetCommandTypeGenerators;

		// Token: 0x04000906 RID: 2310
		private static Dictionary<PrincipalOptionKind, string> _loginOptionsNames;

		// Token: 0x04000907 RID: 2311
		protected static Dictionary<EndpointProtocolOptions, string> _endpointProtocolOptionsNames;

		// Token: 0x04000908 RID: 2312
		private static Dictionary<MessageValidationMethod, string> _MessageValidationMethodNames;

		// Token: 0x04000909 RID: 2313
		protected static Dictionary<PortTypes, TokenGenerator> _portTypesGenerators;

		// Token: 0x0400090A RID: 2314
		protected static Dictionary<SetOptions, TokenGenerator> _setOptionsGenerators;

		// Token: 0x0400090B RID: 2315
		private static Dictionary<PrivilegeType80, TokenGenerator> _privilegeType80Generators;

		// Token: 0x0400090C RID: 2316
		protected static Dictionary<RaiseErrorOptions, TokenGenerator> _raiseErrorOptionsGenerators;

		// Token: 0x0400090D RID: 2317
		private static Dictionary<RestoreStatementKind, TokenGenerator> _restoreStatementKindGenerators;

		// Token: 0x0400090E RID: 2318
		protected static Dictionary<DatabaseMirroringEndpointRole, TokenGenerator> _databaseMirroringEndpointRoleGenerators;

		// Token: 0x0400090F RID: 2319
		private static Dictionary<RouteOptionKind, string> _RouteOptionTypeNames;

		// Token: 0x04000910 RID: 2320
		protected static Dictionary<SecurityObjectKind, List<TokenGenerator>> _securityObjectKindGenerators;

		// Token: 0x04000911 RID: 2321
		protected static Dictionary<SetOffsets, TokenGenerator> _setOffsetsGenerators;

		// Token: 0x04000912 RID: 2322
		protected static Dictionary<IsolationLevel, List<TokenGenerator>> _isolationLevelGenerators;

		// Token: 0x04000913 RID: 2323
		protected static Dictionary<SoapMethodAction, TokenGenerator> _soapMethodActionGenerators;

		// Token: 0x04000914 RID: 2324
		protected static Dictionary<SoapMethodSchemas, TokenGenerator> _soapMethodSchemasGenerators;

		// Token: 0x04000915 RID: 2325
		protected static Dictionary<SoapMethodFormat, string> _soapMethodFormatNames;

		// Token: 0x04000916 RID: 2326
		protected static Dictionary<SubqueryComparisonPredicateType, TokenGenerator> _subqueryComparisonPredicateTypeGenerators;

		// Token: 0x04000917 RID: 2327
		protected static Dictionary<TableSampleClauseOption, TokenGenerator> _tableSampleClauseOptionGenerators;

		// Token: 0x04000918 RID: 2328
		private static Dictionary<TriggerType, List<TokenGenerator>> _triggerTypeGenerators;

		// Token: 0x04000919 RID: 2329
		protected static Dictionary<UnqualifiedJoinType, List<TokenGenerator>> _unqualifiedJoinTypeGenerators;

		// Token: 0x0400091A RID: 2330
		protected static Dictionary<ViewOptionKind, string> _viewOptionTypeNames;

		// Token: 0x0400091B RID: 2331
		protected static Dictionary<WaitForOption, TokenGenerator> _waitForOptionGenerators;

		// Token: 0x0400091C RID: 2332
		protected static Dictionary<XmlForClauseOptions, List<TokenGenerator>> _xmlForClauseOptionsGenerators;

		// Token: 0x0400091D RID: 2333
		private static Dictionary<DeleteUpdateAction, List<TokenGenerator>> _deleteUpdateActionGenerators;

		// Token: 0x0400091E RID: 2334
		protected SqlScriptGeneratorOptions _options;

		// Token: 0x0400091F RID: 2335
		protected ScriptWriter _writer;

		// Token: 0x04000920 RID: 2336
		private Dictionary<TSqlFragment, Dictionary<string, AlignmentPoint>> _alignmentPointsForFragments;

		// Token: 0x020000D9 RID: 217
		internal class ListGenerationOption
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060013FE RID: 5118 RVA: 0x0008EB93 File Offset: 0x0008CD93
			// (set) Token: 0x060013FF RID: 5119 RVA: 0x0008EB9B File Offset: 0x0008CD9B
			public bool Parenthesised { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06001400 RID: 5120 RVA: 0x0008EBA4 File Offset: 0x0008CDA4
			// (set) Token: 0x06001401 RID: 5121 RVA: 0x0008EBAC File Offset: 0x0008CDAC
			public bool AlwaysGenerateParenthesis { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06001402 RID: 5122 RVA: 0x0008EBB5 File Offset: 0x0008CDB5
			// (set) Token: 0x06001403 RID: 5123 RVA: 0x0008EBBD File Offset: 0x0008CDBD
			public bool IndentParentheses { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x0008EBC6 File Offset: 0x0008CDC6
			// (set) Token: 0x06001405 RID: 5125 RVA: 0x0008EBCE File Offset: 0x0008CDCE
			public bool AlignParentheses { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06001406 RID: 5126 RVA: 0x0008EBD7 File Offset: 0x0008CDD7
			// (set) Token: 0x06001407 RID: 5127 RVA: 0x0008EBDF File Offset: 0x0008CDDF
			public bool NewLineBeforeOpenParenthesis { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x0008EBE8 File Offset: 0x0008CDE8
			// (set) Token: 0x06001409 RID: 5129 RVA: 0x0008EBF0 File Offset: 0x0008CDF0
			public bool NewLineAfterOpenParenthesis { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x0008EBF9 File Offset: 0x0008CDF9
			// (set) Token: 0x0600140B RID: 5131 RVA: 0x0008EC01 File Offset: 0x0008CE01
			public bool NewLineBeforeCloseParenthesis { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600140C RID: 5132 RVA: 0x0008EC0A File Offset: 0x0008CE0A
			// (set) Token: 0x0600140D RID: 5133 RVA: 0x0008EC12 File Offset: 0x0008CE12
			public SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType Separator { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600140E RID: 5134 RVA: 0x0008EC1B File Offset: 0x0008CE1B
			// (set) Token: 0x0600140F RID: 5135 RVA: 0x0008EC23 File Offset: 0x0008CE23
			public bool NewLineBeforeFirstItem { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06001410 RID: 5136 RVA: 0x0008EC2C File Offset: 0x0008CE2C
			// (set) Token: 0x06001411 RID: 5137 RVA: 0x0008EC34 File Offset: 0x0008CE34
			public bool NewLineBeforeItems { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06001412 RID: 5138 RVA: 0x0008EC3D File Offset: 0x0008CE3D
			// (set) Token: 0x06001413 RID: 5139 RVA: 0x0008EC45 File Offset: 0x0008CE45
			public int MultipleIndentItems { get; set; }

			// Token: 0x06001414 RID: 5140 RVA: 0x0008EC50 File Offset: 0x0008CE50
			public static SqlScriptGeneratorVisitor.ListGenerationOption CreateOptionFromFormattingConfig(SqlScriptGeneratorOptions formatting)
			{
				return new SqlScriptGeneratorVisitor.ListGenerationOption
				{
					Parenthesised = true,
					AlwaysGenerateParenthesis = true,
					NewLineBeforeOpenParenthesis = formatting.NewLineBeforeOpenParenthesisInMultilineList,
					NewLineAfterOpenParenthesis = true,
					IndentParentheses = false,
					NewLineBeforeCloseParenthesis = formatting.NewLineBeforeCloseParenthesisInMultilineList,
					AlignParentheses = false,
					NewLineBeforeItems = true,
					NewLineBeforeFirstItem = false,
					MultipleIndentItems = 1,
					Separator = SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType.Comma
				};
			}

			// Token: 0x04000921 RID: 2337
			public static SqlScriptGeneratorVisitor.ListGenerationOption MultipleLineSelectElementOption = new SqlScriptGeneratorVisitor.ListGenerationOption
			{
				Parenthesised = false,
				AlwaysGenerateParenthesis = false,
				IndentParentheses = false,
				AlignParentheses = false,
				Separator = SqlScriptGeneratorVisitor.ListGenerationOption.SeparatorType.Comma,
				NewLineBeforeFirstItem = false,
				NewLineBeforeItems = true,
				MultipleIndentItems = 0
			};

			// Token: 0x020000DA RID: 218
			internal enum SeparatorType
			{
				// Token: 0x0400092E RID: 2350
				Comma,
				// Token: 0x0400092F RID: 2351
				Dot,
				// Token: 0x04000930 RID: 2352
				Space
			}
		}
	}
}
