using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Data
{
	// Token: 0x02000014 RID: 20
	internal class StringsHelper : Strings
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0000B1D7 File Offset: 0x000093D7
		internal StringsHelper()
		{
			this._resources = new ResourceManager("Microsoft.Data.SqlClient.Resources.Strings", base.GetType().Assembly);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000B1FC File Offset: 0x000093FC
		private static StringsHelper GetLoader()
		{
			if (StringsHelper.s_loader == null)
			{
				StringsHelper stringsHelper = new StringsHelper();
				Interlocked.CompareExchange<StringsHelper>(ref StringsHelper.s_loader, stringsHelper, null);
			}
			return StringsHelper.s_loader;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000B228 File Offset: 0x00009428
		public static string GetResourceString(string res)
		{
			StringsHelper loader = StringsHelper.GetLoader();
			if (loader == null)
			{
				return null;
			}
			string @string = loader._resources.GetString(res, Strings.Culture);
			return @string ?? res;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000B258 File Offset: 0x00009458
		public static string GetString(string res, params object[] args)
		{
			res = StringsHelper.GetResourceString(res);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, res, args);
			}
			return res;
		}

		// Token: 0x0400001D RID: 29
		private static StringsHelper s_loader;

		// Token: 0x0400001E RID: 30
		private readonly ResourceManager _resources;

		// Token: 0x0200018F RID: 399
		internal class ResourceNames
		{
			// Token: 0x04000C65 RID: 3173
			internal const string ADP_Ascending = "Ascending";

			// Token: 0x04000C66 RID: 3174
			internal const string ADP_CollectionIndexInt32 = "Invalid index {0} for this {1} with Count={2}.";

			// Token: 0x04000C67 RID: 3175
			internal const string ADP_CollectionIndexString = "A {0} with {1} '{2}' is not contained by this {3}.";

			// Token: 0x04000C68 RID: 3176
			internal const string ADP_CollectionInvalidType = "The {0} only accepts non-null {1} type objects, not {2} objects.";

			// Token: 0x04000C69 RID: 3177
			internal const string ADP_CollectionIsNotParent = "The {0} is already contained by another {1}.";

			// Token: 0x04000C6A RID: 3178
			internal const string ADP_CollectionIsParent = "The {0} with is already contained by this {1}.";

			// Token: 0x04000C6B RID: 3179
			internal const string ADP_CollectionNullValue = "The {0} only accepts non-null {1} type objects.";

			// Token: 0x04000C6C RID: 3180
			internal const string ADP_CollectionRemoveInvalidObject = "Attempted to remove an {0} that is not contained by this {1}.";

			// Token: 0x04000C6D RID: 3181
			internal const string ADP_CollectionUniqueValue = "The {0}.{1} is required to be unique, '{2}' already exists in the collection.";

			// Token: 0x04000C6E RID: 3182
			internal const string ADP_ConnectionAlreadyOpen = "The connection was not closed. {0}";

			// Token: 0x04000C6F RID: 3183
			internal const string ADP_ConnectionStateMsg_Closed = "The connection's current state is closed.";

			// Token: 0x04000C70 RID: 3184
			internal const string ADP_ConnectionStateMsg_Connecting = "The connection's current state is connecting.";

			// Token: 0x04000C71 RID: 3185
			internal const string ADP_ConnectionStateMsg_Open = "The connection's current state is open.";

			// Token: 0x04000C72 RID: 3186
			internal const string ADP_ConnectionStateMsg_OpenExecuting = "The connection's current state is executing.";

			// Token: 0x04000C73 RID: 3187
			internal const string ADP_ConnectionStateMsg_OpenFetching = "The connection's current state is fetching.";

			// Token: 0x04000C74 RID: 3188
			internal const string ADP_ConnectionStateMsg = "The connection's current state: {0}.";

			// Token: 0x04000C75 RID: 3189
			internal const string ADP_ConnectionStringSyntax = "Format of the initialization string does not conform to specification starting at index {0}.";

			// Token: 0x04000C76 RID: 3190
			internal const string ADP_DataReaderClosed = "Invalid attempt to call {0} when reader is closed.";

			// Token: 0x04000C77 RID: 3191
			internal const string ADP_DelegatedTransactionPresent = "Cannot enlist in the transaction because the connection is the primary connection for a delegated or promoted transaction.";

			// Token: 0x04000C78 RID: 3192
			internal const string ADP_Descending = "Descending";

			// Token: 0x04000C79 RID: 3193
			internal const string ADP_EmptyString = "Expecting non-empty string for '{0}' parameter.";

			// Token: 0x04000C7A RID: 3194
			internal const string ADP_InternalConnectionError = "Internal DbConnection Error: {0}";

			// Token: 0x04000C7B RID: 3195
			internal const string ADP_InvalidDataDirectory = "The DataDirectory substitute is not a string.";

			// Token: 0x04000C7C RID: 3196
			internal const string ADP_InvalidEnumerationValue = "The {0} enumeration value, {1}, is invalid.";

			// Token: 0x04000C7D RID: 3197
			internal const string ADP_InvalidKey = "Invalid keyword, contain one or more of 'no characters', 'control characters', 'leading or trailing whitespace' or 'leading semicolons'.";

			// Token: 0x04000C7E RID: 3198
			internal const string ADP_InvalidOffsetValue = "Invalid parameter Offset value '{0}'. The value must be greater than or equal to 0.";

			// Token: 0x04000C7F RID: 3199
			internal const string ADP_InvalidValue = "The value contains embedded nulls (\\u0000).";

			// Token: 0x04000C80 RID: 3200
			internal const string ADP_InvalidXMLBadVersion = "Invalid Xml; can only parse elements of version one.";

			// Token: 0x04000C81 RID: 3201
			internal const string ADP_NoConnectionString = "The ConnectionString property has not been initialized.";

			// Token: 0x04000C82 RID: 3202
			internal const string ADP_NonCLSException = "A Non CLS Exception was caught.";

			// Token: 0x04000C83 RID: 3203
			internal const string ADP_NotAPermissionElement = "Given security element is not a permission element.";

			// Token: 0x04000C84 RID: 3204
			internal const string ADP_OpenConnectionPropertySet = "Not allowed to change the '{0}' property. {1}";

			// Token: 0x04000C85 RID: 3205
			internal const string ADP_PendingAsyncOperation = "Can not start another operation while there is an asynchronous operation pending.";

			// Token: 0x04000C86 RID: 3206
			internal const string ADP_PermissionTypeMismatch = "Type mismatch.";

			// Token: 0x04000C87 RID: 3207
			internal const string ADP_PooledOpenTimeout = "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.";

			// Token: 0x04000C88 RID: 3208
			internal const string ADP_NonPooledOpenTimeout = "Timeout attempting to open the connection.  The time period elapsed prior to attempting to open the connection has been exceeded.  This may have occurred because of too many simultaneous non-pooled connection attempts.";

			// Token: 0x04000C89 RID: 3209
			internal const string ADP_InvalidMixedUsageOfSecureAndClearCredential = "Cannot use Credential with UserID, UID, Password, or PWD connection string keywords.";

			// Token: 0x04000C8A RID: 3210
			internal const string ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity = "Cannot use Credential with Integrated Security connection string keyword.";

			// Token: 0x04000C8B RID: 3211
			internal const string ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection = "Cannot use Credential with Context Connection keyword.";

			// Token: 0x04000C8C RID: 3212
			internal const string ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword = "Cannot set the AccessToken property if 'UserID', 'UID', 'Password', or 'PWD' has been specified in connection string.";

			// Token: 0x04000C8D RID: 3213
			internal const string ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity = "Cannot set the AccessToken property if the 'Integrated Security' connection string keyword has been set to 'true' or 'SSPI'.";

			// Token: 0x04000C8E RID: 3214
			internal const string ADP_InvalidMixedUsageOfAccessTokenAndContextConnection = "Cannot set the AccessToken property with the 'Context Connection' keyword.";

			// Token: 0x04000C8F RID: 3215
			internal const string ADP_InvalidMixedUsageOfAccessTokenAndCredential = "Cannot set the AccessToken property if the Credential property is already set.";

			// Token: 0x04000C90 RID: 3216
			internal const string ADP_InvalidMixedUsageOfCredentialAndAccessToken = "Cannot set the Credential property if the AccessToken property is already set.";

			// Token: 0x04000C91 RID: 3217
			internal const string ADP_InvalidMixedUsageOfAccessTokenAndAuthentication = "Cannot set the AccessToken property if 'Authentication' has been specified in the connection string.";

			// Token: 0x04000C92 RID: 3218
			internal const string ADP_MustBeReadOnly = "{0} must be marked as read only.";

			// Token: 0x04000C93 RID: 3219
			internal const string DataCategory_Data = "Data";

			// Token: 0x04000C94 RID: 3220
			internal const string DataCategory_StateChange = "StateChange";

			// Token: 0x04000C95 RID: 3221
			internal const string DataCategory_Update = "Update";

			// Token: 0x04000C96 RID: 3222
			internal const string DbCommand_CommandTimeout = "Time to wait for command to execute.";

			// Token: 0x04000C97 RID: 3223
			internal const string DbConnection_State = "The ConnectionState indicating whether the connection is open or closed.";

			// Token: 0x04000C98 RID: 3224
			internal const string DbConnection_StateChange = "Event triggered when the connection changes state.";

			// Token: 0x04000C99 RID: 3225
			internal const string DbParameter_DbType = "The parameter generic type.";

			// Token: 0x04000C9A RID: 3226
			internal const string DbParameter_Direction = "Input, output, or bidirectional parameter.";

			// Token: 0x04000C9B RID: 3227
			internal const string DbParameter_IsNullable = "a design-time property used for strongly typed code-generation.";

			// Token: 0x04000C9C RID: 3228
			internal const string DbParameter_Offset = "Offset in variable length data types.";

			// Token: 0x04000C9D RID: 3229
			internal const string DbParameter_ParameterName = "Name of the parameter.";

			// Token: 0x04000C9E RID: 3230
			internal const string DbParameter_Size = "Size of variable length data types (string & arrays).";

			// Token: 0x04000C9F RID: 3231
			internal const string DbParameter_SourceColumn = "When used by a DataAdapter.Update, the source column name that is used to find the DataSetColumn name in the ColumnMappings. This is to copy a value between the parameter and a data row.";

			// Token: 0x04000CA0 RID: 3232
			internal const string DbParameter_SourceVersion = "When used by a DataAdapter.Update (UpdateCommand only), the version of the DataRow value that is used to update the data source.";

			// Token: 0x04000CA1 RID: 3233
			internal const string DbParameter_SourceColumnNullMapping = "When used by DataAdapter.Update, the parameter value is changed from DBNull.Value into (Int32)1 or (Int32)0 if non-null.";

			// Token: 0x04000CA2 RID: 3234
			internal const string DbParameter_Value = "Value of the parameter.";

			// Token: 0x04000CA3 RID: 3235
			internal const string MDF_QueryFailed = "Unable to build the '{0}' collection because execution of the SQL query failed. See the inner exception for details.";

			// Token: 0x04000CA4 RID: 3236
			internal const string MDF_TooManyRestrictions = "More restrictions were provided than the requested schema ('{0}') supports.";

			// Token: 0x04000CA5 RID: 3237
			internal const string MDF_InvalidRestrictionValue = "'{2}' is not a valid value for the '{1}' restriction of the '{0}' schema collection.";

			// Token: 0x04000CA6 RID: 3238
			internal const string MDF_UndefinedCollection = "The requested collection ({0}) is not defined.";

			// Token: 0x04000CA7 RID: 3239
			internal const string MDF_UndefinedPopulationMechanism = "The population mechanism '{0}' is not defined.";

			// Token: 0x04000CA8 RID: 3240
			internal const string MDF_UnsupportedVersion = "The requested collection ({0}) is not supported by this version of the provider.";

			// Token: 0x04000CA9 RID: 3241
			internal const string MDF_MissingDataSourceInformationColumn = "One of the required DataSourceInformation tables columns is missing.";

			// Token: 0x04000CAA RID: 3242
			internal const string MDF_IncorrectNumberOfDataSourceInformationRows = "The DataSourceInformation table must contain exactly one row.";

			// Token: 0x04000CAB RID: 3243
			internal const string MDF_MissingRestrictionColumn = "One or more of the required columns of the restrictions collection is missing.";

			// Token: 0x04000CAC RID: 3244
			internal const string MDF_MissingRestrictionRow = "A restriction exists for which there is no matching row in the restrictions collection.";

			// Token: 0x04000CAD RID: 3245
			internal const string MDF_NoColumns = "The schema table contains no columns.";

			// Token: 0x04000CAE RID: 3246
			internal const string MDF_UnableToBuildCollection = "Unable to build schema collection '{0}';";

			// Token: 0x04000CAF RID: 3247
			internal const string MDF_AmbiguousCollectionName = "The collection name '{0}' matches at least two collections with the same name but with different case, but does not match any of them exactly.";

			// Token: 0x04000CB0 RID: 3248
			internal const string MDF_CollectionNameISNotUnique = "There are multiple collections named '{0}'.";

			// Token: 0x04000CB1 RID: 3249
			internal const string MDF_DataTableDoesNotExist = "The collection '{0}' is missing from the metadata XML.";

			// Token: 0x04000CB2 RID: 3250
			internal const string MDF_InvalidXml = "The metadata XML is invalid.";

			// Token: 0x04000CB3 RID: 3251
			internal const string MDF_InvalidXmlMissingColumn = "The metadata XML is invalid. The {0} collection must contain a {1} column and it must be a string column.";

			// Token: 0x04000CB4 RID: 3252
			internal const string MDF_InvalidXmlInvalidValue = "The metadata XML is invalid. The {1} column of the {0} collection must contain a non-empty string.";

			// Token: 0x04000CB5 RID: 3253
			internal const string DataCategory_Action = "Action";

			// Token: 0x04000CB6 RID: 3254
			internal const string DataCategory_Behavior = "Behavior";

			// Token: 0x04000CB7 RID: 3255
			internal const string DataCategory_Fill = "Fill";

			// Token: 0x04000CB8 RID: 3256
			internal const string DataCategory_InfoMessage = "InfoMessage";

			// Token: 0x04000CB9 RID: 3257
			internal const string DataCategory_Mapping = "Mapping";

			// Token: 0x04000CBA RID: 3258
			internal const string DataCategory_StatementCompleted = "StatementCompleted";

			// Token: 0x04000CBB RID: 3259
			internal const string DataCategory_Udt = "UDT";

			// Token: 0x04000CBC RID: 3260
			internal const string DataCategory_Notification = "Notification";

			// Token: 0x04000CBD RID: 3261
			internal const string DataCategory_Schema = "Schema";

			// Token: 0x04000CBE RID: 3262
			internal const string DataCategory_Xml = "XML";

			// Token: 0x04000CBF RID: 3263
			internal const string DataCategory_Advanced = "Advanced";

			// Token: 0x04000CC0 RID: 3264
			internal const string DataCategory_Context = "Context";

			// Token: 0x04000CC1 RID: 3265
			internal const string DataCategory_Initialization = "Initialization";

			// Token: 0x04000CC2 RID: 3266
			internal const string DataCategory_Pooling = "Pooling";

			// Token: 0x04000CC3 RID: 3267
			internal const string DataCategory_NamedConnectionString = "Named ConnectionString";

			// Token: 0x04000CC4 RID: 3268
			internal const string DataCategory_Security = "Security";

			// Token: 0x04000CC5 RID: 3269
			internal const string DataCategory_Source = "Source";

			// Token: 0x04000CC6 RID: 3270
			internal const string DataCategory_Replication = "Replication";

			// Token: 0x04000CC7 RID: 3271
			internal const string DataCategory_ConnectionResilency = "Connection Resiliency";

			// Token: 0x04000CC8 RID: 3272
			internal const string ExtendedPropertiesDescr = "The collection that holds custom user information.";

			// Token: 0x04000CC9 RID: 3273
			internal const string DataSetCaseSensitiveDescr = "Indicates whether comparing strings within the DataSet is case sensitive.";

			// Token: 0x04000CCA RID: 3274
			internal const string DataSetDataSetNameDescr = "The name of this DataSet.";

			// Token: 0x04000CCB RID: 3275
			internal const string DataSetDefaultViewDescr = "Indicates a custom \"view\" of the data contained by the DataSet. This view allows filtering, searching, and navigating through the custom data view.";

			// Token: 0x04000CCC RID: 3276
			internal const string DataSetEnforceConstraintsDescr = "Indicates whether constraint rules are to be followed.";

			// Token: 0x04000CCD RID: 3277
			internal const string DataSetHasErrorsDescr = "Indicates that the DataSet has errors.";

			// Token: 0x04000CCE RID: 3278
			internal const string DataSetLocaleDescr = "Indicates a locale under which to compare strings within the DataSet.";

			// Token: 0x04000CCF RID: 3279
			internal const string DataSetNamespaceDescr = "Indicates the XML uri namespace for the root element pointed at by this DataSet.";

			// Token: 0x04000CD0 RID: 3280
			internal const string DataSetPrefixDescr = "Indicates the prefix of the namespace used for this DataSet.";

			// Token: 0x04000CD1 RID: 3281
			internal const string DataSetRelationsDescr = "The collection that holds the relations for this DataSet.";

			// Token: 0x04000CD2 RID: 3282
			internal const string DataSetTablesDescr = "The collection that holds the tables for this DataSet.";

			// Token: 0x04000CD3 RID: 3283
			internal const string DataSetMergeFailedDescr = "Occurs when it is not possible to merge schemas for two tables with the same name.";

			// Token: 0x04000CD4 RID: 3284
			internal const string DataSetInitializedDescr = "Occurs after Initialization is finished.";

			// Token: 0x04000CD5 RID: 3285
			internal const string DataSetDescr = "Represents an in-memory cache of data.";

			// Token: 0x04000CD6 RID: 3286
			internal const string DataTableCaseSensitiveDescr = "Indicates whether comparing strings within the table is case sensitive.";

			// Token: 0x04000CD7 RID: 3287
			internal const string DataTableChildRelationsDescr = "Returns the child relations for this table.";

			// Token: 0x04000CD8 RID: 3288
			internal const string DataTableColumnsDescr = "The collection that holds the columns for this table.";

			// Token: 0x04000CD9 RID: 3289
			internal const string DataTableConstraintsDescr = "The collection that holds the constraints for this table.";

			// Token: 0x04000CDA RID: 3290
			internal const string DataTableDataSetDescr = "Indicates the DataSet to which this table belongs.";

			// Token: 0x04000CDB RID: 3291
			internal const string DataTableDefaultViewDescr = "This is the default DataView for the table.";

			// Token: 0x04000CDC RID: 3292
			internal const string DataTableDisplayExpressionDescr = "The expression used to compute the data-bound value of this row.";

			// Token: 0x04000CDD RID: 3293
			internal const string DataTableHasErrorsDescr = "Returns whether the table has errors.";

			// Token: 0x04000CDE RID: 3294
			internal const string DataTableLocaleDescr = "Indicates a locale under which to compare strings within the table.";

			// Token: 0x04000CDF RID: 3295
			internal const string DataTableMinimumCapacityDescr = "Indicates an initial starting size for this table.";

			// Token: 0x04000CE0 RID: 3296
			internal const string DataTableNamespaceDescr = "Indicates the XML uri namespace for the elements contained in this table.";

			// Token: 0x04000CE1 RID: 3297
			internal const string DataTablePrefixDescr = "Indicates the Prefix of the namespace used for this table in XML representation.";

			// Token: 0x04000CE2 RID: 3298
			internal const string DataTableParentRelationsDescr = "Returns the parent relations for this table.";

			// Token: 0x04000CE3 RID: 3299
			internal const string DataTablePrimaryKeyDescr = "Indicates the column(s) that represent the primary key for this table.";

			// Token: 0x04000CE4 RID: 3300
			internal const string DataTableRowsDescr = "Indicates the collection that holds the rows of data for this table.";

			// Token: 0x04000CE5 RID: 3301
			internal const string DataTableTableNameDescr = "Indicates the name used to look up this table in the Tables collection of a DataSet.";

			// Token: 0x04000CE6 RID: 3302
			internal const string DataTableRowChangedDescr = "Occurs after a row in the table has been successfully edited.";

			// Token: 0x04000CE7 RID: 3303
			internal const string DataTableRowChangingDescr = "Occurs when the row is being changed so that the event handler can modify or cancel the change. The user can modify values in the row and should throw an  exception to cancel the edit.";

			// Token: 0x04000CE8 RID: 3304
			internal const string DataTableRowDeletedDescr = "Occurs after a row in the table has been successfully deleted.";

			// Token: 0x04000CE9 RID: 3305
			internal const string DataTableRowDeletingDescr = "Occurs when a row in the table marked for deletion.  Throw an exception to cancel the deletion.";

			// Token: 0x04000CEA RID: 3306
			internal const string DataTableColumnChangingDescr = "Occurs when a value has been submitted for this column.  The user can modify the proposed value and should throw an exception to cancel the edit.";

			// Token: 0x04000CEB RID: 3307
			internal const string DataTableColumnChangedDescr = "Occurs when a value has been changed for this column.";

			// Token: 0x04000CEC RID: 3308
			internal const string DataTableRowsClearingDescr = "Occurs prior to clearing all rows from the table.";

			// Token: 0x04000CED RID: 3309
			internal const string DataTableRowsClearedDescr = "Occurs after all rows in the table has been successfully cleared.";

			// Token: 0x04000CEE RID: 3310
			internal const string DataTableRowsNewRowDescr = "Occurs after a new DataRow has been instantiated.";

			// Token: 0x04000CEF RID: 3311
			internal const string DataRelationRelationNameDescr = "The name used to look up this relation in the Relations collection of a DataSet.";

			// Token: 0x04000CF0 RID: 3312
			internal const string DataRelationChildColumnsDescr = "Indicates the child columns of this relation.";

			// Token: 0x04000CF1 RID: 3313
			internal const string DataRelationParentColumnsDescr = "Indicates the parent columns of this relation.";

			// Token: 0x04000CF2 RID: 3314
			internal const string DataRelationNested = "Indicates whether relations are nested.";

			// Token: 0x04000CF3 RID: 3315
			internal const string ForeignKeyConstraintDeleteRuleDescr = "For deletions, indicates what kind of cascading should take place across this relation.";

			// Token: 0x04000CF4 RID: 3316
			internal const string ForeignKeyConstraintUpdateRuleDescr = "For updates, indicates what kind of cascading should take place across this relation.";

			// Token: 0x04000CF5 RID: 3317
			internal const string ForeignKeyConstraintAcceptRejectRuleDescr = "For accept and reject changes, indicates what kind of cascading should take place across this relation.";

			// Token: 0x04000CF6 RID: 3318
			internal const string ForeignKeyConstraintChildColumnsDescr = "Indicates the child columns of this constraint.";

			// Token: 0x04000CF7 RID: 3319
			internal const string ForeignKeyConstraintParentColumnsDescr = "Indicates the parent columns of this constraint.";

			// Token: 0x04000CF8 RID: 3320
			internal const string ForeignKeyRelatedTableDescr = "Indicates the child table of this constraint.";

			// Token: 0x04000CF9 RID: 3321
			internal const string KeyConstraintColumnsDescr = "Indicates the columns of this constraint.";

			// Token: 0x04000CFA RID: 3322
			internal const string KeyConstraintIsPrimaryKeyDescr = "Indicates if this constraint is a primary key.";

			// Token: 0x04000CFB RID: 3323
			internal const string ConstraintNameDescr = "Indicates the name of this constraint.";

			// Token: 0x04000CFC RID: 3324
			internal const string ConstraintTableDescr = "Indicates the table of this constraint.";

			// Token: 0x04000CFD RID: 3325
			internal const string DataColumnAllowNullDescr = "Indicates whether null values are allowed in this column.";

			// Token: 0x04000CFE RID: 3326
			internal const string DataColumnAutoIncrementDescr = "Indicates whether the column automatically increments itself for new rows added to the table.  The type of this column must be Int16, Int32, or Int64.";

			// Token: 0x04000CFF RID: 3327
			internal const string DataColumnAutoIncrementSeedDescr = "Indicates the starting value for an AutoIncrement column.";

			// Token: 0x04000D00 RID: 3328
			internal const string DataColumnAutoIncrementStepDescr = "Indicates the increment used by an AutoIncrement column.";

			// Token: 0x04000D01 RID: 3329
			internal const string DataColumnCaptionDescr = "Indicates the default user-interface caption for this column.";

			// Token: 0x04000D02 RID: 3330
			internal const string DataColumnColumnNameDescr = "Indicates the name used to look up this column in the Columns collection of a DataTable.";

			// Token: 0x04000D03 RID: 3331
			internal const string DataColumnDataTableDescr = "Returns the DataTable to which this column belongs.";

			// Token: 0x04000D04 RID: 3332
			internal const string DataColumnDataTypeDescr = "Indicates the type of data stored in this column.";

			// Token: 0x04000D05 RID: 3333
			internal const string DataColumnDefaultValueDescr = "Indicates the default column value used when adding new rows to the table.";

			// Token: 0x04000D06 RID: 3334
			internal const string DataColumnExpressionDescr = "Indicates the value that this column computes for each row based on other columns instead of taking user input.";

			// Token: 0x04000D07 RID: 3335
			internal const string DataColumnMappingDescr = "Indicates how this column persists in XML: as an attribute, element, simple content node, or nothing.";

			// Token: 0x04000D08 RID: 3336
			internal const string DataColumnNamespaceDescr = "Indicates the XML uri for elements or attributes stored in this column.";

			// Token: 0x04000D09 RID: 3337
			internal const string DataColumnPrefixDescr = "Indicates the Prefix used for this DataColumn in xml representation.";

			// Token: 0x04000D0A RID: 3338
			internal const string DataColumnOrdinalDescr = "Indicates the index of this column in the Columns collection.";

			// Token: 0x04000D0B RID: 3339
			internal const string DataColumnReadOnlyDescr = "Indicates whether this column allows changes once a row has been added to the table.";

			// Token: 0x04000D0C RID: 3340
			internal const string DataColumnUniqueDescr = "Indicates whether this column should restrict its values in the rows of the table to be unique.";

			// Token: 0x04000D0D RID: 3341
			internal const string DataColumnMaxLengthDescr = "Indicates the maximum length of the value this column allows.";

			// Token: 0x04000D0E RID: 3342
			internal const string DataColumnDateTimeModeDescr = "Indicates DateTimeMode of this DataColumn.";

			// Token: 0x04000D0F RID: 3343
			internal const string DataViewAllowDeleteDescr = "Indicates whether this DataView and the user interface associated with it allows deletes.";

			// Token: 0x04000D10 RID: 3344
			internal const string DataViewAllowEditDescr = "Indicates whether this DataView and the user interface associated with it allows edits.";

			// Token: 0x04000D11 RID: 3345
			internal const string DataViewAllowNewDescr = "Indicates whether this DataView and the user interface associated with it allows new rows to be added.";

			// Token: 0x04000D12 RID: 3346
			internal const string DataViewCountDescr = "Returns the number of items currently in this view.";

			// Token: 0x04000D13 RID: 3347
			internal const string DataViewDataViewManagerDescr = "This returns a pointer to back to the DataViewManager that owns this DataSet (if any).";

			// Token: 0x04000D14 RID: 3348
			internal const string DataViewIsOpenDescr = "Indicates whether the view is open.";

			// Token: 0x04000D15 RID: 3349
			internal const string DataViewRowFilterDescr = "Indicates an expression used to filter the data returned by this DataView.";

			// Token: 0x04000D16 RID: 3350
			internal const string DataViewRowStateFilterDescr = "Indicates the versions of data returned by this DataView.";

			// Token: 0x04000D17 RID: 3351
			internal const string DataViewSortDescr = "Indicates the names of the column and the order in which data is returned by this DataView.";

			// Token: 0x04000D18 RID: 3352
			internal const string DataViewApplyDefaultSortDescr = "Indicates whether to use the default sort if the Sort property is not set.";

			// Token: 0x04000D19 RID: 3353
			internal const string DataViewTableDescr = "Indicates the table this DataView uses to get data.";

			// Token: 0x04000D1A RID: 3354
			internal const string DataViewListChangedDescr = "Indicates that the data returned by this DataView has somehow changed.";

			// Token: 0x04000D1B RID: 3355
			internal const string DataViewManagerDataSetDescr = "Indicates the source of data for this DataViewManager.";

			// Token: 0x04000D1C RID: 3356
			internal const string DataViewManagerTableSettingsDescr = "Indicates the sorting/filtering/state settings for any table in the corresponding DataSet.";

			// Token: 0x04000D1D RID: 3357
			internal const string Xml_SimpleTypeNotSupported = "DataSet doesn't support 'union' or 'list' as simpleType.";

			// Token: 0x04000D1E RID: 3358
			internal const string Xml_MissingAttribute = "Invalid {0} syntax: missing required '{1}' attribute.";

			// Token: 0x04000D1F RID: 3359
			internal const string Xml_ValueOutOfRange = "Value '{1}' is invalid for attribute '{0}'.";

			// Token: 0x04000D20 RID: 3360
			internal const string Xml_AttributeValues = "The value of attribute '{0}' should be '{1}' or '{2}'.";

			// Token: 0x04000D21 RID: 3361
			internal const string Xml_ElementTypeNotFound = "Cannot find ElementType name='{0}'.";

			// Token: 0x04000D22 RID: 3362
			internal const string Xml_RelationParentNameMissing = "Parent table name is missing in relation '{0}'.";

			// Token: 0x04000D23 RID: 3363
			internal const string Xml_RelationChildNameMissing = "Child table name is missing in relation '{0}'.";

			// Token: 0x04000D24 RID: 3364
			internal const string Xml_RelationTableKeyMissing = "Parent table key is missing in relation '{0}'.";

			// Token: 0x04000D25 RID: 3365
			internal const string Xml_RelationChildKeyMissing = "Child table key is missing in relation '{0}'.";

			// Token: 0x04000D26 RID: 3366
			internal const string Xml_UndefinedDatatype = "Undefined data type: '{0}'.";

			// Token: 0x04000D27 RID: 3367
			internal const string Xml_DatatypeNotDefined = "Data type not defined.";

			// Token: 0x04000D28 RID: 3368
			internal const string Xml_InvalidField = "Invalid XPath selection inside field node. Cannot find: {0}.";

			// Token: 0x04000D29 RID: 3369
			internal const string Xml_InvalidSelector = "Invalid XPath selection inside selector node: {0}.";

			// Token: 0x04000D2A RID: 3370
			internal const string Xml_InvalidKey = "Invalid 'Key' node inside constraint named: {0}.";

			// Token: 0x04000D2B RID: 3371
			internal const string Xml_DuplicateConstraint = "The constraint name {0} is already used in the schema.";

			// Token: 0x04000D2C RID: 3372
			internal const string Xml_CannotConvert = "Cannot convert '{0}' to type '{1}'.";

			// Token: 0x04000D2D RID: 3373
			internal const string Xml_MissingRefer = "Missing '{0}' part in '{1}' constraint named '{2}'.";

			// Token: 0x04000D2E RID: 3374
			internal const string Xml_MismatchKeyLength = "Invalid Relation definition: different length keys.";

			// Token: 0x04000D2F RID: 3375
			internal const string Xml_CircularComplexType = "DataSet doesn't allow the circular reference in the ComplexType named '{0}'.";

			// Token: 0x04000D30 RID: 3376
			internal const string Xml_CannotInstantiateAbstract = "DataSet cannot instantiate an abstract ComplexType for the node {0}.";

			// Token: 0x04000D31 RID: 3377
			internal const string Xml_MultipleTargetConverterError = "An error occurred with the multiple target converter while writing an Xml Schema.  See the inner exception for details.";

			// Token: 0x04000D32 RID: 3378
			internal const string Xml_MultipleTargetConverterEmpty = "An error occurred with the multiple target converter while writing an Xml Schema.  A null or empty string was returned.";

			// Token: 0x04000D33 RID: 3379
			internal const string Xml_MergeDuplicateDeclaration = "Duplicated declaration '{0}'.";

			// Token: 0x04000D34 RID: 3380
			internal const string Xml_MissingTable = "Cannot load diffGram. Table '{0}' is missing in the destination dataset.";

			// Token: 0x04000D35 RID: 3381
			internal const string Xml_MissingSQL = "Cannot load diffGram. The 'sql' node is missing.";

			// Token: 0x04000D36 RID: 3382
			internal const string Xml_ColumnConflict = "Column name '{0}' is defined for different mapping types.";

			// Token: 0x04000D37 RID: 3383
			internal const string Xml_InvalidPrefix = "Prefix '{0}' is not valid, because it contains special characters.";

			// Token: 0x04000D38 RID: 3384
			internal const string Xml_NestedCircular = "Circular reference in self-nested table '{0}'.";

			// Token: 0x04000D39 RID: 3385
			internal const string Xml_FoundEntity = "DataSet cannot expand entities. Use XmlValidatingReader and set the EntityHandling property accordingly.";

			// Token: 0x04000D3A RID: 3386
			internal const string Xml_PolymorphismNotSupported = "Type '{0}' does not implement IXmlSerializable interface therefore can not proceed with serialization.";

			// Token: 0x04000D3B RID: 3387
			internal const string Xml_CanNotDeserializeObjectType = "Unable to proceed with deserialization. Data does not implement IXMLSerializable, therefore polymorphism is not supported.";

			// Token: 0x04000D3C RID: 3388
			internal const string Xml_DataTableInferenceNotSupported = "DataTable does not support schema inference from Xml.";

			// Token: 0x04000D3D RID: 3389
			internal const string Xml_MultipleParentRows = "Cannot proceed with serializing DataTable '{0}'. It contains a DataRow which has multiple parent rows on the same Foreign Key.";

			// Token: 0x04000D3E RID: 3390
			internal const string Xml_IsDataSetAttributeMissingInSchema = "IsDataSet attribute is missing in input Schema.";

			// Token: 0x04000D3F RID: 3391
			internal const string Xml_TooManyIsDataSetAtributeInSchema = "Cannot determine the DataSet Element. IsDataSet attribute exist more than once.";

			// Token: 0x04000D40 RID: 3392
			internal const string Xml_DynamicWithoutXmlSerializable = "DataSet will not serialize types that implement IDynamicMetaObjectProvider but do not also implement IXmlSerializable.";

			// Token: 0x04000D41 RID: 3393
			internal const string Expr_NYI = "The feature not implemented. {0}.";

			// Token: 0x04000D42 RID: 3394
			internal const string Expr_MissingOperand = "Syntax error: Missing operand after '{0}' operator.";

			// Token: 0x04000D43 RID: 3395
			internal const string Expr_TypeMismatch = "Type mismatch in expression '{0}'.";

			// Token: 0x04000D44 RID: 3396
			internal const string Expr_ExpressionTooComplex = "Expression is too complex.";

			// Token: 0x04000D45 RID: 3397
			internal const string Expr_UnboundName = "Cannot find column [{0}].";

			// Token: 0x04000D46 RID: 3398
			internal const string Expr_InvalidString = "The expression contains an invalid string constant: {0}.";

			// Token: 0x04000D47 RID: 3399
			internal const string Expr_UndefinedFunction = "The expression contains undefined function call {0}().";

			// Token: 0x04000D48 RID: 3400
			internal const string Expr_Syntax = "Syntax error in the expression.";

			// Token: 0x04000D49 RID: 3401
			internal const string Expr_FunctionArgumentCount = "Invalid number of arguments: function {0}().";

			// Token: 0x04000D4A RID: 3402
			internal const string Expr_MissingRightParen = "The expression is missing the closing parenthesis.";

			// Token: 0x04000D4B RID: 3403
			internal const string Expr_UnknownToken = "Cannot interpret token '{0}' at position {1}.";

			// Token: 0x04000D4C RID: 3404
			internal const string Expr_UnknownToken1 = "Expected {0}, but actual token at the position {2} is {1}.";

			// Token: 0x04000D4D RID: 3405
			internal const string Expr_DatatypeConvertion = "Cannot convert from {0} to {1}.";

			// Token: 0x04000D4E RID: 3406
			internal const string Expr_DatavalueConvertion = "Cannot convert value '{0}' to Type: {1}.";

			// Token: 0x04000D4F RID: 3407
			internal const string Expr_InvalidName = "Invalid column name [{0}].";

			// Token: 0x04000D50 RID: 3408
			internal const string Expr_InvalidDate = "The expression contains invalid date constant '{0}'.";

			// Token: 0x04000D51 RID: 3409
			internal const string Expr_NonConstantArgument = "Only constant expressions are allowed in the expression list for the IN operator.";

			// Token: 0x04000D52 RID: 3410
			internal const string Expr_InvalidPattern = "Error in Like operator: the string pattern '{0}' is invalid.";

			// Token: 0x04000D53 RID: 3411
			internal const string Expr_InWithoutParentheses = "Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.";

			// Token: 0x04000D54 RID: 3412
			internal const string Expr_ArgumentType = "Type mismatch in function argument: {0}(), argument {1}, expected {2}.";

			// Token: 0x04000D55 RID: 3413
			internal const string Expr_ArgumentTypeInteger = "Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.";

			// Token: 0x04000D56 RID: 3414
			internal const string Expr_TypeMismatchInBinop = "Cannot perform '{0}' operation on {1} and {2}.";

			// Token: 0x04000D57 RID: 3415
			internal const string Expr_AmbiguousBinop = "Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.";

			// Token: 0x04000D58 RID: 3416
			internal const string Expr_InWithoutList = "Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.";

			// Token: 0x04000D59 RID: 3417
			internal const string Expr_UnsupportedOperator = "The expression contains unsupported operator '{0}'.";

			// Token: 0x04000D5A RID: 3418
			internal const string Expr_InvalidNameBracketing = "The expression contains invalid name: '{0}'.";

			// Token: 0x04000D5B RID: 3419
			internal const string Expr_MissingOperandBefore = "Syntax error: Missing operand before '{0}' operator.";

			// Token: 0x04000D5C RID: 3420
			internal const string Expr_TooManyRightParentheses = "The expression has too many closing parentheses.";

			// Token: 0x04000D5D RID: 3421
			internal const string Expr_UnresolvedRelation = "The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.";

			// Token: 0x04000D5E RID: 3422
			internal const string Expr_AggregateArgument = "Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.";

			// Token: 0x04000D5F RID: 3423
			internal const string Expr_AggregateUnbound = "Unbound reference in the aggregate expression '{0}'.";

			// Token: 0x04000D60 RID: 3424
			internal const string Expr_EvalNoContext = "Cannot evaluate non-constant expression without current row.";

			// Token: 0x04000D61 RID: 3425
			internal const string Expr_ExpressionUnbound = "Unbound reference in the expression '{0}'.";

			// Token: 0x04000D62 RID: 3426
			internal const string Expr_ComputeNotAggregate = "Cannot evaluate. Expression '{0}' is not an aggregate.";

			// Token: 0x04000D63 RID: 3427
			internal const string Expr_FilterConvertion = "Filter expression '{0}' does not evaluate to a Boolean term.";

			// Token: 0x04000D64 RID: 3428
			internal const string Expr_InvalidType = "Invalid type name '{0}'.";

			// Token: 0x04000D65 RID: 3429
			internal const string Expr_LookupArgument = "Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.";

			// Token: 0x04000D66 RID: 3430
			internal const string Expr_InvokeArgument = "Need a row or a table to Invoke DataFilter.";

			// Token: 0x04000D67 RID: 3431
			internal const string Expr_ArgumentOutofRange = "{0}() argument is out of range.";

			// Token: 0x04000D68 RID: 3432
			internal const string Expr_IsSyntax = "Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.";

			// Token: 0x04000D69 RID: 3433
			internal const string Expr_Overflow = "Value is either too large or too small for Type '{0}'.";

			// Token: 0x04000D6A RID: 3434
			internal const string Expr_DivideByZero = "Divide by zero error encountered.";

			// Token: 0x04000D6B RID: 3435
			internal const string Expr_BindFailure = "Cannot find the parent relation '{0}'.";

			// Token: 0x04000D6C RID: 3436
			internal const string Expr_InvalidHoursArgument = "'hours' argument is out of range. Value must be between -14 and +14.";

			// Token: 0x04000D6D RID: 3437
			internal const string Expr_InvalidMinutesArgument = "'minutes' argument is out of range. Value must be between -59 and +59.";

			// Token: 0x04000D6E RID: 3438
			internal const string Expr_InvalidTimeZoneRange = "Provided range for time one exceeds total of 14 hours.";

			// Token: 0x04000D6F RID: 3439
			internal const string Expr_MismatchKindandTimeSpan = "Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.";

			// Token: 0x04000D70 RID: 3440
			internal const string Expr_UnsupportedType = "A DataColumn of type '{0}' does not support expression.";

			// Token: 0x04000D71 RID: 3441
			internal const string Data_EnforceConstraints = "Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.";

			// Token: 0x04000D72 RID: 3442
			internal const string Data_CannotModifyCollection = "Collection itself is not modifiable.";

			// Token: 0x04000D73 RID: 3443
			internal const string Data_CaseInsensitiveNameConflict = "The given name '{0}' matches at least two names in the collection object with different cases, but does not match either of them with the same case.";

			// Token: 0x04000D74 RID: 3444
			internal const string Data_NamespaceNameConflict = "The given name '{0}' matches at least two names in the collection object with different namespaces.";

			// Token: 0x04000D75 RID: 3445
			internal const string Data_InvalidOffsetLength = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

			// Token: 0x04000D76 RID: 3446
			internal const string Data_ArgumentOutOfRange = "'{0}' argument is out of range.";

			// Token: 0x04000D77 RID: 3447
			internal const string Data_ArgumentNull = "'{0}' argument cannot be null.";

			// Token: 0x04000D78 RID: 3448
			internal const string Data_ArgumentContainsNull = "'{0}' argument contains null value.";

			// Token: 0x04000D79 RID: 3449
			internal const string DataColumns_OutOfRange = "Cannot find column {0}.";

			// Token: 0x04000D7A RID: 3450
			internal const string DataColumns_Add1 = "Column '{0}' already belongs to this DataTable.";

			// Token: 0x04000D7B RID: 3451
			internal const string DataColumns_Add2 = "Column '{0}' already belongs to another DataTable.";

			// Token: 0x04000D7C RID: 3452
			internal const string DataColumns_Add3 = "Cannot have more than one SimpleContent columns in a DataTable.";

			// Token: 0x04000D7D RID: 3453
			internal const string DataColumns_Add4 = "Cannot add a SimpleContent column to a table containing element columns or nested relations.";

			// Token: 0x04000D7E RID: 3454
			internal const string DataColumns_AddDuplicate = "A column named '{0}' already belongs to this DataTable.";

			// Token: 0x04000D7F RID: 3455
			internal const string DataColumns_AddDuplicate2 = "Cannot add a column named '{0}': a nested table with the same name already belongs to this DataTable.";

			// Token: 0x04000D80 RID: 3456
			internal const string DataColumns_AddDuplicate3 = "A column named '{0}' already belongs to this DataTable: cannot set a nested table name to the same name.";

			// Token: 0x04000D81 RID: 3457
			internal const string DataColumns_Remove = "Cannot remove a column that doesn't belong to this table.";

			// Token: 0x04000D82 RID: 3458
			internal const string DataColumns_RemovePrimaryKey = "Cannot remove this column, because it's part of the primary key.";

			// Token: 0x04000D83 RID: 3459
			internal const string DataColumns_RemoveChildKey = "Cannot remove this column, because it is part of the parent key for relationship {0}.";

			// Token: 0x04000D84 RID: 3460
			internal const string DataColumns_RemoveConstraint = "Cannot remove this column, because it is a part of the constraint {0} on the table {1}.";

			// Token: 0x04000D85 RID: 3461
			internal const string DataColumns_RemoveExpression = "Cannot remove this column, because it is part of an expression: {0} = {1}.";

			// Token: 0x04000D86 RID: 3462
			internal const string DataColumn_AutoIncrementAndExpression = "Cannot set AutoIncrement property for a computed column.";

			// Token: 0x04000D87 RID: 3463
			internal const string DataColumn_AutoIncrementAndDefaultValue = "Cannot set AutoIncrement property for a column with DefaultValue set.";

			// Token: 0x04000D88 RID: 3464
			internal const string DataColumn_DefaultValueAndAutoIncrement = "Cannot set a DefaultValue on an AutoIncrement column.";

			// Token: 0x04000D89 RID: 3465
			internal const string DataColumn_AutoIncrementSeed = "AutoIncrementStep must be a non-zero value.";

			// Token: 0x04000D8A RID: 3466
			internal const string DataColumn_NameRequired = "ColumnName is required when it is part of a DataTable.";

			// Token: 0x04000D8B RID: 3467
			internal const string DataColumn_ChangeDataType = "Cannot change DataType of a column once it has data.";

			// Token: 0x04000D8C RID: 3468
			internal const string DataColumn_NullDataType = "Column requires a valid DataType.";

			// Token: 0x04000D8D RID: 3469
			internal const string DataColumn_DefaultValueDataType = "The DefaultValue for column {0} is of type {1} and cannot be converted to {2}.";

			// Token: 0x04000D8E RID: 3470
			internal const string DataColumn_DefaultValueDataType1 = "The DefaultValue for the column is of type {0} and cannot be converted to {1}.";

			// Token: 0x04000D8F RID: 3471
			internal const string DataColumn_DefaultValueColumnDataType = "The DefaultValue for column {0} is of type {1}, but the column is of type {2}.";

			// Token: 0x04000D90 RID: 3472
			internal const string DataColumn_ReadOnlyAndExpression = "Cannot change ReadOnly property for the expression column.";

			// Token: 0x04000D91 RID: 3473
			internal const string DataColumn_UniqueAndExpression = "Cannot change Unique property for the expression column.";

			// Token: 0x04000D92 RID: 3474
			internal const string DataColumn_ExpressionAndUnique = "Cannot create an expression on a column that has AutoIncrement or Unique.";

			// Token: 0x04000D93 RID: 3475
			internal const string DataColumn_ExpressionAndReadOnly = "Cannot set expression because column cannot be made ReadOnly.";

			// Token: 0x04000D94 RID: 3476
			internal const string DataColumn_ExpressionAndConstraint = "Cannot set Expression property on column {0}, because it is a part of a constraint.";

			// Token: 0x04000D95 RID: 3477
			internal const string DataColumn_ExpressionInConstraint = "Cannot create a constraint based on Expression column {0}.";

			// Token: 0x04000D96 RID: 3478
			internal const string DataColumn_ExpressionCircular = "Cannot set Expression property due to circular reference in the expression.";

			// Token: 0x04000D97 RID: 3479
			internal const string DataColumn_NullKeyValues = "Column '{0}' has null values in it.";

			// Token: 0x04000D98 RID: 3480
			internal const string DataColumn_NullValues = "Column '{0}' does not allow nulls.";

			// Token: 0x04000D99 RID: 3481
			internal const string DataColumn_ReadOnly = "Column '{0}' is read only.";

			// Token: 0x04000D9A RID: 3482
			internal const string DataColumn_NonUniqueValues = "Column '{0}' contains non-unique values.";

			// Token: 0x04000D9B RID: 3483
			internal const string DataColumn_NotInTheTable = "Column '{0}' does not belong to table {1}.";

			// Token: 0x04000D9C RID: 3484
			internal const string DataColumn_NotInAnyTable = "Column must belong to a table.";

			// Token: 0x04000D9D RID: 3485
			internal const string DataColumn_SetFailed = "Couldn't store <{0}> in {1} Column.  Expected type is {2}.";

			// Token: 0x04000D9E RID: 3486
			internal const string DataColumn_CannotSetToNull = "Cannot set Column '{0}' to be null. Please use DBNull instead.";

			// Token: 0x04000D9F RID: 3487
			internal const string DataColumn_LongerThanMaxLength = "Cannot set column '{0}'. The value violates the MaxLength limit of this column.";

			// Token: 0x04000DA0 RID: 3488
			internal const string DataColumn_HasToBeStringType = "MaxLength applies to string data type only. You cannot set Column '{0}' property MaxLength to be non-negative number.";

			// Token: 0x04000DA1 RID: 3489
			internal const string DataColumn_CannotSetMaxLength = "Cannot set Column '{0}' property MaxLength to '{1}'. There is at least one string in the table longer than the new limit.";

			// Token: 0x04000DA2 RID: 3490
			internal const string DataColumn_CannotSetMaxLength2 = "Cannot set Column '{0}' property MaxLength. The Column is SimpleContent.";

			// Token: 0x04000DA3 RID: 3491
			internal const string DataColumn_CannotSimpleContentType = "Cannot set Column '{0}' property DataType to {1}. The Column is SimpleContent.";

			// Token: 0x04000DA4 RID: 3492
			internal const string DataColumn_CannotSimpleContent = "Cannot set Column '{0}' property MappingType to SimpleContent. The Column DataType is {1}.";

			// Token: 0x04000DA5 RID: 3493
			internal const string DataColumn_ExceedMaxLength = "Column '{0}' exceeds the MaxLength limit.";

			// Token: 0x04000DA6 RID: 3494
			internal const string DataColumn_NotAllowDBNull = "Column '{0}' does not allow DBNull.Value.";

			// Token: 0x04000DA7 RID: 3495
			internal const string DataColumn_CannotChangeNamespace = "Cannot change the Column '{0}' property Namespace. The Column is SimpleContent.";

			// Token: 0x04000DA8 RID: 3496
			internal const string DataColumn_AutoIncrementCannotSetIfHasData = "Cannot change AutoIncrement of a DataColumn with type '{0}' once it has data.";

			// Token: 0x04000DA9 RID: 3497
			internal const string DataColumn_NotInTheUnderlyingTable = "Column '{0}' does not belong to underlying table '{1}'.";

			// Token: 0x04000DAA RID: 3498
			internal const string DataColumn_InvalidDataColumnMapping = "DataColumn with type '{0}' is a complexType. Can not serialize value of a complex type as Attribute";

			// Token: 0x04000DAB RID: 3499
			internal const string DataColumn_CannotSetDateTimeModeForNonDateTimeColumns = "The DateTimeMode can be set only on DataColumns of type DateTime.";

			// Token: 0x04000DAC RID: 3500
			internal const string DataColumn_InvalidDateTimeMode = "'{0}' is Invalid DataSetDateTime value.";

			// Token: 0x04000DAD RID: 3501
			internal const string DataColumn_DateTimeMode = "Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.";

			// Token: 0x04000DAE RID: 3502
			internal const string DataColumn_INullableUDTwithoutStaticNull = "Type '{0}' does not contain static Null property or field.";

			// Token: 0x04000DAF RID: 3503
			internal const string DataColumn_UDTImplementsIChangeTrackingButnotIRevertible = "Type '{0}' does not implement IRevertibleChangeTracking; therefore can not proceed with RejectChanges().";

			// Token: 0x04000DB0 RID: 3504
			internal const string DataColumn_SetAddedAndModifiedCalledOnNonUnchanged = "SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.";

			// Token: 0x04000DB1 RID: 3505
			internal const string DataColumn_OrdinalExceedMaximun = "Ordinal '{0}' exceeds the maximum number.";

			// Token: 0x04000DB2 RID: 3506
			internal const string DataColumn_NullableTypesNotSupported = "DataSet does not support System.Nullable<>.";

			// Token: 0x04000DB3 RID: 3507
			internal const string DataConstraint_NoName = "Cannot change the name of a constraint to empty string when it is in the ConstraintCollection.";

			// Token: 0x04000DB4 RID: 3508
			internal const string DataConstraint_Violation = "Cannot enforce constraints on constraint {0}.";

			// Token: 0x04000DB5 RID: 3509
			internal const string DataConstraint_ViolationValue = "Column '{0}' is constrained to be unique.  Value '{1}' is already present.";

			// Token: 0x04000DB6 RID: 3510
			internal const string DataConstraint_NotInTheTable = "Constraint '{0}' does not belong to this DataTable.";

			// Token: 0x04000DB7 RID: 3511
			internal const string DataConstraint_OutOfRange = "Cannot find constraint {0}.";

			// Token: 0x04000DB8 RID: 3512
			internal const string DataConstraint_Duplicate = "Constraint matches constraint named {0} already in collection.";

			// Token: 0x04000DB9 RID: 3513
			internal const string DataConstraint_DuplicateName = "A Constraint named '{0}' already belongs to this DataTable.";

			// Token: 0x04000DBA RID: 3514
			internal const string DataConstraint_UniqueViolation = "These columns don't currently have unique values.";

			// Token: 0x04000DBB RID: 3515
			internal const string DataConstraint_ForeignTable = "These columns don't point to this table.";

			// Token: 0x04000DBC RID: 3516
			internal const string DataConstraint_ParentValues = "This constraint cannot be enabled as not all values have corresponding parent values.";

			// Token: 0x04000DBD RID: 3517
			internal const string DataConstraint_AddFailed = "This constraint cannot be added since ForeignKey doesn't belong to table {0}.";

			// Token: 0x04000DBE RID: 3518
			internal const string DataConstraint_RemoveFailed = "Cannot remove a constraint that doesn't belong to this table.";

			// Token: 0x04000DBF RID: 3519
			internal const string DataConstraint_NeededForForeignKeyConstraint = "Cannot remove unique constraint '{0}'. Remove foreign key constraint '{1}' first.";

			// Token: 0x04000DC0 RID: 3520
			internal const string DataConstraint_CascadeDelete = "Cannot delete this row because constraints are enforced on relation {0}, and deleting this row will strand child rows.";

			// Token: 0x04000DC1 RID: 3521
			internal const string DataConstraint_CascadeUpdate = "Cannot make this change because constraints are enforced on relation {0}, and changing this value will strand child rows.";

			// Token: 0x04000DC2 RID: 3522
			internal const string DataConstraint_ClearParentTable = "Cannot clear table {0} because ForeignKeyConstraint {1} enforces constraints and there are child rows in {2}.";

			// Token: 0x04000DC3 RID: 3523
			internal const string DataConstraint_ForeignKeyViolation = "ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.";

			// Token: 0x04000DC4 RID: 3524
			internal const string DataConstraint_BadObjectPropertyAccess = "Property not accessible because '{0}'.";

			// Token: 0x04000DC5 RID: 3525
			internal const string DataConstraint_RemoveParentRow = "Cannot remove this row because it has child rows, and constraints on relation {0} are enforced.";

			// Token: 0x04000DC6 RID: 3526
			internal const string DataConstraint_AddPrimaryKeyConstraint = "Cannot add primary key constraint since primary key is already set for the table.";

			// Token: 0x04000DC7 RID: 3527
			internal const string DataConstraint_CantAddConstraintToMultipleNestedTable = "Cannot add constraint to DataTable '{0}' which is a child table in two nested relations.";

			// Token: 0x04000DC8 RID: 3528
			internal const string DataKey_TableMismatch = "Cannot create a Key from Columns that belong to different tables.";

			// Token: 0x04000DC9 RID: 3529
			internal const string DataKey_NoColumns = "Cannot have 0 columns.";

			// Token: 0x04000DCA RID: 3530
			internal const string DataKey_TooManyColumns = "Cannot have more than {0} columns.";

			// Token: 0x04000DCB RID: 3531
			internal const string DataKey_DuplicateColumns = "Cannot create a Key when the same column is listed more than once: '{0}'";

			// Token: 0x04000DCC RID: 3532
			internal const string DataKey_RemovePrimaryKey = "Cannot remove unique constraint since it's the primary key of a table.";

			// Token: 0x04000DCD RID: 3533
			internal const string DataKey_RemovePrimaryKey1 = "Cannot remove unique constraint since it's the primary key of table {0}.";

			// Token: 0x04000DCE RID: 3534
			internal const string DataRelation_ColumnsTypeMismatch = "Parent Columns and Child Columns don't have type-matching columns.";

			// Token: 0x04000DCF RID: 3535
			internal const string DataRelation_KeyColumnsIdentical = "ParentKey and ChildKey are identical.";

			// Token: 0x04000DD0 RID: 3536
			internal const string DataRelation_KeyLengthMismatch = "ParentColumns and ChildColumns should be the same length.";

			// Token: 0x04000DD1 RID: 3537
			internal const string DataRelation_KeyZeroLength = "ParentColumns and ChildColumns must not be zero length.";

			// Token: 0x04000DD2 RID: 3538
			internal const string DataRelation_ForeignRow = "The row doesn't belong to the same DataSet as this relation.";

			// Token: 0x04000DD3 RID: 3539
			internal const string DataRelation_NoName = "RelationName is required when it is part of a DataSet.";

			// Token: 0x04000DD4 RID: 3540
			internal const string DataRelation_ForeignTable = "GetChildRows requires a row whose Table is {0}, but the specified row's Table is {1}.";

			// Token: 0x04000DD5 RID: 3541
			internal const string DataRelation_ForeignDataSet = "This relation should connect two tables in this DataSet to be added to this DataSet.";

			// Token: 0x04000DD6 RID: 3542
			internal const string DataRelation_GetParentRowTableMismatch = "GetParentRow requires a row whose Table is {0}, but the specified row's Table is {1}.";

			// Token: 0x04000DD7 RID: 3543
			internal const string DataRelation_SetParentRowTableMismatch = "SetParentRow requires a child row whose Table is {0}, but the specified row's Table is {1}.";

			// Token: 0x04000DD8 RID: 3544
			internal const string DataRelation_DataSetMismatch = "Cannot have a relationship between tables in different DataSets.";

			// Token: 0x04000DD9 RID: 3545
			internal const string DataRelation_TablesInDifferentSets = "Cannot create a relation between tables in different DataSets.";

			// Token: 0x04000DDA RID: 3546
			internal const string DataRelation_AlreadyExists = "A relation already exists for these child columns.";

			// Token: 0x04000DDB RID: 3547
			internal const string DataRelation_DoesNotExist = "This relation doesn't belong to this relation collection.";

			// Token: 0x04000DDC RID: 3548
			internal const string DataRelation_AlreadyInOtherDataSet = "This relation already belongs to another DataSet.";

			// Token: 0x04000DDD RID: 3549
			internal const string DataRelation_AlreadyInTheDataSet = "This relation already belongs to this DataSet.";

			// Token: 0x04000DDE RID: 3550
			internal const string DataRelation_DuplicateName = "A Relation named '{0}' already belongs to this DataSet.";

			// Token: 0x04000DDF RID: 3551
			internal const string DataRelation_NotInTheDataSet = "Relation {0} does not belong to this DataSet.";

			// Token: 0x04000DE0 RID: 3552
			internal const string DataRelation_OutOfRange = "Cannot find relation {0}.";

			// Token: 0x04000DE1 RID: 3553
			internal const string DataRelation_TableNull = "Cannot create a collection on a null table.";

			// Token: 0x04000DE2 RID: 3554
			internal const string DataRelation_TableWasRemoved = "The table this collection displays relations for has been removed from its DataSet.";

			// Token: 0x04000DE3 RID: 3555
			internal const string DataRelation_ChildTableMismatch = "Cannot add a relation to this table's ParentRelation collection where this table isn't the child table.";

			// Token: 0x04000DE4 RID: 3556
			internal const string DataRelation_ParentTableMismatch = "Cannot add a relation to this table's ChildRelation collection where this table isn't the parent table.";

			// Token: 0x04000DE5 RID: 3557
			internal const string DataRelation_RelationNestedReadOnly = "Cannot set the 'Nested' property to false for this relation.";

			// Token: 0x04000DE6 RID: 3558
			internal const string DataRelation_TableCantBeNestedInTwoTables = "The same table '{0}' cannot be the child table in two nested relations.";

			// Token: 0x04000DE7 RID: 3559
			internal const string DataRelation_LoopInNestedRelations = "The table ({0}) cannot be the child table to itself in nested relations.";

			// Token: 0x04000DE8 RID: 3560
			internal const string DataRelation_CaseLocaleMismatch = "Cannot add a DataRelation or Constraint that has different Locale or CaseSensitive settings between its parent and child tables.";

			// Token: 0x04000DE9 RID: 3561
			internal const string DataRelation_ParentOrChildColumnsDoNotHaveDataSet = "Cannot create a DataRelation if Parent or Child Columns are not in a DataSet.";

			// Token: 0x04000DEA RID: 3562
			internal const string DataRelation_InValidNestedRelation = "Nested table '{0}' which inherits its namespace cannot have multiple parent tables in different namespaces.";

			// Token: 0x04000DEB RID: 3563
			internal const string DataRelation_InValidNamespaceInNestedRelation = "Nested table '{0}' with empty namespace cannot have multiple parent tables in different namespaces.";

			// Token: 0x04000DEC RID: 3564
			internal const string DataRow_NotInTheDataSet = "The row doesn't belong to the same DataSet as this relation.";

			// Token: 0x04000DED RID: 3565
			internal const string DataRow_NotInTheTable = "Cannot perform this operation on a row not in the table.";

			// Token: 0x04000DEE RID: 3566
			internal const string DataRow_ParentRowNotInTheDataSet = "This relation and child row don't belong to same DataSet.";

			// Token: 0x04000DEF RID: 3567
			internal const string DataRow_EditInRowChanging = "Cannot change a proposed value in the RowChanging event.";

			// Token: 0x04000DF0 RID: 3568
			internal const string DataRow_EndEditInRowChanging = "Cannot call EndEdit() inside an OnRowChanging event.";

			// Token: 0x04000DF1 RID: 3569
			internal const string DataRow_BeginEditInRowChanging = "Cannot call BeginEdit() inside the RowChanging event.";

			// Token: 0x04000DF2 RID: 3570
			internal const string DataRow_CancelEditInRowChanging = "Cannot call CancelEdit() inside an OnRowChanging event.  Throw an exception to cancel this update.";

			// Token: 0x04000DF3 RID: 3571
			internal const string DataRow_DeleteInRowDeleting = "Cannot call Delete inside an OnRowDeleting event.  Throw an exception to cancel this delete.";

			// Token: 0x04000DF4 RID: 3572
			internal const string DataRow_ValuesArrayLength = "Input array is longer than the number of columns in this table.";

			// Token: 0x04000DF5 RID: 3573
			internal const string DataRow_NoCurrentData = "There is no Current data to access.";

			// Token: 0x04000DF6 RID: 3574
			internal const string DataRow_NoOriginalData = "There is no Original data to access.";

			// Token: 0x04000DF7 RID: 3575
			internal const string DataRow_NoProposedData = "There is no Proposed data to access.";

			// Token: 0x04000DF8 RID: 3576
			internal const string DataRow_RemovedFromTheTable = "This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.";

			// Token: 0x04000DF9 RID: 3577
			internal const string DataRow_DeletedRowInaccessible = "Deleted row information cannot be accessed through the row.";

			// Token: 0x04000DFA RID: 3578
			internal const string DataRow_InvalidVersion = "Version must be Original, Current, or Proposed.";

			// Token: 0x04000DFB RID: 3579
			internal const string DataRow_OutOfRange = "There is no row at position {0}.";

			// Token: 0x04000DFC RID: 3580
			internal const string DataRow_RowInsertOutOfRange = "The row insert position {0} is invalid.";

			// Token: 0x04000DFD RID: 3581
			internal const string DataRow_RowInsertTwice = "The rowOrder value={0} has been found twice for table named '{1}'.";

			// Token: 0x04000DFE RID: 3582
			internal const string DataRow_RowInsertMissing = "Values are missing in the rowOrder sequence for table '{0}'.";

			// Token: 0x04000DFF RID: 3583
			internal const string DataRow_RowOutOfRange = "The given DataRow is not in the current DataRowCollection.";

			// Token: 0x04000E00 RID: 3584
			internal const string DataRow_AlreadyInOtherCollection = "This row already belongs to another table.";

			// Token: 0x04000E01 RID: 3585
			internal const string DataRow_AlreadyInTheCollection = "This row already belongs to this table.";

			// Token: 0x04000E02 RID: 3586
			internal const string DataRow_AlreadyDeleted = "Cannot delete this row since it's already deleted.";

			// Token: 0x04000E03 RID: 3587
			internal const string DataRow_Empty = "This row is empty.";

			// Token: 0x04000E04 RID: 3588
			internal const string DataRow_AlreadyRemoved = "Cannot remove a row that's already been removed.";

			// Token: 0x04000E05 RID: 3589
			internal const string DataRow_MultipleParents = "A child row has multiple parents.";

			// Token: 0x04000E06 RID: 3590
			internal const string DataRow_InvalidRowBitPattern = "Unrecognized row state bit pattern.";

			// Token: 0x04000E07 RID: 3591
			internal const string DataSet_SetNameToEmpty = "Cannot change the name of the DataSet to an empty string.";

			// Token: 0x04000E08 RID: 3592
			internal const string DataSet_SetDataSetNameConflicting = "The name '{0}' is invalid. A DataSet cannot have the same name of the DataTable.";

			// Token: 0x04000E09 RID: 3593
			internal const string DataSet_UnsupportedSchema = "The schema namespace is invalid. Please use this one instead: {0}.";

			// Token: 0x04000E0A RID: 3594
			internal const string DataSet_CannotChangeCaseLocale = "Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.";

			// Token: 0x04000E0B RID: 3595
			internal const string DataSet_CannotChangeSchemaSerializationMode = "SchemaSerializationMode property can be set only if it is overridden by derived DataSet.";

			// Token: 0x04000E0C RID: 3596
			internal const string DataTable_ForeignPrimaryKey = "PrimaryKey columns do not belong to this table.";

			// Token: 0x04000E0D RID: 3597
			internal const string DataTable_CannotAddToSimpleContent = "Cannot add a nested relation or an element column to a table containing a SimpleContent column.";

			// Token: 0x04000E0E RID: 3598
			internal const string DataTable_NoName = "TableName is required when it is part of a DataSet.";

			// Token: 0x04000E0F RID: 3599
			internal const string DataTable_MultipleSimpleContentColumns = "DataTable already has a simple content column.";

			// Token: 0x04000E10 RID: 3600
			internal const string DataTable_MissingPrimaryKey = "Table doesn't have a primary key.";

			// Token: 0x04000E11 RID: 3601
			internal const string DataTable_InvalidSortString = "{0} isn't a valid Sort string entry.";

			// Token: 0x04000E12 RID: 3602
			internal const string DataTable_CanNotSerializeDataTableHierarchy = "Cannot serialize the DataTable. A DataTable being used in one or more DataColumn expressions is not a descendant of current DataTable.";

			// Token: 0x04000E13 RID: 3603
			internal const string DataTable_CanNotRemoteDataTable = "This DataTable can only be remoted as part of DataSet. One or more Expression Columns has reference to other DataTable(s).";

			// Token: 0x04000E14 RID: 3604
			internal const string DataTable_CanNotSetRemotingFormat = "Cannot have different remoting format property value for DataSet and DataTable.";

			// Token: 0x04000E15 RID: 3605
			internal const string DataTable_CanNotSerializeDataTableWithEmptyName = "Cannot serialize the DataTable. DataTable name is not set.";

			// Token: 0x04000E16 RID: 3606
			internal const string DataTable_DuplicateName = "A DataTable named '{0}' already belongs to this DataSet.";

			// Token: 0x04000E17 RID: 3607
			internal const string DataTable_DuplicateName2 = "A DataTable named '{0}' with the same Namespace '{1}' already belongs to this DataSet.";

			// Token: 0x04000E18 RID: 3608
			internal const string DataTable_SelfnestedDatasetConflictingName = "The table ({0}) cannot be the child table to itself in a nested relation: the DataSet name conflicts with the table name.";

			// Token: 0x04000E19 RID: 3609
			internal const string DataTable_DatasetConflictingName = "The name '{0}' is invalid. A DataTable cannot have the same name of the DataSet.";

			// Token: 0x04000E1A RID: 3610
			internal const string DataTable_AlreadyInOtherDataSet = "DataTable already belongs to another DataSet.";

			// Token: 0x04000E1B RID: 3611
			internal const string DataTable_AlreadyInTheDataSet = "DataTable already belongs to this DataSet.";

			// Token: 0x04000E1C RID: 3612
			internal const string DataTable_NotInTheDataSet = "Table {0} does not belong to this DataSet.";

			// Token: 0x04000E1D RID: 3613
			internal const string DataTable_OutOfRange = "Cannot find table {0}.";

			// Token: 0x04000E1E RID: 3614
			internal const string DataTable_InRelation = "Cannot remove a table that has existing relations.  Remove relations first.";

			// Token: 0x04000E1F RID: 3615
			internal const string DataTable_InConstraint = "Cannot remove table {0}, because it referenced in ForeignKeyConstraint {1}.  Remove the constraint first.";

			// Token: 0x04000E20 RID: 3616
			internal const string DataTable_TableNotFound = "DataTable '{0}' does not match to any DataTable in source.";

			// Token: 0x04000E21 RID: 3617
			internal const string DataMerge_MissingDefinition = "Target DataSet missing definition for {0}.";

			// Token: 0x04000E22 RID: 3618
			internal const string DataMerge_MissingConstraint = "Target DataSet missing {0} {1}.";

			// Token: 0x04000E23 RID: 3619
			internal const string DataMerge_DataTypeMismatch = "<target>.{0} and <source>.{0} have conflicting properties: DataType property mismatch.";

			// Token: 0x04000E24 RID: 3620
			internal const string DataMerge_PrimaryKeyMismatch = "<target>.PrimaryKey and <source>.PrimaryKey have different Length.";

			// Token: 0x04000E25 RID: 3621
			internal const string DataMerge_PrimaryKeyColumnsMismatch = "Mismatch columns in the PrimaryKey : <target>.{0} versus <source>.{1}.";

			// Token: 0x04000E26 RID: 3622
			internal const string DataMerge_ReltionKeyColumnsMismatch = "Relation {0} cannot be merged, because keys have mismatch columns.";

			// Token: 0x04000E27 RID: 3623
			internal const string DataMerge_MissingColumnDefinition = "Target table {0} missing definition for column {1}.";

			// Token: 0x04000E28 RID: 3624
			internal const string DataMerge_MissingPrimaryKeyColumnInSource = "PrimaryKey column {0} does not exist in source Table.";

			// Token: 0x04000E29 RID: 3625
			internal const string DataIndex_RecordStateRange = "The RowStates parameter must be set to a valid combination of values from the DataViewRowState enumeration.";

			// Token: 0x04000E2A RID: 3626
			internal const string DataIndex_FindWithoutSortOrder = "Find finds a row based on a Sort order, and no Sort order is specified.";

			// Token: 0x04000E2B RID: 3627
			internal const string DataIndex_KeyLength = "Expecting {0} value(s) for the key being indexed, but received {1} value(s).";

			// Token: 0x04000E2C RID: 3628
			internal const string DataStorage_AggregateException = "Invalid usage of aggregate function {0}() and Type: {1}.";

			// Token: 0x04000E2D RID: 3629
			internal const string DataStorage_InvalidStorageType = "Invalid storage type: {0}.";

			// Token: 0x04000E2E RID: 3630
			internal const string DataStorage_ProblematicChars = "The DataSet Xml persistency does not support the value '{0}' as Char value, please use Byte storage instead.";

			// Token: 0x04000E2F RID: 3631
			internal const string DataStorage_SetInvalidDataType = "Type of value has a mismatch with column type";

			// Token: 0x04000E30 RID: 3632
			internal const string DataStorage_IComparableNotDefined = "Type '{0}' does not implement IComparable interface. Comparison cannot be done.";

			// Token: 0x04000E31 RID: 3633
			internal const string DataView_SetFailed = "Cannot set {0}.";

			// Token: 0x04000E32 RID: 3634
			internal const string DataView_SetDataSetFailed = "Cannot change DataSet on a DataViewManager that's already the default view for a DataSet.";

			// Token: 0x04000E33 RID: 3635
			internal const string DataView_SetRowStateFilter = "RowStateFilter cannot show ModifiedOriginals and ModifiedCurrents at the same time.";

			// Token: 0x04000E34 RID: 3636
			internal const string DataView_SetTable = "Cannot change Table property on a DefaultView or a DataView coming from a DataViewManager.";

			// Token: 0x04000E35 RID: 3637
			internal const string DataView_CanNotSetDataSet = "Cannot change DataSet property once it is set.";

			// Token: 0x04000E36 RID: 3638
			internal const string DataView_CanNotUseDataViewManager = "DataSet must be set prior to using DataViewManager.";

			// Token: 0x04000E37 RID: 3639
			internal const string DataView_CanNotSetTable = "Cannot change Table property once it is set.";

			// Token: 0x04000E38 RID: 3640
			internal const string DataView_CanNotUse = "DataTable must be set prior to using DataView.";

			// Token: 0x04000E39 RID: 3641
			internal const string DataView_CanNotBindTable = "Cannot bind to DataTable with no name.";

			// Token: 0x04000E3A RID: 3642
			internal const string DataView_SetIListObject = "Cannot set an object into this list.";

			// Token: 0x04000E3B RID: 3643
			internal const string DataView_AddNewNotAllowNull = "Cannot call AddNew on a DataView where AllowNew is false.";

			// Token: 0x04000E3C RID: 3644
			internal const string DataView_NotOpen = "DataView is not open.";

			// Token: 0x04000E3D RID: 3645
			internal const string DataView_CreateChildView = "The relation is not parented to the table to which this DataView points.";

			// Token: 0x04000E3E RID: 3646
			internal const string DataView_CanNotDelete = "Cannot delete on a DataSource where AllowDelete is false.";

			// Token: 0x04000E3F RID: 3647
			internal const string DataView_CanNotEdit = "Cannot edit on a DataSource where AllowEdit is false.";

			// Token: 0x04000E40 RID: 3648
			internal const string DataView_GetElementIndex = "Index {0} is either negative or above rows count.";

			// Token: 0x04000E41 RID: 3649
			internal const string DataView_AddExternalObject = "Cannot add external objects to this list.";

			// Token: 0x04000E42 RID: 3650
			internal const string DataView_CanNotClear = "Cannot clear this list.";

			// Token: 0x04000E43 RID: 3651
			internal const string DataView_InsertExternalObject = "Cannot insert external objects to this list.";

			// Token: 0x04000E44 RID: 3652
			internal const string DataView_RemoveExternalObject = "Cannot remove objects not in the list.";

			// Token: 0x04000E45 RID: 3653
			internal const string DataROWView_PropertyNotFound = "{0} is neither a DataColumn nor a DataRelation for table {1}.";

			// Token: 0x04000E46 RID: 3654
			internal const string Range_Argument = "Min ({0}) must be less than or equal to max ({1}) in a Range object.";

			// Token: 0x04000E47 RID: 3655
			internal const string Range_NullRange = "This is a null range.";

			// Token: 0x04000E48 RID: 3656
			internal const string RecordManager_MinimumCapacity = "MinimumCapacity must be non-negative.";

			// Token: 0x04000E49 RID: 3657
			internal const string CodeGen_InvalidIdentifier = "Cannot generate identifier for name '{0}'.";

			// Token: 0x04000E4A RID: 3658
			internal const string CodeGen_DuplicateTableName = "There is more than one table with the same name '{0}' (even if namespace is different).";

			// Token: 0x04000E4B RID: 3659
			internal const string CodeGen_TypeCantBeNull = "Column '{0}': Type '{1}' cannot be null.";

			// Token: 0x04000E4C RID: 3660
			internal const string CodeGen_NoCtor0 = "Column '{0}': Type '{1}' does not have parameterless constructor.";

			// Token: 0x04000E4D RID: 3661
			internal const string CodeGen_NoCtor1 = "Column '{0}': Type '{1}' does not have constructor with string argument.";

			// Token: 0x04000E4E RID: 3662
			internal const string SqlConvert_ConvertFailed = "Cannot convert object of type '{0}' to object of type '{1}'.";

			// Token: 0x04000E4F RID: 3663
			internal const string DataSet_DefaultDataException = "Data Exception.";

			// Token: 0x04000E50 RID: 3664
			internal const string DataSet_DefaultConstraintException = "Constraint Exception.";

			// Token: 0x04000E51 RID: 3665
			internal const string DataSet_DefaultDeletedRowInaccessibleException = "Deleted rows inaccessible.";

			// Token: 0x04000E52 RID: 3666
			internal const string DataSet_DefaultDuplicateNameException = "Duplicate name not allowed.";

			// Token: 0x04000E53 RID: 3667
			internal const string DataSet_DefaultInRowChangingEventException = "Operation not supported in the RowChanging event.";

			// Token: 0x04000E54 RID: 3668
			internal const string DataSet_DefaultInvalidConstraintException = "Invalid constraint.";

			// Token: 0x04000E55 RID: 3669
			internal const string DataSet_DefaultMissingPrimaryKeyException = "Missing primary key.";

			// Token: 0x04000E56 RID: 3670
			internal const string DataSet_DefaultNoNullAllowedException = "Null not allowed.";

			// Token: 0x04000E57 RID: 3671
			internal const string DataSet_DefaultReadOnlyException = "Column is marked read only.";

			// Token: 0x04000E58 RID: 3672
			internal const string DataSet_DefaultRowNotInTableException = "Row not found in table.";

			// Token: 0x04000E59 RID: 3673
			internal const string DataSet_DefaultVersionNotFoundException = "Version not found.";

			// Token: 0x04000E5A RID: 3674
			internal const string Load_ReadOnlyDataModified = "ReadOnly Data is Modified.";

			// Token: 0x04000E5B RID: 3675
			internal const string DataTableReader_InvalidDataTableReader = "DataTableReader is invalid for current DataTable '{0}'.";

			// Token: 0x04000E5C RID: 3676
			internal const string DataTableReader_SchemaInvalidDataTableReader = "Schema of current DataTable '{0}' in DataTableReader has changed, DataTableReader is invalid.";

			// Token: 0x04000E5D RID: 3677
			internal const string DataTableReader_CannotCreateDataReaderOnEmptyDataSet = "DataTableReader Cannot be created. There is no DataTable in DataSet.";

			// Token: 0x04000E5E RID: 3678
			internal const string DataTableReader_DataTableReaderArgumentIsEmpty = "Cannot create DataTableReader. Argument is Empty.";

			// Token: 0x04000E5F RID: 3679
			internal const string DataTableReader_ArgumentContainsNullValue = "Cannot create DataTableReader. Arguments contain null value.";

			// Token: 0x04000E60 RID: 3680
			internal const string DataTableReader_InvalidRowInDataTableReader = "Current DataRow is either in Deleted or Detached state.";

			// Token: 0x04000E61 RID: 3681
			internal const string DataTableReader_DataTableCleared = "Current DataTable '{0}' is empty. There is no DataRow in DataTable.";

			// Token: 0x04000E62 RID: 3682
			internal const string RbTree_InvalidState = "DataTable internal index is corrupted: '{0}'.";

			// Token: 0x04000E63 RID: 3683
			internal const string RbTree_EnumerationBroken = "Collection was modified; enumeration operation might not execute.";

			// Token: 0x04000E64 RID: 3684
			internal const string NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration = "Simple type '{0}' has already be declared with different '{1}'.";

			// Token: 0x04000E65 RID: 3685
			internal const string DataDom_Foliation = "Invalid foliation.";

			// Token: 0x04000E66 RID: 3686
			internal const string DataDom_TableNameChange = "Cannot change the table name once the associated DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E67 RID: 3687
			internal const string DataDom_TableNamespaceChange = "Cannot change the table namespace once the associated DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E68 RID: 3688
			internal const string DataDom_ColumnNameChange = "Cannot change the column name once the associated DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E69 RID: 3689
			internal const string DataDom_ColumnNamespaceChange = "Cannot change the column namespace once the associated DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E6A RID: 3690
			internal const string DataDom_ColumnMappingChange = "Cannot change the ColumnMapping property once the associated DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E6B RID: 3691
			internal const string DataDom_TableColumnsChange = "Cannot add or remove columns from the table once the DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E6C RID: 3692
			internal const string DataDom_DataSetTablesChange = "Cannot add or remove tables from the DataSet once the DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E6D RID: 3693
			internal const string DataDom_DataSetNestedRelationsChange = "Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E6E RID: 3694
			internal const string DataDom_DataSetNull = "The DataSet parameter is invalid. It cannot be null.";

			// Token: 0x04000E6F RID: 3695
			internal const string DataDom_DataSetNameChange = "Cannot change the DataSet name once the DataSet is mapped to a loaded XML document.";

			// Token: 0x04000E70 RID: 3696
			internal const string DataDom_CloneNode = "This type of node cannot be cloned: {0}.";

			// Token: 0x04000E71 RID: 3697
			internal const string DataDom_MultipleLoad = "Cannot load XmlDataDocument if it already contains data. Please use a new XmlDataDocument.";

			// Token: 0x04000E72 RID: 3698
			internal const string DataDom_MultipleDataSet = "DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.";

			// Token: 0x04000E73 RID: 3699
			internal const string DataDom_EnforceConstraintsShouldBeOff = "Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.";

			// Token: 0x04000E74 RID: 3700
			internal const string DataDom_NotSupport_GetElementById = "GetElementById() is not supported on DataDocument.";

			// Token: 0x04000E75 RID: 3701
			internal const string DataDom_NotSupport_EntRef = "Cannot create entity references on DataDocument.";

			// Token: 0x04000E76 RID: 3702
			internal const string DataDom_NotSupport_Clear = "Clear function on DateSet and DataTable is not supported on XmlDataDocument.";

			// Token: 0x04000E77 RID: 3703
			internal const string StrongTyping_CannotRemoveColumn = "Cannot remove column since it is built in to this dataSet.";

			// Token: 0x04000E78 RID: 3704
			internal const string StrongTyping_CananotRemoveRelation = "Cannot remove relation since it is built in to this dataSet.";

			// Token: 0x04000E79 RID: 3705
			internal const string propertyChangedEventDescr = "Occurs whenever a property for this control changes.";

			// Token: 0x04000E7A RID: 3706
			internal const string collectionChangedEventDescr = "Occurs whenever this collection's membership changes.";

			// Token: 0x04000E7B RID: 3707
			internal const string StrongTyping_CananotAccessDBNull = "Cannot get value because it is DBNull.";

			// Token: 0x04000E7C RID: 3708
			internal const string ADP_PropertyNotSupported = "The '{0}' property requires Microsoft WindowsNT or a WindowsNT based operating system.";

			// Token: 0x04000E7D RID: 3709
			internal const string ConfigProviderNotFound = "Unable to find the requested .NET Framework Data Provider.  It may not be installed.";

			// Token: 0x04000E7E RID: 3710
			internal const string ConfigProviderInvalid = "The requested .NET Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.";

			// Token: 0x04000E7F RID: 3711
			internal const string ConfigProviderNotInstalled = "Failed to find or load the registered .NET Framework Data Provider.";

			// Token: 0x04000E80 RID: 3712
			internal const string ConfigProviderMissing = "The missing .NET Framework Data Provider's assembly qualified name is required.";

			// Token: 0x04000E81 RID: 3713
			internal const string ConfigBaseElementsOnly = "Only elements allowed.";

			// Token: 0x04000E82 RID: 3714
			internal const string ConfigBaseNoChildNodes = "Child nodes not allowed.";

			// Token: 0x04000E83 RID: 3715
			internal const string ConfigUnrecognizedAttributes = "Unrecognized attribute '{0}'.";

			// Token: 0x04000E84 RID: 3716
			internal const string ConfigUnrecognizedElement = "Unrecognized element.";

			// Token: 0x04000E85 RID: 3717
			internal const string ConfigSectionsUnique = "The '{0}' section can only appear once per config file.";

			// Token: 0x04000E86 RID: 3718
			internal const string ConfigRequiredAttributeMissing = "Required attribute '{0}' not found.";

			// Token: 0x04000E87 RID: 3719
			internal const string ConfigRequiredAttributeEmpty = "Required attribute '{0}' cannot be empty.";

			// Token: 0x04000E88 RID: 3720
			internal const string ADP_EmptyArray = "Expecting non-empty array for '{0}' parameter.";

			// Token: 0x04000E89 RID: 3721
			internal const string ADP_SingleValuedProperty = "The only acceptable value for the property '{0}' is '{1}'.";

			// Token: 0x04000E8A RID: 3722
			internal const string ADP_DoubleValuedProperty = "The acceptable values for the property '{0}' are '{1}' or '{2}'.";

			// Token: 0x04000E8B RID: 3723
			internal const string ADP_InvalidPrefixSuffix = "Specified QuotePrefix and QuoteSuffix values do not match.";

			// Token: 0x04000E8C RID: 3724
			internal const string ADP_InvalidArgumentLength = "The length of argument '{0}' exceeds its limit of '{1}'.";

			// Token: 0x04000E8D RID: 3725
			internal const string SQL_WrongType = "Expecting argument of type {1}, but received type {0}.";

			// Token: 0x04000E8E RID: 3726
			internal const string ADP_InvalidConnectionOptionValue = "Invalid value for key '{0}'.";

			// Token: 0x04000E8F RID: 3727
			internal const string ADP_MissingConnectionOptionValue = "Use of key '{0}' requires the key '{1}' to be present.";

			// Token: 0x04000E90 RID: 3728
			internal const string ADP_InvalidConnectionOptionValueLength = "The value's length for key '{0}' exceeds its limit of '{1}'.";

			// Token: 0x04000E91 RID: 3729
			internal const string ADP_KeywordNotSupported = "Keyword not supported: '{0}'.";

			// Token: 0x04000E92 RID: 3730
			internal const string ADP_UdlFileError = "Unable to load the UDL file.";

			// Token: 0x04000E93 RID: 3731
			internal const string ADP_InvalidUDL = "Invalid UDL file.";

			// Token: 0x04000E94 RID: 3732
			internal const string ADP_InternalProviderError = "Internal .NET Framework Data Provider error {0}.";

			// Token: 0x04000E95 RID: 3733
			internal const string ADP_NoQuoteChange = "The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.";

			// Token: 0x04000E96 RID: 3734
			internal const string ADP_MissingSourceCommand = "The DataAdapter.SelectCommand property needs to be initialized.";

			// Token: 0x04000E97 RID: 3735
			internal const string ADP_MissingSourceCommandConnection = "The DataAdapter.SelectCommand.Connection property needs to be initialized;";

			// Token: 0x04000E98 RID: 3736
			internal const string ADP_InvalidMultipartName = "{0} \"{1}\".";

			// Token: 0x04000E99 RID: 3737
			internal const string ADP_InvalidMultipartNameQuoteUsage = "{0} \"{1}\", incorrect usage of quotes.";

			// Token: 0x04000E9A RID: 3738
			internal const string ADP_InvalidMultipartNameToManyParts = "{0} \"{1}\", the current limit of \"{2}\" is insufficient.";

			// Token: 0x04000E9B RID: 3739
			internal const string SQL_BulkCopyDestinationTableName = "SqlBulkCopy.WriteToServer failed because the SqlBulkCopy.DestinationTableName is an invalid multipart name";

			// Token: 0x04000E9C RID: 3740
			internal const string SQL_TDSParserTableName = "Processing of results from SQL Server failed because of an invalid multipart name";

			// Token: 0x04000E9D RID: 3741
			internal const string SQL_UDTTypeName = "SqlParameter.UdtTypeName is an invalid multipart name";

			// Token: 0x04000E9E RID: 3742
			internal const string SQL_TypeName = "SqlParameter.TypeName is an invalid multipart name";

			// Token: 0x04000E9F RID: 3743
			internal const string SQL_SqlCommandCommandText = "SqlCommand.DeriveParameters failed because the SqlCommand.CommandText property value is an invalid multipart name";

			// Token: 0x04000EA0 RID: 3744
			internal const string ODBC_ODBCCommandText = "OdbcCommandBuilder.DeriveParameters failed because the OdbcCommand.CommandText property value is an invalid multipart name";

			// Token: 0x04000EA1 RID: 3745
			internal const string OLEDB_OLEDBCommandText = "OleDbCommandBuilder.DeriveParameters failed because the OleDbCommandBuilder.CommandText property value is an invalid multipart name";

			// Token: 0x04000EA2 RID: 3746
			internal const string SQLMSF_FailoverPartnerNotSupported = "Connecting to a mirrored SQL Server instance using the MultiSubnetFailover connection option is not supported.";

			// Token: 0x04000EA3 RID: 3747
			internal const string ADP_ColumnSchemaExpression = "The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.";

			// Token: 0x04000EA4 RID: 3748
			internal const string ADP_ColumnSchemaMismatch = "Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.";

			// Token: 0x04000EA5 RID: 3749
			internal const string ADP_ColumnSchemaMissing1 = "Missing the DataColumn '{0}' for the SourceColumn '{2}'.";

			// Token: 0x04000EA6 RID: 3750
			internal const string ADP_ColumnSchemaMissing2 = "Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.";

			// Token: 0x04000EA7 RID: 3751
			internal const string ADP_InvalidSourceColumn = "SourceColumn is required to be a non-empty string.";

			// Token: 0x04000EA8 RID: 3752
			internal const string ADP_MissingColumnMapping = "Missing SourceColumn mapping for '{0}'.";

			// Token: 0x04000EA9 RID: 3753
			internal const string ADP_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the {2} method.";

			// Token: 0x04000EAA RID: 3754
			internal const string ODBC_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the .NET Framework Odbc Data Provider.";

			// Token: 0x04000EAB RID: 3755
			internal const string OLEDB_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the .NET Framework OleDb Data Provider.";

			// Token: 0x04000EAC RID: 3756
			internal const string SQL_NotSupportedEnumerationValue = "The {0} enumeration value, {1}, is not supported by the .NET Framework SqlClient Data Provider.";

			// Token: 0x04000EAD RID: 3757
			internal const string ADP_ComputerNameEx = "Unable to retrieve the ComputerNameDnsFullyQualified, {0}.";

			// Token: 0x04000EAE RID: 3758
			internal const string ADP_MissingTableSchema = "Missing the '{0}' DataTable for the '{1}' SourceTable.";

			// Token: 0x04000EAF RID: 3759
			internal const string ADP_InvalidSourceTable = "SourceTable is required to be a non-empty string";

			// Token: 0x04000EB0 RID: 3760
			internal const string ADP_MissingTableMapping = "Missing SourceTable mapping: '{0}'";

			// Token: 0x04000EB1 RID: 3761
			internal const string ADP_CommandTextRequired = "{0}: CommandText property has not been initialized";

			// Token: 0x04000EB2 RID: 3762
			internal const string ADP_ConnectionRequired = "{0}: Connection property has not been initialized.";

			// Token: 0x04000EB3 RID: 3763
			internal const string ADP_OpenConnectionRequired = "{0} requires an open and available Connection. {1}";

			// Token: 0x04000EB4 RID: 3764
			internal const string ADP_ConnectionRequired_Fill = "Fill: SelectCommand.Connection property has not been initialized.";

			// Token: 0x04000EB5 RID: 3765
			internal const string ADP_ConnectionRequired_FillPage = "FillPage: SelectCommand.Connection property has not been initialized.";

			// Token: 0x04000EB6 RID: 3766
			internal const string ADP_ConnectionRequired_FillSchema = "FillSchema: SelectCommand.Connection property has not been initialized.";

			// Token: 0x04000EB7 RID: 3767
			internal const string ADP_ConnectionRequired_Insert = "Update requires the InsertCommand to have a connection object. The Connection property of the InsertCommand has not been initialized.";

			// Token: 0x04000EB8 RID: 3768
			internal const string ADP_ConnectionRequired_Update = "Update requires the UpdateCommand to have a connection object. The Connection property of the UpdateCommand has not been initialized.";

			// Token: 0x04000EB9 RID: 3769
			internal const string ADP_ConnectionRequired_Delete = "Update requires the DeleteCommand to have a connection object. The Connection property of the DeleteCommand has not been initialized.";

			// Token: 0x04000EBA RID: 3770
			internal const string ADP_ConnectionRequired_Batch = "Update requires a connection object.  The Connection property has not been initialized.";

			// Token: 0x04000EBB RID: 3771
			internal const string ADP_ConnectionRequired_Clone = "Update requires the command clone to have a connection object. The Connection property of the command clone has not been initialized.";

			// Token: 0x04000EBC RID: 3772
			internal const string ADP_ConnecitonRequired_UpdateRows = "Update requires a connection.";

			// Token: 0x04000EBD RID: 3773
			internal const string ADP_OpenConnectionRequired_Insert = "Update requires the {0}Command to have an open connection object. {1}";

			// Token: 0x04000EBE RID: 3774
			internal const string ADP_OpenConnectionRequired_Update = "Update requires the {0}Command to have an open connection object. {1}";

			// Token: 0x04000EBF RID: 3775
			internal const string ADP_OpenConnectionRequired_Delete = "Update requires the {0}Command to have an open connection object. {1}";

			// Token: 0x04000EC0 RID: 3776
			internal const string ADP_OpenConnectionRequired_Clone = "Update requires the updating command to have an open connection object. {1}";

			// Token: 0x04000EC1 RID: 3777
			internal const string ADP_NoStoredProcedureExists = "The stored procedure '{0}' doesn't exist.";

			// Token: 0x04000EC2 RID: 3778
			internal const string ADP_TransactionCompleted = "The transaction assigned to this command must be the most nested pending local transaction.";

			// Token: 0x04000EC3 RID: 3779
			internal const string ADP_TransactionConnectionMismatch = "The transaction is either not associated with the current connection or has been completed.";

			// Token: 0x04000EC4 RID: 3780
			internal const string ADP_TransactionCompletedButNotDisposed = "The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.";

			// Token: 0x04000EC5 RID: 3781
			internal const string ADP_TransactionRequired = "{0} requires the command to have a transaction when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.";

			// Token: 0x04000EC6 RID: 3782
			internal const string ADP_OpenResultSetExists = "There is already an open SqlResultSet associated with this command which must be closed first.";

			// Token: 0x04000EC7 RID: 3783
			internal const string ADP_OpenReaderExists = "There is already an open DataReader associated with this {0} which must be closed first.";

			// Token: 0x04000EC8 RID: 3784
			internal const string ADP_DeriveParametersNotSupported = "{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{1}.";

			// Token: 0x04000EC9 RID: 3785
			internal const string ADP_CalledTwice = "The method '{0}' cannot be called more than once for the same execution.";

			// Token: 0x04000ECA RID: 3786
			internal const string ADP_IncorrectAsyncResult = "Incorrect async result.";

			// Token: 0x04000ECB RID: 3787
			internal const string ADP_MissingSelectCommand = "The SelectCommand property has not been initialized before calling '{0}'.";

			// Token: 0x04000ECC RID: 3788
			internal const string ADP_UnwantedStatementType = "The StatementType {0} is not expected here.";

			// Token: 0x04000ECD RID: 3789
			internal const string ADP_FillSchemaRequiresSourceTableName = "FillSchema: expected a non-empty string for the SourceTable name.";

			// Token: 0x04000ECE RID: 3790
			internal const string ADP_InvalidMaxRecords = "The MaxRecords value of {0} is invalid; the value must be >= 0.";

			// Token: 0x04000ECF RID: 3791
			internal const string ADP_InvalidStartRecord = "The StartRecord value of {0} is invalid; the value must be >= 0.";

			// Token: 0x04000ED0 RID: 3792
			internal const string ADP_FillRequiresSourceTableName = "Fill: expected a non-empty string for the SourceTable name.";

			// Token: 0x04000ED1 RID: 3793
			internal const string ADP_FillChapterAutoIncrement = "Hierarchical chapter columns must map to an AutoIncrement DataColumn.";

			// Token: 0x04000ED2 RID: 3794
			internal const string ADP_MissingDataReaderFieldType = "DataReader.GetFieldType({0}) returned null.";

			// Token: 0x04000ED3 RID: 3795
			internal const string ADP_OnlyOneTableForStartRecordOrMaxRecords = "Only specify one item in the dataTables array when using non-zero values for startRecords or maxRecords.";

			// Token: 0x04000ED4 RID: 3796
			internal const string ADP_UpdateRequiresSourceTable = "Update unable to find TableMapping['{0}'] or DataTable '{0}'.";

			// Token: 0x04000ED5 RID: 3797
			internal const string ADP_UpdateRequiresSourceTableName = "Update: expected a non-empty SourceTable name.";

			// Token: 0x04000ED6 RID: 3798
			internal const string ADP_MissingTableMappingDestination = "Missing TableMapping when TableMapping.DataSetTable='{0}'.";

			// Token: 0x04000ED7 RID: 3799
			internal const string ADP_UpdateRequiresCommandClone = "Update requires the command clone to be valid.";

			// Token: 0x04000ED8 RID: 3800
			internal const string ADP_UpdateRequiresCommandSelect = "Auto SQL generation during Update requires a valid SelectCommand.";

			// Token: 0x04000ED9 RID: 3801
			internal const string ADP_UpdateRequiresCommandInsert = "Update requires a valid InsertCommand when passed DataRow collection with new rows.";

			// Token: 0x04000EDA RID: 3802
			internal const string ADP_UpdateRequiresCommandUpdate = "Update requires a valid UpdateCommand when passed DataRow collection with modified rows.";

			// Token: 0x04000EDB RID: 3803
			internal const string ADP_UpdateRequiresCommandDelete = "Update requires a valid DeleteCommand when passed DataRow collection with deleted rows.";

			// Token: 0x04000EDC RID: 3804
			internal const string ADP_UpdateMismatchRowTable = "DataRow[{0}] is from a different DataTable than DataRow[0].";

			// Token: 0x04000EDD RID: 3805
			internal const string ADP_RowUpdatedErrors = "RowUpdatedEvent: Errors occurred; no additional is information available.";

			// Token: 0x04000EDE RID: 3806
			internal const string ADP_RowUpdatingErrors = "RowUpdatingEvent: Errors occurred; no additional is information available.";

			// Token: 0x04000EDF RID: 3807
			internal const string ADP_ResultsNotAllowedDuringBatch = "When batching, the command's UpdatedRowSource property value of UpdateRowSource.FirstReturnedRecord or UpdateRowSource.Both is invalid.";

			// Token: 0x04000EE0 RID: 3808
			internal const string ADP_UpdateConcurrencyViolation_Update = "Concurrency violation: the UpdateCommand affected {0} of the expected {1} records.";

			// Token: 0x04000EE1 RID: 3809
			internal const string ADP_UpdateConcurrencyViolation_Delete = "Concurrency violation: the DeleteCommand affected {0} of the expected {1} records.";

			// Token: 0x04000EE2 RID: 3810
			internal const string ADP_UpdateConcurrencyViolation_Batch = "Concurrency violation: the batched command affected {0} of the expected {1} records.";

			// Token: 0x04000EE3 RID: 3811
			internal const string ADP_InvalidCommandTimeout = "Invalid CommandTimeout value {0}; the value must be >= 0.";

			// Token: 0x04000EE4 RID: 3812
			internal const string ADP_UninitializedParameterSize = "{1}[{0}]: the Size property has an invalid size of 0.";

			// Token: 0x04000EE5 RID: 3813
			internal const string ADP_PrepareParameterType = "{0}.Prepare method requires all parameters to have an explicitly set type.";

			// Token: 0x04000EE6 RID: 3814
			internal const string ADP_PrepareParameterSize = "{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.";

			// Token: 0x04000EE7 RID: 3815
			internal const string ADP_PrepareParameterScale = "{0}.Prepare method requires parameters of type '{1}' have an explicitly set Precision and Scale.";

			// Token: 0x04000EE8 RID: 3816
			internal const string ADP_MismatchedAsyncResult = "Mismatched end method call for asyncResult.  Expected call to {0} but {1} was called instead.";

			// Token: 0x04000EE9 RID: 3817
			internal const string ADP_ClosedConnectionError = "Invalid operation. The connection is closed.";

			// Token: 0x04000EEA RID: 3818
			internal const string ADP_ConnectionIsDisabled = "The connection has been disabled.";

			// Token: 0x04000EEB RID: 3819
			internal const string ADP_LocalTransactionPresent = "Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry.";

			// Token: 0x04000EEC RID: 3820
			internal const string ADP_TransactionPresent = "Connection currently has transaction enlisted.  Finish current transaction and retry.";

			// Token: 0x04000EED RID: 3821
			internal const string ADP_EmptyDatabaseName = "Database cannot be null, the empty string, or string of only whitespace.";

			// Token: 0x04000EEE RID: 3822
			internal const string ADP_DatabaseNameTooLong = "The argument is too long.";

			// Token: 0x04000EEF RID: 3823
			internal const string ADP_InvalidConnectTimeoutValue = "Invalid 'Connect Timeout' value which must be an integer >= 0.";

			// Token: 0x04000EF0 RID: 3824
			internal const string ADP_InvalidSourceBufferIndex = "Invalid source buffer (size of {0}) offset: {1}";

			// Token: 0x04000EF1 RID: 3825
			internal const string ADP_InvalidDestinationBufferIndex = "Invalid destination buffer (size of {0}) offset: {1}";

			// Token: 0x04000EF2 RID: 3826
			internal const string ADP_DataReaderNoData = "No data exists for the row/column.";

			// Token: 0x04000EF3 RID: 3827
			internal const string ADP_NumericToDecimalOverflow = "The numerical value is too large to fit into a 96 bit decimal.";

			// Token: 0x04000EF4 RID: 3828
			internal const string ADP_StreamClosed = "Invalid attempt to {0} when stream is closed.";

			// Token: 0x04000EF5 RID: 3829
			internal const string ADP_InvalidSeekOrigin = "Specified SeekOrigin value is invalid.";

			// Token: 0x04000EF6 RID: 3830
			internal const string ADP_DynamicSQLJoinUnsupported = "Dynamic SQL generation is not supported against multiple base tables.";

			// Token: 0x04000EF7 RID: 3831
			internal const string ADP_DynamicSQLNoTableInfo = "Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.";

			// Token: 0x04000EF8 RID: 3832
			internal const string ADP_DynamicSQLNoKeyInfoDelete = "Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.";

			// Token: 0x04000EF9 RID: 3833
			internal const string ADP_DynamicSQLNoKeyInfoUpdate = "Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.";

			// Token: 0x04000EFA RID: 3834
			internal const string ADP_DynamicSQLNoKeyInfoRowVersionDelete = "Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.";

			// Token: 0x04000EFB RID: 3835
			internal const string ADP_DynamicSQLNoKeyInfoRowVersionUpdate = "Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.";

			// Token: 0x04000EFC RID: 3836
			internal const string ADP_DynamicSQLNestedQuote = "Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.";

			// Token: 0x04000EFD RID: 3837
			internal const string ADP_NonSequentialColumnAccess = "Invalid attempt to read from column ordinal '{0}'.  With CommandBehavior.SequentialAccess, you may only read from column ordinal '{1}' or greater.";

			// Token: 0x04000EFE RID: 3838
			internal const string ADP_InvalidDateTimeDigits = "Data type '{0}' can not be formatted as a literal because it has an invalid date time digits.";

			// Token: 0x04000EFF RID: 3839
			internal const string ADP_InvalidFormatValue = "The value can not be formatted as a literal of the requested type.";

			// Token: 0x04000F00 RID: 3840
			internal const string ADP_InvalidMaximumScale = "Data type '{0}' can not be formatted as a literal because it has an invalid maximum scale.";

			// Token: 0x04000F01 RID: 3841
			internal const string ADP_LiteralValueIsInvalid = "The literal value provided is not a valid literal for the data type '{0}'.";

			// Token: 0x04000F02 RID: 3842
			internal const string ADP_EvenLengthLiteralValue = "'{0}':The length of the literal value must be even.";

			// Token: 0x04000F03 RID: 3843
			internal const string ADP_HexDigitLiteralValue = "'{0}':The literal value must be a string with hexadecimal digits";

			// Token: 0x04000F04 RID: 3844
			internal const string ADP_QuotePrefixNotSet = "{0} requires open connection when the quote prefix has not been set.";

			// Token: 0x04000F05 RID: 3845
			internal const string ADP_UnableToCreateBooleanLiteral = "Can not determine the correct boolean literal values. Boolean literals can not be created.";

			// Token: 0x04000F06 RID: 3846
			internal const string ADP_UnsupportedNativeDataTypeOleDb = "Literals of the native data type associated with data type '{0}' are not supported.";

			// Token: 0x04000F07 RID: 3847
			internal const string ADP_InvalidDataType = "The parameter data type of {0} is invalid.";

			// Token: 0x04000F08 RID: 3848
			internal const string ADP_UnknownDataType = "No mapping exists from object type {0} to a known managed provider native type.";

			// Token: 0x04000F09 RID: 3849
			internal const string ADP_UnknownDataTypeCode = "Unable to handle an unknown TypeCode {0} returned by Type {1}.";

			// Token: 0x04000F0A RID: 3850
			internal const string ADP_DbTypeNotSupported = "No mapping exists from DbType {0} to a known {1}.";

			// Token: 0x04000F0B RID: 3851
			internal const string ADP_VersionDoesNotSupportDataType = "The version of SQL Server in use does not support datatype '{0}'.";

			// Token: 0x04000F0C RID: 3852
			internal const string ADP_ParameterValueOutOfRange = "Parameter value '{0}' is out of range.";

			// Token: 0x04000F0D RID: 3853
			internal const string ADP_BadParameterName = "Specified parameter name '{0}' is not valid.";

			// Token: 0x04000F0E RID: 3854
			internal const string ADP_MultipleReturnValue = "Multiple return value parameters are not supported.";

			// Token: 0x04000F0F RID: 3855
			internal const string ADP_InvalidSizeValue = "Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.";

			// Token: 0x04000F10 RID: 3856
			internal const string ADP_NegativeParameter = "Invalid value for argument '{0}'. The value must be greater than or equal to 0.";

			// Token: 0x04000F11 RID: 3857
			internal const string ADP_InvalidMetaDataValue = "Invalid value for this metadata.";

			// Token: 0x04000F12 RID: 3858
			internal const string ADP_NotRowType = "Metadata must be SqlDbType.Row";

			// Token: 0x04000F13 RID: 3859
			internal const string ADP_ParameterConversionFailed = "Failed to convert parameter value from a {0} to a {1}.";

			// Token: 0x04000F14 RID: 3860
			internal const string ADP_ParallelTransactionsNotSupported = "{0} does not support parallel transactions.";

			// Token: 0x04000F15 RID: 3861
			internal const string ADP_TransactionZombied = "This {0} has completed; it is no longer usable.";

			// Token: 0x04000F16 RID: 3862
			internal const string ADP_DbRecordReadOnly = "'{0}' cannot be called when the record is read only.";

			// Token: 0x04000F17 RID: 3863
			internal const string ADP_DbDataUpdatableRecordReadOnly = "'{0}' cannot be called when the DbDataRecord is read only.";

			// Token: 0x04000F18 RID: 3864
			internal const string ADP_InvalidImplicitConversion = "Implicit conversion of object type '{0}' to data type '{1}' is not supported.";

			// Token: 0x04000F19 RID: 3865
			internal const string ADP_InvalidBufferSizeOrIndex = "Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.";

			// Token: 0x04000F1A RID: 3866
			internal const string ADP_InvalidDataLength = "Data length '{0}' is less than 0.";

			// Token: 0x04000F1B RID: 3867
			internal const string ADP_InvalidDataLength2 = "Specified length '{0}' is out of range.";

			// Token: 0x04000F1C RID: 3868
			internal const string ADP_NonSeqByteAccess = "Invalid {2} attempt at dataIndex '{0}'.  With CommandBehavior.SequentialAccess, you may only read from dataIndex '{1}' or greater.";

			// Token: 0x04000F1D RID: 3869
			internal const string ADP_OffsetOutOfRangeException = "Offset must refer to a location within the value.";

			// Token: 0x04000F1E RID: 3870
			internal const string ODBC_GetSchemaRestrictionRequired = "\"The ODBC managed provider requires that the TABLE_NAME restriction be specified and non-null for the GetSchema indexes collection.";

			// Token: 0x04000F1F RID: 3871
			internal const string ADP_InvalidArgumentValue = "Invalid argument value for method '{0}'.";

			// Token: 0x04000F20 RID: 3872
			internal const string ADP_OdbcNoTypesFromProvider = "The ODBC provider did not return results from SQLGETTYPEINFO.";

			// Token: 0x04000F21 RID: 3873
			internal const string ADP_NullDataTable = "Unexpected null DataTable argument";

			// Token: 0x04000F22 RID: 3874
			internal const string ADP_NullDataSet = "Unexpected null DataSet argument.";

			// Token: 0x04000F23 RID: 3875
			internal const string OdbcConnection_ConnectionStringTooLong = "Connection string exceeds maximum allowed length of {0}.";

			// Token: 0x04000F24 RID: 3876
			internal const string Odbc_GetTypeMapping_UnknownType = "{0} - unable to map type.";

			// Token: 0x04000F25 RID: 3877
			internal const string Odbc_UnknownSQLType = "Unknown SQL type - {0}.";

			// Token: 0x04000F26 RID: 3878
			internal const string Odbc_UnknownURTType = "Unknown URT type - {0}.";

			// Token: 0x04000F27 RID: 3879
			internal const string Odbc_NegativeArgument = "Invalid negative argument!";

			// Token: 0x04000F28 RID: 3880
			internal const string Odbc_CantSetPropertyOnOpenConnection = "Can't set property on an open connection.";

			// Token: 0x04000F29 RID: 3881
			internal const string Odbc_NoMappingForSqlTransactionLevel = "No valid mapping for a SQL_TRANSACTION '{0}' to a System.Data.IsolationLevel enumeration value.";

			// Token: 0x04000F2A RID: 3882
			internal const string Odbc_CantEnableConnectionpooling = "{0} - unable to enable connection pooling...";

			// Token: 0x04000F2B RID: 3883
			internal const string Odbc_CantAllocateEnvironmentHandle = "{0} - unable to allocate an environment handle.";

			// Token: 0x04000F2C RID: 3884
			internal const string Odbc_FailedToGetDescriptorHandle = "{0} - unable to get descriptor handle.";

			// Token: 0x04000F2D RID: 3885
			internal const string Odbc_NotInTransaction = "Not in a transaction";

			// Token: 0x04000F2E RID: 3886
			internal const string Odbc_UnknownOdbcType = "Invalid OdbcType enumeration value={0}.";

			// Token: 0x04000F2F RID: 3887
			internal const string Odbc_NullData = "Use IsDBNull when DBNull.Value data is expected.";

			// Token: 0x04000F30 RID: 3888
			internal const string Odbc_ExceptionMessage = "{0} [{1}] {2}";

			// Token: 0x04000F31 RID: 3889
			internal const string Odbc_ExceptionNoInfoMsg = "{0} - no error information available";

			// Token: 0x04000F32 RID: 3890
			internal const string Odbc_ConnectionClosed = "The connection is closed.";

			// Token: 0x04000F33 RID: 3891
			internal const string Odbc_OpenConnectionNoOwner = "An internal connection does not have an owner.";

			// Token: 0x04000F34 RID: 3892
			internal const string Odbc_MDACWrongVersion = "The .NET Framework Odbc Data Provider requires Microsoft Data Access Components(MDAC) version 2.6 or later.  Version {0} was found currently installed.";

			// Token: 0x04000F35 RID: 3893
			internal const string OleDb_MDACWrongVersion = "The .NET Framework OleDb Data Provider requires Microsoft Data Access Components(MDAC) version 2.6 or later.  Version {0} was found currently installed.";

			// Token: 0x04000F36 RID: 3894
			internal const string OleDb_SchemaRowsetsNotSupported = "'{0}' interface is not supported by the '{1}' provider.  GetOleDbSchemaTable is unavailable with the current provider.";

			// Token: 0x04000F37 RID: 3895
			internal const string OleDb_NoErrorInformation2 = "'{0}' failed with no error message available, result code: {1}.";

			// Token: 0x04000F38 RID: 3896
			internal const string OleDb_NoErrorInformation = "No error message available, result code: {0}.";

			// Token: 0x04000F39 RID: 3897
			internal const string OleDb_MDACNotAvailable = "The .NET Framework Data Providers require Microsoft Data Access Components(MDAC).  Please install Microsoft Data Access Components(MDAC) version 2.6 or later.";

			// Token: 0x04000F3A RID: 3898
			internal const string OleDb_MSDASQLNotSupported = "The .NET Framework Data Provider for OLEDB (Microsoft.Data.OleDb) does not support the Microsoft OLE DB Provider for ODBC Drivers (MSDASQL). Use the .NET Framework Data Provider for ODBC (System.Data.Odbc).";

			// Token: 0x04000F3B RID: 3899
			internal const string OleDb_PossiblePromptNotUserInteractive = "The .NET Framework Data Provider for OLEDB will not allow the OLE DB Provider to prompt the user in a non-interactive environment.";

			// Token: 0x04000F3C RID: 3900
			internal const string OleDb_ProviderUnavailable = "The '{0}' provider is not registered on the local machine.";

			// Token: 0x04000F3D RID: 3901
			internal const string OleDb_CommandTextNotSupported = "The ICommandText interface is not supported by the '{0}' provider. Use CommandType.TableDirect instead.";

			// Token: 0x04000F3E RID: 3902
			internal const string OleDb_TransactionsNotSupported = "The ITransactionLocal interface is not supported by the '{0}' provider.  Local transactions are unavailable with the current provider.";

			// Token: 0x04000F3F RID: 3903
			internal const string OleDb_ConnectionStringSyntax = "Format of the initialization string does not conform to the OLE DB specification.  Starting around char[{0}] in the connection string.";

			// Token: 0x04000F40 RID: 3904
			internal const string OleDb_AsynchronousNotSupported = "'Asynchronous Processing' is not a supported feature of the .NET Framework Data OLE DB Provider(Microsoft.Data.OleDb).";

			// Token: 0x04000F41 RID: 3905
			internal const string OleDb_NoProviderSpecified = "An OLE DB Provider was not specified in the ConnectionString.  An example would be, 'Provider=SQLOLEDB;'.";

			// Token: 0x04000F42 RID: 3906
			internal const string OleDb_InvalidProviderSpecified = "The OLE DB Provider specified in the ConnectionString is too long.";

			// Token: 0x04000F43 RID: 3907
			internal const string OleDb_InvalidRestrictionsDbInfoKeywords = "No restrictions are expected for the DbInfoKeywords OleDbSchemaGuid.";

			// Token: 0x04000F44 RID: 3908
			internal const string OleDb_InvalidRestrictionsDbInfoLiteral = "No restrictions are expected for the DbInfoLiterals OleDbSchemaGuid.";

			// Token: 0x04000F45 RID: 3909
			internal const string OleDb_InvalidRestrictionsSchemaGuids = "No restrictions are expected for the schema guid OleDbSchemaGuid.";

			// Token: 0x04000F46 RID: 3910
			internal const string OleDb_NotSupportedSchemaTable = "The {0} OleDbSchemaGuid is not a supported schema by the '{1}' provider.";

			// Token: 0x04000F47 RID: 3911
			internal const string OleDb_ConfigWrongNumberOfValues = "The '{0}' configuration setting has the wrong number of values.";

			// Token: 0x04000F48 RID: 3912
			internal const string OleDb_ConfigUnableToLoadXmlMetaDataFile = "Unable to load the XML file specified in configuration setting '{0}'.";

			// Token: 0x04000F49 RID: 3913
			internal const string OleDb_CommandParameterBadAccessor = "Command parameter[{0}] '{1}' is invalid.";

			// Token: 0x04000F4A RID: 3914
			internal const string OleDb_CommandParameterCantConvertValue = "Command parameter[{0}] '{1}' data value could not be converted for reasons other than sign mismatch or data overflow.";

			// Token: 0x04000F4B RID: 3915
			internal const string OleDb_CommandParameterSignMismatch = "Conversion failed for command parameter[{0}] '{1}' because the data value was signed and the type used by the provider was unsigned.";

			// Token: 0x04000F4C RID: 3916
			internal const string OleDb_CommandParameterDataOverflow = "Conversion failed for command parameter[{0}] '{1}' because the data value overflowed the type used by the provider.";

			// Token: 0x04000F4D RID: 3917
			internal const string OleDb_CommandParameterUnavailable = "Provider encountered an error while sending command parameter[{0}] '{1}' value and stopped processing.";

			// Token: 0x04000F4E RID: 3918
			internal const string OleDb_CommandParameterDefault = "Parameter[{0}] '{1}' has no default value.";

			// Token: 0x04000F4F RID: 3919
			internal const string OleDb_CommandParameterError = "Error occurred with parameter[{0}]: {1}.";

			// Token: 0x04000F50 RID: 3920
			internal const string OleDb_BadStatus_ParamAcc = "Microsoft.Data.OleDb.OleDbDataAdapter internal error: invalid parameter accessor: {0} {1}.";

			// Token: 0x04000F51 RID: 3921
			internal const string OleDb_UninitializedParameters = "Parameter[{0}]: the OleDbType property is uninitialized: OleDbType.{1}.";

			// Token: 0x04000F52 RID: 3922
			internal const string OleDb_NoProviderSupportForParameters = "The ICommandWithParameters interface is not supported by the '{0}' provider.  Command parameters are unsupported with the current provider.";

			// Token: 0x04000F53 RID: 3923
			internal const string OleDb_NoProviderSupportForSProcResetParameters = "Retrieving procedure parameter information is not supported by the '{0}' provider.";

			// Token: 0x04000F54 RID: 3924
			internal const string OleDb_CanNotDetermineDecimalSeparator = "Can not determine the server's decimal separator. Non-integer numeric literals can not be created.";

			// Token: 0x04000F55 RID: 3925
			internal const string OleDb_Fill_NotADODB = "Object is not an ADODB.RecordSet or an ADODB.Record.";

			// Token: 0x04000F56 RID: 3926
			internal const string OleDb_Fill_EmptyRecordSet = "Unable to retrieve the '{0}' interface from the ADODB.RecordSet object.";

			// Token: 0x04000F57 RID: 3927
			internal const string OleDb_Fill_EmptyRecord = "Unable to retrieve the IRow interface from the ADODB.Record object.";

			// Token: 0x04000F58 RID: 3928
			internal const string OleDb_ISourcesRowsetNotSupported = "Type does not support the OLE DB interface ISourcesRowset";

			// Token: 0x04000F59 RID: 3929
			internal const string OleDb_IDBInfoNotSupported = "Cannot construct the ReservedWords schema collection because the provider does not support IDBInfo.";

			// Token: 0x04000F5A RID: 3930
			internal const string OleDb_PropertyNotSupported = "The property's value was not set because the provider did not support the '{0}' property, or the consumer attempted to get or set values of properties not in the Initialization property group and the data source object is uninitialized.";

			// Token: 0x04000F5B RID: 3931
			internal const string OleDb_PropertyBadValue = "Failed to initialize the '{0}' property for one of the following reasons:\n\tThe value data type was not the data type of the property or was not null. For example, the property was DBPROP_MEMORYUSAGE, which has a data type of Int32, and the data type was Int64.\n\tThe value was not a valid value. For example, the property was DBPROP_MEMORYUSAGE and the value was negative.\n\tThe value was a valid value for the property and the provider supports the property as a settable property, but the provider does not support the value specified. This includes the case where the value was added to the property in OLE DB after the provider was written.";

			// Token: 0x04000F5C RID: 3932
			internal const string OleDb_PropertyBadOption = "The value of Options was invalid.";

			// Token: 0x04000F5D RID: 3933
			internal const string OleDb_PropertyBadColumn = "The ColumnID element was invalid.";

			// Token: 0x04000F5E RID: 3934
			internal const string OleDb_PropertyNotAllSettable = "A '{0}' property was specified to be applied to all columns but could not be applied to one or more of them.";

			// Token: 0x04000F5F RID: 3935
			internal const string OleDb_PropertyNotSettable = "The '{0}' property was read-only, or the consumer attempted to set values of properties in the Initialization property group after the data source object was initialized. Consumers can set the value of a read-only property to its current value. This status is also returned if a settable column property could not be set for the particular column.";

			// Token: 0x04000F60 RID: 3936
			internal const string OleDb_PropertyNotSet = "The optional '{0}' property's value was not set to the specified value and setting the property to the specified value was not possible.";

			// Token: 0x04000F61 RID: 3937
			internal const string OleDb_PropertyConflicting = "The '{0}'property's value was not set because doing so would have conflicted with an existing property.";

			// Token: 0x04000F62 RID: 3938
			internal const string OleDb_PropertyNotAvailable = "(Reserved).";

			// Token: 0x04000F63 RID: 3939
			internal const string OleDb_PropertyStatusUnknown = "The provider returned an unknown DBPROPSTATUS_ value '{0}'.";

			// Token: 0x04000F64 RID: 3940
			internal const string OleDb_BadAccessor = "Accessor validation was deferred and was performed while the method returned data. The binding was invalid for this column or parameter.";

			// Token: 0x04000F65 RID: 3941
			internal const string OleDb_BadStatusRowAccessor = "OleDbDataAdapter internal error: invalid row set accessor: Ordinal={0} Status={1}.";

			// Token: 0x04000F66 RID: 3942
			internal const string OleDb_CantConvertValue = "The data value could not be converted for reasons other than sign mismatch or data overflow. For example, the data was corrupted in the data store but the row was still retrievable.";

			// Token: 0x04000F67 RID: 3943
			internal const string OleDb_CantCreate = "The provider could not allocate memory in which to return {0} data.";

			// Token: 0x04000F68 RID: 3944
			internal const string OleDb_DataOverflow = "Conversion failed because the {0} data value overflowed the type specified for the {0} value part in the consumer's buffer.";

			// Token: 0x04000F69 RID: 3945
			internal const string OleDb_GVtUnknown = "OleDbDataAdapter internal error: [get] Unknown OLE DB data type: 0x{0} ({1}).";

			// Token: 0x04000F6A RID: 3946
			internal const string OleDb_SignMismatch = "Conversion failed because the {0} data value was signed and the type specified for the {0} value part in the consumer's buffer was unsigned.";

			// Token: 0x04000F6B RID: 3947
			internal const string OleDb_SVtUnknown = "OleDbDataAdapter internal error: [set] Unknown OLE DB data type: 0x{0} ({1}).";

			// Token: 0x04000F6C RID: 3948
			internal const string OleDb_Unavailable = "The provider could not determine the {0} value. For example, the row was just created, the default for the {0} column was not available, and the consumer had not yet set a new {0} value.";

			// Token: 0x04000F6D RID: 3949
			internal const string OleDb_UnexpectedStatusValue = "OLE DB Provider returned an unexpected status value of {0}.";

			// Token: 0x04000F6E RID: 3950
			internal const string OleDb_ThreadApartmentState = "The OleDbDataReader.Read must be used from the same thread on which is was created if that thread's ApartmentState was not ApartmentState.MTA.";

			// Token: 0x04000F6F RID: 3951
			internal const string OleDb_NoErrorMessage = "Unspecified error: {0}";

			// Token: 0x04000F70 RID: 3952
			internal const string OleDb_FailedGetDescription = "IErrorInfo.GetDescription failed with {0}.";

			// Token: 0x04000F71 RID: 3953
			internal const string OleDb_FailedGetSource = "IErrorInfo.GetSource failed with {0}.";

			// Token: 0x04000F72 RID: 3954
			internal const string OleDb_DBBindingGetVector = "DBTYPE_VECTOR data is not supported by the .NET Framework Data OLE DB Provider(Microsoft.Data.OleDb).";

			// Token: 0x04000F73 RID: 3955
			internal const string ADP_InvalidMinMaxPoolSizeValues = "Invalid min or max pool size values, min pool size cannot be greater than the max pool size.";

			// Token: 0x04000F74 RID: 3956
			internal const string ADP_ObsoleteKeyword = "The '{0}' keyword is obsolete. Use '{1}' instead.";

			// Token: 0x04000F75 RID: 3957
			internal const string SQL_CannotGetDTCAddress = "Unable to get the address of the distributed transaction coordinator for the server, from the server.  Is DTC enabled on the server?";

			// Token: 0x04000F76 RID: 3958
			internal const string SQL_InvalidOptionLength = "The length of the value for the connection parameter <{0}> exceeds the maximum allowed 65535 characters.";

			// Token: 0x04000F77 RID: 3959
			internal const string SQL_InvalidPacketSizeValue = "Invalid 'Packet Size'.  The value must be an integer >= 512 and <= 32768.";

			// Token: 0x04000F78 RID: 3960
			internal const string SQL_NullEmptyTransactionName = "Invalid transaction or invalid name for a point at which to save within the transaction.";

			// Token: 0x04000F79 RID: 3961
			internal const string SQL_SnapshotNotSupported = "The {0} enumeration value, {1}, is not supported by SQL Server 7.0 or SQL Server 2000.";

			// Token: 0x04000F7A RID: 3962
			internal const string SQL_UserInstanceFailoverNotCompatible = "User Instance and Failover are not compatible options.  Please choose only one of the two in the connection string.";

			// Token: 0x04000F7B RID: 3963
			internal const string SQL_AuthenticationAndIntegratedSecurity = "Cannot use 'Authentication' with 'Integrated Security'.";

			// Token: 0x04000F7C RID: 3964
			internal const string SQL_IntegratedWithPassword = "Cannot use 'Authentication=Active Directory Integrated' with 'Password' or 'PWD' connection string keywords.";

			// Token: 0x04000F7D RID: 3965
			internal const string SQL_InteractiveWithPassword = "Cannot use 'Authentication=Active Directory Interactive' with 'Password' or 'PWD' connection string keywords.";

			// Token: 0x04000F7E RID: 3966
			internal const string SQL_DeviceFlowWithUsernamePassword = "Cannot use 'Authentication=Active Directory Device Code Flow' with 'User ID', 'UID', 'Password' or 'PWD' connection string keywords.";

			// Token: 0x04000F7F RID: 3967
			internal const string SQL_NonInteractiveWithPassword = "Cannot use 'Authentication={0}' with 'Password' or 'PWD' connection string keywords.";

			// Token: 0x04000F80 RID: 3968
			internal const string SQL_SettingIntegratedWithCredential = "Cannot use 'Authentication=Active Directory Integrated', if the Credential property has been set.";

			// Token: 0x04000F81 RID: 3969
			internal const string SQL_SettingCredentialWithIntegrated = "Cannot set the Credential property if 'Authentication=Active Directory Integrated' has been specified in the connection string.";

			// Token: 0x04000F82 RID: 3970
			internal const string SQL_Certificate = "Incorrect authentication parameters specified with certificate authentication.";

			// Token: 0x04000F83 RID: 3971
			internal const string SQL_EncryptionNotSupportedByClient = "The instance of SQL Server you attempted to connect to requires encryption but this machine does not support it.";

			// Token: 0x04000F84 RID: 3972
			internal const string SQL_EncryptionNotSupportedByServer = "The instance of SQL Server you attempted to connect to does not support encryption.";

			// Token: 0x04000F85 RID: 3973
			internal const string SQL_CTAIPNotSupportedByServer = "The instance of SQL Server you attempted to connect to does not support CTAIP.";

			// Token: 0x04000F86 RID: 3974
			internal const string SQL_InvalidSQLServerVersionUnknown = "Unsupported SQL Server version.  The .NET Framework SqlClient Data Provider can only be used with SQL Server versions 7.0 and later.";

			// Token: 0x04000F87 RID: 3975
			internal const string SQL_CannotModifyPropertyAsyncOperationInProgress = "{0} cannot be changed while async operation is in progress.";

			// Token: 0x04000F88 RID: 3976
			internal const string SQL_AsyncConnectionRequired = "This command requires an asynchronous connection. Set \"Asynchronous Processing=true\" in the connection string.";

			// Token: 0x04000F89 RID: 3977
			internal const string SQL_FatalTimeout = "Timeout expired.  The connection has been broken as a result.";

			// Token: 0x04000F8A RID: 3978
			internal const string SQL_InstanceFailure = "Instance failure.";

			// Token: 0x04000F8B RID: 3979
			internal const string SQL_CredentialsNotProvided = "Either Credential or both 'User ID' and 'Password' (or 'UID' and 'PWD') connection string keywords must be specified, if 'Authentication={0}'.";

			// Token: 0x04000F8C RID: 3980
			internal const string SQL_ChangePasswordArgumentMissing = "The '{0}' argument must not be null or empty.";

			// Token: 0x04000F8D RID: 3981
			internal const string SQL_ChangePasswordConflictsWithSSPI = "ChangePassword can only be used with SQL authentication, not with integrated security.";

			// Token: 0x04000F8E RID: 3982
			internal const string SQL_ChangePasswordUseOfUnallowedKey = "The keyword '{0}' must not be specified in the connectionString argument to ChangePassword.";

			// Token: 0x04000F8F RID: 3983
			internal const string SQL_UnknownSysTxIsolationLevel = "Unrecognized System.Transactions.IsolationLevel enumeration value: {0}.";

			// Token: 0x04000F90 RID: 3984
			internal const string SQL_InvalidPartnerConfiguration = "Server {0}, database {1} is not configured for database mirroring.";

			// Token: 0x04000F91 RID: 3985
			internal const string SQL_MarsUnsupportedOnConnection = "The connection does not support MultipleActiveResultSets.";

			// Token: 0x04000F92 RID: 3986
			internal const string SQL_MSALFailure = "Failed to authenticate the user {0} in Active Directory (Authentication={1}).";

			// Token: 0x04000F93 RID: 3987
			internal const string SQL_MSALInnerException = "Error code 0x{0}";

			// Token: 0x04000F94 RID: 3988
			internal const string SQL_ChangePasswordRequiresYukon = "ChangePassword requires SQL Server 9.0 or later.";

			// Token: 0x04000F95 RID: 3989
			internal const string SQL_NonLocalSSEInstance = "SSE Instance re-direction is not supported for non-local user instances.";

			// Token: 0x04000F96 RID: 3990
			internal const string SQL_UnsupportedAuthentication = "The authentication '{0}' is not supported.";

			// Token: 0x04000F97 RID: 3991
			internal const string SQL_UnsupportedSqlAuthenticationMethod = "SQL authentication method '{0}' is not supported.";

			// Token: 0x04000F98 RID: 3992
			internal const string SQL_CannotCreateAuthProvider = "Failed to instantiate an authentication provider with type '{1}' for '{0}'.";

			// Token: 0x04000F99 RID: 3993
			internal const string SQL_CannotCreateAuthInitializer = "Failed to instantiate a SqlAuthenticationInitializer with type '{0}'.";

			// Token: 0x04000F9A RID: 3994
			internal const string SQL_CannotInitializeAuthProvider = "The provider '{0}' threw an exception while initializing.";

			// Token: 0x04000F9B RID: 3995
			internal const string SQL_UnsupportedAuthenticationByProvider = "The provider '{0}' does not support authentication '{1}'.";

			// Token: 0x04000F9C RID: 3996
			internal const string SQL_CannotFindAuthProvider = "Cannot find an authentication provider for '{0}'.";

			// Token: 0x04000F9D RID: 3997
			internal const string SQL_CannotGetAuthProviderConfig = "Failed to read the config section for authentication providers.";

			// Token: 0x04000F9E RID: 3998
			internal const string SQL_ParameterCannotBeEmpty = "Parameter '{0}' cannot be null or empty.";

			// Token: 0x04000F9F RID: 3999
			internal const string SQL_AsyncOperationCompleted = "The asynchronous operation has already completed.";

			// Token: 0x04000FA0 RID: 4000
			internal const string SQL_PendingBeginXXXExists = "The command execution cannot proceed due to a pending asynchronous operation already in progress.";

			// Token: 0x04000FA1 RID: 4001
			internal const string SQL_NonXmlResult = "Invalid command sent to ExecuteXmlReader.  The command must return an Xml result.";

			// Token: 0x04000FA2 RID: 4002
			internal const string SQL_NotificationsRequireYukon = "Notifications require SQL Server 9.0 or later.";

			// Token: 0x04000FA3 RID: 4003
			internal const string SQL_InvalidUdt3PartNameFormat = "Invalid 3 part name format for UdtTypeName.";

			// Token: 0x04000FA4 RID: 4004
			internal const string SQL_InvalidParameterTypeNameFormat = "Invalid 3 part name format for TypeName.";

			// Token: 0x04000FA5 RID: 4005
			internal const string SQL_InvalidParameterNameLength = "The length of the parameter '{0}' exceeds the limit of 128 characters.";

			// Token: 0x04000FA6 RID: 4006
			internal const string SQL_PrecisionValueOutOfRange = "Precision value '{0}' is either less than 0 or greater than the maximum allowed precision of 38.";

			// Token: 0x04000FA7 RID: 4007
			internal const string SQL_ScaleValueOutOfRange = "Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 38.";

			// Token: 0x04000FA8 RID: 4008
			internal const string SQL_TimeScaleValueOutOfRange = "Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 7.";

			// Token: 0x04000FA9 RID: 4009
			internal const string SQL_ParameterInvalidVariant = "Parameter '{0}' exceeds the size limit for the sql_variant datatype.";

			// Token: 0x04000FAA RID: 4010
			internal const string SQL_ParameterTypeNameRequired = "The {0} type parameter '{1}' must have a valid type name.";

			// Token: 0x04000FAB RID: 4011
			internal const string SQL_InvalidInternalPacketSize = "Invalid internal packet size:";

			// Token: 0x04000FAC RID: 4012
			internal const string SQL_InvalidTDSVersion = "The SQL Server instance returned an invalid or unsupported protocol version during login negotiation.";

			// Token: 0x04000FAD RID: 4013
			internal const string SQL_InvalidTDSPacketSize = "Invalid Packet Size.";

			// Token: 0x04000FAE RID: 4014
			internal const string SQL_ParsingError = "Internal connection fatal error.";

			// Token: 0x04000FAF RID: 4015
			internal const string SQL_ParsingErrorWithState = "Internal connection fatal error. Error state: {0}";

			// Token: 0x04000FB0 RID: 4016
			internal const string SQL_ParsingErrorValue = "Internal connection fatal error. Error state: {0}, Value: {1}";

			// Token: 0x04000FB1 RID: 4017
			internal const string SQL_ParsingErrorOffset = "Internal connection fatal error. Error state: {0}, Offset: {1}";

			// Token: 0x04000FB2 RID: 4018
			internal const string SQL_ParsingErrorFeatureId = "Internal connection fatal error. Error state: {0}, Feature Id: {1}";

			// Token: 0x04000FB3 RID: 4019
			internal const string SQL_ParsingErrorToken = "Internal connection fatal error. Error state: {0}, Token : {1}";

			// Token: 0x04000FB4 RID: 4020
			internal const string SQL_ParsingErrorLength = "Internal connection fatal error. Error state: {0}, Length: {1}";

			// Token: 0x04000FB5 RID: 4021
			internal const string SQL_ParsingErrorStatus = "Internal connection fatal error. Error state: {0}, Status: {1}";

			// Token: 0x04000FB6 RID: 4022
			internal const string SQL_ParsingErrorAuthLibraryType = "Internal connection fatal error. Error state: {0}, Authentication Library Type: {1}";

			// Token: 0x04000FB7 RID: 4023
			internal const string SQL_ConnectionLockedForBcpEvent = "The connection cannot be used because there is an ongoing operation that must be finished.";

			// Token: 0x04000FB8 RID: 4024
			internal const string SQL_SNIPacketAllocationFailure = "Memory allocation for internal connection failed.";

			// Token: 0x04000FB9 RID: 4025
			internal const string SQL_SmallDateTimeOverflow = "SqlDbType.SmallDateTime overflow.  Value '{0}' is out of range.  Must be between 1/1/1900 12:00:00 AM and 6/6/2079 11:59:59 PM.";

			// Token: 0x04000FBA RID: 4026
			internal const string SQL_TimeOverflow = "SqlDbType.Time overflow.  Value '{0}' is out of range.  Must be between 00:00:00.0000000 and 23:59:59.9999999.";

			// Token: 0x04000FBB RID: 4027
			internal const string SQL_MoneyOverflow = "SqlDbType.SmallMoney overflow.  Value '{0}' is out of range.  Must be between -214,748.3648 and 214,748.3647.";

			// Token: 0x04000FBC RID: 4028
			internal const string SQL_CultureIdError = "The Collation specified by SQL Server is not supported.";

			// Token: 0x04000FBD RID: 4029
			internal const string SQL_OperationCancelled = "Operation cancelled by user.";

			// Token: 0x04000FBE RID: 4030
			internal const string SQL_SevereError = "A severe error occurred on the current command.  The results, if any, should be discarded.";

			// Token: 0x04000FBF RID: 4031
			internal const string SQL_SSPIGenerateError = "The target principal name is incorrect.  Cannot generate SSPI context.";

			// Token: 0x04000FC0 RID: 4032
			internal const string SQL_InvalidSSPIPacketSize = "Invalid SSPI packet size.";

			// Token: 0x04000FC1 RID: 4033
			internal const string SQL_SSPIInitializeError = "Cannot initialize SSPI package.";

			// Token: 0x04000FC2 RID: 4034
			internal const string SQL_Timeout = "Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.";

			// Token: 0x04000FC3 RID: 4035
			internal const string SQL_Timeout_PreLogin_Begin = "Connection Timeout Expired.  The timeout period elapsed at the start of the pre-login phase.  This could be because of insufficient time provided for connection timeout.";

			// Token: 0x04000FC4 RID: 4036
			internal const string SQL_Timeout_PreLogin_InitializeConnection = "Connection Timeout Expired.  The timeout period elapsed while attempting to create and initialize a socket to the server.  This could be either because the server was unreachable or unable to respond back in time.";

			// Token: 0x04000FC5 RID: 4037
			internal const string SQL_Timeout_PreLogin_SendHandshake = "Connection Timeout Expired.  The timeout period elapsed while making a pre-login handshake request.  This could be because the server was unable to respond back in time.";

			// Token: 0x04000FC6 RID: 4038
			internal const string SQL_Timeout_PreLogin_ConsumeHandshake = "Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement.  This could be because the pre-login handshake failed or the server was unable to respond back in time.";

			// Token: 0x04000FC7 RID: 4039
			internal const string SQL_Timeout_Login_Begin = "Connection Timeout Expired.  The timeout period elapsed at the start of the login phase.  This could be because of insufficient time provided for connection timeout.";

			// Token: 0x04000FC8 RID: 4040
			internal const string SQL_Timeout_Login_ProcessConnectionAuth = "Connection Timeout Expired.  The timeout period elapsed while attempting to authenticate the login.  This could be because the server failed to authenticate the user or the server was unable to respond back in time.";

			// Token: 0x04000FC9 RID: 4041
			internal const string SQL_Timeout_PostLogin = "Connection Timeout Expired.  The timeout period elapsed during the post-login phase.  The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.";

			// Token: 0x04000FCA RID: 4042
			internal const string SQL_Timeout_FailoverInfo = "This failure occurred while attempting to connect to the {0} server.";

			// Token: 0x04000FCB RID: 4043
			internal const string SQL_Timeout_RoutingDestinationInfo = "This failure occurred while attempting to connect to the routing destination. The duration spent while attempting to connect to the original server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4};  ";

			// Token: 0x04000FCC RID: 4044
			internal const string SQL_Duration_PreLogin_Begin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0};";

			// Token: 0x04000FCD RID: 4045
			internal const string SQL_Duration_PreLoginHandshake = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; ";

			// Token: 0x04000FCE RID: 4046
			internal const string SQL_Duration_Login_Begin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; ";

			// Token: 0x04000FCF RID: 4047
			internal const string SQL_Duration_Login_ProcessConnectionAuth = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; ";

			// Token: 0x04000FD0 RID: 4048
			internal const string SQL_Duration_PostLogin = "The duration spent while attempting to connect to this server was - [Pre-Login] initialization={0}; handshake={1}; [Login] initialization={2}; authentication={3}; [Post-Login] complete={4}; ";

			// Token: 0x04000FD1 RID: 4049
			internal const string SQL_UserInstanceFailure = "A user instance was requested in the connection string but the server specified does not support this option.";

			// Token: 0x04000FD2 RID: 4050
			internal const string SQL_InvalidServerCertificate = "The server certificate failed application validation.";

			// Token: 0x04000FD3 RID: 4051
			internal const string SQL_ExceedsMaxDataLength = "Specified data length {0} exceeds the allowed maximum length of {1}.";

			// Token: 0x04000FD4 RID: 4052
			internal const string SQL_InvalidRead = "Invalid attempt to read when no data is present.";

			// Token: 0x04000FD5 RID: 4053
			internal const string SQL_NonBlobColumn = "Invalid attempt to GetBytes on column '{0}'.  The GetBytes function can only be used on columns of type Text, NText, or Image.";

			// Token: 0x04000FD6 RID: 4054
			internal const string SQL_NonCharColumn = "Invalid attempt to GetChars on column '{0}'.  The GetChars function can only be used on columns of type Text, NText, Xml, VarChar or NVarChar.";

			// Token: 0x04000FD7 RID: 4055
			internal const string SQL_StreamNotSupportOnColumnType = "Invalid attempt to GetStream on column '{0}'. The GetStream function can only be used on columns of type Binary, Image, Udt or VarBinary.";

			// Token: 0x04000FD8 RID: 4056
			internal const string SQL_TextReaderNotSupportOnColumnType = "Invalid attempt to GetTextReader on column '{0}'. The GetTextReader function can only be used on columns of type Char, NChar, NText, NVarChar, Text or VarChar.";

			// Token: 0x04000FD9 RID: 4057
			internal const string SQL_XmlReaderNotSupportOnColumnType = "Invalid attempt to GetXmlReader on column '{0}'. The GetXmlReader function can only be used on columns of type Xml.";

			// Token: 0x04000FDA RID: 4058
			internal const string SQL_InvalidBufferSizeOrIndex = "Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.";

			// Token: 0x04000FDB RID: 4059
			internal const string SQL_InvalidDataLength = "Data length '{0}' is less than 0.";

			// Token: 0x04000FDC RID: 4060
			internal const string SQL_SqlResultSetClosed = "Invalid attempt to call method {0} when SqlResultSet is closed.";

			// Token: 0x04000FDD RID: 4061
			internal const string SQL_SqlResultSetClosed2 = "Operation cannot be completed because the SqlResultSet is closed.";

			// Token: 0x04000FDE RID: 4062
			internal const string SQL_SqlRecordReadOnly = "'{0}' cannot be called when the record is read only.";

			// Token: 0x04000FDF RID: 4063
			internal const string SQL_SqlRecordReadOnly2 = "Operation cannot be completed because the record is read only.";

			// Token: 0x04000FE0 RID: 4064
			internal const string SQL_SqlResultSetRowDeleted = "Invalid attempt to call method {0} when the current row is deleted";

			// Token: 0x04000FE1 RID: 4065
			internal const string SQL_SqlResultSetRowDeleted2 = "Operation cannot be completed because the current row is deleted";

			// Token: 0x04000FE2 RID: 4066
			internal const string SQL_SqlResultSetCommandNotInSameConnection = "Operation cannot be completed because the command that created the SqlResultSet has been dissociated from the original connection. SqlResultSet is closed.";

			// Token: 0x04000FE3 RID: 4067
			internal const string SQL_SqlResultSetNoAcceptableCursor = "SqlResultSet could not be created for the given query with the desired options.";

			// Token: 0x04000FE4 RID: 4068
			internal const string SQL_SqlUpdatableRecordReadOnly = "'{0}' cannot be called when the SqlDataRecord is read only.";

			// Token: 0x04000FE5 RID: 4069
			internal const string SQL_BulkLoadMappingInaccessible = "The mapped collection is in use and cannot be accessed at this time;";

			// Token: 0x04000FE6 RID: 4070
			internal const string SQL_BulkLoadMappingsNamesOrOrdinalsOnly = "Mappings must be either all name or all ordinal based.";

			// Token: 0x04000FE7 RID: 4071
			internal const string SQL_BulkLoadCannotConvertValue = "The given value{0} of type {1} from the data source cannot be converted to type {2} for Column {3} [{4}] Row {5}.";

			// Token: 0x04000FE8 RID: 4072
			internal const string SQL_BulkLoadCannotConvertValueWithoutRowNo = "The given value{0} of type {1} from the data source cannot be converted to type {2} for Column {3} [{4}].";

			// Token: 0x04000FE9 RID: 4073
			internal const string SQL_BulkLoadNonMatchingColumnMapping = "The given ColumnMapping does not match up with any column in the source or destination.";

			// Token: 0x04000FEA RID: 4074
			internal const string SQL_BulkLoadNonMatchingColumnName = "The given ColumnName '{0}' does not match up with any column in data source.";

			// Token: 0x04000FEB RID: 4075
			internal const string SQL_BulkLoadStringTooLong = "String or binary data would be truncated in table '{0}', column '{1}'. Truncated value: '{2}'.";

			// Token: 0x04000FEC RID: 4076
			internal const string SQL_BulkLoadInvalidTimeout = "Timeout Value '{0}' is less than 0.";

			// Token: 0x04000FED RID: 4077
			internal const string SQL_BulkLoadInvalidVariantValue = "Value cannot be converted to SqlVariant.";

			// Token: 0x04000FEE RID: 4078
			internal const string SQL_BulkLoadExistingTransaction = "Unexpected existing transaction.";

			// Token: 0x04000FEF RID: 4079
			internal const string SQL_BulkLoadNoCollation = "Failed to obtain column collation information for the destination table. If the table is not in the current database the name must be qualified using the database name (e.g. [mydb]..[mytable](e.g. [mydb]..[mytable]); this also applies to temporary-tables (e.g. #mytable would be specified as tempdb..#mytable).";

			// Token: 0x04000FF0 RID: 4080
			internal const string SQL_BulkLoadConflictingTransactionOption = "Must not specify SqlBulkCopyOption.UseInternalTransaction and pass an external Transaction at the same time.";

			// Token: 0x04000FF1 RID: 4081
			internal const string SQL_BulkLoadInvalidOperationInsideEvent = "Function must not be called during event.";

			// Token: 0x04000FF2 RID: 4082
			internal const string SQL_BulkLoadMissingDestinationTable = "The DestinationTableName property must be set before calling this method.";

			// Token: 0x04000FF3 RID: 4083
			internal const string SQL_BulkLoadInvalidDestinationTable = "Cannot access destination table '{0}'.";

			// Token: 0x04000FF4 RID: 4084
			internal const string SQL_BulkLoadNotAllowDBNull = "Column '{0}' does not allow DBNull.Value.";

			// Token: 0x04000FF5 RID: 4085
			internal const string Sql_BulkLoadLcidMismatch = "The locale id '{0}' of the source column '{1}' and the locale id '{2}' of the destination column '{3}' do not match.";

			// Token: 0x04000FF6 RID: 4086
			internal const string SQL_BulkLoadPendingOperation = "Attempt to invoke bulk copy on an object that has a pending operation.";

			// Token: 0x04000FF7 RID: 4087
			internal const string SQL_ConnectionDoomed = "The requested operation cannot be completed because the connection has been broken.";

			// Token: 0x04000FF8 RID: 4088
			internal const string SQL_OpenResultCountExceeded = "Open result count exceeded.";

			// Token: 0x04000FF9 RID: 4089
			internal const string GT_Disabled = "Global Transactions are not enabled for this Azure SQL Database. Please contact Azure SQL Database support for assistance.";

			// Token: 0x04000FFA RID: 4090
			internal const string GT_UnsupportedSysTxVersion = "The currently loaded System.Transactions.dll does not support Global Transactions. Please upgrade to .NET Framework 4.6.2 or later.";

			// Token: 0x04000FFB RID: 4091
			internal const string SQL_BatchedUpdatesNotAvailableOnContextConnection = "Batching updates is not supported on the context connection.";

			// Token: 0x04000FFC RID: 4092
			internal const string SQL_ContextAllowsLimitedKeywords = "The only additional connection string keyword that may be used when requesting the context connection is the Type System Version keyword.";

			// Token: 0x04000FFD RID: 4093
			internal const string SQL_ContextAllowsOnlyTypeSystem2005 = "The context connection does not support Type System Version=SQL Server 2000.";

			// Token: 0x04000FFE RID: 4094
			internal const string SQL_ContextConnectionIsInUse = "The context connection is already in use.";

			// Token: 0x04000FFF RID: 4095
			internal const string SQL_ContextUnavailableOutOfProc = "The requested operation requires a SqlClr context, which is only available when running in the Sql Server process.";

			// Token: 0x04001000 RID: 4096
			internal const string SQL_ContextUnavailableWhileInProc = "The requested operation requires a Sql Server execution thread.  The current thread was started by user code or other non-Sql Server engine code.";

			// Token: 0x04001001 RID: 4097
			internal const string SQL_NestedTransactionScopesNotSupported = "Nested TransactionScopes are not supported.";

			// Token: 0x04001002 RID: 4098
			internal const string SQL_NotAvailableOnContextConnection = "The requested operation is not available on the context connection.";

			// Token: 0x04001003 RID: 4099
			internal const string SQL_NotificationsNotAvailableOnContextConnection = "Notifications are not available on the context connection.";

			// Token: 0x04001004 RID: 4100
			internal const string SQL_UnexpectedSmiEvent = "Unexpected server event: {0}.";

			// Token: 0x04001005 RID: 4101
			internal const string SQL_UserInstanceNotAvailableInProc = "User instances are not allowed when running in the Sql Server process.";

			// Token: 0x04001006 RID: 4102
			internal const string SQL_ArgumentLengthMismatch = "The length of '{0}' must match the length of '{1}'.";

			// Token: 0x04001007 RID: 4103
			internal const string SQL_InvalidSqlDbTypeWithOneAllowedType = "The SqlDbType '{0}' is invalid for {1}.  Only {2} is supported.";

			// Token: 0x04001008 RID: 4104
			internal const string SQL_PipeErrorRequiresSendEnd = "An error occurred with a prior row sent to the SqlPipe.  SendResultsEnd must be called before anything else can be sent.";

			// Token: 0x04001009 RID: 4105
			internal const string SQL_TooManyValues = "Too many values.";

			// Token: 0x0400100A RID: 4106
			internal const string SQL_StreamWriteNotSupported = "The Stream does not support writing.";

			// Token: 0x0400100B RID: 4107
			internal const string SQL_StreamReadNotSupported = "The Stream does not support reading.";

			// Token: 0x0400100C RID: 4108
			internal const string SQL_StreamSeekNotSupported = "The Stream does not support seeking.";

			// Token: 0x0400100D RID: 4109
			internal const string SQL_ExClientConnectionId = "ClientConnectionId:{0}";

			// Token: 0x0400100E RID: 4110
			internal const string SQL_ExErrorNumberStateClass = "Error Number:{0},State:{1},Class:{2}";

			// Token: 0x0400100F RID: 4111
			internal const string SQL_ExOriginalClientConnectionId = "ClientConnectionId before routing:{0}";

			// Token: 0x04001010 RID: 4112
			internal const string SQL_ExRoutingDestination = "Routing Destination:{0}";

			// Token: 0x04001011 RID: 4113
			internal const string SqlMisc_NullString = "Null";

			// Token: 0x04001012 RID: 4114
			internal const string SqlMisc_MessageString = "Message";

			// Token: 0x04001013 RID: 4115
			internal const string SqlMisc_ArithOverflowMessage = "Arithmetic Overflow.";

			// Token: 0x04001014 RID: 4116
			internal const string SqlMisc_DivideByZeroMessage = "Divide by zero error encountered.";

			// Token: 0x04001015 RID: 4117
			internal const string SqlMisc_NullValueMessage = "Data is Null. This method or property cannot be called on Null values.";

			// Token: 0x04001016 RID: 4118
			internal const string SqlMisc_TruncationMessage = "Numeric arithmetic causes truncation.";

			// Token: 0x04001017 RID: 4119
			internal const string SqlMisc_DateTimeOverflowMessage = "SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.";

			// Token: 0x04001018 RID: 4120
			internal const string SqlMisc_ConcatDiffCollationMessage = "Two strings to be concatenated have different collation.";

			// Token: 0x04001019 RID: 4121
			internal const string SqlMisc_CompareDiffCollationMessage = "Two strings to be compared have different collation.";

			// Token: 0x0400101A RID: 4122
			internal const string SqlMisc_InvalidFlagMessage = "Invalid flag value.";

			// Token: 0x0400101B RID: 4123
			internal const string SqlMisc_NumeToDecOverflowMessage = "Conversion from SqlDecimal to Decimal overflows.";

			// Token: 0x0400101C RID: 4124
			internal const string SqlMisc_ConversionOverflowMessage = "Conversion overflows.";

			// Token: 0x0400101D RID: 4125
			internal const string SqlMisc_InvalidDateTimeMessage = "Invalid SqlDateTime.";

			// Token: 0x0400101E RID: 4126
			internal const string SqlMisc_TimeZoneSpecifiedMessage = "A time zone was specified. SqlDateTime does not support time zones.";

			// Token: 0x0400101F RID: 4127
			internal const string SqlMisc_InvalidArraySizeMessage = "Invalid array size.";

			// Token: 0x04001020 RID: 4128
			internal const string SqlMisc_InvalidPrecScaleMessage = "Invalid numeric precision/scale.";

			// Token: 0x04001021 RID: 4129
			internal const string SqlMisc_FormatMessage = "The input wasn't in a correct format.";

			// Token: 0x04001022 RID: 4130
			internal const string SqlMisc_SqlTypeMessage = "SqlType error.";

			// Token: 0x04001023 RID: 4131
			internal const string SqlMisc_LenTooLargeMessage = "The SqlBytes and SqlChars don't support length of more than 2GB in this version.";

			// Token: 0x04001024 RID: 4132
			internal const string SqlMisc_StreamErrorMessage = "An error occurred while reading.";

			// Token: 0x04001025 RID: 4133
			internal const string SqlMisc_StreamClosedMessage = "Stream has been closed or disposed.";

			// Token: 0x04001026 RID: 4134
			internal const string SqlMisc_NoBufferMessage = "There is no buffer. Read or write operation failed.";

			// Token: 0x04001027 RID: 4135
			internal const string SqlMisc_SetNonZeroLenOnNullMessage = "Cannot set to non-zero length, because current value is Null.";

			// Token: 0x04001028 RID: 4136
			internal const string SqlMisc_BufferInsufficientMessage = "The buffer is insufficient. Read or write operation failed.";

			// Token: 0x04001029 RID: 4137
			internal const string SqlMisc_WriteNonZeroOffsetOnNullMessage = "Cannot write to non-zero offset, because current value is Null.";

			// Token: 0x0400102A RID: 4138
			internal const string SqlMisc_WriteOffsetLargerThanLenMessage = "Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.";

			// Token: 0x0400102B RID: 4139
			internal const string SqlMisc_TruncationMaxDataMessage = "Data returned is larger than 2Gb in size. Use SequentialAccess command behavior in order to get all of the data.";

			// Token: 0x0400102C RID: 4140
			internal const string SqlMisc_InvalidFirstDayMessage = "Argument to GetDayOfWeek must be integer between 1 and 7.";

			// Token: 0x0400102D RID: 4141
			internal const string SqlMisc_NotFilledMessage = "SQL Type has not been loaded with data.";

			// Token: 0x0400102E RID: 4142
			internal const string SqlMisc_AlreadyFilledMessage = "SQL Type has already been loaded with data.";

			// Token: 0x0400102F RID: 4143
			internal const string SqlMisc_ClosedXmlReaderMessage = "Invalid attempt to access a closed XmlReader.";

			// Token: 0x04001030 RID: 4144
			internal const string SqlMisc_InvalidOpStreamClosed = "Invalid attempt to call {0} when the stream is closed.";

			// Token: 0x04001031 RID: 4145
			internal const string SqlMisc_InvalidOpStreamNonWritable = "Invalid attempt to call {0} when the stream non-writable.";

			// Token: 0x04001032 RID: 4146
			internal const string SqlMisc_InvalidOpStreamNonReadable = "Invalid attempt to call {0} when the stream non-readable.";

			// Token: 0x04001033 RID: 4147
			internal const string SqlMisc_InvalidOpStreamNonSeekable = "Invalid attempt to call {0} when the stream is non-seekable.";

			// Token: 0x04001034 RID: 4148
			internal const string SqlMisc_SubclassMustOverride = "Subclass did not override a required method.";

			// Token: 0x04001035 RID: 4149
			internal const string SQL_CannotCreateNormalizer = "Cannot create normalizer for '{0}'.";

			// Token: 0x04001036 RID: 4150
			internal const string Sql_InternalError = "Internal Error";

			// Token: 0x04001037 RID: 4151
			internal const string Sql_NullCommandText = "Command parameter must have a non null and non empty command text.";

			// Token: 0x04001038 RID: 4152
			internal const string Sql_MismatchedMetaDataDirectionArrayLengths = "MetaData parameter array must have length equivalent to ParameterDirection array argument.";

			// Token: 0x04001039 RID: 4153
			internal const string ADP_AdapterMappingExceptionMessage = "Data adapter mapping error.";

			// Token: 0x0400103A RID: 4154
			internal const string ADP_DataAdapterExceptionMessage = "Data adapter error.";

			// Token: 0x0400103B RID: 4155
			internal const string ADP_DBConcurrencyExceptionMessage = "DB concurrency violation.";

			// Token: 0x0400103C RID: 4156
			internal const string ADP_OperationAborted = "Operation aborted.";

			// Token: 0x0400103D RID: 4157
			internal const string ADP_OperationAbortedExceptionMessage = "Operation aborted due to an exception (see InnerException for details).";

			// Token: 0x0400103E RID: 4158
			internal const string DataAdapter_AcceptChangesDuringFill = "Whether or not Fill will call DataRow.AcceptChanges.";

			// Token: 0x0400103F RID: 4159
			internal const string DataAdapter_AcceptChangesDuringUpdate = "Whether or not Update will call DataRow.AcceptChanges.";

			// Token: 0x04001040 RID: 4160
			internal const string DataAdapter_ContinueUpdateOnError = "Whether or not to continue to the next DataRow when the Update events, RowUpdating and RowUpdated, Status is UpdateStatus.ErrorsOccurred.";

			// Token: 0x04001041 RID: 4161
			internal const string DataAdapter_FillLoadOption = "How the adapter fills the DataTable from the DataReader.";

			// Token: 0x04001042 RID: 4162
			internal const string DataAdapter_MissingMappingAction = "The action taken when a table or column in the TableMappings is missing.";

			// Token: 0x04001043 RID: 4163
			internal const string DataAdapter_MissingSchemaAction = "The action taken when a table or column in the DataSet is missing.";

			// Token: 0x04001044 RID: 4164
			internal const string DataAdapter_TableMappings = "How to map source table to DataSet table.";

			// Token: 0x04001045 RID: 4165
			internal const string DataAdapter_FillError = "Event triggered when a recoverable error occurs during Fill.";

			// Token: 0x04001046 RID: 4166
			internal const string DataAdapter_ReturnProviderSpecificTypes = "Should Fill return provider specific values or common CLSCompliant values.";

			// Token: 0x04001047 RID: 4167
			internal const string DataColumnMapping_DataSetColumn = "DataColumn.ColumnName";

			// Token: 0x04001048 RID: 4168
			internal const string DataColumnMapping_SourceColumn = "Source column name - case sensitive.";

			// Token: 0x04001049 RID: 4169
			internal const string DataColumnMappings_Count = "The number of items in the collection";

			// Token: 0x0400104A RID: 4170
			internal const string DataColumnMappings_Item = "The specified DataColumnMapping object.";

			// Token: 0x0400104B RID: 4171
			internal const string DataTableMapping_ColumnMappings = "Individual columns mappings when this table mapping is matched.";

			// Token: 0x0400104C RID: 4172
			internal const string DataTableMapping_DataSetTable = "DataTable.TableName";

			// Token: 0x0400104D RID: 4173
			internal const string DataTableMapping_SourceTable = "The DataTableMapping source table name. This name is case sensitive.";

			// Token: 0x0400104E RID: 4174
			internal const string DataTableMappings_Count = "The number of items in the collection";

			// Token: 0x0400104F RID: 4175
			internal const string DataTableMappings_Item = "The specified DataTableMapping object";

			// Token: 0x04001050 RID: 4176
			internal const string DbDataAdapter_DeleteCommand = "Used during Update for deleted rows in DataSet.";

			// Token: 0x04001051 RID: 4177
			internal const string DbDataAdapter_InsertCommand = "Used during Update for new rows in DataSet.";

			// Token: 0x04001052 RID: 4178
			internal const string DbDataAdapter_SelectCommand = "Used during Fill/FillSchema.";

			// Token: 0x04001053 RID: 4179
			internal const string DbDataAdapter_UpdateCommand = "Used during Update for modified rows in DataSet.";

			// Token: 0x04001054 RID: 4180
			internal const string DbDataAdapter_RowUpdated = "Event triggered before every DataRow during Update.";

			// Token: 0x04001055 RID: 4181
			internal const string DbDataAdapter_RowUpdating = "Event triggered after every DataRow during Update.";

			// Token: 0x04001056 RID: 4182
			internal const string DbDataAdapter_UpdateBatchSize = "Number of rows to batch together before executing against the data source.";

			// Token: 0x04001057 RID: 4183
			internal const string DbTable_Connection = "Connection used if the the Select/Insert/Update/DeleteCommands do not already have a connection.";

			// Token: 0x04001058 RID: 4184
			internal const string DbTable_DeleteCommand = "Used during Update for deleted rows in the DataTable.";

			// Token: 0x04001059 RID: 4185
			internal const string DbTable_InsertCommand = "Used during Update for new rows in the DataTable.";

			// Token: 0x0400105A RID: 4186
			internal const string DbTable_SelectCommand = "Used during Fill.";

			// Token: 0x0400105B RID: 4187
			internal const string DbTable_UpdateCommand = "Used during Update for modified rows in the DataTable.";

			// Token: 0x0400105C RID: 4188
			internal const string DbTable_ReturnProviderSpecificTypes = "Should Fill return provider specific values or common CLSCompliant values.";

			// Token: 0x0400105D RID: 4189
			internal const string DbTable_TableMapping = "How to map source table to DataTable.";

			// Token: 0x0400105E RID: 4190
			internal const string DbTable_ConflictDetection = "How are the Insert/Update/DeleteCommands generated when not set by the user.";

			// Token: 0x0400105F RID: 4191
			internal const string DbTable_UpdateBatchSize = "Number of rows to batch together before executing against the data source.";

			// Token: 0x04001060 RID: 4192
			internal const string DbConnectionString_ConnectionString = "The connection string used to connect to the Data Source.";

			// Token: 0x04001061 RID: 4193
			internal const string DbConnectionString_Driver = "The name of the ODBC Driver to use when connecting to the Data Source.";

			// Token: 0x04001062 RID: 4194
			internal const string DbConnectionString_DSN = "The DSN to use when connecting to the Data Source.";

			// Token: 0x04001063 RID: 4195
			internal const string DbConnectionString_AdoNetPooler = "When true, indicates that managed connection pooling should be used.";

			// Token: 0x04001064 RID: 4196
			internal const string DbConnectionString_FileName = "The UDL file to use when connecting to the Data Source.";

			// Token: 0x04001065 RID: 4197
			internal const string DbConnectionString_OleDbServices = "Specifies which OLE DB Services to enable or disable with the OleDb Provider.";

			// Token: 0x04001066 RID: 4198
			internal const string DbConnectionString_Provider = "The name of the OLE DB Provider to use when connecting to the Data Source.";

			// Token: 0x04001067 RID: 4199
			internal const string DbConnectionString_ApplicationName = "The name of the application.";

			// Token: 0x04001068 RID: 4200
			internal const string DbConnectionString_AsynchronousProcessing = "When true, enables usage of the Asynchronous functionality in the .NET Framework Data Provider.";

			// Token: 0x04001069 RID: 4201
			internal const string DbConnectionString_AttachDBFilename = "The name of the primary file, including the full path name, of an attachable database.";

			// Token: 0x0400106A RID: 4202
			internal const string DbConnectionString_ConnectTimeout = "The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.";

			// Token: 0x0400106B RID: 4203
			internal const string DbConnectionString_ConnectionReset = "When true, indicates the connection state is reset when removed from the pool.";

			// Token: 0x0400106C RID: 4204
			internal const string DbConnectionString_ContextConnection = "When true, indicates the connection should be from the Sql Server context.  Available only when running in the Sql Server process.";

			// Token: 0x0400106D RID: 4205
			internal const string DbConnectionString_CurrentLanguage = "The SQL Server Language record name.";

			// Token: 0x0400106E RID: 4206
			internal const string DbConnectionString_DataSource = "Indicates the name of the data source to connect to.";

			// Token: 0x0400106F RID: 4207
			internal const string DbConnectionString_Encrypt = "When true, SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.";

			// Token: 0x04001070 RID: 4208
			internal const string DbConnectionString_HostNameInCertificate = "The hostname to be expected in the server's certificate when encryption is negotiated, if it's different from the default value derived from Addr/Address/Server.";

			// Token: 0x04001071 RID: 4209
			internal const string DbConnectionString_ServerCertificate = "The path to a certificate file to match against the SQL Server TLS/SSL certificate.";

			// Token: 0x04001072 RID: 4210
			internal const string DbConnectionString_Enlist = "Sessions in a Component Services (or MTS, if you are using Microsoft Windows NT) environment should automatically be enlisted in a global transaction where required.";

			// Token: 0x04001073 RID: 4211
			internal const string DbConnectionString_InitialCatalog = "The name of the initial catalog or database in the data source.";

			// Token: 0x04001074 RID: 4212
			internal const string DbConnectionString_FailoverPartner = "The name or network address of the instance of SQL Server that acts as a failover partner.";

			// Token: 0x04001075 RID: 4213
			internal const string DbConnectionString_IntegratedSecurity = "Whether the connection is to be a secure connection or not.";

			// Token: 0x04001076 RID: 4214
			internal const string DbConnectionString_LoadBalanceTimeout = "The minimum amount of time (in seconds) for this connection to live in the pool before being destroyed.";

			// Token: 0x04001077 RID: 4215
			internal const string DbConnectionString_MaxPoolSize = "The maximum number of connections allowed in the pool.";

			// Token: 0x04001078 RID: 4216
			internal const string DbConnectionString_MinPoolSize = "The minimum number of connections allowed in the pool.";

			// Token: 0x04001079 RID: 4217
			internal const string DbConnectionString_MultipleActiveResultSets = "When true, multiple result sets can be returned and read from one connection.";

			// Token: 0x0400107A RID: 4218
			internal const string DbConnectionString_MultiSubnetFailover = "If your application is connecting to a high-availability, disaster recovery (AlwaysOn) availability group (AG) on different subnets, MultiSubnetFailover=Yes configures SqlConnection to provide faster detection of and connection to the (currently) active server.";

			// Token: 0x0400107B RID: 4219
			internal const string DbConnectionString_TransparentNetworkIPResolution = "If your application connects to different networks, TransparentNetworkIPResolution=Yes configures SqlConnection to provide transparent connection resolution to the currently active server, independently of the network IP topology.";

			// Token: 0x0400107C RID: 4220
			internal const string DbConnectionString_NetworkLibrary = "The network library used to establish a connection to an instance of SQL Server.";

			// Token: 0x0400107D RID: 4221
			internal const string DbConnectionString_PacketSize = "Size in bytes of the network packets used to communicate with an instance of SQL Server.";

			// Token: 0x0400107E RID: 4222
			internal const string DbConnectionString_Password = "Indicates the password to be used when connecting to the data source.";

			// Token: 0x0400107F RID: 4223
			internal const string DbConnectionString_PersistSecurityInfo = "When false, security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.";

			// Token: 0x04001080 RID: 4224
			internal const string DbConnectionString_Pooling = "When true, the connection object is drawn from the appropriate pool, or if necessary, is created and added to the appropriate pool.";

			// Token: 0x04001081 RID: 4225
			internal const string DbConnectionString_Replication = "Used by SQL Server in Replication.";

			// Token: 0x04001082 RID: 4226
			internal const string DbConnectionString_TransactionBinding = "Indicates binding behavior of connection to a System.Transactions Transaction when enlisted.";

			// Token: 0x04001083 RID: 4227
			internal const string DbConnectionString_TrustServerCertificate = "When true (and encrypt=true), SQL Server uses SSL encryption for all data sent between the client and server without validating the server certificate.";

			// Token: 0x04001084 RID: 4228
			internal const string DbConnectionString_TypeSystemVersion = "Indicates which server type system the provider will expose through the DataReader.";

			// Token: 0x04001085 RID: 4229
			internal const string DbConnectionString_UserID = "Indicates the user ID to be used when connecting to the data source.";

			// Token: 0x04001086 RID: 4230
			internal const string DbConnectionString_UserInstance = "Indicates whether the connection will be re-directed to connect to an instance of SQL Server running under the user's account.";

			// Token: 0x04001087 RID: 4231
			internal const string DbConnectionString_WorkstationID = "The name of the workstation connecting to SQL Server.";

			// Token: 0x04001088 RID: 4232
			internal const string DbConnectionString_ApplicationIntent = "Declares the application workload type when connecting to a server.";

			// Token: 0x04001089 RID: 4233
			internal const string DbConnectionString_ConnectRetryCount = "Number of attempts to restore connection.";

			// Token: 0x0400108A RID: 4234
			internal const string DbConnectionString_ConnectRetryInterval = "Delay between attempts to restore connection.";

			// Token: 0x0400108B RID: 4235
			internal const string DbConnectionString_Authentication = "Specifies the method of authenticating with SQL Server.";

			// Token: 0x0400108C RID: 4236
			internal const string DbConnectionString_Certificate = "Specified client certificate for authenticating with SQL Server.  ";

			// Token: 0x0400108D RID: 4237
			internal const string OdbcConnection_ConnectionString = "Information used to connect to a Data Source.";

			// Token: 0x0400108E RID: 4238
			internal const string OdbcConnection_ConnectionTimeout = "Current connection timeout value, not settable in the ConnectionString.";

			// Token: 0x0400108F RID: 4239
			internal const string OdbcConnection_Database = "Current data source catalog value, 'Database=X' in the connection string.";

			// Token: 0x04001090 RID: 4240
			internal const string OdbcConnection_DataSource = "Current data source, 'Server=X' in the connection string.";

			// Token: 0x04001091 RID: 4241
			internal const string OdbcConnection_Driver = "Current ODBC driver.";

			// Token: 0x04001092 RID: 4242
			internal const string OdbcConnection_ServerVersion = "Version of the product accessed by the ODBC Driver.";

			// Token: 0x04001093 RID: 4243
			internal const string OleDbConnection_ConnectionString = "Information used to connect to a Data Source.";

			// Token: 0x04001094 RID: 4244
			internal const string OleDbConnection_ConnectionTimeout = "Current connection timeout value, 'Connect Timeout=X' in the ConnectionString.";

			// Token: 0x04001095 RID: 4245
			internal const string OleDbConnection_Database = "Current data source catalog value, 'Initial Catalog=X' in the connection string.";

			// Token: 0x04001096 RID: 4246
			internal const string OleDbConnection_DataSource = "Current data source, 'Data Source=X' in the connection string.";

			// Token: 0x04001097 RID: 4247
			internal const string OleDbConnection_Provider = "Current OLE DB provider ProgID, 'Provider=X' in the connection string.";

			// Token: 0x04001098 RID: 4248
			internal const string OleDbConnection_ServerVersion = "Version of the product accessed by the OLE DB Provider.";

			// Token: 0x04001099 RID: 4249
			internal const string SqlConnection_AccessToken = "Access token to use for authentication.";

			// Token: 0x0400109A RID: 4250
			internal const string SqlConnection_Asynchronous = "State of connection, synchronous or asynchronous.  'Asynchronous Processing=x' in the connection string.";

			// Token: 0x0400109B RID: 4251
			internal const string SqlConnection_Replication = "Information used to connect for replication.";

			// Token: 0x0400109C RID: 4252
			internal const string SqlConnection_ConnectionString = "Information used to connect to a DataSource, such as 'Data Source=x;Initial Catalog=x;Integrated Security=SSPI'.";

			// Token: 0x0400109D RID: 4253
			internal const string SqlConnection_ConnectionTimeout = "Current connection timeout value, 'Connect Timeout=X' in the ConnectionString.";

			// Token: 0x0400109E RID: 4254
			internal const string SqlConnection_Database = "Current SQL Server database, 'Initial Catalog=X' in the connection string.";

			// Token: 0x0400109F RID: 4255
			internal const string SqlConnection_DataSource = "Current SqlServer that the connection is opened to, 'Data Source=X' in the connection string.";

			// Token: 0x040010A0 RID: 4256
			internal const string SqlConnection_PacketSize = "Network packet size, 'Packet Size=x' in the connection string.";

			// Token: 0x040010A1 RID: 4257
			internal const string SqlConnection_ServerVersion = "Version of the SQL Server accessed by the SqlConnection.";

			// Token: 0x040010A2 RID: 4258
			internal const string SqlConnection_WorkstationId = "Workstation Id, 'Workstation ID=x' in the connection string.";

			// Token: 0x040010A3 RID: 4259
			internal const string SqlConnection_StatisticsEnabled = "Collect statistics for this connection.";

			// Token: 0x040010A4 RID: 4260
			internal const string SqlConnection_CustomColumnEncryptionKeyStoreProviders = "Custom column encryption key store providers.";

			// Token: 0x040010A5 RID: 4261
			internal const string SqlConnection_ClientConnectionId = "A guid to represent the physical connection.";

			// Token: 0x040010A6 RID: 4262
			internal const string SqlConnection_Credential = "User Id and secure password to use for authentication.";

			// Token: 0x040010A7 RID: 4263
			internal const string DbConnection_InfoMessage = "Event triggered when messages arrive from the DataSource.";

			// Token: 0x040010A8 RID: 4264
			internal const string DbCommand_CommandText = "Command text to execute.";

			// Token: 0x040010A9 RID: 4265
			internal const string DbCommand_CommandType = "How to interpret the CommandText.";

			// Token: 0x040010AA RID: 4266
			internal const string DbCommand_Connection = "Connection used by the command.";

			// Token: 0x040010AB RID: 4267
			internal const string DbCommand_Parameters = "The parameters collection.";

			// Token: 0x040010AC RID: 4268
			internal const string DbCommand_Transaction = "The transaction used by the command.";

			// Token: 0x040010AD RID: 4269
			internal const string DbCommand_UpdatedRowSource = "When used by a DataAdapter.Update, how command results are applied to the current DataRow.";

			// Token: 0x040010AE RID: 4270
			internal const string DbCommand_StatementCompleted = "When records are affected by a given statement by the execution of the command.";

			// Token: 0x040010AF RID: 4271
			internal const string SqlCommand_Notification = "Notification values used by Microsoft SQL Server.";

			// Token: 0x040010B0 RID: 4272
			internal const string SqlCommand_NotificationAutoEnlist = "Automatic enlistment in notifications used by Microsoft SQL Server.";

			// Token: 0x040010B1 RID: 4273
			internal const string DbCommandBuilder_ConflictOption = "How the where clause is auto-generated for the Update and Delete commands when not specified by the user.";

			// Token: 0x040010B2 RID: 4274
			internal const string DbCommandBuilder_CatalogLocation = "Indicates the position of the catalog name in a qualified table name in a text command.";

			// Token: 0x040010B3 RID: 4275
			internal const string DbCommandBuilder_CatalogSeparator = "The character that separates the catalog name from the rest of the identifier in a text command.";

			// Token: 0x040010B4 RID: 4276
			internal const string DbCommandBuilder_SchemaSeparator = "The character that separates the schema name from the rest of the identifier in a text command.";

			// Token: 0x040010B5 RID: 4277
			internal const string DbCommandBuilder_QuotePrefix = "The prefix string wrapped around sql objects.";

			// Token: 0x040010B6 RID: 4278
			internal const string DbCommandBuilder_QuoteSuffix = "The suffix string wrapped around sql objects.";

			// Token: 0x040010B7 RID: 4279
			internal const string DbCommandBuilder_DataAdapter = "The DataAdapter for which to automatically generate Commands.";

			// Token: 0x040010B8 RID: 4280
			internal const string DbCommandBuilder_SchemaLocation = "Use schema from DataTable or the SelectCommand.";

			// Token: 0x040010B9 RID: 4281
			internal const string DbCommandBuilder_SetAllValues = "How the set clause is auto-generated for the Update command when not specified by the user.";

			// Token: 0x040010BA RID: 4282
			internal const string OdbcCommandBuilder_DataAdapter = "The DataAdapter for which to automatically generate OdbcCommands";

			// Token: 0x040010BB RID: 4283
			internal const string OdbcCommandBuilder_QuotePrefix = "The character used in a text command as the opening quote for quoting identifiers that contain special characters.";

			// Token: 0x040010BC RID: 4284
			internal const string OdbcCommandBuilder_QuoteSuffix = "The character used in a text command as the closing quote for quoting identifiers that contain special characters.";

			// Token: 0x040010BD RID: 4285
			internal const string OleDbCommandBuilder_DataAdapter = "The DataAdapter for which to automatically generate OleDbCommands";

			// Token: 0x040010BE RID: 4286
			internal const string OleDbCommandBuilder_DecimalSeparator = "The decimal separator used in numeric literals.";

			// Token: 0x040010BF RID: 4287
			internal const string OleDbCommandBuilder_QuotePrefix = "The prefix string wrapped around sql objects";

			// Token: 0x040010C0 RID: 4288
			internal const string OleDbCommandBuilder_QuoteSuffix = "The suffix string wrapped around sql objects";

			// Token: 0x040010C1 RID: 4289
			internal const string SqlCommandBuilder_DataAdapter = "The DataAdapter for which to automatically generate SqlCommands";

			// Token: 0x040010C2 RID: 4290
			internal const string SqlCommandBuilder_DecimalSeparator = "The decimal separator used in numeric literals.";

			// Token: 0x040010C3 RID: 4291
			internal const string SqlCommandBuilder_QuotePrefix = "The character used in a text command as the opening quote for quoting identifiers that contain special characters.";

			// Token: 0x040010C4 RID: 4292
			internal const string SqlCommandBuilder_QuoteSuffix = "The character used in a text command as the closing quote for quoting identifiers that contain special characters.";

			// Token: 0x040010C5 RID: 4293
			internal const string DbDataParameter_Precision = "Only necessary to set for decimal and numeric parameters when using with Prepare, FillSchema and CommandBuilder scenarios.";

			// Token: 0x040010C6 RID: 4294
			internal const string DbDataParameter_Scale = "Only necessary to set for decimal and numeric parameters when using with Prepare, FillSchema and CommandBuilder scenarios.";

			// Token: 0x040010C7 RID: 4295
			internal const string OdbcParameter_OdbcType = "The parameter native type.";

			// Token: 0x040010C8 RID: 4296
			internal const string OleDbParameter_OleDbType = "The parameter native type.";

			// Token: 0x040010C9 RID: 4297
			internal const string SqlParameter_ParameterName = "Name of the parameter, like '@p1'";

			// Token: 0x040010CA RID: 4298
			internal const string SqlParameter_SqlDbType = "The parameter native type.";

			// Token: 0x040010CB RID: 4299
			internal const string SqlParameter_TypeName = "The server's name for the type.";

			// Token: 0x040010CC RID: 4300
			internal const string SqlParameter_Offset = "Offset in variable length data types.";

			// Token: 0x040010CD RID: 4301
			internal const string SqlParameter_XmlSchemaCollectionDatabase = "XmlSchemaCollectionDatabase";

			// Token: 0x040010CE RID: 4302
			internal const string SqlParameter_XmlSchemaCollectionOwningSchema = "XmlSchemaCollectionOwningSchema";

			// Token: 0x040010CF RID: 4303
			internal const string SqlParameter_XmlSchemaCollectionName = "XmlSchemaCollectionName";

			// Token: 0x040010D0 RID: 4304
			internal const string SqlParameter_UnsupportedTVPOutputParameter = "ParameterDirection '{0}' specified for parameter '{1}' is not supported. Table-valued parameters only support ParameterDirection.Input.";

			// Token: 0x040010D1 RID: 4305
			internal const string SqlParameter_DBNullNotSupportedForTVP = "DBNull value for parameter '{0}' is not supported. Table-valued parameters cannot be DBNull.";

			// Token: 0x040010D2 RID: 4306
			internal const string SqlParameter_InvalidTableDerivedPrecisionForTvp = "Precision '{0}' required to send all values in column '{1}' exceeds the maximum supported precision '{2}'. The values must all fit in a single precision.";

			// Token: 0x040010D3 RID: 4307
			internal const string SqlParameter_UnexpectedTypeNameForNonStruct = "TypeName specified for parameter '{0}'.  TypeName must only be set for Structured parameters.";

			// Token: 0x040010D4 RID: 4308
			internal const string MetaType_SingleValuedStructNotSupported = "SqlDbType.Structured type is only supported for multiple valued types.";

			// Token: 0x040010D5 RID: 4309
			internal const string NullSchemaTableDataTypeNotSupported = "DateType column for field '{0}' in schema table is null.  DataType must be non-null.";

			// Token: 0x040010D6 RID: 4310
			internal const string InvalidSchemaTableOrdinals = "Invalid column ordinals in schema table.  ColumnOrdinals, if present, must not have duplicates or gaps.";

			// Token: 0x040010D7 RID: 4311
			internal const string SQL_EnumeratedRecordMetaDataChanged = "Metadata for field '{0}' of record '{1}' did not match the original record's metadata.";

			// Token: 0x040010D8 RID: 4312
			internal const string SQL_EnumeratedRecordFieldCountChanged = "Number of fields in record '{0}' does not match the number in the original record.";

			// Token: 0x040010D9 RID: 4313
			internal const string SQLUDT_MaxByteSizeValue = "range: 0-8000";

			// Token: 0x040010DA RID: 4314
			internal const string SQLUDT_Unexpected = "unexpected error encountered in SqlClient data provider. {0}";

			// Token: 0x040010DB RID: 4315
			internal const string SQLUDT_InvalidDbId = "Unable to get Type Info for {0},{1}";

			// Token: 0x040010DC RID: 4316
			internal const string SQLUDT_CantLoadAssembly = "The provider has failed to load the following assembly: {0}";

			// Token: 0x040010DD RID: 4317
			internal const string SQLUDT_InvalidUdtTypeName = "UdtTypeName property must be set for UDT parameters.";

			// Token: 0x040010DE RID: 4318
			internal const string SQLUDT_UnexpectedUdtTypeName = "UdtTypeName property must be set only for UDT parameters.";

			// Token: 0x040010DF RID: 4319
			internal const string SQLUDT_InvalidSqlType = "Specified type is not registered on the target server.{0}.";

			// Token: 0x040010E0 RID: 4320
			internal const string SQLUDT_InWhereClause = "UDT parameters not permitted in the where clause unless part of the primary key.";

			// Token: 0x040010E1 RID: 4321
			internal const string SqlUdt_InvalidUdtMessage = "'{0}' is an invalid user defined type, reason: {1}.";

			// Token: 0x040010E2 RID: 4322
			internal const string SqlUdtReason_MultipleSerFormats = "supports both in-memory and user-defined formats";

			// Token: 0x040010E3 RID: 4323
			internal const string SqlUdtReason_CannotSupportNative = "Native format can't be supported.";

			// Token: 0x040010E4 RID: 4324
			internal const string SqlUdtReason_CannotSupportUserDefined = "does not implement IBinarySerialize";

			// Token: 0x040010E5 RID: 4325
			internal const string SqlUdtReason_NotSerializable = "not serializable";

			// Token: 0x040010E6 RID: 4326
			internal const string SqlUdtReason_NoPublicConstructors = "no public constructors";

			// Token: 0x040010E7 RID: 4327
			internal const string SqlUdtReason_NotNullable = "does not implement INullable";

			// Token: 0x040010E8 RID: 4328
			internal const string SqlUdtReason_NoPublicConstructor = "does not have a public constructor";

			// Token: 0x040010E9 RID: 4329
			internal const string SqlUdtReason_NoUdtAttribute = "no UDT attribute";

			// Token: 0x040010EA RID: 4330
			internal const string SqlUdtReason_MaplessNotYetSupported = "Serialization without mapping is not yet supported.";

			// Token: 0x040010EB RID: 4331
			internal const string SqlUdtReason_ParseMethodMissing = "'public static x Parse(System.Data.SqlTypes.SqlString)' method is missing";

			// Token: 0x040010EC RID: 4332
			internal const string SqlUdtReason_ToStringMethodMissing = "'public override string ToString()' method is missing";

			// Token: 0x040010ED RID: 4333
			internal const string SqlUdtReason_NullPropertyMissing = "'public static x Null { get; }' method is missing";

			// Token: 0x040010EE RID: 4334
			internal const string SqlUdtReason_NativeFormatNoFieldSupport = "Native format does not support fields (directly or through another field) of type '{0}'";

			// Token: 0x040010EF RID: 4335
			internal const string SqlUdtReason_TypeNotPublic = "Type is not public";

			// Token: 0x040010F0 RID: 4336
			internal const string SqlUdtReason_NativeUdtNotSequentialLayout = "Native UDT not sequential layout due to type '{0}'";

			// Token: 0x040010F1 RID: 4337
			internal const string SqlUdtReason_NativeUdtMaxByteSize = "Native UDT specifies a max byte size";

			// Token: 0x040010F2 RID: 4338
			internal const string SqlUdtReason_NonSerializableField = "field '{0}' is marked non-serialized";

			// Token: 0x040010F3 RID: 4339
			internal const string SqlUdtReason_NativeFormatExplictLayoutNotAllowed = "The type of field '{0}' is marked as explicit layout which is not allowed in Native format";

			// Token: 0x040010F4 RID: 4340
			internal const string SqlUdtReason_MultivaluedAssemblyId = "Multiple valued assembly references must have a nonzero Assembly Id.";

			// Token: 0x040010F5 RID: 4341
			internal const string SQLTVP_TableTypeCanOnlyBeParameter = "Structured, multiple-valued types can only be used for parameters, and cannot be nested within another type.";

			// Token: 0x040010F6 RID: 4342
			internal const string SqlFileStream_InvalidPath = "The path name is not valid.";

			// Token: 0x040010F7 RID: 4343
			internal const string SqlFileStream_InvalidParameter = "An invalid parameter was passed to the function.";

			// Token: 0x040010F8 RID: 4344
			internal const string SqlFileStream_FileAlreadyInTransaction = "The process cannot access the file specified because it has been opened in another transaction.";

			// Token: 0x040010F9 RID: 4345
			internal const string SqlFileStream_PathNotValidDiskResource = "The path name is invalid or does not point to a disk file.";

			// Token: 0x040010FA RID: 4346
			internal const string SqlDelegatedTransaction_PromotionFailed = "Failure while attempting to promote transaction.";

			// Token: 0x040010FB RID: 4347
			internal const string SqlDependency_SqlDependency = "Dependency object used to receive query notifications.";

			// Token: 0x040010FC RID: 4348
			internal const string SqlDependency_HasChanges = "Property to indicate if this dependency is invalid.";

			// Token: 0x040010FD RID: 4349
			internal const string SqlDependency_Id = "A string that uniquely identifies this dependency object.";

			// Token: 0x040010FE RID: 4350
			internal const string SqlDependency_OnChange = "Event that can be used to subscribe for change notifications.";

			// Token: 0x040010FF RID: 4351
			internal const string SqlDependency_AddCommandDependency = "To add a command to existing dependency object.";

			// Token: 0x04001100 RID: 4352
			internal const string SqlDependency_Duplicate = "Command is already associated with another dependency object. Can not overwrite.";

			// Token: 0x04001101 RID: 4353
			internal const string SQLNotify_AlreadyHasCommand = "This SqlCommand object is already associated with another SqlDependency object.";

			// Token: 0x04001102 RID: 4354
			internal const string SqlNotify_SqlDepCannotBeCreatedInProc = "SqlDependency object cannot be created when running inside the SQL Server process.";

			// Token: 0x04001103 RID: 4355
			internal const string SqlDependency_DatabaseBrokerDisabled = "The SQL Server Service Broker for the current database is not enabled, and as a result query notifications are not supported.  Please enable the Service Broker for this database if you wish to use notifications.";

			// Token: 0x04001104 RID: 4356
			internal const string SqlDependency_DefaultOptionsButNoStart = "When using SqlDependency without providing an options value, SqlDependency.Start() must be called prior to execution of a command added to the SqlDependency instance.";

			// Token: 0x04001105 RID: 4357
			internal const string SqlDependency_EventNoDuplicate = "SqlDependency.OnChange does not support multiple event registrations for the same delegate.";

			// Token: 0x04001106 RID: 4358
			internal const string SqlDependency_DuplicateStart = "SqlDependency does not support calling Start() with different connection strings having the same server, user, and database in the same app domain.";

			// Token: 0x04001107 RID: 4359
			internal const string SqlDependency_IdMismatch = "No SqlDependency exists for the key.";

			// Token: 0x04001108 RID: 4360
			internal const string SqlDependency_NoMatchingServerStart = "When using SqlDependency without providing an options value, SqlDependency.Start() must be called for each server that is being executed against.";

			// Token: 0x04001109 RID: 4361
			internal const string SqlDependency_NoMatchingServerDatabaseStart = "SqlDependency.Start has been called for the server the command is executing against more than once, but there is no matching server/user/database Start() call for current command.";

			// Token: 0x0400110A RID: 4362
			internal const string SqlDependency_InvalidTimeout = "Timeout specified is invalid. Timeout cannot be < 0.";

			// Token: 0x0400110B RID: 4363
			internal const string SQLNotify_ErrorFormat = "Notification Error. Type={0}, Info={1}, Source={2}.";

			// Token: 0x0400110C RID: 4364
			internal const string SqlMetaData_NoMetadata = "GetMetaData is not valid for this SqlDbType.";

			// Token: 0x0400110D RID: 4365
			internal const string SqlMetaData_InvalidSqlDbTypeForConstructorFormat = "The dbType {0} is invalid for this constructor.";

			// Token: 0x0400110E RID: 4366
			internal const string SqlMetaData_NameTooLong = "The name is too long.";

			// Token: 0x0400110F RID: 4367
			internal const string SqlMetaData_SpecifyBothSortOrderAndOrdinal = "The sort order and ordinal must either both be specified, or neither should be specified (SortOrder.Unspecified and -1).  The values given were: order = {0}, ordinal = {1}.";

			// Token: 0x04001110 RID: 4368
			internal const string SqlProvider_InvalidDataColumnType = "The type of column '{0}' is not supported.  The type is '{1}'";

			// Token: 0x04001111 RID: 4369
			internal const string SqlProvider_InvalidDataColumnMaxLength = "The size of column '{0}' is not supported. The size is {1}.";

			// Token: 0x04001112 RID: 4370
			internal const string SqlProvider_NotEnoughColumnsInStructuredType = "There are not enough fields in the Structured type.  Structured types must have at least one field.";

			// Token: 0x04001113 RID: 4371
			internal const string SqlProvider_DuplicateSortOrdinal = "The sort ordinal {0} was specified twice.";

			// Token: 0x04001114 RID: 4372
			internal const string SqlProvider_MissingSortOrdinal = "The sort ordinal {0} was not specified.";

			// Token: 0x04001115 RID: 4373
			internal const string SqlProvider_SortOrdinalGreaterThanFieldCount = "The sort ordinal {0} on field {1} exceeds the total number of fields.";

			// Token: 0x04001116 RID: 4374
			internal const string IEnumerableOfSqlDataRecordHasNoRows = "There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead.";

			// Token: 0x04001117 RID: 4375
			internal const string SqlPipe_CommandHookedUpToNonContextConnection = "SqlPipe does not support executing a command with a connection that is not a context connection.";

			// Token: 0x04001118 RID: 4376
			internal const string SqlPipe_MessageTooLong = "Message length {0} exceeds maximum length supported of 4000.";

			// Token: 0x04001119 RID: 4377
			internal const string SqlPipe_IsBusy = "Could not use the pipe while it is busy with another operation.";

			// Token: 0x0400111A RID: 4378
			internal const string SqlPipe_AlreadyHasAnOpenResultSet = "A result set is currently being sent to the pipe. End the current result set before calling {0}.";

			// Token: 0x0400111B RID: 4379
			internal const string SqlPipe_DoesNotHaveAnOpenResultSet = "Result set has not been initiated.  Call SendResultSetStart before calling {0}.";

			// Token: 0x0400111C RID: 4380
			internal const string SNI_PN0 = "HTTP Provider";

			// Token: 0x0400111D RID: 4381
			internal const string SNI_PN1 = "Named Pipes Provider";

			// Token: 0x0400111E RID: 4382
			internal const string SNI_PN2 = "Session Provider";

			// Token: 0x0400111F RID: 4383
			internal const string SNI_PN3 = "Sign Provider";

			// Token: 0x04001120 RID: 4384
			internal const string SNI_PN4 = "Shared Memory Provider";

			// Token: 0x04001121 RID: 4385
			internal const string SNI_PN5 = "SMux Provider";

			// Token: 0x04001122 RID: 4386
			internal const string SNI_PN6 = "SSL Provider";

			// Token: 0x04001123 RID: 4387
			internal const string SNI_PN7 = "TCP Provider";

			// Token: 0x04001124 RID: 4388
			internal const string SNI_PN8 = "VIA Provider";

			// Token: 0x04001125 RID: 4389
			internal const string SNI_PN9 = "CTAIP Provider";

			// Token: 0x04001126 RID: 4390
			internal const string SNI_PN10 = "";

			// Token: 0x04001127 RID: 4391
			internal const string SNI_PN11 = "SQL Network Interfaces";

			// Token: 0x04001128 RID: 4392
			internal const string SNI_ERROR_1 = "I/O Error detected in read/write operation";

			// Token: 0x04001129 RID: 4393
			internal const string SNI_ERROR_2 = "Connection was terminated";

			// Token: 0x0400112A RID: 4394
			internal const string SNI_ERROR_3 = "Asynchronous operations not supported";

			// Token: 0x0400112B RID: 4395
			internal const string SNI_ERROR_4 = "";

			// Token: 0x0400112C RID: 4396
			internal const string SNI_ERROR_5 = "Invalid parameter(s) found";

			// Token: 0x0400112D RID: 4397
			internal const string SNI_ERROR_6 = "Unsupported protocol specified";

			// Token: 0x0400112E RID: 4398
			internal const string SNI_ERROR_7 = "Invalid connection found when setting up new session protocol";

			// Token: 0x0400112F RID: 4399
			internal const string SNI_ERROR_8 = "Protocol not supported";

			// Token: 0x04001130 RID: 4400
			internal const string SNI_ERROR_9 = "Associating port with I/O completion mechanism failed";

			// Token: 0x04001131 RID: 4401
			internal const string SNI_ERROR_10 = "";

			// Token: 0x04001132 RID: 4402
			internal const string SNI_ERROR_11 = "Timeout error";

			// Token: 0x04001133 RID: 4403
			internal const string SNI_ERROR_12 = "No server name supplied";

			// Token: 0x04001134 RID: 4404
			internal const string SNI_ERROR_13 = "TerminateListener() has been called";

			// Token: 0x04001135 RID: 4405
			internal const string SNI_ERROR_14 = "Win9x not supported";

			// Token: 0x04001136 RID: 4406
			internal const string SNI_ERROR_15 = "Function not supported";

			// Token: 0x04001137 RID: 4407
			internal const string SNI_ERROR_16 = "Shared-Memory heap error";

			// Token: 0x04001138 RID: 4408
			internal const string SNI_ERROR_17 = "Cannot find an ip/ipv6 type address to connect";

			// Token: 0x04001139 RID: 4409
			internal const string SNI_ERROR_18 = "Connection has been closed by peer";

			// Token: 0x0400113A RID: 4410
			internal const string SNI_ERROR_19 = "Physical connection is not usable";

			// Token: 0x0400113B RID: 4411
			internal const string SNI_ERROR_20 = "Connection has been closed";

			// Token: 0x0400113C RID: 4412
			internal const string SNI_ERROR_21 = "Encryption is enforced but there is no valid certificate";

			// Token: 0x0400113D RID: 4413
			internal const string SNI_ERROR_22 = "Couldn't load library";

			// Token: 0x0400113E RID: 4414
			internal const string SNI_ERROR_23 = "Cannot open a new thread in server process";

			// Token: 0x0400113F RID: 4415
			internal const string SNI_ERROR_24 = "Cannot post event to completion port";

			// Token: 0x04001140 RID: 4416
			internal const string SNI_ERROR_25 = "Connection string is not valid";

			// Token: 0x04001141 RID: 4417
			internal const string SNI_ERROR_26 = "Error Locating Server/Instance Specified";

			// Token: 0x04001142 RID: 4418
			internal const string SNI_ERROR_27 = "Error getting enabled protocols list from registry";

			// Token: 0x04001143 RID: 4419
			internal const string SNI_ERROR_28 = "Server doesn't support requested protocol";

			// Token: 0x04001144 RID: 4420
			internal const string SNI_ERROR_29 = "Shared Memory is not supported for clustered server connectivity";

			// Token: 0x04001145 RID: 4421
			internal const string SNI_ERROR_30 = "Invalid attempt bind to shared memory segment";

			// Token: 0x04001146 RID: 4422
			internal const string SNI_ERROR_31 = "Encryption(ssl/tls) handshake failed";

			// Token: 0x04001147 RID: 4423
			internal const string SNI_ERROR_32 = "Packet size too large for SSL Encrypt/Decrypt operations";

			// Token: 0x04001148 RID: 4424
			internal const string SNI_ERROR_33 = "SSRP error";

			// Token: 0x04001149 RID: 4425
			internal const string SNI_ERROR_34 = "Could not connect to the Shared Memory pipe";

			// Token: 0x0400114A RID: 4426
			internal const string SNI_ERROR_35 = "An internal exception was caught";

			// Token: 0x0400114B RID: 4427
			internal const string SNI_ERROR_36 = "The Shared Memory dll used to connect to SQL Server 2000 was not found";

			// Token: 0x0400114C RID: 4428
			internal const string SNI_ERROR_37 = "The SQL Server 2000 Shared Memory client dll appears to be invalid/corrupted";

			// Token: 0x0400114D RID: 4429
			internal const string SNI_ERROR_38 = "Cannot open a Shared Memory connection to SQL Server 2000";

			// Token: 0x0400114E RID: 4430
			internal const string SNI_ERROR_39 = "Shared memory connectivity to SQL Server 2000 is either disabled or not available on this machine";

			// Token: 0x0400114F RID: 4431
			internal const string SNI_ERROR_40 = "Could not open a connection to SQL Server";

			// Token: 0x04001150 RID: 4432
			internal const string SNI_ERROR_41 = "Cannot open a Shared Memory connection to a remote SQL server";

			// Token: 0x04001151 RID: 4433
			internal const string SNI_ERROR_42 = "Could not establish dedicated administrator connection (DAC) on default port. Make sure that DAC is enabled";

			// Token: 0x04001152 RID: 4434
			internal const string SNI_ERROR_43 = "An error occurred while obtaining the dedicated administrator connection (DAC) port. Make sure that SQL Browser is running, or check the error log for the port number";

			// Token: 0x04001153 RID: 4435
			internal const string SNI_ERROR_44 = "Could not compose Service Principal Name (SPN) for Windows Integrated Authentication. Possible causes are server(s) incorrectly specified to connection API calls, Domain Name System (DNS) lookup failure or memory shortage";

			// Token: 0x04001154 RID: 4436
			internal const string SNI_ERROR_47 = "Connecting with the MultiSubnetFailover connection option to a SQL Server instance configured with more than 64 IP addresses is not supported.";

			// Token: 0x04001155 RID: 4437
			internal const string SNI_ERROR_48 = "Connecting to a named SQL Server instance using the MultiSubnetFailover connection option is not supported.";

			// Token: 0x04001156 RID: 4438
			internal const string SNI_ERROR_49 = "Connecting to a SQL Server instance using the MultiSubnetFailover connection option is only supported when using the TCP protocol.";

			// Token: 0x04001157 RID: 4439
			internal const string SNI_ERROR_50 = "Local Database Runtime error occurred. ";

			// Token: 0x04001158 RID: 4440
			internal const string SNI_ERROR_51 = "An instance name was not specified while connecting to a Local Database Runtime. Specify an instance name in the format (localdb)\\instance_name.";

			// Token: 0x04001159 RID: 4441
			internal const string SNI_ERROR_52 = "Unable to locate a Local Database Runtime installation. Verify that SQL Server Express is properly installed and that the Local Database Runtime feature is enabled.";

			// Token: 0x0400115A RID: 4442
			internal const string SNI_ERROR_53 = "Invalid Local Database Runtime registry configuration found. Verify that SQL Server Express is properly installed.";

			// Token: 0x0400115B RID: 4443
			internal const string SNI_ERROR_54 = "Unable to locate the registry entry for SQLUserInstance.dll file path. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

			// Token: 0x0400115C RID: 4444
			internal const string SNI_ERROR_55 = "Registry value contains an invalid SQLUserInstance.dll file path. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

			// Token: 0x0400115D RID: 4445
			internal const string SNI_ERROR_56 = "Unable to load the SQLUserInstance.dll from the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

			// Token: 0x0400115E RID: 4446
			internal const string SNI_ERROR_57 = "Invalid SQLUserInstance.dll found at the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

			// Token: 0x0400115F RID: 4447
			internal const string Snix_Connect = "A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections.";

			// Token: 0x04001160 RID: 4448
			internal const string Snix_PreLoginBeforeSuccessfullWrite = "The client was unable to establish a connection because of an error during connection initialization process before login. Possible causes include the following:  the client tried to connect to an unsupported version of SQL Server; the server was too busy to accept new connections; or there was a resource limitation (insufficient memory or maximum allowed connections) on the server.";

			// Token: 0x04001161 RID: 4449
			internal const string Snix_PreLogin = "A connection was successfully established with the server, but then an error occurred during the pre-login handshake.";

			// Token: 0x04001162 RID: 4450
			internal const string Snix_LoginSspi = "A connection was successfully established with the server, but then an error occurred when obtaining the security/SSPI context information for integrated security login.";

			// Token: 0x04001163 RID: 4451
			internal const string Snix_Login = "A connection was successfully established with the server, but then an error occurred during the login process.";

			// Token: 0x04001164 RID: 4452
			internal const string Snix_EnableMars = "Connection open and login was successful, but then an error occurred while enabling MARS for this connection.";

			// Token: 0x04001165 RID: 4453
			internal const string Snix_AutoEnlist = "Connection open and login was successful, but then an error occurred while enlisting the connection into the current distributed transaction.";

			// Token: 0x04001166 RID: 4454
			internal const string Snix_GetMarsSession = "Failed to establish a MARS session in preparation to send the request to the server.";

			// Token: 0x04001167 RID: 4455
			internal const string Snix_Execute = "A transport-level error has occurred when sending the request to the server.";

			// Token: 0x04001168 RID: 4456
			internal const string Snix_Read = "A transport-level error has occurred when receiving results from the server.";

			// Token: 0x04001169 RID: 4457
			internal const string Snix_Close = "A transport-level error has occurred during connection clean-up.";

			// Token: 0x0400116A RID: 4458
			internal const string Snix_SendRows = "A transport-level error has occurred while sending information to the server.";

			// Token: 0x0400116B RID: 4459
			internal const string Snix_ProcessSspi = "A transport-level error has occurred during SSPI handshake.";

			// Token: 0x0400116C RID: 4460
			internal const string LocalDB_CreateFailed = "Local Database Runtime: Cannot create named instance.";

			// Token: 0x0400116D RID: 4461
			internal const string LocalDB_BadConfigSectionType = "Local Database Runtime: system.data.localdb configuration file section is of unknown type.";

			// Token: 0x0400116E RID: 4462
			internal const string LocalDB_FailedGetDLLHandle = "Local Database Runtime: Cannot load SQLUserInstance.dll.";

			// Token: 0x0400116F RID: 4463
			internal const string LocalDB_MethodNotFound = "Invalid SQLUserInstance.dll found at the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.";

			// Token: 0x04001170 RID: 4464
			internal const string LocalDB_UnobtainableMessage = "Cannot obtain Local Database Runtime error message";

			// Token: 0x04001171 RID: 4465
			internal const string LocalDB_InvalidVersion = "Local Database Runtime: Invalid instance version specification found in the configuration file.";

			// Token: 0x04001172 RID: 4466
			internal const string TCE_InvalidKeyEncryptionAlgorithm = "Invalid key encryption algorithm specified: '{0}'. Expected value: '{1}'.";

			// Token: 0x04001173 RID: 4467
			internal const string TCE_InvalidKeyEncryptionAlgorithmSysErr = "Internal error. Invalid key encryption algorithm specified: '{0}'. Expected value: '{1}'.";

			// Token: 0x04001174 RID: 4468
			internal const string TCE_NullKeyEncryptionAlgorithm = "Key encryption algorithm cannot be null.";

			// Token: 0x04001175 RID: 4469
			internal const string TCE_NullKeyEncryptionAlgorithmSysErr = "Internal error. Key encryption algorithm cannot be null.";

			// Token: 0x04001176 RID: 4470
			internal const string TCE_EmptyColumnEncryptionKey = "Empty column encryption key specified.";

			// Token: 0x04001177 RID: 4471
			internal const string TCE_NullColumnEncryptionKey = "Column encryption key cannot be null.";

			// Token: 0x04001178 RID: 4472
			internal const string TCE_EmptyEncryptedColumnEncryptionKey = "Internal error. Empty encrypted column encryption key specified.";

			// Token: 0x04001179 RID: 4473
			internal const string TCE_NullEncryptedColumnEncryptionKey = "Internal error. Encrypted column encryption key cannot be null.";

			// Token: 0x0400117A RID: 4474
			internal const string TCE_LargeCertificatePathLength = "Specified certificate path has {0} bytes, which exceeds maximum length of {1} bytes.";

			// Token: 0x0400117B RID: 4475
			internal const string TCE_LargeCertificatePathLengthSysErr = "Internal error. Specified certificate path has {0} bytes, which exceeds maximum length of {1} bytes.";

			// Token: 0x0400117C RID: 4476
			internal const string TCE_NullCertificatePath = "Certificate path cannot be null. Use the following format: <certificate location>{2}<certificate store>{2}<certificate thumbprint>, where <certificate location> is either '{0}' or '{1}'.";

			// Token: 0x0400117D RID: 4477
			internal const string TCE_NullCertificatePathSysErr = "Internal error. Certificate path cannot be null. Use the following format: <certificate location>{2}<certificate store>{2}<certificate thumbprint>, where <certificate location> is either '{0}' or '{1}'.";

			// Token: 0x0400117E RID: 4478
			internal const string TCE_NullCspPath = "Column master key path cannot be null. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{0}<Key Identifier>.";

			// Token: 0x0400117F RID: 4479
			internal const string TCE_NullCspPathSysErr = "Internal error. Column master key path cannot be null. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{0}<Key Identifier>.";

			// Token: 0x04001180 RID: 4480
			internal const string TCE_NullCngPath = "Column master key path cannot be null. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{0}<Key Identifier>.";

			// Token: 0x04001181 RID: 4481
			internal const string TCE_NullCngPathSysErr = "Internal error. Column master key path cannot be null. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{0}<Key Identifier>.";

			// Token: 0x04001182 RID: 4482
			internal const string TCE_InvalidCertificatePath = "Invalid certificate path: '{0}'. Use the following format: <certificate location>{3}<certificate store>{3}<certificate thumbprint>, where <certificate location> is either '{1}' or '{2}'.";

			// Token: 0x04001183 RID: 4483
			internal const string TCE_InvalidCertificatePathSysErr = "Internal error. Invalid certificate path: '{0}'. Use the following format: <certificate location>{3}<certificate store>{3}<certificate thumbprint>, where <certificate location> is either '{1}' or '{2}'.";

			// Token: 0x04001184 RID: 4484
			internal const string TCE_InvalidCspPath = "Invalid column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001185 RID: 4485
			internal const string TCE_InvalidCspPathSysErr = "Internal error. Invalid column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001186 RID: 4486
			internal const string TCE_InvalidCngPath = "Invalid column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001187 RID: 4487
			internal const string TCE_InvalidCngPathSysErr = "Internal error. Invalid column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001188 RID: 4488
			internal const string TCE_InvalidCertificateLocation = "Invalid certificate location '{0}' in certificate path '{1}'. Use the following format: <certificate location>{4}<certificate store>{4}<certificate thumbprint>, where <certificate location> is either '{2}' or '{3}'.";

			// Token: 0x04001189 RID: 4489
			internal const string TCE_InvalidCertificateLocationSysErr = "Internal error. Invalid certificate location '{0}' in certificate path '{1}'. Use the following format: <certificate location>{4}<certificate store>{4}<certificate thumbprint>, where <certificate location> is either '{2}' or '{3}'.";

			// Token: 0x0400118A RID: 4490
			internal const string TCE_InvalidCertificateStore = "Invalid certificate store '{0}' specified in certificate path '{1}'. Expected value: '{2}'.";

			// Token: 0x0400118B RID: 4491
			internal const string TCE_InvalidCertificateStoreSysErr = "Internal error. Invalid certificate store '{0}' specified in certificate path '{1}'. Expected value: '{2}'.";

			// Token: 0x0400118C RID: 4492
			internal const string TCE_EmptyCertificateThumbprint = "Empty certificate thumbprint specified in certificate path '{0}'.";

			// Token: 0x0400118D RID: 4493
			internal const string TCE_EmptyCertificateThumbprintSysErr = "Internal error. Empty certificate thumbprint specified in certificate path '{0}'.";

			// Token: 0x0400118E RID: 4494
			internal const string TCE_EmptyCspName = "Empty Microsoft cryptographic service provider (CSP) name specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x0400118F RID: 4495
			internal const string TCE_EmptyCspNameSysErr = "Internal error. Empty Microsoft cryptographic service provider (CSP) name specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001190 RID: 4496
			internal const string TCE_EmptyCngName = "Empty Microsoft Cryptography API: Next Generation (CNG) provider name specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001191 RID: 4497
			internal const string TCE_EmptyCngNameSysErr = "Internal error. Empty Microsoft Cryptography API: Next Generation (CNG) provider name specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001192 RID: 4498
			internal const string TCE_EmptyCspKeyId = "Empty key identifier specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001193 RID: 4499
			internal const string TCE_EmptyCspKeyIdSysErr = "Internal error. Empty key identifier specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft cryptographic service provider (CSP): <CSP Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001194 RID: 4500
			internal const string TCE_EmptyCngKeyId = "Empty key identifier specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001195 RID: 4501
			internal const string TCE_EmptyCngKeyIdSysErr = "Internal error. Empty key identifier specified in column master key path: '{0}'. Use the following format for a key stored in a Microsoft Cryptography API: Next Generation (CNG) provider: <CNG Provider Name>{1}<Key Identifier>.";

			// Token: 0x04001196 RID: 4502
			internal const string TCE_InvalidCspName = "Invalid Microsoft cryptographic service provider (CSP) name: '{0}'. Verify that the CSP provider name in column master key path: '{1}' is valid and installed on the machine.";

			// Token: 0x04001197 RID: 4503
			internal const string TCE_InvalidCspNameSysErr = "Internal error. Invalid Microsoft cryptographic service provider (CSP) name: '{0}'. Verify that the CSP provider name in column master key path: '{1}' is valid and installed on the machine.";

			// Token: 0x04001198 RID: 4504
			internal const string TCE_InvalidCspKeyId = "Invalid key identifier: '{0}'. Verify that the key identifier in column master key path: '{1}' is valid and exists in the CSP.";

			// Token: 0x04001199 RID: 4505
			internal const string TCE_InvalidCspKeyIdSysErr = "Internal error. Invalid key identifier: '{0}'. Verify that the key identifier in column master key path: '{1}' is valid and exists in the CSP.";

			// Token: 0x0400119A RID: 4506
			internal const string TCE_InvalidCngKey = "An error occurred while opening the Microsoft Cryptography API: Next Generation (CNG) key: '{0}'. Verify that the CNG provider name '{1}' is valid, installed on the machine, and the key '{2}' exists.";

			// Token: 0x0400119B RID: 4507
			internal const string TCE_InvalidCngKeySysErr = "Internal error. An error occurred while opening the Microsoft Cryptography API: Next Generation (CNG) key: '{0}'. Verify that the CNG provider name '{1}' is valid, installed on the machine, and the key '{2}' exists.";

			// Token: 0x0400119C RID: 4508
			internal const string TCE_CertificateNotFound = "Certificate with thumbprint '{0}' not found in certificate store '{1}' in certificate location '{2}'.";

			// Token: 0x0400119D RID: 4509
			internal const string TCE_CertificateNotFoundSysErr = "Certificate with thumbprint '{0}' not found in certificate store '{1}' in certificate location '{2}'. Verify the certificate path in the column master key definition in the database is correct, and the certificate has been imported correctly into the certificate location/store.";

			// Token: 0x0400119E RID: 4510
			internal const string TCE_InvalidAlgorithmVersionInEncryptedCEK = "Specified encrypted column encryption key contains an invalid encryption algorithm version '{0}'. Expected version is '{1}'.";

			// Token: 0x0400119F RID: 4511
			internal const string TCE_InvalidCiphertextLengthInEncryptedCEK = "The specified encrypted column encryption key's ciphertext length: {0} does not match the ciphertext length: {1} when using column master key (certificate) in '{2}'. The encrypted column encryption key may be corrupt, or the specified certificate path may be incorrect.";

			// Token: 0x040011A0 RID: 4512
			internal const string TCE_InvalidCiphertextLengthInEncryptedCEKCsp = "The specified encrypted column encryption key's ciphertext length: {0} does not match the ciphertext length: {1} when using column master key (asymmetric key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Microsoft Cryptographic Service provider (CSP) path may be incorrect.";

			// Token: 0x040011A1 RID: 4513
			internal const string TCE_InvalidCiphertextLengthInEncryptedCEKCng = "The specified encrypted column encryption key's ciphertext length: {0} does not match the ciphertext length: {1} when using column master key (asymmetric key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Microsoft Cryptography API: Next Generation (CNG) provider path may be incorrect.";

			// Token: 0x040011A2 RID: 4514
			internal const string TCE_InvalidSignatureInEncryptedCEK = "The specified encrypted column encryption key's signature length: {0} does not match the signature length: {1} when using column master key (certificate) in '{2}'. The encrypted column encryption key may be corrupt, or the specified certificate path may be incorrect.";

			// Token: 0x040011A3 RID: 4515
			internal const string TCE_InvalidSignatureInEncryptedCEKCsp = "The specified encrypted column encryption key's signature length: {0} does not match the signature length: {1} when using column master key (asymmetric key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Microsoft cryptographic service provider (CSP) path may be incorrect.";

			// Token: 0x040011A4 RID: 4516
			internal const string TCE_InvalidSignatureInEncryptedCEKCng = "The specified encrypted column encryption key's signature length: {0} does not match the signature length: {1} when using column master key (asymmetric key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Microsoft Cryptography API: Next Generation (CNG) provider path may be incorrect.";

			// Token: 0x040011A5 RID: 4517
			internal const string TCE_InvalidCertificateSignature = "The specified encrypted column encryption key signature does not match the signature computed with the column master key (certificate) in '{0}'. The encrypted column encryption key may be corrupt, or the specified path may be incorrect.";

			// Token: 0x040011A6 RID: 4518
			internal const string TCE_InvalidSignature = "The specified encrypted column encryption key signature does not match the signature computed with the column master key (asymmetric key) in '{0}'. The encrypted column encryption key may be corrupt, or the specified path may be incorrect.";

			// Token: 0x040011A7 RID: 4519
			internal const string TCE_CertificateWithNoPrivateKey = "Certificate specified in key path '{0}' does not have a private key to encrypt a column encryption key. Verify the certificate is imported correctly.";

			// Token: 0x040011A8 RID: 4520
			internal const string TCE_CertificateWithNoPrivateKeySysErr = "Certificate specified in key path '{0}' does not have a private key to decrypt a column encryption key. Verify the certificate is imported correctly.";

			// Token: 0x040011A9 RID: 4521
			internal const string TCE_NullColumnEncryptionKeySysErr = "Internal error. Column encryption key cannot be null.";

			// Token: 0x040011AA RID: 4522
			internal const string TCE_InvalidKeySize = "The column encryption key has been successfully decrypted but its length: {1} does not match the length: {2} for algorithm '{0}'. Verify the encrypted value of the column encryption key in the database.";

			// Token: 0x040011AB RID: 4523
			internal const string TCE_InvalidEncryptionType = "Encryption type '{1}' specified for the column in the database is either invalid or corrupted. Valid encryption types for algorithm '{0}' are: {2}.";

			// Token: 0x040011AC RID: 4524
			internal const string TCE_NullPlainText = "Internal error. Plaintext value cannot be null.";

			// Token: 0x040011AD RID: 4525
			internal const string TCE_VeryLargeCiphertext = "Cannot encrypt. Encrypting resulted in {0} bytes of ciphertext which exceeds the maximum allowed limit of {1} bytes. The specified plaintext value is likely too large (plaintext size is: {2} bytes).";

			// Token: 0x040011AE RID: 4526
			internal const string TCE_NullCipherText = "Internal error. Ciphertext value cannot be null.";

			// Token: 0x040011AF RID: 4527
			internal const string TCE_InvalidCipherTextSize = "Specified ciphertext has an invalid size of {0} bytes, which is below the minimum {1} bytes required for decryption.";

			// Token: 0x040011B0 RID: 4528
			internal const string TCE_InvalidAlgorithmVersion = "The specified ciphertext's encryption algorithm version '{0}' does not match the expected encryption algorithm version '{1}'.";

			// Token: 0x040011B1 RID: 4529
			internal const string TCE_InvalidAuthenticationTag = "Specified ciphertext has an invalid authentication tag.";

			// Token: 0x040011B2 RID: 4530
			internal const string TCE_NullColumnEncryptionAlgorithm = "Internal error. Encryption algorithm cannot be null. Valid algorithms are: {0}.";

			// Token: 0x040011B3 RID: 4531
			internal const string TCE_UnexpectedDescribeParamFormatParameterMetadata = "Internal error. The result returned by '{0}' is invalid. The parameter metadata resultset is missing.";

			// Token: 0x040011B4 RID: 4532
			internal const string TCE_UnexpectedDescribeParamFormatAttestationInfo = "Internal error. The result returned by '{0}' is invalid. The attestation information resultset is missing for enclave type '{1}'. ";

			// Token: 0x040011B5 RID: 4533
			internal const string TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata = "Internal error. Error occurred when populating enclave metadata. The referenced column encryption key ordinal '{0}' is missing in the encryption metadata returned by SQL Server. Max ordinal is '{1}'. ";

			// Token: 0x040011B6 RID: 4534
			internal const string TCE_InvalidEncryptionKeyOrdinalParameterMetadata = "Internal error. Error occurred when populating parameter metadata. The referenced column encryption key ordinal '{0}' is missing in the encryption metadata returned by SQL Server. Max ordinal is '{1}'. ";

			// Token: 0x040011B7 RID: 4535
			internal const string TCE_MultipleRowsReturnedForAttestationInfo = "Internal error. Error occurred when parsing the results of '{0}'. The attestation information resultset is expected to contain only one row, but it contains multiple rows.";

			// Token: 0x040011B8 RID: 4536
			internal const string TCE_ParamEncryptionMetaDataMissing = "Internal error. Metadata for parameter '{1}' in statement or procedure '{2}' is missing in resultset returned by {0}.";

			// Token: 0x040011B9 RID: 4537
			internal const string TCE_ProcEncryptionMetaDataMissing = "Internal error. Metadata for parameters for command '{1}' in a batch is missing in the resultset returned by {0}.";

			// Token: 0x040011BA RID: 4538
			internal const string TCE_ColumnMasterKeySignatureVerificationFailed = "The signature returned by SQL Server for the column master key, specified in key path '{0}', is invalid (does not match the computed signature). Recreate column master key metadata, making sure the signature inside the metadata is computed using the column master key being referenced in the metadata. If the error persists, please contact Microsoft for assistance.";

			// Token: 0x040011BB RID: 4539
			internal const string TCE_ColumnMasterKeySignatureNotFound = "Internal error. The signature returned by SQL Server for enclave-enabled column master key, specified at key path '{0}', cannot be null or empty.";

			// Token: 0x040011BC RID: 4540
			internal const string TCE_UnableToVerifyColumnMasterKeySignature = "Unable to verify a column master key signature. Error message: {0} ";

			// Token: 0x040011BD RID: 4541
			internal const string TCE_ParamEncryptionFailed = "Failed to encrypt parameter '{0}'.";

			// Token: 0x040011BE RID: 4542
			internal const string TCE_ColumnDecryptionFailed = "Failed to decrypt column '{0}'.";

			// Token: 0x040011BF RID: 4543
			internal const string TCE_ParamDecryptionFailed = "Failed to decrypt parameter '{0}'.";

			// Token: 0x040011C0 RID: 4544
			internal const string TCE_UnknownColumnEncryptionAlgorithm = "Encryption algorithm '{0}' for the column in the database is either invalid or corrupted. Valid algorithms are: {1}.";

			// Token: 0x040011C1 RID: 4545
			internal const string TCE_UnknownColumnEncryptionAlgorithmId = "Encryption algorithm id '{0}' for the column in the database is either invalid or corrupted. Valid encryption algorithm ids are: {1}.";

			// Token: 0x040011C2 RID: 4546
			internal const string TCE_UnsupportedNormalizationVersion = "Normalization version '{0}' received from {2} is not supported. Valid normalization versions are: {1}.";

			// Token: 0x040011C3 RID: 4547
			internal const string TCE_UnrecognizedKeyStoreProviderName = "Failed to decrypt a column encryption key. Invalid key store provider name: '{0}'. A key store provider name must denote either a system key store provider or a registered custom key store provider. Valid system key store provider names are: {1}. Valid (currently registered) custom key store provider names are: {2}. Please verify key store provider information in column master key definitions in the database, and verify all custom key store providers used in your application are registered properly.";

			// Token: 0x040011C4 RID: 4548
			internal const string TCE_KeyDecryptionFailedCertStore = "Failed to decrypt a column encryption key using key store provider: '{0}'. The last 10 bytes of the encrypted column encryption key are: '{1}'.";

			// Token: 0x040011C5 RID: 4549
			internal const string TCE_UntrustedKeyPath = "Column master key path '{0}' received from server '{1}' is not a trusted key path.";

			// Token: 0x040011C6 RID: 4550
			internal const string TCE_KeyDecryptionFailed = "Failed to decrypt a column encryption key using key store provider: '{0}'. Verify the properties of the column encryption key and its column master key in your database. The last 10 bytes of the encrypted column encryption key are: '{1}'.";

			// Token: 0x040011C7 RID: 4551
			internal const string TCE_UnsupportedDatatype = "Encryption and decryption of data type '{0}' is not supported.";

			// Token: 0x040011C8 RID: 4552
			internal const string TCE_DecryptionFailed = "Decryption failed. The last 10 bytes of the encrypted column encryption key are: '{0}'. The first 10 bytes of ciphertext are: '{1}'.";

			// Token: 0x040011C9 RID: 4553
			internal const string TCE_ExceptionWhenGeneratingEnclavePackage = "Error encountered while generating package to be sent to enclave. Error message: {0}";

			// Token: 0x040011CA RID: 4554
			internal const string TCE_InvalidKeyIdUnableToCastToUnsignedShort = "Internal Error. The given key id '{0}' is not valid. Error occurred when converting the key id to unsigned short. Error Message: {1}";

			// Token: 0x040011CB RID: 4555
			internal const string TCE_InvalidDatabaseIdUnableToCastToUnsignedInt = "Internal Error. The given database id '{0}' is not valid. Error occurred when converting the database id to unsigned int. Error Message: {1}";

			// Token: 0x040011CC RID: 4556
			internal const string TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt = "Invalid attestation parameters specified by the enclave provider for enclave type '{0}'. Error occurred when converting the value '{1}' of parameter '{2}' to unsigned int. Error Message: {3}";

			// Token: 0x040011CD RID: 4557
			internal const string TCE_InvalidKeyStoreProviderName = "Invalid key store provider name: '{0}'. A key store provider name must denote either a system key store provider or a registered custom key store provider. Valid system key store provider names are: {1}. Valid (currently registered) custom key store provider names are: {2}. Please verify key store provider information in column master key definitions in the database, and verify all custom key store providers used in your application are registered properly.";

			// Token: 0x040011CE RID: 4558
			internal const string TCE_FailedToEncryptRegisterRulesBytePackage = "Internal Error.  Failed to encrypt byte package to be sent to the enclave. Error Message: {0} ";

			// Token: 0x040011CF RID: 4559
			internal const string TCE_OffsetOutOfBounds = "Internal Error. Failed to serialize keys to be sent to the enclave. The start offset specified by argument '{0}' for method {1}.{2} is out of bounds.";

			// Token: 0x040011D0 RID: 4560
			internal const string TCE_InsufficientBuffer = "Internal Error. The buffer specified by argument '{0}' for method '{1}.{2}' has insufficient space.";

			// Token: 0x040011D1 RID: 4561
			internal const string TCE_ColumnEncryptionKeysNotFound = "Internal Error. Encrypted column encryption keys not found when trying to send the keys to the enclave.";

			// Token: 0x040011D2 RID: 4562
			internal const string TCE_NullEnclaveSessionDuringQueryExecution = "Internal Error. Enclave session is null during query execution. Enclave type is '{0}' and enclaveAttestationUrl is '{1}'.";

			// Token: 0x040011D3 RID: 4563
			internal const string TCE_NullEnclavePackageForEnclaveBasedQuery = "Internal Error. Enclave package is null during execution of an enclave based query. Enclave type is '{0}' and enclaveAttestationUrl is '{1}'.";

			// Token: 0x040011D4 RID: 4564
			internal const string TCE_AttestationInfoNotReturnedFromSQLServer = "Attestation information was not returned by SQL Server. Enclave type is '{0}' and enclave attestation URL is '{1}'.";

			// Token: 0x040011D5 RID: 4565
			internal const string TCE_UnableToEstablishSecureChannel = "Unable to establish secure channel. Error Message: {0}";

			// Token: 0x040011D6 RID: 4566
			internal const string TCE_NullArgumentInConstructorInternal = "Internal Error. Null argument '{0}' specified when constructing an object of type '{1}'. '{0}' cannot be null.";

			// Token: 0x040011D7 RID: 4567
			internal const string TCE_EmptyArgumentInConstructorInternal = "Internal Error. Empty argument '{0}' specified when constructing an object of type '{1}'. '{0}' cannot be empty.";

			// Token: 0x040011D8 RID: 4568
			internal const string TCE_NullArgumentInternal = "Internal Error. Argument '{0}' cannot be null when executing method '{1}.{2}'.";

			// Token: 0x040011D9 RID: 4569
			internal const string TCE_EmptyArgumentInternal = "Internal Error. Argument '{0}' cannot be empty when executing method '{1}.{2}'.";

			// Token: 0x040011DA RID: 4570
			internal const string TCE_DbConnectionString_EnclaveAttestationUrl = "Specifies an endpoint of an enclave attestation service, which will be used to verify whether the enclave, configured in the SQL Server instance for computations on database columns encrypted using Always Encrypted, is valid and secure.";

			// Token: 0x040011DB RID: 4571
			internal const string TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig = "Failed to read the configuration section for enclave providers. Make sure the section is correctly formatted in your application configuration file. Error Message: {0}";

			// Token: 0x040011DC RID: 4572
			internal const string TCE_CannotCreateSqlColumnEncryptionEnclaveProvider = "Failed to instantiate an enclave provider with type '{1}' for name '{0}'. Error message: {2} ";

			// Token: 0x040011DD RID: 4573
			internal const string TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty = "Internal Error. SqlColumnEncryptionEnclaveProviderName cannot be null or empty.";

			// Token: 0x040011DE RID: 4574
			internal const string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe = "Error occurred when reading '{0}' resultset. Attestation URL has not been specified in the connection string, but the query requires enclave computations.  Enclave type is '{1}'. ";

			// Token: 0x040011DF RID: 4575
			internal const string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage = "Error occurred when generating enclave package. Attestation URL has not been specified in the connection string, but the query requires enclave computations.  Enclave type is '{0}'. ";

			// Token: 0x040011E0 RID: 4576
			internal const string TCE_EnclaveTypeNullForEnclaveBasedQuery = "Internal Error. Enclave type received from SQL Server is null or empty when executing a query requiring enclave computations.";

			// Token: 0x040011E1 RID: 4577
			internal const string TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery = "Executing a query requires enclave computations, but the application configuration is missing the enclave provider  section.";

			// Token: 0x040011E2 RID: 4578
			internal const string TCE_EnclaveProviderNotFound = "No enclave provider found for enclave type '{0}' and attestation protocol '{1}'. Please specify the correct attestation protocol in the connection string. ";

			// Token: 0x040011E3 RID: 4579
			internal const string TCE_NullEnclaveSessionReturnedFromProvider = "Unable to communicate with the enclave. Null enclave session information received from the enclave provider. Enclave type is '{0}' and enclave attestation URL is '{1}'.";

			// Token: 0x040011E4 RID: 4580
			internal const string TCE_ParamInvalidForceColumnEncryptionSetting = "Cannot set {0} for {3} '{1}' because encryption is not enabled for the statement or procedure '{2}'.";

			// Token: 0x040011E5 RID: 4581
			internal const string TCE_ParamUnExpectedEncryptionMetadata = "Cannot execute statement or procedure '{1}' because {2} was set for {3} '{0}' and the database expects this parameter to be sent as plaintext. This may be due to a configuration error.";

			// Token: 0x040011E6 RID: 4582
			internal const string TCE_NotSupportedByServer = "{0} instance in use does not support column encryption.";

			// Token: 0x040011E7 RID: 4583
			internal const string TCE_EnclaveComputationsNotSupported = "You have specified the enclave attestation URL and attestation protocol in the connection string, but the SQL Server in use does not support enclave based computations -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649 for\u202fmore\u202fdetails.";

			// Token: 0x040011E8 RID: 4584
			internal const string TCE_AttestationURLNotSupported = "You have specified the enclave attestation URL in the connection string, but the SQL Server in use does not support enclave based computations -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649 for\u202fmore\u202fdetails.";

			// Token: 0x040011E9 RID: 4585
			internal const string TCE_AttestationProtocolNotSupported = "You have specified the attestation protocol in the connection string, but the SQL Server in use does not support enclave based computations -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649 for\u202fmore\u202fdetails.";

			// Token: 0x040011EA RID: 4586
			internal const string TCE_EnclaveTypeNotReturned = "You have specified the enclave attestation URL in the connection string, but the SQL Server did not return an enclave type. Please make sure the enclave type is correctly configured in your instance -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649 for\u202fmore\u202fdetails.";

			// Token: 0x040011EB RID: 4587
			internal const string TCE_BatchedUpdateColumnEncryptionSettingMismatch = "{0} should be identical on all commands ({1}, {2}, {3}, {4}) when doing batch updates.";

			// Token: 0x040011EC RID: 4588
			internal const string TCE_StreamNotSupportOnEncryptedColumn = "Retrieving encrypted column '{0}' as a {1} is not supported.";

			// Token: 0x040011ED RID: 4589
			internal const string TCE_SequentialAccessNotSupportedOnEncryptedColumn = "Retrieving encrypted column '{0}' with {1} is not supported.";

			// Token: 0x040011EE RID: 4590
			internal const string TCE_CanOnlyCallOnce = "Key store providers cannot be set more than once.";

			// Token: 0x040011EF RID: 4591
			internal const string TCE_NullCustomKeyStoreProviderDictionary = "Column encryption key store provider dictionary cannot be null. Expecting a non-null value.";

			// Token: 0x040011F0 RID: 4592
			internal const string TCE_InvalidCustomKeyStoreProviderName = "Invalid key store provider name '{0}'. '{1}' prefix is reserved for system key store providers.";

			// Token: 0x040011F1 RID: 4593
			internal const string TCE_NullProviderValue = "Null reference specified for key store provider '{0}'. Expecting a non-null value.";

			// Token: 0x040011F2 RID: 4594
			internal const string TCE_EmptyProviderName = "Invalid key store provider name specified. Key store provider names cannot be null or empty.";

			// Token: 0x040011F3 RID: 4595
			internal const string TCE_SqlCommand_ColumnEncryptionSetting = "Column encryption setting for the command. Overrides the connection level default.";

			// Token: 0x040011F4 RID: 4596
			internal const string TCE_DbConnectionString_ColumnEncryptionSetting = "Default column encryption setting for all the commands on the connection.";

			// Token: 0x040011F5 RID: 4597
			internal const string TCE_SqlParameter_ForceColumnEncryption = "Forces parameter to be encrypted before sending sensitive data to server. ";

			// Token: 0x040011F6 RID: 4598
			internal const string TCE_SqlConnection_TrustedColumnMasterKeyPaths = "Dictionary object containing SQL Server names and their trusted column master key paths.";

			// Token: 0x040011F7 RID: 4599
			internal const string SQLROR_RecursiveRoutingNotSupported = "Two or more redirections have occurred. Only one redirection per login is allowed.";

			// Token: 0x040011F8 RID: 4600
			internal const string SQLROR_FailoverNotSupported = "Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported.";

			// Token: 0x040011F9 RID: 4601
			internal const string SQLROR_UnexpectedRoutingInfo = "Unexpected routing information received.";

			// Token: 0x040011FA RID: 4602
			internal const string SQLROR_InvalidRoutingInfo = "Invalid routing information received.";

			// Token: 0x040011FB RID: 4603
			internal const string SQLROR_TimeoutAfterRoutingInfo = "Server provided routing information, but timeout already expired.";

			// Token: 0x040011FC RID: 4604
			internal const string SQLCR_InvalidConnectRetryCountValue = "Invalid ConnectRetryCount value (should be 0-255).";

			// Token: 0x040011FD RID: 4605
			internal const string SQLCR_InvalidConnectRetryIntervalValue = "Invalid ConnectRetryInterval value (should be 1-60).";

			// Token: 0x040011FE RID: 4606
			internal const string SQLCR_NextAttemptWillExceedQueryTimeout = "Next reconnection attempt will exceed query timeout. Reconnection was terminated.";

			// Token: 0x040011FF RID: 4607
			internal const string SQLCR_EncryptionChanged = "The server did not preserve SSL encryption during a recovery attempt, connection recovery is not possible.";

			// Token: 0x04001200 RID: 4608
			internal const string SQLCR_TDSVestionNotPreserved = "The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible.";

			// Token: 0x04001201 RID: 4609
			internal const string SQLCR_AllAttemptsFailed = "The connection is broken and recovery is not possible.  The client driver attempted to recover the connection one or more times and all attempts failed.  Increase the value of ConnectRetryCount to increase the number of recovery attempts.";

			// Token: 0x04001202 RID: 4610
			internal const string SQLCR_UnrecoverableServer = "The connection is broken and recovery is not possible.  The connection is marked by the server as unrecoverable.  No attempt was made to restore the connection.";

			// Token: 0x04001203 RID: 4611
			internal const string SQLCR_UnrecoverableClient = "The connection is broken and recovery is not possible.  The connection is marked by the client driver as unrecoverable.  No attempt was made to restore the connection.";

			// Token: 0x04001204 RID: 4612
			internal const string SQLCR_NoCRAckAtReconnection = "The server did not acknowledge a recovery attempt, connection recovery is not possible.";

			// Token: 0x04001205 RID: 4613
			internal const string DbConnectionString_PoolBlockingPeriod = "Defines the blocking period behavior for a connection pool.";

			// Token: 0x04001206 RID: 4614
			internal const string AZURESQL_GenericEndpoint = ".database.windows.net";

			// Token: 0x04001207 RID: 4615
			internal const string AZURESQL_GermanEndpoint = ".database.cloudapi.de";

			// Token: 0x04001208 RID: 4616
			internal const string AZURESQL_UsGovEndpoint = ".database.usgovcloudapi.net";

			// Token: 0x04001209 RID: 4617
			internal const string AZURESQL_ChinaEndpoint = ".database.chinacloudapi.cn";

			// Token: 0x0400120A RID: 4618
			internal const string TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled = "Defines whether query metadata caching is enabled.";

			// Token: 0x0400120B RID: 4619
			internal const string TCE_SqlConnection_ColumnEncryptionKeyCacheTtl = "Defines the time-to-live of entries in the column encryption key cache.";

			// Token: 0x0400120C RID: 4620
			internal const string SQL_Timeout_Execution = "Execution Timeout Expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.";

			// Token: 0x0400120D RID: 4621
			internal const string AttestationTokenSignatureValidationFailed = "The validation of an attestation token failed. The token signature does not match the signature omputed using a public key retrieved from the attestation public key endpoint at '{0}'. Verify the DNS apping for the endpoint -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649\u202ffor\u202fmore\u202fdetails. If correct, contact Customer Support Services.";

			// Token: 0x0400120E RID: 4622
			internal const string EnclaveRetrySleepInSecondsValueException = "Internal error occurred when retrying the download of the HGS root certificate after the initial request failed. Contact Customer Support Services.";

			// Token: 0x0400120F RID: 4623
			internal const string EnclaveSessionInvalidationFailed = "Internal error. Unable to invalidate the requested enclave session, because it does not exist in the cache. Contact Customer Support Services.";

			// Token: 0x04001210 RID: 4624
			internal const string ExpiredAttestationToken = "The validation of an attestation token failed. The token received from SQL Server is expired. Contact Customer Support Services.";

			// Token: 0x04001211 RID: 4625
			internal const string FailToCreateEnclaveSession = "Failed to create enclave session as attestation server is busy.";

			// Token: 0x04001212 RID: 4626
			internal const string FailToParseAttestationInfo = "The validation of an attestation information failed. The attestation information has an invalid format. Contact Customer Support Services. Error details: '{0}'.";

			// Token: 0x04001213 RID: 4627
			internal const string FailToParseAttestationToken = "The validation of an attestation token failed. The token has an invalid format. Contact Customer Support Services. Error details: '{0}'.";

			// Token: 0x04001214 RID: 4628
			internal const string GetAttestationSigningCertificateFailedInvalidCertificate = "The attestation service returned an expired HGS root certificate for attestation URL '{0}'. Check the HGS root certificate configured for your HGS instance -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2160553\u202ffor\u202fmore\u202fdetails.";

			// Token: 0x04001215 RID: 4629
			internal const string GetAttestationSigningCertificateRequestFailedFormat = "The obtained HGS root certificate for attestation URL '{0}' has an invalid format. Verify the attestation URL is correct and the HGS server is online and fully initialized -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2160553\u202ffor\u202fmore\u202fdetails. For additional support contact Customer Support Services. Error details: '{1}'.";

			// Token: 0x04001216 RID: 4630
			internal const string GetAttestationTokenSigningKeysFailed = "The validation of an attestation token failed. Cannot retrieve a public key from the attestation public key endpoint, or the retrieved key has an invalid format. Error details: '{0}'.";

			// Token: 0x04001217 RID: 4631
			internal const string GetSharedSecretFailed = "Signature verification of the enclave's Diffie-Hellman key failed. Contact Customer Support Services.";

			// Token: 0x04001218 RID: 4632
			internal const string InvalidArgumentToBase64UrlDecoder = "The validation of an attestation token failed due to an error while decoding the enclave public key obtained from SQL Server. Contact Customer Support Services.";

			// Token: 0x04001219 RID: 4633
			internal const string InvalidArgumentToSHA256 = "The validation of an attestation token failed due to an error while computing a hash of the enclave public key obtained from SQL Server. Contact Customer Support Services.";

			// Token: 0x0400121A RID: 4634
			internal const string InvalidAttestationToken = "The validation of the attestation token has failed during signature validation. Exception: '{0}'.";

			// Token: 0x0400121B RID: 4635
			internal const string InvalidClaimInAttestationToken = "The validation of an attestation token failed. Claim '{0}' in the token has an invalid value of '{1}'. Verify the attestation policy -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649\u202ffor\u202fmore\u202fdetails. If the policy is correct, contact Customer Support Services.";

			// Token: 0x0400121C RID: 4636
			internal const string MissingClaimInAttestationToken = "The validation of the attestation token failed. Claim '{0}' is missing in the token. Verify the attestation policy -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2157649\u202ffor\u202fmore\u202fdetails. If the policy is correct, contact Customer Support Services.";

			// Token: 0x0400121D RID: 4637
			internal const string VerifyEnclaveDebuggable = "Failed to check if the enclave is running in the production mode. Contact Customer Support Services.";

			// Token: 0x0400121E RID: 4638
			internal const string VerifyEnclavePolicyFailedFormat = "Could not verify enclave policy due to a difference between the expected and actual values of the policy on property '{0}'. Actual: '{1}', Expected: '{2}' -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2160553\u202ffor\u202fmore\u202fdetails.";

			// Token: 0x0400121F RID: 4639
			internal const string VerifyEnclaveReportFailed = "Signature verification of the enclave report failed. The report signature does not match the signature computed using the HGS root certificate. Verify the DNS mapping for the endpoint -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2160553 for\u202fmore\u202fdetails. If correct, contact Customer Support Services.";

			// Token: 0x04001220 RID: 4640
			internal const string VerifyEnclaveReportFormatFailed = "The enclave report received from SQL Server is not in the correct format. Contact Customer Support Services.";

			// Token: 0x04001221 RID: 4641
			internal const string VerifyHealthCertificateChainFormat = "Failed to build a chain of trust between the enclave host's health report and the HGS root certificate for attestation URL '{0}' with status: '{1}'. Verify the attestation URL matches the URL configured on the SQL Server -\u202fsee\u202fhttps://go.microsoft.com/fwlink/?linkid=2160553 for\u202fmore\u202fdetails. If both the client and SQL Server use the same attestation service, contact Customer Support Services.";

			// Token: 0x04001222 RID: 4642
			internal const string TCE_DbConnectionString_AttestationProtocol = "Specifies an attestation protocol for its corresponding enclave attestation service.";

			// Token: 0x04001223 RID: 4643
			internal const string TCE_DbConnectionString_IPAddressPreference = "Specifies an IP address preference when connecting to SQL instances.";

			// Token: 0x04001224 RID: 4644
			internal const string TCE_EnclaveTypeNotSupported = "The enclave type '{0}' returned from the server is not supported.";

			// Token: 0x04001225 RID: 4645
			internal const string TCE_AttestationProtocolNotSupportEnclaveType = "Failed to initialize connection. The attestation protocol '{0}' does not support the enclave type '{1}'.";

			// Token: 0x04001226 RID: 4646
			internal const string TCE_AttestationProtocolNotSpecifiedForGeneratingEnclavePackage = "Error occurred when generating enclave package. Attestation Protocol has not been specified in the connection string, but the query requires enclave computations.";

			// Token: 0x04001227 RID: 4647
			internal const string SQLUDT_InvalidSize = "UDT size must be less than {1}, size: {0}";

			// Token: 0x04001228 RID: 4648
			internal const string SEC_ProtocolWarning = "Security Warning: The negotiated {0} is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.";

			// Token: 0x04001229 RID: 4649
			internal const string net_invalid_enum = "The specified value is not valid in the '{0}' enumeration.";

			// Token: 0x0400122A RID: 4650
			internal const string SQL_BulkLoadInvalidOrderHint = "The given column order hint is not valid.";

			// Token: 0x0400122B RID: 4651
			internal const string SQL_BulkLoadOrderHintDuplicateColumn = "The column '{0}' was specified more than once.";

			// Token: 0x0400122C RID: 4652
			internal const string SQL_BulkLoadOrderHintInvalidColumn = "The sorted column '{0}' is not valid in the destination table.";

			// Token: 0x0400122D RID: 4653
			internal const string SQL_BulkLoadUnspecifiedSortOrder = "A column order hint cannot have an unspecified sort order.";

			// Token: 0x0400122E RID: 4654
			internal const string SQL_UnsupportedAuthenticationSpecified = "Unsupported authentication specified in this context: {0}";

			// Token: 0x0400122F RID: 4655
			internal const string SQL_Timeout_Active_Directory_Interactive_Authentication = "Active Directory Interactive authentication timed out. The user took too long to respond to the authentication request.";

			// Token: 0x04001230 RID: 4656
			internal const string SQL_SettingInteractiveWithCredential = "Cannot use 'Authentication=Active Directory Interactive', if the Credential property has been set.";

			// Token: 0x04001231 RID: 4657
			internal const string SQL_SettingCredentialWithInteractive = "Cannot set the Credential property if 'Authentication=Active Directory Interactive' has been specified in the connection string.";

			// Token: 0x04001232 RID: 4658
			internal const string SqlConnection_ServerProcessId = "Server Process Id (SPID) of the active connection.";

			// Token: 0x04001233 RID: 4659
			internal const string SQL_Timeout_Active_Directory_DeviceFlow_Authentication = "Active Directory Device Code Flow authentication timed out. The user took too long to respond to the authentication request.";

			// Token: 0x04001234 RID: 4660
			internal const string SQL_SettingCredentialWithDeviceFlow = "Cannot set the Credential property if 'Authentication=Active Directory Device Code Flow' has been specified in the connection string.";

			// Token: 0x04001235 RID: 4661
			internal const string SQL_SettingCredentialWithNonInteractive = "Cannot set the Credential property if 'Authentication={0}' has been specified in the connection string.";

			// Token: 0x04001236 RID: 4662
			internal const string SQL_SettingDeviceFlowWithCredential = "Cannot use 'Authentication=Active Directory Device Code Flow', if the Credential property has been set.";

			// Token: 0x04001237 RID: 4663
			internal const string SQL_SettingNonInteractiveWithCredential = "Cannot use 'Authentication={0}', if the Credential property has been set.";

			// Token: 0x04001238 RID: 4664
			internal const string SqlDependency_UnexpectedValueOnDeserialize = "Unexpected type detected on deserialize.";

			// Token: 0x04001239 RID: 4665
			internal const string SqlRetryLogic_InvalidRange = "Value '{0}' is out of range. Must be between {1} and {2}.";

			// Token: 0x0400123A RID: 4666
			internal const string SqlRetryLogic_RetryCanceled = "The retry has been canceled at attempt {0}.";

			// Token: 0x0400123B RID: 4667
			internal const string SqlRetryLogic_RetryExceeded = "The number of retries has exceeded the maximum of {0} attempt(s).";

			// Token: 0x0400123C RID: 4668
			internal const string SqlRetryLogic_InvalidMinMaxPair = "'{0}' is not less than '{1}'; '{2}' cannot be greater than '{3}'.";

			// Token: 0x0400123D RID: 4669
			internal const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

			// Token: 0x0400123E RID: 4670
			internal const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

			// Token: 0x0400123F RID: 4671
			internal const string Arg_RemoveArgNotFound = "Cannot remove the specified item because it was not found in the specified Collection.";

			// Token: 0x04001240 RID: 4672
			internal const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

			// Token: 0x04001241 RID: 4673
			internal const string SQL_ParameterDirectionInvalidForOptimizedBinding = "Parameter '{0}' cannot have Direction Output or InputOutput when EnableOptimizedParameterBinding is enabled on the parent command.";

			// Token: 0x04001242 RID: 4674
			internal const string AAD_Token_Retrieving_Timeout = "Connection timed out while retrieving an access token using '{0}' authentication method. Last error: {1}: {2}";

			// Token: 0x04001243 RID: 4675
			internal const string DbConnectionString_FailoverPartnerSPN = "The service principal name (SPN) of the failover partner.";

			// Token: 0x04001244 RID: 4676
			internal const string DbConnectionString_ServerSPN = "The service principal name (SPN) of the server.";

			// Token: 0x04001245 RID: 4677
			internal const string SNI_PlatformNotSupportedNetFx = "The '{0}' platform is not supported when targeting .NET Framework.";
		}
	}
}
