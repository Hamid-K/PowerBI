using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010CB RID: 4299
	internal abstract class GenericDbEnvironment : DbEnvironment
	{
		// Token: 0x060070AF RID: 28847 RVA: 0x001836A4 File Offset: 0x001818A4
		public GenericDbEnvironment(IEngineHost host, ConnectionStringHandler connectionStringHandler, string dataSourceName, IResource resource, Value connectionProperties, Value options)
			: base(host, resource, dataSourceName, null, null, options, null, null)
		{
			GenericDbEnvironment <>4__this = this;
			this.connectionStringHandler = connectionStringHandler;
			this.connectionProperties = connectionProperties;
			this.connectionString = ConnectionStringHandler.HandleFormatExceptions<string>(dataSourceName, connectionProperties, delegate
			{
				string @string = connectionStringHandler.GetString(connectionProperties);
				if (host.QueryService<IExtensibilityService>() == null)
				{
					<>4__this.connectionStringHandler.ValidateSource(@string);
				}
				return @string;
			});
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x060070B0 RID: 28848 RVA: 0x000091AE File Offset: 0x000073AE
		public override HashSet<string> SearchableTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x060070B1 RID: 28849 RVA: 0x000091AE File Offset: 0x000073AE
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x060070B2 RID: 28850 RVA: 0x00087920 File Offset: 0x00085B20
		protected bool SqlCompatibleWindowsAuth
		{
			get
			{
				return base.UserOptions.GetBool("SqlCompatibleWindowsAuth", true);
			}
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x0018371F File Offset: 0x0018191F
		protected virtual DbConnectionStringBuilder NewConnectionStringBuilder()
		{
			return new DbConnectionStringBuilder(false);
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x00183727 File Offset: 0x00181927
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new GenericConnectionStringBuilder(base.Host, base.DataSourceNameString, this.connectionStringHandler, this.connectionString, this.Resource, this.SqlCompatibleWindowsAuth);
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x00183752 File Offset: 0x00181952
		protected override Exception ProcessFileLoadException(FileLoadException e)
		{
			return DataSourceException.NewFileLoadException<Message2>(base.Host, Strings.FileLoadingException(e.FileName, this.Resource.Kind), this.Resource, e);
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x00002105 File Offset: 0x00000305
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			return ResourceExceptionKind.None;
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x000091AE File Offset: 0x000073AE
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x000091AE File Offset: 0x000073AE
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x000091AE File Offset: 0x000073AE
		protected override SqlSettings LoadSqlSettings()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x000091AE File Offset: 0x000073AE
		public override DataTable LoadSchemas(DbConnection connection)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003E22 RID: 15906
		public static readonly OptionRecordDefinition NativeQueryOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL")
		});

		// Token: 0x04003E23 RID: 15907
		protected readonly ConnectionStringHandler connectionStringHandler;

		// Token: 0x04003E24 RID: 15908
		protected readonly Value connectionProperties;

		// Token: 0x04003E25 RID: 15909
		protected readonly string connectionString;
	}
}
