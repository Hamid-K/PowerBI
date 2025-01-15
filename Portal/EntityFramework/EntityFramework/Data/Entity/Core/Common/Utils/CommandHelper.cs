using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F3 RID: 1523
	internal static class CommandHelper
	{
		// Token: 0x06004A7E RID: 19070 RVA: 0x00108124 File Offset: 0x00106324
		internal static void ConsumeReader(DbDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
			{
				while (reader.NextResult())
				{
				}
			}
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x0010813C File Offset: 0x0010633C
		internal static async Task ConsumeReaderAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			if (reader != null && !reader.IsClosed)
			{
				cancellationToken.ThrowIfCancellationRequested();
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x0010818C File Offset: 0x0010638C
		internal static void ParseFunctionImportCommandText(string commandText, string defaultContainerName, out string containerName, out string functionImportName)
		{
			string[] array = commandText.Split(new char[] { '.' });
			containerName = null;
			functionImportName = null;
			if (2 == array.Length)
			{
				containerName = array[0].Trim();
				functionImportName = array[1].Trim();
			}
			else if (1 == array.Length && defaultContainerName != null)
			{
				containerName = defaultContainerName;
				functionImportName = array[0].Trim();
			}
			if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(functionImportName))
			{
				throw new InvalidOperationException(Strings.EntityClient_InvalidStoredProcedureCommandText);
			}
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00108200 File Offset: 0x00106400
		internal static void SetStoreProviderCommandState(EntityCommand entityCommand, EntityTransaction entityTransaction, DbCommand storeProviderCommand)
		{
			storeProviderCommand.CommandTimeout = entityCommand.CommandTimeout;
			storeProviderCommand.Connection = entityCommand.Connection.StoreConnection;
			storeProviderCommand.Transaction = ((entityTransaction != null) ? entityTransaction.StoreTransaction : null);
			storeProviderCommand.UpdatedRowSource = entityCommand.UpdatedRowSource;
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00108240 File Offset: 0x00106440
		internal static void SetEntityParameterValues(EntityCommand entityCommand, DbCommand storeProviderCommand, EntityConnection connection)
		{
			foreach (object obj in storeProviderCommand.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				ParameterDirection direction = dbParameter.Direction;
				if ((direction & ParameterDirection.Output) != (ParameterDirection)0)
				{
					int num = entityCommand.Parameters.IndexOf(dbParameter.ParameterName);
					if (0 <= num)
					{
						EntityParameter entityParameter = entityCommand.Parameters[num];
						object obj2 = dbParameter.Value;
						TypeUsage typeUsage = entityParameter.GetTypeUsage();
						if (Helper.IsSpatialType(typeUsage))
						{
							obj2 = CommandHelper.GetSpatialValueFromProviderValue(obj2, (PrimitiveType)typeUsage.EdmType, connection);
						}
						entityParameter.Value = obj2;
					}
				}
			}
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x001082FC File Offset: 0x001064FC
		private static object GetSpatialValueFromProviderValue(object spatialValue, PrimitiveType parameterType, EntityConnection connection)
		{
			DbSpatialServices spatialServices = DbProviderServices.GetSpatialServices(DbConfiguration.DependencyResolver, connection);
			if (Helper.IsGeographicType(parameterType))
			{
				return spatialServices.GeographyFromProviderValue(spatialValue);
			}
			return spatialServices.GeometryFromProviderValue(spatialValue);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0010832C File Offset: 0x0010652C
		internal static EdmFunction FindFunctionImport(MetadataWorkspace workspace, string containerName, string functionImportName)
		{
			EntityContainer entityContainer;
			if (!workspace.TryGetEntityContainer(containerName, DataSpace.CSpace, out entityContainer))
			{
				throw new InvalidOperationException(Strings.EntityClient_UnableToFindFunctionImportContainer(containerName));
			}
			EdmFunction edmFunction = null;
			foreach (EdmFunction edmFunction2 in entityContainer.FunctionImports)
			{
				if (edmFunction2.Name == functionImportName)
				{
					edmFunction = edmFunction2;
					break;
				}
			}
			if (edmFunction == null)
			{
				throw new InvalidOperationException(Strings.EntityClient_UnableToFindFunctionImport(containerName, functionImportName));
			}
			if (edmFunction.IsComposableAttribute)
			{
				throw new InvalidOperationException(Strings.EntityClient_FunctionImportMustBeNonComposable(containerName + "." + functionImportName));
			}
			return edmFunction;
		}
	}
}
