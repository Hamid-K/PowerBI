using System;
using System.CodeDom.Compiler;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Data.Entity.SqlServer.Resources
{
	// Token: 0x02000043 RID: 67
	[GeneratedCode("Resources.SqlServer.tt", "1.0.0.0")]
	internal sealed class EntityRes
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0001A58E File Offset: 0x0001878E
		private EntityRes()
		{
			this.resources = new ResourceManager("System.Data.Entity.SqlServer.Properties.Resources.SqlServer", typeof(SqlProviderServices).Assembly());
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001A5B8 File Offset: 0x000187B8
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes entityRes = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, entityRes, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001A5E4 File Offset: 0x000187E4
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001A5E7 File Offset: 0x000187E7
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001A5F4 File Offset: 0x000187F4
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
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
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001A674 File Offset: 0x00018874
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001A69D File Offset: 0x0001889D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001A6A8 File Offset: 0x000188A8
		public static object GetObject(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetObject(name, EntityRes.Culture);
		}

		// Token: 0x0400012A RID: 298
		internal const string ArgumentIsNullOrWhitespace = "ArgumentIsNullOrWhitespace";

		// Token: 0x0400012B RID: 299
		internal const string SqlProvider_GeographyValueNotSqlCompatible = "SqlProvider_GeographyValueNotSqlCompatible";

		// Token: 0x0400012C RID: 300
		internal const string SqlProvider_GeometryValueNotSqlCompatible = "SqlProvider_GeometryValueNotSqlCompatible";

		// Token: 0x0400012D RID: 301
		internal const string ProviderReturnedNullForGetDbInformation = "ProviderReturnedNullForGetDbInformation";

		// Token: 0x0400012E RID: 302
		internal const string ProviderDoesNotSupportType = "ProviderDoesNotSupportType";

		// Token: 0x0400012F RID: 303
		internal const string NoStoreTypeForEdmType = "NoStoreTypeForEdmType";

		// Token: 0x04000130 RID: 304
		internal const string Mapping_Provider_WrongManifestType = "Mapping_Provider_WrongManifestType";

		// Token: 0x04000131 RID: 305
		internal const string ADP_InternalProviderError = "ADP_InternalProviderError";

		// Token: 0x04000132 RID: 306
		internal const string UnableToDetermineStoreVersion = "UnableToDetermineStoreVersion";

		// Token: 0x04000133 RID: 307
		internal const string SqlProvider_NeedSqlDataReader = "SqlProvider_NeedSqlDataReader";

		// Token: 0x04000134 RID: 308
		internal const string SqlProvider_Sql2008RequiredForSpatial = "SqlProvider_Sql2008RequiredForSpatial";

		// Token: 0x04000135 RID: 309
		internal const string SqlProvider_SqlTypesAssemblyNotFound = "SqlProvider_SqlTypesAssemblyNotFound";

		// Token: 0x04000136 RID: 310
		internal const string SqlProvider_IncompleteCreateDatabase = "SqlProvider_IncompleteCreateDatabase";

		// Token: 0x04000137 RID: 311
		internal const string SqlProvider_IncompleteCreateDatabaseAggregate = "SqlProvider_IncompleteCreateDatabaseAggregate";

		// Token: 0x04000138 RID: 312
		internal const string SqlProvider_DdlGeneration_MissingInitialCatalog = "SqlProvider_DdlGeneration_MissingInitialCatalog";

		// Token: 0x04000139 RID: 313
		internal const string SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog = "SqlProvider_DdlGeneration_CannotDeleteDatabaseNoInitialCatalog";

		// Token: 0x0400013A RID: 314
		internal const string SqlProvider_DdlGeneration_CannotTellIfDatabaseExists = "SqlProvider_DdlGeneration_CannotTellIfDatabaseExists";

		// Token: 0x0400013B RID: 315
		internal const string SqlProvider_CredentialsMissingForMasterConnection = "SqlProvider_CredentialsMissingForMasterConnection";

		// Token: 0x0400013C RID: 316
		internal const string SqlProvider_InvalidGeographyColumn = "SqlProvider_InvalidGeographyColumn";

		// Token: 0x0400013D RID: 317
		internal const string SqlProvider_InvalidGeometryColumn = "SqlProvider_InvalidGeometryColumn";

		// Token: 0x0400013E RID: 318
		internal const string Mapping_Provider_WrongConnectionType = "Mapping_Provider_WrongConnectionType";

		// Token: 0x0400013F RID: 319
		internal const string Update_NotSupportedServerGenKey = "Update_NotSupportedServerGenKey";

		// Token: 0x04000140 RID: 320
		internal const string Update_NotSupportedIdentityType = "Update_NotSupportedIdentityType";

		// Token: 0x04000141 RID: 321
		internal const string Update_SqlEntitySetWithoutDmlFunctions = "Update_SqlEntitySetWithoutDmlFunctions";

		// Token: 0x04000142 RID: 322
		internal const string Cqt_General_UnsupportedExpression = "Cqt_General_UnsupportedExpression";

		// Token: 0x04000143 RID: 323
		internal const string SqlGen_ApplyNotSupportedOnSql8 = "SqlGen_ApplyNotSupportedOnSql8";

		// Token: 0x04000144 RID: 324
		internal const string SqlGen_NiladicFunctionsCannotHaveParameters = "SqlGen_NiladicFunctionsCannotHaveParameters";

		// Token: 0x04000145 RID: 325
		internal const string SqlGen_InvalidDatePartArgumentExpression = "SqlGen_InvalidDatePartArgumentExpression";

		// Token: 0x04000146 RID: 326
		internal const string SqlGen_InvalidDatePartArgumentValue = "SqlGen_InvalidDatePartArgumentValue";

		// Token: 0x04000147 RID: 327
		internal const string SqlGen_TypedNaNNotSupported = "SqlGen_TypedNaNNotSupported";

		// Token: 0x04000148 RID: 328
		internal const string SqlGen_TypedPositiveInfinityNotSupported = "SqlGen_TypedPositiveInfinityNotSupported";

		// Token: 0x04000149 RID: 329
		internal const string SqlGen_TypedNegativeInfinityNotSupported = "SqlGen_TypedNegativeInfinityNotSupported";

		// Token: 0x0400014A RID: 330
		internal const string SqlGen_PrimitiveTypeNotSupportedPriorSql10 = "SqlGen_PrimitiveTypeNotSupportedPriorSql10";

		// Token: 0x0400014B RID: 331
		internal const string SqlGen_CanonicalFunctionNotSupportedPriorSql10 = "SqlGen_CanonicalFunctionNotSupportedPriorSql10";

		// Token: 0x0400014C RID: 332
		internal const string SqlGen_ParameterForLimitNotSupportedOnSql8 = "SqlGen_ParameterForLimitNotSupportedOnSql8";

		// Token: 0x0400014D RID: 333
		internal const string SqlGen_ParameterForSkipNotSupportedOnSql8 = "SqlGen_ParameterForSkipNotSupportedOnSql8";

		// Token: 0x0400014E RID: 334
		internal const string Spatial_WellKnownGeographyValueNotValid = "Spatial_WellKnownGeographyValueNotValid";

		// Token: 0x0400014F RID: 335
		internal const string Spatial_WellKnownGeometryValueNotValid = "Spatial_WellKnownGeometryValueNotValid";

		// Token: 0x04000150 RID: 336
		internal const string SqlSpatialServices_ProviderValueNotSqlType = "SqlSpatialServices_ProviderValueNotSqlType";

		// Token: 0x04000151 RID: 337
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid = "SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid";

		// Token: 0x04000152 RID: 338
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt = "SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt";

		// Token: 0x04000153 RID: 339
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid = "SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid";

		// Token: 0x04000154 RID: 340
		internal const string SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt = "SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt";

		// Token: 0x04000155 RID: 341
		internal const string TransientExceptionDetected = "TransientExceptionDetected";

		// Token: 0x04000156 RID: 342
		internal const string ELinq_DbFunctionDirectCall = "ELinq_DbFunctionDirectCall";

		// Token: 0x04000157 RID: 343
		internal const string AutomaticMigration = "AutomaticMigration";

		// Token: 0x04000158 RID: 344
		internal const string InvalidDatabaseName = "InvalidDatabaseName";

		// Token: 0x04000159 RID: 345
		internal const string SqlServerMigrationSqlGenerator_UnknownOperation = "SqlServerMigrationSqlGenerator_UnknownOperation";

		// Token: 0x0400015A RID: 346
		private static EntityRes loader;

		// Token: 0x0400015B RID: 347
		private readonly ResourceManager resources;
	}
}
