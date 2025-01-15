using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F2C RID: 3884
	internal sealed class AnalysisServicesNativeQueryTableValue : TableValue
	{
		// Token: 0x060066C5 RID: 26309 RVA: 0x00161A7B File Offset: 0x0015FC7B
		public AnalysisServicesNativeQueryTableValue(IEngineHost host, string query, AnalysisServicesService service, Action testConnection)
		{
			this.host = host;
			this.query = query;
			this.service = service;
			this.testConnection = testConnection;
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x00161AA0 File Offset: 0x0015FCA0
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					using (IDataReader dataReader = this.ExecuteCommand(CommandBehavior.SchemaOnly))
					{
						this.InitType(dataReader);
					}
				}
				return this.type;
			}
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00161AE8 File Offset: 0x0015FCE8
		private static Func<IDataReader, int, Value> GetRetrievalFunction(Type type, TypeValue intendedType)
		{
			if (type.FullName == "System.String")
			{
				return new Func<IDataReader, int, Value>(AnalysisServicesNativeQueryTableValue.RetrieveObjectAsText);
			}
			return ValueMarshaller.GetRetrievalFunction(type, intendedType);
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x00161B10 File Offset: 0x0015FD10
		private static Value RetrieveObjectAsText(IDataReader reader, int index)
		{
			object obj = reader[index];
			return TextValue.New((obj == null || obj is string) ? ((string)obj) : obj.ToString());
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x00161B44 File Offset: 0x0015FD44
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IDataReader dataReader = null;
			IEnumerator<IValueReference> enumerator;
			try
			{
				dataReader = this.ExecuteCommand(CommandBehavior.Default);
				if (this.type == null)
				{
					this.InitType(dataReader);
				}
				enumerator = new DbDataReaderEnumerator(dataReader, true, this.type.ItemType, "AnalysisServices", new Func<Type, TypeValue, Func<IDataReader, int, Value>>(AnalysisServicesNativeQueryTableValue.GetRetrievalFunction), null);
			}
			catch
			{
				if (dataReader != null)
				{
					dataReader.Dispose();
				}
				throw;
			}
			return enumerator;
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x00161BB0 File Offset: 0x0015FDB0
		public override IPageReader GetReader()
		{
			return new DataReaderPageReader(this.ExecuteCommand(CommandBehavior.Default));
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x00161BBE File Offset: 0x0015FDBE
		public override void TestConnection()
		{
			this.testConnection();
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x00161BCC File Offset: 0x0015FDCC
		private IDataReader ExecuteCommand(CommandBehavior behavior)
		{
			HostResourceQueryPermissionService.VerifyQueryPermission(this.host, this.service.Resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, this.query);
			return this.service.ExecuteCommand(this.service.DataCache, behavior, this.query, Array.Empty<KeyValuePair<string, object>>());
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x00161C18 File Offset: 0x0015FE18
		private void InitType(IDataReader reader)
		{
			this.type = DataReaderSchemaTableTableTypeValue.New(reader);
		}

		// Token: 0x04003884 RID: 14468
		private readonly IEngineHost host;

		// Token: 0x04003885 RID: 14469
		private readonly string query;

		// Token: 0x04003886 RID: 14470
		private readonly AnalysisServicesService service;

		// Token: 0x04003887 RID: 14471
		private readonly Action testConnection;

		// Token: 0x04003888 RID: 14472
		private TableTypeValue type;
	}
}
