using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Scripting;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E5 RID: 229
	public static class JsonScripter
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00073DD0 File Offset: 0x00071FD0
		public static string ScriptAlter(NamedMetadataObject obj, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Alter, false);
			AlterJsonCommand alterJsonCommand = new AlterJsonCommand(obj);
			if (includeRestrictedInformation)
			{
				alterJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return alterJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00073E08 File Offset: 0x00072008
		public static string ScriptAlter(Database db, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			AlterJsonCommand alterJsonCommand = new AlterJsonCommand(db);
			if (includeRestrictedInformation)
			{
				alterJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return alterJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00073E3C File Offset: 0x0007203C
		public static string ScriptCreateOrReplace(NamedMetadataObject obj, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.CreateOrReplace, false);
			CreateOrReplaceJsonCommand createOrReplaceJsonCommand = new CreateOrReplaceJsonCommand(obj);
			if (includeRestrictedInformation)
			{
				createOrReplaceJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return createOrReplaceJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00073E74 File Offset: 0x00072074
		public static string ScriptCreateOrReplace(Database db, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			CreateOrReplaceJsonCommand createOrReplaceJsonCommand = new CreateOrReplaceJsonCommand(db);
			if (includeRestrictedInformation)
			{
				createOrReplaceJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return createOrReplaceJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00073EA8 File Offset: 0x000720A8
		public static string ScriptCreate(NamedMetadataObject obj, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Create, false);
			CreateJsonCommand createJsonCommand = new CreateJsonCommand(obj);
			if (includeRestrictedInformation)
			{
				createJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return createJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00073EE0 File Offset: 0x000720E0
		public static string ScriptCreate(Database db, bool includeRestrictedInformation = false)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			CreateJsonCommand createJsonCommand = new CreateJsonCommand(db);
			if (includeRestrictedInformation)
			{
				createJsonCommand.JsonSerializeOptions.IncludeRestrictedInformation = true;
			}
			return createJsonCommand.ScriptOut();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00073F14 File Offset: 0x00072114
		public static string ScriptDelete(NamedMetadataObject obj)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Delete, false);
			return new DeleteJsonCommand(obj).ScriptOut();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00073F2E File Offset: 0x0007212E
		public static string ScriptDelete(Database db)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			return new DeleteJsonCommand(db).ScriptOut();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00073F46 File Offset: 0x00072146
		public static string ScriptRefresh(NamedMetadataObject obj)
		{
			return JsonScripter.ScriptRefresh(obj, RefreshType.Full);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00073F4F File Offset: 0x0007214F
		public static string ScriptRefresh(NamedMetadataObject obj, RefreshType refreshType)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Refresh, false);
			return new RefreshJsonCommand(obj, refreshType).ScriptOut();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00073F6A File Offset: 0x0007216A
		public static string ScriptRefresh(NamedMetadataObject obj, RefreshType refreshType, RefreshPolicyBehavior behavior)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Refresh, true);
			return new RefreshJsonCommand(obj, refreshType, behavior).ScriptOut();
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00073F86 File Offset: 0x00072186
		[Obsolete("Deprecated. Use the ScriptRefresh(NamedMetadataObject, RefreshType, DateTime) overload that also accepts the refresh-type as parameter instead.")]
		public static string ScriptRefresh(NamedMetadataObject obj, DateTime effectiveDate)
		{
			return JsonScripter.ScriptRefresh(obj, RefreshType.Full, effectiveDate);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00073F90 File Offset: 0x00072190
		public static string ScriptRefresh(NamedMetadataObject obj, RefreshType refreshType, DateTime effectiveDate)
		{
			JsonScripter.ValidateObjectForScripting(obj, "obj", JsonCommandType.Refresh, true);
			if (!Utils.CanApplyRefreshPolicies(refreshType))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			return new RefreshJsonCommand(obj, refreshType, effectiveDate).ScriptOut();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00073FBF File Offset: 0x000721BF
		public static string ScriptRefresh(ICollection<NamedMetadataObject> objects, RefreshType refreshType)
		{
			JsonScripter.ValidateObjectsCollectionForScriptingRefresh(objects, "objects", false);
			return new RefreshJsonCommand(objects, refreshType).ScriptOut();
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00073FD9 File Offset: 0x000721D9
		public static string ScriptRefresh(ICollection<NamedMetadataObject> objects, RefreshType refreshType, RefreshPolicyBehavior behavior)
		{
			JsonScripter.ValidateObjectsCollectionForScriptingRefresh(objects, "objects", true);
			return new RefreshJsonCommand(objects, refreshType, behavior).ScriptOut();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00073FF4 File Offset: 0x000721F4
		public static string ScriptRefresh(ICollection<NamedMetadataObject> objects, RefreshType refreshType, DateTime effectiveDate)
		{
			JsonScripter.ValidateObjectsCollectionForScriptingRefresh(objects, "objects", true);
			if (!Utils.CanApplyRefreshPolicies(refreshType))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			return new RefreshJsonCommand(objects, refreshType, effectiveDate).ScriptOut();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00074022 File Offset: 0x00072222
		public static string ScriptRefresh(Database db)
		{
			return JsonScripter.ScriptRefresh(db, RefreshType.Full);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0007402B File Offset: 0x0007222B
		public static string ScriptRefresh(Database db, RefreshType refreshType)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			return new RefreshJsonCommand(db, refreshType).ScriptOut();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00074044 File Offset: 0x00072244
		public static string ScriptRefresh(Database db, RefreshType refreshType, RefreshPolicyBehavior behavior)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			return new RefreshJsonCommand(db, refreshType, behavior).ScriptOut();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0007405E File Offset: 0x0007225E
		[Obsolete("Deprecated. Use the ScriptRefresh(Database, RefreshType, DateTime) overload that also accepts the refresh-type as parameter instead.")]
		public static string ScriptRefresh(Database db, DateTime effectiveDate)
		{
			return JsonScripter.ScriptRefresh(db, RefreshType.Full, effectiveDate);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00074068 File Offset: 0x00072268
		public static string ScriptRefresh(Database db, RefreshType refreshType, DateTime effectiveDate)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			if (!Utils.CanApplyRefreshPolicies(refreshType))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType);
			}
			return new RefreshJsonCommand(db, refreshType, effectiveDate).ScriptOut();
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00074098 File Offset: 0x00072298
		public static string ScriptBackup(Database db, string filePath)
		{
			return JsonScripter.ScriptBackupImpl(db, filePath, null, null, null);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000740BF File Offset: 0x000722BF
		public static string ScriptBackup(Database db, string filePath, string password, bool allowOverwrite, bool applyCompression)
		{
			return JsonScripter.ScriptBackupImpl(db, filePath, password, new bool?(allowOverwrite), new bool?(applyCompression));
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000740D8 File Offset: 0x000722D8
		public static string ScriptRestore(string filePath, string databaseName)
		{
			return JsonScripter.ScriptRestoreImpl(filePath, databaseName, null, null, null, null, null, null, null);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0007411C File Offset: 0x0007231C
		public static string ScriptRestore(string filePath, string databaseName, bool allowOverwrite, string password, string dbStorageLocation, ReadWriteMode readWriteMode)
		{
			return JsonScripter.ScriptRestoreImpl(filePath, databaseName, new bool?(allowOverwrite), password, dbStorageLocation, new ReadWriteMode?(readWriteMode), null, null, null);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0007415C File Offset: 0x0007235C
		public static string ScriptRestore(string filePath, string databaseName, bool allowOverwrite, string password, string dbStorageLocation, ReadWriteMode readWriteMode, RestoreSecurity restoreSecurity)
		{
			return JsonScripter.ScriptRestoreImpl(filePath, databaseName, new bool?(allowOverwrite), password, dbStorageLocation, new ReadWriteMode?(readWriteMode), new RestoreSecurity?(restoreSecurity), null, null);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0007419C File Offset: 0x0007239C
		public static string ScriptRestore(string filePath, string databaseName, bool allowOverwrite, string password, string dbStorageLocation, ReadWriteMode readWriteMode, RestoreSecurity restoreSecurity, bool? ignoreIncompatibilities)
		{
			return JsonScripter.ScriptRestoreImpl(filePath, databaseName, new bool?(allowOverwrite), password, dbStorageLocation, new ReadWriteMode?(readWriteMode), new RestoreSecurity?(restoreSecurity), ignoreIncompatibilities, null);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000741D4 File Offset: 0x000723D4
		public static string ScriptRestore(RestoreInfo restoreInfo)
		{
			return JsonScripter.ScriptRestoreImpl(restoreInfo.File, restoreInfo.DatabaseName, new bool?(restoreInfo.AllowOverwrite), restoreInfo.Password, restoreInfo.DbStorageLocation, new ReadWriteMode?(restoreInfo.DatabaseReadWriteMode), new RestoreSecurity?(restoreInfo.Security), new bool?(restoreInfo.IgnoreIncompatibilities), new bool?(restoreInfo.ForceRestore));
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00074238 File Offset: 0x00072438
		public static string ScriptAttach(string folder)
		{
			return JsonScripter.ScriptAttachImpl(folder, null, null);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00074255 File Offset: 0x00072455
		public static string ScriptAttach(string folder, ReadWriteMode readWriteMode)
		{
			return JsonScripter.ScriptAttachImpl(folder, new ReadWriteMode?(readWriteMode), null);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00074264 File Offset: 0x00072464
		public static string ScriptAttach(string folder, ReadWriteMode readWriteMode, string password)
		{
			return JsonScripter.ScriptAttachImpl(folder, new ReadWriteMode?(readWriteMode), password);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00074273 File Offset: 0x00072473
		public static string ScriptDetach(Database db)
		{
			return JsonScripter.ScriptDetachImpl(db, null);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0007427C File Offset: 0x0007247C
		public static string ScriptDetach(Database db, string password)
		{
			return JsonScripter.ScriptDetachImpl(db, password);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00074288 File Offset: 0x00072488
		public static string ScriptSynchronize(string databaseName, string source)
		{
			return JsonScripter.ScriptSynchronizeImpl(databaseName, source, null, null);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000742AE File Offset: 0x000724AE
		public static string ScriptSynchronize(string databaseName, string source, SynchronizeSecurity synchronizeSecurity, bool applyCompression)
		{
			return JsonScripter.ScriptSynchronizeImpl(databaseName, source, new SynchronizeSecurity?(synchronizeSecurity), new bool?(applyCompression));
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000742C4 File Offset: 0x000724C4
		public static string ScriptMergePartitions(Partition targetPartition, IEnumerable<Partition> sourcePartitions)
		{
			JsonScripter.ValidateObjectForScripting(targetPartition, "targetPartition", JsonCommandType.MergePartitions, false);
			if (sourcePartitions == null)
			{
				throw new ArgumentNullException("sourcePartitions");
			}
			Table table = targetPartition.Table;
			foreach (Partition partition in sourcePartitions)
			{
				if (partition.Table != table)
				{
					throw new ArgumentException(TomSR.Exception_JsonScriptCannotScriptOutMergePartitionsNotSameTable(partition.Name));
				}
			}
			return new MergePartitionsJsonCommand(targetPartition, sourcePartitions).ScriptOut();
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00074350 File Offset: 0x00072550
		public static string ScriptExport(Database db)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			return new ExportJsonCommand(db).ScriptOut();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00074368 File Offset: 0x00072568
		[Obsolete("Deprecated. Use the ScriptApplyAutomaticAggregations(Database) overload that accepts the database as parameter instead.")]
		public static string ScriptApplyAutomaticAggregations(string databaseName)
		{
			if (string.IsNullOrEmpty(databaseName))
			{
				throw new ArgumentNullException("databaseName");
			}
			return new ApplyAutomaticAggregationsJsonCommand(databaseName).ScriptOut();
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00074388 File Offset: 0x00072588
		public static string ScriptApplyAutomaticAggregations(Database database)
		{
			JsonScripter.ValidateDatabaseForScripting(database, "database");
			return new ApplyAutomaticAggregationsJsonCommand(database).ScriptOut();
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000743A0 File Offset: 0x000725A0
		[Obsolete("Deprecated. Use the ScriptApplyAutomaticAggregations(Database, AutomaticAggregationOptions) overload that accepts the database as parameter instead.")]
		public static string ScriptApplyAutomaticAggregations(string databaseName, AutomaticAggregationOptions options)
		{
			if (string.IsNullOrEmpty(databaseName))
			{
				throw new ArgumentNullException("databaseName");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return new ApplyAutomaticAggregationsJsonCommand(databaseName, options).ScriptOut();
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000743CF File Offset: 0x000725CF
		public static string ScriptApplyAutomaticAggregations(Database database, AutomaticAggregationOptions options)
		{
			JsonScripter.ValidateDatabaseForScripting(database, "database");
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return new ApplyAutomaticAggregationsJsonCommand(database, options).ScriptOut();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000743F6 File Offset: 0x000725F6
		public static string GenerateSchema()
		{
			return JsonScripter.GenerateSchema(CompatibilityMode.PowerBI, 1000000);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00074404 File Offset: 0x00072604
		public static string GenerateSchema(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.Formatting = 1;
					jsonTextWriter.WriteStartObject();
					jsonTextWriter.WritePropertyName("description");
					jsonTextWriter.WriteValue("JSON script supported by Analysis Services");
					jsonTextWriter.WritePropertyName("anyOf");
					jsonTextWriter.WriteStartArray();
					foreach (JsonCommand jsonCommand in JsonCommandFactory.GetJsonSchemaSupportedCommands(mode, dbCompatibilityLevel))
					{
						jsonCommand.WriteSchema(jsonTextWriter, true, mode, dbCompatibilityLevel);
					}
					jsonTextWriter.WriteEndArray();
					jsonTextWriter.WriteEndObject();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000744DC File Offset: 0x000726DC
		private static string ScriptBackupImpl(Database db, string filePath, string password = null, bool? allowOverwrite = null, bool? applyCompression = null)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			return new BackupJsonCommand(db, filePath, password, allowOverwrite, applyCompression).ScriptOut();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0007450C File Offset: 0x0007270C
		private static string ScriptRestoreImpl(string filePath, string databaseName, bool? allowOverwrite = null, string password = null, string dbStorageLocation = null, ReadWriteMode? readWriteMode = null, RestoreSecurity? restoreSecurity = null, bool? ignoreIncompatibilities = null, bool? forceRestore = null)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (databaseName == null)
			{
				throw new ArgumentNullException("databaseName");
			}
			return new RestoreJsonCommand(filePath, databaseName, allowOverwrite, password, dbStorageLocation, readWriteMode, restoreSecurity, ignoreIncompatibilities, forceRestore).ScriptOut();
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0007454D File Offset: 0x0007274D
		private static string ScriptAttachImpl(string folder, ReadWriteMode? readWriteMode = null, string password = null)
		{
			if (string.IsNullOrEmpty(folder))
			{
				throw new ArgumentNullException("folder");
			}
			return new AttachJsonCommand(folder, readWriteMode, password).ScriptOut();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0007456F File Offset: 0x0007276F
		private static string ScriptDetachImpl(Database db, string password = null)
		{
			JsonScripter.ValidateDatabaseForScripting(db, "db");
			return new DetachJsonCommand(db, password).ScriptOut();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00074588 File Offset: 0x00072788
		private static string ScriptSynchronizeImpl(string databaseName, string source, SynchronizeSecurity? synchronizeSecurity = null, bool? applyCompression = null)
		{
			if (string.IsNullOrEmpty(databaseName))
			{
				throw new ArgumentNullException("databaseName");
			}
			if (string.IsNullOrEmpty(source))
			{
				throw new ArgumentNullException("source");
			}
			return new SynchronizeJsonCommand(databaseName, source, synchronizeSecurity, applyCompression).ScriptOut();
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000745BE File Offset: 0x000727BE
		private static void ValidateDatabaseForScripting(Database db, string dbParamName)
		{
			if (db == null)
			{
				throw new ArgumentNullException(dbParamName);
			}
			if (db.StorageEngineUsed != StorageEngineUsed.TabularMetadata)
			{
				throw new ArgumentException(TomSR.Exception_JsonScriptCannotScriptOutNonTMDatabase, dbParamName);
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000745E0 File Offset: 0x000727E0
		private static void ValidateObjectForScripting(NamedMetadataObject obj, string objParmaName, JsonCommandType commandType, bool isApplyPolicyRefresh = false)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(objParmaName);
			}
			if (obj.Model == null || obj.Model.Database == null)
			{
				throw new ArgumentException(TomSR.Exception_JsonScriptCannotScriptOutObjectWithoutDatabase, objParmaName);
			}
			switch (commandType)
			{
			case JsonCommandType.Alter:
				if (!ObjectTreeHelper.SupportsScriptOut(obj.ObjectType))
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandAlterNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
				}
				break;
			case JsonCommandType.CreateOrReplace:
				if (!ObjectTreeHelper.SupportsScriptOut(obj.ObjectType))
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandCreateOrReplaceNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
				}
				break;
			case JsonCommandType.Create:
				if (!ObjectTreeHelper.SupportsScriptOut(obj.ObjectType))
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandCreateNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
				}
				break;
			case JsonCommandType.Delete:
				if (!ObjectTreeHelper.SupportsScriptOut(obj.ObjectType))
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandDeleteNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
				}
				break;
			case JsonCommandType.Refresh:
				if (isApplyPolicyRefresh)
				{
					if (obj.ObjectType != ObjectType.Model && obj.ObjectType != ObjectType.Table)
					{
						throw new InvalidOperationException(TomSR.Exception_JsonCommandRefreshPolicyNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
					}
				}
				else if (!ObjectTreeHelper.SupportsScriptOut(obj.ObjectType) || !ObjectTreeHelper.SupportsRefresh(obj.ObjectType))
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandRefreshNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(obj.ObjectType)));
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00074728 File Offset: 0x00072928
		private static void ValidateObjectsCollectionForScriptingRefresh(ICollection<NamedMetadataObject> objects, string objectsParamName, bool isApplyPolicyRefresh = false)
		{
			if (objects == null)
			{
				throw new ArgumentNullException(objectsParamName);
			}
			if (objects.Count == 0)
			{
				throw new ArgumentException(TomSR.Exception_JsonCommandRefreshScriptOutObjectsNotSpecified, objectsParamName);
			}
			Database database = null;
			string text = string.Format("{0}.Item", objectsParamName);
			foreach (NamedMetadataObject namedMetadataObject in objects)
			{
				JsonScripter.ValidateObjectForScripting(namedMetadataObject, text, JsonCommandType.Refresh, isApplyPolicyRefresh);
				if (database == null)
				{
					database = namedMetadataObject.Model.Database;
				}
				else if (database != namedMetadataObject.Model.Database)
				{
					throw new InvalidOperationException(TomSR.Exception_JsonCommandScriptOutMultipleDbsNotSupported);
				}
			}
		}

		// Token: 0x040001CA RID: 458
		private const RefreshType DefaultRefreshType = RefreshType.Full;
	}
}
