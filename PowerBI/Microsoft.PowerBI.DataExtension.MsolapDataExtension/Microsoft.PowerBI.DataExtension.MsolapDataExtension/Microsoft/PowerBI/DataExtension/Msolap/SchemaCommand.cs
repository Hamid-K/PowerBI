using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x02000010 RID: 16
	internal sealed class SchemaCommand : IDbSchemaCommand, IDisposable
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002F50 File Offset: 0x00001150
		internal SchemaCommand(Connection connection, SchemaCommand command, IPrivateInformationService piiService)
		{
			this._connection = connection;
			this._command = command;
			this._piiService = piiService;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F70 File Offset: 0x00001170
		public void SetTelemetryIds(string clientActivityId, string currentActivityId, string rootActivityId)
		{
			this._clientActivityId = clientActivityId;
			this._currentActivityId = currentActivityId;
			this._rootActivityId = rootActivityId;
			this._command.AddProperty(SchemaCommandProperties.ActivityId, this._clientActivityId);
			this._command.AddProperty(SchemaCommandProperties.CurrentActivityId, this._currentActivityId);
			this._command.AddProperty(SchemaCommandProperties.RequestId, this._rootActivityId);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002FC8 File Offset: 0x000011C8
		public void SetTimeout(int timeout)
		{
			this._command.AddProperty(SchemaCommandProperties.CommandTimeout, timeout);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FDC File Offset: 0x000011DC
		public void SetApplicationContext(string applicationContext)
		{
			if (applicationContext == null)
			{
				return;
			}
			this._command.AddProperty(SchemaCommandProperties.ApplicationContext, applicationContext);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002FF0 File Offset: 0x000011F0
		public string GetModelMetadata(string catalogName, string perspectiveName, string supportedVersion, TranslationsBehavior translationsBehavior)
		{
			string text = null;
			try
			{
				int num = (int)this._command.GetSchemasRestrictionsCount(SchemaGuid.Csdl);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (num == 2)
				{
					supportedVersion = null;
					if (perspectiveName != null)
					{
						this.ValidateBaseCubeNameForAS2012RTM(catalogName, perspectiveName);
						perspectiveName = null;
					}
				}
				dictionary[SchemaCommandUtils.RestrictionParamNames.CATALOGNAME] = catalogName;
				dictionary[SchemaCommandUtils.RestrictionParamNames.PERSPECTIVENAME] = perspectiveName;
				dictionary[SchemaCommandUtils.RestrictionParamNames.VERSION] = supportedVersion;
				if (translationsBehavior == TranslationsBehavior.Ignore)
				{
					dictionary[SchemaCommandUtils.RestrictionParamNames.IGNORETRANSLATIONS] = true;
				}
				else if (translationsBehavior == TranslationsBehavior.PrintAll)
				{
					dictionary[SchemaCommandUtils.RestrictionParamNames.PRINTALLTRANSLATIONS] = true;
				}
				SchemaCommandUtils.DiscoverSchemaParams msolapDiscoverSchemaParams = SchemaCommandUtils.GetMsolapDiscoverSchemaParams(SchemaCommandUtils.SchemaNames.DISCOVER_CSDL_METADATA, dictionary, num);
				text = this._command.GetModelMetadata(msolapDiscoverSchemaParams.restrictions);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to get CSDL.", new object[0]);
			}
			return text;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000030C8 File Offset: 0x000012C8
		public DataSet GetSchemaDataSet(string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			string text = "Failed to get " + schemaName + ".";
			DataSet schemaDataSet;
			try
			{
				int num = (int)this._command.GetSchemasRestrictionsCount(SchemaCommandUtils.GetSchemaGuid(schemaName));
				SchemaCommandUtils.DiscoverSchemaParams msolapDiscoverSchemaParams = SchemaCommandUtils.GetMsolapDiscoverSchemaParams(schemaName, restrictions, num);
				schemaDataSet = this.GetSchemaDataSet(msolapDiscoverSchemaParams.schemaGuid, msolapDiscoverSchemaParams.restrictions, text);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, text, new object[0]);
			}
			return schemaDataSet;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000313C File Offset: 0x0000133C
		public DataSet GetDependencyGraph(string databaseName, string depedencyObjectType, string depedencyQuery)
		{
			object[] array = new object[]
			{
				databaseName,
				null,
				string.Empty
			};
			return this.GetSchemaDataSet(SchemaGuid.DependencyGraph, array, "Failed to get the dependency graph.");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003171 File Offset: 0x00001371
		public IReadOnlyList<DataSet> ExecuteBatchSchemaCommand(string commandText)
		{
			throw new NotImplementedException("ExecuteBatchSchemaCommand not implemented.");
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000317D File Offset: 0x0000137D
		public void SaveDatabase(string dbName)
		{
			throw new NotImplementedException("SaveDatabase not implemented.");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003189 File Offset: 0x00001389
		public DataSet GetAnnotations(string dbName)
		{
			throw new NotImplementedException("GetAnnotations not implemented.");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003198 File Offset: 0x00001398
		internal string GetBaseCubeName(string catalogName, string perspectiveName)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary[SchemaCommandUtils.RestrictionParamNames.CATALOGNAME] = catalogName;
			dictionary[SchemaCommandUtils.RestrictionParamNames.CUBENAME] = perspectiveName;
			DataSet schemaDataSet = this.GetSchemaDataSet(SchemaCommandUtils.SchemaNames.MDSCHEMA_CUBES, dictionary);
			if (schemaDataSet.Tables.Count == 0 || schemaDataSet.Tables[0].Rows.Count == 0)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException("MdSchemaCubesPerspectiveRestrictionInvalidError", "MDSCHEMA_CUBES returned a result with no cubes.", ErrorSource.User, null);
			}
			object obj = schemaDataSet.Tables[0].Rows[0]["BASE_CUBE_NAME"];
			if (!(obj is DBNull))
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000323C File Offset: 0x0000143C
		private void ValidateBaseCubeNameForAS2012RTM(string catalogName, string perspectiveName)
		{
			using (IDbSchemaCommand dbSchemaCommand = this._connection.CreateSchemaCommand())
			{
				if (this._clientActivityId != null || this._currentActivityId != null || this._rootActivityId != null)
				{
					dbSchemaCommand.SetTelemetryIds(this._clientActivityId, this._currentActivityId, this._rootActivityId);
				}
				if (((SchemaCommand)dbSchemaCommand).GetBaseCubeName(catalogName, perspectiveName) != null)
				{
					throw DataExtensionErrorUtils.CreateDataExtensionException("InvalidNonDefaultPerspectiveRestrictionError", "The datasource specifies the non-default perspective, but this version of Analysis Services does no support metadata for non-default perspectives.Choose the default perspective instead.", ErrorSource.User, null);
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000032C4 File Offset: 0x000014C4
		private DataSet GetSchemaDataSet(Guid schemaGuid, object[] restrictions, string failureMessage)
		{
			DataSet dataSet;
			try
			{
				using (DataReader dataReader = this._command.ExecuteReader(schemaGuid, restrictions))
				{
					dataSet = dataReader.ReadToDataSet("rowsetTable", this._piiService);
				}
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, failureMessage, new object[0]);
			}
			return dataSet;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003330 File Offset: 0x00001530
		public void Dispose()
		{
			this._command.Dispose();
		}

		// Token: 0x0400004A RID: 74
		private readonly SchemaCommand _command;

		// Token: 0x0400004B RID: 75
		private readonly IPrivateInformationService _piiService;

		// Token: 0x0400004C RID: 76
		private readonly Connection _connection;

		// Token: 0x0400004D RID: 77
		private string _clientActivityId;

		// Token: 0x0400004E RID: 78
		private string _currentActivityId;

		// Token: 0x0400004F RID: 79
		private string _rootActivityId;
	}
}
