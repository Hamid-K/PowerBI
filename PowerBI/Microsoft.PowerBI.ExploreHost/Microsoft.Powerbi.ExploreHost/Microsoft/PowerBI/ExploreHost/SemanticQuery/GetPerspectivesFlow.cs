using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry.PIIUtils;
using Microsoft.ReportingServices.Common;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000041 RID: 65
	internal sealed class GetPerspectivesFlow : ExploreClientHandlerBaseFlow
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00006B8C File Offset: 0x00004D8C
		internal GetPerspectivesFlow(ExploreClientHandlerContext context, IPowerViewHandler powerViewHandler, string databaseID)
			: base(context, databaseID)
		{
			this.powerViewHandler = powerViewHandler;
			this.databaseID = databaseID;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006BA4 File Offset: 0x00004DA4
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00006BAC File Offset: 0x00004DAC
		public string SerializedPerspectivesInfo { get; private set; }

		// Token: 0x06000222 RID: 546 RVA: 0x00006BB8 File Offset: 0x00004DB8
		protected override void InternalRun()
		{
			ExploreHostServiceErrorExtractorFactory instance = ExploreHostServiceErrorExtractorFactory.Instance;
			ExploreHostDataSourceInfo dataSource = this.powerViewHandler.GetActiveSession(this.databaseID).GetDataSource();
			string databaseName = dataSource.DatabaseName;
			string cubeName = dataSource.CubeName;
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "CATALOG_NAME", databaseName },
				{ "CUBE_NAME", cubeName }
			};
			DataSet dataSet;
			ServiceError serviceError;
			bool flag = ExploreHostUtils.TryGetSchemaDataSet(this.Context, "MDSCHEMA_CUBES", instance.Create(), base.DatabaseID, dictionary, out dataSet, out serviceError);
			try
			{
				PerspectivesInfoResponse perspectivesInfoResponse = new PerspectivesInfoResponse();
				if (!flag)
				{
					perspectivesInfoResponse.PerspectivesInfo = null;
					perspectivesInfoResponse.Error = serviceError;
				}
				else
				{
					if (dataSet.Tables.Count == 0)
					{
						throw ExploreHostUtils.CreateAndTracePowerBIExploreException("ErrorGettingPerspectives", "Schema contains zero cubes", null, ErrorSource.PowerBI, this.powerViewHandler.GetModelLocation(this.databaseID), ServiceErrorStatusCode.GeneralError);
					}
					PerspectivesInfo perspectivesInfo = ExtensionPerspectivesBuilder.BuildPerspectivesList(dataSet.Tables[0], cubeName);
					perspectivesInfoResponse.PerspectivesInfo = perspectivesInfo;
				}
				this.SerializedPerspectivesInfo = GetPerspectivesFlow.SerializePerspectivesInfo(perspectivesInfoResponse);
			}
			catch (PowerBIExploreException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw ExploreHostUtils.CreateAndTracePowerBIExploreException("ErrorGettingPerspectives", ex.Message.MarkAsCustomerContent(), ex, ErrorSource.PowerBI, this.powerViewHandler.GetModelLocation(this.databaseID), ServiceErrorStatusCode.GeneralError);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006D04 File Offset: 0x00004F04
		private static string SerializePerspectivesInfo(PerspectivesInfoResponse perspectivesInfo)
		{
			string text2;
			try
			{
				string text = JsonConvert.SerializeObject(perspectivesInfo);
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArgumentException("Newtonsoft failed to serialize PerspectivesInfo.");
				}
				text2 = text;
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to serialize PerspectivesInfo to JSON.", ex);
			}
			return text2;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006D4C File Offset: 0x00004F4C
		public static string CreatePerspectivesInfoResponseFromException(Exception e, ServiceErrorStatusCode statusCode, string powerBIErrorCode = null)
		{
			return GetPerspectivesFlow.SerializePerspectivesInfo(PerspectivesInfoResponse.CreatePerspectivesInfoResponseFromException(e, statusCode, ExploreHostServiceErrorExtractorFactory.Instance.Create(), powerBIErrorCode));
		}

		// Token: 0x040000D0 RID: 208
		private IPowerViewHandler powerViewHandler;

		// Token: 0x040000D1 RID: 209
		private readonly string databaseID;

		// Token: 0x040000D2 RID: 210
		private const string CATALOG_NAME = "CATALOG_NAME";

		// Token: 0x040000D3 RID: 211
		private const string CUBE_NAME = "CUBE_NAME";

		// Token: 0x040000D4 RID: 212
		private const string MDSCHEMA_CUBES = "MDSCHEMA_CUBES";
	}
}
